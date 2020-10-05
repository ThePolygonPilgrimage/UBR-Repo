using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject[] Menus;

    public GameObject[] MenuButtons;

    public Sprite highlighted;

    public GameObject nextMenu;

    public GameObject previousMenu;

    // Start is called before the first frame update
    void Start()
    {
        //Set Default Menu
        //Set all menus inactive
        SetAllMenusStatus(false);

        //Set the desired menu active
        SetMenuActive("Menu - Welcome");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Set_Play_Menu(Image btnImage)
    {
        //Set previous and next menus
        SetPreviousAndNextMenu("Play");

        //Switch all other buttons back to their default graphic
        for (int i = 0; i < MenuButtons.Length; i++)
        {
            MenuButtons[i].GetComponent<ResetButtonImage>().ResetImage();
        }

        //Switch the button graphic to the highlighted graphic
        btnImage.sprite = highlighted;

        //Set all menus inactive
        SetAllMenusStatus(false);

        //Set the desired menu active
        SetMenuActive("Menu - Play");
    }

    public void Set_Loadout_Menu(Image btnImage)
    {
        //Switch all other buttons back to their default graphic
        for (int i = 0; i < MenuButtons.Length; i++)
        {
            MenuButtons[i].GetComponent<ResetButtonImage>().ResetImage();
        }

        //Switch the button graphic to the highlighted graphic
        btnImage.sprite = highlighted;

        //Set all menus inactive
        SetAllMenusStatus(false);

        //Set the desired menu active
        SetMenuActive("Menu - Loadout");
    }

    public void Set_Soldier_Menu(Image btnImage)
    {
        //Switch all other buttons back to their default graphic
        for (int i = 0; i < MenuButtons.Length; i++)
        {
            MenuButtons[i].GetComponent<ResetButtonImage>().ResetImage();
        }

        //Switch the button graphic to the highlighted graphic
        btnImage.sprite = highlighted;

        //Set all menus inactive
        SetAllMenusStatus(false);

        //Set the desired menu active
        SetMenuActive("Menu - Soldier");
    }

    public void Set_Barracks_Menu(Image btnImage)
    {
        //Switch all other buttons back to their default graphic
        for (int i = 0; i < MenuButtons.Length; i++)
        {
            MenuButtons[i].GetComponent<ResetButtonImage>().ResetImage();
        }

        //Switch the button graphic to the highlighted graphic
        btnImage.sprite = highlighted;

        //Set all menus inactive
        SetAllMenusStatus(false);

        //Set the desired menu active
        SetMenuActive("Menu - Barracks");
    }

    public void Set_BattlePass_Menu(Image btnImage)
    {
        //Switch all other buttons back to their default graphic
        for (int i = 0; i < MenuButtons.Length; i++)
        {
            MenuButtons[i].GetComponent<ResetButtonImage>().ResetImage();
        }

        //Switch the button graphic to the highlighted graphic
        btnImage.sprite = highlighted;

        //Set all menus inactive
        SetAllMenusStatus(false);

        //Set the desired menu active
        SetMenuActive("Menu - BattlePass");
    }

    public void Set_Store_Menu(Image btnImage)
    {
        //Switch all other buttons back to their default graphic
        for (int i = 0; i < MenuButtons.Length; i++)
        {
            MenuButtons[i].GetComponent<ResetButtonImage>().ResetImage();
        }

        //Switch the button graphic to the highlighted graphic
        btnImage.sprite = highlighted;

        //Set all menus inactive
        SetAllMenusStatus(false);

        //Set the desired menu active
        SetMenuActive("Menu - Store");
    }

    public void SetAllMenusStatus(bool setFlag)
    {
        for (int i = 0; i < Menus.Length; i++)
        {
            Menus[i].SetActive(setFlag);
        }
    }

    public void SetMenuActive(string menuName)
    {
        for (int i = 0; i < Menus.Length; i++)
        {
            if (Menus[i].name == menuName)
                Menus[i].SetActive(true);
        }
    }

    public void SetPreviousAndNextMenu(string nextMenu)
    {

    }

    public void CloseSubMenu(string menuName)
    {
        for (int i = 0; i < Menus.Length; i++)
        {
            if(Menus[i].name == menuName)
            {
                Menus[i].transform.GetChild(1).GetComponent<Animator>().SetTrigger("FadeOut");
            }
        }
    }

    public void FadeOutSubMenuTitle()
    {

    }
}
