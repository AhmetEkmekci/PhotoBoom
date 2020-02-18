using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhotoBoom.Infrastructure
{
    public static class StringCollectionExtentions
    {
        public static List<string> StringToList(this string value)
        {
            return
                value
                    ?.Split(new char[] { ',', '|' }, StringSplitOptions.RemoveEmptyEntries)
                    ?.ToList()
                ?? new List<string>();
        }

        public static string ListToString(this List<string> value)
        {
            return
                (value?.Any() ?? false)
                    ? string.Join(",", value.Where(x => !string.IsNullOrEmpty(x)))
                    : string.Empty;
        }

        public static List<List<string>> StringToListD2(this string value)
        {
            return
                value
                    ?.Split(new char[] { '\\' }, StringSplitOptions.RemoveEmptyEntries)
                    ?.Select(x => x.StringToList())
                    ?.ToList()
                ?? new List<List<string>>();
        }

        public static string ListToStringD2(this List<List<string>> value)
        {
            return
                (value?.Any() ?? false)
                    ? string.Join("\\", value.Select(x => x.ListToString()).Where(x => !string.IsNullOrEmpty(x)))
                    : string.Empty;
        }

    }
}