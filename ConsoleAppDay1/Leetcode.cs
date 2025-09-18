using System.Collections;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

public class Leetcode
{
    public static int ClimbStairsMemo(int n, Dictionary<int, int>? memo = null)
    {
        memo ??= new Dictionary<int, int>();

        if (memo.ContainsKey(n))
            return memo[n];
        if (n == 1)
            return 1;
        if (n == 2)
            return 2;

        memo[n] = ClimbStairsMemo(n - 1, memo) + ClimbStairsMemo(n - 2, memo);
        return memo[n];

    }

    public int treeHeight(TreeNode root)
    {
        if (root == null)
            return 0;

        return 1 + Math.Max(treeHeight(root.left), treeHeight(root.right));
    }

    public bool isBalanced(TreeNode root)
    {
        if (root == null)
            return true;
        int min = Math.Min(treeHeight(root.left), treeHeight(root.right));
        int max = Math.Max(treeHeight(root.left), treeHeight(root.right));

        if (max - min <= 1)
            return false;

        return isBalanced(root.left) && isBalanced(root.right);
    }

    public int MySqrt(int x)
    {
        if (x == 0)
            return 0;
        int l = 1, r = x, answer = 0;
        while (l <= r)
        {
            int mid = l + (r - l) / 2;

            if ((long)mid * mid <= x)
            {
                answer = mid;
                l = mid + 1;
            }
            else
            {
                r = mid - 1;
            }
        }
        return answer;
    }

    public class ListNode
    {
        public int val;
        public ListNode next;
        public ListNode(int val = 0, ListNode next = null)
        {
            this.val = val;
            this.next = next;
        }
    }

    public ListNode DeleteDuplicates(ListNode head)
    {
        if (head == null)
            return null;
        if (head.next == null)
            return head;

        if (head.val == head.next.val)
        {
            ListNode n = head.next;
            return DeleteDuplicates(n);
        }

        ListNode a = head;
        a.next = DeleteDuplicates(head.next);
        return a;
    }

    public class TreeNode
    {
        public int val;
        public TreeNode left;
        public TreeNode right;
        public TreeNode(int val = 0, TreeNode left = null, TreeNode right = null)
        {
            this.val = val;
            this.left = left;
            this.right = right;
        }
    }

    public TreeNode SortedArrayToBST(int[] nums)
    {
        if (nums == null)
            return null;

        return BuildTree(nums, 0, nums.Length - 1);
    }

    public static TreeNode BuildTree(int[] nums, int l, int r)
    {
        if (l > r)
            return null;

        int mid = l + (r - l) / 2;

        TreeNode res = new TreeNode(nums[mid],
            (BuildTree(nums, l, mid - 1)), (BuildTree(nums, mid + 1, r)));
        return res;
    }

    public int MinDepth(TreeNode root)
    {
        if (root != null && root.left == null && root.right == null)
            return 1;
        if (root != null && (root.left == null || root.right == null))
            return 1 + Math.Max(MinDepth(root.left), MinDepth(root.right));
        if (root == null)
            return 0;


        return 1 + Math.Min(MinDepth(root.left), MinDepth(root.right));
    }

    public bool HasPathSum(TreeNode root, int targetSum)
    {
        if (root == null)
            return false;

        if (root.left == null && root.right == null)
            return root.val == targetSum;

        return HasPathSum(root.left, targetSum - root.val)
            || HasPathSum(root.right, targetSum - root.val);
    }

    public IList<IList<int>> Generate(int numRows)
    {
        var Lists = new List<IList<int>>();

        if (numRows < 0) return null;
        if (numRows == 0) return Lists;
        if (numRows == 1)
        {
            Lists.Add(new List<int> { 1 });
            return Lists;
        }

        Lists.Add(new List<int> { 1 });


        int i = 1;
        while (i < numRows)
        {
            int currSize = i + 1;
            int toFill = i - 1;
            int j = 1;
            List<int> newList = new List<int> { 1 };
            while (toFill != 0)
            {
                newList.Add(Lists[i - 1][j] + Lists[i - 1][j - 1]);
                j++;
                toFill--;
            }
            newList.Add(1);
            i++;
            Lists.Add(newList);
        }
        return Lists;
    }

    public bool IsValid(string s)
    {
        Stack<char> stack = new Stack<char>();

        foreach (char c in s)
        {
            if (c == '[' || c == '(' || c == '{')
                stack.Push(c);
            else
            {
                if (stack.Count == 0)
                    return false;

                char top = stack.Peek();

                if ((c == ')' && top == '(') ||
                    (c == ']' && top == '[') ||
                    (c == '}' && top == '{'))
                {
                    stack.Pop();
                }
                else
                    return false;
            }
        }
        return stack.Count == 0;
    }

    public IList<int> GetRow(int rowIndex)
    {
        var Lists = new List<IList<int>>();

        if (rowIndex < 0) return null;
        if (rowIndex == 0)
        {
            return new List<int> { 1 };
        }

        Lists.Add(new List<int> { 1 });


        int i = 1;
        while (i < rowIndex)
        {
            int currSize = i + 1;
            int toFill = i - 1;
            int j = 1;
            List<int> newList = new List<int> { 1 };
            while (toFill != 0)
            {
                newList.Add(Lists[i - 1][j] + Lists[i - 1][j - 1]);
                j++;
                toFill--;
            }
            newList.Add(1);
            i++;
            Lists.Add(newList);
        }
        return Lists[rowIndex];
    }

    public string AddBinary(string a, string b)
    {
        if (string.IsNullOrEmpty(a)) return b ?? "0";
        if (string.IsNullOrEmpty(b)) return a ?? "0";

        char carry = '0';

        int i = a.Length - 1;
        int j = b.Length - 1;

        string res = "";

        while (i >= 0 || j >= 0)
        {
            char x = i >= 0 ? a[i] : '0';
            char y = j >= 0 ? b[j] : '0';

            string iterationRes = BinaryAddChars(x, y, carry);
            carry = iterationRes[0];
            res = iterationRes[1] + res;
            i--; j--;
        }


        if (carry == '1')
        {
            res = '1' + res;
        }

        res = res.TrimStart('0');
        return res == "" ? "0" : res;
    }


    public string BinaryAddChars(char a, char b, char c = '0')
    {
        if (c == '1')
        {
            if (a == '1' && b == '1')
                return "11";
            else if (a == '1' || b == '1')
                return "10";
            else
                return "01";
        }
        else
        {
            if (a == '1' && b == '1')
                return "10";
            else if (a == '1' || b == '1')
                return "01";
            else
                return "00";
        }
    }

