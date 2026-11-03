using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class ItemDrag : MonoBehaviour
{

    [HideInInspector] public Vector2 SavePosition;
    [HideInInspector] public bool IsTrigger;

    Transform SaveObject;

    public Text TextComponent;

    public int IDDrag;


    public UnityEvent OnDrapTrue;
    public UnityEvent OnDrapDown;
    public UnityEvent OnDrap;

    void Start() {
        SavePosition = transform.position;
    }

    private void OnMouseDown() {
       
    }

    private void OnMouseUp() {
        if (IsTrigger)
        {
            int ID_Drop = SaveObject.GetComponent<ItemDrop>().IDDrop;

            if(IDDrag == ID_Drop)
            {
                transform.SetParent(SaveObject);
                transform.localPosition = Vector3.zero;
                transform.localScale = new Vector2(1f, 1f);
                SaveObject.GetComponent<SpriteRenderer>().enabled = false;

                OnDrapTrue.Invoke();

                // cuccess
                DataQuiz.DataScore += 100;
                GameSystem.Instance.CurrentData++;

                SistemMusicGroup.instance.SoundEffect(1);
            }
            else
            {
                transform.position = SavePosition;
                 OnDrapDown.Invoke();

                // fail
                DataQuiz.DataScore -= 50;
                DataQuiz.DataHealth--;
                SistemMusicGroup.instance.SoundEffect(2);
            }
        }
        else
        {
            transform.position = SavePosition;
            OnDrapDown.Invoke();

            // no trigger
        }
    }

    private void OnMouseDrag() {
        Vector2 PosTarget = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = PosTarget;
        OnDrap.Invoke();
    }

    private void OnTriggerStay2D(Collider2D trig) {
        if(trig.gameObject.CompareTag("Drop"))
        {
            IsTrigger = true;
            SaveObject = trig.gameObject.transform;
        }
    }

    private void OnTriggerExit2D(Collider2D trig) {
        if(trig.gameObject.CompareTag("Drop"))
        {
            IsTrigger = false;
        }
    }
}
