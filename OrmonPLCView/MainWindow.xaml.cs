using OrmonPLC_Comunication.CIP;
using System;
using System.Net;
using System.Windows;
using System.Windows.Media.Imaging;

namespace OrmonPLCView
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public OrmonPLC_CIP pLC_CIP;

        public MainWindow()
        {
            InitializeComponent();
            Init();
        }

        public void Init()
        {
            pLC_CIP = new OrmonPLC_CIP();

            pLC_CIP.IP = "192.168.0.";
            bool flag = VertifyIpPort(tb_Ip.Text, tb_Port.Text);
            if (flag)
            {
               // pLC_CIP = new OrmonPLC_CIP(tb_Ip.Text, tb_Port.Text);
            }

        }

        private bool VertifyIpPort(string ip, string port)
        {
            IPAddress ipAddress;
            int portAddress;
            bool b1 = IPAddress.TryParse(ip, out ipAddress);
            bool b2 = int.TryParse(port, out portAddress);
            if (b1 && b2)//同时成功
            {
                return true;
            }
            return false;
        }

        private void btn1_Click(object sender, RoutedEventArgs e)
        {
            if (pLC_CIP.Connect())
            {
                this.image.Source = new BitmapImage(new Uri("pack://application:,,,/Images/Image2.png"));
            }
            else
            {
                this.image.Source = new BitmapImage(new Uri("pack://application:,,,/Images/Image1.png"));
            }
        }

        private void SendMessage_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ReceiveMessage_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
