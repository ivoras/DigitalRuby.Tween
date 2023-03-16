/*
The MIT License (MIT)
Copyright (c) 2016 Digital Ruby, LLC
http://www.digitalruby.com
Created by Jeff Johnson

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
*/

#if UNITY || UNITY_2017_4_OR_NEWER

#define IS_UNITY

#endif

using System;
using UnityEngine;

namespace DigitalRuby.Tween
{
    /// <summary>
    /// Tween scale functions
    /// Implementations based on http://theinstructionlimit.com/flash-style-tweeneasing-functions-in-c, which are based on http://www.robertpenner.com/easing/
    /// </remarks>
    public static class TweenScaleFunctions
    {
        private const float halfPi = Mathf.PI * 0.5f;

        /// <summary>
        /// A linear progress scale function.
        /// </summary>
        public static readonly Func<float, float> Linear = LinearFunc;
        private static float LinearFunc(float progress) { return progress; }

        /// <summary>
        /// A quadratic (x^2) progress scale function that eases in.
        /// </summary>
        public static readonly Func<float, float> QuadraticEaseIn = QuadraticEaseInFunc;
        private static float QuadraticEaseInFunc(float progress) { return EaseInPower(progress, 2); }

        /// <summary>
        /// A quadratic (x^2) progress scale function that eases out.
        /// </summary>
        public static readonly Func<float, float> QuadraticEaseOut = QuadraticEaseOutFunc;
        private static float QuadraticEaseOutFunc(float progress) { return EaseOutPower(progress, 2); }

        /// <summary>
        /// A quadratic (x^2) progress scale function that eases in and out.
        /// </summary>
        public static readonly Func<float, float> QuadraticEaseInOut = QuadraticEaseInOutFunc;
        private static float QuadraticEaseInOutFunc(float progress) { return EaseInOutPower(progress, 2); }

        /// <summary>
        /// A cubic (x^3) progress scale function that eases in.
        /// </summary>
        public static readonly Func<float, float> CubicEaseIn = CubicEaseInFunc;
        private static float CubicEaseInFunc(float progress) { return EaseInPower(progress, 3); }

        /// <summary>
        /// A cubic (x^3) progress scale function that eases out.
        /// </summary>
        public static readonly Func<float, float> CubicEaseOut = CubicEaseOutFunc;
        private static float CubicEaseOutFunc(float progress) { return EaseOutPower(progress, 3); }

        /// <summary>
        /// A cubic (x^3) progress scale function that eases in and out.
        /// </summary>
        public static readonly Func<float, float> CubicEaseInOut = CubicEaseInOutFunc;
        private static float CubicEaseInOutFunc(float progress) { return EaseInOutPower(progress, 3); }

        /// <summary>
        /// A quartic (x^4) progress scale function that eases in.
        /// </summary>
        public static readonly Func<float, float> QuarticEaseIn = QuarticEaseInFunc;
        private static float QuarticEaseInFunc(float progress) { return EaseInPower(progress, 4); }

        /// <summary>
        /// A quartic (x^4) progress scale function that eases out.
        /// </summary>
        public static readonly Func<float, float> QuarticEaseOut = QuarticEaseOutFunc;
        private static float QuarticEaseOutFunc(float progress) { return EaseOutPower(progress, 4); }

        /// <summary>
        /// A quartic (x^4) progress scale function that eases in and out.
        /// </summary>
        public static readonly Func<float, float> QuarticEaseInOut = QuarticEaseInOutFunc;
        private static float QuarticEaseInOutFunc(float progress) { return EaseInOutPower(progress, 4); }

        /// <summary>
        /// A quintic (x^5) progress scale function that eases in.
        /// </summary>
        public static readonly Func<float, float> QuinticEaseIn = QuinticEaseInFunc;
        private static float QuinticEaseInFunc(float progress) { return EaseInPower(progress, 5); }

        /// <summary>
        /// A quintic (x^5) progress scale function that eases out.
        /// </summary>
        public static readonly Func<float, float> QuinticEaseOut = QuinticEaseOutFunc;
        private static float QuinticEaseOutFunc(float progress) { return EaseOutPower(progress, 5); }

        /// <summary>
        /// A quintic (x^5) progress scale function that eases in and out.
        /// </summary>
        public static readonly Func<float, float> QuinticEaseInOut = QuinticEaseInOutFunc;
        private static float QuinticEaseInOutFunc(float progress) { return EaseInOutPower(progress, 5); }

