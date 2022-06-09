using System.Collections;
using UnityEngine;

// This class is for the Boost system and its methods.

namespace CDR.MovementSystem
{
    public class Boost : ActionSystem.Action, IBoost
    {
        [Tooltip("Delay before resuming boost regen in seconds.")]
        [SerializeField]
        private float regenDelaySeconds;
        [SerializeField]
        private BoostValue _boostValue;
        [SerializeField]
        private VerticalBoostData _verticalBoostData;
        [SerializeField]
        private HorizontalBoostData _horizontalBoostData;

        public IBoostValue boostValue => _boostValue;
        public IBoostData horizontalBoostData => _horizontalBoostData;
        public IBoostData verticalBoostData => _verticalBoostData;

        private Controller controller;
        private Coroutine _FixedCoroutine;

        private void Start()
        {
            controller = (Controller)Character.controller;
            _boostValue.ModifyValueWithoutEvent(_boostValue.MaxValue);
            StartCoroutine(_boostValue.Regenerate());
        }

        private IEnumerator FixedCoroutine(Vector3 direction, float time, bool isHorizontal)
        {
            float currentTime = time;
            while(currentTime > 0)
            {
                RotateObject();

                controller.AddRbForce(transform.rotation * direction);

                if(isHorizontal)
                    controller.AddRbForce(CentripetalForce(), ForceMode.Acceleration);

                currentTime -= Time.fixedDeltaTime;

                yield return new WaitForFixedUpdate();
            }
            End();
        }

        Vector3 CentripetalForce()
        {
            TargetingSystem.ITargetData currentTarget = Character.targetHandler.GetCurrentTarget();
            float cForce = Mathf.Pow(controller.velocity.magnitude, 2f) / currentTarget.distance;
            return currentTarget.direction * (-cForce * 1.195f);
        }

        private void RotateObject()
        {
            TargetingSystem.ITargetData currentTarget = Character.targetHandler.GetCurrentTarget();
            var look = Quaternion.LookRotation(-currentTarget.direction);
            var quat = Quaternion.RotateTowards(transform.rotation, look, 50f);
            quat.x = 0f;
            quat.z = 0f;

            controller.Rotate(Quaternion.RotateTowards(transform.rotation, quat, 50f));
        }
     
        public void HorizontalBoost(Vector2 direction)
        {
            if(_boostValue.CanUse() && !isActive)
            {
                var dir = new Vector3(direction.x, 0f, direction.y);

                Use();
                _FixedCoroutine = StartCoroutine(FixedCoroutine(new Vector3(direction.x, 0f, direction.y)
                    * horizontalBoostData.distance / horizontalBoostData.time,
                    horizontalBoostData.time, true));
            }
        }
    
        public void VerticalBoost(float direction)
        {
            if (_boostValue.CanUse() && !isActive)
            {
                Use();
                StartCoroutine(FixedCoroutine(new Vector3(0f, direction, 0f)
                    * verticalBoostData.distance / verticalBoostData.time,
                    verticalBoostData.time, false));              
            }
        }

        private IEnumerator ResumeRegen()
        {
            yield return new WaitForSeconds(regenDelaySeconds);
            _boostValue.SetIsRegening(true);
        }

        public override void Use()
        {
            base.Use();
            _boostValue.Consume();
            _boostValue.SetIsRegening(false);
            Character.movement.SetSpeedClamp(false);

            Character.movement.End();
            //Character.input.DisableInput("Movement");
        }

        public override void End()
        {
            base.End();
            if(_FixedCoroutine != null)
            {
                StopCoroutine(_FixedCoroutine);
            }

            
            StartCoroutine(ResumeRegen());
            Character.movement.Use();
            Character.movement.Move(Vector2.zero);
            Character.movement.SetSpeedClamp(true);
            //Character.input.EnableInput("Movement");

            Character.movement.SetDistanceToTarget
                (
                    Vector3.Distance(Character.targetHandler.GetCurrentTarget().activeCharacter.position,
                    transform.position)
                );
        }
    }
}