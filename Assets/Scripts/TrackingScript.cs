using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class TrackingScript : MonoBehaviour, ITrackableEventHandler
{
    private TrackableBehaviour mTrackableBehaviour;
    public static bool isTracked = false;

    void Start()
    {
        mTrackableBehaviour = GetComponent<TrackableBehaviour>();
        if (mTrackableBehaviour)
        {
            mTrackableBehaviour.RegisterTrackableEventHandler(this);
        }
    }

    public void OnTrackableStateChanged(
                                    TrackableBehaviour.Status previousStatus,
                                    TrackableBehaviour.Status newStatus)
    {
        if (newStatus == TrackableBehaviour.Status.DETECTED ||
            newStatus == TrackableBehaviour.Status.TRACKED ||
            newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
        {
            Debug.Log("TRACKED!!");
            isTracked = true;
            UnityMessageManager.Instance.SendMessageToFlutter("#tracked#");
        }
        else
        {
            Debug.Log("LOST!!");
            isTracked = false;
            UnityMessageManager.Instance.SendMessageToFlutter("#lost#");
        }
    }
}