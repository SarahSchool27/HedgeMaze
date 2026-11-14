using Examples.TrumbusTrace;
using Game.Examples;
using Game.MinigameFramework.Scripts.Framework.Input;
using Game.MinigameFramework.Scripts.Framework.PlayerInfo;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace HedgeMaze {
    public class HedgeMazeGameManager : MonoBehaviour {
        // TIME VARIABLES
        [Header("Time")]
        public float roundDuration = 15;
        [HideInInspector] public float timer = 0;
        [SerializeField] public float textTime = 1;

        // SCORING VARIABLES
        [SerializeField] private GameObject roundOneHedges;
        [SerializeField] private GameObject roundTwoHedges;
        [SerializeField] private GameObject roundThreeHedges;

        [SerializeField] List<HedgeMazePawn> players;
   
        // UI VARIABLES
        [Header("UI")]
        [SerializeField] private GameObject roundOneText;
        [SerializeField] private GameObject roundTwoText;
        [SerializeField] private GameObject roundThreeText;
        [SerializeField] private GameObject finishedText;
        [SerializeField] private TextMeshProUGUI timerText;


        private void Start() {
            StartCoroutine(GameTimer());
        }
        IEnumerator GameTimer() {
            // Intro Animation
            HedgeMazePawn.isPawnInputEnabled = false;
            roundOneText.SetActive(true);
            yield return new WaitForSeconds(textTime);
            roundOneText.SetActive(false);
            HedgeMazePawn.isPawnInputEnabled = true;
            // Timer
            while (timer < roundDuration) {
                timer += Time.deltaTime;
                timerText.text = "" + ((int)(roundDuration - timer));
                yield return null;
            }

            timer = 0f;
            HedgeMazePawn.isPawnInputEnabled = false;
            roundTwoText.SetActive(true);
            roundOneHedges.SetActive(false);
            roundTwoHedges.SetActive(true);
            foreach (HedgeMazePawn pawn in players) {
                pawn.StartRound2();
            }
            yield return new WaitForSeconds(textTime);
            roundTwoText.SetActive(false);
            HedgeMazePawn.isPawnInputEnabled = true;

            // Timer
            timer = 0f;
            while (timer < roundDuration) {
                timer += Time.deltaTime;
                timerText.text = "" + ((int)(roundDuration - timer));
                yield return null;
            }

            HedgeMazePawn.isPawnInputEnabled = false;
            roundThreeText.SetActive(true);
            roundTwoHedges.SetActive(false);
            roundThreeHedges.SetActive(true);
            foreach (HedgeMazePawn pawn in players) {
                pawn.StartRound3();
            }
            yield return new WaitForSeconds(textTime);
            roundThreeText.SetActive(false);
            HedgeMazePawn.isPawnInputEnabled = true;

            // Timer
            timer = 0f;
            while (timer < roundDuration) {
                timer += Time.deltaTime;
                timerText.text = "" + ((int)(roundDuration - timer));
                yield return null;
            }

            HedgeMazePawn.isPawnInputEnabled = false;
            finishedText.SetActive(true);
            yield return new WaitForSeconds(textTime);
            finishedText.SetActive(false);

            StartCoroutine(EndMinigame());
        }

        IEnumerator EndMinigame() {
            List<int> scores = new();
            foreach (HedgeMazePawn pawn in players) {
                scores.Add(pawn.score);
            }
            MinigameManager.Ranking ranking = new();
            ranking.DetermineRankingFromScores(scores);
            // End
            yield return new WaitForSeconds(2);
            MinigameManager.instance.EndMinigame(ranking);
            StopAllCoroutines(); // prevent EndMinigame from being called multiple times
        }
    }
}
