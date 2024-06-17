using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomTrailsEmmit : MonoBehaviour
{
    private TrailRenderer trailRender;
    private float duration;
    private float timeStamp;

    // Start is called before the first frame update
    void Start()
    {
        trailRender = GetComponent<TrailRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time > timeStamp + duration)
        {
            duration = Random.Range(0.05f, 0.03f);
            timeStamp = Time.time;
            trailRender.emitting = !trailRender.emitting;
        }
    }
}
