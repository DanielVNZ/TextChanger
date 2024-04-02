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
using Newtonsoft.Json;
using System.IO;
using Newtonsoft.Json.Linq;

namespace SimpleTextChanger


{

   



    [FileLocation(nameof(SimpleTextChanger))]
    [SettingsUIGroupOrder(kButtonGroup, kGroupTwo, kGroupThree)]
    [SettingsUIShowGroupName(kButtonGroup, kGroupTwo, kGroupThree)]

    public class Setting : ModSetting
    {
        public const string kSection = "Main";
        public const string kKeyboardGroup = "Button";
        public const string kButtonGroup = "Button";
        public const string kButtonSelectionGroup = "Button";
        public const string kGroupTwo = "Buttons";
        public const string kGroupThree = "Buttonss";



        public string selectedButton = "";
        public string selectedVariableName = "";

        public string New_Game_Text = "New Game";
        public string Continue_Game_Text = "Continue";
        public string Load_Game_Text = "Load Game";
        public string Save_Game_Text = "Save Game";
        public string Editor_Text = "Editor";
        public string Options_Text = "Options";
        public string Exit_Text = "Exit";


        
        

        public const string a = "a";
        public const string b = "b";
        public const string c = "c";
        public const string d = "d";
        public const string e = "e";
        public const string f = "f";
        public const string g = "g";
        public const string h = "h";
        public const string i = "i";
        public const string j = "j";    
        public const string k = "k";
        public const string l = "l";
        public const string m = "m";
        public const string n = "n";
        public const string o = "o";
        public const string p = "p";
        public const string q = "q";
        public const string r = "r";
        public const string s = "s";
        public const string t = "t";
        public const string u = "u";
        public const string v = "v";
        public const string w = "w";
        public const string x = "x";
        public const string y = "y";
        public const string z = "z";
        public const string space = " ";
        public const string exclamation_mark = "!";
        public const string full_stop = ".";


        private Mod m_Mod;



        // Static variable to hold the selected option
        // public string OptionValues { get; set; } = "Default Text"; // Default value

        public Setting(IMod mod) : base(mod)
        {
            GetSettingsFilePath();

            LoadSettings(); // Load settings upon initialization
            //SetDefaults(); // Set default values upon initialization
            


        }

        public string GetSettingsFilePath()
        {
            string appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            appDataPath = Path.Combine(appDataPath, "..", "LocalLow"); // Traverse to LocalLow

            string modSettingsDirectory = Path.Combine(appDataPath, "Colossal Order", "Cities Skylines II", "ModSettings", "SimpleTextChanger");

            // Create the directory if it doesn't exist
            if (!Directory.Exists(modSettingsDirectory))
            {
                Directory.CreateDirectory(modSettingsDirectory);
            }

            return Path.Combine(modSettingsDirectory, "SimpleTextChangerSettings.json");
        }

        public class SettingsData
        {
            public string New_Game_Text = "New Game";
            public string Continue_Game_Text = "Continue";
            public string Load_Game_Text = "Load Game";
            public string Save_Game_Text = "Save Game";
            public string Editor_Text = "Editor";
            public string Options_Text = "Options";
            public string Exit_Text = "Exit";
        }

        public void SaveSettings()
        {
            try
            {
                SettingsData data = new SettingsData()
                {
                    New_Game_Text = New_Game_Text,
                    Continue_Game_Text = Continue_Game_Text,
                    Load_Game_Text= Load_Game_Text,
                    Save_Game_Text = Save_Game_Text,    
                    Editor_Text = Editor_Text,
                    Options_Text = Options_Text,
                    Exit_Text = Exit_Text,
                    // ... populate other settings ...
                };

                string jsonData = JsonConvert.SerializeObject(data, Formatting.Indented); // Indented for readability
                File.WriteAllText(GetSettingsFilePath(), jsonData);

                Mod.log.Info($"Saving settings to: {GetSettingsFilePath()}");
                Mod.log.Info($"New Game: {New_Game_Text}");
                Mod.log.Info($"Continue Game: {Continue_Game_Text}");
                Mod.log.Info($"Load Game: {Load_Game_Text}");

                Mod.log.Info($"JSON DATA: {jsonData}");


                Mod.log.Info("Settings saved successfully.");
            }
            catch (Exception ex)
            {
                Mod.log.Error($"Error saving settings: {ex.Message}");
            }
        }

