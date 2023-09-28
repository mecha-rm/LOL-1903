using LoLSDK;
using RM_CCC;
using SimpleJSON;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace RM_CCC
{
    // The save system for the game.
    [System.Serializable]
    public class CCC_GameData
    {
        // Shows if the game data is valid.
        public bool valid = false;
    }

    // Used to save the game.
    public class SaveSystem : MonoBehaviour
    {
        // The game data.
        // The last game save. This is only for testing purposes.
        public CCC_GameData lastSave;

        // The data that was loaded.
        public CCC_GameData loadedData;

        // The manager for the game.
        public GameplayManager gameManager;

        // LOL - AutoSave //
        // Added from the ExampleCookingGame. Used for feedback from autosaves.
        WaitForSeconds feedbackTimer = new WaitForSeconds(2);
        Coroutine feedbackMethod;
        public TMP_Text feedbackText;

        // The string shown when having feedback.
        private string feedbackString = "Saving Data";

        // The string key for the feedback.
        private const string FEEDBACK_STRING_KEY = "sve_msg_savingGame";

        // Start is called before the first frame update
        void Start()
        {
            // Sets the save result to the instance.
            LOLSDK.Instance.SaveResultReceived += OnSaveResult;

            // Gets the language definition.
            JSONNode defs = SharedState.LanguageDefs;

            // Sets the save complete text.
            if (defs != null)
                feedbackString = defs[FEEDBACK_STRING_KEY];
        }

        // Set save and load operations.
        public void Initialize(Button newGameButton, Button continueButton)
        {
            // Makes the continue button disappear if there is no data to load. 
            Helper.StateButtonInitialize<CCC_GameData>(newGameButton, continueButton, OnLoadData);
        }

        // Checks if the game manager has been set.
        private bool IsGameManagerSet()
        {
            if (gameManager == null)
                gameManager = FindObjectOfType<GameplayManager>(true);

            // Game manager does not exist.
            if (gameManager == null)
            {
                Debug.LogWarning("The Game Manager couldn't be found.");
                return false;
            }

            return true;
        }

        // Sets the last bit of saved data to the loaded data object.
        public void SetLastSaveAsLoadedData()
        {
            loadedData = lastSave;
        }

        // Clears out the last save and the loaded data object.
        public void ClearLoadedAndLastSaveData()
        {
            lastSave = null;
            loadedData = null;
        }

        // Saves data.
        public bool SaveGame()
        {
            // The game manager does not exist if false.
            if (!IsGameManagerSet())
            {
                Debug.LogWarning("The Game Manager couldn't be found.");
                return false;
            }

            // Determines if saving wa a success.
            bool success = false;

            // Generates the save data.
            // TODO: add
            // CCC_GameData savedData = gameManager.GenerateSaveData();
            CCC_GameData savedData = null;

            // Stores the most recent save.
            lastSave = savedData;

            // If the instance has been initialized.
            if (LOLSDK.Instance.IsInitialized)
            {
                // Makes sure that the feedback string is set.
                if (FEEDBACK_STRING_KEY != string.Empty)
                {
                    // Gets the language definition.
                    JSONNode defs = SharedState.LanguageDefs;

                    // Sets the feedback string if it wasn't already set.
                    if (feedbackString != defs[FEEDBACK_STRING_KEY])
                        feedbackString = defs[FEEDBACK_STRING_KEY];
                }


                // Send the save state.
                LOLSDK.Instance.SaveState(savedData);
                success = true;
            }
            else // Not initialized.
            {
                Debug.LogError("The SDK has not been initialized. Improper save made.");
                success = false;
            }

            return success;
        }

        // Called for saving the result.
        private void OnSaveResult(bool success)
        {
            if (!success)
            {
                Debug.LogWarning("Saving not successful");
                return;
            }

            if (feedbackMethod != null)
                StopCoroutine(feedbackMethod);



            // ...Auto Saving Complete
            feedbackMethod = StartCoroutine(Feedback(feedbackString));
        }

        // Feedback while result is saving.
        IEnumerator Feedback(string text)
        {
            // Only updates the text that the feedback text was set.
            if (feedbackText != null)
                feedbackText.text = text;

            yield return feedbackTimer;

            // Only updates the content if the feedback text has been set.
            if (feedbackText != null)
                feedbackText.text = string.Empty;

            // nullifies the feedback method.
            feedbackMethod = null;
        }

        // Checks if the game has loaded data.
        public bool HasLoadedData()
        {
            // Used to see if the data is available.
            bool result;

            // Checks to see if the data exists.
            if (loadedData != null) // Exists.
            {
                // Checks to see if the data is valid.
                result = loadedData.valid;
            }
            else // No data.
            {
                // Not readable.
                result = false;
            }

            // Returns the result.
            return result;
        }

        // Removes the loaded data.
        public void ClearLoadedData()
        {
            loadedData = null;
        }

        // The gameplay manager now checks if there is loadedData. If so, then it will load in the data when the game starts.
        // // Loads a saved game. This returns 'false' if there was no data.
        // public bool LoadGame()
        // {
        //     // No loaded data.
        //     if(loadedData == null)
        //     {
        //         Debug.LogWarning("There is no saved game.");
        //         return false;
        //     }
        // 
        //     // TODO: load the game data.
        // 
        //     return true;
        // }

        // Called to load data from the server.
        private void OnLoadData(CCC_GameData loadedGameData)
        {
            // Overrides serialized state data or continues with editor serialized values.
            if (loadedGameData != null)
            {
                loadedData = loadedGameData;
            }
            else // No game data found.
            {
                Debug.LogError("No game data found.");
                loadedData = null;
                return;
            }

            // TODO: save data for game loading.
            if (!IsGameManagerSet())
            {
                Debug.LogError("Game gameManager not found.");
                return;
            }

            // TODO: this automatically loads the game if the continue button is pressed.
            // If there is no data to load, the button is gone. 
            // You should move the buttons around to accomidate for this.
            // LoadGame();
        }


    }
}