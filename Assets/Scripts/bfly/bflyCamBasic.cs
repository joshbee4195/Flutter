using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class bflyCamBasic : MonoBehaviour
{

    public GameObject player;
    public Vector3 offset;

    public int score;
    public TextMeshProUGUI scoreCount;
    public float scoreCheck;

    public float scoreRate; //first V is 0.01f

    public GameObject endPopup;

    public TextMeshProUGUI scoreEnd;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = player.transform.position + offset;


        if (Time.timeScale == 1)
        {
            scoreCheck += scoreRate;

            score = (int)scoreCheck;


            //score += 1;

            scoreCount.SetText(score.ToString());
        }

        else
        {
            endPopup.SetActive(true);

            scoreEnd.SetText("Score: " +  score.ToString());
        }
    }

    public void RetryClick()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);

        Time.timeScale = 1;
    }


    //obstacles:
    
    //wind pushing player                       - sideways

    //flowers - static in the way               - from bottom coming up

    //rainfall / lightning / other weather      - from top coming down


}
