using System;
using System.Collections;
using System.Collections.Generic;

namespace FakeDb
{
    public class Db : IEnumerable<object[]>
    {
        private List<object[]> _data;

        public Db()
        {
            _data = new List<object[]>();
            for (int i = 0; i < 10; i++)
            {
                _data.Add(new object[] { i, i.ToString() });
            }
        }

        public IEnumerator GetEnumerator()
        {
            return _data.GetEnumerator();
        }

        IEnumerator<object[]> IEnumerable<object[]>.GetEnumerator()
        {
            return _data.GetEnumerator();
        }
    }
}
