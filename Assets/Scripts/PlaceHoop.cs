using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARRaycastManager))]
public class PlaceHoop : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Instantiates this hoop prefab on a plane at the touch location.")]
    GameObject m_HoopPrefab;

    [SerializeField]
    [Tooltip("Instantiates this ball prefab in front of the AR Camera.")]
    GameObject m_BallPrefab;

    ARRaycastManager m_RaycastManager;

    static List<ARRaycastHit> s_Hits = new List<ARRaycastHit>();

    private bool isPlaced = false;

    void Awake()
    {
        m_RaycastManager = GetComponent<ARRaycastManager>();
    }

    void Update()
    {
        if (isPlaced)
            return;

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                if (m_RaycastManager.Raycast(touch.position, s_Hits, TrackableType.PlaneWithinPolygon))
                {
                    Pose hitPose = s_Hits[0].pose;

                    // Instantiate the hoop
                    GameObject spawnedHoop = Instantiate(m_HoopPrefab, hitPose.position, Quaternion.AngleAxis(180, Vector3.up));
                    spawnedHoop.transform.parent = transform.parent;

                    // Instantiate the ball
                    GameObject spawnedBall = Instantiate(m_BallPrefab);
                    spawnedBall.transform.parent = m_RaycastManager.transform.Find("AR Camera").gameObject.transform;

                    // Disable ARPlaneManager to stop drawing planes
                    ARPlaneManager planeManager = FindObjectOfType<ARPlaneManager>();
                    if (planeManager != null)
                    {
                        planeManager.enabled = false;
                    }

                    // Set the flag to true to avoid further placements
                    isPlaced = true;
                }
            }
        }
    }
}
