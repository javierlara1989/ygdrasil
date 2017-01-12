using UnityEngine;
using System.Collections;

public class marianaController : MonoBehaviour {

	public float maxSpeed = 10f;
	public bool facingRight = true;
	Rigidbody rBody;
	Animator anim;
	float vertical_move;
	float horizontal_move;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
		rBody = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
		horizontal_move = Input.GetAxis("Horizontal");
		vertical_move = rBody.velocity.y;
		rBody.velocity = new Vector3(horizontal_move * maxSpeed, rBody.velocity.y);
		rBody.constraints = RigidbodyConstraints.FreezeRotation;

		anim.SetFloat("speed", Mathf.Abs(horizontal_move));
		anim.SetFloat("v_speed", Mathf.Abs(vertical_move));

		if(horizontal_move > 0 && !facingRight)
			Flip();
		if(horizontal_move < 0 && facingRight)
			Flip();

		if (Input.GetKeyDown ("space"))
			rBody.AddForce(transform.up * 5, ForceMode.Impulse);
		if (Input.GetKeyDown ("z")) {
			if (anim.GetCurrentAnimatorStateInfo(0).IsName("idle")) {
				anim.SetTrigger("punch");
			} else if (anim.GetCurrentAnimatorStateInfo(0).IsName("punch")) {
				anim.SetTrigger("punch_2");
			}
		}
	}

	void Flip() {
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.z *= -1;
		transform.localScale = theScale;
	}
}
