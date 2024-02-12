﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YMCL.Main.UI.Initialize;
using YMCL.Main.UI.Main;

namespace YMCL.Main.Public
{
    public class Const
    {
        public class Window
        {
            public static MainWindow mainWindow { get; set; }
            public static InitializeWindow initializeWindow { get; set; }
            static Window()
            {
                foreach (System.Windows.Window window in System.Windows.Application.Current.Windows)
                {
                    if (window.GetType() == typeof(InitializeWindow))
                    {
                        initializeWindow = window as InitializeWindow;
                    }
                    if (window.GetType() == typeof(MainWindow))
                    {
                        mainWindow = window as MainWindow;
                    }
                }
            }
        }

        public static string Version { get; } = "1.0.0.20240125_Alpha";
        public static string UpdaterId { get; } = "97B62D3AD1724EFA9AFC7A8D8971BBB1";
        public static string AzureClientId { get; } = "c06d4d68-7751-4a8a-a2ff-d1b46688f428";
        public static string VersionSettingFileName { get; } = "YMCL.Setting.DaiYu";

        //Path
        public static string DataRootPath { get; } = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\DaiYu.YMCL";
        public static string PublicDataRootPath { get; } = "C:\\ProgramData\\DaiYu.YMCL";
        public static string JavaDataPath { get; } = DataRootPath + "\\YMCL.Java.DaiYu";
        public static string SettingDataPath { get; } = DataRootPath + "\\YMCL.Setting.DaiYu";
        public static string CustomHomePageXamlPath { get; } = DataRootPath + "\\YMCL.CustomHomePageXaml.DaiYu";
        public static string AccountDataPath { get; } = DataRootPath + "\\YMCL.Account.DaiYu";
        public static string MinecraftFolderDataPath { get; } = DataRootPath + "\\YMCL.MinecraftFolder.DaiYu";
        public static string YMCLPathData { get; } = DataRootPath + "\\YMCL.ExePath.DaiYu";
        public static string YMCLBat { get; } = PublicDataRootPath + "\\launch.bat";
    }
}
