using UnityEngine;
using UnityEngine.UI;

public class BtnSistemMusic : MonoBehaviour
{
    public Sprite[] MusicIconImages;
    public Image ImageIcon;
    public Text TeksPlayerMusic;

    private void OnEnable() {
        if(SistemMusicGroup.instance == null) return;
        if(SistemMusicGroup.instance.SourceMusicBackground == null) return;
        if (TeksPlayerMusic == null) return;

        if(SistemMusicGroup.instance.SourceMusicBackground.isPlaying)
        {
            TeksPlayerMusic.text = "Music On";
        }
        else
        {
             TeksPlayerMusic.text = "Music off";
        }
    }

    public void BtnMusicClick()
    {
        if(SistemMusicGroup.instance.SourceMusicBackground.isPlaying)
        {
            SistemMusicGroup.instance.SourceMusicBackground.Pause();
            ImageIcon.sprite = MusicIconImages[1];
            TeksPlayerMusic.text = "Music off";
        }
        else
        {
            SistemMusicGroup.instance.SourceMusicBackground.UnPause();
            ImageIcon.sprite = MusicIconImages[0];
            TeksPlayerMusic.text = "Music on";
        }
    }
}