        /// <summary>
        /// A quintic (x^8) progress scale function that eases in.
        /// </summary>
        public static readonly Func<float, float> OcticEaseIn = OcticEaseInFunc;
        private static float OcticEaseInFunc(float progress) { return EaseInPower(progress, 8); }

        /// <summary>
        /// A quintic (x^8) progress scale function that eases out.
        /// </summary>
        public static readonly Func<float, float> OcticEaseOut = OcticEaseOutFunc;
        private static float OcticEaseOutFunc(float progress) { return EaseOutPower(progress, 8); }

        /// <summary>
        /// A quintic (x^8) progress scale function that eases in and out.
        /// </summary>
        public static readonly Func<float, float> OcticEaseInOut = OcticEaseInOutFunc;
        private static float OcticEaseInOutFunc(float progress) { return EaseInOutPower(progress, 8); }

        /// <summary>
        /// A sine progress scale function that eases in.
        /// </summary>
        public static readonly Func<float, float> SineEaseIn = SineEaseInFunc;
        private static float SineEaseInFunc(float progress) { return Mathf.Sin(progress * halfPi - halfPi) + 1; }

        /// <summary>
        /// A sine progress scale function that eases out.
        /// </summary>
        public static readonly Func<float, float> SineEaseOut = SineEaseOutFunc;
        private static float SineEaseOutFunc(float progress) { return Mathf.Sin(progress * halfPi); }

        /// <summary>
        /// A sine progress scale function that eases in and out.
        /// </summary>
        public static readonly Func<float, float> SineEaseInOut = SineEaseInOutFunc;
        private static float SineEaseInOutFunc(float progress) { return (Mathf.Sin(progress * Mathf.PI - halfPi) + 1) / 2; }

        /// <summary>
        /// An exponential progress scale function that eases in.
        /// </summary>
        public static readonly Func<float, float> ExpoEaseIn = ExpoEaseInFunc;
        private static float ExpoEaseInFunc(float x) { return x == 0 ? 0 : Mathf.Pow(2, 10 * x - 10); }

        /// <summary>
        /// An exponential progress scale function that eases out.
        /// </summary>
        public static readonly Func<float, float> ExpoEaseOut = ExpoEaseOutFunc;
        private static float ExpoEaseOutFunc(float x) { return x == 1 ? 1 : 1 - Mathf.Pow(2, -10 * x); }

        /// <summary>
        /// An exponential progress scale function that eases in and out.
        /// </summary>
        public static readonly Func<float, float> ExpoEaseInOut = ExpoEaseInOutFunc;
        private static float ExpoEaseInOutFunc(float x) {
            return x == 0
                ? 0
                : x == 1
                    ? 1
                    : x < 0.5
                        ? Mathf.Pow(2, 20 * x - 10) / 2
                        : (2 - Mathf.Pow(2, -20 * x + 10)) / 2;
        }

        /// <summary>
        /// A progress scale function close to a circle segment that eases in.
        /// </summary>
        public static readonly Func<float, float> CircEaseIn = CircEaseInFunc;
        private static float CircEaseInFunc(float x) { return 1 - Mathf.Sqrt(1 - Mathf.Pow(x, 2)); }

        /// <summary>
        /// A progress scale function close to a circle segment that eases out.
        /// </summary>
        public static readonly Func<float, float> CircEaseOut = CircEaseOutFunc;
        private static float CircEaseOutFunc(float x) { return Mathf.Sqrt(1 - Mathf.Pow(x - 1, 2)); }

        /// <summary>
        /// A progress scale function close to circle segment that eases in and out.
        /// </summary>
        public static readonly Func<float, float> CircEaseInOut = CircEaseInOutFunc;
        private static float CircEaseInOutFunc(float x) {
            return x < 0.5
                ? (1 - Mathf.Sqrt(1 - Mathf.Pow(2 * x, 2))) / 2
                : (Mathf.Sqrt(1 - Mathf.Pow(-2 * x + 2, 2)) + 1) / 2;
        }

        /// <summary>
        /// A progress scale function doing a slight bounce that eases in.
        /// </summary>
        public static readonly Func<float, float> ElasticEaseIn = ElasticEaseInFunc;
        private static float ElasticEaseInFunc(float x) {
            return x == 0
			? 0
            : x == 1
                ? 1
                : -Mathf.Pow(2, 10 * x - 10) * Mathf.Sin((x * 10 - 10.75f) * c4);
        }

