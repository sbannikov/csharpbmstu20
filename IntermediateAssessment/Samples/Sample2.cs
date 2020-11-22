using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IntermediateAssessment.Samples
{
    public class Matrix
    {
        private double[][] data;
        private int n;
        private int m;
        public Matrix(int N, int M)
        {
            n = N;
            m = M;
            data = new double[][] { new double[] { 1.0 } };
        }
    }
}