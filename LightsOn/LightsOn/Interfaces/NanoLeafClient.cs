using LightsOn.Settings;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Xamarin.Essentials;

namespace LightsOn.Interfaces
{

    class NanoLeafClient : INanoLeafClient
    {
        string Token;
        public NanoLeafClient()
        {
            //getAuthorizationToken();
        }

        public async void LoadAuhtTokenFromSettings()
        {
            foreach (var element in NanoLeafSettings.NanoLeafDevices)
            {
                element.Token = await SecureStorage.GetAsync(element.Name).ConfigureAwait(true);
            }
        }

        public string PostCommand(Uri url)
        {
            //HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            //HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            //Stream resStream = response.GetResponseStream();

            //return response;

            // Create a request using a URL that can receive a post.   
            WebRequest request = WebRequest.Create(url);
            // Set the Method property of the request to POST.  
            request.Method = "POST";

            // Create POST data and convert it to a byte array.  
            string postData = "This is a test that posts this string to a Web server.";
            byte[] byteArray = Encoding.UTF8.GetBytes(postData);

            // Set the ContentType property of the WebRequest.  
            request.ContentType = "application/x-www-form-urlencoded";
            // Set the ContentLength property of the WebRequest.  
            request.ContentLength = byteArray.Length;

            // Get the request stream.  
            Stream dataStream = request.GetRequestStream();
            // Write the data to the request stream.  
            dataStream.Write(byteArray, 0, byteArray.Length);
            // Close the Stream object.  
            dataStream.Close();

            // Get the response.  
            WebResponse response = request.GetResponse();
            // Display the status.  
            Console.WriteLine(((HttpWebResponse)response).StatusDescription);

            // Get the stream containing content returned by the server.  
            // The using block ensures the stream is automatically closed.
            using (dataStream = response.GetResponseStream())
            {
                // Open the stream using a StreamReader for easy access.  
                StreamReader reader = new StreamReader(dataStream);
                // Read the content.  
                string responseFromServer = reader.ReadToEnd();
                // Display the content.  
                Console.WriteLine(responseFromServer);

                string[] separatingStrings = { "\""};

                string[] words = responseFromServer.Split(separatingStrings, System.StringSplitOptions.RemoveEmptyEntries);
                System.Console.WriteLine($"{words.Length} substrings in text:");

                reader.Dispose();

                // Close the response.  
                response.Close();


                return words[3];
            }




        }

        public string PutCommand(System.Uri url)
        {
            //HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            //HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            //Stream resStream = response.GetResponseStream();

            //return response;

            // Create a request using a URL that can receive a post.   
            WebRequest request = WebRequest.Create(url + Token + "/state");
            // Set the Method property of the request to POST.  
            request.Method = "PUT";

            // Create POST data and convert it to a byte array.  
            string postData = "{\"on\" : {\"value\":false}}";
            byte[] byteArray = Encoding.UTF8.GetBytes(postData);

            // Set the ContentType property of the WebRequest.  
            request.ContentType = "application/x-www-form-urlencoded";
            // Set the ContentLength property of the WebRequest.  
            request.ContentLength = byteArray.Length;

            // Get the request stream.  
            Stream dataStream = request.GetRequestStream();
            // Write the data to the request stream.  
            dataStream.Write(byteArray, 0, byteArray.Length);
            // Close the Stream object.  
            dataStream.Close();

            // Get the response.  
            WebResponse response = request.GetResponse();
            // Display the status.  
            Console.WriteLine(((HttpWebResponse)response).StatusDescription);

            // Get the stream containing content returned by the server.  
            // The using block ensures the stream is automatically closed.
            using (dataStream = response.GetResponseStream())
            {
                // Open the stream using a StreamReader for easy access.  
                StreamReader reader = new StreamReader(dataStream);
                // Read the content.  
                string responseFromServer = reader.ReadToEnd();
                // Display the content.  
                Console.WriteLine(responseFromServer);

                string[] separatingStrings = { "\"" };

                string[] words = responseFromServer.Split(separatingStrings, System.StringSplitOptions.RemoveEmptyEntries);
                System.Console.WriteLine($"{words.Length} substrings in text:");
                
                reader.Dispose();

                // Close the response.  
                response.Close();


                return words[3];
            }



        }

        public void SwitchNanoLeaf(bool state, NanoLeafSettings.NanoLeafDevice Device)
        {
            var client = new RestClient("http://" + Device.Address + ":16021/api/v1/"+ Device.Token + "/state");
            var request = new RestRequest(Method.PUT);
            if(state==false)
                request.AddParameter("undefined", "{\"on\" : {\"value\":false}}", ParameterType.RequestBody);
            else
                request.AddParameter("undefined", "{\"on\" : {\"value\":true}}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
        }

        public void getAuthorizationToken(string Name)
        {
            foreach (var element in NanoLeafSettings.NanoLeafDevices)
            {
                if (element.Name == Name)
                {
                    var AuthToken = PostCommand(new Uri("http://" + element.Address + ":16021/api/v1/new"));
                    SecureStorage.SetAsync(element.Name, AuthToken).ConfigureAwait(true);
                    element.Token = Token;
                }
            }
        }



        //public async Task<bool> SaveAuthorizationToken(string Ip)
        //{
        //    return false;
        //    ////var AuthToken = getAuthorizationToken(Ip);
        //    //try
        //    //{
        //    //    await SecureStorage.SetAsync("token", AuthToken).ConfigureAwait(true);
        //    //    return true;
        //    //}
        //    //catch (TimeoutException ex)
        //    //{
        //    //    Debug.WriteLine("Error", "Could not Connect to Server" + ex);
        //    //    return false;
        //    //}
        //    //catch (Exception ex)
        //    //{
        //    //    Debug.WriteLine("Error", "Could not safe AuthToken" + ex);
        //    //    return false;
        //    //    throw;
        //    //}
          
        //}

        //public async Task<string> LoadAuthorizationToken(string Ip)
        //{
        //    return "false";
        //    //try
        //    //{
        //    //    var Token = await SecureStorage.GetAsync("token").ConfigureAwait(true);
        //    //    if (Token == "0")
        //    //    {
        //    //        Token = getAuthorizationToken(Ip);
        //    //        return Token;
        //    //    }
        //    //    else if (Token != null)
        //    //    {
        //    //        return Token;
        //    //    }
        //    //    else
        //    //        return Token;
        //    //}
        //    //catch (PlatformNotSupportedException ex)
        //    //{
        //    //    Debug.WriteLine("Error", "Could not safe AuthToken" + ex);
        //    //    // Possible that device doesn't support secure storage on device.
        //    //    return null;
        //    //}
        //    //catch (Exception ex)
        //    //{
        //    //    Debug.WriteLine("Error", "Could not safe AuthToken" + ex);
        //    //    return null;
        //    //    throw;
        //    //}
        //}

    }
}
