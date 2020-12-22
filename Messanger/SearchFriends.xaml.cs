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
    public partial class SearchFriends : Window {
        public MessangerApp Head;
        public string login;
        public SearchFriends(string log, MessangerApp ma) {
            InitializeComponent();
            login = log;
            Head = ma;
        }

        private async void search_b_Click(object sender, RoutedEventArgs e) {
            HttpClient client = new HttpClient();
            var values = new Dictionary<string, string> {
                { "login", userLogin_tb.Text == "login" ? "" : userLogin_tb.Text },
            };

            var content = new FormUrlEncodedContent(values);

            if (userLogin_tb.Text == login) {
                MessageBox.Show("This login is for you!"); return;
            }

            try {
                var response = await client.PostAsync("https://xcomplextestapp.000webhostapp.com/searchUser.php", content);
                string responseString = await response.Content.ReadAsStringAsync();

                if (!responseString.Contains('-')) {
                    MessageBoxResult result = MessageBox.Show(responseString, "Add", MessageBoxButton.YesNo);
                    switch (result) {
                        case MessageBoxResult.Yes:
                            values = new Dictionary<string, string>
                            {
                                { "login", userLogin_tb.Text},
                                { "mylogin", login},
                            };
                            content = new FormUrlEncodedContent(values);
                            response = await client.PostAsync("https://xcomplextestapp.000webhostapp.com/addChat.php", content);
                            responseString = await response.Content.ReadAsStringAsync();
                            MessageBox.Show(responseString);
                            Head.getChats();
                            break;
                    }
                }
                else
                    MessageBox.Show("User not found!");
            }
            catch (HttpRequestException x) {
                MessageBox.Show("Connection failed!");
            }
        }

        private void back_b_Click(object sender, RoutedEventArgs e) {
            this.Close();
        }

        private void userLogin_tb_LostFocus(object sender, RoutedEventArgs e) {
            if (userLogin_tb.Text == "") {
                userLogin_tb.Text = "Input Login";
                userLogin_tb.Foreground = new SolidColorBrush(Colors.DarkGray);
            }
        }

        private void userLogin_tb_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e) {
            if (userLogin_tb.Text == "Input Login") {
                userLogin_tb.Text = "";
                userLogin_tb.Foreground = new SolidColorBrush(Colors.Black);
            }
        }
    }
}
