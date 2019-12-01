using LightsOn.Settings;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace LightsOn.Interfaces
{
    public interface INanoLeafClient
    {
        //#region get SessionID
        //string GetSessionID(string user, string password);
        //#endregion

        //string GetResponse(string challenge, string kennwort);
        //string GetMD5Hash(string input);
        //string GetValue(XDocument doc, string name);

        string PostCommand(System.Uri url);
        string PutCommand(System.Uri url);

        void SwitchNanoLeaf(bool state, NanoLeafSettings.NanoLeafDevice Device);

        void LoadAuhtTokenFromSettings();

        void getAuthorizationToken(string Name);

        //Task<bool> SaveAuthorizationToken(string Ip);
        //Task<string> LoadAuthorizationToken(string Ip);

        //string GetDectTemp(string ain, string sid);
        //string GetDectTempStatistic(string ain, string sid);

    }
}