        public void LoadSettings()
        {
            try
            {
                if (File.Exists(GetSettingsFilePath()))
                {
                    string jsonData = File.ReadAllText(GetSettingsFilePath());
                    SettingsData data = JsonConvert.DeserializeObject<SettingsData>(jsonData);

                    New_Game_Text = data.New_Game_Text;
                    Continue_Game_Text = data.Continue_Game_Text;
                    Load_Game_Text = data.Load_Game_Text;
                    Save_Game_Text = data.Save_Game_Text;
                    Editor_Text = data.Editor_Text;
                    Options_Text = data.Options_Text;
                    Exit_Text = data.Exit_Text;
                    // ... load other settings ...

                    m_Mod.ReplaceText("Menu.NEW_GAME", New_Game_Text);
                    m_Mod.ReplaceText("Menu.CONTINUE_GAME", Continue_Game_Text);
                    m_Mod.ReplaceText("Menu.LOAD_GAME", Load_Game_Text);
                    m_Mod.ReplaceText("Menu.SAVE_GAME", Save_Game_Text);
                    m_Mod.ReplaceText("Menu.EDITOR", Editor_Text);
                    m_Mod.ReplaceText("Menu.OPTIONS", Options_Text);
                    m_Mod.ReplaceText("Menu.EXIT_GAME", Exit_Text);


                    Mod.log.Info("Settings loaded successfully.");
                    Mod.log.Info($"JSON DATA {jsonData}");
                }
                else
                {
                    Mod.log.Info("No settings file found, using defaults.");
                }
            }
            catch (Exception ex)
            {
                Mod.log.Error($"Error loading settings: {ex.Message}");
            }
            
        }





        public void SetModReference(Mod mod)
        {
            m_Mod = mod;
        }


        [SettingsUISection(kSection, kButtonGroup)]

        public bool Button_New_Game_Text
        {
            set
            {
                //selectedButton = New_Game_Text;
                selectedVariableName = "New Game";
            }
        }

        [SettingsUISection(kSection, kButtonGroup)]
        public bool Button_Continue_Game_Text
        {
            set
            {
                //selectedButton = Continue_Game_Text;
                selectedVariableName = "Continue";
            }
        }

        [SettingsUISection(kSection, kButtonGroup)]
        public bool Button_Load_Game_Text
        {
            set
            {
                //selectedButton = Load_Game_Text;
                selectedVariableName = "Load Game";
            }
        }

        [SettingsUISection(kSection, kButtonGroup)]
        public bool Button_Save_Game_Text
        {
            set
            {
                //selectedButton = Save_Game_Text;
                selectedVariableName = "Save Game";
            }
        }

        [SettingsUISection(kSection, kButtonGroup)]
        public bool Button_Editor_Text
        {
            set
            {
                //selectedButton = Editor_Text;
                selectedVariableName = "Editor";
            }
        }

        [SettingsUISection(kSection, kButtonGroup)]
        public bool Button_Options_Text
        {
            set
            {
                //selectedButton = Options_Text;
                selectedVariableName = "Options";
            }
        }

        [SettingsUISection(kSection, kButtonGroup)]
        public bool Button_Exit_Text
        {
            set
            {
                //selectedButton = Exit_Text;
                selectedVariableName = "Exit";
            }
        }

        [SettingsUISection(kSection, kGroupTwo)]
        public bool Button_a
        {
            set
            {
                selectedButton += "a";
                Mod.log.Info("added letter 'a'");
                Mod.log.Info($"Selected Button: {selectedButton}");


            }
        }

        [SettingsUISection(kSection, kGroupTwo)]
        public bool Button_b
        {
            set
            {
                selectedButton += "b";
                Mod.log.Info("added letter 'b'");
                Mod.log.Info($"Selected Button: {selectedButton}");
            }
        }

        [SettingsUISection(kSection, kGroupTwo)]
        public bool Button_c
        {
            set
            {
                selectedButton += "c";
                Mod.log.Info("added letter 'c'");
                Mod.log.Info($"Selected Button: {selectedButton}");
            }
        }

