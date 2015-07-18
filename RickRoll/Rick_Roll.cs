using System;
using LeagueSharp;


namespace RickRoll
{
    class Rick_Roll
    {
        public int deaths;
        public String Enemy;

        public Rick_Roll(Obj_AI_Hero Enemy)
        {
            deaths = Enemy.Deaths;
            this.Enemy = Enemy.Name;
        }
    }
}
