//-----------------------------------------------------------------------
// <copyright file="EnsureOdinInspectorDefine.cs" company="Sirenix IVS">
// Copyright (c) Sirenix IVS. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

//#if UNITY_EDITOR

using System;
using System.Linq;
using UnityEditor;
using System.Collections.Generic;
using UnityEngine;

namespace AppCentralCore
{
    public  class EnsureAppCentralDefine
    {

#if UNITY_EDITOR

        public static void EnsureScriptingDefineSymbol(string[] DEFINES, string[] UN_DEFINES)
        {
            var currentTarget = EditorUserBuildSettings.selectedBuildTargetGroup;

            if (currentTarget == BuildTargetGroup.Unknown)
            {
                return;
            }

            var definesString = PlayerSettings.GetScriptingDefineSymbolsForGroup(currentTarget).Trim();
            var defines = definesString.Split(';');

            List<string> defines_List = defines.ToList<string>();

            foreach (var item in UN_DEFINES)
            {
                defines_List.Remove(item);
            }


            foreach (var define in DEFINES)
            {

                if (defines_List.Contains(define) == false)
                {
                    if (definesString.EndsWith(";", StringComparison.InvariantCulture) == false)
                    {
                        definesString += ";";
                    }

                    definesString += define;
                }
            }


            var defines2 = definesString.Split(';');

            List<string> defines_List2 = defines2.ToList<string>();

            String definesString2 = "";

            foreach (var item in UN_DEFINES)
            {
                defines_List2.Remove(item);
            }

            foreach (var item in defines_List2)
            {
                if (definesString2.EndsWith(";", StringComparison.InvariantCulture) == false)
                {
                    definesString2 += ";";
                }

                definesString2 += item;
            }



            PlayerSettings.SetScriptingDefineSymbolsForGroup(currentTarget, definesString2);

            Unity.CodeEditor.CodeEditor.CurrentEditor.SyncAll();

        }

#endif
    }

}

//#endif