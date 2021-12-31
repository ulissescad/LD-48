using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadLevel : MonoBehaviour
{
    [SerializeField]
    private string _sceneName;
    
    private Button _button; 
    // Start is called before the first frame update
    void Start()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(Load);
    }

    private void Load()
    {
        SceneManager.LoadScene(_sceneName);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
