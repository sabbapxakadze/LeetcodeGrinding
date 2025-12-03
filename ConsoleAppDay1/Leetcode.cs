using System;
using System.Collections;
using System.Diagnostics;
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
            if (nums[i] > nums[i - 1])
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

        if (Palindrome(s, 0, s.Length - 1))
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
        var max = d.OrderByDescending(x => x.Value).First();
        var degrees = d.Where(x => x.Value == max.Value);

        int min = nums.Length;

        foreach (var item in degrees)
        {
            int a = 0;
            int b = nums.Length - 1;

            while (a <= b)
            {
                if (nums[a] != item.Key)
                    a++;
                if (nums[b] != item.Key)
                    b--;
                if (nums[a] == item.Key && nums[b] == item.Key)
                {
                    int len = b - a + 1;
                    min = min > len ? len : min;
                    break;
                }
            }
        }
        return min;
    }
    class KthLargest
    {
        private int k;
        private PriorityQueue<int, int> minHeap;

        public KthLargest(int k, int[] nums)
        {
            this.k = k;
            minHeap = new PriorityQueue<int, int>();

            foreach (var num in nums)
            {
                Add(num);
            }
        }
        public int Add(int val)
        {
            minHeap.Enqueue(val, val);

            if (minHeap.Count > k)
            {
                minHeap.Dequeue();
            }

            return minHeap.Peek();
        }
    }
    public int Search(int[] nums, int target)
    {
        if (nums == null || nums.Length == 0)
            return -1;
        int i = 0, j = nums.Length - 1;

        while (i <= j)
        {
            int mid = i + (j - i) / 2;
            if (nums[mid] == target)
                return mid;
            else if (nums[mid] > target)
                j = mid - 1;
            else
                i = mid + 1;
        }
        return -1;
    }
    public class MyHashSet
    {
        List<int> set;
        public MyHashSet()
        {
            set = new List<int>();
        }

        public void Add(int key)
        {
            foreach (int i in set)
            {
                if (key == i)
                    return;
            }
            set.Add(key);
        }

        public void Remove(int key)
        {
            bool contains = false;
            foreach (int i in set)
            {
                if (key == i)
                {
                    contains = true;
                    break;
                }
            }
            if (contains)
                set.Remove(key);
        }

        public bool Contains(int key)
        {
            foreach (int i in set)
            {
                if (key == i)
                    return true;
            }
            return false;
        }
    }
    public class MyHashMap
    {
        List<int[]> map;
        public MyHashMap()
        {
            map = new List<int[]>();
        }

        public void Put(int key, int value)
        {
            foreach (var item in map)
            {
                if (item[0] == key)
                {
                    item[1] = value;
                    return;
                }
            }
            map.Add(new int[] { key, value });
        }

        public int Get(int key)
        {
            foreach (var item in map)
            {
                if (item[0] == key)
                    return item[1];
            }
            return -1;
        }

        public void Remove(int key)
        {
            bool exists = false;
            int[] refs = null;
            foreach (var item in map)
            {
                if (item[0] == key)
                {
                    refs = item;
                    exists = true;
                    break;
                }
            }
            if (exists)
                map.Remove(refs);
        }
    }
    public string ToLowerCase(string s)
    {
        if (string.IsNullOrEmpty(s))
            return s;
        string res = "";

        foreach (char c in s)
        {
            if (c >= 'A' && c <= 'Z')
                res += (char)(c + 32);
            else
                res += c;
        }
        return res;
    }
    public bool IsOneBitCharacter(int[] bits)
    {
        if (bits == null || bits.Length == 0)
            return false;
        if (bits.Length == 1)
            return bits[0] == 0;

        bool inTwo = bits[0] == 1;    // 1 1 1 0 false  1 1 0
        for (int i = 1; i < bits.Length - 1; i++)
        {
            if (!inTwo)
            {
                if (bits[i] == 1)
                {
                    inTwo = true;
                }
            }
            else
            {
                inTwo = false;
            }
        }
        return bits[bits.Length - 1] == 0 && !inTwo;
    }
    public IList<int> SelfDividingNumbers(int left, int right)
    {
        List<int> l = new List<int>();
        while (left <= right)
        {
            int lfi = left, rfi = right;
            while (lfi != 0)
            {
                if (lfi % 10 == 0 || left % (lfi % 10) != 0)
                    break;
                else
                {
                    lfi /= 10;
                }
            }
            if (lfi == 0)
                l.Add(left);
            left++;
        }
        return l;
    }
    public int[][] FloodFill(int[][] image, int sr, int sc, int color)
    {
        if (image == null || image.Length == 0)
            return image;

        int orig = image[sr][sc];

        if (orig == color)
            return image;

        void Paint(int r, int c)
        {
            if (r < 0 || c < 0 || r > image.Length - 1 || c > image[0].Length - 1)
                return;

            if (image[r][c] != orig)
                return;

            image[r][c] = color;

            Paint(r + 1, c);
            Paint(r - 1, c);
            Paint(r, c + 1);
            Paint(r, c - 1);
        }
        Paint(sr, sc);

        return image;
    }
    public char NextGreatestLetter(char[] letters, char target)
    {
        if (letters == null || letters.Length == 0)
            return '/';

        int i = 0, j = letters.Length - 1;

        while (i <= j)
        {
            int mid = i + (j - i) / 2;
            if (letters[mid] > target)
                j = mid - 1;
            else
                i = mid + 1;
        }

        return i < letters.Length ? letters[i] : letters[0];
    }
    public int MinCostClimbingStairs(int[] cost)
    {
        if (cost == null || cost.Length == 0)
            return 0;

        Array.Resize(ref cost, cost.Length + 2);

        for (int i = cost.Length - 3; i >= 0; i--)
        {
            cost[i] += Math.Min(cost[i + 1], cost[i + 2]);
        }
        return Math.Min(cost[0], cost[1]);
    }
    public int DominantIndex(int[] nums)
    {
        if (nums == null || nums.Length == 0)
            return -1;
        if (nums.Length == 1)
            return 0;

        int max = 0;
        int secMax = -1;
        for (int i = 0; i < nums.Length; i++)
        {
            if (nums[i] > nums[max])
            {
                secMax = max;
                max = i;
            }
            else if (secMax == -1 || nums[i] > nums[secMax])
            {
                secMax = i;
            }
        }
        return 2 * nums[secMax] <= nums[max] ? max : -1;
    }
    public string ShortestCompletingWord(string licensePlate, string[] words)
    {
        if (string.IsNullOrEmpty(licensePlate) || words == null || words.Length == 0)
            return null;

        Dictionary<char, int> need = new Dictionary<char, int>();
        foreach (char c in licensePlate.ToLower())
        {
            if (char.IsLetter(c))
            {
                if (!need.ContainsKey(c)) need[c] = 0;
                need[c]++;
            }
        }

        bool IsCompleting(string word)
        {
            var count = new Dictionary<char, int>();
            foreach (char c in word.ToLower())
            {
                if (!char.IsLetter(c)) continue;
                if (!count.ContainsKey(c)) count[c] = 0;
                count[c]++;
            }

            foreach (var kv in need)
                if (!count.ContainsKey(kv.Key) || count[kv.Key] < kv.Value)
                    return false;

            return true;
        }

        int min = int.MaxValue;
        string res = "";

        foreach (string w in words)
        {
            if (IsCompleting(w) && w.Length < min)
            {
                min = w.Length;
                res = w;
            }
        }
        return res;
    }
    public int CountPrimeSetBits(int left, int right)
    {
        bool IsPrime(int x)
        {
            if (x == 2)
                return true;
            if (x <= 1 || x % 2 == 0)
                return false;

            int i = 3;
            while (i * i <= x)
            {
                if (x % i == 0)
                    return false;
                i += 2;
            }
            return true;
        }
        int CountOneBits(int x)
        {
            int c = 0;
            while (x != 0)
            {
                if ((x & 1) == 1)
                    c++;
                x >>= 1;
            }
            return c;
        }

        int count = 0;

        while (left <= right)
        {
            int ones = CountOneBits(left);
            if (IsPrime(ones))
                count++;
            left++;
        }
        return count;
    }
    public bool IsToeplitzMatrix(int[][] matrix)
    {
        if (matrix == null || matrix.Length == 0 || matrix[0].Length == 0)
            return false;

        int rows = matrix.Length;
        int cols = matrix[0].Length;

        for (int col = 0; col < cols; col++)
        {
            int r = 0, c = col;
            int temp = matrix[r][c];
            while (r < rows && c < cols)
            {
                if (temp != matrix[r][c])
                    return false;
                r++; c++;
            }
        }
        for (int row = 1; row < rows; row++)
        {
            int c = 0, r = row;
            int temp = matrix[r][c];
            while (r < rows && c < cols)
            {
                if (temp != matrix[r][c])
                    return false;
                r++; c++;
            }
        }
        return true;
    }
    public int NumJewelsInStones(string jewels, string stones)
    {
        if (string.IsNullOrEmpty(jewels) || string.IsNullOrEmpty(stones))
            return 0;

        int count = 0;

        foreach (char c in jewels)
        {
            string x = stones;
            x = x.Replace(c.ToString(), "");
            count += stones.Length - x.Length;
        }
        return count;
    }
    public int MinDiffInBST(TreeNode root)
    {
        if (root == null)
            return 0;

        List<int> pre = new List<int>();

        void Preorder(TreeNode root, List<int> list)
        {
            if (root == null)
                return;
            if (root.left == null && root.right == null)
            {
                list.Add(root.val);
                return;
            }
            Preorder(root.left, list);
            list.Add(root.val);
            Preorder(root.right, list);
        }
        Preorder(root, pre);
        int min = int.MaxValue;
        for (int i = 1; i < pre.Count; i++)
        {
            int iter = pre[i] - pre[i - 1];
            if (min > iter)
                min = iter;
        }
        return min;
    }
    public bool RotateString(string s, string goal)
    {
        if (string.IsNullOrEmpty(s) || string.IsNullOrEmpty(goal))
            return false;

        int i = s.Length;
        while (i >= 1)
        {
            s = s.Substring(1) + s[0];
            if (s == goal)
                return true;
            i--;
        }
        return false;
    }
    public int UniqueMorseRepresentations(string[] words)
    {
        if (words == null || words.Length == 0)
            return 0;
        Dictionary<char, string> code = new Dictionary<char, string>(){
            {'a', ".-"},
            {'b', "-..."}, {'c', "-.-."}, {'d', "-.."},
            {'e', "."}, {'f', "..-."}, {'g', "--."}, {'h', "...."},
            {'i', ".."}, {'j', ".---"}, {'k', "-.-"}, {'l', ".-.."},
            {'m', "--"}, {'n', "-."}, {'o', "---"}, {'p', ".--."},
            {'q', "--.-"}, {'r', ".-."}, {'s', "..."}, {'t', "-"},
            {'u', "..-"}, {'v', "...-"}, {'w', ".--"}, {'x', "-..-"},
            {'y', "-.--"}, {'z', "--.."}, };
        HashSet<string> set = new HashSet<string>();
        foreach (string s in words)
        {
            string x = "";
            foreach (char c in s)
            {
                x += code[c];
            }
            set.Add(x);
        }
        return set.Count;
    }
    public int[] NumberOfLines(int[] widths, string s)
    {
        if (widths == null || string.IsNullOrEmpty(s))
            return Array.Empty<int>();

        int x = 100;
        int c = 0;

        for (int i = 0; i < s.Length; i++)
        {
            int index = s[i] - 'a';
            if (x < widths[index])
            {
                c++;
                x = 100;
            }
            x -= widths[index];
        }
        return new int[] { c, 100 - x };
    }
    public double LargestTriangleArea(int[][] points)
    {
        double maxArea = 0.0;
        int n = points.Length;

        for (int i = 0; i < n; i++)
        {
            for (int j = i + 1; j < n; j++)
            {
                for (int k = j + 1; k < n; k++)
                {
                    double area = Math.Abs(
                        points[i][0] * (points[j][1] - points[k][1]) +
                        points[j][0] * (points[k][1] - points[i][1]) +
                        points[k][0] * (points[i][1] - points[j][1])
                    ) / 2.0;
                    maxArea = Math.Max(maxArea, area);
                }
            }
        }
        return maxArea;
    }
    public string MostCommonWord(string paragraph, string[] banned)
    {
        if (string.IsNullOrEmpty(paragraph) || banned == null)
            return null;
        paragraph = paragraph.ToLower();

        char[] punct = { '!', '?', '\'', ',', ';', '.', '`', '-', '_' };
        foreach (var p in punct)
        {
            paragraph = paragraph.Replace(p.ToString(), " ");
        }
        var arr = paragraph.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
        Dictionary<string, int> d = new Dictionary<string, int>();
        HashSet<string> bans = new HashSet<string>(banned.Select(x => x.ToLower()));
        foreach (var item in arr)
        {
            if (bans.Contains(item))
                continue;

            if (!d.ContainsKey(item))
                d[item] = 0;
            else
                d[item]++;
        }
        int mostCommon = d.Max(x => x.Value);
        return d.Where(x => x.Value == mostCommon).First().Key;
    }

    public int[] ShortestToChar(string s, char c)
    {
        if (string.IsNullOrEmpty(s))
            return Array.Empty<int>();


        int[] res = new int[s.Length];
        int last_c = -1 * s.Length;

        for (int i = 0; i < res.Length; i++)
        {
            if (s[i] == c)
                last_c = i;
            res[i] = i - last_c;
        }
        for (int i = s.Length - 1; i >= 0; i--)
        {
            if (s[i] == c)
                last_c = i;
            else
                res[i] = Math.Min(res[i], Math.Abs(last_c - i));
        }
        return res;
    }
    public string ToGoatLatin(string sentence)
    {
        if (string.IsNullOrEmpty(sentence))
            return sentence;

        bool IsVowel(char c)
        {
            c = char.ToLower(c);
            switch (c)
            {
                case 'a':
                    return true;
                case 'e':
                    return true;
                case 'i':
                    return true;
                case 'o':
                    return true;
                case 'u':
                    return true;
                default:
                    return false;
            }
        }
        string ACount(int cnt)
        {
            string res = "";
            for (int i = 0; i < cnt; i++)
            {
                res += "a";
            }
            return res;
        }
        string res = "";
        string[] arr = sentence.Split(" ");
        int cnt = 1;
        for (int i = 0; i < arr.Length; i++)
        {
            if (IsVowel(arr[i][0]))
                res += arr[i] + "ma";
            else
                res += arr[i].Substring(1, arr[i].Length - 1) + arr[i][0] + "ma";
            res += ACount(cnt);
            cnt++;

            if (i != arr.Length - 1)
                res += " ";
        }
        return res;
    }
    public IList<IList<int>> LargeGroupPositions(string s)
    {
        if (string.IsNullOrEmpty(s))
            return null;

        IList<IList<int>> lists = new List<IList<int>>();
        char temp = s[0];
        int currStart = 0;
        for (int i = 1; i < s.Length; i++)
        {
            if (temp != s[i])
            {
                if (i - 1 - currStart >= 2)
                    lists.Add(new List<int> { currStart, i - 1 });
                currStart = i;
                temp = s[i];
            }
        }
        if (s.Length - currStart >= 3)
            lists.Add(new List<int> { currStart, s.Length - 1 });
        return lists;
    }
    public int[][] FlipAndInvertImage(int[][] image)
    {
        if (image == null || image.Length == 0)
            return Array.Empty<int[]>();

        int[][] arr = new int[image.Length][];
        for (int i = 0; i < arr.Length; i++)
        {
            arr[i] = new int[image[0].Length];
        }

        for (int i = 0; i < image.Length; i++)
        {
            for (int j = image[0].Length - 1; j >= 0; j--)
            {
                arr[i][arr[0].Length - j - 1] = 1 ^ image[i][j];
            }
        }
        return arr;
    }
    public bool IsRectangleOverlap(int[] rec1, int[] rec2)
    {
        if (rec1[2] <= rec2[0] || rec2[2] <= rec1[0])
            return false;

        if (rec1[3] <= rec2[1] || rec2[3] <= rec1[1])
            return false;

        return true;
    }
    public bool BackspaceCompare(string s, string t)
    {
        if (string.IsNullOrEmpty(s) || string.IsNullOrEmpty(t))
            return false;

        string n1 = "";
        string n2 = "";
        for (int i = 0; i < s.Length; i++)
        {
            if (s[i] == '#')
            {
                if (n1.Length > 0)
                {
                    n1 = n1.Substring(0, n1.Length - 1);
                }
            }
            else
            {
                n1 += s[i];
            }
        }
        for (int i = 0; i < s.Length; i++)
        {
            if (t[i] == '#')
            {
                if (n2.Length > 0)
                {
                    n2 = n2.Substring(0, n2.Length - 1);
                }
            }
            else
            {
                n2 += t[i];
            }
        }
        return n1 == n2;
    }
    public bool BuddyStrings(string s, string goal)
    {
        if (string.IsNullOrEmpty(s) || string.IsNullOrEmpty(goal))
            return false;

        if (s.Length != goal.Length)
            return false;

        List<char> difs = new List<char>();
        int temp = 0;
        if (s == goal)
        {
            var set = new HashSet<char>();
            foreach (var x in s)
            {
                if (set.Contains(x))
                    return true;
                set.Add(x);
            }
            return false;
        }
        for (int i = 0; i < s.Length; i++)
        {
            if (s[i] != goal[i])
            {
                difs.Add(s[i]);
                difs.Add(goal[i]);
            }
        }
        return difs.Count == 4 && difs[0] == difs[3]
            && difs[1] == difs[2];
    }
    public bool LemonadeChange(int[] bills)
    {
        if (bills == null || bills.Length == 0)
            return false;

        int count5 = 0, count10 = 0;

        foreach (int x in bills)
        {
            if (x == 5)
            {
                count5++;
            }
            else if (x == 10)
            {
                if (count5 <= 0)
                    return false;
                count10++;
                count5--;
            }
            else
            {
                if (count10 > 0 && count5 > 0)
                {
                    count10--;
                    count5--;
                }
                else if (count5 >= 3)
                {
                    count5 -= 3;
                }
                else
                {
                    return false;
                }
            }
        }
        return true;
    }
    public int[][] Transpose(int[][] matrix)
    {
        if (matrix == null)
            return Array.Empty<int[]>();

        int[][] res = new int[matrix[0].Length][];
        for (int i = 0; i < res.Length; i++)
        {
            res[i] = new int[matrix.Length];
        }

        for (int i = 0; i < res.Length; i++)
        {
            for (int j = 0; j < res[0].Length; j++)
            {
                res[i][j] = matrix[j][i];
            }
        }
        return res;
    }
    public int BinaryGap(int n)
    {
        if (n <= 0) return 0;

        int maxGap = 0;
        int cnt = 0;
        bool temp = false;

        while (n != 0)
        {
            if ((n & 1) == 1)
            {
                if (temp)
                {
                    maxGap = Math.Max(maxGap, cnt);
                }
                cnt = 1;
                temp = true;
            }
            else
            {
                if (temp)
                {
                    cnt++;
                }
            }
            n >>= 1;
        }
        return maxGap;
    }
    public ListNode MiddleNode(ListNode head)
    {
        if (head == null)
            return null;

        ListNode fast = head;
        ListNode slow = head;

        while (fast != null && fast.next != null)
        {
            slow = slow.next;
            fast = fast.next.next;
        }
        return slow;
    }
    public int ProjectionArea(int[][] grid)
    {
        if (grid == null)
            return 0;
        int cnt = 0;
        for (int i = 0; i < grid.Length; i++)
        {
            int maxC = 0;
            int maxR = 0;
            for (int j = 0; j < grid[i].Length; j++)
            {
                if (grid[i][j] > 0)
                    cnt++;

                maxC = Math.Max(grid[i][j], maxC);
                maxR = Math.Max(grid[j][i], maxR);
            }
            cnt += maxC + maxR;
        }
        return cnt;
    }
    public string[] UncommonFromSentences(string s1, string s2)
    {
        if (string.IsNullOrEmpty(s1) || string.IsNullOrEmpty(s2))
            return Array.Empty<string>();

        var first = s1.Split(" ");
        var second = s2.Split(" ");

        Dictionary<string, int> d1 = new Dictionary<string, int>();

        foreach (var item in first)
        {
            if (d1.ContainsKey(item))
                d1[item]++;
            else
                d1[item] = 1;
        }
        foreach (var item in second)
        {
            if (d1.ContainsKey(item))
                d1[item]++;
            else
                d1[item] = 1;
        }
        return d1.Where(x => x.Value == 1).Select(x => x.Key).ToArray();
    }
    public int[] FairCandySwap(int[] aliceSizes, int[] bobSizes)
    {
        if (aliceSizes == null || bobSizes == null)
            return Array.Empty<int>();

        int aliceSum = aliceSizes.Sum();
        int bobSum = bobSizes.Sum();

        HashSet<int> set = new HashSet<int>(bobSizes);
        int difference = (bobSum - aliceSum) / 2;
        foreach (int a in aliceSizes)
        {
            if (set.Contains(a + difference))
            {
                return new int[] { a, a + difference };
            }
        }

        return Array.Empty<int>();
    }
    public int SurfaceArea(int[][] grid)
    {
        int n = grid.Length;
        int m = grid[0].Length;
        int area = 0;

        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < m; j++)
            {
                int h = grid[i][j];
                if (h > 0)
                {
                    // top + bottom
                    area += 2;
                    // north
                    area += (i == 0) ? h : Math.Max(h - grid[i - 1][j], 0);
                    // south
                    area += (i == n - 1) ? h : Math.Max(h - grid[i + 1][j], 0);
                    // west
                    area += (j == 0) ? h : Math.Max(h - grid[i][j - 1], 0);
                    // east
                    area += (j == m - 1) ? h : Math.Max(h - grid[i][j + 1], 0);
                }
            }
        }
        return area;
    }
    public bool IsMonotonic(int[] nums)
    {
        if (nums == null || nums.Length == 0)
            return false;
        if (nums.Length == 1)
            return true;

        bool IsIncreasing(int[] arr)
        {
            for (int i = 1; i < arr.Length; i++)
            {
                if (arr[i] < arr[i - 1])
                    return false;
            }
            return true;
        }
        bool IsDecreasing(int[] arr)
        {
            for (int i = 1; i < arr.Length; i++)
            {
                if (arr[i] > arr[i - 1])
                    return false;
            }
            return true;
        }
        return IsIncreasing(nums) || IsDecreasing(nums);
    }
    public TreeNode IncreasingBST(TreeNode root)
    {
        if (root == null)
            return null;
        List<int> InOrder(TreeNode node)
        {
            if (node == null)
                return new List<int>();

            List<int> l = new List<int>();
            l.AddRange(InOrder(node.left));
            l.Add(node.val);
            l.AddRange(InOrder(node.right));

            return l;
        }
        List<int> list = InOrder(root);

        TreeNode res = new TreeNode(list[0]);
        TreeNode temp = res;

        for (int i = 1; i < list.Count; i++)
        {
            temp.right = new TreeNode(list[i]);
            temp = temp.right;
        }
        return res;
    }
    public int[] SortArrayByParity(int[] nums)
    {
        int[] res = new int[nums.Length];
        int even = 0, odd = res.Length - 1;
        foreach (int i in nums)
        {
            if (i % 2 == 0)
            {
                res[even] = i;
                even++;
            }
            else
            {
                res[odd] = i;
                odd--;
            }
        }
        return res;
    }
    public int SmallestRangeI(int[] nums, int k)
    {
        if (nums == null || nums.Length == 0)
            return 0;
        if (k < 0)
            return 0;

        Array.Sort(nums);
        k = k > 0 ? k : -k;
        int min = nums[0] + k;
        int max = nums[nums.Length - 1] - k;
        return Math.Max(max - min, 0);
    }
    public bool HasGroupsSizeX(int[] deck)
    {
        if (deck == null || deck.Length <= 1)
            return false;

        Dictionary<int, int> d = new Dictionary<int, int>();
        foreach (var i in deck)
        {
            if (d.ContainsKey(i))
                d[i]++;
            else
                d[i] = 1;
        }
        int GCD(int a, int b)
        {
            if (b == 0) return a;
            return GCD(b, a % b);
        }
        int gcd = 0;
        foreach (var item in d.Values)
        {
            gcd = GCD(gcd, item);
        }
        return gcd >= 2;
    }
    public string ReverseOnlyLetters(string s)
    {
        if (string.IsNullOrEmpty(s))
            return s;

        int i = 0, j = s.Length - 1;
        char[] arr = new char[s.Length];
        while (i <= j)
        {
            if (char.IsLetter(s[i]) && char.IsLetter(s[j]))
            {
                arr[i] = s[j];
                arr[j] = s[i];
                i++;
                j--;
                continue;
            }
            if (!char.IsLetter(s[i]) && !char.IsLetter(s[j]))
            {
                arr[i] = s[i];
                arr[j] = s[j];
                i++;
                j--;
            }
            else if (!char.IsLetter(s[i]))
            {
                arr[i] = s[i];
                i++;
            }
            else if (!char.IsLetter(s[j]))
            {
                arr[j] = s[j];
                j--;
            }
        }
        return new string(arr);
    }
    public int[] SortArrayByParityII(int[] nums)
    {
        if (nums == null || nums.Length == 0)
            return Array.Empty<int>();

        int[] res = new int[nums.Length];
        int even = 0, odd = 1;
        for (int i = 0; i < nums.Length; i++)
        {
            int temp = nums[i] % 2;
            if (temp == 0)
            {
                res[even] = nums[i];
                even += 2;
            }
            else
            {
                res[odd] = nums[i];
                odd += 2;
            }
        }
        return res;
    }
    public bool IsLongPressedName(string name, string typed)
    {
        if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(typed))
            return false;

        int i = 0;
        int j = 0;
        while (j < typed.Length)
        {
            if (i < name.Length && name[i] == typed[j])
            {
                i++;
                j++;
            }
            else if (j > 0 && typed[j] == typed[j - 1])
            {
                j++;
            }
            else
                return false;
        }
        return i == name.Length;
    }
    public int NumUniqueEmails(string[] emails)
    {
        if (emails == null || emails.Length == 0)
            return 0;

        HashSet<string> set = new HashSet<string>();

        foreach (string email in emails)
        {
            var items = email.Split("@");
            string l = "";
            foreach (char c in items[0])
            {
                if (c == '.')
                    continue;
                else if (c == '+')
                    break;
                else
                    l += c;
            }
            foreach (char c in items[1])
            {
                if (c == '.' || c == '+')
                    continue;
                else
                    l += c;
            }
            set.Add(l + "@" + items[1]);
        }
        return set.Count;
    }
    public int RangeSumBST(TreeNode root, int low, int high)
    {
        if (root == null || low > high)
            return 0;

        void PreOrderForRangeSum(TreeNode root, List<int> l)
        {
            if (root == null)
                return;

            if (root.val < low)
            {
                PreOrderForRangeSum(root.right, l);
                return;
            }

            if (root.val > high)
            {
                PreOrderForRangeSum(root.left, l);
                return;
            }

            PreOrderForRangeSum(root.left, l);
            l.Add(root.val);
            PreOrderForRangeSum(root.right, l);
        }
        List<int> l = new List<int>();
        PreOrderForRangeSum(root, l);

        return l.Sum();
    }
    public bool ValidMountainArray(int[] arr)
    {
        if (arr == null || arr.Length <= 1)
            return false;

        int max = int.MinValue;
        int indexMax = 0;
        for (int i = 0; i < arr.Length; i++)
        {
            if (arr[i] > max)
            {
                max = arr[i];
                indexMax = i;
            }

        }
        bool IsIncreasing(int[] arr, int i, int j)
        {
            while (i < j)
            {
                if (arr[i] >= arr[i + 1])
                    return false;
                i++;
            }
            return true;
        }
        bool IsDecreasing(int[] arr, int i, int j)
        {
            if (i == j)
                return false;
            while (i < j)
            {
                if (arr[i] <= arr[i + 1])
                    return false;
                i++;
            }
            return true;
        }

        return IsIncreasing(arr, 0, indexMax) && IsDecreasing(arr, indexMax, arr.Length - 1);
    }
    public int[] DiStringMatch(string s)
    {
        if (string.IsNullOrEmpty(s))
            return Array.Empty<int>();

        int[] res = new int[s.Length + 1];

        int min = 0, max = s.Length;

        int index = 0;
        foreach (char c in s)
        {
            if (c == 'D')
            {
                res[index] = max;
                max--;
            }
            else
            {

                res[index] = min;
                min++;
            }
        }
        res[index] = min;
        return res;
    }
    public int MinDeletionSize(string[] strs)
    {
        if (strs == null || strs.Length == 0)
            return 0;
        if (strs.Length == 1)
            return 0;

        bool IsAscending(string l)
        {
            if (l.Length == 1)
                return true;
            for (int i = 1; i < l.Length; i++)
            {
                if (l[i].CompareTo(l[i - 1]) < 0)
                    return false;
            }
            return true;
        }
        int res = 0;
        for (int i = 0; i < strs[0].Length; i++)
        {
            string temp = "";
            for (int j = 0; j < strs.Length; j++)
            {
                temp += strs[j][i];
            }
            if (!IsAscending(temp))
                res++;
        }
        return res;
    }
    public bool IsAlienSorted(string[] words, string order)
    {
        if (words == null || words.Length == 0)
            return false;
        if (string.IsNullOrEmpty(order))
            return false;

        Dictionary<char, int> map = new Dictionary<char, int>();
        for (int i = 0; i < order.Length; i++)
        {
            map[order[i]] = i;
        }

        bool IsAscending(string curr, string prev)
        {
            int len = Math.Min(curr.Length, prev.Length);

            for (int i = 0; i < len; i++)
            {
                if (map[curr[i]] != map[prev[i]])
                    return map[curr[i]] > map[prev[i]];
            }
            return curr.Length >= prev.Length;
        }
        for (int i = 1; i < words.Length; i++)
        {
            if (!IsAscending(words[i], words[i - 1]))
                return false;
        }
        return true;
    }
    public int RepeatedNTimes(int[] nums)
    {
        if (nums == null || nums.Length == 0)
            return 0;
        Dictionary<int, int> d = new Dictionary<int, int>();
        foreach (int x in nums)
        {
            if (!d.ContainsKey(x))
                d[x] = 1;
            else
                d[x]++;
        }
        return d.Where(x => x.Value == nums.Length / 2).First().Key;
    }
    // First Assignment
    public IList<IList<int>> Permute(int[] nums)
    {
        if (nums == null || nums.Length == 0)
            return null;

        List<IList<int>> Recursion(List<int> head, List<int> nums)
        {
            if (nums.Count == 0)
                return new List<IList<int>> { new List<int>(head) };

            List<IList<int>> ls = new List<IList<int>>();
            foreach (int i in nums)
            {
                List<int> l = new List<int>(head);
                l.Add(i);
                List<int> n = new List<int>(nums);
                n.Remove(i);
                ls.AddRange(Recursion(l, n));
            }
            return ls;
        }
        return Recursion(new List<int>(), nums.ToList());
    }
    public IList<IList<int>> Subsets(int[] nums)
    {
        if (nums == null || nums.Length == 0)
            return new List<IList<int>>() { new List<int>() };

        List<IList<int>> result = new List<IList<int>>();

        List<int> set = new List<int>();
        void Backtrack(int index)
        {
            if (index > nums.Length - 1)
            {
                result.Add(new List<int>(set));
                return;
            }

            set.Add(nums[index]);
            Backtrack(index + 1);

            set.RemoveAt(set.Count - 1);
            Backtrack(index + 1);
        }
        Backtrack(0);
        return result;
    }
    public IList<IList<int>> CombinationSum(int[] candidates, int target)
    {
        if (candidates == null || candidates.Length == 0)
            return new List<IList<int>>();

        List<IList<int>> res = new List<IList<int>>();
        List<int> current = new List<int>();
        void Backtrack(int targ, int index)
        {
            if (targ == 0)
            {
                res.Add(new List<int>(current));
            }
            for (int i = index; i < candidates.Length; i++)
            {
                if (candidates[i] > targ)
                    continue;

                current.Add(candidates[i]);
                Backtrack(targ - candidates[i], i);
                current.RemoveAt(current.Count - 1);
            }
        }
        Backtrack(target, 0);
        return res;
    }
    public IList<string> LetterCombinations(string digits)
    {
        if (string.IsNullOrEmpty(digits))
            return new List<string>();
        Dictionary<int, string> map = new Dictionary<int, string>()
        {
            { 2, "abc"},
            { 3, "def" },
            { 4, "ghi" },
            { 5, "jkl" },
            { 6, "mno" },
            { 7, "pqrs" },
            { 8, "tuv" },
            { 9, "wxyz" },
        };

        List<string> Recursion(string comb)
        {
            if (string.IsNullOrEmpty(comb))
                return new List<string>() { "" };

            int num = int.Parse(comb);

            if (num < 10 && num > 1)
            {
                List<string> l = new List<string>();
                foreach (char c in map[num])
                {
                    l.Add(c.ToString());
                }
                return l;
            }

            int firstDigit = int.Parse(comb.Substring(0, 1));
            List<string> lis = new List<string>();
            var rec = Recursion(comb.Substring(1));
            foreach (var x in map[firstDigit])
            {
                foreach (var y in rec)
                {
                    lis.Add(x + y.ToString());
                }
            }
            return lis;
        }
        return Recursion(digits);
    }
    public IList<string> GenerateParenthesis(int n)
    {
        if (n <= 0)
            return new List<string>();

        int open = 0;
        int closed = 0;

        List<string> res = new List<string>();

        void Recursion(string curr, int open, int closed)
        {
            if (curr.Length == 2 * n)
            {
                res.Add(curr);
                return;
            }
            if (open < n)
                Recursion(curr + "(", open + 1, closed);
            if (closed < open)
                Recursion(curr + ")", open, closed + 1);
        }
        Recursion("", 0, 0);
        return res;
    }
    public bool Exist(char[][] board, string word)
    {
        if (board == null || string.IsNullOrEmpty(word))
            return false;
        int row = board.Length;
        int col = board[0].Length;
        bool[,] visited = new bool[row, col];

        bool Recursion(int i, int j, int index)
        {
            if (index == word.Length)
                return true;

            if (i >= row || j >= col || i < 0 || j < 0)
                return false;
            if (visited[i, j] || board[i][j] != word[index])
                return false;

            visited[i, j] = true;
            bool res = Recursion(i + 1, j, index + 1)
                || Recursion(i - 1, j, index + 1)
                || Recursion(i, j + 1, index + 1)
                || Recursion(i, j - 1, index + 1);

            visited[i, j] = false;
            return res;
        }

        bool res = false;
        for (int i = 0; i < board.Length; i++)
        {
            for (int j = 0; j < board[0].Length; j++)
            {
                if (board[i][j] == word[0] && Recursion(i, j, 0))
                    return true;
            }
        }
        return res;
    }
    // End of 1st Assignment
    public bool IsUnivalTree(TreeNode root)
    {
        if (root == null)
            return true;
        bool Recursion(int val, TreeNode node)
        {
            if (node == null)
                return true;
            if (val != node.val)
                return false;
            return Recursion(val, node.left) && Recursion(val, node.right);
        }
        return Recursion(root.val, root);
    }
    public int LengthOfLongestSubstring(string s)
    {
        if (string.IsNullOrEmpty(s))
            return 0;
        if (s.Length == 1)
            return 1;

        int max = int.MinValue;
        HashSet<char> set = new HashSet<char>();
        int l = 0;
        for (int r = 0; r < s.Length; r++)
        {
            while (set.Contains(s[r]))
            {
                set.Remove(s[l]);
                l++;
            }
            max = Math.Max(max, r - l + 1);
            set.Add(s[r]);
        }
        return max;
    }
    public int LargestPerimeter(int[] nums)
    {
        Array.Sort(nums);
        if (nums.Length < 2)
            return 0;
        int max = 0;
        int a = nums[0], b = nums[1];

        for (int i = 2; i < nums.Length; i++)
        {
            if (a + b > nums[i] && a + nums[i] > b && b + nums[i] > a)
                max = a + b + nums[i];

            a = nums[i - 1];
            b = nums[i];
        }
        return max;
    }
    public int[] GetConcatenation(int[] nums)
    {
        if (nums == null || nums.Length == 0)
            return Array.Empty<int>();

        int[] res = new int[nums.Length * 2];
        for (int i = 0; i < res.Length; i++)
        {
            res[i] = nums[i % nums.Length];
        }
        return res;
    }
    public int[] TwoSum(int[] nums, int target)
    {
        if (nums == null || nums.Length == 0)
            return Array.Empty<int>();

        List<int> l = nums.ToList();
        foreach (var item in l)
        {
            if (l.Contains(target - item))
                if (item != target - item)
                    return new int[] { l.IndexOf(item), l.IndexOf(target - item) };
                else if (l.Where(x => x == item).Count() > 1)
                    return new int[] { l.IndexOf(item), l.LastIndexOf(item) };
        }
        return Array.Empty<int>();
    }
    public IList<IList<string>> GroupAnagrams(string[] strs)
    {
        if (strs == null || strs.Length == 0)
            return new List<IList<string>>() { new List<string>() };

        List<IList<string>> res = new List<IList<string>>();

        Dictionary<string, List<string>> d = new Dictionary<string, List<string>>();

        for (int i = 0; i < strs.Length; i++)
        {
            char[] arr = strs[i].ToCharArray();
            Array.Sort(arr);
            string temp = new string(arr);

            if (d.ContainsKey(temp))
            {
                d[temp].Add(strs[i]);
            }
            else
            {
                d[temp] = new List<string> { strs[i] };
            }
        }
        foreach (var item in d)
        {
            res.Add(item.Value);
        }
        return res;
    }
    public int[] SortArray(int[] nums)
    {
        int[] Divide(int[] arr)
        {
            if (arr.Length == 1)
                return arr;
            if (arr.Length == 2)
                return Sort(new int[] { arr[0] }, new int[] { arr[1] });

            int[] f = new int[(arr.Length + 1) / 2];
            int[] s = new int[arr.Length / 2];

            for (int i = 0; i < arr.Length; i++)
            {
                if (i >= f.Length)
                {
                    s[i - f.Length] = arr[i];
                }
                else
                {
                    f[i] = arr[i];
                }
            }
            return Sort(Divide(f), Divide(s));
        }

        int[] Sort(int[] arr1, int[] arr2)
        {
            int i = 0, j = 0;
            int[] res = new int[arr1.Length + arr2.Length];
            while (i < arr1.Length && j < arr2.Length)
            {
                if (arr1[i] > arr2[j])
                    res[i + j] = arr2[j++];
                else if (arr1[i] <= arr2[j])
                    res[i + j] = arr1[i++];
            }
            while (i < arr1.Length)
            {
                res[i + j] = arr1[i++];
            }
            while (j < arr2.Length)
            {
                res[i + j] = arr2[j++];
            }
            return res;
        }
        return Divide(nums);
    }
    public void SortColors(int[] nums)
    {
        int r = 0, g = 0, b = 0;
        for (int i = 0; i < nums.Length; i++)
        {
            if (nums[i] == 0)
                r++;
            else if (nums[i] == 1)
                g++;
            else
                b++;
        }
        for (int i = 0; i < nums.Length; i++)
        {
            if (r != 0)
            {
                nums[i] = 0;
                r--;
            }
            else if (g != 0)
            {
                nums[i] = 1;
                g--;
            }
            else
            {
                nums[i] = 2;
            }
        }
    }
    public int[] TopKFrequent(int[] nums, int k)
    {
        if (nums == null || k <= 0)
            return Array.Empty<int>();

        Dictionary<int, int> d = new Dictionary<int, int>();
        foreach (int i in nums)
        {
            if (!d.ContainsKey(i))
                d[i] = 1;
            else
                d[i]++;
        }
        if (k > d.Count)
            return Array.Empty<int>();
        return d.OrderByDescending(x => x.Value).Take(k).Select(x => x.Key).ToArray();
    }
    public class NumMatrix
    {
        int[][] pr;
        int sum;
        public NumMatrix(int[][] matrix)
        {
            int s = 0;
            pr = new int[matrix.Length][];
            for (int i = 0; i < matrix.Length; i++)
                pr[i] = new int[matrix[0].Length];

            for (int i = 0; i < matrix.Length; i++)
            {
                for (int j = 0; j < matrix[0].Length; j++)
                {
                    int top = i > 0 ? pr[i - 1][j] : 0;
                    int left = j > 0 ? pr[i][j - 1] : 0;
                    int diag = (i > 0 && j > 0) ? pr[i - 1][j - 1] : 0;

                    pr[i][j] = matrix[i][j] + top + left - diag;
                }
            }
        }
        public int SumRegion(int row1, int col1, int row2, int col2)
        {
            int total = pr[row2][col2];

            int above = row1 > 0 ? pr[row1 - 1][col2] : 0;
            int left = col1 > 0 ? pr[row2][col1 - 1] : 0;
            int overlap = (row1 > 0 && col1 > 0) ? pr[row1 - 1][col1 - 1] : 0;

            return total - above - left + overlap;
        }
    }
    public int[] ProductExceptSelf(int[] nums)
    {
        if (nums == null || nums.Length == 0)
            return Array.Empty<int>();
        int[] pr = new int[nums.Length];
        int[] sf = new int[nums.Length];
        for (int i = 0; i < nums.Length; i++)
        {
            if (i == 0)
                pr[0] = 1;
            else
            {
                pr[i] = pr[i - 1] * nums[i - 1];
            }
        }

        for (int i = nums.Length - 1; i >= 0; i--)
        {
            if (i == nums.Length - 1)
                sf[i] = 1;
            else
                sf[i] = pr[i + 1] * nums[i + 1];
        }
        int[] res = new int[nums.Length];
        for (int i = 0; i < nums.Length; i++)
        {
            res[i] = sf[i] * sf[i];
        }
        return res;
    }
    public string MergeAlternately(string word1, string word2)
    {
        if (string.IsNullOrEmpty(word1) || string.IsNullOrEmpty(word2))
            return "";
        int i = 0, j = 0;
        string res = "";
        while (i < word1.Length || j < word2.Length)
        {
            if (i < word1.Length)
                res += word1[i++];
            if (j < word2.Length)
                res += word2[j++];
        }
        return res;
    }
    public int CharacterReplacement(string s, int k)
    {
        if (string.IsNullOrEmpty(s))
            return 0;
        Dictionary<char, int> d = new Dictionary<char, int>();
        int i = 0, j = 0;
        int max = 0;
        while (i < s.Length && j < s.Length)
        {
            if (!d.ContainsKey(s[j]))
                d[s[j]] = 0;
            d[s[j]]++;
            int maxVal = d.Max(x => x.Value);
            int windowSize = j - i + 1;
            if (windowSize - maxVal > k)
            {
                d[s[i]]--;
                i++;
            }
            max = Math.Max(max, j - i + 1);
            j++;
        }
        return max;
    }
    public bool CheckInclusion(string s1, string s2)
    {
        if (string.IsNullOrEmpty(s1) || string.IsNullOrEmpty(s2))
            return false;

        Dictionary<char, int> firstMap = new Dictionary<char, int>();
        foreach (char c in s1)
        {
            if (!firstMap.ContainsKey(c))
                firstMap[c] = 0;
            firstMap[c]++;
        }

        Dictionary<char, int> windowMap = new Dictionary<char, int>();
        int i = 0, j = 0;
        while (i < s2.Length && j < s2.Length)
        {
            char c = s2[j];
            if (!firstMap.ContainsKey(c))
            {
                j++;
                i = j;
                windowMap = new Dictionary<char, int>();
                continue;
            }

            if (!windowMap.ContainsKey(c))
                windowMap[c] = 0;
            windowMap[c]++;

            if (j - i + 1 > s1.Length)
            {
                char leftCharacter = s2[i++];
                if (windowMap[leftCharacter] == 1)
                    windowMap.Remove(leftCharacter);
                else
                    windowMap[leftCharacter]--;
            }
            if (windowMap.Count == firstMap.Count)
            {
                bool tempRes = true;
                foreach (var k in windowMap.Keys)
                {
                    if (!firstMap.ContainsKey(k) || windowMap[k] != firstMap[k])
                    {
                        tempRes = false;
                        break;
                    }
                }
                if (tempRes)
                    return true;
            }

            j++;
        }
        return false;
    }
    public int MinSubArrayLen(int target, int[] nums)
    {
        if (nums == null || nums.Length == 0)
            return 0;

        int i = 0, j = 0;
        int min = nums.Length + 1;
        int tempTarg = target;
        while (i < nums.Length && j < nums.Length)
        {
            tempTarg -= nums[j];
            j++;
            while (tempTarg <= 0)
            {
                min = Math.Min(min, j - i);
                tempTarg += nums[i];
                i++;
            }
        }
        return min == nums.Length + 1 ? 0 : min;
    }
    public class MinStack
    {
        int[] stack;
        int[] minStack;
        int curr;
        public MinStack()
        {
            stack = new int[64];
            minStack = new int[64];
            curr = 0;
        }
        public void Push(int val)
        {
            if (curr == stack.Length)
            {
                Array.Resize(ref stack, stack.Length * 2);
                Array.Resize(ref minStack, minStack.Length * 2);
            }

            stack[curr] = val;
            if (curr == 0)
                minStack[curr] = val;
            else
                minStack[curr] = Math.Min(val, minStack[curr - 1]);
            curr++;
        }
        public void Pop()
        {
            if (curr == 0) return;
            curr--;
        }
        public int Top()
        {
            if (curr == 0) return 0;
            return stack[curr - 1];
        }
        public int GetMin()
        {
            if (curr == 0) return 0;
            return minStack[curr - 1];
        }
    }
    public int EvalRPN(string[] tokens)
    {
        if (tokens == null || tokens.Length == 0)
            return 0;

        int count = 0;
        Stack<string> st = new Stack<string>();
        for (int i = 0; i < tokens.Length; i++)
        {
            if (tokens[i] == "+" || tokens[i] == "-"
                || tokens[i] == "/" || tokens[i] == "*")
            {
                if (st.Count < 2)
                    return 0;
                int sec = int.Parse(st.Pop());
                int first = int.Parse(st.Pop());
                string res = "";
                if (tokens[i] == "+") res = (first + sec).ToString();
                else if (tokens[i] == "-") res = (first - sec).ToString();
                else if (tokens[i] == "/") res = (first / sec).ToString();
                else if (tokens[i] == "*") res = (first * sec).ToString();
                st.Push(res);
            }
            else
                st.Push(tokens[i]);
        }
        return int.Parse(st.Peek());
    }
    public int[] AsteroidCollision(int[] asteroids)
    {
        if (asteroids == null || asteroids.Length == 0)
            return Array.Empty<int>();

        Stack<int> st = new Stack<int>();
        int i = 0;
        while (i < asteroids.Length)
        {
            if (st.Count == 0)
                st.Push(asteroids[i++]);
            else
            {
                if (st.Peek() < 0 || asteroids[i] > 0)
                    st.Push(asteroids[i++]);
                else
                {
                    if (Math.Abs(asteroids[i]) > Math.Abs(st.Peek()))
                    {
                        st.Pop();
                    }
                    else if (asteroids[i] + st.Peek() == 0)
                    {
                        st.Pop();
                        i++;
                    }
                    else
                    {
                        i++;
                    }
                }
            }
        }
        int[] res = new int[st.Count];
        for (int j = res.Length - 1; j >= 0; j--)
        {
            res[j] = st.Pop();
        }
        return res;
    }
    public bool SearchMatrix(int[][] matrix, int target)
    {
        if (matrix == null || matrix.Length == 0 || matrix[0].Length == 0)
            return false;

        int m = matrix.Length;
        int n = matrix[0].Length;

        int left = 0, right = m * n - 1;

        while (left <= right)
        {
            int mid = left + (right - left) / 2;
            int midVal = matrix[mid / n][mid % n];

            if (midVal == target) return true;
            if (midVal < target) left = mid + 1;
            else right = mid - 1;
        }
        return false;
    }
    public int MinEatingSpeed(int[] piles, int h)
    {
        if (piles == null || h <= 0)
            return 0;

        int i = 1, j = piles.Max();
        while (i <= j)
        {
            int mid = i + (j - i) / 2;
            long sum = 0;
            foreach (int p in piles)
            {
                sum += (long)(p + mid - 1) / mid;
            }
            if (sum <= h)
            {
                j = mid - 1;
            }
            else
            {
                i = mid + 1;
            }
        }
        return i;
    }
    public void ReorderList(ListNode head)
    {
        if (head == null || head.next == null)
            return;

        ListNode slow = head, fast = head;
        while (fast.next != null && fast.next.next != null)
        {
            slow = slow.next;
            fast = fast.next.next;
        }

        ListNode prev = null, curr = slow.next;
        while (curr != null)
        {
            ListNode nextTemp = curr.next;
            curr.next = prev;
            prev = curr;
            curr = nextTemp;
        }
        slow.next = null;

        ListNode first = head, second = prev;
        while (second != null)
        {
            ListNode tmp1 = first.next;
            ListNode tmp2 = second.next;

            first.next = second;
            second.next = tmp1;

            first = tmp1;
            second = tmp2;
        }
    }
    public ListNode RemoveNthFromEnd(ListNode head, int n)
    {
        List<int> l = new List<int>();
        ListNode temp = head;
        while (temp != null)
        {
            l.Add(temp.val);
            temp = temp.next;
        }
        l.RemoveAt(l.Count - n);
        ListNode res = null;
        ListNode current = null;
        for (int i = 0; i < l.Count; i++)
        {
            if (res == null)
            {
                res = new ListNode(l[i]);
                current = res;
            }
            else
            {
                current.next = new ListNode(l[i]);
                current = current.next;
            }
        }
        return res;
    }
    public TreeNode LowestCommonAncestor(TreeNode root, TreeNode p, TreeNode q)
    {
        if (root == null)
            return null;

        if (root.val < p.val && root.val < q.val)
        {
            return LowestCommonAncestor(root.right, p, q);
        }
        else if (root.val > p.val && root.val > q.val)
        {
            return LowestCommonAncestor(root.left, p, q);
        }
        return root;
    }
    public TreeNode InsertIntoBST(TreeNode root, int val)
    {
        if (root == null)
            return new TreeNode(val);

        if (root.val > val)
        {
            return new TreeNode(root.val, InsertIntoBST(root.left, val), root.right);
        }
        if (root.val < val)
        {
            return new TreeNode(root.val, root.left, InsertIntoBST(root.right, val));
        }
        return root;
    }
    public TreeNode DeleteNode(TreeNode root, int key)
    {
        if (root == null)
            return root;

        if (root.val > key)
            return new TreeNode(root.val, DeleteNode(root.left, key), root.right);
        else if (root.val < key)
            return new TreeNode(root.val, root.left, DeleteNode(root.right, key));
        if (root.val == key)
        {
            if (root.left == null)
                return root.right;
            if (root.right == null)
                return root.left;

            TreeNode cur = root.right;
            while (cur.left != null)
            {
                cur = cur.left;
            }
            root.val = cur.val;
            root.right = DeleteNode(root.right, root.val);
        }
        return root;
    }
    public IList<IList<int>> LevelOrder(TreeNode root)
    {
        if (root == null)
            return new List<IList<int>>();
        List<IList<int>> res = new List<IList<int>>();
        Dictionary<int, List<int>> d = new Dictionary<int, List<int>>();
        void Travel(TreeNode node, int level = 0)
        {
            if (node == null)
                return;

            if (d.ContainsKey(level))
                d[level].Add(node.val);
            else
                d[level] = new List<int> { node.val };
            Travel(node.left, level + 1);
            Travel(node.right, level + 1);
        }
        Travel(root);
        foreach (var item in d)
        {
            res.Add(item.Value);
        }
        return res;
    }
    public IList<int> RightSideView(TreeNode root)
    {
        if (root == null)
            return new List<int>();
        Dictionary<int, List<int>> d = new Dictionary<int, List<int>>();

        void Travel(TreeNode node, int level = 0)
        {
            if (node == null)
                return;

            if (d.ContainsKey(level))
                d[level].Add(node.val);
            else
                d[level] = new List<int> { node.val };
            Travel(node.left, level + 1);
            Travel(node.right, level + 1);
        }
        Travel(root);
        List<int> res = new List<int>();
        foreach (var i in d.Values)
        {
            if (i.Count != 0)
                res.Add(i.ElementAt(i.Count - 1));
        }
        return res;
    }
    public bool IsValidBST(TreeNode root)
    {
        if (root == null)
            return true;

        List<int> l = new List<int>();
        void InOrder(TreeNode node)
        {
            if (node == null)
                return;
            InOrder(node.left);
            l.Add(node.val);
            InOrder(node.right);
        }
        InOrder(root);
        for (int i = 1; i < l.Count; i++)
        {
            if (l[i - 1] >= l[i])
                return false;
        }
        return true;
    }
    public int KthSmallest(TreeNode root, int k)
    {
        if (root == null || k <= 0)
            return 0;

        int res = 0;
        int index = 0;
        void InOrder(TreeNode node)
        {
            if (node == null || index >= k)
                return;

            InOrder(node.left);
            index++;
            if (index == k)
            {
                res = node.val;
                return;
            }
            InOrder(node.right);
        }
        InOrder(root);
        return res;
    }
    public class Node1
    {
        public int val;
        public Node1 next;
        public Node1 random;

        public Node1(int _val)
        {
            val = _val;
            next = null;
            random = null;
        }
    }
    public Node1 CopyRandomList(Node1 head)
    {
        if (head == null)
            return null;

        Dictionary<Node1, Node1> d = new Dictionary<Node1, Node1>();
        Node1 curr = head;
        while (curr != null)
        {
            d[curr] = new Node1(curr.val);
            curr = curr.next;
        }
        curr = head;
        while (curr != null)
        {
            d[curr].next = curr.next == null ? null : d[curr.next];
            d[curr].random = curr.random == null ? null : d[curr.random];
            curr = curr.next;
        }
        return d[head];
    }
    public int LastStoneWeight(int[] stones)
    {
        if (stones == null || stones.Length == 0)
            return 0;
        Array.Sort(stones);
        List<int> l = new List<int>(stones);
        while (l.Count > 1)
        {
            int big = l[l.Count - 1];
            int small = l[l.Count - 2];
            if (big == small)
            {
                l.RemoveAt(l.Count - 1);
                l.RemoveAt(l.Count - 1);
            }
            else
            {
                int temp = big - small;
                l.RemoveAt(l.Count - 1);
                l.RemoveAt(l.Count - 1);
                l.Add(temp);
                l.Sort();
            }
        }
        return l.Count == 1 ? l[0] : 0;
    }
    public int[][] KClosest(int[][] points, int k)
    {
        if (points == null || k <= 0)
            return Array.Empty<int[]>();

        Dictionary<int, double> d = new Dictionary<int, double>();
        for (int i = 0; i < points.Length; i++)
        {
            d[i] = points[i][0] * points[i][0] + points[i][1] * points[i][1];
        }
        var ks = d.OrderBy(x => x.Value).Select(x => x.Key).ToList();
        int[][] res = new int[k][];
        for (int i = 0; i < res.Length; i++)
        {
            res[i] = new int[2];
            res[i][0] = points[ks[i]][0];
            res[i][1] = points[ks[i]][1];
        }
        return res;
    }
    public int FindKthLargest(int[] nums, int k)
    {
        if (nums == null || k <= 0)
            return 1;
        PriorityQueue<int, int> q = new PriorityQueue<int, int>();
        foreach (var item in nums)
        {
            q.Enqueue(item, item);
        }
        int i = 0;
        while (i != nums.Length - k)
        {
            q.Dequeue();
            i++;
        }
        return q.Dequeue();
    }
    public int LeastInterval(char[] tasks, int n)
    {
        if (tasks == null || n < 0)
            return 0;
        Dictionary<char, int> d = new Dictionary<char, int>();
        foreach (char c in tasks)
        {
            if (!d.ContainsKey(c))
                d[c] = 0;
            d[c]++;
        }
        PriorityQueue<int, int> pq = new PriorityQueue<int, int>();
        foreach (var item in d.Values)
        {
            pq.Enqueue(item, -item);
        }
        int time = 0;
        while (pq.Count > 0)
        {
            var temp = new List<int>();
            int cyc = n + 1;
            while (cyc > 0 && pq.Count > 0)
            {
                int cnt = pq.Dequeue();
                if (cnt - 1 > 0)
                    temp.Add(cnt - 1);
                time++;
                cyc--;
            }
            foreach (var item in temp)
                pq.Enqueue(item, -item);
            if (pq.Count > 0)
                time += cyc;
        }
        return time;
    }
    public int SubsetXORSum(int[] nums)
    {
        if (nums == null)
            return 0;

        int DFS(int i, int sum, int[] arr)
        {
            if (i == arr.Length)
                return sum;
            return DFS(i + 1, sum ^ arr[i], arr) + DFS(i + 1, sum, arr);
        }
        return DFS(0, 0, nums);
    }
    public IList<IList<int>> CombinationSum2(int[] candidates, int target)
    {
        if (candidates == null)
            return new List<IList<int>>();
        List<IList<int>> res = new List<IList<int>>();
        Array.Sort(candidates);

        void Recursion(int s, List<int> current, int targ)
        {
            if (targ == 0)
            {
                res.Add(new List<int>(current));
                return;
            }
            if (targ < 0)
                return;

            for (int i = s; i < candidates.Length; i++)
            {
                if (i != s && candidates[i] == candidates[i - 1])
                    continue;

                current.Add(candidates[i]);
                Recursion(i + 1, current, targ - candidates[i]);
                current.RemoveAt(current.Count - 1);
            }
        }
        Recursion(0, new List<int>(), target);
        return res;
    }
    public IList<IList<int>> Combine(int n, int k)
    {
        if (n <= 0)
            return new List<IList<int>>();
        List<IList<int>> lists = new List<IList<int>>();
        void Backtrack(List<int> current, int index = 1)
        {
            if (current.Count == k)
            {
                lists.Add(new List<int>(current));
                return;
            }
            if (index > n)
                return;
            current.Add(index);
            Backtrack(current, index + 1);
            current.RemoveAt(current.Count - 1);
            Backtrack(current, index + 1);
        }
        Backtrack(new List<int>());
        return lists;
    }
    public IList<IList<int>> SubsetsWithDup(int[] nums)
    {
        if (nums == null || nums.Length == 0)
            return new List<IList<int>>();
        Array.Sort(nums);
        List<IList<int>> lists = new List<IList<int>>();
        void Backtrack(List<int> current, int index = 0)
        {
            lists.Add(new List<int>(current));

            for (int i = index; i < nums.Length; i++)
            {
                if (i > index && nums[i] == nums[i - 1])
                    continue;

                current.Add(nums[i]);
                Backtrack(current, index + 1);
                current.RemoveAt(current.Count - 1);
            }
        }
        Backtrack(new List<int>());
        return lists;
    }
    public IList<IList<int>> PermuteUnique(int[] nums)
    {
        if (nums == null || nums.Length == 0)
            return new List<IList<int>>();
        List<IList<int>> lists = new List<IList<int>>();
        Dictionary<int, int> d = new Dictionary<int, int>();
        foreach (int i in nums)
        {
            if (!d.ContainsKey(i))
                d[i] = 0;
            d[i]++;
        }
        void Backtrack(List<int> current)
        {
            if (current.Count == nums.Length)
            {
                lists.Add(new List<int>(current));
                return;
            }
            foreach (var item in d)
            {
                if (item.Value > 0)
                {
                    current.Add(item.Key);
                    d[item.Key]--;
                    Backtrack(current);
                    d[item.Key]++;
                    current.RemoveAt(current.Count - 1);
                }
            }
        }
        Backtrack(new List<int>());
        return lists;
    }
    public IList<IList<string>> Partition(string s)
    {
        if (string.IsNullOrEmpty(s))
            return new List<IList<string>>();
        List<IList<string>> lists = new List<IList<string>>();
        bool IsPalindrome(string a)
        {
            if (a.Length == 1)
                return true;
            int i = 0, j = a.Length - 1;
            while (i <= j)
            {
                if (a[i] != a[j])
                    return false;
                i++; j--;
            }
            return true;
        }
        void Backtrack(List<string> current, int index = 0)
        {
            if (index == s.Length)
            {
                lists.Add(new List<string>(current));
            }
            for (int last = index; last < s.Length; last++)
            {
                string substring = s.Substring(index, last - index + 1);
                if (IsPalindrome(substring))
                {
                    current.Add(substring);
                    Backtrack(current, last + 1);
                    current.RemoveAt(current.Count - 1);
                }
            }
        }
        Backtrack(new List<string>());
        return lists;
    }
    public bool Makesquare(int[] matchsticks)
    {
        if (matchsticks == null || matchsticks.Length == 0)
            return false;
        int sum = matchsticks.Sum();
        if (sum % 4 != 0)
            return false;
        int groupSize = sum / 4;
        Array.Sort(matchsticks);
        Array.Reverse(matchsticks);
        bool res = false;

        void Backtrack(int[] lengths, int index)
        {
            if (res)
                return;
            if (index == matchsticks.Length)
            {
                res = (lengths[0] == groupSize && lengths[1] == groupSize &&
                       lengths[2] == groupSize && lengths[3] == groupSize);
            }
            for (int j = 0; j < lengths.Length; j++)
            {
                if (lengths[j] < groupSize && lengths[j] + matchsticks[index] <= groupSize)
                {
                    lengths[j] += matchsticks[index];
                    Backtrack(lengths, index + 1);
                    lengths[j] -= matchsticks[index];
                }
            }
        }
        Backtrack(new int[4], 0);
        return res;
    }
    public class TrieNode
    {
        public Dictionary<char, TrieNode> children =
                                new Dictionary<char, TrieNode>();
        public bool endOfWord = false;
    }
    public class Trie
    {
        TrieNode root;

        public Trie()
        {
            root = new TrieNode();
        }

        public void Insert(string word)
        {
            TrieNode curr = root;
            foreach (var c in word)
            {
                if (!curr.children.ContainsKey(c))
                    curr.children[c] = new TrieNode();
                curr = curr.children[c];
            }
            curr.endOfWord = true;
        }

        public bool Search(string word)
        {
            TrieNode curr = root;
            foreach (char c in word)
            {
                if (!curr.children.ContainsKey(c))
                {
                    return false;
                }
                curr = curr.children[c];
            }
            return curr.endOfWord;
        }

        public bool StartsWith(string prefix)
        {
            TrieNode cur = root;
            foreach (char c in prefix)
            {
                if (!cur.children.ContainsKey(c))
                {
                    return false;
                }
                cur = cur.children[c];
            }
            return true;
        }
    }
    public int FindJudge(int n, int[][] trust)
    {
        if (n <= 0 || trust == null)
            return -1;
        Dictionary<int, int> inD = new Dictionary<int, int>();
        Dictionary<int, int> outD = new Dictionary<int, int>();
        for (int i = 1; i <= n; i++)
        {
            inD[i] = 0;
            outD[i] = 0;
        }
        foreach (int[] arr in trust)
        {
            inD[arr[1]]++;
            outD[arr[0]]++;
        }
        for (int i = 1; i <= n; i++)
        {
            if (inD[i] == n - 1 && outD[i] == 0)
                return i;
        }
        return -1;
    }
    public int NumIslands(char[][] grid)
    {
        if (grid == null)
            return 0;
        bool[][] boolArray = new bool[grid.Length][];
        for (int i = 0; i < grid.Length; i++)
            boolArray[i] = new bool[grid[0].Length];

        for (int i = 0; i < grid.Length; i++)
            for (int j = 0; j < grid[0].Length; j++)
                boolArray[i][j] = false;

        int res = 0;

        void DFS(int r, int c)
        {
            if (r < 0 || c < 0 || r >= grid.Length || c >= grid[0].Length
                || boolArray[r][c] || grid[r][c] != '1')
                return;
            boolArray[r][c] = true;
            DFS(r, c + 1);
            DFS(r, c - 1);
            DFS(r + 1, c);
            DFS(r - 1, c);
        }
        for (int i = 0; i < grid.Length; i++)
        {
            for (int j = 0; j < grid[0].Length; j++)
            {
                if (!boolArray[i][j] && grid[i][j] == '1')
                {
                    res++;
                    DFS(i, j);
                }
            }
        }
        return res;
    }
    public int MaxAreaOfIsland(int[][] grid)
    {
        if (grid == null)
            return 0;
        int res = 0;
        bool[][] newArr = new bool[grid.Length][];
        for (int i = 0; i < newArr.Length; i++)
        {
            newArr[i] = new bool[grid[0].Length];
        }

        int DFS(int r, int c)
        {
            if (r < 0 || c < 0 || r >= grid.Length || c >= grid[0].Length
                || grid[r][c] == 0 || newArr[r][c])
                return 0;

            newArr[r][c] = true;
            int area = 1;
            area += DFS(r, c + 1);
            area += DFS(r, c - 1);
            area += DFS(r + 1, c);
            area += DFS(r - 1, c);
            return area;
        }
        for (int i = 0; i < grid.Length; i++)
        {
            for (int j = 0; j < grid[0].Length; j++)
            {
                if (!newArr[i][j] && grid[i][j] == 1)
                {
                    int area = DFS(i, j);
                    res = Math.Max(res, area);
                }
            }
        }
        return res;
    }
    public IList<IList<int>> ThreeSum(int[] nums)
    {
        if (nums == null || nums.Length == 0)
            return new List<IList<int>>();
        List<IList<int>> lists = new List<IList<int>>();
        Array.Sort(nums);
        for (int i = 0; i < nums.Length - 2; i++)
        {
            if (i > 0 && nums[i] == nums[i - 1])
                continue;
            int x = i + 1, y = nums.Length - 1;
            while (x <= y)
            {
                int temp = nums[i] + nums[x] + nums[y];
                if (temp == 0)
                {
                    lists.Add(new List<int> { nums[i], nums[x++], nums[y--] });
                    while (x < y && nums[x] == nums[x - 1])
                        x++;
                    while (x < y && nums[y] == nums[y + 1])
                        y--;
                }
                else if (temp > 0)
                {
                    y--;
                }
                else
                {
                    x++;
                }
            }
        }
        return lists;
    }
    public IList<IList<int>> FourSum(int[] nums, int target)
    {
        if (nums == null || nums.Length == 0)
            return new List<IList<int>>();
        List<IList<int>> lists = new List<IList<int>>();
        Array.Sort(nums);
        for (int i = 0; i < nums.Length - 3; i++)
        {
            if (i > 0 && nums[i] == nums[i - 1])
                continue;
            for (int j = nums.Length - 1; j > i + 2; j--)
            {
                if (j < nums.Length - 1 && nums[j] == nums[j + 1])
                    continue;
                int x = i + 1, y = j - 1;
                while (x < y)
                {
                    long res = (long)nums[i] + nums[x] + nums[y] + nums[j];
                    if (res == target)
                    {
                        lists.Add(new List<int> { nums[i], nums[x++], nums[y--], nums[j] });
                        while (x < y && nums[x] == nums[x - 1])
                            x++;
                        while (x < y && nums[y] == nums[y + 1])
                            y--;
                    }
                    else if (res < target)
                        x++;
                    else
                        y--;
                }
            }
        }
        return lists;
    }
    public int Tribonacci(int n)
    {
        if (n <= 0)
            return 0;
        if (n <= 2)
            return 1;
        int[] arr = new int[n + 1];
        arr[0] = 0;
        arr[1] = 1;
        arr[2] = 1;
        for (int i = 3; i < arr.Length; i++)
        {
            arr[i] = arr[i - 1] + arr[i - 2] + arr[i - 3];
        }
        return arr[n];
    }
    public int Rob(int[] nums)
    {
        if (nums == null || nums.Length == 0)
            return 0;

        int[] memo = new int[nums.Length];
        Array.Fill(memo, -1);
        int DFS(int[] arr, int i)
        {
            if (i >= arr.Length)
                return 0;
            if (memo[i] != -1)
                return memo[i];

            memo[i] = Math.Max(arr[i] + DFS(arr, i + 2), DFS(arr, i + 1));
            return memo[i];
        }
        return DFS(nums, 0);
    }
    public int RobII(int[] nums)
    {
        if (nums.Length == 1)
            return nums[0];
        int[] memo1 = new int[nums.Length];
        int[] memo2 = new int[nums.Length];
        Array.Fill(memo1, -1);
        Array.Fill(memo2, -1);
        int DFS(int[] arr, int i, int[] memo)
        {
            if (i >= arr.Length)
                return 0;
            if (memo[i] != -1)
                return memo[i];

            memo[i] = Math.Max(arr[i] + DFS(arr, i + 2, memo), DFS(arr, i + 1, memo));
            return memo[i];
        }
        int[] sub1 = new int[nums.Length - 1];
        int[] sub2 = new int[nums.Length - 1];
        for (int i = 0; i < nums.Length - 1; i++)
        {
            sub1[i] = nums[i];
            sub2[i] = nums[i + 1];
        }
        return Math.Max(DFS(sub1, 0, memo1), DFS(sub2, 0, memo2));
    }
    public string LongestPalindrome(string s)
    {
        if (string.IsNullOrEmpty(s))
            return null;

        string longest = "";
        for (int i = 0; i < s.Length; i++)
        {
            int l = i, r = i;
            while (l >= 0 && r < s.Length && s[l] == s[r])
            {
                if (r - l + 1 > longest.Length)
                    longest = s.Substring(l, r - l + 1);
                l--;
                r++;
            }
            l = i;
            r = i + 1;
            while (l >= 0 && r < s.Length && s[l] == s[r])
            {
                if (r - l + 1 > longest.Length)
                    longest = s.Substring(l, r - l + 1);
                l--;
                r++;
            }
        }
        return longest;
    }
    public int CountSubstrings(string s)
    {
        if (string.IsNullOrEmpty(s))
            return 0;

        int count = 0;
        for (int i = 0; i < s.Length; i++)
        {
            int l = i, r = i;
            while (l >= 0 && r < s.Length && s[l] == s[r])
            {
                count++;
                l--;
                r++;
            }
            l = i;
            r = i + 1;
            while (l >= 0 && r < s.Length && s[l] == s[r])
            {
                count++;
                l--;
                r++;
            }
        }
        return count;
    }
    public int UniquePaths(int m, int n)
    {
        if (m <= 0 || n <= 0)
            return 0;
        int[,] arr = new int[m, n];
        int x = 0;
        while (x < arr.GetLength(0))
        {
            arr[x++, 0] = 1;
        }
        x = 0;
        while (x < arr.GetLength(1))
        {
            arr[0, x++] = 1;
        }
        for (int i = 1; i < arr.GetLength(0); i++)
        {
            for (int j = 1; j < arr.GetLength(1); j++)
            {
                arr[i, j] = arr[i, j - 1] + arr[i - 1, j];
            }
        }
        return arr[m - 1, n - 1];
    }
    public int UniquePathsWithObstacles(int[][] obstacleGrid)
    {
        if (obstacleGrid == null)
            return 0;
        if (obstacleGrid[obstacleGrid.Length - 1][obstacleGrid[0].Length - 1] == 1)
            return 0;

        int x = 0;
        while (x < obstacleGrid.Length)
        {
            if (obstacleGrid[x][0] != 1)
                obstacleGrid[x++][0] = -1;
            else
                break;
        }
        x = 0;
        while (x < obstacleGrid[0].Length)
        {
            if (obstacleGrid[0][x] != 1)
                obstacleGrid[0][x++] = -1;
            else
                break;
        }
        for (int i = 1; i < obstacleGrid.Length; i++)
        {
            for (int j = 1; j < obstacleGrid[0].Length; j++)
            {
                if (obstacleGrid[i][j] != 1)
                {
                    int r = obstacleGrid[i - 1][j] != 1 ? obstacleGrid[i - 1][j] : 0;
                    int c = obstacleGrid[i][j - 1] != 1 ? obstacleGrid[i][j - 1] : 0;
                    obstacleGrid[i][j] = r + c;
                }
            }
        }
        return -1 * obstacleGrid[obstacleGrid.Length - 1][obstacleGrid[0].Length - 1];
    }
    public int MinPathSum(int[][] grid)
    {
        if (grid == null)
            return 0;
        int MinPath(int i, int j, int?[,] memo)
        {
            if (i == grid.Length || j == grid[0].Length)
                return int.MaxValue;
            if (memo[i, j].HasValue)
                return memo[i, j].Value;
            if (i == grid.Length - 1 && j == grid[0].Length - 1)
                return grid[i][j];
            int res = grid[i][j] + Math.Min(MinPath(i + 1, j, memo), MinPath(i, j + 1, memo));
            memo[i, j] = res;
            return res;
        }
        int?[,] memo = new int?[grid.Length, grid[0].Length];
        return MinPath(0, 0, memo);
    }
    public int LongestCommonSubsequence(string text1, string text2)
    {
        if (string.IsNullOrEmpty(text1) || string.IsNullOrEmpty(text2))
            return 0;
        int[,] arr = new int[text1.Length + 1, text2.Length + 1];
        for (int i = text1.Length - 1; i >= 0; i--)
        {
            for (int j = text2.Length - 1; j >= 0; j--)
            {
                if (text1[i] == text2[j])
                    arr[i, j] = 1 + arr[i + 1, j + 1];
                else
                    arr[i, j] = Math.Max(arr[i + 1, j], arr[i, j + 1]);
            }
        }
        return arr[0, 0];
    }
    public bool IsValidSudoku(char[][] board)
    {
        if (board == null || board.Length != 9) return false;

        HashSet<char>[] rows = new HashSet<char>[9];
        HashSet<char>[] cols = new HashSet<char>[9];
        HashSet<char>[] boxes = new HashSet<char>[9];
        for (int i = 0; i < 9; i++)
        {
            rows[i] = new HashSet<char>();
            cols[i] = new HashSet<char>();
            boxes[i] = new HashSet<char>();
        }
        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                char c = board[i][j];
                if (c == '.') continue;
                int boxIndex = (i / 3) * 3 + j / 3;
                if (!rows[i].Add(c) || !cols[j].Add(c) || !boxes[boxIndex].Add(c))
                    return false;
            }
        }
        return true;
    }
    public int LongestConsecutive(int[] nums)
    {
        if (nums == null || nums.Length == 0)
            return 0;

        HashSet<int> s = new HashSet<int>(nums);
        int max = 1;
        foreach (int n in nums)
        {
            int temp = 1;
            int v = n + 1;
            int b = n - 1;
            while (s.Contains(v))
            {
                s.Remove(v);
                v++;
                temp++;
            }
            while (s.Contains(b))
            {
                s.Remove(b);
                b--;
                temp++;
            }
            max = Math.Max(max, temp);
        }
        return max;
    }
    public int MaxProfit(int[] prices)
    {
        if (prices == null || prices.Length <= 1)
            return 0;
        int prof = 0;
        for (int i = 1; i < prices.Length; i++)
        {
            if (prices[i] > prices[i - 1])
                prof += prices[i] - prices[i - 1];
        }
        return prof;
    }
    public IList<int> MajorityElement(int[] nums)
    {
        if (nums == null || nums.Length == 0)
            return new List<int>();
        Dictionary<int, int> d = new Dictionary<int, int>();
        foreach (int i in nums)
        {
            if (!d.ContainsKey(i))
                d[i] = 0;
            d[i]++;
        }
        return d.Where(x => x.Value >= nums.Length / 3).Select(x => x.Key).ToList();
    }
    public int SubarraySum(int[] nums, int k)
    {
        if (nums == null || nums.Length == 0)
            return 0;

        int currentSum = 0, res = 0;
        Dictionary<int, int> pref = new Dictionary<int, int>();
        pref[0] = 1;
        foreach (int i in nums)
        {
            currentSum += i;
            int difference = currentSum - k;
            if (pref.ContainsKey(difference))
                res += pref[difference];
            pref[currentSum] = 1 + (pref.ContainsKey(currentSum) ? pref[currentSum] : 0);
        }
        return res;
    }
    public int NumRescueBoats(int[] people, int limit)
    {
        if (people == null || people.Length == 0)
            return 0;
        Array.Sort(people);
        int i = 0, j = people.Length - 1;
        int res = 0;
        while (i <= j)
        {
            if (people[i] + people[j] <= limit)
            {
                res++;
                i++;
                j--;
            }
            else
            {
                res++;
                j--;
            }
        }
        return res;
    }
    public IList<int> FindClosestElements(int[] arr, int k, int x)
    {
        if (arr == null || arr.Length == 0)
            return new List<int>();

        int closest = int.MaxValue;
        int closestIndex = 0;
        for (int i = 0; i < arr.Length; i++)
        {
            int temp = Math.Abs(x - arr[i]);
            if (closest > temp)
            {
                closestIndex = i;
                closest = temp;
            }
        }

        int left = closestIndex;
        int right = closestIndex;
        while (right - left + 1 < k)
        {
            if (left == 0)
                right++;
            else if (right == arr.Length - 1)
                left--;
            else if (Math.Abs(x - arr[left]) <= Math.Abs(x - arr[right]))
                left--;
            else
                right++;
        }
        List<int> list = new List<int>();
        while (left <= right)
        {
            list.Add(arr[left++]);
        }
        return list;
    }
    public int[] DailyTemperatures(int[] temperatures)
    {
        if (temperatures == null || temperatures.Length == 0)
            return Array.Empty<int>();
        int[] arr = new int[temperatures.Length];
        Stack<int> monoStack = new Stack<int>();
        for (int i = 0; i < temperatures.Length; i++)
        {
            while (monoStack.Count > 0 && temperatures[i] > temperatures[monoStack.Peek()])
            {
                int prevIndex = monoStack.Pop();
                arr[prevIndex] = i - prevIndex;
            }
            monoStack.Push(i);
        }
        return arr;
    }
    public class StockSpanner
    {
        Stack<(int price, int span)> s;
        public StockSpanner()
        {
            s = new Stack<(int price, int span)>();
        }

        public int Next(int price)
        {
            int span = 1;
            while (s.Count > 0 && price >= s.Peek().price)
            {
                span += s.Pop().span;
            }
            s.Push((price, span));
            return span;
        }
    }
    public int FirstMissingPositive(int[] nums)
    {
        if (nums == null || nums.Length == 0)
            return 0;

        int i = 0;
        while (i < nums.Length)
        {
            int cIndex = nums[i] - 1;
            if (nums[i] > 0 && nums[i] <= nums.Length && nums[i] != nums[cIndex])
            {
                int temp = nums[i];
                nums[i] = nums[cIndex];
                nums[cIndex] = temp;
            }
            else
            {
                i++;
            }
        }
        for (int j = 0; j < nums.Length; j++)
        {
            if (nums[j] != j + 1)
                return j + 1;
        }
        return nums.Length + 1;
    }
    public int MaxSubArray(int[] nums)
    {
        if (nums == null || nums.Length == 0)
            return 0;
        int currentSum = nums[0];
        int max = currentSum;
        for (int i = 1; i < nums.Length; i++)
        {
            currentSum = Math.Max(nums[i], currentSum + nums[i]);
            max = Math.Max(max, currentSum);
        }
        return max;
    }
    public int[][] Insert(int[][] intervals, int[] newInterval)
    {
        if (intervals == null || intervals.Length == 0)
            return new int[][] { newInterval };

        List<int[]> result = new List<int[]>();
        bool inserted = false;

        for (int i = 0; i < intervals.Length; i++)
        {
            if (intervals[i][1] < newInterval[0])
            {
                result.Add(intervals[i]);
            }
            else if (intervals[i][0] > newInterval[1])
            {
                if (!inserted)
                {
                    result.Add(newInterval);
                    inserted = true;
                }
                result.Add(intervals[i]);
            }
            else
            {
                newInterval[0] = Math.Min(newInterval[0], intervals[i][0]);
                newInterval[1] = Math.Max(newInterval[1], intervals[i][1]);
            }
        }
        if (!inserted)
        {
            result.Add(newInterval);
        }
        return result.ToArray();
    }
    public int[][] Merge(int[][] intervals)
    {
        if (intervals == null)
            return Array.Empty<int[]>();
        if (intervals.Length == 1)
            return intervals;
        List<int[]> l = new List<int[]>();
        Array.Sort(intervals, (a, b) => a[0].CompareTo(b[0]));

        int prevMin = intervals[0][0];
        int prevMax = intervals[0][1];
        for (int i = 1; i < intervals.Length; i++)
        {
            int currMin = intervals[i][0];
            int currMax = intervals[i][1];
            if (currMin > prevMax)
            {
                l.Add(new int[] { prevMin, prevMax });
                prevMin = currMin;
                prevMax = currMax;
            }
            else
            {
                prevMin = Math.Min(prevMin, currMin);
                prevMax = Math.Max(prevMax, currMax);
            }
        }
        l.Add(new int[] { prevMin, prevMax });
        return l.ToArray();
    }
    public int EraseOverlapIntervals(int[][] intervals)
    {
        if (intervals == null || intervals.Length <= 1)
            return 0;
        Array.Sort(intervals, (a, b) => a[0].CompareTo(b[0]));
        int prevMax = intervals[0][1];
        int res = 0;
        for (int i = 1; i < intervals.Length; i++)
        {
            int currMin = intervals[i][0];
            int currMax = intervals[i][1];
            if (currMin >= prevMax)
            {
                prevMax = currMax;
                continue;
            }
            else
            {
                res++;
                prevMax = Math.Min(prevMax, currMax);
            }
        }
        return res;
    }
    public ListNode InsertGreatestCommonDivisors(ListNode head)
    {
        if (head == null)
            return null;

        int GCD(int x, int y)
        {
            if (y == 0)
                return x;
            return GCD(y, x % y);
        }
        ListNode temp = head;
        while (temp.next != null)
        {
            int gcd = GCD(temp.val, temp.next.val);
            ListNode G = new ListNode(gcd);
            G.next = temp.next;
            temp.next = G;
            temp = G.next;
        }
        return head;
    }
    public void Rotate(int[][] matrix)
    {
        if (matrix == null)
            return;

        int n = matrix.Length;
        for (int i = 0; i < n; i++)
        {
            for (int j = i + 1; j < n; j++)
            {
                int temp = matrix[i][j];
                matrix[i][j] = matrix[j][i];
                matrix[j][i] = temp;
            }
        }
        for (int i = 0; i < n; i++)
        {
            Array.Reverse(matrix[i]);
        }
    }
    public List<int> SpiralOrder(int[][] matrix)
    {
        if (matrix == null)
            return new List<int>();

        int left = 0, right = matrix[0].Length;
        int top = 0, bottom = matrix.Length;
        List<int> res = new List<int>();
        while (left < right && top < bottom)
        {
            for (int i = left; i < right; i++)
            {
                res.Add(matrix[top][i]);
            }
            top++;
            for (int i = top; i < bottom; i++)
            {
                res.Add(matrix[i][right - 1]);
            }
            right--;
            if (!(left < right && top < bottom))
            {
                break;
            }
            for (int i = right - 1; i >= left; i--)
            {
                res.Add(matrix[bottom - 1][i]);
            }
            bottom--;
            for (int i = bottom - 1; i >= top; i--)
            {
                res.Add(matrix[i][left]);
            }
            left++;
        }
        return res;
    }
    public int FindMin(int[] nums)
    {
        if (nums == null || nums.Length == 0)
            return 0;
        int i = 0, j = nums.Length - 1;
        while (i < j)
        {
            if (nums[i] < nums[j])
                return nums[i];

            int mid = (i + j) / 2;
            if (nums[mid] > nums[j])
                i = mid + 1;
            else
                j = mid - 1;
        }
        return nums[i];
    }
    public int[] SearchRange(int[] nums, int target)
    {
        if (nums == null || nums.Length == 0)
            return new int[] { -1, -1 };

        int i = 0, j = nums.Length - 1;

        int first = -1, last = 0;

        while (i <= j)
        {
            int mid = (i + j) / 2;
            if (nums[mid] < target)
            {
                i = mid + 1;
            }
            else if (nums[mid] > target)
            {
                j = mid - 1;
            }
            else
            {
                first = mid;
                j--;
            }
        }
        if (first == -1)
            return new int[] { -1, -1 };
        i = 0; j = nums.Length - 1;
        while (i <= j)
        {
            int mid = (i + j) / 2;
            if (nums[mid] < target)
            {
                i = mid + 1;
            }
            else if (nums[mid] > target)
            {
                j = mid - 1;
            }
            else
            {
                last = mid;
                i++;
            }
        }
        return new int[] { first, last };
    }
    public int GetSum(int a, int b)
    {
        for (int i = 0; i < 32; i++)
        {
            int carry = (a & b) << 1;
            a ^= b;
            b = carry;
        }
        return a;
    }
    public int CarFleet(int target, int[] position, int[] speed)
    {
        if (position == null || speed == null)
            return 0;
        if (position.Length == 1)
            return 1;

        Dictionary<int, int> d = new Dictionary<int, int>(); // key-pos val-speed
        for (int i = 0; i < position.Length; i++)
        {
            d[position[i]] = speed[i];
        }
        position = d.OrderBy(x => x.Key).Select(x => x.Key).ToArray();
        speed = d.OrderBy(x => x.Key).Select(x => x.Value).ToArray();

        int[] steps = new int[position.Length];
        for (int i = 0; i < steps.Length; i++)
        {
            steps[i] = (target - position[i] + speed[i] - 1) / speed[i];
        }
        Stack<double> mono = new Stack<double>();
        for (int i = steps.Length - 1; i >= 0; i--)
        {
            double time = (double)(target - position[i]) / speed[i];
            if (mono.Count == 0 || mono.Peek() < time)
                mono.Push(time);
        }
        return mono.Count;
    }
    public string SimplifyPath(string path)
    {
        if (string.IsNullOrEmpty(path))
            return null;

        Stack<string> s = new Stack<string>();
        int i = 0;

        while (i < path.Length)
        {
            while (i < path.Length && path[i] == '/')
            {
                i++;
            }
            string temp = "";
            while (i < path.Length && path[i] != '/')
            {
                temp += path[i++];
            }

            if (temp == "." || temp == "")
            {
                continue;
            }
            else if (temp == "..")
            {
                if (s.Count != 0)
                    s.Pop();
            }
            else
                s.Push(temp);
        }
        if (s.Count == 0)
            return "/";
        string[] dirs = s.Reverse().ToArray();
        return "/" + string.Join("/", dirs);
    }
    public int FindDuplicate(int[] nums)
    {
        if (nums == null || nums.Length == 0)
            return 0;

        int slow = 0, fast = 0;
        while (true)
        {
            slow = nums[slow];
            fast = nums[nums[fast]];
            if (slow == fast)
                break;
        }
        int slow2 = 0;
        while (true)
        {
            slow = nums[slow];
            slow2 = nums[slow2];
            if (slow == slow2)
                return slow;
        }
        return 0;
    }
    public string MinWindow(string s, string t)
    {
        if (string.IsNullOrEmpty(s) || string.IsNullOrEmpty(t))
            return null;
        Dictionary<char, int> tD = new Dictionary<char, int>();
        foreach (var c in t)
        {
            if (!tD.ContainsKey(c))
                tD[c] = 0;
            tD[c]++;
        }

        Dictionary<char, int> windowD = new Dictionary<char, int>();
        int have = 0, need = tD.Count;
        int resultLength = s.Length + 1, left = 0;
        int[] indexes = new int[2];
        for (int r = 0; r < s.Length; r++)
        {
            if (!windowD.ContainsKey(s[r]))
                windowD[s[r]] = 0;
            windowD[s[r]]++;

            if (tD.ContainsKey(s[r]) && windowD[s[r]] == tD[s[r]])
            {
                have++;
            }
            while (have == need)
            {
                if (resultLength > r - left + 1)
                {
                    resultLength = r - left + 1;
                    indexes[0] = left;
                    indexes[1] = r;
                }

                char leftC = s[left];
                windowD[leftC]--;
                if (tD.ContainsKey(leftC) && tD[leftC] > windowD[leftC])
                    have--;
                left++;
            }
        }
        return resultLength == s.Length + 1 ? "" : s.Substring(indexes[0], resultLength);
    }
    public ListNode ReverseBetween(ListNode head, int left, int right)
    {
        if (head == null || left > right)
            return null;
        List<int> l = new List<int>();
        while (head != null)
        {
            l.Add(head.val);
            head = head.next;
        }
        l.Reverse(left - 1, right - left + 1);

        ListNode res = new ListNode(l[0]);
        ListNode temp = res;
        for (int i = 1; i < l.Count; i++)
        {
            temp.next = new ListNode(l[i]);
            temp = temp.next;
        }
        return res;
    }
    public class MyCircularQueue
    {
        int k;
        int currSize;
        ListNode last;
        public MyCircularQueue(int k)
        {
            this.k = k;
            currSize = 0;
            last = null;
        }

        public bool EnQueue(int value)
        {
            if (currSize == k)
                return false;
            ListNode node = new ListNode(value);
            if (currSize == 0)
            {
                last = node;
                last.next = last;
            }
            else
            {
                node.next = last.next;
                last.next = node;
                last = node;
            }
            currSize++;
            return true;
        }

        public bool DeQueue()
        {
            if (currSize == 0)
                return false;
            else if (currSize == 1)
            {
                last = null;
            }
            else
            {
                last.next = last.next.next;
            }
            currSize--;
            return true;
        }

        public int Front()
        {
            if (currSize == 0)
                return -1;
            return last.next.val;
        }

        public int Rear()
        {
            if (currSize == 0)
                return -1;
            return last.val;
        }

        public bool IsEmpty()
        {
            return currSize == 0;
        }

        public bool IsFull()
        {
            return currSize == k;
        }
    }
    public class QNode
    {
        public bool val;
        public bool isLeaf;
        public QNode topLeft;
        public QNode topRight;
        public QNode bottomLeft;
        public QNode bottomRight;

        public QNode()
        {
            val = false;
            isLeaf = false;
            topLeft = null;
            topRight = null;
            bottomLeft = null;
            bottomRight = null;
        }

        public QNode(bool _val, bool _isLeaf)
        {
            val = _val;
            isLeaf = _isLeaf;
            topLeft = null;
            topRight = null;
            bottomLeft = null;
            bottomRight = null;
        }

        public QNode(bool _val, bool _isLeaf, QNode _topLeft, QNode _topRight, QNode _bottomLeft, QNode _bottomRight)
        {
            val = _val;
            isLeaf = _isLeaf;
            topLeft = _topLeft;
            topRight = _topRight;
            bottomLeft = _bottomLeft;
            bottomRight = _bottomRight;
        }
    }
    public QNode Construct(int[][] grid)
    {
        int first = grid[0][0];
        bool allSame = true;
        for (int i = 0; i < grid.Length; i++)
        {
            for (int j = 0; j < grid[0].Length; j++)
            {
                if (!allSame)
                    break;
                if (grid[i][j] != first)
                {
                    allSame = false;
                    break;
                }
            }
        }
        if (allSame)
            return new QNode(first == 1, true);

        QNode res = new QNode();
        int[][] topL = new int[grid.Length / 2][];
        int[][] topR = new int[grid.Length / 2][];
        int[][] bottomL = new int[grid.Length / 2][];
        int[][] bottomR = new int[grid.Length / 2][];
        for (int i = 0; i < topL.Length; i++)
        {
            topL[i] = new int[grid.Length / 2];
            topR[i] = new int[grid.Length / 2];
            bottomL[i] = new int[grid.Length / 2];
            bottomR[i] = new int[grid.Length / 2];
        }
        for (int i = 0; i < topL.Length; i++)
        {
            for (int j = 0; j < topL[0].Length; j++)
            {
                topL[i][j] = grid[i][j];
                topR[i][j] = grid[i][j + (topL.Length)];
                bottomL[i][j] = grid[i + (topL.Length)][j];
                bottomR[i][j] = grid[i + (topL.Length)][j + (topL.Length)];
            }
        }

        res.isLeaf = false;
        res.val = false;

        res.topLeft = Construct(topL);
        res.topRight = Construct(topR);
        res.bottomLeft = Construct(bottomL);
        res.bottomRight = Construct(bottomR);
        return res;
    }
    public class GNode
    {
        public int val;
        public IList<GNode> neighbors;

        public GNode()
        {
            val = 0;
            neighbors = new List<GNode>();
        }

        public GNode(int _val)
        {
            val = _val;
            neighbors = new List<GNode>();
        }

        public GNode(int _val, List<GNode> _neighbors)
        {
            val = _val;
            neighbors = _neighbors;
        }
    }
    public GNode CloneGraph(GNode node)
    {
        if (node == null)
            return null;

        Dictionary<GNode, GNode> d = new Dictionary<GNode, GNode>();
        GNode Dfs(GNode root)
        {
            if (d.ContainsKey(root))
                return d[root];

            d[root] = new GNode(root.val);
            for (int i = 0; i < root.neighbors.Count; i++)
            {
                d[root].neighbors.Add(Dfs(root.neighbors[i]));
            }
            return d[root];
        }
        return Dfs(node);
    }
    public string Convert(string s, int numRows)
    {
        if (numRows == 1 || s.Length <= numRows)
            return s;

        List<string> rows = new List<string>();
        for (int i = 0; i < Math.Min(s.Length, numRows); i++)
        {
            rows.Add("");
        }
        int currRow = 0;
        bool goingDown = false;

        foreach (char c in s)
        {
            rows[currRow] += c;

            if (currRow == 0 || currRow == numRows - 1)
                goingDown = !goingDown;

            currRow += goingDown ? 1 : -1;
        }
        return string.Join("", rows);
    }
    public int ShipWithinDays(int[] weights, int days)
    {
        if (weights == null || days < 0)
            return 0;

        bool CanShip(int[] ws, int maxWeight)
        {
            int count = 1;
            int i = 0;
            int temp = 0;
            while (i < ws.Length)
            {
                if (temp + ws[i] <= maxWeight)
                {
                    temp += ws[i++];
                }
                else
                {
                    if (ws[i] > maxWeight)
                        return false;
                    count++;
                    temp = 0;
                }
            }
            return count <= days;
        }
        int i = weights.Max(), j = weights.Sum();
        while (i <= j)
        {
            int mid = (i + j) / 2;
            bool res = CanShip(weights, mid);
            if (res)
            {
                j = mid - 1;
            }
            else
            {
                i = mid + 1;
            }
        }
        return i;
    }
    public int SearchRotated(int[] nums, int target)
    {
        if (nums == null || nums.Length == 0)
            return -1;
        int i = 0, j = nums.Length - 1;
        while (i < j)
        {
            int mid = (i + j) / 2;
            if (nums[mid] > nums[j])
                i = mid + 1;
            else
                j = mid;
        }
        int BinarySearch(int i, int j)
        {
            while (i <= j)
            {
                int mid = (i + j) / 2;
                if (nums[mid] > target)
                {
                    j = mid - 1;
                }
                else if (nums[mid] < target)
                {
                    i = mid + 1;
                }
                else
                    return mid;
            }
            return -1;
        }
        if (target >= nums[i] && target <= nums[nums.Length - 1])
            return BinarySearch(i, nums.Length - 1);
        else
            return BinarySearch(0, i - 1);
    }
    public TreeNode RemoveLeafNodes(TreeNode root, int target)
    {
        if (root == null)
            return null;

        root.left = RemoveLeafNodes(root.left, target);
        root.right = RemoveLeafNodes(root.right, target);

        if (root.left == null && root.right == null)
        {
            return root.val == target ? null : root;
        }

        return root;
    }
    public bool CanPartitionKSubsets(int[] nums, int k)
    {
        if (nums == null || k <= 0)
            return false;

        int total = nums.Sum(), target = total / k;
        if (total % k != 0)
            return false;
        bool[] used = new bool[nums.Length];
        Array.Sort(nums);
        Array.Reverse(nums);
        bool Backtrack(int index, int kLeft, int thisSum)
        {
            if (kLeft == 0)
                return true;
            if (thisSum == target)
                return Backtrack(0, kLeft - 1, 0);

            for (int i = index; i < nums.Length; i++)
            {
                if (used[i] || nums[i] + thisSum > target)
                    continue;
                used[i] = true;
                bool res = Backtrack(i + 1, kLeft, thisSum + nums[i]);
                if (res)
                    return true;
                used[i] = false;
                if (thisSum == 0) break;
            }
            return false;
        }
        return Backtrack(0, k, 0);
    }
    public void SetZeroes(int[][] matrix)
    {
        if (matrix == null)
            return;
        int r = matrix.Length, c = matrix[0].Length;
        bool[] rows = new bool[r];
        bool[] cols = new bool[c];
        for (int i = 0; i < r; i++)
        {
            for (int j = 0; j < c; j++)
            {
                if (matrix[i][j] == 0)
                {
                    rows[i] = true;
                    cols[j] = true;
                }
            }
        }
        for (int i = 0; i < r; i++)
        {
            for (int j = 0; j < c; j++)
            {
                if (rows[i] || cols[j])
                    matrix[i][j] = 0;
            }
        }
    }
    public class Twitter
    {
        private class User
        {
            public int Id;
            public HashSet<int> Followings;
            public List<(int TweetId, int Time)> Tweets;
            public User(int id)
            {
                Id = id;
                Followings = new HashSet<int>();
                Tweets = new List<(int, int)>();
            }
        }
        private Dictionary<int, User> users;
        private int globalTime;
        public Twitter()
        {
            users = new Dictionary<int, User>();
            globalTime = 0;
        }
        private User GetOrCreateUser(int userId)
        {
            if (!users.ContainsKey(userId))
                users[userId] = new User(userId);
            return users[userId];
        }
        public void PostTweet(int userId, int tweetId)
        {
            var user = GetOrCreateUser(userId);
            user.Tweets.Add((tweetId, globalTime++));
        }
        public IList<int> GetNewsFeed(int userId)
        {
            if (!users.ContainsKey(userId))
                return new List<int>();

            var user = users[userId];
            var feed = new List<(int tweetId, int time)>();

            feed.AddRange(user.Tweets);

            foreach (var fid in user.Followings)
            {
                if (users.ContainsKey(fid))
                    feed.AddRange(users[fid].Tweets);
            }
            return feed
                .OrderByDescending(t => t.time)
                .Take(10)
                .Select(t => t.tweetId)
                .ToList();
        }
        public void Follow(int followerId, int followeeId)
        {
            if (followerId == followeeId) return;
            var follower = GetOrCreateUser(followerId);
            GetOrCreateUser(followeeId);
            follower.Followings.Add(followeeId);
        }
        public void Unfollow(int followerId, int followeeId)
        {
            if (followerId == followeeId) return;
            if (!users.ContainsKey(followerId)) return;
            users[followerId].Followings.Remove(followeeId);
        }
    }
    public int OrangesRotting(int[][] grid)
    {
        if (grid == null)
            return 0;
        Queue<int[]> q = new Queue<int[]>();
        int freshOranges = 0, time = 0;
        int[][] dirs = new int[4][];
        dirs[0] = new int[2] { 0, -1 };
        dirs[1] = new int[2] { 0, 1 };
        dirs[2] = new int[2] { -1, 0 };
        dirs[3] = new int[2] { 1, 0 };
        for (int i = 0; i < grid.Length; i++)
        {
            for (int j = 0; j < grid[0].Length; j++)
            {
                if (grid[i][j] == 1)
                    freshOranges++;
                if (grid[i][j] == 2)
                    q.Enqueue(new int[2] { i, j });
            }
        }
        while (q.Count > 0 && freshOranges > 0)
        {
            int size = q.Count;
            for (int i = 0; i < size; i++)
            {
                int r = q.Peek()[0];
                int c = q.Peek()[1];
                q.Dequeue();
                foreach (var dir in dirs)
                {
                    int nr = r + dir[0];
                    int nc = c + dir[1];

                    if (nr < 0 || nc < 0 || nr >= grid.Length || nc >= grid[0].Length || grid[nr][nc] != 1)
                        continue;
                    grid[nr][nc] = 2;
                    freshOranges--;
                    q.Enqueue(new int[] { nr, nc });
                }
            }
            time++;
        }
        return freshOranges == 0 ? time : -1;
    }
    public TreeNode BuildTree(int[] preorder, int[] inorder)
    {
        if (preorder == null || inorder == null || preorder.Length == 0 || inorder.Length == 0)
            return null;
        TreeNode root = new TreeNode(preorder[0]);

        int indexOfRoot = -1;
        for (int i = 0; i < inorder.Length; i++)
        {
            if (inorder[i] == preorder[0])
            {
                indexOfRoot = i;
                break;
            }
        }
        if (indexOfRoot == -1)
            return null;

        var leftInorder = inorder.Take(indexOfRoot).ToArray();
        var rightInorder = inorder.Skip(indexOfRoot + 1).ToArray();

        int leftCount = leftInorder.Length;
        if (1 + leftCount > preorder.Length) leftCount = preorder.Length - 1;

        var leftPreorder = preorder.Skip(1).Take(leftCount).ToArray();
        var rightPreorder = preorder.Skip(1 + leftCount).ToArray();
        root.left = BuildTree(leftPreorder, leftInorder);
        root.right = BuildTree(rightPreorder, rightInorder);
        return root;
    }
    public TreeNode BstFromPreorder(int[] preorder)
    {
        if (preorder == null || preorder.Length == 0)
            return null;
        TreeNode root = new TreeNode(preorder[0]);
        int indexOfGreaterThanRoot = -1;
        for (int i = 1; i < preorder.Length; i++)
        {
            if (preorder[i] > preorder[0])
            {
                indexOfGreaterThanRoot = i;
                break;
            }
        }
        if (indexOfGreaterThanRoot == -1)
        {
            var leftV = preorder.Skip(1).ToArray();
            root.left = BstFromPreorder(leftV);
            return root;
        }
        var left = preorder.Skip(1).Take(indexOfGreaterThanRoot - 1).ToArray();
        var right = preorder.Skip(indexOfGreaterThanRoot).ToArray();
        root.left = BstFromPreorder(left);
        root.right = BstFromPreorder(right);
        return root;
    }
    public void Solve(char[][] board)
    {
        if (board == null)
            return;
        int regionCount = 0;
        int[][] dirs = new int[4][];
        dirs[0] = new int[] { 0, 1 };
        dirs[1] = new int[] { 1, 0 };
        dirs[2] = new int[] { 0, -1 };
        dirs[3] = new int[] { -1, 0 };

        int r = board.Length, c = board[0].Length;
        bool isOnSide(int v, int b)
        {
            if (v == 0 || v == r - 1)
                return true;
            if (b == 0 || b == c - 1)
                return true;
            return false;
        }
        void MarkAsTemp(int x, int y)
        {
            if (x < 0 || x >= r || y < 0 || y >= c || board[x][y] != 'O')
                return;
            board[x][y] = 'T';
            foreach (var dir in dirs)
            {

                MarkAsTemp(dir[0] + x, dir[1] + y);
            }
        }
        for (int i = 0; i < r; i++)
        {
            for (int j = 0; j < c; j++)
            {
                if (isOnSide(i, j) && board[i][j] == 'O')
                    MarkAsTemp(i, j);
            }
        }
        for (int i = 0; i < r; i++)
        {
            for (int j = 0; j < c; j++)
            {
                if (board[i][j] == 'O')
                    board[i][j] = 'X';
                else if (board[i][j] == 'T')
                    board[i][j] = 'O';
            }
        }
    }
    public int Trap(int[] height)
    {
        if (height == null || height.Length == 0)
            return 0;
        int sum = 0;
        int i = 0, j = height.Length - 1;
        int maxL = height[i], maxR = height[j];
        while (i < j)
        {
            if (maxL <= maxR)
            {
                i++;
                maxL = Math.Max(maxL, height[i]);
                sum += maxL - height[i];
            }
            else
            {
                j--;
                maxR = Math.Max(maxR, height[j]);
                sum += maxR - height[j];
            }
        }
        return sum;
    }
    public string Tree2str(TreeNode root)
    {
        if (root == null)
            return "";
        string r = $"{root.val}";
        string left = Tree2str(root.left);
        string right = Tree2str(root.right);
        if (left == "" && right == "")
            return r;
        if (left == "")
            return $"{r}()({right})";
        if (right == "")
            return $"{r}({left})";
        return $"{r}({left})({right})";
    }
    public int[] SortedSquares(int[] nums)
    {
        if (nums == null || nums.Length == 0)
            return Array.Empty<int>();
        List<int> l = new List<int>();
        foreach (var num in nums)
        {
            l.Add(num * num);
        }
        return l.Order().ToArray();
    }
    public int OpenLock(string[] deadends, string target)
    {
        if (deadends == null || string.IsNullOrEmpty(target))
            return -1;
        if (deadends.Contains("0000"))
            return -1;
        Queue<(string, int)> q = new Queue<(string, int)>();
        q.Enqueue(("0000", 0));
        HashSet<string> visited = new HashSet<string>(deadends);
        while (q.Count != 0)
        {
            var it = q.Dequeue();
            if (it.Item1 == target)
                return it.Item2;
            foreach (var item in Children(it.Item1))
            {
                if (!visited.Contains(item))
                {
                    visited.Add(item);
                    q.Enqueue((item, it.Item2 + 1));
                }

            }
        }
        return -1;
        List<string> Children(string lockStr)
        {
            var res = new List<string>();
            for (int i = 0; i < 4; i++)
            {
                int digit = lockStr[i] - '0';
                string up = lockStr.Substring(0, i) +
                    ((digit + 1) % 10) + lockStr.Substring(i + 1);
                string down = lockStr.Substring(0, i) +
                    ((digit + 9) % 10) + lockStr.Substring(i + 1);
                res.Add(up);
                res.Add(down);
            }
            return res;
        }
    }
    public bool CanFinish(int numCourses, int[][] prerequisites)
    {
        if (numCourses <= 0 || prerequisites == null)
            return false;
        Dictionary<int, List<int>> adjMap = new Dictionary<int, List<int>>();
        for (int i = 0; i < numCourses; i++)
        {
            adjMap[i] = new List<int>();
        }
        foreach (var item in prerequisites)
        {
            adjMap[item[1]].Add(item[0]);
        }
        HashSet<int> visited = new HashSet<int>();
        bool Dfs(int curr)
        {
            if (visited.Contains(curr))
                return false;
            if (adjMap[curr].Count == 0)
                return true;

            visited.Add(curr);
            foreach (var item in adjMap[curr])
            {
                if (!Dfs(item))
                    return false;
            }
            visited.Remove(curr);
            adjMap[curr] = new List<int>();
            return true;
        }
        for (int i = 0; i < numCourses; i++)
        {
            if (!Dfs(i))
                return false;
        }
        return true;
    }
    public class TimeMap
    {
        Dictionary<string, SortedList<int, string>> map;
        public TimeMap()
        {
            map = new Dictionary<string, SortedList<int, string>>();
        }
        public void Set(string key, string value, int timestamp)
        {
            if (!map.ContainsKey(key))
            {
                map[key] = new SortedList<int, string>();
            }
            if (!map[key].ContainsKey(timestamp))
            {
                map[key][timestamp] = value;
            }
        }

        public string Get(string key, int timestamp)
        {
            if (!map.ContainsKey(key))
                return "";
            var dMap = map[key];
            var keys = dMap.Keys;
            if (dMap.ContainsKey(timestamp))
                return dMap[timestamp];
            int i = 0, j = dMap.Count - 1;
            string res = "";
            while (i <= j)
            {
                int mid = (i + j) / 2;
                if (keys[mid] <= timestamp)
                {
                    res = dMap[keys[mid]];
                    i = mid + 1;
                }
                else
                {
                    j = mid - 1;
                }
            }
            return res;
        }
    }
    public int[] FindRedundantConnection(int[][] edges)
    {
        int[] parent = new int[edges.Length + 1];
        for (int i = 0; i < parent.Length; i++)
        {
            parent[i] = i;
        }
        int FindRoot(int x)
        {
            if (parent[x] != x)
                return FindRoot(parent[x]);
            return x;
        }
        bool Union(int x, int y)
        {
            if (FindRoot(x) == FindRoot(y))
                return false;
            parent[FindRoot(y)] = FindRoot(x);
            return true;
        }
        int[] res = new int[2];
        foreach (var item in edges)
        {
            bool temp = Union(item[0], item[1]);
            if (!temp)
            {
                res[0] = item[0];
                res[1] = item[1];
            }
        }
        return res;
    }
    public class UnionFind
    {
        private int[] parent;
        public UnionFind(int n)
        {
            parent = new int[n];
            for (int i = 0; i < n; i++)
            {
                parent[i] = i;
            }
        }
        public int Find(int x)
        {
            if (parent[x] != x)
                return Find(parent[x]);
            return x;
        }
        public bool Union(int x, int y)
        {
            if (Find(x) == Find(y))
                return false;
            parent[Find(y)] = Find(x);
            return true;
        }
    }
    public IList<IList<string>> AccountsMerge(IList<IList<string>> accounts)
    {
        if (accounts == null)
            return new List<IList<string>>();
        UnionFind uf = new UnionFind(accounts.Count);
        Dictionary<string, int> emailToAcc = new Dictionary<string, int>();

        for (int i = 0; i < accounts.Count; i++)
        {
            for (int j = 1; j < accounts[i].Count; j++)
            {
                string email = accounts[i][j];
                if (emailToAcc.ContainsKey(email))
                {
                    uf.Union(i, emailToAcc[email]);
                }
                else
                {
                    emailToAcc[email] = i;
                }
            }
        }
        Dictionary<int, List<string>> emailGroup = new Dictionary<int, List<string>>();
        foreach (var kvp in emailToAcc)
        {
            string email = kvp.Key;
            int leader = uf.Find(kvp.Value);
            if (!emailGroup.ContainsKey(leader))
            {
                emailGroup[leader] = new List<string>();
            }
            emailGroup[leader].Add(email);
        }
        List<IList<string>> res = new List<IList<string>>();
        foreach (var x in emailGroup)
        {
            int accId = x.Key;
            List<string> emails = x.Value;
            emails.Sort(StringComparer.Ordinal);
            List<string> merged = new List<string> { accounts[accId][0] };
            merged.AddRange(emails);
            res.Add(merged);
        }
        return res;
    }
    public int MaxSubarraySumCircular(int[] nums)
    {
        if (nums == null || nums.Length == 0)
            return 0;
        int total = 0;
        int currMin = 0, currMax = 0;
        int min = nums[0], max = nums[0];
        foreach (int i in nums)
        {
            total += i;
            currMin = Math.Min(i, currMin + i);
            currMax = Math.Max(i, currMax + i);
            min = Math.Min(min, currMin);
            max = Math.Max(max, currMax);
        }
        return max < 0 ? max : Math.Max(max, total - min);
    }
    public int NumDecodings(string s, int index = 0, Dictionary<int, int>? memo = null)
    {
        memo ??= new Dictionary<int, int>();

        if (index == s.Length)
            return 1;
        if (s[index] == '0')
            return 0;
        if (memo.ContainsKey(index))
            return memo[index];

        int count = NumDecodings(s, index + 1, memo);
        if (index + 1 < s.Length)
        {
            int ft = int.Parse(s.Substring(index, 2));
            if (ft >= 10 && ft <= 26)
                count += NumDecodings(s, index + 2, memo);
        }
        memo[index] = count;
        return memo[index];
    }
    public int CoinChange(int[] coins, int amount, Dictionary<int, int>? memo = null)
    {
        memo ??= new Dictionary<int, int>();
        if (coins == null || coins.Length == 0 || amount < 0)
            return -1;
        if (amount == 0)
            return 0;
        if (memo.ContainsKey(amount))
            return memo[amount];

        int min = int.MaxValue;
        for (int i = 0; i < coins.Length; i++)
        {
            int coin = CoinChange(coins, amount - coins[i], memo);
            if (coin >= 0 && coin < min)
                min = coin + 1;
        }
        memo[amount] = (min == int.MaxValue) ? -1 : min;
        return memo[amount];
    }
    public int[] FindOrder(int numCourses, int[][] prerequisites)
    {
        if (prerequisites == null || numCourses <= 0)
            return Array.Empty<int>();
        var adjList = new Dictionary<int, List<int>>();
        var indegrees = new int[numCourses];
        // key-prerequisite, value-what gets unlocked
        for (int i = 0; i < numCourses; i++)
        {
            adjList[i] = new List<int>();
        }
        foreach (var item in prerequisites)
        {
            int val = item[0];
            int key = item[1];
            adjList[key].Add(val);
            indegrees[val]++;
        }
        Queue<int> q = new Queue<int>();
        for (int i = 0; i < numCourses; i++)
        {
            if (indegrees[i] == 0)
                q.Enqueue(i);
        }
        List<int> ord = new List<int>();
        while (q.Count > 0)
        {
            int temp = q.Dequeue();
            ord.Add(temp);
            foreach (var depended in adjList[temp])
            {
                indegrees[depended]--;
                if (indegrees[depended] == 0)
                    q.Enqueue(depended);
            }
        }
        return ord.Count == numCourses ? ord.ToArray() : Array.Empty<int>();
    }
    public IList<bool> CheckIfPrerequisite(int numCourses, int[][] prerequisites, int[][] queries)
    {
        if (prerequisites == null || queries == null)
            return new List<bool>();
        var adjList = new Dictionary<int, List<int>>();
        for (int i = 0; i < numCourses; i++)
            adjList[i] = new List<int>();
        foreach (var item in prerequisites)
        {
            var key = item[0];
            var val = item[1];
            adjList[key].Add(val);
        }
        bool Bfs(int curr, int toReach)
        {
            HashSet<int> set = new HashSet<int>();
            Queue<int> q = new Queue<int>();
            set.Add(curr);
            q.Enqueue(curr);
            while (q.Count > 0)
            {
                var c = q.Dequeue();
                if (c == toReach)
                    return true;
                foreach (var neighbour in adjList[c])
                {
                    if (!set.Contains(neighbour))
                    {
                        set.Add(neighbour);
                        q.Enqueue(neighbour);
                    }
                }
            }
            return false;
        }
        List<bool> bools = new List<bool>();
        foreach (var item in queries)
        {
            int pre = item[0];
            int reach = item[1];
            bools.Add(Bfs(pre, reach));
        }
        return bools;
    }
    public TreeNode LowestCommonAncestor1(TreeNode root, TreeNode p, TreeNode q)
    {
        if (root == null)
            return null;
        bool Contains(TreeNode node, int target)
        {
            if (node == null)
                return false;
            if (node.val == target)
                return true;
            return Contains(node.left, target) || Contains(node.right, target);
        }
        if (root == p && Contains(p, q.val))
            return p;
        if (root == q && Contains(q, p.val))
            return q;

        if (Contains(root.left, p.val) && Contains(root.left, q.val))
            return LowestCommonAncestor1(root.left, p, q);
        else if (Contains(root.right, p.val) && Contains(root.right, q.val))
            return LowestCommonAncestor1(root.right, p, q);
        else if (Contains(root, p.val) && Contains(root, q.val))
            return root;
        return null;
    }
    public string LexSmallest(string s)
    {
        if (string.IsNullOrEmpty(s))
            return null;

        string smalles = s;
        for (int i = 0; i < s.Length; i++)
        {
            string pref = new string(s.Substring(0, i).Reverse().ToArray()) + s.Substring(i);

            string suf = s.Substring(0, s.Length - i) + new string(s.Substring(s.Length - i).Reverse().ToArray());

            smalles = smalles.CompareTo(pref) < 0 ? smalles : pref;
            smalles = smalles.CompareTo(suf) < 0 ? smalles : suf;
        }
        return smalles;
    }
    public string MaxSumOfSquares(int num, int sum)
    {
        if (num < 0 || sum < 0)
            return "";
        if (sum > num * 9)
            return "";
        double score = 0;
        StringBuilder s = new StringBuilder();

        while (num != 0)
        {
            int big = 9;
            while (big > sum)
            {
                big--;
            }
            sum -= big;
            score += big * big;
            num--;
            s.Append(big);
        }
        return s.ToString();
    }
    public long MaxAlternatingSum(int[] nums)
    {
        if (nums == null || nums.Length == 0)
            return 0;
        for (int x = 0; x < nums.Length; x++)
        {
            if (nums[x] < 0) nums[x] = -1 * nums[x];
        }
        Array.Sort(nums);
        int i = 0, j = nums.Length - 1;
        long sum = 0;
        while (i <= j)
        {
            if (i != j)
                sum += (nums[j] * nums[j--]) - (nums[i] * nums[i++]);
            else
                sum += nums[j] * nums[j--];
        }
        return sum;
    }
    public double[] CalcEquation(IList<IList<string>> equations, double[] values, IList<IList<string>> queries)
    {
        if (equations == null || values == null || queries == null)
            return Array.Empty<double>();
        Dictionary<string, List<(string den, double val)>> adjList
            = new Dictionary<string, List<(string den, double x)>>();
        for (int i = 0; i < equations.Count; i++)
        {
            string en = equations[i][0];
            string de = equations[i][1];
            double value = values[i];
            if (!adjList.ContainsKey(en))
            {
                adjList[en] = new List<(string, double)>();
            }
            adjList[en].Add((de, value));
            if (!adjList.ContainsKey(de))
            {
                adjList[de] = new List<(string, double)>();
            }
            adjList[de].Add((en, 1 / value)); // constructed adjacencyList with bi-directions.
        }
        double Dfs(string source, string targ, HashSet<string> visited)
        {
            if (!adjList.ContainsKey(source) || !adjList.ContainsKey(targ))
                return -1;
            if (source == targ)
                return 1;
            visited.Add(source);
            foreach (var (denum, weight) in adjList[source])
            {
                if (!visited.Contains(denum))
                {
                    double res = Dfs(denum, targ, visited);
                    if (res != -1)
                        return weight * res;
                }
            }
            return -1;
        }
        double[] res = new double[queries.Count];
        for (int i = 0; i < queries.Count; i++)
        {
            string e = queries[i][0];
            string d = queries[i][1];
            HashSet<string> vis = new HashSet<string>();
            res[i] = Dfs(e, d, vis);
        }
        return res;
    }
    public int MaxProduct(int[] nums)
    {
        if (nums == null || nums.Length == 0)
            return 0;
        int max = nums.Max();
        int currMax = 1, currMin = 1;
        foreach (var item in nums)
        {
            int temp = currMax * item;
            currMax = Math.Max(Math.Max(currMax * item, item), currMin * item);
            currMin = Math.Min(Math.Min(temp, item), currMin * item);
            max = Math.Max(max, currMax);
        }
        return max;
    }
    public static bool WordBreak(string s, IList<string> wordDict, Dictionary<string, bool>? memo = null)
    {
        memo ??= new Dictionary<string, bool>();
        if (string.IsNullOrEmpty(s))
            return true;
        if (memo.ContainsKey(s))
            return memo[s];
        foreach (var str in wordDict)
        {
            if (s.StartsWith(str))
            {
                bool res = WordBreak(s.Substring(str.Length), wordDict, memo);
                if (res)
                {
                    memo[s] = true;
                    return true;
                }
            }
        }
        memo[s] = false;
        return memo[s];
    }
    public int LengthOfLIS(int[] nums)
    {
        if (nums == null || nums.Length == 0)
            return 0;
        int[] tab = new int[nums.Length];
        for (int i = nums.Length - 1; i >= 0; i--)
        {
            int j = i + 1;
            int res = 1;
            while (j != nums.Length)
            {
                if (nums[i] < nums[j])
                    res = Math.Max(res, tab[j] + 1);
                j++;
            }
            tab[i] = res;
        }
        return tab.Max();
    }
    public IList<IList<int>> CombinationSum3(int k, int n)
    {
        if (k < 0)
            return new List<IList<int>>();
        List<IList<int>> lists = new List<IList<int>>();
        void Recursion(int curr, int targ, List<int> currentList)
        {
            if (currentList.Count == k && targ == 0)
            {
                lists.Add(new List<int>(currentList));
                return;
            }
            if (curr > targ)
                return;

            for (int i = curr + 1; i < 10; i++)
            {
                currentList.Add(i);
                Recursion(i + 1, targ - i, currentList);
                currentList.RemoveAt(currentList.Count - 1);
            }
        }
        Recursion(1, n, new List<int>());
        return lists;
    }
    public int CombinationSum4(int[] nums, int target)
    {
        if (nums == null || nums.Length == 0)
            return 0;
        int[] dp = new int[target + 1];
        dp[0] = 1;
        for (int i = 1; i < dp.Length; i++)
        {
            foreach (var n in nums)
            {
                dp[i] += i - n < 0 ? 0 : dp[i - n];
            }
        }
        return dp[target];
    }
    public int NumSquares(int n)
    {
        if (n < 0)
            return -1;
        int[] dp = new int[n + 1];
        Array.Fill(dp, n + 1);
        dp[0] = 0;
        for (int i = 1; i <= n; i++)
        {
            for (int j = 1; j * j <= i; j++)
            {
                int sq = j * j;
                if (i - sq < 0)
                    break;
                dp[i] = Math.Min(dp[i], 1 + dp[i - sq]);
            }
        }
        return dp[n];
    }
    public int SmallestNumber(int n)
    {
        if (n <= 0)
            return 0;
        int num = 1;
        while (num <= n)
        {
            num *= 2;
        }
        return num - 1;
    }
    public int LastStoneWeightII(int[] stones)
    {
        if (stones == null || stones.Length == 0)
            return -1;
        int sum = stones.Sum();
        int targ = (sum + 1) / 2;

        int[][] dp = new int[stones.Length][];
        for (int i = 0; i < dp.Length; i++)
        {
            dp[i] = new int[targ + 1];
            for (int j = 0; j < dp[i].Length; j++)
            {
                dp[i][j] = -1;
            }
        }
        int Dfs(int i, int total)
        {
            if (total >= targ || i == stones.Length)
            {
                return Math.Abs(total - (sum - total));
            }
            if (dp[i][total] != -1)
            {
                return dp[i][total];
            }
            dp[i][total] = Math.Min(Dfs(i + 1, total), Dfs(i + 1, total + stones[i]));
            return dp[i][total];
        }
        return Dfs(0, 0);
    }
    public int Change(int amount, int[] coins)
    {
        if (amount < 0 || coins == null)
            return -1;

        int[] tabul = new int[amount + 1];
        tabul[0] = 1;
        foreach (var coin in coins)
        {
            for (int i = coin; i <= amount; i++)
            {
                tabul[i] += tabul[i - coin];
            }
        }
        return tabul[amount];
    }
    public int FindTargetSumWays(int[] nums, int target)
    {
        if (nums == null || nums.Length == 0)
            return 0;
        Dictionary<(int, int), int> memo = new Dictionary<(int, int), int>();

        int Dfs(int index, int targ)
        {
            if (memo.ContainsKey((index, targ)))
            {
                return memo[(index, targ)];
            }
            if (index == nums.Length)
            {
                return targ == 0 ? 1 : 0;
            }

            int add = Dfs(index + 1, targ - nums[index]);
            int sub = Dfs(index + 1, targ + nums[index]);
            memo[(index, targ)] = add + sub;
            return memo[(index, targ)];
        }
        return Dfs(0, target);
    }
    public bool IsInterleave(string s1, string s2, string s3)
    {
        if (s1.Length + s2.Length != s3.Length)
            return false;
        bool?[,] dp = new bool?[s1.Length + 1, s2.Length + 1];
        dp[s1.Length, s2.Length] = true;
        bool canForm(int i, int j)
        {
            if (i + j == s3.Length)
            {
                return i == s1.Length && j == s2.Length;
            }
            if (dp[i, j].HasValue)
                return dp[i, j].Value;
            bool res = false;
            if (i < s1.Length && s1[i] == s3[i + j])
            {
                res = canForm(i + 1, j);
            }
            if (!res && j < s2.Length && s2[j] == s3[i + j])
            {
                res = canForm(i, j + 1);
            }
            dp[i, j] = res;
            return res;
        }
        return canForm(0, 0);
    }
    public bool StoneGame(int[] piles)
    {
        if (piles == null || piles.Length == 0)
            return false;
        Dictionary<(int, int), int> memo = new Dictionary<(int, int), int>();
        int Dfs(int l, int r)
        {
            if (l > r)
                return 0;
            if (memo.ContainsKey((l, r)))
                return memo[(l, r)];

            bool even = (r - l) % 2 == 0;
            int left = even ? piles[l] : 0;
            int right = even ? piles[r] : 0;
            memo[(l, r)] = Math.Max(Dfs(l + 1, r) + left, Dfs(l, r - 1) + right);
            return memo[(l, r)];
        }
        int sum = piles.Sum();
        return Dfs(0, piles.Length - 1) > sum / 2;
    }
    public int MaxTurbulenceSize(int[] arr)
    {
        if (arr == null || arr.Length == 0)
            return 0;
        int res = 0, count = 0;
        int sign = -1;
        for (int i = 0; i < arr.Length - 1; i++)
        {
            if (arr[i] > arr[i + 1])
            {
                count = (sign == 0) ? count + 1 : 1;
                sign = 1;
            }
            else if (arr[i] < arr[i + 1])
            {
                count = (sign == 1) ? count + 1 : 1;
                sign = 0;
            }
            else
            {
                count = 0;
                sign = -1;
            }
            res = Math.Max(res, count);
        }
        return res + 1;
    }
    public int PathSumIII(TreeNode root, int targetSum)
    {
        if (root == null)
            return 0;
        int count = 0;
        int Dfs(TreeNode node, long sum)
        {
            if (node == null)
                return 0;

            int count = (node.val == sum) ? 1 : 0;
            count += Dfs(node.left, sum - node.val);
            count += Dfs(node.right, sum - node.val);
            return count;
        }
        return Dfs(root, targetSum)
            + PathSumIII(root.left, targetSum)
            + PathSumIII(root.right, targetSum);
    }
    public int ThreeSumClosest(int[] nums, int target)
    {
        if (nums == null || nums.Length == 0)
            return 0;
        Array.Sort(nums);
        int sum = nums[0] + nums[1] + nums[2];

        for (int i = 0; i < nums.Length; i++)
        {
            int l = i + 1, r = nums.Length - 1;
            while (l < r)
            {
                int temp = nums[i] + nums[l] + nums[r];
                if (temp == target)
                    return target;
                else if (temp > target)
                {
                    r--;
                }
                else
                {
                    l++;
                }
                if (Math.Abs(target - temp) < Math.Abs(target - sum))
                    sum = temp;
            }
        }
        return sum;
    }
    public bool CanJump(int[] nums)
    {
        // greedy is the best
        if (nums == null || nums.Length == 0)
            return false;
        int g = nums.Length - 1;
        for (int i = nums.Length - 1; i >= 0; i--)
        {
            if (i + nums[i] >= g)
                g = i;
        }
        return g == 0 ? true : false;
    }
    public int Jump(int[] nums)
    {
        if (nums == null || nums.Length == 0)
            return 0;
        int res = 0;
        int l = 0, r = 0;
        while (r < nums.Length - 1)
        {
            int far = 0;
            int i = l;
            while (i <= r)
            {
                far = Math.Max(far, i + nums[i]);
                i++;
            }
            l = r + 1;
            r = far;
            res++;
        }
        return res;
    }
    public int CanCompleteCircuit(int[] gas, int[] cost)
    {
        if (gas == null || cost == null)
            return -1;
        if (gas.Sum() < cost.Sum())
            return -1;
        int[] diffs = new int[gas.Length];
        for (int i = 0; i < gas.Length; i++)
        {
            diffs[i] = gas[i] - cost[i];
        }
        int total = 0;
        int res = 0;
        for (int i = 0; i < gas.Length; i++)
        {
            total += diffs[i];
            if (total < 0)
            {
                total = 0;
                res = i + 1;
            }
        }
        return res;
    }
    public double MyPow(double x, int n)
    {
        if (x == 0)
            return 0;
        if (n == 0)
            return 1;
        double Helper(double x, long n)
        {
            if (n == 0)
                return 1;
            double half = Helper(x, n / 2);
            return n % 2 == 1 ? x * half * half : half * half;
        }
        double result = Helper(x, Math.Abs((long)n));
        if (n < 0)
            result = 1 / result;
        return result;
    }
    public bool IsNStraightHand(int[] hand, int groupSize)
    {
        if (hand == null || hand.Length % groupSize != 0)
            return false;
        Array.Sort(hand);
        Dictionary<int, int> d = new Dictionary<int, int>();
        foreach (int i in hand)
        {
            if (!d.ContainsKey(i))
                d[i] = 0;
            d[i]++;
        }
        foreach (int card in hand)
        {
            if (d[card] == 0)
                continue;
            for (int i = 0; i < groupSize; i++)
            {
                int next = card + i;
                if (!d.ContainsKey(next) || d[next] == 0)
                    return false;
                d[next]--;
            }
        }
        return true;
    }
    public int LongestIncreasingPath(int[][] matrix)
    {
        if (matrix == null)
            return 0;
        int rows = matrix.Length;
        int cols = matrix[0].Length;
        int[][] dp = new int[rows][];

        for (int i = 0; i < dp.Length; i++)
        {
            dp[i] = new int[cols];
        }

        int Bfs(int i, int j)
        {
            if (i < 0 || j < 0 || i >= rows || j >= cols)
                return 0;
            if (dp[i][j] != 0)
                return dp[i][j];
            int res = 0;
            int[][] directions = new int[][]
            {
                    new int[] { -1, 0 },
                    new int[] { 1, 0 },
                    new int[] { 0, -1 },
                    new int[] { 0, 1 }
            };
            foreach (var item in directions)
            {
                int x = item[0] + i;
                int y = item[1] + j;
                if (x >= 0 && y >= 0 && x < rows && y < cols && matrix[x][y] > matrix[i][j])
                {
                    res = Math.Max(res, Bfs(x, y));
                }
            }
            dp[i][j] = 1 + res;
            return dp[i][j];
        }
        int max = 0;
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                max = Math.Max(max, Bfs(i, j));
            }
        }
        return max;
    }
    public int MinimumEffortPath(int[][] heights)
    {
        if (heights == null || heights.Length == 0)
            return 0;
        var pq = new PriorityQueue<(int diff, int r, int c), int>();
        pq.Enqueue((0, 0, 0), 0);
        var visited = new HashSet<(int, int)>();
        int[][] directions = new int[][]
        {
                    new int[] { -1, 0 },
                    new int[] { 1, 0 },
                    new int[] { 0, -1 },
                    new int[] { 0, 1 }
        };

        while (pq.Count != 0)
        {
            var current = pq.Dequeue();
            int diff = current.diff, r = current.r, c = current.c;

            if (visited.Contains((r, c)))
                continue;
            visited.Add((r, c));
            if (r == heights.Length - 1 && c == heights[0].Length - 1)
            {
                return diff;
            }

            foreach (var dir in directions)
            {
                int x = r + dir[0];
                int y = c + dir[1];

                if (x < 0 || y < 0 || x >= heights.Length || y >= heights[0].Length || visited.Contains((x, y)))
                {
                    continue;
                }

                int z = Math.Max(diff, Math.Abs(heights[r][c] - heights[x][y]));
                pq.Enqueue((z, x, y), z);
            }
        }
        return 0;
    }
    public ListNode SwapPairs(ListNode head)
    {
        if (head == null || head.next == null)
            return head;
        ListNode nn = head.next.next ?? null;
        ListNode temp = head;
        head = temp.next;
        head.next = temp;
        head.next.next = SwapPairs(nn);
        return head;
    }
    public ListNode RotateRight(ListNode head, int k)
    {
        if (head == null || k <= 0)
            return head;
        int count = 1;
        ListNode tail = head;
        while (tail.next != null)
        {
            count++;
            tail = tail.next;
        }
        tail.next = head;

        int steps = count - (k % count);
        if (steps == count)
        {
            tail.next = null;
            return head;
        }
        ListNode temp = head;

        while (steps > 1)
        {
            temp = temp.next;
            steps--;
        }
        ListNode newHead = temp.next;
        temp.next = null;
        return newHead;
    }
    public IList<IList<int>> PacificAtlantic(int[][] heights)
    {
        if (heights == null || heights.Length == 0)
            return new List<IList<int>>();
        int[][] directions = new int[][]
            {
                    new int[] { -1, 0 },
                    new int[] { 1, 0 },
                    new int[] { 0, -1 },
                    new int[] { 0, 1 }
            };
        int r = heights.Length;
        int c = heights[0].Length;
        int[][] pac = new int[r][];
        int[][] atl = new int[r][];
        for (int i = 0; i < r; i++)
        {
            pac[i] = new int[c];
            atl[i] = new int[c];
        }
        void Dfs(int i, int j, int[][] ocean)
        {
            if (ocean[i][j] == 1)
                return;

            ocean[i][j] = 1;
            foreach (var dir in directions)
            {
                int x = dir[0] + i;
                int y = dir[1] + j;
                if (x < 0 || y < 0 || x >= r || y >= c)
                    continue;
                if (heights[i][j] <= heights[x][y])
                    Dfs(x, y, ocean);
            }
        }
        for (int i = 0; i < r; i++)
            Dfs(i, 0, pac);
        for (int j = 0; j < c; j++)
            Dfs(0, j, pac);

        for (int i = 0; i < r; i++)
            Dfs(i, c - 1, atl);
        for (int j = 0; j < c; j++)
            Dfs(r - 1, j, atl);
        List<IList<int>> result = new List<IList<int>>();

        for (int i = 0; i < r; i++)
        {
            for (int j = 0; j < c; j++)
            {
                if (atl[i][j] == 1 && pac[i][j] == 1)
                    result.Add(new List<int> { i, j });
            }
        }
        return result;
    }
    public ListNode SortList(ListNode head)
    {
        if (head == null)
            return null;
        List<int> l = new List<int>();
        while (head != null)
        {
            l.Add(head.val);
            head = head.next;
        }
        l.Sort();
        ListNode r = new ListNode(l[0]);
        ListNode temp = r;
        for (int i = 1; i < l.Count; i++)
        {
            temp.next = new ListNode(l[i]);
            temp = temp.next;
        }
        return r;
    }
    public int NetworkDelayTime(int[][] times, int n, int k)
    {
        if (times == null || n < 0)
            return -1;
        var adj = new Dictionary<int, List<int[]>>();
        foreach (var t in times)
        {
            if (!adj.ContainsKey(t[0]))
            {
                adj[t[0]] = new List<int[]>();
            }
            adj[t[0]].Add(new int[] { t[1], t[2] });
        }
        var d = new Dictionary<int, int>();
        for (int i = 1; i <= n; i++)
            d[i] = int.MaxValue;

        void Dfs(int node, int time)
        {
            if (!d.ContainsKey(node))
                return;
            if (time >= d[node])
                return;
            d[node] = time;
            if (!adj.ContainsKey(node))
                return;
            foreach (var item in adj[node])
            {
                Dfs(item[0], item[1] + time);
            }
        }
        Dfs(k, 0);
        int res = d.Values.Max();
        return res != int.MaxValue ? res : -1;
    }
    public IList<int> FindMinHeightTrees(int n, int[][] edges)
    {
        if (n == 1)
            return new List<int> { 0 };
        var adjList = new Dictionary<int, List<int>>();
        foreach (var item in edges)
        {
            int n1 = item[0];
            int n2 = item[1];
            if (!adjList.ContainsKey(n1)) adjList[n1] = new List<int>();
            adjList[n1].Add(n2);
            if (!adjList.ContainsKey(n2)) adjList[n2] = new List<int>();
            adjList[n2].Add(n1);
        }

        var edgeCnt = new Dictionary<int, int>();
        var leaves = new Queue<int>();
        foreach (var item in adjList)
        {
            edgeCnt[item.Key] = item.Value.Count;
            if (item.Value.Count == 1)
                leaves.Enqueue(item.Key);
        }
        while (n > 2)
        {
            int cnt = leaves.Count;
            n -= cnt;
            for (int i = 0; i < cnt; i++)
            {
                var node = leaves.Dequeue();
                foreach (var neighbour in adjList[node])
                {
                    edgeCnt[neighbour]--;
                    if (edgeCnt[neighbour] == 1)
                        leaves.Enqueue(neighbour);
                }
            }
        }
        return new List<int>(leaves);
    }
    public class WordDictionary
    {
        class Trienode
        {
            public Trienode[] nodes = new Trienode[26];
            public bool end = false;
        }
        Trienode root;
        public WordDictionary()
        {
            root = new Trienode();
        }

        public void AddWord(string word)
        {
            Trienode cur = root;
            foreach (var c in word)
            {
                if (cur.nodes[c - 'a'] == null)
                    cur.nodes[c - 'a'] = new Trienode();
                cur = cur.nodes[c - 'a'];
            }
            cur.end = true;
        }

        public bool Search(string word)
        {
            bool Dfs(int j, Trienode node)
            {
                for (int i = j; i < word.Length; i++)
                {
                    if (word[i] == '.')
                    {
                        foreach (var item in node.nodes)
                        {
                            if (item != null && Dfs(i + 1, item))
                                return true;
                        }
                        return false;
                    }
                    else
                    {
                        if (node.nodes[word[i] - 'a'] == null)
                            return false;
                        node = node.nodes[word[i] - 'a'];
                    }
                }
                return node.end;
            }
            return Dfs(0, root);
        }
    }
    // Contest 2
    public int MinMoves(int[] nums)
    {
        if (nums == null || nums.Length == 0)
            return 0;
        int res = 0;
        int max = nums.Max();
        foreach (var num in nums)
        {
            res += Math.Abs(num - max);
        }
        return res;
    }
    public int CountMajoritySubarrays(int[] nums, int target)
    {
        int total = 0;

        for (int i = 0; i < nums.Length; i++)
        {
            int countT = 0;
            int countN = 0;
            for (int j = i; j < nums.Length; j++)
            {
                if (nums[j] == target)
                    countT++;
                else
                    countN++;
            }
            if (countT > countN)
                total++;
        }
        return total;
    }
    public int Rob(TreeNode root)
    {
        if (root == null)
            return 0;
        Dictionary<TreeNode, int> memo = new Dictionary<TreeNode, int>();
        int Recursion(TreeNode node)
        {
            if (node == null)
                return 0;
            if (memo.ContainsKey(node))
                return memo[node];
            int res = node.val;
            if (node.left != null)
            {
                res += Recursion(node.left.left) + Recursion(node.left.right);
            }
            if (node.right != null)
            {
                res += Recursion(node.right.left) + Recursion(node.right.right);
            }
            memo[node] = Math.Max(res, Recursion(node.left) + Recursion(node.right));
            return memo[node];
        }
        return Recursion(root);
    }
    public int MaxPathSum(TreeNode root)
    {
        if (root == null)
            return 0;
        int max = int.MinValue;
        Dictionary<TreeNode, int> memo = new Dictionary<TreeNode, int>();
        int Dfs(TreeNode node)
        {
            if (node == null)
                return 0;
            if (memo.ContainsKey(node))
                return memo[node];

            int left = Math.Max(0, Dfs(node.left));
            int right = Math.Max(0, Dfs(node.right));

            max = Math.Max(max, node.val + left + right);
            memo[node] = node.val + Math.Max(left, right);
            return memo[node];
        }
        Dfs(root);
        return max;
    }
    public class Codec
    {
        public string serialize(TreeNode root)
        {
            if (root == null)
                return "[]";
            List<string> l = new List<string>();
            Queue<TreeNode?> q = new Queue<TreeNode?>();

            q.Enqueue(root);
            while (q.Count != 0)
            {
                var item = q.Dequeue();
                if (item == null)
                {
                    l.Add("null");
                    continue;
                }
                l.Add(item.val.ToString());
                q.Enqueue(item.left);
                q.Enqueue(item.right);
            }

            int last = l.Count - 1;
            while (last >= 0 && l[last] == "null")
                last--;
            return "[" + string.Join(",", l.Take(last + 1)) + "]";
        }
        public TreeNode deserialize(string data)
        {
            if (data == null || data == "[]")
                return null;

            data = data.Substring(1, data.Length - 2);

            List<string> l = data.Split(new string[] { "," }, StringSplitOptions.None).ToList();
            if (l.Count == 0 || l[0] == "null")
                return null;

            TreeNode res = new TreeNode(int.Parse(l[0]));
            Queue<TreeNode> q = new Queue<TreeNode>();
            q.Enqueue(res);
            int i = 1;
            while (q.Count > 0 && i < l.Count)
            {
                var node = q.Dequeue();
                if (i < l.Count && l[i] != "null")
                {
                    node.left = new TreeNode(int.Parse(l[i]));
                    q.Enqueue(node.left);
                }
                i++;
                if (i < l.Count && l[i] != "null")
                {
                    node.right = new TreeNode(int.Parse(l[i]));
                    q.Enqueue(node.right);
                }
                i++;
            }
            return res;
        }
    }
    public int MinDistance(string word1, string word2)
    {
        int[,] dp = new int[word1.Length + 1, word2.Length + 1];

        for (int i = 0; i <= word1.Length; i++)
        {
            dp[i, word2.Length] = word1.Length - i;
        }
        for (int i = 0; i <= word2.Length; i++)
        {
            dp[word1.Length, i] = word2.Length - i;
        }

        for (int i = word1.Length - 1; i >= 0; i--)
        {
            for (int j = word2.Length - 1; j >= 0; j--)
            {
                if (word1[i] == word2[j])
                    dp[i, j] = dp[i + 1, j + 1];
                else
                    dp[i, j] = 1 + Math.Min(Math.Min(dp[i, j + 1], dp[i + 1, j]), dp[i + 1, j + 1]);
            }
        }
        return dp[0, 0];
    }
    public IList<IList<int>> PathSum(TreeNode root, int targetSum)
    {
        if (root == null)
            return new List<IList<int>>();
        List<IList<int>> lists = new List<IList<int>>();
        void Dfs(TreeNode node, int target, List<int> curr)
        {
            if (node == null)
                return;
            curr.Add(node.val);

            if (node.left == null && node.right == null && node.val == target)
                lists.Add(new List<int>(curr));
            else
            {
                Dfs(node.left, target - node.val, curr);
                Dfs(node.right, target - node.val, curr);
            }
            curr.RemoveAt(curr.Count - 1);
        }
        Dfs(root, targetSum, new List<int>());
        return lists;
    }
    public int IntegerBreak(int n)
    {
        if (n <= 0)
            return 0;
        int[] dp = new int[n + 1];
        dp[1] = 1;
        for (int i = 2; i < dp.Length; i++)
        {
            dp[i] = i == n ? 0 : i;
            for (int j = 1; j < i; j++)
            {
                dp[i] = Math.Max(dp[i], dp[j] * dp[i - j]);
            }
        }
        return dp[n];
    }
    public int NumTilePossibilities(string tiles)
    {
        if (string.IsNullOrEmpty(tiles))
            return 0;
        HashSet<string> set = new HashSet<string>();
        bool[] isUsed = new bool[tiles.Length];
        void Backtrack(string curr)
        {
            if (curr.Length > 0)
                set.Add(curr);
            for (int i = 0; i < tiles.Length; i++)
            {
                if (isUsed[i])
                    continue;
                isUsed[i] = true;
                Backtrack(curr + tiles[i]);
                isUsed[i] = false;
            }
        }
        Backtrack("");
        return set.Count;
    }
    public IList<string> RestoreIpAddresses(string s)
    {
        if (string.IsNullOrEmpty(s))
            return new List<string>();
        List<string> l = new List<string>();

        void Backtrack(int index, List<string> parts)
        {
            if (index == s.Length && parts.Count == 4)
            {
                l.Add(string.Join(".", parts));
                return;
            }
            if (parts.Count > 4)
                return;
            for (int i = 1; i <= 3 && index + i <= s.Length; i++)
            {
                string seg = s.Substring(index, i);
                if (int.Parse(seg) > 255)
                    continue;
                if (seg.Length > 1 && seg[0] == '0')
                    continue;

                parts.Add(seg);
                Backtrack(index + i, parts);
                parts.RemoveAt(parts.Count - 1);
            }
        }
        Backtrack(0, new List<string>());
        return l;
    }
    public int BeautifulSubsets(int[] nums, int k)
    {
        if (nums == null || nums.Length == 0)
            return 0;
        Array.Sort(nums);
        int res = 0;
        void Recursion(List<int> curr, int index)
        {
            if (index == nums.Length)
            {
                if (curr.Count > 0)
                    res++;
                return;
            }

            Recursion(curr, index + 1);

            bool add = true;
            foreach (var item in curr)
            {
                if (Math.Abs(item - nums[index]) == k)
                {
                    add = false;
                    break;
                }
            }
            if (add)
            {
                curr.Add(nums[index]);
                Recursion(curr, index + 1);
                curr.RemoveAt(curr.Count - 1);
            }
        }
        Recursion(new List<int>(), 0);
        return res;
    }
    public IList<string> WordBreak(string s, IList<string> wordDict)
    {
        if (string.IsNullOrEmpty(s))
            return new List<string>();
        List<string> list = new List<string>();
        void Recursion(List<string> curr, int index)
        {
            if (index == s.Length)
            {
                if (curr.Count > 0)
                {
                    list.Add(string.Join(" ", curr));
                }
                return;
            }
            string substr = s.Substring(index);
            foreach (var word in wordDict)
            {
                if (substr.StartsWith(word))
                {
                    curr.Add(word);
                    Recursion(curr, index + word.Length);
                    curr.RemoveAt(curr.Count - 1);
                }
            }
        }
        Recursion(new List<string>(), 0);
        return list;
    }
    public string FindDifferentBinaryString(string[] nums)
    {
        if (nums == null || nums.Length == 0)
            return null;
        HashSet<string> set = new HashSet<string>(nums);
        string res = "";
        void Recursion(string curr = "")
        {
            if (!string.IsNullOrEmpty(res))
                return;
            if (curr.Length == nums[0].Length)
            {
                if (!set.Contains(curr))
                    res = curr;
                return;
            }
            Recursion(curr + '0');
            Recursion(curr + '1');
        }
        Recursion();
        return res;
    }
    public IList<int> PartitionLabels(string s)
    {
        if (string.IsNullOrEmpty(s))
            return new List<int>();
        Dictionary<char, int> map = new Dictionary<char, int>();
        for (int i = 0; i < s.Length; i++)
        {
            map[s[i]] = i;
        }
        List<int> result = new List<int>();
        int index = 0;
        int LastIndex = map[s[index]];
        int startIndex = 0;
        while (index < s.Length)
        {
            int current = map[s[index]];
            if (current > LastIndex)
                LastIndex = current;
            if (index < LastIndex)
                index++;
            else
            {
                index++;
                result.Add(LastIndex - startIndex + 1);
                startIndex = LastIndex + 1;
            }
        }
        return result;
    }
    public int MaxUniqueSplit(string s)
    {
        if (string.IsNullOrEmpty(s))
            return 0;
        int max = 0;
        void Dfs(int index, HashSet<string> curr)
        {
            if (index == s.Length)
            {
                max = Math.Max(max, curr.Count);
                return;
            }
            for (int i = index; i < s.Length; i++)
            {
                string x = s.Substring(index, i - index + 1);
                if (curr.Contains(x))
                    continue;
                curr.Add(x);
                Dfs(i + 1, curr);
                curr.Remove(x);
            }
        }
        Dfs(0, new HashSet<string>());
        return max;
    }
    public int SingleNonDuplicate(int[] nums)
    {
        if (nums == null || nums.Length == 0)
            return 0;
        int l = 0, r = nums.Length - 1;
        while (l < r)
        {
            int m = l + (r - l) / 2;
            if ((m & 1) == 1)
            {
                m--;
            }
            if (nums[m] != nums[m + 1])
            {
                r = m;
            }
            else
            {
                l = m + 2;
            }
        }
        return nums[l];
    }
    public bool SearchSecond(int[] nums, int target)
    {
        int l = 0, r = nums.Length - 1;
        while (l <= r)
        {
            int mid = l + (r - l) / 2;
            if (nums[mid] == target)
                return true;
            if (nums[mid] == nums[l] && nums[r] == nums[mid])
            {
                l++;
                r--;
            }
            else if (nums[l] <= nums[mid])
            {
                if (target >= nums[l] && target < nums[mid])
                    r = mid - 1;
                else
                    l = mid + 1;
            }
            else
            {
                if (target > nums[mid] && target <= nums[r])
                    l = mid + 1;
                else
                    r = mid - 1;
            }
        }
        return false;
    }
    public IList<IList<int>> SubsetsRevision(int[] nums)
    {
        if (nums == null || nums.Length == 0)
            return new List<IList<int>>();
        List<IList<int>> lists = new List<IList<int>>();
        void Backtrack(int index, List<int> curr)
        {
            lists.Add(new List<int>(curr));
            for (int i = index; i < nums.Length; i++)
            {
                curr.Add(nums[i]);
                Backtrack(i + 1, curr);
                curr.RemoveAt(curr.Count - 1);
            }
        }
        Backtrack(0, new List<int>());
        return lists;
    }
    public IList<IList<int>> CombineRevision(int n, int k)
    {
        if (k <= 0 || n <= 0)
            return new List<IList<int>>();
        List<IList<int>> lists = new List<IList<int>>();
        void Backtrack(int index, List<int> curr)
        {
            if (curr.Count == k)
            {
                lists.Add(new List<int>(curr));
                return;
            }
            for (int i = index; i <= n; i++)
            {
                curr.Add(i);
                Backtrack(i + 1, curr);
                curr.RemoveAt(curr.Count - 1);
            }
        }
        Backtrack(1, new List<int>());
        return lists;
    }
    public IList<IList<int>> PermuteRevision(int[] nums)
    {
        if (nums == null || nums.Length == 0)
            return new List<IList<int>>();
        List<IList<int>> lists = new List<IList<int>>();
        bool[] used = new bool[nums.Length];
        void Backtrack(List<int> curr)
        {
            if (curr.Count == nums.Length)
            {
                lists.Add(new List<int>(curr));
                return;
            }
            for (int i = 0; i < nums.Length; i++)
            {
                if (used[i])
                    continue;
                used[i] = true;
                curr.Add(nums[i]);
                Backtrack(curr);
                curr.RemoveAt(curr.Count - 1);
                used[i] = false;
            }
        }
        Backtrack(new List<int>());
        return lists;
    }
    public int SearchRevision(int[] nums, int target)
    {
        if (nums == null || nums.Length == 0)
            return -1;
        int l = 0, r = nums.Length - 1;
        while (l < r)
        {
            int mid = l + (r - l) / 2;
            if (nums[mid] == target)
                return mid;
            if (nums[l] <= nums[mid])
            {
                if (nums[l] <= target && target < nums[mid])
                    r = mid - 1;
                else
                    l = mid + 1;
            }
            else
            {
                if (target <= nums[r] && target > nums[mid])
                    l = mid + 1;
                else
                    r = mid - 1;
            }
        }
        return nums[l] == target ? l : -1;
    }
    int MinOperations(int[] nums)
    {
        Array.Sort(nums);
        var correctSum = new int[nums.Length];
        var prefixSum = new int[nums.Length];
        for (int i = 0; i < nums.Length; i++)
        {
            correctSum[i] = i + 1;
            if (i == 0)
            {
                prefixSum[0] = nums[0];
                continue;
            }
            prefixSum[i] = prefixSum[i - 1] + nums[i];
        }
        int count = 0;
        for (int i = 0; i < nums.Length; i++)
        {
            count += prefixSum[i] - count - correctSum[i];
        }
        return count;
    }
    public int MinOperationsII(int[] nums)
    {
        if (nums == null || nums.Length == 0)
            return 0;
        int count = 0;
        for (int i = 1; i < nums.Length; i++)
        {
            if (nums[i] <= nums[i - 1])
            {
                count += nums[i - 1] - nums[i] + 1;
                nums[i] = Math.Max(nums[i - 1] + 1, nums[i]);
            }
        }
        return count;
    }
    public int MinMovesI(int[] nums)
    {
        if (nums == null || nums.Length == 0)
            return 0;
        int count = 0, min = nums.Min();
        foreach (var num in nums)
            count += num - min;
        return count;
    }
    public bool CanPartition(int[] nums)
    {
        if (nums == null || nums.Length == 0)
            return false;
        int sum = nums.Sum();
        int targ = sum / 2;
        if (sum % 2 == 1) return false;
        HashSet<int> set = new HashSet<int>() { 0 };
        for (int i = nums.Length - 1; i >= 0; i--)
        {
            var tempSet = new HashSet<int>();
            foreach (var item in set)
            {
                if (item + nums[i] == targ)
                    return true;
                tempSet.Add(item + nums[i]);
                tempSet.Add(item);
            }
            set = tempSet;
        }
        return set.Contains(targ);
    }
    public List<int> LeftSideView(TreeNode root)
    {
        if (root == null)
            return new List<int>();
        Dictionary<int, List<int>> map = new Dictionary<int, List<int>>();
        void Inorder(TreeNode node, int level)
        {
            if (node == null)
                return;
            Inorder(node.left, level + 1);
            if (!map.ContainsKey(level))
            {
                map[level] = new List<int>();
                map[level].Add(node.val);
            }

            Inorder(node.right, level + 1);
        }
        Inorder(root, 0);
        List<int> result = new List<int>();
        for (int i = 0; i < map.Count; i++)
        {
            result.Add(map[i][0]);
        }
        return result;
    }
    public ListNode MergeKLists(ListNode[] lists)
    {
        ListNode res = new ListNode();
        ListNode temp = res;

        bool allNull = false;
        while (!allNull)
        {
            int min = int.MaxValue;
            int index = -1;
            for (int i = 0; i < lists.Length; i++)
            {
                if (lists[i] != null && lists[i].val < min)
                {
                    min = lists[i].val;
                    index = i;
                }
            }
            if (index == -1)
                allNull = true;
            else
            {
                temp.next = lists[index];
                temp = temp.next;
                lists[index] = lists[index].next;
            }
        }
        return res.next;
    }
    public int MinimumTotal(IList<IList<int>> triangle)
    {
        if (triangle == null || triangle.Count == 0)
            return 0;
        int?[,] memo = new int?[triangle.Count, triangle.Count];
        int Recursion(int ListIndex = 0, int currIndex = 0)
        {
            if (ListIndex == triangle.Count)
                return 0;
            if (memo[ListIndex, currIndex].HasValue)
                return memo[ListIndex, currIndex].Value;
            int res = triangle[ListIndex][currIndex];
            res += Math.Min(Recursion(ListIndex + 1, currIndex),
                Recursion(ListIndex + 1, currIndex + 1));
            memo[ListIndex, currIndex] = res;
            return res;
        }
        return Recursion(0, 0);
    }
    public int DeleteAndEarn(int[] nums)
    {
        if (nums == null || nums.Length == 0)
            return 0;
        var map = new Dictionary<int, int>();
        foreach (var item in nums)
        {
            if (!map.ContainsKey(item))
                map[item] = 0;
            map[item]++;
        }
        var list = map.Keys.ToList();
        list.Sort();
        int e1 = 0, e2 = 0;
        for (int i = 0; i < list.Count; i++)
        {
            int curr = list[i] * map[list[i]];
            if (i > 0 && list[i] == list[i - 1] + 1)
            {
                int t = e2;
                e2 = Math.Max(e2, e1 + curr);
                e1 = t;
            }
            else
            {
                int t = e2;
                e2 = curr + e2;
                e1 = t;
            }
        }
        return e2;
    }
    public int NumTrees(int n)
    {
        if (n <= 1)
            return 1;
        if (n == 2)
            return 2;
        int[] dp = new int[n + 1];
        dp[0] = 1;
        dp[1] = 1;
        for (int i = 2; i <= n; i++)
        {
            int total = 0;
            for (int j = 1; j <= i; j++)
            {
                int left = j - 1;
                int right = i - j;
                total += dp[left] * dp[right];
            }
            dp[i] = total;
        }
        return dp[n];
    }
    // Biweekly-Contest
    public int MinimumFlips(int n)
    {
        if (n < 0)
            return 0;
        int reversed = 0;
        int temp = n;
        while (temp > 0)
        {
            reversed <<= 1;
            reversed = reversed | (temp & 1);
            temp >>= 1;
        }
        int c = 0;
        for (int i = 0; i < 32; i++)
        {
            int a = n & 1;
            int b = reversed & 1;
            if (a != b)
                c++;
            n >>= 1;
            reversed >>= 1;
        }
        return c;
    }
    public int TotalWaviness(int num1, int num2)
    {
        if (num2 < 100)
            return 0;
        int Wave(int x)
        {
            string st = x.ToString();
            if (st.Length < 3)
                return 0;
            int count = 0;
            for (int i = 1; i < st.Length - 1; i++)
            {
                int curr = st[i] - '0';
                int prev = st[i - 1] - '0';
                int next = st[i + 1] - '0';
                count += (curr > prev && curr > next) ? 1 : 0;
                count += (curr < prev && curr < next) ? 1 : 0;
            }
            return count;
        }
        int result = 0;
        for (int i = num1; i <= num2; i++)
        {
            result += Wave(i);
        }
        return result;
    }
    public int MincostTickets(int[] days, int[] costs)
    {
        int n = days.Length;
        int[] dp = new int[n + 1];

        for (int i = n - 1; i >= 0; i--)
        {
            dp[i] = int.MaxValue;
            int j = i;
            for (int x = 0; x < 3; x++)
            {
                int y = x == 0 ? 1 : x == 1 ? 7 : 30;
                int c = costs[x];
                while (j < n && days[j] < days[i] + y)
                    j++;
                dp[i] = Math.Min(dp[i], dp[j] + c);
            }
        }
        return dp[0];
    }
    public int NthUglyNumber(int n)
    {
        if (n == 1)
            return 1;
        var pq = new PriorityQueue<long, long>();
        var set = new HashSet<long>();
        pq.Enqueue(1L, 1L);
        set.Add(1L);
        long u = 1;
        for (int i = 0; i < n; i++)
        {
            u = pq.Dequeue();
            long a = u * 2;
            long b = u * 3;
            long c = u * 5;

            if (!set.Contains(a))
            {
                set.Add(a);
                pq.Enqueue(a, a);
            }

            if (!set.Contains(b))
            {
                set.Add(b);
                pq.Enqueue(b, b);
            }

            if (!set.Contains(c))
            {
                set.Add(c);
                pq.Enqueue(c, c);
            }
        }
        return (int)u;
    }
    public double New21Game(int n, int k, int maxPts)
    {
        if (k == 0)
            return 1.0;
        double wSum = 0;
        for (int i = k; i < k + maxPts; i++)
        {
            wSum += i <= n ? 1.0 : 0.0;
        }
        var dp = new Dictionary<int, double>();
        for (int i = k - 1; i >= 0; i--)
        {
            dp[i] = wSum / maxPts;
            double r = 0.0;
            if (i + maxPts <= n)
                r = dp.ContainsKey(i + maxPts) ? dp[i + maxPts] : 1.0;
            wSum += dp[i] - r;
        }
        return dp[0];
    }
    public int LongestCommonSubsequenceRevision(string text1, string text2)
    {
        if (string.IsNullOrEmpty(text1) || string.IsNullOrEmpty(text2))
            return 0;
        int[,] arr = new int[text1.Length + 1, text2.Length + 1];
        for (int i = text1.Length - 1; i >= 0; i--)
        {
            for (int j = text2.Length - 1; j >= 0; j--)
            {
                if (text1[i] == text2[j])
                    arr[i, j] = 1 + arr[i + 1, j + 1];
                else
                    arr[i, j] = Math.Max(arr[i + 1, j], arr[i, j + 1]);
            }
        }
        return arr[0, 0];
    }
    public int LongestPalindromeSubseq(string s)
    {
        if (string.IsNullOrEmpty(s))
            return 0;
        string r = "";
        for (int i = s.Length - 1; i >= 0; i--)
        {
            r += s[i];
        }
        int[,] arr = new int[s.Length + 1, r.Length + 1];
        for (int i = s.Length - 1; i >= 0; i--)
        {
            for (int j = r.Length - 1; j >= 0; j--)
            {
                if (s[i] == r[j])
                    arr[i, j] = 1 + arr[i + 1, j + 1];
                else
                    arr[i, j] = Math.Max(arr[i + 1, j], arr[i, j + 1]);
            }
        }
        return arr[0, 0];
    }
    public int LenLongestFibSubseq(int[] arr)
    {
        if (arr == null || arr.Length == 0)
            return 0;
        var s = new HashSet<int>(arr);
        int res = 0;
        for (int i = 0; i < arr.Length - 1; i++)
        {
            for (int j = i + 1; j < arr.Length; j++)
            {
                int prev = arr[i];
                int curr = arr[j];
                int next = prev + curr;
                int l = 2;
                while (s.Contains(next))
                {
                    prev = curr;
                    curr = next;
                    next = prev + curr;
                    l++;
                    res = Math.Max(res, l);
                }
            }
        }
        return res;
    }
    public int MaxProfit1(int[] prices)
    {
        if (prices == null || prices.Length == 0)
            return 0;
        int res = 0;
        var memo = new Dictionary<(int, bool, bool, int), int>();
        int Rec(int index, int curr, int priceBought, bool bought, bool cooldown)
        {
            if (index >= prices.Length)
                return curr;
            var key = (index, bought, cooldown, priceBought);
            if (memo.ContainsKey(key))
                return curr + memo[key];
            int best = curr;
            if (cooldown)
            {
                best = Math.Max(best, Rec(index + 1, curr, priceBought, bought, false));
                memo[key] = best - curr;
                return best;
            }
            best = Math.Max(best, Rec(index + 1, curr, priceBought, bought, false));
            if (!bought)
            {
                best = Math.Max(best, Rec(index + 1, curr, prices[index], true, false));
            }
            else
            {
                best = Math.Max(best, Rec(index + 1, curr + (prices[index] - priceBought), 0, false, true));
            }
            memo[key] = best - curr;
            return best;
        }
        Rec(0, 0, 0, false, false);
        return memo[(0, false, false, 0)];
    }
    public int CountServers(int[][] grid)
    {
        if (grid == null || grid.Length == 0)
            return 0;
        int res = 0;
        var rows = new int[grid.Length];
        var columns = new int[grid[0].Length];
        for (int i = 0; i < grid.Length; i++)
        {
            for (int j = 0; j < grid[0].Length; j++)
            {
                if (grid[i][j] == 1)
                {
                    rows[i]++;
                    columns[j]++;
                }
            }
        }
        for (int i = 0; i < grid.Length; i++)
        {
            for (int j = 0; j < grid[0].Length; j++)
            {
                if (grid[i][j] == 1 && (rows[i] > 1 || columns[j] > 1))
                    res++;
            }
        }
        return res;
    }
    public int SubarraySumRevision(int[] nums, int k)
    {
        int[] pref = new int[nums.Length];
        pref[0] = nums[0];
        for (int i = 1; i < nums.Length; i++)
        {
            pref[i] = nums[i] + pref[i - 1];
        }
        int answer = 0;
        Dictionary<int, int> map = new Dictionary<int, int>();
        map[0] = 1;
        for (int r = 0; r < nums.Length; r++)
        {
            int need = pref[r] - k;
            if (map.ContainsKey(need))
            {
                answer += map[need];
            }
            if (!map.ContainsKey(pref[r]))
                map[pref[r]] = 0;
            map[pref[r]]++;
        }
        return answer;
    }
    public int FindMaxFish(int[][] grid)
    {
        if (grid == null || grid.Length == 0)
            return 0;
        int result = 0;
        bool[,] b = new bool[grid.Length, grid[0].Length];

        var dirs = new int[4][];
        dirs[0] = new int[] { 0, 1 };
        dirs[1] = new int[] { 0, -1 };
        dirs[2] = new int[] { 1, 0 };
        dirs[3] = new int[] { -1, 0 };
        int Rec(int i, int j)
        {
            if (i < 0 || i >= grid.Length || j < 0 || j >= grid[0].Length)
                return 0;
            if (b[i, j] || grid[i][j] == 0)
                return 0;
            b[i, j] = true;

            int sum = grid[i][j];
            foreach (var dir in dirs)
            {
                sum += Rec(i + dir[0], j + dir[1]);
            }
            return sum;
        }
        for (int i = 0; i < grid.Length; i++)
        {
            for (int j = 0; j < grid[0].Length; j++)
            {
                if (grid[i][j] > 0 && !b[i, j])
                    result = Math.Max(result, Rec(i, j));
            }
        }
        return result;
    }
    public int CountSubIslands(int[][] grid1, int[][] grid2)
    {
        var dirs = new int[][]
        {
            new int[] {1, 0},
            new int[] {-1, 0},
            new int[] {0, 1},
            new int[] {0, -1}
        };
        var visited = new bool[grid1.Length, grid1[0].Length];
        int subIslands = 0;
        bool Recursion(int i, int j)
        {
            if (i < 0 || j < 0 || i >= grid2.Length || j >= grid2[0].Length)
                return true;
            if (visited[i, j] || grid2[i][j] == 0)
                return true;
            visited[i, j] = true;
            bool isSub = grid1[i][j] == 1;

            foreach (var d in dirs)
            {
                bool child = Recursion(i + d[0], j + d[1]);
                if (!child) isSub = false;
            }
            return isSub;
        }
        for (int i = 0; i < grid1.Length; i++)
        {
            for (int j = 0; j < grid1[0].Length; j++)
            {
                if (grid2[i][j] == 1 && !visited[i, j])
                {
                    if (Recursion(i, j))
                        subIslands++;
                }
            }
        }
        return subIslands;
    }
    public int ShortestBridge(int[][] grid)
    {
        if (grid == null || grid.Length == 0)
            return 0;
        var dirs = new int[][]
        {
            new int[] {1, 0},
            new int[] {-1, 0},
            new int[] {0, 1},
            new int[] {0, -1}
        };
        var q = new Queue<(int, int)>();
        var visited = new bool[grid.Length, grid[0].Length];
        bool found = false;
        for (int i = 0; i < grid.Length && !found; i++)
        {
            for (int j = 0; j < grid[0].Length && !found; j++)
            {
                if (grid[i][j] == 1)
                {
                    Recursion(i, j);
                    found = true;
                }
            }
        }
        void Recursion(int i, int j)
        {
            if (i < 0 || j < 0 || i >= grid.Length || j >= grid[0].Length)
                return;
            if (visited[i, j] || grid[i][j] == 0)
                return;
            visited[i, j] = true;
            q.Enqueue((i, j));
            foreach (var dir in dirs)
            {
                Recursion(i + dir[0], j + dir[1]);
            }
        }
        int steps = 0;
        while (q.Count != 0)
        {
            int size = q.Count;
            for (int i = 0; i < size; i++)
            {
                (int x, int y) = q.Dequeue();
                foreach (var dir in dirs)
                {
                    int a = x + dir[0];
                    int b = y + dir[1];
                    if (a < 0 || b < 0 || a >= grid.Length || b >= grid[0].Length || visited[a, b])
                        continue;
                    if (grid[a][b] == 1)
                        return steps;
                    q.Enqueue((a, b));
                    visited[a, b] = true;
                }
            }
            steps++;
        }
        return -1;
    }
    public int FindLeastNumOfUniqueInts(int[] arr, int k)
    {
        if (arr == null || arr.Length == 0)
            return 0;
        var map = new Dictionary<int, int>();
        foreach (var n in arr)
        {
            if (!map.ContainsKey(n))
                map[n] = 0;
            map[n]++;
        }
        var pq = new PriorityQueue<int, int>();
        foreach (var item in map)
        {
            pq.Enqueue(item.Key, item.Value);
        }
        while (k >= 0)
        {
            int a = pq.Dequeue();
            if (map[a] == 1)
                map.Remove(a);
            else
            {
                map[a]--;
                pq.Enqueue(a, map[a]);
            }
            k--;
        }
        return pq.Count;
    }
    public int NumEnclaves(int[][] grid)
    {
        if (grid == null || grid.Length == 0)
            return 0;
        var boolArr = new bool[grid.Length, grid[0].Length];
        int res = 0;
        void Recursion(int i, int j)
        {
            if (i < 0 || j < 0 || i >= grid.Length || j >= grid[0].Length)
                return;
            if (boolArr[i, j] || grid[i][j] == 0)
                return;

            var dirs = new int[][]
            {
                new int[] {0, 1},
                new int[] {0, -1},
                new int[] {1, 0},
                new int[] {-1, 0},
            };
            boolArr[i, j] = true;
            foreach (var dir in dirs)
            {
                Recursion(i + dir[0], j + dir[1]);
            }
        }
        for (int i = 0; i < grid.Length; i++)
        {
            if (grid[i][0] == 1) Recursion(i, 0);
            if (grid[i][grid[0].Length - 1] == 1) Recursion(i, grid[0].Length - 1);
        }
        for (int i = 0; i < grid[0].Length; i++)
        {
            if (grid[0][i] == 1) Recursion(0, i);
            if (grid[grid.Length - 1][i] == 1) Recursion(grid.Length - 1, i);
        }
        for (int i = 0; i < grid.Length; i++)
        {
            for (int j = 0; j < grid[0].Length; j++)
            {
                if (grid[i][j] == 1 && !boolArr[i, j])
                    res++;
            }
        }
        return res;
    }
    public int ShortestPathBinaryMatrix(int[][] grid)
    {
        if (grid == null || grid.Length == 0)
            return -1;
        int r = grid.Length - 1, c = grid[0].Length - 1;
        if (grid[0][0] == 1 || grid[r][c] == 1)
            return -1;

        Queue<(int, int)> q = new Queue<(int, int)>();
        var visited = new bool[grid.Length, grid[0].Length];
        q.Enqueue((0, 0));
        visited[0, 0] = true;

        var dirs = new int[][]
        {
            new int[] { 0, 1 }, new int[] { 0, -1 },
            new int[] { 1, 0 }, new int[] { -1, 0 },
            new int[] { 1, 1 }, new int[] { 1, -1 },
            new int[] { -1, 1 }, new int[] { -1, -1 }
        };
        
        int step = 1;
        while (q.Count != 0)
        {
            int size = q.Count;
            for (int i = 0; i < size; i++)
            {
                (int a, int b) = q.Dequeue();
                if (a == r && b == c)
                    return step;
                foreach (var dir in dirs)
                {
                    int x = a + dir[0], y = b + dir[1];
                    if (x >= 0 && y >= 0 && x <= r && y <= c && !visited[x, y] && grid[x][y] == 0)
                    {
                        q.Enqueue((x, y));
                        visited[x, y] = true;
                    }
                }
            }
            step++;
        }
        return -1;
    }
    public int LadderLength(string beginWord, string endWord, IList<string> wordList)
    {
        if (string.IsNullOrEmpty(beginWord) || string.IsNullOrEmpty(endWord))
            return 0;
        if (wordList == null || wordList.Count == 0 || beginWord == endWord)
            return 0;
        var map = new Dictionary<string, List<string>>();
        wordList.Add(beginWord);
        foreach (var word in wordList)
        {
            if (!map.ContainsKey(word))
                map[word] = new List<string>();
            foreach (var st in wordList)
            {
                if (st == word)
                    continue;
                int diff = 0;
                for (int i = 0; i < word.Length; i++)
                    if (st[i] != word[i]) diff++;
                if (diff == 1)
                {
                    map[word].Add(st);
                    if (!map.ContainsKey(st))
                        map[st] = new List<string>();
                    map[st].Add(word);
                }
            }
        }
        var q = new Queue<(string, int)>();
        var visited = new HashSet<string>();
        q.Enqueue((beginWord, 1));
        while (q.Count != 0)
        {
            int size = q.Count;
            for (int i = 0; i < size; i++)
            {
                (string word, int diff) = q.Dequeue();
                if (word == endWord)
                    return diff;
                foreach (var w in map[word])
                {
                    if (!visited.Contains(w))
                    {
                        visited.Add(w);
                        q.Enqueue((w, diff + 1));
                    }
                }
            }
        }
        return 0;
    }
    public int NumDistinct(string s, string t)
    {
        if (string.IsNullOrEmpty(s) || string.IsNullOrEmpty(t))
            return 0;
        int occur = 0;
        int?[,] memo = new int?[s.Length + 1, t.Length + 1];
        int Recursion(int index, int curr)
        {
            if (curr == t.Length)
                return 1;
            if (index == s.Length || curr + s.Length - index < t.Length)
                return 0;
            if (memo[index, curr].HasValue)
                return memo[index, curr].Value;

            int occs = 0;
            if (s[index] == t[curr])
                occs += Recursion(index + 1, curr + 1);

            occs += Recursion(index + 1, curr);
            memo[index, curr] = occs;
            return memo[index, curr].Value;
        }
        return Recursion(0, 0);
    }
    static void Main(string[] args)
    {
        Console.WriteLine();
    }
}