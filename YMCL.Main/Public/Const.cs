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

        //Path
        public static string DataRootPath { get; } = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\DaiYu.YMCL";
        public static string PublicDataRootPath { get; } = "C:\\ProgramData\\DaiYu.YMCL";
        public static string JavaDataPath { get; } = DataRootPath + "\\YMCL.Java.DaiYu";
        public static string SettingDataPath { get; } = DataRootPath + "\\YMCL.Setting.DaiYu";
        public static string AccountDataPath { get; } = DataRootPath + "\\YMCL.Account.DaiYu";
        public static string MinecraftFolderDataPath { get; } = DataRootPath + "\\YMCL.MinecraftFolder.DaiYu";
        public static string LaunchPageXamlPath { get; } = DataRootPath + "\\CustomPage\\YMCL.LaunchPageXaml.DaiYu";
    }
}