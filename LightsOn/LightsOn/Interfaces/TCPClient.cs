using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using static LightsOn.Settings.TcpSettings;

namespace LightsOnXamerin.Interfaces
{
    public class TCPClient : ITCPClient
    {
        public TCPClient()
        {

        }


        private bool SendTcpCommand(string server, string message)
        {

            try
            {
                // Create a TcpClient.
                // Note, for this client to work you need to have a TcpServer 
                // connected to the same address as specified by the server, port
                // combination.
                Int32 port = 1337;
                TcpClient client = new TcpClient(server, port);

                // Translate the passed message into ASCII and store it as a Byte array.
                Byte[] data = System.Text.Encoding.ASCII.GetBytes(message);

                // Get a client stream for reading and writing.
                //  Stream stream = client.GetStream();

                NetworkStream stream = client.GetStream();

                // Send the message to the connected TcpServer. 
                stream.Write(data, 0, data.Length);

                Debug.WriteLine("Sent: {0}", message);

                // Receive the TcpServer.response.

                // Buffer to store the response bytes.
                data = new Byte[256];

                // String to store the response ASCII representation.
                String responseData = String.Empty;

                // Read the first batch of the TcpServer response bytes.
                //Int32 bytes = stream.Read(data, 0, data.Length);
                //responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
                //Console.WriteLine("Received: {0}", responseData);

                // Close everything.
                stream.Close();
                client.Close();


            }
            catch (TimeoutException ex)
            {
                Debug.WriteLine("Could not connect to server"+ ex);
            }


            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                throw;
            }

            return false;

        }

        public bool SendTcpOffCommand(string server, int Fs20Address)
        {
            return SendTcpCommand(server, "FS20;" + Fs20Address + ";" + TcpFS20CommandList.OFF + "\n");
        }

        public bool SendTcpOffCommand(string server)
        {
            return SendTcpCommand(server, "LED;" + 0 + ";" + 0 + ";" + 0 + "\n");
        }

        public bool SendTcpCommand(string server,int Fs20Address, string Fs20Command)
        {
            return SendTcpCommand(server, "FS20;" + Fs20Address + ";" + Fs20Command + "\n");
        }

        public bool SendTcpCommand(string server, double Red, double Green, double Blue)
        {
            return SendTcpCommand(server, "LED;" + Red + ";" + Green+ ";" + Blue + "\n");
        }

        public bool SendTcpCommand(string server, int IrCommand)
        {
            return SendTcpCommand(server, "IR;" + IrCommand + "\n");
        }


    }

}
