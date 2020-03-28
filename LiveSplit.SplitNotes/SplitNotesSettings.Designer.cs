namespace LiveSplit.SplitNotes
{
    partial class SplitNotesSettings
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.sizeUpDown = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.sizeUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // sizeUpDown
            // 
            this.sizeUpDown.Location = new System.Drawing.Point(4, 4);
            this.sizeUpDown.Maximum = new decimal(new int[] {
            512,
            0,
            0,
            0});
            this.sizeUpDown.Minimum = new decimal(new int[] {
            128,
            0,
            0,
            0});
            this.sizeUpDown.Name = "sizeUpDown";
            this.sizeUpDown.Size = new System.Drawing.Size(120, 20);
            this.sizeUpDown.TabIndex = 0;
            this.sizeUpDown.Value = new decimal(new int[] {
            128,
            0,
            0,
            0});
            // 
            // SplitNotesSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.sizeUpDown);
            this.Name = "SplitNotesSettings";
            this.Size = new System.Drawing.Size(852, 571);
            ((System.ComponentModel.ISupportInitialize)(this.sizeUpDown)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.NumericUpDown sizeUpDown;
    }
}
