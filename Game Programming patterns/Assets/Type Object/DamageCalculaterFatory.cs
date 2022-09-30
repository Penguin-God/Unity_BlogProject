using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageCalculater
{
    int CalculateDamage(int damage, Player player, Monster monster);
}

public class DamageCalculaterFatory : MonoBehaviour
{
    public IDamageCalculater GetCalculater(MonsterRegion region)
    {
        switch (region)
        {   
            case MonsterRegion.MapleWorld: return new LevelDamageCalculater(); 
            case MonsterRegion.ArcaneRiver: return new ArcaneDamageCalculater(); 
        }
        return null;
    }

    class LevelDamageCalculater : IDamageCalculater
    {
        public int CalculateDamage(int damage, Player player, Monster monster)
        {
            if (monster.Level - player.Level >= 20)
                damage /= 2;
            return damage;
        }
    }

    class ArcaneDamageCalculater : IDamageCalculater
    {
        public int CalculateDamage(int damage, Player player, Monster monster)
        {
            damage = new LevelDamageCalculater().CalculateDamage(damage, player, monster);
            monster._arcanePorce = 30;
            if (monster._arcanePorce > player.ArcanePorce)
                damage /= 2;
            return damage;
        }
    }
}
