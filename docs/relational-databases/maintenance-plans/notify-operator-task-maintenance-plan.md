---
title: "Notify Operator Task (Maintenance Plan)"
description: Notify Operator Task (Maintenance Plan)
author: MikeRayMSFT
ms.author: mikeray
ms.date: "03/14/2017"
ms.service: sql
ms.subservice: supportability
ms.topic: conceptual
f1_keywords:
  - "sql13.swb.maint.notifyoperator.f1"
helpviewer_keywords:
  - "Notify Operator Task dialog box"
ms.assetid: 39c0797c-ad2b-4591-85c9-a23a7f902895
---
# Notify Operator Task (Maintenance Plan)
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  Use the **Notify Operators Task** dialog to add an automatic notification to this maintenance plan. To use this task you must have Database Mail enabled and properly configured with MSDB as a Mail Host Database, and have a [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent operator with a valid e-mail address.  
  
 This task uses the sp_notify_operator stored procedure.  
  
## Options  
 **Connection**  
 Select the server connection to use when performing this task.  
  
 **New**  
 Create a new server connection to use when performing this task. The **New Connection** dialog box is described below.  
  
 **Operators to notify**  
 Specify the recipient of the e-mail.  
  
 **Notification message subject**  
 Specify the text to include in the notification message subject line.  
  
 **Notification message body**  
 Specify the text to include in the notification message body.  
  
 **View T-SQL**  
 View the [!INCLUDE[tsql](../../includes/tsql-md.md)] statements performed against the server for this task, based on the selected options.  
  
> [!NOTE]  
>  When the number of objects affected is large, this display can take a considerable amount of time.  
  
## New Connection Dialog Box  
 **Connection name**  
 Enter a name for the new connection.  
  
 **Select or enter a server name**  
 Select a server to connect to when performing this task.  
  
 **Refresh**  
 Refresh the list of available servers.  
  
 **Enter information to log on to the server**  
 Specify how to authenticate against the server.  
  
 **Use Windows integrated security**  
 Connect to an instance of the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssDE](../../includes/ssde-md.md)] with [!INCLUDE[msCoName](../../includes/msconame-md.md)] Windows Authentication.  
  
 **Use a specific user name and password**  
 Connect to an instance of the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssDE](../../includes/ssde-md.md)] using [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Authentication. This option is not available.  
  
 **User name**  
 Provide a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login to use when authenticating. This option is not available.  
  
 **Password**  
 Provide a password to use when authenticating. This option is not available.  
  
## See Also  
 [Database Mail](../../relational-databases/database-mail/database-mail.md)   
 [sp_notify_operator &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-notify-operator-transact-sql.md)  
  
  
