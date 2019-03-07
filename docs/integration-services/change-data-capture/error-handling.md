---
title: "Error Handling | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
ms.assetid: ff79e19d-afca-42a4-81b0-62d759380d11
author: "douglaslMS"
ms.author: "douglasl"
manager: craigg
---
# Error Handling
  An Oracle CDC Instance mines changes from a single Oracle source database (an Oracle RAC cluster is considered a single database) and writes the committed changes to change tables in a CDC database in the target [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance.  
  
 A CDC Instance maintains its state in a system table called **cdc.xdbcdc_state**. This table can be queried any time to find the state of the CDC Instance. For more information about the cdc.xdbcdc_state table, see [cdc.xdbcdc_state](../../integration-services/change-data-capture/the-oracle-cdc-databases.md#BKMK_cdcxdbcdc_state).  
  
 The table below describes the CDC Instance states in the xdbcdc_state table.  
  
 For each state, the following two indications are shown for the corresponding columns in the cdc.xdbcdc_state table:  
  
-   Instance is not active (there is no Windows process currently handling it). If the **active** column value is 1, a subprocess of the Oracle CDC Service handling this specific Oracle CDC Instance is running.  
  
-   If the **error** column value is 0, the Oracle CDC Instance is not in an error condition. If the **error** column value is 1, there is an error that prevents the Oracle CDC Instance from processing changes.  
  
     If the **error** column has a value of 1 and the **active** column value is also 1, then a recoverable error is occurring for the Oracle CDC Instance, which can be resolved automatically. If the error column has a value of 1 and the active column has a value of 0, then in most cases a manual workaround may be needed to resolve the problem before processing can be resumed.  
  
 The following table describes the various status codes that the Oracle CDC Instance may report in its state table.  
  
|Status|Active Status Code|Error Status Code|Description|Substatus|  
|------------|------------------------|-----------------------|-----------------|---------------|  
|ABORTED|0|1|The Oracle CDC Instance is not running. The ABORTED substatus indicates that the Oracle CDC Instance was ACTIVE and then has stopped unexpectedly.|The ABORTED substatus is established by the Oracle CDC Service main instance when it detects that the Oracle CDC Instance is not running while its status is ACTIVE.|  
|ERROR|0|1|The Oracle CDC Instance is not running. The ERROR status indicates that the CDC instance was ACTIVE but then encountered an error that is not recoverable and disabled itself.|MISCONFIGURED: An unrecoverable configuration error was detected.<br /><br /> PASSWORD-REQUIRED: There is no password set for the Change Data Capture Designer for Oracle by Attunity or the configured password is not valid. This can be because of a change to the service asymmetric key password.|  
|RUNNING|1|0|The CDC instance is running and is processing change records.|IDLE: All change records were processed and stored into the target control (**_CT**) tables. There is no active transaction with the control tables.<br /><br /> PROCESSING: There are change records being processed that are not yet written to the control (**_CT**) tables.|  
|STOPPED|0|0|The CDC instance is not running.|The STOP substatus indicates that the CDC instance was ACTIVE and then was stopped correctly.|  
|SUSPENDED|1|1|The CDC instance is running but processing is suspended due to a recoverable error.|DISCONNECTED: The connection with the source Oracle database cannot be established. Processing will resume once connection is restored.<br /><br /> STORAGE: The storage is full. Processing will resume when storage becomes available. In some cases, this status may not appear because the status table cannot be updated.<br /><br /> LOGGER: The logger is connected to Oracle but it cannot read the Oracle transaction logs because of a temporary problem.|  
|DATAERROR|x|x|This status code is only used for the **xdbcdc_trace** table. It does not appear in the **xdbcdc_state** table. Trace records with this status indicate a problem with an Oracle log record. The bad log record is stored in the **data** column as a BLOB.|BADRECORD: The attached log record could not be parsed.<br /><br /> CONVERT-ERROR: The data in some columns could not be converted to the target columns in the capture table. This status may appear only if the configuration specifies that conversion errors should produce trace records.|  
  
 Because the Oracle CDC Service state is stored in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], there may be cases where the state value in the database might not reflect the actual state of the service. The most common scenario is when the service loses its connection to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and cannot resume it (for any reason). In that case, the state stored in **cdc.xdbcdc_state** becomes stale. If the last update timestamp (UTC) is more than a minute old, the state is probably stale. In this case, use the Windows Event Viewer to find additional information about the status of the service.  
  
## Error Handling  
 This section describes how the Oracle CDC Service handles errors.  
  
### Logging  
 The Oracle CDC Service creates error information in one of the following places.  
  
-   The Windows event log, which is used with logging errors and to indicate Oracle CDC Service life cycle events (starting, stopping, (re)connection to the target [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance).  
  
-   The MSXDBCDC.dbo.xdbcdc_trace table, which is used for general logging and tracing by the Oracle CDC Service main process.  
  
-   The \<cdc-database>.cdc.xdbcdc_trace table, which is used for general logging and tracing by Oracle CDC Instances. This means that errors related to a specific Oracle CDC Instance are logged to that instance's trace table.  
  
 Information is logged by the Oracle CDC service when the service:  
  
-   Is started or stopped by the service control manager.  
  
-   Cannot connect to the associated [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance and when it successfully establishes a connection following a failure.  
  
-   Encounters an error starting Oracle CDC Service instances.  
  
-   Detects that an Oracle CDC Instance has aborted.  
  
-   Encounters an unexpected error.  
  
 Information is logged by the CDC instance when the instance:  
  
-   Is enabled or disabled.  
  
-   Encounters an error.  
  
-   Recovers from a recoverable error.  
  
 The trace table is also used for recording detailed trace information for troubleshooting.  
  
### Handling Source Oracle Connection Errors  
 The Oracle CDC Service needs a persistent connection with the source Oracle database. Many connection errors that are unrelated to the service configuration (such as networking errors) are considered transient. Therefore, if the Oracle CDC Service cannot establish connection with the Oracle database (either on start or during work following a disconnection), the service changes its state to SUSPENDED/DISCONNECTED and enters a retry loop where the connection is retried at regular intervals. When the connection is re-established, processing continues.  
  
 Other types of connection errors are not transient (for example, bad credentials, insufficient privileges, and bad database address). When these errors occur, the Oracle CDC Service state is set to ERROR/MISCONFIGURED or to ERROR/PASSWORD-REQUIRED and the service is disabled. When the user fixes the underlying error, the Oracle CDC Instance should be manually re-enabled for processing to resume.  
  
 When it is not clear whether the error is transient, it is best to assume it is transient.  
  
### Handling Target SQL Server Connection Errors  
 The Oracle CDC Service needs a persistent connection to the target [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance. This connection is used to:  
  
-   Ensure that there are no other services by the same name currently working with this [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance.  
  
-   Check which Oracle CDC Instance is enabled or disabled and start (or stop) its subprocess.  
  
 When the service establishes a connection with the target [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance and verifies that it is the only Oracle CDC service by this name that is working, it can check which Oracle CDC instances are enabled and start their handling processes (afterward the service stops these processes when they are disabled). The Oracle CDC instances use their [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] connections to work with the CDC database of the Oracle CDC instance.  
  
 How errors are handled when the connection to the target [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] fails depends on whether the errors are transient.  
  
 For known non-transient errors (such as bad credentials, insufficient privileges, bad connection information), the service logs an error to the Windows event log and stops (it cannot write to the trace table because it cannot connect to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]). In this case, the user must resolve the error and restart the Oracle CDC Windows service.  
  
 For transient errors and unexpected errors, the operation is retried several times and if the failure persists for a significant time period, the CDC Service stops its CDC Instance subprocesses and goes back to its initial connection attempt (by this time, an Oracle CDC Service on another machine may have already taken control of the named CDC service).  
  
### Handling Target SQL Server Storage Full Errors  
 When the Oracle CDC Service detects that it cannot insert new change data into the target [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] CDC database, it writes a warning to the event log and tries to insert a trace record (although that may fail for the same reason). It then retries the operation in a specific interval until it is successful.  
  
### Handling Oracle CDC Errors  
 The Oracle CDC Instance reads the Oracle transaction log and processes it. If the CDC processing encounters an error, it is reported in the **cdc.xdbcdc_state** table and the user needs to manually intervene based on the reported error.  
  
 For example, the Oracle CDC Instance may not be active for an extended duration and the required Oracle transaction log files are no longer available. In this case, the Oracle DBA must restore the required logs for the Oracle CDC Instance to resume processing.  
  
### Handling Unexpected Oracle CDC Instance Failures  
 The Oracle CDC Service monitors its CDC Instance subprocesses. When a CDC Instance subprocess aborts, the CDC Service disables it in the MSXDBCDC.dbo.xdbcdc_databases table and updates its cdc.xdbcdc_state status to ABORTED. In this case, the standard Windows Error Reporting dialog box is used to report this error for further analysis.  
  
## See Also  
 [Change Data Capture Designer for Oracle by Attunity](../../integration-services/change-data-capture/change-data-capture-designer-for-oracle-by-attunity.md)   
 [The Oracle CDC Instance](../../integration-services/change-data-capture/the-oracle-cdc-instance.md)  
  
  
