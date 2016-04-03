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
using System.Windows.Shapes;

namespace MaterialColors
{
    /// <summary>
    /// Interaction logic for About.xaml
    /// </summary>
    public partial class About : Window
    {
        public About()
        {
            InitializeComponent();
        }

        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            TextBlock label = (TextBlock)sender;
            switch(Convert.ToInt32(label.Tag))
            {
                case 0:
                    System.Diagnostics.Process.Start("https://plus.google.com/u/0/+AmitJayant");
                    break;

                case 1:
                    System.Diagnostics.Process.Start("https://github.com/AmJay/MaterialColorsApp");
                    break;

                case 2:
                    System.Diagnostics.Process.Start("https://github.com/romannurik/MaterialColorsApp");
                    break;
            }
        }
    }
}