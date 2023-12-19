namespace FamilyTree.BLL
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class SpecialRecordInformation
    {
        public SpecialRecordInformation()
        {
        }

        public SpecialRecordInformation(DAL.Models.SpecialRecord specialRecord)
        {
            this.Id = specialRecord.Id;
            this.RecordType = specialRecord.RecordType;
            this.HouseNumber = specialRecord.HouseNumber;
            this.Priest = specialRecord.Priest;
            this.Record = specialRecord.Record;
            this.EventId = specialRecord.EventId;
        }

        public int Id { get; set; }

        public string RecordType { get; set; }

        public int? HouseNumber { get; set; }

        public string Priest { get; set; }

        public string Record { get; set; }

        public int EventId { get; set; }

        public string FullRecordType
        {
            get
            {
                switch (this.RecordType)
                {
                    case "metric book":
                        return "Метрична книга";
                    case "confessional record":
                        return "Сповідний розпис";
                    case "census record":
                        return "Ревізька казка";
                    case "population census":
                        return "Перепис населення";
                    default:
                        return "Невідомо";
                }
            }
        }
    }
}
