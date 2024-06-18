using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Questions : MonoBehaviour
{
    int _score;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void SaveScore()
    {
        _score = PlayerPrefs.GetInt("Score");
        _score++;
        PlayerPrefs.SetInt("Score", _score);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
