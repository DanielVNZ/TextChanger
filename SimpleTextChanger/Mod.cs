// In Mod.cs

using Colossal.Localization;
using Game;
using Game.Modding;
using Game.SceneFlow;
using UnityEngine;
using Colossal.Logging;
using Colossal.IO.AssetDatabase;
using static Colossal.IO.AssetDatabase.AtlasFrame;
using System.IO;
using System;


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
            m_Setting.LoadSettings();
            /*ReplaceText("Menu.NEW_GAME", m_Setting.New_Game_Text);
            ReplaceText("Menu.CONTINUE_GAME", m_Setting.Continue_Game_Text);
            ReplaceText("Menu.LOAD_GAME", m_Setting.Load_Game_Text);
            ReplaceText("Menu.SAVE_GAME", m_Setting.Save_Game_Text);
            ReplaceText("Menu.EDITOR", m_Setting.Editor_Text);
            ReplaceText("Menu.OPTIONS", m_Setting.Options_Text);
            ReplaceText("Menu.EXIT_GAME", m_Setting.Exit_Text);*/
            Mod.log.Info($"New Game Text {m_Setting.New_Game_Text}");

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
                m_Setting.GetSettingsFilePath();
                m_Setting.SaveSettings();
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
