using System;

class BetaflightCalculator
{
    private double Clamp(double value, double min, double max)
    {
        return Math.Max(min, Math.Min(max, value));
    }

    public double BfCalc(double rcCommand, double rcRate, double expo, double superRate)
    {
        rcCommand = Clamp(rcCommand, 0, 1);
        double absRcCommand = Math.Abs(rcCommand);

        if (rcRate > 2.0)
        {
            rcRate = rcRate + (14.54 * (rcRate - 2.0));
        }

        if (expo != 0)
        {
            rcCommand = rcCommand * Math.Pow(Math.Abs(rcCommand), 3) * expo + rcCommand * (1.0 - expo);
        }

        double angleRate = 200.0 * rcRate * rcCommand;
        if (superRate != 0)
        {
            double rcSuperFactor = 1.0 / Clamp(1.0 - absRcCommand * superRate, 0.01, 1.00);
            angleRate *= rcSuperFactor;
        }

        return angleRate;
    }
}

class Program
{
    static void Main()
    {
        BetaflightCalculator calculator = new BetaflightCalculator();
        double result = calculator.BfCalc(0.5, 0.7, 0.3, 0.2);
        Console.WriteLine("Angle Rate: " + result);
    }
}
