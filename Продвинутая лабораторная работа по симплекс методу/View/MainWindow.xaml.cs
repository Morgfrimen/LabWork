using Microsoft.Win32;
using System;
using System.Windows;
using System.Windows.Forms;
using ApplicationWindows.View;

namespace Продвинутая_лабораторная_работа_по_симплекс_методу
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Timer timer = new Timer();
        public MainWindow()
        {
            InitializeComponent();
            timer.Interval = 5000;
            timer.Tick += Timer_Tick;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            //ApplicationWindows.View.MainWindow window = new ApplicationWindows.View.MainWindow();
            ApplicationWindows.View.MainWindow window = new ApplicationWindows.View.MainWindow();
            window.WindowState = WindowState.Normal;
            window.Show();
            window.Activate();
            var timer = (Timer)sender;
            timer.Stop();
            this.Close();
        }

        private void Window_Initialized(object sender, EventArgs e)
        {
            Get45PlusFromRegistry();
        }

        private void Get45PlusFromRegistry()
        {
            const string subkey = @"SOFTWARE\Microsoft\NET Framework Setup\NDP\v4\Full\";
            var ndpKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32).OpenSubKey(subkey);
            if (ndpKey != null && ndpKey.GetValue("Release") != null)
            {
                this.Consol.Text += ($"{CheckFor45PlusVersion((int)ndpKey.GetValue("Release"))}");
                timer.Start();
            }
            else
            {
                Consol.Text += ("Ниже 4.5, требуется обновить. Дальнейший запуск программы не возможен.");
            }
            // Checking the version using >= enables forward compatibility.
            string CheckFor45PlusVersion(int releaseKey)
            {
                if (releaseKey >= 528040)
                    return "4.8 или старше";
                if (releaseKey >= 461808)
                    return "4.7.2";
                if (releaseKey >= 461308)
                    return "4.7.1";
                if (releaseKey >= 460798)
                    return "4.7";
                if (releaseKey >= 394802)
                    return "4.6.2";
                if (releaseKey >= 394254)
                    return "4.6.1";
                if (releaseKey >= 393295)
                    return "4.6";
                if (releaseKey >= 379893)
                    return "4.5.2";
                if (releaseKey >= 378675)
                    return "4.5.1";
                if (releaseKey >= 378389)
                    return "4.5";
                // This code should never execute. A non-null release key should mean
                // that 4.5 or later is installed.
                return "Версия 4.5 не установлена";
            }
        }
    }
}
