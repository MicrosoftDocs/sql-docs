---
title: "SQL Server 2014 Release Notes | Microsoft Docs"
ms.custom: ""
ms.date: "01/31/2017"
ms.prod: "sql-server-2014"
ms.technology: "server-general"
ms.reviewer: ""
ms.suite: ""
ms.tgt_pltfrm: ""
ms.topic: "article"
ms.assetid: bf4c4922-80b3-4be3-bf71-228247f97004
caps.latest.revision: 100
author: "craigg-msft"
ms.author: "craigg"
manager: "jhubbard"
---
# SQL Server 2014 Release Notes
This Release Notes document describes known issues that you should read about before you install or troubleshoot [!INCLUDE[ssSQL14](../includes/sssql14-md.md)].  
  
## <a name="top"></a>Contents  
[1.0 Before You Install](#BeforeInstall)  
  
[2.0 Product Documentation](#ProdDoc)  
  
[3.0 Database Engine](#DBEngine)  
  
[4.0 Reporting Services](#SSRS)  
  
[5.0 SQL Server 2014 on Windows Azure Virtual Machines](#AzureVM)  
  
[6.0 Analysis Services](#SSAS)  
  
[7.0 Data Quality Services](#DQS)  
  
[8.0 Upgrade Advisor](#UA)  
  
## <a name="BeforeInstall"></a>1.0 Before You Install  
  
### 1.1 Limitations and Restrictions in SQL Server 2014 RTM  
  
#### 1.1.1 General limitations and restrictions  
  
1.  Upgrading from SQL Server 2014 CTP 1 to SQL Server 2014 RTM is NOT supported.  
  
2.  Installing SQL Server 2014 CTP 1 side-by-side with SQL Server 2014 RTM is NOT supported.  
  
3.  Attaching or restoring a SQL Server 2014 CTP 1 database to SQL Server 2014 RTM is NOT supported.  
  
**Workaround:** None.  
  
#### 1.2 Considerations for Upgrading SQL Server 2014 CTP 2 to SQL Server 2014 RTM and Downgrading from SQL Server 2014 RTM to SQL Server 2014 CTP 2  
  
#### 1.2.1 Upgrading from SQL Server 2014 CTP 2 to SQL Server RTM is fully supported  
Specifically, you can:  
  
1.  Attach a SQL Server 2014 CTP 2 database to an instance of SQL Server 2014 RTM.  
  
2.  Restore a database backup taken on SQL Server 2014 CTP 2 to an instance of SQL Server 2014 RTM.  
  
3.  In-place upgrade to SQL Server 2014 RTM.  
  
4.  Rolling upgrade to SQL Server 2014 RTM. You are required to switch to manual failover mode before initiating the rolling upgrade. Please refer to [Upgrade and Update of Availability Group Servers with Minimal Downtime and Data Loss](http://msdn.microsoft.com/library/dn178483.aspx) for details.  
  
5.  Data collected by Transaction Performance Collection Sets installed in SQL Server 2014 CTP 2 cannot be viewed through SQL Server Management Studio in SQL Server 2014 RTM, and vice versa. Use SQL Server Management Studio in SQL Server 2014 CTP 2 to view data collected by the Collection Set installed in SQL Server 2014 CTP 2, and use SQL Server Management Studio in SQL Server 2014 RTM to view data collected by the Collection Set installed in SQL Server 2014 RTM.  
  
### 1.2.2 Downgrading from SQL Server 2014 RTM to SQL Server 2014 CTP 2  
This is not supported.  
  
**Workaround:** There is no workaround for downgrade. We recommend that you back-up the database before upgrading to SQL Server 2014 RTM.  
  
![Arrow icon used with Back to Top link](../sql-server/media/uparrow16x16.gif "Arrow icon used with Back to Top link")[Top](#top)  
  
## 1.3 Incorrect version of StreamInsight Client on SQL Server 2014 media/ISO/CAB  
The wrong version of StreamInsight.msi and StreamInsightClient.msi is located in the following path on the SQL Server media/ISO/CAB (StreamInsight\\\<Architecture\>\\\<Language ID\>).  
  
**Workaround:** Download and install the correct version from the [SQL Server 2014 Feature Pack download page](http://go.microsoft.com/fwlink/?LinkID=306709).  
  
## <a name="ProdDoc"></a>2.0 Product Documentation  
  
### 2.1 Report Builder content is not available in some languages  
**Issue:** Report Builder content is not available in the following languages.  
  
-   Greek (el-GR)  
  
-   Norwegian (Bokmal) (nb-NO)  
  
-   Finnish (fi-FI)  
  
-   Danish (da-DK)  
  
In [!INCLUDE[ssSQL11](../includes/sssql11-md.md)], this content was available in a CHM file that shipped with the product and was available in these languages. The CHM files no longer ship with the product and the Report Builder content is only available on MSDN. MSDN does not support these languages. Report Builder was also removed from TechNet and is no longer available in those supported languages.  
  
**Workaround:** None.  
  
### 2.2 PowerPivot content is not available in some languages  
**Issue:** Power Pivot content is not available in the following languages.  
  
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
  
![Arrow icon used with Back to Top link](../sql-server/media/uparrow16x16.gif "Arrow icon used with Back to Top link")[Top](#top)  
  
## <a name="DBEngine"></a>3.0 Database Engine  
  
### 3.1 Changes made for Standard Edition in SQL Server 2014 RTM  
SQL Server 2014 Standard has the following changes:  
  
-   The Buffer Pool Extension feature allows using the maximum size of up to 4x times of configured memory.  
  
-   The maximum memory has been raised from 64GB to 128GB.  
  
### 3.2 In-Memory OLTP Issues  
  
#### 3.2.1 Memory Optimization Advisor flags default constraints as incompatible  
**Issue:** The Memory Optimized Advisor in SQL Server Management Studio flags all default constraints as incompatible. Not all default constraints are supported in a memory-optimized table; the Advisor does not distinguish between supported and unsupported types of default constraints. Supported default constraints include all constants, expressions and built-in functions supported within natively compiled stored procedures. To see the list of functions supported in natively compiled stored procedures, refer to [Supported Constructs in Natively Compiled Stored Procedures](http://msdn.microsoft.com/library/dn452279(v=sql.120).aspx).  
  
**Workaround:** If you want to use the advisor to identify blockers, please ignore the compatible default constraints. To use the Memory Optimization Advisor to migrate tables that have compatible default constraints, but no other blockers, follow these steps:  
  
1.  Remove the default constraints from the table definition.  
  
2.  Use the Advisor to produce a migration script on the table.  
  
3.  Add back the default constraints in the migration script.  
  
4.  Execute the migration script.  
  
#### 3.2.2 Informational message “file access denied” incorrectly reported as an error in the SQL Server 2014 error log  
**Issue:** When restarting a server that has databases that contain memory-optimized tables, you may see the following type of error messages in the SQL Server 2014 error log:  
  
```  
[ERROR]Unable to delete file C:\Program Files\Microsoft SQL   
Server\....old.dll. This error may be due to a previous failure to unload   
memory-optimized table DLLs.  
```  
  
This is, in fact, an informational message and no user action is required.  
  
**Workaround:** None. This is an informational message.  
  
#### 3.2.3 Missing index details incorrectly report included columns for memory-optimized table  
**Issue:** If SQL Server 2014 detects a missing index for a query on a memory-optimized table, it will report a missing index in the SHOWPLAN_XML, as well as in the missing index DMVs such as sys.dm_db_missing_index_details. In some cases, the missing index details will contain included columns. As all columns are implicitly included with all indexes on memory-optimized tables, it is not allowed to explicitly specify included columns with memory-optimized indexes.  
  
**Workaround:** Do not specify the INCLUDE clause with indexes on memory-optimized tables.  
  
#### 3.2.4 Missing index details omit missing indexes if a hash index exists but is not suitable for the query  
**Issue:** If you have a HASH index on columns of a memory-optimized table referenced in a query, but the index cannot be used for the query, SQL Server 2014 will not always report a missing index in SHOWPLAN_XML and in the DMV sys.dm_db_missing_index_details.  
  
In particular, if a query contains equality predicates that involve a subset of the index key columns or if it contains inequality predicates that involve the index key columns, the HASH index cannot be used as is, and a different index would be required to execute the query efficiently.  
  
**Workaround:** In case you are using hash indexes, inspect the queries and query plans to determine if the queries could benefit from Index Seek operations on a subset of the index key, or Index Seek operations on inequality predicates. If you need to seek on a subset of the index key, either use a NONCLUSTERED index, or use a HASH index on exactly the columns you need to seek on. If you need to seek on an inequality predicate, use a NONCLUSTERED index instead of HASH.  
  
#### 3.2.5 Failure when using a memory-optimized table and memory-optimized table variable in the same query, if the database option READ_COMMITTED_SNAPSHOT is set to ON  
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
  
#### 3.2.6 Procedure and query execution statistics for natively compiled stored procedures record worker time in multiples of 1000  
**Issue:** After enabling the collection of procedure or query execution statistics collection for natively compiled stored procedures using sp_xtp_control_proc_exec_stats or sp_xtp_control_query_exec_stats, you will see the *_worker_time reported in multiples of 1000, in the DMVs sys.dm_exec_procedure_stats and sys.dm_exec_query_stats. Query executions that have a worker time of less than 500 microseconds will be reported as having a worker_time of 0.  
  
**Workaround:** None. Do not rely on worker_time reported in the execution stats DMVs for short-running queries in natively compiled stored procedures.  
  
#### 3.2.7 Error with SHOWPLAN_XML for natively compiled stored procedures that contain long expressions  
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
  
#### 3.2.8 Using a string parameter or variable with DATEPART and related functions in a natively compiled stored procedure results in an error  
**Issue:** When using a parameter or variable that has a string datatype such as (var)char or n(var)char with the built-in functions DATEPART, DAY, MONTH, and YEAR inside a natively compiled stored procedure, you will see an error message stating that the datatype datetimeoffset is not supported with natively compiled stored procedures.  
  
**Workaround:** Assign the string parameter or variable to a new variable of type datetime2, and use that variable in the function DATEPART, DAY, MONTH, or YEAR. For example:  
  
```  
DECLARE @d datetime2 = @string  
DATEPART(weekday, @d)  
```  
  
#### 3.2.9 Native Compilation Advisor flags DELETE FROM clauses incorrectly  
**Issue:** Native Compilation Advisor flags DELETE FROM clauses inside a stored procedure incorrectly as incompatible.  
  
**Workaround:** None.  
  
### 3.3 Register through SSMS adds DAC meta-data with mismatched instance IDs  
**Issue:** When registering or deleting a Data-Tier Application package (.dacpac) through SQL Server Management Studio, the sysdac* tables are not updated correctly to allow a user to query dacpac history for the database.  The instance_id for sysdac_history_internal and sysdac_instances_internal do not match to allow for a join.  
  
**Workaround:** This issue is fixed with the feature pack redistribution of the [Data-Tier Application Framework](https://www.microsoft.com/download/details.aspx?id=42295).  After the update is applied, all new history entries will use the value listed for the instance_id in the sysdac_instances_internal table.  
  
If you already have the issue with mismatched instance_id values, the only way to correct the mismatched values is to connect to the server as a user with privileges to write to MSDB database and update the instance_id values to match.  If there has been multiple register and unregister events of the same database, you may need to look at the time/date to see what records match with the current instance_id values.  
  
1.  Connect to the server in SQL Server Management Studio using a login that has update permissions to MSDB.  
  
2.  Open a new query using the MSDB database.  
  
3.  Run this query to see all of your active dac instances.  Find the instance that you want to correct and note the instance_id:  
  
    `select * from` sysdac_instances_internal  
  
4.  Run this query to see all of the history entries:  
  
    `select * from` sysdac_history_internal  
  
5.  Identify the rows that should correspond to the instance you are fixing  
  
6.  Update the sysdac_history_internal.instance_id value to the value you noted in step 3 (from the sysdac_instances_internal table):  
  
    `update` sysdac_history_internal `set` instance_id = '\<value from step 3\>' `where` \<expression that matches the rows you want to update\>  
  
![Arrow icon used with Back to Top link](../sql-server/media/uparrow16x16.gif "Arrow icon used with Back to Top link")[Top](#top)  
  
## <a name="SSRS"></a>4.0 Reporting Services  
  
### 4.1 The SQL Server 2012 Reporting Services Native Mode report server cannot run side-by-side with SQL Server 2014 Reporting Services SharePoint Components  
**Issue:** The [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] Native mode Windows service ‘SQL Server Reporting Services’ (ReportingServicesService.exe) fails to start if there are [!INCLUDE[ssSQL14](../includes/sssql14-md.md)] [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] SharePoint components installed on the same server.  
  
**Workaround:** Uninstall [!INCLUDE[ssSQL14](../includes/sssql14-md.md)] [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] SharePoint components and restart Microsoft SQL Server 2012 Reporting Services Windows service.  
  
**More Information:**  
  
[!INCLUDE[ssSQL11](../includes/sssql11-md.md)] [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] Native Mode cannot run side-by-side with either of the following:  
  
-   [!INCLUDE[ssSQL14](../includes/sssql14-md.md)] [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] Add-in for SharePoint Products  
  
-   [!INCLUDE[ssSQL14](../includes/sssql14-md.md)] [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] SharePoint Shared Service  
  
The side-by-side installation prevents the [!INCLUDE[ssSQL11](../includes/sssql11-md.md)] [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] Native Mode Windows Service from starting. Error messages similar to the following will be seen in the Windows Event log:  
  
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
  
For more information, see [SQL Server 2014 Reporting Services Tips, Tricks, and Troubleshooting](http://go.microsoft.com/fwlink/?LinkID=391254).  
  
### 4.2 Required Upgrade Order for Multi-node SharePoint Farm to SQL Server 2014 Reporting Services  
**Issue:** Report rendering in a multi-node farm fails if instances of the [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] SharePoint Shared Service are upgraded before all instances of the [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] Add-in for SharePoint Products.  
  
**Workaround:** In a multi-node SharePoint farm:  
  
1.  First upgrade all instances of the [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] Add-in for SharePoint Products.  
  
2.  Then upgrade all instances of the [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] SharePoint Shared Service.  
  
For more information, see [SQL Server 2014 Reporting Services Tips, Tricks, and Troubleshooting](http://go.microsoft.com/fwlink/?LinkID=391254)  
  
![Arrow icon used with Back to Top link](../sql-server/media/uparrow16x16.gif "Arrow icon used with Back to Top link")[Top](#top)  
  
## <a name="AzureVM"></a>5.0 SQL Server 2014 RTM on Windows Azure Virtual Machines  
  
### 5.1 The Add Azure Replica Wizard returns an error when configuring an Availability Group Listener in Windows Azure  
**Issue:** If an Availability Group has a Listener, the Add Azure Replica Wizard will return an error when trying to configure the Listener in Windows Azure.  
  
This is because Availability Group Listeners require assigning one IP address in every subnet hosting Availability Group replicas, including the Azure subnet.  
  
**Workaround:**  
  
1.  In the Listener page, assign a free static IP address in the Azure subnet that will host the Availability Group replica to the Availability Group Listener.  
  
    This will allow the Wizard to complete adding the replica in Windows Azure.  
  
2.  After the Wizard completes, you will need to finish the configuration of the Listener in Windows Azure as described in [Listener Configuration for AlwaysOn Availability Groups in Windows Azure](http://msdn.microsoft.com/library/dn376546.aspx)  
  
![Arrow icon used with Back to Top link](../sql-server/media/uparrow16x16.gif "Arrow icon used with Back to Top link")[Top](#top)  
  
## <a name="SSAS"></a>6.0 Analysis Services  
  
### 6.1 MSOLAP.5 must be downloaded, installed and registered for a SharePoint 2010 new farm configured with SQL Server 2014  
**Issue:**  
  
-   For a SharePoint 2010 farm configured with a SQL Server 2014 RTM deployment, PowerPivot workbooks cannot connect to data models because the provider referenced in the connection string is not installed.  
  
**Workaround:**  
  
1.  Download the MSOLAP.5 provider from the [!INCLUDE[ssSQL11SP1](../includes/sssql11sp1-md.md)] Feature Pack. Install the provider on the application servers running Excel Services. For more information, see the section “Microsoft Analysis Services OLE DB Provider for Microsoft SQL Server 2012 SP1” [Microsoft SQL Server 2012 SP1 Feature Pack](http://www.microsoft.com/download/details.aspx?id=35580).  
  
2.  Register MSOLAP.5 as a trusted provider with SharePoint Excel Services. For more information, see [Add MSOLAP.5 as a Trusted Data Provider in Excel Services](http://technet.microsoft.com/library/hh758436.aspx).  
  
**More Information:**  
  
-   [!INCLUDE[ssSQL14](../includes/sssql14-md.md)] includes MSOLAP.6. [!INCLUDE[ssSQL11](../includes/sssql11-md.md)] and [!INCLUDE[ssSQL14](../includes/sssql14-md.md)][!INCLUDE[ssGemini](../includes/ssgemini-md.md)] workbooks use MSOLAP.5. If MSOLAP.5 is not installed on the computer running Excel Services, Excel Services cannot load the data models.  
  
### 6.2 MSOLAP.5 must be downloaded, installed and registered for a SharePoint 2013 new farm configured with SQL Server 2014  
**Issue:**  
  
-   For a SharePoint 2013 farm configured with a [!INCLUDE[ssSQL14](../includes/sssql14-md.md)] deployment, Excel workbooks referencing the MSOLAP.5 provider cannot connect to tabula data models because the provider referenced in the connection string is not installed.  
  
**Workaround:**  
  
1.  Download the MSOLAP.5 provider from the [!INCLUDE[ssSQL11SP1](../includes/sssql11sp1-md.md)] Feature Pack. Install the provider on the application servers running Excel Services. For more information, see the section “Microsoft Analysis Services OLE DB Provider for Microsoft SQL Server 2012 SP1” [Microsoft SQL Server 2012 SP1 Feature Pack](http://www.microsoft.com/download/details.aspx?id=35580).  
  
2.  Register MSOLAP.5 as a trusted provider with SharePoint Excel Services. For more information, see [Add MSOLAP.5 as a Trusted Data Provider in Excel Services](http://technet.microsoft.com/library/hh758436.aspx).  
  
**More Information:**  
  
-   [!INCLUDE[ssSQL14](../includes/sssql14-md.md)] includes MSOLAP.6. but SQL Server 2014 PowerPivot workbooks use MSOLAP.5. If MSOLAP.5 is not installed on the computer running Excel Services, Excel Services cannot load the data models.  
  
### 6.3 Corrupt Data Refresh Schedules  
**Issue:**  
  
-   You update a refresh schedule and the schedule becomes corrupt and unusable.  
  
**Workaround:**  
  
1.  In Microsoft Excel, clear the custom advanced properties. Please see the “Workaround” section of the following knowledge base article [KB 2927748](http://support.microsoft.com/kb/2927748).  
  
**More Information:**  
  
-   When you update a data refresh schedule for a workbook, if the serialized length of the refresh schedule is smaller than the original schedule, the buffer size is not correctly updated and the new schedule information is merged with the old schedule information resulting in a corrupt schedule.  
  
![Arrow icon used with Back to Top link](../sql-server/media/uparrow16x16.gif "Arrow icon used with Back to Top link")[Top](#top)  
  
## <a name="DQS"></a>7.0 Data Quality Services  
  
### 7.1 No cross-version support for Data Quality Services in Master Data Services  
**Issue:** The following scenarios are not supported:  
  
-   Master Data Services 2014 hosted in a SQL Server Database Engine database in SQL Server 2012 with Data Quality Services 2012 installed.  
  
-   Master Data Services 2012 hosted in a SQL Server Database Engine database in SQL Server 2014 with Data Quality Services 2014 installed.  
  
**Workaround:** Use the same version of Master Data Services as the Database Engine database and Data Quality Services.  
  
![Arrow icon used with Back to Top link](../sql-server/media/uparrow16x16.gif "Arrow icon used with Back to Top link")[Top](#top)  
  
## <a name="UA"></a>8.0 Upgrade Advisor Issues  
  
### 8.1  SQL Server 2014 Upgrade Advisor reports irrelevant upgrade issues for SQL Server Reporting Services  
**Issue:** SQL Server Upgrade Advisor (SSUA) shipped with the SQL Server 2014 media incorrectly reports multiple errors when analyzing SQL Server Reporting Services server.  
  
**Workaround:** This issue is fixed in the SQL Server Upgrade Advisor provided in the [SQL Server 2014 Feature Pack for SSUA](http://go.microsoft.com/fwlink/?LinkID=306709).  
  
### 8.2  SQL Server 2014 Upgrade Advisor reports an error when analyzing SQL Server Integration Services server  
**Issue:** SQL Server Upgrade Advisor (SSUA) shipped with the SQL Server 2014 media reports an error when analyzing SQL Server Integration Services server.  The error that is displayed to the user is:  
  
```  
The installed version of Integration Services does not support Upgrade Advisor.   
The assembly information is "Microsoft.SqlServer.ManagedDTS, Version=11.0.0.0,   
Culture=neutral, PublicKeyToken=89845dcd8080cc91  
```  
  
**Workaround:** This issue is fixed in the SQL Server Upgrade Advisor provided in the [SQL Server 2014 Feature Pack for SSUA](http://go.microsoft.com/fwlink/?LinkID=306709).  
  
![Arrow icon used with Back to Top link](../sql-server/media/uparrow16x16.gif "Arrow icon used with Back to Top link")[Top](#top)  
  
[!INCLUDE[get-help-options](../includes/paragraph-content/get-help-options.md)]