using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ValueImage.Interface;
using ValueImage;
using ValueImageUI.FuncForm;
using ValueImage.Infrastructure;
using ValueImageUI.FunForm;
using MathHelper.Infrastructure;
using System.IO;

namespace ValueImageUI
{
    public partial class FrmMain : Form
    {
        String fileName;
        OpenFileDialog openFileDialog = new OpenFileDialog();
        IValueImage valueImage = ValueImageManager.GetValueImage(System.Drawing.Imaging.PixelFormat.Format24bppRgb);
        public FrmMain()
        {
            InitializeComponent();
            openFileDialog.Filter = ".jpg图片|*.jpg";
            this.PicShow.Invalidated += delegate(Object sender, InvalidateEventArgs e)
            {
                this.pictureBox1.Image = this.PicShow.Image;
            };
        }

        private void BtnOpen_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                fileName = openFileDialog.FileName;
                this.PicShow.Image = Image.FromFile(fileName);
                this.PicShow.Refresh();
            }
        }

        private void BtnOrgImage_Click(object sender, EventArgs e)
        {
            this.PicShow.Image = Image.FromFile(fileName);
            this.PicShow.Refresh();
        }

        private void BtnErode_Click(object sender, EventArgs e)
        {
            Bitmap srcImage = (Bitmap)this.PicShow.Image;
            ((IDisNoise)valueImage).Erode(srcImage, ValueImage.Infrastructure.FilterWindowType.Cros3);
            this.PicShow.Refresh();
        }

        private void BtnDelation_Click(object sender, EventArgs e)
        {
            Bitmap srcImage = (Bitmap)this.PicShow.Image;
            ((IDisNoise)valueImage).Delation(srcImage, ValueImage.Infrastructure.FilterWindowType.Cros5);
            this.PicShow.Refresh();
        }

        private void BtnOpen_Click_1(object sender, EventArgs e)
        {
            Bitmap srcImage = (Bitmap)this.PicShow.Image;
            ((IDisNoise)valueImage).Open(srcImage, ValueImage.Infrastructure.FilterWindowType.Hori3);
            this.PicShow.Refresh();
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            Bitmap srcImage = (Bitmap)this.PicShow.Image;
            ((IDisNoise)valueImage).Close(srcImage, ValueImage.Infrastructure.FilterWindowType.Hori5);
            this.PicShow.Refresh();
        }

        private void BtnGrayErode_Click(object sender, EventArgs e)
        {
            FrmTemplateSize frmSetTemplate = FrmTemplateSize.GetInstance();
            frmSetTemplate.SetAction(grayErode);
            frmSetTemplate.Show();
            frmSetTemplate.Activate();
        }

        private void grayErode(Byte[] template)
        {
            var srcImage = (Bitmap)this.PicShow.Image;
            for (int i = 0; i < template.Length; i++)
            {
                if (template[i] == 0)
                    template[i] = 255;
            }

            ((IDisNoise)valueImage).GrayErode(srcImage, template);
            this.PicShow.Refresh();
        }

        private void BtnGrayDelation_Click(object sender, EventArgs e)
        {
            FrmTemplateSize frmSetTemplate = FrmTemplateSize.GetInstance();
            frmSetTemplate.SetAction(grayDeilation);
            frmSetTemplate.Show();
            frmSetTemplate.Activate();
        }

        private void grayDeilation(Byte[] template)
        {
            var srcImage = (Bitmap)this.PicShow.Image;
            ((IDisNoise)valueImage).GrayDelation(srcImage, template);
            this.PicShow.Refresh();
        }

        private void BtnGrayOpen_Click(object sender, EventArgs e)
        {
            FrmTemplateSize frmSetTemplate = FrmTemplateSize.GetInstance();
            frmSetTemplate.SetAction(grayOpen);
            frmSetTemplate.Show();
            frmSetTemplate.Activate();
        }

        private void grayOpen(Byte[] template)
        {
            var srcImage = (Bitmap)this.PicShow.Image;
            ((IDisNoise)valueImage).GrayOpen(srcImage, template);
            this.PicShow.Refresh();
        }

        private void BtnGrayClose_Click(object sender, EventArgs e)
        {
            FrmTemplateSize frmSetTemplate = FrmTemplateSize.GetInstance();
            frmSetTemplate.SetAction(grayClose);
            frmSetTemplate.Show();
            frmSetTemplate.Activate();
        }

        private void grayClose(Byte[] template)
        {
            var srcImage = (Bitmap)this.PicShow.Image;
            ((IDisNoise)valueImage).GrayClose(srcImage, template);
            this.PicShow.Refresh();
        }

        private void BtnMorphologic_Click(object sender, EventArgs e)
        {
            FrmTemplateSize frmSetTemplate = FrmTemplateSize.GetInstance();
            frmSetTemplate.SetAction(grayMorphologic);
            frmSetTemplate.Show();
            frmSetTemplate.Activate();
        }

        private void grayMorphologic(Byte[] template)
        {
            var srcImage = (Bitmap)this.PicShow.Image;
            ((IDisNoise)valueImage).GrayMorphologic(srcImage, template);
            this.PicShow.Refresh();
        }

        private void BtnGrayScaleMax_Click(object sender, EventArgs e)
        {
            var srcImage = (Bitmap)this.PicShow.Image;
            ((IGray)valueImage).ConvertToGrayscale(srcImage, ValueImage.Infrastructure.GrayscaleType.Maximum);
            this.PicShow.Refresh();
        }

        private void BtnGrayscaleMin_Click(object sender, EventArgs e)
        {
            var srcImage = (Bitmap)this.PicShow.Image;
            ((IGray)valueImage).ConvertToGrayscale(srcImage, ValueImage.Infrastructure.GrayscaleType.Minimal);
            this.PicShow.Refresh();
        }

        private void BtnGrayscale_Click(object sender, EventArgs e)
        {
            var srcImage = (Bitmap)this.PicShow.Image;
            ((IGray)valueImage).ConvertToGrayscale(srcImage, ValueImage.Infrastructure.GrayscaleType.Average);
            this.PicShow.Refresh();
        }

        private void BtnGrayscalePower_Click(object sender, EventArgs e)
        {
            var srcImage = (Bitmap)this.PicShow.Image;
            ((IGray)valueImage).ConvertToGrayscale(srcImage, 0.299F, 0.587F, 0.114F);
            this.PicShow.Refresh();
        }

        private void BtnGrayStrech_Click(object sender, EventArgs e)
        {
            FrmGrayscaleStretch frmGraystretch = FrmGrayscaleStretch.GetInstance();
            frmGraystretch.SetAction(grayscaleStretch);
            frmGraystretch.Show();
        }
        private void grayscaleStretch(Point point1, Point point2)
        {
            var srcImage = (Bitmap)this.PicShow.Image;
            ((IGray)valueImage).GrayscaleStretch(srcImage, point1.X, point1.Y, point2.X, point2.Y);
            this.PicShow.Refresh();
        }

        private void BtnGrayStrechInner_Click(object sender, EventArgs e)
        {
            var srcImage = (Bitmap)this.PicShow.Image;
            ((IGray)valueImage).GrayscaleStretch(srcImage);
            this.PicShow.Refresh();
        }

        private void BtnLinearChange_Click(object sender, EventArgs e)
        {
            var srcImage = (Bitmap)this.PicShow.Image;
            valueImage.LinearChange(srcImage, -1F, 255F);
            this.PicShow.Refresh();
        }

        private void BtnHistEqualization_Click(object sender, EventArgs e)
        {
            var srcImage = (Bitmap)this.PicShow.Image;
            valueImage.HistEqualization(srcImage);
            this.PicShow.Refresh();
        }

        private void BtnMove_Click(object sender, EventArgs e)
        {
            var srcImage = (Bitmap)this.PicShow.Image;
            valueImage.Move(srcImage, 20, 20);
            this.PicShow.Refresh();
        }

        private void BtnGaussNoise_Click(object sender, EventArgs e)
        {
            var srcImage = (Bitmap)this.PicShow.Image;
            ((INoise)valueImage).Noise(srcImage, ValueImage.Infrastructure.NoiseType.Gauss);
            this.PicShow.Refresh();
        }

        private void BtnRayleighNoise_Click(object sender, EventArgs e)
        {
            var srcImage = (Bitmap)this.PicShow.Image;
            ((INoise)valueImage).Noise(srcImage, ValueImage.Infrastructure.NoiseType.Rayleigh);
            this.PicShow.Refresh();
        }

        private void BtnIndexNoise_Click(object sender, EventArgs e)
        {
            var srcImage = (Bitmap)this.PicShow.Image;
            ((INoise)valueImage).Noise(srcImage, ValueImage.Infrastructure.NoiseType.Index);
            this.PicShow.Refresh();
        }

        private void BtnPepperNoise_Click(object sender, EventArgs e)
        {
            var srcImage = (Bitmap)this.PicShow.Image;
            ((INoise)valueImage).Noise(srcImage, ValueImage.Infrastructure.NoiseType.Pepper);
            this.PicShow.Refresh();
        }

        private void BtnFFT_Click(object sender, EventArgs e)
        {
            var srcImage = (Bitmap)this.PicShow.Image;
            ((IFrequency)valueImage).FFT(srcImage, false);
            this.PicShow.Refresh();
        }

        private void BtnAmplitude_Click(object sender, EventArgs e)
        {
            var srcImage = (Bitmap)this.PicShow.Image;
            ((IFrequency)valueImage).Amplitude(srcImage);
            this.PicShow.Refresh();
        }

        private void BtnPharse_Click(object sender, EventArgs e)
        {
            var srcImage = (Bitmap)this.PicShow.Image;
            ((IFrequency)valueImage).Phase(srcImage);
            this.PicShow.Refresh();
        }

        private void BtnLowpass_Click(object sender, EventArgs e)
        {
            var srcImage = (Bitmap)this.PicShow.Image;
            ((IFilter)valueImage).ComponentFilter(srcImage, ValueImage.Infrastructure.RateFilterType.LowPass);
            this.PicShow.Refresh();
        }

        private void BtnHighpass_Click(object sender, EventArgs e)
        {
            var srcImage = (Bitmap)this.PicShow.Image;
            ((IFilter)valueImage).HighpassFilter(srcImage, ValueImage.Infrastructure.RateFilterRadius.HighPass);
            this.PicShow.Refresh();
        }

        private void BtnBandpass_Click(object sender, EventArgs e)
        {
            var srcImage = (Bitmap)this.PicShow.Image;
            ((IFilter)valueImage).BandpassFilter(srcImage, ValueImage.Infrastructure.RateFilterRadius.BandPassInner, ValueImage.Infrastructure.RateFilterRadius.BandPassOuter);
            this.PicShow.Refresh();
        }

        private void BtnBandstop_Click(object sender, EventArgs e)
        {
            var srcImage = (Bitmap)this.PicShow.Image;
            ((IFilter)valueImage).ComponentFilter(srcImage, ValueImage.Infrastructure.RateFilterType.BandStop);
            this.PicShow.Refresh();
        }

        private void BtnOrientationFilter_Click(object sender, EventArgs e)
        {
            var srcImage = (Bitmap)this.PicShow.Image;
            ((IFilter)valueImage).OrientationFilter(srcImage, 60, 120);
            this.PicShow.Refresh();
        }

        private void BtnMeanFilter_Click(object sender, EventArgs e)
        {
            var srcImage = (Bitmap)this.PicShow.Image;
            ((IFilter)valueImage).MeanFilter(srcImage, ValueImage.Infrastructure.TemplateType.T3x3);
            this.PicShow.Refresh();
        }

        private void BtnMedianFitler_Click(object sender, EventArgs e)
        {
            var srcImage = (Bitmap)this.PicShow.Image;
            ((IFilter)valueImage).MedianFilter(srcImage, ValueImage.Infrastructure.TemplateType.T3x3);
            this.PicShow.Refresh();
        }

        private void BtnGaussFilter_Click(object sender, EventArgs e)
        {
            var srcImage = (Bitmap)this.PicShow.Image;
            ((IFilter)valueImage).GaussFilter(srcImage, 2);
            this.PicShow.Refresh();
        }

        private void BtnStatisticFilter_Click(object sender, EventArgs e)
        {
            var srcImage = (Bitmap)this.PicShow.Image;
            ((IFilter)valueImage).StatisticFilter(srcImage, ValueImage.Infrastructure.TemplateType.T5x5, 0.8);
            this.PicShow.Refresh();
        }

        private void BtnMaskEdge_Click(object sender, EventArgs e)
        {
            FrmMask frmMask = FrmMask.GetInstance();
            frmMask.SetAction(maskEdge);
            frmMask.Show();
            frmMask.Activate();
        }

        private void maskEdge(MaskType type, Int32 thresholding)
        {
            var srcImage = (Bitmap)this.PicShow.Image;
            ((IEdge)valueImage).MaskEgde(srcImage, type, thresholding);
            this.PicShow.Refresh();
        }

        private void BtnGaussEdge_Click(object sender, EventArgs e)
        {
            FrmGaussOperator frmGauss = FrmGaussOperator.GetInstance();
            frmGauss.SetAction(gaussEdge);
            frmGauss.Show();
            frmGauss.Activate();
        }

        private void gaussEdge(GaussFilterType type, Double sigma, Double thresholding)
        {
            var srcImage = (Bitmap)this.PicShow.Image;
            ((IEdge)valueImage).GaussEgde(srcImage, type, sigma, thresholding);
            this.PicShow.Refresh();
        }

        private void BtnDivisionWord_Click(object sender, EventArgs e)
        {
            //var srcImage = (Bitmap)this.PicShow.Image;
            //IDivision divisionImage = (IDivision)valueImage;
            //divisionImage.Projection(srcImage, 250);
            //this.PicShow.Refresh();
        }

        private void BtnProjectionHori_Click(object sender, EventArgs e)
        {
            var srcImage = (Bitmap)this.PicShow.Image;

            List<Int32> list = new List<int>();

            for (int i = 1; i < 180; i++)
            {
                Int32[] projections = ((IFrequency)valueImage).Projection(srcImage, System.Math.PI / 180 * i, OrientationType.Horizontal);
                Int32 sum = 0;
                foreach (var item in projections)
                {
                    sum += item;
                }
                list.Add(sum);
            }

            Int32 maxIndex, max = 0;
            for (int i = 0; i < list.Count; i++)
            {
                if (max < list[i])
                {
                    max = list[i];
                    maxIndex = i;
                }
            }

            Int32[] test = ((IFrequency)valueImage).Projection(srcImage, System.Math.PI - System.Math.PI / 180, OrientationType.Horizontal);




            FrmOrientation frmOrientation = new FrmOrientation(projection, OrientationType.Horizontal, srcImage.Width, srcImage.Height);
            frmOrientation.Show();
        }

        private void BtnProjectionVert_Click(object sender, EventArgs e)
        {
            var srcImage = (Bitmap)this.PicShow.Image;
            FrmOrientation frmOrientation = new FrmOrientation(projection, OrientationType.Vertical, srcImage.Width, srcImage.Height);
            frmOrientation.Show();
        }

        private Int32[] projection(OrientationType orientType)
        {
            var srcImage = (Bitmap)this.PicShow.Image;
            return ((IFrequency)valueImage).Projection(srcImage, orientType);
        }

        private void BtnCutRectangle_Click(object sender, EventArgs e)
        {
            var srcImage = (Bitmap)this.PicShow.Image;
            var test = ((IDivision)valueImage).CutRectangle(srcImage, 0, 0, 25, 30);
        }

        private void BtnBinarization_Click(object sender, EventArgs e)
        {
            var srcImage = (Bitmap)this.PicShow.Image;
            valueImage.Binarization(srcImage, 254);
            this.PicShow.Refresh();
        }

        private void BtnOptimalThresholding_Click(object sender, EventArgs e)
        {
            var srcImage = (Bitmap)this.PicShow.Image;
            ((IGray)valueImage).OptimalThreshold(srcImage);
            this.PicShow.Refresh();
        }

        private void BtnInvertColor_Click(object sender, EventArgs e)
        {
            var srcImage = (Bitmap)this.PicShow.Image;
            valueImage.InvertColor(srcImage);
            this.PicShow.Refresh();
        }

        private void BtnOstuThreshold_Click(object sender, EventArgs e)
        {
            var srcImage = (Bitmap)this.PicShow.Image;
            ((IGray)valueImage).OstuThreshold(srcImage);
            this.PicShow.Refresh();
        }



        private void convertValue(Byte[,] data)
        {
            for (int i = 0; i < data.GetLength(0); i++)
            {
                for (int j = 0; j < data.GetLength(1); j++)
                {
                    data[i, j] = data[i, j] == 0 ? (Byte)1 : (Byte)0;
                }
            }
        }
        private void convertValue(Int32[] data)
        {
            for (int i = 0; i < data.Length; i++)
            {
                data[i] = data[i] == 0 ? 1 : 0;
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Bitmap orgImage = (Bitmap)this.PicShow.Image;
            orgImage.Save("D:\\asdsadsadasddsa.jpg");

            //String name = Path.GetFileName(this.fileName);

            //((IGray)valueImage).ConvertToGrayscale(orgImage, GrayscaleType.Maximum);
            //((IGray)valueImage).OptimalThreshold(orgImage);
            //orgImage = ((IBinarization)valueImage).BileanerZoom(orgImage, 40, 40);
            //orgImage.Save(Path.Combine("D:\\", name));





            changeImageForChina();




            //Bitmap orgImage = (Bitmap)this.PicShow.Image;
            //Int32 angle = ((IOther)valueImage).OffsetAngle(orgImage);
            //this.PicShow.Image = ((IGeometry)valueImage).BileanerRotate(orgImage, -2);
            //this.PicShow.Refresh();

            //Bitmap orgImage = (Bitmap)this.PicShow.Image;
            //Int32[] horiProject = ((IFrequency)valueImage).Projection(orgImage, OrientationType.Horizontal);

            //Int32 total = 0;
            //for (int i = 0; i < horiProject.Length; i++)
            //{
            //    total += horiProject[i];
            //}

            //Int32 median = total / horiProject.Length;


            //List<Int32> test = new List<int>();
            //for (int i = 0; i < horiProject.Length; i++)
            //{
            //    if (horiProject[i] > median)
            //        test.Add(i);
            //}


            //var asd = "asd";

            //Bitmap srcImage = (Bitmap)this.PicShow.Image;
            //((IGray)valueImage).ConvertToGrayscale(srcImage, GrayscaleType.Maximum);
            ////((IFilter)valueImage).MedianFilter(srcImage, TemplateType.T3x3);
            //((IGray)valueImage).OptimalThreshold(srcImage);
            //((IDisNoise)valueImage).kFillFilter(srcImage);
            //Int32 angle = ((IOther)valueImage).OffsetAngle(srcImage);
            //srcImage = ((IGeometry)valueImage).BileanerRotate(srcImage, 90 - angle);
            //((IGray)valueImage).ConvertToGrayscale(srcImage, GrayscaleType.Maximum);
            //((IGray)valueImage).OptimalThreshold(srcImage);

            //Int32[] horiProjection = ((IFrequency)valueImage).Projection(srcImage, OrientationType.Horizontal);
            //Line[] horiLines = SeparatorLine(horiProjection, 255, 1);
            //Line line = new Line(0, 0);
            //Int32 max = 0;
            //for (int i = 0; i < horiLines.Length - 1; i++)
            //{
            //    if ((horiLines[i + 1].Start - horiLines[i].End) > max)
            //    {
            //        line = new Line(horiLines[i].End, horiLines[i + 1].Start);
            //        max = (horiLines[i].End - horiLines[i + 1].Start);
            //    }
            //}
            //if (line.End != 0)
            //    srcImage = ((IDivision)valueImage).CutRectangle(srcImage, line.Start, 0, line.End, srcImage.Width);

            //this.PicShow.Refresh();

            //cutWord(srcImage);
        }

        private void changeImageForChina()
        {
            //Bitmap orgImage = (Bitmap)this.PicShow.Image;

            DirectoryInfo dir = new DirectoryInfo(@"D:\wordtest");
            foreach (var file in dir.GetFiles())
            {
                String filename = file.Name;

                Bitmap orgImage = (Bitmap)Image.FromFile(file.FullName);
                Bitmap srcImage = (Bitmap)orgImage.Clone();
                ((IGray)valueImage).ConvertToGrayscale(srcImage, GrayscaleType.Maximum);
                ((IGray)valueImage).OptimalThreshold(srcImage);
                Int32[] horiPro = ((IFrequency)valueImage).Projection(srcImage, OrientationType.Horizontal);
                Int32 startIndex = 0;
                Int32 endIndex = 0;
                for (int i = 0; i < horiPro.Length; i++)
                {
                    if (horiPro[i] == 255)
                        startIndex = i;
                    else
                        break;
                }
                for (int i = horiPro.Length - 1; i >= 0; i--)
                {
                    if (horiPro[i] == 255)
                        endIndex = i;
                    else
                        break;
                }

                Int32 vstartIndex = 0, vendIndex = 0;
                srcImage = ((IDivision)valueImage).CutRectangle(srcImage, startIndex, 0, endIndex, orgImage.Width);
                Int32[] vertPro = ((IFrequency)valueImage).Projection(srcImage, OrientationType.Vertical);
                for (int i = 0; i < vertPro.Length; i++)
                {
                    if (vertPro[i] == 255)
                        vstartIndex = i;
                    else
                        break;
                }
                for (int i = vertPro.Length - 1; i >= 0; i--)
                {
                    if (vertPro[i] == 255)
                        vendIndex = i;
                    else
                        break;
                }

                orgImage = ((IDivision)valueImage).CutRectangle(orgImage, startIndex + 1, vstartIndex + 1, endIndex, vendIndex);
                ((IGray)valueImage).ConvertToGrayscale(orgImage, GrayscaleType.Maximum);
                ((IGray)valueImage).OptimalThreshold(orgImage);
                orgImage = ((IGeometry)valueImage).BileanerZoom(orgImage, 40, 40);
                ((IGray)valueImage).ConvertToGrayscale(orgImage, GrayscaleType.Maximum);
                ((IGray)valueImage).OptimalThreshold(orgImage);
                orgImage.Save("D:\\wordword\\" + filename);
            }
        }

        private void cutWord(Bitmap srcImage)
        {
            this.PicShow.Image = srcImage;
            this.PicShow.Refresh();
            Int32[] frequency = ((IFrequency)valueImage).Projection(srcImage, OrientationType.Vertical);
            Line[] lines = SeparatorLine(frequency, 250, 1);

            List<Bitmap> srcMaps = new List<Bitmap>();
            for (int i = 0; i < lines.Length - 1; i++)
            {
                Bitmap temp = ((IDivision)valueImage).CutRectangle(srcImage, 0, lines[i].End, srcImage.Height, lines[i + 1].Start);

                if (temp.Width > 20)
                {
                    srcMaps.Add(((IDivision)valueImage).CutRectangle(temp, 0, 0, srcImage.Height, temp.Width / 2));
                    srcMaps.Add(((IDivision)valueImage).CutRectangle(temp, 0, temp.Width / 2, srcImage.Height, temp.Width));
                }
                else
                    srcMaps.Add(temp);
            }
            Int32[] horifrequency;

            DirectoryInfo dir = new DirectoryInfo("D:\\numbertest");
            FileInfo[] files = dir.GetFiles();
            Int32 startNum = files.Length;
            for (int i = 0; i < srcMaps.Count; i++)
            {
                if (srcMaps[i].Width <= 4) continue;

                horifrequency = ((IFrequency)valueImage).Projection(srcMaps[i], OrientationType.Horizontal);
                Line[] horilines = SeparatorLine(horifrequency, 250, 1);
                if (horilines.Length == 2)
                    srcMaps[i] = ((IDivision)valueImage).CutRectangle(srcMaps[i], horilines[0].End, 0, horilines[1].Start + 1, srcMaps[i].Width);
                else if (horilines.Length == 3)
                    srcMaps[i] = ((IDivision)valueImage).CutRectangle(srcMaps[i], horilines[1].End, 0, horilines[2].Start + 1, srcMaps[i].Width);


                srcMaps[i] = ((IGeometry)valueImage).BileanerZoom(srcMaps[i], 9, 12);

                srcMaps[i].Save("D:\\numbertest\\" + startNum + ".jpg");
                startNum++;
            }
        }

        /// <summary>
        ///  获得分割线
        /// </summary>
        /// <param name="projection">投影数组</param>
        /// <param name="thresholding">分割阈值</param>
        /// <param name="leastWidth">最小宽度</param>
        private Line[] SeparatorLine(Int32[] projection, Int32 thresholding, Int32 leastWidth)
        {
            List<Line> lines = new List<Line>();
            Int32 start = -1;
            Int32 count = 0;
            for (int i = 0; i < projection.Length; i++)
            {
                if (projection[i] >= thresholding)
                    start = i;
                else
                    start = -1;
                if (start != -1) count++;
                else
                {
                    if (count >= leastWidth)
                        lines.Add(new Line(i - count, i));
                    start = -1;
                    count = 0;
                }
            }
            if (count > 0)
                lines.Add(new Line(start - count, start));
            return lines.ToArray();
        }


        private class Line
        {
            public Line(Int32 start, Int32 end)
            {
                this.Start = start;
                this.End = end;
            }

            internal Int32 Start { get; set; }

            internal Int32 End { get; set; }
        }

        private void BtnUniformQuantization_Click(object sender, EventArgs e)
        {
            var srcImage = (Bitmap)this.PicShow.Image;
            List<ColorBytes> list = new List<ColorBytes>();
            list.Add(new ColorBytes(Color.White.B, Color.White.G, Color.White.R));
            list.Add(new ColorBytes(Color.Black.B, Color.Blue.G, Color.Blue.R));

            ((IDivision)valueImage).UniformQuantization(srcImage, list.ToArray());

            this.PicShow.Refresh();
        }

        private void BtnThining_Click(object sender, EventArgs e)
        {
            var srcImage = (Bitmap)this.PicShow.Image;
            ((IDivision)valueImage).HilditchThinning(srcImage);
            this.PicShow.Refresh();
        }

        private void BtnZoomNN_Click(object sender, EventArgs e)
        {
            var srcImage = (Bitmap)this.PicShow.Image;
            ((IGeometry)valueImage).Zoom(srcImage, 0.5, 0.5, ZoomType.NearestNeighbor);
            this.PicShow.Refresh();
        }

        private void BtnAmphilinearity_Click(object sender, EventArgs e)
        {
            var srcImage = (Bitmap)this.PicShow.Image;
            ((IGeometry)valueImage).Zoom(srcImage, 0.5, 0.5, ZoomType.Amphilinearity);
            this.PicShow.Refresh();
        }

        private void BtnZhangThinning_Click(object sender, EventArgs e)
        {
            var srcImage = (Bitmap)this.PicShow.Image;
            ((IDivision)valueImage).ZhangThinning(srcImage);
            this.PicShow.Refresh();
        }

        private void ZhangExpendThinning_Click(object sender, EventArgs e)
        {
            var srcImage = (Bitmap)this.PicShow.Image;
            ((IDivision)valueImage).ZhangExpandThinning(srcImage);
            this.PicShow.Refresh();
        }

        private void BtnGabor_Click(object sender, EventArgs e)
        {
            var srcImage = (Bitmap)this.PicShow.Image;
            GaborParam parma = new GaborParam
            {
                Gamma = 0.5,
                Theta = 0.5,
                Lambda = 0.5,
                Sigma = 0.5,
                Psi = 0.5
            };
            ((IFrequency)valueImage).Gabor(srcImage, parma);
            this.PicShow.Refresh();
        }

        private void BtnSplice_Click(object sender, EventArgs e)
        {
            var srcImage = (Bitmap)this.PicShow.Image;
            var infos = new ImageInfo[5];
            for (int i = 0; i < infos.Length; i++)
            {
                infos[i] = new ImageInfo();
                infos[i].OrgImage = srcImage;
                infos[i].Location = new Point(srcImage.Width * (i + 1) + 20 * i, 40);
                infos[i].Size = new Size(srcImage.Width + 30, srcImage.Height + 30);
            }
            Bitmap image = ((IGeometry)valueImage).SpliceImage(infos);
            this.PicShow.Image = image;
            this.PicShow.Refresh();
        }

        private void BtnkFill_Click(object sender, EventArgs e)
        {
            var srcImage = (Bitmap)this.PicShow.Image;
            ((IDisNoise)valueImage).kFillFilter(srcImage);
            this.PicShow.Refresh();
        }

        private void BtnDensity_Click(object sender, EventArgs e)
        {
            var srcImage = (Bitmap)this.PicShow.Image;
            Rectangle[] rects = ((IFrequency)valueImage).Density(srcImage, 50);
            foreach (var item in rects)
            {
                ((IGeometry)valueImage).FillRectangle(srcImage, item.Y, item.X, item.Y + item.Height, item.X + item.Width, Color.LightBlue);
            }

            this.PicShow.Refresh();
        }

        private void BtnBoxImage_Click(object sender, EventArgs e)
        {

        }

        private void BtnRotate_Click(object sender, EventArgs e)
        {
            //for (int i = 0; i < 32; i++)
            //{
            var srcImage = (Bitmap)this.PicShow.Image;
            //this.PicShow.Image = ((IGeometry)valueImage).BileanerRotate(srcImage, 90);
            //this.PicShow.Image.Save("D:\\TEST.JPG");
            //}
            this.PicShow.Refresh();
        }

        private void BtnFillBreakpoint_Click(object sender, EventArgs e)
        {
            var srcImage = (Bitmap)this.PicShow.Image;
            ((IBinarization)valueImage).FillBreakpoint(srcImage, FilterLevelType.Level01);
            this.PicShow.Refresh();
        }

        private void BtnNoiseKiller_Click(object sender, EventArgs e)
        {
            var srcImage = (Bitmap)this.PicShow.Image;
            ((IBinarization)valueImage).NosieKiller(srcImage, FilterLevelType.Level02);
            this.PicShow.Refresh();
        }

        private void BtnShewCorrection_Click(object sender, EventArgs e)
        {
            var srcImage = (Bitmap)this.PicShow.Image;
            this.PicShow.Image = ((IBinarization)valueImage).ShewCorrection(srcImage, ShewDetectionType.Projection);
            this.PicShow.Refresh();
        }

        //private void rotate(Bitmap srcImage, Double angle, out Bitmap result)
        //{
        //    //result = new Bitmap()
        //}
    }
}
