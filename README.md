# SwarmFit

[![CI](https://github.com/swharden/SwarmFit/actions/workflows/ci.yaml/badge.svg)](https://github.com/swharden/SwarmFit/actions/workflows/ci.yaml)

**SwarmFit is a .NET package for fitting curves to X/Y data** points using [particle swarm optimization](https://en.wikipedia.org/wiki/Particle_swarm_optimization). Unlike other gradient decent strategies, finding the derivative of the error function is not required. SwarmFit can be used to calculate best fit curves for arbitrary equations that use any number of parameters.

![](dev/fit2.gif)

## Quickstart

```cs
// data points to fit
double[] xs = [1, 2, 3, 4, 5];
double[] ys = [304, 229, 174, 134, 111];

// a custom function to fit to the data using any number of parameters
static double MyFitFunc(double x, double[] parameters)
{
	double a = parameters[0];
	double b = parameters[1];
	double c = parameters[2];
	return a + b * Math.Exp(x * c);
}

// the minimum and maximum value for each parameter
double[] minVars = [-100, -5000, -10];
double[] maxVars = [100, 5000, 10];

// perform the fit with general purpose settings
double[] solution = QuickFit.Solve(xs, ys, MyFitFunc, minVars, maxVars);

// display the solution
double a = solution[0];
double b = solution[1];
double c = solution[2];
Console.WriteLine($"Y = {a} + {b} * e^(x * {c})");
```

## Graphical Demo

A sample application is included with this repository which generates random data and fits it using common curve functions. This application demonstrates advanced features such as fitter fine-tuning, iterative error logging, charting, etc.

![](dev/example3.png)