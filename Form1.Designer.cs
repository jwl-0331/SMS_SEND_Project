using System;

namespace project_SMS
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
            button_LISTEN = new System.Windows.Forms.Button();
            label1 = new System.Windows.Forms.Label();
            PortNum = new System.Windows.Forms.TextBox();
            listBox1 = new System.Windows.Forms.ListBox();
            SuspendLayout();
            // 
            // button_LISTEN
            // 
            button_LISTEN.Location = new System.Drawing.Point(294, 49);
            button_LISTEN.Name = "button_LISTEN";
            button_LISTEN.Size = new System.Drawing.Size(75, 23);
            button_LISTEN.TabIndex = 0;
            button_LISTEN.Text = "LISTEN";
            button_LISTEN.UseVisualStyleBackColor = true;
            button_LISTEN.Click += button_LISTEN_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(52, 53);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(64, 15);
            label1.TabIndex = 2;
            label1.Text = "PORTNUM";
            // 
            // PortNum
            // 
            PortNum.Location = new System.Drawing.Point(122, 49);
            PortNum.Name = "PortNum";
            PortNum.Size = new System.Drawing.Size(166, 23);
            PortNum.TabIndex = 3;
            // 
            // listBox1
            // 
            listBox1.FormattingEnabled = true;
            listBox1.ItemHeight = 15;
            listBox1.Location = new System.Drawing.Point(52, 93);
            listBox1.Name = "listBox1";
            listBox1.Size = new System.Drawing.Size(317, 259);
            listBox1.TabIndex = 4;
            // 
            // Form1
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(418, 416);
            Controls.Add(listBox1);
            Controls.Add(PortNum);
            Controls.Add(label1);
            Controls.Add(button_LISTEN);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }


        #endregion

        private System.Windows.Forms.Button button_LISTEN;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox PortNum;
        private System.Windows.Forms.ListBox listBox1;
    }
}
