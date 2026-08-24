---
title: "View logical backup device contents"
description: Learn how to view the properties and contents of a logical backup device in SQL Server by using SQL Server Management Studio or Transact-SQL.
author: MashaMSFT
ms.author: mathoma
ms.date: "03/15/2017"
ms.service: sql
ms.subservice: backup-restore
ms.topic: how-to
helpviewer_keywords:
  - "displaying backup content"
  - "viewing backup content"
  - "database backups [SQL Server], viewing content"
  - "backing up databases [SQL Server], viewing content"
  - "backing up databases [SQL Server], properties"
  - "displaying backup properties"
  - "backup devices [SQL Server], viewing information"
  - "viewing backup properties"
  - "database backups [SQL Server], properties"
---
# View the Properties and Contents of a Logical Backup Device (SQL Server)
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  This topic describes how to view the properties and contents of a logical backup device in [!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)] by using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or [!INCLUDE[tsql](../../includes/tsql-md.md)].  

<a id="BeforeYouBegin"></a>
<a id="Security"></a>

## Security

For information about security, see [RESTORE LABELONLY &#40;Transact-SQL&#41;](../../t-sql/statements/restore-statements-labelonly-transact-sql.md).  
  
<a id="Permissions"></a>

## Permissions

In [!INCLUDE[sql2008-md](../../includes/sql2008-md.md)] and later versions, obtaining information about a backup set or backup device requires CREATE DATABASE permission. For more information, see [GRANT Database Permissions &#40;Transact-SQL&#41;](../../t-sql/statements/grant-database-permissions-transact-sql.md).  
  
##  <a name="SSMSProcedure"></a> Using SQL Server Management Studio  
  
#### To view the properties and contents of a logical backup device  
  
1.  After connecting to the appropriate instance of the [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)], in Object Explorer, click the server name to expand the server tree.  
  
2.  Expand **Server Objects**, and expand **Backup Devices**.  
  
3.  Click the device and right-click **Properties**, which opens the **Backup Device** dialog box.  
  
4.  The **General** page displays the device name and destination, which is either a tape device or file path.  
  
5.  In the **Select a Page** pane, click **Media Contents**.  
  
6.  The right-hand pane displays in the following properties panels:  
  
    -   **Media**  
  
         Media sequence information (the media sequence number, the family sequence number, and the mirror identifier, if any) and the media creation date and time.  
  
    -   **Media set**  
  
         Media set information: the media set name and description, if any, and the number of families in the media set.  
  
7.  The **Backup sets** grid displays information about the contents of the media set.  
  
> [!NOTE]  
>  For more information, see [Media Contents Page](../../relational-databases/backup-restore/backup-device-media-contents-page.md).  
  
##  <a name="TsqlProcedure"></a> Using Transact-SQL  
  
#### To view the properties and contents of a logical backup device  
  
1.  Connect to the [!INCLUDE[ssDE](../../includes/ssde-md.md)].  
  
2.  From the Standard bar, click **New Query**.  
  
3.  Use the [RESTORE LABELONLY](../../t-sql/statements/restore-statements-labelonly-transact-sql.md) statement. This example returns information about the `AdvWrks2008R2Backup` logical backup device.  
  
```sql  
USE AdventureWorks2022;  
RESTORE LABELONLY  
   FROM AdvWrks2008R2Backup ;  
GO  
  
```  
  
## Related content

- [backupfilegroup (Transact-SQL)](../system-tables/backupfilegroup-transact-sql.md)
- [backupfile (Transact-SQL)](../system-tables/backupfile-transact-sql.md)
- [backupset (Transact-SQL)](../system-tables/backupset-transact-sql.md)
- [backupmediaset (Transact-SQL)](../system-tables/backupmediaset-transact-sql.md)
- [backupmediafamily (Transact-SQL)](../system-tables/backupmediafamily-transact-sql.md)
- [sys.sp_addumpdevice (Transact-SQL)](../system-stored-procedures/sp-addumpdevice-transact-sql.md)
- [Backup Devices (SQL Server)](backup-devices-sql-server.md)
