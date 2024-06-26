﻿using YMCL.Main.UI.Initialize;
using YMCL.Main.UI.Main;
using YMCL.Main.UI.MusicPlayer;
using YMCL.Main.UI.MusicPlayer.DesktopLyric;
using YMCL.Main.UI.MusicPlayer.Main;
using YMCL.Main.UI.TaskManage.TaskCenter;

namespace YMCL.Main.Public
{
    public class Const
    {
        public class Window
        {
            public static MainWindow main { get; set; }
            public static InitializeWindow initialize { get; set; }
            public static Tasks tasks { get; set; }
            public static MusicPlayer musicPlayer { get; set; }
            public static DesktopLyric desktopLyric { get; set; }
            static Window()
            {
                foreach (System.Windows.Window window in System.Windows.Application.Current.Windows)
                {
                    if (window.GetType() == typeof(InitializeWindow))
                    {
                        initialize = window as InitializeWindow;
                    }
                    if (window.GetType() == typeof(MainWindow))
                    {
                        main = window as MainWindow;
                    }
                    if (window.GetType() == typeof(Tasks))
                    {
                        tasks = window as Tasks;
                    }
                    if (window.GetType() == typeof(MusicPlayer))
                    {
                        musicPlayer = window as MusicPlayer;
                    }
                    if (window.GetType() == typeof(DesktopLyric))
                    {
                        desktopLyric = window as DesktopLyric;
                    }
                }
            }
        }

        public static string Version { get; } = "1.0.0.202402251126_Alpha";
        public static string UpdaterId { get; } = "97B62D3AD1724EFA9AFC7A8D8971BBB1";
        public static string AzureClientId { get; } = "c06d4d68-7751-4a8a-a2ff-d1b46688f428";
        public static string VersionSettingFileName { get; } = "YMCL.Setting.DaiYu";

        //Path
        public static string DataRootPath { get; } = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\DaiYu.YMCL";
        public static string PublicDataRootPath { get; } = "C:\\ProgramData\\DaiYu.YMCL";
        public static string JavaDataPath { get; } = DataRootPath + "\\YMCL.Java.DaiYu";
        public static string SettingDataPath { get; } = DataRootPath + "\\YMCL.Setting.DaiYu";
        public static string CustomHomePageXamlPath { get; } = DataRootPath + "\\YMCL.CustomHomePageXaml.DaiYu";
        public static string CustomHomePageDllPath { get; } = DataRootPath + "\\YMCL.CustomHomePageXaml.DaiYu";
        public static string CustomHomePageCSharpPath { get; } = DataRootPath + "\\YMCL.CustomHomePageCSharp.DaiYu";
        public static string AccountDataPath { get; } = DataRootPath + "\\YMCL.Account.DaiYu";
        public static string PlayListDataPath { get; } = DataRootPath + "\\YMCL.PlayList.DaiYu";
        public static string MinecraftFolderDataPath { get; } = DataRootPath + "\\YMCL.MinecraftFolder.DaiYu";
        public static string YMCLPathData { get; } = DataRootPath + "\\YMCL.ExePath.DaiYu";
        public static string YMCLBat { get; } = PublicDataRootPath + "\\launch.bat";
    }
}
