---
description: "sp_syspolicy_set_config_history_retention (Transact-SQL)"
title: "sp_syspolicy_set_config_history_retention (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "08/09/2016"
ms.service: sql
ms.reviewer: ""
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords: 
  - "sp_syspolicy_set_config_history_retention_TSQL"
  - "sp_syspolicy_set_config_history_retention"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sp_syspolicy_set_config_history_retention"
ms.assetid: 2574898a-e724-4447-b96c-ff778471339d
author: VanMSFT
ms.author: vanto
---
# sp_syspolicy_set_config_history_retention (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Specifies the number of days to keep policy evaluation history for Policy-Based Management.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_syspolicy_set_config_history_retention [ @value = ] value  
```  
  
## Arguments  
`[ @value = ] value`
 Is the number of days to retain Policy-Based Management history. *value* is **sqlvariant**.  
  
## Return Code Values  
 **0** (success) or **1** (failure)  
  
## Remarks  
 You must run sp_syspolicy_set_config_history_retention in the context of the msdb system database.  
  
 If *value* is set to 0, the history will not be automatically removed.  
  
 To view the current value for history retention, run the following query:  
  
```  
SELECT current_value FROM msdb.dbo.syspolicy_configuration  
WHERE name = 'HistoryRetentionInDays'  
```  
  
## Permissions  
 Requires membership in the PolicyAdministratorRole fixed database role.  
  
> [!IMPORTANT]  
>  Possible elevation of credentials: Users in the PolicyAdministratorRole role can create server triggers and schedule policy executions that can affect the operation of the instance of the [!INCLUDE[ssDE](../../includes/ssde-md.md)]. For example, users in the PolicyAdministratorRole role can create a policy that can prevent most objects from being created in the [!INCLUDE[ssDE](../../includes/ssde-md.md)]. Because of this possible elevation of credentials, the PolicyAdministratorRole role should be granted only to users who are trusted with controlling the configuration of the [!INCLUDE[ssDE](../../includes/ssde-md.md)].  
  
## Examples  
 The following example sets the policy evaluation history retention to 28 days.  
  
```  
EXEC msdb.dbo.sp_syspolicy_set_config_history_retention @value = 28;  
  
GO  
```  
  
## See Also  
 [Policy-Based Management Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/policy-based-management-stored-procedures-transact-sql.md)   
 [sp_syspolicy_configure &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-syspolicy-configure-transact-sql.md)  
  
  
