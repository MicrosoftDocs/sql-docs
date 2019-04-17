---
title: "managed_backup.sp_backup_master_switch (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/10/2016"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "sp_ backup_master_switch"
  - "smart_admin.sp_backup_master_switch"
  - "sp_ backup_master_switch_TSQL"
  - "smart_admin.sp_backup_master_switch_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sp_ backup_master_switch"
  - "smart_admin.sp_backup_master_switch"
ms.assetid: 1ed2b2b2-c897-41cc-bed5-1c6bc47b9dd2
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
---
# managed_backup.sp_backup_master_switch (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2016-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2016-xxxx-xxxx-xxx-md.md)]

  Pauses or resumes the [!INCLUDE[ss_smartbackup](../../includes/ss-smartbackup-md.md)].  
  
 Use this stored procedure to temporarily pause and them resume [!INCLUDE[ss_smartbackup](../../includes/ss-smartbackup-md.md)]. This makes sure that all the configurations settings remain and is retained when the operations resume. When [!INCLUDE[ss_smartbackup](../../includes/ss-smartbackup-md.md)] is paused the retention period is not enforced. This means that there is no check to determine whether files should be deleted from storage or if there are backup file corrupted, or break in log chain.  
  

  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```sql  
EXEC managed_backup.sp_backup_master_switch   
                     [@new_state = ] { 0 | 1}  
```  
  
##  <a name="Arguments"></a> Arguments  
 @state  
 Set the state of [!INCLUDE[ss_smartbackup](../../includes/ss-smartbackup-md.md)]. The @state parameter is **BIT**. When set to a value of 0 the operations are paused, and when set to a value of 1 the operation resume.  
  
## Return Code Value  
 0 (success) or 1 (failure)  
  
## Security  
 Describes security issues related to the statement.Include Permissions as a subsection (H3 heading). Consider including other subsections for Ownership Chaining and Auditing if appropriate.  
  
### Permissions  
 Requires membership in **db_backupoperator** database role, with **ALTER ANY CREDENTIAL** permissions, and **EXECUTE** permissions on **sp_delete_backuphistory**stored procedure.  
  
## Examples  
 The following example can be used to pause [!INCLUDE[ss_smartbackup](../../includes/ss-smartbackup-md.md)] on the instance it is executed on:  
  
```  
Use msdb;  
GO  
EXEC managed_backup.sp_backup_master_switch @new_state=0;  
Go  
  
```  
  
 The following example can be used to resume [!INCLUDE[ss_smartbackup](../../includes/ss-smartbackup-md.md)].  
  
```  
Use msdb;  
GO  
EXEC managed_backup.sp_backup_master_switch @new_state=1;  
Go  
  
```  
  
## See Also  
 [SQL Server Managed Backup to Microsoft Azure](../../relational-databases/backup-restore/sql-server-managed-backup-to-microsoft-azure.md)  
  
  
