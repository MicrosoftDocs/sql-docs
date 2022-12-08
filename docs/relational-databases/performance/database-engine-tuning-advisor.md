---
title: "Database Engine Tuning Advisor"
description: Learn how to use Database Engine Tuning Advisor to troubleshoot, tune a large set of queries, analyze design changes, and manage storage space in SQL Server.
ms.custom: ""
ms.date: "05/11/2021"
ms.service: sql
ms.reviewer: ""
ms.subservice: performance
ms.topic: conceptual
f1_keywords: 
  - "sql13.dta.general.f1"
author: WilliamDAssafMSFT
ms.author: wiassaf
---
# Database Engine Tuning Advisor
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  The [!INCLUDE[msCoName](../../includes/msconame-md.md)] Database Engine Tuning Advisor (DTA) analyzes databases and makes recommendations that you can use to optimize query performance. You can use the Database Engine Tuning Advisor to select and create an optimal set of indexes, indexed views, or table partitions without having an expert understanding of the database structure or the internals of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. Using the DTA, you can perform the following tasks:
  
-   Troubleshoot the performance of a specific problem query  
  
-   Tune a large set of queries across one or more databases  
  
-   Perform an exploratory what-if analysis of potential physical design changes  
  
-   Manage storage space  
  
  
> [!NOTE]
> The Database Engine Tuning Advisor is not supported for [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)] or [!INCLUDE[ssazuremi_md](../../includes/ssazuremi_md.md)]. Instead, consider the strategies recommended in [Monitoring and performance tuning in Azure SQL Database and Azure SQL Managed Instance](/azure/azure-sql/database/monitor-tune-overview). For [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)], see also the [Database Advisor performance recommendations for Azure SQL Database](/azure/azure-sql/database/database-advisor-implement-performance-recommendations).

## Database Engine Tuning Advisor benefits  
 Optimizing query performance can be difficult without a full understanding the database structure and the queries that are run against the database. The **Database Engine Tuning Advisor (DTA)** can make this task easier by analyzing the current query plan cache or by analyzing a workload of [!INCLUDE[tsql](../../includes/tsql-md.md)] queries that you create and recommending an appropriate physical design. For more advanced database administrators, DTA exposes a powerful mechanism to perform exploratory what-if analysis of different physical design alternatives. The DTA can provide the following information.  
  
-   Recommend the best mix of rowstore and [columnstore](../../relational-databases/performance/columnstore-index-recommendations-in-database-engine-tuning-advisor-dta.md) indexes for databases by using the query optimizer to analyze queries in a workload.  
  
-   Recommend aligned or non-aligned partitions for databases referenced in a workload.  
  
-   Recommend indexed views for databases referenced in a workload.  
  
-   Analyze the effects of the proposed changes, including index usage, query distribution among tables, and query performance in the workload.  
  
-   Recommend ways to tune the database for a small set of problem queries.  
  
-   Allow you to customize the recommendation by specifying advanced options such as disk space constraints.  
  
-   Provide reports that summarize the effects of implementing the recommendations for a given workload.  

-   Consider alternatives in which you supply possible design choices in the form of hypothetical configurations for Database Engine Tuning Advisor to evaluate.

-   Tune workloads from a variety of sources including SQL Server Query Store, Plan Cache, SQL Server Profiler Trace file or table, or a .SQL file.

  
The Database Engine Tuning Advisor is designed to handle the following types of query workloads:  
  
-   Online transaction processing (OLTP) queries only  
  
-   Online analytical processing (OLAP) queries only  
  
-   Mixed OLTP and OLAP queries  
  
-   Query-heavy workloads (more queries than data modifications)  
  
-   Update-heavy workloads (more data modifications than queries)  
  
## DTA Components and Concepts  
 **Database Engine Tuning Advisor Graphical User Interface**  
 An easy-to-use interface in which you can specify the workload and select various tuning options.  
  
 **dta** Utility  
 The command prompt version of Database Engine Tuning Advisor. The **dta** utility is designed to allow you to use Database Engine Tuning Advisor functionality in applications and scripts.  
  
 **workload**  
 A Transact-SQL script file, trace file, or trace table that contains a representative workload for the databases you want to tune. Beginning with [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)], you can specify the plan cache as the workload.  Beginning with [!INCLUDE[sssql16-md](../../includes/sssql16-md.md)], you can [specify the Query Store as the workload](../../relational-databases/performance/tuning-database-using-workload-from-query-store.md). 
  
 **XML input file**  
 A XML-formatted file that Database Engine Tuning Advisor can use to tune workloads. The XML input file supports advanced tuning options that are not available in either the GUI or **dta** utility.  
  
## Limitations and Restrictions  
 The Database Engine Tuning Advisor has the following limitations and restrictions.  
  
-   It cannot add or drop unique indexes or indexes that enforce `PRIMARY KEY` or `UNIQUE` constraints.  
  
-   It cannot analyze a database that is set to single-user mode.  
  
