using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
   public Text TextScore;
   public Text TextHighestScore;
   public GameObject ObjekTextWin, ObjekTextLose;
   public int TargetScore = 1500;

   private void Start() {
        if(DataQuiz.DataScore > PlayerPrefs.GetInt("score"))
        {
            PlayerPrefs.SetInt("score", DataQuiz.DataScore);
        }

        if(DataQuiz.DataScore >= TargetScore)
        {
            SistemMusicGroup.instance.SoundEffect(4);
            ObjekTextWin.SetActive(true);  
            ObjekTextLose.SetActive(false);  
        }
        else
        {
            SistemMusicGroup.instance.SoundEffect(5);
            ObjekTextWin.SetActive(false); 
            ObjekTextLose.SetActive(true);
        }

        TextScore.text = DataQuiz.DataScore.ToString();
        TextHighestScore.text = PlayerPrefs.GetInt("score").ToString();
        
   }
}
