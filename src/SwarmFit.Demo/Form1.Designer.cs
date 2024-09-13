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
        formsPlot1 = new ScottPlot.WinForms.FormsPlot();
        btnExp3P = new Button();
        SuspendLayout();
        // 
        // btnExp2P
        // 
        btnExp2P.Location = new Point(12, 12);
        btnExp2P.Name = "btnExp2P";
        btnExp2P.Size = new Size(51, 30);
        btnExp2P.TabIndex = 1;
        btnExp2P.Text = "2P";
        btnExp2P.UseVisualStyleBackColor = true;
        // 
        // formsPlot1
        // 
        formsPlot1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        formsPlot1.DisplayScale = 1F;
        formsPlot1.Location = new Point(12, 48);
        formsPlot1.Name = "formsPlot1";
        formsPlot1.Size = new Size(660, 456);
        formsPlot1.TabIndex = 2;
        // 
        // btnExp3P
        // 
        btnExp3P.Location = new Point(69, 12);
        btnExp3P.Name = "btnExp3P";
        btnExp3P.Size = new Size(51, 30);
        btnExp3P.TabIndex = 3;
        btnExp3P.Text = "3P";
        btnExp3P.UseVisualStyleBackColor = true;
        // 
        // Form1
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(684, 516);
        Controls.Add(btnExp3P);
        Controls.Add(btnExp2P);
        Controls.Add(formsPlot1);
        Name = "Form1";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "SarmFit Demo";
        ResumeLayout(false);
    }

    #endregion
    private Button btnExp2P;
    private ScottPlot.WinForms.FormsPlot formsPlot1;
    private Button btnExp3P;
}