---
title: "sp_add_log_file_recover_suspect_db (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/16/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "sp_add_log_file_recover_suspect_db_TSQL"
  - "sp_add_log_file_recover_suspect_db"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sp_add_log_file_recover_suspect_db"
ms.assetid: b41ca3a5-7222-4c22-a012-e66a577a82f6
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# sp_add_log_file_recover_suspect_db (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Adds a log file to a file group when recovery cannot complete on a database due to insufficient log space (error 9002). After the file is added, **sp_add_log_file_recover_suspect_db** turns off the suspect setting and completes the recovery of the database. The parameters are the same as those for ALTER DATABASE *database_name* ADD LOG FILE.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_add_log_file_recover_suspect_db [ @dbName= ] 'database' ,   
    [ @name = ] 'logical_file_name' ,   
    [ @filename= ] 'os_file_name' ,   
    [ @size = ] 'size' ,   
    [ @maxsize = ] 'max_size' ,   
    [ @filegrowth = ] 'growth_increment'  
```  
  
## Arguments  
 [ **@dbName =** ] **'**_database_**'**  
 Is the name of the database. *database* is **sysname**, with no default.  
  
 [ **@name=** ] **'**_logical_file_name_**'**  
 Is the name used in the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] when referencing the file. The name must be unique in the server. *logical_file_name* is **nvarchar(260)**, with no default.  
  
 [ **@filename =** ] **'**_os_file_name_**'**  
 Is the path and file name used by the operating system for the file. The file must reside in the server in which the [!INCLUDE[ssDE](../../includes/ssde-md.md)] is installed. *os_file_name* is **nvarchar(260)**, with no default.  
  
 [ **@size=** ] **'**_size_ **'**  
 Is the initial size of the file. *size* is **nvarchar(20)**, with a default of NULL. Specify a whole number; do not include a decimal. The MB and KB suffixes can be used to specify megabytes or kilobytes. The default is MB. The minimum value is 512 KB. If *size* is not specified, the default is 1 MB.  
  
 [ **@maxsize=** ] **'**_max_size_ **'**  
 Is the maximum size to which the file can grow. *max_size* is **nvarchar(20)**, with a default of NULL. Specify a whole number; do not include a decimal. The MB and KB suffixes can be used to specify megabytes or kilobytes. The default is MB.  
  
 If *max_size* is not specified, the file will grow until the disk is full. The [!INCLUDE[msCoName](../../includes/msconame-md.md)] Windows application log warns an administrator when a disk is about to become full.  
  
 [ **@filegrowth=** ] **'**_growth_increment_ **'**  
 Is the amount of space added to the file each time new space is required. *growth_increment* is **nvarchar(20)**, with a default of NULL. A value of 0 indicates no growth. Specify a whole number; do not include a decimal. The value can be specified in MB, KB, or percent (%). When % is specified, the growth increment is the specified percentage of the size of the file at the time the increment occurs. If a number is specified without an MB, KB, or % suffix, the default is MB.  
  
 If *growth_increment* is NULL, the default value is 10%, and the minimum size value is 64 KB. The size specified is rounded to the nearest 64 KB.  
  
## Return Code Values  
 0 (success) or 1 (failure)  
  
## Result Sets  
 None  
  
## Permissions  
 Execute permissions default to members of the **sysadmin** fixed server role. These permissions are not transferable.  
  
## Examples  
 In the following example, the database `db1` was marked suspect during recovery due to insufficient log space (error 9002).  
  
```  
USE master;  
GO  
EXEC sp_add_log_file_recover_suspect_db db1, logfile2,  
'C:\Program Files\Microsoft SQL  
    Server\MSSQL13.MSSQLSERVER\MSSQL\Data\db1_logfile2.ldf',   
    '1MB';  
```  
  
## See Also  
 [ALTER DATABASE &#40;Transact-SQL&#41;](../../t-sql/statements/alter-database-transact-sql.md)   
 [sp_add_data_file_recover_suspect_db &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-add-data-file-recover-suspect-db-transact-sql.md)   
 [System Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)  
  
  
