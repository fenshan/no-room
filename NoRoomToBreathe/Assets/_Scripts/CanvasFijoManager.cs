using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum PanelesFijos
{
    Desktop, Whatsapp, Facebook
};

public class CanvasFijoManager : MonoBehaviour {

    public GameManager gm = GameManager.instance;
    public WhatsappManager wm;
    public FacebookManager fm;

    public PanelesFijos panelActivoFijo = PanelesFijos.Desktop;

    public void ActiveWhatsapp()
    {
        gm.ActivarPanel(Canvas.Whatsapp, true);
        gm.ActivarPanel(Canvas.Desktop, false);
        gm.ActivarPanel(Canvas.Facebook, false);
        panelActivoFijo = PanelesFijos.Whatsapp;
    }

    public void ActiveFacebook()
    {
        gm.ActivarPanel(Canvas.Facebook, true);
        gm.ActivarPanel(Canvas.Desktop, false);
        gm.ActivarPanel(Canvas.Whatsapp, false);
        panelActivoFijo = PanelesFijos.Facebook;
    }

    public void ActiveDesktop()
    {
        gm.ActivarPanel(Canvas.Desktop, true);
        gm.ActivarPanel(Canvas.Facebook, false);
        gm.ActivarPanel(Canvas.Whatsapp, false);
        panelActivoFijo = PanelesFijos.Desktop;
    }


    void Update()
    {
        if (gm.state==State.F)
        {
            if (Input.GetKeyUp(KeyCode.Escape))
            {
                //if (panelActivoFijo != PanelesFijos.Desktop && )
                //{
                //    Debug.Log(" app");
                //    ActiveDesktop();
                //}

                if (panelActivoFijo == PanelesFijos.Desktop)
                {
                    //Debug.Log("cerrar");
                    gm.Quit();
                }
            }






        }
    }

}
