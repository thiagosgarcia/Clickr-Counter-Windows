using System;
using System.Collections.Generic;
using System.Text;

namespace Clickr
{
    public abstract class Commands
    {

        public static void Rate()
        {
            Windows.System.Launcher.LaunchUriAsync(
                new Uri("ms-windows-store:reviewapp?appid=4b4ad23b-5625-40fa-82a7-59f9e8e67f01"));
        }

        public string Email { get { return "tgarcia@thiagogarcia.net"; } }

        public abstract void Mail();
    }
}
