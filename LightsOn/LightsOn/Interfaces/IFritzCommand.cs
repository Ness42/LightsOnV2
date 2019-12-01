using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Xml.Linq;

namespace LightsOn.Interfaces
{
    public interface IFritzCommand
    {
        #region get SessionID
        string GetSessionID(string user, string password);
        #endregion

        string GetResponse(string challenge, string kennwort);
        string GetMD5Hash(string input);
        string GetValue(XDocument doc, string name);

        HttpWebResponse DectCommand(string ain, string switchcmd, string sid);
        string GetDectTemp(string ain, string sid);
        string GetDectTempStatistic(string ain, string switchcmd, string sid);

    }
}
