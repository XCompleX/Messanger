using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Net.Http;

namespace Messanger {
    public partial class MainWindow : Window {
        public MainWindow() {
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

        private async void login_b_Click(object sender, RoutedEventArgs e) {
            HttpClient client = new HttpClient();
            var values = new Dictionary<string, string>
            {
                { "login", login_tb.Text == "login" ? "" : login_tb.Text },
                { "password", password_tb.Text == "password" ? "" : password_tb.Text }
            };

            var content = new FormUrlEncodedContent(values);

            try {
                var response = await client.PostAsync("https://xcomplextestapp.000webhostapp.com/login.php", content);
                string responseString = await response.Content.ReadAsStringAsync();

                string msg = "";
                foreach (string i in responseString.Split(",")) {
                    msg += i + '\n';
                }
                if (msg.Contains('+')) {
                    MessangerApp mes = new MessangerApp(login_tb.Text);
                    mes.HeadWindow = this;
                    mes.Show();
                    this.Hide();
                }
                else
                    MessageBox.Show(msg);
            }
            catch (HttpRequestException x) {
                MessageBox.Show("Connection failed!");
            }
        }

        private void forgot_l_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e) {
            ForgotPassword recoveryWindow = new ForgotPassword();
            recoveryWindow.HeadWindow = this;
            recoveryWindow.Show();
            this.Hide();
        }

        private void register_l_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e) {
            Register regWindow = new Register();
            regWindow.HeadWindow = this;
            regWindow.Show();
            this.Hide();
        }
    }
}
