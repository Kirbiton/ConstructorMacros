using System;
using System.Collections.Generic;
using System.IO;
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

namespace ConstructorMacrosWPF
{
    public partial class Page2 : Page
    {
        string MacrosNamesPath = "MacrosNames.txt";
        public Page2()
        {
            InitializeComponent();
            UpdateNames();
        }

        public void UpdateNames()
        {
            listBox.Items.Clear();
            FileStream MacrosNamesFile = new FileStream(MacrosNamesPath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            StreamReader readFile = new StreamReader(MacrosNamesFile);
            string line;
            while ((line = readFile.ReadLine()) != null)
            {
                listBox.Items.Add(line);
            }
            MacrosNamesFile.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (new FileInfo($"{textBox.Text}.txt").Exists) MessageBox.Show("Макрос с таким именем уже существует");
            else
            {
                File.Create($"{textBox.Text}.txt");
                File.AppendAllText(MacrosNamesPath, $"{textBox.Text}\n");
                Application.Current.Shutdown();
                System.Diagnostics.Process.Start(Application.ResourceAssembly.Location);
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (listBox.SelectedIndex != -1)
            {
                App.Current.Resources["MacrosName"] = listBox.SelectedItem.ToString();
                NavigationService.Navigate(new Page1());
            }

        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (listBox.SelectedIndex != -1)
            {
                listBox.Items.RemoveAt(listBox.SelectedIndex);
                File.Delete($"{listBox.SelectedItem}.txt");
                File.WriteAllText(MacrosNamesPath, "");
                for (int i = 0; i < listBox.Items.Count; i++)
                {
                    File.AppendAllText(MacrosNamesPath, $"{listBox.Items[i]}\n");
                }
                UpdateNames();
            }
        }
    }
}
