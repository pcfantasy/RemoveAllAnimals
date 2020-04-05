using ICities;
using System.IO;
using RemoveAllAnimals.Util;

namespace RemoveAllAnimals
{
    public class RemoveAllAnimals : IUserMod
    {
        public static bool IsEnabled = false;
        public static bool removeAnimal = false;

        public string Name
        {
            get { return "Remove All Animals"; }
        }

        public string Description
        {
            get { return "Remove all animals in the city to save citizen instance count"; }
        }

        public void OnEnabled()
        {
            IsEnabled = true;
            FileStream fs = File.Create("RemoveAllAnimals.txt");
            fs.Close();
        }

        public void OnDisabled()
        {
            IsEnabled = false;
        }

        public static void SaveSetting()
        {
            //save langugae
            FileStream fs = File.Create("RemoveAllAnimals_setting.txt");
            StreamWriter streamWriter = new StreamWriter(fs);
            streamWriter.WriteLine(removeAnimal);
            streamWriter.Flush();
            fs.Close();
        }

        public static void LoadSetting()
        {
            if (File.Exists("RemoveAllAnimals_setting.txt"))
            {
                FileStream fs = new FileStream("RemoveAllAnimals_setting.txt", FileMode.Open);
                StreamReader sr = new StreamReader(fs);
                string strLine = sr.ReadLine();

                if (strLine == "False")
                {
                    removeAnimal = false;
                }
                else
                {
                    removeAnimal = true;
                }
                sr.Close();
                fs.Close();
            }
        }

        public void OnSettingsUI(UIHelperBase helper)
        {
            LoadSetting();
            UIHelperBase group = helper.AddGroup(Localization.Get("SAVE_CITIZEN_INSTANCE_COUNT_DESCRIPTION"));
            group.AddCheckbox(Localization.Get("SAVE_CITIZEN_INSTANCE_ENABLE"), removeAnimal, (index) => removeAnimalEnable(index));
            SaveSetting();
        }

        public void removeAnimalEnable(bool index)
        {
            removeAnimal = index;
            SaveSetting();
        }
    }
}

