using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace MonoGuiFramework.System
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using Newtonsoft.Json;
    
    public enum TypeResource
    {
        Texture2D = 1,
        Font = 2
    }
    
    static public class Resources
    {
        static List<string> loadResources = new List<string>();

        static public bool IsLoaded { get; private set; } = false;
        static public EventHandler LoadUserResources;

        class ResoureceInfo
        {
            public string Name { get; set; }
            public TypeResource Type { get; set; }
        }

        static Dictionary<string, Object> resources { get; set; } = new Dictionary<string, object>();

        static public bool LoadResource()
        {
            foreach(var json in Resources.loadResources)
            {
                var json_list = JsonConvert.DeserializeObject<List<ResoureceInfo>>(json);
                foreach (var item in json_list)
                {
                    Resources.AddResource(item.Name, item.Type);
                }
            }
            
            return Resources.IsLoaded = true;
        }

        static public void AddJsonLoadResources(string json)
        {
            Resources.loadResources.Add(json);
        }

        static public void AddResource(string name, TypeResource type)
        {
            var graphics = GraphicsSingleton.GetInstance();
            var content = GraphicsSingleton.GetInstance().Content;

            switch (type)
            {
                case TypeResource.Texture2D:
                    {
                        var tex = content.Load<Texture2D>(name);
                        resources.Add(name, tex);
                    }
                    break;
                case TypeResource.Font: resources.Add(name, content.Load<SpriteFont>(name)); break;
                default: throw new Exception("Undefined resource type");
            }
        }

        public static Object GetResource(string name)
        {
            if (!Resources.IsLoaded)
                throw new Exception("Trying to get resource before downloading it");

            return resources.Keys.Any((x)=>x.Equals(name)) ? resources[name] : null;
        }
        
        public static List<Object> GetResources(List<string> names)
        {
            if (!Resources.IsLoaded)
                throw new Exception("Trying to get resource before downloading it");

            List<Object> getResources = new List<object>();

            foreach(var item in names)
            {
                if (resources.Any((x) => x.Key.Equals(item)))
                {
                    getResources.Add(resources.FirstOrDefault((x) => x.Key.Equals(item)).Value);
                }
            }

            return getResources;
        }
    }
}
