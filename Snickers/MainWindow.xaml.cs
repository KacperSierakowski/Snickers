using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace Snickers
{
    public partial class MainWindow : Window
    {
        private static TextBlock MyTextBlock = new TextBlock();
        private static Button MyButton = new Button();
        //private static bool IsSending = false;
        public MainWindow()
        {
            InitializeComponent();
            MyTextBlock = textBlock;
            MyButton = button;
        }
        private void OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            StartSending();
        }
        private void OnMouseUp(object sender, MouseButtonEventArgs e)
        {
            StopSending();
        }
        public delegate void UpdateTextCallback(string message);
        private static Thread MyThread;
        public void StartSending()
        {
            //IsSending = true;
            MyThread = new Thread(SendData);
            MyThread.Start();
        }
        static private void SendData()
        {
            int i = 0;
            while (true)
            {
                Thread.Sleep(1000);
                MyTextBlock.Dispatcher.Invoke(
                    new UpdateTextCallback(UpdateText),
                    new object[] { i.ToString() }
                );
                i++;
            }
        }
        static private void UpdateText(string message)
        {
            MyTextBlock.Text=(message + "\n");
        }
        static private void StopSending()
        {
            //IsSending = false;
            MyThread.Abort();
        }
    }
}
