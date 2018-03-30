---
title: "Configure the fill factor Server Configuration Option | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
ms.topic: "article"
helpviewer_keywords: 
  - "fill factor option [SQL Server]"
ms.assetid: b920ec34-ba8b-4bb8-af53-a3ffd06bafa6
caps.latest.revision: 28
author: "craigg-msft"
ms.author: "rickbyh"
manager: "jhubbard"
---
# Configure the fill factor Server Configuration Option
  This topic describes how to configure the **fill factor** server configuration option in [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] by using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or [!INCLUDE[tsql](../../includes/tsql-md.md)]. Fill factor is provided for fine-tuning index data storage and performance. When an index is created or rebuilt, the fill-factor value determines the percentage of space on each leaf-level page to be filled with data, reserving the rest as free space for future growth. For more information, see [Specify Fill Factor for an Index](../../2014/database-engine/specify-fill-factor-for-an-index.md).  
  
 **In This Topic**  
  
-   **Before you begin:**  
  
     [Recommendations](#Recommendations)  
  
     [Security](#Security)  
  
-   **To configure the fill factor option, using:**  
  
     [SQL Server Management Studio](#SSMSProcedure)  
  
     [Transact-SQL](#TsqlProcedure)  
  
-   **Follow Up:**  [After you configure the fill factor option](#FollowUp)  
  
##  <a name="BeforeYouBegin"></a> Before You Begin  
  
###  <a name="Recommendations"></a> Recommendations  
  
-   This option is an advanced option and should be changed only by an experienced database administrator or certified [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] technician.  
  
###  <a name="Security"></a> Security  
  
####  <a name="Permissions"></a> Permissions  
 Execute permissions on **sp_configure** with no parameters or with only the first parameter are granted to all users by default. To execute **sp_configure** with both parameters to change a configuration option or to run the RECONFIGURE statement, a user must be granted the ALTER SETTINGS server-level permission. The ALTER SETTINGS permission is implicitly held by the **sysadmin** and **serveradmin** fixed server roles.  
  
##  <a name="SSMSProcedure"></a> Using SQL Server Management Studio  
  
#### To configure the fill factor option  
  
1.  In Object Explorer, right-click a server and select **Properties**.  
  
2.  Click the **Database Settings** node.  
  
3.  In the **Default index fill factor** box, type or select the index fill factor that you want.  
  
##  <a name="TsqlProcedure"></a> Using Transact-SQL  
  
#### To configure the fill factor option  
  
1.  Connect to the [!INCLUDE[ssDE](../../includes/ssde-md.md)].  
  
2.  From the Standard bar, click **New Query**.  
  
3.  Copy and paste the following example into the query window and click **Execute**. This example shows how to use [sp_configure](../Topic/sp_configure%20\(Transact-SQL\).md) to set the value of the `fill factor` option to `100`.  
  
```tsql  
Use AdventureWorks2012;  
GO  
sp_configure 'show advanced options', 1;  
GO  
RECONFIGURE;  
GO  
sp_configure 'fill factor', 100;  
GO  
RECONFIGURE;  
GO  
```  
  
 For more information, see [Server Configuration Options &#40;SQL Server&#41;](../../2014/database-engine/server-configuration-options-sql-server.md).  
  
##  <a name="FollowUp"></a> Follow Up: After you configure the fill factor option  
 The server must be restarted before the setting can take effect.  
  
## See Also  
 [RECONFIGURE &#40;Transact-SQL&#41;](../Topic/RECONFIGURE%20\(Transact-SQL\).md)   
 [ALTER INDEX &#40;Transact-SQL&#41;](../Topic/ALTER%20INDEX%20\(Transact-SQL\).md)   
 [CREATE INDEX &#40;Transact-SQL&#41;](../Topic/CREATE%20INDEX%20\(Transact-SQL\).md)   
 [Specify Fill Factor for an Index](../../2014/database-engine/specify-fill-factor-for-an-index.md)   
 [Server Configuration Options &#40;SQL Server&#41;](../../2014/database-engine/server-configuration-options-sql-server.md)   
 [sp_configure &#40;Transact-SQL&#41;](../Topic/sp_configure%20\(Transact-SQL\).md)   
 [sys.indexes &#40;Transact-SQL&#41;](../Topic/sys.indexes%20\(Transact-SQL\).md)  
  
  