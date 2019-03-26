---
title: "Configure the priority boost Server Configuration Option | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: configuration
ms.topic: conceptual
helpviewer_keywords: 
  - "priority boost option"
ms.assetid: 765f1e83-dd52-44fb-b0c8-1078f213607b
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
---
# Configure the priority boost Server Configuration Option
  This topic describes how to configure the **priority boost** configuration option in [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] by using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or [!INCLUDE[tsql](../../includes/tsql-md.md)]. Use the **priority boost** option to specify whether [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] should run at a higher [!INCLUDE[msCoName](../../includes/msconame-md.md)] Windows 2008 or Windows 2008 R2 scheduling priority than other processes on the same computer. If you set this option to 1, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] runs at a priority base of 13 in the Windows 2008 or Windows Server 2008 R2 scheduler. The default is 0, which is a priority base of 7.  
  
> [!IMPORTANT]  
>  [!INCLUDE[ssNoteDepFutureDontUse](../../includes/ssnotedepfuturedontuse-md.md)]  
  
 **In This Topic**  
  
-   **Before you begin:**  
  
     [Limitations and Restrictions](#Restrictions)  
  
     [Security](#Security)  
  
-   **To configure the priority boost option, using:**  
  
     [SQL Server Management Studio](#SSMSProcedure)  
  
     [Transact-SQL](#TsqlProcedure)  
  
-   **Follow Up:**  [After you configure the priority boost option](#FollowUp)  
  
##  <a name="BeforeYouBegin"></a> Before You Begin  
  
###  <a name="Restrictions"></a> Limitations and Restrictions  
  
-   Raising the priority too high may drain resources from essential operating system and network functions, resulting in problems shutting down SQL Server or using other operating system tasks on the server.  
  
###  <a name="Security"></a> Security  
  
####  <a name="Permissions"></a> Permissions  
 Execute permissions on **sp_configure** with no parameters or with only the first parameter are granted to all users by default. To execute **sp_configure** with both parameters to change a configuration option or to run the RECONFIGURE statement, a user must be granted the ALTER SETTINGS server-level permission. The ALTER SETTINGS permission is implicitly held by the **sysadmin** and **serveradmin** fixed server roles.  
  
##  <a name="SSMSProcedure"></a> Using SQL Server Management Studio  
  
#### To configure the priority boost option  
  
1.  In Object Explorer, right-click a server and select **Properties**.  
  
2.  Click the **Processors** node.  
  
3.  Under **Threads**, select the **Boost SQL Server priority** check box.  
  
4.  Stop and restart [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
##  <a name="TsqlProcedure"></a> Using Transact-SQL  
  
#### To configure the priority boost option  
  
1.  Connect to the [!INCLUDE[ssDE](../../includes/ssde-md.md)].  
  
2.  From the Standard bar, click **New Query**.  
  
3.  Copy and paste the following example into the query window and click **Execute**. This example shows how to use [sp_configure](/sql/relational-databases/system-stored-procedures/sp-configure-transact-sql) to set the value of the `priority boost` option to `1`.  
  
```tsql  
USE AdventureWorks2012 ;  
GO  
EXEC sp_configure 'show advanced options', 1;  
GO  
RECONFIGURE ;  
GO  
EXEC sp_configure 'priority boost', 1 ;  
GO  
RECONFIGURE;  
GO  
  
```  
  
 For more information, see [Server Configuration Options &#40;SQL Server&#41;](server-configuration-options-sql-server.md).  
  
##  <a name="FollowUp"></a> Follow Up: After you configure the priority boost option  
 The server must be restarted before the setting can take effect.  
  
## See Also  
 [RECONFIGURE &#40;Transact-SQL&#41;](/sql/t-sql/language-elements/reconfigure-transact-sql)   
 [Server Configuration Options &#40;SQL Server&#41;](server-configuration-options-sql-server.md)   
 [sp_configure &#40;Transact-SQL&#41;](/sql/relational-databases/system-stored-procedures/sp-configure-transact-sql)  
  
  
