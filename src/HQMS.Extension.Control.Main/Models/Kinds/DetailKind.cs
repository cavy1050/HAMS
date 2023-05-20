using Npoi.Mapper.Attributes;

namespace HQMS.Extension.Control.Main.Models
{
    public class DetailKind
    {
        [Column("年月")]
        public string FREPORTDATESTR { get; set; }

        [Column("住院号")]
        public string A48 { get; set; }

        [Column("姓名")]
        public string A11 { get; set; }

        [Column("年龄（岁）")]
        public string A14 { get; set; }

        [Column("入院时间")]
        public string B12 { get; set; }

        [Column("出院时间")]
        public string B15 { get; set; }

        [Column("实际住院（天）")]
        public string B20 { get; set; }

        [Column("出院主要诊断编码")]
        public string C03C { get; set; }

        [Column("出院主要诊断名称")]
        public string C04N { get; set; }

        [Column("主要手术操作编码")]
        public string C14x01C { get; set; }

        [Column("主要手术操作名称")]
        public string C15x01N { get; set; }

        [Column("主要手术操作日期")]
        public string C16x01 { get; set; }

        [Column("主要手术操作级别")]
        public string C17x01 { get; set; }

        [Column("主要手术操作术者")]
        public string C18x01 { get; set; }

        [Column("主要手术操作切口愈合等级")]
        public string C21x01C { get; set; }

        [Column("主要手术操作麻醉方式")]
        public string C22x01C { get; set; }

        [Column("入院科别")]
        public string B13C { get; set; }

        [Column("出院科别")]
        public string B16C { get; set; }

        [Column("离院方式")]
        public string B34C { get; set; }

        [Column("住院总费用")]
        public string D01 { get; set; }
    }
}
