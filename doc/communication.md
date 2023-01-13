#  Module Communication Protocol Definition  
The two types `RequestServiceEvent`,`ResponseServiceEvent` inherit from `PubSubEvent<string>` provide module communication support.The detail is as follow.

##  Request Message Format  
| Number | Identification | Name | Type | Length | Description |
| :-- | :-- | :-- | :-- | :-- | :-- |
| 1 | svc_code | request service number | string | 4  | see the service definition |
| 2 | svc_name | request service name   | string | 50 | used for filter service |
| 3 | msg_code | message number         | string | 30 | ULID number |
| 4 | souc_mod_name | source module name | string | 50 | |
| 5 | tagt_mod_name | target module name | string | 50 | |
| 6 | svc_cont | request service content | string | 8000 | see the service content definition     |

##  Response Message Format  
| Number | Identification | Name | Type | Length | Memo |
| :-- | :-- | :-- | :-- | :-- | :-- |
| 1 | svc_code      | response service number  | string       | 4    |      |
| 2 | svc_name      | request service name     | string       | 50   |      |
| 3 | msg_code      | message number           | string       | 30   |      |
| 4 | souc_mod_name | source module name       | string       | 50   |      |
| 5 | tagt_mod_name | target module name       | string array | 50   |      |
| 6 | svc_cont      | response service content | string       | 8000 |      |
| 7 | ret_code      | response service number  | string       | 4    | 1:success 0:fail |
| 8 | ret_msg       | error message            | string       | 500  |      |

 
##  Module Communication Service Definition
| Number | Code | Name | Description |
| :-- | :-- | :-- | :-- |
| 1 | 1101 | ApplicationAlterationService  | change app status    |
| 2 | 1102 | ApplictionVerificationService | validate app licence |
|   |
| 3 | 2101 | AccountVerificationService   | validate account password  |
| 4 | 2102 | AccountAuthenticationService | get account rights         | 
|   |
| 5 | 3101 | MenuInitializationService | get account authorized menu list | 
| 6 | 3102 | MenuActivationService     | active menu item                 | 

##   Module Communication Service Content Definition
###  `1101` ApplicationAlterationService
- Request String Format  

| Number | Parameter Code | Parameter Type | Parameter Length | Dictionary | Description |
| :-- | :-- | :-- | :-- | :-- | :-- |
| 1 | app_ctl_type | string | 2 | Y | |
| 2 | app_act_flag | string | 2 | Y | |

- Response String Format  

Empty

###  `2101` AccountVerificationService
- Request String Format  

| Number | Parameter Code | Parameter Type | Parameter Length | Dictionary | Description |
| :-- | :-- | :-- | :-- | :-- | :-- |
| 1 | acct_code | string | 8 | | |
| 2 | acct_pwd  | string | 8 | | |

- Response String Format 

| Number | Parameter Code | Parameter Type | Parameter Length | Dictionary | Description |
| :-- | :-- | :-- | :-- | :-- | :-- |
| 1 | acct_code | string | 8 | | |
| 2 | acct_pwd  | string | 8 | | |
| 3 | acct_name | string | 8 | | |

###  `3101` MenuInitializationService
- Request String Format  

| Number | Parameter Code | Parameter Type | Parameter Length | Dictionary | Description |
| :-- | :-- | :-- | :-- | :-- | :-- |
| 1 | acct_code  | string | 8 | | |

- Response String Format ( Note Identification : menus)

| Number | Parameter Code | Parameter Type | Parameter Length | Dictionary | Description |
| :-- | :-- | :-- | :-- | :-- | :-- |
| 1 | menu_code       | string | 30  | | |
| 2 | menu_name       | string | 30  | | |
| 3 | menu_super_code | string | 30  | | |
| 4 | menu_super_name | string | 30  | | |
| 5 | menu_mod_name   | string | 100 | | module name |
| 6 | menu_mod_ref    | string | 100 | | module assembly name |
| 7 | menu_mod_type   | string | 500 | | module fully qualified name |

###  `3102` MenuActivationService
- Request String Format  

| Number | Parameter Code | Parameter Type | Parameter Length | Dictionary | Description |
| :-- | :-- | :-- | :-- | :-- | :-- |
| 1 | menu_code       | string | 30  | | |
| 2 | menu_name       | string | 30  | | |
| 3 | menu_mod_name   | string | 100 | | module name |
| 4 | menu_mod_ref    | string | 100 | | module assembly name |
| 5 | menu_mod_type   | string | 500 | | module fully qualified name |

- Response String Format  

Empty

##  Module Communication Service Dictionary Definition  
- `app_ctl_type` (application control type)  

| Code Value | Code Name |
| :-- | :-- |
| 01 | LoginWindow     |
| 02 | MainWindow      |
| 03 | MainLeftDrawer  |

- `app_act_flag` (application control active flag)  

| Code Value | Code Name |
| :-- | :-- |
| 0 | InActive |
| 1 | Active   |