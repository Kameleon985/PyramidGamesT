using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ChestAnimation : MonoBehaviour
{
    private Animator animator;
    private ParticleSystem particles;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        particles = GetComponentInChildren<ParticleSystem>();
    }

    private void OnEnable()
    {
        EventHandler.OnDecline += Declined;
        EventHandler.OnChestOpen += PlayAnimation;
    }

    private void Declined()
    {
        particles.Stop();
        animator.SetBool("Open", false);
        animator.SetBool("Decline", true);
    }

    private void PlayAnimation()
    {
        particles.Play();
        animator.SetBool("Decline", false);
        animator.SetBool("HasKey", PlayerData.HasKey);
        animator.SetBool("HasOpened", PlayerData.ChestOpened);
        animator.SetBool("Open", true);
    }

    private void OnDisable()
    {
        EventHandler.OnDecline -= Declined;
        EventHandler.OnChestOpen -= PlayAnimation;
    }

}
