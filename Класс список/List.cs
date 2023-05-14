using System;
using System.Runtime.CompilerServices;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Intrinsics.Arm;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.ComponentModel.Design;

namespace List
{    //Начало класса
    class Node
    {
        private int key;
        private Node next;
        public Node()  //конструктор по умолчанию
        {
            key = 0;
            next = null;
        }

        public Node(int _key, Node _next)   // конструктор  с параметрами
        {
            key = _key;
            next = _next;
        }

        public void OutputKey()
        {

            Console.WriteLine(key);
        }

        public void OutputNext()
        {

            Console.WriteLine(next);
        }
       public int Key
        { 
            get { return key; } 
            set { key = value; }
        }
        public Node Next
        {
            get { return next; }
            set { next = value; }
        }
    }

    class List
    {
        Node head;
        public List Copy(List a)
        {
            if(a.IsEmpty())
            {
                Console.WriteLine("Empty");
                return null;
            }
            ClearList();
            
            Node p = a.head;

            while (p.Next != null)
            {
                AddIntoEnd(p.Key);
                p = p.Next;
            }
            AddIntoEnd(p.Key);
            return this;
        }
        public List()
        {
            head = null;
        }
        public List(Node _head)
        {
            head = _head;
        }

        public List(int[] x)
        {
            int i = 0;
            while (i < x.Length)
            {
                AddIntoEnd(x[i]);
                i++;
            }
        }

        public List(List s)
        {
            Copy(s);
        }

        public bool IsEmpty()
        {
            if (head == null) return true;
            else return false;
        }

        public void OutputHead()
        {
            Console.WriteLine(head.Key);
        }

        //Поиск узла НАДО ПРОВЕРИТЬ!!!
        public Node Find(int _key)
        {
            Node p = head;
            while ((p != null) && (p.Key != _key))
            {
                p = p.Next;
            }
            if (p == null)
            {
                //Console.WriteLine("Узел отсутствует");
                return null;
            }
            return p;
        }

        public void AddIntoEnd(int x)
        {

            if (IsEmpty())
            {
                head = new Node(x, null);
            }
            else
            {
                Node p = head;
                while (p.Next != null)
                {
                    p = p.Next;
                }
                Node g = new Node(x, null);
                p.Next = g;

            }
        }
        public void AddIntoHead(int x)
        {
            if (IsEmpty())
            {
                head = new Node(x, null);
            }
            else
            {
                Node g = new Node();
                g = head;
                Node p = new Node(x, g);
                head = p;
            }
        }

        public void AddIntoPosition(int t, int x)//x-позиция
        {
            if ((x - ListLen()) > 1)
            {
                Console.WriteLine("Position can't be found");
                return;
            }

            if (x == 1)
            {
                AddIntoHead(t);
                return;
            }

            int y = 1;
            Node p = head;
            Node g = new Node(t, null);
            Node c;
            while ((y < (x - 1)) && (p.Next != null))
            {
                p = p.Next;
                y++;
            }
            if (p.Next == null)
            {
                AddIntoEnd(t);
                return;
            }
            //p.OutputKey();
            c = p.Next;
            p.Next = g;
            g.Next = c;
        }

        public void AddAfterKey(int t, int key)
        {
            if ((ListLen() == 1) && (key == head.Key)) { AddIntoEnd(t); return; }

            if (IsEmpty())
            {
                Console.WriteLine("Can't find this key");
                return;
            }

            Node p = head;
            Node c;
            Node g = new Node(t, null);
            if (head.Key == key) { c = head.Next; head.Next = g; g.Next = c; return; }
            while (p.Next != null)
            {
                if ((p.Next).Key == key)
                {
                    p = p.Next;
                    c = p.Next;
                    p.Next = g;
                    g.Next = c;
                    return;
                }
                if (((p.Next).Next == null) && ((p.Next).Key == key))
                {
                    AddIntoEnd(t);
                    return;
                }
                p = p.Next;
            }
        }
        public void OutputList()
        {
            Node p = head;
            while (p != null)
            {
                p.OutputKey();
                p = p.Next;
            }
        }

