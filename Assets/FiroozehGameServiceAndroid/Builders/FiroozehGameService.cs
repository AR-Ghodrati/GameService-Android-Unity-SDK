﻿// <copyright file="FiroozehGameService.cs" company="Firoozeh Technology LTD">
// Copyright (C) 2019 Firoozeh Technology LTD. All Rights Reserved.
//
//  Licensed under the Apache License, Version 2.0 (the "License");
//  you may not use this file except in compliance with the License.
//  You may obtain a copy of the License at
//
//  http://www.apache.org/licenses/LICENSE-2.0
//
//  Unless required by applicable law or agreed to in writing, software
//  distributed under the License is distributed on an "AS IS" BASIS,
//  WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//  See the License for the specific language governing permissions and
//    limitations under the License.
// </copyright>



using System;
using FiroozehGameServiceAndroid.Builders.App;
using FiroozehGameServiceAndroid.Builders.Native;
using FiroozehGameServiceAndroid.Core;
using FiroozehGameServiceAndroid.Core.Native;
using FiroozehGameServiceAndroid.Enums;
using FiroozehGameServiceAndroid.Utils;
/**
* @author Alireza Ghodrati
*/

namespace FiroozehGameServiceAndroid.Builders
{
    
    #if UNITY_ANDROID
    public sealed class FiroozehGameService
    {

        public static GameService Instance;
        public static bool IsReady;


        private  static Action<string> _errorAction;
        public   static GameServiceClientConfiguration Configuration;
        private  const string Tag = "FiroozehGameService";


        public static void ConfigurationInstance(GameServiceClientConfiguration configuration)
        {  
            Configuration = configuration;     
        }

        public static void Run(Action<string> onError)
        {
            LogUtil.InitialLogger(Configuration);
            _errorAction = onError;

            
            if (Instance != null)
            {
                LogUtil.LogWarning(Tag,"GameService Initialized Before , Do Nothing..");
                return;
            }

            switch (Configuration.InstanceType)
            {
             
                case InstanceType.Native:
                    GameServiceNativeInitializer.Init(Configuration,OnSuccessInit,OnErrorInit);
                    break;
                case InstanceType.Auto:
                    GameServiceAppInitializer.Init(Configuration,OnSuccessInit,OnErrorInit);       
                    break;
                default:
                    LogUtil.LogError(Tag,"Invalid Instance Type , Auto Type Selected...");
                    GameServiceAppInitializer.Init(Configuration,OnSuccessInit,OnErrorInit);       
                    break;
            }
             
         
        }

        private static void OnSuccessInit(GameService gameService)
        {
            //Instance = (GameService) gameService;
            IsReady = true;
            LogUtil.LogDebug(Tag,"GameService Is Ready To Use!");
        }
        
        private static void OnErrorInit(string error)
        {
            // Switch To Native Mode
            if (error.Equals(ErrorList.GameServiceInstallDialogDismiss)
                || error.Equals(ErrorList.GameServiceUpdateDialogDismiss)
                || error.Equals(ErrorList.GameServiceNotInstalled))
            {
                // Native Mode Call
                GameServiceNativeInitializer.Init(Configuration,OnSuccessInit,OnErrorInit);
            }
            else
            _errorAction.Invoke(error);
        }

      
    }
    #endif
}
