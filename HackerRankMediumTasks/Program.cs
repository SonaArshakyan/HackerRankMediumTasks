using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using textWriter = System.Console;

namespace HackerRankMediumTasks
{
    class Program
    {
        static void Main(string[] args)
        {
            //Medium Tasks From  Strings
            //Task1
            //int q = Convert.ToInt32(Console.ReadLine());
            //for (int qItr = 0; qItr < q; qItr++)
            //{
            //    string s = Console.ReadLine();
            //    int result = sherlockAndAnagrams(s);
            //    textWriter.WriteLine(result);
            //}
            //Task2 highestValuePalindrome
            //string[] nk = Console.ReadLine().Split(' ');
            //int n = Convert.ToInt32(nk[0]);
            //int k = Convert.ToInt32(nk[1]);
            //string s = Console.ReadLine();
            //string result = highestValuePalindrome(s, n, k);
            //textWriter.WriteLine(result);

            //Task 3 Bear and Steady Gene          
            //string gene =
            //// File.ReadAllText("test.txt");
            //Console.ReadLine();
            //int result = steadyGenes(gene);
            //textWriter.WriteLine(result);

            //Task4 Common Child
            //int n = Convert.ToInt32(Console.ReadLine());
            //int[] arr = Array.ConvertAll(Console.ReadLine().Split(' '), arrTemp => Convert.ToInt32(arrTemp));
            //int reslt = lilysHomework(arr);
            //textWriter.WriteLine(reslt);



        }
        static int steadyGene1(string gene)
        {
            int n = gene.Length;
            int limit = n / 4;
            // A-0 , C-2 , G-6 , T-19
            int[] arr = new int[26];
            for (int i = 0; i < n; i++)
            {
                arr[gene[i] - 'A']++;
            }
            if (arr[0] == limit && arr[2] == limit && arr[6] == limit && arr[19] == limit) return 0;
            int upper = 0;
            int lower = 0;
            int minlen = n;
            while (upper < n && lower < n)
            {
                while (arr[0] > limit || arr[2] > limit || arr[19] > limit || arr[6] > limit && upper < n)
                {
                    arr[gene[upper] - 'A']--;
                    upper += 1;
                }
                while (arr[0] <= limit && arr[2] <= limit && arr[19] <= limit && arr[6] <= limit)
                {
                    arr[gene[lower] - 'A']++;
                    lower += 1;
                }
                if (upper - lower < minlen)
                    minlen = upper - lower + 1;
            }
            return minlen;
        }
        static bool validityCheck(Dictionary<char, int> dict, int limit)
        {
            if (dict['A'] <= limit && dict['C'] <= limit && dict['G'] <= limit && dict['T'] <= limit)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        static int steadyGenes(string s)
        {
            int leng = s.Length; int maxIndex = 0;
            Dictionary<char, int> Gact = new Dictionary<char, int>() { { 'G', 0 }, { 'A', 0 }, { 'C', 0 }, { 'T', 0 } };
            int limit = leng / 4;
            for (int i = leng - 1; i >= 0; i--)
            {
                Gact[s[i]]++;
                if (!validityCheck(Gact, limit))
                {
                    maxIndex = i + 1;
                    Gact[s[i]]--;
                    break;
                }
            }
            int minlength = 999999;
            for (int minIndex = -1; minIndex < leng - 1 && maxIndex < leng && minIndex < maxIndex; minIndex++)
            {
                while (!validityCheck(Gact, limit) && maxIndex < leng)
                {
                    Gact[s[maxIndex]]--;
                    maxIndex++;
                }
                int substringLength = Math.Max(0, maxIndex - minIndex - 1);
                if (substringLength < minlength)
                {
                    minlength = substringLength;
                }
                Gact[s[minIndex + 1]]++;
            }
            return minlength;

        }


        static Dictionary<char, int> CountCharElementsOfString(string gene)
        {
            Dictionary<char, int> Gact = new Dictionary<char, int>() { { 'G', 0 }, { 'A', 0 }, { 'C', 0 }, { 'T', 0 } };
            foreach (char c in gene)
            {
                Gact[c]++;
            }
            return Gact;
        }
        static Dictionary<char, int> GetUnnecessaryLettersOfstring(Dictionary<char, int> Gact, int limit)
        {
            Dictionary<char, int> unnecessaryLetters = new Dictionary<char, int>();
            foreach (var pair in Gact)
            {
                if (pair.Value >= limit)
                {
                    unnecessaryLetters[pair.Key] = pair.Value - limit;
                }
            }
            return unnecessaryLetters;
        }
        static int steadyGene(string gene)
        {
            int n = gene.Length;
            int limit = n / 4;
            var Gact = CountCharElementsOfString(gene);
            var unnecessaryLetters = GetUnnecessaryLettersOfstring(Gact, limit);
            int countOfUnnecessaryLetters = unnecessaryLetters.Values.Sum();
            if (countOfUnnecessaryLetters == 0) return 0;
            for (int j = 0, i = countOfUnnecessaryLetters; i <= n && j + i <= n;)
            {
                if (DoesSubGenContainUnnecessaryLetters(gene.Substring(j, i), unnecessaryLetters))
                    return gene.Substring(j, i).Length;
                j++;
                if (j + i > n)
                { i++; j = 0; }
            }
            return 0;
        }
        static bool DoesSubGenContainUnnecessaryLetters(string s, Dictionary<char, int> unnecessaryLetters)
        {
            int count = 0;
            int n = s.Length;
            int unnecessaryLettersSum = unnecessaryLetters.Values.Sum();
            foreach (var c in unnecessaryLetters.Keys)
            {
                count += n - s.Replace(c.ToString(), "").Length;
                if (count == unnecessaryLettersSum)
                    return true;
            }
            return false;
        }



        static string highestValuePalindrome(string s, int n, int k)
        {
            int count = 0;
            if (n == 1) return "9";
            Dictionary<int, int> differNumbers = new Dictionary<int, int>();
            for (int i = 0, j = n - 1; i < j; i++, j--)
            {
                if (s[i] != s[j])
                {
                    if (s[i] == '9' || s[j] == '9')
                        count++;
                    else count = count + 2;
                    if (count <= k)
                    {
                        differNumbers.Add(i, j);
                    }
                    else return "-1";
                }
            }
            var arrayOfString = s.ToArray();
            if (count == k)
            {
                foreach (var pair in differNumbers)
                {
                    arrayOfString[pair.Key] = '9';
                    arrayOfString[pair.Value] = '9';
                    count--;
                    k--;
                }
            }
            else if (k > 1)
            {
                for (int i = 0, j = n - 1; i < j; i++, j--)
                {
                    if (k != 0 && (arrayOfString[i] != '9' || arrayOfString[j] != '9'))
                    {
                        arrayOfString[i] = '9';
                        arrayOfString[j] = '9';
                        k = k - 2;
                    }
                }
            }
            else
            {
                arrayOfString[n / 2] = '9';
            }
            string charsStr = new string(arrayOfString);
            return charsStr;

        }
        static int sherlockAndAnagrams(string s)
        {
            int n = s.Length;
            int count = 0;
            List<string> subStringsOfstring = new List<string>();
            for (int i = 1; i <= n; i++)
            {
                for (int j = 0; j + i <= n; j++)
                {
                    subStringsOfstring.Add(s.Substring(j, i));
                }
            }

            for (int i = 0; i < subStringsOfstring.Count; i++)
            {

                for (int j = i + 1; j < subStringsOfstring.Count; j++)
                {
                    if (subStringsOfstring[i].Length == subStringsOfstring[j].Length)
                    {
                        if (areanagrams(subStringsOfstring[i], subStringsOfstring[j]) == 1)
                        { count++; }
                    }

                }
            }

            return count;
        }
        static int areanagrams(string s1, string s2)
        {
            int[] a1 = new int[26]; int[] a2 = new int[26];
            int n = s1.Length; int m = s2.Length;
            for (int i = 0; i < n; i++)
            {
                a1[s1[i] - 'a']++;
            }

            for (int i = 0; i < m; i++)
            {
                a2[s2[i] - 'a']++;
            }

            for (int i = 0; i < 26; i++)
            {
                if (a1[i] - a2[i] != 0)
                {
                    return 0;//two strings are not anagram
                }
            }
            return 1;
        }
    }
}

