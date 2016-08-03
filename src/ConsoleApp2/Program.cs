using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MysqlEntityFarmework;

namespace ConsoleApp2
{
    //枚举类
    public enum Gender
    {
        //女
        Female,
        //男
        Male,
    }

    //抽象类
    public abstract class a{
        //抽象方法
        public abstract string ss();
    };

    //继承a类
    public class b : a {
        //父类抽象方法的实现
        public override string ss()
        {
            return Gender.Male.ToString();
        }

        //b类的方法
        public string cs = Gender.Female.ToString();

        /// <summary>
        /// 打印数组
        /// </summary>
        /// <param name="List"></param>
        private void printlist(int[] List) {

            foreach (int s in List.ToList())
            {
                Console.Write(s + ",");
            }
            Console.WriteLine();
        }

        /// <summary>
        /// 泡沫算法
        /// </summary>
        /// <param name="List">int[]</param>
        public void BubbleSort(int[] List)
        {
            int i, j, temp; //交换标志 
            bool exchange;
            for (i = 0; i < List.Length; i++) //最多做R.Length-1趟排序 
            {
                exchange = false; //本趟排序开始前，交换标志应为假
                for (j = List.Length - 2; j >= i; j--)
                {
                    if (List[j + 1] < List[j]) //交换条件
                    {
                        temp = List[j + 1];
                        List[j + 1] = List[j];
                        List[j] = temp;
                        exchange = true; //发生了交换，故将交换标志置为真 
                    }
                }
                if (!exchange) //本趟排序未发生交换，提前终止算法 
                {
                    break;
                }
            }

            printlist(List);
        }

        /// <summary>
        /// 插入排序
        /// </summary>
        /// <param name="List">int[]</param>
        public void Sort(int[] List)
        {
            for (int i = 1; i < List.Length; ++i)
            {
                int t = List[i];
                int j = i;
                while ((j > 0) && (List[j - 1] > t))
                {
                    List[j] = List[j - 1];
                    --j;
                }
                List[j] = t;
            }

            printlist(List);
        }

        /// <summary>
        /// 选择排序
        /// </summary>
        /// <param name="List">int[]</param>
        public void Sort2(int[] List)
        {
            int min;

            for (int i = 0; i < List.Length - 1; ++i)
            {
                min = i;
                for (int j = i + 1; j < List.Length; ++j)
                {
                    if (List[j] < List[min])
                        min = j;
                }
                int t = List[min];
                List[min] = List[i];
                List[i] = t;
            }

            printlist(List);
        }
    }

    //主类
    public class Program
    {
        public static void Main(string[] args)
        {
            //通过子类实例化父类
            //a A = new b();
            //实例化子类
            //b B = new b();
            //调用父类方法
            //string abc = A.ss();

            //B.BubbleSort(new int[] { 1, 5, 3, 6, 10, 55, 9, 2, 87, 12, 34, 75, 33, 47 });
            //B.Sort(new int[] { 1, 5, 3, 6, 10, 55, 9, 2, 87, 12, 34, 75, 33, 47 });
            //B.Sort2(new int[] { 1, 5, 3, 6, 10, 55, 9, 2, 87, 12, 34, 75, 33, 47 });

            SampleData.InitDB();

            Console.ReadKey();

        }
    }
}
