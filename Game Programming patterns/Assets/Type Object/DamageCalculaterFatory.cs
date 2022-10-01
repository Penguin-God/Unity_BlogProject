using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageCalculater
{
    int CalculateDamage(int damage, Player player, Monster monster);
}

public class DamageCalculaterFatory
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
            if (monster.Type.Level - player.Level >= 20)
                damage /= 2;
            return damage;
        }
    }

    class ArcaneDamageCalculater : IDamageCalculater
    {
        int _arcanePorce = 30; // TODO : 어디선가 몬스터 정보 이용해서 가져오기

        public int CalculateDamage(int damage, Player player, Monster monster)
        {
            damage = new LevelDamageCalculater().CalculateDamage(damage, player, monster);
            
            if (_arcanePorce > player.ArcanePorce)
                damage /= 2;
            return damage;
        }
    }
}
