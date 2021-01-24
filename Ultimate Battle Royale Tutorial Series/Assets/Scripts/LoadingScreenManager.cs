using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;
using TMPro;

public class LoadingScreenManager : MonoBehaviour
{
    //Variables
    [Header("Static Instance")]
    public static LoadingScreenManager instance;

    [Header("Spinning Circle")]
    public RectTransform imgRect;
    public float rotateSpeed = -200f;

    [Header("Loading Texts")]
    public string[] loadingTexts;
    public TextMeshProUGUI loadingTextDisplay;

    [Header("Background Image")]
    public Animator backgroundAnimator;
    public enum backgroundAnimType { BottomRight, BottomLeft, UpperRight, Center }
    public backgroundAnimType backgroundAnim;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        //Kick off the LoadingTexts function
        StartCoroutine(ShowLoadingTexts());

        //Setup our background animation
        switch (backgroundAnim)
        {
            case backgroundAnimType.BottomRight:
                backgroundAnimator.SetInteger("BackgroundAnimation", 1);
                break;
            case backgroundAnimType.BottomLeft:
                backgroundAnimator.SetInteger("BackgroundAnimation", 2);
                break;
            case backgroundAnimType.UpperRight:
                backgroundAnimator.SetInteger("BackgroundAnimation", 3);
                break;
            case backgroundAnimType.Center:
                backgroundAnimator.SetInteger("BackgroundAnimation", 4);
                break;
            default:
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Rotate the Circle Spinner
        imgRect.Rotate(0f, 0f, rotateSpeed * Time.deltaTime);

        //When we are connected lets load our settings
        if (!PhotonNetwork.IsConnected)
            LaunchManager.instance.LoadSettings();
    }

    public IEnumerator ShowLoadingTexts()
    {
        //Loop through the texts and show each one, then
        //pause for 3 seconds
        for (int i = 0; i < loadingTexts.Length; i++)
        {
            loadingTextDisplay.text = loadingTexts[i];
            yield return new WaitForSeconds(3f);
        }

        Debug.Log("Loading the Main Menu");
        //Then load the main player lobby
        StartCoroutine(LoadScene("Player Lobby"));
    }

    public IEnumerator LoadScene(string newScene)
    {
        //For now we will wait a short bit.  Later this will work out how much
        //longer we need to wait.
        yield return new WaitForSeconds(5f);
        PhotonNetwork.JoinLobby();
        PhotonNetwork.LoadLevel(newScene);
    }
}
