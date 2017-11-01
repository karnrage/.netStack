using System;

namespace basic_13
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            IterateArray();
        }
        
        static void IterateArray()
        {
            Console.WriteLine("Itareting thru an Array");
            List<int> mylist = new List<int>(new int[] {1,3, 5,7,9});

            foreach(int i in mylist)
            {
                Console.WriteLine(i);
            }
        }

        static void FindMax()
        {
            Console.WriteLine("find max");
            List<int> maxList = new List<int><int>(new int[] {1,3,5,7,9,-13,100,-25,0,99});
            int max = maxList[0];
            int total = maxList[0];
            for (int i=1; i <maxList.count; i++)
            {
                if(maxList[i] > max) {max = maxList[i];}
                total += maxList[i];
                .........need from uploaded
            }
        }

        static void OddArray()
        {
            Console.WriteLine
        }

        
        static void GreaterThanY()
        {
            Console.WriteLine
        }

        static void SquareValues()
        {
            Console.WriteLine("sqr the values");
            Console.WriteLine("the array before sqr is {0}", Array);

        }

        static void Eliminatenegs(List<int> array)
        {
            Console.WriteLine("elim negs");
            Console.WriteLine("the array before elim negs is {0}", array);
            for (int i=0)
            
        }

        static void minmaxavg(List<int> minmaxavg)
        {
            Console.WriteLine(min, max, avg);
            int mmin=minmaxavg[0];
            int max2= minmaxavg[0];
            int usm 2 = minmaxavg maxavg[0]
        }

        static void ShiftArray(List<int> shiftArray)
        {
            Console.WriteLine("Shiftint the values in an array");
            for (int i=0; i < shiftArray.Count-1; i++)
            {
                shiftArray[i]=shiftArray[i+1];                
            }
            shiftArray [shiftArray.Count -1]=0;
            Console.WriteLine("the shifted array is {0}.", shiftArray);
        }
           
        static void NumToStrng(List<object> numToStrig_
        {
            Console.WriteLine("number to stirng");
            for(int i=0; i< numtoString.Count; i ++)
            {
                if ((int(numtostring[i]<0) {numToString[i]= "Dojo";}
                
            }
        })
        }
    }   
}
