using System;
using System.Drawing;

namespace MathematicalMorphology
{
    public static class MathMorph
    {
        ///<summary>Наращивание</summary>
        ///<param name="B">Бинарное изображение</param>
        ///<param name="S">Структурный элемент</param>
        ///<param name="Xoffset">X координата начала координат структурного элемента</param>
        ///<param name="Yoffset">Y координата начала координат структурного элемента</param>
        public static bool[][] Dilation (bool[][] B, bool[][] S, int Xoffset, int Yoffset)
        {
            int x, y, a, b, k, l;
            bool[][] C = new bool[][] { };

            if (SetTheory.Verification (B) && SetTheory.Verification (S))
            {
                int w = B.Length;
                int h = B[0].Length;
                C = SetTheory.NewArr (w, h);

                for (int i = 0; i < w; i++)
                {
                    for (int j = 0; j < h; j++)
                    {
                        if (B[i][j])
                        {
                            k = 0;
                            l = 0;
                            x = i - Xoffset;
                            y = j - Yoffset;
                            a = S.Length;
                            b = S[0].Length;

                            if (x < 0 && w - a - i >= 0)
                            {
                                k = Math.Abs (x);
                                a = S.Length - k;
                            } else if (x >= 0 && w - a - i < 0)
                            {
                                a = S.Length - Math.Abs (w - a - x);
                            }

                            if (y < 0 && h - b - j >= 0)
                            {
                                l = Math.Abs (y);
                                b = S[0].Length - l;
                            } else if (y >= 0 && h - b - j < 0)
                            {
                                b = S.Length - Math.Abs (h - b - y);
                            }
                            
                            RecordInTheArr (ref C, SetTheory.Union (CopyOfTheArr (C, a, b, x, y), S), x, y);
                        }
                    }
                }
            }

            return C;
        }

        ///<summary>Эрозия</summary>
        ///<param name="B">Бинарное изображение</param>
        ///<param name="S">Структурный элемент</param>
        ///<param name="Xoffset">X координата начала координат структурного элемента</param>
        ///<param name="Yoffset">Y координата начала координат структурного элемента</param>
        public static bool[][] Erosion (bool[][] B, bool[][] S, int Xoffset, int Yoffset)
        {
            int x, y, a, b, k, l;
            bool[][] C = new bool[][] { };

            if (SetTheory.Verification (B) && SetTheory.Verification (S))
            {
                int w = B.Length;
                int h = B[0].Length;
                C = SetTheory.NewArr (w, h);

                for (int i = 0; i < w; i++)
                {
                    for (int j = 0; j < h; j++)
                    {
                        if (B[i][j])
                        {
                            k = 0;
                            l = 0;
                            x = i - Xoffset;
                            y = j - Yoffset;
                            a = S.Length;
                            b = S[0].Length;

                            if (x < 0 && w - a - i >= 0)
                            {
                                k = Math.Abs (x);
                                a = S.Length - k;
                            } else if (x >= 0 && w - a - i < 0)
                            {
                                a = S.Length - Math.Abs (w - a - x);
                            }

                            if (y < 0 && h - b - j >= 0)
                            {
                                l = Math.Abs (y);
                                b = S[0].Length - l;
                            } else if (y >= 0 && h - b - j < 0)
                            {
                                b = S.Length - Math.Abs (h - b - y);
                            }

                            C[i][j] = CompareArr (S, CopyOfTheArr (B, a, b, x, y));
                        }
                    }
                }
            }

            return C;
        }

        ///<summary>Размыкание</summary>
        ///<param name="B">Бинарное изображение</param>
        ///<param name="S">Структурный элемент</param>
        ///<param name="Xoffset">X координата начала координат структурного элемента</param>
        ///<param name="Yoffset">Y координата начала координат структурного элемента</param>
        public static bool[][] Opening (bool[][] B, bool[][] S, int Xoffset, int Yoffset)
        {
            return MathMorph.Dilation (MathMorph.Erosion (B, S, Xoffset, Yoffset), S, Xoffset, Yoffset);
        }

        ///<summary>Замыкание</summary>
        ///<param name="B">Бинарное изображение</param>
        ///<param name="S">Структурный элемент</param>
        ///<param name="Xoffset">X координата начала координат структурного элемента</param>
        ///<param name="Yoffset">Y координата начала координат структурного элемента</param>
        public static bool[][] Closing (bool[][] B, bool[][] S, int Xoffset, int Yoffset)
        {
            return MathMorph.Erosion (MathMorph.Dilation (B, S, Xoffset, Yoffset), S, Xoffset, Yoffset);
        }



