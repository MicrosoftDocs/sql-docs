---
title: "Delete a Backup Device (SQL Server) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: backup-restore
ms.topic: conceptual
helpviewer_keywords: 
  - "database backups [SQL Server], deleting devices"
  - "backup devices [SQL Server], deleting"
  - "deleting backup devices"
  - "removing backup devices"
  - "backing up databases [SQL Server], backup devices"
ms.assetid: 7be62480-ed6a-4262-a071-1feba73b1c02
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
---
# Delete a Backup Device (SQL Server)
  This topic describes how to delete a backup device in [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] by using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or [!INCLUDE[tsql](../../includes/tsql-md.md)].  
  
 **In This Topic**  
  
-   **Before you begin:**  
  
     [Security](#Security)  
  
-   **To delete a backup device, using:**  
  
     [SQL Server Management Studio](#SSMSProcedure)  
  
     [Transact-SQL](#TsqlProcedure)  
  
##  <a name="BeforeYouBegin"></a> Before You Begin  
  
###  <a name="Security"></a> Security  
  
####  <a name="Permissions"></a> Permissions  
 Requires membership in the **diskadmin** fixed server role.  
  
##  <a name="SSMSProcedure"></a> Using SQL Server Management Studio  
  
#### To delete a backup device  
  
1.  After connecting to the appropriate instance of the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)], in Object Explorer, click the server name to expand the server tree.  
  
2.  Expand **Server Objects**, and then expand **Backup Devices**.  
  
3.  Right-click the device you want, and then click **Delete**.  
  
4.  In the **Delete Object** dialog box, verify that the correct device name appears in the **Object Name** column.  
  
5.  Click **OK**.  
  
##  <a name="TsqlProcedure"></a> Using Transact-SQL  
  
#### To delete a backup device  
  
1.  Connect to the [!INCLUDE[ssDE](../../includes/ssde-md.md)].  
  
2.  From the Standard bar, click **New Query**.  
  
3.  Copy and paste the following example into the query. This example shows how to use [sp_dropdevice](/sql/relational-databases/system-stored-procedures/sp-dropdevice-transact-sql) to delete a backup device. Execute the first example to create the `mybackupdisk` backup device and the physical name `c:\backup\backup1.bak`. Execute `sp_dropdevice` to delete the `mybackupdisk` backup device. The `delfile` parameter deletes the physical name.  
  
```sql  
--Define a backup device and physical name.   
USE AdventureWorks2012 ;  
GO  
EXEC sp_addumpdevice 'disk', 'mybackupdisk', 'c:\backup\backup1.bak' ;  
GO  
--Delete the backup device and the physical name.  
USE AdventureWorks2012 ;  
GO  
EXEC sp_dropdevice ' mybackupdisk ', 'delfile' ;  
GO  
  
```  
  
## See Also  
 [View the Properties and Contents of a Logical Backup Device &#40;SQL Server&#41;](view-the-properties-and-contents-of-a-logical-backup-device-sql-server.md)   
 [sys.backup_devices &#40;Transact-SQL&#41;](/sql/relational-databases/system-catalog-views/sys-backup-devices-transact-sql)   
 [BACKUP &#40;Transact-SQL&#41;](/sql/t-sql/statements/backup-transact-sql)   
 [Backup Devices &#40;SQL Server&#41;](backup-devices-sql-server.md)   
 [sp_addumpdevice &#40;Transact-SQL&#41;](/sql/relational-databases/system-stored-procedures/sp-addumpdevice-transact-sql)  
  
  
