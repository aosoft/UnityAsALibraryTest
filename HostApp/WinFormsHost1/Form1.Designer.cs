using UnityLibrary.Server.Hubs;

namespace WinFormsHost1
{
    partial class Form1
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
            this._splitContainer = new System.Windows.Forms.SplitContainer();
            this._textBox = new System.Windows.Forms.TextBox();
            this._btnAnimation = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this._splitContainer)).BeginInit();
            this._splitContainer.Panel1.SuspendLayout();
            this._splitContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // _splitContainer
            // 
            this._splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this._splitContainer.Location = new System.Drawing.Point(0, 0);
            this._splitContainer.Name = "_splitContainer";
            // 
            // _splitContainer.Panel1
            // 
            this._splitContainer.Panel1.Controls.Add(this._textBox);
            this._splitContainer.Panel1.Controls.Add(this._btnAnimation);
            this._splitContainer.Size = new System.Drawing.Size(778, 544);
            this._splitContainer.SplitterDistance = 320;
            this._splitContainer.TabIndex = 0;
            this._splitContainer.Text = "splitContainer1";
            // 
            // _textBox
            // 
            this._textBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._textBox.Font = new System.Drawing.Font("Yu Gothic UI", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this._textBox.Location = new System.Drawing.Point(12, 12);
            this._textBox.Multiline = true;
            this._textBox.Name = "_textBox";
            this._textBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this._textBox.Size = new System.Drawing.Size(305, 455);
            this._textBox.TabIndex = 1;
            this._textBox.TextChanged += new System.EventHandler(this.TextBox_OnTextChanged);
            // 
            // _btnAnimation
            // 
            this._btnAnimation.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._btnAnimation.Font = new System.Drawing.Font("Yu Gothic UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this._btnAnimation.Location = new System.Drawing.Point(12, 473);
            this._btnAnimation.Name = "_btnAnimation";
            this._btnAnimation.Size = new System.Drawing.Size(305, 59);
            this._btnAnimation.TabIndex = 0;
            this._btnAnimation.Text = "アニメーション切り替え";
            this._btnAnimation.UseVisualStyleBackColor = true;
            this._btnAnimation.Click += new System.EventHandler(this.BtnAnimation_OnClick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(778, 544);
            this.Controls.Add(this._splitContainer);
            this.Name = "Form1";
            this.Text = "Form1";
            this._splitContainer.Panel1.ResumeLayout(false);
            this._splitContainer.Panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._splitContainer)).EndInit();
            this._splitContainer.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer _splitContainer;
        private System.Windows.Forms.Button _btnAnimation;
        private System.Windows.Forms.TextBox _textBox;
    }
}

