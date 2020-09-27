using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Nodebb_Redis_KeyConverter.Classes;

namespace Nodebb_Redis_KeyConverter
{
    class Program
    {
        public static List<string> AllLines = new List<string>();
        public static Dictionary<int,User>Users = new Dictionary<int, User>();
        public static Dictionary<int,Post>Posts = new Dictionary<int, Post>();
        public static Dictionary<int,Topic>Topics = new Dictionary<int, Topic>();
        public static Dictionary<int, ImportedUser> ImportedUsers = new Dictionary<int, ImportedUser>();


        static void Main(string[] args)
        {
            string path = "";
           if (args.Length != 0)
           {
               path = args[0];
           }
var settings = new JsonSerializerSettings();
settings.CheckAdditionalContent = false;

            var alltx = File.ReadAllText(path);
            alltx = alltx.Substring(1, alltx.Length - 2);
            File.WriteAllText(path,alltx);

            var alllines = File.ReadAllLines(path);

            
                foreach (var L in alllines)
                {

                try
                {
                    var parts = L.Split(new string[] { " : " }, StringSplitOptions.None);
                    var trimedpart = parts[0].Replace("\"", "");
                    var identifypart = trimedpart.Split(':');
                    var data = parts[1].TrimEnd(',');


                    if (identifypart.Length == 2)
                    {
                        switch (identifypart[0])
                        {
                            case "user":

                                var NUser = JsonConvert.DeserializeObject<User>(data, settings);
                                Users.Add(Convert.ToInt32(identifypart[1]), (User)NUser);
                                break;
                            case "post":
                                var NPostr = JsonConvert.DeserializeObject<Post>(data, settings);
                                Posts.Add(Convert.ToInt32(identifypart[1]), (Post)NPostr);
                                break;
                            case "topic":
                                var NTopic = JsonConvert.DeserializeObject<Topic>(data, settings);
                                Topics.Add(Convert.ToInt32(identifypart[1]), (Topic)NTopic);
                                break;
                            case "_imported_user":
                                var NIUser = JsonConvert.DeserializeObject<ImportedUser>(data, settings);
                                ImportedUsers.Add(Convert.ToInt32(identifypart[1]), (ImportedUser)NIUser);
                                break;
                        }
                    }
                }
                catch 
                {

                }
                }
         
            

            File.WriteAllText("Users.json", JsonConvert.SerializeObject(Users,Formatting.Indented));
            File.WriteAllText("Topics.json", JsonConvert.SerializeObject(Topics, Formatting.Indented));
            File.WriteAllText("Posts.json", JsonConvert.SerializeObject(Posts, Formatting.Indented));
            File.WriteAllText("ImportedUsers.json", JsonConvert.SerializeObject(ImportedUsers, Formatting.Indented));
        }






    }
}
