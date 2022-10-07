using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 코루틴 WaitFor관련 객체 생성없이 캐시를 통해 가비지를 줄이기 위한 클래스
// 출처 : https://ejonghyuck.github.io/blog/2016-12-12/unity-coroutine-optimization/
internal static class YieldInstructionCache
{
    public static readonly WaitForEndOfFrame WaitForEndOfFrame = new WaitForEndOfFrame();
    public static readonly WaitForFixedUpdate WaitForFixedUpdate = new WaitForFixedUpdate();
    private static readonly Dictionary<float, WaitForSeconds> waitForSeconds = new Dictionary<float, WaitForSeconds>();

    public static WaitForSeconds WaitForSeconds(float seconds)
    {
        WaitForSeconds wfs;
        if (!waitForSeconds.TryGetValue(seconds, out wfs))
            waitForSeconds.Add(seconds, wfs = new WaitForSeconds(seconds));
        return wfs;
    }
}
