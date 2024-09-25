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
        btnStep.Click += (s, e) => Iterate(1);
        btnSolve.Click += (s, e) => Iterate(100);
        btnImprove.Click += (s, e) => IterateUntilImproved();
        btnZeroError.Click += (s, e) => IterateUntilNoError();
        btnImproveStop.Click += (s, e) => IterateUntilImprovementsStop();
        checkBox1.CheckedChanged += (s, e) => Randomize();
        Load += (s, e) => Randomize();
    }

    void Iterate(int count)
    {
        if (Fitter is null)
            return;

        FitSolution solution = Fitter.Solve(count);
        DisplayResult(solution);
    }

    void IterateUntilImproved()
    {
        if (Fitter is null)
            return;

        // iterate until a single improvement is achieved, but set the maximum limit anyway to
        // a very large number to prevent an infinite loop if a perfect solution has already been found.
        FitSolution solution = Fitter.Solve(maxIterations: 1_000_000, maxImprovements: 1);
        DisplayResult(solution);
    }

    void IterateUntilNoError()
    {
        if (Fitter is null)
            return;

        // iterate until a single improvement is achieved, but set the maximum limit anyway to
        // a very large number to prevent an infinite loop if a perfect solution has already been found.
        FitSolution solution = Fitter.Solve(maxIterations: 1_000_000, errorThreshold: 0);
        DisplayResult(solution);
    }

    void IterateUntilImprovementsStop()
    {
        if (Fitter is null)
            return;

        // iterate until a single improvement is achieved, but set the maximum limit anyway to
        // a very large number to prevent an infinite loop if a perfect solution has already been found.
        while (true)
        {
            int originalImprovements = Fitter.ImprovementCount;
            Fitter.Solve(maxIterations: 1000, errorThreshold: 0);
            if (Fitter.ImprovementCount == originalImprovements)
            {
                break;
            }
        }

        DisplayResult(Fitter.BestSolution);
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
            $"Improvements: {solution.ImprovementCount:N0}\n" +
            $"Error: {solution.Error}";

        Plotting.PlotFitCurve(formsPlot1.Plot, SelectedFunction, solution);
        formsPlot1.Plot.Title(SelectedFunction.Formula(solution.Parameters));
        formsPlot1.Refresh();
    }
}
