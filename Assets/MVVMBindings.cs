using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public static class MVVMBindings
{
    public static async UniTaskVoid BindToCore<TValue>(IUniTaskAsyncEnumerable<TValue> source,
        Action<TValue> setter, CancellationToken cancellationToken, bool rebindOnError)
    {
        var repeat = false;
        BIND_AGAIN:
        var e = source.GetAsyncEnumerator(cancellationToken);
        try
        {
            while (true)
            {
                bool moveNext;
                try
                {
                    moveNext = await e.MoveNextAsync();
                    repeat = false;
                }
                catch (Exception ex)
                {
                    if (ex is OperationCanceledException) return;

                    if (rebindOnError && !repeat)
                    {
                        repeat = true;
                        goto BIND_AGAIN;
                    }
                    else
                    {
                        throw;
                    }
                }

                if (!moveNext) return;

                setter(e.Current);
            }
        }
        finally
        {
            if (e != null)
            {
                await e.DisposeAsync();
            }
        }
    }

    public static void BindToGOActivity(this IUniTaskAsyncEnumerable<bool> source, GameObject target,
        bool rebindOnError = true)
    {
        void Setter(bool value)
        {
            target.SetActive(value);
        }

        BindToCore(source, Setter, target.GetCancellationTokenOnDestroy(), rebindOnError).Forget();
    }
    public static void BindToTint(this IUniTaskAsyncEnumerable<Color> source, Image target,
        bool rebindOnError = true)
    {
        void Setter(Color value)
        {
            target.color = value;
        }

        BindToCore(source, Setter, target.GetCancellationTokenOnDestroy(), rebindOnError).Forget();
    }
}