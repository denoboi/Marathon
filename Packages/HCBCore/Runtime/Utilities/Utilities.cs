using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HCB.Utilities
{
    public static class HCBUtilities
    {
        public static void DestroyExtended(Object obj)
        {
            if (Application.isPlaying)
                Object.Destroy(obj);
            else Object.DestroyImmediate(obj);
        }

        public static T KeyByValue<T, W>(this Dictionary<T, W> dict, W val)
        {
            T key = default;
            foreach (KeyValuePair<T, W> pair in dict)
            {
                if (EqualityComparer<W>.Default.Equals(pair.Value, val))
                {
                    key = pair.Key;
                    break;
                }
            }
            return key;
        }

        public static bool IsNullOrDestroyed(object obj, out System.Type type)
        {

            if (obj == null)
            {
                type = null;
                return true;
            }

            System.Type objType = obj.GetType();

            type = objType;

            return ReferenceEquals(objType, type);
        }

        public static Bounds TransformBounds(this Transform self, Bounds bounds)
        {
            var center = self.TransformPoint(bounds.center) - self.position;
            var points = bounds.GetCorners();

            var result = new Bounds(center, Vector3.zero);
            foreach (var point in points)
                result.Encapsulate(self.TransformPoint(point - self.position));
            return result;
        }
        public static Bounds InverseTransformBounds(this Transform self, Bounds bounds)
        {
            var center = self.InverseTransformPoint(bounds.center);
            var points = bounds.GetCorners();

            var result = new Bounds(center, Vector3.zero);
            foreach (var point in points)
                result.Encapsulate(self.InverseTransformPoint(point));
            return result;
        }

        // bounds
        public static List<Vector3> GetCorners(this Bounds obj, bool includePosition = true)
        {
            var result = new List<Vector3>();
            for (int x = -1; x <= 1; x += 2)
                for (int y = -1; y <= 1; y += 2)
                    for (int z = -1; z <= 1; z += 2)
                        result.Add((includePosition ? obj.center : Vector3.zero) + (obj.size / 2).Times(new Vector3(x, y, z)));
            return result;
        }
        public static Vector3 Times(this Vector3 self, Vector3 other)
        {
            return new Vector3(self.x * other.x, self.y * other.y, self.z * other.z);
        }

        public static string ScoreShow(double Score)
        {
            string result;
            string[] ScoreNames = new string[] { "", "k", "M", "B", "T", "aa", "ab", "ac", "ad", "ae", "af", "ag", "ah", "ai", "aj", "ak", "al", "am", "an", "ao", "ap", "aq", "ar", "as", "at", "au", "av", "aw", "ax", "ay", "az", "ba", "bb", "bc", "bd", "be", "bf", "bg", "bh", "bi", "bj", "bk", "bl", "bm", "bn", "bo", "bp", "bq", "br", "bs", "bt", "bu", "bv", "bw", "bx", "by", "bz", };
            int i;

            for (i = 0; i < ScoreNames.Length; i++)
                if (Score < 900)
                    break;
                else Score = System.Math.Floor(Score / 100f) / 10f;

            if (Score == System.Math.Floor(Score))
                result = Score.ToString() + ScoreNames[i];
            else result = Score.ToString("F1") + ScoreNames[i];
            return result;
        }

        public static string ScoreShowF2(double Score)
        {
            string result;
            string[] ScoreNames = new string[] { "", "k", "M", "B", "T", "aa", "ab", "ac", "ad", "ae", "af", "ag", "ah", "ai", "aj", "ak", "al", "am", "an", "ao", "ap", "aq", "ar", "as", "at", "au", "av", "aw", "ax", "ay", "az", "ba", "bb", "bc", "bd", "be", "bf", "bg", "bh", "bi", "bj", "bk", "bl", "bm", "bn", "bo", "bp", "bq", "br", "bs", "bt", "bu", "bv", "bw", "bx", "by", "bz", };
            int i;

            for (i = 0; i < ScoreNames.Length; i++)
                if (Score < 900)
                    break;
                else Score /= 1000f;

            if (Score == System.Math.Floor(Score))
                result = Score.ToString() + ScoreNames[i];
            else result = Score.ToString("F2") + ScoreNames[i];
            return result;
        }

        public static float Normalize01(float currentValue, float maxValue)
        {
            return Mathf.Abs((currentValue - 0) / (maxValue - 0));
        }
    }


    public static class IListExtensions
    {
        /// <summary>
        /// Shuffles the element order of the specified list.
        /// </summary>
        public static void Shuffle<T>(this IList<T> ts)
        {
            var count = ts.Count;
            var last = count - 1;
            for (var i = 0; i < last; ++i)
            {
                var r = UnityEngine.Random.Range(i, count);
                var tmp = ts[i];
                ts[i] = ts[r];
                ts[r] = tmp;
            }
        }
    }
}
