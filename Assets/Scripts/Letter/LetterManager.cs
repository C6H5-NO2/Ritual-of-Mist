﻿using System.Collections.Generic;
using ThisGame.Utils;
using UnityEngine;

namespace ThisGame.Letter {
    // SingletonManager or ScriptableObject ??? 
    public class LetterManager : SingletonManager<LetterManager> {
        public IdSoDict<LetterData> Dict { get; private set; }

        public List<LetterData> ReceivedLetters { get; private set; }

        public void AddReceivedLetter(string nameid) {
            var data = Dict[nameid];
            if(data.Received)
                return;
            data.Received = true;
            data.Read = false;
            ReceivedLetters.Add(data);
        }

        private void PushOnDay(int day) {
            switch(day) {
                case 1:
                    AddReceivedLetter("letter_first");
                    break;
                case 2:
                    AddReceivedLetter("letter_daily_1");
                    break;
                case 3:
                    AddReceivedLetter("letter_daily_2");
                    break;
                case 4:
                    AddReceivedLetter("letter_daily_3");
                    break;
            }
        }

        protected override void Awake() {
            if(Instance == null) {
                var sos = Resources.LoadAll<LetterData>("SOs/LetterData");
                this.Dict = new IdSoDict<LetterData>(sos, true);
                this.ReceivedLetters = new List<LetterData>();
                TimeManager.Instance.OnNewDay += PushOnDay;
            }
            base.Awake();
        }
    }
}