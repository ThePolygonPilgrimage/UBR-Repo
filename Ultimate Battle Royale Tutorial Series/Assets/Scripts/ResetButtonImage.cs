using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResetButtonImage : MonoBehaviour
{
    public Sprite defaultImage;

  public void ResetImage()
  {
        this.GetComponent<Image>().sprite = defaultImage;
  }
}
