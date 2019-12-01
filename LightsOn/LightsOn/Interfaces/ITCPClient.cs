using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace LightsOnXamerin.Interfaces
{
    public interface ITCPClient
    {
        // methods
        #region Connect
        //bool SendTcpCommand(string server, string message);
        #endregion
        bool SendTcpOffCommand(string server, int Fs20Address);
        bool SendTcpOffCommand(string server);
        bool SendTcpCommand(string server, int Fs20Address, string Fs20Command);
        bool SendTcpCommand(string server, double Red, double Green, double Blue);
        bool SendTcpCommand(string server, int IrCommand);
    }
}
