using System;
using System.IO;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;

#if NET_4_6
#else
using Mono.CSharp;
#endif

namespace Tools
{
	public class Probability
	{
        public static bool IsProbOccur(double prob)
        {
            if (Math.Abs(prob) < Double.Epsilon)
            {
                return false;
            }

            if (prob > 1.0)
            {
                return true;
            }

            if (prob.CompareTo(1.0) == 0)
            {
                return true;
            }

            int prb = (int)(prob * 10000);

            System.Random ra = new System.Random(GetRandomSeed());
            int result = ra.Next(1, 10000);
            if (result <= prb)
            {
                return true;
            }

            return false;
        }

        public static void ProbAction(double prob1, Action action1,
                                      double prob2, Action action2)
        {
            if (Math.Abs(prob1+prob2) < Double.Epsilon)
            {
                return;
            }

            int prb1 = (int)(prob1 * 1000);
            int prb2 = (int)(prob2 * 1000);

            System.Random ra = new System.Random(GetRandomSeed());
            int result = ra.Next(1, 1000);
            if (result <= prb1)
            {
                action1();
                return;
            }
            if (result <= prb1+prb2)
            {
                action2();
                return;
            }
        }

        public static int GetRandomNum(int min, int max)
        {
			if (min == max) 
			{
				return min;
			}

            System.Random ra = new System.Random(GetRandomSeed());
            return ra.Next(min, max);

        }

        public static int GetGaussianRandomNum(int min, int max)
        {
            System.Random ra = new System.Random(GetRandomSeed());

            int[] iResult = { ra.Next(min, max), ra.Next(min, max), ra.Next(min, max) };

            return (iResult[0] + iResult[1] + iResult[2]) / 3;
        }

		public static int gaussrand(int E, int V, int L)
		{
			int R;
			do {
				
				double V1, V2, S, X;
				//int phase = 0;

				System.Random ra = new System.Random (GetRandomSeed ());

					do {
						double U1 = (double)ra.NextDouble();
						double U2 = (double)ra.NextDouble();

						V1 = 2 * U1 - 1;
						V2 = 2 * U2 - 1;
						S = V1 * V1 + V2 * V2;
                } while(S >= 1 || S.CompareTo(0) == 0);

					X = V1 * Math.Sqrt (-2 * Math.Log (S) / S);
	


				X = X * V + E;
				R = (int)X;

			} while(R <= E + L && R >= E - L);

			return R;
		}

        public static bool Calc(int iRate)
        {
            System.Random ran = new System.Random(GetRandomSeed());
            int RandKey = ran.Next(1, 100);
            if (RandKey <= iRate)
            {
                return true;
            }

            return false;
        }

		public static List<int> GetRandomNumArrayWithStableSum(int count, int sum)
		{
			List<int> list = new List<int> ();
			while(list.Count != count-1)
			{
				int random = GetRandomNum (0, sum);
				if (list.Contains (random)) 
				{
					continue;
				}

				list.Add (random);
			}

			list.Sort ();

			List<int> resultList = new List<int> ();
			for (int i = 0; i < list.Count+1; i++)
			{
				if (i == 0) 
				{
					resultList.Add (list [i] - 0);
				} 
				else if (i == list.Count)
				{
					resultList.Add (100 - list [i - 1]);
				}
				else
				{
					resultList.Add (list [i] - list [i - 1]);
				}
			}

			return resultList;
		}

        public static void ProbGroup(double prob1, Action action1,
                              double prob2, Action action2,
                              double prob3=0.0, Action action3 = null,
                              double prob4=0.0, Action action4 = null)
        {
            

            int p1 = (int)(prob1 * 1000);
            int p2 = (int)((prob2 + prob1) * 1000);
            int p3 = (int)((prob3 + prob2 + prob1) * 1000);
            int p4 = (int)((prob4 + prob2 + prob1) * 1000);

            if(p4 > 1000)
            {
                throw new ArgumentException("Total prob cout > 1.0");
            }

            int rad = GetRandomNum(0, 1000);
            if(rad < p1)
            {
                action1();
            }
            else if (rad < p2)
            {
                action2();
            }
            else if (rad < p3)
            {
                action3();
            }
            else if (rad < p4)
            {
                action4();
            }
        }

        private static int GetRandomSeed()
        {
            //byte[] bytes = new byte[4];
            //System.Security.Cryptography.RNGCryptoServiceProvider rng = new System.Security.Cryptography.RNGCryptoServiceProvider();
            //rng.GetBytes(bytes);
            //return BitConverter.ToInt32(bytes, 0);

            byte[] buffer = Guid.NewGuid().ToByteArray();
            return BitConverter.ToInt32(buffer, 0);
        }
    }

