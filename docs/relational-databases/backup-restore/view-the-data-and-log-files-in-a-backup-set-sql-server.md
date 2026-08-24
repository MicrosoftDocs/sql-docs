---
title: "View backup set data & Log files"
description: Learn how to view the data and log files in a backup set in SQL Server by using SQL Server Management Studio or Transact-SQL.
author: MashaMSFT
ms.author: mathoma
ms.date: "12/17/2019"
ms.service: sql
ms.subservice: backup-restore
ms.topic: how-to
helpviewer_keywords:
  - "database backups [SQL Server], viewing backup sets"
  - "viewing backup set information"
  - "backup sets [SQL Server], viewing files in"
  - "displaying backup set information"
  - "transaction log backups [SQL Server], viewing backup sets"
  - "backing up [SQL Server], viewing backup sets"
---
# View the data and log files in a backup set (SQL Server)
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  This topic describes how to view the data and log files in a backup set in [!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)] by using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or [!INCLUDE[tsql](../../includes/tsql-md.md)].  

<a id="BeforeYouBegin"></a>
<a id="Security"></a>

## Security

For information about security, see [RESTORE FILELISTONLY &#40;Transact-SQL&#41;](../../t-sql/statements/restore-statements-filelistonly-transact-sql.md)  
  
<a id="Permissions"></a>

## Permissions

In [!INCLUDE[sql2008-md](../../includes/sql2008-md.md)] and later versions, obtaining information about a backup set or backup device requires CREATE DATABASE permission. For more information, see [GRANT Database Permissions &#40;Transact-SQL&#41;](../../t-sql/statements/grant-database-permissions-transact-sql.md).  
  
##  <a name="SSMSProcedure"></a> Using SQL Server Management Studio  
  
#### To view the data and log files in a backup set  
  
1.  After connecting to the appropriate instance of the [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)], in Object Explorer, click the server name to expand the server tree.  
  
2.  Expand **Databases**, and, depending on the database, either select a user database or expand **System Databases** and select a system database.  
  
3.  Right-click the database, and then click **Properties**, which opens the **Database Properties** dialog box.  
  
4.  In the **Select a Page** pane, click **Files**.  
  
5.  Look in the **Database files** grid for a list of the data and log files and their properties.  

##  <a name="TsqlProcedure"></a> Using Transact-SQL  
  
#### To view the data and log files in a backup set  
  
1.  Connect to the [!INCLUDE[ssDE](../../includes/ssde-md.md)].  
  
2.  From the Standard bar, click **New Query**.  
  
3.  Use the [RESTORE FILELISTONLY](../../t-sql/statements/restore-statements-filelistonly-transact-sql.md) statement. This example returns information about the second backup set (`FILE=2`) on the `AdventureWorksBackups` backup device.  
  
```sql  
USE AdventureWorks2022;  
RESTORE FILELISTONLY FROM AdventureWorksBackups   
   WITH FILE=2;  
GO  
```  
  
## Related content

- [backupfilegroup (Transact-SQL)](../system-tables/backupfilegroup-transact-sql.md)
- [backupfile (Transact-SQL)](../system-tables/backupfile-transact-sql.md)
- [backupset (Transact-SQL)](../system-tables/backupset-transact-sql.md)
- [backupmediaset (Transact-SQL)](../system-tables/backupmediaset-transact-sql.md)
- [backupmediafamily (Transact-SQL)](../system-tables/backupmediafamily-transact-sql.md)
- [Backup Devices (SQL Server)](backup-devices-sql-server.md)
