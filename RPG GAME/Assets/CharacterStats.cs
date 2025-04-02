using UnityEngine;

public class CharacterStats : MonoBehaviour
{

    [Header("Major stats")]
    public Stat strenght; // 1 point increase damage by 1 and critical strike by 1%
    public Stat agility; // 1 point increase evasion by 1% and critical chance by 1%
    public Stat intelligence; // 1 point increase magic damage by 1 and magic resist by 3
    public Stat vitality; // 1 point increase health by 3 or 4 or 5 points.


    [Header("Defensive Stats")]
    public Stat maxHealth;
    public Stat armor;
    public Stat evasion;



    public Stat damage;

    [SerializeField] private int currentHealth;


    protected virtual void Start()
    {
        currentHealth = maxHealth.GetValue();
    }

    public virtual void DoDamage(CharacterStats _targetStats)
  {
    if (TargetCanAvoidAttack(_targetStats))
      return;

    int totalDamage = damage.GetValue() + strenght.GetValue();

    totalDamage = CheckTargetArmor(_targetStats, totalDamage);
    _targetStats.TakeDamage(totalDamage);
  }


  public virtual void TakeDamage(int _damage)
    {
        currentHealth -= _damage;

        Debug.Log(_damage);

        if (currentHealth < 0)
            Die();
    }

    protected virtual void Die()
    {
        //throw new NotImplementedException();
    }

  private int CheckTargetArmor(CharacterStats _targetStats, int totalDamage)
  {
    totalDamage -= _targetStats.armor.GetValue();
    totalDamage = Mathf.Clamp(totalDamage, 0, int.MaxValue);
    return totalDamage;
  }

   private bool TargetCanAvoidAttack(CharacterStats _targetStats)
  {
    int totalEvasion = _targetStats.evasion.GetValue() + _targetStats.agility.GetValue();

    if (Random.Range(0, 100) < totalEvasion)
    {
        return true;
    }

    return false;
  }

}
