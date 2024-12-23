using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Forms;
using System.Windows.Threading;
using Application = System.Windows.Application;

namespace ConstructorMacrosWPF
{

    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
            Main.Navigate(new Page2());
        }
    }
}
