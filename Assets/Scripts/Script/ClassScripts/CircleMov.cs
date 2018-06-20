using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CircleMov : MonoBehaviour {

    private Transform _playerTransform;
    private PhotonView myPhotonView;

    public GameObject _player;
    private float speed;
    private Ray cameraRay;
    private Plane groundPlane;
    private float rayLength;
    public Vector3 PointToLook { get; set; }
    private Vector3 destination;

	private void Start ()
    {
        myPhotonView = GetComponentInParent<PhotonView>();
        speed = 5f;
	}

    private void Update()
    {
        if (!myPhotonView.isMine)
        {
            return;
        }
        groundPlane = new Plane(Vector3.up, transform.position);
        cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (groundPlane.Raycast(cameraRay, out rayLength))
        {
            PointToLook = cameraRay.GetPoint(rayLength);
        }
        transform.parent.LookAt(new Vector3(PointToLook.x, transform.position.y, PointToLook.z));
        if (_playerTransform != null)
        { 
            destination = _playerTransform.position + ((new Vector3(PointToLook.x, 0f, PointToLook.z) - _playerTransform.position).normalized) * 1.8f;
            transform.parent.position = Vector3.Lerp(transform.position, new Vector3(destination.x, destination.y + 2.5f, destination.z), speed * Time.deltaTime);
        }
    }

    public void SetTargetTransform(GameObject player)
    {
        _playerTransform = player.GetComponent<Transform>();
    }
}