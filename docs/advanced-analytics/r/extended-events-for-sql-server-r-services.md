---
title: "Extended Events for SQL Server R Services | Microsoft Docs"
ms.custom: 
  - "SQL2016_New_Updated"
ms.date: "11/29/2016"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "r-services"
ms.tgt_pltfrm: ""
ms.topic: "article"
ms.assetid: 4e90e057-aacb-4adc-8da6-64861f4e87df
caps.latest.revision: 13
author: "jeannt"
ms.author: "jeannt"
manager: "jhubbard"
---
# Extended Events for SQL Server R Services
  [!INCLUDE[rsql_productname](../../includes/rsql-productname-md.md)] provides a set of extended events to use in troubleshooting operations related to the [!INCLUDE[rsql_launchpad](../../includes/rsql-launchpad-md.md)] or R jobs sent to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
 To view a list of events related to SQL Server, run the following query from [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)].  
  
```  
select o.name as event_name, o.description  
  from sys.dm_xe_objects o  
  join sys.dm_xe_packages p  
    on o.package_guid = p.guid  
 where o.object_type = 'event'  
   and p.name = 'SQLSatellite';  
```  
  
 However, some additional extended events for [!INCLUDE[rsql_productname](../../includes/rsql-productname-md.md)] are fired only from external processes, such as the [!INCLUDE[rsql_launchpad](../../includes/rsql-launchpad-md.md)], and BXLServer, the satellite process that starts the R runtime. For more information about how to capture these events, see [Collecting Events from External Processes](#bkmk_externalevents).  
  
 For general information about using extended events, see [SQL Server Extended Events Sessions](../../relational-databases/extended-events/sql-server-extended-events-sessions.md).  

  
##  <a name="bkmk_xeventtable"></a> Table of  Extended Events  
  
|Event|Description|Use|  
|-----------|-----------------|---------|  
|connection_accept|Occurs when a new connection is accepted. This event serves to log all connection attempts.||  
|failed_launching|Launching failed.|Indicates an error.|  
|satellite_abort_connection|Abort connection record||  
|satellite_abort_received|Fires when an abort message is received over a satellite connection.||  
|satellite_abort_sent|Fires when an abort message is sent over satellite connection.||  
|satellite_authentication_completion|Fires when authentication completes for a connection over TCP or Namedpipe.||  
|satellite_authorization_completion|Fires when authorization completes for a connection over TCP or Namedpipe.||  
|satellite_cleanup|Fires when satellite calls cleanup.|Fired only from external process. See instructions on collecting events from external processes.|  
|satellite_data_chunk_sent|Fires when the satellite connection finishes sending a single data chunk.|The event reports the number of rows sent, the number of columns, the number of SNI packets usedm and time elapsed in milliseconds while sending the chunk. The information can help you understand how much time is spent passing different types of data, and how many packets are used.|  
|satellite_data_receive_completion|Fires when all the required data by a query is received over the satellite connection.|Fired only from external process. See instructions on collecting events from external processes.|  
|satellite_data_send_completion|Fires when all required data for a session is sent over the satellite connection.||  
|satellite_data_send_start|Fires when data transmission starts (just before first data chunk is sent).||  
|satellite_error|Used for tracing sql satellite error||  
|satellite_invalid_sized_message|Message's size is not valid||  
|satellite_message_coalesced|Used for tracing message coalescing at networking layer||  
|satellite_message_ring_buffer_record|message ring buffer record||  
|satellite_message_summary|summary information about messaging||  
|satellite_message_version_mismatch|Message's version field is not matched||  
|satellite_messaging|Used for tracing messaging event (bind, unbind etc)||  
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
|satellite_sessionId_mismatch|Message's session id is not expected||  
  
###  <a name="bkmk_externalevents"></a> Collecting Events from External Processes  
 [!INCLUDE[rsql_productname](../../includes/rsql-productname-md.md)] starts  some services that run outside of the SQL Server process. To capture events related to these external processes, you must create an events trace  configuration file and place the file in the same directory as the executable for the process.  
  
-   **[!INCLUDE[rsql_launchpad](../../includes/rsql-launchpad-md.md)]**   
  
     To capture events related to the Launchpad, place the *.config* file in the Binn directory for the SQL Server instance.  In a default installation, this would be:
      `C:\Program Files\Microsoft SQL Server\MSSQL_version_number.MSSQLSERVER\MSSQL\Binn`.  
  
-   **BXLServer** is the satellite process that supports SQL extensibility with R and other external script languages.  
  
     To capture events related to BXLServer, place the *.config* file in the R installation directory.  In a default installation, this would be:
      `C:\Program Files\Microsoft SQL Server\MSSQL_version_number.MSSQLSERVER\R_SERVICES\library\RevoScaleR\rxLibs\x64`.  
  
> [!IMPORTANT]
>   The configuration file must be named the same as the executable, using the format “[name].xevents.xml”. In other words, the files must be named as follows:  
>   
> - Launchpad.xevents.xml  
> - bxlserver.xevents.xml  
  
 The configuration file itself has the following format:  
  
```  
\<?xml version="1.0" encoding="utf-8"?>  
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
  
 **Notes:**  
  
-   To configure the trace, edit the *session name* placeholder, the placeholder for the filename (`[SessionName].xel`), and the names of the events you want to capture (such as `[XEvent Name 1]`, `[XEvent Name 1]`).  
  
-   Any number of `event package` tags may appear, and will be collected as long as the name attribute is correct.  
  
### Example: Capturing Launchpad events  
 The following example shows the definition of an event trace for the Launchpad service.  
  
```  
\<?xml version="1.0" encoding="utf-8"?>  
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
  
 **Notes:**  
  
-   Place the *.config* file in the Binn directory for the SQL Server instance.  
  
-   This file must be named *Launchpad.xevents.xml*.  
  
### Example: Capturing BXLServer events  
 The following example shows the definition of an event trace for the BXLServer executable.  
  
```  
\<?xml version="1.0" encoding="utf-8"?>  
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
  
 **Notes:**  
  
-   Place the *.config* file in the same directory as the BXLServer executable.  
  
-   This file must be named *bxlserver.xevents.xml*.  
  
## See Also
[Custom Management Studio Reports for R Services](../../advanced-analytics/r-services/monitor-r-services-using-custom-reports-in-management-studio.md)  
 [SQL Server R Services](../../advanced-analytics/r-services/sql-server-r-services.md)   
 [Managing and Monitoring R Solutions](../../advanced-analytics/r-services/managing-and-monitoring-r-solutions.md)  
  
  
