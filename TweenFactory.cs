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
    /// Tween manager - do not add directly as a script, instead call the static methods in your other scripts.
    /// </summary>
    public class TweenFactory : MonoBehaviour
    {
        private static GameObject root;
        private static readonly List<ITween> tweens = new List<ITween>();
        private static GameObject toDestroy;

        private static void EnsureCreated()
        {
            if (root == null && Application.isPlaying)
            {
                root = GameObject.Find("DigitalRubyTween");
                if (root == null || root.GetComponent<TweenFactory>() == null)
                {
                    if (root != null)
                    {
                        toDestroy = root;
                    }
                    root = new GameObject { name = "DigitalRubyTween", hideFlags = HideFlags.HideAndDontSave };
                    root.AddComponent<TweenFactory>().hideFlags = HideFlags.HideAndDontSave;
                }
                if (Application.isPlaying)
                {
                    GameObject.DontDestroyOnLoad(root);
                }
            }
        }

        private void Start()
        {
            UnityEngine.SceneManagement.SceneManager.sceneLoaded += SceneManagerSceneLoaded;
            if (toDestroy != null)
            {
                GameObject.Destroy(toDestroy);
                toDestroy = null;
            }
        }

        private void SceneManagerSceneLoaded(UnityEngine.SceneManagement.Scene s, UnityEngine.SceneManagement.LoadSceneMode m)
        {
            if (ClearTweensOnLevelLoad)
            {
                tweens.Clear();
            }
        }

        private void Update()
        {
            ITween t;

            for (int i = tweens.Count - 1; i >= 0; i--)
            {
                t = tweens[i];
                if (t.Update(t.TimeFunc()) && i < tweens.Count && tweens[i] == t)
                {
                    tweens.RemoveAt(i);
                }
            }
        }

        /// <summary>
        /// Start and add a float tween
        /// </summary>
        /// <param name="key">Key</param>
        /// <param name="start">Start value</param>
        /// <param name="end">End value</param>
        /// <param name="duration">Duration in seconds</param>
        /// <param name="scaleFunc">Scale function</param>
        /// <param name="progress">Progress handler</param>
        /// <param name="completion">Completion handler</param>
        /// <returns>FloatTween</returns>
        public static Tween<T> Tween<T>(object key, T start, T end, float duration, Func<float, float> scaleFunc, Action<ITween<T>> progress, Action<ITween<T>> completion = null) where T : struct
        {
            switch (typeof(T).Name)
            {
                case nameof(Single): return (Tween<T>)(object)Tween(key, (float)(object)start, (float)(object)end, duration, scaleFunc, (Action<ITween<float>>)progress, (Action<ITween<float>>)completion);
                case nameof(Vector2): return (Tween<T>)(object)Tween(key, (Vector2)(object)start, (Vector2)(object)end, duration, scaleFunc, (Action<ITween<Vector2>>)progress, (Action<ITween<Vector2>>)completion);
                case nameof(Vector3): return (Tween<T>)(object)Tween(key, (Vector3)(object)start, (Vector3)(object)end, duration, scaleFunc, (Action<ITween<Vector3>>)progress, (Action<ITween<Vector3>>)completion);
                case nameof(Vector4): return (Tween<T>)(object)Tween(key, (Vector4)(object)start, (Vector4)(object)end, duration, scaleFunc, (Action<ITween<Vector4>>)progress, (Action<ITween<Vector4>>)completion);
                case nameof(Color): return (Tween<T>)(object)Tween(key, (Color)(object)start, (Color)(object)end, duration, scaleFunc, (Action<ITween<Color>>)progress, (Action<ITween<Color>>)completion);
                case nameof(Quaternion): return (Tween<T>)(object)Tween(key, (Quaternion)(object)start, (Quaternion)(object)end, duration, scaleFunc, (Action<ITween<Quaternion>>)progress, (Action<ITween<Quaternion>>)completion);
                default: throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Start and add a float tween
        /// </summary>
        /// <param name="key">Key</param>
        /// <param name="start">Start value</param>
        /// <param name="end">End value</param>
        /// <param name="duration">Duration in seconds</param>
        /// <param name="scaleFunc">Scale function</param>
        /// <param name="progress">Progress handler</param>
        /// <param name="completion">Completion handler</param>
        /// <returns>FloatTween</returns>
        public static FloatTween Tween(object key, float start, float end, float duration, Func<float, float> scaleFunc, System.Action<ITween<float>> progress, System.Action<ITween<float>> completion = null)
        {
            FloatTween t = new FloatTween();
            t.Key = key;
            t.Setup(start, end, duration, scaleFunc, progress, completion);
            t.Start();
            AddTween(t);

            return t;
        }

        /// <summary>
        /// Start and add a Vector2 tween
        /// </summary>
        /// <param name="key">Key</param>
        /// <param name="start">Start value</param>
        /// <param name="end">End value</param>
        /// <param name="duration">Duration in seconds</param>
        /// <param name="scaleFunc">Scale function</param>
        /// <param name="progress">Progress handler</param>
        /// <param name="completion">Completion handler</param>
        /// <returns>Vector2Tween</returns>
        public static Vector2Tween Tween(object key, Vector2 start, Vector2 end, float duration, Func<float, float> scaleFunc, System.Action<ITween<Vector2>> progress, System.Action<ITween<Vector2>> completion = null)
        {
            Vector2Tween t = new Vector2Tween();
            t.Key = key;
            t.Setup(start, end, duration, scaleFunc, progress, completion);
            t.Start();
            AddTween(t);

            return t;
        }

        /// <summary>
        /// Start and add a Vector3 tween
        /// </summary>
        /// <param name="key">Key</param>
        /// <param name="start">Start value</param>
        /// <param name="end">End value</param>
        /// <param name="duration">Duration in seconds</param>
        /// <param name="scaleFunc">Scale function</param>
        /// <param name="progress">Progress handler</param>
        /// <param name="completion">Completion handler</param>
        /// <returns>Vector3Tween</returns>
        public static Vector3Tween Tween(object key, Vector3 start, Vector3 end, float duration, Func<float, float> scaleFunc, System.Action<ITween<Vector3>> progress, System.Action<ITween<Vector3>> completion = null)
        {
            Vector3Tween t = new Vector3Tween();
            t.Key = key;
            t.Setup(start, end, duration, scaleFunc, progress, completion);
            t.Start();
            AddTween(t);

            return t;
        }

        /// <summary>
        /// Start and add a Vector4 tween
        /// </summary>
        /// <param name="key">Key</param>
        /// <param name="start">Start value</param>
        /// <param name="end">End value</param>
        /// <param name="duration">Duration in seconds</param>
        /// <param name="scaleFunc">Scale function</param>
        /// <param name="progress">Progress handler</param>
        /// <param name="completion">Completion handler</param>
        /// <returns>Vector4Tween</returns>
        public static Vector4Tween Tween(object key, Vector4 start, Vector4 end, float duration, Func<float, float> scaleFunc, System.Action<ITween<Vector4>> progress, System.Action<ITween<Vector4>> completion = null)
        {
            Vector4Tween t = new Vector4Tween();
            t.Key = key;
            t.Setup(start, end, duration, scaleFunc, progress, completion);
            t.Start();
            AddTween(t);

            return t;
        }

        /// <summary>
        /// Start and add a Color tween
        /// </summary>
        /// <param name="start">Start value</param>
        /// <param name="end">End value</param>
        /// <param name="duration">Duration in seconds</param>
        /// <param name="scaleFunc">Scale function</param>
        /// <param name="progress">Progress handler</param>
        /// <param name="completion">Completion handler</param>
        /// <returns>ColorTween</returns>
        public static ColorTween Tween(object key, Color start, Color end, float duration, Func<float, float> scaleFunc, System.Action<ITween<Color>> progress, System.Action<ITween<Color>> completion = null)
        {
            ColorTween t = new ColorTween();
            t.Key = key;
            t.Setup(start, end, duration, scaleFunc, progress, completion);
            t.Start();
            AddTween(t);

            return t;
        }

        /// <summary>
        /// Start and add a Quaternion tween
        /// </summary>
        /// <param name="start">Start value</param>
        /// <param name="end">End value</param>
        /// <param name="duration">Duration in seconds</param>
        /// <param name="scaleFunc">Scale function</param>
        /// <param name="progress">Progress handler</param>
        /// <param name="completion">Completion handler</param>
        /// <returns>QuaternionTween</returns>
        public static QuaternionTween Tween(object key, Quaternion start, Quaternion end, float duration, Func<float, float> scaleFunc, System.Action<ITween<Quaternion>> progress, System.Action<ITween<Quaternion>> completion = null)
        {
            QuaternionTween t = new QuaternionTween();
            t.Key = key;
            t.Setup(start, end, duration, scaleFunc, progress, completion);
            t.Start();
            AddTween(t);

            return t;
        }

        /// <summary>
        /// Add a tween
        /// </summary>
        /// <param name="tween">Tween to add</param>
        public static void AddTween(ITween tween)
        {
            EnsureCreated();
            if (tween.Key != null)
            {
                RemoveTweenKey(tween.Key, AddKeyStopBehavior);
            }
            tweens.Add(tween);
        }

        /// <summary>
        /// Remove a tween
        /// </summary>
        /// <param name="tween">Tween to remove</param>
        /// <param name="stopBehavior">Stop behavior</param>
        /// <returns>True if removed, false if not</returns>
        public static bool RemoveTween(ITween tween, TweenStopBehavior stopBehavior)
        {
            tween.Stop(stopBehavior);
            return tweens.Remove(tween);
        }

        /// <summary>
        /// Remove a tween by key
        /// </summary>
        /// <param name="key">Key to remove</param>
        /// <param name="stopBehavior">Stop behavior</param>
        /// <returns>True if removed, false if not</returns>
        public static bool RemoveTweenKey(object key, TweenStopBehavior stopBehavior)
        {
            if (key == null)
            {
                return false;
            }

            bool foundOne = false;
            for (int i = tweens.Count - 1; i >= 0; i--)
            {
                ITween t = tweens[i];
                if (key.Equals(t.Key))
                {
                    t.Stop(stopBehavior);
                    tweens.RemoveAt(i);
                    foundOne = true;
                }
            }
            return foundOne;
        }

        /// <summary>
        /// Clear all tweens
        /// </summary>
        public static void Clear()
        {
            tweens.Clear();
        }

        /// <summary>
        /// Stop behavior if you add a tween with a key and tweens already exist with the key
        /// </summary>
        public static TweenStopBehavior AddKeyStopBehavior = TweenStopBehavior.DoNotModify;

        /// <summary>
        /// Whether to clear tweens on level load, default is false
        /// </summary>
        public static bool ClearTweensOnLevelLoad { get; set; }

        /// <summary>
        /// Default time func
        /// </summary>
        public static Func<float> DefaultTimeFunc = TimeFuncDeltaTime;

        /// <summary>
        /// Time func delta time instance
        /// </summary>
        public static readonly Func<float> TimeFuncDeltaTimeFunc = TimeFuncDeltaTime;

        /// <summary>
        /// Time func unscaled delta time instance
        /// </summary>
        public static readonly Func<float> TimeFuncUnscaledDeltaTimeFunc = TimeFuncUnscaledDeltaTime;

        /// <summary>
        /// Time func that uses Time.deltaTime
        /// </summary>
        /// <returns>Time.deltaTime</returns>
        private static float TimeFuncDeltaTime()
        {
            return Time.deltaTime;
        }

        /// <summary>
        /// Time func that uses Time.unscaledDeltaTime
        /// </summary>
        /// <returns>Time.unscaledDeltaTime</returns>
        private static float TimeFuncUnscaledDeltaTime()
        {
            return Time.unscaledDeltaTime;
        }
    }

#endif

}