using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class PlayerController : MonoBehaviour {

    public int MoveSpeed = 4;
    public Subject<Vector3> MoveDirection;
    
    // ! Debug
    public Plane plane;

    Animator _anim;
    CharacterController _controller;

    void Start() {
        MoveDirection = new Subject<Vector3>();
        plane = new Plane(Vector3.up, 0);

        _anim = GetComponent<Animator>();
        _controller = GetComponent<CharacterController>();
    }

    void Update() {
        var h = Input.GetAxisRaw("Horizontal");
        var v = Input.GetAxisRaw("Vertical");
        // var h = Keyboard.current.aKey.isPressed ? -1 : (Keyboard.current.dKey.isPressed ? 1 : 0); // a d
        // var v = Keyboard.current.sKey.isPressed ? -1 : (Keyboard.current.wKey.isPressed ? 1 : 0); // s w

        Vector3 dir = MoveSpeed * new Vector3(h, 0, v);

        if (h != 0 || v != 0) { // Walking
            MoveDirection.OnNext(dir);
            _anim.SetBool("walking", true);

            _controller.SimpleMove(dir);
        } else _anim.SetBool("walking", false);

        var doSit = Input.GetKey(KeyCode.LeftShift);
        _anim.SetBool("Sitting", doSit);
    }

    void FixedUpdate() {
        // Camera.main.transform.position 
        // Rotate transform yaw towards cursor
        var pos = GetCursorWorldPos();
        float theta = Vector3.SignedAngle(Vector3.forward, pos - transform.position, Vector3.up);

        var old = transform.eulerAngles;
        // Perform rotation
        transform.eulerAngles = new Vector3(old.x, theta, old.z);

        MoveDirection
            .DistinctUntilChanged(v => _controller.SimpleMove(v));
    }

    public Vector3 GetCursorWorldPos() {
        float distance;
        Vector3 worldPosition = Vector3.zero;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (plane.Raycast(ray, out distance))
            worldPosition = ray.GetPoint(distance);

        return worldPosition;
    }


}


// Aim player at mouse pos
// Expose input observable
// weapon (melee) system
// spell system
// inventory
// break trees for wood item in inventory
// make grid for placement of 1x1 cubes (check blocks around in grid)
// wood items can be placed at cursor pos runtime as blocks
// make enemy prefab
// enemy FOV eyes with targets in view list observable
    // calculate next steps see if the projected distance overlaps objects in FOV view
// make day night cycle (with varying automated fog and darkness)
// spawn enemies over the day (grow from 0% scale to 75% at sunset and 100% at night)
// night time enemies are buffed and head towards core

