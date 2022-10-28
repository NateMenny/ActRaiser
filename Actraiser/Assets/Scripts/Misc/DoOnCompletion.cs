using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;

public class DoOnCompletion : MonoBehaviour
{
    [SerializeField] VideoPlayer vp;
    [SerializeField] Text text;

    
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
        StartCoroutine("EnableObjectAfterSeconds", vp.length+0.424);
    }

    IEnumerator EnableObjectAfterSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        text.text = "press 'ESC' to quit";
    }
}
