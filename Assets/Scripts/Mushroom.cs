﻿using UnityEngine;

public class Mushroom : Entity, IAttackable
{
	public Mushroom() : base("PiPi", 300, -1, null, OnDie) { }
	public int AttackDamage => 40;
	public float jumpSpan = 0, jumpWait = 0;
	public bool hasTarget = false;
	public GameObject dieEffect, potion, paralysisEffect;

	Animator animator;
	Rigidbody2D rigidbody2d;
	Vector3 transformOrg;

	void Start()
	{
		animator = GetComponent<Animator>();
		rigidbody2d = GetComponent<Rigidbody2D>();
		transformOrg = transform.localScale;
	}

	void Update()
	{
		switch (Game.gameState)
		{
			case Game.GameState.Playing:
				rigidbody2d.bodyType = RigidbodyType2D.Dynamic;
				break;
			case Game.GameState.Pause:
				rigidbody2d.bodyType = RigidbodyType2D.Static;
				break;
		}
	}

	void OnCollisionStay2D(Collision2D collision)
	{
		switch (collision.collider.tag)
		{
			case "Ground":
				if (Game.isPlaying)
				{
					if (jumpSpan >= 0) jumpSpan += Time.deltaTime;
					if (jumpSpan >= jumpWait)
					{
						jumpSpan = -1;
						animator.Play("Jump");
						entityDirection = hasTarget ? Slime.instance.transform.position.x > transform.position.x ? 1 : -1 : Mathf.RoundToInt(Random.value) * -2 + 1;
						transform.localScale = Vector3.Scale(transformOrg, new Vector3(entityDirection, 1, 1));
						rigidbody2d.AddForce(new Vector3(entityDirection * 80, 250, 0) * (hasTarget ? 2f : 1f));
					}
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
			case "Slime":
				if (Game.isPlaying)
				{
					Instantiate(paralysisEffect, transform.position, Quaternion.identity);
					Game.lastattack = 1;
					Entity target = collision.collider.GetComponentInParent<Entity>();
					target.Suffer(AttackDamage);
					target.ApplyEffect(new EntityEffect(EntityEffect.EntityEffectType.Paralyze, 1));
					Destroy(gameObject);
				}
				break;
		}
	}

	static void OnDie(Entity entity)
	{
		entity.GetComponent<Animator>().Play("die");
		entity.entityDirection = 0;
	}

	void DieAnimationEnd()
	{
		Game.moneycount += 30;
		Game.expcount += 3;
		Instantiate(dieEffect, transform.position, Quaternion.identity);
		if (Random.value <= 0.5) Instantiate(potion, transform.position, Quaternion.identity);
		Destroy(gameObject);
	}
}
