using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelperClasses
{
    public static class NonParallelExtensions
    {

        public static void AddRange<T>(this ObservableCollection<T> oc, IEnumerable<T> collection)
        {
            if (collection == null)
            {
                //throw new ArgumentNullException("collection");
            }
            foreach (var item in collection)
            {
                oc.Add(item);
            }


        }

        public static void AddRange<T>(this List<T> l, IEnumerable<T> collection)
        {
            if (collection == null)
            {
                //throw new ArgumentNullException("collection");
            }
            foreach (var item in collection)
            {
                l.Add(item);
            }


        }


    }

    public static class Extensions
    {

        public static void AddRange<T>(this ObservableCollection<T> oc, IEnumerable<T> collection)
        {
            if (collection == null)
            {
                //throw new ArgumentNullException("collection");
            }
            Parallel.ForEach(collection, item =>
            {
                oc.Add(item);
            }
            );

        }
    }
}
