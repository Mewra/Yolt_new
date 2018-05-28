using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestController : MonoBehaviour {

	public float speed;
	Vector3 _speed;
	public Camera camera;

   
	// Use this for initialization
	void Start () {
		_speed = Vector3.zero;
	}
	
	// Update is called once per frame
	void Update () {
        
        if (Input.GetKey(KeyCode.W))
        {
            _speed = (Vector3.forward * speed * Time.deltaTime);
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.A))
        {
            _speed = (Vector3.left * speed * Time.deltaTime);
            transform.Translate(Vector3.left * speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.S))
        {
            _speed = (Vector3.back * speed * Time.deltaTime);
            transform.Translate(Vector3.back * speed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.D))
        {
            _speed = (Vector3.right * speed * Time.deltaTime);
            transform.Translate(Vector3.right * speed * Time.deltaTime);
        }
        
      
		transform.position += _speed*Time.deltaTime * speed;
		Vector3 p = new Vector3();
        Camera  c = camera;
        Event   e = Event.current;
        Vector2 mousePos = new Vector2();

        // Get the mouse position from Event.
        // Note that the y position from Event is inverted.
        //mousePos.x = e.mousePosition.x;
        //mousePos.y = c.pixelHeight - e.mousePosition.y;
		mousePos = Input.mousePosition;

        p = c.ScreenToWorldPoint(new Vector2(mousePos.x, mousePos.y));
		//Debug.Log("hit in "+ mousePos);
		RaycastHit hit;
		if(Physics.Raycast(c.transform.position, p-c.transform.position, out hit, Mathf.Infinity)){
			
			transform.forward = (hit.point - transform.position).normalized;
		}
		GetComponent<CharacterMeshController>().characterTransform = transform;
		GetComponent<CharacterMeshController>().inputDirection = _speed;
		GetComponent<CharacterMeshController>().speed = speed;
		if(Input.GetKeyDown("space")){
			GetComponent<CharacterMeshController>().Shoot();
		}
	}
}
