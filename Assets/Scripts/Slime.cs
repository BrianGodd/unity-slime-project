﻿#pragma warning disable CS0108
using UnityEngine;

/**
 * Pickup potion: line 142
 * Consume potion: line 83
 */

public class Slime : MonoBehaviour//Entity
{
	public int direction;
	public static Slime instance;
	public static Animator animator;
	public static Rigidbody2D rigidbody2d;
	public static SpriteRenderer spriteRender;
	public static Transform transform;


	public GameObject Bomb;
	float moveSpeed = 1600f, jumpStrength = 2e4f, dropStrength = 100f;
	public static float suppression = 1;

	public static bool isTouchingGround = false, bouncable = false, allowMove = false;

	public static int potionCount = 0, potionMax = 100, keyCount = 0;

	public GameObject keyCountObject, potionCountObject, paralysis, heal;

	public Behaviour flareLayer;

	/*public Slime() : base(6, 1, ImmuneOn, DeathHandler) {
        instance = this;
    }*/

	void Start()
	{
		keyCountObject = GameObject.Find("Key Count");
		potionCountObject = GameObject.Find("Potion Count");
		animator = GetComponent<Animator>();
		rigidbody2d = GetComponent<Rigidbody2D>();
		spriteRender = GetComponent<SpriteRenderer>();
		transform = GetComponent<Transform>();
		flareLayer = (Behaviour)Camera.main.GetComponent ("FlareLayer");
	}

