namespace MarkerMetro.Unity.WinLegacy
{
    public static class Math
    {
        public static int DivRem(int a, int b, out int result)
        {
            int quotient = a / b;
            result = a - b * quotient;
            return quotient;
        }
    }
}
