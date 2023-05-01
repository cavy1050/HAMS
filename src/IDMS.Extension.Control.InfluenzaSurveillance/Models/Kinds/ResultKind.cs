using Npoi.Mapper.Attributes;

namespace IDMS.Extension.Control.InfluenzaSurveillance.Models
{
    public class ResultKind
    {
        [Column("病历号")]
        public string BLH { get; set; }

        [Column("姓名")]
        public string HZXM { get; set; }

        [Column("性别")]
        public string SEX { get; set; }

        [Column("年龄")]
        public string BRXSNL { get; set; }

        [Column("就诊日期")]
        public string JZRQ { get; set; }

        [Column("体温")]
        public string TW { get; set; }

        [Column("现病史")]
        public string XBS { get; set; }

        [Column("是否开展新冠核酸")]
        public string SFHS { get; set; }

        [Column("是否核酸阳性")]
        public string SFHSYX { get; set; }
    }
}
