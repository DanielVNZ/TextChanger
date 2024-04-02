// In Mod.cs

using Colossal.Localization;
using Game;
using Game.Modding;
using Game.SceneFlow;
using UnityEngine;
using Colossal.Logging;
using Colossal.IO.AssetDatabase;
using static Colossal.IO.AssetDatabase.AtlasFrame;

namespace SimpleTextChanger
{
    public class Mod : IMod
    {
        public static ILog log = LogManager.GetLogger($"{nameof(SimpleTextChanger)}.{nameof(Mod)}").SetShowsErrorsInUI(false);
        private Setting m_Setting;
        public string newText = "TEST";
        public string entryID_New_Game = "Menu.NEW_GAME";
        public string Name => "Text Changer"; // Name of your mod

        public string Description => "Changes text in the game"; // Description of your mod

        public void OnLoad(UpdateSystem updateSystem)
        {
            log.Info("TextChanger mod loaded.");

            

            m_Setting = new Setting(this);
            m_Setting.RegisterInOptionsUI();
            m_Setting.SetModReference(this);
            GameManager.instance.localizationManager.AddSource("en-US", new LocaleEN(m_Setting));

            // Prompt user for input and replace text


            log.Info(nameof(OnLoad));

            if (GameManager.instance.modManager.TryGetExecutableAsset(this, out var asset))
                log.Info($"Current mod asset at {asset.path}");



            // Other code...

            ReplaceText(entryID_New_Game, newText);

        }


        public void OnDispose()
        {
            ReplaceText(entryID_New_Game, newText);

            log.Info(nameof(OnDispose));
            if (m_Setting != null)
            {
                m_Setting.UnregisterInOptionsUI();
                m_Setting = null;
            }
        }


        public void ReplaceText(string entryID, string newText)
        {
            // Set active locale to modify localization data
            GameManager.instance.localizationManager.SetActiveLocale("en-US");

            // Check if the entry ID exists in the localization data
            if (GameManager.instance.localizationManager.activeDictionary.TryGetValue(entryID, out var value))
            {
                // Update the value if the entry ID exists
                GameManager.instance.localizationManager.activeDictionary.Add(entryID, newText, false);
                log.Info($"TextChanger: Replaced '{entryID}' with '{newText}'");
            }
            else
            {
                log.Info($"TextChanger: Entry ID '{entryID}' does not exist in the localization data. No changes made.");
            }
        }
    }
}
