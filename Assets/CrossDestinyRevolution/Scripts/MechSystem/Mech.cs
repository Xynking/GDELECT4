using System.Collections;
using System.Collections.Generic;
using CDR.AttackSystem;
using CDR.MovementSystem;
using UnityEngine;

namespace CDR.MechSystem
{
    public class Mech : ActiveCharacter, IMech
    {
        [SerializeField] Boost _boost;
        [SerializeField] MeleeAttack _meleeAttack;
        [SerializeField] RangeAttack _rangeAttack;
        [SerializeField] IShield _shield;
        [SerializeField] SpecialAttack _specialAttack1;
        [SerializeField] SpecialAttack _specialAttack2;
        [SerializeField] SpecialAttack _specialAttack3;

        public IBoost boost => _boost;

        public IMeleeAttack meleeAttack => _meleeAttack;

        public IRangeAttack rangeAttack => _rangeAttack;

        public IShield shield => _shield;

        public ISpecialAttack specialAttack1 => _specialAttack1;

        public ISpecialAttack specialAttack2 => _specialAttack2;

        public ISpecialAttack specialAttack3 => _specialAttack3;

        protected override void Awake()
        {
            base.Awake();

            _shield = GetComponent<IShield>();
        }
    }
}

