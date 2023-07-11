using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerTracker : MonoBehaviour
{
    [SerializeField] string terrainTag;
    private int triggeredCount;

    public bool triggered
    {
        get
        {
            return triggeredCount > 0;
        }
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(terrainTag))
        {
            triggeredCount++;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag(terrainTag))
        {
            triggeredCount--;
        }
    }
}
