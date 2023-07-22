using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AppCentralCore;

namespace AppCentralAPI
{
    public abstract class OnThemeChangeBase : MonoBehaviour
    {
        protected void OnEnable()
        {
            AppCentralThemeSwitchController.OnInitializeThemePerms += ThemeChangeEventReceiver;
        }

        public abstract void ThemeChangeEventReceiver();

        protected void OnDisable()
        {
            AppCentralThemeSwitchController.OnInitializeThemePerms -= ThemeChangeEventReceiver;
        }
    }
}
