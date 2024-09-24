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
        formsPlot1 = new ScottPlot.WinForms.FormsPlot();
        comboBox1 = new ComboBox();
        button1 = new Button();
        label2 = new Label();
        checkBox1 = new CheckBox();
        SuspendLayout();
        // 
        // formsPlot1
        // 
        formsPlot1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        formsPlot1.DisplayScale = 1F;
        formsPlot1.Location = new Point(12, 88);
        formsPlot1.Name = "formsPlot1";
        formsPlot1.Size = new Size(725, 499);
        formsPlot1.TabIndex = 8;
        // 
        // comboBox1
        // 
        comboBox1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
        comboBox1.FormattingEnabled = true;
        comboBox1.Location = new Point(12, 12);
        comboBox1.Name = "comboBox1";
        comboBox1.Size = new Size(725, 23);
        comboBox1.TabIndex = 9;
        // 
        // button1
        // 
        button1.Location = new Point(12, 41);
        button1.Name = "button1";
        button1.Size = new Size(97, 41);
        button1.TabIndex = 10;
        button1.Text = "Randomize";
        button1.UseVisualStyleBackColor = true;
        // 
        // label2
        // 
        label2.AutoSize = true;
        label2.Location = new Point(193, 54);
        label2.Name = "label2";
        label2.Size = new Size(38, 15);
        label2.TabIndex = 13;
        label2.Text = "label2";
        // 
        // checkBox1
        // 
        checkBox1.AutoSize = true;
        checkBox1.Location = new Point(122, 53);
        checkBox1.Name = "checkBox1";
        checkBox1.Size = new Size(56, 19);
        checkBox1.TabIndex = 14;
        checkBox1.Text = "Noise";
        checkBox1.UseVisualStyleBackColor = true;
        // 
        // Form1
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(749, 599);
        Controls.Add(checkBox1);
        Controls.Add(label2);
        Controls.Add(button1);
        Controls.Add(comboBox1);
        Controls.Add(formsPlot1);
        Name = "Form1";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "SwarmFit Demo";
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion
    private ScottPlot.WinForms.FormsPlot formsPlot1;
    private ComboBox comboBox1;
    private Button button1;
    private Label label2;
    private CheckBox checkBox1;
}