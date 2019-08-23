---
title: "View and Read Failover Cluster Instance Diagnostics Log | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: high-availability
ms.topic: conceptual
ms.assetid: 68074bd5-be9d-4487-a320-5b51ef8e2b2d
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# View and Read Failover Cluster Instance Diagnostics Log
  All critical errors and warning events for the SQL Server Resource DLL are written to the Windows event log. A running log of the diagnostic information specific to SQL Server is captured by the [sp_server_diagnostics &#40;Transact-SQL&#41;](/sql/relational-databases/system-stored-procedures/sp-server-diagnostics-transact-sql) system stored procedure and is written to the SQL Server failover cluster diagnostics (also known as the *SQLDIAG* logs) log files.  
  
-   **Before you begin:**  [Recommendations](#Recommendations), [Security](#Security)  
  
-   **To View the Diagnostic Log, using:**  [SQL Server Management Studio](#SSMSProcedure), [Transact-SQL](#TsqlProcedure)  
  
-   **To Configure Diagnostic Log settings, using:** [Transact-SQL](#TsqlConfigure)  
  
##  <a name="BeforeYouBegin"></a> Before You Begin  
  
###  <a name="Recommendations"></a> Recommendations  
 By default, the SQLDIAG are stored under a local LOG folder of the SQL Server instance directory, for example, 'C\Program Files\Microsoft SQL Server\MSSQL12.\<InstanceName>\MSSQL\LOG' of the owning node of the AlwaysOn Failover Cluster Instance (FCI). The size of each SQLDIAG log file is fixed at 100 MB. Ten such log files are stored on the computer before they are recycled for new logs.  
  
 The logs use the extended events file format. The **sys.fn_xe_file_target_read_file** system function can be used to read the files that are created by Extended Events. One event, in XML format, is returned per row. Query the system view to parse the XML data as a result-set. For more information, see [sys.fn_xe_file_target_read_file &#40;Transact-SQL&#41;](/sql/relational-databases/system-functions/sys-fn-xe-file-target-read-file-transact-sql).  
  
###  <a name="Security"></a> Security  
  
####  <a name="Permissions"></a> Permissions  
 VIEW SERVER STATE permission is needed to run **fn_xe_file_target_read_file**.  
  
 Open SQL Server Management Studio as Administrator  
  
##  <a name="SSMSProcedure"></a> Using SQL Server Management Studio  
 **To view the Diagnostic log files:**  
  
1.  From the **File** menu, select **Open**, **File**, and choose the diagnostic log file you want to view.  
  
2.  The events are displayed as rows in the right pane, and by default **name**, and **timestamp** are the only two columns displayed.  
  
     This also activates the **ExtendedEvents** menu.  
  
3.  To see more columns, go the **ExtendedEvents** menu, and select **Choose Columns**.  
  
     A dialog box opens with the available columns allowing you to select the columns for display.  
  
4.  You can filter, and sort the event data using the **ExtendedEvents** menu and selecting the **Filter** option.  
  
##  <a name="TsqlProcedure"></a> Using Transact-SQL  
 **To view the Diagnostic log files:**  
  
 To view all the log items in the SQLDIAG log file, use the following query:  
  
```  
SELECT  
xml_data.value('(event/@name)[1]','varchar(max)') AS 'Name'  
,xml_data.value('(event/@package)[1]','varchar(max)') AS 'Package'  
,xml_data.value('(event/@timestamp)[1]','datetime') AS 'Time'  
,xml_data.value('(event/data[@name=''state'']/value)[1]','int') AS 'State'  
,xml_data.value('(event/data[@name=''state_desc'']/text)[1]','varchar(max)') AS 'State Description'  
,xml_data.value('(event/data[@name=''failure_condition_level'']/value)[1]','int') AS 'Failure Conditions'  
,xml_data.value('(event/data[@name=''node_name'']/value)[1]','varchar(max)') AS 'Node_Name'  
,xml_data.value('(event/data[@name=''instancename'']/value)[1]','varchar(max)') AS 'Instance Name'  
,xml_data.value('(event/data[@name=''creation time'']/value)[1]','datetime') AS 'Creation Time'  
,xml_data.value('(event/data[@name=''component'']/value)[1]','varchar(max)') AS 'Component'  
,xml_data.value('(event/data[@name=''data'']/value)[1]','varchar(max)') AS 'Data'  
,xml_data.value('(event/data[@name=''info'']/value)[1]','varchar(max)') AS 'Info'  
FROM  
 ( SELECT object_name AS 'event'  
  ,CONVERT(xml,event_data) AS 'xml_data'  
  FROM sys.fn_xe_file_target_read_file('C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\Log\SQLNODE1_MSSQLSERVER_SQLDIAG_0_129936003752530000.xel',NULL,NULL,NULL)   
)   
AS XEventData  
ORDER BY Time;  
  
```  
  
> [!NOTE]  
>  You can filter the results for specific components or state using the WHERE clause.  
  
##  <a name="TsqlConfigure"></a> Using Transact-SQL  
 **To configure the Diagnostic Log Properties**  
  
> [!NOTE]  
>  For an example of this procedure, see [Example (Transact-SQL)](#TsqlExample), later in this section.  
  
 Using the Data Definition Language (DDL) statement, `ALTER SERVER CONFIGURATION`, you can start or stop logging diagnostic data captured by the [sp_server_diagnostics &#40;Transact-SQL&#41;](/sql/relational-databases/system-stored-procedures/sp-server-diagnostics-transact-sql) procedure, and set SQLDIAG log configuration parameters such as the log file rollover count, log file size, and file location. For syntax details, see [Setting diagnostic log options](/sql/t-sql/statements/alter-server-configuration-transact-sql#Diagnostic).  
  
###  <a name="ConfigTsqlExample"></a> Examples (Transact-SQL)  
  
####  <a name="TsqlExample"></a> Setting diagnostic log options  
 The examples in this section show how to set the values for the diagnostic log option.  
  
##### A. Starting diagnostic logging  
 The following example starts the logging of diagnostic data.  
  
```  
ALTER SERVER CONFIGURATION SET DIAGNOSTICS LOG ON;  
```  
  
##### B. Stopping diagnostic logging  
 The following example stops the logging of diagnostic data.  
  
```  
ALTER SERVER CONFIGURATION SET DIAGNOSTICS LOG OFF;  
```  
  
##### C. Specifying the location of the diagnostic logs  
 The following example sets the location of the diagnostic logs to the specified file path.  
  
```  
ALTER SERVER CONFIGURATION  
SET DIAGNOSTICS LOG PATH = 'C:\logs';  
```  
  
##### D. Specifying the maximum size of each diagnostic log  
 The following example set the maximum size of each diagnostic log to 10 megabytes.  
  
```  
ALTER SERVER CONFIGURATION   
SET DIAGNOSTICS LOG MAX_SIZE = 10 MB;  
```  
  
## See Also  
 [Failover Policy for Failover Cluster Instances](failover-policy-for-failover-cluster-instances.md)  
  
  
