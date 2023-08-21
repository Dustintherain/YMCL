﻿using Newtonsoft.Json;
using Panuon.WPF.UI;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using YMCL.Class;
using YMCL.Pages.Forms;

namespace YMCL
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public class Global
    {
        public static MusicPlayer form_musicplayer = Application.Current.Windows.Cast<WindowX>()
        .FirstOrDefault(window => window is MusicPlayer) as MusicPlayer; 
        public static MainWindow form_main = Application.Current.Windows.Cast<WindowX>()
        .FirstOrDefault(window => window is MainWindow) as MainWindow;
    }
    public partial class App : Application
    {
        
        class MinecraftPathCalss
        {
            public string? MCPath;
        }
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            
            //入口点
            if (!Directory.Exists("./YMCL"))
            {
                System.IO.DirectoryInfo directoryInfo = new System.IO.DirectoryInfo("./YMCL");
                directoryInfo.Create();
            }
            if (!Directory.Exists("./YMCL/Temp"))
            {
                System.IO.DirectoryInfo directoryInfo = new System.IO.DirectoryInfo("./YMCL/Temp");
                directoryInfo.Create();
            }
            if (!Directory.Exists("./YMCL/Accounts"))
            {
                System.IO.DirectoryInfo directoryInfo = new System.IO.DirectoryInfo("./YMCL/Accounts");
                directoryInfo.Create();
            }

            if (!File.Exists("./YMCL/YMCL.Setting.json"))
            {
                var obj = new SettingInfo()
                {
                    AloneCore = true,
                    DisplayInformation = "True",
                    Java = "Null",
                    LoginIndex = "0",
                    LoginName = "Steve",
                    LoginType = "离线登录",
                    MaxMem = 1024,
                    Theme = "Light",
                    DownloadSoure = "Mcbbs",
                    MaxDownloadThreads = "64",
                    MinecraftPath = ".minecraft",
                    MainWindowHeight = 521,
                    MainWindowWidth = 900,
                    PlayerWindowHeight = 521,
                    PlayerWindowWidth = 900,
                    PlayerVolume = 0.5,
                    ThemeColor = Color.FromArgb(255, 0, 120, 215)
                };
                File.WriteAllText("./YMCL/YMCL.Setting.json", JsonConvert.SerializeObject(obj, Formatting.Indented));
            }
            if (!File.Exists("./YMCL/YMCL.MinecraftPath.json"))
            {
                List<string> MinecraftPathList = new List<string> { ".minecraft" };
                File.WriteAllText("./YMCL/YMCL.MinecraftPath.json", JsonConvert.SerializeObject(MinecraftPathList, Formatting.Indented));
            }
            if (!File.Exists("./YMCL/YMCL.PlayList.json"))
            {
                File.WriteAllText("./YMCL/YMCL.PlayList.json", "");
            }
        }
    }
}
