using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.Experimental.XR;
using System;
using UnityEngine.XR.ARSubsystems;

public class ARspawner : MonoBehaviour
{
    public GameObject placementIndicator;

    ARRaycastManager RayMan;
    ARSessionOrigin arOrigin;
    Pose placementPose;
    bool placementPoseIsValid = false;

    void Start()
    {
        arOrigin = FindObjectOfType<ARSessionOrigin>();
        RayMan = FindObjectOfType<ARRaycastManager>();
    }


    void Update()
    {
        UpdatePlacementPose();
        UpdatePlacementIndicator();
    }

    private void UpdatePlacementIndicator()
    {
        if (placementPoseIsValid)
        {
            placementIndicator.SetActive(true);
            placementIndicator.transform.SetPositionAndRotation(placementPose.position, placementPose.rotation);
        }
        else
        {
            placementIndicator.SetActive(false);
        }
    }

    private void UpdatePlacementPose()
    {
        var screenCenter = Camera.current.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));
        var hits = new List<ARRaycastHit>();
        //arOrigin.Raycast(screenCenter,hits, TrackableType.Planes);
        //bool Raycast(screenCenter, hits, TrackableType trackableTypeMask = TrackableType.All);
        RayMan.Raycast(screenCenter, hits, UnityEngine.XR.ARSubsystems.TrackableType.Planes);


        placementPoseIsValid = hits.Count > 0;
        if (placementPoseIsValid)
        {
            placementPose = hits[0].pose;
        }
    }
}
