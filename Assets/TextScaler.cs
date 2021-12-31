using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextScaler : MonoBehaviour
{
    // Start is called before the first frame update

    private string text;
    void Start()
    {
        text = this.GetComponent<TMP_Text>().text;

        this.GetComponent<TMP_Text>().text="";
        StartCoroutine("teste");
        


    }
    
    IEnumerator teste()
    {
        foreach (var VARIABLE in text)
        {
            this.GetComponent<TMP_Text>().text += VARIABLE;
            yield return new WaitForSeconds(0.1f);
        }
        
        StopCoroutine("teste");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
