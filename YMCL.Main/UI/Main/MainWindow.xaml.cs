﻿using Hardcodet.Wpf.TaskbarNotification;
using Newtonsoft.Json;
using Panuon.WPF.UI;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Xml.Linq;
using YMCL.Main.Public;
using YMCL.Main.Public.Lang;
using YMCL.Main.UI.TaskManage.TaskCenter;
using static YMCL.Main.App;
using Cursors = System.Windows.Input.Cursors;
using Size = System.Windows.Size;

namespace YMCL.Main.UI.Main
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : WindowX
    {
        bool _firstLoad = true;
        private TaskbarIcon _tb;
        #region UI
        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            Root.Visibility = Visibility.Hidden;
            ShowInTaskbar = false;
            e.Cancel = true;
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //将装饰器添加到窗口的Content控件上(Resize)
            var c = this.Content as UIElement;
            var layer = AdornerLayer.GetAdornerLayer(c);
            layer.Add(new WindowResizeAdorner(c));

            if (_firstLoad)
            {
                _firstLoad = false;
                ParameterProcessing();
            }
        }

        bool isMouseDown = false;
        private void WindowX_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (isMouseDown)
            {
                var setting = JsonConvert.DeserializeObject<Public.Class.Setting>(File.ReadAllText(Const.SettingDataPath));
                setting.MainWidth = ActualWidth;
                setting.MainHeight = ActualHeight;
                File.WriteAllText(Const.SettingDataPath, JsonConvert.SerializeObject(setting, Formatting.Indented));
            }
        }

        private void WindowX_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            isMouseDown = true;
        }

        private void WindowX_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            isMouseDown = false;
        }
        private void TitleBar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Root.Visibility = Visibility.Hidden;
            ShowInTaskbar = false;
            Environment.Exit(0);
        }
        #endregion
        #region Resize
        public class WindowResizeAdorner : Adorner
        {
            //4条边
            Thumb _leftThumb, _topThumb, _rightThumb, _bottomThumb;
            //4个角
            Thumb _lefTopThumb, _rightTopThumb, _rightBottomThumb, _leftbottomThumb;
            //布局容器，如果不使用布局容器，则需要给上述8个控件布局，实现和Grid布局定位是一样的，会比较繁琐且意义不大。
            Grid _grid;
            UIElement _adornedElement;
            Window _window;
            public WindowResizeAdorner(UIElement adornedElement) : base(adornedElement)
            {
                _adornedElement = adornedElement;
                _window = Window.GetWindow(_adornedElement);
                //初始化thumb
                _leftThumb = new Thumb();
                _leftThumb.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                _leftThumb.VerticalAlignment = VerticalAlignment.Stretch;
                _leftThumb.Cursor = Cursors.SizeWE;
                _topThumb = new Thumb();
                _topThumb.HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch;
                _topThumb.VerticalAlignment = VerticalAlignment.Top;
                _topThumb.Cursor = Cursors.SizeNS;
                _rightThumb = new Thumb();
                _rightThumb.HorizontalAlignment = System.Windows.HorizontalAlignment.Right;
                _rightThumb.VerticalAlignment = VerticalAlignment.Stretch;
                _rightThumb.Cursor = Cursors.SizeWE;
                _bottomThumb = new Thumb();
                _bottomThumb.HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch;
                _bottomThumb.VerticalAlignment = VerticalAlignment.Bottom;
                _bottomThumb.Cursor = Cursors.SizeNS;
                _lefTopThumb = new Thumb();
                _lefTopThumb.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                _lefTopThumb.VerticalAlignment = VerticalAlignment.Top;
                _lefTopThumb.Cursor = Cursors.SizeNWSE;
                _rightTopThumb = new Thumb();
                _rightTopThumb.HorizontalAlignment = System.Windows.HorizontalAlignment.Right;
                _rightTopThumb.VerticalAlignment = VerticalAlignment.Top;
                _rightTopThumb.Cursor = Cursors.SizeNESW;
                _rightBottomThumb = new Thumb();
                _rightBottomThumb.HorizontalAlignment = System.Windows.HorizontalAlignment.Right;
                _rightBottomThumb.VerticalAlignment = VerticalAlignment.Bottom;
                _rightBottomThumb.Cursor = Cursors.SizeNWSE;
                _leftbottomThumb = new Thumb();
                _leftbottomThumb.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                _leftbottomThumb.VerticalAlignment = VerticalAlignment.Bottom;
                _leftbottomThumb.Cursor = Cursors.SizeNESW;
                _grid = new Grid();
                _grid.Children.Add(_leftThumb);
                _grid.Children.Add(_topThumb);
                _grid.Children.Add(_rightThumb);
                _grid.Children.Add(_bottomThumb);
                _grid.Children.Add(_lefTopThumb);
                _grid.Children.Add(_rightTopThumb);
                _grid.Children.Add(_rightBottomThumb);
                _grid.Children.Add(_leftbottomThumb);
                AddVisualChild(_grid);
                foreach (Thumb thumb in _grid.Children)
                {
                    int thumnSize = 10;
                    if (thumb.HorizontalAlignment == System.Windows.HorizontalAlignment.Stretch)
                    {
                        thumb.Width = double.NaN;
                        thumb.Margin = new Thickness(thumnSize, 0, thumnSize, 0);
                    }
                    else
                    {
                        thumb.Width = thumnSize;
                    }
                    if (thumb.VerticalAlignment == VerticalAlignment.Stretch)
                    {
                        thumb.Height = double.NaN;
                        thumb.Margin = new Thickness(0, thumnSize, 0, thumnSize);
                    }
                    else
                    {
                        thumb.Height = thumnSize;
                    }
                    thumb.Background = System.Windows.Media.Brushes.Green;
                    thumb.Template = new ControlTemplate(typeof(Thumb))
                    {
                        VisualTree = GetFactory(new SolidColorBrush(Colors.Transparent))
                    };
                    thumb.DragDelta += Thumb_DragDelta;
                }
            }

            protected override Visual GetVisualChild(int index)
            {
                return _grid;
            }
            protected override int VisualChildrenCount
            {
                get
                {
                    return 1;
                }
            }
            protected override Size ArrangeOverride(Size finalSize)
            {
                //直接给grid布局，grid内部的thumb会自动布局。
                _grid.Arrange(new Rect(new System.Windows.Point(-(_window.RenderSize.Width - finalSize.Width) / 2, -(_window.RenderSize.Height - finalSize.Height) / 2), _window.RenderSize));
                return finalSize;
            }
            //拖动逻辑
            private void Thumb_DragDelta(object sender, DragDeltaEventArgs e)
            {
                var c = _window;
                var thumb = sender as FrameworkElement;
                double left, top, width, height;
                if (thumb.HorizontalAlignment == System.Windows.HorizontalAlignment.Left)
                {
                    left = c.Left + e.HorizontalChange;
                    width = c.Width - e.HorizontalChange;
                }
                else
                {
                    left = c.Left;
                    width = c.Width + e.HorizontalChange;
                }
                if (thumb.HorizontalAlignment != System.Windows.HorizontalAlignment.Stretch)
                {
                    if (width > 63)
                    {
                        c.Left = left;
                        c.Width = width;
                    }
                }
                if (thumb.VerticalAlignment == VerticalAlignment.Top)
                {

                    top = c.Top + e.VerticalChange;
                    height = c.Height - e.VerticalChange;
                }
                else
                {
                    top = c.Top;
                    height = c.Height + e.VerticalChange;
                }

                if (thumb.VerticalAlignment != VerticalAlignment.Stretch)
                {
                    if (height > 63)
                    {
                        c.Top = top;
                        c.Height = height;
                    }
                }
            }
            //thumb的样式
            FrameworkElementFactory GetFactory(System.Windows.Media.Brush back)
            {
                var fef = new FrameworkElementFactory(typeof(System.Windows.Shapes.Rectangle));
                fef.SetValue(System.Windows.Shapes.Rectangle.FillProperty, back);
                return fef;
            }
        }
        #endregion
        #region Pages
        Pages.Launch.Launch launch = new();
        Pages.Setting.Setting setting = new();
        Pages.Download.Download download = new();
        Pages.More.More more = new();
        Tasks tasks = new();
        #endregion
        #region TurnPage
        private void ToLaunch_Checked(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = launch;
        }

        private void ToSetting_Checked(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = setting;
        }

        private void ToDownload_Checked(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = download;
        }

        private void ToMore_Checked(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = more;
        }
        #endregion
        //protected override void OnSourceInitialized(EventArgs e)
        //{
        //    MessageBoxX.Show("Show");
        //    base.OnSourceInitialized(e);
        //    HwndSource hwndSource = PresentationSource.FromVisual(this) as HwndSource;
        //    if (hwndSource != null)
        //    {
        //        IntPtr handle = hwndSource.Handle;
        //        hwndSource.AddHook(new HwndSourceHook(WndProc));
        //    }
        //}

        //IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        //{
        //    if (msg == 9001)
        //    {

        //        Root.Visibility = Visibility.Visible;
        //        WindowState = WindowState.Normal;
        //        ShowInTaskbar = true;
        //        Show();
        //    }
        //    return hwnd;
        //}

        public MainWindow()
        {
            InitializeComponent();
            var setting = JsonConvert.DeserializeObject<Public.Class.Setting>(File.ReadAllText(Const.SettingDataPath));
            Width = setting.MainWidth;
            Height = setting.MainHeight;

            _tb = (TaskbarIcon)FindResource("NotifyIcon");
            _tb.Icon = new Icon(System.IO.Path.Combine(Const.PublicDataRootPath, "Icon.ico"));
        }
        void ParameterProcessing()
        {
            if (App.StartupArgs.Length > 0)
            {
                var urlScheme = System.Web.HttpUtility.UrlDecode(App.StartupArgs[0]);
                urlScheme = urlScheme.Substring(7, urlScheme.Length - 8);
                var actions = urlScheme.Split("--| ").ToList();
                actions.RemoveAt(0);
                foreach (var item in actions)
                {
                    var action = item.Trim();
                    string[] temp = Regex.Split(action, "(?=(?:(?:[^\"]*\"){2})*[^\"]*$) ");
                    var args = new List<string>();
                    foreach (var arg in temp)
                    {
                        var a = arg.Trim();
                        a = a.Trim('\"');
                        a = a.Trim('\'');
                        args.Add(a);
                    }
                    try
                    {
                        switch (args[0])
                        {
                            case "launch":
                                if (args.Count >= 3 && args[2] != null)
                                {
                                    if (args.Count >= 4 && args[3] != null)
                                    {
                                        launch.LaunchClient(args[1], args[2], false, args[3]);
                                    }
                                    else
                                    {
                                        launch.LaunchClient(args[1], args[2], false);
                                    }
                                }
                                else
                                {
                                    launch.LaunchClient(args[1], msg: false);
                                }
                                break;
                        }
                    }
                    catch
                    {
                        MessageBoxX.Show(LangHelper.Current.GetText("ArgsError"), "Yu Minecraft Launcher");
                    }
                }
            }
        }
    }
}
