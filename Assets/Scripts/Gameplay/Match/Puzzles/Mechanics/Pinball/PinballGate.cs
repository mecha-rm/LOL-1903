using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RM_EM
{
    // The gate for the pinball game.
    public class PinballGate : MonoBehaviour
    {
        // The match manager.
        public MatchManager matchManager;

        // The pinball mechanic.
        public PinballMechanic mechanic;

        // The door of the gate.
        public GameObject door;

        // Gets set to 'true' if the gate is open.
        public bool openedGate = false;

        // The max weight that can be handled before the gate opens.
        public float maxWeight = 100.0F;

        // The timer for how long the gate stays open for.
        public float openTimer = 0.0F;

        // The maximum time for the gate to be open.
        public float openTimerMax = 10.0F;

        // The balls touching the gate.
        public List<BallValue> touchingBalls = new List<BallValue>();

        // Start is called before the first frame update
        void Start()
        {
            // Sets the manager instance.
            if (matchManager == null)
                matchManager = MatchManager.Instance;
        }
        
        // OnCollisionEnter2D
        private void OnCollisionEnter2D(Collision2D collision)
        {
            BallValue ball;

            // Tries to get the ball component.
            if(collision.gameObject.TryGetComponent<BallValue>(out ball))
            {
                // Add to the list.
                if(!touchingBalls.Contains(ball))
                    touchingBalls.Add(ball);
            }
        }

        // OnCollisionExit2D
        private void OnCollisionExit2D(Collision2D collision)
        {
            BallValue ball;

            // Tries to get the ball component.
            if (collision.gameObject.TryGetComponent<BallValue>(out ball))
            {
                // Remove from the list.
                if (touchingBalls.Contains(ball))
                    touchingBalls.Remove(ball);
            }
        }

        // OnMouseDown
        private void OnMouseDown()
        {
            // If the pinball gate has been clicked on, change the gate.
            // TODO: I think I only allow the player to open the gate. So change that.
            if(openedGate)
            {
                CloseGate();
            }
            else
            {
                OpenGate();
            }
        }

        // Checks if the pinball gate is open.
        public bool IsOpen()
        {
            return openedGate;
        }

        // TODO: change how the door opens and closes, and allow the player to manually open it.
        // Opens the gate.
        public void OpenGate()
        {
            // Open the gate.
            openedGate = true;
            
            // Set the timer to max.
            ResetOpenTimerToMax();

            // Disables door.
            door.gameObject.SetActive(false);
        }

        // Closes the gate.
        public void CloseGate()
        {
            // Close the gate.
            openedGate = false;

            // Set timer to 0.
            openTimer = 0.0F;

            // Enables door.
            door.gameObject.SetActive(true);
        }

        // Set the timer to max.
        public void ResetOpenTimerToMax()
        {
            openTimer = openTimerMax;
        }

        // Calculates the weight being applied on the gate.
        public float CalculateAppliedWeight()
        {
            // The balls in contact with the gate (direct and in-direct).
            List<BallValue> contactBalls = new List<BallValue>();

            // The sum of the weights.
            float weightSum = 0.0F;

            // Goes through all touching balls.
            for (int i = 0; i < touchingBalls.Count; i++)
            {
                // The current ball.
                BallValue currentBall = touchingBalls[i];

                // If the ball isn't in the list, add it.
                if (!contactBalls.Contains(currentBall))
                    contactBalls.Add(currentBall);

                // TODO: account for the balls touching other balls, but not the gate itself.
                // TODO: there needs to be a better way to do this. Maybe do something with the physics engine?

                
            }
           
            // Sum up the weights of the balls.
            foreach(BallValue ball in contactBalls)
            {
                // Add to the current ball's weight.
                weightSum += ball.GetWeight();
            }

            return weightSum;
        }

        // Update is called once per frame
        void Update()
        {
            // Checks if the manager has been paused.
            if(!matchManager.MatchPaused)
            {
                // The gate is open or closed.
                if(openedGate)
                {
                    // The open timer is going.
                    if (openTimer > 0.0F)
                    {
                        // Reduce timer.
                        openTimer -= Time.deltaTime;

                        // Bounds check for timer.
                        if (openTimer < 0.0F)
                            openTimer = 0.0F;
                    }

                    // Close the gate if the open timer is zero.
                    if (openTimer == 0.0F)
                        CloseGate();
                }
                else
                {
                    // Checks if the weight is too much, causing the gate to open.
                    if (CalculateAppliedWeight() > maxWeight)
                    {
                        OpenGate();
                    }
                }

            }
            
        }
    }
}