public class Tabulation
{

    //static void Main(string[] args)
    //{
    //    Console.WriteLine(Fib(47772132));
    //}
    public static int Fib(int n)
    {
        if (n <= 0) return 0;
        if (n == 1) return 0;
        if (n == 2) return 1;

        int[] arr = new int[n];
        arr[0] = 0;
        arr[1] = 1;

        for (int i = 2; i < arr.Length; i++)
        {
            arr[i] = arr[i - 1] + arr[i - 2];
        }

        return arr[n - 1];
    }
}