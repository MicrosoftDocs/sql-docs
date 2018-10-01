---
title: "managed_backup.fn_get_parameter (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "10/03/2016"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "smart_admin.fn_get_parameter_TSQL"
  - "smart_admin.fn_get_parameter"
  - "fn_get_parameter_TSQL"
  - "fn_get_parameter"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "fn_get_parameter"
  - "smart_admin.fn_get_parameter"
ms.assetid: ed94e54d-4516-4806-a8ce-f013d3a04122
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
---
# managed_backup.fn_get_parameter (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2016-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2016-xxxx-xxxx-xxx-md.md)]

  Returns a table of 0, 1, or more rows of parameter and value pairs.  
  
 Use this stored procedure to review all or a specific configuration settings for Smart Admin.  
  
 If the parameter has never been configured, the function returns 0 rows.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```sql  
managed_backup.fn_get_parameter ('parameter_name' | '' | NULL )  
```  
  
##  <a name="Arguments"></a> Arguments  
 parameter_name  
 Name of the parameter. parameter_name is **NVARCHAR(128)**. If NULL or an empty string is supplied as an argument to the function, name-values pairs for all configured Smart Admin parameters are returned.  
  
## Table Returned  
  
|Column Name|Data Type|Description|  
|-----------------|---------------|-----------------|  
|parameter_name|NVARCHAR(128)|Name of the parameter. The following is a current list of parameters returned:<br/><br/>**FileRetentionDebugXevent**<br/><br/>**SSMBackup2WADebugXevent**<br/><br/>**SSMBackup2WANotificationEmailIds**<br/><br/>**SSMBackup2WAEnableUserDefinedPolicy**<br/><br/>**SSMBackup2WAEverConfigured**<br/><br/>**StorageOperationDebugXevent**|  
|parameter_value|NVARCHAR(128)|Current set value of the parameter.|  
  
## Security  
  
### Permissions  
 Requires SELECT permissions on the function.  
  
## Examples  
 The following example returns all the parameters which have been configured at least once, and their current values.  
  
```  
USE MSDB  
GO  
SELECT *   
FROM managed_backup.fn_get_parameter (NULL)  
  
```  
  
 The following example returns the email ID specified to receive the error notifications. If no rows are returns, then it means that this email notification option has not been enabled.  
  
```  
USE MSDB  
GO  
SELECT *  
FROM managed_backup.fn_get_parameter ('SSMBackup2WANotficationEmailIds')  
  
```  
  
## See Also  
 [SQL Server Managed Backup to Microsoft Azure](../../relational-databases/backup-restore/sql-server-managed-backup-to-microsoft-azure.md)  
  
  
