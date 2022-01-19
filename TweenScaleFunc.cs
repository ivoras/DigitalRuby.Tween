using System;

namespace DigitalRuby.Tween
{
    public enum TweenScaleFunc
    {
        /// <summary>
        /// x = y
        /// </summary>
        Linear,
        /// <summary>
        /// x = siny
        /// </summary>
        Sine,
        /// <summary>
        /// x = y^2
        /// </summary>
        Quadratic,
        /// <summary>
        /// x = y^3
        /// </summary>
        Cubic,
        /// <summary>
        /// x = y^4
        /// </summary>
        Quartic,
        /// <summary>
        /// x = y^5
        /// </summary>
        Quintic,
        /// <summary>
        /// x = y^8
        /// </summary>
        Octic
    }

    public enum FuncEase
    {
        EaseInOut,
        EaseIn,
        EaseOut
    }

    public static class TweenScaleFuncExtensions
    {
        public static Func<float, float> GetFunc(this TweenScaleFunc func, FuncEase ease)
        {
            switch (func)
            {
                default:
                case TweenScaleFunc.Linear: return TweenScaleFunctions.Linear;
                case TweenScaleFunc.Sine:
                    switch (ease)
                    {
                        case FuncEase.EaseIn: return TweenScaleFunctions.SineEaseIn;
                        case FuncEase.EaseOut: return TweenScaleFunctions.SineEaseOut;
                        default:
                        case FuncEase.EaseInOut: return TweenScaleFunctions.SineEaseInOut;
                    }
                case TweenScaleFunc.Quadratic:
                    switch (ease)
                    {
                        case FuncEase.EaseIn: return TweenScaleFunctions.QuadraticEaseIn;
                        case FuncEase.EaseOut: return TweenScaleFunctions.QuadraticEaseOut;
                        default:
                        case FuncEase.EaseInOut: return TweenScaleFunctions.QuadraticEaseInOut;
                    }
                case TweenScaleFunc.Cubic:
                    switch (ease)
                    {
                        case FuncEase.EaseIn: return TweenScaleFunctions.CubicEaseIn;
                        case FuncEase.EaseOut: return TweenScaleFunctions.CubicEaseOut;
                        default:
                        case FuncEase.EaseInOut: return TweenScaleFunctions.CubicEaseInOut;
                    }
                case TweenScaleFunc.Quartic:
                    switch (ease)
                    {
                        case FuncEase.EaseIn: return TweenScaleFunctions.QuarticEaseIn;
                        case FuncEase.EaseOut: return TweenScaleFunctions.QuarticEaseOut;
                        default:
                        case FuncEase.EaseInOut: return TweenScaleFunctions.QuarticEaseInOut;
                    }
                case TweenScaleFunc.Quintic:
                    switch (ease)
                    {
                        case FuncEase.EaseIn: return TweenScaleFunctions.QuinticEaseIn;
                        case FuncEase.EaseOut: return TweenScaleFunctions.QuinticEaseOut;
                        default:
                        case FuncEase.EaseInOut: return TweenScaleFunctions.QuinticEaseInOut;
                    }
                case TweenScaleFunc.Octic:
                    switch (ease)
                    {
                        case FuncEase.EaseIn: return TweenScaleFunctions.OcticEaseIn;
                        case FuncEase.EaseOut: return TweenScaleFunctions.OcticEaseOut;
                        default:
                        case FuncEase.EaseInOut: return TweenScaleFunctions.OcticEaseInOut;
                    }
            }
        }
    }
}
