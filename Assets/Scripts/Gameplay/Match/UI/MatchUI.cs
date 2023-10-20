using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using util;
using UnityEngine.UI;

namespace RM_EM
{
    // The UI for matches. All UI elements that call functions should use this script.
    public class MatchUI : GameplayUI
    {
        [Header("Match")]

        // The match manager.
        public MatchManager matchManager;

        // The time for the match UI.
        public TMP_Text timerText;

        // The UI contnet shown when the match ends.
        public GameObject matchEnd;

        [Header("Match/Players")]

        // Player 1's UI
        public PlayerMatchUI p1UI;

        // Player 2's UI
        public PlayerMatchUI p2UI;

        // Start is called before the first frame update
        protected override void Start()
        {
            base.Start();

            // Grabs the instance.
            if (matchManager == null)
                matchManager = MatchManager.Instance;

            // Hides the match end.
            HideMatchEnd();
        }

        // INTERFACE UPDATES //

        // Updates the timer displayed.
        public void UpdateTimerText()
        {
            timerText.text = GameplayManager.GetTimeFormatted(matchManager.matchTime);
        }

        // Updates all player displays.
        public void UpdateAllPlayerUI()
        {
            // Seperate into different functions.

            // Player 1
            UpdatePlayer1EquationDisplay();
            UpdatePlayer1PointsBar();
            UpdatePlayer1PowerBarFill();
            UpdatePlayer1PowerBarColor();

            // Player 2
            UpdatePlayer2EquationDisplay();
            UpdatePlayer2PointsBar();
            UpdatePlayer2PowerBarFill();
            UpdatePlayer2PowerBarColor();
        }

        // Updates UI that changes when an equation is compelted.
        public bool UpdateOnEquationCompleteUI(PlayerMatch player)
        {
            // The variable that checks the update.
            bool updated = false;

            // Updates the displays.
            if (player == matchManager.p1)
            {
                UpdatePlayer1EquationDisplay();
                UpdatePlayer1PointsBar();
                UpdatePlayer1PowerBarFill();
                updated = true;
            }
            else if (player == matchManager.p2)
            {
                UpdatePlayer2EquationDisplay();
                UpdatePlayer2PointsBar();
                UpdatePlayer2PowerBarFill();
                updated = true;
            }

            return updated;
        }

        // EQUATION DISPLAYS
        // Updates the player 1 equation.
        public void UpdatePlayer1EquationDisplay()
        {
            p1UI.UpdatePlayerEquationDisplay();
        }

        // Updates the player 2 equation.
        public void UpdatePlayer2EquationDisplay()
        {
            p2UI.UpdatePlayerEquationDisplay();
        }


        // PROGRESS BARS //

        // Updates player 1's points bar.
        public void UpdatePlayer1PointsBar()
        {
            p1UI.UpdatePlayerPointsBar();
        }

        // Updates player 2's points bar.
        public void UpdatePlayer2PointsBar()
        {
            p2UI.UpdatePlayerPointsBar();
        }

        // POWERS //

        // Updates player 1's power bar.
        public void UpdatePlayer1PowerBarFill()
        {
            p1UI.UpdatePlayerPowerBarFill();
        }

        // Update's player 2's power bar.
        public void UpdatePlayer2PowerBarFill()
        {
            p2UI.UpdatePlayerPowerBarFill();
        }

        // Updates player 1's power bar color.
        public void UpdatePlayer1PowerBarColor()
        {
            p1UI.UpdatePlayerPowerBarColor();
        }

        // Updates player 2's power bar color.
        public void UpdatePlayer2PowerBarColor()
        {
            p2UI.UpdatePlayerPowerBarColor();
        }

        // OPERATIONS
        // Use player 1's power.
        public void UsePlayer1Power()
        {
            matchManager.p1.UsePower();
        }

        // Use player 2's power.
        public void UsePlayer2Power()
        {
            matchManager.p2.UsePower();
        }

        // Skips
        // Player 1 Skip
        public void UsePlayer1EquationSkip()
        {
            matchManager.p1.SkipEquation();
        }

        // Player 2 Skip
        public void UsePlayer2EquationSkip()
        {
            matchManager.p2.SkipEquation();
        }



        // WINDOWS //

        // Shows the match end.
        public void ShowMatchEnd()
        {
            matchEnd.SetActive(true);
        }

        // Hides the match end.
        public void HideMatchEnd()
        {
            matchEnd.SetActive(false);
        }

        // MATCH END //
        // Return to the game world.
        public void ToWorldScene()
        {
            matchManager.ToWorldScene();
        }
    }
}