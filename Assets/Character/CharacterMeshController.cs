using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMeshController : MonoBehaviour {

	public Vector3 inputDirection;
	public float speed;
	public Transform characterTransform;
	public float blendFactor = 0.2f;
	public float CHAR_DEFAULT_SPEED = 2f;
	Vector3 blend_vec;

	Animator myAnimator;

	// Use this for initialization
	void Start () {
		inputDirection = Vector3.zero;
		speed = 0;
		characterTransform = transform;
		myAnimator = GetComponent<Animator>();
		Debug.Assert(myAnimator!=null);
		blend_vec = Vector3.zero;
	}
	
	// Update is called once per frame
	void Update () {
		blend_vec = Vector3.Lerp(blend_vec,characterTransform.InverseTransformDirection(inputDirection).normalized,blendFactor);
		GetComponent<Animator>().SetFloat("speed_x",blend_vec.x);
		GetComponent<Animator>().SetFloat("speed_y",blend_vec.z);
		if(speed > CHAR_DEFAULT_SPEED){
			myAnimator.SetFloat("speed_multiplier", speed/CHAR_DEFAULT_SPEED);
		}else{
			myAnimator.SetFloat("speed_multiplier",1f);
		}
	}

	public void Shoot(){
		myAnimator.SetTrigger("shoot");
	}
}
