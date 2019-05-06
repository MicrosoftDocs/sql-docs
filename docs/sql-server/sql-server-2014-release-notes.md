---
title: "SQL Server 2014 Release Notes | Microsoft Docs"
ms.custom: ""
ms.date: "03/15/2018"
ms.prod: sql
ms.technology: install
ms.reviewer: ""
ms.topic: conceptual
ms.assetid: bf4c4922-80b3-4be3-bf71-228247f97004
author: craigg-msft
ms.author: craigg
manager: jhubbard
monikerRange: "= sql-server-2014 || = sqlallproducts-allversions"
---
# SQL Server 2014 Release Notes
[!INCLUDE[tsql-appliesto-ss2014-xxxx-xxxx-xxx-md](../includes/tsql-appliesto-ss2014-xxxx-xxxx-xxx-md.md)]
This article describes known issues with [!INCLUDE[ssSQL14](../includes/sssql14-md.md)] releases, including related service packs.

## SQL Server 2014 Service Pack 2 (SP2)

SQL Server 2014 SP2 contains rollups of released hotfixes for SQL Server 2014 SP1 CU7. It contains improvements centered around performance, scalability, and diagnostics based on the feedback from customers and the SQL community.

### Performance and scalability improvements in SP2

|Feature|Description|For more information|
|---|---|---|
|Automatic Soft NUMA partitioning|You can automatically configure Soft NUMA on systems reporting 8 or more CPUs per NUMA node.|[Soft-NUMA (SQL Server)](https://docs.microsoft.com/sql/database-engine/configure-windows/soft-numa-sql-server)|
|Buffer Pool Extension|Enables SQL Server Buffer Pool to scale beyond 8 TB.|[Buffer Pool Extension](https://docs.microsoft.com/sql/database-engine/configure-windows/buffer-pool-extension)|
|Dynamic Memory Object Scaling| Dynamically partition memory object based on number of nodes and cores. This enhancement eliminates the need of Trace Flag 8048 post SQL 2014 SP2.|[Dynamic Memory Object Scaling](https://blogs.msdn.microsoft.com/sql_server_team/dynamic-memory-object-scaling/)|
|MAXDOP hint for DBCC CHECK* commands|This improvement is useful to run DBCC CHECKDB with a MAXDOP setting other than the sp_configure value.|[Hints (Transact-SQL) - Query](https://docs.microsoft.com/sql/t-sql/queries/hints-transact-sql-query)|
|SOS_RWLock spinlock improvement|Removes the need for spinlock for SOS_RWLock and instead uses lock-free techniques similar to in-memory OLTP. |[SOS_RWLock Redesign](https://blogs.msdn.microsoft.com/psssql/2016/04/07/sql-2016-it-just-runs-faster-sos_rwlock-redesign/)|
|Spatial Native Implementation|Significant improvement in spatial query performance.|[Spatial performance improvements in SQL Server 2012 and 2014](https://support.microsoft.com/help/3107399/spatial-performance-improvements-in-sql-server-2012-and-2014)

### Supportability and diagnostics improvements in SP2

|Feature|Description|For more information|
|---|---|---|
|AlwaysON timeout logging|Added new logging capability for Lease Timeout messages so that the current time and the expected renewal times are logged. |[Improved AlwaysOn Availability Group Lease Timeout Diagnostics](https://blogs.msdn.microsoft.com/alwaysonpro/2016/02/23/improved-alwayson-availability-group-lease-timeout-diagnostics/)
|AlwaysON XEvents and performance counters|New AlwaysON XEvents and performance counters to improve diagnostics when troubleshooting latency issues with AlwaysON. |[KB 3107172](https://support.microsoft.com/help/3107172/improve-tempdb-spill-diagnostics-by-using-extended-events-in-sql-serve) and [KB 3107400](https://support.microsoft.com/help/3107400/improved-tempdb-spill-diagnostics-in-showplan-xml-schema-in-sql-server)
|Change tracking cleanup|A new stored procedure sp_flush_CT_internal_table_on_demand cleans up change tracking internal tables on demand.|[KB 3173157](https://support.microsoft.com/help/3173157/adds-a-stored-procedure-for-the-manual-cleanup-of-the-change-tracking)
|Database cloning|Use the new DBCC command to troubleshoot existing production databases by cloning the schema, metadata, and statistics but without the data. Cloned databases are not meant to be used in production environments.|[KB 3177838](https://support.microsoft.com/help/3177838/how-to-use-dbcc-clonedatabase-to-generate-a-schema-and-statistics-only)
|DMF additions|New DMF sys.dm_db_incremental_stats_properties expose information per-partition for incremental statistics.|[KB 3170114](https://support.microsoft.com/help/3170114/update-to-add-dmf-sys-dm-db-incremental-stats-properties-in-sql-server)
|DMF for retrieving input buffer in SQL Server|A new DMF for retrieving the input buffer for a session/request (sys.dm_exec_input_buffer) is now available. This is functionally equivalent to DBCC INPUTBUFFER.|[sys.dm_exec_input_buffer](https://docs.microsoft.com/sql/relational-databases/system-dynamic-management-views/sys-dm-exec-input-buffer-transact-sql)
|DROP DDL Support for Replication|Allows a table that's included as an article in transactional replication publication to be dropped from the database and the publication.|[KB 3170123](https://support.microsoft.com/help/3170123/supports-drop-table-ddl-for-articles-that-are-included-in-transactiona)
|IFI privilege to SQL service account|Determine whether Instant File initialization (IFI) is in effect at the SQL Server service startup.|[Database File Initialization](https://docs.microsoft.com/sql/relational-databases/databases/database-instant-file-initialization)
|Memory Grants - Handling issues|You can leverage diagnostic hints while running queries by capping their memory grants to prevent memory contention.|[KB 3107401](https://support.microsoft.com/help/3107401/new-query-memory-grant-options-are-available-min-grant-percent-and-max)
|Query execution lightweight per-operator profiling |Optimizes collecting per-operator query execution statistics such as actual number of rows.|[Developers Choice: Query progress - anytime, anywhere](https://blogs.msdn.microsoft.com/sql_server_team/query-progress-anytime-anywhere/)
|Query execution diagnostics|Actual rows read are now  reported in the query execution plans to help improve query performance troubleshooting.|[KB 3107397](https://support.microsoft.com/help/3107397/improved-diagnostics-for-query-execution-plans-that-involve-residual-p)
|Query execution diagnostics for tempdb spill|Hash Warning and Sort Warnings now have additional columns to track physical I/O statistics, memory used, and rows affected. |[Improve temptdb spill diagnostics](https://support.microsoft.com/help/3107172/improve-tempdb-spill-diagnostics-by-using-extended-events-in-sql-serve)
|Tempdb supportability |Use a new Errorlog message for the number of tempdb files, and tempdb data file changes, at server startup.|[KB 2963384](https://support.microsoft.com/help/2963384/fix-sql-server-crashes-when-the-log-file-of-tempdb-database-is-full-in)


In addition, note the following fixes:
- The Xevent call stack now include modules names and offset instead of absolute addresses.
- Better correlation between diagnostics XE and DMVs - Query_hash and query_plan_hash are used for identifying a query uniquely. DMV defines them as varbinary(8), while XEvent defines them as UINT64. Since SQL server does not have "unsigned bigint", casting does not always work. This improvement introduces new XEvent action/filter columns equivalent to query_hash and query_plan_hash except when they are defined as INT64. This fix helps correlating queries between XE and DMVs.
- Support for UTF-8 in BULK INSERT and BCP - Support for export and import of data encoded in UTF-8 character set is now enabled in BULK INSERT and BCP.

### Download pages and more information for SP2

- [Download Service Pack 2 for Microsoft SQL Server 2014](https://www.microsoft.com/download/details.aspx?id=53168)
- [SQL Server 2014 Service Pack 2 is now available](https://www.microsoft.com/download/details.aspx?id=53167)
- [SQL Server 2012 SP2 Express](https://www.microsoft.com/download/details.aspx?id=53167)
- [SQL Server 2014 SP2 Feature Pack](https://www.microsoft.com/download/details.aspx?id=53164)
- [SQL Server 2014 SP2 Report Builder](https://www.microsoft.com/download/details.aspx?id=53163)
- [SQL Server 2014 SP2 Reporting Services Add-in for Microsoft Sharepoint](https://www.microsoft.com/download/details.aspx?id=53162)
- [SQL Server 2014 SP2 Semantic Language Statistics](https://www.microsoft.com/download/details.aspx?id=53165)
- [SQL Server 2014 Service Pack 2 Release Information](https://support.microsoft.com/help/3171021/sql-server-2014-service-pack-2-release-information)

## SQL Server 2014 Service Pack 1 (SP1)

SQL Server 2014 SP1 contains fixes provided in SQL Server 2014 CU 1 up to and including CU 5, as well as a rollup of fixes previously shipped in SQL Server 2012 SP2.

> [!NOTE]
> If your SQL Server instance has SSISDB catalog enabled, and if you get an installation error when you upgrade to SP1, follow the instructions described for this issue on [Error 912 or 3417 when you install SQL Server 2014 SP1](https://support.microsoft.com/help/3018269/error-912-or-3417-when-you-install-sql-server-2014-sp1-build-12-0-4050/).

### Download pages and more information for SP1

- [Download Service Pack 1 for Microsoft SQL Server 2014](https://www.microsoft.com/download/details.aspx?id=46694)
- [SQL Server 2014 Service Pack 1 has released - Updated](https://blogs.msdn.microsoft.com/sqlreleaseservices/sql-server-2014-service-pack-1-has-released-updated/)
- [Microsoft SQL Server 2014 SP1 Express](https://www.microsoft.com/download/details.aspx?id=42299)
- [Microsoft SQL Server 2014 SP1 Feature Pack](https://www.microsoft.com/download/details.aspx?id=46696)


## Before You Install SQL Server 2014 RTM

### Limitations and Restrictions in SQL Server 2014 RTM

1.  Upgrading from SQL Server 2014 CTP 1 to SQL Server 2014 RTM is NOT supported.  
2.  Installing SQL Server 2014 CTP 1 side-by-side with SQL Server 2014 RTM is NOT supported.  
3.  Attaching or restoring a SQL Server 2014 CTP 1 database to SQL Server 2014 RTM is NOT supported.  

**Workaround:** None.

#### Upgrading from SQL Server 2014 CTP 2 to SQL Server RTM
Upgrade is fully supported. Specifically, you can:

1.  Attach a SQL Server 2014 CTP 2 database to an instance of SQL Server 2014 RTM.    
2.  Restore a database backup taken on SQL Server 2014 CTP 2 to an instance of SQL Server 2014 RTM.    
3.  In-place upgrade to SQL Server 2014 RTM.
4.  Rolling upgrade to SQL Server 2014 RTM. You are required to switch to manual failover mode before initiating the rolling upgrade. Refer to [Upgrade and Update of Availability Group Servers with Minimal Downtime and Data Loss](https://msdn.microsoft.com/library/dn178483.aspx) for details.    
5.  Data collected by Transaction Performance Collection Sets installed in SQL Server 2014 CTP 2 cannot be viewed through SQL Server Management Studio in SQL Server 2014 RTM, and vice versa.
  
#### Downgrading from SQL Server 2014 RTM to SQL Server 2014 CTP 2  
This action is not supported.  
  
**Workaround:** There is no workaround for downgrade. We recommend that you back up the database before upgrading to SQL Server 2014 RTM.  
  
#### Incorrect version of StreamInsight Client on SQL Server 2014 media/ISO/CAB  
The wrong version of StreamInsight.msi and StreamInsightClient.msi is located in the following path on the SQL Server media/ISO/CAB (StreamInsight\\\<Architecture\>\\\<Language ID\>).  
  
**Workaround:** Download and install the correct version from the [SQL Server 2014 Feature Pack download page](https://go.microsoft.com/fwlink/?LinkID=306709).  
  
### <a name="ProdDoc"></a>Product Documentation RTM
  
Report Builder and PowerPivit content are not available in some languages. 

**Issue:** Report Builder content is not available in the following languages:  
  
-   Greek (el-GR)  
-   Norwegian (Bokmal) (nb-NO)  
-   Finnish (fi-FI)  
-   Danish (da-DK)  
  
In [!INCLUDE[ssSQL11](../includes/sssql11-md.md)], this content was available in a CHM file that shipped with the product and was available in these languages. The CHM files no longer ship with the product and the Report Builder content is only available on MSDN. MSDN does not support these languages. Report Builder was also removed from TechNet and is no longer available in those supported languages.  
  
**Workaround:** None.  
  
**Issue:** Power Pivot content is not available in the following languages:
  
-   Greek (el-GR)  
-   Norwegian (Bokmal) (nb-NO)  
-   Finnish (fi-FI)  
-   Danish (da-DK)  
-   Czech (cs-CZ)  
-   Hungarian (hu-HU)  
-   Dutch (Netherlands) (nl-NL)  
-   Polish (pl-PL)  
-   Swedish (sv-SE)  
-   Turkish (tr-TR)  
-   Portuguese (Portugal) (pt-PT)  
  
In [!INCLUDE[ssSQL11](../includes/sssql11-md.md)], this content was available on TechNet and was available in these languages. This content was removed from TechNet and is no longer available in these supported languages.  
  
**Workaround:** None.  
  
### <a name="DBEngine"></a>Database Engine (RTM)
  
#### Changes made for Standard Edition in SQL Server 2014 RTM  
SQL Server 2014 Standard has the following changes:  
  
-   The Buffer Pool Extension feature allows using the maximum size of up to 4x times of configured memory.    
-   The maximum memory has been raised from 64 GB to 128 GB.  
 
#### Memory Optimization Advisor flags default constraints as incompatible  
**Issue:** The Memory Optimized Advisor in SQL Server Management Studio flags all default constraints as incompatible. Not all default constraints are supported in a memory-optimized table; the Advisor does not distinguish between supported and unsupported types of default constraints. Supported default constraints include all constants, expressions, and built-in functions supported within natively compiled stored procedures. To see the list of functions supported in natively compiled stored procedures, refer to [Supported Constructs in Natively Compiled Stored Procedures](https://msdn.microsoft.com/library/dn452279(v=sql.120).aspx).  
  
**Workaround:** If you want to use the advisor to identify blockers, ignore the compatible default constraints. To use the Memory Optimization Advisor to migrate tables that have compatible default constraints, but no other blockers, follow these steps:  
  
1.  Remove the default constraints from the table definition.    
2.  Use the Advisor to produce a migration script on the table.    
3.  Add back the default constraints in the migration script.    
4.  Execute the migration script.  
  
#### Informational message "file access denied" incorrectly reported as an error in the SQL Server 2014 error log  
**Issue:** When restarting a server that has databases that contain memory-optimized tables, you may see the following type of error messages in the SQL Server 2014 error log:  
  
```  
[ERROR]Unable to delete file C:\Program Files\Microsoft SQL   
Server\....old.dll. This error may be due to a previous failure to unload   
memory-optimized table DLLs.  
```  
This message is actually informational and no user action is required.  
  
**Workaround:** None. This is an informational message.  
  
#### Missing index details incorrectly report included columns for memory-optimized table  
**Issue:** If SQL Server 2014 detects a missing index for a query on a memory-optimized table, it will report a missing index in the SHOWPLAN_XML, as well as in the missing index DMVs such as sys.dm_db_missing_index_details. In some cases, the missing index details will contain included columns. As all columns are implicitly included with all indexes on memory-optimized tables, it is not allowed to explicitly specify included columns with memory-optimized indexes.  
  
**Workaround:** Do not specify the INCLUDE clause with indexes on memory-optimized tables.  
  
#### Missing index details omit missing indexes when a hash index exists but is not suitable for the query  
**Issue:** If you have a HASH index on columns of a memory-optimized table referenced in a query, but the index cannot be used for the query, SQL Server 2014 will not always report a missing index in SHOWPLAN_XML and in the DMV sys.dm_db_missing_index_details.  
  
In particular, if a query contains equality predicates that involve a subset of the index key columns or if it contains inequality predicates that involve the index key columns, the HASH index cannot be used as is, and a different index would be required to execute the query efficiently.  
  
**Workaround:** In case you are using hash indexes, inspect the queries and query plans to determine if the queries could benefit from Index Seek operations on a subset of the index key, or Index Seek operations on inequality predicates. If you need to seek on a subset of the index key, either use a NONCLUSTERED index, or use a HASH index on exactly the columns you need to seek on. If you need to seek on an inequality predicate, use a NONCLUSTERED index instead of HASH.  
  
#### Failure when using a memory-optimized table and memory-optimized table variable in the same query, if the database option READ_COMMITTED_SNAPSHOT is set to ON  
**Issue:** If the database option READ_COMMITTED_SNAPSHOT is set to ON, and you access both a memory-optimized table and a memory-optimized table variable in the same statement outside the context of a user transaction, you may encounter this error message:  
  
```  
Msg 41359  
A query that accesses memory optimized tables using the READ COMMITTED  
isolation level, cannot access disk based tables when the database option  
READ_COMMITTED_SNAPSHOT is set to ON. Provide a supported isolation level  
for the memory optimized table using a table hint, such as WITH (SNAPSHOT).  
```  
  
**Workaround:** Either use the table hint WITH (SNAPSHOT) with the table variable, or set the database option MEMORY_OPTIMIZED_ELEVATE_TO_SNAPSHOT to ON, using the following statement:  
  
```  
ALTER DATABASE CURRENT   
SET MEMORY_OPTIMIZED_ELEVATE_TO_SNAPSHOT=ON  
```  
  
#### Procedure and query execution statistics for natively compiled stored procedures record worker time in multiples of 1000  
**Issue:** After enabling the collection of procedure or query execution statistics collection for natively compiled stored procedures using sp_xtp_control_proc_exec_stats or sp_xtp_control_query_exec_stats, you will see the *_worker_time reported in multiples of 1000, in the DMVs sys.dm_exec_procedure_stats and sys.dm_exec_query_stats. Query executions that have a worker time of less than 500 microseconds will be reported as having a worker_time of 0.  
  
**Workaround:** None. Do not rely on worker_time reported in the execution stats DMVs for short-running queries in natively compiled stored procedures.  
  
#### Error with SHOWPLAN_XML for natively compiled stored procedures that contain long expressions  
**Issue:** If a natively compiled stored procedure contains a long expression, obtaining the SHOWPLAN_XML for the procedure, either using the T-SQL option SET SHOWPLAN_XML ON or using the option 'Display Estimated Execution Plan' in Management Studio, may result in the following error:  
  
```  
Msg 41322. MAT/PIT export/import encountered a failure for memory  
optimized table or natively compiled stored procedure with object ID  
278292051 in database ID 6. The error code was  
0xc00cee81.  
```  
  
**Workaround:** Two suggested workarounds:  
  
1.  Add parentheses to the expression, similar to the following example:  
  
    Instead of:  
  
    ```  
    SELECT @v0 + @v1 + @v2 + ... + @v199  
    ```  
  
    Write:  
  
    ```  
    SELECT((@v0 + ... + @v49) + (@v50 + ... + @v99)) + ((@v100 + ... + @v149) + (@v150 + ... + @v199))  
    ```  
  
2.  Create a second procedure with a slightly simplified expression, for showplan purposes - the general shape of the plan should be the same. For example, instead of:  
  
    ```  
    SELECT @v0 +@v1 +@v2 +...+@v199  
    ```  
  
    Write:  
  
    ```  
    SELECT @v0 +@v1  
    ```  
  
#### Using a string parameter or variable with DATEPART and related functions in a natively compiled stored procedure results in an error  
**Issue:** When using a natively compiled stored procedure that uses string parameter or variable with the built-in functions DATEPART, DAY, MONTH, and YEAR, an error message shows that datetimeoffset is not supported with natively compiled stored procedures.  
  
**Workaround:** Assign the string parameter or variable to a new variable of type datetime2, and use that variable in the function DATEPART, DAY, MONTH, or YEAR. For example:  
  
```  
DECLARE @d datetime2 = @string  
DATEPART(weekday, @d)  
```  
  
#### Native Compilation Advisor flags DELETE FROM clauses incorrectly  
**Issue:** Native Compilation Advisor flags DELETE FROM clauses inside a stored procedure incorrectly as incompatible.  
  
**Workaround:** None.  
  
#### Register through SSMS adds DAC meta-data with mismatched instance IDs  
**Issue:** When registering or deleting a Data-Tier Application package (.dacpac) through SQL Server Management Studio, the sysdac* tables are not updated correctly to allow a user to query dacpac history for the database.  The instance_id for sysdac_history_internal and sysdac_instances_internal do not match to allow for a join.  
  
**Workaround:** This issue is fixed with the feature pack redistribution of the [Data-Tier Application Framework](https://www.microsoft.com/download/details.aspx?id=42295).  After the update is applied, all new history entries will use the value listed for the instance_id in the sysdac_instances_internal table.  
  
If you already have the issue with mismatched instance_id values, the only way to correct the mismatched values is to connect to the server as a user with privileges to write to MSDB database and update the instance_id values to match.  If you get several register and unregister events from the same database, you may need to look at the time/date to see which records match with the current instance_id value.  
  
1.  Connect to the server in SQL Server Management Studio using a login that has update permissions to MSDB.    
2.  Open a new query using the MSDB database.    
3.  Run this query to see all of your active dac instances.  Find the instance that you want to correct and note the instance_id:  
  
    `select * from` sysdac_instances_internal  
  
4.  Run this query to see all of the history entries:  
  
    `select * from` sysdac_history_internal  
  
5.  Identify the rows that should correspond to the instance you are fixing. 
6.  Update the sysdac_history_internal.instance_id value to the value you noted in step 3 (from the sysdac_instances_internal table):  
  
    `update` sysdac_history_internal `set` instance_id = '\<value from step 3\>' `where` \<expression that matches the rows you want to update\>  
  
### <a name="SSRS"></a>Reporting Services (RTM)
  
#### The SQL Server 2012 Reporting Services Native Mode report server cannot run side-by-side with SQL Server 2014 Reporting Services SharePoint Components  
**Issue:** The [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] Native mode Windows service 'SQL Server Reporting Services' (ReportingServicesService.exe) fails to start when there are [!INCLUDE[ssSQL14](../includes/sssql14-md.md)] [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] SharePoint components installed on the same server.  
  
**Workaround:** Uninstall [!INCLUDE[ssSQL14](../includes/sssql14-md.md)] [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] SharePoint components and restart Microsoft SQL Server 2012 Reporting Services Windows service.  
  
**More Information:**  
  
[!INCLUDE[ssSQL11](../includes/sssql11-md.md)] [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] Native Mode cannot run side-by-side in either of the following conditions:  
  
-   [!INCLUDE[ssSQL14](../includes/sssql14-md.md)] [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] Add-in for SharePoint Products    
-   [!INCLUDE[ssSQL14](../includes/sssql14-md.md)] [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] SharePoint Shared Service  
  
The side-by-side installation prevents the [!INCLUDE[ssSQL11](../includes/sssql11-md.md)] [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] Native Mode Windows Service from starting. Error messages, similar to the those depicted here, will be seen in the Windows Event log:  
  
```  
Log Name:   Application  
Source:          Report Server (<SQL instance ID>)  
Event ID:        117  
Task Category:   Startup/Shutdown  
Level:           Error  
Keywords:        Classic  
Description:     The report server database is an invalid version.  
  
Log Name:      Application  
Source:        Report Server (<SQL instance ID>)  
Event ID:      107  
Task Category: Management  
Level:         Error  
Keywords:      Classic  
Description:   Report Server (DENALI) cannot connect to the report server database.  
```  
  
For more information, see [SQL Server 2014 Reporting Services Tips, Tricks, and Troubleshooting](https://go.microsoft.com/fwlink/?LinkID=391254).  
  
#### Required Upgrade Order for Multi-node SharePoint Farm to SQL Server 2014 Reporting Services  
**Issue:** Report rendering in a multi-node farm fails if instances of the [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] SharePoint Shared Service are upgraded before all instances of the [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] Add-in for SharePoint Products.  
  
**Workaround:** In a multi-node SharePoint farm:  
  
1.  First upgrade all instances of the [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] Add-in for SharePoint Products.    
2.  Then upgrade all instances of the [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] SharePoint Shared Service.  
  
For more information, see [SQL Server 2014 Reporting Services Tips, Tricks, and Troubleshooting](https://go.microsoft.com/fwlink/?LinkID=391254)  
  
### <a name="AzureVM"></a>SQL Server 2014 RTM on Windows Azure Virtual Machines  
  
#### The Add Azure Replica Wizard returns an error when configuring an Availability Group Listener in Windows Azure  
**Issue:** If an Availability Group has a Listener, the Add Azure Replica Wizard will return an error when trying to configure the Listener in Windows Azure.  
  
This issue is because Availability Group Listeners require assigning one IP address in every subnet hosting Availability Group replicas, including the Azure subnet.  
  
**Workaround:**  
  
1.  In the Listener page, assign a free static IP address in the Azure subnet that will host the Availability Group replica to the Availability Group Listener.  
  
    This workaround will allow the Wizard to complete adding the replica in Windows Azure.  
  
2.  After the Wizard completes, you will need to finish the configuration of the Listener in Windows Azure as described in [Listener Configuration for AlwaysOn Availability Groups in Windows Azure](https://msdn.microsoft.com/library/dn376546.aspx)  
  
### <a name="SSAS"></a>Analysis Services (RTM)
  
#### MSOLAP.5 must be downloaded, installed, and registered for a SharePoint 2010 new farm configured with SQL Server 2014  
**Issue:**  
  
-   For a SharePoint 2010 MSOLAP.5 must be downloaded, installed and registered for a SharePoint 2013 new farm configured with SQL Server 2014farm configured with a SQL Server 2014 RTM deployment, PowerPivot workbooks cannot connect to data models because the provider referenced in the connection string is not installed.  
  
**Workaround:**  
  
1.  Download the MSOLAP.5 provider from the [!INCLUDE[ssSQL11SP1](../includes/sssql11sp1-md.md)] Feature Pack. Install the provider on the application servers running Excel Services. For more information, see the section "Microsoft Analysis Services OLE DB Provider for Microsoft SQL Server 2012 SP1" [Microsoft SQL Server 2012 SP1 Feature Pack](https://www.microsoft.com/download/details.aspx?id=35580).  
  
2.  Register MSOLAP.5 as a trusted provider with SharePoint Excel Services. For more information, see [Add MSOLAP.5 as a Trusted Data Provider in Excel Services](https://technet.microsoft.com/library/hh758436.aspx).  
  
**More Information:**  
  
-   [!INCLUDE[ssSQL14](../includes/sssql14-md.md)] includes MSOLAP.6. [!INCLUDE[ssSQL11](../includes/sssql11-md.md)] and [!INCLUDE[ssSQL14](../includes/sssql14-md.md)][!INCLUDE[ssGemini](../includes/ssgemini-md.md)] workbooks use MSOLAP.5. If MSOLAP.5 is not installed on the computer running Excel Services, Excel Services cannot load the data models.  
  
#### MSOLAP.5 must be downloaded, installed and registered for a SharePoint 2013 new farm configured with SQL Server 2014  
**Issue:**  
  
-   For a SharePoint 2013 farm configured with a [!INCLUDE[ssSQL14](../includes/sssql14-md.md)] deployment, Excel workbooks referencing the MSOLAP.5 provider cannot connect to tabula data models because the provider referenced in the connection string is not installed.  
  
**Workaround:**  
  
1.  Download the MSOLAP.5 provider from the [!INCLUDE[ssSQL11SP1](../includes/sssql11sp1-md.md)] Feature Pack. Install the provider on the application servers running Excel Services. For more information, see the section "Microsoft Analysis Services OLE DB Provider for Microsoft SQL Server 2012 SP1" [Microsoft SQL Server 2012 SP1 Feature Pack](https://www.microsoft.com/download/details.aspx?id=35580).  
  
2.  Register MSOLAP.5 as a trusted provider with SharePoint Excel Services. For more information, see [Add MSOLAP.5 as a Trusted Data Provider in Excel Services](https://technet.microsoft.com/library/hh758436.aspx).  
  
**More Information:**  
  
-   [!INCLUDE[ssSQL14](../includes/sssql14-md.md)] includes MSOLAP.6. but SQL Server 2014 PowerPivot workbooks use MSOLAP.5. If MSOLAP.5 is not installed on the computer running Excel Services, Excel Services cannot load the data models.  
  
#### Corrupt Data Refresh Schedules (RTM)
**Issue:**  
  
-   You update a refresh schedule and the schedule becomes corrupt and unusable.  
  
**Workaround:**  
  
1.  In Microsoft Excel, clear the custom advanced properties. See the "Workaround" section of the following knowledge base article [KB 2927748](https://support.microsoft.com/kb/2927748).  
  
**More Information:**  
  
-    If the serialized length of the refresh schedule is smaller than the original schedule, when you update a data refresh schedule for a workbook the buffer size is not correctly updated and the new schedule information is merged with the old schedule information resulting in a corrupt schedule.  
  
### <a name="DQS"></a>Data Quality Services (RTM)
  
#### No cross-version support for Data Quality Services in Master Data Services  
**Issue:** The following scenarios are not supported:  
  
-   Master Data Services 2014 hosted in a SQL Server Database Engine database in SQL Server 2012 with Data Quality Services 2012 installed.  
  
-   Master Data Services 2012 hosted in a SQL Server Database Engine database in SQL Server 2014 with Data Quality Services 2014 installed.  
  
**Workaround:** Use the same version of Master Data Services as the Database Engine database and Data Quality Services.  
  
### <a name="UA"></a>Upgrade Advisor Issues (RTM)
  
#### SQL Server 2014 Upgrade Advisor reports irrelevant upgrade issues for SQL Server Reporting Services  
**Issue:** SQL Server Upgrade Advisor (SSUA) shipped with the SQL Server 2014 media incorrectly reports multiple errors when analyzing SQL Server Reporting Services server.  
  
**Workaround:** This issue is fixed in the SQL Server Upgrade Advisor provided in the [SQL Server 2014 Feature Pack for SSUA](https://go.microsoft.com/fwlink/?LinkID=306709).  
  
#### SQL Server 2014 Upgrade Advisor reports an error when analyzing SQL Server Integration Services server  
**Issue:** SQL Server Upgrade Advisor (SSUA) shipped with the SQL Server 2014 media reports an error when analyzing SQL Server Integration Services server.  The error that is displayed to the user is:  
  
```  
The installed version of Integration Services does not support Upgrade Advisor.   
The assembly information is "Microsoft.SqlServer.ManagedDTS, Version=11.0.0.0,   
Culture=neutral, PublicKeyToken=89845dcd8080cc91  
```  
  
**Workaround:** This issue is fixed in the SQL Server Upgrade Advisor provided in the [SQL Server 2014 Feature Pack for SSUA](https://go.microsoft.com/fwlink/?LinkID=306709).  
  
[!INCLUDE[get-help-options](../includes/paragraph-content/get-help-options.md)]
