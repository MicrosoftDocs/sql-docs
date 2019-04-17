---
title: "SQL Server Management Studio Support for In-Memory OLTP | Microsoft Docs"
ms.custom: ""
ms.date: "03/07/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: in-memory-oltp
ms.topic: conceptual
ms.assetid: ee847b5f-6a1a-448e-a746-d61a023881ff
author: CarlRabeler
ms.author: carlrab
manager: craigg
---
# SQL Server Management Studio Support for In-Memory OLTP
  [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] is an integrated environment for managing your [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] infrastructure. [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] provides tools to configure, monitor, and administer instances of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. For more information, see [SQL Server Management Studio](../../ssms/sql-server-management-studio-ssms.md)  
  
 The tasks in this topic describe how to use [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] to manage memory-optimized tables; indexes on memory-optimized tables; natively compiled stored procedures; and user-defined, memory-optimized table types.  
  
 For information on how to programmatically create memory-optimized tables, see [Creating a Memory-Optimized Table and a Natively Compiled Stored Procedure](creating-a-memory-optimized-table-and-a-natively-compiled-stored-procedure.md).  
  
### To create a database with a memory-optimized data filegroup  
  
1.  In **Object Explorer**, connect to an instance of the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Database Engine and then expand that instance.  
  
2.  Right-click **Databases**, and then click **New Database**.  
  
3.  To add a new memory-optimized data filegroup, click the **Filegroups** page. Under **MEMORY OPTIMIZED DATA**, click **Add filegroup** and then enter the name of the memory-optimized data filegroup.  The column labeled **FILESTREAM Files** represents the number of containers in the filegroup. Containers are added on the **General** page.  
  
4.  To add a file (container) to the filegroup, click the **General** page. Under **Database files**, click **Add**. Select **File Type** as **FILESTREAM Data**, specify the logical name of the container, select the memory-optimized filegroup, and make sure that **Autogrowth / Maxsize** is set to **Unlimited**.  
  
     For more information on how to create a new database by using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], see [Create a Database](../databases/create-a-database.md).  
  
### To create a memory-optimized table  
  
1.  In **Object Explorer**, right-click the **Tables** node of your database, click **New**, and then click **Memory Optimized Table**.  
  
     A template for creating memory-optimized tables is displayed.  
  
2.  To replace the template parameters, click **Specify Values for Template Parameters** on the **Query** menu.  
  
     For more information on how to use templates, see [Template Explorer](../../ssms/template/template-explorer.md).  
  
3.  In **Object Explorer**, tables will be ordered first by disk-based tables followed by memory-optimized tables. Use **Object Explorer Details** to see all tables ordered by name.  
  
### To create a natively compiled stored procedure  
  
1.  In **Object Explorer**, right-click the **Stored Procedures** node of your database, click **New**, and then click **Natively Compiled Stored Procedure**.  
  
     A template for creating natively compiled stored procedures is displayed.  
  
2.  To replace the template parameters, click **Specify Values for Template Parameters** on the **Query menu**.  
  
     For more information on how to create a new stored procedure, see [Create a Stored Procedure](../stored-procedures/create-a-stored-procedure.md).  
  
### To create a user-defined memory-optimized table type  
  
1.  In **Object Explorer**, expand the **Types** node of your database, right-click the **User-Defined Table Types** node, click **New**, and then click **User-Defined Memory Optimized Table Type**.  
  
     A template for creating user-defined memory-optimized table type is displayed.  
  
