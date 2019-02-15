---
title: "Configure the remote access Server Configuration Option | Microsoft Docs"
ms.custom: ""
ms.date: "08/11/2017"
ms.prod: sql
ms.prod_service: high-availability
ms.reviewer: ""
ms.technology: configuration
ms.topic: conceptual
helpviewer_keywords: 
  - "remote servers [SQL Server], stored procedure execution"
ms.assetid: f5de748d-1c55-4714-9661-38fe62e5095f
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
---
# Configure the remote access Server Configuration Option
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]

  This topic is about the "Remote Access" feature. This configuration option is an obscure [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] communication feature that is deprecated, and you probably shouldn't be using it. If you reached this page because you are having trouble connecting to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], see one of the following topics instead:  
  
-   [Tutorial: Getting Started with the Database Engine](../../relational-databases/tutorial-getting-started-with-the-database-engine.md)  
  
-   [Logging In to SQL Server](../../database-engine/configure-windows/logging-in-to-sql-server.md)  
  
-   [Connect to SQL Server When System Administrators Are Locked Out](../../database-engine/configure-windows/connect-to-sql-server-when-system-administrators-are-locked-out.md)  
  
-   [Connect to a Registered Server &#40;SQL Server Management Studio&#41;](../../tools/sql-server-management-studio/connect-to-a-registered-server-sql-server-management-studio.md)  
  
-   [Connect to Any SQL Server Component from SQL Server Management Studio](../../ssms/f1-help/connect-to-any-sql-server-component-from-sql-server-management-studio.md)  
  
-   [Connect to the Database Engine With sqlcmd](../../relational-databases/scripting/sqlcmd-connect-to-the-database-engine.md)  
  
-   [How to Troubleshoot Connecting to the SQL Server Database Engine](https://social.technet.microsoft.com/wiki/contents/articles/2102.how-to-troubleshoot-connecting-to-the-sql-server-database-engine.aspx)  
  
 Programmers may be interested in the following topics:  
  
-   [How To: Connect to SQL Server Using SQL Authentication in ASP.NET 2.0](https://msdn.microsoft.com/library/ff648340.aspx)  
  
-   [Connecting to an Instance of SQL Server](../../relational-databases/server-management-objects-smo/create-program/connecting-to-an-instance-of-sql-server.md)  
  
-   [How to: Create Connections to SQL Server Databases](https://msdn.microsoft.com/library/s4yys16a.aspx)  
  
 **The main body of this topic starts here.**  
  
 This topic describes how to configure the **remote access** server configuration option in [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] by using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or [!INCLUDE[tsql](../../includes/tsql-md.md)]. The **remote access** option controls the execution of stored procedures from local or remote servers on which instances of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] are running. This default value for this option is 1. This grants permission to run local stored procedures from remote servers or remote stored procedures from the local server. To prevent local stored procedures from being run from a remote server or remote stored procedures from being run on the local server, set the option to 0.  
  
> [!IMPORTANT]  
>  [!INCLUDE[ssNoteDepNextDontUse](../../includes/ssnotedepnextdontuse-md.md)] Use [sp_addlinkedserver](../../relational-databases/system-stored-procedures/sp-addlinkedserver-transact-sql.md) instead.
  
 **In This Topic**  
  
-   **Before you begin:**  
  
     [Limitations and Restrictions](#Restrictions)  
  
     [Security](#Security)  
  
-   **To configure the remote access option, using:**  
  
     [SQL Server Management Studio](#SSMSProcedure)  
  
     [Transact-SQL](#TsqlProcedure)  
  
-   **Follow Up:**  [After you configure the remote access option](#FollowUp)  
  
##  <a name="BeforeYouBegin"></a> Before You Begin  
  
###  <a name="Restrictions"></a> Limitations and Restrictions  
  
-   The **remote access** option only applies to servers that are added by using [sp_addserver](../../relational-databases/system-stored-procedures/sp-addserver-transact-sql.md), and is included for backward compatibility.  
  
###  <a name="Security"></a> Security  
  
####  <a name="Permissions"></a> Permissions  
 Execute permissions on **sp_configure** with no parameters or with only the first parameter are granted to all users by default. To execute **sp_configure** with both parameters to change a configuration option or to run the RECONFIGURE statement, a user must be granted the ALTER SETTINGS server-level permission. The ALTER SETTINGS permission is implicitly held by the **sysadmin** and **serveradmin** fixed server roles.  
  
##  <a name="SSMSProcedure"></a> Using SQL Server Management Studio  
  
#### To configure the remote access option  
  
1.  In Object Explorer, right-click a server and select **Properties**.  
  
2.  Click the **Connections** node.  
  
3.  Under **Remote server connections**, select or clear the **Allow remote connections to this server** check box.  
  
##  <a name="TsqlProcedure"></a> Using Transact-SQL  
  
#### To configure the remote access option  
  
1.  Connect to the [!INCLUDE[ssDE](../../includes/ssde-md.md)].  
  
2.  From the Standard bar, click **New Query**.  
  
3.  Copy and paste the following example into the query window and click **Execute**. This example shows how to use [sp_configure](../../relational-databases/system-stored-procedures/sp-configure-transact-sql.md) to set the value of the `remote access` option to `0`.  
  
```sql  
EXEC sp_configure 'remote access', 0 ;  
GO  
RECONFIGURE ;  
GO  
  
```  
  
 For more information, see [Server Configuration Options &#40;SQL Server&#41;](../../database-engine/configure-windows/server-configuration-options-sql-server.md).  
  
##  <a name="FollowUp"></a> Follow Up: After you configure the remote access option  
 This setting does not take effect until you restart SQL Server.  
  
## See Also  
 [RECONFIGURE &#40;Transact-SQL&#41;](../../t-sql/language-elements/reconfigure-transact-sql.md)   
 [Server Configuration Options &#40;SQL Server&#41;](../../database-engine/configure-windows/server-configuration-options-sql-server.md)   
 [sp_configure &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-configure-transact-sql.md)  
  
  
