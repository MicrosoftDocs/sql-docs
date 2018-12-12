---
title: "sp_trace_setevent (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "sp_trace_setevent_TSQL"
  - "sp_trace_setevent"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sp_trace_setevent"
ms.assetid: 7662d1d9-6d0f-443a-b011-c901a8b77a44
author: "stevestein"
ms.author: "sstein"
manager: craigg
---
# sp_trace_setevent (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Adds or removes an event or event column to a trace. **sp_trace_setevent** may be executed only on existing traces that are stopped (*status* is **0**). An error is returned if this stored procedure is executed on a trace that does not exist or whose *status* is not **0**.  
  
> [!IMPORTANT]  
>  [!INCLUDE[ssNoteDepFutureAvoid](../../includes/ssnotedepfutureavoid-md.md)] Use Extended Events instead.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_trace_setevent [ @traceid = ] trace_id   
          , [ @eventid = ] event_id  
          , [ @columnid = ] column_id  
          , [ @on = ] on  
```  
  
## Arguments  
 [ **@traceid=** ] *trace_id*  
 Is the ID of the trace to be modified. *trace_id* is **int**, with no default. The user employs this *trace_id* value to identify, modify, and control the trace.  
  
 [ **@eventid=** ] *event_id*  
 Is the ID of the event to turn on. *event_id* is **int**, with no default.  
  
 This table lists the events that can be added to or removed from a trace.  
  
|Event number|Event name|Description|  
|------------------|----------------|-----------------|  
|0-9|Reserved|Reserved|  
|10|RPC:Completed|Occurs when a remote procedure call (RPC) has completed.|  
|11|RPC:Starting|Occurs when an RPC has started.|  
|12|SQL:BatchCompleted|Occurs when a [!INCLUDE[tsql](../../includes/tsql-md.md)] batch has completed.|  
|13|SQL:BatchStarting|Occurs when a [!INCLUDE[tsql](../../includes/tsql-md.md)] batch has started.|  
|14|Audit Login|Occurs when a user successfully logs in to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].|  
|15|Audit Logout|Occurs when a user logs out of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].|  
|16|Attention|Occurs when attention events, such as client-interrupt requests or broken client connections, happen.|  
|17|ExistingConnection|Detects all activity by users connected to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] before the trace started.|  
|18|Audit Server Starts and Stops|Occurs when the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] service state is modified.|  
|19|DTCTransaction|Tracks [!INCLUDE[msCoName](../../includes/msconame-md.md)] Distributed Transaction Coordinator (MS DTC) coordinated transactions between two or more databases.|  
|20|Audit Login Failed|Indicates that a login attempt to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] from a client failed.|  
|21|EventLog|Indicates that events have been logged in the Windows application log.|  
|22|ErrorLog|Indicates that error events have been logged in the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] error log.|  
|23|Lock:Released|Indicates that a lock on a resource, such as a page, has been released.|  
|24|Lock:Acquired|Indicates acquisition of a lock on a resource, such as a data page.|  
|25|Lock:Deadlock|Indicates that two concurrent transactions have deadlocked each other by trying to obtain incompatible locks on resources the other transaction owns.|  
|26|Lock:Cancel|Indicates that the acquisition of a lock on a resource has been canceled (for example, due to a deadlock).|  
|27|Lock:Timeout|Indicates that a request for a lock on a resource, such as a page, has timed out due to another transaction holding a blocking lock on the required resource. Time-out is determined by the @@LOCK_TIMEOUT function, and can be set with the SET LOCK_TIMEOUT statement.|  
|28|Degree of Parallelism Event (7.0 Insert)|Occurs before a SELECT, INSERT, or UPDATE statement is executed.|  
|29-31|Reserved|Use Event 28 instead.|  
|32|Reserved|Reserved|  
|33|Exception|Indicates that an exception has occurred in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].|  
|34|SP:CacheMiss|Indicates when a stored procedure is not found in the procedure cache.|  
|35|SP:CacheInsert|Indicates when an item is inserted into the procedure cache.|  
|36|SP:CacheRemove|Indicates when an item is removed from the procedure cache.|  
|37|SP:Recompile|Indicates that a stored procedure was recompiled.|  
|38|SP:CacheHit|Indicates when a stored procedure is found in the procedure cache.|  
|39|Deprecated|Deprecated|  
|40|SQL:StmtStarting|Occurs when the [!INCLUDE[tsql](../../includes/tsql-md.md)] statement has started.|  
|41|SQL:StmtCompleted|Occurs when the [!INCLUDE[tsql](../../includes/tsql-md.md)] statement has completed.|  
|42|SP:Starting|Indicates when the stored procedure has started.|  
|43|SP:Completed|Indicates when the stored procedure has completed.|  
|44|SP:StmtStarting|Indicates that a [!INCLUDE[tsql](../../includes/tsql-md.md)] statement within a stored procedure has started executing.|  
|45|SP:StmtCompleted|Indicates that a [!INCLUDE[tsql](../../includes/tsql-md.md)] statement within a stored procedure has finished executing.|  
|46|Object:Created|Indicates that an object has been created, such as for CREATE INDEX, CREATE TABLE, and CREATE DATABASE statements.|  
|47|Object:Deleted|Indicates that an object has been deleted, such as in DROP INDEX and DROP TABLE statements.|  
|48|Reserved||  
|49|Reserved||  
|50|SQL Transaction|Tracks [!INCLUDE[tsql](../../includes/tsql-md.md)] BEGIN, COMMIT, SAVE, and ROLLBACK TRANSACTION statements.|  
|51|Scan:Started|Indicates when a table or index scan has started.|  
|52|Scan:Stopped|Indicates when a table or index scan has stopped.|  
|53|CursorOpen|Indicates when a cursor is opened on a [!INCLUDE[tsql](../../includes/tsql-md.md)] statement by ODBC, OLE DB, or DB-Library.|  
|54|TransactionLog|Tracks when transactions are written to the transaction log.|  
|55|Hash Warning|Indicates that a hashing operation (for example, hash join, hash aggregate, hash union, and hash distinct) that is not processing on a buffer partition has reverted to an alternate plan. This can occur because of recursion depth, data skew, trace flags, or bit counting.|  
|56-57|Reserved||  
|58|Auto Stats|Indicates an automatic updating of index statistics has occurred.|  
|59|Lock:Deadlock Chain|Produced for each of the events leading up to the deadlock.|  
|60|Lock:Escalation|Indicates that a finer-grained lock has been converted to a coarser-grained lock (for example, a page lock escalated or converted to a TABLE or HoBT lock).|  
|61|OLE DB Errors|Indicates that an OLE DB error has occurred.|  
|62-66|Reserved||  
|67|Execution Warnings|Indicates any warnings that occurred during the execution of a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] statement or stored procedure.|  
|68|Showplan Text (Unencoded)|Displays the plan tree of the [!INCLUDE[tsql](../../includes/tsql-md.md)] statement executed.|  
|69|Sort Warnings|Indicates sort operations that do not fit into memory. Does not include sort operations involving the creating of indexes; only sort operations within a query (such as an ORDER BY clause used in a SELECT statement).|  
|70|CursorPrepare|Indicates when a cursor on a [!INCLUDE[tsql](../../includes/tsql-md.md)] statement is prepared for use by ODBC, OLE DB, or DB-Library.|  
|71|Prepare SQL|ODBC, OLE DB, or DB-Library has prepared a [!INCLUDE[tsql](../../includes/tsql-md.md)] statement or statements for use.|  
|72|Exec Prepared SQL|ODBC, OLE DB, or DB-Library has executed a prepared [!INCLUDE[tsql](../../includes/tsql-md.md)] statement or statements.|  
|73|Unprepare SQL|ODBC, OLE DB, or DB-Library has unprepared (deleted) a prepared [!INCLUDE[tsql](../../includes/tsql-md.md)] statement or statements.|  
|74|CursorExecute|A cursor previously prepared on a [!INCLUDE[tsql](../../includes/tsql-md.md)] statement by ODBC, OLE DB, or DB-Library is executed.|  
|75|CursorRecompile|A cursor opened on a [!INCLUDE[tsql](../../includes/tsql-md.md)] statement by ODBC or DB-Library has been recompiled either directly or due to a schema change.<br /><br /> Triggered for ANSI and non-ANSI cursors.|  
|76|CursorImplicitConversion|A cursor on a [!INCLUDE[tsql](../../includes/tsql-md.md)] statement is converted by [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] from one type to another.<br /><br /> Triggered for ANSI and non-ANSI cursors.|  
|77|CursorUnprepare|A prepared cursor on a [!INCLUDE[tsql](../../includes/tsql-md.md)] statement is unprepared (deleted) by ODBC, OLE DB, or DB-Library.|  
|78|CursorClose|A cursor previously opened on a [!INCLUDE[tsql](../../includes/tsql-md.md)] statement by ODBC, OLE DB, or DB-Library is closed.|  
|79|Missing Column Statistics|Column statistics that could have been useful for the optimizer are not available.|  
|80|Missing Join Predicate|Query that has no join predicate is being executed. This could result in a long-running query.|  
|81|Server Memory Change|[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] memory usage has increased or decreased by either 1 megabyte (MB) or 5 percent of the maximum server memory, whichever is greater.|  
|82-91|User Configurable (0-9)|Event data defined by the user.|  
|92|Data File Auto Grow|Indicates that a data file was extended automatically by the server.|  
|93|Log File Auto Grow|Indicates that a log file was extended automatically by the server.|  
|94|Data File Auto Shrink|Indicates that a data file was shrunk automatically by the server.|  
|95|Log File Auto Shrink|Indicates that a log file was shrunk automatically by the server.|  
|96|Showplan Text|Displays the query plan tree of the SQL statement from the query optimizer. Note that the **TextData** column does not contain the Showplan for this event.|  
|97|Showplan All|Displays the query plan with full compile-time details of the SQL statement executed. Note that the **TextData** column does not contain the Showplan for this event.|  
|98|Showplan Statistics Profile|Displays the query plan with full run-time details of the SQL statement executed. Note that the **TextData** column does not contain the Showplan for this event.|  
|99|Reserved||  
|100|RPC Output Parameter|Produces output values of the parameters for every RPC.|  
|101|Reserved||  
|102|Audit Database Scope GDR|Occurs every time a GRANT, DENY, REVOKE for a statement permission is issued by any user in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] for database-only actions such as granting permissions on a database.|  
|103|Audit Object GDR Event|Occurs every time a GRANT, DENY, REVOKE for an object permission is issued by any user in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].|  
|104|Audit AddLogin Event|Occurs when a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login is added or removed; for **sp_addlogin** and **sp_droplogin**.|  
|105|Audit Login GDR Event|Occurs when a Windows login right is added or removed; for **sp_grantlogin**, **sp_revokelogin**, and **sp_denylogin**.|  
|106|Audit Login Change Property Event|Occurs when a property of a login, except passwords, is modified; for **sp_defaultdb** and **sp_defaultlanguage**.|  
|107|Audit Login Change Password Event|Occurs when a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login password is changed.<br /><br /> Passwords are not recorded.|  
|108|Audit Add Login to Server Role Event|Occurs when a login is added or removed from a fixed server role; for **sp_addsrvrolemember**, and **sp_dropsrvrolemember**.|  
|109|Audit Add DB User Event|Occurs when a login is added or removed as a database user (Windows or [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]) to a database; for **sp_grantdbaccess**, **sp_revokedbaccess**, **sp_adduser**, and **sp_dropuser**.|  
|110|Audit Add Member to DB Role Event|Occurs when a login is added or removed as a database user (fixed or user-defined) to a database; for **sp_addrolemember**, **sp_droprolemember**, and **sp_changegroup**.|  
|111|Audit Add Role Event|Occurs when a login is added or removed as a database user to a database; for **sp_addrole** and **sp_droprole**.|  
|112|Audit App Role Change Password Event|Occurs when a password of an application role is changed.|  
|113|Audit Statement Permission Event|Occurs when a statement permission (such as CREATE TABLE) is used.|  
|114|Audit Schema Object Access Event|Occurs when an object permission (such as SELECT) is used, both successfully or unsuccessfully.|  
|115|Audit Backup/Restore Event|Occurs when a BACKUP or RESTORE command is issued.|  
|116|Audit DBCC Event|Occurs when DBCC commands are issued.|  
|117|Audit Change Audit Event|Occurs when audit trace modifications are made.|  
|118|Audit Object Derived Permission Event|Occurs when a CREATE, ALTER, and DROP object commands are issued.|  
|119|OLEDB Call Event|Occurs when OLE DB provider calls are made for distributed queries and remote stored procedures.|  
|120|OLEDB QueryInterface Event|Occurs when OLE DB **QueryInterface** calls are made for distributed queries and remote stored procedures.|  
|121|OLEDB DataRead Event|Occurs when a data request call is made to the OLE DB provider.|  
|122|Showplan XML|Occurs when an SQL statement executes. Include this event to identify Showplan operators. Each event is stored in a well-formed XML document. Note that the **Binary** column for this event contains the encoded Showplan. Use SQL Server Profiler to open the trace and view the Showplan.|  
|123|SQL:FullTextQuery|Occurs when a full text query executes.|  
|124|Broker:Conversation|Reports the progress of a [!INCLUDE[ssSB](../../includes/sssb-md.md)] conversation.|  
|125|Deprecation Announcement|Occurs when you use a feature that will be removed from a future version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].|  
|126|Deprecation Final Support|Occurs when you use a feature that will be removed from the next major release of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].|  
|127|Exchange Spill Event|Occurs when communication buffers in a parallel query plan have been temporarily written to the **tempdb** database.|  
|128|Audit Database Management Event|Occurs when a database is created, altered, or dropped.|  
|129|Audit Database Object Management Event|Occurs when a CREATE, ALTER, or DROP statement executes on database objects, such as schemas.|  
|130|Audit Database Principal Management Event|Occurs when principals, such as users, are created, altered, or dropped from a database.|  
|131|Audit Schema Object Management Event|Occurs when server objects are created, altered, or dropped.|  
|132|Audit Server Principal Impersonation Event|Occurs when there is an impersonation within server scope, such as EXECUTE AS LOGIN.|  
|133|Audit Database Principal Impersonation Event|Occurs when an impersonation occurs within the database scope, such as EXECUTE AS USER or SETUSER.|  
|134|Audit Server Object Take Ownership Event|Occurs when the owner is changed for objects in server scope.|  
|135|Audit Database Object Take Ownership Event|Occurs when a change of owner for objects within database scope occurs.|  
|136|Broker:Conversation Group|Occurs when [!INCLUDE[ssSB](../../includes/sssb-md.md)] creates a new conversation group or drops an existing conversation group.|  
|137|Blocked Process Report|Occurs when a process has been blocked for more than a specified amount of time. Does not include system processes or processes that are waiting on non deadlock-detectable resources. Use **sp_configure** to configure the threshold and frequency at which reports are generated.|  
|138|Broker:Connection|Reports the status of a transport connection managed by [!INCLUDE[ssSB](../../includes/sssb-md.md)].|  
|139|Broker:Forwarded Message Sent|Occurs when [!INCLUDE[ssSB](../../includes/sssb-md.md)] forwards a message.|  
|140|Broker:Forwarded Message Dropped|Occurs when [!INCLUDE[ssSB](../../includes/sssb-md.md)] drops a message that was intended to be forwarded.|  
|141|Broker:Message Classify|Occurs when [!INCLUDE[ssSB](../../includes/sssb-md.md)] determines the routing for a message.|  
|142|Broker:Transmission|Indicates that errors have occurred in the [!INCLUDE[ssSB](../../includes/sssb-md.md)] transport layer. The error number and state values indicate the source of the error.|  
|143|Broker:Queue Disabled|Indicates a poison message was detected because there were five consecutive transaction rollbacks on a [!INCLUDE[ssSB](../../includes/sssb-md.md)] queue. The event contains the database ID and queue ID of the queue that contains the poison message.|  
|144-145|Reserved||  
|146|Showplan XML Statistics Profile|Occurs when an SQL statement executes. Identifies the Showplan operators and displays complete, compile-time data. Note that the **Binary** column for this event contains the encoded Showplan. Use SQL Server Profiler to open the trace and view the Showplan.|  
|148|Deadlock Graph|Occurs when an attempt to acquire a lock is canceled because the attempt was part of a deadlock and was chosen as the deadlock victim. Provides an XML description of a deadlock.|  
|149|Broker:Remote Message Acknowledgement|Occurs when [!INCLUDE[ssSB](../../includes/sssb-md.md)] sends or receives a message acknowledgement.|  
|150|Trace File Close|Occurs when a trace file closes during a trace file rollover.|  
|151|Reserved||  
|152|Audit Change Database Owner|Occurs when ALTER AUTHORIZATION is used to change the owner of a database and permissions are checked to do that.|  
|153|Audit Schema Object Take Ownership Event|Occurs when ALTER AUTHORIZATION is used to assign an owner to an object and permissions are checked to do that.|  
|154|Reserved||  
|155|FT:Crawl Started|Occurs when a full-text crawl (population) starts. Use to check if a crawl request is picked up by worker tasks.|  
|156|FT:Crawl Stopped|Occurs when a full-text crawl (population) stops. Stops occur when a crawl completes successfully or when a fatal error occurs.|  
|157|FT:Crawl Aborted|Occurs when an exception is encountered during a full-text crawl. Usually causes the full-text crawl to stop.|  
|158|Audit Broker Conversation|Reports audit messages related to [!INCLUDE[ssSB](../../includes/sssb-md.md)] dialog security.|  
|159|Audit Broker Login|Reports audit messages related to [!INCLUDE[ssSB](../../includes/sssb-md.md)] transport security.|  
|160|Broker:Message Undeliverable|Occurs when [!INCLUDE[ssSB](../../includes/sssb-md.md)] is unable to retain a received message that should have been delivered to a service.|  
|161|Broker:Corrupted Message|Occurs when [!INCLUDE[ssSB](../../includes/sssb-md.md)] receives a corrupted message.|  
|162|User Error Message|Displays error messages that users see in the case of an error or exception.|  
|163|Broker:Activation|Occurs when a queue monitor starts an activation stored procedure, sends a QUEUE_ACTIVATION notification, or when an activation stored procedure started by a queue monitor exits.|  
|164|Object:Altered|Occurs when a database object is altered.|  
|165|Performance statistics|Occurs when a compiled query plan has been cached for the first time, recompiled, or removed from the plan cache.|  
|166|SQL:StmtRecompile|Occurs when a statement-level recompilation occurs.|  
|167|Database Mirroring State Change|Occurs when the state of a mirrored database changes.|  
|168|Showplan XML For Query Compile|Occurs when an SQL statement compiles. Displays the complete, compile-time data. Note that the **Binary** column for this event contains the encoded Showplan. Use SQL Server Profiler to open the trace and view the Showplan.|  
|169|Showplan All For Query Compile|Occurs when an SQL statement compiles. Displays complete, compile-time data. Use to identify Showplan operators.|  
|170|Audit Server Scope GDR Event|Indicates that a grant, deny, or revoke event for permissions in server scope occurred, such as creating a login.|  
|171|Audit Server Object GDR Event|Indicates that a grant, deny, or revoke event for a schema object, such as a table or function, occurred.|  
|172|Audit Database Object GDR Event|Indicates that a grant, deny, or revoke event for database objects, such as assemblies and schemas, occurred.|  
|173|Audit Server Operation Event|Occurs when Security Audit operations such as altering settings, resources, external access, or authorization are used.|  
|175|Audit Server Alter Trace Event|Occurs when a statement checks for the ALTER TRACE permission.|  
|176|Audit Server Object Management Event|Occurs when server objects are created, altered, or dropped.|  
|177|Audit Server Principal Management Event|Occurs when server principals are created, altered, or dropped.|  
|178|Audit Database Operation Event|Occurs when database operations occur, such as checkpoint or subscribe query notification.|  
|180|Audit Database Object Access Event|Occurs when database objects, such as schemas, are accessed.|  
|181|TM: Begin Tran starting|Occurs when a BEGIN TRANSACTION request starts.|  
|182|TM: Begin Tran completed|Occurs when a BEGIN TRANSACTION request completes.|  
|183|TM: Promote Tran starting|Occurs when a PROMOTE TRANSACTION request starts.|  
|184|TM: Promote Tran completed|Occurs when a PROMOTE TRANSACTION request completes.|  
|185|TM: Commit Tran starting|Occurs when a COMMIT TRANSACTION request starts.|  
|186|TM: Commit Tran completed|Occurs when a COMMIT TRANSACTION request completes.|  
|187|TM: Rollback Tran starting|Occurs when a ROLLBACK TRANSACTION request starts.|  
|188|TM: Rollback Tran completed|Occurs when a ROLLBACK TRANSACTION request completes.|  
|189|Lock:Timeout (timeout > 0)|Occurs when a request for a lock on a resource, such as a page, times out.|  
|190|Progress Report: Online Index Operation|Reports the progress of an online index build operation while the build process is running.|  
|191|TM: Save Tran starting|Occurs when a SAVE TRANSACTION request starts.|  
|192|TM: Save Tran completed|Occurs when a SAVE TRANSACTION request completes.|  
|193|Background Job Error|Occurs when a background job terminates abnormally.|  
|194|OLEDB Provider Information|Occurs when a distributed query runs and collects information corresponding to the provider connection.|  
|195|Mount Tape|Occurs when a tape mount request is received.|  
|196|Assembly Load|Occurs when a request to load a CLR assembly occurs.|  
|197|Reserved||  
|198|XQuery Static Type|Occurs when an XQuery expression is executed. This event class provides the static type of the XQuery expression.|  
|199|QN: subscription|Occurs when a query registration cannot be subscribed. The **TextData** column contains information about the event.|  
|200|QN: parameter table|Information about active subscriptions is stored in internal parameter tables. This event class occurs when a parameter table is created or deleted. Typically, these tables are created or deleted when the database is restarted. The **TextData** column contains information about the event.|  
|201|QN: template|A query template represents a class of subscription queries. Typically, queries in the same class are identical except for their parameter values. This event class occurs when a new subscription request falls into an already existing class of (Match), a new class (Create), or a Drop class, which indicates cleanup of templates for query classes without active subscriptions. The **TextData** column contains information about the event.|  
|202|QN: dynamics|Tracks internal activities of query notifications. The **TextData** column contains information about the event.|  
|212|Bitmap Warning|Indicates when bitmap filters have been disabled in a query.|  
|213|Database Suspect Data Page|Indicates when a page is added to the **suspect_pages** table in **msdb**.|  
|214|CPU threshold exceeded|Indicates when the Resource Governor detects a query has exceeded the CPU threshold value (REQUEST_MAX_CPU_TIME_SEC).|  
|215|PreConnect:Starting|Indicates when a LOGON trigger or Resource Governor classifier function starts execution.|  
|216|PreConnect:Completed|Indicates when a LOGON trigger or Resource Governor classifier function completes execution.|  
|217|Plan Guide Successful|Indicates that SQL Server successfully produced an execution plan for a query or batch that contained a plan guide.|  
|218|Plan Guide Unsuccessful|Indicates that SQL Server could not produce an execution plan for a query or batch that contained a plan guide. SQL Server attempted to generate an execution plan for this query or batch without applying the plan guide. An invalid plan guide may be the cause of this problem. You can validate the plan guide by using the sys.fn_validate_plan_guide system function.|  
|235|Audit Fulltext||  
  
 [ **@columnid=** ] *column_id*  
 Is the ID of the column to be added for the event. *column_id* is **int**, with no default.  
  
 The following table lists the columns that can be added for an event.  
  
|Column number|Column name|Description|  
|-------------------|-----------------|-----------------|  
|1|**TextData**|Text value dependent on the event class that is captured in the trace.|  
|2|**BinaryData**|Binary value dependent on the event class captured in the trace.|  
|3|**DatabaseID**|ID of the database specified by the USE *database* statement, or the default database if no USE *database* statement is issued for a given connection.<br /><br /> The value for a database can be determined by using the DB_ID function.|  
|4|**TransactionID**|System-assigned ID of the transaction.|  
|5|**LineNumber**|Contains the number of the line that contains the error. For events that involve [!INCLUDE[tsql](../../includes/tsql-md.md)] statements, like **SP:StmtStarting**, the **LineNumber** contains the line number of the statement in the stored procedure or batch.|  
|6|**NTUserName**|[!INCLUDE[msCoName](../../includes/msconame-md.md)] Windows user name.|  
|7|**NTDomainName**|Windows domain to which the user belongs.|  
|8|**HostName**|Name of the client computer that originated the request.|  
|9|**ClientProcessID**|ID assigned by the client computer to the process in which the client application is running.|  
|10|**ApplicationName**|Name of the client application that created the connection to an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. This column is populated with the values passed by the application rather than the displayed name of the program.|  
|11|**LoginName**|[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login name of the client.|  
|12|**SPID**|Server Process ID assigned by [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] to the process associated with the client.|  
|13|**Duration**|Amount of elapsed time (in microseconds) taken by the event. This data column is not populated by the Hash Warning event.|  
|14|**StartTime**|Time at which the event started, when available.|  
|15|**EndTime**|Time at which the event ended. This column is not populated for starting event classes, such as **SQL:BatchStarting** or **SP:Starting**. It is also not populated by the **Hash Warning** event.|  
|16|**Reads**|Number of logical disk reads performed by the server on behalf of the event. This column is not populated by the **Lock:Released** event.|  
|17|**Writes**|Number of physical disk writes performed by the server on behalf of the event.|  
|18|**CPU**|Amount of CPU time (in milliseconds) used by the event.|  
|19|**Permissions**|Represents the bitmap of permissions; used by Security Auditing.|  
|20|**Severity**|Severity level of an exception.|  
|21|**EventSubClass**|Type of event subclass. This data column is not populated for all event classes.|  
|22|**ObjectID**|System-assigned ID of the object.|  
|23|**Success**|Success of the permissions usage attempt; used for auditing.<br /><br /> **1** = success**0** = failure|  
|24|**IndexID**|ID for the index on the object affected by the event. To determine the index ID for an object, use the **indid** column of the **sysindexes** system table.|  
|25|**IntegerData**|Integer value dependent on the event class captured in the trace.|  
|26|**ServerName**|Name of the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], either *servername* or *servername\instancename*, being traced.|  
|27|**EventClass**|Type of event class being recorded.|  
|28|**ObjectType**|Type of object, such as: table, function, or stored procedure.|  
|29|**NestLevel**|The nesting level at which this stored procedure is executing. See [@@NESTLEVEL &#40;Transact-SQL&#41;](../../t-sql/functions/nestlevel-transact-sql.md).|  
|30|**State**|Server state, in case of an error.|  
|31|**Error**|Error number.|  
|32|**Mode**|Lock mode of the lock acquired. This column is not populated by the **Lock:Released** event.|  
|33|**Handle**|Handle of the object referenced in the event.|  
|34|**ObjectName**|Name of object accessed.|  
|35|**DatabaseName**|Name of the database specified in the USE *database* statement.|  
|36|**FileName**|Logical name of the file name modified.|  
|37|**OwnerName**|Owner name of the referenced object.|  
|38|**RoleName**|Name of the database or server-wide role targeted by a statement.|  
|39|**TargetUserName**|User name of the target of some action.|  
|40|**DBUserName**|[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database user name of the client.|  
|41|**LoginSid**|Security identifier (SID) of the logged-in user.|  
|42|**TargetLoginName**|Login name of the target of some action.|  
|43|**TargetLoginSid**|SID of the login that is the target of some action.|  
|44|**ColumnPermissions**|Column-level permissions status; used by Security Auditing.|  
|45|**LinkedServerName**|Name of the linked server.|  
|46|**ProviderName**|Name of the OLE DB provider.|  
|47|**MethodName**|Name of the OLE DB method.|  
|48|**RowCounts**|Number of rows in the batch.|  
|49|**RequestID**|ID of the request containing the statement.|  
|50|**XactSequence**|A token to describe the current transaction.|  
|51|**EventSequence**|Sequence number for this event.|  
|52|**BigintData1**|**bigint** value, which is dependent on the event class captured in the trace.|  
|53|**BigintData2**|**bigint** value, which is dependent on the event class captured in the trace.|  
|54|**GUID**|GUID value, which is dependent on the event class captured in the trace.|  
|55|**IntegerData2**|Integer value, which is dependent on the event class captured in the trace.|  
|56|**ObjectID2**|ID of the related object or entity, if available.|  
|57|**Type**|Integer value, which is dependent on the event class captured in the trace.|  
|58|**OwnerID**|Type of the object that owns the lock. For lock events only.|  
|59|**ParentName**|Name of the schema the object is within.|  
|60|**IsSystem**|Indicates whether the event occurred on a system process or a user process.<br /><br /> **1** = system<br /><br /> **0** = user.|  
|61|**Offset**|Starting offset of the statement within the stored procedure or batch.|  
|62|**SourceDatabaseID**|ID of the database in which the source of the object exists.|  
|63|**SqlHandle**|64-bit hash based on the text of an ad hoc query or the database and object ID of an SQL object. This value can be passed to **sys.dm_exec_sql_text()** to retrieve the associated SQL text.|  
|64|**SessionLoginName**|The login name of the user who originated the session. For example, if you connect to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] using **Login1** and execute a statement as **Login2**, **SessionLoginName** displays **Login1**, while **LoginName** displays **Login2**. This data column displays both [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and Windows logins.|  
  
 **[ @on=]** *on*  
 Specifies whether to turn the event ON (1) or OFF (0). *on* is **bit**, with no default.  
  
 If *on* is set to **1**, and *column_id* is NULL, then the event is set to ON and all columns are cleared. If *column_id* is not null, then the column is set to ON for that event.  
  
 If *on* is set to **0**, and *column_id* is NULL, then the event is turned OFF and all columns are cleared. If *column_id* is not null, then the column is turned OFF.  
  
 This table illustrates the interaction between **@on** and **@columnid**.  
  
|@on|@columnid|Result|  
|---------|---------------|------------|  
|ON (**1**)|NULL|Event is turned ON.<br /><br /> All Columns are cleared.|  
||NOT NULL|Column is turned ON for the specified Event.|  
|OFF (**0**)|NULL|Event is turned OFF.<br /><br /> All Columns are cleared.|  
||NOT NULL|Column is turned OFF for the specified Event.|  
  
## Return Code Values  
 The following table describes the code values that users may get following completion of the stored procedure.  
  
|Return code|Description|  
|-----------------|-----------------|  
|0|No error.|  
|1|Unknown error.|  
|2|The trace is currently running. Changing the trace at this time will result in an error.|  
|3|The specified event is not valid. The event may not exist or it is not an appropriate one for the store procedure.|  
|4|The specified column is not valid.|  
|9|The specified trace handle is not valid.|  
|11|The specified column is used internally and cannot be removed.|  
|13|Out of memory. Returned when there is not enough memory to perform the specified action.|  
|16|The function is not valid for this trace.|  
  
## Remarks  
 **sp_trace_setevent** performs many of the actions previously executed by extended stored procedures available in earlier versions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. Use **sp_trace_setevent** instead of the following:  
  
-   **xp_trace_addnewqueue**  
  
-   **xp_trace_eventclassrequired**  
  
-   **xp_trace_seteventclassrequired**  
  
 Users must execute **sp_trace_setevent** for each column added for each event. During each execution, if **@on** is set to **1**, **sp_trace_setevent** adds the specified event to the list of events of the trace. If **@on** is set to **0**, **sp_trace_setevent** removes the specified event from the list.  
  
 Parameters of all SQL Trace stored procedures (**sp_trace_xx**) are strictly typed. If these parameters are not called with the correct input parameter data types, as specified in the argument description, the stored procedure will return an error.  
  
 For an example of using trace stored procedures, see [Create a Trace &#40;Transact-SQL&#41;](../../relational-databases/sql-trace/create-a-trace-transact-sql.md).  
  
## Permissions  
 User must have ALTER TRACE permission.  
  
## See Also  
 [sys.fn_trace_geteventinfo &#40;Transact-SQL&#41;](../../relational-databases/system-functions/sys-fn-trace-geteventinfo-transact-sql.md)   
 [sys.fn_trace_getinfo &#40;Transact-SQL&#41;](../../relational-databases/system-functions/sys-fn-trace-getinfo-transact-sql.md)   
 [sp_trace_generateevent &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-trace-generateevent-transact-sql.md)   
 [SQL Server Event Class Reference](../../relational-databases/event-classes/sql-server-event-class-reference.md)   
 [SQL Trace](../../relational-databases/sql-trace/sql-trace.md)  
  
  