2.  To replace the template parameters, click **Specify Values for Template Parameters** on the **Query** menu.  
  
     For more information on how to create a new stored procedure, see [CREATE TYPE &#40;Transact-SQL&#41;](/sql/t-sql/statements/create-type-transact-sql).  
  
## Memory Monitoring  
  
#### View Memory Usage by Memory-Optimized Objects Report  
  
-   In **Object Explorer**, right-click your database, click **Reports**, click **Standard Reports**, and then click **Memory Usage By Memory Optimized Objects**.  
  
     This report provides detailed data on the utilization of memory space by memory-optimized objects within the database.  
  
#### View Properties for Allocated and Used Memory for a Table, Database  
  
1.  To get information about in-memory usage:  
  
    -   In **Object Explorer**, right-click on your memory-optimized table, click **Properties**, and then the **Storage** page. The value for the **Data Space** property indicates the memory used by the data in the table. The value for the **Index Space** property indicates the memory used by the indexes on table.  
  
    -   In **Object Explorer**, right-click on your database, click **Properties**, and then click the **General** page. The value for the **Memory Allocated To Memory Optimized Objects** property indicates the memory allocated to memory-optimized objects in the database. The value for the **Memory Used By Memory Optimized Objects** property indicates the memory used by memory-optimized objects in the database.  
  
## Supported Features in [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)]  
 [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] supports features and operations that are supported by the database engine on databases with memory-optimized data filegroup, memory-optimized tables, indexes, and natively compiled stored procedures.  
  
 For database, table, stored procedure, user-defined table type, or index objects, the following [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] features have been updated or extended to support In-Memory OLTP.  
  
-   Object Explorer  
  
    -   Context menus  
  
    -   Filter settings  
  
    -   Script As  
  
    -   Tasks  
  
    -   Reports  
  
    -   Properties  
  
    -   Database tasks:  
  
        -   Attach and detach a database that contains memory-optimized tables.  
  
             The **Attach Databases** user interface does not display the memory-optimized data filegroup. However, you can proceed with attaching the database and the database will be attached correctly.  
  
            > [!NOTE]  
            >  If you want to use [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] to attach a database that has a memory-optimized data filegroup container, and if the database's memory-optimized data filegroup container was created on another computer, the location of the memory-optimized data filegroup container must be the same on both computers. If you want the location of the database's memory-optimized data filegroup container to be different on the new computer, you can, use [!INCLUDE[tsql](../../includes/tsql-md.md)] to attach the database. In the following example, the location of the memory-optimized data filegroup container on the new computer is C:\Folder2. But when the memory-optimized data filegroup container was created, on the first computer, the location was C:\Folder1.  
            >   
            >  `CREATE DATABASE[imoltp] ON`  
            >   
            >  `(NAME =N'imoltp',FILENAME=N'C:\Folder2\imoltp.mdf'),`  
            >   
            >  `(NAME =N'imoltp_mod1',FILENAME=N'C:\Folder2\imoltp_mod1'),`  
            >   
            >  `(NAME =N'imoltp_log',FILENAME=N'C:\Folder2\imoltp_log.ldf')`  
            >   
            >  `FOR ATTACH`  
            >   
            >  `GO`  
  
        -   Generate scripts.  
  
             In the **Generate and Publish Scripts Wizard**, the default value for **Check for object existence** scripting option is FALSE. If the value of **Check for object existence** scripting option is set to TRUE in the **Set Scripting Options** screen of the wizard, the script generated would contain "CREATE PROCEDURE <procedure_name> AS" and "ALTER PROCEDURE <procedure_name> <procedure_definition>". When executed, the generated script will return an error as ALTER PROCEDURE is not supported on natively compiled stored procedures.  
  
             To change the generated script for each natively compiled stored procedure:  
  
            1.  In "CREATE PROCEDURE <procedure_name> AS", replace "AS" with "<procedure_definition>".  
  
            2.  Delete "ALTER PROCEDURE <procedure_name> <procedure_definition>".  
  
        -   Copy databases. For databases with memory-optimized objects, the creation of the database on the destination server and transfer of data will not be executed within a transaction.  
  
        -   Import and export data. Use the **[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Import and Export WizardCopy data from one or more tables or views** option. If the destination table is a memory-optimized table that does not exist in the destination database:  
  
            1.  In the **[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Import and Export Wizard**, in the **Specify Table Copy or Query** screen, select **Copy data from one or more tables or views**. Then click **Next**.  
  
            2.  Click **Edit Mappings**. Then select **Create destination table** and click **Edit SQL**. Enter the CREATE TABLE syntax for creating a memory-optimized table on the destination database. Click **OK** and complete the remaining steps in the wizard.  
  
        -   Maintenance plans. The maintenance tasks reorganize index and rebuild index are not supported on memory-optimized tables and their indexes. Therefore, when a maintenance plan for rebuild index and reorganize index are executed, the memory-optimized tables and their indexes in the selected databases are omitted.  
  
             The maintenance task update statistics are not supported with a sample scan on memory-optimized tables and their indexes. Therefore, when a maintenance plan for update statistics is executed, the statistics for memory-optimized tables and their indexes are always updated to `WITH FULLSCAN, NORECOMPUTE`.  
  
-   Object Explorer details pane  
  
-   Template Explorer  
  
## Unsupported Features in [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)]  
 For In-Memory OLTP objects, [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] does not support features and operations that are also not supported by the database engine.  
  
 For more information on unsupported [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] features, see [Supported SQL Server Features](unsupported-sql-server-features-for-in-memory-oltp.md).  
  
## See Also  
 [SQL Server Support for In-Memory OLTP](sql-server-support-for-in-memory-oltp.md)  
  
  
