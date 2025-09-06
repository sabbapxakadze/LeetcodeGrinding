public class Memoization
{
    //static void Main(string[] args)
    //{
    //    //Console.WriteLine(countConstruct(
    //    //    "aaaaaaaaaaaaaaaaaaaaaaaf",
    //    //    new string[] { "a", "aa", "aaaa" }
    //    //));

    //    var lists = allConstruct("aaaaaaaaaaaa", new string[] { "aa", "a", "aaaa", "aaaaa" });

    //    for (int i = 0; i < lists.Count; i++)
    //    {
    //        Console.WriteLine($"List {i + 1}: {string.Join(", ", lists[i])}");
    //    }

    //    Console.WriteLine("aaa");

    //}

    public static List<List<string>> allConstruct(string target, string[] wordBank, Dictionary<string, List<List<string>>>? memo = null)
    {
        memo ??= new Dictionary<string, List<List<string>>>();

        if (memo.ContainsKey(target)) return memo[target];
        if (string.IsNullOrEmpty(target)) return new List<List<string>> { new List<string>() };
        if (wordBank == null || wordBank.Length == 0) return null;

        List<List<string>> allList = new List<List<string>>();

        foreach (string word in wordBank)
        {
            if (target.StartsWith(word))
            {
                List<string> oneWay = new List<string> { word };
                string slicedWord = target.Substring(word.Length);

                foreach (List<string> list in allConstruct(slicedWord, wordBank, memo))
                {
                    if (list != null)
                    {
                        List<string> adder = new List<string>(oneWay);
                        adder.AddRange(list);
                        allList.Add(adder);
                    }
                }
            }
        }

        memo[target] = allList;
        return memo[target];
    }

    public static int countConstruct(string target, string[] wordBank, Dictionary<string, int>? memo = null)
    {
        memo ??= new Dictionary<string, int>();

        if (memo.ContainsKey(target)) return memo[target];
        if (string.IsNullOrEmpty(target)) return 1;
        if (wordBank == null || wordBank.Length == 0) return 0;

        int count = 0;

        foreach (string word in wordBank)
        {
            bool isValid = target.StartsWith(word) ? true : false;

            if (isValid)
            {
                string remainderString = target.Substring(word.Length);
                count += countConstruct(remainderString, wordBank, memo);
            }
        }

        memo[target] = count;
        return memo[target];
    }

    public static bool canConstruct(string target, string[] wordBank, Dictionary<string, bool>? memo = null)
    {
        memo ??= new Dictionary<string, bool>();

        if (memo.ContainsKey(target)) return memo[target];
        if (string.IsNullOrEmpty(target)) return true;
        if (wordBank == null || wordBank.Length == 0) return false;

        foreach (string word in wordBank)
        {
            bool isValid = target.StartsWith(word) ? true : false;

            if (isValid)
            {
                string remainderString = target.Substring(word.Length);
                if (canConstruct(remainderString, wordBank, memo))
                {
                    memo[target] = true;
                    return memo[target];
                }

            }
        }
        memo[target] = false;
        return memo[target];
    }

    public static List<int> bestSum(int target, int[] nums, Dictionary<int, List<int>>? memo = null)
    {
        memo ??= new Dictionary<int, List<int>>();

        if (memo.ContainsKey(target)) return memo[target];

        if (target == 0) return new List<int>();
        if (nums == null || nums.Length == 0) return null;
        if (target < 0) return null;



        List<int> shortestComb = null;


        foreach (int n in nums)
        {
            int remainder = target - n;
            if (remainder == 0) return new List<int> { n };

            List<int> remainderList = bestSum(remainder, nums, memo);

            if (remainderList != null)
            {
                var combination = new List<int>(remainderList) { n };


                if (shortestComb == null || shortestComb.Count > combination.Count)
                {
                    shortestComb = combination;
                }

            }
        }
        memo[target] = shortestComb;
        return shortestComb;

    }

    public static List<int> howSum(int target, int[] nums, Dictionary<int, List<int>>? memo = null)
    {
        memo ??= new Dictionary<int, List<int>>();

        if (memo.ContainsKey(target)) return memo[target];
        if (target == 0)
            return new List<int>();
        if (nums == null || nums.Length == 0)
            return null;
        if (target < 0)
        {
            memo[target] = null;
            return null;
        }

        foreach (int n in nums)
        {
            int remainder = target - n;
            List<int> remainderList = howSum(remainder, nums, memo);


            if (remainderList != null)
            {
                var x = new List<int>(remainderList);
                x.Add(n);

                memo[target] = x;
                return memo[target];
            }
        }
        memo[target] = null;
        return null;
    }

    public static bool canSum(int target, int[] nums, Dictionary<int, bool> memo = null)
    {
        memo ??= new Dictionary<int, bool>();


        if (memo.ContainsKey(target)) return memo[target];

        if (target == 0)
            return true;

        if (nums == null || nums.Length == 0)
            return false;

        if (target < 0) return false;

        foreach (int n in nums)
        {
            memo[n] = canSum(target - n, nums, memo);
            if (memo[n] == true)
                return true;
        }
        memo[target] = false;
        return false;
    }

    public static int Fib(int n, Dictionary<int, int> memo = null)
    {
        memo ??= new Dictionary<int, int>();

        if (memo.ContainsKey(n))
            return memo[n];
        if (n <= 2)
            return 1;

        memo[n] = Fib(n - 1, memo) + Fib(n - 2, memo);

        return memo[n];
    }


    public static int GridTraveller(int m, int n, Dictionary<(int, int), int> memo = null)
    {
        memo ??= new Dictionary<(int, int), int>();
        if (m <= 0 || n <= 0) return 0;
        if (n == 1 && m == 1)
            return 1;

        if (memo.ContainsKey((m, n)))
        {
            return memo[(m, n)];
        }

        memo[(m, n)] = GridTraveller(m - 1, n, memo) + GridTraveller(m, n - 1, memo);
        Console.WriteLine("hi");
        return memo[(m, n)];
    }
    public int Fib(int n)
    {
        if (n <= 0) return 0;
        if (n == 1) return 1;

        int[] arr = new int[n + 1];

        int i = 2;

        arr[0] = 0;
        arr[1] = 1;

        while (i <= arr.Length)
        {
            arr[i] = arr[i - 1] + arr[i - 2];
            i++;
        }
        return arr[n];
    }
}