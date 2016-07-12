﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Core;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

//“空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409 上有介绍

namespace MidtermProject
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        ApplicationDataContainer localseetings = Windows.Storage.ApplicationData.Current.LocalSettings;
        public MainPage()
        {
            this.InitializeComponent();

        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;

            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility =
                    AppViewBackButtonVisibility.Collapsed;
        }


        private void Regester_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(RegesiterScene), "");
        }
        
        //用户登录逻辑和网络访问
        async private void Login_Click(object sender, RoutedEventArgs e)
        {
            if(Username.Text == "")
            {
                var i = new MessageDialog("Please enter the username!").ShowAsync();
                return;
            }

            if (Password.Password == "")
            {
                var i = new MessageDialog("Please enter the password!").ShowAsync();
                return;
            }

            string data = Username.Text + '\t' + Password.Password;
            HttpClient httpClient = new HttpClient();
            HttpResponseMessage response = await httpClient.PostAsync("http://sunzhongyang.com:7000/login", new StringContent(data));
            string receive = await response.Content.ReadAsStringAsync();
            
            if (receive == "Login success")
            {
                this.localseetings.Values["user"] = Username.Text;
                this.localseetings.Values["box"] = "receive";
                Frame.Navigate(typeof(MailPage), "");

            }
            else
            {
                var c = new MessageDialog(receive).ShowAsync();
            }
        }
    }
}
