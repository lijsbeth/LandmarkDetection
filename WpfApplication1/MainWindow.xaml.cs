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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Forms;
using System.Drawing;
using Microsoft.ProjectOxford.Face;
using Microsoft.ProjectOxford.Face.Contract;

namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private readonly IFaceServiceClient faceServiceClient = new FaceServiceClient("86a9c1bbf12f4b7890602265d61b8b18");

        public MainWindow()
        {
            InitializeComponent();
        }

        private async Task<FaceLandmarks[]> UploadAndDetectFaces(string imageFilePath)
        {
            try
            {
                using (System.IO.Stream imageFileStream = System.IO.File.OpenRead(imageFilePath))
                {
                    var faces = await faceServiceClient.DetectAsync(imageFileStream, returnFaceLandmarks: true);
                    var faceLands = faces.Select(face => face.FaceLandmarks);
                    return faceLands.ToArray();
                }
            }
            catch (Exception)
            {
                return new FaceLandmarks[0];
            }
        }

        private async void BrowseButton_Click(object sender, RoutedEventArgs e)
        {
   
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            DialogResult result = fbd.ShowDialog();
            string[] files = new string[0];

            if (!string.IsNullOrWhiteSpace(fbd.SelectedPath))
            {
                files = System.IO.Directory.GetFiles(fbd.SelectedPath);
            }

            for (int i = 0; i< files.Length; i++)
            {

                string filePath = files[i];
                string dir, name;
                dir = System.IO.Path.GetDirectoryName(filePath);
                name = System.IO.Path.GetFileName(filePath);

                Bitmap img = new Bitmap(System.Drawing.Image.FromFile(filePath));

                FaceLandmarks[] faceLands = await UploadAndDetectFaces(filePath);

                if (faceLands.Length > 0)
                {
                    Graphics g = Graphics.FromImage(img);
                    System.Drawing.Pen p = new System.Drawing.Pen(System.Drawing.Color.Red, 3);
                    for (int j=0; j<faceLands.Length; j++)
                    {                         
                        g.DrawRectangle(p, new System.Drawing.Rectangle(Convert.ToInt16(Math.Round(faceLands[j].EyebrowLeftInner.X)), Convert.ToInt16(Math.Round(faceLands[j].EyebrowLeftInner.Y)), 2, 2));
                        g.DrawRectangle(p, new System.Drawing.Rectangle(Convert.ToInt16(Math.Round(faceLands[j].EyebrowLeftOuter.X)), Convert.ToInt16(Math.Round(faceLands[j].EyebrowLeftOuter.Y)), 2, 2));
                        g.DrawRectangle(p, new System.Drawing.Rectangle(Convert.ToInt16(Math.Round(faceLands[j].EyebrowRightInner.X)), Convert.ToInt16(Math.Round(faceLands[j].EyebrowRightInner.Y)), 2, 2));
                        g.DrawRectangle(p, new System.Drawing.Rectangle(Convert.ToInt16(Math.Round(faceLands[j].EyebrowRightOuter.X)), Convert.ToInt16(Math.Round(faceLands[j].EyebrowRightOuter.Y)), 2, 2));
                        g.DrawRectangle(p, new System.Drawing.Rectangle(Convert.ToInt16(Math.Round(faceLands[j].EyeLeftBottom.X)), Convert.ToInt16(Math.Round(faceLands[j].EyeLeftBottom.Y)), 2, 2));
                        g.DrawRectangle(p, new System.Drawing.Rectangle(Convert.ToInt16(Math.Round(faceLands[j].EyeLeftInner.X)), Convert.ToInt16(Math.Round(faceLands[j].EyeLeftInner.Y)), 2, 2));
                        g.DrawRectangle(p, new System.Drawing.Rectangle(Convert.ToInt16(Math.Round(faceLands[j].EyeLeftOuter.X)), Convert.ToInt16(Math.Round(faceLands[j].EyeLeftOuter.Y)), 2, 2));
                        g.DrawRectangle(p, new System.Drawing.Rectangle(Convert.ToInt16(Math.Round(faceLands[j].EyeLeftTop.X)), Convert.ToInt16(Math.Round(faceLands[j].EyeLeftTop.Y)), 2, 2));
                        g.DrawRectangle(p, new System.Drawing.Rectangle(Convert.ToInt16(Math.Round(faceLands[j].EyeRightBottom.X)), Convert.ToInt16(Math.Round(faceLands[j].EyeRightBottom.Y)), 2, 2));
                        g.DrawRectangle(p, new System.Drawing.Rectangle(Convert.ToInt16(Math.Round(faceLands[j].EyeRightInner.X)), Convert.ToInt16(Math.Round(faceLands[j].EyeRightInner.Y)), 2, 2));
                        g.DrawRectangle(p, new System.Drawing.Rectangle(Convert.ToInt16(Math.Round(faceLands[j].EyeRightOuter.X)), Convert.ToInt16(Math.Round(faceLands[j].EyeRightOuter.Y)), 2, 2));
                        g.DrawRectangle(p, new System.Drawing.Rectangle(Convert.ToInt16(Math.Round(faceLands[j].EyeRightTop.X)), Convert.ToInt16(Math.Round(faceLands[j].EyeRightTop.Y)), 2, 2));
                        g.DrawRectangle(p, new System.Drawing.Rectangle(Convert.ToInt16(Math.Round(faceLands[j].MouthLeft.X)), Convert.ToInt16(Math.Round(faceLands[j].MouthLeft.Y)), 2, 2));
                        g.DrawRectangle(p, new System.Drawing.Rectangle(Convert.ToInt16(Math.Round(faceLands[j].MouthRight.X)), Convert.ToInt16(Math.Round(faceLands[j].MouthRight.Y)), 2, 2));
                        g.DrawRectangle(p, new System.Drawing.Rectangle(Convert.ToInt16(Math.Round(faceLands[j].NoseLeftAlarOutTip.X)), Convert.ToInt16(Math.Round(faceLands[j].NoseLeftAlarOutTip.Y)), 2, 2));
                        g.DrawRectangle(p, new System.Drawing.Rectangle(Convert.ToInt16(Math.Round(faceLands[j].NoseLeftAlarTop.X)), Convert.ToInt16(Math.Round(faceLands[j].NoseLeftAlarTop.Y)), 2, 2));
                        g.DrawRectangle(p, new System.Drawing.Rectangle(Convert.ToInt16(Math.Round(faceLands[j].NoseRightAlarOutTip.X)), Convert.ToInt16(Math.Round(faceLands[j].NoseRightAlarOutTip.Y)), 2, 2));
                        g.DrawRectangle(p, new System.Drawing.Rectangle(Convert.ToInt16(Math.Round(faceLands[j].NoseRightAlarTop.X)), Convert.ToInt16(Math.Round(faceLands[j].NoseRightAlarTop.Y)), 2, 2));
                        g.DrawRectangle(p, new System.Drawing.Rectangle(Convert.ToInt16(Math.Round(faceLands[j].NoseRootLeft.X)), Convert.ToInt16(Math.Round(faceLands[j].NoseRootLeft.Y)), 2, 2));
                        g.DrawRectangle(p, new System.Drawing.Rectangle(Convert.ToInt16(Math.Round(faceLands[j].NoseRootRight.X)), Convert.ToInt16(Math.Round(faceLands[j].NoseRootRight.Y)), 2, 2));
                        g.DrawRectangle(p, new System.Drawing.Rectangle(Convert.ToInt16(Math.Round(faceLands[j].NoseTip.X)), Convert.ToInt16(Math.Round(faceLands[j].NoseTip.Y)), 2, 2));
                        g.DrawRectangle(p, new System.Drawing.Rectangle(Convert.ToInt16(Math.Round(faceLands[j].PupilLeft.X)), Convert.ToInt16(Math.Round(faceLands[j].PupilLeft.Y)), 2, 2));
                        g.DrawRectangle(p, new System.Drawing.Rectangle(Convert.ToInt16(Math.Round(faceLands[j].PupilRight.X)), Convert.ToInt16(Math.Round(faceLands[j].PupilRight.Y)), 2, 2));
                        g.DrawRectangle(p, new System.Drawing.Rectangle(Convert.ToInt16(Math.Round(faceLands[j].UnderLipBottom.X)), Convert.ToInt16(Math.Round(faceLands[j].UnderLipBottom.Y)), 2, 2));
                        g.DrawRectangle(p, new System.Drawing.Rectangle(Convert.ToInt16(Math.Round(faceLands[j].UnderLipTop.X)), Convert.ToInt16(Math.Round(faceLands[j].UnderLipTop.Y)), 2, 2));
                        g.DrawRectangle(p, new System.Drawing.Rectangle(Convert.ToInt16(Math.Round(faceLands[j].UpperLipBottom.X)), Convert.ToInt16(Math.Round(faceLands[j].UpperLipBottom.Y)), 2, 2));
                        g.DrawRectangle(p, new System.Drawing.Rectangle(Convert.ToInt16(Math.Round(faceLands[j].UpperLipTop.X)), Convert.ToInt16(Math.Round(faceLands[j].UpperLipTop.Y)), 2, 2));

                    }

                }
               
                Bitmap img_out = new Bitmap(img);
                img_out.Save(String.Concat(dir, "\\landmarked_", name));

            }

            
            
        }


    }
}
