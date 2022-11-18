using System;
using System.Collections;
using System.Collections.Generic;

namespace IzumiTools
{
    [Serializable]
    public class TypeAndCountList<T> : IEnumerable<TypeAndCountList<T>.TypeAndCount>, IEnumerable
    {
        public TypeAndCountList() { }
        public TypeAndCountList(TypeAndCountList<T> sample)
        {
            foreach (var stackedElement in sample.list)
                list.Add(new TypeAndCount(stackedElement));
        }
        [Serializable]
        public class TypeAndCount
        {
            public T type;
            public int count;
            public TypeAndCount(T type, int count = 1)
            {
                this.type = type;
                this.count = count;
            }
            public TypeAndCount(TypeAndCount sample)
            {
                type = sample.type;
                count = sample.count;
            }
        }
        public List<TypeAndCount> list = new List<TypeAndCount>();
        public bool denyNegativeCount = true;
        public List<T> Types
        {
            get
            {
                List<T> types = new List<T>();
                foreach (var each in list)
                {
                    types.Add(each.type);
                }
                return types;
            }
        }
        public TypeAndCount FindByType(T type)
        {
            return list.Find(each => each.type.Equals(type));
        }
        public int StackCount(T type)
        {
            TypeAndCount find = FindByType(type);
            return find != null ? find.count : 0;
        }
        public int TotalCount()
        {
            int total = 0;
            list.ForEach(each => total += each.count);
            return total;
        }
        public int TypeCount()
        {
            return list.Count;
        }
        public bool IsEmpty => TypeCount() == 0;
        public bool AcceptsCount(int count)
        {
            return count > 0 || !denyNegativeCount;
        }
        public TypeAndCount Set(T type, int count)
        {
            if (!AcceptsCount(count))
                return null;
            TypeAndCount typeAndCount = FindByType(type);
            if (typeAndCount != null)
                typeAndCount.count = count;
            else
                list.Add(typeAndCount = new TypeAndCount(type, count));
            return typeAndCount;
        }
        public TypeAndCount Add(T type, int count = 1)
        {
            TypeAndCount typeAndCount = FindByType(type);
            if (typeAndCount != null)
            {
                if (!AcceptsCount(typeAndCount.count += count))
                    list.Remove(typeAndCount);
            }
            else if (AcceptsCount(count))
                list.Add(typeAndCount = new TypeAndCount(type, count));
            return typeAndCount;
        }
        public TypeAndCount Add(TypeAndCount typeAndCount)
        {
            return Add(typeAndCount.type, typeAndCount.count);
        }
        public void AddAll<T2>(TypeAndCountList<T2> anotherList) where T2 : T
        {
            anotherList.ForEach(each => Add(each.type, each.count));
        }
        public TypeAndCount Remove(T type, int count = 1)
        {
            return Add(type, -count);
        }
        public TypeAndCount Remove<T2>(TypeAndCountList<T2>.TypeAndCount typeAndCount) where T2 : T
        {
            return Remove(typeAndCount.type, typeAndCount.count);
        }
        public void RemoveAll<T2>(TypeAndCountList<T2> anotherList) where T2 : T
        {
            foreach (var typeAndCount in anotherList)
            {
                Remove(typeAndCount);
            }
        }
        public void ForEach(Action<TypeAndCount> action)
        {
            list.ForEach(each => action.Invoke(each));
        }
        public IEnumerator<TypeAndCount> GetEnumerator()
        {
            return list.GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return list.GetEnumerator();
        }
        public void Clear()
        {
            list.Clear();
        }
        public void SortByTypeCount(bool increasing)
        {
            if (increasing)
                list.Sort((stack1, stack2) => stack1.count - stack2.count);
            else
                list.Sort((stack1, stack2) => stack2.count - stack1.count);
        }
        public bool ContentsEquals(TypeAndCountList<T> anotherStackList)
        {
            if (TypeCount() != anotherStackList.TypeCount())
                return false;
            foreach (var stack in list)
            {
                if (anotherStackList.StackCount(stack.type) != stack.count)
                    return false;
            }
            return true;
        }
    }
}