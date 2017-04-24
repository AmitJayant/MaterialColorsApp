using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using MaterialColors.Properties;

namespace MaterialColors
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Grid grid;
        Rectangle rectangle;
        Label labelColorNum, labelHex;
        String includeHash;

        public MainWindow()
        {
            InitializeComponent();            
        }

        // Events /////////////////////////////////////////////////////////////
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Read from settings and set checkbox
            includeHash = Settings.Default["IncludeHash"].ToString();
            if (includeHash.Equals("true"))
            {
                chkHash.IsChecked = true;
            }
            else
            {
                chkHash.IsChecked = false;
            }
        }

        private void ColorGroup_ButtonClick(object sender, RoutedEventArgs e)
        {
            setDefaultStates();

            // Make ellipse bigger (selected state)
            Button btn = (Button)sender;            
            btn.Width = 26;
            btn.Height = 26;

            setupColorPalette(btn);
        }

        private void ColorSwatch_ButtonClick(object sender, RoutedEventArgs e)
        {
            // Copy color hex to clipboard
            grid = (Grid)VisualTreeHelper.GetChild((Button)sender, 0);
            Label labelHex = (Label)VisualTreeHelper.GetChild(grid, 2);

            if (includeHash.Equals("true"))
            {
                Clipboard.SetText(labelHex.Content.ToString());
            }
            else
            {
                Clipboard.SetText(labelHex.Content.ToString().Replace("#", ""));
            }
        }

        private void labelAbout_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            // Show about dialog
            About about = new About();
            about.Owner = Window.GetWindow(this);
            about.ShowDialog();
        }        

        private void chkHash_Click(object sender, RoutedEventArgs e)
        {
            if (chkHash.IsChecked == true)
            {
                Settings.Default["IncludeHash"] = "true";
                includeHash = "true";
            }
            else
            {
                Settings.Default["IncludeHash"] = "false";
                includeHash = "false";
            }
            Settings.Default.Save();
        }

        // Methods ////////////////////////////////////////////////////////////
        private void setDefaultStates()
        {
            Button[] colorButtons = { btnAmber, btnBlue, btnBlueGrey,
                btnBrown, btnCyan, btnDeepOrange, btnDeepPurple,
                btnGreen, btnGrey, btnIndigo, btnLightBlue,
                btnLightGreen, btnLime, btnOrange, btnPink,
                btnPurple, btnRed, btnTeal, btnYellow };

            for (int i = 0; i < colorButtons.Length; i++)
            {
                colorButtons[i].Width = 16;
                colorButtons[i].Height = 16;
            }
        }

        private void setupColorPalette(Button btn)
        {
            Button[] swatchesButton = { btn50, btn100, btn200, btn300, btn400,
                btn500, btn600, btn700, btn800, btn900, btnA100, btnA200, btnA400, btnA700 };
            String colorHex;

            colorGroup.Content = MaterialColorsEngine.getColorGroup(btn.Tag);

            if (Convert.ToInt32(btn.Tag) >= 16)
            {
                btnA100.Visibility = Visibility.Hidden;
                btnA200.Visibility = Visibility.Hidden;
                btnA400.Visibility = Visibility.Hidden;
                btnA700.Visibility = Visibility.Hidden;
            } else
            {
                btnA100.Visibility = Visibility.Visible;
                btnA200.Visibility = Visibility.Visible;
                btnA400.Visibility = Visibility.Visible;
                btnA700.Visibility = Visibility.Visible;
            }

            for (int i = 0; i < 14; i++)
            {
                if (Convert.ToInt32(btn.Tag) >= 16 && i >= 10)
                {
                    break;
                }

                colorHex = MaterialColorsEngine.getMaterialColor(btn.Tag, i);
                grid = (Grid)VisualTreeHelper.GetChild(swatchesButton[i], 0);
                rectangle = (Rectangle)VisualTreeHelper.GetChild(grid, 0);
                labelColorNum = (Label)VisualTreeHelper.GetChild(grid, 1);
                labelHex = (Label)VisualTreeHelper.GetChild(grid, 2);

                // Set content
                labelHex.Content = colorHex;

                // Calculate readable color value
                Color backColor = (Color)ColorConverter.ConvertFromString(colorHex);
                Color foreColor = (perceivedBrightness(backColor) > 130 ? Colors.Black : Colors.White);

                // Set colors
                rectangle.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString(colorHex));
                labelColorNum.Foreground = new SolidColorBrush(foreColor);
                labelHex.Foreground = new SolidColorBrush(foreColor);
            }
        }

        private int perceivedBrightness(Color c)
        {
            return (int)Math.Sqrt(
            c.R * c.R * .299 +
            c.G * c.G * .587 +
            c.B * c.B * .114);
        }
    }
}