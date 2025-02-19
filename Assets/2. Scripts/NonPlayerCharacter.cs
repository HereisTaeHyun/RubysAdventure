using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NonPlayerCharacter : MonoBehaviour
{
    public float displayTime = 4.0f;
    public GameObject dialogBox;
    public int leftRobots;

    private float displayTimer;
    [SerializeField] private TextMeshProUGUI talk;
    [SerializeField] private TextMeshProUGUI talkShadow;
    // Start is called before the first frame update
    void Start()
    {
        dialogBox.SetActive(false);
        displayTimer = -1.0f;

        leftRobots = GameObject.FindGameObjectsWithTag("ENEMY").Length;
        SetDisplayText(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(displayTimer >= 0)
        {
            displayTimer -= Time.deltaTime;
            if(displayTimer < 0)
            {
                dialogBox.SetActive(false);
            }
        }
    }

    public void DisplayDialog()
    {
        displayTimer = displayTime;
        dialogBox.SetActive(true);
    }

    public void NoticeRobotFixed()
    {
        leftRobots -= 1;
        bool isEnd = (leftRobots <= 0);
        // if(leftRobots <= 0)
        // {
        //     isEnd = true;
        // }
        // else
        // {
        //     isEnd = false;
        // }
        SetDisplayText(isEnd);
    }

    private void SetDisplayText(bool isEnd)
    {
        if(isEnd == true)
        {   
            talk.text = $"All Done!\nYou are\nThe best Fox!";
            talkShadow.text = talk.text;         
        }
        else if(isEnd == false)
        {
            talk.text = $"Help!\nFIx the robots!\n Left:{leftRobots}";
            talkShadow.text = talk.text;
        }
    }
}
