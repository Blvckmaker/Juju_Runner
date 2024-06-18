using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FizzBuz : MonoBehaviour
{
    [SerializeField] int _total = 100;
    // Start is called before the first frame update
    void Start()
    {
        FizzBuzz();
    }

    void FizzBuzz()
    {
        for (int i = 1; i <= _total; i++)
        {
            if (i % 3 == 0 && i % 5 == 0)
                Debug.Log("FizzBuzz");
            else if (i % 3 == 0)
                    Debug.Log("Fizz");
                else if (i % 5 == 0)
                    Debug.Log("Buzz");
                else
                    Debug.Log(i);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