-   If you specify a maximum disk space for tuning recommendations that exceeds the actual available space, Database Engine Tuning Advisor uses the value you specify. However, when you execute the recommendation script to implement it, the script may fail if more disk space is not added first. Maximum disk space can be specified with the **-B** option of the **dta** utility, or by entering a value in the **Advanced Tuning Options** dialog box.  
  
-   For security reasons, Database Engine Tuning Advisor cannot tune a workload in a trace table that resides on a remote server. To work around this limitation, you can use a trace file instead of a trace table or copy the trace table to the remote server.  
  
-   When you impose constraints, such as those imposed when you specify a maximum disk space for tuning recommendations (by using the **-B** option or the **Advanced Tuning Options** dialog box), Database Engine Tuning Advisor may be forced to drop certain existing indexes. In this case, the resulting Database Engine Tuning Advisor recommendation may produce a negative expected improvement.  
  
-   When you specify a constraint to limit tuning time (by using the **-A** option with the **dta** utility or by checking **Limit tuning time** on the **Tuning Options** tab), Database Engine Tuning Advisor may exceed that time limit to produce an accurate expected improvement and the analysis reports for whatever portion of the workload has been consumed so far.  
  
-   Database Engine Tuning Advisor might not make recommendations under the following circumstances:  
  
    1.  The table being tuned contains less than 10 data pages.  
  
    2.  The recommended indexes would not offer enough improvement in query performance over the current physical database design.  
  
    3.  The user who runs Database Engine Tuning Advisor is not a member of the **db_owner** database role or the **sysadmin** fixed server role. The queries in the workload are analyzed in the security context of the user who runs the Database Engine Tuning Advisor. The user must be a member of the **db_owner** database role.  
  
-   Database Engine Tuning Advisor stores tuning session data and other information in the `msdb` database. If changes are made to the `msdb` database, you may risk losing tuning session data. To eliminate this risk, implement an appropriate backup strategy for the `msdb` database.  
  
## Performance Considerations  
 Database Engine Tuning Advisor can consume significant processor and memory resources during analysis. To avoid slowing down your production server, follow one of these strategies:  
  
-   Tune your databases when your server is free. Database Engine Tuning Advisor can affect maintenance task performance.  
  
-   Use the test server/production server feature. For more information, see  [Reduce the Production Server Tuning Load](../../relational-databases/performance/reduce-the-production-server-tuning-load.md).  
  
-   Specify only the physical database design structures you want Database Engine Tuning Advisor to analyze. Database Engine Tuning Advisor provides many options, but specifies only those that are necessary.  
  
## Dependency on xp_msver extended stored procedure  
 Database Engine Tuning Advisor depends on the **xp_msver** extended stored procedure to provide full functionality. This extended stored procedure is turned on by default. Database Engine Tuning Advisor uses this extended stored procedure to fetch the number of processors and available memory on the computer where the database that you are tuning resides. If **xp_msver** is unavailable, Database Engine Tuning Advisor assumes the hardware characteristics of the computer where Database Engine Tuning Advisor is running. If the hardware characteristics of the computer where Database Engine Tuning Advisor is running are not available, one processor and 1024 megabytes (MBs) of memory are assumed.  
  
 This dependency affects partitioning recommendations because the number of partitions recommended depends on these two values (number of processors and available memory). The dependency also affects your tuning results when you use a test server to tune your production server. In this scenario, Database Engine Tuning Advisor uses **xp_msver** to fetch hardware properties from the production server. After tuning the workload on the test server, Database Engine Tuning Advisor uses these hardware properties to generate a recommendation. For more information, see [xp_msver &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/xp-msver-transact-sql.md).  
  
## Database Engine Tuning Advisor tasks  
 The following table lists common Database Engine Tuning Advisor tasks and the articles that describe how to perform them.  
  
|Database Engine Tuning Advisor task|article|  
|-----------------------------------------|-----------|  
|Initialize and start the Database Engine Tuning Advisor.<br /><br /> Create a workload by specifying the plan cache, by creating a script, or by generating a trace file or trace table.<br /><br /> Tune a database by using the Database Engine Tuning Advisor graphical user interface tool.<br /><br /> Create XML input files to tune workloads.<br /><br /> View descriptions of the Database Engine Tuning Advisor user interface options.|[Start and Use the Database Engine Tuning Advisor](../../relational-databases/performance/start-and-use-the-database-engine-tuning-advisor.md)|  
|View the results of the database tuning operation.<br /><br /> Select and implement tuning recommendations.<br /><br /> Perform what-if exploratory analysis against the workload.<br /><br /> Review existing tuning sessions, clone sessions based on existing ones <br />or edit existing tuning recommendations for further evaluation or implementation.<br /><br /> View descriptions of the Database Engine Tuning Advisor user interface options.|[View and Work with the Output from the Database Engine Tuning Advisor](../../relational-databases/performance/view-and-work-with-the-output-from-the-database-engine-tuning-advisor.md)|  
  
  
