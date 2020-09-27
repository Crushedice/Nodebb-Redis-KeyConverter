using Newtonsoft.Json;
using Nodebb_Redis_KeyConverter.Classes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Nodebb_Redis_KeyConverter
{
    class Program
    {
        public static List<string> AllLines = new List<string>();
        public static Dictionary<int, ImportedUser> ImportedUsers = new Dictionary<int, ImportedUser>();
        public static Dictionary<int, Post> Posts = new Dictionary<int, Post>();
        public static Dictionary<int, Topic> Topics = new Dictionary<int, Topic>();
        public static Dictionary<int, User> Users = new Dictionary<int, User>();


        static void Main(string[] args)
        {
            string path = string.Empty;
            if(args.Length != 0)
            {
                path = args[0];
            }
            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.CheckAdditionalContent = false;

            string alltx = File.ReadAllText(path);
            alltx = alltx.Substring(1, alltx.Length - 2);
            File.WriteAllText(path, alltx);

            string[] alllines = File.ReadAllLines(path);


            foreach(string L in alllines)
            {
                try
                {
                    string[] parts = L.Split(new string[] { " : " }, StringSplitOptions.None);
                    string trimedpart = parts[0].Replace("\"", string.Empty);
                    string[] identifypart = trimedpart.Split(':');
                    string data = parts[1].TrimEnd(',');


                    if(identifypart.Length == 2)
                    {
                        switch(identifypart[0])
                        {
                            case "user":

                                User NUser = JsonConvert.DeserializeObject<User>(data, settings);
                                Users.Add(Convert.ToInt32(identifypart[1]), NUser);
                                break;
                            case "post":
                                Post NPostr = JsonConvert.DeserializeObject<Post>(data, settings);
                                Posts.Add(Convert.ToInt32(identifypart[1]), NPostr);
                                break;
                            case "topic":
                                Topic NTopic = JsonConvert.DeserializeObject<Topic>(data, settings);
                                Topics.Add(Convert.ToInt32(identifypart[1]), NTopic);
                                break;
                            case "_imported_user":
                                ImportedUser NIUser = JsonConvert.DeserializeObject<ImportedUser>(data, settings);
                                ImportedUsers.Add(Convert.ToInt32(identifypart[1]), NIUser);
                                break;
                        }
                    }
                } catch
                {
                }
            }


            File.WriteAllText("Users.json", JsonConvert.SerializeObject(Users, Formatting.Indented));
            File.WriteAllText("Topics.json", JsonConvert.SerializeObject(Topics, Formatting.Indented));
            File.WriteAllText("Posts.json", JsonConvert.SerializeObject(Posts, Formatting.Indented));
            File.WriteAllText("ImportedUsers.json", JsonConvert.SerializeObject(ImportedUsers, Formatting.Indented));
        }
    }
}
