﻿using UnityEngine;

public class Bird : Entity, IAttackable
{
	public Bird() : base("Bird", 150, 1, OnSuffer, OnDie) { }
	public int AttackDamage => 15;
	public bool isWall = false;

	readonly float moveSpeed = 0.05f; //bird movement speed
	float offset = 0;
	public GameObject effect;

	//GameObject progressBar;

	private void Start()
	{
		GetComponent<Animator>().Play("colliderchange");
	}

	private void Update()
	{
		transform.localScale = new Vector2(entityDirection * 0.5f, 0.5f);
		switch (Game.gameState)
		{
			case Game.GameState.Lobby:
				Destroy(gameObject);
				break;
			case Game.GameState.Playing:
				
				float newY;
				do newY = Random.Range(-0.03f, 0.03f); while (Mathf.Abs(newY + offset) >= 0.6);
				offset += newY;
				transform.Translate(new Vector2(moveSpeed * entityDirection, newY));
				break;
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		switch (collision.tag)
		{
			case "Slime":
				if (Game.gameState == Game.GameState.Playing)
				{
					collision.GetComponentInParent<Entity>().Suffer(AttackDamage);
					Game.lastattack = 0;
				}
				break;
			case "Walls":
				if (isWall) entityDirection *= -1;
				break;
		}
	}

	static void OnSuffer(Entity entity, float amount)
	{
		entity.GetComponent<Animator>().Play("suffer");
		//lifebarprefab.changeamount(amount);
	}

	static void OnDie(Entity entity)
	{
		entity.GetComponent<Animator>().Play("die");
		entity.entityDirection = 0;
	}

	void DieAnimationEnd()
	{
		Game.moneycount += 20;
		Game.expcount += 2.5f;
		Destroy(gameObject);
		GetComponentInParent<EnemySpawnerHandler>().isActive = true;
	}

	void DieEffectStart()
	{
		Instantiate(effect, transform.position, Quaternion.identity);
	}

	void WallK()
	{
		isWall = true;
	}
}
