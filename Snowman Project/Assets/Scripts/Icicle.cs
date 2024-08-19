using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Icicle : MonoBehaviour
{
    public float sizeBreakThreshold;
    Rigidbody2D rb;
    bool broken = false;
    [SerializeField] public bool canMove = false;
    public float meltLength = 3f;
    float currentMeltTime = 0;
    IcicleBase baseIcicle;
    public bool canBeBroken = true;
    GameObject child;

    public float regrowthDuration = 3f;
    float currentRegrowthTime = 0f;

    // Start is called before the first frame update
    void Start()
    {
        child = transform.GetChild(0).gameObject;
        rb = child.GetComponent<Rigidbody2D>();
        baseIcicle = transform.parent.GetComponent<IcicleBase>();
    }

    // Update is called once per frame
    void Update()
    {
        if (canBeBroken)
        {
            if (GameManager.Instance.playerSize.radius >= sizeBreakThreshold)
                CanMove(true);
            else
                CanMove(false);
        }

        //print(transform.parent.GetComponent<IcicleBase>().icicleStartScale);

        if (broken)
        {
            SetSize(Mathf.Abs((currentMeltTime / meltLength) - 1f));
            currentMeltTime += Time.deltaTime;
            if (currentMeltTime > meltLength)
            {
                Destroy(gameObject);
            }
        }

        if (!canBeBroken)
        {
            if (currentRegrowthTime >= regrowthDuration)
            {
                SetSize(1f);
                currentRegrowthTime = 0f;
                canBeBroken = true;
                gameObject.transform.localScale = transform.parent.GetComponent<IcicleBase>().icicleStartScale;
            }
            else
            {
                currentRegrowthTime += Time.deltaTime;
                SetSize(currentRegrowthTime / regrowthDuration);
            }
        }
    }

    public void CollisionEvent()
    {
        if (!rb.isKinematic)
        {
            broken = true;
            baseIcicle.isBroken = true;
        }
    }

    public void SetSize(float t)
    {
        gameObject.transform.localScale = Vector3.Lerp(Vector3.zero, transform.parent.GetComponent<IcicleBase>().icicleStartScale, t);
    }

    public void CanMove(bool input)
    {
        child = transform.GetChild(0).gameObject;
        rb = child.GetComponent<Rigidbody2D>();
        rb.isKinematic = !input;
        canMove = input;
    }
}
