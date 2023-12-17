using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Kyrsach.Core
{
    [Serializable]
    public struct Record:IComparable
    {
        [JsonInclude]
        public int score;
        [JsonInclude]
        public string name;
        public Record(int score , string name)
        {
            this.score = score; 
            this.name = name;
        }
        public int CompareTo(object obj)
        {
            try
            {
                Record otherRecord = (Record)obj;
                if (this.score > otherRecord.score)
                    return -1;
                else
                    return 1;
            }
            catch
            {
                throw new ArgumentException("Object is not a Record");
            }
        }
    }
    public class Records
    {
        private Records() { }
        private static Records s_Instance;
        public static Records GetInstance()
        {
            return s_Instance = (s_Instance != null) ? s_Instance : new Records();
        }
        private List<Record> CompanyRecords = new List<Record>(5);
        private List<Record> NotEndRecords = new List<Record>(5);
        public void SetCompanyRecords(List<Record> companyRecords)
        {
            CompanyRecords = companyRecords;
        }
        public void SetNotEndRecords(List<Record> notEndRecords)
        {
            NotEndRecords = notEndRecords;
        }
        public List<Record> GetCompanyRecords()
        {
            return CompanyRecords;
        }
        public List<Record> GetNotEndRecords()
        {
            return NotEndRecords;
        }
        public void AddCompanyRecords(int score,string name)
        {
            CompanyRecords.Add(new Record(score,name));
            CompanyRecords.Sort();
            if (CompanyRecords.Count > 4)
                CompanyRecords.RemoveAt(4);
        }
        public void AddNotEndRecords(int score, string name)
        {
            NotEndRecords.Add(new Record(score, name));
            NotEndRecords.Sort();
            if (NotEndRecords.Count > 4)
                NotEndRecords.RemoveAt(4);
        }
    }
}
