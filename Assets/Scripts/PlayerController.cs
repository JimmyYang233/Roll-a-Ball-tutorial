using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

	public float speed;
	public float jumpHeight;
	public Text countText;
	public Text winText;
	public float flyRangeMin;
	public float flyRangeMax;
	
	private Rigidbody rb;
	private float distToGround = 0.5f;
	private int count = 0;
	private bool win = false;

	void Start()
	{
		rb = this.GetComponent<Rigidbody>();
		setCountText();
		winText.text = "";
	}
	
	void Update()
	{
		
	}

	void FixedUpdate()
	{
		if (Input.GetKey(KeyCode.Space) && isGrounded())
		{
			Vector3 jumpVelocity = new Vector3(0, jumpHeight, 0);
			rb.velocity = rb.velocity + jumpVelocity;
		}

		float moveHorizontal = Input.GetAxis("Horizontal");
		float moveVertical = Input.GetAxis("Vertical");
		Vector3 movement = new Vector3(moveHorizontal,0 , moveVertical);
		
		rb.AddForce(movement* speed);
	}

	void OnTriggerEnter(Collider other)
	{
		Debug.Log("True");
		if (other.gameObject.CompareTag("PickUp"))
		{
			Debug.Log("true");
			
			other.gameObject.SetActive(false);
			count = count + 1;
			setCountText();
		}
		else if (other.gameObject.CompareTag("Finish"))
		{
			if (win)
			{
				winText.text = "You Win!!!";
				rb.AddForce(new Vector3(Random.Range(flyRangeMin,flyRangeMax), 5000, Random.Range(flyRangeMin,flyRangeMax)));
			}
			
		}
	}

	bool isGrounded()
	{
		return Physics.Raycast(transform.position, Vector3.down, distToGround);
	}

	private void setCountText()
	{
		countText.text = "Count: " + count.ToString();
		if (count >= 7)
		{
			win = true;
		}
	}
}
