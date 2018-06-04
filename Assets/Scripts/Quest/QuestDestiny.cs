using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class QuestDestiny
{
    public static bool ShouldBadThingHappens()
    {
        float random = Random.Range(0f, 1f);
        float threshold = 0.5f;
        
        // TODO: use Karma to manupulate threshold

        return random < threshold;
    }
}
