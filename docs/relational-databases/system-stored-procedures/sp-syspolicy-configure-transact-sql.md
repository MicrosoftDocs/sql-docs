---
title: "sp_syspolicy_configure (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "sp_syspolicy_configure"
  - "sp_syspolicy_configure_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sp_syspolicy_configure"
ms.assetid: 70c10922-9345-4190-ba69-808a43f760da
author: VanMSFT
ms.author: vanto
manager: craigg
---
# sp_syspolicy_configure (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Configures settings for Policy-Based Management, such as whether Policy-Based Management is enabled.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_syspolicy_configure [ @name = ] 'name'  
    , [ @value = ] value  
```  
  
## Arguments  
`[ @name = ] 'name'`
 Is the name of the setting that you want to configure. *name* is **sysname**, is required, and cannot be NULL or an empty string.  
  
 *name* can be any of the following values:  
  
-   'Enabled' - Determines whether Policy-Based Management is enabled.  
  
-   'HistoryRetentionInDays' - Specifies the number of days that policy evaluation history should be retained. If set to 0, the history will not be automatically removed.  
  
-   'LogOnSuccess' - Specifies whether Policy-Based Management logs successful policy evaluations.  
  
`[ @value = ] value`
 Is the value that is associated with the specified value for *name*. *value* is **sql_variant**, and is required.  
  
-   If you specify 'Enabled' for *name*, you can use either of the following values:  
  
    -   0 = Disables Policy-Based Management.  
  
    -   1 = Enables Policy-Based Management.  
  
-   If you specify 'HistoryRententionInDays' for *name*, specify the number of days as an integer value.  
  
-   If you specify 'LogOnSuccess' for *name*, you can use either of the following values:  
  
    -   0 = Logs only failed policy evaluations.  
  
    -   1 = Logs both successful and failed policy evaluations.  
  
## Return Code Values  
 **0** (success) or **1** (failure)  
  
## Remarks  
 You must run sp_syspolicy_configure in the context of the msdb system database.  
  
 To view current values for these settings, query the msdb.dbo.syspolicy_configuration system view.  
  
## Permissions  
 Requires membership in the PolicyAdministratorRole fixed database role.  
  
> [!IMPORTANT]  
>  Possible elevation of credentials: Users in the PolicyAdministratorRole role can create server triggers and schedule policy executions that can affect the operation of the instance of the [!INCLUDE[ssDE](../../includes/ssde-md.md)]. For example, users in the PolicyAdministratorRole role can create a policy that can prevent most objects from being created in the [!INCLUDE[ssDE](../../includes/ssde-md.md)]. Because of this possible elevation of credentials, the PolicyAdministratorRole role should be granted only to users who are trusted with controlling the configuration of the [!INCLUDE[ssDE](../../includes/ssde-md.md)].  
  
## Examples  
 The following example enables Policy-Based Management.  
  
```  
EXEC msdb.dbo.sp_syspolicy_configure @name = N'Enabled'  
, @value = 1;  
  
GO  
```  
  
 The following example sets the policy history retention to 14 days.  
  
```  
EXEC msdb.dbo.sp_syspolicy_configure @name = N'HistoryRetentionInDays'  
, @value = 14;  
  
GO  
```  
  
 The following example configures Policy-Based Management to log both successful and failed policy evaluations.  
  
```  
EXEC msdb.dbo.sp_syspolicy_configure @name = N'LogOnSuccess'  
, @value = 1;  
  
GO  
```  
  
## See Also  
 [Policy-Based Management Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/policy-based-management-stored-procedures-transact-sql.md)   
 [sp_syspolicy_set_config_enabled &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-syspolicy-set-config-enabled-transact-sql.md)   
 [sp_syspolicy_set_config_history_retention &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-syspolicy-set-config-history-retention-transact-sql.md)   
 [sp_syspolicy_set_log_on_success &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-syspolicy-set-log-on-success-transact-sql.md)  
  
  
