using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CDR.ActionSystem;

namespace CDR.AttackSystem
{
	public class RangeAttack : Action
	{
		float FireRate;
		GameObject GunPoint; //Invisible gameobject for the location of the gun barrel
		Transform TargetPoint;

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

