using DigitalRuby.Tween;

namespace UnityEngine
{
    public class WaitForTween : CustomYieldInstruction
    {
        private ITween _tween;

        public WaitForTween(ITween tween)
        {
            _tween = tween;
        }

        public override bool keepWaiting => _tween?.State != TweenState.Stopped;
    }
}
