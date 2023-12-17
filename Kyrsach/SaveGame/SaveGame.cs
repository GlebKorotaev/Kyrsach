using Kyrsach.Core;
using Kyrsach.Core.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Kyrsach.SaveGame
{

   
    [Serializable]
    public class SaveGame
    {
        [JsonInclude]
        public int m_NumberLevel;
        [JsonInclude]
        public Properties player;
        [JsonInclude]
        public List<Properties> enemys;
        [JsonInclude]
        public List<Properties> enemysAttacks;
        [JsonInclude]
        public List<Properties> playerAttacks;
        [JsonInclude]
        public int Score;
        [JsonInclude]
        public int Wave;
        [JsonInclude]
        public List<Record> CompanyRecords;
        [JsonInclude]
        public  List<Record> NotEndRecords;
        public SaveGame(int m_NumberLevel, Properties player, List<Properties> enemys, List<Properties> enemysAttacks, List<Properties> playerAttacks, int score,List<Record> CompanyRecords, List<Record> NotEndRecords, int wave)
        {
            this.m_NumberLevel = m_NumberLevel;
            this.player = player;
            this.enemys = enemys;
            this.enemysAttacks = enemysAttacks;
            this.playerAttacks = playerAttacks;
            Score = score;
            this.CompanyRecords = CompanyRecords;
            this.NotEndRecords = NotEndRecords;
            Wave = wave;
        }
    }
}