        //===---------------------------===//
        //      Дополнительные методы      //
        //===---------------------------===//

        ///<summary>Сравнение первого множества со вторым, для каждого значения true множества A значение множества B должно совпадать</summary>
        ///<returns>Совпадает ли множество B в значениях true множества A</returns>
        public static bool CompareArr (bool[][] A, bool[][] B)
        {
            bool boo = false;
            int w = A.Length;
            int h = A[0].Length;

            if (Verification (A, B))
            {
                boo = true;
                for (int i = 0; i < w; i++)
                {
                    for (int j = 0; j < h; j++)
                    {
                        if (A[i][j] && !B[i][j])
                        {
                            boo = false;
                            break;
                        }
                    }
                }
            }
            
            return boo;
        }
        
        /// <summary>Копирование из массива</summary>
        /// <param name="A">Исходный массив</param>
        /// <param name="w">Ширина копируемого массива</param>
        /// <param name="h">Высота копируемого массива</param>
        /// <param name="Xoffset">Начальная координата X исходного массива, с которой начинается копирование</param>
        /// <param name="Yoffset">Начальная координата Y исходного массива, с которой начинается копирование</param>
        /// <returns>Скопированный массив</returns>
        public static bool[][] CopyOfTheArr (bool[][] A, int w, int h, int Xoffset, int Yoffset)
        {
            Xoffset = (Xoffset > 0) ? Xoffset : 0;
            Yoffset = (Yoffset > 0) ? Yoffset : 0;
            bool[][] C = new bool[w][];
            for (int i = 0; i < w; i++)
            {
                C[i] = new bool[h];
                for (int j = 0; j < h; j++)
                {
                    C[i][j] = A[i + Xoffset][j + Yoffset];
                }
            }

            return C;
        }
        
        /// <summary>Запись в массив</summary>
        /// <param name="A">Массив, в который идет запись</param>
        /// <param name="B">Массив, который записывают</param>
        /// <param name="Xoffset">Начальная X координата записи</param>
        /// <param name="Yoffset">Начальная Y координата записи</param>
        public static void RecordInTheArr (ref bool[][] A, bool[][] B, int Xoffset, int Yoffset)
        {
            int a = 0;
            int b = 0;
            int w = B.Length;
            int h = B[0].Length;

            if (Xoffset + w > A.Length) w -= Math.Abs (A.Length - (Xoffset + w));
            if (Yoffset + h > A[0].Length) h -= Math.Abs (A[0].Length - (Yoffset + h));
            if (Xoffset < 0)
            {
                a = -Xoffset;
                w += Xoffset;
                Xoffset = 0;
            }
            if (Yoffset < 0)
            {
                b = -Yoffset;
                h += Yoffset;
                Yoffset = 0;
            }

            for (int i = 0; i < w; i++)
                for (int j = 0; j < h; j++)
                    A[i + Xoffset][j + Yoffset] = B[i + a][j + b];
        }

        /// <summary>Конвертирование массива в битмапу</summary>
        public static Bitmap ConvertToBitmap (bool[][] A)
        {
            int w = A.Length;
            int h = A[0].Length;
            Bitmap bmp = new Bitmap (h, w);

            for (int i = 0; i < w; i++)
                for (int j = 0; j < h; j++)
                    bmp.SetPixel (j, i, (A[i][j]) ? Color.Black : Color.White);

            return bmp;
        }

        /// <summary>Конвертирование битмапы в массив</summary>
        public static bool[][] ConvertToBool (Bitmap bmp)
        {
            int w = bmp.Width;
            int h = bmp.Height;
            int blackHashCode = Color.FromArgb (0xff, 0x00, 0x00, 0x00).GetHashCode ();
            bool[][] C = SetTheory.NewArr (h, w);

            for (int i = 0; i < w; i++)
                for (int j = 0; j < h; j++)
                    C[j][i] = (bmp.GetPixel (i, j).GetHashCode () == blackHashCode);
            
            return C;
        }

        ///<summary>Проверка массивов на одинаковый размер</summary>
        public static bool Verification (bool[][] A, bool[][] B)
        {
            bool check = false;
            if (SetTheory.Verification (A) && SetTheory.Verification (B))
            {
                check = true;
                if (A.Length != B.Length) check = false;
                if (A[0].Length != B[0].Length) check = false;
            }

            return check;
        }
    }
}
