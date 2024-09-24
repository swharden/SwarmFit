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
        comboFormula = new ComboBox();
        btnRandomize = new Button();
        checkBox1 = new CheckBox();
        btnStep = new Button();
        btnSolve = new Button();
        richTextBox1 = new RichTextBox();
        SuspendLayout();
        // 
        // formsPlot1
        // 
        formsPlot1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        formsPlot1.DisplayScale = 1F;
        formsPlot1.Location = new Point(12, 119);
        formsPlot1.Name = "formsPlot1";
        formsPlot1.Size = new Size(725, 468);
        formsPlot1.TabIndex = 8;
        // 
        // comboFormula
        // 
        comboFormula.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        comboFormula.DropDownStyle = ComboBoxStyle.DropDownList;
        comboFormula.FormattingEnabled = true;
        comboFormula.Location = new Point(12, 12);
        comboFormula.Name = "comboFormula";
        comboFormula.Size = new Size(725, 23);
        comboFormula.TabIndex = 9;
        // 
        // btnRandomize
        // 
        btnRandomize.Location = new Point(12, 41);
        btnRandomize.Name = "btnRandomize";
        btnRandomize.Size = new Size(97, 41);
        btnRandomize.TabIndex = 10;
        btnRandomize.Text = "Randomize";
        btnRandomize.UseVisualStyleBackColor = true;
        // 
        // checkBox1
        // 
        checkBox1.AutoSize = true;
        checkBox1.Location = new Point(115, 54);
        checkBox1.Name = "checkBox1";
        checkBox1.Size = new Size(56, 19);
        checkBox1.TabIndex = 14;
        checkBox1.Text = "Noise";
        checkBox1.UseVisualStyleBackColor = true;
        // 
        // btnStep
        // 
        btnStep.Location = new Point(177, 41);
        btnStep.Name = "btnStep";
        btnStep.Size = new Size(97, 41);
        btnStep.TabIndex = 15;
        btnStep.Text = "Step";
        btnStep.UseVisualStyleBackColor = true;
        // 
        // btnSolve
        // 
        btnSolve.Location = new Point(280, 41);
        btnSolve.Name = "btnSolve";
        btnSolve.Size = new Size(97, 41);
        btnSolve.TabIndex = 16;
        btnSolve.Text = "Solve";
        btnSolve.UseVisualStyleBackColor = true;
        // 
        // richTextBox1
        // 
        richTextBox1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        richTextBox1.BackColor = SystemColors.Control;
        richTextBox1.BorderStyle = BorderStyle.None;
        richTextBox1.Location = new Point(383, 41);
        richTextBox1.Name = "richTextBox1";
        richTextBox1.ReadOnly = true;
        richTextBox1.Size = new Size(354, 72);
        richTextBox1.TabIndex = 17;
        richTextBox1.Text = "";
        // 
        // Form1
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(749, 599);
        Controls.Add(richTextBox1);
        Controls.Add(btnSolve);
        Controls.Add(btnStep);
        Controls.Add(checkBox1);
        Controls.Add(btnRandomize);
        Controls.Add(comboFormula);
        Controls.Add(formsPlot1);
        Name = "Form1";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "SwarmFit Demo";
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion
    private ScottPlot.WinForms.FormsPlot formsPlot1;
    private ComboBox comboFormula;
    private Button btnRandomize;
    private CheckBox checkBox1;
    private Button btnStep;
    private Button btnSolve;
    private RichTextBox richTextBox1;
}