using UnityEngine;

public class BtnInfoComponent : MonoBehaviour
{
    public GameObject CanvasDescription;
    public void BtnShowDescription(bool IsOpen)
    {
        if (IsOpen)
        {
            CanvasDescription.SetActive(true);
        }
        else
        {
            CanvasDescription.SetActive(false);
        }
    }
}