        public List InputList(int x)
        {

            int i = 1;
            while (i <= x)
            {
                AddIntoEnd(Int32.Parse(Console.ReadLine()));
                i++;
            }
            return this;
        }

        public Node Max()
        {
            Node max = new Node();
            Node p = head;
            while (p != null)
            {
                if (p.Key > max.Key)
                {
                    max = p;
                }
                p = p.Next;
            }
            return max;
        }
        public Node Min()
        {
            Node min = head;
            Node p = head;
            while (p != null)
            {
                if (p.Key < min.Key)
                {
                    min = p;
                }
                p = p.Next;
            }
            return min;
        }

        public void ClearList()
        {
            head = null;
        }

        public void RemovingHead()
        {
            head = head.Next;
        }

        public void RemovingEnd()
        {
            if (IsEmpty())
            {
                Console.WriteLine("List is empty");
                return;
            }
            if (ListLen() == 1)
            {
                RemovingHead();
                return;
            }
            Node p = head;
            while ((p.Next).Next != null)
            {
                p = p.Next;
            }
            p.Next = null;
        }

        public int ListLen()
        {
            Node p = head;
            if (p == null)
            {
                return 0;
            }
            else
            {
                int len = 1;
                while (p.Next != null)
                {
                    p = p.Next;
                    len++;
                }
                return len;
            }
        }
        public void RemovingPosition(int x)
        {

            if ((x > ListLen()) || IsEmpty())
            {
                Console.WriteLine("Position can't be found");
                return;
            }
            int y = 1;
            Node p = head;
            Node c;
            if (x == 1) { RemovingHead(); return; }
            if (x == ListLen()) { RemovingEnd(); return; }
            while (y < (x - 1))
            {
                p = p.Next;
                y++;
            }
            c = p.Next;
            p.Next = c.Next;
        }

        public void RemovingKey(int key)
        {
            if ((ListLen() == 1) && (key == head.Key)) { RemovingHead(); return; }
            if (IsEmpty())
            {
                Console.WriteLine("Can't find this key");
                return;
            }

            Node p = head;
            Node c;
            while (p.Next != null)
            {
                if (key == head.Key) { RemovingHead(); }
                if (((p.Next).Key == key) && ((p.Next).Next == null))
                {
                    RemovingEnd(); return;
                }
                if ((p.Next).Key == key)
                {
                    c = p.Next;
                    p.Next = c.Next;
                }
                p = p.Next;
            }
        }

        public void ListSort()
        {
            Node p = head;
            Node q = head.Next;
            int buf;
            while (p != null)
            {
                while (q != null)
                {
                    if (p.Key > q.Key)
                    {
                        buf = p.Key;
                        p.Key = q.Key;
                        q.Key = buf;
                    }
                    q = q.Next;
                }

                if (p.Next != null)
                {
                    p = p.Next;
                    q = p.Next;
                }
                else return;
            }
            return;
        }
        public Node this[int i]
        {
            get 
            { 
                Node b=head;
                int j = 1;
                if ((b==null)||(i>ListLen())||(i<0))
                {
                    Console.WriteLine("Индекса не существует");
                    return b;
                }
                while (j<i)
                {
                    b=b.Next;
                    j++;
                }
                return b;
            }
            set 
            {
                Node b = head;
                int j = 1;
                if ((b == null) || (i > ListLen()) || (i < 0))
                {
                    Console.WriteLine("Индекса не существует");
                    return;
                }
                while (j < i)
                {
                    b = b.Next;
                    j++;
                }
                b=value;

            }

        }
        public static List operator+ (List a,List b)
        {
            List c = new List();
            c.Copy(a);
            Node p = b.head;
            while(p!=null)
            {
                c.AddIntoEnd(p.Key);
                p = p.Next;
            }
            return c;
        }
        public static List operator-(List a, List b)
        {
            List c = new List();
            c.Copy(b);
            Node p = a.head;
            while (p != null)
            {
                c.AddIntoEnd(p.Key);
                p = p.Next;
            }
            return c;
        }
        public static bool operator== (List a,List b)
        {
            if (a.ListLen() != b.ListLen()) return false;
            Node p= a.head;
            Node q= b.head;
            while(p!=null)
            {
                if (p.Key != q.Key) return false;
                p = p.Next;
                q = q.Next;
            }
            return true;
        }
        public static bool operator !=(List a, List b)
        {
            if (a.ListLen() != b.ListLen()) return true;
            Node p = a.head;
            Node q = b.head;
            while (p != null)
            {
                if (p.Key != q.Key) return true;
                p = p.Next;
                q = q.Next;
            }
            return false;
        }
    }

