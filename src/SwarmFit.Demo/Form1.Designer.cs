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
        btnImprove = new Button();
        btnZeroError = new Button();
        btnImproveStop = new Button();
        SuspendLayout();
        // 
        // formsPlot1
        // 
        formsPlot1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        formsPlot1.DisplayScale = 1F;
        formsPlot1.Location = new Point(12, 166);
        formsPlot1.Name = "formsPlot1";
        formsPlot1.Size = new Size(804, 421);
        formsPlot1.TabIndex = 8;
        // 
        // comboFormula
        // 
        comboFormula.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        comboFormula.DropDownStyle = ComboBoxStyle.DropDownList;
        comboFormula.FormattingEnabled = true;
        comboFormula.Location = new Point(12, 12);
        comboFormula.Name = "comboFormula";
        comboFormula.Size = new Size(804, 23);
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
        btnStep.Text = "Iterate Once";
        btnStep.UseVisualStyleBackColor = true;
        // 
        // btnSolve
        // 
        btnSolve.Location = new Point(280, 41);
        btnSolve.Name = "btnSolve";
        btnSolve.Size = new Size(97, 41);
        btnSolve.TabIndex = 16;
        btnSolve.Text = "Iterate 100 Times";
        btnSolve.UseVisualStyleBackColor = true;
        // 
        // richTextBox1
        // 
        richTextBox1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        richTextBox1.BackColor = SystemColors.Control;
        richTextBox1.BorderStyle = BorderStyle.None;
        richTextBox1.Location = new Point(12, 88);
        richTextBox1.Name = "richTextBox1";
        richTextBox1.ReadOnly = true;
        richTextBox1.Size = new Size(804, 72);
        richTextBox1.TabIndex = 17;
        richTextBox1.Text = "";
        // 
        // btnImprove
        // 
        btnImprove.Location = new Point(383, 41);
        btnImprove.Name = "btnImprove";
        btnImprove.Size = new Size(97, 41);
        btnImprove.TabIndex = 18;
        btnImprove.Text = "Iterate Until Improved";
        btnImprove.UseVisualStyleBackColor = true;
        // 
        // btnZeroError
        // 
        btnZeroError.Location = new Point(486, 42);
        btnZeroError.Name = "btnZeroError";
        btnZeroError.Size = new Size(97, 41);
        btnZeroError.TabIndex = 19;
        btnZeroError.Text = "Iterate Until Zero Error";
        btnZeroError.UseVisualStyleBackColor = true;
        // 
        // button1
        // 
        btnImproveStop.Location = new Point(589, 42);
        btnImproveStop.Name = "button1";
        btnImproveStop.Size = new Size(129, 41);
        btnImproveStop.TabIndex = 20;
        btnImproveStop.Text = "Iterate Until Improvements Stop";
        btnImproveStop.UseVisualStyleBackColor = true;
        // 
        // Form1
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(828, 599);
        Controls.Add(btnImproveStop);
        Controls.Add(btnZeroError);
        Controls.Add(btnImprove);
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
    private Button btnImprove;
    private Button btnZeroError;
    private Button btnImproveStop;
}