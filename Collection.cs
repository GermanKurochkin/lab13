using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace lab13
{
    public class MyCollection : List<Engine>, IEnumerable
    {
        public new int Count
        {
            get
            {
                return list.Count;
            }
        }
        List<Engine> list;

        public new Engine this[int index]
        {
            get
            {
                if (index < 0 || index > Count)
                {
                    Console.WriteLine("Неверный индекс");
                    return new Engine();
                }
                else return list[index];
            }
            set
            {
                if (index < 0 || index > Count)
                {
                    Console.WriteLine("Неверный индекс");
                }
                else list[index] = value;
            }
        }
        public MyCollection()
        {
            list = new List<Engine>();
        }
        public MyCollection(int size)
        {
            list = new List<Engine>();
            Engine eng = new Engine();
            for (int i = 0; i < size; i++)
            {
                list.Add(eng.MakeRandom());
            }
        }
        public MyCollection(MyCollection collection)
        {
            list = collection.list;
        }

        public void Add()
        {
            Engine eng = new Engine();
            list.Add(eng.MakeRandom());
        }
        public bool Delete(int position)
        {
            if (list.Count < position || position < 0)
            {
                return false;
            }
            else
            {
                list.RemoveAt(position);
                return true;
            }

        }
        public new void Sort()
        {          
            list.Sort();
        }
        new public void Clear()
        {
            list.Clear();
        }
        public void Show()
        {
            foreach (Engine item in list)
            {
                Console.WriteLine(item);
            }
        }

        public new IEnumerator GetEnumerator()
        {
            foreach (var item in list)
            {
                yield return item;
            }
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return (IEnumerator)GetEnumerator();
        }

    }

    public delegate void CollectionHandler(object source, CollectionHandlerEventArgs args);
    public class MyNewCollection : MyCollection
    {
        public string Name { get; set; }
        public List<Engine> list;
        public MyNewCollection()
        {
            list = new List<Engine>();
        }
        public MyNewCollection(int size)
        {
            list = new List<Engine>(size);
            Engine element = new Engine();
            for (int i = 0; i < size; i++)
            {
           
                list.Add(element.MakeRandom());
            }
        }
        public MyNewCollection(MyNewCollection collection)
        {
            list = collection.list;
        }

        public void AddRandom()
        {
            Engine element = new Engine();
            element = element.MakeRandom();
            list.Add(element);
            OnCollectionCountChanged(this, new CollectionHandlerEventArgs(this.Name, "Add", list[list.IndexOf(element)]));
           
        }
        new public void Add(Engine elem)
        {
            list.Add(elem);
            OnCollectionCountChanged(this, new CollectionHandlerEventArgs(this.Name, "Add", list[list.IndexOf(elem)]));
        
        }
        public bool Remove(int position)
        {
            if (list.Count < position || position < 0)
            {
                return false;
            }
            else
            {
                OnCollectionCountChanged(this, new CollectionHandlerEventArgs(this.Name, "Delete", list[position]));
           
                list.RemoveAt(position);
                return true;
            }
        }
        public void ChangeValue(int position)
        {
            if (list.Count < position || position < 0)
            {
                return ;
            }
            Engine element = new Engine();
            OnCollectionReferenceChanged(this, new CollectionHandlerEventArgs(this.Name, "Changed", list[position]));
            list[position] = element.MakeRandom() ;
        }

      
        public new Engine this[int index]
        {
            get
            {
                return list[index];
            }
            set
            {
                OnCollectionReferenceChanged(this, new CollectionHandlerEventArgs(this.Name, "Changed", list[index]));
                list[index] = value;
            }
        }


       
        public event CollectionHandler CollectionCountChanged;
     
        public event CollectionHandler CollectionReferenceChanged;


        public void OnCollectionCountChanged(object source, CollectionHandlerEventArgs args)
        {
            if (CollectionCountChanged != null)
            {
                CollectionCountChanged(source, args);
            }
        }
        public void OnCollectionReferenceChanged(object source, CollectionHandlerEventArgs args)
        {
            if (CollectionReferenceChanged != null)
            {
                CollectionReferenceChanged(source, args);
            }
        }
     
        public void Show()
        {
            Console.WriteLine($"Коллекция {Name}:");
            foreach (var item in this.list)
            {
                Console.WriteLine(item);
            }
        }
    }

    public class CollectionHandlerEventArgs : System.EventArgs
    {
        public string NameCollection { get; set; }
        public string ChangeType { get; set; }
        public object ObjectData { get; set; }

        public CollectionHandlerEventArgs(string name, string type, object obj)
        {
            NameCollection = name;
            ChangeType = type;
            ObjectData = obj;
        }

        public override string ToString()
        {
            return "Имя коллекции: " + NameCollection + "\nОбъект: " + ObjectData + " - " + ChangeType;
        }
    }

   


}
