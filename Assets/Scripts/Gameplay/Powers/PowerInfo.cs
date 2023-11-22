using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RM_EM
{
    // The power prefabs.
    public class PowerInfo : MonoBehaviour
    {
        // The singleton instance.
        private static PowerInfo instance;

        // Gets set to 'true' when the singleton has been instanced.
        // This isn't needed, but it helps with the clarity.
        private bool instanced = false;

        [Header("Prefabs")]

        // The nothing power (for testing purposes only).
        public PowerNothing nothing;

        // Increases the number of points the user gets.
        public PowerPoints pointsPlus;

        // Decreases the number of points the target gets.
        public PowerPoints pointsMinus;

        // Shortens the equation.
        public PowerEquationChange equationShorten;

        // Lengths an equation.
        public PowerEquationChange equationLengthen;

        // Transfers points from one player to another.
        public PowerPointsTransfer pointsTransfer;

        // Blocks the target from gaining more points for a time.
        public PowerPointsBlock pointsBlock;

        // Twists the opponent's render (no use for the player since the AI isn't effected).
        public PowerTwist twist;

        [Header("Symbols")]

        // The default power symbol.
        public Sprite defaultSymbol;

        // The none/nothing symbol.
        public Sprite nothingSymbol;

        // The points plus symbol.
        public Sprite pointsPlusSymbol;

        // The points minus symbol.
        public Sprite pointsMinusSymbol;

        // The equation shorten symbol.
        public Sprite equationShortenSymbol;

        // The equation lengthn symbol.
        public Sprite equationLengthenSymbol;

        // The points transfer symbol.
        public Sprite pointsTransferSymbol;

        // The points block symbol.
        public Sprite pointsBlockSymbol;

        // The render twist symbol.
        public Sprite twistSymbol;

        // Constructor
        private PowerInfo()
        {
            // ...
        }

        // Awake is called when the script is being loaded
        protected virtual void Awake()
        {
            // If the instance hasn't been set, set it to this object.
            if (instance == null)
            {
                instance = this;
            }
            // If the instance isn't this, destroy the game object.
            else if (instance != this)
            {
                Destroy(gameObject);
            }

            // Run code for initialization.
            if (!instanced)
            {
                instanced = true;
            }
        }

        // Gets the instance.
        public static PowerInfo Instance
        {
            get
            {
                // Checks if the instance exists.
                if (instance == null)
                {
                    // Tries to find the instance.
                    instance = FindObjectOfType<PowerInfo>(true);


                    // The instance doesn't already exist.
                    if (instance == null)
                    {
                        // Generate the instance.
                        GameObject go = new GameObject("Power Prefabs (singleton)");
                        instance = go.AddComponent<PowerInfo>();
                    }

                }

                // Return the instance.
                return instance;
            }
        }

        // Returns 'true' if the object has been initialized.
        public bool Instantiated
        {
            get
            {
                return instanced;
            }
        }

        // TODO: combine these functions into one.

        // INFO
        // Gets a power.
        public Power GetPower(Power.powerType powerT)
        {
            // The power object.
            Power powerObject = null;

            switch (powerT)
            {
                default:
                case Power.powerType.none:
                    powerObject = nothing;
                    break;

                case Power.powerType.pointsPlus:
                    powerObject = pointsPlus;
                    break;

                case Power.powerType.pointsMinus:
                    powerObject = pointsMinus;
                    break;

                case Power.powerType.equationShorten:
                    powerObject = equationShorten;
                    break;

                case Power.powerType.equationLengthen:
                    powerObject = equationLengthen;
                    break;

                case Power.powerType.pointsTransfer:
                    powerObject = pointsTransfer;
                    break;

                case Power.powerType.pointsBlock:
                    powerObject = pointsBlock;
                    break;

                case Power.powerType.twist:
                    powerObject = twist;
                    break;
            }

            return powerObject;
        }

        // Gets a power symbol.
        public Sprite GetPowerSymbol(Power.powerType powerT)
        {
            // The power sprite.
            Sprite powerSprite = null;

            switch (powerT)
            {
                default:
                case Power.powerType.none:
                    powerSprite = nothingSymbol;
                    break;

                case Power.powerType.pointsPlus:
                    powerSprite = pointsPlusSymbol;
                    break;

                case Power.powerType.pointsMinus:
                    powerSprite = pointsMinusSymbol;
                    break;

                case Power.powerType.equationShorten:
                    powerSprite = equationShortenSymbol;
                    break;

                case Power.powerType.equationLengthen:
                    powerSprite = equationLengthenSymbol;
                    break;

                case Power.powerType.pointsTransfer:
                    powerSprite = pointsTransferSymbol;
                    break;

                case Power.powerType.pointsBlock:
                    powerSprite = pointsBlockSymbol;
                    break;

                case Power.powerType.twist:
                    powerSprite = twistSymbol;
                    break;
            }

            return powerSprite;
        }

        // Gets the name of the power type name.
        // 'shortHand' determines if the name is the shorthand or the full-name.
        public static string GetPowerTypeName(Power.powerType power, bool shortHand)
        {
            // Sets the name string to be empty.
            string name = string.Empty;

            switch(power)
            {
                case Power.powerType.none:
                    name = "None";
                    break;

                case Power.powerType.pointsPlus:
                    name = "Points Plus";
                    break;

                case Power.powerType.pointsMinus:
                    name = "Points Minus";
                    break;

                case Power.powerType.equationShorten:
                    name = "Question Short";
                    break;

                case Power.powerType.equationLengthen:
                    name = "Question Long";
                    break;

                case Power.powerType.pointsTransfer:
                    name = "Points Transfer";
                    break;

                case Power.powerType.pointsBlock:
                    name = "Points Block";
                    break;

                case Power.powerType.twist:
                    name = "Twist";
                    break;
            }

            return name;
        }

        // Gets the power type description.
        public static string GetPowerTypeDescription(Power.powerType power)
        {
            // Sets the description string to be empty.
            string desc = string.Empty;

            switch (power)
            {
                case Power.powerType.none:
                    desc = "A power that does nothing.";
                    break;
                
                case Power.powerType.pointsPlus:
                    desc = "Increases the user's points gained for correct answers for a time.";
                    break;
                
                case Power.powerType.pointsMinus:
                    desc = "Decreases the opponent's points gained answers for a time.";
                    break;

                case Power.powerType.equationShorten:
                    desc = "Shortens the user's questions, and increases the points they get for correct answers.";
                    break;

                case Power.powerType.equationLengthen:
                    desc = "Increases the length of the opponent's questions, and decreases the points they get for correct answers";
                    break;

                case Power.powerType.pointsTransfer:
                    desc = "Increases user's points, and reduces the opponent's points everytime the user gets a correct answer.";
                    break;

                case Power.powerType.pointsBlock:
                    desc = "Blocks the target from gaining more points until the obstruction is removed.";
                    break;

                case Power.powerType.twist:
                    desc = "A power that flips the opponent's view upside down.";
                    break;
            }

            // TODO: implement translation.

            return desc;
        }
    }
}