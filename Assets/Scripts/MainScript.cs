using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MainScript : MonoBehaviour, IEventSystemHandler
{
    [SerializeField]
    public TextMesh mytext = null;
    public GameObject player = null;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("started");
        mytext.text="hello world!";

        UnityMessageManager.Instance.SendMessageToFlutter("started");

        changeAnimator("idle");

        
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < Input.touchCount; ++i)
        {
            Debug.Log("ok");
            UnityMessageManager.Instance.SendMessageToFlutter("touched init!");

            if (Input.GetTouch(i).phase.Equals(TouchPhase.Began))
            {
                var hit = new RaycastHit();

                Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(i).position);

                if (Physics.Raycast(ray, out hit))
                {
                    Debug.Log("touched");
                    // This method is used to send data to Flutter
                    UnityMessageManager.Instance.SendMessageToFlutter("The cube feels touched.");
                }
            }
        }
    }

    // This method is called from Flutter
    public void changeText(String message)
    {
        Debug.Log(message);
        mytext.text=message;
    }

    public void changeAnimator(String animatorPath){
        Animator animator = player.gameObject.GetComponent<Animator>();
        animator.runtimeAnimatorController = Resources.Load(animatorPath) as RuntimeAnimatorController;
    }
}
