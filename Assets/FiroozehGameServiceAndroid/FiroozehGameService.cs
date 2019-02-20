﻿using System.Collections.Generic;
using FiroozehGameServiceAndroid.Core;
using FiroozehGameServiceAndroid.Interfaces;
using FiroozehGameServiceAndroid.Models;
using Newtonsoft.Json;
using UnityEngine;

namespace FiroozehGameServiceAndroid
{
    public sealed class FiroozehGameService {

        private readonly AndroidJavaObject _gameServiceObj;
        private readonly bool _haveNotification;

#if UNITY_ANDROID
        public FiroozehGameService(AndroidJavaObject gameService, bool haveNotification)
        {
            if (gameService != null)
            {
                _haveNotification = haveNotification;
                _gameServiceObj = gameService;
            }
            else throw new GameServiceException("GameServiceObj Is NULL");
        }

#endif

#if UNITY_ANDROID

        public void GetAchievements(DelegateCore.OnGetAchievement callback,DelegateCore.OnError error)
        {
            if (_gameServiceObj != null)
            {
                _gameServiceObj.Call("GetAchievement", new GameServiceCallback(Oncallback =>
                    {
                        callback.Invoke(JsonConvert.DeserializeObject<List<Achievement>>(Oncallback));
                 
                    }
                    , error.Invoke));
            }
        }

#endif

#if UNITY_ANDROID
        public void UnlockAchievement(string achievementId, 
            DelegateCore.OnUnlockAchievement callback,
            DelegateCore.OnError error)
        {
            if (_gameServiceObj != null)
            {
                _gameServiceObj.Call("UnlockAchievement", achievementId,_haveNotification, new GameServiceCallback(Oncallback => {
                        callback.Invoke(JsonConvert.DeserializeObject<Achievement>(Oncallback));
                    }
                    , error.Invoke));
            }
        }

#endif
#if UNITY_ANDROID
        public void ShowAchievementsUI(DelegateCore.OnError error)
        {
            if (_gameServiceObj != null)
            {
                _gameServiceObj.Call("ShowAchievementUI", new GameServiceCallback(Oncallback => { }
                    , error.Invoke));
            }
        }
#endif
#if UNITY_ANDROID

        public void ShowLeaderBoardsUI(DelegateCore.OnError error)
        {
            if (_gameServiceObj != null)
            {
                _gameServiceObj.Call("ShowLeaderBoardUI", new GameServiceCallback(Oncallback => { }

                    , error.Invoke));
            }
        }
#endif
#if UNITY_ANDROID
        public void GetLeaderBoards(DelegateCore.OnGetLeaderBoards callback, DelegateCore.OnError error)
        {
            if (_gameServiceObj != null)
            {
                _gameServiceObj.Call("GetLeaderBoards", new GameServiceCallback(Oncallback => {
                        callback.Invoke(JsonConvert.DeserializeObject<List<LeaderBoard>>(Oncallback));
                    }
                    , error.Invoke));
            }
        }
#endif
#if UNITY_ANDROID
        public void GetLeaderBoardDetails(string leaderBoardId,
            DelegateCore.OnGetLeaderBoardDetails callback,
            DelegateCore.OnError error)

        {
            if (_gameServiceObj != null)
            {
                _gameServiceObj.Call("GetLeaderBoardData", leaderBoardId, new GameServiceCallback(oncallback => {
                        callback.Invoke(JsonConvert.DeserializeObject<LeaderBoardDetails>(oncallback));
                    }, error.Invoke));
            }
        }
#endif
#if UNITY_ANDROID
        //TODO CHECK IT
        public void SubmitScore(string scoreId
            ,int scoreValue,
            DelegateCore.OnCallback callback,
            DelegateCore.OnError error)

        {
            if (_gameServiceObj != null)
            {
                _gameServiceObj.Call("SubmitScore", scoreId, scoreValue,_haveNotification,new GameServiceCallback(callback.Invoke, error.Invoke));
            }
        }
#endif
       
#if UNITY_ANDROID
        public void SaveGame(string saveGameName
            ,string saveGameDescription
            ,string saveGameCover
            ,string saveGameData
            , DelegateCore.OnCallback callback
            , DelegateCore.OnError error)
        {
            if (_gameServiceObj != null)
            {
                _gameServiceObj.Call("SaveData", saveGameName, saveGameDescription, saveGameCover, saveGameData, new GameServiceCallback(callback.Invoke, error.Invoke));
            }
        }
#endif
#if UNITY_ANDROID
        public void GetSaveGame(DelegateCore.OnCallback saveGameData, DelegateCore.OnError error)
        {
            if (_gameServiceObj != null)
            {
                _gameServiceObj.Call("GetLastSave", new GameServiceCallback(saveGameData.Invoke, error.Invoke));
            }
        }
#endif
#if UNITY_ANDROID
        public void DisposeService()
        {
            if (_gameServiceObj != null)
            {
                _gameServiceObj.Call("DisposeService");
            }
        }

#endif
        
#if UNITY_ANDROID
        public void GetSDKVersion(DelegateCore.OnCallback version, DelegateCore.OnError error)
        {
            if (_gameServiceObj != null)
            {
                _gameServiceObj.Call("GetSDKVersion", new GameServiceCallback(version.Invoke, error.Invoke));
            }
        }

#endif



    }
}
