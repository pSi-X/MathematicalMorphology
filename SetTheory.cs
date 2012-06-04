using System;

namespace MathematicalMorphology
{
    public static class SetTheory
    {
        ///<summary>Бинарные операции</summary>
        public enum binaryOperations
        {
            Union,
            Intersection,
            Difference,
            SymmetricDifference,
            Complement,
            CartesianProduct
        }

        ///<summary>Объединение</summary>
        public static bool[][] Union (bool[][] A, bool[][] B)
        {
            return Operation (A, B, binaryOperations.Union);
        }

        ///<summary>Пересечение</summary>
        public static bool[][] Intersection (bool[][] A, bool[][] B)
        {
            return Operation (A, B, binaryOperations.Intersection);
        }

        ///<summary>Разность</summary>
        public static bool[][] Difference (bool[][] A, bool[][] B)
        {
            return Operation (A, B, binaryOperations.Difference);
        }

        ///<summary>Симметрическая разность</summary>
        public static bool[][] SymmetricDifference (bool[][] A, bool[][] B)
        {
            return Operation (A, B, binaryOperations.SymmetricDifference);
        }

        ///<summary>Дополнение</summary>
        public static bool[][] Сomplement (bool[][] A)
        {
            bool[][] C = new bool[][] { };
            if (Verification (A))
            {
                int w = A.Length;
                int h = A[0].Length;
                C = new bool[w][];

                for (int i = 0; i < w; i++)
                {
                    C[i] = new bool[h];
                    for (int j = 0; j < h; j++)
                    {
                        C[i][j] = !A[i][j];
                    }
                }
            }

            return C;
        }

        ///<summary>Декартово произведение</summary>
        public static bool[][] CartesianProduct (bool[][] A, bool[][] B)
        {
            bool[][] C = new bool[][] { };
            if (Verification (A) && Verification (B))
            {
                int k = 0;
                int w1 = A.Length;
                int h1 = A[0].Length;
                int w2 = B.Length;
                int h2 = B[0].Length;

                C = NewArr (w1 * h1 * w2 * h2, 2);
                for (int i = 0; i < w1; i++)
                    for (int j = 0; j < h1; j++)
                        for (int m = 0; m < w2; m++)
                            for (int n = 0; n < h2; n++)
                            {
                                C[k] = new bool[2] { A[i][j], B[m][n] };
                                k++;
                            }
            }

            return C;
        }

        ///<summary>Операции с множествами</summary>
        public static bool[][] Operation (bool[][] A, bool[][] B, binaryOperations op)
        {
            bool[][] C = new bool[][] { };
            if (Verification (A) && Verification (B))
            {
                bool a, b;
                int w = Math.Max (A.Length, B.Length);
                int h = Math.Max (A[0].Length, B[0].Length);
                C = new bool[w][];

                for (int i = 0; i < w; i++)
                {
                    C[i] = new bool[h];
                    for (int j = 0; j < h; j++)
                    {
                        a = (A.Length > i && A[0].Length > j) ? A[i][j] : false;
                        b = (B.Length > i && B[0].Length > j) ? B[i][j] : false;
                        switch (op)
                        {
                            case binaryOperations.Union:
                                C[i][j] = a || b;
                                break;
                            case binaryOperations.Intersection:
                                C[i][j] = a && b;
                                break;
                            case binaryOperations.Difference:
                                C[i][j] = a && !b;
                                break;
                            case binaryOperations.SymmetricDifference:
                                C[i][j] = (a && !b) || (!a && b);
                                break;
                            default:
                                break;
                        }
                    }
                }
            }

            return C;
        }

        ///<summary>Проверка массива на соответствие прямоугольной форме</summary>
        public static bool Verification (bool[][] A)
        {
            bool check = false;
            if (A != null)
            {
                check = true;
                int w = A.Length;
                int length = A[0].Length;

                for (int i = 0; i < w; i++)
                {
                    if (A[i].Length != length)
                    {
                        check = false;
                        break;
                    }
                }
            }

            return check;
        }



        //===---------------------------===//
        //      Дополнительные методы      //
        //===---------------------------===//

        /// <summary>Создание массива, заполненного определенным значением</summary>
        /// <param name="w">Ширина массива</param>
        /// <param name="h">Высота массива</param>
        /// <param name="value">Значение, которым заполняется массив</param>
        /// <returns>Новый массив, заполненный определенным значением</returns>
        public static bool[][] NewArr (int w, int h, bool value = false)
        {
            bool[][] C = new bool[w][];
            for (int i = 0; i < w; i++)
            {
                C[i] = new bool[h];
                for (int j = 0; j < h; j++)
                {
                    C[i][j] = value;
                }
            }

            return C;
        }
    }
}
