## Module Communication Protocol Definition  
The two types `RequestServiceEvent`,`ResponseServiceEvent` inherit from `PubSubEvent<string>` provide module communication support.The detail is as follow.

### Request Message Format  
| Serial Number | Identification | Name | Type | Length | Memo |
| :--           | :--            | :--  | :--  | :--    | :--  |
| 1 | svc_code      | request service number  | string | 4    | see the service difinition     |
| 2 | msg_id        | message number          | string | 30   | time(yyyyMMddHHmmss)+random No.(4) |
| 3 | souc_mod_name | source module name      | string | 50   |      |
| 4 | tagt_mod_name | target module name      | string | 50   |      |
| 5 | svc_cont      | request service content | string | 8000 |      |

### Response Message Format  
| Serial Number | Identification | Name | Type | Length | Memo |
| :--           | :--            | :--  | :--  | :--    | :--  |
| 1 | svc_code      | response service number  | string       | 4    |      |
| 2 | msg_id        | message number           | string       | 30   |      |
| 3 | souc_mod_name | source module name       | string       | 50   |      |
| 4 | tagt_mod_name | target module name       | string array | 50   |      |
| 5 | ret_code      | response service number  | string       | 4    | 1:success 0:fail |
| 6 | ret_msg       | error message            | string       | 500  |      |
| 7 | svc_cont      | response service content | string       | 8000 |      |
 
### Module Communication Service Definition
| Serial Number | Code | Name | Description |
| :--           | :--  | :--  | :--         |
| 1 | 1101 | ApplictionVerificationService  | validate app licence |
| 2 | 1201 | ApplicationStatusService       | change app status    |
|   |
| 3 | 2101 | AccountVerificationService   | validate account password  |
| 4 | 2201 | AccountAuthenticationService | get account rights | 