    public int FindLostNum(int[] arr)
    {
        if (arr == null || arr.Length == 0)
            return 0;

        int sumArr = 0;
        int sumN = 0;
        int cnt = 1;
        foreach (int i in arr)
        {
            sumArr += i;
            sumN += cnt;
            cnt++;
        }
        sumN += cnt;
        return sumN - sumArr;
    }

    public int SingleNumber(int[] nums)
    {
        Dictionary<int, int> d = new Dictionary<int, int>();
        foreach (int i in nums)
        {
            if (d.ContainsKey(i))
                d[i] = d[i] + 1;
            else
                d[i] = 0;
        }

        var res = d.FirstOrDefault(x => x.Value == 0);
        return res.Key;
    }

    public bool HasCycle(ListNode head, HashSet<ListNode>? set = null)
    {
        set ??= new HashSet<ListNode>();

        if (head == null)
            return false;
        if (set.Contains(head))
            return true;

        set.Add(head);

        return HasCycle(head.next, set);
    }
    public bool HasCycle(ListNode head)
    {
        return HasCycle(head, null);
    }

    public ListNode GetIntersectionNode(ListNode headA, ListNode headB, HashSet<ListNode>? set = null)
    {
        set ??= new HashSet<ListNode>();

        while (headA != null)
        {
            set.Add(headA);
            headA = headA.next;
        }

        while (headB != null)
        {
            if (set.Contains(headB))
                return headB;
            headB = headB.next;
        }
        return null;
    }
    public ListNode GetIntersectionNode(ListNode headA, ListNode headB)
    {
        return GetIntersectionNode(headA, headB, null);
    }

    public int ReverseBits(int n)
    {
        uint result = 0;
        uint x = (uint)n;

        for (int i = 0; i < 32; i++)
        {
            result <<= 1;
            result |= (x & 1);
            x >>= 1;
        }
        return (int)result;
    }

    public int HammingWeight(int n)
    {
        int c = 0;
        for (int i = 0; i < 32; i++)
        {
            c = (n & 1) == 1 ? ++c : c;
            n >>= 1;
        }
        return c;
    }

    public bool IsHappy(int n, HashSet<int>? set = null)
    {
        set ??= new HashSet<int>();

        if (set.Contains(n))
            return false;
        if (n == 1)
            return true;

        int newNum = 0;
        set.Add(n);
        while (n != 0)
        {
            int rem = n % 10;
            newNum += rem * rem;
            n /= 10;
        }
        return IsHappy(newNum, set);
    }

    public ListNode RemoveElements(ListNode head, int val)
    {
        if (head == null)
            return null;

        if (head.val == val)
            return RemoveElements(head.next, val);

        head.next = RemoveElements(head.next, val);
        return head;
    }

    public bool IsIsomorphic(string s, string t)
    {
        if (s.Length != t.Length)
            return false;

        var mapST = new Dictionary<char, char>();
        var mapTS = new Dictionary<char, char>();

        for (int i = 0; i < s.Length; i++)
        {
            char cs = s[i];
            char ct = t[i];

            if (mapST.ContainsKey(cs) && mapST[cs] != ct)
                return false;
            if (mapTS.ContainsKey(ct) && mapTS[ct] != cs)
                return false;

            mapST[cs] = ct;
            mapTS[ct] = cs;
        }
        return true;
    }

    public bool ContainsDuplicate(int[] nums)
    {
        if (nums == null)
            return false;

        HashSet<int> set = new HashSet<int>();

        foreach (int i in nums)
        {
            if (set.Contains(i))
                return true;
            set.Add(i);
        }

        return false;
    }

    public bool ContainsNearbyDuplicate(int[] nums, int k)
    {
        if (nums == null)
            return false;
        if (k <= 0)
            return false;

        Dictionary<int, int> d = new Dictionary<int, int>();
        int c = 0;

        foreach (int i in nums)
        {
            if (d.ContainsKey(i))
            {
                if (c - d[i] <= k)
                    return true;
            }
            d[i] = c;
            c++;
        }
        return false;
    }

    public int CountNodes(TreeNode root)
    {
        if (root == null)
            return 0;

        int leftH = GetHeight(root.left);
        int rightH = GetHeight(root.right);

        if (leftH == rightH)
        {
            return (1 << leftH) + CountNodes(root.right);
        }
        else
        {
            return (1 << rightH) + CountNodes(root.left);
        }
    }
    public int GetHeight(TreeNode root)
    {
        if (root == null)
            return 0;
        int h = 0;
        while (root != null)
        {
            h++;
            root = root.left;
        }
        return h;
    }

    public class MyStack
    {

        Queue<int> q;

        public MyStack()
        {
            q = new Queue<int>();
        }

        public void Push(int x)
        {
            q.Enqueue(x);
        }

        public int Pop()
        {
            Queue<int> newQ = new Queue<int>();
            while (q.Count != 1)
            {
                newQ.Enqueue(q.Dequeue());
            }
            int res = q.Dequeue();
            q = newQ;
            return res;
        }

        public int Top()
        {
            Queue<int> newQ = new Queue<int>();
            while (q.Count != 1)
            {
                newQ.Enqueue(q.Dequeue());
            }
            int res = q.Peek();
            newQ.Enqueue(q.Dequeue());
            q = newQ;
            return res;
        }

        public bool Empty()
        {
            return q.Count == 0;
        }
    }

    public TreeNode InvertTree(TreeNode root)
    {
        if (root == null)
            return null;
        return new TreeNode(root.val, InvertTree(root.right), InvertTree(root.left));
    }

    public IList<string> SummaryRanges(int[] nums)
    {
        if (nums.Length <= 0)
            return new List<string>();
        if (nums.Length == 1)
            return new List<string> { ToRange(nums[0]) };

        int prev = nums[0];
        int a = nums[0];

        List<string> l = new List<string>();


        for (int i = 1; i < nums.Length; i++)
        {
            int b = nums[i];
            if (b - prev == 1)
            {
                prev = nums[i];
                if (i == nums.Length - 1)
                {
                    l.Add(ToRange(a, prev));
                }
            }
            else
            {
                l.Add(ToRange(a, prev));
                a = b;
                prev = b;
                if (i == nums.Length - 1)
                {
                    l.Add(ToRange(b));
                }
            }
        }
        return l;
    }
    public string ToRange(int a, int? b = null)
    {
        if (b == null || a == b)
            return $"{a}";
        return $"{a}->{b}";
    }

    public bool IsPowerOfTwo(int n)
    {
        if (n < 0)
            return false;

        int cnt = 0;
        for (int i = 0; i < 32; i++)
        {
            if ((n & 1) == 1)
                cnt++;
            n >>= 1;
        }
        return cnt == 1;
    }

    public class MyQueue
    {

        Stack<int> s;

