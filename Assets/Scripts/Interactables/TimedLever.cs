using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class TimedLever : Subject, IInteractable {
    [SerializeField] private float timer = 10f;
    private float currentTimer = 10f;

    public bool canReset = true;

    private Animator animator;

    private void Awake() {
        animator = GetComponent<Animator>();
        currentTimer = timer;
    }

    public void OnInteract(PlayerInteractions interactee) {
        currentTimer = timer;

        NotifyListener();

        animator.SetFloat("timerSpeed", 1/timer);
        if (animator.GetBool("isActive")) {
            animator.Play("Lever", -1, 0f);
        } else {
            animator.SetBool("isActive", true);
        }
    }

    private void Update() {
        if (currentTimer > 0) currentTimer -= Time.deltaTime;

        if (currentTimer <= 0) {
            Reset();

            animator.SetBool("isActive", false);
        }
    }
}
