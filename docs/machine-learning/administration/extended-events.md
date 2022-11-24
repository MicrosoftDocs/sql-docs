---
title: Monitor scripts with extended events
description: Learn how to use extended events to monitor and troubleshooting operations related to the SQL Server Machine Learning Services, SQL Server Launchpad, and Python or R jobs external scripts.
ms.service: sql
ms.subservice: machine-learning-services
ms.date: 03/04/2020
ms.topic: how-to
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.custom: seo-lt-2019
monikerRange: ">=sql-server-2016||>=sql-server-linux-ver15||=azuresqldb-mi-current"
---
# Monitor Python and R scripts with extended events in SQL Server Machine Learning Services
[!INCLUDE [SQL Server 2016 SQL MI](../../includes/applies-to-version/sqlserver2016-asdbmi.md)]

Learn how to use extended events to monitor and troubleshooting operations related to the SQL Server Machine Learning Services, SQL Server Launchpad, and Python or R jobs external scripts.

## Extended events for SQL Server Machine Learning Services

To view a list of events related to SQL Server Machine Learning Services, run the following query from Azure Data Studio or SQL Server Management Studio.

```sql
SELECT o.name AS event_name, o.description
FROM sys.dm_xe_objects o
JOIN sys.dm_xe_packages p
ON o.package_guid = p.guid
WHERE o.object_type = 'event'
AND p.name = 'SQLSatellite';
```

For more information about how to use extended events, see [Extended Events Tools](../../relational-databases/extended-events/extended-events-tools.md).

## Additional events specific to Machine Learning Services

Additional extended events are available for components that are related to and used by SQL Server Machine Learning Services, such as the [!INCLUDE[rsql_launchpad](../../includes/rsql-launchpad-md.md)], and BXLServer, and the satellite process that starts the Python or R runtime. These additional extended events are fired from the external processes; therefore, they must be captured using an external utility.

