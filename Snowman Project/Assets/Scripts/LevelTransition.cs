using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelTransition : MonoBehaviour
{
    public int nextLevel;
    public Image fade;
    public float fadeDuration = 1f;
    float fadeCurrent = 0f;
    bool go = false;
    bool startLevel = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (startLevel)
        {
            fade.color = Color.Lerp(Color.black, Color.clear, fadeCurrent / fadeDuration);

            if (fadeCurrent >= fadeDuration)
            {
                fadeCurrent = 0f;
                fade.color = Color.clear;
                startLevel = false;
            }

            fadeCurrent += Time.deltaTime;
        }
        if (go)
        {
            if (fadeCurrent >= fadeDuration) 
            { 
                SceneManager.LoadScene(nextLevel);
            }
            fade.color = new Color(fade.color.r, fade.color.g, fade.color.b, fadeCurrent / fadeDuration);

            fadeCurrent += Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            go = true;
        }
    }
}
