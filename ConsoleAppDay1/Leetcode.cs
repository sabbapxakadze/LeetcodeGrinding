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

        for(int h = 0; h < 12; h++)
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
            res = ((reminder + x + y)%10) + res;
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
    static void Main(string[] args)
    {
        Leetcode l = new Leetcode();
        Console.WriteLine(l.RepeatedSubstringPattern("abcdbabc"));
    }
}