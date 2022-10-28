using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class CheckGameCompletion : MonoBehaviour
{
    public AudioClip lossSound;
    public AudioClip winSound;
    public Sprite winPose;
    public Sprite losePose;

    [Header("UI")]
    public Text topText;
    public Button botLeftButton;
    public Button botRightButton;
    public Image playerPose;


    // Start is called before the first frame update
    void Start()
    {
        if(GameManager.instance.IsWinConditionMet)
        {
            Time.timeScale = 1;
            GameManager.instance.currentLevel.PlaySoundEffect(winSound);
            topText.text = "YOU WON!";
            botLeftButton.GetComponentInChildren<Text>().text = "REPLAY";
            botRightButton.gameObject.GetComponentInChildren<Text>().text = "QUIT";
            playerPose.sprite = winPose;
        }
        else
        {
           
            GameManager.instance.currentLevel.PlaySoundEffect(lossSound);
            topText.text = "CONTINUE?";
            botLeftButton.gameObject.GetComponentInChildren<Text>().text = "YES";
            botRightButton.gameObject.GetComponentInChildren<Text>().text = "NO";
            playerPose.sprite = losePose;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
