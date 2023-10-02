using UnityEngine;
using System.Collections;

//My Collection of useful methods that eases game making process.

namespace EZ_Core
{
    public static class EZ_Math
    {
        /// <summary>
        /// Interpolate between min and max with smoothing at the limits. The interpolation will gradually speed up at the start and slow down towards the end.
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="t"></param>
        /// <returns>Returns current position depending on value of t</returns>
        public static Vector3 SmoothStep(Vector3 from, Vector3 to, float t)
        {
            return new Vector3(Mathf.SmoothStep(from.x, to.x, t),
                                Mathf.SmoothStep(from.y, to.y, t),
                                Mathf.SmoothStep(from.z, to.z, t));
        }

        /// <summary>
        /// Return the angle in degrees created between the x-axis and the vector (to - from)
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns>The angle in degrees</returns>
        public static float GetSignedAngle(Vector2 from, Vector2 to)
        {
            Vector2 targetDirection = to - from;

            return Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;
        }

        /// <summary>
        /// Return the array index chosen from the array of probabilities that is passed as parameter
        /// </summary>
        /// <param name="probs">
        /// Array containing the probabilities
        /// </param>
        /// <returns>
        /// Array index of the chosen one
        /// </returns>
        public static int Choose(float[] probs)
        {
            float total = 0;

            for (int i = 0; i < probs.Length; ++i)
            {
                total += probs[i];
            }

            float randomPoint = Random.value * total;

            for (int i = 0; i < probs.Length; ++i)
            {
                if (randomPoint < probs[i])
                {
                    return i;
                }
                else
                {
                    randomPoint -= probs[i];
                }
            }

            return probs.Length - 1;
        }

        /// <summary>
        /// Return a value between 0 to 1. Depending on the explosion radius, the value reaching 0 when the actor is far from the explosion, 
        /// and 1 when it is very close to the explosion.
        /// </summary>
        /// <param name="actorPosition"></param>
        /// <param name="expPosition"></param>
        /// <param name="expRadius"></param>
        /// <returns>Return a value between 0 to 1</returns>
        public static float GetExplosionRatio(Vector2 actorPosition, Vector2 expPosition, float expRadius)
        {
            var dir = (actorPosition - expPosition);
            float calc = 1 - (dir.magnitude / expRadius);
            if (calc <= 0)
            {
                calc = 0;
            }
            return calc;
        }

        /// <summary>
        /// Return a value between 0 to 1. Depending on the explosion radius, the value reaching 0 when the actor is far from the explosion, 
        /// and 1 when it is very close to the explosion.
        /// </summary>
        /// <param name="actorPosition"></param>
        /// <param name="expPosition"></param>
        /// <param name="expRadius"></param>
        /// <returns>Return a value between 0 to 1</returns>
        public static float GetExplosionRatio(Vector3 actorPosition, Vector3 expPosition, float expRadius)
        {
            var dir = (actorPosition - expPosition);
            float calc = 1 - (dir.magnitude / expRadius);
            if (calc <= 0)
            {
                calc = 0;
            }
            return calc;
        }

        /// <summary>
        /// Apply an explosion force from the center of the explosion to the body. The amount of force is inverse linear, 
        /// which means that the force is greater when the body is close to the explosion, and force is smaller when body is far.
        /// </summary>
        /// <param name="body"></param>
        /// <param name="expForce"></param>
        /// <param name="expPosition"></param>
        /// <param name="expRadius"></param>
        public static void AddExplosionForce(Rigidbody2D body, float expForce, Vector2 expPosition, float expRadius)
        {
            if (body)
            {
                Vector2 position2d = body.transform.position;
                var dir = position2d - expPosition;
                float calc = 1 - (dir.magnitude / expRadius);
                if (calc <= 0)
                {
                    calc = 0;
                }

                body.AddForce(dir.normalized * expForce * calc);
            }
        }

        /// <summary>
        /// Apply an explosion force from the center of the explosion to the body. The amount of force is inverse linear, 
        /// which means that the force is greater when the body is close to the explosion, and force is smaller when body is far.
        /// </summary>
        /// <param name="body"></param>
        /// <param name="expForce"></param>
        /// <param name="expPosition"></param>
        /// <param name="expRadius"></param>
        public static void AddExplosionForce(Rigidbody body, float expForce, Vector3 expPosition, float expRadius)
        {
            if (body)
            {
                Vector3 position = body.transform.position;
                var dir = position - expPosition;
                float calc = 1 - (dir.magnitude / expRadius);
                if (calc <= 0)
                {
                    calc = 0;
                }

                body.AddForce(dir.normalized * expForce * calc);
            }
        }

        /// <summary>
        /// Return the number after being rounded to X multiple.
        /// </summary>
        /// <param name="number"></param>
        /// <param name="multiple"></param>
        /// <returns></returns>
        public static int RoundToNearestXMultiple(int number, int multiple)
        {
            return RoundToNearestXMultiple((float)number, multiple);
        }

        /// <summary>
        /// Return the number after being rounded to X multiple.
        /// </summary>
        /// <param name="number"></param>
        /// <param name="multiple"></param>
        /// <returns></returns>
        public static int RoundToNearestXMultiple(float number, int multiple)
        {
            return Mathf.RoundToInt(number / multiple) * multiple;
        }
    }
}