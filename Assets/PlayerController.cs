using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {

	Rigidbody2D rigid2D;
	Animator animator;
	float jumpForce = 680.0f;
	float walkForce = 30.0f;
	float maxWalkSpeed = 2.0f;

	// Use this for initialization
	void Start () {
		this.rigid2D = GetComponent<Rigidbody2D> ();
		this.animator = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		// ジャンプする
		if (Input.GetKeyDown (KeyCode.Space) && this.rigid2D.velocity.y == 0) {
			this.animator.SetTrigger ("JumpTrigger");
			this.rigid2D.AddForce (transform.up * this.jumpForce);
		}

		int key = 0;
		if (Input.GetKey (KeyCode.RightArrow))
			key = 1;
		if (Input.GetKey (KeyCode.LeftArrow))
			key = -1;

		// プレイヤーの速度
		float speedx = Mathf.Abs (this.rigid2D.velocity.x);

		// スピード調整
		if (speedx < this.maxWalkSpeed) {
			this.rigid2D.AddForce (transform.right * key * this.walkForce);
		}

		// 動く方法に応じて反転
		if (key != 0) {
			transform.localScale = new Vector3 (key, 1, 1);
		}

		// プレイヤーの速度い応じてアニメーション速度を変える
		if (this.rigid2D.velocity.y == 0) {
			this.animator.speed = speedx / 2.0f;
		} else {
			this.animator.speed = 1.0f;
		}

		// 画面の外に出た場合は最初から
		if (transform.position.y < -10) {
			SceneManager.LoadScene ("GameScene");
		}

	}

	// ゴールに到達
	void OnTriggerEnter2D(Collider2D other) {
		Debug.Log ("ゴール");
		SceneManager.LoadScene ("ClearScene");
	}
}
