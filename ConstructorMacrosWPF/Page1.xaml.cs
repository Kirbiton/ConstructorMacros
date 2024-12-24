using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;
using System.Windows.Forms;
using Application = System.Windows.Application;
using System.IO;

namespace ConstructorMacrosWPF
{
    /// <summary>
    /// Логика взаимодействия для Page1.xaml
    /// </summary>
    public partial class Page1 : Page
    {
        string MacrosPath = $"{Convert.ToString(App.Current.Resources["MacrosName"])}.txt";
        int indexToChange;
        bool ChangeButt = false;
        string[] MacMass;
        bool MacEn = false;
        Key MacKey = Key.P;
        DispatcherTimer timer1;


        public Page1()
        {
            InitializeComponent();
            label1.Content = $"Кнопка макроса: {Convert.ToString(MacKey)}";
            TimeSpan time1 = TimeSpan.FromMilliseconds(1);
            timer1 = new DispatcherTimer(time1, DispatcherPriority.Normal, delegate
            {
                if (Keyboard.IsKeyDown(MacKey))
                {
                    for (int i = 0; i < MacMass.Length; i++)
                    {
                        SendKeys.SendWait(MacMass[i]);

                    }
                }
            }, Application.Current.Dispatcher);
            timer1.Stop();

            FileStream MacrosNamesFile = new FileStream(MacrosPath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            StreamReader readFile = new StreamReader(MacrosNamesFile);
            string line;
            while ((line = readFile.ReadLine()) != null)
            {
                listBox.Items.Add(line);
            }
            MacrosNamesFile.Close();
        }
        private void button1_Click(object sender, RoutedEventArgs e)
        {
            if (!ChangeButt) listBox.Items.Add(textBox1.Text);
            else
            {
                listBox.Items.RemoveAt(indexToChange);
                listBox.Items.Insert(indexToChange, textBox1.Text);
                ChangeButt = false;
            }
            textBox1.Text = "";
            UpdateMacMass();
        }
        private void UpdateMacMass()
        {
            File.WriteAllText(MacrosPath, "");
            MacMass = new string[listBox.Items.Count];
            for (int i = 0; i < listBox.Items.Count; i++)
            {
                File.AppendAllText(MacrosPath, $"{listBox.Items[i]}\n");
                MacMass[i] = listBox.Items[i].ToString();
            }
        }
        private void textBox2_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            MacKey = e.Key;
            label1.Content = $"Кнопка макроса: {Convert.ToString(MacKey)}";
            textBox2.Text = string.Empty;
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {

            MacEn = !MacEn;
            if (MacEn)
            {
                UpdateMacMass();
                timer1.Start();
            }
            else timer1.Stop();
        }

        private void Change_Click(object sender, RoutedEventArgs e)
        {
            if (listBox.SelectedIndex != -1)
            {
                indexToChange = listBox.SelectedIndex;
                textBox1.Text = listBox.SelectedItem.ToString();
                ChangeButt = true;
                firstFocus = false;
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {

            listBox.Items.RemoveAt(listBox.SelectedIndex);
            UpdateMacMass();
        }

        bool firstFocus = true;
        public static bool IsFirtstFocus(bool x)
        {
            if(x) return true;
            else return false;
        }
        private void textBox1_GotFocus(object sender, RoutedEventArgs e)
        {
            if (firstFocus)
            {
                textBox1.Text = string.Empty;
                firstFocus = false;
            }
        }
    }
}
