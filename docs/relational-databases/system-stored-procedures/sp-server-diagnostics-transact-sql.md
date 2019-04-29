---
title: "sp_server_diagnostics (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "11/14/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "sp_server_diagnostics"
  - "sp_server_diagnostics_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sp_server_diagnostics"
ms.assetid: 62658017-d089-459c-9492-c51e28f60efe
author: stevestein
ms.author: sstein
manager: craigg
---
# sp_server_diagnostics (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2012-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2012-xxxx-xxxx-xxx-md.md)]

Captures diagnostic data and health information about [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] to detect potential failures. The procedure runs in repeat mode and sends results periodically. It can be invoked from either a regular or a DAC connection.  
  
**Applies to**: [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] ( [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)]).  
  
![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
sp_server_diagnostics [@repeat_interval =] 'repeat_interval_in_seconds'   
```  
  
## Arguments  
`[ @repeat_interval = ] 'repeat_interval_in_seconds'`
 Indicates the time interval at which the stored procedure will run repeatedly to send health information.  
  
 *repeat_interval_in_seconds* is **int** with the default of 0. The valid parameter values are 0, or any value equal to or more than 5. The stored procedure has to run at least 5 seconds to return complete data. The minimum value for the stored procedure to run in the repeat mode is 5 seconds.  
  
 If this parameter is not specified, or if the specified value is 0, the stored procedure will return data one time and then exit.  
  
 If the specified value is less than the minimum value, it will raise an error and return nothing.  
  
 If the specified value is equal to or more than 5, the stored procedure runs repeatedly to return the health state until it is manually canceled.  
  
## Return Code Values  
0 (success) or 1 (failure)  
  
## Result Sets  
**sp_server_diagnostics** returns the following information  
  
|Column|Data type|Description|  
|------------|---------------|-----------------|  
|**creation_time**|**datetime**|Indicates the time stamp of row creation. Each row in a single rowset has the same time stamp.|  
|**component_type**|**sysname**|Indicates whether the row contains information for the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance level component or for an Always On availability group:<br /><br /> instance<br /><br /> Always On:AvailabilityGroup|  
|**component_name**|**sysname**|Indicates the name of component or the name of the availability group:<br /><br /> system<br /><br /> resource<br /><br /> query_processing<br /><br /> io_subsystem<br /><br /> events<br /><br /> *\<name of the availability group>*|  
|**state**|**int**|Indicates the health status of the component:<br /><br /> 0<br /><br /> 1<br /><br /> 2<br /><br /> 3|  
|**state_desc**|**sysname**|Describes the state column. Descriptions that correspond to the values in the state column are:<br /><br /> 0: Unknown<br /><br /> 1: clean<br /><br /> 2: warning<br /><br /> 3: error|  
|**data**|**varchar (max)**|Specifies data that is specific to the component.|  
  
 Here are the descriptions of the five components:  
  
-   **system**: Collects data from a system perspective on spinlocks, severe processing conditions, non-yielding tasks, page faults, and CPU usage. This information is produces an overall health state recommendation.  
  
-   **resource**:  Collects data from a resource perspective on physical and virtual memory, buffer pools, pages, cache and other memory objects. This information produces an overall health state recommendation.  
  
-   **query_processing**: Collects data from a query-processing perspective on the worker threads, tasks, wait types, CPU intensive sessions, and blocking tasks. This information produces an overall health state recommendation.  
  
-   **io_subsystem**: Collects data on IO. In addition to diagnostic data, this component produces a clean healthy or warning health state only for an IO subsystem.  
  
-   **events**: Collects data and surfaces through the stored procedure on the errors and events of interest recorded by the server, including details about ring buffer exceptions, ring buffer events about memory broker, out of memory, scheduler monitor, buffer pool, spinlocks, security, and connectivity . Events will always show 0 as the state.  
  
-   **\<name of the availability group>**: Collects data for the specified availability group (if component_type = "Always On:AvailabilityGroup").  
  
## Remarks  
From a failure perspective, the system, resource, and query_processing components will be leveraged for failure detection while the io_subsystem and events components will be leveraged for diagnostic purposes only.  
  
The following table maps the components to their associated health states.  
  
|Components|Clean (1)|Warning (2)|Error (3)|Unknowns (0)|  
|----------------|-----------------|-------------------|-----------------|--------------------|  
|system|x|x|x||  
|resource|x|x|x||  
|query_processing|x|x|x||  
|io_subsystem|x|x|||  
|events||||x|  
  
The (x) in each row represents valid health states for the component. For example, io_subsystem will either show as clean or warning. It will not show the error states.  
 
> [!NOTE]
> Execution of sp_server_diagnostics internal procedure is implemented on a preemptive thread at high priority.
  
## Permissions  
Requires VIEW SERVER STATE permission on the server.  
  
## Examples  
It is best practice to use the extended sessions to capture the health information and save it to a file that is located outside of SQL Server. Therefore, you can still access it if there is a failure. The following example saves the output from an event session to a file:  
```sql  
CREATE EVENT SESSION [diag]  
ON SERVER  
           ADD EVENT [sp_server_diagnostics_component_result] (set collect_data=1)  
           ADD TARGET [asynchronous_file_target] (set filename='c:\temp\diag.xel');  
GO  
ALTER EVENT SESSION [diag]  
      ON SERVER STATE = start;  
GO  
```  
  
The example query below reads the extended session log file:  
```sql  
SELECT  
    xml_data.value('(/event/@name)[1]','varchar(max)') AS Name  
  , xml_data.value('(/event/@package)[1]', 'varchar(max)') AS Package  
  , xml_data.value('(/event/@timestamp)[1]', 'datetime') AS 'Time'  
  , xml_data.value('(/event/data[@name=''component_type'']/value)[1]','sysname') AS Sysname  
  , xml_data.value('(/event/data[@name=''component_name'']/value)[1]','sysname') AS Component  
  , xml_data.value('(/event/data[@name=''state'']/value)[1]','int') AS State  
  , xml_data.value('(/event/data[@name=''state_desc'']/value)[1]','sysname') AS State_desc  
  , xml_data.query('(/event/data[@name="data"]/value/*)') AS Data  
FROM   
(  
      SELECT  
                        object_name as event  
                        ,CONVERT(xml, event_data) as xml_data  
       FROM    
      sys.fn_xe_file_target_read_file('C:\Program Files\Microsoft SQL Server\MSSQL13.MSSQLSERVER\MSSQL\Log\*.xel', NULL, NULL, NULL)  
)   
AS XEventData  
ORDER BY time;  
```  
  
The following example captures the output of sp_server_diagnostics to a table in a non-repeat mode:  
```sql  
CREATE TABLE SpServerDiagnosticsResult  
(  
      create_time DateTime,  
      component_type sysname,  
      component_name sysname,  
      state int,  
      state_desc sysname,  
      data xml  
);  
INSERT INTO SpServerDiagnosticsResult  
EXEC sp_server_diagnostics; 
```  

The example query below reads the summary output from the table:  
```sql  
SELECT create_time,
       component_name,
       state_desc 
FROM SpServerDiagnosticsResult;  
``` 

The example query below reads some of the detailed output from the each component in the table:  
```sql  
-- system
select data.value('(/system/@systemCpuUtilization)[1]','bigint') as 'System_CPU',
   data.value('(/system/@sqlCpuUtilization)[1]','bigint') as 'SQL_CPU',
   data.value('(/system/@nonYieldingTasksReported)[1]','bigint') as 'NonYielding_Tasks',
   data.value('(/system/@pageFaults)[1]','bigint') as 'Page_Faults',
   data.value('(/system/@latchWarnings)[1]','bigint') as 'Latch_Warnings',
   data.value('(/system/@BadPagesDetected)[1]','bigint') as 'BadPages_Detected',
   data.value('(/system/@BadPagesFixed)[1]','bigint') as 'BadPages_Fixed'
from SpServerDiagnosticsResult 
where component_name like 'system'
go

-- Resource Monitor
select data.value('(./Record/ResourceMonitor/Notification)[1]', 'VARCHAR(max)') AS [Notification],
	data.value('(/resource/memoryReport/entry[@description=''Working Set'']/@value)[1]', 'bigint')/1024 AS [SQL_Mem_in_use_MB],
	data.value('(/resource/memoryReport/entry[@description=''Available Paging File'']/@value)[1]', 'bigint')/1024 AS [Avail_Pagefile_MB],
	data.value('(/resource/memoryReport/entry[@description=''Available Physical Memory'']/@value)[1]', 'bigint')/1024 AS [Avail_Physical_Mem_MB],
	data.value('(/resource/memoryReport/entry[@description=''Available Virtual Memory'']/@value)[1]', 'bigint')/1024 AS [Avail_VAS_MB],
	data.value('(/resource/@lastNotification)[1]','varchar(100)') as 'LastNotification',
	data.value('(/resource/@outOfMemoryExceptions)[1]','bigint') as 'OOM_Exceptions'
from SpServerDiagnosticsResult 
where component_name like 'resource'
go

-- Nonpreemptive waits
select waits.evt.value('(@waitType)','varchar(100)') as 'Wait_Type',
   waits.evt.value('(@waits)','bigint') as 'Waits',
   waits.evt.value('(@averageWaitTime)','bigint') as 'Avg_Wait_Time',
   waits.evt.value('(@maxWaitTime)','bigint') as 'Max_Wait_Time'
from SpServerDiagnosticsResult 
	CROSS APPLY data.nodes('/queryProcessing/topWaits/nonPreemptive/byDuration/wait') AS waits(evt)
where component_name like 'query_processing'
go

-- Preemptive waits
select waits.evt.value('(@waitType)','varchar(100)') as 'Wait_Type',
   waits.evt.value('(@waits)','bigint') as 'Waits',
   waits.evt.value('(@averageWaitTime)','bigint') as 'Avg_Wait_Time',
   waits.evt.value('(@maxWaitTime)','bigint') as 'Max_Wait_Time'
from SpServerDiagnosticsResult 
	CROSS APPLY data.nodes('/queryProcessing/topWaits/preemptive/byDuration/wait') AS waits(evt)
where component_name like 'query_processing'
go

-- CPU intensive requests
select cpureq.evt.value('(@sessionId)','bigint') as 'SessionID',
   cpureq.evt.value('(@command)','varchar(100)') as 'Command',
   cpureq.evt.value('(@cpuUtilization)','bigint') as 'CPU_Utilization',
   cpureq.evt.value('(@cpuTimeMs)','bigint') as 'CPU_Time_ms'
from SpServerDiagnosticsResult 
	CROSS APPLY data.nodes('/queryProcessing/cpuIntensiveRequests/request') AS cpureq(evt)
where component_name like 'query_processing'
go

-- Blocked Process Report
select blk.evt.query('.') as 'Blocked_Process_Report_XML'
from SpServerDiagnosticsResult 
	CROSS APPLY data.nodes('/queryProcessing/blockingTasks/blocked-process-report') AS blk(evt)
where component_name like 'query_processing'
go

-- IO
select data.value('(/ioSubsystem/@ioLatchTimeouts)[1]','bigint') as 'Latch_Timeouts',
   data.value('(/ioSubsystem/@totalLongIos)[1]','bigint') as 'Total_Long_IOs'
from SpServerDiagnosticsResult 
where component_name like 'io_subsystem'
go

-- Event information
select xevts.evt.value('(@name)','varchar(100)') as 'xEvent_Name',
   xevts.evt.value('(@package)','varchar(100)') as 'Package',
   xevts.evt.value('(@timestamp)','datetime') as 'xEvent_Time',
   xevts.evt.query('.') as 'Event Data'
from SpServerDiagnosticsResult 
	CROSS APPLY data.nodes('/events/session/RingBufferTarget/event') AS xevts(evt)
where component_name like 'events'
go  
``` 
  
## See Also  
 [Failover Policy for Failover Cluster Instances](../../sql-server/failover-clusters/windows/failover-policy-for-failover-cluster-instances.md)  
  
  
