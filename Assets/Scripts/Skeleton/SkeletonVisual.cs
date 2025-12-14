using System;
using UnityEngine;

[RequireComponent (typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
public class SkeletonVisual : MonoBehaviour {

    [SerializeField] private EnemyAI enemyAI;
    [SerializeField] private EnemyEntity enemyEntity;
    [SerializeField] private GameObject enemyShadow;
    
    private static readonly int IsRunning = Animator.StringToHash("IsRunning");
    private static readonly int Attack = Animator.StringToHash("Attack");
    private static readonly int ChasingSpeedMultiplier = Animator.StringToHash("ChasingSpeedMultiplier");
    private static readonly int TakeHit = Animator.StringToHash("TakeHit");
    private static readonly int IsDie = Animator.StringToHash("IsDie");

    private Animator _animator;
    private SpriteRenderer _spriteRenderer;

    private void Awake() {
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        enemyAI.OnEnemyAttack += _enemyAI_OnEnemyAttack;
        enemyEntity.OnTakeHit += _enemyEntity_OnTakeHit;
        enemyEntity.OnDeath += _enemyEntity_OnDeath;
    }

    private void Update() {
        _animator.SetBool(IsRunning, enemyAI.IsRunning);
        _animator.SetFloat(ChasingSpeedMultiplier, enemyAI.GetRoamingAnimationSpeed());
    }

    private void OnDestroy()
    {
        enemyAI.OnEnemyAttack -= _enemyAI_OnEnemyAttack;
        enemyEntity.OnTakeHit -= _enemyEntity_OnTakeHit;
        enemyEntity.OnDeath -= _enemyEntity_OnDeath;
    }

    public void TriggerAttackAnimationTurnOff()
    {
        enemyEntity.PolygonCollide2DTurnOff();
    }

    public void TriggerAttackAnimationTurnOn()
    {
        enemyEntity.PolygonColliderTurnOn();
    }

    private void _enemyAI_OnEnemyAttack(object sender, EventArgs e)
    {
        _animator.SetTrigger(Attack);
    }

    private void _enemyEntity_OnTakeHit(object sender, EventArgs e)
    {
        _animator.SetTrigger(TakeHit);
    }

    private void _enemyEntity_OnDeath(object sender, EventArgs e)
    {
        _animator.SetBool(IsDie, true);
        _spriteRenderer.sortingOrder = -1;
        enemyShadow.SetActive(false);
    }
}