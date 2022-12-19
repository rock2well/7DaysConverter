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

namespace SevenDaysConverter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Model Model { get; set; } = new Model();

        List<BitmapImage> Images { get; set; } = new List<BitmapImage>();
        BitmapImage NullImage { get; set; } = new BitmapImage();

        public MainWindow()
        {
            InitializeComponent();

            //! 画像ロード
            for(int i=0;i<26;++i)
            {
                char filename = (char)('a' + i);
                string path = "dot/" + filename + ".bmp";
                using(var fs = new FileStream(path, FileMode.Open, FileAccess.Read))
                {
                    BitmapImage image = new BitmapImage();

                    image.BeginInit();
                    image.StreamSource = fs;
                    image.DecodePixelWidth = 8;
                    image.DecodePixelHeight = 8;
                    image.CacheOption= BitmapCacheOption.OnLoad;
                    image.CreateOptions = BitmapCreateOptions.None;
                    image.EndInit();
                    image.Freeze();

                    Images.Add(image);
                }
            }
            //! nullイメージ
            {
                string nullFilePath = "dot/null.bmp";
                using(var fs = new FileStream(nullFilePath,FileMode.Open, FileAccess.Read))
                {
                    NullImage.BeginInit();
                    NullImage.StreamSource = fs;
                    NullImage.DecodePixelWidth = 8;
                    NullImage.DecodePixelHeight = 8;
                    NullImage.CacheOption= BitmapCacheOption.OnLoad;
                    NullImage.CreateOptions = BitmapCreateOptions.None;
                    NullImage.EndInit();
                    NullImage.Freeze();
                }
            }

            ResetImages();
            RefreshEnglishText();
        }

        private void ResetImages()
        {
            for (int i = 0; i < Model.Word.Length; ++i)
            {
                SetImage(i, -1);
            }
        }

        private void SetImage(int imageNo, int count)
        {
            Image? target = null;
            switch(imageNo)
            {
                case 0:
                    target = Image00;
                    break;
                case 1:
                    target = Image01;
                    break;
                case 2:
                    target = Image02;
                    break;
                case 3:
                    target = Image03;
                    break;
                case 4:
                    target = Image04;
                    break;
                case 5:
                    target = Image05;
                    break;
                case 6:
                    target = Image06;
                    break;
                case 7:
                    target = Image07;
                    break;
                case 8:
                    target = Image08;
                    break;
                case 9:
                    target = Image09;
                    break;
                case 10:
                    target = Image10;
                    break;
                case 11:
                    target = Image11;
                    break;
                default:
                    break;
            }
            if (target != null)
            {
                if (count >= 0 && count < Images.Count)
                    target.Source = Images[count];
                else
                    target.Source = NullImage;
            }
        }

        private void RefreshEnglishText()
        {
            EnglishText.Text = "";
            foreach(var c in Model.Word)
            {
                if('a' <= c && c <= 'z')
                    EnglishText.Text += c;
                else
                    EnglishText.Text += ' ';
            }
        }

        private void Convert_Click(object sender, RoutedEventArgs e)
        {
            Model.Convert();

            Candidates.Text = "";
            foreach(var candidate in Model.Candidate)
            {
                Candidates.Text += candidate + ", "; 
            }
        }

        private void UpButton_Click(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            string buttonNoStr = button.Name.Replace("UpButton", "");
            int buttonNo = int.Parse(buttonNoStr);

            Model.Sub(buttonNo);
            SetImage(buttonNo, Model.Word[buttonNo] - 'a');
            RefreshEnglishText();
        }

        private void DownButtton_Click(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            string buttonNoStr = button.Name.Replace("DownButton", "");
            int buttonNo = int.Parse(buttonNoStr);

            Model.Add(buttonNo);
            SetImage(buttonNo, Model.Word[buttonNo] - 'a');
            RefreshEnglishText();
        }

        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            string buttonNoStr = button.Name.Replace("ResetButton", "");
            int buttonNo = int.Parse(buttonNoStr);

            Model.Reset(buttonNo);
            SetImage(buttonNo, -1);
            RefreshEnglishText();
        }

        private void ResetAll_Click(object sender, RoutedEventArgs e)
        {
            Model.Reset();

            ResetImages();

            RefreshEnglishText();
        }
    }
}
