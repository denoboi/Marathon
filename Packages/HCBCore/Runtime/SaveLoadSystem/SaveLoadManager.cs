using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Newtonsoft.Json;
using UnityEngine;


namespace HCB.Core
{
    public static class SaveLoadManager
    {
        const string JSON_PREFIX = "/Data_";
        /// <summary>Save Generic Data.
        /// <para>Save file as Object in Persistent Data Path. <see cref="UnityEngine.Application.persistentDataPath"/> for more information.</para>
        /// </summary>
        public static bool SavePDP<T>(T data, string fileName)
        {
            return Save(data, Application.persistentDataPath + JSON_PREFIX + fileName);
        }

        /// <summary>Save Generic Data.
        /// <para>Save file as Object in custom Path.</para>
        /// </summary>
        private static bool Save<T>(T saveData, string pathFileName)
        {
            FileStream dataStream = new FileStream(pathFileName, FileMode.Create);

            BinaryFormatter converter = new BinaryFormatter();
            converter.Serialize(dataStream, saveData);

            dataStream.Close();
            return true;

        }

        /// <summary>Load Generic Data.
        /// <para>Load file as Object from Persistent Data Path. <see cref="UnityEngine.Application.persistentDataPath"/> for more information.</para>
        /// </summary>
        public static T LoadPDP<T>(string fileName, T _default) where T : new()
        {
            return Load<T>(Application.persistentDataPath + JSON_PREFIX + fileName, _default);
        }

        /// <summary>Load Generic Data.
        /// <para>Load file as Object from custom Path.</para>
        /// </summary>
        private static T Load<T>(string pathFileName, T _default) where T : new()
        {
            try
            {
                if (File.Exists(pathFileName))
                {
                    // File exists 
                    FileStream dataStream = new FileStream(pathFileName, FileMode.Open);

                    BinaryFormatter converter = new BinaryFormatter();

                    var saveData = converter.Deserialize(dataStream);

                    dataStream.Close();
                    return (T)saveData;
                }
                else
                {
                    if (_default != null)
                        return _default;
                    else return default(T);
                }
            }
            catch
            {
                // File does not exist
                Debug.LogError("Save file not found in " + pathFileName);
                if (_default != null)
                    return _default;
                else return default(T);
            }


        }

        public static void DeleteFile(string fileName)
        {
            File.Delete(Application.persistentDataPath + JSON_PREFIX + fileName);
        }
    }
}
