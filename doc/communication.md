#  Module Communication Protocol Definition  
The two types `RequestEvent`,`ResponseEvent` inherit from `PubSubEvent<string>` provide module communication support.The detail is as follow.

##  Request Message Format  
| Number | Identification | Name | Type | Length | Description |
| :-- | :-- | :-- | :-- | :-- | :-- |
| 1 | svc_code      | service number         | string | 4    | see the service definition |
| 2 | svc_name      | service name           | string | 50   | used for filter service |
| 3 | svc_type      | service type           | string | 1    | |
| 4 | svc_bhvr_type | service behaviour type | string | 1    | |
| 5 | msg_code      | message number         | string | 30   | ULID number |
| 6 | souc_mdl      | source module          | string | 1    | |
| 7 | tagt_mdl      | target module          | string | 1    | |
| 8 | svc_time      | event service time     | string | 19   | YYYY-MM-DD mm:hh:ss |
| 9 | svc_cont      | event service content  | string | 8000 | see the service content definition |

##  Response Message Format  
| Number | Identification | Name | Type | Length | Memo |
| :-- | :-- | :-- | :-- | :-- | :-- |
| 1  | svc_code      | event service number   | string | 4    | |
| 2  | svc_name      | event service name     | string | 50   | |
| 3  | svc_type      | event service type     | string | 1    | |
| 4  | svc_bhvr_type | service behaviour type | string | 1    | |
| 5  | msg_code      | message number         | string | 30   | |
| 6  | souc_mdl      | source module          | string | 1    | |
| 7  | tagt_mdl      | target module          | string | 1    | |
| 8  | svc_time      | event service time     | string | 19   | |
| 9  | svc_cont      | event service content  | string | 8000 | |
| 10 | ret_code      | event service number   | string | 4    | |
| 11 | ret_msg       | error message          | string | 500  | |

 
##  Module Communication Service Definition
| Number | Code | Name | Description |
| :-- | :-- | :-- | :-- |
| 1 | 1101 | ApplicationEvent     | give framework modules notice that eventservice is initialzed |
| 2 | 2101 | PathEvent            | change appliction status                                      |
| 3 | 3101 | DataBaseEvent        | validate app licence                                          |
| 4 | 4101 | LogEvent             | validate account password                                     |
| 5 | 5101 | ThemeEvent           | get account rights                                            | 
| 6 | 6101 | AccountEvent         | get account authorized menu list                              | 
| 7 | 7101 | ExtensionModuleEvent | active menu item                                              | 

##   Module Communication Service Content Definition
###  `1101.4` ApplicationAlterationEvent
- Request String Format  

| Number | Parameter Code | Parameter Type | Parameter Length | Dictionary | Description |
| :-- | :-- | :-- | :-- | :-- | :-- |
| 1 | app_ctl_type | string | 1 | Y | |
| 2 | app_act_flag | string | 1 | Y | |

- Response String Format  

Empty

###  `5101.1` ThemeDefaultInitializationEvent
- Request String Format  

| Number | Parameter Code | Parameter Type | Parameter Length | Dictionary | Description |
| :-- | :-- | :-- | :-- | :-- | :-- |
| 1 | thm_type | string | 1 | Y | |
| 2 | thm_pry_col | string | 3 | Y | |
| 2 | thm_sec_col | string | 3 | Y | |

- Response String Format  

Empty

###  `6101` AccountEvent
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

###  `7101` ExtensionModuleEvent
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
- `svc_type`

| Code Value | Code Name |
| :-- | :-- |
| 1 | Request  |
| 2 | Response |

- `svc_bhvr_type`

| Code Value | Code Name |
| :-- | :-- |
| 1 | DefaultInitialization |
| 2 | Initialization        |
| 3 | Addition              |
| 4 | Alteration            |
| 5 | Deletion              |
| 6 | Activation            |
| 7 | Persistence           |

- `souc_mdl` / `tagt_mdl`  

| Code Value | Code Name |
| :-- | :-- |
| 1 | All                      |
| 2 | ApplictionModule         |
| 3 | KernelModule             |
| 4 | ServiceModule            |
| 5 | LoginModule              |
| 6 | MainHeaderModule         |
| 7 | MainLeftDrawerModule     |
| 8 | BasicConfigurationModule |

- `app_ctl_type`  

| Code Value | Code Name |
| :-- | :-- |
| 1 | LoginWindow     |
| 2 | MainWindow      |
| 3 | MainLeftDrawer  |

- `app_act_flag`  

| Code Value | Code Name |
| :-- | :-- |
| 0 | InActive |
| 1 | Active   |

- `thm_type`  

| Code Value | Code Name |
| :-- | :-- |
| 0 | Inherit |
| 1 | Light   |
| 2 | Dark |
