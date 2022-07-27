using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowText : MonoBehaviour
{
    private string textValue;
    public Text textElement;

    private int[] score;
    // Start is called before the first frame update
    void Start()
    {
        score = new int[2];
    }

    // Update is called once per frame
    void Update()
    {
        score[0] = PlayerPrefs.GetInt("PlayerScore");
        score[1] = PlayerPrefs.GetInt("EnemyScore");
        textValue = string.Format("{0} : {1}", score[0], score[1]);
        textElement.text = textValue;
    }
}
