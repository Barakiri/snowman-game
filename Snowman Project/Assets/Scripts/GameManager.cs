using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public Checklist Checklist;

    public List<GameObject> RequiredPickups = new List<GameObject>();
    List<GameObject> CollectedPickups;

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
        CollectedPickups = new List<GameObject>();
        Checklist = GameObject.Find("Checklist").GetComponent<Checklist>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    public void CollectPickup(GameObject pickup)
    {
        print("hello");
        CollectedPickups.Add(pickup);
        Checklist.Check(pickup);
    }
}
