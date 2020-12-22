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
using System.Threading;

namespace Messanger {
    public partial class MessangerApp : Window {
        public MainWindow HeadWindow;
        public string login;
        public Thread updateMessages;
        public MessangerApp(string log) {
            InitializeComponent();
            login = log;

            getChats();
        }
       
        private void exit_b_Click(object sender, RoutedEventArgs e) {
            HeadWindow.Show();
            this.Close();
        }
        private void add_b_Click(object sender, RoutedEventArgs e) {
            SearchFriends af = new SearchFriends(login,this);
            af.Show();
        }

        private async void inputMessage_tb_KeyDown(object sender, KeyEventArgs e) {
            if (e.Key == Key.Enter) {
                HttpClient client = new HttpClient();
                var values = new Dictionary<string, string>
                {
                      { "login", chats_lb.SelectedItem.ToString() },
                      { "mylogin", login },
                      { "message", inputMessage_tb.Text },
                  };

                var content = new FormUrlEncodedContent(values);

                try {
                    var response = await client.PostAsync("https://xcomplextestapp.000webhostapp.com/addMessage.php", content);
                }
                catch (HttpRequestException x) {
                    MessageBox.Show("Ошибка подключения!");
                }
            }
        }

        private void inputMessage_tb_LostFocus(object sender, RoutedEventArgs e) {
            if (inputMessage_tb.Text == "") {
                inputMessage_tb.Text = "Input message...";
                inputMessage_tb.Foreground = new SolidColorBrush(Colors.DarkGray);
            }
        } 

        private void inputMessage_tb_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e) {
            if (inputMessage_tb.Text == "Input message...") {
                inputMessage_tb.Text = "";
                inputMessage_tb.Foreground = new SolidColorBrush(Colors.White);
            }
        }

        public async void getChats() {
              HttpClient client = new HttpClient();
              var values = new Dictionary<string, string>
              {
                  { "login", login }
              };

              var content = new FormUrlEncodedContent(values);

              try {
                  var response = await client.PostAsync("https://xcomplextestapp.000webhostapp.com/getChats.php", content);
                  string responseString = await response.Content.ReadAsStringAsync();
                  string[] mas = responseString.TrimEnd().TrimStart().Split(",");
                  for (int i = 0; i < mas.Length; i++)
                      chats_lb.Items.Add(mas[i]);

                  chats_lb.SelectedIndex = 0;

                  updateMessages = new Thread(new ThreadStart(getMessages));
                  updateMessages.IsBackground = true;
                  updateMessages.Start();
              }
              catch (HttpRequestException x) {
                  MessageBox.Show("Ошибка подключения!");
              }
        }

        public async void getMessages() {
            HttpClient client = new HttpClient();
            while (true) {
                string si = null;
                Dispatcher.Invoke(() => si = chats_lb.SelectedItem.ToString());

                var values = new Dictionary<string, string> {
                    { "login", si },
                    { "mylogin", login },
                };

                var content = new FormUrlEncodedContent(values);

                try {
                    var response = await client.PostAsync("https://xcomplextestapp.000webhostapp.com/getChat.php", content);
                    string responseString = await response.Content.ReadAsStringAsync();
                    string[] text = responseString.Split('\n');
                    Application.Current.Dispatcher.Invoke(
                        new Action(() => { 
                            chat_rtb.Document.Blocks.Clear();

                            for (int i = 0; i < text.Length; i++) {
                                Paragraph m = new Paragraph();
                                if (text[i].Contains(login)) {
                                    m.TextAlignment = TextAlignment.Right;
                                    text[i] = text[i].Replace(login.ToString() + ":", "");
                                }
                                else {
                                    m.TextAlignment = TextAlignment.Left;
                                    text[i] = text[i].Replace(chats_lb.SelectedItem.ToString() + ":", "");
                                }
                                m.Inlines.Add(text[i]);
                                chat_rtb.Document.Blocks.Add(m);
                            }
                        })
                    );
                }
                catch (HttpRequestException x) {
                    MessageBox.Show("Ошибка подключения!");
                }

                Thread.Sleep(1000);
            }
        }
    }
}
