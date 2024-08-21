using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CosmeticManager : MonoBehaviour
{
    public static CosmeticManager Instance;

    public List<Cosmetic> cosmetics = new List<Cosmetic>();
    public List<float> sizeStorage = new List<float>();
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

    public void StoreSize(int i)
    {
        sizeStorage[i-1] = PlayerController.Instance.gameObject.GetComponent<Sizer>().radius;
    }

    public void SetFinal()
    {
        FinalSizeSetter.Instance.ball1.localScale = Vector3.one * sizeStorage[0];
        FinalSizeSetter.Instance.ball2.localScale = Vector3.one * sizeStorage[1];
        FinalSizeSetter.Instance.ball3.localScale = Vector3.one * sizeStorage[2];

        foreach (var c in cosmetics)
        {
            if (c.enabled)
            {
                if (c.ball == 1)
                {
                    FinalSizeSetter.Instance.ball1.GetChild(0).GetChild(c.iBall).gameObject.SetActive(true);
                }
                if (c.ball == 2)
                {
                    FinalSizeSetter.Instance.ball2.GetChild(0).GetChild(c.iBall).gameObject.SetActive(true);
                }
                if (c.ball == 3)
                {
                    FinalSizeSetter.Instance.ball3.GetChild(0).GetChild(c.iBall).gameObject.SetActive(true);
                }
            }
        }
    }

    public void CosmeticUpdate(bool disable)
    {
        if (disable)
        {
            for (int i = 0; i < cosmetics.Count; i++)
            {
                //Debug.Log($"{i}, {cosmetics[i].enabled}");
            }
            foreach (var c in cosmetics)
            {
                if (c.ball == currentBall && c.enabled)
                {
                    switch (currentBall)
                    {
                        case 1:
                            PlayerController.Instance.transform.GetChild(currentBall).GetChild(c.iBall).gameObject.SetActive(true);
                            Debug.Log(PlayerController.Instance.transform.GetChild(currentBall).GetChild(c.iBall).gameObject.name);
                            DisableAllInBall(2);
                            DisableAllInBall(3);
                            break;
                        case 2:
                            DisableAllInBall(1);
                            PlayerController.Instance.transform.GetChild(currentBall).GetChild(c.iBall).gameObject.SetActive(true);
                            DisableAllInBall(3);
                            break;
                        case 3:
                            DisableAllInBall(1);
                            DisableAllInBall(2);
                            PlayerController.Instance.transform.GetChild(currentBall).GetChild(c.iBall).gameObject.SetActive(true);
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
                            PlayerController.Instance.transform.GetChild(currentBall).GetChild(c.iBall).gameObject.SetActive(true);
                            break;
                        case 2:
                            PlayerController.Instance.transform.GetChild(currentBall).GetChild(c.iBall).gameObject.SetActive(true);
                            break;
                        case 3:
                            PlayerController.Instance.transform.GetChild(currentBall).GetChild(c.iBall).gameObject.SetActive(true);
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
                for (int i = 0; i >= PlayerController.Instance.transform.GetChild(ball).childCount; i++)
                {
                    PlayerController.Instance.transform.GetChild(ball).GetChild(i).gameObject.SetActive(false);
                }
                break;
            case 2:
                for (int i = 0; i >= PlayerController.Instance.transform.GetChild(ball).childCount; i++)
                {
                    PlayerController.Instance.transform.GetChild(ball).GetChild(i).gameObject.SetActive(false);
                }
                break;
            case 3:
                for (int i = 0; i >= PlayerController.Instance.transform.GetChild(ball).childCount; i++)
                {
                    PlayerController.Instance.transform.GetChild(ball).GetChild(i).gameObject.SetActive(false);
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