        [SettingsUISection(kSection, kGroupTwo)]
        public bool Button_d
        {
            set
            {
                selectedButton += "d";
                Mod.log.Info("added letter 'd'");
                Mod.log.Info($"Selected Button: {selectedButton}");
            }
        }

        [SettingsUISection(kSection, kGroupTwo)]
        public bool Button_e
        {
            set
            {
                selectedButton += "e";
                Mod.log.Info("added letter 'e'");
                Mod.log.Info($"Selected Button: {selectedButton}");
            }
        }

        [SettingsUISection(kSection, kGroupTwo)]
        public bool Button_f
        {
            set
            {
                selectedButton += "f";
                Mod.log.Info("added letter 'f'");
                Mod.log.Info($"Selected Button: {selectedButton}");
            }
        }

        [SettingsUISection(kSection, kGroupTwo)]
        public bool Button_g
        {
            set
            {
                selectedButton += "g";
                Mod.log.Info("added letter 'g'");
                Mod.log.Info($"Selected Button: {selectedButton}");
            }
        }

        [SettingsUISection(kSection, kGroupTwo)]
        public bool Button_h
        {
            set
            {
                selectedButton += "h";
                Mod.log.Info("added letter 'h'");
                Mod.log.Info($"Selected Button: {selectedButton}");
            }
        }

        [SettingsUISection(kSection, kGroupTwo)]
        public bool Button_i
        {
            set
            {
                selectedButton += "i";
                Mod.log.Info("added letter 'i'");
                Mod.log.Info($"Selected Button: {selectedButton}");
            }
        }

        [SettingsUISection(kSection, kGroupTwo)]
        public bool Button_j
        {
            set
            {
                selectedButton += "j";
                Mod.log.Info("added letter 'j'");
                Mod.log.Info($"Selected Button: {selectedButton}");
            }
        }

        [SettingsUISection(kSection, kGroupTwo)]
        public bool Button_k
        {
            set
            {
                selectedButton += "k";
                Mod.log.Info("added letter 'k'");
                Mod.log.Info($"Selected Button: {selectedButton}");
            }
        }

        [SettingsUISection(kSection, kGroupTwo)]
        public bool Button_l
        {
            set
            {
                selectedButton += "l";
                Mod.log.Info("added letter 'l'");
                Mod.log.Info($"Selected Button: {selectedButton}");
            }
        }

        [SettingsUISection(kSection, kGroupTwo)]
        public bool Button_m
        {
            set
            {
                selectedButton += "m";
                Mod.log.Info("added letter 'm'");
                Mod.log.Info($"Selected Button: {selectedButton}");
            }
        }

        [SettingsUISection(kSection, kGroupTwo)]
        public bool Button_n
        {
            set
            {
                selectedButton += "n";
                Mod.log.Info("added letter 'n'");
                Mod.log.Info($"Selected Button: {selectedButton}");
            }
        }

        [SettingsUISection(kSection, kGroupTwo)]
        public bool Button_o
        {
            set
            {
                selectedButton += "o";
                Mod.log.Info("added letter 'o'");
                Mod.log.Info($"Selected Button: {selectedButton}");
            }
        }

        [SettingsUISection(kSection, kGroupTwo)]
        public bool Button_p
        {
            set
            {
                selectedButton += "p";
                Mod.log.Info("added letter 'p'");
                Mod.log.Info($"Selected Button: {selectedButton}");
            }
        }

        [SettingsUISection(kSection, kGroupTwo)]
        public bool Button_q
        {
            set
            {
                selectedButton += "q";
                Mod.log.Info("added letter 'q'");
                Mod.log.Info($"Selected Button: {selectedButton}");
            }
        }

        [SettingsUISection(kSection, kGroupTwo)]
        public bool Button_r
        {
            set
            {
                selectedButton += "r";
                Mod.log.Info("added letter 'r'");
                Mod.log.Info($"Selected Button: {selectedButton}");
            }
        }

        [SettingsUISection(kSection, kGroupTwo)]
        public bool Button_s
        {
            set
            {
                selectedButton += "s";
                Mod.log.Info("added letter 's'");
                Mod.log.Info($"Selected Button: {selectedButton}");
            }
        }

        [SettingsUISection(kSection, kGroupTwo)]
        public bool Button_t
        {
            set
            {
                selectedButton += "t";
                Mod.log.Info("added letter 't'");
                Mod.log.Info($"Selected Button: {selectedButton}");
            }
        }

