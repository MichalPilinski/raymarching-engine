using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using raymarching.ComputationClasses;
using System.Numerics;
using raymarching.Interfaces;
using raymarching.DistanceProviders;
using raymarching.Lights;

namespace raymarching
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        WriteableBitmap TempBitmap { set; get; }
        Renderer Renderer { set; get; }

        public MainWindow()
        {
            InitializeComponent();
            
            TempBitmap = new WriteableBitmap((int)this.Width, (int)this.Height, 96, 96, System.Windows.Media.PixelFormats.Bgr32, null);
         
            InitializeCanvas();
            InitializeRenderer();
            CreateScene();

            Canvas.MouseDown += new MouseButtonEventHandler(RenderLoopEvent);
        }

        private void CreateScene()
        {
            var SceneObjects = new List<IDistanceProvider>();

            var SpheresConst = new Vector3(1, 10, 10);
            SceneObjects.Add(new Sphere(new Vector3(15, 15, 15), 5, SpheresConst));
            SceneObjects.Add(new Sphere(new Vector3(15, 15, 8), 2, new Vector3(5, 5, 5)));
            SceneObjects.Add(new Sphere(new Vector3(15, 6, 7), 2, SpheresConst));

            var SceneLights = new List<ILight>();
            SceneLights.Add(new PointLight(new Vector3(10, 10, 0), 1));

            this.Renderer.SetScene(SceneObjects, SceneLights);
        }

        private void InitializeCanvas()
        {
            Canvas.Source = TempBitmap;

            Canvas.Stretch = Stretch.None;
            Canvas.HorizontalAlignment = HorizontalAlignment.Left;
            Canvas.VerticalAlignment = VerticalAlignment.Top;
        }

        private void InitializeRenderer()
        {
            Renderer = new Renderer(new Vector2((int)this.Width, (int)this.Height), new Vector2(0.1f, 100f));
        }

        protected void RenderLoopEvent(object sender, EventArgs e)
        {
            System.Drawing.Color[,] RenderPixelArray = Renderer.Render();
            DrawData(TempBitmap, RenderPixelArray);
        }

        private void DrawData(WriteableBitmap Bitmap, System.Drawing.Color[,] Pixels)
        {
            try
            {
                Bitmap.Lock();

                unsafe
                {
                    IntPtr pBackBuffer = Bitmap.BackBuffer;
                    int RandomNum = new Random().Next(255);

                    for (int i = 0; i < Bitmap.PixelHeight; i++)
                    {
                        for (int j = 0; j < Bitmap.PixelWidth; j++)
                        {
                            pBackBuffer += 4;

                            // Compute the pixel's color.
                            int color_data = Pixels[j,i].R << 16; // R
                            color_data |= Pixels[j,i].G << 8;   // G
                            color_data |= Pixels[j,i].B << 0;   // B

                            // Assign the color data to the pixel.
                            *((int*)pBackBuffer) = color_data;
                        }
                    }
                }

                // Specify the area of the bitmap that changed.
                Bitmap.AddDirtyRect(new Int32Rect(0, 0, Bitmap.PixelWidth, Bitmap.PixelHeight));
            }
            finally
            {
                // Release the back buffer and make it available for display.
                Bitmap.Unlock();
            }
        }
    }
}
