using BeatSaberMarkupLanguage.Attributes;
using BeatSaberMarkupLanguage.Util;

namespace CloseBrowsers
{
    public class SettingController:PersistentSingleton<SettingController>
    {
        [UIValue("Enable")]
        public bool Enable
        {
            get => PluginConfig.Instance.Enable;
            set => PluginConfig.Instance.Enable = value;
        }
    }
}
