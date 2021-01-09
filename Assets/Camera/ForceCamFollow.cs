using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceCamFollow : MonoBehaviour {

    public Vector3 offset = new Vector3(0, 7, -1.5f);
    public Vector3 last = Vector3.zero;
    public Vector3 delta = Vector3.zero;
    public float dragSpeed = 0.1f;
    // [Range(0, 1)] public float camSmoothing = 0.5f;

    void FixedUpdate() {
        var cam = Camera.main;

        if (Input.GetMouseButtonUp(1)) {
            delta = Input.mousePosition - last;
            last = Input.mousePosition;
            print(delta);
        }

        cam.transform.eulerAngles = new Vector3(75 + delta.y * dragSpeed, delta.x * dragSpeed, 0);
        cam.transform.position = transform.position + offset;
    }

}
