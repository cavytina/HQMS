using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HQMS.Frame.Kernel.Core
{
    public class PathCollecter : IList<BaseKind>
    {
        IList<BaseKind> Paths = new List<BaseKind>();

        public bool IsReadOnly => Paths.IsReadOnly;

        public int Count => Paths.Count;

        public BaseKind this[int index]
        {
            get => Paths[index];
            set => Paths[index] = value;
        }

        public BaseKind this[string nameIndex]
        {
            set { Paths[Paths.IndexOf(Paths.FirstOrDefault(x => x.Name == nameIndex))] = value; }
            get { return Paths[Paths.IndexOf(Paths.FirstOrDefault(x => x.Name == nameIndex))]; }
        }

        public string GetContent(string nameArgs)
        {
            return Paths.FirstOrDefault(x => x.Name == nameArgs).Content;
        }

        public void Add(BaseKind item)
        {
            Paths.Add(item);
        }

        public void Clear()
        {
            Paths.Clear();
        }

        public bool Contains(string nameItem)
        {
            return Paths.FirstOrDefault(x => x.Name == nameItem) != null ? true : false;
        }

        public bool Contains(BaseKind Item)
        {
            return Paths.Contains(Item);
        }

        public void CopyTo(BaseKind[] array, int arrayIndex)
        {
            Paths.CopyTo(array, arrayIndex);
        }

        public IEnumerator<BaseKind> GetEnumerator()
        {
            return Paths.GetEnumerator();
        }

        public bool Remove(BaseKind item)
        {
            return Paths.Remove(item);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)Paths).GetEnumerator();
        }

        public int IndexOf(BaseKind item)
        {
            return Paths.IndexOf(item);
        }

        public void Insert(int index, BaseKind item)
        {
            Paths.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            Paths.RemoveAt(index);
        }
    }
}
