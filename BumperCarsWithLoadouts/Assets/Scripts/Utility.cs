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

    /// <summary>
    /// Finds the object furthest away from the current object in a list
    /// </summary>
    /// <param name="objs">The list of objects to retrieve the furthest from</param>
    /// <param name="current">The current object to compare distances to</param>
    /// <returns></returns>
    public static GameObject FindFurthestObject(List<GameObject> objs, GameObject current)
    {
        GameObject target = null;

        //checks for a target that is greater than a certain distance away
        Vector3 farthest = new Vector3(0, 0, 0);
        float farthestDist = farthest.sqrMagnitude;
        //target = null;
        //Vector3 dir = target.transform.position - current.transform.position;
        //float tempDist = dir.sqrMagnitude;

        //compare distance between this car and every other car
        for (int i = 0; i < objs.Count; ++i)
        {
            //get distance between
            Vector3 temp = objs[i].transform.position - current.transform.position;
            float tempDist = temp.sqrMagnitude;

            //check if its farther
            if (tempDist > farthestDist)
            {
                farthestDist = tempDist;
                target = objs[i];
            }
        }

        return target;
    }

    /// <summary>
    /// Finds the object furthest away from the current object in an array
    /// </summary>
    /// <param name="objs">The array of objects to retrieve the furthest from</param>
    /// <param name="current">The current object to compare distances to</param>
    /// <returns></returns>
    public static GameObject FindFurthestObject(GameObject[] objs, GameObject current)
    {
        GameObject target = null;

        //checks for a target that is greater than a certain distance away
        Vector3 farthest = new Vector3(0, 0, 0);
        float farthestDist = farthest.sqrMagnitude;
        //target = null;
        //Vector3 dir = target.transform.position - current.transform.position;
        //float tempDist = farthestDist;

        //compare distance between this car and every other car
        for (int i = 0; i < objs.Length; ++i)
        {
            //get distance between
            Vector3 temp = objs[i].transform.position - current.transform.position;
            float tempDist = temp.sqrMagnitude;

            //check if its farther
            if (tempDist > farthestDist)
            {
                farthestDist = tempDist;
                target = objs[i];
            }
        }

        return target;
    }
}
