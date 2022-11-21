---
title: "Should table or stored procedure be ported to in-memory OLTP"
description: Use the Transaction Performance Analysis report in SQL Server Management Studio to evaluate whether In-Memory OLTP can improve database application performance.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.date: "08/02/2017"
ms.service: sql
ms.subservice: in-memory-oltp
ms.topic: conceptual
ms.custom: seo-dt-2019
helpviewer_keywords:
  - "Analyze, Migrate, Report"
  - "AMR"
ms.assetid: c1ef96f1-290d-4952-8369-2f49f27afee2
monikerRange: "=azuresqldb-current||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Determining if a Table or Stored Procedure Should Be Ported to In-Memory OLTP
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

  The Transaction Performance Analysis report in [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] helps you evaluate if In-Memory OLTP will improve your database application's performance. The report also indicates how much work you must do to enable In-Memory OLTP in your application. After you identify a disk-based table to port to In-Memory OLTP, you can use the [Memory Optimization Advisor](../../relational-databases/in-memory-oltp/memory-optimization-advisor.md), to help you migrate the table. Similarly, the [Native Compilation Advisor](../../relational-databases/in-memory-oltp/native-compilation-advisor.md) will help you port a stored procedure to a natively compiled stored procedure. For information about migration methodologies, see [In-Memory OLTP - Common Workload Patterns and Migration Considerations](/previous-versions/dn673538(v=msdn.10)).  
  
 The Transaction Performance Analysis report is run directly against the production database, or a test database with an active workload that is similar to the production workload.  
  
 The report and migration advisors help you accomplish the following tasks:  
  
-   Analyze your workload to determine hot spots where In-Memory OLTP can potentially help to improve performance. The Transaction Performance Analysis report recommends tables and stored procedures that would benefit most from conversion to In-Memory OLTP.  
  
-   Help you plan and execute your migration to In-Memory OLTP. The migration path from a disk based table to a memory-optimized table can be time consuming. The Memory-Optimization Advisor helps you identify the incompatibilities in your table that you must remove before moving the table to In-Memory OLTP. The Memory-Optimization Advisor also helps you understand the impact that the migration of a table to a memory-optimized table will have on your application.  
  
     You can see if your application would benefit from In-Memory OLTP, when you want to plan your migration to In-Memory OLTP, and whenever you work to migrate some of your tables and stored procedures to In-Memory OLTP.  
  
    > [!IMPORTANT]  
    >  The performance of a database system is dependent on a variety of factors, not all of which the transaction performance collector can observe and measure. Therefore, the transaction performance analysis report does not guarantee actual performance gains will match its predictions, if any predictions are made.  
  
 The Transaction Performance Analysis report and the migration advisors are installed as part of SQL Server Management Studio (SSMS) when you select **Management Tools-Basic** or **Management Tools-Advanced** when you install [!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)], or when you [Download SQL Server Management Studio](../../ssms/download-sql-server-management-studio-ssms.md).    
  
## Transaction Performance Analysis Reports  
 You can generate transaction performance analysis reports in **Object Explorer** by right-clicking on the database, selecting **Reports**, then **Standard Reports**, and then **Transaction Performance Analysis Overview**. The database needs to have an active workload, or a recent run of a workload, in order to generate a meaningful analysis report.  
  
### Tables
  
 The details report for a table consists of three sections:  
  
-   Scan Statistics Section  
  
     This section includes a single table that shows the statistics that were collected about scans on the database table. The columns are:  
  
    -   Percent of total accesses. The percentage of scans and seeks on this table with respect to the activity of the entire database. The higher this percentage, the more heavily used the table is compared to other tables in the database.  
  
    -   Lookup Statistics/Range Scan Statistics. This column records the number of point lookups and range scans (index scans and table scans) conducted on the table during profiling. Average per transaction is an estimate.  
    
-   Contention Statistics Section  
  
     This section includes a table that shows contention on the database table. For more information regarding database latches and locks, please see Locking Architecture. The columns are as follows:  
  
    -   Percent of total waits. The percentage of latch and lock waits on this database table compared to activity of the database. The higher this percentage, the more heavily used the table is compared to other tables in the database.  
  
    -   Latch Statistics. These columns record the number of latch waits for queries involving for this table. For information on latches, see Latching. The higher this number, the more latch contention on the table.  
  
    -   Lock Statistics. This group of columns record the number of page lock acquisitions and waits for queries for this table. For more information on locks, see Understanding Locking in SQL Server. The more waits, the more lock contention on the table.  
  
-   Migration Difficulties Section  
  
     This section includes a table that shows the difficulty of converting this database table to a memory-optimized table. A higher difficulty rating indicates more difficultly to convert the table. To see details to convert this database table, please use the Memory Optimization Advisor.  
  
Scan and contention statistics on the table details report is gathered and aggregated from sys.dm_db_index_operational_stats (Transact-SQL).  