        [SettingsUISection(kSection, kGroupTwo)]
        public bool Button_u
        {
            set
            {
                selectedButton += "u";
                Mod.log.Info("added letter 'u'");
                Mod.log.Info($"Selected Button: {selectedButton}");
            }
        }

        [SettingsUISection(kSection, kGroupTwo)]
        public bool Button_v
        {
            set
            {
                selectedButton += "v";
                Mod.log.Info("added letter 'v'");
                Mod.log.Info($"Selected Button: {selectedButton}");
            }
        }

        [SettingsUISection(kSection, kGroupTwo)]
        public bool Button_w
        {
            set
            {
                selectedButton += "w";
                Mod.log.Info("added letter 'w'");
                Mod.log.Info($"Selected Button: {selectedButton}");
            }
        }

        [SettingsUISection(kSection, kGroupTwo)]
        public bool Button_x
        {
            set
            {
                selectedButton += "x";
                Mod.log.Info("added letter 'x'");
                Mod.log.Info($"Selected Button: {selectedButton}");
            }
        }

        [SettingsUISection(kSection, kGroupTwo)]
        public bool Button_y
        {
            set
            {
                selectedButton += "y";
                Mod.log.Info("added letter 'y'");
                Mod.log.Info($"Selected Button: {selectedButton}");
            }
        }

        [SettingsUISection(kSection, kGroupTwo)]
        public bool Button_z
        {
            set
            {
                selectedButton += "z";
                Mod.log.Info("added letter 'z'");
                Mod.log.Info($"Selected Button: {selectedButton}");
            }
        }

        [SettingsUISection(kSection, kGroupTwo)]
        public bool Button_space
        {
            set
            {
                selectedButton += " ";
                Mod.log.Info("added letter ' '");
                Mod.log.Info($"Selected Button: {selectedButton}");
            }
        }

        [SettingsUISection(kSection, kGroupTwo)]
        public bool Button_exclamation_mark
        {
            set
            {
                selectedButton += "!";
                Mod.log.Info("added letter '!'");
                Mod.log.Info($"Selected Button: {selectedButton}");
            }
        }

        [SettingsUISection(kSection, kGroupTwo)]
        public bool Button_full_stop
        {
            set
            {
                selectedButton += ".";
                Mod.log.Info("added letter '.'");
                Mod.log.Info($"Selected Button: {selectedButton}");
            }
        }


        [SettingsUISection(kSection, kGroupTwo)]
        public bool Button_clear
        {
            set
            {
                selectedButton = "";
                Mod.log.Info("Clearing text");
                Mod.log.Info($"Selected Button: {selectedButton}");
            }
        }

        


        [SettingsUISection(kSection, kGroupThree)]
        public bool Button_Submit
        {
            // When the button is clicked, update newText with the selected value
            set
            {
                Mod.log.Info("Button clicked");

                Mod.log.Info($"Selected Button: {selectedButton}");

                GetSettingsFilePath();

                

                if (selectedVariableName != null)
                {
                    switch (selectedVariableName)
                    {
                        case "New Game":
                            m_Mod.ReplaceText("Menu.NEW_GAME", selectedButton);
                            New_Game_Text = selectedButton;
                            selectedButton = "";
                            SaveSettings();
                            break;
                        case "Resume":
                            m_Mod.ReplaceText("Menu.CONTINUE_GAME", selectedButton);
                            m_Mod.ReplaceText("Menu.RESUME_GAME", selectedButton);
                            Continue_Game_Text = selectedButton;
                            selectedButton = "";
                            SaveSettings();
                            break;
                        case "Load Game":
                            m_Mod.ReplaceText("Menu.LOAD_GAME", selectedButton);
                            Load_Game_Text = selectedButton;
                            selectedButton = "";
                            SaveSettings();
                            break;
                        case "Save Game":
                            m_Mod.ReplaceText("Menu.SAVE_GAME", selectedButton);
                            Save_Game_Text = selectedButton;
                            selectedButton = "";
                            SaveSettings();
                            break;
                        case "Editor":
                            m_Mod.ReplaceText("Menu.EDITOR", selectedButton);
                            Editor_Text = selectedButton;
                            selectedButton = "";
                            SaveSettings();
                            break;
                        case "Options":
                            m_Mod.ReplaceText("Menu.OPTIONS", selectedButton);
                            Options_Text = selectedButton;
                            selectedButton = "";
                            SaveSettings();
                            break;
                        case "Exit":
                            m_Mod.ReplaceText("Menu.EXIT_GAME", selectedButton);
                            m_Mod.ReplaceText("Menu.EXIT_APPLICATION", selectedButton);
                            Exit_Text = selectedButton;
                            selectedButton = "";
                            SaveSettings();
                            break;
                    }
                }


            }
        }

        


