using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IcicleBase : MonoBehaviour
{
    Vector3 icicleStartingPoint;
    Quaternion icicleStartingRotation;
    [HideInInspector] public bool isBroken = false;
    public GameObject iciclePrefab;
    [HideInInspector] public Vector3 icicleStartScale;

    public float regrowthCooldown = 10f;
    float currentRegrowthCooldownTime = 0f;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < transform.childCount; i++) 
        {
            if (transform.GetChild(i).CompareTag("Icicle"))
            {
                icicleStartingPoint = transform.GetChild(i).GetComponent<Transform>().localPosition;
                icicleStartingRotation = transform.GetChild(i).GetComponent<Transform>().localRotation;
                icicleStartScale = transform.GetChild(i).GetComponent<Transform>().localScale;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isBroken)
        {
            if (currentRegrowthCooldownTime >= regrowthCooldown) 
            {
                currentRegrowthCooldownTime = 0f;
                GameObject icicle;
                icicle = Instantiate(iciclePrefab, icicleStartingPoint + transform.localPosition, icicleStartingRotation * transform.localRotation, transform);
                icicle.GetComponent<Icicle>().canBeBroken = false;
                icicle.GetComponent<Icicle>().SetSize(0f);
                icicle.GetComponent<Icicle>().CanMove(false);
                isBroken = false;
            }
            currentRegrowthCooldownTime += Time.deltaTime;
        }
    }
}