using System;
using System.Collections;
using System.Collections.Generic;

namespace ConsoleApp3
{
    delegate bool MyDel<T>(T x);
    internal static class link
    {

        public static IEnumerable<T> Where<T>(this IEnumerable<T> source, MyDel<T> D)
        {
            return new WhereIEnumerable<T>(source, D);
        }
        public class WhereIEnumerable<T>: IEnumerable<T>
        {
            private readonly IEnumerable<T> _source;
            private readonly MyDel<T> _predicate;

            public WhereIEnumerable(IEnumerable<T> source, MyDel<T> D)
            {
                _source = source;
                _predicate = D;
            }
            public IEnumerator<T> GetEnumerator()
            {
                return new  WhereIEnumerator(_source, _predicate);
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }

            public class WhereIEnumerator : IEnumerator<T>
            {
                private readonly IEnumerable<T> _source;
                private readonly MyDel<T> _predicate;
                private IEnumerator<T> _enumerator;
                public WhereIEnumerator(IEnumerable<T> source, MyDel<T> D)
                {
                    _source = source;
                    _predicate = D;
                    _enumerator = source.GetEnumerator();
                }
                public T Current => _enumerator.Current;
                object IEnumerator.Current => Current;

                
                public bool MoveNext()
                {
                    while (_enumerator.MoveNext())
                    {
                        if (_predicate(_enumerator.Current))
                        {
                            return true;
                        }
                    }
                    return false;
                }

                public void Reset()
                {
                    _enumerator = _source.GetEnumerator();
                }
                public void Dispose()
                {
                    _enumerator.Dispose();
                }

            }
        }


        public static IEnumerable<T> Take<T>(this IEnumerable<T> source,int value)
        {
            return new TakeIEnumerable<T>(source, value);
        }
        public class TakeIEnumerable<T> : IEnumerable<T>
        {
            private readonly IEnumerable<T> _source;
            private readonly int value;

            public TakeIEnumerable(IEnumerable<T> source, int value)
            {
                _source = source;
                this.value = value;
            }
            public IEnumerator<T> GetEnumerator()
            {
                return new TakeIEnumerator(_source, value);
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }

            public class TakeIEnumerator : IEnumerator<T>
            {
                private readonly IEnumerable<T> _source;
                private readonly int value;
                private  int _taken;
                private IEnumerator<T> _enumerator;
                public TakeIEnumerator(IEnumerable<T> source, int value)
                {
                    _source = source;
                    this.value = value;
                    _enumerator = source.GetEnumerator();
                    _taken = 0;
                }
                public T Current => _enumerator.Current;
                object IEnumerator.Current => Current;

                public bool MoveNext()
                {
                    if (_taken >= value)
                    {
                        return false;
                    }

                    if (_enumerator.MoveNext())
                    {
                        _taken++;
                        return true;
                    }

                    return false;
                }

                public void Reset()
                {
                    _enumerator = _source.GetEnumerator();
                }
                public void Dispose()
                {
                    _enumerator.Dispose();
                }

            }
        }
    }
}
