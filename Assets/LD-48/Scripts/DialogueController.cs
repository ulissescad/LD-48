using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

public class DialogueController : MonoBehaviour
{
    public UnityEvent CreateBadDialogue = new UnityEvent();
    public UnityEvent CreateGoodDialogue = new UnityEvent();

    [SerializeField]
    private Button _confirmButton;
    
    [SerializeField]
    private TMP_Text _dialoguePrefab;
    
    [SerializeField]
    private string [] _introPrases = new string[0];
    
    [SerializeField]
    private string [] _endPrases = new string[0];
    
    [SerializeField]
    private string [] _badPrases = new string[0];
    
    [SerializeField]
    private string [] _goodPrases = new string[0];
    
    private bool bad = false;
    private int index = 0;
    private bool canSpeak=true;

    void Start()
    {
        CreateBadDialogue.AddListener(BadDialogue);
        CreateGoodDialogue.AddListener(GoodDialogue);
        _confirmButton.onClick.AddListener(Intro);
        Intro();
    }

    void BadDialogue()
    {

        var count = transform.childCount;
        
        if (!bad)
        {
            for (int i = 0; i < count; i++)
            {
                var child = transform.GetChild(i).gameObject;
                Destroy(child);
            }

            bad = true;
        }

        if (count < 3)
        {
            var dialogue = Instantiate(_dialoguePrefab, transform);
            dialogue.text = _badPrases[Random.Range(0,_badPrases.Length)];
        }
        else
        {
            var child = transform.GetChild(0).gameObject;
            Destroy(child);
            var dialogue = Instantiate(_dialoguePrefab, transform);
            dialogue.text = _badPrases[Random.Range(0,_badPrases.Length)];
            
        }
        
    }
    
    void GoodDialogue()
    {
        bad = false;
        
        var count = transform.childCount;
        for (int i = 0; i < count; i++)
        {
            var child = transform.GetChild(i).gameObject;
            Destroy(child);
        }
        var dialogue = Instantiate(_dialoguePrefab, transform);
        dialogue.text = _goodPrases[Random.Range(0,_goodPrases.Length)];

    }

    public void Intro()
    {
        _confirmButton.gameObject.SetActive(true);
        
        var count = transform.childCount;
        
        if (count < 3)
        {
            var dialogue = Instantiate(_dialoguePrefab, transform);
            dialogue.text = _introPrases[index];
        }
        else
        {
            var child = transform.GetChild(0).gameObject;
            Destroy(child);
            var dialogue = Instantiate(_dialoguePrefab, transform);
            dialogue.text = _introPrases[index];
            
        }
        
        index++;

        if (index >= _introPrases.Length)
        {
            _confirmButton.gameObject.SetActive(false);
            _confirmButton.onClick.RemoveListener(Intro);
            _confirmButton.onClick.AddListener(Gameover);
            GameManager.SINGLETON.IntroOver();
            index = 0;
        }
    }
    
    public void Gameover()
    {
        if (canSpeak == false) return;
        
        _confirmButton.gameObject.SetActive(true);
        
        var count = transform.childCount;
        
        if (count < 3)
        {
            var dialogue = Instantiate(_dialoguePrefab, transform);
            dialogue.text = _endPrases[index];
        }
        else
        {
            var child = transform.GetChild(0).gameObject;
            Destroy(child);
            var dialogue = Instantiate(_dialoguePrefab, transform);
            dialogue.text = _endPrases[index];
            
        }
        
        index++;

        if (index >= _introPrases.Length-1)
        {
            _confirmButton.gameObject.SetActive(false);
            canSpeak = false;
        }

    }
    
    public void Clear()
    {
        var count = transform.childCount;
        for (int i = 0; i < count; i++)
        {
            var child = transform.GetChild(i).gameObject;
            Destroy(child);
        }
    }
}
