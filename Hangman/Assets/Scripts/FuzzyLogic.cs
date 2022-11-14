using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuzzyLogic : MonoBehaviour
{

    public AnimationCurve TimeonTaskshort;
    public AnimationCurve TimeonTaskmedium;
    public AnimationCurve TimeonTasklong;

    int totNumber;

    float totshortValue = 0f;
    float totmedValue = 0f;
    float totlongValue = 0f;

    public int evaluateValue(int time)
    {
        totshortValue = TimeonTaskshort.Evaluate(time);
        totmedValue = TimeonTaskmedium.Evaluate(time);
        totlongValue = TimeonTasklong.Evaluate(time);

        if (totshortValue > totmedValue && totshortValue > totlongValue)
        {
            totNumber = 1;
        }

        if (totmedValue > totshortValue && totmedValue > totlongValue)
        {
            totNumber = 2;
        }

        if (totlongValue > totshortValue && totlongValue > totmedValue)
        {
            totNumber = 3;
        }
        return totNumber;
    }
}
