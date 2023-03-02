using MathNet.Numerics.LinearAlgebra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PseudoskinService.Services.Regression
{
    public static class RegressionAnalysis
    {
        public static int FindDegreeOfBestFit(double[] x, double[] y)
        {
            int degree = 1;
            double rSquare = 0;
            while (rSquare <1 && degree <x.Length-1)
            {
                degree++;
                var coefficients = FitPolynomial(x, y, degree);
                var predicted = EvaluatePolynomial(x, coefficients);
                rSquare = CalculateRSquared(y, predicted);
            }

            return degree;
        }

        public static double[] FindPredictedBestFits(double[] x, double[] y)
        {
            var degreeOfBestFit = FindDegreeOfBestFit(x, y);
            var coefficientOfBestFit = FitPolynomial(x, y, degreeOfBestFit);
            var predictedBestFits = EvaluatePolynomial(x, coefficientOfBestFit);

            return predictedBestFits;
        }
        public static double[] FitPolynomial(double[] x, double[] y, int degree)
        {
            var X = Matrix<double>.Build.Dense(x.Length, degree + 1, (i, j) => Math.Pow(x[i], j));

            var Y = Vector<double>.Build.DenseOfArray(y);
            var coefficients = X.QR().Solve(Y);

            return coefficients.ToArray();
        }

        public static double[] EvaluatePolynomial(double[] x, double[] coefficients)
        {
            var y = new double[x.Length];
            for (int i = 0; i < x.Length; i++)
            {
                double sum = 0;
                for (int j = coefficients.Length-1; j>=0; j--)
                {
                    sum += coefficients[j] * Math.Pow(x[i], j);
                }
                y[i] = sum;
            }

            return y;
        }

        public static double CalculateRSquared(double[] y, double[] predicted)
        {
            var mean = y.Average();
            var ssTot = y.Select(yi => Math.Pow(yi - mean, 2)).Sum();
            var ssRes = y.Zip(predicted, (yi, pi) => Math.Pow(yi - pi, 2)).Sum();
            var rSquared = 1 - ssRes / ssTot;

            return rSquared;
        }
    }
}