        public MyQueue()
        {
            s = new Stack<int>();
        }

        public void Push(int x)
        {
            Stack<int> newS = new Stack<int>();


            while (s.Count != 0)
            {
                newS.Push(s.Pop());
            }

            s.Push(x);

            while (newS.Count != 0)
            {
                s.Push(newS.Pop());
            }
        }

        public int Pop()
        {
            if (s.Count == 0)
                throw new InvalidOperationException("Queue is empty");

            return s.Pop();
        }

        public int Peek()
        {
            if (s.Count == 0)
                throw new InvalidOperationException("Queue is empty");

            return s.Peek();
        }

        public bool Empty()
        {
            return s.Count == 0;
        }
    }

    public bool IsPalindrome(ListNode head)
    {
        if (head == null)
            return true;
        if (head.next == null)
            return true;

        List<int> l = new List<int>();
        while (head != null)
        {
            l.Add(head.val);
            head = head.next;
        }

        int i = 0, j = l.Count - 1;
        while (i <= j)
        {
            if (l[i] != l[j])
                return false;
            i++; j--;
        }
        return true;
    }

    public bool IsAnagram(string s, string t)
    {
        if (s == null && t == null)
            return true;
        if (s == null || t == null)
            return false;
        if (s.Length != t.Length)
            return false;

        List<char> set = [.. s];

        foreach (char c in t)
        {
            if (set.Contains(c))
            {
                set.Remove(c);
            }
        }
        return set.Count == 0;
    }

    public IList<string> BinaryTreePaths(TreeNode root)
    {
        if (root == null)
            return new List<string>();

        if (root.left == null && root.right == null)
        {
            return new List<string> { $"{root.val}" };
        }

        List<string> l = new List<string>();


        if (root.left != null)
        {
            IList<string> l1 = BinaryTreePaths(root.left);

            foreach (string x in l1)
            {
                l.Add($"{root.val}->{x}");
            }
        }
        if (root.right != null)
        {
            IList<string> l1 = BinaryTreePaths(root.right);

            foreach (string x in l1)
            {
                l.Add($"{root.val}->{x}");
            }
        }
        return l;
    }

    public int AddDigits(int num)
    {
        if (num < 10)
            return num;

        return AddDigits(num % 10 + AddDigits(num / 10));
    }

    public bool IsUgly(int n)
    {
        if (n == 1)
            return true;
        if (n <= 0)
            return false;
        while (n > 5)
        {
            if (n % 2 == 0)
                n /= 2;
            else if (n % 3 == 0)
                n /= 3;
            else if (n % 5 == 0)
                n /= 5;
            else
                return false;
        }
        return n <= 5;
    }

    public bool IsPrime(int n)
    {
        if (n <= 1)
            return false;
        if (n == 2)
            return true;
        if (n % 2 == 0)
            return false;

        for (int i = 3; i * i <= n; i += 2)
        {
            if (n % i == 0)
                return false;
        }
        return true;
    }

    public int MissingNumber(int[] nums)
    {
        int cnt = nums.Length;
        int sum = 0;

        foreach (int i in nums)
        {
            sum += i;
        }

        int r = 0;
        while (cnt >= 0)
        {
            r += cnt;
            cnt--;
        }
        return r - sum;
    }

    bool IsBadVersion(int n)
    {
        return false;
    }

    public int FirstBadVersion(int n)
    {
        if (n <= 0)
            return 0;

        int i = 1, j = n;

        while (i < j)
        {
            int mid = i + (j - i) / 2;
            if (!IsBadVersion(mid))
            {
                i = mid + 1;
            }
            else
            {
                j = mid;
            }
        }
        return i;
    }

    public void MoveZeroes(int[] nums)
    {
        if (nums == null || nums.Length <= 1)
            return;

        int zeroCount = nums[0] == 0 ? 1 : 0;

        for (int i = 1; i < nums.Length; i++)
        {
            if (nums[i] == 0)
            {
                zeroCount++;
            }
            else
            {
                if (zeroCount != 0)
                {
                    nums[i - zeroCount] = nums[i];
                    nums[i] = 0;
                }
            }
        }
    }

    public bool WordPattern(string pattern, string s)
    {
        if (string.IsNullOrEmpty(pattern) || string.IsNullOrEmpty(s))
            return false;

        Dictionary<char, string> d1 = new Dictionary<char, string>();
        Dictionary<string, char> d2 = new Dictionary<string, char>();

        string[] strArray = s.Split(" ");
        if (pattern.Length != strArray.Length)
            return false;

        for (int i = 0; i < pattern.Length; i++)
        {
            if (!d1.ContainsKey(pattern[i]))
                d1[pattern[i]] = strArray[i];
            else if (d1[pattern[i]] != strArray[i])
                return false;

            if (!d2.ContainsKey(strArray[i]))
                d2[strArray[i]] = pattern[i];
            else if (d2[strArray[i]] != pattern[i])
                return false;
        }
        return d1.Count == d2.Count;
    }

    public ListNode AddTwoNumbers(ListNode l1, ListNode l2, int remainder)
    {
        if (l1 == null && l2 == null)
            if (remainder == 0)
                return null;
            else return new ListNode(remainder);


        int x = l1 == null ? 0 : l1.val;
        int y = l2 == null ? 0 : l2.val;
        int val = (remainder + x + y) % 10;
        remainder = (remainder + x + y) / 10;

        ListNode n = new ListNode(val);
        n.next = AddTwoNumbers(l1?.next, l2?.next, remainder);

        return n;
    }

    public bool CanWinNim(int n, Dictionary<int, bool>? memo = null)
    {
        //    memo ??= new Dictionary<int, bool>();
        //    if (memo.ContainsKey(n))
        //        return memo[n];

        //    if (n <= 3)
        //        return true;

        //    if (n == 4)
        //        return false;

        //    bool result = !CanWinNim(n - 1, memo) ||
        //                  !CanWinNim(n - 2, memo) ||
        //                  !CanWinNim(n - 3, memo);

        //    memo[n] = result;
        //    return memo[n]; This is correct but on big inputs stack gets BOOMED.
        //                      So I tried Induction on this one :D
        //                      And so on it deduces that CanWinNim(n) = n % 4 != 0 :DDDDDD
        return n % 4 != 0;
    }

    public class NumArray
    {
        int[] nums;
        public NumArray(int[] nums)
        {
            this.nums = nums;
        }

        public int SumRange(int left, int right)
        {
            if (left < 0 || right >= nums.Length)
                return -1;

            if (left > right)
                return -1;

            int sum = 0;
            while (left <= right)
            {
                sum += nums[left];
                left++;
            }
            return sum;
        }
    }

