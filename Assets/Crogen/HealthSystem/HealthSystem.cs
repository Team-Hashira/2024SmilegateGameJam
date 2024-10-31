using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Crogen.HealthSystem
{
    public abstract class HealthSystem : MonoBehaviour
    {
        [Header("Hp Option")]
        [SerializeField] private float _hp = 100.0f;
        public float maxHp = 100.0f;

        public event Action<float, float> OnHPChangeEvent;
        public event Action OnHPUpEvent;
        public event Action OnHPDownEvent;
        public event Action OnDieEvent;

        private void Awake()
        {
            _hp = maxHp;
        }

        public float Hp
        {
            get => _hp;
            set
            {
                OnHpChange();
                OnHPChangeEvent?.Invoke(_hp, value);
                if (gameObject.activeSelf == true)
                {
                    if(_hp < value)
                    {
                        OnHpUp();
                        OnHPUpEvent?.Invoke();
                    }
                    else if (_hp > value)
                    {
                        OnHpDown();
                        OnHPDownEvent?.Invoke();
                    }

                    _hp = value;
                    
                    if (_hp <= 0.1f)
                    {
                        OnDie();
                        OnDieEvent?.Invoke();
                    }                
                }
            }
        }

        protected abstract void OnHpChange();
        protected abstract void OnHpUp();
        protected abstract void OnHpDown();
        protected abstract void OnDie();
    }    
}