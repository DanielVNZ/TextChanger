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

namespace SimpleTextChanger
{
    [FileLocation(nameof(SimpleTextChanger))]
    [SettingsUIGroupOrder(kDropdownGroup, kTextBoxGroup)]
    [SettingsUIShowGroupName(kDropdownGroup, kTextBoxGroup)]
    public class Setting : ModSetting
    {
        public const string kSection = "Main";
        public const string kDropdownGroup = "Dropdown";
        public const string kButtonGroup = "Button";
        public const string kTextBoxGroup = "TextBox";
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

        // Create a settings UI section for a text box

        [SettingsUISection(kSection, kDropdownGroup)]

        // Define property for the dropdown of strings
        public SomeEnum EnumDropdown { get; set; } = SomeEnum.New_Game;


        [SettingsUISection(kSection, kTextBoxGroup)] // Add a new section for text box
        public string TextBoxText { get; set; } = "Default Text";

        public override void SetDefaults()
        {
            //EnumDropdown = SomeEnum.Value1;

        }


        [SettingsUISection(kSection, kButtonGroup)]

        public bool Button_Submit
        {
            // When the button is clicked, update newText with the selected value
            set
            {
                Mod.log.Info("Button clicked");

                if (m_Mod != null)
                {
                    Mod.log.Info("m_Mod is not null");

                    // Get selected value from the dropdown menu
                    string selectedValue_New_Game = GetSelectedEnumAsString();
                    Mod.log.Info($"Selected value: {selectedValue_New_Game}");

                    // Update newText variable in Mod.cs with the selected value
                    m_Mod.newText = selectedValue_New_Game;
                    Mod.log.Info("New text updated successfully.");

                    m_Mod.ReplaceText("Menu.NEW_GAME", selectedValue_New_Game);
                }
                else
                {
                    Mod.log.Error("m_Mod is null. Unable to update new text.");
                }
            }
        }

        public string GetSelectedEnumAsString()
        {
            string enumString = EnumDropdown.ToString();
            // Replace underscores with spaces
            return enumString.Replace("_", " ");
        }

        public enum SomeEnum
        {
            New_Game,
            New_Game_Bro,
            Value3,
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






                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.EnumDropdown)), "New Game" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.EnumDropdown)), $"Use any enum property with getter and setter to get enum dropdown" },

                // Add localization for the text box label
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.TextBoxText)), "Enter Text" },

                // Update the settings UI group for the text box
                { m_Setting.GetOptionGroupLocaleID(Setting.kTextBoxGroup), "Text Boxes" },

                { m_Setting.GetOptionGroupLocaleID(Setting.kDropdownGroup), "Dropdowns" },

                { m_Setting.GetEnumValueLocaleID(Setting.SomeEnum.New_Game), "New Game" },
                { m_Setting.GetEnumValueLocaleID(Setting.SomeEnum.New_Game_Bro), "New game Bro" },
                { m_Setting.GetEnumValueLocaleID(Setting.SomeEnum.Value3), "Value 3" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.Button_Submit)), "Save Changes" },


            };
        }

        public void Unload()
        {

        }
    }


}