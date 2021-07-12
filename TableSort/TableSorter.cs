using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TableSort
{
    public static class TableSorter
    {
        public static List<T> Sort<T>(HashSet<T> tables, HashSet<Tuple<T, T>> tableDependencyEdges) where T : IEquatable<T>
        {
            var elements = new List<T>();
            var independentNodes = new HashSet<T>(tables.Where(n => tableDependencyEdges.All(e => e.Item2.Equals(n) == false)));

            while (independentNodes.Any())
            {
                var n = independentNodes.First();
                independentNodes.Remove(n);
                elements.Add(n);

                foreach (var e in tableDependencyEdges.Where(e => e.Item1.Equals(n)).ToList())
                {
                    var m = e.Item2;

                    tableDependencyEdges.Remove(e);

                    if (tableDependencyEdges.All(me => me.Item2.Equals(m) == false))
                    {
                        independentNodes.Add(m);
                    }
                }
            }

            if (tableDependencyEdges.Any())
            {
                return null;
            }
            else
            {
                return elements;
            }
        }
    }
}