        public override void SetDefaults()
        {
            //selectedButton = "";
            //selectedVariableName = "";
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

                //{ m_Setting.GetOptionGroupLocaleID(Setting.kKeyboardGroup), $"Current Text: [{m_Setting.selectedButton}]" },
                //{ m_Setting.GetOptionGroupLocaleID(Setting.kButtonSelectionGroup), "Step 2. Type out the button text. " },
                { m_Setting.GetOptionGroupLocaleID(Setting.kButtonGroup), "Step 1. Select a button to change its text." },
                { m_Setting.GetOptionGroupLocaleID(Setting.kGroupTwo), "Step 2. Type out the button text. " },
                { m_Setting.GetOptionGroupLocaleID(Setting.kGroupThree), "Step 3. Click on Save Changes " },


                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.Button_New_Game_Text)), "New Game" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.Button_New_Game_Text)), "Change the text for the New Game button" },
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.Button_Continue_Game_Text)), "Continue Game" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.Button_Continue_Game_Text)), "Change the text for the Continue button !WARNING! MAY NOT WORK" },
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.Button_Load_Game_Text)), "Load Game" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.Button_Load_Game_Text)), "Change the text for the Load Game button" },
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.Button_Save_Game_Text)), "Save Game" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.Button_Save_Game_Text)), "Change the text for the Save Game button" },
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.Button_Editor_Text)), "Editor" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.Button_Editor_Text)), "Change the text for the Editor button" },
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.Button_Options_Text)), "Options" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.Button_Options_Text)), "Change the text for the Options button" },
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.Button_Exit_Text)), "Exit" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.Button_Exit_Text)), "Change the text for the Exit button" },
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.Button_full_stop)), "Full Stop"},
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.Button_full_stop)), "Add a full stop"},


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
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.Button_g)), "g"},
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.Button_g)), "Add the letter g" },
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.Button_h)), "h"},
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.Button_h)), "Add the letter h" },
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.Button_i)), "i"},
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.Button_i)), "Add the letter i" },
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.Button_j)), "j"},
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.Button_j)), "Add the letter j" },
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.Button_k)), "k"},
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.Button_k)), "Add the letter k" },
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.Button_l)), "l"},
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.Button_l)), "Add the letter l" },
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.Button_m)), "m"},
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.Button_m)), "Add the letter m" },
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.Button_n)), "n"},
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.Button_n)), "Add the letter n" },
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.Button_o)), "o"},
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.Button_o)), "Add the letter o" },
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.Button_p)), "p"},
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.Button_p)), "Add the letter p" },
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.Button_q)), "q"},
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.Button_q)), "Add the letter q" },
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.Button_r)), "r"},
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.Button_r)), "Add the letter r" },
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.Button_s)), "s"},
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.Button_s)), "Add the letter s" },
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.Button_t)), "t"},
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.Button_t)), "Add the letter t" },
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.Button_u)), "u"},
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.Button_u)), "Add the letter u" },
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.Button_v)), "v"},
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.Button_v)), "Add the letter v" },
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.Button_w)), "w"},
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.Button_w)), "Add the letter w" },
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.Button_x)), "x"},
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.Button_x)), "Add the letter x" },
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.Button_y)), "y"},
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.Button_y)), "Add the letter y" },
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.Button_z)), "z"},
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.Button_z)), "Add the letter z" },
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.Button_space)), "Space"},
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.Button_space)), "Add a Space" },
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.Button_clear)), "Clear Text"},
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.Button_clear)), "Clears text" },
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.Button_exclamation_mark)), "Exclamation Mark"},
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.Button_exclamation_mark)), "Add an exclamation mark" },



                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.Button_Submit)), "Save Changes" },

            };
        }

        public void Unload()
        {

        }
    }


}