    class Set: List
    {
        int power;
        public int Power
        {
            get { return power; }
            set { power = value; }
        }
        public Set():base()
        { 
            power = 0;

        }
        public Set(int n)
        {
            Random rnd = new Random();
            int value;
            power = 0;
            while (power != n)
            {
                value = rnd.Next(0, 100);
                AddElement(value);
            }
        }

        public Set(Set x)
        {
            CopySet(x);
        }
        public void AddElement(int value)
        {
            if (Find(value) == null) 
            { 
                AddIntoEnd(value);
                power++; 
            }
        }

        public void InputSet(int x)
        {
            while(power!=x)
            {
                AddElement(Int32.Parse(Console.ReadLine()));
            }
        }

        public void OutputSet()
        {
            OutputList();
        }

        public Node FindSet(int x)
        {
            Node p = Find(x);
            return p;
        }

        public Set CopySet(Set set)
        {
            Copy(set);
            Power = set.Power;
            return this;
        }

        public static bool operator== (Set x, Set y)
        {
            if (x.Power!=y.Power) return false;
            int i = 0;
            while (i != 0) 
            {
                if (y.Find(x[i].Key)==null) return false;
            }
            return true;
        }

        public static bool operator !=(Set x, Set y)
        {
            if (x == y) return false;
            else return true;
        }

        public static Set operator +(Set x, Set y)
        {
            Set s = new Set();
            for (int i = 0;i<=x.Power;i++)
            {
                s.AddElement(x[i].Key);
            }
            for (int i = 0; i <= y.Power; i++)
            {
                s.AddElement(y[i].Key);
            }
            return s;
        }

        public Set Add(Set x)
        {
            Set set = new Set();
            set.CopySet(this);
            CopySet(set + x);
            return this;
        }

        public static Set operator /(Set x, int y)//добавление элемента 
        {
            Set s=new Set();
            s.CopySet(x);
            s.AddElement(y);
            return s;
        }


        public static Set operator *(Set x, Set y)
        {
            Set s = new Set();
            for (int i = 0;i<= x.Power;i++)
            {
                if (y.Find(x[i].Key)!=null)
                {
                    s.AddElement(x[i].Key);
                }
            }
            return s;
        }
        public Set And(Set y)
        {
            Set s = new Set();
            s.CopySet(this * y);
            CopySet(s);
            return this;
        }

        public static Set operator- (Set x, Set y)
        {
            Set s=new Set();
            s.CopySet(x*y);
            Set set = new Set();
            for (int i = 0;i<=x.Power;i++)
            {
                if (s.Find(x[i].Key)==null)
                {
                    set.AddElement(x[i].Key);
                }
            }
            return set;
        }

        public Set Sub(Set y)
        {
            Set s = new Set();
            s.CopySet(this-y);
            CopySet(s);
            return this;
        }

        public Set Ideal(int x)//дополнение до идеального
        {
            Set set = new Set();
            for (int i = 0; i<=x;i++)
            {
                set.AddElement(i);
            }
            Set s= new Set(set);
            set.CopySet(s.Sub(this));
            return set;
        }

        public Set DeleteElement(int x)
        {
            Set set = new Set();
            set.CopySet(this);
            set.RemovingKey(x);
            set.Power--;
            return set;
        }
        public Set DelElement(int x)
        {
            CopySet(DeleteElement(x));
            return this;
        }

