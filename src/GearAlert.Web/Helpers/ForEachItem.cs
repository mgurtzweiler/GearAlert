using System;
using System.Collections.Generic;
using System.Text;

namespace GearAlert.Web.Helpers {
    public class ForEachItem<T> {
        public T Item { get; set; }
        public bool First { get; set; }
        public bool Last { get; set; }
        public int Index { get; set; }
        public bool NewGroup { get; set; }
    }

    public static class ViewExtensions {
        
        public static void Shuffle<T>(this IList<T> list) {
            Random rng = new Random();
            int n = list.Count;
            while (n > 1) {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }


        public static string ToCommaSeperatedList(this IEnumerable<object> items, string itemTemplate) {
            StringBuilder sb = new StringBuilder();
            foreach (var thing in items.ToFor()) {
                sb.Append(string.Format(itemTemplate, thing.Item));
                if (!thing.Last)
                    sb.Append(", ");
            }
            return sb.ToString();
        }
        public static IEnumerable<ForEachItem<T>> ToFor<T>(this IEnumerable<T> items) {
            return ToFor(items, 0);
        }

        public static IEnumerable<ForEachItem<T>> ToFor<T>(this IEnumerable<T> items, int group) {
            var list = new List<ForEachItem<T>>();
            foreach (var item in items) {
                var fei = new ForEachItem<T> { First = (list.Count == 0), Item = item, Index = list.Count };
                fei.NewGroup = (group > 0 && (list.Count % group == group - 1));
                list.Add(fei);
            }
            if (list.Count > 0) list[list.Count - 1].Last = true;
            return list;
        }

        public static bool IsNullOrEmpty(this string str) {
            return string.IsNullOrEmpty(str);
        }

        public static string DefaultIfEmpty(this string str, string value) {
            if (str.IsNullOrEmpty())
                return value;
            return str;
        }

        //public static string LimitWithElipsesOnWordBoundary(this string str, int characterCount)  {

        //    if (characterCount < 5) return str.Limit(characterCount);       // Can’t do much with such a short limit

        //    if (str.Length <= characterCount - 3)
        //        return str;
        //    else  {
        //        int lastspace = str.Substring(0, characterCount - 3).LastIndexOf(' ');

        //        if (lastspace > 0 && lastspace > characterCount - 10) {

        //            return str.Substring(0, lastspace) + “…”;

        //        } else {

        //        // No suitable space was found

        //           return str.Substring(0, characterCount – 3) + “…”;

        //        }

        //    }
        //}

        public static string TrimWithEllipsis(this string str, int length) {
            return string.Concat(str.Substring(0, (str.Length > length) ? length : str.Length), "&hellip;");
        }
    }


}