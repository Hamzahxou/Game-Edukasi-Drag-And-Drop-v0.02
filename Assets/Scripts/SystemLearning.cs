using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SystemLearning : MonoBehaviour
{
    [System.Serializable]
    public class DataComponent
    {
        public string Name;
        public Sprite Image;
        public AudioClip Audio;
        public string Deskription;
    }

    public List<DataComponent> DataComponents;

    [Header("Data Learning")]
    public int DataNumber;
    public Image ImageComponent;
    public Text TextComponent;
    public Text TextNumber;
    public Text TextDescription;

    public AudioSource SourceAudio;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        DataNumber = 0;
        SetLearning();
    }

    public void SetLearning()
    {
        ImageComponent.GetComponent<Animation>().Play("ShowImageComponent");
        ImageComponent.sprite = DataComponents[DataNumber].Image;
        TextComponent.text = DataComponents[DataNumber].Name;
        SetAudio();
        TextDescription.text = DataComponents[DataNumber].Deskription;
        TextNumber.text = (DataNumber + 1).ToString() + "/" + DataComponents.Count.ToString();
    }

    public void SetAudio()
    {
        if(SourceAudio.clip != null && SourceAudio.isPlaying)
        {
            SourceAudio.Stop();
        }

        SourceAudio.clip = DataComponents[DataNumber].Audio;
    }

    public void CallAudio()
    {
        if(SourceAudio.clip != null && SourceAudio.isPlaying)
        {
            SourceAudio.Stop();
        }

        SourceAudio.Play();
    }

    public void NextLearning(bool IsNext)
    {
        if (IsNext)
        {
            DataNumber++;
            if(DataNumber >= DataComponents.Count) DataNumber = 0;
        }
        else
        {
            DataNumber--;
            if (DataNumber < 0) DataNumber = DataComponents.Count - 1;
        }

        SetLearning();
    }

}
