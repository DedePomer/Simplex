using System;
using System.IO;

namespace Симплекс_метод
{
    class Program
    {
        static bool ProverkaStroki(double[,] mas, int n, int m)// проверяет есть ли в строке оценки положтельные числа
        {
            bool a = false;
            int b = 0;
            for (int i = 0; i < m; i++)
            {
                if (mas[n-1, i] > 0)
                {
                    a = true;
                    b++;
                }
                else if (mas[n-1, i] <= 0)
                {
                    a = false;
                }
                if (b != 0)
                    break;
            }
            return a;

        }
        static void PodchetF(double[,] mas, int n, int m)// Метод Гаусса + определление разрешающего элемента
        {
            double max = mas[n-1, 0];
            int jmax = 0;
            for (int j = 0; j < m ; j++)// определяет сьолбец
            {
                if (max < mas[n-1, j])
                {
                    max = mas[n-1, j];
                    jmax = j;
                }
            }

            double min = mas[0, m-1] / mas[0, jmax];
            int imin = 0;
            for (int i = 0; i < n-1; i++)// определяет строку
            {
                if (min > (mas[i, m-1] / mas[i, jmax]))
                {
                    min = mas[i, m-1] / mas[i, jmax];
                    imin = i;
                }
            }

            double razel = mas[imin, jmax];
            double[] mas2 = new double[m];    
            double[] mas3 = new double[n];
            for (int i = 0; i < n; i++)
			{
             mas3[i] = mas[i,jmax] ;
			}
            for (int j = 0; j < m; j++)
            {
                    mas[imin, j] = mas[imin, j] / razel;
            }
            for (int j = 0; j < m; j++)
            {
                mas2[j] = mas[imin, j];
            }
            for (int i = 0; i < n; i++)// складываем строки
            {               
               if (i!=imin)
	           {
                 for (int j = 0; j < m; j++)
			     {
                   mas[i,j]=mas[i,j]+mas2[j]*(-mas3[i]);
			     }
	           }               
            }   

        }

        static void Main(string[] args)
        {
            string path = "Симплекс-метод";                        
            string[] mas = File.ReadAllLines(path);
            int n; // кол-во строк  в симплекс таблице     
            int m;// кол-во столбцов в симплекс таблице  
            string [] mas1 = mas[0].Split(' ');
            n = mas.Length;
            m = mas1.Length + (n - 1);
            double [,] simp = new double[n,m];
            
            for (int i = 0; i < n; i++)
            {               
                mas1 = mas[i].Split(' ');
                for (int j = 0; j < mas1.Length; j++)
                {  if (i == n - 1)
                        simp[i, j] = Convert.ToInt32(mas1[j]);
                    else if (j == 0)
                    {
                        simp[i, m - 1] = Convert.ToInt32(mas1[j]);                        
                    }
                    else 
                        simp[i, j-1] = Convert.ToInt32(mas1[j]);                                  
                }                
            }
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    if (j == (i+1) + mas1.Length - 1 && i !=n-1)
                    {
                        simp[i, j] = 1;
                    }

                }
            }
            while(ProverkaStroki(simp,  n,  m)==true)
            {
              PodchetF(simp,  n,  m);            
            }
            string otv = "F= "+(simp[n - 1, m - 1] * -1)+", ";
            mas1 = mas[0].Split(' ');
            int kolx = mas1.Length - 1;
            int kolxi = n;
            int a = 0;
            int kol1 = 0;
            for (int j = 0; j < kolx; j++) 
            {
                kol1 = 0;
                a = 0;
                for (int i = 0; i < n; i++)
                {
                    if (simp[i,j]==0)
                    {
                        a ++;
                    }
                    else if (simp[i, j] == 1)
                    {
                        kol1++;                        
                        kolxi = i;
                    }
                }
                if (a == n - 1 && kol1 == 1)
                {
                    otv = otv + "X" + (j + 1) + "=" + simp[kolxi, m - 1] + ", ";
                }
                else
                {
                    otv = otv + "X" + (j + 1) + "=0, ";
                }
            }


           
            path = "Ответ.csv";
            File.WriteAllText(path, otv);
            Console.WriteLine("Ответ загружен в одноимённую папку.");           

        }
    }
}
