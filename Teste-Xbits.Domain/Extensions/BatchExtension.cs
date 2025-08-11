namespace Teste_Xbits.Domain.Extensions;

public static class BatchExtension
{
    public static List<List<T>> PartitionInChunksOf<T>(this List<T> items, int count)
    {
        var chunks = new List<List<T>>();
        var current = 0;
        while (current < items.Count)
        {
            chunks.Add(items.Skip(current).Take(count).ToList());
            current += count;
        }

        return chunks;
    }

    public static IEnumerable<List<T>> PartitionInChunksForEnumerable<T>(this IEnumerable<T> source, int chunkSize)
    {
        var list = new List<T>();
        foreach (var item in source)
        {
            list.Add(item);
            if (list.Count == chunkSize)
            {
                yield return list;
                list = [];
            }
        }

        if (list.Count > 0)
        {
            yield return list;
        }
    }
}