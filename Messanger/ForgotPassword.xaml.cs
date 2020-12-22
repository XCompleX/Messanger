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
    public partial class ForgotPassword : Window {
        public MainWindow HeadWindow;
        public ForgotPassword() {
            InitializeComponent();
        }

        private void login_tb_LostFocus(object sender, RoutedEventArgs e) {
            if (login_tb.Text == "") {
                login_tb.Text = "login";
                login_tb.Foreground = new SolidColorBrush(Colors.DarkGray);
            }
        }

        private void login_tb_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e) {
            if (login_tb.Text == "login") {
                login_tb.Text = "";
                login_tb.Foreground = new SolidColorBrush(Colors.Black);
            }
        }

        private void back_b_Click(object sender, RoutedEventArgs e) {
            HeadWindow.Show();
            this.Close();
        }

        private async void recovery_b_Click(object sender, RoutedEventArgs e) {
            HttpClient client = new HttpClient();
            var values = new Dictionary<string, string> {
                { "login", login_tb.Text == "login" ? "" : login_tb.Text },
            };

            var content = new FormUrlEncodedContent(values);

            try {
                var response = await client.PostAsync("https://xcomplextestapp.000webhostapp.com/mail.php", content);
                string responseString = await response.Content.ReadAsStringAsync();
                string msg = "";

                foreach (string i in responseString.Split(","))
                    msg += i + '\n';

                MessageBox.Show(msg);
            }
            catch (HttpRequestException x) {
                MessageBox.Show("Connection failed!");
            }
        }
    }
}
