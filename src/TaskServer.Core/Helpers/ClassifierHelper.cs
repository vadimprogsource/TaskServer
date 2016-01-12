using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskServer.Interfaces;
using TaskServer.Objects;

namespace TaskServer.Core.Helpers
{
    public static class ClassifierHelper
    {

        public static IEnumerable<TOutput> Merge<TKey, TInput, TOutput>(this IEnumerable<TInput> @in, IEnumerable<TOutput> @out, Func<TInput, TKey> inKeySelector, Func<TOutput, TKey> outKeySelector,Func<TInput,TOutput,TOutput> compositeInOut, Func<TInput, TOutput> createOut)
        {
            Dictionary<TKey, TOutput> outHash = @out.ToDictionary(outKeySelector);

            foreach (TInput x in @in)
            {
                TOutput y;

                if (outHash.TryGetValue(inKeySelector(x), out y))
                {
                    yield return compositeInOut(x, y);
                    continue;
                }

                yield return createOut(x);
            }


        }


        public static IEnumerable<IAggregateClassifier> Aggregate1(this IEnumerable<IClassifier> key,IEnumerable<IAggregateClassifier> data)
        {
            Dictionary<int, IAggregateClassifier> dataHash = data.ToDictionary(x => x.Id);

            foreach(IClassifier x in key)
            {
                AggregateClassifier cls = new AggregateClassifier { Id = x.Id, Name = x.Name, Total = 0 };

                IAggregateClassifier y;

                if (dataHash.TryGetValue(x.Id, out y))
                {
                    cls.Total = y.Total;
                }

                yield return cls;

            }
        }
    }
}
