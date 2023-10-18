using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RM_EM
{
    // The ball value script.
    public class BallValue : PuzzleValue
    {
        // The pinball mechanic.
        public PinballMechanic mechanic;

        // The bubble value's rigidbody.
        public new Rigidbody2D rigidbody;

        // The balls this ball is touching.
        public List<BallValue> touchingBalls;

        // The weight of the ball.
        public float weight = 1;

        // Start is called before the first frame update
        protected override void Start()
        {
            base.Start();

            // Grabs the rigidbody.
            if (rigidbody == null)
                rigidbody = GetComponent<Rigidbody2D>();
        }

        // Collision Enter 2D
        private void OnCollisionEnter2D(Collision2D collision)
        {
            // Component.
            BallValue ballValue;

            // Tries to get the component.
            if(collision.gameObject.TryGetComponent(out ballValue))
            {
                RegisterBallTouching(ballValue);
            }
        }

        // Collision Exit 2D
        private void OnCollisionExit(Collision collision)
        {
            // Component.
            BallValue ballValue;

            // Tries to get the component.
            if (collision.gameObject.TryGetComponent(out ballValue))
            {
                UnregisterBallTouching(ballValue);
            }
        }

        // Returns the weight of the ball.
        public float GetWeight()
        {
            // return rigidbody.mass;
            return weight;
        }

        // Gets the ball's weight multiplied by it's scale.
        public float GetWeightScaled()
        {
            // Calculates the average scale (ignores z).
            float avgScale = (transform.localScale.x + transform.localScale.y) / 2.0F;

            // Gets the scaled weight.
            float weightScaled = GetWeight() * avgScale;

            // Returns the scaled weight.
            return weightScaled;
        }

        // Registers that the provided ball is touching this ball.
        public void RegisterBallTouching(BallValue ballValue)
        {
            // Checks if it's already in the list.
            if(!touchingBalls.Contains(ballValue))
                touchingBalls.Add(ballValue);
        }

        // Registers that the provided ball is no longer touching this ball.
        public void UnregisterBallTouching(BallValue ballValue)
        {
            // Checks if it's in the list.
            if(touchingBalls.Contains(ballValue))
                touchingBalls.Remove(ballValue);
        }

        // OnHit Function
        public override void OnHit(bool rightAnswer)
        {
            mechanic.ReturnBall(this);
        }

        // Update is called once per frame
        protected override void Update()
        {
            base.Update();

            // If the ball is in the death zone.
            if(mechanic.BallInDeathZone(this))
            {
                // Return the ball.
                mechanic.ReturnBall(this);
            }
        }
    }
}