using Microsoft.UI;
using Microsoft.UI.Input;
using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Windows.System;

namespace Calculator_WinUi3
{
    public sealed partial class MainWindow : Window
    {
        // Get App Windows ID
        private AppWindow _apw;
        private OverlappedPresenter _presenter;
        public void GetAppWindowAndPresenter()
        {
            var hWnd = WinRT.Interop.WindowNative.GetWindowHandle(this);
            WindowId myWndId = Microsoft.UI.Win32Interop.GetWindowIdFromWindow(hWnd);
            _apw = AppWindow.GetFromWindowId(myWndId);
            _presenter = _apw.Presenter as OverlappedPresenter;
        }

        public MainWindow()
        {
            // Initialize Calculator Form
            this.InitializeComponent();

            // Set TitleBar style 
            ExtendsContentIntoTitleBar = true;
            SetTitleBar(AppTitleBar);

            // Set Windows Size by using Win32 API since WinUi 3 don't have Width, Height Args
            IntPtr hWnd = WinRT.Interop.WindowNative.GetWindowHandle(this);
            var windowId = Microsoft.UI.Win32Interop.GetWindowIdFromWindow(hWnd);
            var appWindow = Microsoft.UI.Windowing.AppWindow.GetFromWindowId(windowId);
            appWindow.Resize(new Windows.Graphics.SizeInt32 { Width = 340, Height = 500 });

            // Set Window to IsResizable false
            GetAppWindowAndPresenter();
            _presenter.IsResizable = false;
        }

        // Setup Globals variable 
        public static class Globals
        {
            public static string OperatorSign;
            public static float Num, Num2;
            public static bool OperatorClicked, posnegPressed, EqualsClicked, _isCtrlKeyPressed, _isShiftKeyPressed, _isMenuKeyPressed = false;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Globals.EqualsClicked = false;
            Button button = (Button)sender;
            if (TextblockResult.Text.Length < 10)
            {
                TextblockResult.FontSize = 45;
                if (TextblockResult.Text == "0" || Globals.OperatorClicked == true)
                {
                    TextblockResult.Text = "";
                    Globals.OperatorClicked = false;
                }
                
                TextblockResult.Text += button.Content;
            }
            else if (TextblockResult.Text.Length < 11)
            {
                TextblockResult.FontSize = 40;
                TextblockResult.Text += button.Content;
            }
            else if (TextblockResult.Text.Length < 12)
            {
                TextblockResult.FontSize = 38;
                TextblockResult.Text += button.Content;
            }
            else if (TextblockResult.Text.Length < 13)
            {
                TextblockResult.FontSize = 36;
                TextblockResult.Text += button.Content;
            }
            else if (TextblockResult.Text.Length < 14)
            {
                TextblockResult.FontSize = 34;
                TextblockResult.Text += button.Content;
            }
            else if (TextblockResult.Text.Length < 15)
            {
                TextblockResult.FontSize = 32;
                TextblockResult.Text += button.Content;
            }
            else if (TextblockResult.Text.Length < 16)
            {
                TextblockResult.FontSize = 30;
                TextblockResult.Text += button.Content;
            }
            else { }
        }

        private void CE_Click(object sender, RoutedEventArgs e)
        {
            if (Globals.EqualsClicked)
            {
                TextblockResult.FontSize = 45;
                TextblockCalc.Text = "";
                TextblockResult.Text = "0";
                Globals.Num = 0;
                Globals.Num2 = 0;
                Globals.EqualsClicked = false;
            }
            else
            {
                TextblockResult.FontSize = 45;
                TextblockResult.Text = "0";
            }
        }

