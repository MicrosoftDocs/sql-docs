---
description: "xp_loginconfig (Transact-SQL)"
title: "xp_loginconfig (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords: 
  - "xp_loginconfig_TSQL"
  - "xp_loginconfig"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "xp_loginconfig"
ms.assetid: d380e799-2857-408a-bcbf-5e73a8e6aa5a
author: VanMSFT
ms.author: vanto
---
# xp_loginconfig (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Reports the login security configuration of an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
> [!IMPORTANT]  
>  [!INCLUDE[ssNoteDepFutureAvoid](../../includes/ssnotedepfutureavoid-md.md)]  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
xp_loginconfig ['config_name']  
```  
  
## Arguments  
 **'** *config_name* **'**  
 Is the configuration value to be displayed. If *config_name* is not specified, all configuration values are reported. *config_name* is **sysname**, with a default of NULL, and can be one of these values.  
  
|Value|Description|  
|-----------|-----------------|  
|**login mode**|Login security mode. Possible values are **Mixed** and **Windows Authentication**.<br /><br /> Replaced by:<br /><br /> `SELECT SERVERPROPERTY('IsIntegratedSecurityOnly'); GO`|  
|**default login**|Name of the default [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login ID for authorized users of trusted connections (for users without matching login name). The default login is **guest**. This value is provided for backward compatibility.|  
|**Default domain**|Name of the default Windows domain for network users of trusted connections. The default domain is the domain of the computer running Windows and [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. This value is provided for backward compatibility.|  
|**audit level**|Audit level. Possible values are **none**, **success**, **failure**, and **all**. Audits are written to the error log and to the Windows Event Viewer.|  
|**set hostname**|Indicates whether the host name from the client login record is replaced with the Windows network user name. Possible values are **true** or **false**. If this is set, the network user name appears in output from **sp_who**.|  
|**map _**|Reports what special Windows characters are mapped to the valid [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] underscore character (_). Possible values are **domain separator** (default), **space**, **null**, or any single character. This value is provided for backward compatibility.|  
|**map $**|Reports what special Windows characters are mapped to the valid [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] dollar sign character ($). Possible values are **domain separator**, **space**, **null**, or any single character. The default is **space**. This value is provided for backward compatibility.|  
|**map #**|Reports what special Windows characters are mapped to the valid [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] number sign character (#). Possible values are **domain separator**, **space**, **null**, or any single character. Default is the hyphen. This value is provided for backward compatibility.|  
  
## Return Code Values  
 0 (success) or 1 (failure)  
  
## Result Sets  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**name**|**sysname**|Configuration value|  
|**config value**|**sysname**|Configuration value setting|  
  
## Remarks  
 **xp_loginconfig** cannot be used to set configuration values.  
  
 To set the login mode and audit level, use [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)].  
  
## Permissions  
 Requires CONTROL permission on the **master** database.  
  
## Examples  
  
### A. How to report all configuration values  
 The following example shows all the currently configured settings.  
  
```  
EXEC xp_loginconfig;  
GO  
```  
  
### B. How to report a specific configuration value  
 The following example shows the setting for only the login mode.  
  
```  
EXEC xp_loginconfig 'login mode';  
GO  
```  
  
## See Also  
 [sp_denylogin &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-denylogin-transact-sql.md)   
 [sp_grantlogin &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-grantlogin-transact-sql.md)   
 [System Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)   
 [sp_revokelogin &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-revokelogin-transact-sql.md)   
 [xp_logininfo &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/xp-logininfo-transact-sql.md)  
  
  
