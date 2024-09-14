namespace SwarmFit.Demo;

partial class Form1
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

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        btnExp2P = new Button();
        btnExp3P = new Button();
        tableLayoutPanel1 = new TableLayoutPanel();
        formsPlot1 = new ScottPlot.WinForms.FormsPlot();
        formsPlot2 = new ScottPlot.WinForms.FormsPlot();
        groupBox1 = new GroupBox();
        checkNoise = new CheckBox();
        label1 = new Label();
        tableLayoutPanel1.SuspendLayout();
        groupBox1.SuspendLayout();
        SuspendLayout();
        // 
        // btnExp2P
        // 
        btnExp2P.Location = new Point(6, 22);
        btnExp2P.Name = "btnExp2P";
        btnExp2P.Size = new Size(51, 30);
        btnExp2P.TabIndex = 1;
        btnExp2P.Text = "2P";
        btnExp2P.UseVisualStyleBackColor = true;
        // 
        // btnExp3P
        // 
        btnExp3P.Location = new Point(63, 22);
        btnExp3P.Name = "btnExp3P";
        btnExp3P.Size = new Size(51, 30);
        btnExp3P.TabIndex = 3;
        btnExp3P.Text = "3P";
        btnExp3P.UseVisualStyleBackColor = true;
        // 
        // tableLayoutPanel1
        // 
        tableLayoutPanel1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        tableLayoutPanel1.ColumnCount = 2;
        tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
        tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
        tableLayoutPanel1.Controls.Add(formsPlot1, 0, 0);
        tableLayoutPanel1.Controls.Add(formsPlot2, 1, 0);
        tableLayoutPanel1.Location = new Point(12, 77);
        tableLayoutPanel1.Name = "tableLayoutPanel1";
        tableLayoutPanel1.RowCount = 1;
        tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
        tableLayoutPanel1.Size = new Size(1078, 573);
        tableLayoutPanel1.TabIndex = 4;
        // 
        // formsPlot1
        // 
        formsPlot1.DisplayScale = 1F;
        formsPlot1.Dock = DockStyle.Fill;
        formsPlot1.Location = new Point(3, 3);
        formsPlot1.Name = "formsPlot1";
        formsPlot1.Size = new Size(533, 567);
        formsPlot1.TabIndex = 0;
        // 
        // formsPlot2
        // 
        formsPlot2.DisplayScale = 1F;
        formsPlot2.Dock = DockStyle.Fill;
        formsPlot2.Location = new Point(542, 3);
        formsPlot2.Name = "formsPlot2";
        formsPlot2.Size = new Size(533, 567);
        formsPlot2.TabIndex = 1;
        // 
        // groupBox1
        // 
        groupBox1.Controls.Add(btnExp2P);
        groupBox1.Controls.Add(btnExp3P);
        groupBox1.Location = new Point(12, 12);
        groupBox1.Name = "groupBox1";
        groupBox1.Size = new Size(123, 59);
        groupBox1.TabIndex = 5;
        groupBox1.TabStop = false;
        groupBox1.Text = "Exponential";
        // 
        // checkNoise
        // 
        checkNoise.AutoSize = true;
        checkNoise.Checked = true;
        checkNoise.CheckState = CheckState.Checked;
        checkNoise.Location = new Point(153, 41);
        checkNoise.Name = "checkNoise";
        checkNoise.Size = new Size(56, 19);
        checkNoise.TabIndex = 6;
        checkNoise.Text = "Noise";
        checkNoise.UseVisualStyleBackColor = true;
        // 
        // label1
        // 
        label1.AutoSize = true;
        label1.Location = new Point(231, 42);
        label1.Name = "label1";
        label1.Size = new Size(38, 15);
        label1.TabIndex = 7;
        label1.Text = "label1";
        // 
        // Form1
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(1102, 662);
        Controls.Add(label1);
        Controls.Add(checkNoise);
        Controls.Add(groupBox1);
        Controls.Add(tableLayoutPanel1);
        Name = "Form1";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "SwarmFit Demo";
        tableLayoutPanel1.ResumeLayout(false);
        groupBox1.ResumeLayout(false);
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion
    private Button btnExp2P;
    private Button btnExp3P;
    private TableLayoutPanel tableLayoutPanel1;
    private GroupBox groupBox1;
    private ScottPlot.WinForms.FormsPlot formsPlot1;
    private CheckBox checkNoise;
    private ScottPlot.WinForms.FormsPlot formsPlot2;
    private Label label1;
}