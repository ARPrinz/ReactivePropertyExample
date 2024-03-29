using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class DataModel : MonoBehaviour
{
    private readonly AsyncReactiveProperty<bool> activityflag = new AsyncReactiveProperty<bool>(false);

    public IReadOnlyAsyncReactiveProperty<bool> ActiveFlag => activityflag;

    public void SwitchFlag()
    {
        activityflag.Value = !activityflag.Value;
    }
}