namespace SwarmFit.Demo;

public partial class Form1 : Form
{
    double[] Xs = [];
    double[] Ys = [];

    SwarmFitter? Fitter;

    readonly IFunction[] StandardFunctions = StandardFunction.GetAll();

    IFunction SelectedFunction => StandardFunctions[comboFormula.SelectedIndex];

    public Form1()
    {
        InitializeComponent();
        StandardFunctions.ToList().ForEach(x => comboFormula.Items.Add($"{x.Name} ({x.Formula()})"));
        comboFormula.SelectedIndex = 0;
        comboFormula.SelectedIndexChanged += (s, e) => Randomize();
        btnRandomize.Click += (s, e) => Randomize();
        btnStep.Click += (s, e) => IterateForward();
        btnSolve.Click += (s, e) => IterateUntilSolved();
        checkBox1.CheckedChanged += (s, e) => Randomize();
        Load += (s, e) => Randomize();
    }

    void IterateForward()
    {
        if (Fitter is null)
            return;

        // iterate until a single better solution is found
        FitSolution solution = Fitter.Solve(maxImprovements: 1);

        DisplayResult(solution);
    }

    void IterateUntilSolved()
    {
        if (Fitter is null)
            return;

        // iterate many times no matter what
        FitSolution solution = Fitter.Solve(maxIterations: 10_000, maxImprovements: 10_000);

        DisplayResult(solution);
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

        Fitter = new(Xs, Ys, SelectedFunction.Function, SelectedFunction.TypicalParameterLimits);
    }

    void DisplayResult(FitSolution solution)
    {
        if (Fitter is null)
            return;

        richTextBox1.Text = $"Calculation time: {solution.Elapsed.TotalMilliseconds:N2} msec\n" +
            $"Iterations: {solution.Iterations:N0}\n" +
            $"Improvements: {solution.Improvements:N0}\n" +
            $"Error: {solution.Error:E3}";

        Plotting.PlotFitCurve(formsPlot1.Plot, SelectedFunction, solution);
        formsPlot1.Plot.Title(SelectedFunction.Formula(solution.Parameters));
        formsPlot1.Refresh();
    }
}
