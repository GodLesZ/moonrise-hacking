using System;
using System.Diagnostics;
using System.Net.Mime;
using Steamworks;

namespace GodLesZ.Moonrise.MoonBot.Library.Login
{
    public class SteamLogin
    {

        public SteamLogin()
        {
            InitSteam();
        }

        private void InitSteam()
        {
            if (!Packsize.Test()) {
                throw new SystemException("[Steamworks.NET] Packsize Test returned false, the wrong version of Steamworks.NET is being run in this platform.");
            }

            if (!DllCheck.Test()) {
                throw new SystemException("[Steamworks.NET] DllCheck Test returned false, One or more of the Steamworks binaries seems to be the wrong version.");
            }

             if (!SteamAPI.Init()) {
                 throw new SystemException("[Steamworks.NET] SteamAPI_Init() failed. Refer to Valve's documentation or the comment above this line for more information.", this);
            }
        }

        public string GetSteamSessionTicket()
        {
            uint num;
            byte[] numArray = new byte[1024];
            SteamUser.GetAuthSessionTicket(numArray, 1024, out num);
            string str = BitConverter.ToString(numArray, 0, (int)num).Replace("-", string.Empty);
            Debug.WriteLine(string.Format("SteamLogin, steamSessionTicket={0}", str));
            MessageSend messageSend = new MessageSend(GameCli.Send);
            int sLoginTransId = GameCli.s_loginTransId;
            AppId_t appID = SteamUtils.GetAppID();
        }

    }
}