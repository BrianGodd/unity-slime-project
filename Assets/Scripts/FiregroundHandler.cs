using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiregroundHandler : MonoBehaviour
{
	public static Animator animator;

	void Start()
	{
		animator = GetComponent<Animator>();
		if (Game.storystate == 7) animator.SetBool("strong", true);
		else animator.SetBool("strong", false);
	}
}
