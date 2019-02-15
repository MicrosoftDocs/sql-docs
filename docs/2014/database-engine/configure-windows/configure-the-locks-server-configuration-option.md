---
title: "Configure the locks Server Configuration Option | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: configuration
ms.topic: conceptual
helpviewer_keywords: 
  - "locks option [SQL Server]"
ms.assetid: b0cf0f86-7652-4574-a9fb-908e10d03973
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
---
# Configure the locks Server Configuration Option
  This topic describes how to configure the **locks** server configuration option in [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] by using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or [!INCLUDE[tsql](../../includes/tsql-md.md)]. The **locks** option sets the maximum number of available locks, thereby limiting the amount of memory the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] uses for them. The default setting is 0, which allows the [!INCLUDE[ssDE](../../includes/ssde-md.md)] to allocate and deallocate lock structures dynamically, based on changing system requirements.  
  
> [!IMPORTANT]  
>  [!INCLUDE[ssNoteDepFutureDontUse](../../includes/ssnotedepfuturedontuse-md.md)]  
  
 **In This Topic**  
  
-   **Before you begin:**  
  
     [Recommendations](#Recommendations)  
  
     [Security](#Security)  
  
-   **To configure the locks option, using:**  
  
     [SQL Server Management Studio](#SSMSProcedure)  
  
     [Transact-SQL](#TsqlProcedure)  
  
-   **Follow Up:**  [After you configure the locks option](#FollowUp)  
  
##  <a name="BeforeYouBegin"></a> Before You Begin  
  
###  <a name="Recommendations"></a> Recommendations  
  
-   This option is an advanced option and should be changed only by an experienced database administrator or certified [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] technician.  
  
-   When the server is started with **locks** set to 0, the lock manager acquires sufficient memory from the [!INCLUDE[ssDE](../../includes/ssde-md.md)] for an initial pool of 2,500 lock structures. As the lock pool is exhausted, additional memory is acquired for the pool.  
  
     Generally, if more memory is required for the lock pool than is available in the [!INCLUDE[ssDE](../../includes/ssde-md.md)] memory pool, and more computer memory is available (the **max server memory** threshold has not been reached), the [!INCLUDE[ssDE](../../includes/ssde-md.md)] allocates memory dynamically to satisfy the request for locks. However, if allocating that memory would cause paging at the operating system level (for example, if another application is running on the same computer as an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and using that memory), more lock space is not allocated. The dynamic lock pool does not acquire more than 60 percent of the memory allocated to the [!INCLUDE[ssDE](../../includes/ssde-md.md)]. After the lock pool has reached 60 percent of the memory acquired by an instance of the [!INCLUDE[ssDE](../../includes/ssde-md.md)], or no more memory is available on the computer, further requests for locks generate an error.  
  
     Allowing [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] to use locks dynamically is the recommended configuration. However, you can set **locks** and override the ability of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] to allocate lock resources dynamically. When **locks** is set to a value other than 0, the [!INCLUDE[ssDE](../../includes/ssde-md.md)] cannot allocate more locks than the value specified in **locks**. Increase this value if [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] displays a message that you have exceeded the number of available locks. Because each lock consumes memory (96 bytes per lock), increasing this value can require increasing the amount of memory dedicated to the server.  
  
-   The **locks** option also affects when lock escalation occurs. When **locks** is set to 0, lock escalation occurs when the memory used by the current lock structures reaches 40 percent of the [!INCLUDE[ssDE](../../includes/ssde-md.md)] memory pool. When **locks** is not set to 0, lock escalation occurs when the number of locks reaches 40 percent of the value specified for **locks**.  
  
###  <a name="Security"></a> Security  
  
####  <a name="Permissions"></a> Permissions  
 Execute permissions on **sp_configure** with no parameters or with only the first parameter are granted to all users by default. To execute **sp_configure** with both parameters to change a configuration option or to run the RECONFIGURE statement, a user must be granted the ALTER SETTINGS server-level permission. The ALTER SETTINGS permission is implicitly held by the **sysadmin** and **serveradmin** fixed server roles.  
  
##  <a name="SSMSProcedure"></a> Using SQL Server Management Studio  
  
#### To configure the locks option  
  
1.  In Object Explorer, right-click a server and select **Properties**.  
  
2.  Click the **Advanced** node.  
  
3.  Under **Parallelism**, type the desired value for the **locks** option.  
  
     Use the **locks** option to set the maximum number of available locks, thereby limiting the amount of memory [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] uses for them.  
  
##  <a name="TsqlProcedure"></a> Using Transact-SQL  
  
#### To configure the locks option  
  
1.  Connect to the [!INCLUDE[ssDE](../../includes/ssde-md.md)].  
  
2.  From the Standard bar, click **New Query**.  
  
3.  Copy and paste the following example into the query window and click **Execute**. This example shows how to use [sp_configure](/sql/relational-databases/system-stored-procedures/sp-configure-transact-sql) to set the value of the `locks` option to set the number of locks available for all users to `20000`.  
  
```tsql  
Use AdventureWorks2012 ;  
GO  
sp_configure 'show advanced options', 1;  
GO  
RECONFIGURE;  
GO  
sp_configure 'locks', 20000;  
GO  
RECONFIGURE;  
GO  
```  
  
 For more information, see [Server Configuration Options &#40;SQL Server&#41;](server-configuration-options-sql-server.md).  
  
##  <a name="FollowUp"></a> Follow Up: After you configure the locks option  
 The server must be restarted before the setting can take effect.  
  
## See Also  
 [RECONFIGURE &#40;Transact-SQL&#41;](/sql/t-sql/language-elements/reconfigure-transact-sql)   
 [Server Configuration Options &#40;SQL Server&#41;](server-configuration-options-sql-server.md)   
 [sp_configure &#40;Transact-SQL&#41;](/sql/relational-databases/system-stored-procedures/sp-configure-transact-sql)  
  
  
