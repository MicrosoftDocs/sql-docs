---
title: "sp_changedistributiondb (Transact-SQL)"
description: "sp_changedistributiondb (Transact-SQL)"
author: markingmyname
ms.author: maghan
ms.date: "03/03/2017"
ms.service: sql
ms.subservice: replication
ms.topic: "reference"
f1_keywords:
  - "sp_changedistributiondb_TSQL"
  - "sp_changedistributiondb"
helpviewer_keywords:
  - "sp_changedistributiondb"
dev_langs:
  - "TSQL"
---
# sp_changedistributiondb (Transact-SQL)
[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]

  Changes the properties of the distribution database. This stored procedure is executed at the Distributor on any database.  
  
 :::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
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
 :::code language="sql" source="../replication/codesnippet/tsql/sp-changedistributiondb-_1.sql":::
  
## Permissions  
 Only members of the **sysadmin** fixed server role can execute **sp_changedistributiondb**.  
  
## See Also  
 [View and Modify Distributor and Publisher Properties](../../relational-databases/replication/view-and-modify-distributor-and-publisher-properties.md)   
 [sp_adddistributiondb &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-adddistributiondb-transact-sql.md)   
 [sp_dropdistributiondb &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-dropdistributiondb-transact-sql.md)   
 [sp_helpdistributiondb &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-helpdistributiondb-transact-sql.md)   
 [Replication Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/replication-stored-procedures-transact-sql.md)  
  
  