        private void C_Click(object sender, RoutedEventArgs e)
        {
            TextblockResult.FontSize = 45;
            TextblockCalc.Text = "";
            TextblockResult.Text = "0";
            Globals.Num = 0;
            Globals.Num2 = 0;
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            if (TextblockResult.Text.Length > 0 && Globals.EqualsClicked == false)
            {
                TextblockResult.Text = TextblockResult.Text.Remove(TextblockResult.Text.Length - 1);
                if (TextblockResult.Text.Length == 0) TextblockResult.Text = "0";
            }
            else { TextblockCalc.Text = ""; }
            if (TextblockResult.Text.Length <= 10) { TextblockResult.FontSize = 45; }
            else if (TextblockResult.Text.Length <= 12) { TextblockResult.FontSize = 40; }
            else if (TextblockResult.Text.Length <= 13) { TextblockResult.FontSize = 38;}
            else if (TextblockResult.Text.Length <= 14) { TextblockResult.FontSize = 36; }
            else if (TextblockResult.Text.Length <= 15) { TextblockResult.FontSize = 34; }
            else if (TextblockResult.Text.Length <= 16) { TextblockResult.FontSize = 32; }
        }

        private void Percent_Click(object sender, RoutedEventArgs e)
        {
            Globals.Num2 = float.Parse(TextblockResult.Text);
            if (!(Globals.Num == 0)) 
            {
                var Percent = (Globals.Num2 * Globals.Num) / 100;
                TextblockCalc.Text += " " + Percent;
                TextblockResult.Text = Percent.ToString();
            }
            else
            {
                TextblockCalc.Text = "0";
            }
        }

        private void OneByX_Click(object sender, RoutedEventArgs e)
        {
            Globals.Num = 1 / float.Parse(TextblockResult.Text);
            TextblockCalc.Text = "1/(" + TextblockResult.Text + ")";
            TextblockResult.Text = Globals.Num.ToString();
        }

        private void sqr_Click(object sender, RoutedEventArgs e)
        {
            Globals.Num = float.Parse(TextblockResult.Text) * float.Parse(TextblockResult.Text);
            TextblockCalc.Text = "sqr(" + TextblockResult.Text + ")";
            TextblockResult.Text = Globals.Num.ToString();
        }

        private void Sqrt_Click(object sender, RoutedEventArgs e)
        {
            Globals.Num = float.Parse(TextblockResult.Text);
            if (Globals.Num > 0)
            {
                Globals.Num = (float)Math.Sqrt(Globals.Num) ;
                TextblockCalc.Text = "√(" + TextblockResult.Text + ")";
                TextblockResult.Text = Globals.Num.ToString();
            }
        }

        private void Operator_Click(object sender, RoutedEventArgs e)
        {
            Globals.posnegPressed = false;
            Button button = (Button)sender;
            if (TextblockCalc.Text.Length > 0)
            {
                if (Globals.EqualsClicked)
                {
                    Globals.EqualsClicked = false;
                }
                else if (TextblockCalc.Text.Contains("+") || TextblockCalc.Text.Contains("-") || TextblockCalc.Text.Contains("x") || TextblockCalc.Text.Contains("÷"))
                {
                    Globals.Num2 = float.Parse(TextblockResult.Text);
                    switch (Globals.OperatorSign)
                    {
                        case "-":
                            Globals.Num =  Globals.Num - Globals.Num2;
                            TextblockResult.Text = Globals.Num.ToString();
                            break;
                        case "+":
                            Globals.Num = Globals.Num + Globals.Num2;
                            TextblockResult.Text = Globals.Num.ToString();
                            break;
                        case "x":
                            Globals.Num = Globals.Num * Globals.Num2;
                            TextblockResult.Text = Globals.Num.ToString();
                            break;
                        case "÷":
                            Globals.Num = Globals.Num / Globals.Num2;
                            TextblockResult.Text = Globals.Num.ToString();
                            break;
                        default:
                            break;
                    }
                }
                TextblockCalc.Text = Globals.Num + " " + button.Content;
                Globals.OperatorSign = (string)button.Content;
            }
            else
            {
                Globals.Num = float.Parse(TextblockResult.Text);
                TextblockCalc.Text = TextblockResult.Text + " " + button.Content;
                Globals.OperatorSign = button.Content.ToString();
            }
            Globals.OperatorClicked = true;
        }

