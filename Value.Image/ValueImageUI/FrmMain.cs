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
            test.Save(@"D:\test.jpg");
        }

        private void BtnBinarization_Click(object sender, EventArgs e)
        {
            var srcImage = (Bitmap)this.PicShow.Image;
            valueImage.Binarization(srcImage, 100);
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

        private void button1_Click(object sender, EventArgs e)
        {
            //var srcImage = (Bitmap)this.PicShow.Image;
            //valueImage.FillRectangle(srcImage, 0, 0, srcImage.Height / 2, srcImage.Width / 2, Color.White);
            //this.PicShow.Refresh();

            var srcImage = (Bitmap)this.PicShow.Image;
            srcImage.Save("D:\\testets.jpg");


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


    }
}
