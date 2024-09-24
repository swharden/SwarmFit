namespace SwarmFit.Demo;

public partial class Form1 : Form
{
    double[] Xs = [];
    double[] Ys = [];

    readonly IFunction[] StandardFunctions = StandardFunction.GetAll();

    IFunction SelectedFunction => StandardFunctions[comboBox1.SelectedIndex];

    public Form1()
    {
        InitializeComponent();
        StandardFunctions.ToList().ForEach(x => comboBox1.Items.Add($"{x.Name} ({x.Formula()})"));
        comboBox1.SelectedIndex = 0;
        comboBox1.SelectedIndexChanged += (s, e) => RandomizeAndFit();
        button1.Click += (s, e) => RandomizeAndFit();
        checkBox1.CheckedChanged += (s, e) => RandomizeAndFit();
        Load += (s, e) => RandomizeAndFit();
    }

    void RandomizeAndFit()
    {
        Randomize();
        Fit();
    }

    void Randomize()
    {
        double[] parameters = SelectedFunction.TypicalParameterLimits.Select(x => x.Random(Random.Shared)).ToArray();

        int count = 10;
        Xs = ScottPlot.Generate.Range(SelectedFunction.TypicalXRange.min, SelectedFunction.TypicalXRange.max, n: count).ToArray();
        Ys = Xs.Select(x => SelectedFunction.Function.Invoke(x, parameters)).ToArray();

        if (checkBox1.Checked)
        {
            double ySpan = Ys.Max() - Ys.Min();
            double amount = 0.2;
            for (int i = 0; i < Ys.Length; i++)
            {
                Ys[i] += ySpan * amount * (Random.Shared.NextDouble() - .5) * 2;
            }
        }

        Plotting.PlotDataPoints(formsPlot1.Plot, Xs, Ys);
        formsPlot1.Plot.Title(SelectedFunction.Formula());
        formsPlot1.Refresh();
    }

    void Fit()
    {
        SwarmFitter fitter = new(Xs, Ys, SelectedFunction.Function, SelectedFunction.TypicalParameterLimits)
        {
            StoreIntermediateSolutions = true
        };

        FitSolution solution = fitter.Solve();
        label2.Text = $"Fit achieved in {solution.Elapsed.TotalMilliseconds:N2} msec " +
            $"after {solution.Iterations:N0} iterations " +
            $"using {solution.Particles:N0} particles " +
            $"with error of {solution.Error:E3}";

        Plotting.PlotFitCurve(formsPlot1.Plot, SelectedFunction, solution);
        formsPlot1.Plot.Title(SelectedFunction.Formula(solution.Parameters));
        formsPlot1.Refresh();
    }
}
