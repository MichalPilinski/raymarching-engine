using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Media;

namespace raymarching
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        WriteableBitmap TempBitmap { set; get; }

        public MainWindow()
        {
            InitializeComponent();

            TempBitmap = new WriteableBitmap((int)this.Width, (int)this.Height, 96, 96, System.Windows.Media.PixelFormats.Bgr32, null);

            Canvas.MouseMove += new MouseEventHandler(RenderLoopEvent);

            Canvas.Source = TempBitmap;
            Canvas.Stretch = Stretch.None;
            Canvas.HorizontalAlignment = HorizontalAlignment.Left;
            Canvas.VerticalAlignment = VerticalAlignment.Top;
        }

        protected void RenderLoopEvent(object sender, EventArgs e)
        {
            DrawRandomData(TempBitmap);
        }

        private void DrawRandomData(WriteableBitmap bitmap)
        {
            try
            {
                bitmap.Lock();

                unsafe
                {
                    IntPtr pBackBuffer = bitmap.BackBuffer;
                    int RandomNum = new Random().Next(255);

                    for (int i = 0; i < bitmap.PixelHeight; i++)
                    {
                        for (int j = 0; j < bitmap.PixelWidth; j++)
                        {
                            pBackBuffer += 4;

                            // Compute the pixel's color.
                            int color_data = 0 << 16; // R
                            color_data |= 0 << 8;   // G
                            color_data |= 0 << 0;   // B

                            // Assign the color data to the pixel.
                            *((int*)pBackBuffer) = color_data;
                        }
                    }
                }

                // Specify the area of the bitmap that changed.
                bitmap.AddDirtyRect(new Int32Rect(0, 0, bitmap.PixelWidth, bitmap.PixelHeight));
            }
            finally
            {
                // Release the back buffer and make it available for display.
                bitmap.Unlock();
            }
        }
    }
}