        private void Equal_Click(object sender, RoutedEventArgs e)
        {
            Globals.posnegPressed = false;
            Globals.EqualsClicked = true;
            Button button = (Button)sender;

            if (TextblockCalc.Text.Length > 0)
            {
                if (TextblockCalc.Text.Contains("+") || TextblockCalc.Text.Contains("-") || TextblockCalc.Text.Contains("x") || TextblockCalc.Text.Contains("÷"))
                {
                    Globals.Num2 = float.Parse(TextblockResult.Text);
                    TextblockCalc.Text = Globals.Num + " " + Globals.OperatorSign + " " + Globals.Num2 + " =";
                    switch (Globals.OperatorSign)
                    {
                        case "-":
                            Globals.Num = Globals.Num - Globals.Num2;
                            break;
                        case "+":
                            Globals.Num = Globals.Num + Globals.Num2;
                            break;
                        case "x":
                            Globals.Num = Globals.Num * Globals.Num2;
                            break;
                        case "÷":
                            Globals.Num = Globals.Num / Globals.Num2;
                            break;
                        default:
                            break;
                    }
                    TextblockResult.Text = Globals.Num.ToString();
                }
            }
            else
            {
                Globals.Num = float.Parse(TextblockResult.Text);
                TextblockCalc.Text = TextblockResult.Text + " " + button.Content;
                Globals.OperatorSign = button.Content.ToString();
            }
            Globals.OperatorClicked = true;
        }

        private void posneg_Click(object sender, RoutedEventArgs e)
        {
            if (!(TextblockResult.Text == 0.ToString()))
            {
                if (Globals.posnegPressed == false)
                {
                    TextblockResult.Text = "-" + TextblockResult.Text;
                    Globals.posnegPressed = true;
                }
                else if (TextblockResult.Text.Contains("-"))
                {
                    TextblockResult.Text = TextblockResult.Text.Replace("-", ""); ;
                    Globals.posnegPressed = false;
                } 
            }
        }

        private void Button_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            // Store State of Control Key
            if (InputKeyboardSource.GetKeyStateForCurrentThread(VirtualKey.Control).ToString().Contains("Down")) { Globals._isCtrlKeyPressed = true; }

            // Store State of Shift Key
            if (InputKeyboardSource.GetKeyStateForCurrentThread(VirtualKey.Shift).ToString().Contains("Down")) { Globals._isShiftKeyPressed = true; }

            // Store State of Alt Key
            if (InputKeyboardSource.GetKeyStateForCurrentThread(VirtualKey.Menu).ToString().Contains("Down")) { Globals._isMenuKeyPressed = true; }

            // Dictionary for KeyRoutedEvent Key & Button to click
            var KeyString = new Dictionary<Tuple<bool, string, string, Button>, Action<object, RoutedEventArgs>>(){
                { new Tuple<bool, string, string, Button>(true, "192", "", Percent_btn), Percent_Click }, { new Tuple<bool, string, string, Button>(false, "R", "", OneByX_btn), OneByX_Click },
                { new Tuple<bool, string, string, Button>(true, "Number7", "NumberPad7", Seven_btn), Button_Click }, { new Tuple<bool, string, string, Button>(true, "Number4", "NumberPad4", Four_btn), Button_Click },
                { new Tuple<bool, string, string, Button>(true, "Number1", "NumberPad1", One_btn), Button_Click }, { new Tuple<bool, string, string, Button>(false, "F9", "", posneg_btn), posneg_Click },
                { new Tuple<bool, string, string, Button>(false, "Delete", "", CE_btn), CE_Click }, { new Tuple<bool, string, string, Button>(false, "Q", "", Sqr_btn), sqr_Click },
                { new Tuple<bool, string, string, Button>(true, "Number8", "NumberPad8", Eight_btn), Button_Click }, { new Tuple<bool, string, string, Button>(true, "Number5", "NumberPad5", Five_btn), Button_Click },
                { new Tuple<bool, string, string, Button>(true, "Number2", "NumberPad2", Two_btn), Button_Click }, { new Tuple<bool, string, string, Button>(false, "Escape", "", C_btn), C_Click },
                { new Tuple<bool, string, string, Button>(false, "Number0", "", Sqrt_btn), Sqrt_Click }, { new Tuple<bool, string, string, Button>(true, "Number0", "NumberPad0", Zero_btn), Button_Click },
                { new Tuple<bool, string, string, Button>(true, "Number9", "NumberPad9", Nine_btn), Button_Click }, { new Tuple<bool, string, string, Button>(true, "Number3", "NumberPad3", Three_btn), Button_Click },
                { new Tuple<bool, string, string, Button>(false, "188", "", Comma_btn), Button_Click }, { new Tuple<bool, string, string, Button>(false, "Back", "", Back_btn), Back_Click },
                { new Tuple<bool, string, string, Button>(true, "191", "Divide", Divide_btn), Operator_Click }, { new Tuple<bool, string, string, Button>(false, "X", "Multiply", Multiply_btn), Operator_Click },
                { new Tuple<bool, string, string, Button>(true, "Number6", "NumberPad6", Six_btn), Button_Click }, { new Tuple<bool, string, string, Button>(false, "Number6", "Substract", Minus_btn), Operator_Click },
                { new Tuple<bool, string, string, Button>(true, "187", "Add", Plus_btn), Operator_Click }, { new Tuple<bool, string, string, Button>(false, "187", "", Equal_btn), Equal_Click }};

