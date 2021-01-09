using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DaynightCycleSystem : MonoBehaviour {

    public int orbitalRadius = 200;
    public float orbitDuration = 2; // Minutes
    public float angle = 0;

    public float elapsedTime = 0;

    private Transform _sun;
    private Transform _moon;

    void Start() {
        _sun = GameObject.FindGameObjectWithTag("Sun").transform;
        _moon = GameObject.FindGameObjectWithTag("Moon").transform;
    }

    void Update() {
        var xOff = Mathf.Sin(angle * Mathf.Deg2Rad) * orbitalRadius;
        var yOff = Mathf.Cos(angle * Mathf.Deg2Rad) * -orbitalRadius;

        _sun.LookAt(Vector3.zero);
        _sun.position = new Vector3(xOff, yOff, 0);

        _moon.LookAt(Vector3.zero);
        _moon.position = new Vector3(-xOff, -yOff, 0);

        var progress = elapsedTime / (orbitDuration * 60);
        angle = 360 * progress + 90; // Initial position sun at dawn

        elapsedTime += Time.deltaTime;
    }

}
