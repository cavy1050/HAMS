using System;
using System.ComponentModel;
using Prism.Mvvm;
using Npoi.Mapper.Attributes;

namespace HQMS.Extension.Control.Main.Models
{
    public class MasterKind : BindableBase
    {
        [Ignore]
        public bool IsSelected { get; set; }

        [Column(PropertyName = "年月")]
        public string FREPORTDATESTR { get; set; }

        [Ignore]
        public string FCODE { get; set; }

        [Column(PropertyName = "项目")]
        public string FNAME { get; set; }

        [Column(PropertyName = "内容")]
        public string FCONTENT { get; set; }

        [Ignore]
        public string FEXTEND { get; set; }
    }
}