    public bool IsPowerOfThree(int n)
    {
        if (n < 1)
            return false;
        if (n == 1)
            return true;
        int rem = n % 3;
        return rem == 0 && IsPowerOfThree(n / 3);
    }

    public int[] CountBits(int n)
    {
        if (n == 0)
            return new int[] { 0 };

        int[] arr = new int[n + 1];
        arr[0] = 0;
        for (int i = 1; i < n; i++)
        {
            int j = i;
            while (j != 0)
            {
                arr[i] += j & 1;
                j >>= 1;
            }
        }
        return arr;
    }
    public bool IsPowerOfFour(int n)
    {
        if (n < 1)
            return false;
        if (n == 1)
            return true;
        while (n != 0)
        {
            if (n % 4 != 0)
                return false;
            n /= 4;
        }
        return true;
    }

    public void ReverseString(char[] s)
    {
        if (s == null)
            return;
        if (s.Length == 0)
            return;

        for (int i = 0, j = s.Length - 1; i <= j; i++, j--)
        {
            char temp = s[i];
            s[i] = s[j];
            s[j] = temp;
        }
    }
    public int[] Intersection(int[] nums1, int[] nums2)
    {
        if (nums1 == null || nums2 == null)
            return null;

        Dictionary<int, bool> d = nums1.
            Distinct().
            ToDictionary(x => x, x => false);

        foreach (int i in nums2)
        {
            if (d.ContainsKey(i))
            {
                d[i] = true;
            }
        }
        return d.Where(x => x.Value == true).Select(x => x.Key).ToArray();
    }

    public int[] Intersect(int[] nums1, int[] nums2)
    {
        if (nums1 == null || nums2 == null)
            return null;

        Dictionary<int, int> d = new Dictionary<int, int>();
        List<int> l = new List<int>();

        foreach (int i in nums1)
        {
            if (d.ContainsKey(i))
                d[i]++;
            else
                d[i] = 1;
        }

        foreach (int i in nums2)
        {
            if (d.ContainsKey(i) && d[i] > 0)
            {
                l.Add(i);
                d[i]--;
            }
        }
        return l.ToArray();
    }

    public bool IsPerfectSquare(int num)
    {
        if (num <= 0)
            return false;

        long i = 0;
        while (i * i <= num)
        {
            if (i * i == num)
                return true;
            i++;
        }
        return false;
    }

    public bool CanConstruct(string ransomNote, string magazine)
    {
        if (string.IsNullOrEmpty(ransomNote) || string.IsNullOrEmpty(magazine))
            return false;

        Dictionary<char, int> d = new Dictionary<char, int>();
        foreach (char c in magazine)
        {
            if (d.ContainsKey(c))
                d[c]++;
            else
                d[c] = 1;
        }

        foreach (char c in ransomNote)
        {
            if (d.ContainsKey(c))
                d[c]--;
            else
                return false;
            if (d[c] < 0)
                return false;
        }
        return true;
    }

    public int FirstUniqChar(string s)
    {
        if (string.IsNullOrEmpty(s))
            return -1;

        Dictionary<char, int> d = new Dictionary<char, int>();
        foreach (char c in s)
        {
            if (d.ContainsKey(c))
                d[c]++;
            else
                d[c] = 1;
        }

        for (int i = 0; i < s.Length; i++)
        {
            if (d[s[i]] == 1)
                return i;
        }
        return -1;
    }

    public char FindTheDifference(string s, string t)
    {
        if (string.IsNullOrEmpty(s))
            return t[0];

        Dictionary<char, int> d = new Dictionary<char, int>();
        foreach (char c in t)
        {
            if (d.ContainsKey(c))
                d[c]++;
            else
                d[c] = 1;
        }
        foreach (char c in s)
        {
            d[c]--;
        }
        return d.FirstOrDefault(x => x.Value == 1).Key;
    }

    public IList<string> ReadBinaryWatch(int turnedOn)
    {
        List<string> res = new List<string>();

        for (int h = 0; h < 12; h++)
        {
            int hBits = BitCount(h);
            for (int m = 0; m < 60; m++)
            {
                int mBits = BitCount(m);
                if (hBits + mBits == turnedOn)
                {
                    if (m < 10)
                        res.Add($"{h}:0{m}");
                    else
                        res.Add($"{h}:{m}");
                }
            }
        }
        return res;
    }
    public int BitCount(int x)
    {
        int c = 0;
        while (x != 0)
        {
            c += x & 1;
            x >>= 1;
        }
        return c;
    }

    public string ToHex(int num)
    {
        if (num == 0)
            return "0";

        string hex = "0123456789abcdef";
        string res = "";
        uint n = (uint)num;

        while (n != 0)
        {
            char c = hex[(int)(n % 16)];
            n /= 16;
            res = c + res;
        }
        return res;
    }
    public string ConvertToTitle(int columnNumber)
    {
        if (columnNumber == 0)
            return "";

        string res = "";

        while (columnNumber > 0)
        {
            int x = columnNumber-- % 26;
            char c = (char)('A' + x);
            res = c + res;
            columnNumber /= 26;
        }
        return res;
    }
    public int TitleToNumber(string columnTitle)
    {
        if (string.IsNullOrEmpty(columnTitle))
            return 0;

        double c = 0;
        for (int i = 0; i < columnTitle.Length; i++)
        {
            int v = columnTitle[i] - 64;
            c += v * Math.Pow(26, columnTitle.Length - i - 1);
        }
        return (int)c;
    }
    public int LongestPalindrome(string s)
    {
        if (string.IsNullOrEmpty(s))
            return 0;
        if (s.Length == 1)
            return 1;

        Dictionary<char, int> d = new Dictionary<char, int>();
        foreach (char c in s)
        {
            if (d.ContainsKey(c))
                d[c]++;
            else
                d[c] = 1;
        }
        int res = 0;

        bool odd = false;
        foreach (var x in d)
        {
            if (d[x.Key] % 2 == 1)
            {
                odd = true;
                res += d[x.Key] - 1;
            }
            else
                res += d[x.Key];
        }
        return odd ? res + 1 : res;
    }

