using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEditor;
using System;
using System.Linq;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using AppCentralCore;

#if AC_APPLOVIN
using AppLovinMax.Scripts.IntegrationManager.Editor;
#endif

public class AppLovinPreBuilder 
{
    public static AppLovinPreBuilder instance = new AppLovinPreBuilder();
    
    public static AppLovinPreBuilder Instance
    {
        get { return instance; }
    }


#if AC_APPLOVIN
    PluginData pluginData;
#endif
    public void InitializeApplovinPluginData(Action onCompleteCallback)
    {
#if AC_APPLOVIN

        AppLovinEditorCoroutine.StartCoroutine(AppLovinIntegrationManager.Instance.LoadPluginData(data =>
        {
            if (data == null)
            {
                ACLogger.UserDebug("No AppLovin Mediation Network Found");
                onCompleteCallback?.Invoke();
            }
            else
            {
                ACLogger.UserDebug("PluginData Found");
                pluginData = data;


                
                
                onCompleteCallback?.Invoke();
            }
        }));

#endif
    }
    
    
    public void RemoveAppLovInMediationNwtorks()
    {
#if AC_APPLOVIN
        ACLogger.UserDebug("RemoveAppLovInMediationNwtorks");


        if (pluginData == null)
        {
            InitializeApplovinPluginData(RemoveAppLovInMediationNwtorks);
            ACLogger.UserDebug("No AppLovin Mediation Network Found");
        }
        else
        {
            newSetting settings = AppCentralSettings.LoadSetting();

            ACLogger.UserDebug("OnPreprocessBuild");
            ACLogger.UserDebug("settings.UseAppLovin=" + settings.UseAppLovin);
            ACLogger.UserDebug("settings.UseAppCentralSDKPhase2=" + settings.UseAppCentralSDKPhase2);
            
            if (!settings.UseAppLovin || !settings.UseAppCentralSDKPhase2)
            {
                
                ACLogger.UserDebug("Removing All AppLovin Mediation NetWorks");

                foreach (var network in pluginData.MediatedNetworks)
                {
                    EditorUtility.DisplayProgressBar("Integration Manager", "Deleting " + network.Name + "...", 0.5f);

                    var pluginRoot = AppLovinIntegrationManager.MediationSpecificPluginParentDirectory;
                    foreach (var pluginFilePath in network.PluginFilePaths)
                    {
                        FileUtil.DeleteFileOrDirectory(Path.Combine(pluginRoot, pluginFilePath));
                    }

                    AppLovinIntegrationManager.UpdateCurrentVersions(network, pluginRoot);

                    // Refresh UI
                    AssetDatabase.Refresh();
                    EditorUtility.ClearProgressBar();
                }

                ACLogger.UserDebug("Removed All AppLovin Mediation NetWorks");
            }
        }
#endif
    }

    public void RemoveAppLovInMediationNwtorksOneByOne(AppLovinMax.Scripts.IntegrationManager.Editor.Network network)
    {
#if AC_APPLOVIN

                    EditorUtility.DisplayProgressBar("Integration Manager", "Deleting " + network.Name + "...", 0.5f);

                    ACLogger.UserDebug("AppLovinIntegrationManager.MediationSpecificPluginParentDirectory:" + AppLovinIntegrationManager.MediationSpecificPluginParentDirectory) ;
                    var pluginRoot = AppLovinIntegrationManager.MediationSpecificPluginParentDirectory;
                    foreach (var pluginFilePath in network.PluginFilePaths)
                    {
                        ACLogger.UserDebug("Path.Combine:" + Path.Combine(pluginRoot, pluginFilePath)) ;

                        FileUtil.DeleteFileOrDirectory(Path.Combine(pluginRoot, pluginFilePath));
                    }

                    AppLovinIntegrationManager.UpdateCurrentVersions(network, pluginRoot);

                    // Refresh UI
                    AssetDatabase.Refresh();
                    EditorUtility.ClearProgressBar();
                

            
        
#endif
    }



    public void UpdateAllMediationNetworksToDefaultVersions( List<MaxAdapter> maxAdapters)
    {
#if AC_APPLOVIN
        {
            AppLovinEditorCoroutine.StartCoroutine(UpdateAllMediationNetworksToDefaultVersionsRoutine(maxAdapters));
        }
#endif 
    }
    
    private IEnumerator UpdateAllMediationNetworksToDefaultVersionsRoutine(List<MaxAdapter> maxAdapters)
    {
#if AC_APPLOVIN
                
        foreach (var adapter in maxAdapters)
        {
            if (adapter.IsEnable)
                yield return AppLovinIntegrationManager.Instance.DownloadPlugin(adapter.network, false);
            else
            {

                string path = Application.dataPath + Path.DirectorySeparatorChar  + adapter.network.PluginFilePaths[0];

                if (Directory.Exists(path))
                {


                    bool HasAnyFiles = false;

                    if (Directory.GetFiles(path).Length > 0)
                        HasAnyFiles = true;

                    ACLogger.UserDebug("HasAnyFiles:" + Directory.GetFiles(path).Length);
                    
                    if (HasAnyFiles)
                    {
                        ACLogger.UserDebug("Mediation adapter removed:" + adapter.network.Name);
                        RemoveAppLovInMediationNwtorksOneByOne(adapter.network);

                    }
                }

            }
 
        }
        

#else

        yield return null;
#endif
    }

    /// <summary>
    /// A helper method for determining if a folder is empty or not.
    /// </summary>
    private static bool IsEmptyRecursive(string path)
    {
        // A folder is empty if it (and all its subdirs) have no files (ignore .meta files)
        return Directory.GetFiles(path).Where(file => !file.EndsWith(".meta")).Count() == 0
            && Directory.GetDirectories(path, "*", SearchOption.AllDirectories).All(IsEmptyRecursive);
    }
    
    private static bool IsEmptyRecursive_File(string path)
    {
        // A folder is empty if it has no files (ignore .meta files)
        return Directory.GetFiles(path).Where(file => !file.EndsWith(".meta")).Count() == 0;
    }

}


#endif
