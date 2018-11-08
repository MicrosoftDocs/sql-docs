---
title: "sp_syspolicy_delete_policy_execution_history (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/03/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "sp_syspolicy_delete_policy_execution_history"
  - "sp_syspolicy_delete_policy_execution_history_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sp_syspolicy_delete_policy_execution_history"
ms.assetid: fe651af9-267e-45ec-b4e7-4b0698fb1be3
author: VanMSFT
ms.author: vanto
manager: craigg
---
# sp_syspolicy_delete_policy_execution_history (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Deletes execution history for policies in Policy-Based Management. You can use this stored procedure to delete execution history for a specific policy or for all policies, and to delete execution history before a specific date.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_syspolicy_delete_policy_execution_history [ @policy_id = ] policy_id ]  
    [ , [ @oldest_date = ] 'oldest_date' ]  
```  
  
## Arguments  
 [ **@policy_id=** ] *policy_id*  
 Is the identifier of the policy for which you want to delete the execution history. *policy_id* is **int**, and is required. Can be NULL.  
  
 [ **@oldest_date=** ] **'***oldest_date***'**  
 Is the oldest date for which you want to keep policy execution history. Any execution history earlier than this date is deleted. *oldest_date* is **datetime**, and is required. Can be NULL.  
  
## Return Code Values  
 **0** (success) or **1** (failure)  
  
## Remarks  
 You must run sp_syspolicy_delete_policy_execution_history in the context of the msdb system database.  
  
 To obtain values for *policy_id*, and to view execution history dates, you can use the following query:  
  
```  
SELECT a.name AS N'policy_name', b.policy_id, b.start_date, b.end_date  
FROM msdb.dbo.syspolicy_policies AS a   
INNER JOIN msdb.dbo.syspolicy_policy_execution_history AS b  
ON a.policy_id = b.policy_id  
```  
  
 The following behavior applies if you specify NULL for one or both values:  
  
-   To delete all policy execution history, specify NULL for both *policy_id* and for *oldest_date*.  
  
-   To delete all policy execution history for a specific policy, specify a policy identifier for *policy_id*, and specify NULL as *oldest_date*.  
  
-   To delete policy execution history for all policies before a specific date, specify NULL for *policy_id*, and specify a date for *oldest_date*.  
  
 To archive policy execution history, you can open the Policy History log in Object Explorer and export the execution history to a file. To access the Policy History log, expand **Management**, right-click **Policy Management**, and then click **View History**.  
  
## Permissions  
 Requires membership in the PolicyAdministratorRole fixed database role.  
  
> [!IMPORTANT]  
>  Possible elevation of credentials: Users in the PolicyAdministratorRole role can create server triggers and schedule policy executions that can affect the operation of the instance of the [!INCLUDE[ssDE](../../includes/ssde-md.md)]. For example, users in the PolicyAdministratorRole role can create a policy that can prevent most objects from being created in the [!INCLUDE[ssDE](../../includes/ssde-md.md)]. Because of this possible elevation of credentials, the PolicyAdministratorRole role should be granted only to users who are trusted with controlling the configuration of the [!INCLUDE[ssDE](../../includes/ssde-md.md)].  
  
## Examples  
 The following example deletes policy execution history before a specific date for a policy with an ID of 7.  
  
```  
EXEC msdb.dbo.sp_syspolicy_delete_policy_execution_history @policy_id = 7  
, @oldest_date = '2009-02-16 16:00:00.000';  
  
GO  
```  
  
## See Also  
 [Policy-Based Management Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/policy-based-management-stored-procedures-transact-sql.md)   
 [sp_syspolicy_set_config_history_retention &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-syspolicy-set-config-history-retention-transact-sql.md)   
 [sp_syspolicy_purge_history &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-syspolicy-purge-history-transact-sql.md)  
  
  
