using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LRUCache
{

    public class LRU
    {
        public int capacity;
        private int noOfElements = 0;
        public Dictionary<int, LinkedListNode<Data>> dict = new Dictionary<int, LinkedListNode<Data>>();
        public LinkedList<Data> LRUList = new LinkedList<Data>();
        public int CurrNoOfElements
        {
            get
            {
                return this.noOfElements;

            }
        }

        public LRU(int c)
        {
            this.capacity = c;
        }


        public void Clear()
        {
            LRUList.Clear();
        }

        public bool TryGetValue(int ky, out string oVal)
        {

            if (dict.ContainsKey(ky))
            {
                LinkedListNode<Data> dictVal = dict[ky];
                // LinkedListNode<Data> lnode = LRUList.Find(dictVal.Value);
                LRUList.Remove(dictVal);
                LRUList.AddFirst(dictVal);

                //LRUList.Remove()
                oVal = dictVal.Value.val;
                return true;


            }
            oVal = null;
            return false;
        }

        public void Add(int key, string str)
        {

            Data d = new Data(key, str);
            LinkedListNode<Data> val;
            if (dict.Count == 0 && LRUList.Count < capacity)
            {

                val = LRUList.AddFirst(d);
                dict.Add(key, val);
                noOfElements++;
            }
            else
            {
                if (dict.ContainsKey(key))
                {
                    throw new ArgumentException("Key already exists");
                }
                else if (LRUList.Count >= capacity)
                {
                    int keyToRemove = LRUList.Last.Value.index;
                    dict.Remove(keyToRemove);
                    LRUList.RemoveLast();

                    val = LRUList.AddFirst(d);
                    dict.Add(key, val);
                }
                else
                {
                    val = LRUList.AddFirst(d);
                    dict.Add(key, val);
                    noOfElements++;
                }
            }
        }
    }

    public class Data
    {
        public int index;
        public string val;
        public Data(int i, string v)
        {
            this.index = i;
            this.val = v;
        }

    }
    class Program
    {
        static void Main(string[] args)
        {
            LRUTest t = new LRUTest();
            t.Test();
            //  LRU lc = new LRU(5);
            ////  lc.CurrNoOfElements = 3;
            //  Random r = new Random();
            ////  lc.LRUList.AddFirst();
            //  for(int i=0;i<10;i++)
            //  {
            //      int key = r.Next(1, 1000);
            //      string value = "testVal" + key;
            //      lc.Add(key, value);
            //  }
            //  Console.WriteLine("Capacity : {0},Current No.Of Elements : {1}", lc.capacity, lc.CurrNoOfElements);
            //  foreach(KeyValuePair<int,LinkedListNode<Data>> kv in lc.dict)
            //  {
            //      Console.WriteLine("Key: {0},Value: {1}", kv.Key,kv.Value);

            //  }

            //  for(int j =0;j<lc.CurrNoOfElements;j++)
            //  {
            //     Console.WriteLine("Node: {0},{1} ",j, lc.LRUList.ElementAt(j).val);

            //  }
            Console.ReadLine();
        }
    }

    public class LRUTest
    {
        Random r = new Random();

        public void Test()
        {
            LRU lruCache = new LRU(3);
            Console.WriteLine("No.Of ElementsinCache: {0}        Capacity : {1}", lruCache.CurrNoOfElements, lruCache.capacity);
            for (int i = 0; i < 8; i++)
            {
                int key = r.Next(1, 6);
                string value = "testVal" + key;
                string outputValue;
                bool result = lruCache.TryGetValue(key, out outputValue);
                if (result)
                {
                    int diff = string.Compare(value, outputValue);
                    Console.WriteLine("inputValue: {0} found in Cache!!!    CacheValue: {1}", value, outputValue);
                    foreach (var item in lruCache.LRUList)
                    {
                        Console.WriteLine(item.val);
                    }
                }
                else
                {
                    Console.WriteLine("Value : {0} not found in Cache***    Adding into Cache", value);
                    try
                    {
                        lruCache.Add(key, value);
                    }
                    catch (Exception e)
                    {

                        Console.WriteLine("Exception is:" + e.Message);
                    }
                    foreach (var item in lruCache.LRUList)
                    {
                        Console.WriteLine(item.val);
                    }
                }
                Console.WriteLine("No.Of ElementsinCache: {0}        Capacity : {1}", lruCache.CurrNoOfElements, lruCache.capacity);
            }
        }

    }
}
