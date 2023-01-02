## Module Communication Protocol Definition  

### Request Message Format
| Serial Number | Identification | Name | Type | Length | Dictionary Identification |
| :----         | :----          | :----| :----| :----  | :----                     |
| 1 | svc_code | 请求服务代码  | 字符型 |  2  |         |
| 2 | svc_name | 请求服务名称  | 字符型 |  10 |         |
| 3 | svc_desc | 请求服务描述  | 字符型 |  50 |         |
| 4 | souc_mod_name | 源模块名称   | 字符型 |  50 |         |
| 5 | tagt_mod_name | 目标模块名称   | 字符型 |  50 |         |
| 6 | svc_cont |  请求内容     | 字符型 |  8000 |         |

### Response Message Format  
 
### Module Communication Service Definition
