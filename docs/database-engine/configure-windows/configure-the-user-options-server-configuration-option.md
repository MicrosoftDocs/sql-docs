---
title: "Configure the user options Server Configuration Option | Microsoft Docs"
ms.custom: ""
ms.date: "03/02/2017"
ms.prod: sql
ms.prod_service: high-availability
ms.reviewer: ""
ms.technology: configuration
ms.topic: conceptual
helpviewer_keywords: 
  - "global default for all users [SQL Server]"
  - "users [SQL Server], global defaults"
  - "user options option [SQL Server]"
ms.assetid: cfed8f86-6bcf-4b90-88eb-9656e22d5dc5
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
---
# Configure the user options Server Configuration Option
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]

  This topic describes how to configure the **user options** server configuration option in [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] by using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or [!INCLUDE[tsql](../../includes/tsql-md.md)]. The **user options** option specifies global defaults for all users. A list of default query processing options is established for the duration of a user's work session. The **user options** option allows you to change the default values of the SET options (if the server's default settings are not appropriate).  
  
 A user can override these defaults by using the SET statement. You can configure **user options** dynamically for new logins. After you change the setting of **user options**, new login sessions use the new setting; current login sessions are not affected.  
  
 **In This Topic**  
  
-   **Before you begin:**  
  
     [Recommendations](#Recommendations)  
  
     [Security](#Security)  
  
-   **To configure the user options configuration option, using:**  
  
     [SQL Server Management Studio](#SSMSProcedure)  
  
     [Transact-SQL](#TsqlProcedure)  
  
-   **Follow Up:**  [After you configure the user options configuration option](#FollowUp)  
  
##  <a name="BeforeYouBegin"></a> Before You Begin  
  
###  <a name="Recommendations"></a> Recommendations  
  
-   The following table lists and describes the configuration values for **user options**. Not all configuration values are compatible with each other. For example, ANSI_NULL_DFLT_ON and ANSI_NULL_DFLT_OFF cannot be set at the same time.  
  
    |Value|Configuration|Description|  
    |-----------|-------------------|-----------------|  
    |1|DISABLE_DEF_CNST_CHK|Controls interim or deferred constraint checking.|  
    |2|IMPLICIT_TRANSACTIONS|For dblib network library connections, controls whether a transaction is started implicitly when a statement is executed. The IMPLICIT_TRANSACTIONS setting has no effect on ODBC or OLEDB connections.|  
    |4|CURSOR_CLOSE_ON_COMMIT|Controls behavior of cursors after a commit operation has been performed.|  
    |8|ANSI_WARNINGS|Controls truncation and NULL in aggregate warnings.|  
    |16|ANSI_PADDING|Controls padding of fixed-length variables.|  
    |32|ANSI_NULLS|Controls NULL handling when using equality operators.|  
    |64|ARITHABORT|Terminates a query when an overflow or divide-by-zero error occurs during query execution.|  
    |128|ARITHIGNORE|Returns NULL when an overflow or divide-by-zero error occurs during a query.|  
    |256|QUOTED_IDENTIFIER|Differentiates between single and double quotation marks when evaluating an expression.|  
    |512|NOCOUNT|Turns off the message returned at the end of each statement that states how many rows were affected.|  
    |1024|ANSI_NULL_DFLT_ON|Alters the session's behavior to use ANSI compatibility for nullability. New columns defined without explicit nullability are defined to allow nulls.|  
    |2048|ANSI_NULL_DFLT_OFF|Alters the session's behavior not to use ANSI compatibility for nullability. New columns defined without explicit nullability do not allow nulls.|  
    |4096|CONCAT_NULL_YIELDS_NULL|Returns NULL when concatenating a NULL value with a string.|  
    |8192|NUMERIC_ROUNDABORT|Generates an error when a loss of precision occurs in an expression.|  
    |16384|XACT_ABORT|Rolls back a transaction if a Transact-SQL statement raises a run-time error.|  
  
-   The bit positions in **user options** are identical to those in @@OPTIONS. Each connection has its own @@OPTIONS function, which represents the configuration environment. When logging in to an instance of \ [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], a user receives a default environment that assigns the current **user options** value to @@OPTIONS. Executing SET statements for **user options** affects the corresponding value in the session's @@OPTIONS function. All connections created after this setting is changed receive the new value.  
  
###  <a name="Security"></a> Security  
  
####  <a name="Permissions"></a> Permissions  
 Execute permissions on **sp_configure** with no parameters or with only the first parameter are granted to all users by default. To execute **sp_configure** with both parameters to change a configuration option or to run the RECONFIGURE statement, a user must be granted the ALTER SETTINGS server-level permission. The ALTER SETTINGS permission is implicitly held by the **sysadmin** and **serveradmin** fixed server roles.  
  
##  <a name="SSMSProcedure"></a> Using SQL Server Management Studio  
  
#### To configure the user options configuration option  
  
1.  In Object Explorer, right-click a server and select **Properties**.  
  
2.  Click the **Connections** node.  
  
3.  In the **Default connection options** box, select one or more attributes to configure the default query-processing options for all connected users.  
  
     By default, no user options are configured.  
  
##  <a name="TsqlProcedure"></a> Using Transact-SQL  
  
#### To configure the user options configuration option  
  
1.  Connect to the [!INCLUDE[ssDE](../../includes/ssde-md.md)].  
  
2.  From the Standard bar, click **New Query**.  
  
3.  Copy and paste the following example into the query window and click **Execute**. This example shows how to use [sp_configure](../../relational-databases/system-stored-procedures/sp-configure-transact-sql.md) to configure the `user options` to change the setting for the ANSI_WARNINGS server option.  
  
```sql  
USE AdventureWorks2012 ;  
GO  
EXEC sp_configure 'user options', 8 ;  
GO  
RECONFIGURE ;  
GO  
  
```  
  
##  <a name="FollowUp"></a> Follow Up: After you configure the user options configuration option  
 The setting takes effect immediately without restarting the server.  
  
## See Also  
 [RECONFIGURE &#40;Transact-SQL&#41;](../../t-sql/language-elements/reconfigure-transact-sql.md)   
 [Server Configuration Options &#40;SQL Server&#41;](../../database-engine/configure-windows/server-configuration-options-sql-server.md)   
 [sp_configure &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-configure-transact-sql.md)   
 [SET Statements &#40;Transact-SQL&#41;](../../t-sql/statements/set-statements-transact-sql.md)  
  
  
