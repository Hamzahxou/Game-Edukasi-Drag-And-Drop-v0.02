using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DataQuiz
{
    public static int MaxTimes = 60; // 1 minute
    public static int DataLevel, DataScore, DataHealth, DataTimes;
}

public class GameSystem : MonoBehaviour
{
    public static GameSystem Instance;
    int MaxLevel = 5;

    [Header("Game Data")]
    public bool GameActive = false;
    public bool GameOver;
    public int MaxHealth = 6;
    private float WidthHealt;
    public int Target;
    public int CurrentData;

    [Header("UI Components")]
    public Text LevelText;
    public Text TimeText;
    public Text ScoreText;
    public RectTransform HealthImage;
    
    [Header("Object UI")]
    public GameObject GUIPause;
    public GameObject GUITransition;

    [System.Serializable]
    public class GameData
    {
        public string Name;
        public Sprite Image;
    }

    [Header("Settings")]
    public GameData[] DataGames;
    public ObjectDrop[] ObjectDrops;
    public ItemDrag[] ItemDrags;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        // GameActive = false;
        GameOver    = false;
        if (HealthImage != null)
        {
            HealthImage.sizeDelta = new Vector2(160 * MaxHealth, HealthImage.sizeDelta.y);
            WidthHealt = HealthImage.sizeDelta.x;
        }
        ResetData();
        Target = ObjectDrops.Length;
        RandomQuiz();
        GameActive = true;
        CurrentData = 0;
    }

    void ResetData()
    {
        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "Quiz0")
        {
         RestartData();
        }
    }

    public void RestartData()
    {
        DataQuiz.DataLevel = 1;
        DataQuiz.DataTimes = DataQuiz.MaxTimes;
        DataQuiz.DataScore = 0;
        DataQuiz.DataHealth = MaxHealth;
    }

    private float s;
    void Update()
    {
        if (GameActive && !GameOver)
        {
            if (DataQuiz.DataTimes > 0)
            {
                s += Time.deltaTime;
                if (s >= 1)
                {
                    DataQuiz.DataTimes--;
                    s = 0;
                }
            }

            if(DataQuiz.DataTimes <= 0)
            {
                //Game Fail No Time
                GameOver = true;
                GameActive = false;
                GUITransition.GetComponent<UIControl>().BtnChangeScene("GameOver");
            }

            if(DataQuiz.DataHealth  <= 0)
            {
                // Game Over No Have Health
                GameOver = true;
                GameActive = false;
                GUITransition.GetComponent<UIControl>().BtnChangeScene("GameOver");
            }

            if(CurrentData >= Target)
            {
                //Game Win
                GameOver = true;
                GameActive = false;

                if(DataQuiz.DataLevel < (MaxLevel))
                {
                    // No transition
                    DataQuiz.DataTimes = DataQuiz.MaxTimes;
                    SceneManager.LoadScene("Quiz" + DataQuiz.DataLevel);
                    
                    DataQuiz.DataLevel++;
                    // Transition Animation Out
                    // GUITransition.GetComponent<UIControl>().BtnChangeScene("Quiz" + DataQuiz.DataLevel);

                }
                else
                {
                    //Game Over
                    GUITransition.GetComponent<UIControl>().BtnChangeScene("GameOver");
                }

            }

            
            SetInfoUI();
        }
    }

    public void RandomQuiz()
    {
        if (DataGames.Length < ItemDrags.Length)
        {
            Debug.LogError("DataGames kurang dari jumlah ItemDrags!");
            return;
        }
       
        List<int> indexPool = new List<int>();
        for (int i = 0; i < DataGames.Length; i++) indexPool.Add(i);
        
        for (int i = 0; i < indexPool.Count; i++)
        {
            int temp = indexPool[i];
            int randomIndex = Random.Range(i, indexPool.Count);
            indexPool[i] = indexPool[randomIndex];
            indexPool[randomIndex] = temp;
        }

        List<int> indexItemDrag = new List<int>();
        for (int i = 0; i < ItemDrags.Length; i++) indexItemDrag.Add(i);
       
        for (int i = 0; i < indexItemDrag.Count; i++)
        {
            int temp = indexItemDrag[i];
            int randomIndex = Random.Range(i, indexItemDrag.Count);
            indexItemDrag[i] = indexItemDrag[randomIndex];
            indexItemDrag[randomIndex] = temp;
        }

        for (int i = 0; i < ItemDrags.Length; i++)
        {
            int dataIndex = indexPool[i];      
            int posisiIndex = indexItemDrag[i]; 
            ItemDrags[posisiIndex].IDDrag = dataIndex; 
            ItemDrags[posisiIndex].TextComponent.text = DataGames[dataIndex].Name;
        }

        for (int i = 0; i < ObjectDrops.Length; i++)
        {
            int dataIndex = indexPool[i]; 
            ObjectDrops[i].Drop.IDDrop = dataIndex;
            ObjectDrops[i].ImageComponent.sprite = DataGames[dataIndex].Image;
        }
    }

    public void SetInfoUI()
    {
        if (LevelText != null) LevelText.text = (DataQuiz.DataLevel).ToString();
        if (TimeText != null) TimeText.text = FormatTime(DataQuiz.DataTimes);
        if (ScoreText != null)
        {
            if(DataQuiz.DataScore <= 0) DataQuiz.DataScore = 0;
            ScoreText.text = DataQuiz.DataScore.ToString();
        }

        if (HealthImage != null)
        {
            float FillRatio = (float)DataQuiz.DataHealth / (float)MaxHealth;
            if(FillRatio <= 0) FillRatio = 0;
            HealthImage.sizeDelta = new Vector2(WidthHealt * FillRatio, HealthImage.sizeDelta.y);
        }
    }


    public void BtnPause(bool IsPause)
    {
        GameActive = !IsPause;
        // if(IsPause)
        // {
        //     GameActive = false;
        //     GUIPause.SetActive(true);
        // }
        // else
        // {
        //     GameActive = true;
        //     GUIPause.SetActive(false);
        // }
    }

    private string FormatTime(int time)
    {
        int Minute = Mathf.FloorToInt(time / 60);
        int Second = Mathf.FloorToInt(time % 60);
        return Minute.ToString("00") + ":" + Second.ToString("00");
    }
}