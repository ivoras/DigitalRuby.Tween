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

    /// <summary>
    /// Interface for a tween object.
    /// </summary>
    public interface ITween
    {
        /// <summary>
        /// The key that identifies this tween - can be null
        /// </summary>
        object Key { get; }

        /// <summary>
        /// Gets the current state of the tween.
        /// </summary>
        TweenState State { get; }

        /// <summary>
        /// Time function
        /// </summary>
        System.Func<float> TimeFunc { get; set; }

        /// <summary>
        /// Start the tween.
        /// </summary>
        void Start();

        /// <summary>
        /// Pauses the tween.
        /// </summary>
        void Pause();

        /// <summary>
        /// Resumes the paused tween.
        /// </summary>
        void Resume();

        /// <summary>
        /// Stops the tween.
        /// </summary>
        /// <param name="stopBehavior">The behavior to use to handle the stop.</param>
        void Stop(TweenStopBehavior stopBehavior);

        /// <summary>
        /// Updates the tween.
        /// </summary>
        /// <param name="elapsedTime">The elapsed time to add to the tween.</param>
        /// <returns>True if done, false if not</returns>
        bool Update(float elapsedTime);
    }
}