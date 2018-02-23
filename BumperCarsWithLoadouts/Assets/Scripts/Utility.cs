using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class for methods that are used often and all other scripts should have access to
/// </summary>
public static class Utility {

    /// <summary>
    /// Maps a value from one range to another
    /// </summary>
    /// <param name="value">Value to map</param>
    /// <param name="inputFrom">Min of the current range</param>
    /// <param name="inputTo">Max of the current range</param>
    /// <param name="outputFrom">Min of the desired range</param>
    /// <param name="outputTo">Max of the desired range</param>
    /// <returns></returns>
    public static float MapValue(this float value, float inputFrom, float inputTo, float outputFrom, float outputTo)
    {
        return (value - inputFrom) / (inputTo - inputFrom) * (outputTo - outputFrom) + outputFrom;
    }

    public static void SetPhysicsSettings()
    {
        Physics.IgnoreLayerCollision(15, 15, true);
    }
}
