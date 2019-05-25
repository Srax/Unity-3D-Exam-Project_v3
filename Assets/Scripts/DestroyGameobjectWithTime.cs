using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyGameobjectWithTime : MonoBehaviour
{

    [Header("If useRandom is TRUE, destroy gameObject after a random amount of seconds between minRndTime - maxRndTime")]
    public bool useRandom = false;
    [Range(1f, 60)] public float maxRndTime = 1f;
    [Range(1f, 60)] public float minRndTime = 1f;
    public float destroyInSeconds = 0.1f;

    private void Start()
    {
        if(useRandom == true)
        {
            float rndNum = Random.Range(minRndTime, maxRndTime);
            Destroy(gameObject, rndNum);
        }

        if(useRandom == false)
        {
            Destroy(gameObject, destroyInSeconds);
        }
    }
}
