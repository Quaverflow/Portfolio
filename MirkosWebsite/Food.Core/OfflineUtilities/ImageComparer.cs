using System.Collections.Generic;

namespace Food.Core
{
    public class ImageComparer : IComparer<string>
    {
        public static bool IsNumeric(string value)
        {
            return int.TryParse(value, out _);
        }


        public int Compare(string s1, string s2)
        {
            const int S1GreaterThanS2 = 1;
            const int S2GreaterThanS1 = -1;


            if(s1.Length > s2.Length)
            { return S1GreaterThanS2; }
            if(s2.Length > s1.Length)
            { return S2GreaterThanS1; }


            string left = "";
            string right = "";

            for (int i = 36; i < s1.Length - 4; i++)
            {
                left += s1[i];
            }
            for (int i = 36; i < s2.Length - 4; i++)
            {
                right += s2[i];
            }

            if(int.Parse(left) > int.Parse(right))
            { return S1GreaterThanS2; }
            if(int.Parse(right) > int.Parse(left))
            { return S2GreaterThanS1; }

            return 1;
        }
    }
}
