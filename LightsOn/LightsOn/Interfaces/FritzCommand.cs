using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Xml.Linq;

namespace LightsOn.Interfaces
{
    enum FritzCommands
    {
        getswitchlist, setswitchon, setswitchof, setswitchtoggle, getswitchstate,
        getswitchpresent, getswitchpower, getswitchenergy, getswitchname, getdevicelistinfos, gettemperature,
        gethkrtsoll, gethkrkomfort, gethkrabsenk, sethkrtsoll, getbasicdevicestats, gettemplatelistinfos,
        applytemplate
    }
    class FritzCommand : IFritzCommand
    {

        public string GetSessionID(string user, string password)
        {
            try
            {
                XDocument doc = XDocument.Load(@"http://fritz.box/login_sid.lua");
                string sid = GetValue(doc, "SID");
                if (sid == "0000000000000000")
                {
                    string challenge = GetValue(doc, "Challenge");
                    string uri = @"http://fritz.box/login_sid.lua?username=" +
                    user + @"&response=" + GetResponse(challenge, password);
                    doc = XDocument.Load(uri);
                    sid = GetValue(doc, "SID");
                }
                return sid;
            }
            catch (WebException ex)
            {
                Debug.WriteLine("Web Error: " + ex);
                return "error";
            }
            catch (TimeoutException ex)
            {
                Debug.WriteLine("Could not connect: " + ex);
                return "error";
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                throw;
            }
        }

        public string GetResponse(string challenge, string kennwort)
        {
            return challenge + "-" + GetMD5Hash(challenge + "-" + kennwort);
        }

        public string GetMD5Hash(string input)
        {
            //var hashAlg = SHA256.Create();
            MD5 hashAlg = MD5.Create();
            byte[] data = hashAlg.ComputeHash(Encoding.Unicode.GetBytes(input));
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sb.Append(data[i].ToString("x2", new CultureInfo("de-DE")));
            }
            hashAlg.Dispose();
            return sb.ToString();
        }

        public string GetValue(XDocument doc, string name)
        {
            XElement info = doc.FirstNode as XElement;
            return info.Element(name).Value;
        }
        XDocument aswerdoc = new XDocument();

        public HttpWebResponse DectCommand(string ain, string switchcmd, string sid)
        {
            try
            {
                string url = "http://fritz.box/webservices/homeautoswitch.lua?ain=" + ain + "&switchcmd=" + switchcmd + "&sid=" + sid;
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream resStream = response.GetResponseStream();
                return response;
            }
            catch (WebException ex)
            {
                Debug.WriteLine("Web Error: " + ex);
                return null;
            }
            catch (TimeoutException ex)
            {
                Debug.WriteLine("Could not connect: " + ex);
                return null;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                throw;
            }
        }

        public string GetDectTemp(string ain, string sid)
        {
            HttpWebResponse response = DectCommand(ain, nameof(FritzCommands.gettemperature), sid);
            Stream resStream;
            if (response != null)
            {
                resStream = response.GetResponseStream();

                // Get the stream containing content returned by the server.  
                // The using block ensures the stream is automatically closed.
                using (resStream = response.GetResponseStream())
                {
                    // Open the stream using a StreamReader for easy access.  
                    StreamReader reader = new StreamReader(resStream);
                    // Read the content.  
                    string responseFromServer = reader.ReadToEnd();
                    // Display the content.  
                    Console.WriteLine(responseFromServer);

                    reader.Dispose();

                    // Close the response.  
                    response.Close();

                    responseFromServer = responseFromServer.Insert(2, ",");
                    responseFromServer = responseFromServer.Insert(4, "°C");

                    return responseFromServer;
                }
            }
            else
                return "No Connection";

        }
        public string GetDectTempStatistic(string ain, string switchcmd, string sid)
        {
            Stream resStream;
            HttpWebResponse response = DectCommand(ain, switchcmd, sid);
            if (response != null)
            {
                resStream = response.GetResponseStream();
                aswerdoc = XDocument.Load(resStream);
                string AnswerString = aswerdoc.Root.Element("temperature").Element("stats").Value;

                List<string> Tempertaturs = new List<string>();
                string[] words = AnswerString.Split(',');

                foreach (var word in words)
                {
                    if (word != "-")
                        Tempertaturs.Add(word.Insert(2, ","));

                }
                return Tempertaturs[0];
            }
            else
                return "no connection";
        }
    }
}