        /// <summary>
        /// A progress scale function doing a slight bounce that eases out.
        /// </summary>
        public static readonly Func<float, float> ElasticEaseOut = ElasticEaseOutFunc;
        private static float ElasticEaseOutFunc(float x) {
            return x == 0
			? 0
			: x == 1
                ? 1
                : Mathf.Pow(2, -10 * x) * Mathf.Sin((x * 10 - 0.75f) * c4) + 1;
         }

        /// <summary>
        /// A progress scale function doing a slight bounce that eases in and out.
        /// </summary>
        public static readonly Func<float, float> ElasticEaseInOut = ElasticEaseInOutFunc;
        private static float ElasticEaseInOutFunc(float x) {
            return x == 0
                ? 0
                : x == 1
                    ? 1
                    : x < 0.5
                        ? -(Mathf.Pow(2, 20 * x - 10) * Mathf.Sin((20 * x - 11.125f) * c5)) / 2
                        : (Mathf.Pow(2, -20 * x + 10) * Mathf.Sin((20 * x - 11.125f) * c5)) / 2 + 1;
        }

        /// <summary>
        /// A progress scale function with a hump that eases in.
        /// </summary>
        public static readonly Func<float, float> BackEaseIn = BackEaseInFunc;
        private static float BackEaseInFunc(float progress) { return c3 * progress * progress * progress - c1 * progress * progress; }

        /// <summary>
        /// A progress scale function with a hump that eases out.
        /// </summary>
        public static readonly Func<float, float> BackEaseOut = BackEaseOutFunc;
        private static float BackEaseOutFunc(float progress) { return 1 + c3 * Mathf.Pow(progress - 1, 3) + c1 * Mathf.Pow(progress - 1, 2); }

        /// <summary>
        /// A progress scale function with two humps that eases in and out.
        /// </summary>
        public static readonly Func<float, float> BackEaseInOut = BackEaseInOutFunc;
        private static float BackEaseInOutFunc(float x) {
            return x < 0.5
			? (Mathf.Pow(2 * x, 2) * ((c2 + 1) * 2 * x - c2)) / 2
			: (Mathf.Pow(2 * x - 2, 2) * ((c2 + 1) * (x * 2 - 2) + c2) + 2) / 2;
        }

        private static float bounceOut(float x) {
            const float n1 = 7.5625f;
            const float d1 = 2.75f;

            if (x < 1 / d1) {
                return n1 * x * x;
            } else if (x < 2 / d1) {
                return n1 * (x -= 1.5f / d1) * x + 0.75f;
            } else if (x < 2.5 / d1) {
                return n1 * (x -= 2.25f / d1) * x + 0.9375f;
            } else {
                return n1 * (x -= 2.625f / d1) * x + 0.984375f;
            }
        }

        /// <summary>
        /// A progress scale function doing several bounces that eases in.
        /// </summary>
        public static readonly Func<float, float> BounceEaseIn = BounceEaseInFunc;
        private static float BounceEaseInFunc(float x) { return 1 - bounceOut(1 - x); }

        /// <summary>
        /// A progress scale function doing several bounces that eases out.
        /// </summary>
        public static readonly Func<float, float> BounceEaseOut = BounceEaseOutFunc;
        private static float BounceEaseOutFunc(float x) { return bounceOut(x); }

        /// <summary>
        /// A progress scale function doing several bounces that eases in and out.
        /// </summary>
        public static readonly Func<float, float> BounceEaseInOut = BounceEaseInOutFunc;
        private static float BounceEaseInOutFunc(float x) {
            return x < 0.5
                ? (1 - bounceOut(1 - 2 * x)) / 2
                : (1 + bounceOut(2 * x - 1)) / 2;
        }

        private static float EaseInPower(float progress, int power)
        {
            return Mathf.Pow(progress, power);
        }

        private static float EaseOutPower(float progress, int power)
        {
            int sign = power % 2 == 0 ? -1 : 1;
            return (sign * (Mathf.Pow(progress - 1, power) + sign));
        }

        private static float EaseInOutPower(float progress, int power)
        {
            progress *= 2.0f;
            if (progress < 1)
            {
                return Mathf.Pow(progress, power) / 2.0f;
            }
            else
            {
                int sign = power % 2 == 0 ? -1 : 1;
                return (sign / 2.0f * (Mathf.Pow(progress - 2, power) + sign * 2));
            }
        }
    }
}