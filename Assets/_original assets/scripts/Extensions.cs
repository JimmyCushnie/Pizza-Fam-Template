using UnityEngine;

namespace PizzaFam.Extensions
{
    public static class Extensions
    {
        public static T RandomItem<T>(this T[] array) => array[UnityEngine.Random.Range(0, array.Length)];

        public static bool IsNullOrEmpty(this string s) => string.IsNullOrEmpty(s);
    }
}
