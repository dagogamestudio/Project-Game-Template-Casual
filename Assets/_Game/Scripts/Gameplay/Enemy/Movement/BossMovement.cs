using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMovement : MonoBehaviour, IMovement
{
    [Header("Side Movement")]
    public float sideSpeed = 2f;
    public float sideRange = 2f;

    [Header("Charge")]
    public float chargeSpeed = 7f;
    public float chargeDuration = 0.8f;

    [Header("Timing")]
    public float moveSideDuration = 2f;
    public float idleDuration = 1.5f;

    private BossState currentState;
    private float timer;

    private Vector3 startPosition;
    private int sideDirection = 1;

    private EnemyController controller;
    private EnemyShooter shooter;

    private void Awake()
    {
        shooter = GetComponent<EnemyShooter>(); // script nembak boss
    }

    private void OnEnable()
    {
        startPosition = transform.position;
        ChangeState(BossState.MoveSide);
    }
    public void Init(EnemyController controller)
    {
        this.controller = controller;
    }
    private void Update()
    {
        if (!GameManager.Instance.isPlaying) return;

        timer += Time.deltaTime;

        switch (currentState)
        {
            case BossState.MoveSide:
                MoveSide();
                if (timer >= moveSideDuration)
                    ChangeState(BossState.Charge);
                break;

            case BossState.Charge:
                ChargeForward();
                if (timer >= chargeDuration)
                    ChangeState(BossState.Shoot);
                break;

            case BossState.Shoot:
                Shoot();
                ChangeState(BossState.Idle);
                break;

            case BossState.Idle:
                if (timer >= idleDuration)
                    ChangeState(BossState.MoveSide);
                break;
        }
    }

    // ===================== STATES =====================

    private void MoveSide()
    {
        float x = sideDirection * sideSpeed * Time.deltaTime;
        transform.Translate(new Vector3(x, 0, 0), Space.World);

        if (Mathf.Abs(transform.position.x - startPosition.x) >= sideRange)
        {
            sideDirection *= -1;
        }
    }

    private void ChargeForward()
    {
        transform.Translate(Vector3.back * chargeSpeed * Time.deltaTime, Space.World);
    }

    private void Shoot()
    {
        if (shooter != null)
            shooter.Fire(); // panggil fungsi nembak
    }

    // ===================== UTIL =====================

    private void ChangeState(BossState newState)
    {
        currentState = newState;
        timer = 0f;
    }
}
public enum BossState
{
    MoveSide,
    Charge,
    Shoot,
    Idle
}