### Stored Procedures

 A stored procedure with high ratio of CPU time to elapsed time is a candidate for migration. The report shows all table references, because natively compiled stored procedures can only reference memory-optimized tables, which can add to the migration cost.  
  
 The details report for a stored procedure consists of two sections:  
  
-   Execution Statistics Section  
  
     This section includes a table that shows the statistics that were collected about the stored procedure's executions. The columns are as follows:  
  
    -   Cached Time. The time this execution plan is cached. If the stored procedure drops out of the plan cache and re-enters, there will be times for each cache.  
  
    -   Total CPU Time. The total CPU time that the stored procedure consumed during profiling. The higher this number, the more CPU the stored procedure used.  
  
    -   Total Execution Time. The total amount of execution time the stored procedure used during profiling. The higher the difference between this number and the CPU time is, the less efficiently the stored procedure is using the CPU.  
  
    -   Total Cache Missed. The number of cache misses (reads from physical storage) that is caused by the stored procedure's executions during profiling.  
  
    -   Execution Count. The number of times this stored procedure executed during profiling.  
  
-   Table References Section  
  
     This section includes a table that shows the tables to which this stored procedure refers. Before converting the stored procedure into a natively compiled stored procedure, all of these tables must be converted to memory-optimized tables, and they must stay on the same server and database.  
  
 Execution Statistics on the stored procedure details report is gathered and aggregated from sys.dm_exec_procedure_stats (Transact-SQL). The references are obtained from sys.sql_expression_dependencies (Transact-SQL).  
  
 To see details about how to convert a stored procedure to a natively compiled stored procedure, please use the Native Compilation Advisor.  
  
## Generating In-Memory OLTP Migration Checklists  
 Migration checklists identify any table or stored procedure features that are not supported with memory-optimized tables or natively compiled stored procedures. The memory-optimization and native compilation advisors can generate a checklist for a single disk-based table or interpreted T-SQL stored procedure. It is also possible to generation migration checklists for multiple tables and stored procedures in a database.  
  
 You can generate a migration checklist in [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] by using the **Generate In-Memory OLTP Migration Checklists** command or by using PowerShell.  
  
**To generate a migration checklist using the UI command**  
  
1.  In **Object Explorer**, right click a database other than the system database, click **Tasks**, and then click **Generate In-Memory OLTP Migration Checklists**.  
  
2.  In the Generate In-Memory OLTP Migration Checklists dialog box, click Next to navigate to the **Configure Checklist Generation Options** page. On this page do the following.  
  
    1.  Enter a folder path in the **Save checklist to** box.  
  
    2.  Verify that **Generate checklists for specific tables and stored procedures** is selected.  
  
    3.  Expand the **Table** and **Stored Procedure** nodes in the section box.  
  
    4.  Select a few objects in the selection box.  
  
3.  Click **Next** and confirm that the list of tasks matches your settings on the **Configure Checklist Generation Options** page.  
  
4.  Click **Finish**, and then confirm that migration checklist reports were generated only for the objects you selected.  

 You can verify the accuracy of the reports by comparing them to reports generated by the Memory Optimization Advisor tool and the Native Compilation Advisor tool. For more information, see [Memory Optimization Advisor](../../relational-databases/in-memory-oltp/memory-optimization-advisor.md) and [Native Compilation Advisor](../../relational-databases/in-memory-oltp/native-compilation-advisor.md).  
  
**To generate a migration checklist using SQL Server PowerShell**  
  
1.  In **Object Explorer**, click on a database and then click **Start PowerShell**. Verify that the following prompt appears.  
  
    ```  
    PS SQLSERVER: \SQL\{Instance Name}\DEFAULT\Databases\{two-part DB Name}>  
    ```  
  
2.  Enter the following command.  
  
    ```  
    Save-SqlMigrationReport -FolderPath "<folder_path>"  
    ```  
  
3.  Verify the following.  
  
    -   The folder path is created, if it doesn't already exist.  
  
    -   The migration checklist report is generated for all tables and stored procedures in the database, and the report is in the location specified by folder_path.  
  
**To generate a migration checklist using Windows PowerShell**  
  
1.  Start an elevated Windows PowerShell session.  
  
2.  Enter the following commands. The object can either be a table or a stored procedure.  
  
    ```  
    [System.Reflection.Assembly]::LoadWithPartialName('Microsoft.SqlServer.SMO')  
  
    ```  
  
    ```  
    Save-SqlMigrationReport -Server "<instance_name>" -Database "<db_name>" -FolderPath "<folder_path1>"  
  
    ```  
  
    ```  
    Save-SqlMigrationReport -Server "<instance_name>" -Database "<db_name>" -Object <object_name> -FolderPath "<folder_path2>"  
  
    ```  
  
3.  Verify the following.  
  
    -   A migration checklist report is generated for all tables and stored procedures in the database, and the report is in the location specified by folder_path.  
  
    -   A migration checklist report for <object_name> is the only report in the location specified by folder_path2.  
  
## See Also  
 [Migrating to In-Memory OLTP](./plan-your-adoption-of-in-memory-oltp-features-in-sql-server.md)  
  
