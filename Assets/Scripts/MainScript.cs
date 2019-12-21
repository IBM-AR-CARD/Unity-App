﻿using System.Collections;
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
    private String currentAnimation = "idle";
    private String[] characterList = new string[]{ "TestMale", "Luffy" };

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

        if (Input.GetKeyDown("i")){
            changeAnimator("idle");
        }else if (Input.GetKeyDown("t")){
            changeAnimator("talking");
        }else if (Input.GetKeyDown("d")){
            changeAnimator("dancing");
        }else if (Input.GetKeyDown("c")){
            String character = characterList[new System.Random().Next(0,characterList.Length) ];
            changeCharacter(character);
        }
    }

    // This method is called from Flutter
    public void changeText(String message)
    {
        Debug.Log(message);
        mytext.text=message;
    }

    // This method is called from Flutter
    public void changeAnimator(String animatorPath){
        GameObject currentPlayer = player.gameObject.transform.GetChild(0).gameObject;
        changeAnimatorWithObject(currentPlayer, animatorPath);
    }

    public void changeAnimatorWithObject(GameObject currentPlayer, String animatorPath){
        Animator animator = currentPlayer.GetComponent<Animator>();
        animator.runtimeAnimatorController = Resources.Load(animatorPath) as RuntimeAnimatorController;
        //return to card center
        currentPlayer.transform.localRotation = Quaternion.Euler(0, -180, 0);
        currentPlayer.transform.localPosition = Vector3.zero;
        currentAnimation = animatorPath;
        Debug.Log("animator changed to " + animatorPath);
    }

    public void changeCharacter(String characterPath){
        GameObject currentPlayer = player.gameObject.transform.GetChild(0).gameObject;
        GameObject newChild = Instantiate(Resources.Load("Models/" + characterPath + "/" + characterPath)) as GameObject;
        GameObject.Destroy(currentPlayer);
        newChild.transform.SetParent(player.transform,false);
        if(!TrackingScript.isTracked){
            hideObject(newChild);
        }
        Debug.Log("character changed to " + characterPath);
        changeAnimatorWithObject(newChild, currentAnimation);
    }


    //ref: https://developer.vuforia.com/forum/unity-extension-technical-discussion/creating-child-object-image-targets-runtime
    private void hideObject(GameObject go)
    {
        Renderer[] rendererComponents = go.GetComponentsInChildren<Renderer>();
        Collider[] colliderComponents = go.GetComponentsInChildren<Collider>();

        // Disable rendering:
        foreach (Renderer component in rendererComponents)
        {
            component.enabled = false;
        }

        // Disable colliders:
        foreach (Collider component in colliderComponents)
        {
            component.enabled = false;
        }
    }
}
