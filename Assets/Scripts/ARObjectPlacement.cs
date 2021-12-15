using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class ARObjectPlacement : MonoBehaviour {

    public ARSessionOrigin ar_session_origin;

    public List<ARRaycastHit> raycastHits = new List<ARRaycastHit>();

    public GameObject cube;

    GameObject instantiateCube;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // 1) Detect the user touch when tap screen
        // 2) Project a raycast
        // 3) Instantiate a virtual cube at the point where raycast meets the detected plane
        if (Input.GetMouseButton(0)) {
           bool collision = ar_session_origin.GetComponent<ARRaycastManager>().Raycast(Input.mousePosition, raycastHits, UnityEngine.XR.ARSubsystems.TrackableType.PlaneWithinPolygon);
           
           if (collision) {

                if(instantiateCube == null)
                {
                    instantiateCube = Instantiate(cube);

                    foreach (var plane in ar_session_origin.GetComponent<ARPlaneManager>().trackables)
                    {
                        plane.gameObject.SetActive(false);
                    }
                    ar_session_origin.GetComponent<ARPlaneManager>().enabled = false;
                }

                instantiateCube.transform.position = raycastHits[0].pose.position;
            } 
        }
    }
}
