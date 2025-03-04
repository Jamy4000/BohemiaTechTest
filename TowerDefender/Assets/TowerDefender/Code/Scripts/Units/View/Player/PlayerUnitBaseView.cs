using UnityEngine;
using UnityEngine.UI;

namespace TowerDefender.Units
{
    /// <summary>
    /// This is a very quick and dirty way to hack my way through a health bar system
    /// I don't like it, but gotta finish the test
    /// </summary>
    public abstract class PlayerUnitBaseView : UnitBaseView
    {
        [SerializeField] private Image _healthBar;

        private void Start()
        {
            _healthBar.fillAmount = 1;
        }

        public override void OnHealthChanged(float newHealth, float maxHealth)
        {
            _healthBar.fillAmount = newHealth / maxHealth;
        }
    }
}