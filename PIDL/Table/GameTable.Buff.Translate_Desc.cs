
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;
namespace GameTable.Buff
{
    public class Translate_Desc
    {
static bool isLoaded = false;
	public static List<Translate_Desc>       list       = new List<Translate_Desc>();
	public static Dictionary<int, Translate_Desc> dict       = new Dictionary<int, Translate_Desc>();
	public int Index;
	public string Name;
	public string KR;
 

#if !SERVER
        public static void Load()
        {
if(isLoaded) return;
isLoaded = true;
          var textAsset = Resources.Load("TableDatas/GameTable.Buff.Translate_Desc") as TextAsset;
          var str = textAsset.text; 
          var loadedList = JsonConvert.DeserializeObject<List<Translate_Desc>>(str);
          for(int i = 0; i < loadedList.Count; i++)
          {
            
              var data = loadedList[i];
              if(loadedList != null)
              {
                    list.Add(loadedList[i]);
                    dict.Add(loadedList[i].Index, loadedList[i]);
              }
          }
    
        }
#else
        public static void Load()
        { 
if(isLoaded) return;
isLoaded = true;
          var str = File.ReadAllText("TableDatas/GameTable.Buff.Translate_Desc" + ".txt");
          var loadedList = JsonConvert.DeserializeObject<List<Translate_Desc>>(str);
          for(int i = 0; i < loadedList.Count; i++)
          {
            
              var data = loadedList[i];
              if(loadedList != null)
              {
                    list.Add(loadedList[i]);
                    dict.Add(loadedList[i].Index, loadedList[i]);
              }
          }
    
        }

#endif
 

        public static Translate_Desc Get(int index)
        {
           if(list.Count == 0) Load();
           bool exist = dict.ContainsKey(index);
           if(exist)
              return dict[index];
           else
              return null;
        }

    }
}
            