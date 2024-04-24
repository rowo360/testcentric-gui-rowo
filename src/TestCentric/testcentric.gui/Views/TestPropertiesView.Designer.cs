namespace TestCentric.Gui.Views
{
    partial class TestPropertiesView
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
            this.header = new System.Windows.Forms.Label();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.testPackageSubView = new TestCentric.Gui.Views.TestPackageSubView();
            this.testPropertiesSubView = new TestCentric.Gui.Views.TestPropertiesSubView();
            this.testResultSubView = new TestCentric.Gui.Views.TestResultSubView();
            this.testOutputSubView = new TestCentric.Gui.Views.TestOutputSubView();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // header
            // 
            this.header.BackColor = System.Drawing.SystemColors.Window;
            this.header.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.4F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.header.Location = new System.Drawing.Point(0, 0);
            this.header.Margin = new System.Windows.Forms.Padding(0);
            this.header.Name = "header";
            this.header.Padding = new System.Windows.Forms.Padding(2);
            this.header.Size = new System.Drawing.Size(525, 18);
            this.header.TabIndex = 0;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel1.BackColor = System.Drawing.SystemColors.Control;
            this.flowLayoutPanel1.Controls.Add(this.header);
            this.flowLayoutPanel1.Controls.Add(this.testPackageSubView);
            this.flowLayoutPanel1.Controls.Add(this.testPropertiesSubView);
            this.flowLayoutPanel1.Controls.Add(this.testResultSubView);
            this.flowLayoutPanel1.Controls.Add(this.testOutputSubView);
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(522, 506);
            this.flowLayoutPanel1.TabIndex = 32;
            this.flowLayoutPanel1.WrapContents = false;
            // 
            // testPackageSubView
            // 
            this.testPackageSubView.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.testPackageSubView.Location = new System.Drawing.Point(3, 21);
            this.testPackageSubView.Name = "testPackageSubView";
            this.testPackageSubView.PackageSettings = "";
            this.testPackageSubView.Size = new System.Drawing.Size(516, 71);
            this.testPackageSubView.TabIndex = 30;
            // 
            // testPropertiesSubView
            // 
            this.testPropertiesSubView.BackColor = System.Drawing.SystemColors.Control;
            this.testPropertiesSubView.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.testPropertiesSubView.Categories = "";
            this.testPropertiesSubView.Description = "";
            this.testPropertiesSubView.FullName = "";
            this.testPropertiesSubView.Location = new System.Drawing.Point(3, 98);
            this.testPropertiesSubView.Name = "testPropertiesSubView";
            this.testPropertiesSubView.Properties = "";
            this.testPropertiesSubView.RunState = "";
            this.testPropertiesSubView.Size = new System.Drawing.Size(516, 209);
            this.testPropertiesSubView.SkipReason = "";
            this.testPropertiesSubView.TabIndex = 30;
            this.testPropertiesSubView.TestCount = "";
            this.testPropertiesSubView.TestType = "";
            // 
            // testResultSubView
            // 
            this.testResultSubView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.testResultSubView.AssertCount = "";
            this.testResultSubView.Assertions = "";
            this.testResultSubView.BackColor = System.Drawing.SystemColors.Control;
            this.testResultSubView.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.testResultSubView.ElapsedTime = "";
            this.testResultSubView.Location = new System.Drawing.Point(3, 313);
            this.testResultSubView.Name = "testResultSubView";
            this.testResultSubView.Outcome = "";
            this.testResultSubView.Size = new System.Drawing.Size(0, 111);
            this.testResultSubView.TabIndex = 29;
            // 
            // testOutputSubView
            // 
            this.testOutputSubView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.testOutputSubView.BackColor = System.Drawing.SystemColors.Control;
            this.testOutputSubView.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.testOutputSubView.Location = new System.Drawing.Point(3, 430);
            this.testOutputSubView.Name = "testOutputSubView";
            this.testOutputSubView.Output = "";
            this.testOutputSubView.Size = new System.Drawing.Size(516, 72);
            this.testOutputSubView.TabIndex = 29;
            // 
            // TestPropertiesView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.flowLayoutPanel1);
            this.Name = "TestPropertiesView";
            this.Size = new System.Drawing.Size(525, 526);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label header;
        private TestPackageSubView testPackageSubView;
        private TestCentric.Gui.Views.TestPropertiesSubView testPropertiesSubView;
        private TestCentric.Gui.Views.TestResultSubView testResultSubView;
        private TestCentric.Gui.Views.TestOutputSubView testOutputSubView;

        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
    }
}
