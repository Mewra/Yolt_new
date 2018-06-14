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
        if (!myPhotonView.isMine)
            return;
        speed = 5f;
        // _playerTransform = _player.GetComponent<Transform>();
	}

    private void Update()
    {
        if (!myPhotonView.isMine)
            return;
        groundPlane = new Plane(Vector3.up, transform.position);
        cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (groundPlane.Raycast(cameraRay, out rayLength))
        {
            PointToLook = cameraRay.GetPoint(rayLength);
        }
        transform.LookAt(new Vector3(PointToLook.x, transform.position.y, PointToLook.z));
        if (_playerTransform != null) { 
            Vector3 oth = new Vector3(_playerTransform.position.x, 3.0f, _playerTransform.position.z);
            //destination = _playerTransform.position + (new Vector3(PointToLook.x, 0f, PointToLook.z) - _playerTransform.position).normalized;
            destination = oth + (new Vector3(PointToLook.x, 0f, PointToLook.z) - oth).normalized;
            transform.position = Vector3.Lerp(transform.position, destination, speed * Time.deltaTime);
        }
    }

    public void SetTargetTransform(GameObject player)
    {
        _playerTransform = player.GetComponent<Transform>();
    }
}