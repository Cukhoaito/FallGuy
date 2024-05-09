using System;
using Animancer;
using Animancer.Units;
using UnityEngine;

namespace FallGuy.Character.States
{
    public class IdleState : CharacterState
    {
        [SerializeField] private ClipTransition _mainAnimation;
        [SerializeField, Seconds] private float _firstRandomizeDelay = 5;
        [SerializeField, Seconds] private float _minRandomizeInterval = 0;
        [SerializeField, Seconds] private float _maxRandomizeInerval = 20;
        [SerializeField] private ClipTransition[] _randomAnimations;
        private float _randomizeTime;
        public override bool CanEnterState => Character.Body.OnGround;
        private void Awake()
        {
            Action onEnd = PlayMainAnimation;
            foreach (var animation in _randomAnimations)
            {
                animation.Events.OnEnd = onEnd;
            }
        }
        private void OnEnable()
        {
            PlayMainAnimation();
            _randomizeTime += _firstRandomizeDelay;
        }
        private void PlayMainAnimation()
        {
            _randomizeTime = UnityEngine.Random
                .Range(_minRandomizeInterval, _maxRandomizeInerval);
            Character.Animancer.Play(_mainAnimation);
        }
        private void PlayRandomAnimation()
        {
            if (_randomAnimations.Length == 0) return;
            var index = UnityEngine.Random.Range(0, _randomAnimations.Length);
            var animation = _randomAnimations[index];
            Character.Animancer.Play(animation);
            CustomFade.Apply(Character.Animancer, Easing.Sine.InOut);
        }
        private void FixedUpdate()
        {
            Character.Rigidbody.velocity = default;
            var state = Character.Animancer.States.Current;

            if (_randomAnimations.Length == 0) return;
            if (state == _mainAnimation.State &&
                state.Time >= _randomizeTime)
            {
                PlayRandomAnimation();
            }
        }

    }
}