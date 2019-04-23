---
title: "Common errors with database mail| Microsoft Docs"
ms.custom: ""
ms.date: "04/22/2019"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: 
ms.topic: conceptual
helpviewer_keywords: 
  - "architecture [SQL Server], Database Mail"
  - "Database Mail [SQL Server], architecture"
  - "Database Mail [SQL Server], components"
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# Common errors with database mail 
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]

This article describes some common errors encountered with database mail and their solutions.

## Could not find stored procedure 'sp_send_dbmail'
The [sp_send_dbmail](../system-stored-procedures/sp-send-dbmail-transact-sql.md) stored procedure is installed in the msdb database. You must either run **sp_send_dbmail** from the msdb database, or specify a three-part name for the stored procedure.

Use [Database Mail Configuration Wizard](configure-database-mail.md) to enable and configure database mail.

## Profile not valid
There are two possible causes for this message. Either the profile specified does not exist, or the user running [sp_send_dbmail (Transact-SQL)](./system-stored-procedures/sp-send-dbmail-transact-sql.md) does not have permission to access the profile.

To check permissions for a profile, run the stored procedure sysmail_help_principalprofile_sp (Transact-SQL) with name of the profile. Use the stored procedure [sysmail_add_principalprofile_sp (Transact-SQL)](../system-stored-procedures/sysmail-help-principalprofile-sp-transact-sql.md) or the [Database Mail Configuration Wizard](configure-database-mail.md) to grant permission for a msdb user or group to access a profile.

## Permission denied on sp_send_dbmail

This topic describes how to troubleshoot an error message stating that the user attempting to send Database Mail does not have permission to execute sp_send_dbmail.

The error text is:
```
EXECUTE permission denied on object 'sp_send_dbmail', 
database 'msdb', schema 'dbo'.
```

To send Database mail, users must be a user in the msdb database and a member of the DatabaseMailUserRole database role in the msdb database. To add msdb users or groups to this role use SQL Server Management Studio or execute the following statement for the user or role that needs to send Database Mail.

```sql
EXEC msdb.dbo.sp_addrolemember @rolename = 'DatabaseMailUserRole'
    ,@membername = '<user or role name>';
GO
```
For more information, see [sp_addrolemember](../system-stored-procedures/sp-addrolemember-transact-sql.md) and [sp_droprolemember](../system-stored-procedures/sp-droprolemember-transact-sql.md).



##  <a name="RelatedContent"></a> Related content 
  
-  [Database mail overview](database-mail.md)
-  [Create a database mail profile](create-a-database-mail-profile.md)
  
  
