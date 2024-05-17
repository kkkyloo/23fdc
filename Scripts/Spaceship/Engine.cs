using System;
using System.Collections.Generic;
using UnityEngine;

namespace Kosmos6
{
    public class Engine : MonoBehaviour
    {
        [Serializable]
        class EngineVisuals
        {
            [SerializeField] private ParticleSystem _particleSystem;

            [Header("Settings")]
            [SerializeField] private float _psEmiittedMax = 50;
            [SerializeField] private float _psEmiittedMin = 0;
            [SerializeField] private float _visualizationLerpRate = 0.25f;

            [Header("Current Values")]
            [SerializeField] private float _psEmiittedCurr = 0;
            [SerializeField] private float _visualizationCurrNormalized = 0; //0-1

            public void VisualizeThrust(float inputMove)
            {
                var emission = _particleSystem.emission;
                _visualizationCurrNormalized = Mathf.Lerp(_visualizationCurrNormalized, inputMove, _visualizationLerpRate);
                _psEmiittedCurr = _psEmiittedMax * _visualizationCurrNormalized;
                emission.rateOverTime = Mathf.Max(_psEmiittedCurr, _psEmiittedMin);
            }
        }

        [SerializeField] private float _moveSpeed = 100f;
        [SerializeField] private List<EngineVisuals> _engineVisuals = new List<EngineVisuals>();

        public Vector3 Thrust(float inputMoveY, float inputMoveX)
        {
            VisualizeThrust(inputMoveY + inputMoveX);

            Vector3 calculateThrustY = inputMoveY * -transform.forward;
            Vector3 calculateThrustX = inputMoveX * -transform.right;

            Vector3 direction = calculateThrustX + calculateThrustY;

            direction.Normalize();

            return _moveSpeed * Time.deltaTime * direction;
        }


        private void VisualizeThrust(float InputMove)
        {
            foreach (var ev in _engineVisuals)
            {
                ev.VisualizeThrust(InputMove);
            }
        }
    }
}

