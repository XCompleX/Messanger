using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Net.Http;

namespace Messanger {
    public partial class Register : Window {
        public MainWindow HeadWindow;
        public Register() {
            InitializeComponent();
        }

        private void login_tb_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e) {
            if (login_tb.Text == "login") {
                login_tb.Text = "";
                login_tb.Foreground = new SolidColorBrush(Colors.Black);
            }
        }

        private void password_tb_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e) {
            if (password_tb.Text == "password") {
                password_tb.Text = "";
                password_tb.Foreground = new SolidColorBrush(Colors.Black);
            }
        }

        private void repPassword_tb_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e) {
            if (repPassword_tb.Text == "repeat password") {
                repPassword_tb.Text = "";
                repPassword_tb.Foreground = new SolidColorBrush(Colors.Black);
            }
        }

        private void firstName_tb_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e) {
            if (firstName_tb.Text == "first name") {
                firstName_tb.Text = "";
                firstName_tb.Foreground = new SolidColorBrush(Colors.Black);
            }
        }

        private void lastName_tb_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e) {
            if (lastName_tb.Text == "last name") {
                lastName_tb.Text = "";
                lastName_tb.Foreground = new SolidColorBrush(Colors.Black);
            }
        }

        private void email_tb_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e) {
            if (email_tb.Text == "email") {
                email_tb.Text = "";
                email_tb.Foreground = new SolidColorBrush(Colors.Black);
            }
        }
        private void login_tb_LostFocus(object sender, RoutedEventArgs e) {
            if (login_tb.Text == "") {
                login_tb.Text = "login";
                login_tb.Foreground = new SolidColorBrush(Colors.DarkGray);
            }
        }

        private void password_tb_LostFocus(object sender, RoutedEventArgs e) {
            if (password_tb.Text == "") {
                password_tb.Text = "password";
                password_tb.Foreground = new SolidColorBrush(Colors.DarkGray);
            }
        }

        private void repPassword_tb_LostFocus(object sender, RoutedEventArgs e) {
            if (repPassword_tb.Text == "") {
                repPassword_tb.Text = "repeat password";
                repPassword_tb.Foreground = new SolidColorBrush(Colors.DarkGray);
            }
        }

        private void firstName_tb_LostFocus(object sender, RoutedEventArgs e) {
            if (firstName_tb.Text == "") {
                firstName_tb.Text = "first name";
                firstName_tb.Foreground = new SolidColorBrush(Colors.DarkGray);
            }
        }

        private void lastName_tb_LostFocus(object sender, RoutedEventArgs e) {
            if (lastName_tb.Text == "") {
                lastName_tb.Text = "last name";
                lastName_tb.Foreground = new SolidColorBrush(Colors.DarkGray);
            }
        }

        private void email_tb_LostFocus(object sender, RoutedEventArgs e) {
            if (email_tb.Text == "") {
                email_tb.Text = "email";
                email_tb.Foreground = new SolidColorBrush(Colors.DarkGray);
            }
        }

        private void back_b_Click(object sender, RoutedEventArgs e) {
            HeadWindow.Show();
            this.Close();
        }

        private async void register_b_Click(object sender, RoutedEventArgs e) {
            HttpClient client = new HttpClient();
            var values = new Dictionary<string, string> {
                { "login", login_tb.Text == "login" ? "" : login_tb.Text },
                { "password", password_tb.Text == "password" ? "" : password_tb.Text },
                { "repPassword", repPassword_tb.Text == "repeat password" ? "" : repPassword_tb.Text },
                { "firstName", firstName_tb.Text == "first name" ? "" : firstName_tb.Text },
                { "lastName", lastName_tb.Text == "last name" ? "" : lastName_tb.Text },
                { "email", email_tb.Text == "email" ? "" : email_tb.Text }
            };

            var content = new FormUrlEncodedContent(values);

            try {
                var response = await client.PostAsync("https://xcomplextestapp.000webhostapp.com/registration.php", content);
                string responseString = await response.Content.ReadAsStringAsync();
                string msg = "";

                foreach (string i in responseString.Split(",")) {
                    msg += i + '\n';
                }

                MessageBox.Show(msg);
            }
            catch (HttpRequestException x) {
                MessageBox.Show("Connection failed!");
            }
        }
    }
}
