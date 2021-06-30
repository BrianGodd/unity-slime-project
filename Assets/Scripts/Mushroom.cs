﻿#pragma warning disable CS0108 // 無法辨認的 #pragma 指示詞
using UnityEngine;

public class Mushroom : Entity, Attackable
{
	public Mushroom() : base("PiPi", 200, -1, null, OnDie) { }
	public int AttackDamage => 40;
	public float jumpSpan = 0, jumpWait = 0;
	public bool hasTarget = false;
	private Animator animator;
	public GameObject effect;

	private void Start()
	{
		animator = GetComponent<Animator>();
	}

	void OnCollisionStay2D(Collision2D collision)
	{
		switch (collision.collider.tag)
		{
			case "Ground":
				jumpSpan += Time.deltaTime;
				if (jumpSpan >= jumpWait)
				{
					animator.Play("Jump");
					direction = hasTarget ? (Slime.transform.position.x - transform.position.x) > 0 ? 1 : -1 : Mathf.RoundToInt(Random.value) * -2 + 1;
					Vector3 current = transform.localScale;
					transform.localScale = new Vector3(-direction * System.Math.Abs(current.x), current.y, current.z);
					GetComponent<Rigidbody2D>().AddForce(new Vector3(direction * 80, 250, 0) * (hasTarget ? 1.5f: 1f)); //direction * 100, 300, 0
				}
				break;
		}
	}

	void OnCollisionEnter2D(Collision2D collision)
	{
		switch (collision.collider.tag)
		{
			case "Ground":
				jumpSpan = 0;
				jumpWait = Random.Range(0.5f, 1.5f);
				break;
		}
	}

	static void OnDie(Entity entity)
	{
		entity.GetComponent<Animator>().Play("die");
		entity.direction = 0;
	}

	void DieAnimationEnd()
	{
		Instantiate(effect).GetComponent<Transform>().position = this.transform.position;
		Destroy(gameObject);
	}

}
