using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RM_EM
{
    // The bubble mechanic.
    public class BubbleMechanic : SpawnMechanic
    {
        [Header("Bubble")]

        // The center of the bubble area.
        public GameObject areaCenter;

        // The width of the area the bubble is spawned into.
        public float areaWidth = 10;

        // The heighto f the area the bubble is spawned into.
        public float areaHeight = 10;

        [Header("Bubble/Bubbles")]
        // The bubble pool.
        public Queue<BubbleValue> bubblePool;

        // The bubble prefab.
        public BubbleValue bubblePrefab;

        // The value sprites for the bubble.
        public PuzzleValueSprites valueSprites;

        // The force applied to bubbles upon being created.
        public float bubbleMoveForce = 10.0F;

        // The life time of spawned bubbles.
        public float bubbleLifeTime = 10.0F;

        // Start is called before the first frame update
        protected override void Start()
        {
            base.Start();
        }

        // Generates a bubble.
        public BubbleValue GenerateBubble(char value, Vector3 spawnPos, Vector3 forceDirec)
        {
            // The bubble being generated.
            BubbleValue bubble;

            // Checks if there are bubbles in the pool.
            if (bubblePool.Count == 0)
            {
                // The new bubble, which is generated from a prefab.
                bubble = Instantiate(bubblePrefab);
            }
            else // No bubbles, so make a new one.
            {
                bubble = bubblePool.Dequeue();
            }

            // Set the bubble value and changes the sprite.
            bubble.puzzleValue.SetValueAndSprite(value, valueSprites);

            // Set position.
            bubble.transform.position = spawnPos;
            
            // Set force values.
            bubble.rigidbody.velocity = Vector2.zero;
            bubble.forceDirec = forceDirec;
            bubble.moveForce = bubbleMoveForce;

            // Set life time.
            bubble.lifeTimerMax = bubbleLifeTime;
            bubble.SetLifeTimerToMax();

            // Other
            // Add the bubble to the values lit.
            puzzleValues.Add(bubble.puzzleValue);
            
            // Return the bubble.
            return bubble;
        }

        // Returns a bubble to the pool.
        public void ReturnBubble(BubbleValue bubble)
        {
            // Reset the velocity.
            bubble.rigidbody.velocity = Vector2.zero;

            // Reset the life time.
            bubble.SetLifeTimerToMax();

            // Turn off the bubble object.
            bubble.gameObject.SetActive(false);

            // Remove from the list.
            puzzleValues.Remove(bubble.puzzleValue);
            
            // Put bubble in the queue.
            bubblePool.Enqueue(bubble);
        }

        // Updates the mechanic.
        public override void UpdateMechanic()
        {
            // Updates the spawn timer.
            bool spawn = UpdateSpawnTimer();

            // Checks if the values have been maxed out.
            bool maxedValues = HasPuzzleValuesCountReachedMax(); 

            // Checks if a bubble should be generated.
            bool generate = spawn && !maxedValues;

            // If a bubble should be generated.
            if(generate)
            {
                // Positioning
                // The top left of the area.
                Vector3 areaTopLeft = areaCenter.transform.position -
                    new Vector3(areaWidth / 2.0F, areaHeight / 2.0F, 0.0F);

                // Calculates a corner offset by getting a random value within the width and the height.
                Vector3 cornerOffset = new Vector3(
                    Random.Range(0, areaWidth),
                    Random.Range(0, areaHeight),
                    0.0F
                    );

                // Calculates the spawn position for teh bubble.
                Vector3 spawnPos = areaTopLeft + cornerOffset;

                // Sets the bubble force direction to be random.
                // The direction is also normalized to make sure its unit length.
                Vector3 forceDirec = util.CustomMath.Rotate(Vector2.right, Random.Range(0, 360), true);
                forceDirec.Normalize();


                // Generates the bubble.
                GenerateBubble(puzzle.GetRandomPuzzleValue(), spawnPos, forceDirec);

                // Resets the spawn timer.
                ResetSpawnTimerToMax();
            }
        }

        // Update is called once per frame
        protected override void Update()
        {
            base.Update();
        }
    }
}