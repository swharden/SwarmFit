# SwarmFit

**SwarmFit is a .NET package for fitting curves to X/Y data** points using particle swarm optimization. Unlike other gradient decent strategies, finding the derivative of the error function is not required. SwarmFit can be used to calculate best fit curves for arbitrary equations that use any number of parameters.

![](https://raw.githubusercontent.com/swharden/SwarmFit/main/dev/fit.gif)

## Quickstart

```cs
// data points to fit
double[] xs = [1, 2, 3, 4, 5];
double[] ys = [304, 229, 174, 134, 111];

// define a fit function using any number of parameters
static double MyFunc(double x, double[] parameters)
{
	double a = parameters[0];
	double b = parameters[1];
	double c = parameters[2];
    return a + b * Math.Exp(x * c);
}

// define the minimum and maximum value for each parameter
double[] paramMins = [-100, -5000, -10];
double[] paramMaxs = [100, 5000, 10];

// perform the fit
double[] solution = QuickFit.Solve(xs, ys, MyFunc, minParams, maxParams);

// display the solution
double a = solution[0];
double b = solution[1];
double c = solution[2];
Console.WriteLine($"Y = {a} + {b} * e^(x * {c})");
```

## Documentation

See [https://github.com/swharden/SwarmFit/](https://github.com/swharden/SwarmFit/) for additional code and documentation