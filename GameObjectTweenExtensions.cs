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
using System.Collections.Generic;
using UnityEngine;

namespace DigitalRuby.Tween
{
#if IS_UNITY

    /// <summary>
    /// Extensions for tween for game objects - unity only
    /// </summary>
    public static class GameObjectTweenExtensions
    {
        /// <summary>
        /// Start and add a float tween
        /// </summary>
        /// <param name="obj">Game object</param>
        /// <param name="key">Key</param>
        /// <param name="start">Start value</param>
        /// <param name="end">End value</param>
        /// <param name="duration">Duration in seconds</param>
        /// <param name="scaleFunc">Scale function</param>
        /// <param name="progress">Progress handler</param>
        /// <param name="completion">Completion handler</param>
        /// <returns>FloatTween</returns>
        public static FloatTween Tween(this GameObject obj, object key, float start, float end, float duration, Func<float, float> scaleFunc, System.Action<ITween<float>> progress, System.Action<ITween<float>> completion = null)
        {
            FloatTween t = TweenFactory.Tween(key, start, end, duration, scaleFunc, progress, completion);
            t.GameObject = obj;
            t.Renderer = obj.GetComponent<Renderer>();
            return t;
        }

        /// <summary>
        /// Start and add a Vector2 tween
        /// </summary>
        /// <param name="obj">Game object</param>
        /// <param name="key">Key</param>
        /// <param name="start">Start value</param>
        /// <param name="end">End value</param>
        /// <param name="duration">Duration in seconds</param>
        /// <param name="scaleFunc">Scale function</param>
        /// <param name="progress">Progress handler</param>
        /// <param name="completion">Completion handler</param>
        /// <returns>Vector2Tween</returns>
        public static Vector2Tween Tween(this GameObject obj, object key, Vector2 start, Vector2 end, float duration, Func<float, float> scaleFunc, System.Action<ITween<Vector2>> progress, System.Action<ITween<Vector2>> completion = null)
        {
            Vector2Tween t = TweenFactory.Tween(key, start, end, duration, scaleFunc, progress, completion);
            t.GameObject = obj;
            t.Renderer = obj.GetComponent<Renderer>();
            return t;
        }

        /// <summary>
        /// Start and add a Vector3 tween
        /// </summary>
        /// <param name="obj">Game object</param>
        /// <param name="key">Key</param>
        /// <param name="start">Start value</param>
        /// <param name="end">End value</param>
        /// <param name="duration">Duration in seconds</param>
        /// <param name="scaleFunc">Scale function</param>
        /// <param name="progress">Progress handler</param>
        /// <param name="completion">Completion handler</param>
        /// <returns>Vector3Tween</returns>
        public static Vector3Tween Tween(this GameObject obj, object key, Vector3 start, Vector3 end, float duration, Func<float, float> scaleFunc, System.Action<ITween<Vector3>> progress, System.Action<ITween<Vector3>> completion = null)
        {
            Vector3Tween t = TweenFactory.Tween(key, start, end, duration, scaleFunc, progress, completion);
            t.GameObject = obj;
            t.Renderer = obj.GetComponent<Renderer>();
            return t;
        }

        /// <summary>
        /// Start and add a Vector4 tween
        /// </summary>
        /// <param name="obj">Game object</param>
        /// <param name="key">Key</param>
        /// <param name="start">Start value</param>
        /// <param name="end">End value</param>
        /// <param name="duration">Duration in seconds</param>
        /// <param name="scaleFunc">Scale function</param>
        /// <param name="progress">Progress handler</param>
        /// <param name="completion">Completion handler</param>
        /// <returns>Vector4Tween</returns>
        public static Vector4Tween Tween(this GameObject obj, object key, Vector4 start, Vector4 end, float duration, Func<float, float> scaleFunc, System.Action<ITween<Vector4>> progress, System.Action<ITween<Vector4>> completion = null)
        {
            Vector4Tween t = TweenFactory.Tween(key, start, end, duration, scaleFunc, progress, completion);
            t.GameObject = obj;
            t.Renderer = obj.GetComponent<Renderer>();
            return t;
        }

        /// <summary>
        /// Start and add a Color tween
        /// </summary>
        /// <param name="obj">Game object</param>
        /// <param name="start">Start value</param>
        /// <param name="end">End value</param>
        /// <param name="duration">Duration in seconds</param>
        /// <param name="scaleFunc">Scale function</param>
        /// <param name="progress">Progress handler</param>
        /// <param name="completion">Completion handler</param>
        /// <returns>ColorTween</returns>
        public static ColorTween Tween(this GameObject obj, object key, Color start, Color end, float duration, Func<float, float> scaleFunc, System.Action<ITween<Color>> progress, System.Action<ITween<Color>> completion = null)
        {
            ColorTween t = TweenFactory.Tween(key, start, end, duration, scaleFunc, progress, completion);
            t.GameObject = obj;
            t.Renderer = obj.GetComponent<Renderer>();
            return t;
        }

        /// <summary>
        /// Start and add a Quaternion tween
        /// </summary>
        /// <param name="obj">Game object</param>
        /// <param name="start">Start value</param>
        /// <param name="end">End value</param>
        /// <param name="duration">Duration in seconds</param>
        /// <param name="scaleFunc">Scale function</param>
        /// <param name="progress">Progress handler</param>
        /// <param name="completion">Completion handler</param>
        /// <returns>QuaternionTween</returns>
        public static QuaternionTween Tween(this GameObject obj, object key, Quaternion start, Quaternion end, float duration, Func<float, float> scaleFunc, System.Action<ITween<Quaternion>> progress, System.Action<ITween<Quaternion>> completion = null)
        {
            QuaternionTween t = TweenFactory.Tween(key, start, end, duration, scaleFunc, progress, completion);
            t.GameObject = obj;
            t.Renderer = obj.GetComponent<Renderer>();
            return t;
        }
    }

#endif

}