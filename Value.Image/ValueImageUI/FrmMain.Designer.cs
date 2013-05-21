namespace ValueImageUI
{
    partial class FrmMain
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.PicShow = new System.Windows.Forms.PictureBox();
            this.GrbDisNoise = new System.Windows.Forms.GroupBox();
            this.BtnMorphologic = new System.Windows.Forms.Button();
            this.BtnGrayClose = new System.Windows.Forms.Button();
            this.BtnGrayOpen = new System.Windows.Forms.Button();
            this.BtnGrayErode = new System.Windows.Forms.Button();
            this.BtnGrayDelation = new System.Windows.Forms.Button();
            this.BtnClose = new System.Windows.Forms.Button();
            this.BtnOpen = new System.Windows.Forms.Button();
            this.BtnDelation = new System.Windows.Forms.Button();
            this.BtnErode = new System.Windows.Forms.Button();
            this.BtnOpenImage = new System.Windows.Forms.Button();
            this.BtnOrgImage = new System.Windows.Forms.Button();
            this.GrbGray = new System.Windows.Forms.GroupBox();
            this.BtnOstuThreshold = new System.Windows.Forms.Button();
            this.BtnOptimalThresholding = new System.Windows.Forms.Button();
            this.BtnGrayscalePower = new System.Windows.Forms.Button();
            this.BtnGrayscale = new System.Windows.Forms.Button();
            this.BtnBinarization = new System.Windows.Forms.Button();
            this.BtnGrayscaleMin = new System.Windows.Forms.Button();
            this.BtnGrayStretch = new System.Windows.Forms.Button();
            this.BtnGrayScaleMax = new System.Windows.Forms.Button();
            this.BtnGrayStretchInner = new System.Windows.Forms.Button();
            this.BtnHilditchThining = new System.Windows.Forms.Button();
            this.BtnMove = new System.Windows.Forms.Button();
            this.BtnHistEqualization = new System.Windows.Forms.Button();
            this.BtnLinearChange = new System.Windows.Forms.Button();
            this.GrdNoise = new System.Windows.Forms.GroupBox();
            this.BtnPepperNoise = new System.Windows.Forms.Button();
            this.BtnIndexNoise = new System.Windows.Forms.Button();
            this.BtnRayleighNoise = new System.Windows.Forms.Button();
            this.BtnGaussNoise = new System.Windows.Forms.Button();
            this.GrbFrequency = new System.Windows.Forms.GroupBox();
            this.BtnProjectionVert = new System.Windows.Forms.Button();
            this.BtnProjectionHori = new System.Windows.Forms.Button();
            this.BtnPhase = new System.Windows.Forms.Button();
            this.BtnAmplitude = new System.Windows.Forms.Button();
            this.BtnFFT = new System.Windows.Forms.Button();
            this.GrbFilter = new System.Windows.Forms.GroupBox();
            this.BtnStatisticFilter = new System.Windows.Forms.Button();
            this.BtnGaussFilter = new System.Windows.Forms.Button();
            this.BtnMedianFitler = new System.Windows.Forms.Button();
            this.BtnMeanFilter = new System.Windows.Forms.Button();
            this.BtnOrientationFilter = new System.Windows.Forms.Button();
            this.BtnBandstopFilter = new System.Windows.Forms.Button();
            this.BtnBandpassFilter = new System.Windows.Forms.Button();
            this.BtnHighpassFilter = new System.Windows.Forms.Button();
            this.BtnLowpassFilter = new System.Windows.Forms.Button();
            this.GrdEdge = new System.Windows.Forms.GroupBox();
            this.BtnGaussEdge = new System.Windows.Forms.Button();
            this.BtnMaskEdge = new System.Windows.Forms.Button();
            this.GrbDivision = new System.Windows.Forms.GroupBox();
            this.BtnZhangThinning = new System.Windows.Forms.Button();
            this.BtnUniformQuantization = new System.Windows.Forms.Button();
            this.BtnCutRectangle = new System.Windows.Forms.Button();
            this.BtnDivisionWord = new System.Windows.Forms.Button();
            this.GrdOther = new System.Windows.Forms.GroupBox();
            this.BtnInvertColor = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.GrdGeometry = new System.Windows.Forms.GroupBox();
            this.BtnAmphilinearity = new System.Windows.Forms.Button();
            this.BtnZoomNN = new System.Windows.Forms.Button();
            this.ZhangExpendThinning = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.PicShow)).BeginInit();
            this.GrbDisNoise.SuspendLayout();
            this.GrbGray.SuspendLayout();
            this.GrdNoise.SuspendLayout();
            this.GrbFrequency.SuspendLayout();
            this.GrbFilter.SuspendLayout();
            this.GrdEdge.SuspendLayout();
            this.GrbDivision.SuspendLayout();
            this.GrdOther.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.GrdGeometry.SuspendLayout();
            this.SuspendLayout();
            // 
            // PicShow
            // 
            this.PicShow.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PicShow.Location = new System.Drawing.Point(12, 12);
            this.PicShow.Name = "PicShow";
            this.PicShow.Size = new System.Drawing.Size(400, 400);
            this.PicShow.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.PicShow.TabIndex = 0;
            this.PicShow.TabStop = false;
            // 
            // GrbDisNoise
            // 
            this.GrbDisNoise.Controls.Add(this.BtnMorphologic);
            this.GrbDisNoise.Controls.Add(this.BtnGrayClose);
            this.GrbDisNoise.Controls.Add(this.BtnGrayOpen);
            this.GrbDisNoise.Controls.Add(this.BtnGrayErode);
            this.GrbDisNoise.Controls.Add(this.BtnGrayDelation);
            this.GrbDisNoise.Controls.Add(this.BtnClose);
            this.GrbDisNoise.Controls.Add(this.BtnOpen);
            this.GrbDisNoise.Controls.Add(this.BtnDelation);
            this.GrbDisNoise.Controls.Add(this.BtnErode);
            this.GrbDisNoise.Location = new System.Drawing.Point(418, 119);
            this.GrbDisNoise.Name = "GrbDisNoise";
            this.GrbDisNoise.Size = new System.Drawing.Size(501, 80);
            this.GrbDisNoise.TabIndex = 1;
            this.GrbDisNoise.TabStop = false;
            this.GrbDisNoise.Text = "去噪";
            // 
            // BtnMorphologic
            // 
            this.BtnMorphologic.Location = new System.Drawing.Point(393, 43);
            this.BtnMorphologic.Name = "BtnMorphologic";
            this.BtnMorphologic.Size = new System.Drawing.Size(96, 23);
            this.BtnMorphologic.TabIndex = 8;
            this.BtnMorphologic.Text = "灰度形态变化";
            this.BtnMorphologic.UseVisualStyleBackColor = true;
            this.BtnMorphologic.Click += new System.EventHandler(this.BtnMorphologic_Click);
            // 
            // BtnGrayClose
            // 
            this.BtnGrayClose.Location = new System.Drawing.Point(297, 43);
            this.BtnGrayClose.Name = "BtnGrayClose";
            this.BtnGrayClose.Size = new System.Drawing.Size(96, 23);
            this.BtnGrayClose.TabIndex = 7;
            this.BtnGrayClose.Text = "灰度闭运算";
            this.BtnGrayClose.UseVisualStyleBackColor = true;
            this.BtnGrayClose.Click += new System.EventHandler(this.BtnGrayClose_Click);
            // 
            // BtnGrayOpen
            // 
            this.BtnGrayOpen.Location = new System.Drawing.Point(201, 43);
            this.BtnGrayOpen.Name = "BtnGrayOpen";
            this.BtnGrayOpen.Size = new System.Drawing.Size(96, 23);
            this.BtnGrayOpen.TabIndex = 6;
            this.BtnGrayOpen.Text = "灰度开运算";
            this.BtnGrayOpen.UseVisualStyleBackColor = true;
            this.BtnGrayOpen.Click += new System.EventHandler(this.BtnGrayOpen_Click);
            // 
            // BtnGrayErode
            // 
            this.BtnGrayErode.Location = new System.Drawing.Point(9, 43);
            this.BtnGrayErode.Name = "BtnGrayErode";
            this.BtnGrayErode.Size = new System.Drawing.Size(96, 23);
            this.BtnGrayErode.TabIndex = 5;
            this.BtnGrayErode.Text = "灰度腐蚀";
            this.BtnGrayErode.UseVisualStyleBackColor = true;
            this.BtnGrayErode.Click += new System.EventHandler(this.BtnGrayErode_Click);
            // 
            // BtnGrayDelation
            // 
            this.BtnGrayDelation.Location = new System.Drawing.Point(105, 43);
            this.BtnGrayDelation.Name = "BtnGrayDelation";
            this.BtnGrayDelation.Size = new System.Drawing.Size(96, 23);
            this.BtnGrayDelation.TabIndex = 4;
            this.BtnGrayDelation.Text = "灰度膨胀";
            this.BtnGrayDelation.UseVisualStyleBackColor = true;
            this.BtnGrayDelation.Click += new System.EventHandler(this.BtnGrayDelation_Click);
            // 
            // BtnClose
            // 
            this.BtnClose.Location = new System.Drawing.Point(297, 20);
            this.BtnClose.Name = "BtnClose";
            this.BtnClose.Size = new System.Drawing.Size(96, 23);
            this.BtnClose.TabIndex = 3;
            this.BtnClose.Text = "二值闭运算";
            this.BtnClose.UseVisualStyleBackColor = true;
            this.BtnClose.Click += new System.EventHandler(this.BtnClose_Click);
            // 
            // BtnOpen
            // 
            this.BtnOpen.Location = new System.Drawing.Point(201, 20);
            this.BtnOpen.Name = "BtnOpen";
            this.BtnOpen.Size = new System.Drawing.Size(96, 23);
            this.BtnOpen.TabIndex = 2;
            this.BtnOpen.Text = "二值开运算";
            this.BtnOpen.UseVisualStyleBackColor = true;
            this.BtnOpen.Click += new System.EventHandler(this.BtnOpen_Click_1);
            // 
            // BtnDelation
            // 
            this.BtnDelation.Location = new System.Drawing.Point(105, 20);
            this.BtnDelation.Name = "BtnDelation";
            this.BtnDelation.Size = new System.Drawing.Size(96, 23);
            this.BtnDelation.TabIndex = 1;
            this.BtnDelation.Text = "二值膨胀";
            this.BtnDelation.UseVisualStyleBackColor = true;
            this.BtnDelation.Click += new System.EventHandler(this.BtnDelation_Click);
            // 
            // BtnErode
            // 
            this.BtnErode.Location = new System.Drawing.Point(9, 20);
            this.BtnErode.Name = "BtnErode";
            this.BtnErode.Size = new System.Drawing.Size(96, 23);
            this.BtnErode.TabIndex = 0;
            this.BtnErode.Text = "二值腐蚀";
            this.BtnErode.UseVisualStyleBackColor = true;
            this.BtnErode.Click += new System.EventHandler(this.BtnErode_Click);
            // 
            // BtnOpenImage
            // 
            this.BtnOpenImage.Location = new System.Drawing.Point(424, 19);
            this.BtnOpenImage.Name = "BtnOpenImage";
            this.BtnOpenImage.Size = new System.Drawing.Size(387, 23);
            this.BtnOpenImage.TabIndex = 2;
            this.BtnOpenImage.Text = "打开图片";
            this.BtnOpenImage.UseVisualStyleBackColor = true;
            this.BtnOpenImage.Click += new System.EventHandler(this.BtnOpen_Click);
            // 
            // BtnOrgImage
            // 
            this.BtnOrgImage.Location = new System.Drawing.Point(811, 19);
            this.BtnOrgImage.Name = "BtnOrgImage";
            this.BtnOrgImage.Size = new System.Drawing.Size(96, 23);
            this.BtnOrgImage.TabIndex = 3;
            this.BtnOrgImage.Text = "原图片";
            this.BtnOrgImage.UseVisualStyleBackColor = true;
            this.BtnOrgImage.Click += new System.EventHandler(this.BtnOrgImage_Click);
            // 
            // GrbGray
            // 
            this.GrbGray.Controls.Add(this.BtnOstuThreshold);
            this.GrbGray.Controls.Add(this.BtnOptimalThresholding);
            this.GrbGray.Controls.Add(this.BtnGrayscalePower);
            this.GrbGray.Controls.Add(this.BtnGrayscale);
            this.GrbGray.Controls.Add(this.BtnBinarization);
            this.GrbGray.Controls.Add(this.BtnGrayscaleMin);
            this.GrbGray.Controls.Add(this.BtnGrayStretch);
            this.GrbGray.Controls.Add(this.BtnGrayScaleMax);
            this.GrbGray.Controls.Add(this.BtnGrayStretchInner);
            this.GrbGray.Location = new System.Drawing.Point(418, 43);
            this.GrbGray.Name = "GrbGray";
            this.GrbGray.Size = new System.Drawing.Size(501, 76);
            this.GrbGray.TabIndex = 4;
            this.GrbGray.TabStop = false;
            this.GrbGray.Text = "灰度化";
            // 
            // BtnOstuThreshold
            // 
            this.BtnOstuThreshold.Location = new System.Drawing.Point(393, 43);
            this.BtnOstuThreshold.Name = "BtnOstuThreshold";
            this.BtnOstuThreshold.Size = new System.Drawing.Size(96, 23);
            this.BtnOstuThreshold.TabIndex = 20;
            this.BtnOstuThreshold.Text = "Ostu阈值化";
            this.BtnOstuThreshold.UseVisualStyleBackColor = true;
            this.BtnOstuThreshold.Click += new System.EventHandler(this.BtnOstuThreshold_Click);
            // 
            // BtnOptimalThresholding
            // 
            this.BtnOptimalThresholding.Location = new System.Drawing.Point(297, 43);
            this.BtnOptimalThresholding.Name = "BtnOptimalThresholding";
            this.BtnOptimalThresholding.Size = new System.Drawing.Size(96, 23);
            this.BtnOptimalThresholding.TabIndex = 19;
            this.BtnOptimalThresholding.Text = "最优阈值化";
            this.BtnOptimalThresholding.UseVisualStyleBackColor = true;
            this.BtnOptimalThresholding.Click += new System.EventHandler(this.BtnOptimalThresholding_Click);
            // 
            // BtnGrayscalePower
            // 
            this.BtnGrayscalePower.Location = new System.Drawing.Point(297, 20);
            this.BtnGrayscalePower.Name = "BtnGrayscalePower";
            this.BtnGrayscalePower.Size = new System.Drawing.Size(96, 23);
            this.BtnGrayscalePower.TabIndex = 12;
            this.BtnGrayscalePower.Text = "权重灰度化";
            this.BtnGrayscalePower.UseVisualStyleBackColor = true;
            this.BtnGrayscalePower.Click += new System.EventHandler(this.BtnGrayscalePower_Click);
            // 
            // BtnGrayscale
            // 
            this.BtnGrayscale.Location = new System.Drawing.Point(201, 20);
            this.BtnGrayscale.Name = "BtnGrayscale";
            this.BtnGrayscale.Size = new System.Drawing.Size(96, 23);
            this.BtnGrayscale.TabIndex = 11;
            this.BtnGrayscale.Text = "均值灰度化";
            this.BtnGrayscale.UseVisualStyleBackColor = true;
            this.BtnGrayscale.Click += new System.EventHandler(this.BtnGrayscale_Click);
            // 
            // BtnBinarization
            // 
            this.BtnBinarization.Location = new System.Drawing.Point(201, 43);
            this.BtnBinarization.Name = "BtnBinarization";
            this.BtnBinarization.Size = new System.Drawing.Size(96, 23);
            this.BtnBinarization.TabIndex = 18;
            this.BtnBinarization.Text = "二值化";
            this.BtnBinarization.UseVisualStyleBackColor = true;
            this.BtnBinarization.Click += new System.EventHandler(this.BtnBinarization_Click);
            // 
            // BtnGrayscaleMin
            // 
            this.BtnGrayscaleMin.Location = new System.Drawing.Point(105, 20);
            this.BtnGrayscaleMin.Name = "BtnGrayscaleMin";
            this.BtnGrayscaleMin.Size = new System.Drawing.Size(96, 23);
            this.BtnGrayscaleMin.TabIndex = 10;
            this.BtnGrayscaleMin.Text = "最小灰度化";
            this.BtnGrayscaleMin.UseVisualStyleBackColor = true;
            this.BtnGrayscaleMin.Click += new System.EventHandler(this.BtnGrayscaleMin_Click);
            // 
            // BtnGrayStretch
            // 
            this.BtnGrayStretch.Location = new System.Drawing.Point(9, 43);
            this.BtnGrayStretch.Name = "BtnGrayStretch";
            this.BtnGrayStretch.Size = new System.Drawing.Size(96, 23);
            this.BtnGrayStretch.TabIndex = 14;
            this.BtnGrayStretch.Text = "灰度拉伸";
            this.BtnGrayStretch.UseVisualStyleBackColor = true;
            this.BtnGrayStretch.Click += new System.EventHandler(this.BtnGrayStrech_Click);
            // 
            // BtnGrayScaleMax
            // 
            this.BtnGrayScaleMax.Location = new System.Drawing.Point(9, 20);
            this.BtnGrayScaleMax.Name = "BtnGrayScaleMax";
            this.BtnGrayScaleMax.Size = new System.Drawing.Size(96, 23);
            this.BtnGrayScaleMax.TabIndex = 9;
            this.BtnGrayScaleMax.Text = "最大灰度化";
            this.BtnGrayScaleMax.UseVisualStyleBackColor = true;
            this.BtnGrayScaleMax.Click += new System.EventHandler(this.BtnGrayScaleMax_Click);
            // 
            // BtnGrayStretchInner
            // 
            this.BtnGrayStretchInner.Location = new System.Drawing.Point(105, 43);
            this.BtnGrayStretchInner.Name = "BtnGrayStretchInner";
            this.BtnGrayStretchInner.Size = new System.Drawing.Size(96, 23);
            this.BtnGrayStretchInner.TabIndex = 13;
            this.BtnGrayStretchInner.Text = "灰度拉伸内置";
            this.BtnGrayStretchInner.UseVisualStyleBackColor = true;
            this.BtnGrayStretchInner.Click += new System.EventHandler(this.BtnGrayStrechInner_Click);
            // 
            // BtnHilditchThining
            // 
            this.BtnHilditchThining.Location = new System.Drawing.Point(297, 20);
            this.BtnHilditchThining.Name = "BtnHilditchThining";
            this.BtnHilditchThining.Size = new System.Drawing.Size(96, 23);
            this.BtnHilditchThining.TabIndex = 21;
            this.BtnHilditchThining.Text = "Hilditch细化";
            this.BtnHilditchThining.UseVisualStyleBackColor = true;
            this.BtnHilditchThining.Click += new System.EventHandler(this.BtnThining_Click);
            // 
            // BtnMove
            // 
            this.BtnMove.Location = new System.Drawing.Point(105, 20);
            this.BtnMove.Name = "BtnMove";
            this.BtnMove.Size = new System.Drawing.Size(96, 23);
            this.BtnMove.TabIndex = 17;
            this.BtnMove.Text = "平移";
            this.BtnMove.UseVisualStyleBackColor = true;
            this.BtnMove.Click += new System.EventHandler(this.BtnMove_Click);
            // 
            // BtnHistEqualization
            // 
            this.BtnHistEqualization.Location = new System.Drawing.Point(6, 20);
            this.BtnHistEqualization.Name = "BtnHistEqualization";
            this.BtnHistEqualization.Size = new System.Drawing.Size(96, 23);
            this.BtnHistEqualization.TabIndex = 16;
            this.BtnHistEqualization.Text = "直方图均衡化";
            this.BtnHistEqualization.UseVisualStyleBackColor = true;
            this.BtnHistEqualization.Click += new System.EventHandler(this.BtnHistEqualization_Click);
            // 
            // BtnLinearChange
            // 
            this.BtnLinearChange.Location = new System.Drawing.Point(9, 20);
            this.BtnLinearChange.Name = "BtnLinearChange";
            this.BtnLinearChange.Size = new System.Drawing.Size(96, 23);
            this.BtnLinearChange.TabIndex = 15;
            this.BtnLinearChange.Text = "线性变化";
            this.BtnLinearChange.UseVisualStyleBackColor = true;
            this.BtnLinearChange.Click += new System.EventHandler(this.BtnLinearChange_Click);
            // 
            // GrdNoise
            // 
            this.GrdNoise.Controls.Add(this.BtnPepperNoise);
            this.GrdNoise.Controls.Add(this.BtnIndexNoise);
            this.GrdNoise.Controls.Add(this.BtnRayleighNoise);
            this.GrdNoise.Controls.Add(this.BtnGaussNoise);
            this.GrdNoise.Location = new System.Drawing.Point(418, 199);
            this.GrdNoise.Name = "GrdNoise";
            this.GrdNoise.Size = new System.Drawing.Size(501, 55);
            this.GrdNoise.TabIndex = 5;
            this.GrdNoise.TabStop = false;
            this.GrdNoise.Text = "噪声";
            // 
            // BtnPepperNoise
            // 
            this.BtnPepperNoise.Location = new System.Drawing.Point(297, 20);
            this.BtnPepperNoise.Name = "BtnPepperNoise";
            this.BtnPepperNoise.Size = new System.Drawing.Size(96, 23);
            this.BtnPepperNoise.TabIndex = 4;
            this.BtnPepperNoise.Text = "椒盐噪声";
            this.BtnPepperNoise.UseVisualStyleBackColor = true;
            this.BtnPepperNoise.Click += new System.EventHandler(this.BtnPepperNoise_Click);
            // 
            // BtnIndexNoise
            // 
            this.BtnIndexNoise.Location = new System.Drawing.Point(201, 20);
            this.BtnIndexNoise.Name = "BtnIndexNoise";
            this.BtnIndexNoise.Size = new System.Drawing.Size(96, 23);
            this.BtnIndexNoise.TabIndex = 3;
            this.BtnIndexNoise.Text = "指数噪声";
            this.BtnIndexNoise.UseVisualStyleBackColor = true;
            this.BtnIndexNoise.Click += new System.EventHandler(this.BtnIndexNoise_Click);
            // 
            // BtnRayleighNoise
            // 
            this.BtnRayleighNoise.Location = new System.Drawing.Point(105, 20);
            this.BtnRayleighNoise.Name = "BtnRayleighNoise";
            this.BtnRayleighNoise.Size = new System.Drawing.Size(96, 23);
            this.BtnRayleighNoise.TabIndex = 2;
            this.BtnRayleighNoise.Text = "瑞利噪声";
            this.BtnRayleighNoise.UseVisualStyleBackColor = true;
            this.BtnRayleighNoise.Click += new System.EventHandler(this.BtnRayleighNoise_Click);
            // 
            // BtnGaussNoise
            // 
            this.BtnGaussNoise.Location = new System.Drawing.Point(9, 20);
            this.BtnGaussNoise.Name = "BtnGaussNoise";
            this.BtnGaussNoise.Size = new System.Drawing.Size(96, 23);
            this.BtnGaussNoise.TabIndex = 1;
            this.BtnGaussNoise.Text = "高斯噪声";
            this.BtnGaussNoise.UseVisualStyleBackColor = true;
            this.BtnGaussNoise.Click += new System.EventHandler(this.BtnGaussNoise_Click);
            // 
            // GrbFrequency
            // 
            this.GrbFrequency.Controls.Add(this.BtnProjectionVert);
            this.GrbFrequency.Controls.Add(this.BtnProjectionHori);
            this.GrbFrequency.Controls.Add(this.BtnPhase);
            this.GrbFrequency.Controls.Add(this.BtnAmplitude);
            this.GrbFrequency.Controls.Add(this.BtnFFT);
            this.GrbFrequency.Location = new System.Drawing.Point(418, 255);
            this.GrbFrequency.Name = "GrbFrequency";
            this.GrbFrequency.Size = new System.Drawing.Size(501, 55);
            this.GrbFrequency.TabIndex = 6;
            this.GrbFrequency.TabStop = false;
            this.GrbFrequency.Text = "频率";
            // 
            // BtnProjectionVert
            // 
            this.BtnProjectionVert.Location = new System.Drawing.Point(393, 20);
            this.BtnProjectionVert.Name = "BtnProjectionVert";
            this.BtnProjectionVert.Size = new System.Drawing.Size(96, 23);
            this.BtnProjectionVert.TabIndex = 7;
            this.BtnProjectionVert.Text = "纵向投影";
            this.BtnProjectionVert.UseVisualStyleBackColor = true;
            this.BtnProjectionVert.Click += new System.EventHandler(this.BtnProjectionVert_Click);
            // 
            // BtnProjectionHori
            // 
            this.BtnProjectionHori.Location = new System.Drawing.Point(297, 20);
            this.BtnProjectionHori.Name = "BtnProjectionHori";
            this.BtnProjectionHori.Size = new System.Drawing.Size(96, 23);
            this.BtnProjectionHori.TabIndex = 6;
            this.BtnProjectionHori.Text = "横向投影";
            this.BtnProjectionHori.UseVisualStyleBackColor = true;
            this.BtnProjectionHori.Click += new System.EventHandler(this.BtnProjectionHori_Click);
            // 
            // BtnPhase
            // 
            this.BtnPhase.Location = new System.Drawing.Point(201, 20);
            this.BtnPhase.Name = "BtnPhase";
            this.BtnPhase.Size = new System.Drawing.Size(96, 23);
            this.BtnPhase.TabIndex = 5;
            this.BtnPhase.Text = "相位变化";
            this.BtnPhase.UseVisualStyleBackColor = true;
            this.BtnPhase.Click += new System.EventHandler(this.BtnPharse_Click);
            // 
            // BtnAmplitude
            // 
            this.BtnAmplitude.Location = new System.Drawing.Point(105, 20);
            this.BtnAmplitude.Name = "BtnAmplitude";
            this.BtnAmplitude.Size = new System.Drawing.Size(96, 23);
            this.BtnAmplitude.TabIndex = 4;
            this.BtnAmplitude.Text = "幅度变换";
            this.BtnAmplitude.UseVisualStyleBackColor = true;
            this.BtnAmplitude.Click += new System.EventHandler(this.BtnAmplitude_Click);
            // 
            // BtnFFT
            // 
            this.BtnFFT.Location = new System.Drawing.Point(9, 20);
            this.BtnFFT.Name = "BtnFFT";
            this.BtnFFT.Size = new System.Drawing.Size(96, 23);
            this.BtnFFT.TabIndex = 2;
            this.BtnFFT.Text = "傅里叶变化";
            this.BtnFFT.UseVisualStyleBackColor = true;
            this.BtnFFT.Click += new System.EventHandler(this.BtnFFT_Click);
            // 
            // GrbFilter
            // 
            this.GrbFilter.Controls.Add(this.BtnStatisticFilter);
            this.GrbFilter.Controls.Add(this.BtnGaussFilter);
            this.GrbFilter.Controls.Add(this.BtnMedianFitler);
            this.GrbFilter.Controls.Add(this.BtnMeanFilter);
            this.GrbFilter.Controls.Add(this.BtnOrientationFilter);
            this.GrbFilter.Controls.Add(this.BtnBandstopFilter);
            this.GrbFilter.Controls.Add(this.BtnBandpassFilter);
            this.GrbFilter.Controls.Add(this.BtnHighpassFilter);
            this.GrbFilter.Controls.Add(this.BtnLowpassFilter);
            this.GrbFilter.Location = new System.Drawing.Point(418, 310);
            this.GrbFilter.Name = "GrbFilter";
            this.GrbFilter.Size = new System.Drawing.Size(501, 79);
            this.GrbFilter.TabIndex = 7;
            this.GrbFilter.TabStop = false;
            this.GrbFilter.Text = "滤波";
            // 
            // BtnStatisticFilter
            // 
            this.BtnStatisticFilter.Location = new System.Drawing.Point(393, 43);
            this.BtnStatisticFilter.Name = "BtnStatisticFilter";
            this.BtnStatisticFilter.Size = new System.Drawing.Size(96, 23);
            this.BtnStatisticFilter.TabIndex = 14;
            this.BtnStatisticFilter.Text = "统计滤波";
            this.BtnStatisticFilter.UseVisualStyleBackColor = true;
            this.BtnStatisticFilter.Click += new System.EventHandler(this.BtnStatisticFilter_Click);
            // 
            // BtnGaussFilter
            // 
            this.BtnGaussFilter.Location = new System.Drawing.Point(297, 43);
            this.BtnGaussFilter.Name = "BtnGaussFilter";
            this.BtnGaussFilter.Size = new System.Drawing.Size(96, 23);
            this.BtnGaussFilter.TabIndex = 13;
            this.BtnGaussFilter.Text = "高斯滤波";
            this.BtnGaussFilter.UseVisualStyleBackColor = true;
            this.BtnGaussFilter.Click += new System.EventHandler(this.BtnGaussFilter_Click);
            // 
            // BtnMedianFitler
            // 
            this.BtnMedianFitler.Location = new System.Drawing.Point(201, 43);
            this.BtnMedianFitler.Name = "BtnMedianFitler";
            this.BtnMedianFitler.Size = new System.Drawing.Size(96, 23);
            this.BtnMedianFitler.TabIndex = 12;
            this.BtnMedianFitler.Text = "中值滤波";
            this.BtnMedianFitler.UseVisualStyleBackColor = true;
            this.BtnMedianFitler.Click += new System.EventHandler(this.BtnMedianFitler_Click);
            // 
            // BtnMeanFilter
            // 
            this.BtnMeanFilter.Location = new System.Drawing.Point(105, 43);
            this.BtnMeanFilter.Name = "BtnMeanFilter";
            this.BtnMeanFilter.Size = new System.Drawing.Size(96, 23);
            this.BtnMeanFilter.TabIndex = 11;
            this.BtnMeanFilter.Text = "均值滤波";
            this.BtnMeanFilter.UseVisualStyleBackColor = true;
            this.BtnMeanFilter.Click += new System.EventHandler(this.BtnMeanFilter_Click);
            // 
            // BtnOrientationFilter
            // 
            this.BtnOrientationFilter.Location = new System.Drawing.Point(9, 43);
            this.BtnOrientationFilter.Name = "BtnOrientationFilter";
            this.BtnOrientationFilter.Size = new System.Drawing.Size(96, 23);
            this.BtnOrientationFilter.TabIndex = 10;
            this.BtnOrientationFilter.Text = "方位滤波";
            this.BtnOrientationFilter.UseVisualStyleBackColor = true;
            this.BtnOrientationFilter.Click += new System.EventHandler(this.BtnOrientationFilter_Click);
            // 
            // BtnBandstopFilter
            // 
            this.BtnBandstopFilter.Location = new System.Drawing.Point(297, 20);
            this.BtnBandstopFilter.Name = "BtnBandstopFilter";
            this.BtnBandstopFilter.Size = new System.Drawing.Size(96, 23);
            this.BtnBandstopFilter.TabIndex = 9;
            this.BtnBandstopFilter.Text = "带阻滤波";
            this.BtnBandstopFilter.UseVisualStyleBackColor = true;
            this.BtnBandstopFilter.Click += new System.EventHandler(this.BtnBandstop_Click);
            // 
            // BtnBandpassFilter
            // 
            this.BtnBandpassFilter.Location = new System.Drawing.Point(201, 20);
            this.BtnBandpassFilter.Name = "BtnBandpassFilter";
            this.BtnBandpassFilter.Size = new System.Drawing.Size(96, 23);
            this.BtnBandpassFilter.TabIndex = 8;
            this.BtnBandpassFilter.Text = "带通滤波";
            this.BtnBandpassFilter.UseVisualStyleBackColor = true;
            this.BtnBandpassFilter.Click += new System.EventHandler(this.BtnBandpass_Click);
            // 
            // BtnHighpassFilter
            // 
            this.BtnHighpassFilter.Location = new System.Drawing.Point(105, 20);
            this.BtnHighpassFilter.Name = "BtnHighpassFilter";
            this.BtnHighpassFilter.Size = new System.Drawing.Size(96, 23);
            this.BtnHighpassFilter.TabIndex = 7;
            this.BtnHighpassFilter.Text = "高通滤波";
            this.BtnHighpassFilter.UseVisualStyleBackColor = true;
            this.BtnHighpassFilter.Click += new System.EventHandler(this.BtnHighpass_Click);
            // 
            // BtnLowpassFilter
            // 
            this.BtnLowpassFilter.Location = new System.Drawing.Point(9, 20);
            this.BtnLowpassFilter.Name = "BtnLowpassFilter";
            this.BtnLowpassFilter.Size = new System.Drawing.Size(96, 23);
            this.BtnLowpassFilter.TabIndex = 6;
            this.BtnLowpassFilter.Text = "低通滤波";
            this.BtnLowpassFilter.UseVisualStyleBackColor = true;
            this.BtnLowpassFilter.Click += new System.EventHandler(this.BtnLowpass_Click);
            // 
            // GrdEdge
            // 
            this.GrdEdge.Controls.Add(this.BtnGaussEdge);
            this.GrdEdge.Controls.Add(this.BtnMaskEdge);
            this.GrdEdge.Location = new System.Drawing.Point(418, 389);
            this.GrdEdge.Name = "GrdEdge";
            this.GrdEdge.Size = new System.Drawing.Size(501, 53);
            this.GrdEdge.TabIndex = 8;
            this.GrdEdge.TabStop = false;
            this.GrdEdge.Text = "边缘锐化";
            // 
            // BtnGaussEdge
            // 
            this.BtnGaussEdge.Location = new System.Drawing.Point(105, 20);
            this.BtnGaussEdge.Name = "BtnGaussEdge";
            this.BtnGaussEdge.Size = new System.Drawing.Size(96, 23);
            this.BtnGaussEdge.TabIndex = 16;
            this.BtnGaussEdge.Text = "高斯算子";
            this.BtnGaussEdge.UseVisualStyleBackColor = true;
            this.BtnGaussEdge.Click += new System.EventHandler(this.BtnGaussEdge_Click);
            // 
            // BtnMaskEdge
            // 
            this.BtnMaskEdge.Location = new System.Drawing.Point(9, 20);
            this.BtnMaskEdge.Name = "BtnMaskEdge";
            this.BtnMaskEdge.Size = new System.Drawing.Size(96, 23);
            this.BtnMaskEdge.TabIndex = 15;
            this.BtnMaskEdge.Text = "模板算子";
            this.BtnMaskEdge.UseVisualStyleBackColor = true;
            this.BtnMaskEdge.Click += new System.EventHandler(this.BtnMaskEdge_Click);
            // 
            // GrbDivision
            // 
            this.GrbDivision.Controls.Add(this.ZhangExpendThinning);
            this.GrbDivision.Controls.Add(this.BtnZhangThinning);
            this.GrbDivision.Controls.Add(this.BtnHilditchThining);
            this.GrbDivision.Controls.Add(this.BtnUniformQuantization);
            this.GrbDivision.Controls.Add(this.BtnCutRectangle);
            this.GrbDivision.Controls.Add(this.BtnDivisionWord);
            this.GrbDivision.Location = new System.Drawing.Point(418, 442);
            this.GrbDivision.Name = "GrbDivision";
            this.GrbDivision.Size = new System.Drawing.Size(501, 100);
            this.GrbDivision.TabIndex = 9;
            this.GrbDivision.TabStop = false;
            this.GrbDivision.Text = "分割";
            // 
            // BtnZhangThinning
            // 
            this.BtnZhangThinning.Location = new System.Drawing.Point(393, 20);
            this.BtnZhangThinning.Name = "BtnZhangThinning";
            this.BtnZhangThinning.Size = new System.Drawing.Size(96, 23);
            this.BtnZhangThinning.TabIndex = 22;
            this.BtnZhangThinning.Text = "Zhang细化";
            this.BtnZhangThinning.UseVisualStyleBackColor = true;
            this.BtnZhangThinning.Click += new System.EventHandler(this.BtnZhangThinning_Click);
            // 
            // BtnUniformQuantization
            // 
            this.BtnUniformQuantization.Location = new System.Drawing.Point(201, 20);
            this.BtnUniformQuantization.Name = "BtnUniformQuantization";
            this.BtnUniformQuantization.Size = new System.Drawing.Size(96, 23);
            this.BtnUniformQuantization.TabIndex = 19;
            this.BtnUniformQuantization.Text = "均匀量化";
            this.BtnUniformQuantization.UseVisualStyleBackColor = true;
            this.BtnUniformQuantization.Click += new System.EventHandler(this.BtnUniformQuantization_Click);
            // 
            // BtnCutRectangle
            // 
            this.BtnCutRectangle.Location = new System.Drawing.Point(105, 20);
            this.BtnCutRectangle.Name = "BtnCutRectangle";
            this.BtnCutRectangle.Size = new System.Drawing.Size(96, 23);
            this.BtnCutRectangle.TabIndex = 18;
            this.BtnCutRectangle.Text = "切割矩形";
            this.BtnCutRectangle.UseVisualStyleBackColor = true;
            this.BtnCutRectangle.Click += new System.EventHandler(this.BtnCutRectangle_Click);
            // 
            // BtnDivisionWord
            // 
            this.BtnDivisionWord.Location = new System.Drawing.Point(9, 20);
            this.BtnDivisionWord.Name = "BtnDivisionWord";
            this.BtnDivisionWord.Size = new System.Drawing.Size(96, 23);
            this.BtnDivisionWord.TabIndex = 17;
            this.BtnDivisionWord.Text = "字符切割";
            this.BtnDivisionWord.UseVisualStyleBackColor = true;
            this.BtnDivisionWord.Click += new System.EventHandler(this.BtnDivisionWord_Click);
            // 
            // GrdOther
            // 
            this.GrdOther.Controls.Add(this.BtnInvertColor);
            this.GrdOther.Controls.Add(this.BtnHistEqualization);
            this.GrdOther.Location = new System.Drawing.Point(12, 497);
            this.GrdOther.Name = "GrdOther";
            this.GrdOther.Size = new System.Drawing.Size(400, 112);
            this.GrdOther.TabIndex = 10;
            this.GrdOther.TabStop = false;
            this.GrdOther.Text = "其他";
            // 
            // BtnInvertColor
            // 
            this.BtnInvertColor.Location = new System.Drawing.Point(102, 20);
            this.BtnInvertColor.Name = "BtnInvertColor";
            this.BtnInvertColor.Size = new System.Drawing.Size(96, 23);
            this.BtnInvertColor.TabIndex = 18;
            this.BtnInvertColor.Text = "反色";
            this.BtnInvertColor.UseVisualStyleBackColor = true;
            this.BtnInvertColor.Click += new System.EventHandler(this.BtnInvertColor_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(316, 419);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(96, 23);
            this.button1.TabIndex = 11;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(183, 418);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(50, 50);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 12;
            this.pictureBox1.TabStop = false;
            // 
            // GrdGeometry
            // 
            this.GrdGeometry.Controls.Add(this.BtnAmphilinearity);
            this.GrdGeometry.Controls.Add(this.BtnZoomNN);
            this.GrdGeometry.Controls.Add(this.BtnMove);
            this.GrdGeometry.Controls.Add(this.BtnLinearChange);
            this.GrdGeometry.Location = new System.Drawing.Point(418, 543);
            this.GrdGeometry.Name = "GrdGeometry";
            this.GrdGeometry.Size = new System.Drawing.Size(501, 55);
            this.GrdGeometry.TabIndex = 13;
            this.GrdGeometry.TabStop = false;
            this.GrdGeometry.Text = "几何";
            // 
            // BtnAmphilinearity
            // 
            this.BtnAmphilinearity.Location = new System.Drawing.Point(297, 20);
            this.BtnAmphilinearity.Name = "BtnAmphilinearity";
            this.BtnAmphilinearity.Size = new System.Drawing.Size(96, 23);
            this.BtnAmphilinearity.TabIndex = 19;
            this.BtnAmphilinearity.Text = "缩放(双线新)";
            this.BtnAmphilinearity.UseVisualStyleBackColor = true;
            this.BtnAmphilinearity.Click += new System.EventHandler(this.BtnAmphilinearity_Click);
            // 
            // BtnZoomNN
            // 
            this.BtnZoomNN.Location = new System.Drawing.Point(201, 20);
            this.BtnZoomNN.Name = "BtnZoomNN";
            this.BtnZoomNN.Size = new System.Drawing.Size(96, 23);
            this.BtnZoomNN.TabIndex = 18;
            this.BtnZoomNN.Text = "缩放(最近邻)";
            this.BtnZoomNN.UseVisualStyleBackColor = true;
            this.BtnZoomNN.Click += new System.EventHandler(this.BtnZoomNN_Click);
            // 
            // ZhangExpendThinning
            // 
            this.ZhangExpendThinning.Location = new System.Drawing.Point(9, 43);
            this.ZhangExpendThinning.Name = "ZhangExpendThinning";
            this.ZhangExpendThinning.Size = new System.Drawing.Size(96, 23);
            this.ZhangExpendThinning.TabIndex = 23;
            this.ZhangExpendThinning.Text = "Zhang扩展细化";
            this.ZhangExpendThinning.UseVisualStyleBackColor = true;
            this.ZhangExpendThinning.Click += new System.EventHandler(this.ZhangExpendThinning_Click);
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(927, 638);
            this.Controls.Add(this.GrdGeometry);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.GrdOther);
            this.Controls.Add(this.GrbDivision);
            this.Controls.Add(this.GrdEdge);
            this.Controls.Add(this.GrbFilter);
            this.Controls.Add(this.GrbFrequency);
            this.Controls.Add(this.GrdNoise);
            this.Controls.Add(this.GrbGray);
            this.Controls.Add(this.BtnOrgImage);
            this.Controls.Add(this.BtnOpenImage);
            this.Controls.Add(this.GrbDisNoise);
            this.Controls.Add(this.PicShow);
            this.Name = "FrmMain";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.PicShow)).EndInit();
            this.GrbDisNoise.ResumeLayout(false);
            this.GrbGray.ResumeLayout(false);
            this.GrdNoise.ResumeLayout(false);
            this.GrbFrequency.ResumeLayout(false);
            this.GrbFilter.ResumeLayout(false);
            this.GrdEdge.ResumeLayout(false);
            this.GrbDivision.ResumeLayout(false);
            this.GrdOther.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.GrdGeometry.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox PicShow;
        private System.Windows.Forms.GroupBox GrbDisNoise;
        private System.Windows.Forms.Button BtnErode;
        private System.Windows.Forms.Button BtnOpenImage;
        private System.Windows.Forms.Button BtnDelation;
        private System.Windows.Forms.Button BtnOpen;
        private System.Windows.Forms.Button BtnOrgImage;
        private System.Windows.Forms.Button BtnGrayErode;
        private System.Windows.Forms.Button BtnGrayDelation;
        private System.Windows.Forms.Button BtnClose;
        private System.Windows.Forms.Button BtnGrayClose;
        private System.Windows.Forms.Button BtnGrayOpen;
        private System.Windows.Forms.Button BtnMorphologic;
        private System.Windows.Forms.GroupBox GrbGray;
        private System.Windows.Forms.Button BtnGrayscale;
        private System.Windows.Forms.Button BtnGrayscaleMin;
        private System.Windows.Forms.Button BtnGrayScaleMax;
        private System.Windows.Forms.Button BtnGrayscalePower;
        private System.Windows.Forms.Button BtnGrayStretch;
        private System.Windows.Forms.Button BtnGrayStretchInner;
        private System.Windows.Forms.Button BtnLinearChange;
        private System.Windows.Forms.Button BtnHistEqualization;
        private System.Windows.Forms.Button BtnMove;
        private System.Windows.Forms.GroupBox GrdNoise;
        private System.Windows.Forms.Button BtnGaussNoise;
        private System.Windows.Forms.Button BtnIndexNoise;
        private System.Windows.Forms.Button BtnRayleighNoise;
        private System.Windows.Forms.Button BtnPepperNoise;
        private System.Windows.Forms.GroupBox GrbFrequency;
        private System.Windows.Forms.Button BtnFFT;
        private System.Windows.Forms.Button BtnAmplitude;
        private System.Windows.Forms.Button BtnPhase;
        private System.Windows.Forms.GroupBox GrbFilter;
        private System.Windows.Forms.Button BtnLowpassFilter;
        private System.Windows.Forms.Button BtnHighpassFilter;
        private System.Windows.Forms.Button BtnBandpassFilter;
        private System.Windows.Forms.Button BtnBandstopFilter;
        private System.Windows.Forms.Button BtnOrientationFilter;
        private System.Windows.Forms.Button BtnMeanFilter;
        private System.Windows.Forms.Button BtnMedianFitler;
        private System.Windows.Forms.Button BtnGaussFilter;
        private System.Windows.Forms.Button BtnStatisticFilter;
        private System.Windows.Forms.GroupBox GrdEdge;
        private System.Windows.Forms.Button BtnMaskEdge;
        private System.Windows.Forms.Button BtnGaussEdge;
        private System.Windows.Forms.GroupBox GrbDivision;
        private System.Windows.Forms.Button BtnDivisionWord;
        private System.Windows.Forms.Button BtnProjectionVert;
        private System.Windows.Forms.Button BtnProjectionHori;
        private System.Windows.Forms.Button BtnCutRectangle;
        private System.Windows.Forms.Button BtnBinarization;
        private System.Windows.Forms.GroupBox GrdOther;
        private System.Windows.Forms.Button BtnOptimalThresholding;
        private System.Windows.Forms.Button BtnInvertColor;
        private System.Windows.Forms.Button BtnOstuThreshold;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button BtnUniformQuantization;
        private System.Windows.Forms.Button BtnHilditchThining;
        private System.Windows.Forms.GroupBox GrdGeometry;
        private System.Windows.Forms.Button BtnZoomNN;
        private System.Windows.Forms.Button BtnAmphilinearity;
        private System.Windows.Forms.Button BtnZhangThinning;
        private System.Windows.Forms.Button ZhangExpendThinning;
    }
}

