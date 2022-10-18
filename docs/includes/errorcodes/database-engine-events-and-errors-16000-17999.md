---
author: MashaMSFT
ms.author: mathoma
ms.reviewer: randolphwest
ms.date: 08/19/2022
ms.topic: include
---
| Error| Severity | Event Logged | Description|
| :------ | :------| :------| :----------------------------- |
|    [16591](../../relational-databases/errors-events/mssqlserver-16591-database-engine-error.md)    |    16    |    No    |    Multiple logical file paths limit has been reached. Statement contains %ld logical file paths, maximum allowed limit is %d.    |
|    [16594](../../relational-databases/errors-events/mssqlserver-16594-database-engine-error.md)    |    16    |    No    |    Path '%ls' was referenced multiple times in result set '%ls'.    |
|    16901    |    16    |    No    |    %hs: This feature has not been implemented yet.    |
|    16902    |    16    |    No    |    %ls: The value of the parameter %ls is invalid.    |
|    16903    |    16    |    No    |    The "%ls" procedure was called with an incorrect number of parameters.    |
|    16904    |    16    |    No    |    sp_cursor: optype: You can only specify ABSOLUTE in conjunction with DELETE or UPDATE.    |
|    16905    |    16    |    No    |    The cursor is already open.    |
|    16906    |    17    |    No    |    Temporary storage used by the cursor to store large object variable values referred by the cursor query is not usable any more.    |
|    16907    |    16    |    No    |    %hs is not allowed in cursor statements.    |
|    16909    |    16    |    No    |    %ls: The cursor identifier value provided (%x) is not valid.    |
|    16910    |    16    |    No    |    The cursor %.*ls is currently used by another statement.    |
|    16911    |    16    |    No    |    %hs: The fetch type %hs cannot be used with forward only cursors.    |
|    16914    |    16    |    No    |    The "%ls" procedure was called with too many parameters.    |
|    16915    |    16    |    No    |    A cursor with the name '%.*ls' already exists.    |
|    16916    |    16    |    No    |    A cursor with the name '%.*ls' does not exist.    |
|    16917    |    16    |    No    |    Cursor is not open.    |
|    16922    |    16    |    No    |    Cursor Fetch: Implicit conversion from data type %s to %s is not allowed.    |
|    16924    |    16    |    No    |    Cursorfetch: The number of variables declared in the INTO list must match that of selected columns.    |
|    16925    |    16    |    No    |    The fetch type %hs cannot be used with dynamic cursors.    |
|    16926    |    16    |    No    |    sp_cursoroption: The column ID (%d) does not correspond to a text, ntext, or image column.    |
|    16927    |    16    |    No    |    Cannot fetch into text, ntext, and image variables.    |
|    16928    |    16    |    No    |    sp_cursor: Exec statement is not allowed as source for cursor insert.    |
|    16929    |    16    |    No    |    The cursor is READ ONLY.    |
|    16930    |    16    |    No    |    The requested row is not in the fetch buffer.    |
|    16931    |    16    |    No    |    There are no rows in the current fetch buffer.    |
|    16932    |    16    |    No    |    The cursor has a FOR UPDATE list and the requested column to be updated is not in this list.    |
|    16933    |    16    |    No    |    The cursor does not include the table being modified or the table is not updatable through the cursor.    |
|    16934    |    10    |    No    |    Optimistic concurrency check failed. The row was modified outside of this cursor.    |
|    16935    |    16    |    No    |    No parameter values were specified for the sp_cursor-%hs statement.    |
|    16936    |    16    |    No    |    sp_cursor: One or more values parameters were invalid.    |
|    16937    |    16    |    No    |    A server cursor cannot be opened on the given statement or statements. Use a default result set or client cursor.    |
|    16938    |    16    |    No    |    sp_cursoropen/sp_cursorprepare: The statement parameter can only be a batch or a stored procedure with a single select, without FOR BROWSE, COMPUTE BY, or variable assignments.    |
|    16941    |    16    |    No    |    Cursor updates are not allowed on tables opened with the NOLOCK option.    |
|    16942    |    16    |    No    |    Could not generate asynchronous keyset. The cursor has been deallocated.    |
|    16943    |    16    |    No    |    Could not complete cursor operation because the table schema changed after the cursor was declared.    |
|    16945    |    16    |    No    |    The cursor was not declared.    |
|    16946    |    16    |    No    |    Could not open the cursor because one or more of its tables have gone out of scope.    |
|    16947    |    16    |    No    |    No rows were updated or deleted.    |
|    16948    |    16    |    No    |    The variable '%.*ls' is not a cursor variable, but it is used in a place where a cursor variable is expected.    |
|    16949    |    16    |    No    |    The variable '%.*ls' is a cursor variable, but it is used in a place where a cursor variable is not valid.    |
|    16950    |    10    |    No    |    The variable '%.*ls' does not currently have a cursor allocated to it.    |
|    16951    |    16    |    No    |    The variable '%.*ls' cannot be used as a parameter because a CURSOR OUTPUT parameter must not have a cursor allocated to it before execution of the procedure.    |
|    16952    |    16    |    No    |    A cursor variable cannot be used as a parameter to a remote procedure call.    |
|    16953    |    10    |    No    |    Remote tables are not updatable. Updatable keyset-driven cursors on remote tables require a transaction with the REPEATABLE_READ or SERIALIZABLE isolation level spanning the cursor.    |
|    16954    |    16    |    No    |    Executing SQL directly; no cursor.    |
|    16955    |    16    |    No    |    Could not create an acceptable cursor.    |
|    16956    |    10    |    No    |    The created cursor is not of the requested type.    |
|    16957    |    16    |    No    |    FOR UPDATE cannot be specified on a READ ONLY cursor.    |
|    16958    |    16    |    No    |    Could not complete cursor operation because the set options have changed since the cursor was declared.    |
|    16959    |    16    |    No    |    Unique table computation failed.    |
|    16960    |    16    |    No    |    You have reached the maximum number of cursors allowed.    |
|    16961    |    10    |    No    |    One or more FOR UPDATE columns have been adjusted to the first instance of their table in the query.    |
|    16962    |    16    |    No    |    The target object type is not updatable through a cursor.    |
|    16963    |    16    |    No    |    You cannot specify scroll locking on a cursor that contains a remote table.    |
|    16964    |    16    |    No    |    For the optimistic cursor, timestamp columns are required if the update or delete targets are remote.    |
|    16965    |    16    |    No    |    Cursor scroll locks were invalidated due to a transaction defect. Reissue the UPDATE or DELETE statement after a cursor fetch.    |
|    16966    |    16    |    No    |    %ls: Specified concurrency control option %d (%ls) is incompatible with static or fast forward only cursors. Only read-only is compatible with static or fast forward only cursors.    |
|    16992    |    16    |    No    |    The cursor operation is required to wait for cursor asynchronous population to complete. However, at this point the transaction cannot be yielded to let the asynchronous population to continue.    |
|    16996    |    16    |    No    |    %ls cannot take output parameters.    |
|    16998    |    16    |    No    |    The asynchronous cursor worktable population thread spawn failed.    |
|    16999    |    20    |    Yes    |    Internal Cursor Error: The cursor is in an invalid state.    |
|    17000    |    10    |    No    |    Usage: sp_autostats <table_name> [, {ON\|OFF} [, <index_name>] ]    |
|    17001    |    16    |    Yes    |    Failure to send an event notification instance of type '%s' on conversation handle '%s'. Error Code = '%s'.    |
|    17002    |    16    |    Yes    |    Failed to post QUEUE_ACTIVATION event. Error code: '0x%s'.    |
|    17003    |    16    |    Yes    |    Closed event notification conversation endpoint with handle '%s', due to the following error: '%.*ls'.    |
|    17004    |    16    |    Yes    |    Event notification conversation on dialog handle '%s' closed without an error.    |
|    17005    |    16    |    Yes    |    Event notification '%ls' in database '%ls' dropped due to send time service broker errors. Check to ensure the conversation handle, service broker contract, and service specified in the event notification are active.    |
|    17049    |    16    |    Yes    |    Unable to cycle error log file from '%ls' to '%ls' due to OS error '%s'. A process outside of SQL Server may be preventing SQL Server from reading the files. As a result, errorlog entries may be lost and it may not be possible to view some SQL Server errorlogs. Make sure no other processes have locked the file with write-only access."    |
|    17051    |    16    |    Yes    |    SQL Server evaluation period has expired.    |
|    [17053](../../relational-databases/errors-events/mssqlserver-17053-database-engine-error.md)    |    16    |    Yes    |    %ls: Operating system error %ls encountered.    |
|    17054    |    16    |    Yes    |    The current event was not reported to the Windows Events log. Operating system error = %s. You may need to clear the Windows Events log if it is full.    |
|    17056    |    10    |    Yes    |    The evaluation period for your edition of SQL Server expires in %d day(s).    |
|    17057    |    16    |    Yes    |    Security context for operating system objects could not be created. SQL Server cannot be started. Look for corresponding entries in the event viewer to help diagnose the root cause.    |
|    17058    |    16    |    Yes    |    initerrlog: Could not open error log file '%s'. Operating system error = %s.    |
|    17060    |    10    |    Yes    |    %s    |
|    17061    |    10    |    Yes    |    Error: %d Severity: %d State: %d %s    |
|    17063    |    16    |    Yes    |    Error: %d Severity: %d State: %d %s    |
|    [17065](../../relational-databases/errors-events/mssqlserver-17065-database-engine-error.md)    |    16    |    Yes    |    SQL Server Assertion: File: <%s>, line = %d Failed Assertion = '%s' %s. This error may be timing-related. If the error persists after rerunning the statement, use DBCC CHECKDB to check the database for structural integrity, or restart the server to ensure in-memory data structures are not corrupted.    |
|    [17066](../../relational-databases/errors-events/mssqlserver-17066-database-engine-error.md)    |    16    |    Yes    |    SQL Server Assertion: File: <%s>, line=%d Failed Assertion = '%s'. This error may be timing-related. If the error persists after rerunning the statement, use DBCC CHECKDB to check the database for structural integrity, or restart the server to ensure in-memory data structures are not corrupted.    |
|    [17067](../../relational-databases/errors-events/mssqlserver-17067-database-engine-error.md)    |    16    |    Yes    |    SQL Server Assertion: File: <%s>, line = %d %s. This error may be timing-related. If the error persists after rerunning the statement, use DBCC CHECKDB to check the database for structural integrity, or restart the server to ensure in-memory data structures are not corrupted.    |
|    17068    |    10    |    No    |    PrintStack Request    |
|    17069    |    10    |    Yes    |    %s    |
|    17070    |    16    |    Yes    |    Clustered instances are not supported on this edition of SQL Server.    |
|    [17083](../../relational-databases/errors-events/mssqlserver-17083-database-engine-error.md)    |        |        |    The body of a natively compiled stored procedure must be an ATOMIC block.    |
|    [17084](../../relational-databases/errors-events/mssqlserver-17084-database-engine-error.md)|        |        |    The WITH clause of BEGIN ATOMIC statement must specify a value for the option '%ls'.    |
|    17101    |    10    |    Yes    |    (c) 2005 Microsoft Corporation.    |
|    17102    |    16    |    Yes    |    Failed to initialize Distributed COM (CoInitializeEx returned %lx). Heterogeneous queries and remote procedure calls are disabled. Check the DCOM configuration using Component Services in Control Panel.    |
|    17103    |    10    |    Yes    |    All rights reserved.    |
|    17104    |    10    |    Yes    |    Server process ID is %ld.    |
|    17105    |    10    |    Yes    |    Could not open master database in system task thread context. Terminating server.    |
|    17106    |    10    |    Yes    |    Common Criteria compliance mode is enabled. This is an informational message only; no user action is required.    |
|    17107    |    10    |    No    |    Perfmon counters for resource governor pools and groups failed to initialize and are disabled.    |
|    17108    |    10    |    Yes    |    Password policy update was successful.    |
|    17109    |    10    |    Yes    |    FallBack certificate was successfully created.    |
|    17110    |    10    |    Yes    |    Registry startup parameters: %.*ls    |
|    17111    |    10    |    Yes    |    Logging SQL Server messages in file '%s'.    |
|    [17112](../../relational-databases/errors-events/mssqlserver-17112-database-engine-error.md)    |    16    |    Yes    |    An invalid startup option %c was supplied, either from the registry or the command prompt. Correct or remove the option.    |
|    17113    |    16    |    Yes    |    Error %ls occurred while opening file '%ls' to obtain configuration information at startup. An invalid startup option might have caused the error. Verify your startup options, and correct or remove them if necessary.    |
|    17114    |    16    |    Yes    |    Error %ls occurred while opening file '%ls' to obtain configuration information at startup time. An invalid startup option might have caused the error. Verify your startup options, and correct or remove them if necessary.    |
|    17115    |    10    |    Yes    |    Command Line Startup Parameters:%.*ls    |
|    17116    |    16    |    Yes    |    Failed to initialize distributed COM; DCOM is not installed. Heterogeneous queries and remote procedure calls are disabled. Check the DCOM configuration using Component Services in Control Panel.    |
|    17119    |    10    |    Yes    |    The number of concurrent user connections was reduced to %ld, because it exceeded the allowable limit for this edition of SQL Server. To avoid this message in the future, use sp_configure to permanently adjust the number of user connections within the licensed limit.    |
|    17120    |    16    |    Yes    |    SQL Server could not spawn %s thread. Check the SQL Server error log and the Windows event logs for information about possible related problems.    |
|    17121    |    10    |    Yes    |    SQL Server is started with trace flag %d, this may cause user to see some error messages masked using '%ls'.    |
|    17123    |    10    |    Yes    |    Logging to event log is disabled. Startup option '-%c' is supplied, either from the registry or the command prompt.    |
|    17124    |    10    |    Yes    |    SQL Server has been configured for lightweight pooling. This is an informational message; no user action is required.    |
|    17125    |    10    |    Yes    |    Using dynamic lock allocation. Initial allocation of %I64u Lock blocks and %I64u Lock Owner blocks per node. This is an informational message only. No user action is required.    |
|    17126    |    10    |    Yes    |    SQL Server is now ready for client connections. This is an informational message; no user action is required.    |
|    17127    |    16    |    Yes    |    initdata: No memory for kernel buffer hash table.    |
|    [17128](../../relational-databases/errors-events/mssqlserver-17128-database-engine-error.md)    |    16    |    Yes    |    initdata: No memory for kernel buffers.    |
|    17129    |    10    |    Yes    |    initconfig: Warning: affinity mask specified is not valid. Defaulting to no affinity. Use sp_configure 'affinity mask' or 'affinity64 mask' to configure the system to be compatible with the CPU mask on the system. You can also configure the system based on the number of licensed CPUs.    |
|    [17130](../../relational-databases/errors-events/mssqlserver-17130-database-engine-error.md)    |    16    |    Yes    |    Not enough memory for the configured number of locks. Attempting to start with a smaller lock hash table, which may impact performance. Contact the database administrator to configure more memory for this instance of the Database Engine.    |
|    17131    |    16    |    Yes    |    Server startup failed due to insufficient memory for descriptor hash tables. Reduce non-essential memory load or increase system memory.    |
|    [17132](../../relational-databases/errors-events/mssqlserver-17132-database-engine-error.md)    |    16    |    Yes    |    Server startup failed due to insufficient memory for descriptor. Reduce non-essential memory load or increase system memory.    |
|    17133    |    16    |    Yes    |    Launch of startup procedure '%s' failed.    |
|    17135    |    10    |    Yes    |    Launched startup procedure '%s'.    |
|    17136    |    10    |    Yes    |    Clearing tempdb database.    |
|    17137    |    10    |    Yes    |    Starting up database '%s'.    |
|    17138    |    16    |    Yes    |    Unable to allocate enough memory to start '%ls'. Reduce non-essential memory load or increase system memory.    |
|    17140    |    16    |    Yes    |    Could not dispatch SQL Server by Service Control Manager. Operating system error = %s.    |
|    17141    |    16    |    Yes    |    Could not register Service Control Handler. Operating system error = %s.    |
|    [17142](../../relational-databases/errors-events/mssqlserver-17142-database-engine-error.md)    |    16    |    Yes    |    SQL Server service has been paused. No new connections will be allowed. To resume the service, use SQL Computer Manager or the Services application in Control Panel.    |
|    17143    |    16    |    Yes    |    %s: Could not set Service Control Status. Operating system error = %s.    |
|    17144    |    10    |    Yes    |    SQL Server is not allowing new connections because the Service Control Manager requested a pause. To resume the service, use SQL Computer Manager or the Services application in Control Panel.    |
|    17145    |    10    |    Yes    |    Service Control Handler received an invalid control code = %d.    |
|    17146    |    10    |    Yes    |    SQL Server is allowing new connections in response to 'continue' request from Service Control Manager. This is an informational message only. No user action is required.    |
|    [17147](../../relational-databases/errors-events/mssqlserver-17147-database-engine-error.md)    |    10    |    Yes    |    SQL Server is terminating because of a system shutdown. This is an informational message only. No user action is required.    |
|    [17148](../../relational-databases/errors-events/mssqlserver-17148-database-engine-error.md)    |    10    |    Yes    |    SQL Server is terminating in response to a 'stop' request from Service Control Manager. This is an informational message only. No user action is required.    |
|    17149    |    10    |    Yes    |    Using the static lock allocation specified in the locks configuration option. Allocated %I64u Lock blocks and %I64u Lock Owner blocks per node. This is an informational message only. No user action is required.    |
|    17150    |    10    |    Yes    |    Lock partitioning is enabled. This is an informational message only. No user action is required.    |
|    17152    |    10    |    Yes    |    Node configuration: node %ld: CPU mask: 0x%0*I64x Active CPU mask: 0x%0*I64x. This message provides a description of the NUMA configuration for this computer. This is an informational message only. No user action is required.    |
|    17153    |    10    |    Yes    |    Processor affinity turned on: processor mask 0x%0*I64x. Threads will execute on CPUs per affinity mask/affinity64 mask config option. This is an informational message; no user action is required.    |
|    17155    |    10    |    Yes    |    I/O affinity turned on, processor mask 0x%0*I64x. Disk I/Os will execute on CPUs per affinity I/O mask/affinity64 mask config option. This is an informational message only; no user action is required.    |
|    17156    |    16    |    Yes    |    initeventlog: Could not initiate the EventLog Service for the key '%s', last error code is %d.    |
|    17158    |    10    |    Yes    |    The server resumed execution after being idle %d seconds. This is an informational message only. No user action is required.    |
|    17159    |    10    |    Yes    |    The server is idle. This is an informational message only. No user action is required.    |
|    17161    |    10    |    Yes    |    SQL Server could not use the NO_BUFFERING option during I/O, because the master file sector size, %d, is incorrect. Move the master file to a drive with a correct sector size.    |
|    17162    |    10    |    Yes    |    SQL Server is starting at normal priority base (=7). This is an informational message only. No user action is required.    |
|    17163    |    10    |    Yes    |    SQL Server is starting at high priority base (=13). This is an informational message only. No user action is required.    |
|    17164    |    10    |    Yes    |    Detected %d CPUs. This is an informational message; no user action is required.    |
|    17165    |    10    |    Yes    |    The RANU instance is terminating in response to its internal time out. This is an informational message only. No user action is required.    |
|    17166    |    10    |    Yes    |    Attempting to initialize Microsoft Distributed Transaction Coordinator (MS DTC). This is an informational message only. No user action is required.    |
|    17167    |    10    |    Yes    |    Support for distributed transactions was not enabled for this instance of the Database Engine because it was started using the minimal configuration option. This is an informational message only. No user action is required.    |
|    17169    |    10    |    Yes    |    Unable to locate kernel HTTP driver Httpapi.dll in path. SQL Server native HTTP support is not available. Error: 0x%lx Your operating system may not support the kernel HTTP driver.    |
|    17170    |    10    |    Yes    |    SQL Server native HTTP support is not available. Could not find function entry point '%hs' in %hs. Error 0x%lx. Native HTTP access to SQL Server requires a later version of the operating system.    |
|    17171    |    10    |    Yes    |    SQL Server native HTTP support failed and will not be available. '%hs()' failed. Error 0x%lx.    |
|    17172    |    16    |    Yes    |    SNIInitialize() failed with error 0x%lx.    |
|    17173    |    10    |    Yes    |    Ignoring trace flag %d specified during startup. It is either an invalid trace flag or a trace flag that cannot be specified during server startup.    |
|    17174    |    10    |    Yes    |    Unable to initialize SQL Server native HTTP support due to insufficient resources. HTTP access to SQL Server will not be available. Error 0x%lx. This error typically indicates insufficient memory. Reduce non-essential memory load or increase system memory.    |
|    17175    |    10    |    Yes    |    The registry settings for SNI protocol configuration are incorrect. The server cannot accept connection requests. Error: 0x%lx. Status: 0x%lx.    |
|    17176    |    10    |    Yes    |    This instance of SQL Server last reported using a process ID of %s at %s (local) %s (UTC). This is an informational message only; no user action is required.    |
|    17177    |    10    |    Yes    |    This instance of SQL Server has been using a process ID of %s since %s (local) %s (UTC). This is an informational message only; no user action is required.    |
|    17178    |    10    |    Yes    |    Address Windowing Extensions is enabled. This is an informational message only; no user action is required.    |
|    17179    |    10    |    Yes    |    Could not use Address Windowing Extensions because the 'lock pages in memory' privilege was not granted.    |
|    17180    |    10    |    Yes    |    SQL Server is not configured to use all of the available system memory. To enable SQL Server to use more memory, set the awe enabled option to 1 by using the sp_configure stored procedure.    |
|    17181    |    16    |    Yes    |    SNIInitializeListener() failed with error 0x%lx.    |
|    17182    |    16    |    Yes    |    TDSSNIClient initialization failed with error 0x%lx, status code 0x%lx. Reason: %S_MSG %.*ls    |
|    17183    |    10    |    Yes    |    Attempting to cycle error log. This is an informational message only; no user action is required.    |
|    17184    |    10    |    Yes    |    The error log has been reinitialized. See the previous log for older entries.    |
|    17185    |    16    |    Yes    |    Unable to update password policy.    |
|    17186    |    16    |    Yes    |    Failed to enqueue %s task. There may be insufficient memory.    |
|    17187    |    16    |    Yes    |    SQL Server is not ready to accept new client connections. Wait a few minutes before trying again. If you have access to the error log, look for the informational message that indicates that SQL Server is ready before trying to connect again. %.*ls    |
|    17188    |    16    |    Yes    |    SQL Server cannot accept new connections, because it is shutting down. The connection has been closed.%.*ls    |
|    17189    |    16    |    Yes    |    SQL Server failed with error code 0x%x to spawn a thread to process a new login or connection. Check the SQL Server error log and the Windows event logs for information about possible related problems.%.*ls    |
|    17190    |    16    |    Yes    |    FallBack certificate initialization failed with error code: %d.    |
|    17191    |    16    |    Yes    |    Cannot accept a new connection because the session has been terminated. This error occurs when a new batch execution is attempted on a session that is logging out, or when a severe error is encountered upon connection. Check the error log to see if this session was terminated by a KILL command or because of severe errors.%.*ls    |
|    17192    |    10    |    Yes    |    Dedicated admin connection support was not started because of error 0x%lx, status code: 0x%lx. This error typically indicates a socket-based error, such as a port already in use.    |
|    17193    |    10    |    Yes    |    SQL Server native SOAP support is ready for client connections. This is an informational message only. No user action is required.    |
|    [17194](../../relational-databases/errors-events/mssqlserver-17194-database-engine-error.md)    |    16    |    Yes    |    The server was unable to load the SSL provider library needed to log in; the connection has been closed. SSL is used to encrypt either the login sequence or all communications, depending on how the administrator has configured the server. See Books Online for information on this error message: %d %.*ls %.*ls    |
|    17195    |    16    |    Yes    |    The server was unable to complete its initialization sequence because the available network libraries do not support the required level of encryption. The server process has stopped. Before restarting the server, verify that SSL certificates have been installed. See Books Online topic "Configuring Client Protocols and Network Libraries".    |
|    17196    |    10    |    Yes    |    Preparing for eventual growth to %d GB with Hot Add Memory.    |
|    17197    |    16    |    Yes    |    Login failed due to timeout; the connection has been closed. This error may indicate heavy server load. Reduce the load on the server and retry login.%.*ls    |
|    17198    |    16    |    Yes    |    Connection failed because the endpoint could not be found. This may result if an endpoint is dropped while a connection attempt is in progress. Attempt to connect to a different endpoint on the server.%.*ls    |
|    17199    |    10    |    Yes    |    Dedicated administrator connection support was not started because it is disabled on this edition of SQL Server. If you want to use a dedicated administrator connection, restart SQL Server using the trace flag %d. This is an informational message only. No user action is required.    |
|    17200    |    16    |    Yes    |    Changing the remote access settings for the Dedicated Admin Connection failed with error 0x%lx, status code 0x%lx.    |
|    17201    |    10    |    Yes    |    Dedicated admin connection support was established for listening locally on port %d.    |
|    17202    |    10    |    Yes    |    Dedicated admin connection support was established for listening remotely on port %d.    |
|    17203    |    16    |    Yes    |    SQL Server cannot start on this machine. The processor(s) (CPU) model does not support all instructions needed for SQL Server to run. Refer to the System Requirements section in BOL for further information.    |
|    [17204](../../relational-databases/errors-events/mssqlserver-17204-database-engine-error.md)    |    16    |    Yes    |    %ls: Could not open file %ls for file number %d. OS error: %ls.    |
|    [17207](../../relational-databases/errors-events/mssqlserver-17207-database-engine-error.md)    |    16    |    Yes    |    %ls: Operating system error %ls occurred while creating or opening file '%ls'. Diagnose and correct the operating system error, and retry the operation.    |
|    17208    |    16    |    Yes    |    %s: File '%s' has an incorrect size. It is listed as %d MB, but should be %d MB. Diagnose and correct disk failures, and restore the database from backup.    |
|    17253    |    10    |    Yes    |    SQL Server cannot use the NO_BUFFERING option during I/O on this file, because the sector size for file '%s', %d, is invalid. Move the file to a disk with a valid sector size.    |
|    17255    |    10    |    Yes    |    Secondary TempDB file '%.*ls' resides on a removable drive and therefore will not be attached during startup.    |
|    17256    |    10    |    Yes    |    Secondary TempDB file '%.*ls' will not be attached during TempDB startup; Drive check failed with error '%ld'.    |
|    17257    |    10    |    Yes    |    System error while trying to initialize disk info; Error '%ld'    |
|    17258    |    10    |    Yes    |    No free space in the TempDB database    |
|    [17300](../../relational-databases/errors-events/mssqlserver-17300-database-engine-error.md)    |    16    |    Yes    |    SQL Server was unable to run a new system task, either because there is insufficient memory or the number of configured sessions exceeds the maximum allowed in the server. Verify that the server has adequate memory. Use sp_configure with option 'user connections' to check the maximum number of user connections allowed. Use sys.dm_exec_sessions to check the current number of sessions, including user processes.    |
|    17303    |    16    |    Yes    |    The session with SPID %d was found to be invalid during termination, possibly because of corruption in the session structure. Contact Product Support Services.    |
|    17308    |    16    |    Yes    |    %s: Process %d generated an access violation. SQL Server is terminating this process.    |
|    17310    |    20    |    Yes    |    A user request from the session with SPID %d generated a fatal exception. SQL Server is terminating this session. Contact Product Support Services with the dump produced in the log directory.    |
|    17311    |    16    |    Yes    |    SQL Server is terminating because of fatal exception %lx. This error may be caused by an unhandled Win32 or C++ exception, or by an access violation encountered during exception handling. Check the SQL error log for any related stack dumps or messages. This exception forces SQL Server to shutdown. To recover from this error, restart the server (unless SQLAgent is configured to auto restart).    |
|    17312    |    16    |    Yes    |    SQL Server is terminating a system or background task %s due to errors in starting up the task (setup state %d).    |
|    17313    |    10    |    Yes    |    Unable to locate driver ntdll.dll in path. SQL Server native HTTP support is not available. Error: 0x%lx Your operating system may not support this driver.    |
|    17401    |    10    |    Yes    |    Server resumed execution after being idle %d seconds: user activity awakened the server. This is an informational message only. No user action is required.    |
|    17403    |    10    |    Yes    |    Server resumed execution after being idle %d seconds. Reason: timer event.    |
|    17404    |    10    |    Yes    |    The server resumed execution after being idle for %d seconds.    |
|    17405    |    24    |    Yes    |    An image corruption/hotpatch detected while reporting exceptional situation. This may be a sign of a hardware problem. Check SQLDUMPER_ERRORLOG.log for details.    |
|    17406    |    10    |    Yes    |    Server resumed execution after being idle %d seconds. Reason: resource pressure.    |
|    17550    |    10    |    Yes    |    DBCC TRACEON %d, server process ID (SPID) %d. This is an informational message only; no user action is required.    |
|    17551    |    10    |    Yes    |    DBCC TRACEOFF %d, server process ID (SPID) %d. This is an informational message only; no user action is required.    |
|    17557    |    16    |    Yes    |    DBCC DBRECOVER failed for database ID %d. Restore the database from a backup.    |
|    17558    |    10    |    Yes    |    Bypassing recovery for database ID %d. This is an informational message only. No user action is required.    |
|    17560    |    10    |    Yes    |    DBCC DBREPAIR: '%ls' index restored for '%ls.%ls'.    |
|    17561    |    10    |    Yes    |    %ls index restored for %ls.%ls.    |
|    17572    |    16    |    Yes    |    DBCC cannot free DLL '%ls'. SQL Server requires this DLL in order to function properly.    |
|    17573    |    10    |    Yes    |    CHECKDB for database '%ls' finished without errors on %ls (local time). This is an informational message only; no user action is required.    |
|    17656    |    10    |    Yes    |    Warning ******************    |
|    17657    |    10    |    Yes    |    Attempting to change default collation to %s.    |
|    17658    |    10    |    Yes    |    SQL Server started in single-user mode. This an informational message only. No user action is required.    |
|    [17659](../../relational-databases/errors-events/mssqlserver-17659-database-engine-error.md)    |    10    |    Yes    |    Warning: System table ID %d has been updated directly in database ID %d and cache coherence may not have been maintained. SQL Server should be restarted.    |
|    [17660](../../relational-databases/errors-events/mssqlserver-17660-database-engine-error.md)    |    10    |    Yes    |    Starting without recovery. This is an informational message only. No user action is required.    |
|    17661    |    10    |    Yes    |    Recovering all databases, but not clearing tempdb. This is an informational message only. No user action is required.    |
|    17663    |    10    |    Yes    |    Server name is '%s'. This is an informational message only. No user action is required.    |
|    17664    |    10    |    Yes    |    The NETBIOS name of the local node that is running the server is '%ls'. This is an informational message only. No user action is required.    |
|    17674    |    10    |    Yes    |    Login: %.*ls %.*ls, server process ID (SPID): %d, kernel process ID (KPID): %d.    |
|    [17676](../../relational-databases/errors-events/mssqlserver-17676-database-engine-error.md)    |    10    |    Yes    |    SQL Server shutdown due to Ctrl-C or Ctrl-Break signal. This is an informational message only. No user action is required.    |
|    17681    |    10    |    Yes    |    Loading default collation %s for this instance of SQL Server.    |
|    17750    |    16    |    Yes    |    Could not load the DLL %ls, or one of the DLLs it references. Reason: %ls.    |
|    17751    |    16    |    Yes    |    Could not find the function %ls in the library %ls. Reason: %ls.    |
|    17752    |    16    |    Yes    |    SQL Server has insufficient memory to run the extended stored procedure '%ls'. Release server memory resources by closing connections or ending transactions.    |
|    17753    |    16    |    No    |    %.*ls can only be executed in the master database.    |
|    17802    |    20    |    Yes    |    The Tabular Data Stream (TDS) version 0x%x of the client library used to open the connection is unsupported or unknown. The connection has been closed. %.*ls    |
|    [17803](../../relational-databases/errors-events/mssqlserver-17803-database-engine-error.md)    |    20    |    Yes    |    There was a memory allocation failure during connection establishment. Reduce nonessential memory load, or increase system memory. The connection has been closed.%.*ls    |
|    17805    |    20    |    Yes    |    The value in the usertype field of the login record is invalid. The value 0x01, which was used by Sybase clients, is no longer supported by SQL Server. Contact the vendor of the client library that is being used to connect to SQL Server.%.*ls    |
|    17806    |    20    |    Yes    |    SSPI handshake failed with error code 0x%x while establishing a connection with integrated security; the connection has been closed.%.*ls    |
|    17807    |    20    |    Yes    |    Event '%ld', which was received from the client, is not recognized by SQL Server. Contact the vendor of the client library that is being used to connect to SQL Server, and have the vendor fix the event number in the tabular data stream that is sent.    |
|    [17809](../../relational-databases/errors-events/mssqlserver-17809-database-engine-error.md)    |    20    |    Yes    |    Could not connect because the maximum number of '%ld' user connections has already been reached. The system administrator can use sp_configure to increase the maximum value. The connection has been closed.%.*ls    |
|    17810    |    20    |    Yes    |    Could not connect because the maximum number of '%ld' dedicated administrator connections already exists. Before a new connection can be made, the existing dedicated administrator connection must be dropped, either by logging off or ending the process.%.*ls    |
|    17812    |    10    |    Yes    |    Dedicated administrator connection has been disconnected. This is an informational message only. No user action is required.    |
|    17813    |    20    |    Yes    |    The requested service has been stopped or disabled and is unavailable at this time. The connection has been closed.%.*ls    |
|    17825    |    18    |    Yes    |    Could not close network endpoint, or could not shut down network library. The cause is an internal error in a network library. Review the error log: the entry listed after this error contains the error code from the network library.    |
|    17826    |    18    |    Yes    |    Could not start the network library because of an internal error in the network library. To determine the cause, review the errors immediately preceding this one in the error log.    |
|    17827    |    20    |    Yes    |    There was a failure while attempting to encrypt a password. The connection has been closed.%.*ls    |
|    17828    |    20    |    Yes    |    The prelogin packet used to open the connection is structurally invalid; the connection has been closed. Please contact the vendor of the client library.%.*ls    |
|    17829    |    20    |    Yes    |    A network error occurred while establishing a connection; the connection has been closed.%.*ls    |
|    17830    |    20    |    Yes    |    Network error code 0x%x occurred while establishing a connection; the connection has been closed. This may have been caused by client or server login timeout expiration. Time spent during login: total %d ms, enqueued %d ms, network writes %d ms, network reads %d ms, establishing SSL %d ms, negotiating SSPI %d ms, validating login %d ms, including user-defined login processing %d ms.%.*ls    |
|    [17832](../../relational-databases/errors-events/mssqlserver-17832-database-engine-error.md)    |    20    |    Yes    |    The login packet used to open the connection is structurally invalid; the connection has been closed. Please contact the vendor of the client library.%.*ls    |
|    17835    |    20    |    Yes    |    Encryption is required to connect to this server but the client library does not support encryption; the connection has been closed. Please upgrade your client library.%.*ls    |
|    17836    |    20    |    Yes    |    Length specified in network packet payload did not match number of bytes read; the connection has been closed. Please contact the vendor of the client library.%.*ls    |
|    17881    |    16    |    Yes    |    '%ls' is an unsupported Open Data Services API.    |
|    [17883](../../relational-databases/errors-events/mssqlserver-17883-database-engine-error.md)    |    10    |    Yes    |    Process %ld:%ld:%ld (0x%lx) Worker 0x%p appears to be non-yielding on Scheduler %ld. Thread creation time: %I64d. Approx Thread CPU Used: kernel %I64d ms, user %I64d ms. Process Utilization %d%%. System Idle %d%%. Interval: %I64d ms.    |
|    [17884](../../relational-databases/errors-events/mssqlserver-17884-database-engine-error.md)    |    10    |    Yes    |    New queries assigned to process on Node %d have not been picked up by a worker thread in the last %d seconds. Blocking or long-running queries can contribute to this condition, and may degrade client response time. Use the "max worker threads" configuration option to increase number of allowable threads, or optimize current running queries. SQL Process Utilization: %d%%. System Idle: %d%%.    |
|    17885    |    16    |    No    |    An unexpected query string was passed to a Web Service Description Language (WSDL) generation procedure.    |
|    17886    |    20    |    Yes    |    The server will drop the connection, because the client driver has sent multiple requests while the session is in single-user mode. This error occurs when a client sends a request to reset the connection while there are batches still running in the session, or when the client sends a request while the session is resetting a connection. Please contact the client driver vendor.    |
|    [17887](../../relational-databases/errors-events/mssqlserver-17887-database-engine-error.md)    |    10    |    Yes    |    IO Completion Listener (0x%lx) Worker 0x%p appears to be non-yielding on Node %ld. Approx CPU Used: kernel %I64d ms, user %I64d ms, Interval: %I64d.    |
|    17888    |    10    |    Yes    |    All schedulers on Node %d appear deadlocked due to a large number of worker threads waiting on %ls. Process Utilization %d%%.    |
|    17889    |    16    |    Yes    |    A new connection was rejected because the maximum number of connections on session ID %d has been reached. Close an existing connection on this session and retry.%.*ls    |
|    [17890](../../relational-databases/errors-events/mssqlserver-17890-database-engine-error.md)    |    10    |    Yes    |    A significant part of sql server process memory has been paged out. This may result in a performance degradation. Duration: %d seconds. Working set (KB): %I64d, committed (KB): %I64d, memory utilization: %d%%.    |
|    17891    |    10    |    Yes    |    Resource Monitor (0x%lx) Worker 0x%p appears to be non-yielding on Node %ld. Memory freed: %I64d KB. Approx CPU Used: kernel %I64d ms, user %I64d ms, Interval: %I64d.    |
|    [17892](../../relational-databases/errors-events/mssqlserver-17892-database-engine-error.md)    |    14    |    Yes    |    Logon failed for login '%.*ls' due to trigger execution.%.*ls    |
|    17894    |    10    |    Yes    |    Dispatcher (0x%lx) from dispatcher pool '%.*ls' Worker 0x%p appears to be non-yielding on Node %ld. Approx CPU Used: kernel %I64d ms, user %I64d ms, Interval: %I64d.    |
