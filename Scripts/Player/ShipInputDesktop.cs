using System;
using UnityEngine;
#if ENABLE_INPUT_SYSTEM
#endif

namespace Kosmos6
{
    public class ShipInputDesktop : MonoBehaviour, IInputShipMovement, IInputShipWeapons
    {
        [field: Header("Ship Movement Values")]
        [field: SerializeField] public float CurrentInputMoveY { get; private set; }
        [field: SerializeField] public float CurrentInputMoveX { get; private set; }

        [field: SerializeField] public bool CurrentInputAcceleration { get; private set; }

        [field: SerializeField] public float CurrentInputRotatePitch { get; private set; }
        [field: SerializeField] public float CurrentInputRotateYaw { get; private set; }
        [field: SerializeField] public float CurrentInputRotateRoll { get; private set; }

        [field: Header("Ship Attack Values")]
        [field: SerializeField] public bool CurrentInputAttack { get; private set; }
        public event Action OnAttackInput;
        //public Vector2 look;

        [Header("Movement Settings")]
        public bool analogMovement;

        [Header("Mouse Cursor Settings")]
        public bool cursorLocked = true;
        public bool cursorInputForLook = true;

#if ENABLE_INPUT_SYSTEM

        private void Update()
        {
            OnRotate();
            OnMove();
            OnAttack();
            OnAcceleration();
        }

        private void OnRotate()
        {
            float pitch = Input.GetAxis("Mouse Y");
            if (Mathf.Abs(pitch) < 0.075f)
                pitch = 0;

            float yaw = Input.GetAxis("Mouse X");
            if (Mathf.Abs(yaw) < 0.075f)
                yaw = 0;

            float roll = Input.GetAxis("Roll");
            if (Mathf.Abs(roll) < 0.075f)
                roll = 0;

            RotateInput(-pitch, yaw, roll);
        }

        private void OnMove()
        {
            MoveInput(Input.GetAxis("Vertical"), (Input.GetAxis("Horizontal")));
        }

        private void OnAttack() => AttackInput(Input.GetAxis("Fire1"));

        private void OnAcceleration() => AccelerationInput(Input.GetButton("Acceleration"));

        private void AccelerationInput(bool newMoveInput) => CurrentInputAcceleration = newMoveInput;

        private void RotateInput(float pitch, float yaw, float roll)
        {
            CurrentInputRotatePitch = pitch;
            CurrentInputRotateYaw = yaw;
            CurrentInputRotateRoll = roll;
        }

#endif

        private void MoveInput(float newMoveInputY, float newMoveInputX)
        {
            CurrentInputMoveY = newMoveInputY;
            CurrentInputMoveX = newMoveInputX;
        }

        private void AttackInput(float newAttackInput)
        {
            if (newAttackInput > 0)
            {
                CurrentInputAttack = true;
                OnAttackInput?.Invoke();
            }
        }

        private void OnApplicationFocus(bool hasFocus) => SetCursorState(cursorLocked);

        private void SetCursorState(bool newState) => Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
    }
}