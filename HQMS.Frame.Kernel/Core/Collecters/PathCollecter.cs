using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HQMS.Frame.Kernel.Core
{
    public class PathCollecter : ICollection<BaseKind>
    {
        ICollection<BaseKind> AppPaths = new List<BaseKind>();

        public bool IsReadOnly => AppPaths.IsReadOnly;

        public int Count => AppPaths.Count;

        public string this[string nameIndex]
        {
            set { AppPaths.FirstOrDefault(x => x.Name == nameIndex).Content = value; }
            get { return AppPaths.FirstOrDefault(x => x.Name == nameIndex).Content; }
        }

        public void Add(BaseKind item)
        {
            AppPaths.Add(item);
        }

        public void Clear()
        {
            AppPaths.Clear();
        }

        public bool Contains(string nameItem)
        {
            return AppPaths.FirstOrDefault(x => x.Name == nameItem) != null ? true : false;
        }

        public bool Contains(BaseKind Item)
        {
            return AppPaths.Contains(Item);
        }

        public void CopyTo(BaseKind[] array, int arrayIndex)
        {
            AppPaths.CopyTo(array, arrayIndex);
        }

        public IEnumerator<BaseKind> GetEnumerator()
        {
            return AppPaths.GetEnumerator();
        }

        public bool Remove(BaseKind item)
        {
            return AppPaths.Remove(item);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)AppPaths).GetEnumerator();
        }
    }
}
