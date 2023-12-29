using System;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CloseBrowsers
{
    /// <summary>
    /// Monobehaviours (scripts) are added to GameObjects.
    /// For a full list of Messages a Monobehaviour can receive from the game, see https://docs.unity3d.com/ScriptReference/MonoBehaviour.html.
    /// </summary>
    public class CloseBrowsersController : MonoBehaviour
    {
        public static CloseBrowsersController Instance { get; private set; }

        // These methods are automatically called by Unity, you should remove any you aren't using.
        #region Monobehaviour Messages
        /// <summary>
        /// Only ever called once, mainly used to initialize variables.
        /// </summary>
        private void Awake()
        {
            // For this particular MonoBehaviour, we only want one instance to exist at any time, so store a reference to it in a static property
            //   and destroy any that are created while one already exists.
            if (Instance != null)
            {
                Plugin.Log?.Warn($"Instance of {GetType().Name} already exists, destroying.");
                GameObject.DestroyImmediate(this);
                return;
            }
            GameObject.DontDestroyOnLoad(this); // Don't destroy this object on scene changes
            Instance = this;
            Plugin.Log?.Debug($"{name}: Awake()");
            SceneManager.activeSceneChanged += CloseBrowsers;
        }

        private void CloseBrowsers(Scene pre, Scene next)
        {
            if (!PluginConfig.Instance.Enable) return;

            if (next.name == "GameCore")
            {
                Plugin.Log.Debug($"{pre.name}=>GameCore");

                Process[] chromeInstances = Process.GetProcessesByName("chrome");
                foreach (Process p in chromeInstances)
                {
                    if (!p.HasExited)
                    {
                        p.CloseMainWindow();
                        if (!p.HasExited)
                        {
                            p.Kill();
                        }
                    }
                }

                Process[] iExploerInstances = Process.GetProcessesByName("iexplore");
                foreach (Process p in iExploerInstances)
                {
                    if (!p.HasExited)
                    {
                        p.CloseMainWindow();
                        if (!p.HasExited)
                        {
                            p.Kill();
                        }
                    }
                }

                Process[] msEdgeInstances = Process.GetProcessesByName("msedge");
                foreach (Process p in msEdgeInstances)
                {
                    if (!p.HasExited)
                    {
                        p.CloseMainWindow();
                        if (!p.HasExited)
                        {
                            p.Kill();
                        }
                    }
                }

                Process[] firefoxInstances = Process.GetProcessesByName("firefox");
                foreach (Process p in firefoxInstances)
                {
                    if (!p.HasExited)
                    {
                        p.CloseMainWindow();
                        if (!p.HasExited)
                        {
                            p.Kill();
                        }
                    }
                }

                Plugin.Log.Debug("Finish Closing Browsers");
            }
        }

        /// <summary>
        /// Only ever called once on the first frame the script is Enabled. Start is called after any other script's Awake() and before Update().
        /// </summary>
        private void Start()
        {


        }


        /// <summary>
        /// Called when the script is being destroyed.
        /// </summary>
        private void OnDestroy()
        {
            Plugin.Log?.Debug($"{name}: OnDestroy()");
            SceneManager.activeSceneChanged -= CloseBrowsers;
            if (Instance == this)
                Instance = null; // This MonoBehaviour is being destroyed, so set the static instance property to null.
        }
        #endregion
    }
}
