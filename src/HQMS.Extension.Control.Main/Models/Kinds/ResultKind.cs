using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HQMS.Extension.Control.Main.Models
{
    public class ResultKind
    {
        /// <summary>
        /// 1	组织机构代码
        /// </summary>
        public string A01 { get; set; }

        /// <summary>
        /// 2	医疗机构名称
        /// </summary>
        public string A02 { get; set; }

        /// <summary>
        /// 3	病案号
        /// </summary>
        public string A48 { get; set; }

        /// <summary>
        /// 4	住院次数
        /// </summary>
        public int A49 { get; set; }

        /// <summary>
        /// 5	入院时间
        /// </summary>
        public DateTime B12 { get; set; }

        /// <summary>
        /// 6	出院时间
        /// </summary>
        public DateTime B15 { get; set; }

        /// <summary>
        /// 7	健康卡号
        /// </summary>
        public string A47 { get; set; }

        /// <summary>
        /// 8	医疗付费方式
        /// </summary>
        public string A46C { get; set; }

        /// <summary>
        /// 9	姓名
        /// </summary>
        public string A11 { get; set; }

        /// <summary>
        /// 10	性别
        /// </summary>
        public string A12C { get; set; }

        /// <summary>
        /// 11	出生日期
        /// </summary>
        public string A13 { get; set; }

        /// <summary>
        /// 12	年龄（岁）
        /// </summary>
        public int A14 { get; set; }

        /// <summary>
        /// 13	国籍
        /// </summary>
        public string A15C { get; set; }

        /// <summary>
        /// 14	婚姻
        /// </summary>
        public string A21C { get; set; }

        /// <summary>
        /// 15	职业
        /// </summary>
        public string A38C { get; set; }

        /// <summary>
        /// 16	民族
        /// </summary>
        public string A19C { get; set; }

        /// <summary>
        /// 17	身份证号
        /// </summary>
        public string A20 { get; set; }

        /// <summary>
        /// 18	出生地址
        /// </summary>
        public string A22 { get; set; }

        /// <summary>
        /// 19	籍贯省（自治区、直辖市）
        /// </summary>
        public string A23C { get; set; }

        /// <summary>
        /// 20	户口地址
        /// </summary>
        public string A24 { get; set; }

        /// <summary>
        /// 21	户口地址邮政编码
        /// </summary>
        public string A25C { get; set; }

        /// <summary>
        /// 22	现住址
        /// </summary>
        public string A26 { get; set; }

        /// <summary>
        /// 23	现住址电话
        /// </summary>
        public string A27 { get; set; }

        /// <summary>
        /// 24	现住址邮政编码
        /// </summary>
        public string A28C { get; set; }

        /// <summary>
        /// 25	工作单位及地址
        /// </summary>
        public string A29 { get; set; }

        /// <summary>
        /// 26	工作单位电话
        /// </summary>
        public string A30 { get; set; }

        /// <summary>
        /// 27	工作单位邮政编码
        /// </summary>
        public string A31C { get; set; }

        /// <summary>
        /// 28	联系人姓名
        /// </summary>
        public string A32 { get; set; }

        /// <summary>
        /// 29	联系人关系
        /// </summary>
        public string A33C { get; set; }

        /// <summary>
        /// 30	联系人地址
        /// </summary>
        public string A34 { get; set; }

        /// <summary>
        /// 31	联系人电话
        /// </summary>
        public string A35 { get; set; }

        /// <summary>
        /// 32	入院途径
        /// </summary>
        public string B11C { get; set; }

        /// <summary>
        /// 33	入院科别
        /// </summary>
        public string B13C { get; set; }

        /// <summary>
        /// 34	入院病房
        /// </summary>
        public string B14 { get; set; }

        /// <summary>
        /// 35	转科科别
        /// </summary>
        public string B21C { get; set; }

        /// <summary>
        /// 36	出院科别
        /// </summary>
        public string B16C { get; set; }

        /// <summary>
        /// 37	出院病房
        /// </summary>
        public string B17 { get; set; }

        /// <summary>
        /// 38	实际住院（天）
        /// </summary>
        public int B20 { get; set; }

        /// <summary>
        /// 39	门（急）诊诊断编码
        /// </summary>
        public string C01C { get; set; }

        /// <summary>
        /// 40	门（急）诊诊断名称
        /// </summary>
        public string C02N { get; set; }

        /// <summary>
        /// 41	出院主要诊断编码
        /// </summary>
        public string C03C { get; set; }

        /// <summary>
        /// 42	出院主要诊断名称
        /// </summary>
        public string C04N { get; set; }

        /// <summary>
        /// 43	出院主要诊断入院病情
        /// </summary>
        public string C05C { get; set; }

        /// <summary>
        /// 44	出院其他诊断编码1
        /// </summary>
        public string C06x01C { get; set; }

        /// <summary>
        /// 45	出院其他诊断名称1
        /// </summary>
        public string C07x01N { get; set; }

        /// <summary>
        /// 46	出院其他诊断入院病情1
        /// </summary>
        public string C08x01C { get; set; }

        /// <summary>
        /// 47	出院其他诊断编码2
        /// </summary>
        public string C06x02C { get; set; }

        /// <summary>
        /// 48	出院其他诊断名称2
        /// </summary>
        public string C07x02N { get; set; }

        /// <summary>
        /// 49	出院其他诊断入院病情2
        /// </summary>
        public string C08x02C { get; set; }

        /// <summary>
        /// 50	出院其他诊断编码3
        /// </summary>
        public string C06x03C { get; set; }

        /// <summary>
        /// 51	出院其他诊断名称3
        /// </summary>
        public string C07x03N { get; set; }

        /// <summary>
        /// 52	出院其他诊断入院病情3
        /// </summary>
        public string C08x03C { get; set; }

        /// <summary>
        /// 53	出院其他诊断编码4
        /// </summary>
        public string C06x04C { get; set; }

        /// <summary>
        /// 54	出院其他诊断名称4
        /// </summary>
        public string C07x04N { get; set; }

        /// <summary>
        /// 55	出院其他诊断入院病情4
        /// </summary>
        public string C08x04C { get; set; }

        /// <summary>
        /// 56	出院其他诊断编码5
        /// </summary>
        public string C06x05C { get; set; }

        /// <summary>
        /// 57	出院其他诊断名称5
        /// </summary>
        public string C07x05N { get; set; }

        /// <summary>
        /// 58	出院其他诊断入院病情5
        /// </summary>
        public string C08x05C { get; set; }

        /// <summary>
        /// 59	出院其他诊断编码6
        /// </summary>
        public string C06x06C { get; set; }

        /// <summary>
        /// 60	出院其他诊断名称6
        /// </summary>
        public string C07x06N { get; set; }

        /// <summary>
        /// 61	出院其他诊断入院病情6
        /// </summary>
        public string C08x06C { get; set; }

        /// <summary>
        /// 62	出院其他诊断编码7
        /// </summary>
        public string C06x07C { get; set; }

        /// <summary>
        /// 63	出院其他诊断名称7
        /// </summary>
        public string C07x07N { get; set; }

        /// <summary>
        /// 64	出院其他诊断入院病情7
        /// </summary>
        public string C08x07C { get; set; }

        /// <summary>
        /// 65	出院其他诊断编码8
        /// </summary>
        public string C06x08C { get; set; }

        /// <summary>
        /// 66	出院其他诊断名称8
        /// </summary>
        public string C07x08N { get; set; }

        /// <summary>
        /// 67	出院其他诊断入院病情8
        /// </summary>
        public string C08x08C { get; set; }

        /// <summary>
        /// 68	出院其他诊断编码9
        /// </summary>
        public string C06x09C { get; set; }

        /// <summary>
        /// 69	出院其他诊断名称9
        /// </summary>
        public string C07x09N { get; set; }

        /// <summary>
        /// 70	出院其他诊断入院病情9
        /// </summary>
        public string C08x09C { get; set; }

        /// <summary>
        /// 71	出院其他诊断编码10
        /// </summary>
        public string C06x10C { get; set; }

        /// <summary>
        /// 72	出院其他诊断名称10
        /// </summary>
        public string C07x10N { get; set; }

        /// <summary>
        /// 73	出院其他诊断入院病情10
        /// </summary>
        public string C08x10C { get; set; }

        /// <summary>
        /// 74	出院其他诊断编码11
        /// </summary>
        public string C06x11C { get; set; }

        /// <summary>
        /// 75	出院其他诊断名称11
        /// </summary>
        public string C07x11N { get; set; }

        /// <summary>
        /// 76	出院其他诊断入院病情11
        /// </summary>
        public string C08x11C { get; set; }

        /// <summary>
        /// 77	出院其他诊断编码12
        /// </summary>
        public string C06x12C { get; set; }

        /// <summary>
        /// 78	出院其他诊断名称12
        /// </summary>
        public string C07x12N { get; set; }

        /// <summary>
        /// 79	出院其他诊断入院病情12
        /// </summary>
        public string C08x12C { get; set; }

        /// <summary>
        /// 80	出院其他诊断编码13
        /// </summary>
        public string C06x13C { get; set; }

        /// <summary>
        /// 81	出院其他诊断名称13
        /// </summary>
        public string C07x13N { get; set; }

        /// <summary>
        /// 82	出院其他诊断入院病情13
        /// </summary>
        public string C08x13C { get; set; }

        /// <summary>
        /// 83	出院其他诊断编码14
        /// </summary>
        public string C06x14C { get; set; }

        /// <summary>
        /// 84	出院其他诊断名称14
        /// </summary>
        public string C07x14N { get; set; }

        /// <summary>
        /// 85	出院其他诊断入院病情14
        /// </summary>
        public string C08x14C { get; set; }

        /// <summary>
        /// 86	出院其他诊断编码15
        /// </summary>
        public string C06x15C { get; set; }

        /// <summary>
        /// 87	出院其他诊断名称15
        /// </summary>
        public string C07x15N { get; set; }

        /// <summary>
        /// 88	出院其他诊断入院病情15
        /// </summary>
        public string C08x15C { get; set; }

        /// <summary>
        /// 89	出院其他诊断编码16
        /// </summary>
        public string C06x16C { get; set; }

        /// <summary>
        /// 90	出院其他诊断名称16
        /// </summary>
        public string C07x16N { get; set; }

        /// <summary>
        /// 91	出院其他诊断入院病情16
        /// </summary>
        public string C08x16C { get; set; }

        /// <summary>
        /// 92	出院其他诊断编码17
        /// </summary>
        public string C06x17C { get; set; }

        /// <summary>
        /// 93	出院其他诊断名称17
        /// </summary>
        public string C07x17N { get; set; }

        /// <summary>
        /// 94	出院其他诊断入院病情17
        /// </summary>
        public string C08x17C { get; set; }

        /// <summary>
        /// 95	出院其他诊断编码18
        /// </summary>
        public string C06x18C { get; set; }

        /// <summary>
        /// 96	出院其他诊断名称18
        /// </summary>
        public string C07x18N { get; set; }

        /// <summary>
        /// 97	出院其他诊断入院病情18
        /// </summary>
        public string C08x18C { get; set; }

        /// <summary>
        /// 98	出院其他诊断编码19
        /// </summary>
        public string C06x19C { get; set; }

        /// <summary>
        /// 99	出院其他诊断名称19
        /// </summary>
        public string C07x19N { get; set; }

        /// <summary>
        /// 100	出院其他诊断入院病情19
        /// </summary>
        public string C08x19C { get; set; }

        /// <summary>
        /// 101	出院其他诊断编码20
        /// </summary>
        public string C06x20C { get; set; }

        /// <summary>
        /// 102	出院其他诊断名称20
        /// </summary>
        public string C07x20N { get; set; }

        /// <summary>
        /// 103	出院其他诊断入院病情20
        /// </summary>
        public string C08x20C { get; set; }

        /// <summary>
        /// 104	出院其他诊断编码21
        /// </summary>
        public string C06x21C { get; set; }

        /// <summary>
        /// 105	出院其他诊断名称21
        /// </summary>
        public string C07x21N { get; set; }

        /// <summary>
        /// 106	出院其他诊断入院病情21
        /// </summary>
        public string C08x21C { get; set; }

        /// <summary>
        /// 107	出院其他诊断编码22
        /// </summary>
        public string C06x22C { get; set; }

        /// <summary>
        /// 108	出院其他诊断名称22
        /// </summary>
        public string C07x22N { get; set; }

        /// <summary>
        /// 109	出院其他诊断入院病情22
        /// </summary>
        public string C08x22C { get; set; }

        /// <summary>
        /// 110	出院其他诊断编码23
        /// </summary>
        public string C06x23C { get; set; }

        /// <summary>
        /// 111	出院其他诊断名称23
        /// </summary>
        public string C07x23N { get; set; }

        /// <summary>
        /// 112	出院其他诊断入院病情23
        /// </summary>
        public string C08x23C { get; set; }

        /// <summary>
        /// 113	出院其他诊断编码24
        /// </summary>
        public string C06x24C { get; set; }

        /// <summary>
        /// 114	出院其他诊断名称24
        /// </summary>
        public string C07x24N { get; set; }

        /// <summary>
        /// 115	出院其他诊断入院病情24
        /// </summary>
        public string C08x24C { get; set; }

        /// <summary>
        /// 116	出院其他诊断编码25
        /// </summary>
        public string C06x25C { get; set; }

        /// <summary>
        /// 117	出院其他诊断名称25
        /// </summary>
        public string C07x25N { get; set; }

        /// <summary>
        /// 118	出院其他诊断入院病情25
        /// </summary>
        public string C08x25C { get; set; }

        /// <summary>
        /// 119	出院其他诊断编码26
        /// </summary>
        public string C06x26C { get; set; }

        /// <summary>
        /// 120	出院其他诊断名称26
        /// </summary>
        public string C07x26N { get; set; }

        /// <summary>
        /// 121	出院其他诊断入院病情26
        /// </summary>
        public string C08x26C { get; set; }

        /// <summary>
        /// 122	出院其他诊断编码27
        /// </summary>
        public string C06x27C { get; set; }

        /// <summary>
        /// 123	出院其他诊断名称27
        /// </summary>
        public string C07x27N { get; set; }

        /// <summary>
        /// 124	出院其他诊断入院病情27
        /// </summary>
        public string C08x27C { get; set; }

        /// <summary>
        /// 125	出院其他诊断编码28
        /// </summary>
        public string C06x28C { get; set; }

        /// <summary>
        /// 126	出院其他诊断名称28
        /// </summary>
        public string C07x28N { get; set; }

        /// <summary>
        /// 127	出院其他诊断入院病情28
        /// </summary>
        public string C08x28C { get; set; }

        /// <summary>
        /// 128	出院其他诊断编码29
        /// </summary>
        public string C06x29C { get; set; }

        /// <summary>
        /// 129	出院其他诊断名称29
        /// </summary>
        public string C07x29N { get; set; }

        /// <summary>
        /// 130	出院其他诊断入院病情29
        /// </summary>
        public string C08x29C { get; set; }

        /// <summary>
        /// 131	出院其他诊断编码30
        /// </summary>
        public string C06x30C { get; set; }

        /// <summary>
        /// 132	出院其他诊断名称30
        /// </summary>
        public string C07x30N { get; set; }

        /// <summary>
        /// 133	出院其他诊断入院病情30
        /// </summary>
        public string C08x30C { get; set; }

        /// <summary>
        /// 134	出院其他诊断编码31
        /// </summary>
        public string C06x31C { get; set; }

        /// <summary>
        /// 135	出院其他诊断名称31
        /// </summary>
        public string C07x31N { get; set; }

        /// <summary>
        /// 136	出院其他诊断入院病情31
        /// </summary>
        public string C08x31C { get; set; }

        /// <summary>
        /// 137	出院其他诊断编码32
        /// </summary>
        public string C06x32C { get; set; }

        /// <summary>
        /// 138	出院其他诊断名称32
        /// </summary>
        public string C07x32N { get; set; }

        /// <summary>
        /// 139	出院其他诊断入院病情32
        /// </summary>
        public string C08x32C { get; set; }

        /// <summary>
        /// 140	出院其他诊断编码33
        /// </summary>
        public string C06x33C { get; set; }

        /// <summary>
        /// 141	出院其他诊断名称33
        /// </summary>
        public string C07x33N { get; set; }

        /// <summary>
        /// 142	出院其他诊断入院病情33
        /// </summary>
        public string C08x33C { get; set; }

        /// <summary>
        /// 143	出院其他诊断编码34
        /// </summary>
        public string C06x34C { get; set; }

        /// <summary>
        /// 144	出院其他诊断名称34
        /// </summary>
        public string C07x34N { get; set; }

        /// <summary>
        /// 145	出院其他诊断入院病情34
        /// </summary>
        public string C08x34C { get; set; }

        /// <summary>
        /// 146	出院其他诊断编码35
        /// </summary>
        public string C06x35C { get; set; }

        /// <summary>
        /// 147	出院其他诊断名称35
        /// </summary>
        public string C07x35N { get; set; }

        /// <summary>
        /// 148	出院其他诊断入院病情35
        /// </summary>
        public string C08x35C { get; set; }

        /// <summary>
        /// 149	出院其他诊断编码36
        /// </summary>
        public string C06x36C { get; set; }

        /// <summary>
        /// 150 出院其他诊断名称36
        /// </summary>
        public string C07x36N { get; set; }

        /// <summary>
        /// 151	出院其他诊断入院病情36
        /// </summary>
        public string C08x36C { get; set; }

        /// <summary>
        /// 152	出院其他诊断编码37
        /// </summary>
        public string C06x37C { get; set; }

        /// <summary>
        /// 153 出院其他诊断名称37
        /// </summary>
        public string C07x37N { get; set; }

        /// <summary>
        /// 154	出院其他诊断入院病情37
        /// </summary>
        public string C08x37C { get; set; }

        /// <summary>
        /// 155	出院其他诊断编码38
        /// </summary>
        public string C06x38C { get; set; }

        /// <summary>
        /// 156 出院其他诊断名称38
        /// </summary>
        public string C07x38N { get; set; }

        /// <summary>
        /// 157	出院其他诊断入院病情38
        /// </summary>
        public string C08x38C { get; set; }

        /// <summary>
        /// 158	出院其他诊断编码39
        /// </summary>
        public string C06x39C { get; set; }

        /// <summary>
        /// 159 出院其他诊断名称39
        /// </summary>
        public string C07x39N { get; set; }

        /// <summary>
        /// 160	出院其他诊断入院病情39
        /// </summary>
        public string C08x39C { get; set; }

        /// <summary>
        /// 161	出院其他诊断编码40
        /// </summary>
        public string C06x40C { get; set; }

        /// <summary>
        /// 162 出院其他诊断名称40
        /// </summary>
        public string C07x40N { get; set; }

        /// <summary>
        /// 163	出院其他诊断入院病情40
        /// </summary>
        public string C08x40C { get; set; }

        /// <summary>
        /// 164	病理诊断编码
        /// </summary>
        public string C09C { get; set; }

        /// <summary>
        /// 165	病理诊断名称
        /// </summary>
        public string C10N { get; set; }

        /// <summary>
        /// 166	病理号
        /// </summary>
        public string C11 { get; set; }

        /// <summary>
        /// 167	损伤、中毒外部原因编码
        /// </summary>
        public string C12C { get; set; }

        /// <summary>
        /// 168	损伤、中毒外部原因名称
        /// </summary>
        public string C13N { get; set; }

        /// <summary>
        /// 169	有无药物过敏
        /// </summary>
        public string C24C { get; set; }

        /// <summary>
        /// 170	过敏药物名称
        /// </summary>
        public string C25 { get; set; }

        /// <summary>
        /// 171	科主任
        /// </summary>
        public string B22 { get; set; }

        /// <summary>
        /// 172	主（副主）任医师
        /// </summary>
        public string B23 { get; set; }

        /// <summary>
        /// 173	主治医师
        /// </summary>
        public string B24 { get; set; }

        /// <summary>
        /// 174	住院医师
        /// </summary>
        public string B25 { get; set; }

        /// <summary>
        /// 175	责任护士
        /// </summary>
        public string B26 { get; set; }

        /// <summary>
        /// 176	进修医师
        /// </summary>
        public string B27 { get; set; }

        /// <summary>
        /// 177	实习医师
        /// </summary>
        public string B28 { get; set; }

        /// <summary>
        /// 178	编码员
        /// </summary>
        public string B29 { get; set; }

        /// <summary>
        /// 179	病案质量
        /// </summary>
        public string B30C { get; set; }

        /// <summary>
        /// 180	质控医师
        /// </summary>
        public string B31 { get; set; }

        /// <summary>
        /// 181	质控护师
        /// </summary>
        public string B32 { get; set; }

        /// <summary>
        /// 182	质控日期
        /// </summary>
        public DateTime B33 { get; set; }

        /// <summary>
        /// 183	死亡患者尸检
        /// </summary>
        public string C34C { get; set; }

        /// <summary>
        /// 184	ABO血型
        /// </summary>
        public string C26C { get; set; }

        /// <summary>
        /// 185	Rh血型
        /// </summary>
        public string C27C { get; set; }

        /// <summary>
        /// 186	主要手术操作编码
        /// </summary>
        public string C14x01C { get; set; }

        /// <summary>
        /// 187 主要手术操作名称
        /// </summary>
        public string C15x01N { get; set; }

        /// <summary>
        /// 188 主要手术操作日期
        /// </summary>
        public String C16x01 { get; set; }

        /// <summary>
        /// 189 主要手术操作级别
        /// </summary>
        public string C17x01 { get; set; }

        /// <summary>
        /// 190 主要手术操作术者
        /// </summary>
        public string C18x01 { get; set; }

        /// <summary>
        /// 191 主要手术操作Ⅰ助
        /// </summary>
        public string C19x01 { get; set; }

        /// <summary>
        /// 192 主要手术操作Ⅱ助
        /// </summary>
        public string C20x01 { get; set; }

        /// <summary>
        /// 193 主要手术操作切口愈合等级
        /// </summary>
        public string C21x01C { get; set; }

        /// <summary>
        /// 194 主要手术操作麻醉方式
        /// </summary>
        public string C22x01C { get; set; }

        /// <summary>
        /// 195 主要手术操作麻醉医师
        /// </summary>
        public string C23x01 { get; set; }

        /// <summary>
        /// 196 其他手术操作编码1
        /// </summary>
        public string C35x01C { get; set; }

        /// <summary>
        /// 197 其他手术操作名称1
        /// </summary>
        public string C36x01N { get; set; }

        /// <summary>
        /// 198 其他手术操作日期1
        /// </summary>
        public String C37x01 { get; set; }

        /// <summary>
        /// 199 其他手术操作级别1
        /// </summary>
        public string C38x01 { get; set; }

        /// <summary>
        /// 200 其他手术操作术者1
        /// </summary>
        public string C39x01 { get; set; }

        /// <summary>
        /// 201 其他手术操作Ⅰ助1
        /// </summary>
        public string C40x01 { get; set; }

        /// <summary>
        /// 202 其他手术操作Ⅱ助1
        /// </summary>
        public string C41x01 { get; set; }

        /// <summary>
        /// 203 其他手术操作切口愈合等级1
        /// </summary>
        public string C42x01C { get; set; }

        /// <summary>
        /// 204 其他手术操作麻醉方式1
        /// </summary>
        public string C43x01C { get; set; }

        /// <summary>
        /// 205 其他手术操作麻醉医师1
        /// </summary>
        public string C44x01 { get; set; }

        /// <summary>
        /// 206 其他手术操作编码2
        /// </summary>
        public string C35x02C { get; set; }

        /// <summary>
        /// 207 其他手术操作名称2
        /// </summary>
        public string C36x02N { get; set; }

        /// <summary>
        /// 208 其他手术操作日期2
        /// </summary>
        public String C37x02 { get; set; }

        /// <summary>
        /// 209 其他手术操作级别2
        /// </summary>
        public string C38x02 { get; set; }

        /// <summary>
        /// 210 其他手术操作术者2
        /// </summary>
        public string C39x02 { get; set; }

        /// <summary>
        /// 211 其他手术操作Ⅰ助2
        /// </summary>
        public string C40x02 { get; set; }

        /// <summary>
        /// 212 其他手术操作Ⅱ助2
        /// </summary>
        public string C41x02 { get; set; }

        /// <summary>
        /// 213 其他手术操作切口愈合等级2
        /// </summary>
        public string C42x02C { get; set; }

        /// <summary>
        /// 214 其他手术操作麻醉方式2
        /// </summary>
        public string C43x02C { get; set; }

        /// <summary>
        /// 215 其他手术操作麻醉医师2
        /// </summary>
        public string C44x02 { get; set; }

        /// <summary>
        /// 216 其他手术操作编码3
        /// </summary>
        public string C35x03C { get; set; }

        /// <summary>
        /// 217 其他手术操作名称3
        /// </summary>
        public string C36x03N { get; set; }

        /// <summary>
        /// 218 其他手术操作日期3
        /// </summary>
        public String C37x03 { get; set; }

        /// <summary>
        /// 219 其他手术操作级别3
        /// </summary>
        public string C38x03 { get; set; }

        /// <summary>
        /// 220 其他手术操作术者3
        /// </summary>
        public string C39x03 { get; set; }

        /// <summary>
        /// 221 其他手术操作Ⅰ助3
        /// </summary>
        public string C40x03 { get; set; }

        /// <summary>
        /// 212 其他手术操作Ⅱ助3
        /// </summary>
        public string C41x03 { get; set; }

        /// <summary>
        /// 223 其他手术操作切口愈合等级3
        /// </summary>
        public string C42x03C { get; set; }

        /// <summary>
        /// 224 其他手术操作麻醉方式3
        /// </summary>
        public string C43x03C { get; set; }

        /// <summary>
        /// 225 其他手术操作麻醉医师3
        /// </summary>
        public string C44x03 { get; set; }

        /// <summary>
        /// 226 其他手术操作编码4
        /// </summary>
        public string C35x04C { get; set; }

        /// <summary>
        /// 227 其他手术操作名称4
        /// </summary>
        public string C36x04N { get; set; }

        /// <summary>
        /// 228 其他手术操作日期4
        /// </summary>
        public String C37x04 { get; set; }

        /// <summary>
        /// 229 其他手术操作级别4
        /// </summary>
        public string C38x04 { get; set; }

        /// <summary>
        /// 230 其他手术操作术者4
        /// </summary>
        public string C39x04 { get; set; }

        /// <summary>
        /// 231 其他手术操作Ⅰ助4
        /// </summary>
        public string C40x04 { get; set; }

        /// <summary>
        /// 232 其他手术操作Ⅱ助4
        /// </summary>
        public string C41x04 { get; set; }

        /// <summary>
        /// 233 其他手术操作切口愈合等级4
        /// </summary>
        public string C42x04C { get; set; }

        /// <summary>
        /// 234 其他手术操作麻醉方式4
        /// </summary>
        public string C43x04C { get; set; }

        /// <summary>
        /// 235 其他手术操作麻醉医师4
        /// </summary>
        public string C44x04 { get; set; }

        /// <summary>
        /// 236 其他手术操作编码5
        /// </summary>
        public string C35x05C { get; set; }

        /// <summary>
        /// 237 其他手术操作名称5
        /// </summary>
        public string C36x05N { get; set; }

        /// <summary>
        /// 238 其他手术操作日期5
        /// </summary>
        public String C37x05 { get; set; }

        /// <summary>
        /// 239 其他手术操作级别5
        /// </summary>
        public string C38x05 { get; set; }

        /// <summary>
        /// 240 其他手术操作术者5
        /// </summary>
        public string C39x05 { get; set; }

        /// <summary>
        /// 241 其他手术操作Ⅰ助5
        /// </summary>
        public string C40x05 { get; set; }

        /// <summary>
        /// 242 其他手术操作Ⅱ助5
        /// </summary>
        public string C41x05 { get; set; }

        /// <summary>
        /// 243 其他手术操作切口愈合等级5
        /// </summary>
        public string C42x05C { get; set; }

        /// <summary>
        /// 244 其他手术操作麻醉方式5
        /// </summary>
        public string C43x05C { get; set; }

        /// <summary>
        /// 245 其他手术操作麻醉医师5
        /// </summary>
        public string C44x05 { get; set; }

        /// <summary>
        /// 246 其他手术操作编码6
        /// </summary>
        public string C35x06C { get; set; }

        /// <summary>
        /// 247 其他手术操作名称6
        /// </summary>
        public string C36x06N { get; set; }

        /// <summary>
        /// 248 其他手术操作日期6
        /// </summary>
        public String C37x06 { get; set; }

        /// <summary>
        /// 249 其他手术操作级别6
        /// </summary>
        public string C38x06 { get; set; }

        /// <summary>
        /// 250 其他手术操作术者6
        /// </summary>
        public string C39x06 { get; set; }

        /// <summary>
        /// 251 其他手术操作Ⅰ助6
        /// </summary>
        public string C40x06 { get; set; }

        /// <summary>
        /// 252 其他手术操作Ⅱ助6
        /// </summary>
        public string C41x06 { get; set; }

        /// <summary>
        /// 253 其他手术操作切口愈合等级6
        /// </summary>
        public string C42x06C { get; set; }

        /// <summary>
        /// 254 其他手术操作麻醉方式6
        /// </summary>
        public string C43x06C { get; set; }

        /// <summary>
        /// 255 其他手术操作麻醉医师6
        /// </summary>
        public string C44x06 { get; set; }

        /// <summary>
        /// 256 其他手术操作编码7
        /// </summary>
        public string C35x07C { get; set; }

        /// <summary>
        /// 257 其他手术操作名称7
        /// </summary>
        public string C36x07N { get; set; }

        /// <summary>
        /// 258 其他手术操作日期7
        /// </summary>
        public String C37x07 { get; set; }

        /// <summary>
        /// 259 其他手术操作级别7
        /// </summary>
        public string C38x07 { get; set; }

        /// <summary>
        /// 260 其他手术操作术者7
        /// </summary>
        public string C39x07 { get; set; }

        /// <summary>
        /// 261 其他手术操作Ⅰ助7
        /// </summary>
        public string C40x07 { get; set; }

        /// <summary>
        /// 262 其他手术操作Ⅱ助7
        /// </summary>
        public string C41x07 { get; set; }

        /// <summary>
        /// 263 其他手术操作切口愈合等级7
        /// </summary>
        public string C42x07C { get; set; }

        /// <summary>
        /// 264 其他手术操作麻醉方式7
        /// </summary>
        public string C43x07C { get; set; }

        /// <summary>
        /// 265 其他手术操作麻醉医师7
        /// </summary>
        public string C44x07 { get; set; }

        /// <summary>
        /// 266 其他手术操作编码8
        /// </summary>
        public string C35x08C { get; set; }

        /// <summary>
        /// 267 其他手术操作名称8
        /// </summary>
        public string C36x08N { get; set; }

        /// <summary>
        /// 268 其他手术操作日期8
        /// </summary>
        public String C37x08 { get; set; }

        /// <summary>
        /// 269 其他手术操作级别8
        /// </summary>
        public string C38x08 { get; set; }

        /// <summary>
        /// 270 其他手术操作术者8
        /// </summary>
        public string C39x08 { get; set; }

        /// <summary>
        /// 271 其他手术操作Ⅰ助8
        /// </summary>
        public string C40x08 { get; set; }

        /// <summary>
        /// 272 其他手术操作Ⅱ助8
        /// </summary>
        public string C41x08 { get; set; }

        /// <summary>
        /// 273 其他手术操作切口愈合等级8
        /// </summary>
        public string C42x08C { get; set; }

        /// <summary>
        /// 274 其他手术操作麻醉方式8
        /// </summary>
        public string C43x08C { get; set; }

        /// <summary>
        /// 275 其他手术操作麻醉医师8
        /// </summary>
        public string C44x08 { get; set; }

        /// <summary>
        /// 276 其他手术操作编码9
        /// </summary>
        public string C35x09C { get; set; }

        /// <summary>
        /// 277 其他手术操作名称9
        /// </summary>
        public string C36x09N { get; set; }

        /// <summary>
        /// 278 其他手术操作日期9
        /// </summary>
        public String C37x09 { get; set; }

        /// <summary>
        /// 279 其他手术操作级别9
        /// </summary>
        public string C38x09 { get; set; }

        /// <summary>
        /// 280 其他手术操作术者9
        /// </summary>
        public string C39x09 { get; set; }

        /// <summary>
        /// 281 其他手术操作Ⅰ助9
        /// </summary>
        public string C40x09 { get; set; }

        /// <summary>
        /// 282 其他手术操作Ⅱ助9
        /// </summary>
        public string C41x09 { get; set; }

        /// <summary>
        /// 283 其他手术操作切口愈合等级9
        /// </summary>
        public string C42x09C { get; set; }

        /// <summary>
        /// 284 其他手术操作麻醉方式9
        /// </summary>
        public string C43x09C { get; set; }

        /// <summary>
        /// 285 其他手术操作麻醉医师9
        /// </summary>
        public string C44x09 { get; set; }

        /// <summary>
        /// 286 其他手术操作编码10
        /// </summary>
        public string C35x10C { get; set; }

        /// <summary>
        /// 287 其他手术操作名称10
        /// </summary>
        public string C36x10N { get; set; }

        /// <summary>
        /// 288 其他手术操作日期10
        /// </summary>
        public String C37x10 { get; set; }

        /// <summary>
        /// 289 其他手术操作级别10
        /// </summary>
        public string C38x10 { get; set; }

        /// <summary>
        /// 290 其他手术操作术者10
        /// </summary>
        public string C39x10 { get; set; }

        /// <summary>
        /// 291 其他手术操作Ⅰ助10
        /// </summary>
        public string C40x10 { get; set; }

        /// <summary>
        /// 292 其他手术操作Ⅱ助10
        /// </summary>
        public string C41x10 { get; set; }

        /// <summary>
        /// 293 其他手术操作切口愈合等级10
        /// </summary>
        public string C42x10C { get; set; }

        /// <summary>
        /// 294 其他手术操作麻醉方式10
        /// </summary>
        public string C43x10C { get; set; }

        /// <summary>
        /// 295 其他手术操作麻醉医师10
        /// </summary>
        public string C44x10 { get; set; }

        /// <summary>
        /// 296 其他手术操作编码11
        /// </summary>
        public string C35x11C { get; set; }

        /// <summary>
        /// 297 其他手术操作名称11
        /// </summary>
        public string C36x11N { get; set; }

        /// <summary>
        /// 298 其他手术操作日期11
        /// </summary>
        public String C37x11 { get; set; }

        /// <summary>
        /// 299 其他手术操作级别11
        /// </summary>
        public string C38x11 { get; set; }

        /// <summary>
        /// 300 其他手术操作术者11
        /// </summary>
        public string C39x11 { get; set; }

        /// <summary>
        /// 301 其他手术操作Ⅰ助11
        /// </summary>
        public string C40x11 { get; set; }

        /// <summary>
        /// 302 其他手术操作Ⅱ助11
        /// </summary>
        public string C41x11 { get; set; }

        /// <summary>
        /// 303 其他手术操作切口愈合等级11
        /// </summary>
        public string C42x11C { get; set; }

        /// <summary>
        /// 304 其他手术操作麻醉方式11
        /// </summary>
        public string C43x11C { get; set; }

        /// <summary>
        /// 305 其他手术操作麻醉医师11
        /// </summary>
        public string C44x11 { get; set; }

        /// <summary>
        /// 306 其他手术操作编码12
        /// </summary>
        public string C35x12C { get; set; }

        /// <summary>
        /// 307 其他手术操作名称12
        /// </summary>
        public string C36x12N { get; set; }

        /// <summary>
        /// 308 其他手术操作日期12
        /// </summary>
        public String C37x12 { get; set; }

        /// <summary>
        /// 309 其他手术操作级别12
        /// </summary>
        public string C38x12 { get; set; }

        /// <summary>
        /// 310 其他手术操作术者12
        /// </summary>
        public string C39x12 { get; set; }

        /// <summary>
        /// 311 其他手术操作Ⅰ助12
        /// </summary>
        public string C40x12 { get; set; }

        /// <summary>
        /// 312 其他手术操作Ⅱ助12
        /// </summary>
        public string C41x12 { get; set; }

        /// <summary>
        /// 313 其他手术操作切口愈合等级12
        /// </summary>
        public string C42x12C { get; set; }

        /// <summary>
        /// 314 其他手术操作麻醉方式12
        /// </summary>
        public string C43x12C { get; set; }

        /// <summary>
        /// 315 其他手术操作麻醉医师12
        /// </summary>
        public string C44x12 { get; set; }

        /// <summary>
        /// 316 其他手术操作编码13
        /// </summary>
        public string C35x13C { get; set; }

        /// <summary>
        /// 317 其他手术操作名称13
        /// </summary>
        public string C36x13N { get; set; }

        /// <summary>
        /// 318 其他手术操作日期13
        /// </summary>
        public String C37x13 { get; set; }

        /// <summary>
        /// 319 其他手术操作级别13
        /// </summary>
        public string C38x13 { get; set; }

        /// <summary>
        /// 320 其他手术操作术者13
        /// </summary>
        public string C39x13 { get; set; }

        /// <summary>
        /// 321 其他手术操作Ⅰ助13
        /// </summary>
        public string C40x13 { get; set; }

        /// <summary>
        /// 322 其他手术操作Ⅱ助13
        /// </summary>
        public string C41x13 { get; set; }

        /// <summary>
        /// 323 其他手术操作切口愈合等级13
        /// </summary>
        public string C42x13C { get; set; }

        /// <summary>
        /// 324 其他手术操作麻醉方式13
        /// </summary>
        public string C43x13C { get; set; }

        /// <summary>
        /// 325 其他手术操作麻醉医师13
        /// </summary>
        public string C44x13 { get; set; }

        /// <summary>
        /// 326 其他手术操作编码14
        /// </summary>
        public string C35x14C { get; set; }

        /// <summary>
        /// 327 其他手术操作名称14
        /// </summary>
        public string C36x14N { get; set; }

        /// <summary>
        /// 328 其他手术操作日期14
        /// </summary>
        public String C37x14 { get; set; }

        /// <summary>
        /// 329 其他手术操作级别14
        /// </summary>
        public string C38x14 { get; set; }

        /// <summary>
        /// 330 其他手术操作术者14
        /// </summary>
        public string C39x14 { get; set; }

        /// <summary>
        /// 331 其他手术操作Ⅰ助14
        /// </summary>
        public string C40x14 { get; set; }

        /// <summary>
        /// 332 其他手术操作Ⅱ助14
        /// </summary>
        public string C41x14 { get; set; }

        /// <summary>
        /// 333 其他手术操作切口愈合等级14
        /// </summary>
        public string C42x14C { get; set; }

        /// <summary>
        /// 334 其他手术操作麻醉方式14
        /// </summary>
        public string C43x14C { get; set; }

        /// <summary>
        /// 335 其他手术操作麻醉医师14
        /// </summary>
        public string C44x14 { get; set; }

        /// <summary>
        /// 336 其他手术操作编码15
        /// </summary>
        public string C35x15C { get; set; }

        /// <summary>
        /// 337 其他手术操作名称15
        /// </summary>
        public string C36x15N { get; set; }

        /// <summary>
        /// 338 其他手术操作日期15
        /// </summary>
        public String C37x15 { get; set; }

        /// <summary>
        /// 339 其他手术操作级别15
        /// </summary>
        public string C38x15 { get; set; }

        /// <summary>
        /// 340 其他手术操作术者15
        /// </summary>
        public string C39x15 { get; set; }

        /// <summary>
        /// 341 其他手术操作Ⅰ助15
        /// </summary>
        public string C40x15 { get; set; }

        /// <summary>
        /// 342 其他手术操作Ⅱ助15
        /// </summary>
        public string C41x15 { get; set; }

        /// <summary>
        /// 343 其他手术操作切口愈合等级15
        /// </summary>
        public string C42x15C { get; set; }

        /// <summary>
        /// 344 其他手术操作麻醉方式15
        /// </summary>
        public string C43x15C { get; set; }

        /// <summary>
        /// 345 其他手术操作麻醉医师15
        /// </summary>
        public string C44x15 { get; set; }

        /// <summary>
        /// 346 其他手术操作编码16
        /// </summary>
        public string C35x16C { get; set; }

        /// <summary>
        /// 347 其他手术操作名称16
        /// </summary>
        public string C36x16N { get; set; }

        /// <summary>
        /// 348 其他手术操作日期16
        /// </summary>
        public String C37x16 { get; set; }

        /// <summary>
        /// 349 其他手术操作级别16
        /// </summary>
        public string C38x16 { get; set; }

        /// <summary>
        /// 350 其他手术操作术者16
        /// </summary>
        public string C39x16 { get; set; }

        /// <summary>
        /// 351 其他手术操作Ⅰ助16
        /// </summary>
        public string C40x16 { get; set; }

        /// <summary>
        /// 352 其他手术操作Ⅱ助16
        /// </summary>
        public string C41x16 { get; set; }

        /// <summary>
        /// 353 其他手术操作切口愈合等级16
        /// </summary>
        public string C42x16C { get; set; }

        /// <summary>
        /// 354 其他手术操作麻醉方式16
        /// </summary>
        public string C43x16C { get; set; }

        /// <summary>
        /// 355 其他手术操作麻醉医师16
        /// </summary>
        public string C44x16 { get; set; }

        /// <summary>
        /// 356 其他手术操作编码17
        /// </summary>
        public string C35x17C { get; set; }

        /// <summary>
        /// 357 其他手术操作名称17
        /// </summary>
        public string C36x17N { get; set; }

        /// <summary>
        /// 358 其他手术操作日期17
        /// </summary>
        public String C37x17 { get; set; }

        /// <summary>
        /// 359 其他手术操作级别17
        /// </summary>
        public string C38x17 { get; set; }

        /// <summary>
        /// 360 其他手术操作术者17
        /// </summary>
        public string C39x17 { get; set; }

        /// <summary>
        /// 361 其他手术操作Ⅰ助17
        /// </summary>
        public string C40x17 { get; set; }

        /// <summary>
        /// 362 其他手术操作Ⅱ助17
        /// </summary>
        public string C41x17 { get; set; }

        /// <summary>
        /// 363 其他手术操作切口愈合等级17
        /// </summary>
        public string C42x17C { get; set; }

        /// <summary>
        /// 364 其他手术操作麻醉方式17
        /// </summary>
        public string C43x17C { get; set; }

        /// <summary>
        /// 365 其他手术操作麻醉医师17
        /// </summary>
        public string C44x17 { get; set; }

        /// <summary>
        /// 366 其他手术操作编码18
        /// </summary>
        public string C35x18C { get; set; }

        /// <summary>
        /// 367 其他手术操作名称18
        /// </summary>
        public string C36x18N { get; set; }

        /// <summary>
        /// 368 其他手术操作日期18
        /// </summary>
        public String C37x18 { get; set; }

        /// <summary>
        /// 369 其他手术操作级别18
        /// </summary>
        public string C38x18 { get; set; }

        /// <summary>
        /// 370 其他手术操作术者18
        /// </summary>
        public string C39x18 { get; set; }

        /// <summary>
        /// 371 其他手术操作Ⅰ助18
        /// </summary>
        public string C40x18 { get; set; }

        /// <summary>
        /// 372 其他手术操作Ⅱ助18
        /// </summary>
        public string C41x18 { get; set; }

        /// <summary>
        /// 373 其他手术操作切口愈合等级18
        /// </summary>
        public string C42x18C { get; set; }

        /// <summary>
        /// 374 其他手术操作麻醉方式18
        /// </summary>
        public string C43x18C { get; set; }

        /// <summary>
        /// 375 其他手术操作麻醉医师18
        /// </summary>
        public string C44x18 { get; set; }

        /// <summary>
        /// 376 其他手术操作编码19
        /// </summary>
        public string C35x19C { get; set; }

        /// <summary>
        /// 377 其他手术操作名称19
        /// </summary>
        public string C36x19N { get; set; }

        /// <summary>
        /// 378 其他手术操作日期19
        /// </summary>
        public String C37x19 { get; set; }

        /// <summary>
        /// 379 其他手术操作级别19
        /// </summary>
        public string C38x19 { get; set; }

        /// <summary>
        /// 380 其他手术操作术者19
        /// </summary>
        public string C39x19 { get; set; }

        /// <summary>
        /// 381 其他手术操作Ⅰ助19
        /// </summary>
        public string C40x19 { get; set; }

        /// <summary>
        /// 382 其他手术操作Ⅱ助19
        /// </summary>
        public string C41x19 { get; set; }

        /// <summary>
        /// 383 其他手术操作切口愈合等级19
        /// </summary>
        public string C42x19C { get; set; }

        /// <summary>
        /// 384 其他手术操作麻醉方式19
        /// </summary>
        public string C43x19C { get; set; }

        /// <summary>
        /// 385 其他手术操作麻醉医师19
        /// </summary>
        public string C44x19 { get; set; }

        /// <summary>
        /// 386 其他手术操作编码20
        /// </summary>
        public string C35x20C { get; set; }

        /// <summary>
        /// 387 其他手术操作名称20
        /// </summary>
        public string C36x20N { get; set; }

        /// <summary>
        /// 388 其他手术操作日期20
        /// </summary>
        public String C37x20 { get; set; }

        /// <summary>
        /// 389 其他手术操作级别20
        /// </summary>
        public string C38x20 { get; set; }

        /// <summary>
        /// 390 其他手术操作术者20
        /// </summary>
        public string C39x20 { get; set; }

        /// <summary>
        /// 391 其他手术操作Ⅰ助20
        /// </summary>
        public string C40x20 { get; set; }

        /// <summary>
        /// 392 其他手术操作Ⅱ助20
        /// </summary>
        public string C41x20 { get; set; }

        /// <summary>
        /// 393 其他手术操作切口愈合等级20
        /// </summary>
        public string C42x20C { get; set; }

        /// <summary>
        /// 394 其他手术操作麻醉方式20
        /// </summary>
        public string C43x20C { get; set; }

        /// <summary>
        /// 395 其他手术操作麻醉医师20
        /// </summary>
        public string C44x20 { get; set; }

        /// <summary>
        /// 396 其他手术操作编码21
        /// </summary>
        public string C35x21C { get; set; }

        /// <summary>
        /// 397 其他手术操作名称21
        /// </summary>
        public string C36x21N { get; set; }

        /// <summary>
        /// 398 其他手术操作日期21
        /// </summary>
        public String C37x21 { get; set; }

        /// <summary>
        /// 399 其他手术操作级别21
        /// </summary>
        public string C38x21 { get; set; }

        /// <summary>
        /// 400 其他手术操作术者21
        /// </summary>
        public string C39x21 { get; set; }

        /// <summary>
        /// 401 其他手术操作Ⅰ助21
        /// </summary>
        public string C40x21 { get; set; }

        /// <summary>
        /// 402 其他手术操作Ⅱ助21
        /// </summary>
        public string C41x21 { get; set; }

        /// <summary>
        /// 403 其他手术操作切口愈合等级21
        /// </summary>
        public string C42x21C { get; set; }

        /// <summary>
        /// 404 其他手术操作麻醉方式21
        /// </summary>
        public string C43x21C { get; set; }

        /// <summary>
        /// 405 其他手术操作麻醉医师21
        /// </summary>
        public string C44x21 { get; set; }

        /// 406 其他手术操作编码22
        /// </summary>
        public string C35x22C { get; set; }

        /// <summary>
        /// 407 其他手术操作名称22
        /// </summary>
        public string C36x22N { get; set; }

        /// <summary>
        /// 408 其他手术操作日期22
        /// </summary>
        public String C37x22 { get; set; }

        /// <summary>
        /// 409 其他手术操作级别22
        /// </summary>
        public string C38x22 { get; set; }

        /// <summary>
        /// 410 其他手术操作术者22
        /// </summary>
        public string C39x22 { get; set; }

        /// <summary>
        /// 411 其他手术操作Ⅰ助22
        /// </summary>
        public string C40x22 { get; set; }

        /// <summary>
        /// 412 其他手术操作Ⅱ助22
        /// </summary>
        public string C41x22 { get; set; }

        /// <summary>
        /// 413 其他手术操作切口愈合等级22
        /// </summary>
        public string C42x22C { get; set; }

        /// <summary>
        /// 414 其他手术操作麻醉方式22
        /// </summary>
        public string C43x22C { get; set; }

        /// <summary>
        /// 415 其他手术操作麻醉医师22
        /// </summary>
        public string C44x22 { get; set; }

        /// 416 其他手术操作编码23
        /// </summary>
        public string C35x23C { get; set; }

        /// <summary>
        /// 417 其他手术操作名称23
        /// </summary>
        public string C36x23N { get; set; }

        /// <summary>
        /// 418 其他手术操作日期23
        /// </summary>
        public String C37x23 { get; set; }

        /// <summary>
        /// 419 其他手术操作级别23
        /// </summary>
        public string C38x23 { get; set; }

        /// <summary>
        /// 420 其他手术操作术者23
        /// </summary>
        public string C39x23 { get; set; }

        /// <summary>
        /// 421 其他手术操作Ⅰ助23
        /// </summary>
        public string C40x23 { get; set; }

        /// <summary>
        /// 422 其他手术操作Ⅱ助23
        /// </summary>
        public string C41x23 { get; set; }

        /// <summary>
        /// 423 其他手术操作切口愈合等级23
        /// </summary>
        public string C42x23C { get; set; }

        /// <summary>
        /// 424 其他手术操作麻醉方式23
        /// </summary>
        public string C43x23C { get; set; }

        /// <summary>
        /// 425 其他手术操作麻醉医师23
        /// </summary>
        public string C44x23 { get; set; }

        /// 426 其他手术操作编码24
        /// </summary>
        public string C35x24C { get; set; }

        /// <summary>
        /// 427 其他手术操作名称24
        /// </summary>
        public string C36x24N { get; set; }

        /// <summary>
        /// 428 其他手术操作日期24
        /// </summary>
        public String C37x24 { get; set; }

        /// <summary>
        /// 429 其他手术操作级别24
        /// </summary>
        public string C38x24 { get; set; }

        /// <summary>
        /// 430 其他手术操作术者24
        /// </summary>
        public string C39x24 { get; set; }

        /// <summary>
        /// 431 其他手术操作Ⅰ助24
        /// </summary>
        public string C40x24 { get; set; }

        /// <summary>
        /// 432 其他手术操作Ⅱ助24
        /// </summary>
        public string C41x24 { get; set; }

        /// <summary>
        /// 433 其他手术操作切口愈合等级24
        /// </summary>
        public string C42x24C { get; set; }

        /// <summary>
        /// 434 其他手术操作麻醉方式24
        /// </summary>
        public string C43x24C { get; set; }

        /// <summary>
        /// 435 其他手术操作麻醉医师24
        /// </summary>
        public string C44x24 { get; set; }

        /// 436 其他手术操作编码25
        /// </summary>
        public string C35x25C { get; set; }

        /// <summary>
        /// 437 其他手术操作名称25
        /// </summary>
        public string C36x25N { get; set; }

        /// <summary>
        /// 438 其他手术操作日期25
        /// </summary>
        public String C37x25 { get; set; }

        /// <summary>
        /// 439 其他手术操作级别25
        /// </summary>
        public string C38x25 { get; set; }

        /// <summary>
        /// 440 其他手术操作术者25
        /// </summary>
        public string C39x25 { get; set; }

        /// <summary>
        /// 441 其他手术操作Ⅰ助25
        /// </summary>
        public string C40x25 { get; set; }

        /// <summary>
        /// 442 其他手术操作Ⅱ助25
        /// </summary>
        public string C41x25 { get; set; }

        /// <summary>
        /// 443 其他手术操作切口愈合等级25
        /// </summary>
        public string C42x25C { get; set; }

        /// <summary>
        /// 444 其他手术操作麻醉方式25
        /// </summary>
        public string C43x25C { get; set; }

        /// <summary>
        /// 445 其他手术操作麻醉医师25
        /// </summary>
        public string C44x25 { get; set; }

        /// 446 其他手术操作编码26
        /// </summary>
        public string C35x26C { get; set; }

        /// <summary>
        /// 447 其他手术操作名称26
        /// </summary>
        public string C36x26N { get; set; }

        /// <summary>
        /// 448 其他手术操作日期26
        /// </summary>
        public String C37x26 { get; set; }

        /// <summary>
        /// 449 其他手术操作级别26
        /// </summary>
        public string C38x26 { get; set; }

        /// <summary>
        /// 450 其他手术操作术者26
        /// </summary>
        public string C39x26 { get; set; }

        /// <summary>
        /// 451 其他手术操作Ⅰ助26
        /// </summary>
        public string C40x26 { get; set; }

        /// <summary>
        /// 452 其他手术操作Ⅱ助26
        /// </summary>
        public string C41x26 { get; set; }

        /// <summary>
        /// 453 其他手术操作切口愈合等级26
        /// </summary>
        public string C42x26C { get; set; }

        /// <summary>
        /// 454 其他手术操作麻醉方式26
        /// </summary>
        public string C43x26C { get; set; }

        /// <summary>
        /// 455 其他手术操作麻醉医师26
        /// </summary>
        public string C44x26 { get; set; }

        /// 456 其他手术操作编码27
        /// </summary>
        public string C35x27C { get; set; }

        /// <summary>
        /// 457 其他手术操作名称27
        /// </summary>
        public string C36x27N { get; set; }

        /// <summary>
        /// 458 其他手术操作日期27
        /// </summary>
        public String C37x27 { get; set; }

        /// <summary>
        /// 459 其他手术操作级别27
        /// </summary>
        public string C38x27 { get; set; }

        /// <summary>
        /// 460 其他手术操作术者27
        /// </summary>
        public string C39x27 { get; set; }

        /// <summary>
        /// 461 其他手术操作Ⅰ助27
        /// </summary>
        public string C40x27 { get; set; }

        /// <summary>
        /// 462 其他手术操作Ⅱ助27
        /// </summary>
        public string C41x27 { get; set; }

        /// <summary>
        /// 463 其他手术操作切口愈合等级27
        /// </summary>
        public string C42x27C { get; set; }

        /// <summary>
        /// 464 其他手术操作麻醉方式27
        /// </summary>
        public string C43x27C { get; set; }

        /// <summary>
        /// 465 其他手术操作麻醉医师27
        /// </summary>
        public string C44x27 { get; set; }

        /// 466 其他手术操作编码28
        /// </summary>
        public string C35x28C { get; set; }

        /// <summary>
        /// 467 其他手术操作名称28
        /// </summary>
        public string C36x28N { get; set; }

        /// <summary>
        /// 468 其他手术操作日期28
        /// </summary>
        public String C37x28 { get; set; }

        /// <summary>
        /// 469 其他手术操作级别28
        /// </summary>
        public string C38x28 { get; set; }

        /// <summary>
        /// 470 其他手术操作术者28
        /// </summary>
        public string C39x28 { get; set; }

        /// <summary>
        /// 471 其他手术操作Ⅰ助28
        /// </summary>
        public string C40x28 { get; set; }

        /// <summary>
        /// 472 其他手术操作Ⅱ助28
        /// </summary>
        public string C41x28 { get; set; }

        /// <summary>
        /// 473 其他手术操作切口愈合等级28
        /// </summary>
        public string C42x28C { get; set; }

        /// <summary>
        /// 474 其他手术操作麻醉方式28
        /// </summary>
        public string C43x28C { get; set; }

        /// <summary>
        /// 475 其他手术操作麻醉医师28
        /// </summary>
        public string C44x28 { get; set; }

        /// 476 其他手术操作编码29
        /// </summary>
        public string C35x29C { get; set; }

        /// <summary>
        /// 477 其他手术操作名称29
        /// </summary>
        public string C36x29N { get; set; }

        /// <summary>
        /// 478 其他手术操作日期29
        /// </summary>
        public String C37x29 { get; set; }

        /// <summary>
        /// 479 其他手术操作级别29
        /// </summary>
        public string C38x29 { get; set; }

        /// <summary>
        /// 480 其他手术操作术者29
        /// </summary>
        public string C39x29 { get; set; }

        /// <summary>
        /// 481 其他手术操作Ⅰ助29
        /// </summary>
        public string C40x29 { get; set; }

        /// <summary>
        /// 482 其他手术操作Ⅱ助29
        /// </summary>
        public string C41x29 { get; set; }

        /// <summary>
        /// 483 其他手术操作切口愈合等级29
        /// </summary>
        public string C42x29C { get; set; }

        /// <summary>
        /// 484 其他手术操作麻醉方式29
        /// </summary>
        public string C43x29C { get; set; }

        /// <summary>
        /// 485 其他手术操作麻醉医师29
        /// </summary>
        public string C44x29 { get; set; }

        /// 486 其他手术操作编码30
        /// </summary>
        public string C35x30C { get; set; }

        /// <summary>
        /// 487 其他手术操作名称30
        /// </summary>
        public string C36x30N { get; set; }

        /// <summary>
        /// 488 其他手术操作日期30
        /// </summary>
        public String C37x30 { get; set; }

        /// <summary>
        /// 489 其他手术操作级别30
        /// </summary>
        public string C38x30 { get; set; }

        /// <summary>
        /// 490 其他手术操作术者30
        /// </summary>
        public string C39x30 { get; set; }

        /// <summary>
        /// 491 其他手术操作Ⅰ助30
        /// </summary>
        public string C40x30 { get; set; }

        /// <summary>
        /// 492 其他手术操作Ⅱ助30
        /// </summary>
        public string C41x30 { get; set; }

        /// <summary>
        /// 493 其他手术操作切口愈合等级30
        /// </summary>
        public string C42x30C { get; set; }

        /// <summary>
        /// 494 其他手术操作麻醉方式30
        /// </summary>
        public string C43x30C { get; set; }

        /// <summary>
        /// 495 其他手术操作麻醉医师30
        /// </summary>
        public string C44x30 { get; set; }

        /// 496 其他手术操作编码31
        /// </summary>
        public string C35x31C { get; set; }

        /// <summary>
        /// 497 其他手术操作名称31
        /// </summary>
        public string C36x31N { get; set; }

        /// <summary>
        /// 498 其他手术操作日期31
        /// </summary>
        public String C37x31 { get; set; }

        /// <summary>
        /// 499 其他手术操作级别31
        /// </summary>
        public string C38x31 { get; set; }

        /// <summary>
        /// 500 其他手术操作术者31
        /// </summary>
        public string C39x31 { get; set; }

        /// <summary>
        /// 501 其他手术操作Ⅰ助31
        /// </summary>
        public string C40x31 { get; set; }

        /// <summary>
        /// 502 其他手术操作Ⅱ助31
        /// </summary>
        public string C41x31 { get; set; }

        /// <summary>
        /// 503 其他手术操作切口愈合等级31
        /// </summary>
        public string C42x31C { get; set; }

        /// <summary>
        /// 504 其他手术操作麻醉方式31
        /// </summary>
        public string C43x31C { get; set; }

        /// <summary>
        /// 505 其他手术操作麻醉医师31
        /// </summary>
        public string C44x31 { get; set; }

        /// 506 其他手术操作编码32
        /// </summary>
        public string C35x32C { get; set; }

        /// <summary>
        /// 507 其他手术操作名称32
        /// </summary>
        public string C36x32N { get; set; }

        /// <summary>
        /// 508 其他手术操作日期32
        /// </summary>
        public String C37x32 { get; set; }

        /// <summary>
        /// 509 其他手术操作级别32
        /// </summary>
        public string C38x32 { get; set; }

        /// <summary>
        /// 510 其他手术操作术者32
        /// </summary>
        public string C39x32 { get; set; }

        /// <summary>
        /// 511 其他手术操作Ⅰ助32
        /// </summary>
        public string C40x32 { get; set; }

        /// <summary>
        /// 512 其他手术操作Ⅱ助32
        /// </summary>
        public string C41x32 { get; set; }

        /// <summary>
        /// 513 其他手术操作切口愈合等级32
        /// </summary>
        public string C42x32C { get; set; }

        /// <summary>
        /// 514 其他手术操作麻醉方式32
        /// </summary>
        public string C43x32C { get; set; }

        /// <summary>
        /// 515 其他手术操作麻醉医师32
        /// </summary>
        public string C44x32 { get; set; }

        /// 516 其他手术操作编码33
        /// </summary>
        public string C35x33C { get; set; }

        /// <summary>
        /// 517 其他手术操作名称33
        /// </summary>
        public string C36x33N { get; set; }

        /// <summary>
        /// 518 其他手术操作日期33
        /// </summary>
        public String C37x33 { get; set; }

        /// <summary>
        /// 519 其他手术操作级别33
        /// </summary>
        public string C38x33 { get; set; }

        /// <summary>
        /// 520 其他手术操作术者33
        /// </summary>
        public string C39x33 { get; set; }

        /// <summary>
        /// 521 其他手术操作Ⅰ助33
        /// </summary>
        public string C40x33 { get; set; }

        /// <summary>
        /// 522 其他手术操作Ⅱ助33
        /// </summary>
        public string C41x33 { get; set; }

        /// <summary>
        /// 523 其他手术操作切口愈合等级33
        /// </summary>
        public string C42x33C { get; set; }

        /// <summary>
        /// 524 其他手术操作麻醉方式33
        /// </summary>
        public string C43x33C { get; set; }

        /// <summary>
        /// 525 其他手术操作麻醉医师33
        /// </summary>
        public string C44x33 { get; set; }

        /// 526 其他手术操作编码34
        /// </summary>
        public string C35x34C { get; set; }

        /// <summary>
        /// 527 其他手术操作名称34
        /// </summary>
        public string C36x34N { get; set; }

        /// <summary>
        /// 528 其他手术操作日期34
        /// </summary>
        public String C37x34 { get; set; }

        /// <summary>
        /// 529 其他手术操作级别34
        /// </summary>
        public string C38x34 { get; set; }

        /// <summary>
        /// 530 其他手术操作术者34
        /// </summary>
        public string C39x34 { get; set; }

        /// <summary>
        /// 531 其他手术操作Ⅰ助34
        /// </summary>
        public string C40x34 { get; set; }

        /// <summary>
        /// 532 其他手术操作Ⅱ助34
        /// </summary>
        public string C41x34 { get; set; }

        /// <summary>
        /// 533 其他手术操作切口愈合等级34
        /// </summary>
        public string C42x34C { get; set; }

        /// <summary>
        /// 534 其他手术操作麻醉方式34
        /// </summary>
        public string C43x34C { get; set; }

        /// <summary>
        /// 535 其他手术操作麻醉医师34
        /// </summary>
        public string C44x34 { get; set; }

        /// 536 其他手术操作编码35
        /// </summary>
        public string C35x35C { get; set; }

        /// <summary>
        /// 537 其他手术操作名称35
        /// </summary>
        public string C36x35N { get; set; }

        /// <summary>
        /// 538 其他手术操作日期35
        /// </summary>
        public String C37x35 { get; set; }

        /// <summary>
        /// 539 其他手术操作级别35
        /// </summary>
        public string C38x35 { get; set; }

        /// <summary>
        /// 540 其他手术操作术者35
        /// </summary>
        public string C39x35 { get; set; }

        /// <summary>
        /// 541 其他手术操作Ⅰ助35
        /// </summary>
        public string C40x35 { get; set; }

        /// <summary>
        /// 542 其他手术操作Ⅱ助35
        /// </summary>
        public string C41x35 { get; set; }

        /// <summary>
        /// 543 其他手术操作切口愈合等级35
        /// </summary>
        public string C42x35C { get; set; }

        /// <summary>
        /// 544 其他手术操作麻醉方式35
        /// </summary>
        public string C43x35C { get; set; }

        /// <summary>
        /// 545 其他手术操作麻醉医师35
        /// </summary>
        public string C44x35 { get; set; }

        /// 546 其他手术操作编码36
        /// </summary>
        public string C35x36C { get; set; }

        /// <summary>
        /// 547 其他手术操作名称36
        /// </summary>
        public string C36x36N { get; set; }

        /// <summary>
        /// 548 其他手术操作日期36
        /// </summary>
        public String C37x36 { get; set; }

        /// <summary>
        /// 549 其他手术操作级别36
        /// </summary>
        public string C38x36 { get; set; }

        /// <summary>
        /// 550 其他手术操作术者36
        /// </summary>
        public string C39x36 { get; set; }

        /// <summary>
        /// 551 其他手术操作Ⅰ助36
        /// </summary>
        public string C40x36 { get; set; }

        /// <summary>
        /// 552 其他手术操作Ⅱ助36
        /// </summary>
        public string C41x36 { get; set; }

        /// <summary>
        /// 553 其他手术操作切口愈合等级36
        /// </summary>
        public string C42x36C { get; set; }

        /// <summary>
        /// 554 其他手术操作麻醉方式36
        /// </summary>
        public string C43x36C { get; set; }

        /// <summary>
        /// 555 其他手术操作麻醉医师36
        /// </summary>
        public string C44x36 { get; set; }

        /// 556 其他手术操作编码37
        /// </summary>
        public string C35x37C { get; set; }

        /// <summary>
        /// 557 其他手术操作名称37
        /// </summary>
        public string C36x37N { get; set; }

        /// <summary>
        /// 558 其他手术操作日期37
        /// </summary>
        public String C37x37 { get; set; }

        /// <summary>
        /// 559 其他手术操作级别37
        /// </summary>
        public string C38x37 { get; set; }

        /// <summary>
        /// 560 其他手术操作术者37
        /// </summary>
        public string C39x37 { get; set; }

        /// <summary>
        /// 561 其他手术操作Ⅰ助37
        /// </summary>
        public string C40x37 { get; set; }

        /// <summary>
        /// 562 其他手术操作Ⅱ助37
        /// </summary>
        public string C41x37 { get; set; }

        /// <summary>
        /// 563 其他手术操作切口愈合等级37
        /// </summary>
        public string C42x37C { get; set; }

        /// <summary>
        /// 564 其他手术操作麻醉方式37
        /// </summary>
        public string C43x37C { get; set; }

        /// <summary>
        /// 565 其他手术操作麻醉医师37
        /// </summary>
        public string C44x37 { get; set; }

        /// 566 其他手术操作编码38
        /// </summary>
        public string C35x38C { get; set; }

        /// <summary>
        /// 567 其他手术操作名称38
        /// </summary>
        public string C36x38N { get; set; }

        /// <summary>
        /// 568 其他手术操作日期38
        /// </summary>
        public String C37x38 { get; set; }

        /// <summary>
        /// 569 其他手术操作级别38
        /// </summary>
        public string C38x38 { get; set; }

        /// <summary>
        /// 570 其他手术操作术者38
        /// </summary>
        public string C39x38 { get; set; }

        /// <summary>
        /// 571 其他手术操作Ⅰ助38
        /// </summary>
        public string C40x38 { get; set; }

        /// <summary>
        /// 572 其他手术操作Ⅱ助38
        /// </summary>
        public string C41x38 { get; set; }

        /// <summary>
        /// 573 其他手术操作切口愈合等级38
        /// </summary>
        public string C42x38C { get; set; }

        /// <summary>
        /// 574 其他手术操作麻醉方式38
        /// </summary>
        public string C43x38C { get; set; }

        /// <summary>
        /// 575 其他手术操作麻醉医师38
        /// </summary>
        public string C44x38 { get; set; }

        /// 576 其他手术操作编码39
        /// </summary>
        public string C35x39C { get; set; }

        /// <summary>
        /// 577 其他手术操作名称39
        /// </summary>
        public string C36x39N { get; set; }

        /// <summary>
        /// 578 其他手术操作日期39
        /// </summary>
        public String C37x39 { get; set; }

        /// <summary>
        /// 579 其他手术操作级别39
        /// </summary>
        public string C38x39 { get; set; }

        /// <summary>
        /// 580 其他手术操作术者39
        /// </summary>
        public string C39x39 { get; set; }

        /// <summary>
        /// 581 其他手术操作Ⅰ助39
        /// </summary>
        public string C40x39 { get; set; }

        /// <summary>
        /// 582 其他手术操作Ⅱ助39
        /// </summary>
        public string C41x39 { get; set; }

        /// <summary>
        /// 583 其他手术操作切口愈合等级39
        /// </summary>
        public string C42x39C { get; set; }

        /// <summary>
        /// 584 其他手术操作麻醉方式39
        /// </summary>
        public string C43x39C { get; set; }

        /// <summary>
        /// 585 其他手术操作麻醉医师39
        /// </summary>
        public string C44x39 { get; set; }

        /// 586 其他手术操作编码40
        /// </summary>
        public string C35x40C { get; set; }

        /// <summary>
        /// 587 其他手术操作名称40
        /// </summary>
        public string C36x40N { get; set; }

        /// <summary>
        /// 588 其他手术操作日期40
        /// </summary>
        public String C37x40 { get; set; }

        /// <summary>
        /// 589 其他手术操作级别40
        /// </summary>
        public string C38x40 { get; set; }

        /// <summary>
        /// 590 其他手术操作术者40
        /// </summary>
        public string C39x40 { get; set; }

        /// <summary>
        /// 591 其他手术操作Ⅰ助40
        /// </summary>
        public string C40x40 { get; set; }

        /// <summary>
        /// 592 其他手术操作Ⅱ助40
        /// </summary>
        public string C41x40 { get; set; }

        /// <summary>
        /// 593 其他手术操作切口愈合等级40
        /// </summary>
        public string C42x40C { get; set; }

        /// <summary>
        /// 594 其他手术操作麻醉方式40
        /// </summary>
        public string C43x40C { get; set; }

        /// <summary>
        /// 595 其他手术操作麻醉医师40
        /// </summary>
        public string C44x40 { get; set; }

        /// <summary>
        /// 596 年龄不足1周岁的年龄（天）
        /// </summary>
        public int A16 { get; set; }

        /// <summary>
        /// 597 新生儿出生体重(克)
        /// </summary>
        public int A18x01 { get; set; }

        /// <summary>
        /// 598 新生儿出生体重(克)2
        /// </summary>
        public int A18x02 { get; set; }

        /// <summary>
        /// 599 新生儿出生体重(克)3
        /// </summary>
        public int A18x03 { get; set; }

        /// <summary>
        /// 600 新生儿出生体重(克)4
        /// </summary>
        public int A18x04 { get; set; }

        /// <summary>
        /// 601 新生儿出生体重(克)5
        /// </summary>
        public int A18x05 { get; set; }

        /// <summary>
        /// 602 新生儿入院体重（克）
        /// </summary>
        public int A17 { get; set; }

        /// <summary>
        /// 603 颅脑损伤患者入院前昏迷时间（天）
        /// </summary>
        public int C28 { get; set; }

        /// <summary>
        /// 604 颅脑损伤患者入院前昏迷时间(小时)
        /// </summary>
        public int C29 { get; set; }

        /// <summary>
        /// 605 颅脑损伤患者入院前昏迷时间(分钟)
        /// </summary>
        public int C30 { get; set; }

        /// <summary>
        /// 606 颅脑损伤患者入院后昏迷时间（天）
        /// </summary>
        public int C31 { get; set; }

        /// <summary>
        /// 607 颅脑损伤患者入院后昏迷时间(小时)
        /// </summary>
        public int C32 { get; set; }

        /// <summary>
        /// 608 颅脑损伤患者入院后昏迷时间(分钟)
        /// </summary>
        public int C33 { get; set; }

        /// <summary>
        /// 609 是否有出院31日内再住院计划
        /// </summary>
        public int B36C { get; set; }

        /// <summary>
        /// 610 出院31天再住院计划目的
        /// </summary>
        public string B37 { get; set; }

        /// <summary>
        /// 611 离院方式
        /// </summary>
        public string B34C { get; set; }

        /// <summary>
        /// 612 医嘱转院、转社区卫生服务机构/乡镇卫生院名称
        /// </summary>
        public string B35 { get; set; }

        /// <summary>
        /// 613 住院总费用
        /// </summary>
        public decimal D01 { get; set; }

        /// <summary>
        /// 614 住院总费用其中自付金额
        /// </summary>
        public decimal D09 { get; set; }

        /// <summary>
        /// 615 1.一般医疗服务费
        /// </summary>
        public decimal D11 { get; set; }

        /// <summary>
        /// 616 2.一般治疗操作费
        /// </summary>
        public decimal D12 { get; set; }

        /// <summary>
        /// 617 3.护理费
        /// </summary>
        public decimal D13 { get; set; }

        /// <summary>
        /// 618 4.综合医疗服务类其他费用
        /// </summary>
        public decimal D14 { get; set; }

        /// <summary>
        /// 619 5.病理诊断费
        /// </summary>
        public decimal D15 { get; set; }

        /// <summary>
        /// 620 6.实验室诊断费
        /// </summary>
        public decimal D16 { get; set; }

        /// <summary>
        /// 621 7.影像学诊断费
        /// </summary>
        public decimal D17 { get; set; }

        /// <summary>
        /// 622 8.临床诊断项目费
        /// </summary>
        public decimal D18 { get; set; }

        /// <summary>
        /// 623 9.非手术治疗项目费
        /// </summary>
        public decimal D19 { get; set; }

        /// <summary>
        /// 624 其中：临床物理治疗费
        /// </summary>
        public decimal D19x01 { get; set; }

        /// <summary>
        /// 625 10.手术治疗费
        /// </summary>
        public decimal D20 { get; set; }

        /// <summary>
        /// 626 其中：麻醉费
        /// </summary>
        public decimal D20x01 { get; set; }

        /// <summary>
        /// 627 其中：手术费
        /// </summary>
        public decimal D20x02 { get; set; }

        /// <summary>
        /// 628 11.康复费
        /// </summary>
        public decimal D21 { get; set; }

        /// <summary>
        /// 629 12.中医治疗费
        /// </summary>
        public decimal D22 { get; set; }

        /// <summary>
        /// 630 13.西药费
        /// </summary>
        public decimal D23 { get; set; }

        /// <summary>
        /// 631 其中：抗菌药物费
        /// </summary>
        public decimal D23x01 { get; set; }

        /// <summary>
        /// 632 14.中成药费
        /// </summary>
        public decimal D24 { get; set; }

        /// <summary>
        /// 633 15.中草药费
        /// </summary>
        public decimal D25 { get; set; }

        /// <summary>
        /// 634 16.血费
        /// </summary>
        public decimal D26 { get; set; }

        /// <summary>
        /// 635 17.白蛋白类制品费
        /// </summary>
        public decimal D27 { get; set; }

        /// <summary>
        /// 636 18.球蛋白类制品费
        /// </summary>
        public decimal D28 { get; set; }

        /// <summary>
        /// 637 19.凝血因子类制品费
        /// </summary>
        public decimal D29 { get; set; }

        /// <summary>
        /// 638 20.细胞因子类制品费
        /// </summary>
        public decimal D30 { get; set; }

        /// <summary>
        /// 640 21.检查用一次性医用材料费
        /// </summary>
        public decimal D31 { get; set; }

        /// <summary>
        /// 641 22.治疗用一次性医用材料费
        /// </summary>
        public decimal D32 { get; set; }

        /// <summary>
        /// 642 23.手术用一次性医用材料费
        /// </summary>
        public decimal D33 { get; set; }

        /// <summary>
        /// 643 24.其他费
        /// </summary>
        public decimal D34 { get; set; }
    }
}