    public int ThirdMax(int[] nums)
    {
        if (nums == null || nums.Length == 0)
            return 0;

        if (nums.Length == 1)
            return nums[0];


        long first = long.MinValue;
        long second = first;
        long third = second;
        if (nums.Length == 2)
        {
            return Math.Max(nums[0], nums[1]);
        }

        foreach (int i in nums)
        {
            if (i == third || i == second || i == first)
                continue;

            if (i > first)
            {
                third = second;
                second = first;
                first = i;
            }
            else if (i >= second)
            {
                third = second;
                second = i;
            }
            else if (i >= third)
            {
                third = i;
            }
        }
        return third == long.MinValue ? (int)first : (int)third;
    }
    public string AddStrings(string num1, string num2)
    {
        if (num1 == "0")
            return num2;
        if (num2 == "0")
            return num1;

        int i = num1.Length - 1, j = num2.Length - 1;
        int reminder = 0;
        string res = "";
        while (i >= 0 || j >= 0)
        {
            int x = 0, y = 0;
            if (i >= 0)
            {
                x = num1[i--] - '0';
            }
            if (j >= 0)
            {
                y = num2[j--] - '0';
            }
            res = ((reminder + x + y) % 10) + res;
            reminder = (reminder + x + y) / 10;
        }
        return reminder == 0 ? res : reminder + res;
    }
    public int CountSegments(string s)
    {
        if (string.IsNullOrWhiteSpace(s))
            return 0;

        string fixedStr = FixSpaces(s);
        return fixedStr.Split(" ").Length;
    }
    public string FixSpaces(string s)
    {
        StringBuilder sb = new StringBuilder();
        bool wasSpace = false;
        foreach (char c in s.Trim())
        {
            if (c == ' ')
            {
                if (!wasSpace)
                {
                    sb.Append(c);
                    wasSpace = true;
                }
            }
            else
            {
                wasSpace = false;
                sb.Append(c);
            }
        }
        return sb.ToString();
    }
    public int ArrangeCoins(int n)
    {
        if (n <= 0)
            return 0;
        int rows = 0;
        int index = 1;
        while (n > 0)
        {
            n -= index++;
            if (n < 0)
                return rows;
            rows++;
        }
        return rows;
    }
    public IList<int> FindDisappearedNumbers(int[] nums)
    {
        if (nums.Length == 0 || nums == null)
            return new List<int>();

        HashSet<int> set = new HashSet<int>(nums);
        List<int> l = new List<int>();

        for (int i = 1; i <= nums.Length; i++)
        {
            if (!set.Contains(i))
                l.Add(i);
        }
        return l;
    }
    public int FindContentChildren(int[] g, int[] s)
    {
        if (s == null || s.Length == 0)
            return 0;
        int count = 0;
        List<int> children = g.OrderBy(x => x).ToList();
        List<int> snacks = s.OrderBy(x => x).ToList();

        int i = 0;
        int j = 0;

        while (i < children.Count && j < snacks.Count)
        {
            if (children[i] > snacks[j])
                j++;
            else
            {
                i++;
                j++;
                count++;
            }
        }
        return count;
    }
    public bool RepeatedSubstringPattern(string s)
    {
        if (string.IsNullOrEmpty(s))
            return false;
        if (s.Length == 1)
            return false;


        int len = 1;
        while (len < s.Length / 2)
        {
            StringBuilder sb = new StringBuilder();
            int count = s.Length / len;
            if (s.Length % len != 0)
            {
                len++;
                continue;
            }

            while (count != 0)
            {
                sb.Append(s.Substring(0, len));
                count--;
            }
            if (sb.ToString() == s)
                return true;
            len++;
        }
        return false;
    }
    public int HammingDistance(int x, int y)
    {
        int c = 0;
        for (int i = 0; i < 32; i++)
        {
            c = (x & 1) == (y & 1) ? c : ++c;
            x >>= 1;
            y >>= 1;
        }
        return c;
    }
    public int IslandPerimeter(int[][] grid)
    {
        if (grid == null)
            return 0;
        int res = 0;
        for (int i = 0; i < grid.Length; i++)
        {
            for (int j = 0; j < grid[i].Length; j++)
            {
                int toAdd = 0;
                if (grid[i][j] == 1)
                {
                    int minI = 0;
                    if (i != 0)
                        minI = grid[i - 1][j] == 1 ? 2 : 0;
                    int minJ = 0;
                    if (j != 0)
                        minJ = grid[i][j - 1] == 1 ? 2 : 0;
                    res += 4 - minI - minJ;
                }
            }
        }
        return res;
    }
    public int FindComplement(int num)
    {
        string res = "";
        while (num > 0)
        {
            int comp = (num & 1) ^ 1;
            res = comp.ToString() + res;
            num >>= 1;
        }
        double n = 0;
        for (int i = 0; i < res.Length; i++)
        {
            char lastBit = res[res.Length - 1 - i];
            n += Math.Pow(2, i) * (lastBit - '0');
        }
        return (int)n;
    }
    public string LicenseKeyFormatting(string s, int k)
    {
        string x = s.Replace("-", "").ToUpper();
        if (x.Length <= k)
            return x;
        int rem = x.Length % k;
        string res = "";
        int c = 0;
        if (rem != 0)
        {
            while (rem != 0)
            {
                res += x[c];
                c++;
                rem--;
            }
            res += "-";
        }
        for (int i = c; i < x.Length; i = i + k)
        {
            res += x.Substring(i, k);
            if (i != x.Length - k)
            {
                res += "-";
            }
        }
        return res;
    }
    public int FindMaxConsecutiveOnes(int[] nums)
    {
        if (nums == null || nums.Length == 0)
            return 0;

        int i = 0;
        int max = 0;
        int temp = 0;
        while (i < nums.Length)
        {
            if (nums[i] == 1)
            {
                temp++;
                if (temp > max)
                    max = temp;
            }
            else
                temp = 0;
            i++;
        }
        return max;
    }
    public int[] ConstructRectangle(int area)
    {
        if (area <= 0)
            return new int[] { };

        int i = (int)Math.Sqrt(area);

        while (area % i != 0)
        {
            i--;
        }
        return new int[] { area / i, i };
    }
    public int FindPoisonedDuration(int[] timeSeries, int duration)
    {
        if (timeSeries == null || timeSeries.Length == 0)
            return 0;
        if (duration == 0)
            return 0;

        int count = duration;

        for (int i = 1; i < timeSeries.Length; i++)
        {
            if (timeSeries[i] - timeSeries[i - 1] < duration)
                count +=
                timeSeries[i] - timeSeries[i - 1];
            else
                count += duration;
        }
        return count;
    }
    public int[] NextGreaterElement(int[] nums1, int[] nums2)
    {
        if (nums2 == null || nums1 == null)
            return null;

        Stack<int> s = new Stack<int>();
        Dictionary<int, int> nexts = new Dictionary<int, int>();
        foreach (int i in nums2)
        {
            while (s.Count != 0 && s.Peek() > i)
            {
                nexts[i] = s.Pop();
            }
            s.Push(i);
        }
        List<int> l = new List<int>();
        foreach (var item in nums1)
        {
            if (!nexts.ContainsKey(item))
                l.Add(-1);
            else
                l.Add(nexts[item]);
        }
        return l.ToArray();
    }
    public string[] FindWords(string[] words)
    {
        if (words == null || words.Length == 0)
            return null;
        string f = "QWERTYUIOPqwertyuiop",
            s = "ASDFGHJKLasdfghjkl",
            t = "ZXCVBNMzxcvbnm";
        HashSet<char> fRow = new HashSet<char>(f);
        HashSet<char> sRow = new HashSet<char>(s);
        HashSet<char> tRow = new HashSet<char>(t);

        List<string> strs = new List<string>();
        foreach (string w in words)
        {
            HashSet<int> i = new HashSet<int>();
            foreach (char c in w)
            {
                if (fRow.Contains(c))
                    i.Add(1);
                if (sRow.Contains(c))
                    i.Add(2);
                if (tRow.Contains(c))
                    i.Add(3);
            }
            if (i.Count == 1)
                strs.Add(w);
        }
        return strs.ToArray();
    }

