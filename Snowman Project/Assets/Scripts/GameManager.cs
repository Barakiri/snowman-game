using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public bool usePickups = true;

    public List<Collectible> optionalPickups = new List<Collectible>();

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

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //if(Input.GetKeyDown(KeyCode.R))
        //{
        //    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        //}


    }


    public void CollectPickup(GameObject pickup)
    {
        for (int i = 0; i < optionalPickups.Count; i++)
        {
            if (optionalPickups[i].pickup == pickup)
            {
                optionalPickups[i].grabbed = true;
                CosmeticManager.Instance.cosmetics[i].enabled = true;
                CosmeticManager.Instance.CosmeticUpdate(true);
            }
        }
    }
}

[Serializable]
public class Collectible
{
    public string name;
    public GameObject pickup;
    public int cosmetic;
    [HideInInspector] public bool grabbed = false;
}
