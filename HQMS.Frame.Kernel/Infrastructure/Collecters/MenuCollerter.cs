using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HQMS.Frame.Kernel.Infrastructure
{
    public class MenuCollerter : IList<MenuKind>
    {
        IList<MenuKind> Menus = new List<MenuKind>();

        public bool IsReadOnly => Menus.IsReadOnly;

        public int Count => Menus.Count;

        public MenuKind this[int index]
        {
            get => Menus[index];
            set => Menus[index] = value;
        }

        public MenuKind this[string codeIndexArgs]
        {
            set { Menus[Menus.IndexOf(Menus.FirstOrDefault(x => x.Code == codeIndexArgs))] = value; }
            get { return Menus[Menus.IndexOf(Menus.FirstOrDefault(x => x.Code == codeIndexArgs))]; }
        }

        public string GetReferName(string codeIndexArgs)
        {
            return Menus.FirstOrDefault(x => x.Code == codeIndexArgs).ReferName;
        }

        public void Add(MenuKind item)
        {
            Menus.Add(item);
        }

        public void Clear()
        {
            Menus.Clear();
        }

        public bool Contains(string codeArgs)
        {
            return Menus.FirstOrDefault(x => x.Code == codeArgs) != null ? true : false;
        }

        public bool Contains(MenuKind Item)
        {
            return Menus.Contains(Item);
        }

        public void CopyTo(MenuKind[] array, int arrayIndex)
        {
            Menus.CopyTo(array, arrayIndex);
        }

        public IEnumerator<MenuKind> GetEnumerator()
        {
            return Menus.GetEnumerator();
        }

        public bool Remove(MenuKind item)
        {
            return Menus.Remove(item);
        }

        public int IndexOf(MenuKind item)
        {
            return Menus.IndexOf(item);
        }

        public void Insert(int index, MenuKind item)
        {
            Menus.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            Menus.RemoveAt(index);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)Menus).GetEnumerator();
        }
    }
}
