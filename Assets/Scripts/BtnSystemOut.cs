using UnityEngine;

public class BtnSystemOut : MonoBehaviour
{

    public GameObject CanvasConfirmOut;

    public void BtnConfirmOut(bool IsOpen)
    {
        if (IsOpen)
        {
            CanvasConfirmOut.SetActive(true);
        }
        else
        {
            CanvasConfirmOut.SetActive(false);
        }
    }

    public void BtnExitGame()
    {
        Application.Quit();
    }
}