        public new void Input(string x)
        {
            string[] values = x.Split(new char[] { ' ' });
            AddElement(Convert.ToInt32(values[0]));
            for (int i = 1; i < values.Length; i++)
            {
                AddElement(Convert.ToInt32(values[i]));
            }
        }

    }
    class Programm
    {
        static void Main(string[] args)
        {
            ////1
            //List s1 = new List();
            //s1.AddIntoHead(1);
            //s1.AddIntoEnd(10);
            //s1.OutputList();
            //Console.WriteLine("--------------");

            ////2
            //List s2= new List();
            //s2.InputList(6);
            //int max = s2.Max().Key;
            //Console.WriteLine("Max: "+max);
            //int min = s2.Min().Key;
            //Console.WriteLine("Min: " + min);
            //Console.WriteLine("--------------");
            //s2.ListSort();
            //s2.OutputList();

            ////3
            //Console.WriteLine("--------------");
            //Console.WriteLine("s[2]=" + s2[2].Key);
            //s2.RemovingPosition(2);
            //s2.OutputList();
            //Console.WriteLine("--------------");

            ////4
            //Node error = s2.Find(s2[6].Key);
            //s2.RemovingEnd();
            //s2.OutputList();
            //Console.WriteLine("--------------");

            ////5
            //List s3 = new List();
            //s3.Copy(s1);
            //if (s1==s3)
            //{
            //    Console.WriteLine("Равны");
            //}
            //else
            //{
            //    Console.WriteLine("Не равны");
            //}
            //s3.Find(15);

            ////6
            //Console.WriteLine("--------------");
            //s3.RemovingHead();
            //s3.RemovingKey(10);
            //s3.OutputList();
            //if (s3.IsEmpty()) Console.WriteLine("Пуст");
            //else Console.WriteLine("Не пуст");

            ////7
            //Console.WriteLine("--------------");
            //int[] x = { 10, 15, 58, 23, 1, 26 };
            //List s4= new List(x);
            //s4.OutputList();
            //s4.Find(25);
            //s4.AddIntoPosition(25, 4);
            //s4.OutputList();

            ////8
            //Console.WriteLine("--------------");
            //Console.WriteLine("s2: ");
            //s2.OutputList();
            //List s5= new List(s2);
            //Console.WriteLine("s5: ");
            //s5.OutputList();
            //if (s5.Find(4) != null) s5.RemovingKey(4);
            //else s5.AddIntoEnd(4);
            //Console.WriteLine("s5: ");
            //s5.OutputList();

            ////9
            //Console.WriteLine("--------------");
            //for (int i=0;i<4;i++)
            //{
            //    s5.AddIntoEnd(Int32.Parse(Console.ReadLine()));
            //}
            //Console.WriteLine("s5:");
            //s5.OutputList();
            //Console.WriteLine("s4:");
            //s4.OutputList();
            //if (s5 == s4)
            //{
            //    Console.WriteLine("Равны");
            //}
            //else
            //{
            //    Console.WriteLine("Не равны");
            //}

            ////10
            //Console.WriteLine("--------------");
            //Console.WriteLine("s5 + s4 - s1:");
            //s5.Copy(s5 + s4 - s1);
            //s5.OutputList();


            //Тесты множество

            //1Создайте множество S1, из 10 случайных чисел. Выведите S1 на экран (используя функцию Print).
            Console.WriteLine("s1:");
            Set s1 = new Set(10);
            s1.OutputSet();
            Console.WriteLine("----------------2");

            //2Создайте множество S2 и инициализируйте его (при создании) значением S1.  Выведите S2 на экран (используйте потоковый вывод). Проверьте равенство множеств S1  и  S2.
            Console.WriteLine("s2:");
            Set s2 = new Set(s1);
            s2.OutputSet();
            if (s1==s2)
                Console.WriteLine("Equal");
            else
                Console.WriteLine("Not equal");
            Console.WriteLine("Power= "+s1.Power+" "+s2.Power);
            Console.WriteLine("----------------3");

            //3Проверьте, есть ли в S1 элемент 5.Создайте множество S3, которое получается удалением/ добавлением из S1 элемента 5.Проверьте, что S1 и S3 – не равны.
            Set s3=new Set();
            if (s1.Find(5) == null)
            {
                Console.WriteLine("Can't find 5");
                s3.CopySet(s1 / 5);
            }
            else
            {
                s3.CopySet(s1.DeleteElement(5));
            }
            Console.WriteLine("s3:");
            s3.OutputSet();
            if (s1 == s3)
                Console.WriteLine("Equal");
            else
                Console.WriteLine("Not equal");
            Console.WriteLine("Power= " + s1.Power + " " + s3.Power);
            Console.WriteLine("----------------4");

            //4Создайте пустое множество S4. Проверьте его на пустоту.  Добавьте в S4 последовательно числа 5, 10, 15, 5.  Выведите S4 на экран.
            Set s4= new Set();
            if (s4.IsEmpty()) Console.WriteLine("Empty");
            else Console.WriteLine("Not Empty");
            s4.AddElement(5);
            s4.AddElement(10);
            s4.AddElement(15);
            s4.AddElement(5);
            Console.WriteLine("s4:");
            s4.OutputSet();
            Console.WriteLine("Power= " + s4.Power);
            Console.WriteLine("----------------5");

            //5Создайте пустое множество S5.  Инициализируйте его множеством S4.  Проверьте, что во множестве S5 есть элемент 15 и удалите его. Выведите получившееся множество на экран.
            Set s5 = new Set();
            s5.CopySet(s4);
            if (s5.Find(15) == null)
            {
                Console.WriteLine("Can't find 15");
            }
            else
            {
                s5.DelElement(15);
            }
            Console.WriteLine("s5:");
            s5.OutputSet();
            Console.WriteLine("Power= " + s5.Power);
            Console.WriteLine("----------------6");

            //6Создайте список T, из 20 случайных чисел. Выведите T на экран. Создайте из T множество S6.  Выведите S6 на экран. Определите количество элементов в S6.
            Set T = new Set(20);
            Console.WriteLine("T:");
            T.OutputSet();
            Console.WriteLine("Power= " + T.Power);
            Console.WriteLine("s6:");
            Set s6= new Set(T);
            s6.OutputSet();
            Console.WriteLine("Power= " + s6.Power);
            Console.WriteLine("----------------7");

            //7Найдите S7 – дополнение S6 до универсального. Найдите множество S8=S7∩S6.
            Set s7 = new Set();
            s7.CopySet(s6.Ideal(10));
            Console.WriteLine("s7:");
            s7.OutputSet();
            Console.WriteLine("Power= " + s7.Power);

            Set s8= new Set();
            s8.CopySet(s7 * s6);
            if(s8.IsEmpty()) { Console.WriteLine("s8 is Empty"); }
            else { Console.WriteLine("s8:"); s8.OutputSet(); }
            Console.WriteLine("----------------8");

            //8Создайте множество S9 ={ 1,3,5,7,9,11,13,15,17,19,21,23,25,27,29} (используйте потоковый ввод).  Найдите V1 = S7 ∩ S9,  V2 = S7 ∪ S9,  V3 = S7 \ S9.
            Set s9=new Set();
            s9.Input("1 3 5 7 9 11 13 15 17 19 21 23 25 27 29");
            Console.WriteLine("s9:");
            s9.OutputSet();
            Set v1=new Set(s7*s9);
            Set v2=new Set(s7+s9);
            Set v3= new Set(s7-s9);
            Console.WriteLine("v1:");
            v1.OutputSet();
            Console.WriteLine("Power= " + v1.Power);
            Console.WriteLine("v2:");
            v2.OutputSet();
            Console.WriteLine("Power= " + v2.Power);
            Console.WriteLine("v3:");
            v3.OutputSet();
            Console.WriteLine("Power= " + v3.Power);
            Console.WriteLine("----------------9");

            //9Измените множество V2, заменив его разностью V2 и V3. Сравните V2  с  S9
            v1.Add(v3);
            Console.WriteLine("v1:");
            v1.OutputSet();
            if (v1==s7) { Console.WriteLine("Equal"); }
            else { Console.WriteLine("Not equal"); }
            Console.WriteLine("Power= " + v1.Power+" "+s7.Power);
            Console.WriteLine("----------------10");

            //10Измените множество V2, заменив его разностью V2 и V3. Сравните V2  с  S9
            v2.Sub(v3);
            Console.WriteLine("v2=v2-s9:");
            v2.OutputList();
            if (v2 == s9) { Console.WriteLine("Equal"); }
            else { Console.WriteLine("Not equal"); }
            Console.WriteLine("Power= " + v2.Power + " " + s9.Power);




        }
    }

}