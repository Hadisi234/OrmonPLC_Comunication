using System;
using System.Windows;
using System.Windows.Controls;

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
            string example = tb_Send.Text;
            if (example.Contains(Environment.NewLine))
            {

            }
            string[] strs = example.Split(',');
        }
    }
}