            // Loop that append number to calculator if Keyboard key is pressed
            for (int i = 0; i < KeyString.Count; i++)
            {
                if (KeyString.ElementAt(i).Key.Item1 && KeyString.ElementAt(i).Key.Item3.Length > 0)
                {
                    if (Globals._isShiftKeyPressed == true && e.Key.ToString() == KeyString.ElementAt(i).Key.Item2 || e.Key.ToString() == KeyString.ElementAt(i).Key.Item3) { sender = KeyString.ElementAt(i).Key.Item4; KeyString.ElementAt(i).Value(sender, null); break; }
                    
                }
                else if (KeyString.ElementAt(i).Key.Item3.Length > 0) { if (e.Key.ToString() == KeyString.ElementAt(i).Key.Item2 || e.Key.ToString() == KeyString.ElementAt(i).Key.Item3) { sender = KeyString.ElementAt(i).Key.Item4; KeyString.ElementAt(i).Value(sender, null); break; } }
                else 
                {
                    if (Globals._isCtrlKeyPressed == true && Globals._isMenuKeyPressed == true && e.Key.ToString() == KeyString.ElementAt(i).Key.Item2) { sender = KeyString.ElementAt(i).Key.Item4; KeyString.ElementAt(i).Value(sender, null); break; }
                    else if (e.Key.ToString() == KeyString.ElementAt(i).Key.Item2 && !(KeyString.ElementAt(i).Key.Item2 == "Number0") ) { sender = KeyString.ElementAt(i).Key.Item4; KeyString.ElementAt(i).Value(sender, null); break; } 
                }
            }

            if (e.Key.ToString() == "187" || e.Key.ToString() == "Add" || e.Key.ToString() == "Number6" || e.Key.ToString() == "Subtract" || e.Key.ToString() == "NumberPad6" || e.Key.ToString() == "X" || e.Key.ToString() == "Multiply" || e.Key.ToString() == "191" || e.Key.ToString() == "Divide" || e.Key.ToString() == "Back" || e.Key.ToString() == "188" || e.Key.ToString() == "Number3" || e.Key.ToString() == "NumberPad3" || e.Key.ToString() == "Number9" || e.Key.ToString() == "NumberPad9" || e.Key.ToString() == "Number0" || e.Key.ToString() == "NumberPad0" || e.Key.ToString() == "Number2" || e.Key.ToString() == "NumberPad2" || e.Key.ToString() == "Number5" || e.Key.ToString() == "NumberPad5" || e.Key.ToString() == "Number8" || e.Key.ToString() == "NumberPad8" || e.Key.ToString() == "Delete" || e.Key.ToString() == "Number1" || e.Key.ToString() == "NumberPad1" || e.Key.ToString() == "Number4" || e.Key.ToString() == "NumberPad4" || e.Key.ToString() == "Number7" || e.Key.ToString() == "NumberPad7" || e.Key.ToString() == "Escape" || e.Key.ToString() == "Q" || e.Key.ToString() == "F9" || e.Key.ToString() == "192") 
            {
                Globals._isShiftKeyPressed = false;
                Globals._isCtrlKeyPressed = false;
                Globals._isMenuKeyPressed = false;
            }
        }
    }
}