using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HQMS.Frame.Kernel.Services;

namespace HQMS.Frame.Kernel.Core
{
    public class LogCollecter : IList<LogKind>
    {
        IList<LogKind> Logs = new List<LogKind>();

        public bool IsReadOnly => Logs.IsReadOnly;

        public int Count => Logs.Count;

        public LogKind this[int index]
        {
            get => Logs[index];
            set => Logs[index] = value;
        }

        public LogKind this[string nameIndex]
        {
            set { Logs[Logs.IndexOf(Logs.FirstOrDefault(x => x.Name == nameIndex))] = value; }
            get { return Logs[Logs.IndexOf(Logs.FirstOrDefault(x => x.Name == nameIndex))]; }
        }

        public ILogController GetLogController(string nameArgs)
        {
            return Logs.FirstOrDefault(x => x.Name == nameArgs).LogController;
        }

        public void Add(LogKind item)
        {
            Logs.Add(item);
        }

        public void Clear()
        {
            Logs.Clear();
        }

        public bool Contains(string nameItem)
        {
            return Logs.FirstOrDefault(x => x.Name == nameItem) != null ? true : false;
        }

        public bool Contains(LogKind Item)
        {
            return Logs.Contains(Item);
        }

        public void CopyTo(LogKind[] array, int arrayIndex)
        {
            Logs.CopyTo(array, arrayIndex);
        }

        public IEnumerator<LogKind> GetEnumerator()
        {
            return Logs.GetEnumerator();
        }

        public bool Remove(LogKind item)
        {
            return Logs.Remove(item);
        }

        public int IndexOf(LogKind item)
        {
            return Logs.IndexOf(item);
        }

        public void Insert(int index, LogKind item)
        {
            Logs.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            Logs.RemoveAt(index);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)Logs).GetEnumerator();
        }
    }
}
