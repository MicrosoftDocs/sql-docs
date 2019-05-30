---
title: "sp_changedistributiondb (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/03/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: replication
ms.topic: "language-reference"
f1_keywords: 
  - "sp_changedistributiondb_TSQL"
  - "sp_changedistributiondb"
helpviewer_keywords: 
  - "sp_changedistributiondb"
ms.assetid: 66f73185-ea9e-43f9-86ed-9dd933cee2f6
author: stevestein
ms.author: sstein
manager: craigg
---
# sp_changedistributiondb (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Changes the properties of the distribution database. This stored procedure is executed at the Distributor on any database.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_changedistributiondb [ @database= ] 'database'   
    [ , [ @property= ] 'property' ]   
    [ , [ @value= ] 'value' ]  
```  
  
## Arguments  
`[ @database = ] 'database'`
 Is the name of the distribution database. *database* is **sysname**, with no default.  
  
`[ @property = ] 'property'`
 Is the property to change for the given database. *property* is **sysname**, and can be one of these values.  
  
|Value|Description|  
|-----------|-----------------|  
|**history_retention**|History table retention period.|  
|**max_distretention**|Maximum distribution retention period.|  
|**min_distretention**|Minimum distribution retention period.|  
|NULL (default)|All available *property* values are printed.|  
  
`[ @value = ] 'value'`
 Is the new value for the specified property. *value* is **nvarchar(255)**, with a default of NULL.  
  
## Return Code Values  
 **0** (success) or **1** (failure)  
  
## Remarks  
 **sp_changedistributiondb** is used in all types of replication.  
  
## Example  
 [!code-sql[HowTo#sp_changedistributiondb](../../relational-databases/replication/codesnippet/tsql/sp-changedistributiondb-_1.sql)]  
  
## Permissions  
 Only members of the **sysadmin** fixed server role can execute **sp_changedistributiondb**.  
  
## See Also  
 [View and Modify Distributor and Publisher Properties](../../relational-databases/replication/view-and-modify-distributor-and-publisher-properties.md)   
 [sp_adddistributiondb &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-adddistributiondb-transact-sql.md)   
 [sp_dropdistributiondb &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-dropdistributiondb-transact-sql.md)   
 [sp_helpdistributiondb &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-helpdistributiondb-transact-sql.md)   
 [Replication Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/replication-stored-procedures-transact-sql.md)  
  
  
