using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayerParticle : MonoBehaviour
{
    public static FollowPlayerParticle Instance;


    public Vector3 offset;
    public ParticleSystem system1;
    public ParticleSystem system2;

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

    void Update()
    {
        transform.position = PlayerController.Instance.transform.position + new Vector3(offset.x, -PlayerController.Instance.gameObject.GetComponent<Sizer>().radius, offset.z);
    }
}
