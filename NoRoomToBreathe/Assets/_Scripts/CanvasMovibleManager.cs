using System.Collections;
using System.Collections.Generic;
using UnityEngine;


enum PanelesMovibles
{
    Lateral, Settings
};

public class CanvasMovibleManager : MonoBehaviour {

    public GameManager gm = GameManager.instance;
    PanelesMovibles panelActivo = PanelesMovibles.Lateral;

    public void ActiveSettings()
    {
        Debug.Log("lo tocas");
        gm.ActivarPanel(Canvas.Settings, true);
        gm.ActivarPanel(Canvas.Lateral, false);
        panelActivo = PanelesMovibles.Settings;
    }

    public void ActiveLateral()
    {
        gm.ActivarPanel(Canvas.Lateral, true);
        gm.ActivarPanel(Canvas.Settings, false);
        panelActivo = PanelesMovibles.Lateral;
    }

    void Update()
    {
        if (gm.state==State.L || gm.state == State.PuttingL)
        {
            if (Input.GetKeyUp(KeyCode.Escape))
            {
                if (panelActivo==PanelesMovibles.Settings)
                {
                    //Debug.Log("settings");
                    ActiveLateral();
                }

                else if (panelActivo==PanelesMovibles.Lateral)
                {
                    //Debug.Log("lateral");
                    gm.pantallaLateral = false;
                }
            }






        }     
    }


}
