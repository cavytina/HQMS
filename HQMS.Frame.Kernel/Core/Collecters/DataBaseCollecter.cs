using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HQMS.Frame.Kernel.Services;

namespace HQMS.Frame.Kernel.Core
{
    public class DataBaseCollecter : IList<DataBaseKind>
    {
        IList<DataBaseKind> DataBases = new List<DataBaseKind>();

        public bool IsReadOnly => DataBases.IsReadOnly;

        public int Count => DataBases.Count;

        public DataBaseKind this[int index]
        {
            get => DataBases[index];
            set => DataBases[index] = value;
        }

        public DataBaseKind this[string nameIndex]
        {
            set { DataBases[DataBases.IndexOf(DataBases.FirstOrDefault(x => x.Name == nameIndex))] = value; }
            get { return DataBases[DataBases.IndexOf(DataBases.FirstOrDefault(x => x.Name == nameIndex))]; }
        }

        public IDataBaseController GetDataBaseController(string nameArgs)
        {
            return DataBases.FirstOrDefault(x => x.Name == nameArgs).DataBaseController;
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

        public int IndexOf(DataBaseKind item)
        {
            return DataBases.IndexOf(item);
        }

        public void Insert(int index, DataBaseKind item)
        {
            DataBases.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            DataBases.RemoveAt(index);
        }
    }
}