    public int[] FindMode(TreeNode root)
    {
        if (root == null)
            return Array.Empty<int>();

        var memo = new Dictionary<int, int>();
        FillMemo(root, memo);
        int max = memo.Max(x => x.Value);
        return memo.Where(x => x.Value == max)
            .Select(x => x.Key).ToArray();
    }
    public static void FillMemo(TreeNode root, Dictionary<int, int>? memo = null)
    {
        memo ??= new Dictionary<int, int>();

        if (root == null)
            return;
        if (memo.ContainsKey(root.val))
            memo[root.val]++;
        else
            memo[root.val] = 1;
        FillMemo(root.left, memo);
        FillMemo(root.right, memo);
    }
    public string ConvertToBase7(int num)
    {
        if (num == 0)
            return "0";

        int cnt = 0;
        string res = "";
        int temp = num;
        if (temp < 0)
            temp = -temp;

        while (temp != 0)
        {
            int rem = temp % 7;
            temp /= 7;
            res = rem + res;
        }
        return num < 0 ? $"-{res}" : res;
    }
    public string[] FindRelativeRanks(int[] score)
    {
        if (score == null || score.Length == 0)
            return null;

        int n = score.Length;
        string[] result = new string[n];

        var sorted = score
            .Select((val, idx) => new { Value = val, Index = idx })
            .OrderByDescending(x => x.Value)
            .ToList();

        for (int i = 0; i < n; i++)
        {
            string rank;
            switch (i)
            {
                case 0: rank = "Gold Medal"; break;
                case 1: rank = "Silver Medal"; break;
                case 2: rank = "Bronze Medal"; break;
                default: rank = (i + 1).ToString(); break;
            }
            result[sorted[i].Index] = rank;
        }

        return result;
    }
    public bool CheckPerfectNumber(int num)
    {
        int sum = 0;
        int i = 1;
        while (i * i <= num)
        {
            if (num % i == 0)
                sum += i;
            if (i * i != num)
                sum += num / i;
        }
        return sum - num == num;
    }
    public bool DetectCapitalUse(string word)
    {
        if (string.IsNullOrEmpty(word))
            return false;

        int upperCount = 0;
        bool firstUpper = false;
        for (int i = 0; i < word.Length; i++)
        {
            if (char.IsUpper(word[i]))
            {
                upperCount++;
                firstUpper = i == 0 ? true : false;
            }
        }
        return (upperCount == 0) || (upperCount == word.Length)
            || (upperCount == 1 && firstUpper);
    }
    public int FindLUSlength(string a, string b)
    {
        if (a == b)
            return -1;
        return Math.Max(a.Length, b.Length);
    }
    public int GetMinimumDifference(TreeNode root)
    {
        var list = new List<int>();
        InOrder(root, list);

        list.Sort();
        int min = int.MaxValue;
        for (int i = 1; i < list.Count; i++)
        {
            int r = list[i] - list[i - 1];
            if (r < min)
                min = r;
        }
        return min;
    }
    public void InOrder(TreeNode root, List<int> l)
    {
        if (root == null)
            return;

        InOrder(root.left, l);
        l.Add(root.val);
        InOrder(root.right, l);
    }
    public string ReverseStr(string s, int k)
    {
        char[] c = s.ToCharArray();

        for (int st = 0; st < s.Length; st += 2 * k)
        {
            int i = st, j = Math.Min(c.Length - 1, st + k - 1);
            while (i < j)
            {
                char temp = c[i];
                c[i] = c[j];
                c[j] = temp;
                i++;
                j--;
            }
        }
        return new string(c);
    }
    public int DiameterOfBinaryTree(TreeNode root)
    {
        if (root == null)
            return 0;

        int max = 0;

        int DFS(TreeNode root)
        {
            if (root == null)
                return 0;
            int leftH = DFS(root.left);
            int rightH = DFS(root.right);
            max = Math.Max(max, leftH + rightH);
            return 1 + Math.Max(leftH, rightH);
        }
        DFS(root);
        return max;
    }
    public bool CheckRecord(string s)
    {
        //A absent P present L late 3 late consecutive A < 2
        if (string.IsNullOrEmpty(s))
            return true;
        int late = 0;
        int abs = 0;
        foreach (char c in s)
        {
            if (abs >= 2)
                return false;
            if (late == 3)
                return false;
            if (c != 'L')
                late = 0;
            else
                late++;
            if (c == 'A')
                abs++;
        }
        return true;
    }
    public string ReverseWords(string s)
    {
        if (string.IsNullOrEmpty(s))
            return "";

        string[] arr = s.Split(" ");

        string ReverseWordsI(string a)
        {
            char[] chs = a.ToCharArray();
            int i = 0, j = a.Length - 1;
            while (i <= j)
            {
                char temp = chs[i];
                chs[i] = chs[j];
                chs[j] = temp;
                i++; j--;
            }
            return new string(chs);
        }
        for (int i = 0; i < arr.Length; i++)
        {
            arr[i] = ReverseWordsI(arr[i]);
        }
        return string.Join(" ", arr);
    }
    public class Node
    {
        public int val;
        public IList<Node> children;
        public Node() { }
        public Node(int _val)
        {
            val = _val;
        }
        public Node(int _val, IList<Node> _children)
        {
            val = _val;
            children = _children;
        }
    }
    public int MaxDepth(Node root)
    {
        if (root == null)
            return 0;
        if (root.children == null || root.children.Count == 0)
            return 1;
        HashSet<int> s = new HashSet<int>();
        foreach (var item in root.children)
        {
            s.Add(MaxDepth(item));
        }
        return 1 + s.Max();
    }
    public int ArrayPairSum(int[] nums)
    {
        if (nums == null || nums.Length == 0)
            return 0;

        Array.Sort(nums);

        int res = 0;
        for (int i = 0; i < nums.Length; i += 2)
        {
            res += nums[i];
        }
        return res;
    }
    public int FindTilt(TreeNode root)
    {
        if (root == null)
            return 0;

        int TreeSum(TreeNode root, Dictionary<TreeNode, int>? memo = null)
        {
            memo ??= new Dictionary<TreeNode, int>();
            if (root == null)
                return 0;
            if (memo.ContainsKey(root))
                return memo[root];

            memo[root] = root.val + TreeSum(root.left, memo) + TreeSum(root.right, memo);
            return memo[root];
        }
        Dictionary<TreeNode, int> memo = new Dictionary<TreeNode, int>();
        int leftSum = TreeSum(root.left, memo);
        int rightSum = TreeSum(root.right, memo);
        int tilt = leftSum - rightSum;
        tilt = tilt < 0 ? -1 * (tilt) : tilt;

        return tilt + FindTilt(root.left) + FindTilt(root.right);
    }
    public int[][] MatrixReshape(int[][] mat, int r, int c)
    {
        if (mat == null)
            return null;
        if (r * c != mat.Length * mat[0].Length)
            return mat;

        int[][] arr = new int[r][];
        for (int i = 0; i < arr.Length; i++)
        {
            arr[i] = new int[c];
        }

        int r1 = 0;
        int c1 = 0;

        for (int i = 0; i < mat.Length; i++)
        {
            for (int j = 0; j < mat[0].Length; j++)
            {
                arr[r1][c1++] = mat[i][j];
                if (c1 == c)
                {
                    c1 = 0;
                    r1++;
                }
            }
        }
        return arr;
    }
    public bool IsSubtree(TreeNode root, TreeNode subRoot)
    {
        if (root == null)
            return false;

        bool SameTree(TreeNode n, TreeNode m)
        {
            if (n == null && m == null)
                return true;
            if (n == null || m == null)
                return false;
            if (n.val != m.val)
                return false;
            return n.val == m.val && SameTree(n.left, m.left) && SameTree(n.right, m.right);
        }

        if (SameTree(root, subRoot))
            return true;

        return IsSubtree(root.left, subRoot) ||
            IsSubtree(root.right, subRoot);
    }
    public int DistributeCandies(int[] candyType)
    {
        if (candyType == null || candyType.Length == 0)
            return 0;

        HashSet<int> set = new HashSet<int>(candyType);
        int size = candyType.Length / 2;
        if (set.Count > size)
            return size;
        else
            return set.Count;
    }
    public IList<int> Preorder(Node root)
    {
        if (root == null)
            return new List<int>();

        List<int> l = new List<int>();
        l.Add(root.val);

        foreach (var node in root.children)
        {
            l.AddRange(Preorder(node));
        }
        return l;
    }
    public IList<int> Postorder(Node root)
    {
        if (root == null)
            return new List<int>();

        List<int> l = new List<int>();
        ;
        foreach (var node in root.children)
        {
            l.AddRange(Postorder(node));
        }
        l.Add(root.val);

        return l;
    }
    public int FindLHS(int[] nums)
    {
        if (nums == null || nums.Length == 0)
            return 0;
        if (nums.Length == 1)
            return nums[0];

        Dictionary<int, int> d = new Dictionary<int, int>();
        foreach (int i in nums)
        {
            if (d.ContainsKey(i))
                d[i]++;
            else
                d[i] = 1;
        }

        int max = 0;
        foreach (var item in d)
        {
            if (d.ContainsKey(item.Key + 1))
                if (max < item.Value + d[item.Key + 1])
                    max = item.Value + d[item.Key + 1];
        }
        return max;
    }
    public int MaxCount(int m, int n, int[][] ops)
    {
        if (ops == null || ops.Length == 0)
            return 0;
        int minM = m, minN = n;

        foreach (var item in ops)
        {
            minM = Math.Min(item[0], minM);
            minN = Math.Min(item[1], minN);
        }
        return minM * minN;
    }
    public string[] FindRestaurant(string[] list1, string[] list2)
    {
        if (list1 == null || list2 == null)
            return Array.Empty<string>();

        Dictionary<string, int> d = new Dictionary<string, int>();
        for (int i = 0; i < list1.Length; i++)
        {
            d[list1[i]] = i;
        }
        Dictionary<string, int> m = new Dictionary<string, int>();
        int minSum = int.MaxValue;
        for (int i = 0; i < list2.Length; i++)
        {
            if (d.ContainsKey(list2[i]))
            {
                if (minSum > d[list2[i]] + i)
                {
                    minSum = d[list2[i]] + i;
                    m[list2[i]] = minSum;
                }
            }
        }
        return m.Where(x => x.Value == minSum).Select(x => x.Key).ToArray();
    }
    public TreeNode MergeTrees(TreeNode root1, TreeNode root2)
    {
        if (root1 == null && root2 == null)
            return null;
        if (root1 == null)
            return root2;
        if (root2 == null)
            return root1;

        TreeNode left = MergeTrees(root1.left, root2.left);
        TreeNode right = MergeTrees(root1.right, root2.right);
        TreeNode newTree = new TreeNode(root1.val + root2.val, left, right);

        return newTree;
    }
    public int MaximumProduct(int[] nums)
    {
        int max1 = int.MinValue, max2 = int.MinValue, max3 = int.MinValue;
        int min1 = int.MaxValue, min2 = int.MaxValue;

        foreach (int n in nums)
        {
            if (n > max1) { max3 = max2; max2 = max1; max1 = n; }
            else if (n > max2) { max3 = max2; max2 = n; }
            else if (n > max3) { max3 = n; }

            if (n < min1) { min2 = min1; min1 = n; }
            else if (n < min2) { min2 = n; }
        }

        return Math.Max(max1 * max2 * max3, max1 * min1 * min2);
    }
    public IList<double> AverageOfLevels(TreeNode root)
    {
        if (root == null)
            return new List<double>();

        void InitMemo(TreeNode root, int lvl = 0,
        Dictionary<int, List<double>>? memo = null)
        {
            if (root == null) return;
            memo ??= new Dictionary<int, List<double>>();

            if (!memo.ContainsKey(lvl))
                memo[lvl] = new List<double>();

            memo[lvl].Add(root.val);

            InitMemo(root.left, lvl + 1, memo);
            InitMemo(root.right, lvl + 1, memo);
        }

        Dictionary<int, List<double>> memo = new Dictionary<int, List<double>>();
        InitMemo(root, 1, memo);

        var averages = memo
                .OrderBy(kv => kv.Key)
                .Select(kv => kv.Value.Average())
                .ToList();

        return averages;
    }
    public int[] FindErrorNums(int[] nums)
    {
        if (nums == null || nums.Length == 0)
            return Array.Empty<int>();

        Dictionary<int, int> d = new Dictionary<int, int>();
        int numSum = 0;
        int sum = 0;
        foreach (int i in nums)
        {
            if (d.ContainsKey(i))
                d[i]++;
            else
                d[i] = 1;

            numSum += i;
        }
        int x = nums.Length;
        for (int i = 1; i <= x; i++)
        {
            sum += i;
        }
     
        int repeatedNum = d.Where(x => x.Value == 2).Select(x => x.Key).First();
        int lostNum = repeatedNum - (numSum - sum);
        return new int[] { repeatedNum, lostNum };
    }
    public bool FindTarget(TreeNode root, int k)
    {
        if (root == null)
            return false;

        Dictionary<int, int> d = new Dictionary<int, int>();

        void InitMemo(TreeNode root, Dictionary<int, int>? memo = null)
        {
            memo ??= new Dictionary<int, int>();
            if (root == null)
                return;
            if (memo.ContainsKey(root.val))
                memo[root.val]++;
            else
                memo[root.val] = 1;
            InitMemo(root.left, memo);
            InitMemo(root.right, memo);
        }

        InitMemo(root, d);
        foreach (var item in d)
        {
            if (k - item.Key == item.Key)
            {
                if (d[item.Key] != 1)
                    return true;
            }
            else if (d.ContainsKey(k - item.Key) && k - item.Key != item.Key)
                return true;
        }
        return false;
    }
    public bool JudgeCircle(string moves)
    {
        if (string.IsNullOrEmpty(moves))
            return false;

        int x = moves.Where(x => x == 'L').Count();
        int y = moves.Where(x => x == 'R').Count();
        int z = moves.Where(x => x == 'U').Count();
        int v = moves.Where(x => x == 'D').Count();

        return x == y && z == v;
    }
    public int[][] ImageSmoother(int[][] img)
    {
        if (img == null || img.Length == 0 || img[0].Length == 0)
            return Array.Empty<int[]>();

        int rows = img.Length;
        int cols = img[0].Length;

        int[][] arr = new int[rows][];
        for (int i = 0; i < rows; i++)
            arr[i] = new int[cols];

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                int sum = 0;
                int count = 0;

                for (int r = i - 1; r <= i + 1; r++)
                {
                    for (int c = j - 1; c <= j + 1; c++)
                    {
                        if (r >= 0 && r < rows && c >= 0 && c < cols)
                        {
                            sum += img[r][c];
                            count++;
                        }
                    }
                }
                arr[i][j] = sum / count; 
            }
        }
        return arr;
    }
    public int FindSecondMinimumValue(TreeNode root)
    {
        if (root == null)
            return -1;

        void InitSet(TreeNode root, HashSet<int> set)
        {
            if (root == null)
                return;
            
            set.Add(root.val);
            InitSet(root.left, set);
            InitSet(root.right, set);
        }
        HashSet<int> s = new HashSet<int>();
        InitSet(root, s);
        if (s.Count > 1)
        {
            int min = s.Min();
            s.Remove(min);
            return s.Min();
        }
        return -1;
    }
    public int FindLengthOfLCIS(int[] nums)
    {
        if (nums == null || nums.Length == 0)
            return 0;

        int max = 1;
        int current = 1;
        for (int i = 1; i < nums.Length; i++)
        {
            if (nums[i] > nums[i-1])
            {
                current++;
            }
            else
            {
                current = 1;
            }
            max = Math.Max(max, current);
        }
        return max;
    }
    public bool ValidPalindrome(string s)
    {
        bool Palindrome(string s, int i, int j)
        {
            if (string.IsNullOrEmpty(s))
                return true;

            while (i <= j)
            {
                if (s[i++] != s[j--])
                    return false;
            }
            return true;
        }

        if (Palindrome(s, 0 , s.Length - 1))
            return true;
        int i = 0, j = s.Length - 1;

        while (i <= j)
        {
            if (s[i] != s[j])
            {
                string first = s.Substring(0, i) + s.Substring(i + 1);
                string second = s.Substring(0, j) + s.Substring(j + 1);

                return Palindrome(first, 0, first.Length - 1) || Palindrome(second, 0, second.Length - 1);
            }
            i++; j--;
        }
        return false;
    }
    public int CalPoints(string[] operations)
    {
        List<int> l = new List<int>();
        int sum = 0;

        for (int i = 0; i < operations.Length; i++)
        {
            if (operations[i] == "+")
            {
                l.Add(l[l.Count - 2] + l[l.Count - 1]); 
            }
            else if (operations[i] == "C")
            {
                l.RemoveAt(l.Count - 1);
            }
            else if (operations[i] == "D")
            {
                l.Add(l[l.Count - 1] * 2);
            }
            else
            {
                l.Add(int.Parse(operations[i]));
            }
        }

        return l.Sum();
    }
    public bool HasAlternatingBits(int n)
    {
        if (n < 0)
            return false;

        int lastBit = n & 1;
        n >>= 1;
        while (n != 0)
        {
            int last = n & 1;
            n >>= 1;
            if (lastBit != last)
            {
                lastBit = last;
            }
            else
                return false;
        }
        return true;
    }
    public int CountBinarySubstrings(string s)
    {
        if (string.IsNullOrEmpty(s))
            return 0;

        char last = s[0];
        int cntZero = s[0] == '0' ? 1 : 0;
        int cntOne = s[0] == '1' ? 1 : 0;
        int cnt = 0;
        for (int i = 1; i < s.Length; i++)
        {
            if (last != s[i])
            {
                cnt += Math.Min(cntZero, cntOne);
                if (last == '0')
                {
                    cntOne = 1;
                }
                else
                {
                    cntZero = 1;
                }
                
            }
            else
            {
                cntZero += last == '0' ? 1 : 0;
                cntOne += last == '1' ? 1 : 0;
            }
            last = s[i];
        }
        return cnt + Math.Min(cntZero, cntOne);
    }
    public int FindShortestSubArray(int[] nums)
    {
        if (nums == null || nums.Length == 0)
            return 0;

        Dictionary<int, int> d = new Dictionary<int, int>();
        foreach (int x in nums)
        {
            if (d.ContainsKey(x))
                d[x]++;
            else
                d[x] = 1;
        }
        int max = d.OrderByDescending(x => x.Value).First().Key;

        int i = 0, j = nums.Length - 1;
        while (i <= j)
        {
            if (nums[i] != max)
                i++;
            if (nums[j] != max)
                j--;
            if (nums[i] == max && nums[j] == max)
                return j - i + 1;
        }
        return 0;
    }
    static void Main(string[] args)
    {
        Dictionary<int, int> d = new Dictionary<int, int>();
        Console.WriteLine();
    }
}