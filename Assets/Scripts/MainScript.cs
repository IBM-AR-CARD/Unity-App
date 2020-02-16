using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using FIMSpace.FLook;
using UnityEngine.SceneManagement;

public class MainScript : MonoBehaviour, IEventSystemHandler
{
    [SerializeField]
    public TextMesh mytext = null;
    public GameObject player = null;
    public GameObject followCamera = null;
    public bool isARScene;
    private String currentAnimation = "idle";
    private String[] characterList = new string[] { "BusinessWomanPFB", "TestMale", "Luffy", "FitFemale", "Jiraiya", "YodaRigged", "BusinessMale", "BusinessFemale", "SmartMale", "UnityChan", "SmartFemale" };

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("started");
        // mytext.text = "hello world!";

        UnityMessageManager.Instance.SendMessageToFlutter("#started#");

        changeCharacter(TrackingScript.currentCharacter);
        changeAnimator(TrackingScript.currentAnimation);


    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < Input.touchCount; ++i)
        {
            Debug.Log("ok");
            // UnityMessageManager.Instance.SendMessageToFlutter("#touched#");

            // if (Input.GetTouch(i).phase.Equals(TouchPhase.Began))
            // {
            //     var hit = new RaycastHit();

            //     Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(i).position);

            //     if (Physics.Raycast(ray, out hit))
            //     {
            //         Debug.Log("touched");
            //         // This method is used to send data to Flutter
            //         UnityMessageManager.Instance.SendMessageToFlutter("Raycast Touched");
            //     }
            // }
        }

        if (Input.GetKeyDown("i"))
        {
            changeAnimator("idle");
        }
        else if (Input.GetKeyDown("t"))
        {
            changeAnimator("talking");
        }
        else if (Input.GetKeyDown("d"))
        {
            changeAnimator("dancing");
        }
        else if (Input.GetKeyDown("c"))
        {
            randomModel();
        }
        else if (Input.GetKeyDown("s"))
        {
            switchSence(isARScene ? "CharScene" : "ARScene");
        }
    }

    public void switchSence(String sceneName)
    {

        SceneManager.LoadScene(sceneName: sceneName);

    }

    // This method is called from Flutter
    public void changeText(String message)
    {
        Debug.Log(message);
        // mytext.text = message;
    }

    // This method is called from Flutter
    public void changeAnimator(String animatorPath)
    {
        GameObject currentPlayer = player.gameObject.transform.GetChild(0).gameObject;
        TrackingScript.currentAnimation = animatorPath;
        changeAnimatorWithObject(currentPlayer, animatorPath);
    }

    public void changeAnimatorWithObject(GameObject currentPlayer, String animatorPath)
    {
        Animator animator = currentPlayer.GetComponent<Animator>();
        animator.runtimeAnimatorController = Resources.Load(animatorPath) as RuntimeAnimatorController;
        //return to card center
        currentPlayer.transform.localRotation = Quaternion.Euler(0, 0, 0);
        currentPlayer.transform.localPosition = Vector3.zero;
        currentAnimation = animatorPath;

        if (isARScene)
        {
            FLookAnimator la = currentPlayer.GetComponent(typeof(FLookAnimator)) as FLookAnimator;
            if (la) la.ObjectToFollow = animatorPath == "dancing" ? null : followCamera.transform; // if not dancing, head start following camera
        }

        Debug.Log("animator changed to " + animatorPath);
    }

    public void changeCharacter(String characterPath)
    {
        Transform currentPlayer = player.gameObject.transform;
        GameObject newChild = Instantiate(Resources.Load("Models/" + characterPath + "/" + characterPath)) as GameObject;
        foreach (Transform child in currentPlayer)
        {
            GameObject.Destroy(child.gameObject);
        }

        newChild.transform.SetParent(player.transform, false);
        if (!TrackingScript.isTracked && isARScene)
        {
            hideObject(newChild);
        }
        Debug.Log("character changed to " + characterPath);
        TrackingScript.currentCharacter = characterPath;
        changeAnimatorWithObject(newChild, currentAnimation);

    }

    public void randomModel()
    {
        String character = characterList[new System.Random().Next(0, characterList.Length)];
        changeCharacter(character);
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
