using Npoi.Mapper.Attributes;

namespace HQMS.Extension.Control.Main.Models
{
    public class MasterKind
    {
        [Ignore]
        public bool IsSelected { get; set; }

        [Column("年月")]
        public string FREPORTDATESTR { get; set; }

        [Ignore]
        public string FCODE { get; set; }

        [Column("项目")]
        public string FNAME { get; set; }

        [Column("内容")]
        public string FCONTENT { get; set; }

        [Ignore]
        public string FEXTEND { get; set; }
    }
}
