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
        tableLayoutPanel1 = new TableLayoutPanel();
        tableLayoutPanel2 = new TableLayoutPanel();
        formsPlotData = new ScottPlot.WinForms.FormsPlot();
        formsPlotVariables = new ScottPlot.WinForms.FormsPlot();
        formsPlotProgress = new ScottPlot.WinForms.FormsPlot();
        btnRandomize = new Button();
        btnStep = new Button();
        lblStatus = new Label();
        tableLayoutPanel1.SuspendLayout();
        tableLayoutPanel2.SuspendLayout();
        SuspendLayout();
        // 
        // tableLayoutPanel1
        // 
        tableLayoutPanel1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        tableLayoutPanel1.ColumnCount = 2;
        tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
        tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
        tableLayoutPanel1.Controls.Add(tableLayoutPanel2, 1, 0);
        tableLayoutPanel1.Controls.Add(formsPlotData, 0, 0);
        tableLayoutPanel1.Location = new Point(12, 48);
        tableLayoutPanel1.Name = "tableLayoutPanel1";
        tableLayoutPanel1.RowCount = 1;
        tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
        tableLayoutPanel1.Size = new Size(964, 409);
        tableLayoutPanel1.TabIndex = 0;
        // 
        // tableLayoutPanel2
        // 
        tableLayoutPanel2.ColumnCount = 1;
        tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
        tableLayoutPanel2.Controls.Add(formsPlotVariables, 0, 0);
        tableLayoutPanel2.Controls.Add(formsPlotProgress, 0, 1);
        tableLayoutPanel2.Dock = DockStyle.Fill;
        tableLayoutPanel2.Location = new Point(485, 3);
        tableLayoutPanel2.Name = "tableLayoutPanel2";
        tableLayoutPanel2.RowCount = 2;
        tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
        tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
        tableLayoutPanel2.Size = new Size(476, 403);
        tableLayoutPanel2.TabIndex = 0;
        // 
        // formsPlotData
        // 
        formsPlotData.DisplayScale = 1F;
        formsPlotData.Dock = DockStyle.Fill;
        formsPlotData.Location = new Point(3, 3);
        formsPlotData.Name = "formsPlotData";
        formsPlotData.Size = new Size(476, 403);
        formsPlotData.TabIndex = 1;
        // 
        // formsPlotVariables
        // 
        formsPlotVariables.DisplayScale = 1F;
        formsPlotVariables.Dock = DockStyle.Fill;
        formsPlotVariables.Location = new Point(3, 3);
        formsPlotVariables.Name = "formsPlotVariables";
        formsPlotVariables.Size = new Size(470, 195);
        formsPlotVariables.TabIndex = 0;
        // 
        // formsPlotProgress
        // 
        formsPlotProgress.DisplayScale = 1F;
        formsPlotProgress.Dock = DockStyle.Fill;
        formsPlotProgress.Location = new Point(3, 204);
        formsPlotProgress.Name = "formsPlotProgress";
        formsPlotProgress.Size = new Size(470, 196);
        formsPlotProgress.TabIndex = 1;
        // 
        // btnRandomize
        // 
        btnRandomize.Location = new Point(12, 12);
        btnRandomize.Name = "btnRandomize";
        btnRandomize.Size = new Size(75, 30);
        btnRandomize.TabIndex = 1;
        btnRandomize.Text = "Randomize";
        btnRandomize.UseVisualStyleBackColor = true;
        // 
        // btnStep
        // 
        btnStep.Location = new Point(93, 12);
        btnStep.Name = "btnStep";
        btnStep.Size = new Size(75, 30);
        btnStep.TabIndex = 2;
        btnStep.Text = "Step";
        btnStep.UseVisualStyleBackColor = true;
        // 
        // lblStatus
        // 
        lblStatus.AutoSize = true;
        lblStatus.Location = new Point(174, 20);
        lblStatus.Name = "lblStatus";
        lblStatus.Size = new Size(38, 15);
        lblStatus.TabIndex = 3;
        lblStatus.Text = "label1";
        // 
        // Form1
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(988, 469);
        Controls.Add(lblStatus);
        Controls.Add(btnStep);
        Controls.Add(btnRandomize);
        Controls.Add(tableLayoutPanel1);
        Name = "Form1";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "SarmFit Demo";
        tableLayoutPanel1.ResumeLayout(false);
        tableLayoutPanel2.ResumeLayout(false);
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private TableLayoutPanel tableLayoutPanel1;
    private TableLayoutPanel tableLayoutPanel2;
    private ScottPlot.WinForms.FormsPlot formsPlotVariables;
    private ScottPlot.WinForms.FormsPlot formsPlotProgress;
    private ScottPlot.WinForms.FormsPlot formsPlotData;
    private Button btnRandomize;
    private Button btnStep;
    private Label lblStatus;
}