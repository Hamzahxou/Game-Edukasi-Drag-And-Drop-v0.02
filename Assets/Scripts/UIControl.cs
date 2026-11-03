using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class UIControl : MonoBehaviour
{
    public bool  IsTransition, IsNotTransition;
    string SaveNameScene;

    private void Awake() {
        if(IsTransition && IsNotTransition)
        {
            gameObject.SetActive(false);
        }
    }
    public void BtnSound()
    {
        SistemMusicGroup.instance.SoundEffect(0);
    }



    public void BtnChangeScene(string SceneName)
    {
        this.gameObject.SetActive(true);
        SaveNameScene = SceneName;
        GetComponent<Animator>().Play("TransitionAnimationOut");
    }
    public void BtnRestart()
    {
        // DataQuiz.DataTimes = DataQuiz.MaxTimes;
        GameSystem.Instance.RestartData();
        // SaveNameScene = SceneManager.GetActiveScene().name;
        SaveNameScene = "Quiz0";
        this.gameObject.SetActive(true);
        GetComponent<Animator>().Play("TransitionAnimationOut");
    }
    public void MoveScene()
    {
        SceneManager.LoadScene(SaveNameScene);
    }

   
}
