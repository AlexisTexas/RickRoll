using System;
using System.Collections.Generic;
using System.Linq;
using LeagueSharp;
using LeagueSharp.Common;

namespace RickRoll
{
    internal class Program
    {
        public static Menu Config;

        private static readonly Obj_AI_Hero Player = ObjectManager.Player;
        private static Rick_Roll[] Troll;
        private static List<Obj_AI_Hero> enemys;
        private static int myKills = 0;
        private static int lastMessage = 0;




        private static void Main(string[] args)
        {
            CustomEvents.Game.OnGameLoad += OnLoad;
        }

        private static void OnLoad(EventArgs args)
        {
           
            Config = new Menu("Rick Roll", "Rick Roll", true);
            Config.AddItem(new MenuItem("Rick", "Rick Roll").SetValue(true));
            Config.AddItem(new MenuItem("myKills", "Only My Kills?").SetValue(true));
            Config.AddItem(new MenuItem("percent", "chance to rickRoll").SetValue(new Slider(100)));
            Config.AddItem(new MenuItem("delay", "delay to shout").SetValue(new Slider(1500,0,3000)));
            

            Config.AddToMainMenu();
            Troll = new Rick_Roll[ObjectManager.Get<Obj_AI_Hero>().Count(enemy => enemy.IsEnemy)];
           

            int count = 0;
            enemys = new List<Obj_AI_Hero>();
            foreach (var hero in ObjectManager.Get<Obj_AI_Hero>())
            {
                if (hero.IsEnemy)
                {
                    Troll[count++] = new Rick_Roll(hero);
                    enemys.Add(hero);
                }
            }
            Game.OnUpdate += Game_OnUpdate;
        }




        private static void Game_OnUpdate(EventArgs args)
        {

            if (Config.Item("myKills").GetValue<bool>())
            {
                if (Player.ChampionsKilled == myKills)
                {
                    return;
                }
                myKills = Player.ChampionsKilled;
            }

            int chance = new Random().Next(1, 100);
            if (chance > Config.Item("percent").GetValue<Slider>().Value)
                return;

            if (!Config.Item("Rick").GetValue<bool>())
                return;
            
            foreach (var enemy in enemys)
            {
                if (enemy.IsEnemy)
                {
                    foreach (Rick_Roll Rick in Troll)
                    {
                        if (enemy.Name.Equals(Rick.Enemy))
                        {
                            if (enemy.Deaths > Rick.deaths)
                            {
                                Utility.DelayAction.Add(Config.Item("delay").GetValue<Slider>().Value, sayRoll);
                                Rick.deaths = enemy.Deaths;
                            }
                        }
                    }
                }
            }
        }

        private static void sayRoll()
        {
            if(lastMessage + 1500 > Environment.TickCount)return;
            var random = new Random().Next(0, 11);
            switch (random)
            {
                case 0:
                    Game.Say("/all Falc� el fal�ma bak�yordu, elimi a�ar a�maz "sana �ok �ektirmi�ler evlad�m" dedi. Falc� hakl� beyler..");
                    break;
                case 1:
                    Game.Say("/all �nsan Doydu�u Yerde De�il , Koydu�u Yerde Mutlu Olur.");
                    break;
                case 2:
                    Game.Say("/all �apkadan Tav�an ��karamam Ama , Boxer'dan piton ��karabilirim");
                    break;
                case 3:
                    Game.Say("/all Meme ellemek stresi azalt�rm��, ama bo�al�nca.");
                    break;
                case 4:
                    Game.Say("/all Zencilerin hayata 10 santim �nde ba�lamas� �ok sa�ma.");
                    break;
                case 5:
                    Game.Say("/all Kayalardan kayar�m, yoktur benim ayar�m. gerekirse kayalara da kayar�m.");
                    break;
                case 6:
                    Game.Say("/all U�ra�ma benim gibi mankenle, girerim g.�t�ne tankerle.");
                    break;
                case 7:
                    Game.Say("/all No panik no heycan ben koyacam sen susacan.");
                    break;
                case 8:
                    Game.Say("/all Mini etek giyip kahkaha at�yorsan, yata�a girip ��gl�k atmaya mecbursun.");
                    break;
                case 9:
                    Game.Say("/all Kizlar der ki lolipop varsa dondurma getirinde y.arrak yalayak.");
                    break;
                case 10:
                    Game.Say("/all Bundan sonra burda k�f�r edilmeyecek, K�f�r edenin am�na koyar�m.");
                    break;
                case 11:
                    Game.Say("/all Seni sevmek ibadetim, ama sevemem cenabetim.");
                    break;

            }
            lastMessage = Environment.TickCount;
        }
    }



}
