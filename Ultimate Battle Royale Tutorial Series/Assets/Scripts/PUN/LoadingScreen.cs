using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class LoadingScreen : MonoBehaviourPunCallbacks
{
    RectTransform rectComponet;
    float rotateSpeed = -200f;

    public Text displayText;

    public string mainMenu;

    [SerializeField] string firstload;
    [SerializeField] string secondload;
    [SerializeField] string thirdload;

    // Start is called before the first frame update
    void Start()
    {
        rectComponet = GetComponent<RectTransform>();
        StartCoroutine(NewTextToShow());
    }

    // Update is called once per frame
    void Update()
    {
        rectComponet.Rotate(0f, 0f, rotateSpeed * Time.deltaTime);
    }

    public IEnumerator NewTextToShow()
    {
        displayText.text = firstload;
        yield return new WaitForSeconds(3f);
        displayText.text = secondload;
        yield return new WaitForSeconds(3f);
        displayText.text = thirdload;
        StartCoroutine(NewScene(mainMenu));
    }

    public IEnumerator NewScene(string newScene)
    {
        yield return new WaitForSeconds(3f);
        PhotonNetwork.JoinLobby();
        PhotonNetwork.LoadLevel(newScene);
    }
}
