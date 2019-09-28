using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LibrarySimplexMethod
{
    public class Simplex
    {
        //количество х
        private int n = 0;
        public int N
        { get => n;
            private set
            { if (value > 0) n = value;
                else throw new ExceptionClassLibrary("Значение должно быть больше нуля.");
            }
        }
        //количество ограничений
        private int m = 0;
        public int M
        { get => m;
            private set
            {
                if (value > 0) m = value;
                else throw new ExceptionClassLibrary("Значение должно быть больше нуля.");
            }
        }
        //матрица коэффициентов
        private double[,] a=null;
        public double[,] A { get => a; private set => a = value; }
        //массив решений
        private double[] b=null;
        public double[] B { get => b; private set => b = value; }
        //кофиуиенты при х в целевой функции
        private double[] c=null;
        public double[] C { get => c; private set => c = value; }
        //знаки операции
        private char[] sign=null;
        public char[] Sign { get => sign; private set => sign = value; }

        private List<int> indexBasis = new List<int>();
        public List<int> IndexBasis { get => indexBasis; private set => indexBasis = value; }

        private double[] delta;

        public Simplex(int n, int m, double[,] a, double[] b, double[] c,char[] sign)
        {
            N = n;
            M = m;
            A = a;
            B = b;
            C = c;
            Sign = sign;
            delta = new double[m];
        }

        //Перевод всех ограничений-неравенств в равентсва
        public void TranslationMatrixA()
        {
            for (int i = 0; i < this.Sign.Length; i++)
            {
                char item = (char)this.Sign[i];
                switch (item)
                {
                    case '<':
                        double[,] a = new double[this.n + 1, this.m];
                        Bin.OldValueInNewMatrix(this.a, ref a);
                        this.n += 1;
                        a[this.n-1, i] = 1;
                        this.a = a;
                        double[] newB = new double[N];
                        Bin.OldValueInNewMatrix(b,ref newB);
                        b = newB;
                        double[] newC = new double[N];
                        Bin.OldValueInNewMatrix(C, ref newC);
                        c = newC;
                        break;
                    case '≤':
                        a = new double[this.n + 1, this.m];
                        Bin.OldValueInNewMatrix(this.a, ref a);
                        this.n += 1;
                        a[this.n - 1, i] = 1;
                        this.a = a;
                        break;
                    case '≥':
                        a = new double[this.n + 1, this.m];
                        Bin.OldValueInNewMatrix(this.a, ref a);
                        this.n += 1;
                        a[this.n - 1, i] = -1;
                        this.a = a;
                        break;
                    case '>':
                        a = new double[this.n + 1, this.m];
                        Bin.OldValueInNewMatrix(this.a, ref a);
                        this.n += 1;
                        a[this.n-1, i] = -1;
                        this.a = a;
                        break;
                }
            }
        }

        //Поиск начального базиса в получившей матрице А
        public void SearchBasis()
        {
            for (int i = 0; i < this.A.GetLength(0); i++)
            {
                int shet = 0;
                for (int j = 0; j < this.A.GetLength(1); j++)
                {
                    if (A[i, j] != 0) shet++;
                }
                if (shet == 1) this.indexBasis.Add(i);
            }
        }
        //ещё пару тестиков не помешало бы
        public double SimpleSimplexMethod(double startZ)
        {
            for (int iteration=0;iteration<150;iteration++)
            {
                double cMin = c.Min();
                if (cMin >= 0) return startZ;
                else
                {
                    int indexBasis = 0;
                    for (int i = 0; i < c.Length; i++)
                    {
                        if (c[i] == cMin)
                        {
                            indexBasis = i;
                            break;
                        } 
                    }
                    int indexOporny = 0;
                    double bOrotny = 0;
                    for (int i = 0; i < M; i++)
                    {
                        if (A[indexBasis, i] > 0)
                        {
                            delta[i] = b[i] / a[indexBasis, i];
                        }
                        else
                        {
                            delta[i] = double.NaN;
                        }

                    }

                    bOrotny = delta.Min();
                    
                    for (int i = 0; i < delta.Length; i++)
                    {
                        if (delta[i] == bOrotny)
                        {
                            indexOporny=i;
                            break;
                        }
                    }
                    for (int i = 0; i < delta.Length; i++)
                    {
                        if (delta[i]==double.NaN)
                        {
                            delta[i] = 0;
                        }
                    }
                    for (int i = 0; i < M; i++)
                    {
                        if (i != indexOporny)
                        {
                            b[i] -= bOrotny * a[indexBasis,i];
                        }
                        else
                        {
                            b[i] = bOrotny;
                        }
                    }
                    
                    for (int i = 0; i < n; i++)
                    {
                        if (i != indexBasis)
                        {
                            a[i, indexOporny] /= a[indexBasis, indexOporny];
                        }
                    }

                    for (int i = 0; i < n; i++)
                    {
                        for (int j = 0; j < m; j++)
                        {
                            if(i!=indexBasis & j != indexOporny)
                            {
                                a[i, j] -= a[i, indexOporny] * a[indexBasis,j];
                            }
                        }
                    }

                    for (int i = 0; i < N; i++)
                    {
                        if (i != indexBasis)
                        {
                            c[i] -= a[i, indexOporny] * c[indexBasis];
                        }
                    }

                    startZ += c[indexBasis] * delta[indexOporny];
                    c[indexBasis] = 0;
                    a[indexBasis, indexOporny] = 1;
                    for (int i = 0; i < M; i++)
                    {
                        if(i!=indexOporny) a[indexBasis, i] = 0;
                    }
                }
            }
            throw new ExceptionClassLibrary("Бесконечные итерации");
        }

        
    }
}