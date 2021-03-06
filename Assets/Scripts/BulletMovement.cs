﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{

	public float yVel;
	public Rigidbody2D rb;
	public GameObject explosion;
	public bool shouldDelete = true;

	private bool velSet;

	// Use this for initialization
	public virtual void Start ()
	{
		if (!velSet) {
			rb.velocity = new Vector3 (0, yVel, 0);
		}
		string level = GameObject.Find ("LevelTracker").GetComponent<LevelTracker> ().getLevel ();
		switch (level) {
		case "Jeroo":
		case "Tron":
		case "The Net":
			GetComponent<SpriteRenderer> ().sprite = Resources.Load<Sprite> ("Art/" + level + "/bullet");
			break;
		default:
			break;
		}
		GetComponent<BoxCollider2D> ().size = GetComponent<SpriteRenderer> ().sprite.bounds.size;
	}

	// Update is called once per frame
	public virtual void Update ()
	{
		if (transform.position.y > 6) {
			Destroy (this.gameObject);
		}
	}

	public virtual void OnDestroy ()
	{
		if (shouldDelete) {
			GameObject.Find ("Player").GetComponent<PlayerControl> ().deleteBullet ();
		}
	}

	public virtual void OnCollisionEnter2D (Collision2D coll)
	{
		if (coll.gameObject.tag == "Bullet") {
			Physics2D.IgnoreCollision (GetComponent<BoxCollider2D> (), coll.collider);
		}
		if (coll.gameObject.name == "Alien 1(Clone)") {
			Destroy (this.gameObject);
			AlienMovement alienArray = GameObject.Find ("AlienParent").GetComponent<AlienMovement> ();
			AlienScript deadAlien = coll.gameObject.GetComponent<AlienScript> ();
			if (GameObject.Find ("LevelTracker").GetComponent<LevelTracker> ().getLevel () == "War Games") {
				alienArray.removeAlien (deadAlien.getRow (), deadAlien.getCol (), "war games ");
			} else {
				alienArray.removeAlien (deadAlien.getRow (), deadAlien.getCol ());
			}
		}
	}

	public void setVelocity (Vector3 vel)
	{
		rb.velocity = vel;
		velSet = true;
	}

	public float getVelocity ()
	{
		return yVel;
	}

}
