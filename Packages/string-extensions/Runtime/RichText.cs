using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace viettel.StringExtensions
{
    public static class RichText
    {
        public static string B(this string text)
        {
            return $"<b>{text}</b>";
        }
        public static string Bold(this string text)
        {
            return B(text);
        }
        public static string I(this string text)
        {
            return $"<i>{text}</i>";
        }
        public static string Italic(this string text)
        {
            return I(text);
        }
    }
}
