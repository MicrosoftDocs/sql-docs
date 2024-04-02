---
title: "View & read failover cluster instance diagnostic log"
description: Learn how to view and read the running diagnostic log produced by a SQL Server failover cluster instance.
author: MashaMSFT
ms.author: mathoma
ms.reviewer: jopilov
ms.date: 02/14/2023
ms.service: sql
ms.subservice: failover-cluster-instance
ms.topic: how-to
---
# View and Read Failover Cluster Instance Diagnostics Log

[!INCLUDE [SQL Server](../../../includes/applies-to-version/sqlserver.md)]

All critical errors and warning events for the SQL Server Resource DLL are written to the Windows event log. A running log of the diagnostic information specific to SQL Server is captured by the [sp_server_diagnostics &#40;Transact-SQL&#41;](../../../relational-databases/system-stored-procedures/sp-server-diagnostics-transact-sql.md) system stored procedure and is written to the SQL Server failover cluster diagnostics (also known as the *SQLDIAG* logs) log files.

- **Before you begin:**  [File name, location and format](#file-name-location-format), [Security](#Security)

- **To View the Diagnostic Log, using:**  [SQL Server Management Studio](#SSMSProcedure), [Transact-SQL](#TsqlProcedure)

- **To Configure Diagnostic Log settings, using:** [Transact-SQL](#TsqlConfigure)

## <a id="BeforeYouBegin"></a> Before You Begin

### <a id="file-name-location-format"></a> File name, location and format

 By default, the SQLDIAG are stored under a local LOG folder of the SQL Server instance directory, for example, 'C\Program Files\Microsoft SQL Server\MSSQL13.\<InstanceName>\MSSQL\LOG' of the owning node of the Always On Failover Cluster Instance (FCI). The maximum size of each SQLDIAG log file is fixed at 100 MB. Ten such log files are stored on the computer before they are recycled for new logs.  The file name is of the following format `MACHINE_SQLINSTANCE_SQLDIAG_0_xxxxxxxxxxxxxxxxx.xel` where the last part 'xxxxxxxx' is an auto-generated number. For example for a default instance the file name would be `NODE1_MSSQLSERVER_SQLDIAG_0_133177967257760000.xel` and for a named instance the name would be `NODE1_SQL2019INST_SQLDIAG_0_133177967257760000.xel`

 The logs use the extended events file format. The `sys.fn_xe_file_target_read_file` system function can be used to read the files that are created by Extended Events and display them as a result-set. One event, in XML format, is returned per row. For more information, see [sys.fn_xe_file_target_read_file (Transact-SQL)](../../../relational-databases/system-functions/sys-fn-xe-file-target-read-file-transact-sql.md).

### <a id="Security"></a> Security

#### <a id="Permissions"></a> Permissions

 VIEW SERVER STATE permission is needed to run **fn_xe_file_target_read_file**.

 Open SQL Server Management Studio as Administrator

## <a id="SSMSProcedure"></a> Use SQL Server Management Studio

 **To view the Diagnostic log files:**

1. From the **File** menu, select **Open**, **File**, and choose the diagnostic log file you want to view.

1. The events are displayed as rows in the right pane, and by default **name**, and **timestamp** are the only two columns displayed.

     This also activates the **ExtendedEvents** menu.

1. To see more columns, go the **ExtendedEvents** menu, and select **Choose Columns**.

     A dialog box opens with the available columns allowing you to select the columns for display.

1. You can filter, and sort the event data using the **ExtendedEvents** menu and selecting the **Filter** option.

## <a id="TsqlProcedure"></a> View Diagnostic log files with Transact-SQL

 **To view the Diagnostic log files:**

 To view all the log items in the SQLDIAG log file, use the following query:

```sql
SELECT
  xml_data.value('(event/@name)[1]','varchar(max)') AS 'Name'
  ,xml_data.value('(event/@package)[1]','varchar(max)') AS 'Package'
  ,xml_data.value('(event/@timestamp)[1]','datetime') AS 'Time'
  ,xml_data.value('(event/data[@name=''state'']/value)[1]','int') AS 'State'
  ,xml_data.value('(event/data[@name=''state_desc'']/text)[1]','varchar(max)') AS 'State   Description'
  ,xml_data.value('(event/data[@name=''failure_condition_level'']/value)[1]','int') AS   'Failure Conditions'
  ,xml_data.value('(event/data[@name=''node_name'']/value)[1]','varchar(max)') AS   'Node_Name'
  ,xml_data.value('(event/data[@name=''instancename'']/value)[1]','varchar(max)') AS   'Instance Name'
  ,xml_data.value('(event/data[@name=''creation time'']/value)[1]','datetime') AS 'Creation   Time'
  ,xml_data.value('(event/data[@name=''component'']/value)[1]','varchar(max)') AS   'Component'
  ,xml_data.value('(event/data[@name=''data'']/value)[1]','varchar(max)') AS 'Data'
  ,xml_data.value('(event/data[@name=''info'']/value)[1]','varchar(max)') AS 'Info'
FROM
 ( SELECT object_name AS 'event'
  ,CONVERT(xml,event_data) AS 'xml_data'
  FROM sys.fn_xe_file_target_read_file('C:\Program Files\Microsoft SQL Server\MSSQL13.MSSQLSERVER\MSSQL\Log\SQLNODE1_MSSQLSERVER_SQLDIAG_0_129936003752530000.xel',NULL,NULL,NULL)
 )
AS XEventData
ORDER BY Time;
```

> [!NOTE]  
> You can filter the results for specific components or state using the WHERE clause.

## <a id="TsqlConfigure"></a> Configure Diagnostic Log Properties with Transact-SQL

 **To configure the Diagnostic log properties:**

> [!NOTE]  
> For an example of this procedure, see [Example (Transact-SQL)](#TsqlExample), later in this section.

 Using the Data Definition Language (DDL) statement, **ALTER SERVER CONFIGURATION**, you can start or stop logging diagnostic data captured by the [sp_server_diagnostics (Transact-SQL)](../../../relational-databases/system-stored-procedures/sp-server-diagnostics-transact-sql.md) procedure, and set SQLDIAG log configuration parameters such as the log file rollover count, log file size, and file location. For syntax details, see [Setting diagnostic log options](../../../t-sql/statements/alter-server-configuration-transact-sql.md#Diagnostic).

### <a id="ConfigTsqlExample"></a> Examples (Transact-SQL)

#### <a id="TsqlExample"></a> Set diagnostic log options

 The examples in this section show how to set the values for the diagnostic log option.

##### A. Start diagnostic logging

 The following example starts the logging of diagnostic data.

```sql
ALTER SERVER CONFIGURATION SET DIAGNOSTICS LOG ON;
```

##### B. Stop diagnostic logging

 The following example stops the logging of diagnostic data.

```sql
ALTER SERVER CONFIGURATION SET DIAGNOSTICS LOG OFF;
```

##### C. Specify the location of the diagnostic logs

 The following example sets the location of the diagnostic logs to the specified file path.

```sql
ALTER SERVER CONFIGURATION
SET DIAGNOSTICS LOG PATH = 'C:\logs';
```

##### D. Specify the maximum size of each diagnostic log

 The following example set the maximum size of each diagnostic log to 10 megabytes.

```sql
ALTER SERVER CONFIGURATION
SET DIAGNOSTICS LOG MAX_SIZE = 10 MB;
```

##### E. Check whether Failover Cluster Instance Diagnostics Log is enabled and current configuration.

 The following example uses the dmv sys.dm_os_server_diagnostics_log_configurations to check current configuration

```sql
SELECT is_enabled, [path], max_size, max_files
FROM sys.dm_os_server_diagnostics_log_configurations;
```

## See also

- [Failover Policy for Failover Cluster Instances](../../../sql-server/failover-clusters/windows/failover-policy-for-failover-cluster-instances.md)
- [sys-dm-os-server-diagnostics-log-configurations](../../../relational-databases/system-dynamic-management-views/sys-dm-os-server-diagnostics-log-configurations.md)
