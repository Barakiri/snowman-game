using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalSizeSetter : MonoBehaviour
{
    public static FinalSizeSetter Instance;

    public Transform ball1;
    public Transform ball2;
    public Transform ball3;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

    }
}
