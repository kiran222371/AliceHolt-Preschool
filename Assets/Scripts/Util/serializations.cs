using System;
using System.Collections;
using System.Collections.Generic;

#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;
# endif

namespace Prechool.Serialization
{
    [Serializable]
    public struct SPair<T1, T2>
    {
        public T1 first;
        public T2 second;
        public T1 Key => first;
        public T2 Value => second;
        public SPair(T1 first, T2 second)
        {
            this.first = first;
            this.second = second;
        }
    }

    [Serializable]
    public class SMap<T1, T2> : IEnumerable
    {
        public List<SPair<T1, T2>> pairs;
        public SMap()
        {
            pairs = new List<SPair<T1, T2>>();
        }
        public SMap(List<SPair<T1, T2>> pairs)
        {
            this.pairs = pairs;
        }
        public bool ContainsKey(T1 key)
        {
            foreach (var pair in pairs)
            {
                if (EqualityComparer<T1>.Default.Equals(pair.Key, key))
                {
                    return true;
                }
            }
            return false;
        }

        public bool ContainsValue(T2 value)
        {
            foreach (var pair in pairs)
            {
                if (EqualityComparer<T2>.Default.Equals(pair.Value, value))
                {
                    return true;
                }
            }
            return false;
        }

        public T2 this[T1 key]
        {
            get
            {
                foreach (var pair in pairs)
                {
                    if (EqualityComparer<T1>.Default.Equals(pair.Key, key))
                    {
                        return pair.Value;
                    }
                }
                throw new KeyNotFoundException($"Key '{key}' not found.");
            }
            set
            {
                for (int i = 0; i < pairs.Count; i++)
                {
                    if (EqualityComparer<T1>.Default.Equals(pairs[i].Key, key))
                    {
                        pairs[i] = new SPair<T1, T2>(key, value);
                        return;
                    }
                }
                pairs.Add(new SPair<T1, T2>(key, value));
            }
        }
        public void Add(T1 key, T2 value)
        {
            if (ContainsKey(key))
            {
                throw new ArgumentException($"An element with the same key '{key}' already exists.");
            }
            pairs.Add(new SPair<T1, T2>(key, value));
        }
        public IEnumerator GetEnumerator()
        {
            return pairs.GetEnumerator();
        }

    }

#if UNITY_EDITOR

    [CustomPropertyDrawer(typeof(SPair<,>))]
    public class SPairDrawer : PropertyDrawer
    {
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            var container = new VisualElement();
            container.style.flexDirection = FlexDirection.Row;
            var firstProperty = property.FindPropertyRelative("first");
            var secondProperty = property.FindPropertyRelative("second");

            var firstField = new PropertyField(firstProperty, "");
            var secondField = new PropertyField(secondProperty, "");

            firstField.style.flexGrow = 1;
            secondField.style.flexGrow = 1;

            container.Add(firstField);
            container.Add(secondField);

            return container;
        }
    }
#endif
}