using Colossal;
using Colossal.IO.AssetDatabase;
using Game.Modding;
using Game.Settings;
using Game.UI;
using Game.UI.Widgets;
using SimpleTextChanger;
using System.Collections.Generic;
using Colossal.UI;
using System.ComponentModel.Design;
using Game.SceneFlow;
using static Colossal.IO.AssetDatabase.AtlasFrame;
using Unity.Entities;
using Game.UI.Editor;
using System.Reflection.Emit;
using System;
using JetBrains.Annotations;
using Colossal.IO.AssetDatabase.Internal;
using UnityEngine.PlayerLoop;

namespace SimpleTextChanger


{

    [FileLocation(nameof(SimpleTextChanger))]
    [SettingsUIGroupOrder(kKeyboardGroup, kButtonGroup)]
    [SettingsUIShowGroupName(kKeyboardGroup, kButtonGroup)]

    public class Setting : ModSetting
    {
        public const string kSection = "Main";
        public const string kKeyboardGroup = "Button";
        public const string kButtonGroup = "Button";



        public string selectedButton = "";
        public string selectedVariableName = "";

        public string New_Game_Text = "";
        public string Continue_Game_Text = "";
        public string Load_Game_Text = "";
        public string Save_Game_Text = "";

        public const string a = "a";
        public const string b = "b";
        public const string c = "c";
        public const string d = "d";
        public const string e = "e";
        public const string f = "f";
        private Mod m_Mod;



        // Static variable to hold the selected option
        // public string OptionValues { get; set; } = "Default Text"; // Default value

        public Setting(IMod mod) : base(mod)
        {
            SetDefaults(); // Set default values upon initialization


        }

        public void SetModReference(Mod mod)
        {
            m_Mod = mod;
        }

        // Define property for the selected option in the dropdown menu
        [SettingsUISection(kSection, kKeyboardGroup)]

        public bool Button_New_Game_Text
        {
            set
            {
                selectedButton = New_Game_Text;
                selectedVariableName = "New Game";
            }
        }
        public bool Button_Continue_Game_Text
        {
            set
            {
                selectedButton = Continue_Game_Text;
                selectedVariableName = "Continue";
            }
        }
        public bool Button_Load_Game_Text
        {
            set
            {
                selectedButton = Load_Game_Text;
                selectedVariableName = "Load Game";
            }
        }
        public bool Button_Save_Game_Text
        {
            set
            {
                selectedButton = Save_Game_Text;
                selectedVariableName = "Save Game";
            }
        }
        public bool Button_a
        {
            set
            {
                selectedButton += "a";
                Mod.log.Info("added letter 'a'");
                Mod.log.Info($"Selected Button: {selectedButton}");


            }
        }
        public bool Button_b
        {
            set
            {
                selectedButton += "b";
                Mod.log.Info("added letter 'b'");
                Mod.log.Info($"Selected Button: {selectedButton}");
            }
        }
        public bool Button_c
        {
            set
            {
                selectedButton += "c";
                Mod.log.Info("added letter 'c'");
                Mod.log.Info($"Selected Button: {selectedButton}");
            }
        }
        public bool Button_d
        {
            set
            {
                selectedButton += "d";
                Mod.log.Info("added letter 'd'");
                Mod.log.Info($"Selected Button: {selectedButton}");
            }
        }
        public bool Button_e
        {
            set
            {
                selectedButton += "e";
                Mod.log.Info("added letter 'e'");
                Mod.log.Info($"Selected Button: {selectedButton}");
            }
        }
        public bool Button_f
        {
            set
            {
                selectedButton += "f";
                Mod.log.Info("added letter 'f'");
                Mod.log.Info($"Selected Button: {selectedButton}");
            }
        }




        public override void SetDefaults()
        {
            //EnumDropdown = SomeEnum.Value1;
            selectedButton = "";
            selectedVariableName = "";

        }


        [SettingsUISection(kSection, kButtonGroup)]

        public bool Button_Submit
        {
            // When the button is clicked, update newText with the selected value
            set
            {
                Mod.log.Info("Button clicked");

                Mod.log.Info($"Selected Button: {selectedButton}");


                if (selectedVariableName != null)
                {
                    switch (selectedVariableName)
                    {
                        case "New Game":
                            m_Mod.ReplaceText("Menu.NEW_GAME", selectedButton);
                            break;
                        case "Resume":
                            m_Mod.ReplaceText("Menu.CONTINUE_GAME", selectedButton);
                            break;
                        case "Load Game":
                            m_Mod.ReplaceText("Menu.LOAD_GAME", selectedButton);
                            break;
                        case "Save Game":
                            m_Mod.ReplaceText("Menu.SAVE_GAME", selectedButton);
                            break;
                    }
                }


            }
        }

    }


    public class LocaleEN : IDictionarySource
    {
        private readonly Setting m_Setting;
        public LocaleEN(Setting setting)
        {
            m_Setting = setting;
        }
        public IEnumerable<KeyValuePair<string, string>> ReadEntries(IList<IDictionaryEntryError> errors, Dictionary<string, int> indexCounts)
        {
            return new Dictionary<string, string>
            {
                { m_Setting.GetSettingsLocaleID(), "Simple Text Changer" },
                { m_Setting.GetOptionTabLocaleID(Setting.kSection), "Main" },
                { m_Setting.GetOptionDescLocaleID(Setting.kSection), "Change the text of the buttons" },

                { m_Setting.GetOptionGroupLocaleID(Setting.kKeyboardGroup), $"Current Text: [{m_Setting.selectedButton}]" },
                { m_Setting.GetOptionGroupLocaleID(Setting.kButtonGroup), "Buttons" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.Button_New_Game_Text)), "New Game" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.Button_New_Game_Text)), "Change the text for the New Game button" },
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.Button_Continue_Game_Text)), "Continue Game" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.Button_Continue_Game_Text)), "Change the text for the Continue button" },
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.Button_Load_Game_Text)), "Load Game" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.Button_Load_Game_Text)), "Change the text for the Load Game button" },
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.Button_Save_Game_Text)), "Save Game" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.Button_Save_Game_Text)), "Change the text for the Save Game button" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.Button_a)), "a"},
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.Button_a)), "Add the letter a" },
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.Button_b)), "b"},
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.Button_b)), "Add the letter b" },
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.Button_c)), "c"},
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.Button_c)), "Add the letter c" },
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.Button_d)), "d"},
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.Button_d)), "Add the letter d" },
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.Button_e)), "e"},
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.Button_e)), "Add the letter e" },
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.Button_f)), "f"},
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.Button_f)), "Add the letter f" },



                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.Button_Submit)), "Save Changes" },

            };
        }

        public void Unload()
        {

        }
    }


}