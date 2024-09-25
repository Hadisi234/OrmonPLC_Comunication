using System;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace 编码转换器
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private EncodeEnum encodeType;

        public MainWindow()
        {
            InitializeComponent();
            this.encodeType = EncodeEnum.ASII;
        }
        enum EncodeEnum
        {
            ASII,
            UTF8,
            Unicode
        }

        private void RadioButton_Click(object sender, RoutedEventArgs e)
        {
            RadioButton rbt = sender as RadioButton;
            if ((bool)rbt.IsChecked)
            {
                switch (rbt.Name)
                {
                    case "ASII":
                        encodeType = EncodeEnum.ASII;
                        break;
                    case "UTF8":
                        encodeType = EncodeEnum.UTF8;
                        break;
                    case "Unicode":
                        encodeType = EncodeEnum.Unicode;
                        break;
                    default:
                        break;
                }
            }

        }

        private void translate_Click(object sender, RoutedEventArgs e)
        {
            //string str1 = "//--------------------------------------------------------Header 24byte-------------------------------------";
            //str1.Length
            string example = new TextRange(tb_Send.Document.ContentStart, tb_Send.Document.ContentEnd).Text;

            //if (example.Contains(Environment.NewLine))
            //{
            //    int temp = example.IndexOf(Environment.NewLine);
            //    int temp1 = example.IndexOf("\r");
            //    int temp2 = example.IndexOf("\n");
            //}
            example = example.Trim();
            string[] strs = example.Split(',');
            byte[] bytes = new byte[strs.Length];
            for (int index = 0; index < strs.Length; index++)
            {
                bytes[index] = Convert.ToByte(strs[index],16);
            }
            tb_Receive.Text = Encoding.ASCII.GetString(bytes);
        }
    }
}
