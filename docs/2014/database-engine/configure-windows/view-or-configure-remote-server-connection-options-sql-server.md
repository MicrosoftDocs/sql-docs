---
title: "View or Configure Remote Server Connection Options (SQL Server) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: configuration
ms.topic: conceptual
helpviewer_keywords: 
  - "remote servers [SQL Server], connection options"
  - "servers [SQL Server], remote"
  - "connections [SQL Server], remote servers"
ms.assetid: 356d3e6b-8514-4bd2-a683-9de147949b2b
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
---
# View or Configure Remote Server Connection Options (SQL Server)
  This topic describes how to view or configure remote server connection options at the server level in [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] by using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or [!INCLUDE[tsql](../../includes/tsql-md.md)].  
  
 **In This Topic**  
  
-   **Before you begin:**  
  
     [Security](#Security)  
  
-   **To view or configure remote server connection options, using:**  
  
     [SQL Server Management Studio](#SSMSProcedure)  
  
     [Transact-SQL](#TsqlProcedure)  
  
-   **Follow Up:**  [After you configure remote server connection options](#FollowUp)  
  
##  <a name="BeforeYouBegin"></a> Before You Begin  
  
###  <a name="Security"></a> Security  
  
####  <a name="Permissions"></a> Permissions  
 Executing **sp_serveroption** requires ALTER ANY LINKED SERVER permission on the server.  
  
##  <a name="SSMSProcedure"></a> Using SQL Server Management Studio  
  
#### To view or configure remote server connection options  
  
1.  In Object Explorer, right-click a server, and then click **Properties**.  
  
2.  In the **SQL Server Properties - \<***server_name***>** dialog box, click **Connections**.  
  
3.  On the **Connections** page, review the **Remote server connections** settings, and modify them if necessary.  
  
4.  Repeat steps 1 through 3 on the other server of the remote server pair.  
  
##  <a name="TsqlProcedure"></a> Using Transact-SQL  
  
#### To view remote server connection options  
  
1.  Connect to the [!INCLUDE[ssDE](../../includes/ssde-md.md)].  
  
2.  From the Standard bar, click **New Query**.  
  
3.  Copy and paste the following example into the query window and click **Execute**. This example uses [sp_helpserver](/sql/relational-databases/system-stored-procedures/sp-helpserver-transact-sql) to return information about all remote servers.  
  
```tsql  
USE master;  
GO  
EXEC sp_helpserver ;  
```  
  
#### To configure remote server connection options  
  
1.  Connect to the [!INCLUDE[ssDE](../../includes/ssde-md.md)].  
  
2.  From the Standard bar, click **New Query**.  
  
3.  Copy and paste the following example into the query window and click **Execute**. This example shows how to use [sp_serveroption](/sql/relational-databases/system-stored-procedures/sp-serveroption-transact-sql) to configure a remote server. The example configures a remote server corresponding to another instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], `SEATTLE3`, to be collation compatible with the local instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
```tsql  
USE master;  
EXEC sp_serveroption 'SEATTLE3', 'collation compatible', 'true';  
```  
  
##  <a name="FollowUp"></a> Follow Up: After you configure remote server connection options  
 The remote server must be stopped and restarted before the setting can take effect.  
  
## See Also  
 [Server Configuration Options &#40;SQL Server&#41;](server-configuration-options-sql-server.md)   
 [Remote Servers](remote-servers.md)   
 [Linked Servers &#40;Database Engine&#41;](../../relational-databases/linked-servers/linked-servers-database-engine.md)   
 [sp_linkedservers &#40;Transact-SQL&#41;](/sql/relational-databases/system-stored-procedures/sp-linkedservers-transact-sql)   
 [sp_helpserver &#40;Transact-SQL&#41;](/sql/relational-databases/system-stored-procedures/sp-helpserver-transact-sql)   
 [sp_serveroption &#40;Transact-SQL&#41;](/sql/relational-databases/system-stored-procedures/sp-serveroption-transact-sql)  
  
  