	public class StreamDir
	{
		public static string[] GetLuaFileName(string path)
		{
			string fullPath = Application.streamingAssetsPath + "/" + path;

			try
			{
	            string[] names = Directory.GetFiles(fullPath, "*.lua");

	            for (int i=0; i< names.Length; i++)
	            {
	                names[i] = names[i].Replace(fullPath, "");
	                names[i] = names[i].Replace(".lua", "");
	            }

	            return names;
			}
			catch (DirectoryNotFoundException) 
			{
				Debug.LogWarning("GetLuaFileName failed, " + fullPath);
				string[] names = { };
				return names;
			}
        }

		public static string[] GetSubDirName(string path)
		{
			string fullPath = Application.streamingAssetsPath;
			if (path != null) 
			{
				fullPath = Application.streamingAssetsPath + "/" + path;
			}
			Debug.Log(fullPath);

			string[] names = Directory.GetDirectories(fullPath);

			for (int i=0; i< names.Length; i++)
			{
				names[i] = names[i].Replace(fullPath, "");
			}

			return names;
		}
	}

    class CSVReader
    {
        public CSVReader(string path)
        {
            if (!File.Exists(path))
            {
                throw new ArgumentException("can not find file:" + path);
            }

            string[] lineArray = File.ReadAllLines(path, Encoding.UTF8);
            if (lineArray.Length < 2)
            {
                throw new ArgumentException("csv lines length < 2, file:" + path);
            }

            string[] key = lineArray[0].Split(',');
            for (int i = 1; i < lineArray.Length; i++)
            {
                string[] value = lineArray[i].Split(',');

                dynamic data = new ExpandoObject();
                var dict = (IDictionary<string, object>)data;
                for (int j = 0; j < key.Length; j++)
                {
                    dict.Add(key[j], value[j]);
                }

                _csv.Add(data);
            }
        }

        public dynamic[] rows
        {
            get
            {
                return _csv.ToArray();
            }
        }

        List<dynamic> _csv = new List<dynamic>();
    }

    [Serializable]  
	public class SerialList<T>  
	{  
		[SerializeField]  
		List<T> target;  

		public SerialList()
		{
			target = new List<T> ();
		}

		public List<T> ToList() { return target; }  

		public SerialList(List<T> target)  
		{  
			this.target = target;  
		}  
	} 

	[Serializable]
	public class SerialDictionary<TKey, TValue> : Dictionary<TKey, TValue>, ISerializationCallbackReceiver
	{
		[SerializeField]
		List<TKey> keys;
		[SerializeField]
		List<TValue> values;

		public void OnBeforeSerialize()
		{
			keys = new List<TKey>(this.Keys);
			values = new List<TValue>(this.Values);
		}

		public void OnAfterDeserialize()
		{
			var count = Math.Min(keys.Count, values.Count);
			for (var i = 0; i < count; ++i)
			{
				this.Add(keys[i], values[i]);
			}
		}
	}

	public class StringT 
	{
		public static bool isChinese(string str)
		{
			if (str == null) 
			{
				return false;
			}

			char[] ch = str.ToCharArray();
			if (str != null)
			{
				for (int i = 0; i < ch.Length; i++)
				{
					if (ch[i] >= 0x4E00 && ch[i]<= 0x9FA5)
					{
						return true;
					}
				}
			}
			return false;
		}
	}

    public static class MyExtensions
    {
        public static void nth_elementFront<T>(this List<T> array, int nthToSeek)
        {
            nth_element<T>(array, 0, nthToSeek, null);
        }

        public static void nth_elementFront<T>(this IList<T> array, int nthToSeek, Comparison<T> comparison)
        {
            nth_element<T>(array, 0, nthToSeek, comparison);
        }

        public static void nth_element<T>(this IList<T> array, int startIndex, int nthToSeek)
        {
            nth_element<T>(array, startIndex, nthToSeek, null);
        }

        public static void nth_element<T>(this IList<T> array, int startIndex, int nthToSeek, Comparison<T> comparison)
        {
            int from = startIndex;
            int to = array.Count-1;

            if (comparison == null)
            {
                comparison = delegate (T x, T y)
                {
                    return ((IComparable<T>)x).CompareTo(y);
                };
            }

            // if from == to we reached the kth element
            while (from < to)
            {
                int r = from, w = to;
                T mid = array[(r + w) / 2];

                // stop if the reader and writer meets
                while (r < w)
                {
                    if (comparison(array[r], mid) > -1)
                    { // put the large values at the end
                        T tmp = array[w];
                        array[w] = array[r];
                        array[r] = tmp;
                        w--;
                    }
                    else
                    { // the value is smaller than the pivot, skip
                        r++;
                    }
                }

                // if we stepped up (r++) we need to step one down
                if (comparison(array[r], mid) > 0)
                {
                    r--;
                }

                // the r pointer is on the end of the first k elements
                if (nthToSeek <= r)
                {
                    to = r;
                }
                else
                {
                    from = r + 1;
                }
            }

            return;
        }

    }   

}