For more information about how to do this, see the section, [Collecting events from external processes](#bkmk_externalevents).

<a name="bkmk_xeventtable"></a> 

## Table of extended events

|Event|Description|Notes|  
|-----------|-----------------|---------|  
|connection_accept|Occurs when a new connection is accepted. This event serves to log all connection attempts.||  
|failed_launching|Launching failed.|Indicates an error.|  
|satellite_abort_connection|Abort connection record||  
|satellite_abort_received|Fires when an abort message is received over a satellite connection.||  
|satellite_abort_sent|Fires when an abort message is sent over satellite connection.||  
|satellite_authentication_completion|Fires when authentication completes for a connection over TCP or Named pipe.||  
|satellite_authorization_completion|Fires when authorization completes for a connection over TCP or Named pipe.||  
|satellite_cleanup|Fires when satellite calls cleanup.|Fired only from external process. See instructions on collecting events from external processes.|  
|satellite_data_chunk_sent|Fires when the satellite connection finishes sending a single data chunk.|The event reports the number of rows sent, the number of columns, the number of SNI packets used and time elapsed in milliseconds while sending the chunk. The information can help you understand how much time is spent passing different types of data, and how many packets are used.|  
|satellite_data_receive_completion|Fires when all the required data by a query is received over the satellite connection.|Fired only from external process. See instructions on collecting events from external processes.|  
|satellite_data_send_completion|Fires when all required data for a session is sent over the satellite connection.||  
|satellite_data_send_start|Fires when data transmission starts.| Data transmission starts just before the first data chunk is sent.|  
|satellite_error|Used for tracing sql satellite error||  
|satellite_invalid_sized_message|Message's size is not valid||  
|satellite_message_coalesced|Used for tracing message coalescing at networking layer||  
|satellite_message_ring_buffer_record|message ring buffer record||  
|satellite_message_summary|summary information about messaging||  
|satellite_message_version_mismatch|Message's version field is not matched||  
|satellite_messaging|Used for tracing messaging event (bind, unbind, etc.)||  
|satellite_partial_message|Used for tracing partial message at networking layer||  
|satellite_schema_received|Fires when schema message is received and read by SQL.||  
|satellite_schema_sent|Fires when schema message is sent by the satellite.|Fired only from external process. See instructions on collecting events from external processes.|  
|satellite_service_start_posted|Fires when service start message is posted to launchpad.|This tells Launchpad to start the external process, and contains an ID for the new session.|  
|satellite_unexpected_message_received|Fires when an unexpected message is received.|Indicates an error.|  
|stack_trace|Occurs when a memory dump of the process is requested.|Indicates an error.|  
|trace_event|Used for tracing purposes|These events can contain SQL Server, Launchpad, and external process trace messages. This includes output to stdout and stderr from R.|  
|launchpad_launch_start|Fires when launchpad starts launching a satellite.|Fired only from Launchpad. See instructions on collecting events from launchpad.exe.|  
|launchpad_resume_sent|Fires when launchpad has launched the satellite and sent a resume message to SQL Server.|Fired only from Launchpad. See instructions on collecting events from launchpad.exe.|  
|satellite_data_chunk_sent|Fires when the satellite connection finishes sending a single data chunk.|Contains information about the number of columns, number of rows, number of packets, and time elapsed sending the chunk.|  
|satellite_sessionId_mismatch|Message's session ID is not expected||  

<a name="bkmk_externalevents"></a>

### Collecting events from external processes

SQL Server Machine Learning Services starts some services that run outside of the SQL Server process. To capture events related to these external processes, you must create an events trace configuration file and place the file in the same directory as the executable for the process.  

::: moniker range=">=sql-server-ver15||>=sql-server-linux-ver15"
> [!IMPORTANT]
> From SQL Server 2019, the isolation mechanism has changed. Therefore you need to give appropriate permissions to the directory where the events trace configuration file is stored. For more information on how to set these permissions, see [the File permissions section in SQL Server 2019 on Windows: Isolation changes for Machine Learning Services](../install/sql-server-machine-learning-services-2019.md#file-permissions).
::: moniker-end

+ **[!INCLUDE[rsql_launchpad](../../includes/rsql-launchpad-md.md)]**   
  
    To capture events related to the Launchpad, place the *.xml* file in the Binn directory for the SQL Server instance. In a default installation, this would be:

    `C:\Program Files\Microsoft SQL Server\MSSQL_version_number.MSSQLSERVER\MSSQL\Binn`.  
  
+ **BXLServer** is the satellite process that supports SQL extensibility with external script languages, such as R or Python. A separate instance of BxlServer is launched for each external language instance.
  
    To capture events related to BXLServer, place the *.xml* file in the R or Python installation directory. In a default installation, this would be:
     
    **R:** `C:\Program Files\Microsoft SQL Server\MSSQL_version_number.MSSQLSERVER\R_SERVICES\library\RevoScaleR\rxLibs\x64`.  

    **Python:** `C:\Program Files\Microsoft SQL Server\MSSQL_version_number.MSSQLSERVER\PYTHON_SERVICES\Lib\site-packages\revoscalepy\rxLibs`.

The configuration file must be named the same as the executable, using the format "[name].xevents.xml". In other words, the files must be named as follows:

+ `Launchpad.xevents.xml`
+ `bxlserver.xevents.xml`

The configuration file itself has the following format:

```xml
<?xml version="1.0" encoding="utf-8"?>  
<event_sessions>  
<event_session name="[session name]" maxMemory="1" dispatchLatency="1" MaxDispatchLatency="2 SECONDS">  
    <description owner="you">Xevent for launchpad or bxl server.</description>  
    <event package="SQLSatellite" name="[XEvent Name 1]" />  
    <event package="SQLSatellite" name="[XEvent Name 2]" />  
    <target package="package0" name="event_file">  
      <parameter name="filename" value="[SessionName].xel" />  
      <parameter name="max_file_size" value="10" />  
      <parameter name="max_rollover_files" value="10" />  
    </target>  
  </event_session>  
</event_sessions>  
```

+ To configure the trace, edit the *session name* placeholder, the placeholder for the filename (`[SessionName].xel`), and the names of the events you want to capture, For example, `[XEvent Name 1]`, `[XEvent Name 1]`).  
+ Any number of event package tags may appear, and will be collected as long as the name attribute is correct.

### Example: Capturing Launchpad events

The following example shows the definition of an event trace for the Launchpad service:

```xml
<?xml version="1.0" encoding="utf-8"?>  
<event_sessions>  
<event_session name="sqlsatelliteut" maxMemory="1" dispatchLatency="1" MaxDispatchLatency="2 SECONDS">  
    <description owner="hay">Xevent for sql tdd runner.</description>  
    <event package="SQLSatellite" name="launchpad_launch_start" />  
    <event package="SQLSatellite" name="launchpad_resume_sent" />  
    <target package="package0" name="event_file">  
      <parameter name="filename" value="launchpad_session.xel" />  
      <parameter name="max_file_size" value="10" />  
      <parameter name="max_rollover_files" value="10" />  
    </target>  
  </event_session>  
</event_sessions>  
```

+ Place the *.xml* file in the Binn directory for the SQL Server instance.
+ This file must be named `Launchpad.xevents.xml`.

### Example: Capturing BXLServer events  

The following example shows the definition of an event trace for the BXLServer executable.
  
```xml
<?xml version="1.0" encoding="utf-8"?>  
<event_sessions>  
 <event_session name="sqlsatelliteut" maxMemory="1" dispatchLatency="1" MaxDispatchLatency="2 SECONDS">  
    <description owner="hay">Xevent for sql tdd runner.</description>  
    <event package="SQLSatellite" name="satellite_abort_received" />  
    <event package="SQLSatellite" name="satellite_authentication_completion" />  
    <event package="SQLSatellite" name="satellite_cleanup" />  
    <event package="SQLSatellite" name="satellite_data_receive_completion" />  
    <event package="SQLSatellite" name="satellite_data_send_completion" />  
    <event package="SQLSatellite" name="satellite_data_send_start" />  
    <event package="SQLSatellite" name="satellite_schema_sent" />   
    <event package="SQLSatellite" name="satellite_unexpected_message_received" />    
    <event package="SQLSatellite" name="satellite_data_chunk_sent" />   
    <target package="package0" name="event_file">  
      <parameter name="filename" value="satellite_session.xel" />  
      <parameter name="max_file_size" value="10" />  
      <parameter name="max_rollover_files" value="10" />  
    </target>  
  </event_session>  
</event_sessions>  
```

+ Place the *.xml* file in the same directory as the BXLServer executable.
+ This file must be named `bxlserver.xevents.xml`.

## Next steps

- [Monitor Python and R script execution using custom reports in SQL Server Management Studio](../../machine-learning/administration/monitor-sql-server-machine-learning-services-using-custom-reports-management-studio.md)
- [Monitor SQL Server Machine Learning Services using dynamic management views (DMVs)](../../machine-learning/administration/monitor-sql-server-machine-learning-services-using-dynamic-management-views.md)