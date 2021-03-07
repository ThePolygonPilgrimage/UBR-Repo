using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Runtime.InteropServices;

public class PlayButtonDrawer : MonoBehaviour
{
    //Variables
    public static PlayButtonDrawer instance;
    public Animator drawAnimator;
    public bool drawerOpen;

    [Header("TextMeshPro Objects")]
    public TextMeshProUGUI selectedMode;
    public TextMeshProUGUI topMode;
    public TextMeshProUGUI midMode;
    public TextMeshProUGUI botMode;

    public int orderSet;
    public int selectedPosition;

    public int[] order;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        orderSet = 0;
        order = new int[] { 1, 2, 3, 4 };
    }

    public void SwitchOrderSet(int setNum)
    {
        switch(setNum)
        {
            case 0:
                //Set Order for Solo
                order = new int[] { 1, 2, 3, 4 };
                break;

            case 1:
                //Set Order for Duos
                order = new int[] { 2, 1, 3, 4 };
                break;

            case 2:
                //Set Order for Trios
                order = new int[] { 3, 1, 2, 4 };
                break;

            case 4:
                //Set Order for Teams
                order = new int[] { 4, 1, 2, 3 };
                break;

            default:
                break;
        }
    }

    public void OpenDrawer()
    {
        drawAnimator.SetBool("Close", false);
        drawAnimator.SetBool("Open", true);
    }

    public void CloseDrawer()
    {
        drawAnimator.SetBool("Open", false);
        drawAnimator.SetBool("Close", true);
    }

    public void DrawerPullClicked()
    {
        if(!drawerOpen) //drawOpen = false;
        {
            OpenDrawer();
            drawerOpen = true;
        }
        else
        {
            CloseDrawer();
            drawerOpen = false;
        }
    }

    public void DrawerButtonPressed(int position)
    {
        //Get the index of order for the postion entered
        selectedPosition = order[position];

        SwitchOrderSet(position);

        ModeSelect(selectedPosition);

        drawerOpen = false;
    }

    public void ModeSelect(int mode)
    {
        switch(mode)
        {
            case 1:
                //Change Selected to
                selectedMode.text = "Solo - 1 Player";

                //Change Top TMP to
                topMode.text = "Duos - 2 Player";

                //Change Mid TMP to
                midMode.text = "Trio - 3 Players";

                //Change Bot TMP to
                botMode.text = "Team - 4 Players";

                //Close the drawer
                CloseDrawer();

                break;

            case 2:
                //Change Selected to
                selectedMode.text = "Duos - 2 Player";

                //Change Top TMP to
                topMode.text = "Solo - 1 Player";

                //Change Mid TMP to
                midMode.text = "Trio - 3 Players";

                //Change Bot TMP to
                botMode.text = "Team - 4 Players";

                //Close the drawer
                CloseDrawer();

                break;

            case 3:
                //Change Selected to
                selectedMode.text = "Trio - 3 Players";

                //Change Top TMP to
                topMode.text = "Solo - 1 Player";

                //Change Mid TMP to
                midMode.text = "Duos - 2 Player";

                //Change Bot TMP to
                botMode.text = "Team - 4 Players";

                //Close the drawer
                CloseDrawer();

                break;

            case 4:
                //Change Selected to
                selectedMode.text = "Team - 4 Players";

                //Change Top TMP to
                topMode.text = "Solo - 1 Player";

                //Change Mid TMP to
                midMode.text = "Duos - 2 Player";

                //Change Bot TMP to
                botMode.text = "Trio - 3 Players";

                //Close the drawer
                CloseDrawer();

                break;

            default:
                break;
        }
    }
}
