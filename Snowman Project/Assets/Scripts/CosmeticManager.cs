using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CosmeticManager : MonoBehaviour
{
    public static CosmeticManager Instance;

    public List<Cosmetic> cosmetics = new List<Cosmetic>();
    public int currentBall = 1;


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

        DontDestroyOnLoad(gameObject);
    }

    public void EnableCosmetic(int i, int iBall)
    {
        cosmetics[i].enabled = true;

        CosmeticUpdate(true);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void CosmeticUpdate(bool disable)
    {
        if (disable)
        {
            foreach (var c in cosmetics)
            {
                if (c.ball == currentBall && c.enabled)
                {
                    switch (currentBall)
                    {
                        case 1:
                            PlayerController.Instance.ballOneBase.GetChild(c.iBall).gameObject.SetActive(true);
                            DisableAllInBall(2);
                            DisableAllInBall(3);
                            break;
                        case 2:
                            DisableAllInBall(1);
                            PlayerController.Instance.ballTwoBase.GetChild(c.iBall).gameObject.SetActive(true);
                            DisableAllInBall(3);
                            break;
                        case 3:
                            DisableAllInBall(1);
                            DisableAllInBall(2);
                            PlayerController.Instance.ballThreeBase.GetChild(c.iBall).gameObject.SetActive(true);
                            break;
                        default:
                            break;
                    }
                }
            }
        }
        else
        {
            foreach (var c in cosmetics)
            {
                if (c.enabled)
                {
                    switch (currentBall)
                    {
                        case 1:
                            PlayerController.Instance.ballOneBase.GetChild(c.iBall).gameObject.SetActive(true);
                            break;
                        case 2:
                            PlayerController.Instance.ballTwoBase.GetChild(c.iBall).gameObject.SetActive(true);
                            break;
                        case 3:
                            PlayerController.Instance.ballThreeBase.GetChild(c.iBall).gameObject.SetActive(true);
                            break;
                    }
                }
            }
        }
    }

    void DisableAllInBall(int ball)
    {
        switch (ball)
        {
            case 1:
                for (int i = 0; i >= PlayerController.Instance.ballOneBase.childCount; i++)
                {
                    PlayerController.Instance.ballOneBase.GetChild(i).gameObject.SetActive(false);
                }
                break;
            case 2:
                for (int i = 0; i >= PlayerController.Instance.ballTwoBase.childCount; i++)
                {
                    PlayerController.Instance.ballTwoBase.GetChild(i).gameObject.SetActive(false);
                }
                break;
            case 3:
                for (int i = 0; i >= PlayerController.Instance.ballThreeBase.childCount; i++)
                {
                    PlayerController.Instance.ballThreeBase.GetChild(i).gameObject.SetActive(false);
                }
                break;
        }
    }
}

[Serializable]
public class Cosmetic
{
    public string name;
    public bool enabled = false;
    public int iBall;
    public int ball;
}