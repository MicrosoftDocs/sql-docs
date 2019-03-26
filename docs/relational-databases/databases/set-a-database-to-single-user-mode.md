---
title: "Set a Database to Single-user Mode | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: 
ms.topic: conceptual
helpviewer_keywords: 
  - "single-user mode [SQL Server], database option"
ms.assetid: fb5254eb-b635-4b39-8361-136fd36f2b1f
author: "stevestein"
ms.author: "sstein"
manager: craigg
---
# Set a Database to Single-user Mode
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  This topic describes how to set a user-defined database to single-user mode in [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] by using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or [!INCLUDE[tsql](../../includes/tsql-md.md)]. Single-user mode specifies that only one user at a time can access the database and is generally used for maintenance actions.  
  
 **In This Topic**  
  
-   **Before you begin:**  
  
     [Limitations and Restrictions](#Restrictions)  
  
     [Prerequisites](#Prerequisites)  
  
     [Security](#Security)  
  
-   **To set a database to single-user mode, using:**  
  
     [SQL Server Management Studio](#SSMSProcedure)  
  
     [Transact-SQL](#TsqlProcedure)  
  
##  <a name="BeforeYouBegin"></a> Before You Begin  
  
###  <a name="Restrictions"></a> Limitations and Restrictions  
  
-   If other users are connected to the database at the time that you set the database to single-user mode, their connections to the database will be closed without warning.  
  
-   The database remains in single-user mode even if the user that set the option logs off. At that point, a different user, but only one, can connect to the database.  
  
###  <a name="Prerequisites"></a> Prerequisites  
  
-   Before you set the database to SINGLE_USER, verify that the AUTO_UPDATE_STATISTICS_ASYNC option is set to OFF. When this option is set to ON, the background thread that is used to update statistics takes a connection against the database, and you will be unable to access the database in single-user mode. For more information, see [ALTER DATABASE SET Options &#40;Transact-SQL&#41;](../../t-sql/statements/alter-database-transact-sql-set-options.md).  
  
###  <a name="Security"></a> Security  
  
####  <a name="Permissions"></a> Permissions  
 Requires ALTER permission on the database.  
  
##  <a name="SSMSProcedure"></a> Using SQL Server Management Studio  
  
#### To set a database to single-user mode  
  
1.  In **Object Explorer**, connect to an instance of the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)], and then expand that instance.  
  
2.  Right-click the database to change, and then click **Properties**.  
  
3.  In the **Database Properties** dialog box, click the **Options** page.  
  
4.  From the **Restrict Access** option, select **Single**.  
  
5.  If other users are connected to the database, an **Open Connections** message will appear. To change the property and close all other connections, click **Yes**.  
  
 You can also set the database to Multiple or Restricted access by using this procedure. For more information about the Restrict Access options, see [Database Properties &#40;Options Page&#41;](../../relational-databases/databases/database-properties-options-page.md).  
  
##  <a name="TsqlProcedure"></a> Using Transact-SQL  
  
#### To set a database to single-user mode  
  
1.  Connect to the [!INCLUDE[ssDE](../../includes/ssde-md.md)].  
  
2.  From the Standard bar, click **New Query**.  
  
3.  Copy and paste the following example into the query window and click **Execute**. This example sets the database to `SINGLE_USER` mode to obtain exclusive access. The example then sets the state of the [!INCLUDE[ssSampleDBobject](../../includes/sssampledbobject-md.md)] database to `READ_ONLY` and returns access to the database to all users.The termination option `WITH ROLLBACK IMMEDIATE` is specified in the first `ALTER DATABASE` statement. This will cause all incomplete transactions to be rolled back and any other connections to the [!INCLUDE[ssSampleDBobject](../../includes/sssampledbobject-md.md)] database to be immediately disconnected.  
  
 [!code-sql[DatabaseDDL#AlterDatabase8](../../relational-databases/databases/codesnippet/tsql/set-a-database-to-single_1.sql)]  
  
## See Also  
 [ALTER DATABASE &#40;Transact-SQL&#41;](../../t-sql/statements/alter-database-transact-sql.md)  
  
  
