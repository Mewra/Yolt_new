using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CircleMov : MonoBehaviour {

    private Transform _playerTransform;

    public GameObject _player;
    private float speed;
    private Ray cameraRay;
    private Plane groundPlane;
    private float rayLength;
    public Vector3 PointToLook { get; set; }
    private Vector3 destination;

	private void Start ()
    {
        speed = 5f;
        _playerTransform = _player.GetComponent<Transform>();
	}
	
	private void Update ()
    {
        groundPlane = new Plane(Vector3.up, transform.position);
        cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (groundPlane.Raycast(cameraRay, out rayLength))
        {
            PointToLook = cameraRay.GetPoint(rayLength);
        }
        transform.LookAt(new Vector3(PointToLook.x, transform.position.y, PointToLook.z));
        destination = _playerTransform.position + (new Vector3(PointToLook.x, 0f, PointToLook.z) - _playerTransform.position).normalized;
        transform.position = Vector3.Lerp(transform.position, destination, speed * Time.deltaTime);
    }
}