using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HQMS.Frame.Kernel.Services;

namespace HQMS.Frame.Kernel.Core
{
    public class DataBaseCollecter : ICollection<DataBaseKind>
    {
        ICollection<DataBaseKind> DataBases = new List<DataBaseKind>();

        public bool IsReadOnly => DataBases.IsReadOnly;

        public int Count => DataBases.Count;

        public IDataBaseController this[string nameIndex]
        {
            set { DataBases.FirstOrDefault(x => x.Name == nameIndex).DataBaseController = value; }
            get { return DataBases.FirstOrDefault(x => x.Name == nameIndex).DataBaseController; }
        }

        public DataBaseKind this[int index]
        {
            get { return DataBases.ElementAtOrDefault(index); }
        }

        public void Add(DataBaseKind item)
        {
            DataBases.Add(item);
        }

        public void Clear()
        {
            DataBases.Clear();
        }

        public bool Contains(string nameItem)
        {
            return DataBases.FirstOrDefault(x => x.Name == nameItem) != null ? true : false;
        }

        public bool Contains(DataBaseKind Item)
        {
            return DataBases.Contains(Item);
        }

        public void CopyTo(DataBaseKind[] array, int arrayIndex)
        {
            DataBases.CopyTo(array, arrayIndex);
        }

        public IEnumerator<DataBaseKind> GetEnumerator()
        {
            return DataBases.GetEnumerator();
        }

        public bool Remove(DataBaseKind item)
        {
            return DataBases.Remove(item);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)DataBases).GetEnumerator();
        }
    }
}
