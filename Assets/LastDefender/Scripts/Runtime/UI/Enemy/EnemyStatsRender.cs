using Assets.LastDefender.Scripts.Core.Models;
using Assets.LastDefender.Scripts.Core.Signals;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class EnemyStatsRender : MonoBehaviour
{
    [SerializeField]
    private Slider _hpSlider;

    public void Init(int maxHealth)
    {
        _hpSlider.maxValue = maxHealth;
        ChangedStats(maxHealth);
    }

    public void ChangedStats(int currentHealth)
    { 
        _hpSlider.value = currentHealth;
    }

}