	void Update()
	{
		switch (GameGlobalController.gameState)
		{
			case GameGlobalController.GameState.Loading:
				flareLayer.enabled = false;
				spriteRender.sortingLayerName = "Black Screen";
				spriteRender.sortingOrder = 3;
				break;
			case GameGlobalController.GameState.Brightening:
				flareLayer.enabled = true;
				spriteRender.sortingLayerName = "Main Objects";
				spriteRender.sortingOrder = 8;
				break;
			case GameGlobalController.GameState.Playing:
			case GameGlobalController.GameState.Lobby:
				moveSpeed = 160 * suppression;
				jumpStrength = 2e4f * suppression;
				dropStrength = 100 * suppression;
				if (LifeHandler.targetlife <= 0 && !LifeHandler.start) DeathHandler();
				// Control immuable
				//if (immuableTime <= 0) spriteRender.color = new Color(255, 255, 255, 90);
				// Control camera postion, except for the time in the welcome screen
				if (!(GameGlobalController.currentLevel == 0 && GameGlobalController.isLobby))
					MainCameraHandler.targetPosition = new Vector3(transform.position.x, transform.position.y, -10);
				// Set Slime to follow physics engine
				rigidbody2d.bodyType = RigidbodyType2D.Dynamic;
				// Response to keyboard input
				if (bouncable && (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) && (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W)))
				{
					MainCameraHandler.allSound = 2;
					rigidbody2d.AddForce(new Vector2(150 * moveSpeed, jumpStrength));
					animator.Play("Jump Right");
					direction = 1;
					allowMove = bouncable = false;
				}
				if (bouncable && (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) && (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W)))
				{
					MainCameraHandler.allSound = 2;
					rigidbody2d.AddForce(new Vector2(150 * -moveSpeed, jumpStrength));
					animator.Play("Jump Left");
					direction = -1;
					allowMove = bouncable = false;
				}
				if (allowMove)
				{
					animator.SetBool("right", Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow));
					animator.SetBool("left", Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow));
					animator.SetBool("jump", Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.W));
					animator.SetBool("crouch", Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.S));
				}
				if ((Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) && allowMove)
				{
					rigidbody2d.AddForce(new Vector2(-moveSpeed * (isTouchingGround ? 1f : 0.5f), 0));
					direction = -1;
				}
				if ((Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) && allowMove)
				{
					rigidbody2d.AddForce(new Vector2(moveSpeed * (isTouchingGround ? 1f : 0.5f), 0));
					direction = 1;
				}
				if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.S))
					rigidbody2d.AddForce(new Vector2(0, -dropStrength));
				if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W)) && isTouchingGround)
				{
					MainCameraHandler.allSound = 2;
					rigidbody2d.AddForce(new Vector2(0, jumpStrength));
				}
				if (Input.GetKeyDown(KeyCode.F) && GameGlobalController.gameState == GameGlobalController.GameState.Playing)
				{
					if(EnergyHandler.nextenergy < 25 || EnergyHandler.targetenergy < 25)
						MainCameraHandler.allSound = 12;
					else
					{	
						MainCameraHandler.allSound = 4;
						Vector3 pos = transform.position + new Vector3(direction * 5, 0, 0);
						Instantiate(Bomb, pos, transform.rotation).GetComponent<Bullet>().moveSpeed *= direction;
						EnergyHandler.changeamount(-25);
					}
				}
				if (Input.GetKeyDown(KeyCode.Q))
				{
					if (potionCount > 0)
					{
						Instantiate(heal).GetComponent<Transform>().position = new Vector3(transform.position.x, transform.position.y - 2.5f, transform.position.z);
						LifeHandler.Heal(30);
						potionCountObject.GetComponent<CountLabel>().updateCount(--potionCount);
					}
				}
				if (Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
					allowMove = true;
				break;
			case GameGlobalController.GameState.Pause:
				rigidbody2d.bodyType = RigidbodyType2D.Static;
				break;
		}
	}
	public static void disappear()
	{
		animator.Play("Disappear");
	}
	private void OnCollisionEnter2D(Collision2D collision)
	{
		switch (collision.collider.tag)
		{
			case "Mushroom":
				if (GameGlobalController.gameState == GameGlobalController.GameState.Playing)
				{
					Instantiate(paralysis).GetComponent<Transform>().position = collision.transform.position;
					LifeHandler.Suffer(collision.collider.GetComponent<Attackable>().AttackDamage);
					GameObject.Find("CharacterLife").GetComponent<Animator>().Play("paralysis");
					Destroy(collision.gameObject);
				}
				break;
			case "Ground":
				bouncable = false;
				break;
			case "Walls":
				if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow))
					bouncable = true;
				break;
		}
	}

	void OnTriggerEnter2D(Collider2D collision)
	{
		switch (collision.tag)
		{
			case "EventTrigger":
				Animation.handler.trigger(collision.GetComponent<TriggerHandler>().triggerId);
				Destroy(collision.gameObject);
				break;
			case "Potion":
				MainCameraHandler.allSound = 9;
				Destroy(collision.gameObject);
				potionCountObject.GetComponent<CountLabel>().updateCount(++potionCount);
				break;
			case "Key":
				MainCameraHandler.allSound = 5;
				Destroy(collision.gameObject);
				keyCountObject.GetComponent<CountLabel>().updateCount(++keyCount);
				break;
		}
	}

	public static void Healanim()
	{
		animator.Play("Heal");
	}

	public static void ImmuneOn()//static void ImmuneOn(Entity entity)
	{
		//SlimeLifeCanvas.Shake();
		//SlimeLifeCanvas.life = entity.health;
		animator.Play("Suffer");
	}

	static void DeathHandler()//static void DeathHandler(Entity entity)
	{
		transform.position = new Vector3(-5, -5, -10);
		GameGlobalController.BadEnd();
	}

	void start2()
	{
		animator.Play("Disappear");
		DarkAnimatorController.animator.speed = 1;
	}

	void jump()
	{
		MainCameraHandler.allSound = 2;
		rigidbody2d.AddForce(new Vector2(12000f, 1.5e4f));
		animator.Play("Jump Right");
		direction = 1;
	}

	void littlejump()
	{
		rigidbody2d.AddForce(new Vector2(4000f, 0.5e4f));
		animator.Play("Jump Right");
		direction = 1;
	}

	void moveback()
	{
		rigidbody2d.AddForce(new Vector2(-6000f, 0));
		animator.Play("Right");
		direction = 1;
	}

	void _null(){}

	public static void normal()
	{
		animator.SetBool("right", false);
		animator.SetBool("left", false);
		animator.SetBool("jump", false);
		animator.SetBool("crouch", false);
	}

	void storyloadend()
	{
		if(GameGlobalController.storystate == 2)
		{
			animator.Play("Disappear");
			GameGlobalController.storystate = 3;
			DarkAnimatorController.animator.SetFloat("speed", 1);
		}
	}
}
