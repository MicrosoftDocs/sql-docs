---
description: "managed_backup.sp_set_parameter (Transact-SQL)"
title: "managed_backup.sp_set_parameter (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/10/2016"
ms.service: sql
ms.reviewer: ""
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords: 
  - "sp_set_parameter_TSQL"
  - "sp_set_parameter"
  - "smart_admin.sp_set_parameter"
  - "smart_admin.sp_set_parameter_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sp_set_parameter"
  - "smart_admin.sp_set_parameter"
ms.assetid: bd8ae5fd-1337-4b7f-b0a4-153cbca9fa5f
author: markingmyname
ms.author: maghan
---
# managed_backup.sp_set_parameter (Transact-SQL)
[!INCLUDE [sqlserver2016](../../includes/applies-to-version/sqlserver2016.md)]

  Sets the value of the specified Smart Admin system parameter.  
  
 The available parameters are related to [!INCLUDE[ss_smartbackup](../../includes/ss-smartbackup-md.md)] . These parameters are used to set the email notifications, enable specific extended events, and enable user set policy based management policies. You must specify the parameter name and the parameter value pairs..  

  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```sql  
EXEC managed_backup.sp_set_parameter   
    [@parameter_name = ] {'SSMBackup2WANotificationEmailIds' | 'SSMBackup2WAEnableUserDefinedPolicy' | 'SSMBackup2WADebugXevent' | 'FileRetentionDebugXevent' | 'StorageOperationDebugXevent'}  
    ,[@parameter_value = ] 'parameter_value'  
```  
  
##  <a name="Arguments"></a> Arguments  
 @parameter_name  
 The name of the parameter you want to set the value for. @parameter_name is NVARCHAR(128). The available parameter names are **SSMBackup2WANotificationEmailIds**, **SSMBackup2WADebugXevent**, **SSMBackup2WAEnableUserDefinedPolicy**, **FileRetentionDebugXevent**, and **StorageOperationDebugXevent**.  
  
 @parameter_value  
 The value for the parameter you want to set. @parameter value is NVARCHAR(128).  Following are the allowed parameter name and value pairs:  
  
-   @parameter_name = 'SSMBackup2WANotificationEmailIds' : @parameter_value  = 'email'  
  
-   @parameter_name = 'SSMBackup2WAEnableUserDefinedPolicy' : @parameter_value  = { 'true' | 'false' }  
  
-   @parameter_name = 'SSMBackup2WADebugXevent': @parameter_value  = { 'true' | 'false' }  
  
-   @parameter_name = 'FileRetentionDebugXevent' : @parameter_value  = { 'true' | 'false' }  
  
-   @parameter_name = 'StorageOperationDebugXevent' = { 'true' | 'false' }  
  
## Return Code Value  
 0 (success) or 1 (failure)  
  
## Best Practices  
 Optional section that describes best practices the user should know when executing the statement or routine.  
  
## Security  
  
### Permissions  
 Requires **EXECUTE** permissions on **managed_backup.sp_set_parameter** stored procedure.  
  
## Examples  
 The following examples enable operational and debug extended events.  
  
```  
-- to enable operational events  
Use msdb;  
Go  
         EXEC managed_backup.sp_set_parameter 'FileRetentionOperationalXevent', 'True'  
--  to enable debug events  
Use msdb;  
Go  
         EXEC managed_backup.sp_set_parameter 'FileRetentionDebugXevent', 'True'  
  
```  
  
 The following example enables email notifications of errors and warnings and sets the emailID to use to send the notifications to:  
  
```  
Use msdb  
Go  
EXEC managed_backup.sp_set_parameter @parameter_name = 'SSMBackup2WANotificationEmailIds', @parameter_value = '<email address>'  
  
```  
  
  
