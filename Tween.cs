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
    /// An implementation of a tween object.
    /// </summary>
    /// <typeparam name="T">The type to tween.</typeparam>
    public class Tween<T> : ITween<T> where T : struct
    {
        private readonly Func<ITween<T>, T, T, float, T> lerpFunc;

        private float currentTime;
        private float duration;
        private Func<float, float> scaleFunc;
        private System.Action<ITween<T>> progressCallback;
        private System.Action<ITween<T>> completionCallback;
        private TweenState state;

        private T start;
        private T end;
        private T value;
        private T previewValue;

        private ITween continueWith;

        /// <summary>
        /// The key that identifies this tween - can be null
        /// </summary>
        public object Key { get; set; }

        /// <summary>
        /// Gets the current time of the tween.
        /// </summary>
        public float CurrentTime { get { return currentTime; } }

        /// <summary>
        /// Gets the duration of the tween.
        /// </summary>
        public float Duration { get { return duration; } }

        /// <summary>
        /// Delay before starting the tween
        /// </summary>
        public float Delay { get; set; }

        /// <summary>
        /// Gets the current state of the tween.
        /// </summary>
        public TweenState State { get { return state; } }

        /// <summary>
        /// Gets the starting value of the tween.
        /// </summary>
        public T StartValue { get { return start; } }

        /// <summary>
        /// Gets the ending value of the tween.
        /// </summary>
        public T EndValue { get { return end; } }

        /// <summary>
        /// Gets the current value of the tween.
        /// </summary>
        public T CurrentValue { get { return value; } }

        /// <summary>
        /// Gets the preview value of the tween.
        /// </summary>
        public T PreviewValue { get { return previewValue; } }

        /// <summary>
        /// Time function - returns elapsed time for next frame
        /// </summary>
        public System.Func<float> TimeFunc { get; set; }

#if IS_UNITY

        /// <summary>
        /// The game object - null if none
        /// </summary>
        public GameObject GameObject { get; set; }

        /// <summary>
        /// The renderer - null if none
        /// </summary>
        public Renderer Renderer { get; set; }

        /// <summary>
        /// Whether to force update even if renderer is null or not visible or deactivated, default is false
        /// </summary>
        public bool ForceUpdate { get; set; }

#endif

        /// <summary>
        /// Gets the current progress of the tween (0 - 1).
        /// </summary>
        public float CurrentProgress { get; private set; }

        /// <summary>
        /// Gets the current linear progress of the tween (0 - 1).
        /// </summary>
        public float LinearProgress { get; private set; }

        /// <summary>
        /// Initializes a new Tween with a given lerp function.
        /// </summary>
        /// <remarks>
        /// C# generics are good but not good enough. We need a delegate to know how to
        /// interpolate between the start and end values for the given type.
        /// </remarks>
        /// <param name="lerpFunc">The interpolation function for the tween type.</param>
        public Tween(Func<ITween<T>, T, T, float, T> lerpFunc)
        {
            this.lerpFunc = lerpFunc;
            state = TweenState.Stopped;

#if IS_UNITY

            TimeFunc = TweenFactory.DefaultTimeFunc;

#else

            // TODO: Implement your own time functions

#endif

        }

        /// <summary>
        /// Initialize a tween.
        /// </summary>
        /// <param name="start">The start value.</param>
        /// <param name="end">The end value.</param>
        /// <param name="duration">The duration of the tween.</param>
        /// <param name="scaleFunc">A function used to scale progress over time.</param>
        /// <param name="progress">Progress callback</param>
        /// <param name="completion">Called when the tween completes</param>
        public Tween<T> Setup(T start, T end, float duration, Func<float, float> scaleFunc, System.Action<ITween<T>> progress, System.Action<ITween<T>> completion = null)
        {
            scaleFunc = (scaleFunc ?? TweenScaleFunctions.Linear);
            currentTime = 0;
            this.duration = duration;
            this.scaleFunc = scaleFunc;
            this.progressCallback = progress;
            this.completionCallback = completion;
            this.start = start;
            this.previewValue = start;
            this.end = end;

            return this;
        }

        /// <summary>
        /// Starts a tween. Setup must be called first.
        /// </summary>
        public void Start()
        {
            if (state != TweenState.Running)
            {
                if (duration <= 0.0f && Delay <= 0.0f)
                {
                    // complete immediately
                    value = end;
                    if (progressCallback != null)
                    {
                        progressCallback(this);
                    }
                    if (completionCallback != null)
                    {
                        completionCallback(this);
                    }
                    return;
                }

                state = TweenState.Running;
                UpdateValue();
            }
        }

        /// <summary>
        /// Pauses the tween.
        /// </summary>
        public void Pause()
        {
            if (state == TweenState.Running)
            {
                state = TweenState.Paused;
            }
        }

        /// <summary>
        /// Resumes the paused tween.
        /// </summary>
        public void Resume()
        {
            if (state == TweenState.Paused)
            {
                state = TweenState.Running;
            }
        }

        /// <summary>
        /// Stops the tween.
        /// </summary>
        /// <param name="stopBehavior">The behavior to use to handle the stop.</param>
        public void Stop(TweenStopBehavior stopBehavior)
        {
            if (state != TweenState.Stopped)
            {
                state = TweenState.Stopped;
                if (stopBehavior == TweenStopBehavior.Complete)
                {
                    currentTime = duration;
                    UpdateValue();
                    if (completionCallback != null)
                    {
                        completionCallback.Invoke(this);
                        completionCallback = null;
                    }
                    if (continueWith != null)
                    {
                        continueWith.Start();

#if IS_UNITY

                        TweenFactory.AddTween(continueWith);

#else

                        // TODO: Implement your own continueWith handling

#endif

                        continueWith = null;
                    }
                }
            }
        }

        /// <summary>
        /// Updates the tween.
        /// </summary>
        /// <param name="elapsedTime">The elapsed time to add to the tween.</param>
        /// <returns>True if done, false if not</returns>
        public bool Update(float elapsedTime)
        {
            if (state == TweenState.Running)
            {
                if (Delay > 0.0f)
                {
                    currentTime += elapsedTime;
                    if (currentTime <= Delay)
                    {
                        // delay is not over yet
                        return false;
                    }
                    else
                    {
                        // set to left-over time beyond delay
                        currentTime = (currentTime - Delay);
                        Delay = 0.0f;
                    }
                }
                else
                {
                    currentTime += elapsedTime;
                }

                if (currentTime >= duration)
                {
                    Stop(TweenStopBehavior.Complete);
                    return true;
                }
                else
                {
                    UpdateValue();
                    return false;
                }
            }
            return (state == TweenState.Stopped);
        }

        /// <summary>
        /// Set another tween to execute when this tween finishes. Inherits the Key and if using Unity, GameObject, Renderer and ForceUpdate properties.
        /// </summary>
        /// <typeparam name="TNewTween">Type of new tween</typeparam>
        /// <param name="tween">New tween</param>
        /// <returns>New tween</returns>
        public Tween<TNewTween> ContinueWith<TNewTween>(Tween<TNewTween> tween) where TNewTween : struct
        {
            tween.Key = Key;

#if IS_UNITY

            tween.GameObject = GameObject;
            tween.Renderer = Renderer;
            tween.ForceUpdate = ForceUpdate;

#endif

            continueWith = tween;
            return tween;
        }

        /// <summary>
        /// Helper that uses the current time, duration, and delegates to update the current value.
        /// </summary>
        private void UpdateValue()
        {

#if IS_UNITY

            if (Renderer == null || Renderer.isVisible || ForceUpdate)
            {

#endif

                LinearProgress = currentTime / duration;
                CurrentProgress = scaleFunc(currentTime / duration);
                previewValue = value;
                value = lerpFunc(this, start, end, CurrentProgress);
                if (progressCallback != null)
                {
                    progressCallback.Invoke(this);
                }

#if IS_UNITY

            }

#endif

        }
    }
}