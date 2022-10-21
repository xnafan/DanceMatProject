namespace DanceMatVisualizerWindow
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.picBoxDanceMat = new DanceMatVisualizerWindow.CustomRenderPictureBox();
            this.tmrRepaint = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.picBoxDanceMat)).BeginInit();
            this.SuspendLayout();
            // 
            // picBoxDanceMat
            // 
            this.picBoxDanceMat.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picBoxDanceMat.Image = global::DanceMatVisualizerWindow.Properties.Resources.DanceMat;
            this.picBoxDanceMat.InitialImage = null;
            this.picBoxDanceMat.Location = new System.Drawing.Point(0, 0);
            this.picBoxDanceMat.Name = "picBoxDanceMat";
            this.picBoxDanceMat.Size = new System.Drawing.Size(779, 910);
            this.picBoxDanceMat.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picBoxDanceMat.TabIndex = 0;
            this.picBoxDanceMat.TabStop = false;
            // 
            // tmrRepaint
            // 
            this.tmrRepaint.Interval = 50;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(779, 910);
            this.Controls.Add(this.picBoxDanceMat);
            this.Name = "MainForm";
            this.Text = "Dance Mat Visualizer";
            ((System.ComponentModel.ISupportInitialize)(this.picBoxDanceMat)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private CustomRenderPictureBox picBoxDanceMat;
        private System.Windows.Forms.Timer tmrRepaint;
    }
}