using System.Collections;
using UnityEngine;

namespace Utilities
{
    public class BlinkController : MonoBehaviour
    {
        [Header("References")] [SerializeField]
        private PlayerModel _playerModel;

        [SerializeField] private CanvasGroup _stressFlash;
        [SerializeField] private CanvasGroup _famousFlash;

        [Header("Settings")] [SerializeField] private float _pulseSpeed = 4f;
        [SerializeField] private float _maxAlpha = 0.5f;

        private Coroutine _stressCo, _famousCo;

        void Awake()
        {
            _playerModel.OnStressChanged += HandleStressChange;
            _playerModel.OnFamousChanged += HandleFamousChange;
        }

        void Start()
        {
            // Check initial state
            HandleStressChange(_playerModel.Stress);
            HandleFamousChange(_playerModel.Famous);
        }

        void HandleStressChange(int value)
        {
            HandleBlink(value, IsStressInDanger, ref _stressCo, _stressFlash);
        }

        void HandleFamousChange(int value)
        {
            HandleBlink(value, IsFamousInDanger, ref _famousCo, _famousFlash);
        }

        static bool IsStressInDanger(int v) => v <= 20 || v >= 80;
        static bool IsFamousInDanger(int v) => v <= 20;

        void HandleBlink(int value, System.Func<int, bool> dangerCheck, ref Coroutine co, CanvasGroup cg)
        {
            if (dangerCheck(value))
            {
                if (co == null) co = StartCoroutine(BlinkRoutine(cg));
            }
            else
            {
                if (co != null)
                {
                    StopCoroutine(co);
                    co = null;
                    cg.alpha = 0;
                }
            }
        }

        IEnumerator BlinkRoutine(CanvasGroup cg)
        {
            while (true)
            {
                cg.alpha = Mathf.Abs(Mathf.Sin(Time.time * _pulseSpeed)) * _maxAlpha;
                yield return null;
            }
        }

        void OnDestroy()
        {
            if (_playerModel != null)
            {
                _playerModel.OnStressChanged -= HandleStressChange;
                _playerModel.OnFamousChanged -= HandleFamousChange;
            }

            // Clean up coroutines
            if (_stressCo != null) StopCoroutine(_stressCo);
            if (_famousCo != null) StopCoroutine(_famousCo);
        }
    }
}