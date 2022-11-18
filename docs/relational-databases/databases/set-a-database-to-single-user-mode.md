---
title: "Set a database to single-user mode"
description: "Set a database to single-user mode"
ms.service: sql
ms.subservice: 
ms.topic: conceptual
helpviewer_keywords: 
  - "single-user mode [SQL Server], database option"
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: ""
ms.custom: FY22Q2Fresh
ms.date: "10/21/2021"
---
# Set a database to single-user mode

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

This article describes how to set a user-defined database to single-user mode in [!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)] by using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or [!INCLUDE[tsql](../../includes/tsql-md.md)]. Single-user mode specifies that only one user at a time can access the database and is generally used for maintenance actions.  
  
##  <a name="Restrictions"></a> Limitations and restrictions  
  
-   If other users are connected to the database at the time that you set the database to single-user mode, their connections to the database will be closed without warning. 
  
-   The database remains in single-user mode even if the after the user that set the option is disconnected. At that point, a different user, but only one, can connect to the database.  
  
##  <a name="Prerequisites"></a> Prerequisites  
  
-   Before you set the database to SINGLE_USER, verify that the AUTO_UPDATE_STATISTICS_ASYNC option is set to OFF. When this option is set to ON, the background thread that is used to update statistics takes a connection against the database, and you will be unable to access the database in single-user mode. For more information, see [ALTER DATABASE SET Options &#40;Transact-SQL&#41;](../../t-sql/statements/alter-database-transact-sql-set-options.md).  
  
## <a name="Security"></a><a name="Permissions"></a> Permissions  
 Requires ALTER permission on the database.  
  
##  <a name="SSMSProcedure"></a> Use SQL Server Management Studio  
  
 To set a database to single-user mode:
  
1.  In **Object Explorer**, connect to an instance of the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)], and then expand that instance.  
  
2.  Right-click the database to change, and then select **Properties**.  
  
3.  In the **Database Properties** dialog box, select the **Options** page.  
  
4.  From the **Restrict Access** option, select **Single**.  
  
5.  If other users are connected to the database, an **Open Connections** message will appear. To change the property and close all other connections, select **Yes**.  
  
 You can also set the database to **Multiple** or **Restricted** access by using this procedure. For more information about the Restrict Access options, see [Database Properties &#40;Options Page&#41;](../../relational-databases/databases/database-properties-options-page.md).  
  
##  <a name="TsqlProcedure"></a> Use Transact-SQL  
  
 To set a database to single-user mode:
  
1.  Connect to the [!INCLUDE[ssDE](../../includes/ssde-md.md)].  
  
2.  From the Standard bar, select **New Query**.  
  
3.  Copy and paste the following example into the query window and select **Execute**. This example sets the database to `SINGLE_USER` mode to obtain exclusive access. The example then sets the state of the [!INCLUDE[ssSampleDBobject](../../includes/sssampledbobject-md.md)] database to `READ_ONLY` and returns access to the database to all users.

> [!WARNING]
> To quickly obtain exclusive access, the code sample uses the termination option `WITH ROLLBACK IMMEDIATE`. This will cause all incomplete transactions to be rolled back and any other connections to the [!INCLUDE[ssSampleDBobject](../../includes/sssampledbobject-md.md)] database to be immediately disconnected.  
  
 [!code-sql[DatabaseDDL#AlterDatabase8](../../relational-databases/databases/codesnippet/tsql/set-a-database-to-single_1.sql)]  
  
## Next steps  
 - [ALTER DATABASE &#40;Transact-SQL&#41;](../../t-sql/statements/alter-database-transact-sql.md)  
  
  
