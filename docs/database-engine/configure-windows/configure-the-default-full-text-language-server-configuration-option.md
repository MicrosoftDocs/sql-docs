---
title: "Configure the default full-text language Server Configuration Option"
description: Learn about the default full-text language option. See how to configure it to specify the default language that SQL Server uses for full-text indexes.
author: rwestMSFT
ms.author: randolphwest
ms.date: "03/02/2017"
ms.service: sql
ms.subservice: configuration
ms.topic: conceptual
helpviewer_keywords:
  - "languages [full-text search]"
  - "default full-text language option"
---
# Configure the default full-text language Server Configuration Option
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  This topic describes how to configure the **default full-text language** server configuration option in [!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)] by using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or [!INCLUDE[tsql](../../includes/tsql-md.md)]. The **default full-text language** option specifies a default language value for full-text indexes. Linguistic analysis is performed on all data that is full-text indexed and is dependent on the language of the data. The default value of this option is the language of the server. For a localized version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Setup sets the **default full-text language** option to the language of the server if an appropriate match exists. For a non-localized version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], the **default full-text language** option is English.  
  
 **In This Topic**  
  
-   **Before you begin:**  
  
     [Limitations and Restrictions](#Restrictions)  
  
     [Recommendations](#Recommendations)  
  
     [Security](#Security)  
  
-   **To configure the default full-text language option, using:**  
  
     [SQL Server Management Studio](#SSMSProcedure)  
  
     [Transact-SQL](#TsqlProcedure)  
  
-   **Follow Up:**  [After you configure the default full-text language option](#FollowUp)  
  
##  <a name="BeforeYouBegin"></a> Before You Begin  
  
###  <a name="Restrictions"></a> Limitations and Restrictions  
  
-   The value of the **default full-text language** option is used in a full-text index when no language is specified for a column through the LANGUAGE **language_term** option in the CREATE FULLTEXT INDEX or ALTER FULLTEXT INDEX statements. If the default full-text language is not supported or the linguistic analysis package is not available, the CREATE or ALTER operation will fail and [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] will return an error message stating that the language specified is not valid.  
  
###  <a name="Recommendations"></a> Recommendations  
  
-   This option is an advanced option and should be changed only by an experienced database administrator or certified [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] professional.  
  
-   The **default full-text language** option requires an LCID value. For a list of supported LCIDs and their related languages, see [sys.fulltext_languages &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-fulltext-languages-transact-sql.md). Other languages may also be available from independent software vendors, for example. If no specific language dialect is found, the Full-Text Engine will automatically switch to the primary language.  
  
###  <a name="Security"></a> Security  
  
####  <a name="Permissions"></a> Permissions  
 Execute permissions on **sp_configure** with no parameters or with only the first parameter are granted to all users by default. To execute **sp_configure** with both parameters to change a configuration option or to run the RECONFIGURE statement, a user must be granted the ALTER SETTINGS server-level permission. The ALTER SETTINGS permission is implicitly held by the **sysadmin** and **serveradmin** fixed server roles.  
  
##  <a name="SSMSProcedure"></a> Using SQL Server Management Studio  
  
#### To configure the default full-text language option  
  
1.  In Object Explorer, right-click a server and select **Properties**.  
  
2.  Click the **Advanced** node.  
  
3.  Under Miscellaneous, use **Default Full Text Language** to specify a default language value for full-text indexed columns.  
  
##  <a name="TsqlProcedure"></a> Using Transact-SQL  
  
#### To configure the default full-text language option  
  
1.  Connect to the [!INCLUDE[ssDE](../../includes/ssde-md.md)].  
  
2.  From the Standard bar, click **New Query**.  
  
3.  Copy and paste the following example into the query window and click **Execute**. This example shows how to use [sp_configure](../../relational-databases/system-stored-procedures/sp-configure-transact-sql.md) to set the value of the `default full-text` option to Dutch (`1043`).  
  
```sql  
USE AdventureWorks2012 ;  
GO  
EXEC sp_configure 'show advanced options', 1 ;  
GO  
RECONFIGURE  
GO  
EXEC sp_configure 'default full-text language', 1043 ;  
GO  
RECONFIGURE  
GO  
  
```  
  
 For more information, see [Server Configuration Options &#40;SQL Server&#41;](../../database-engine/configure-windows/server-configuration-options-sql-server.md).  
  
##  <a name="FollowUp"></a> Follow Up: After you configure the default full-text language option  
 The setting takes effect immediately without restarting the server.  
  
## See Also  
 [sys.fulltext_languages &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-fulltext-languages-transact-sql.md)   
 [RECONFIGURE &#40;Transact-SQL&#41;](../../t-sql/language-elements/reconfigure-transact-sql.md)   
 [Server Configuration Options &#40;SQL Server&#41;](../../database-engine/configure-windows/server-configuration-options-sql-server.md)   
 [sp_configure &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-configure-transact-sql.md)   
 [CREATE FULLTEXT INDEX &#40;Transact-SQL&#41;](../../t-sql/statements/create-fulltext-index-transact-sql.md)   
 [ALTER FULLTEXT INDEX &#40;Transact-SQL&#41;](../../t-sql/statements/alter-fulltext-index-transact-sql.md)  
  
  
