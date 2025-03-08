using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utils
{
    /// <summary>
    /// Random coin flip
    /// </summary>
    /// <returns> -1 or 1 </returns>
    public static int Flip()
    {
        int r = Random.Range(0, 2);
        r = r == 1 ? 1 : -1;

        return r;
    }
}
