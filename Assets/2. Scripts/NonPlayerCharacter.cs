using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NonPlayerCharacter : MonoBehaviour
{
    public float displayTime = 4.0f;
    public GameObject dialogBox;
    private float displayTimer;
    // Start is called before the first frame update
    void Start()
    {
        dialogBox.SetActive(false);
        displayTimer = -1.0f;
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
}
