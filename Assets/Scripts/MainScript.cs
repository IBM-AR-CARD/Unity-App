using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainScript : MonoBehaviour
{
    public TextMesh mytext = null;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("started");
        mytext.text="hello world!";
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
