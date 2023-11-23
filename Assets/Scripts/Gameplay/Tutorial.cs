using SimpleJSON;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace RM_EM
{
    // The tutorial class. This is a singleton that gets gets consulted for tutorial text.
    public class Tutorial : MonoBehaviour
    {
        [System.Serializable]
        public struct TutorialData
        {
            // Bools for clearing certain tutorials.
            public bool clearedOpening;
            public bool clearedFirstMatch;
            public bool clearedFirstMatchLoss;
            public bool clearedPostFirstMatch;

            public bool clearedFirstPower;
            public bool clearedFinalMatch;
            public bool clearedGameOver;

            // Exponents
            public bool clearedExponentBasics;
            public bool clearedProduct;
            public bool clearedPowerOfAPower;
            public bool clearedProductOfAProduct;

            public bool clearedZero;
            public bool clearedNegative;
        }

        // The singleton instance.
        private static Tutorial instance;

        // Gets set to 'true' when the singleton has been instanced.
        // This isn't needed, but it helps with the clarity.
        private static bool instanced = false;

        // The definitions from the json file.
        private JSONNode defs;

        [Header("Triggers")]
        
        // TODO: maybe seperate them based on the tutorial type (world, match). Maybe re-arrange the function.
        // Bools for clearing certain tutorials.
        public bool clearedOpening = false;
        public bool clearedFirstMatch = false;
        public bool clearedFirstMatchLoss = false;
        public bool clearedPostFirstMatch = false;

        public bool clearedFirstPower = false;
        public bool clearedFinalMatch = false;
        public bool clearedGameOver = false;

        [Header("Triggers/Exponents")]
        public bool clearedExponentBasics = false;
        public bool clearedProduct = false;
        public bool clearedPowerOfAPower = false;
        public bool clearedProductOfAProduct = false;

        public bool clearedZero = false;
        public bool clearedNegative = false;

        // Constructor
        private Tutorial()
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
                return;
            }

            // Run code for initialization.
            if (!instanced)
            {
                instanced = true;

                // Loads the language definitions.
                defs = SharedState.LanguageDefs;
            }
        }

        // Start is called before the first frame update
        void Start()
        {
            // Don't destroy this game object on load.
            DontDestroyOnLoad(gameObject);
        }

        // Gets the instance.
        public static Tutorial Instance
        {
            get
            {
                // Checks if the instance exists.
                if (instance == null)
                {
                    // Tries to find the instance.
                    instance = FindObjectOfType<Tutorial>(true);


                    // The instance doesn't already exist.
                    if (instance == null)
                    {
                        // Generate the instance.
                        GameObject go = new GameObject("Tutorial (singleton)");
                        instance = go.AddComponent<Tutorial>();
                    }

                }

                // Return the instance.
                return instance;
            }
        }

        // Returns 'true' if the object has been initialized.
        public static bool Instantiated
        {
            get
            {
                return instanced;
            }
        }

        // Gets the test pages.
        public List<Page> GetTestPages()
        {
            // The test pages.
            List<Page> pages = new List<Page>()
            {
                new Page("This is a test."),
                new Page("This is only a test.")
            };

            // Returns the pages.
            return pages;
        }

        // The opening tutorial.
        public List<Page> GetOpeningTutorial()
        {
            // Pages
            List<Page> pages = new List<Page>();

            // Loads the pages
            if (defs != null) // Translation
            {
                pages.Add(new Page(defs["trl_opening_00"], "trl_opening_00"));
            }
            else
            {
                pages.Add(new Page("Welcome to the exponent club! As the name suggests, we play the exponent game here. Do you want to play? Oh, you�ve never played the game before? I guess the club will have to teach you then! Challenge me to a match, and I�ll show you the ropes!"));
            }
            

            clearedOpening = true;

            return pages;
        }

        // The exponent basics tutorial.
        public List<Page> GetExponentBasicsTutorial()
        {
            // The test pages.
            List<Page> pages = new List<Page>()
            {
                new Page("Time to teach you some exponent basics. Exponents are math operations where you multiply a value by itself a certain number of times, with said number being determined by the exponent. Exponent operations have multiple rules, which will be explained later."),
            };

            clearedExponentBasics = true;

            return GetTestPages();
        }

        // The first match tutorial.
        public List<Page> GetFirstMatchTutorial()
        {
            // The test pages.
            List<Page> pages = new List<Page>()
            {
                new Page("The exponent game is pretty symbol. You and your opponent will both be given equations with missing values, which can be numbers or operations. You need to pick the right value from your puzzle board to complete the equation. When the equation is complete, you get points, and move onto the next equation. The player fills their points bar first wins. Good luck!"),
            };

            clearedFirstMatch = true;

            return GetTestPages();
        }

        // The first match loss tutorial.
        public List<Page> GetFirstMatchLossTutorial()
        {
            // The test pages.
            List<Page> pages = new List<Page>()
            {
                new Page("You�ve lost the introductory match. It�s important that you learn this rule before going forward, so please try again."),
            };

            clearedFirstMatchLoss = true;

            return GetTestPages();
        }

        // The first match win/post-first match tutorial.
        public List<Page> GetFirstMatchWinTutorial()
        {
            // The test pages.
            List<Page> pages = new List<Page>()
            {
                new Page("Congratulations on winning the introduction match! Now that you�re familiar with the rules, you�ll have to beat every member of the club! Each member focuses on one or more exponent rules, so you�ll get introduced to each rule one at a time. You�ll be a master at this in no time. Good luck!"),
            };

            clearedPostFirstMatch = true;

            return GetTestPages();
        }

        // The product rule tutorial.
        public List<Page> GetProductRuleTutorial()
        {
            // The test pages.
            List<Page> pages = new List<Page>()
            {
                new Page("This match uses the product rule. When two terms with the same base are multiplied together, it can be simplified to multiplying the base by the sum of the exponents."),
            };

            clearedProduct = true;

            return GetTestPages();
        }

        // The power of a power rule tutorial.
        public List<Page> GetPowerOfAPowerRuleTutorial()
        {
            // The test pages.
            List<Page> pages = new List<Page>()
            {
                new Page("This match uses the power of a power rule. When a term with an exponent is having its result get another exponent applied to it, it can be converted to the base value to a single, combined exponent. The combined exponent is the two exponents from the original expression multiplied together."),
            };

            clearedPowerOfAPower = true;

            return GetTestPages();
        }

        // The product of a product rule tutorial.
        public List<Page> GetPowerOfAProductRuleTutorial()
        {
            // The test pages.
            List<Page> pages = new List<Page>()
            {
                new Page("This match uses the product of a product rule. If two terms with different bases and the same exponents are multiplied, the bases are multiplied together, with their result being put to the power of the shared exponent."),
            };

            clearedProductOfAProduct = true;

            return GetTestPages();
        }

        // The zero exponent rule tutorial.
        public List<Page> GetZeroExponentRuleTutorial()
        {
            // The test pages.
            List<Page> pages = new List<Page>()
            {
                new Page("This match uses the zero exponent rule. Any value to the power of 0 will give the result of 1."),
            };

            clearedZero = true;

            return GetTestPages();
        }

        // The negative exponent rule tutorial.
        public List<Page> GetNegativeExponentRuleTutorial()
        {
            // The test pages.
            List<Page> pages = new List<Page>()
            {
                new Page("This match uses the negative exponent rule. The negative exponent rule dictates that any value to the power of a negative exponent becomes a fraction with a numerator of 1. The value with the exponent applied becomes the denominator."),
            };

            clearedNegative = true;

            return GetTestPages();
        }

        // The first power tutorial.
        public List<Page> GetFirstPowerTutorial()
        {
            // The test pages.
            List<Page> pages = new List<Page>()
            {
                new Page("You just got a match power! A power can be used to give you an edge during a match, but only after building up its energy first. When you defeat a challenger, you also receive the power, which you can equip for future matches. Check the power menu to see what each power does, and select what power you want."),
            };

            clearedFirstPower = true;

            return GetTestPages();
        }

        // The final match tutorial.
        public List<Page> GetFinalMatchTutorial()
        {
            List<Page> pages = new List<Page>()
            {
                new Page("You�ve learned all the exponent rules and defeated all the other club members, so now it�s time for your final challenge: a match against me! Head over to the next room so we can face off!"),
            };

            clearedFinalMatch = true;

            return GetTestPages();
        }

        // The game over tutorial.
        public List<Page> GetGameOverTutorial()
        {
            List<Page> pages = new List<Page>()
            {
                new Page("You got a game over. You can rematch your opponent now, or try to take on any other available challengers. However, you�ll need to beat all the club members to clear this trial, so be ready to come back at some point."),
            };

            clearedGameOver = true;

            return GetTestPages();
        }


        // Generates tutorial data.
        public TutorialData GenerateTutorialData()
        {
            TutorialData data = new TutorialData();

            // Saving the data
            data.clearedOpening = clearedOpening;
            data.clearedFirstMatch = clearedFirstMatch;
            data.clearedFirstMatchLoss = clearedFirstMatchLoss;
            data.clearedPostFirstMatch = clearedPostFirstMatch;

            data.clearedFirstPower = clearedFirstPower;
            data.clearedFinalMatch = clearedFinalMatch;
            data.clearedGameOver = clearedGameOver;

            data.clearedExponentBasics = clearedExponentBasics;
            data.clearedProduct = clearedProduct;
            data.clearedPowerOfAPower = clearedPowerOfAPower;
            data.clearedProductOfAProduct = clearedProductOfAProduct;

            data.clearedZero = clearedZero;
            data.clearedNegative = clearedNegative;

            return data;
    }

        // Loads tutorial data.
        public void LoadTutorialData(TutorialData data)
        {
            // Loading the data
            clearedOpening = data.clearedOpening;
            clearedFirstMatch = data.clearedFirstMatch;
            clearedFirstMatchLoss = data.clearedFirstMatchLoss;
            clearedPostFirstMatch = data.clearedPostFirstMatch;

            clearedFirstPower = data.clearedFirstPower;
            clearedFinalMatch = data.clearedFinalMatch;
            clearedGameOver = data.clearedGameOver;

            clearedExponentBasics = data.clearedExponentBasics;
            clearedProduct = data.clearedProduct;
            clearedPowerOfAPower = data.clearedPowerOfAPower;
            clearedProductOfAProduct = data.clearedProductOfAProduct;

            clearedZero = data.clearedZero;
            clearedNegative = data.clearedNegative;
        }


        // This function is called when the MonoBehaviour will be destroyed.
        private void OnDestroy()
        {
            // If the saved instance is being deleted, set 'instanced' to false.
            if (instance == this)
            {
                instanced = false;
            }
        }
    }
}