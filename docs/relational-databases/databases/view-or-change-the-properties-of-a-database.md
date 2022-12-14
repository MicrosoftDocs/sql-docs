---
title: "View or Change the Properties of a Database | Microsoft Docs"
description: Learn how to view or change the properties of a database in SQL Server by using SQL Server Management Studio or Transact-SQL. 
ms.custom: ""
ms.date: "01/05/2018"
ms.service: sql
ms.reviewer: ""
ms.subservice: supportability
ms.topic: conceptual
helpviewer_keywords: 
  - "displaying databases"
  - "database viewing [SQL Server]"
  - "databases [SQL Server], viewing"
  - "viewing databases"
ms.assetid: 9e8ac097-84b7-46c7-85e3-c1e79f94d747
author: WilliamDAssafMSFT
ms.author: wiassaf
monikerRange: "=azuresqldb-current||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# View or Change the Properties of a Database
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

  This topic describes how to view or change the properties of a database in [!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)] by using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or [!INCLUDE[tsql](../../includes/tsql-md.md)]. After you change a database property, the modification takes effect immediately.  
  
 **In This Topic**  
  
-   **Before you begin:**  
  
     [Recommendations](#Recommendations)  
  
     [Security](#Security)  
  
-   **To view or change the properties of a database, using:**  
  
     [SQL Server Management Studio](#SSMSProcedure)  
  
     [Transact-SQL](#TsqlProcedure)  
  
##  <a name="BeforeYouBegin"></a> Before You Begin  
  
###  <a name="Recommendations"></a> Recommendations  
  
-   When AUTO_CLOSE is ON, some columns in the [sys.databases](../../relational-databases/system-catalog-views/sys-databases-transact-sql.md) catalog view and DATABASEPROPERTYEX function will return NULL because the database is unavailable to retrieve the data. To resolve this, open the database.  
  
###  <a name="Security"></a> Security  
  
####  <a name="Permissions"></a> Permissions  
 Requires ALTER permission on the database to change the properties of a database. Requires at least membership in the Public database role to view the properties of a database.  
  
##  <a name="SSMSProcedure"></a> Using SQL Server Management Studio  
  
#### To view or change the properties of a database  
  
1.  In **Object Explorer**, connect to an instance of the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)], and then expand that instance.  
  
2.  Expand **Databases**, right-click the database to view, and then click **Properties**.  
  
3.  In the **Database Properties** dialog box, select a page to view the corresponding information. For example, select the **Files** page to view data and log file information.  
  
##  <a name="TsqlProcedure"></a> Using Transact-SQL  
 Transact-SQL provides a number of different methods for viewing the properties of a database and for changing the properties of a database. To view the properties of a database, you can use the [DATABASEPROPERTYEX &#40;Transact-SQL&#41;](../../t-sql/functions/databasepropertyex-transact-sql.md) function and the [sys.databases &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-databases-transact-sql.md) catalog view. To change the properties of a database, you can use the version of the ALTER DATABASE statement for your environment:  [ALTER DATABASE &#40;Transact-SQL&#41;](../../t-sql/statements/alter-database-transact-sql.md) or [ALTER DATABASE (Azure SQL Database)](../../t-sql/statements/alter-database-transact-sql.md). To view database scoped properties, use the [sys.database_scoped_configurations &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-database-scoped-configurations-transact-sql.md) catalog view and to alter database scoped properties, use the [ALTER DATABASE SCOPED CONFIGURATION &#40;Transact-SQL&#41;](../../t-sql/statements/alter-database-scoped-configuration-transact-sql.md) statement.  
  
#### To view a property of a database by using the DATABASEPROPERTYEX function  
  
1.  Connect to the [!INCLUDE[ssDE](../../includes/ssde-md.md)] and then connect to the database for which you wish to view its properties.  
  
2.  From the Standard bar, click **New Query**.  
  
3.  Copy and paste the following example into the query window and click **Execute**. This example uses the [DATABASEPROPERTYEX](../../t-sql/functions/databasepropertyex-transact-sql.md) system function to return the status of the AUTO_SHRINK database option in the [!INCLUDE[ssSampleDBobject](../../includes/sssampledbobject-md.md)] database. A return value of 1 means that the option is set to ON, and a return value of 0 means that the option is set to OFF.  
  
    ```sql  
    SELECT DATABASEPROPERTYEX('AdventureWorks2012', 'IsAutoShrink');  
    ```  
  
#### To view the properties of a database by querying sys.databases  
  
1.  Connect to the [!INCLUDE[ssDE](../../includes/ssde-md.md)] and then connect to the database for which you wish to view its properties..  
  
2.  From the Standard bar, click **New Query**.  
  
3.  Copy and paste the following example into the query window and click **Execute**. This example queries the [sys.databases](../../relational-databases/system-catalog-views/sys-databases-transact-sql.md) catalog view to view several properties of the [!INCLUDE[ssSampleDBobject](../../includes/sssampledbobject-md.md)] database. This example returns the database ID number (`database_id`), whether the database is read-only or read-write (`is_read_only`), the collation for the database (`collation_name`), and the database compatibility level (`compatibility_level`).  
  
    ```sql  
    SELECT database_id, is_read_only, collation_name, compatibility_level  
    FROM sys.databases WHERE name = 'AdventureWorks2012';  
    ```  
  
#### To view the properties of a database-scoped configuration by querying sys.databases_scoped_configuration  
  
1.  Connect to the [!INCLUDE[ssDE](../../includes/ssde-md.md)] and then connect to the database for which you wish to view its properties..  
  
2.  From the Standard bar, click **New Query**.  
  
3.  Copy and paste the following example into the query window and click **Execute**. This example queries the [sys.database_scoped_configurations &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-database-scoped-configurations-transact-sql.md) catalog view to view several properties of the current database.  
  
    ```sql  
    SELECT configuration_id, name, value, value_for_secondary  
    FROM sys.database_scoped_configurations;  
    ```  
  
     For more examples, see [sys.database_scoped_configurations &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-database-scoped-configurations-transact-sql.md)  
  
#### To change the properties of a SQL Server 2016 database using ALTER DATABASE  
  
1.  Connect to the [!INCLUDE[ssDE](../../includes/ssde-md.md)].  
  
2.  From the Standard bar, click **New Query**.  
  
3.  Copy and paste the following example into the query window. The example determines the state of snapshot isolation on the [!INCLUDE[ssSampleDBobject](../../includes/sssampledbobject-md.md)] database, changes the state of the property, and then verifies the change.  
  
     To determine the state of snapshot isolation, select the first `SELECT` statement and click **Execute**.  
  
     To change the state of snapshot isolation, select the `ALTER DATABASE` statement and click **Execute**.  
  
     To verify the change, select the second `SELECT` statement, and click **Execute**.  
  
     [!code-sql[DatabaseDDL#AlterDatabase9](../../relational-databases/databases/codesnippet/tsql/view-or-change-the-prope_1.sql)]  
  
#### To change the database-scoped properties using ALTER DATABASE SCOPED CONFIGURATION  
  
1.  Connect to a database in your SQL Server instance.  
  
2.  From the Standard bar, click **New Query**.  
  
3.  Copy and paste the following example into the query window. The following example sets MAXDOP for a secondary database to the value for the primary database.  
  
    ```sql  
    ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET MAXDOP = PRIMARY   
    ```  
  
## See Also  
 [sys.databases &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-databases-transact-sql.md)   
 [DATABASEPROPERTYEX &#40;Transact-SQL&#41;](../../t-sql/functions/databasepropertyex-transact-sql.md)   
 [ALTER DATABASE &#40;Transact-SQL&#41;](../../t-sql/statements/alter-database-transact-sql.md)   
 [ALTER DATABASE (Azure SQL Database)](../../t-sql/statements/alter-database-transact-sql.md)   
 [ALTER DATABASE SCOPED CONFIGURATION &#40;Transact-SQL&#41;](../../t-sql/statements/alter-database-scoped-configuration-transact-sql.md)   
 [sys.database_scoped_configurations &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-database-scoped-configurations-transact-sql.md)  

  
