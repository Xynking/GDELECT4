using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CDR.ActionSystem;

namespace CDR.AttackSystem
{
    public class MeleeAttack : Action
    {

		public override void Use()
		{
			base.Use();

			End();
		}

		public override void End()
		{
			base.End();
		}
	}
}

