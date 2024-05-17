using System.Collections.Generic;
using UnityEngine;

namespace Kosmos6
{
    public class ShipMovement : MonoBehaviour
    {
        [Header("Movement Multipliers")]

        [SerializeField] private float _pitchSpeed = 125000f;
        [SerializeField] private float _yawSpeed = 125000f;
        [SerializeField] private float _rollSpeed = 125000f;
        [SerializeField] private float _moveSpeed = 125000f;
        [SerializeField] private float _accelerationSpeed = 225000f;
        private bool _acceleration = false;

        [Header("Drag Multipliers")]
        [Range(0.5f, 50f)]
        [SerializeField] private float _proportionalAngularDrag = 5f;
        [Range(10f, 1000f)]
        [SerializeField] private float _proportionalDrag = 100f;

        [Header("Links")]
        public SpaceShip SpaceShip;
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private List<Engine> Engines = new();

        private void FixedUpdate()
        {
            Turn(SpaceShip.InputShipMovement.CurrentInputRotatePitch,
                SpaceShip.InputShipMovement.CurrentInputRotateYaw,
                SpaceShip.InputShipMovement.CurrentInputRotateRoll);

            if (SpaceShip.InputShipMovement.CurrentInputAcceleration)
            {
                _acceleration = true;
                Move(SpaceShip.InputShipMovement.CurrentInputMoveY, SpaceShip.InputShipMovement.CurrentInputMoveX);
            }
            else
            {
                _acceleration = false;
                Move(SpaceShip.InputShipMovement.CurrentInputMoveY, SpaceShip.InputShipMovement.CurrentInputMoveX);
            }
        }

        private void Turn(float inputPitch, float inputYaw, float inputRoll)
        {
            if (!Mathf.Approximately(0f, inputPitch))
                _rigidbody.AddTorque(_pitchSpeed * inputPitch * Time.fixedDeltaTime * transform.right);
            if (!Mathf.Approximately(0f, inputYaw))
                _rigidbody.AddTorque(_yawSpeed * inputYaw * Time.fixedDeltaTime * transform.up);
            if (!Mathf.Approximately(0f, inputRoll))
                _rigidbody.AddTorque(_rollSpeed * inputRoll * Time.fixedDeltaTime * transform.forward);

            _rigidbody.AddForce(-_rigidbody.angularVelocity * _proportionalAngularDrag * Time.fixedDeltaTime);
        }

        private void Move(float inputMoveY, float inputMoveX)
        {
            Vector3 resultingThrust = new();
            foreach (var engine in Engines)
                resultingThrust += resultingThrust + engine.Thrust(inputMoveY, inputMoveX);

            if (_acceleration)
                _rigidbody.AddForce(_accelerationSpeed * Time.fixedDeltaTime * resultingThrust);
            else
                _rigidbody.AddForce(_moveSpeed * Time.fixedDeltaTime * resultingThrust);


            _rigidbody.AddForce(_proportionalDrag * Time.fixedDeltaTime * -_rigidbody.velocity);
        }
    }
}