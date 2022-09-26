---
author: MashaMSFT
ms.author: mathoma
ms.reviewer: randolphwest
ms.date: 08/19/2022
ms.topic: include
---
| Error| Severity | Event Logged | Description|
| :------ | :------| :------| :----------------------------- |
|    1001    |    16    |    No    |    Line %d: Length or precision specification %d is invalid.    |
|    1002    |    16    |    No    |    Line %d: Specified scale %d is invalid.    |
|    1003    |    15    |    No    |    Line %d: %ls clause allowed only for %ls.    |
|    1004    |    16    |    No    |    Invalid column prefix '%.*ls': No table name specified    |
|    1005    |    15    |    No    |    Line %d: Invalid procedure number (%d). Must be between 1 and 32767.    |
|    1006    |    15    |    No    |    CREATE TRIGGER contains no statements.    |
|    1007    |    15    |    No    |    The %S_MSG '%.*ls' is out of the range for numeric representation (maximum precision 38).    |
|    1008    |    15    |    No    |    The SELECT item identified by the ORDER BY number %d contains a variable as part of the expression identifying a column position. Variables are only allowed when ordering by an expression referencing a column name.    |
|    1009    |    16    |    No    |    The keyword DEFAULT is not allowed in DBCC commands.    |
|    1010    |    15    |    No    |    Invalid escape character '%.*ls'.    |
|    1011    |    15    |    No    |    The correlation name '%.*ls' is specified multiple times in a FROM clause.    |
|    1012    |    15    |    No    |    The correlation name '%.*ls' has the same exposed name as table '%.*ls'.    |
|    1013    |    15    |    No    |    The objects "%.*ls" and "%.*ls" in the FROM clause have the same exposed names. Use correlation names to distinguish them.    |
|    1014    |    15    |    No    |    TOP clause contains an invalid value.    |
|    1015    |    15    |    No    |    An aggregate cannot appear in an ON clause unless it is in a subquery contained in a HAVING clause or select list, and the column being aggregated is an outer reference.    |
|    1016    |    15    |    No    |    Outer join operators cannot be specified in a query containing joined tables.    |
|    1018    |    15    |    No    |    Incorrect syntax near '%.*ls'. If this is intended as a part of a table hint, A WITH keyword and parenthesis are now required. See SQL Server Books Online for proper syntax.    |
|    1019    |    15    |    No    |    Invalid column list after object name in GRANT/REVOKE statement.    |
|    1020    |    15    |    No    |    Sub-entity lists (such as column or security expressions) cannot be specified for entity-level permissions.    |
|    1021    |    10    |    No    |    FIPS Warning: Line %d has the non-ANSI statement '%ls'.    |
|    1022    |    10    |    No    |    FIPS Warning: Line %d has the non-ANSI clause '%ls'.    |
|    1023    |    15    |    No    |    Invalid parameter %d specified for %ls.    |
|    1024    |    10    |    No    |    FIPS Warning: Line %d has the non-ANSI function '%ls'.    |
|    1025    |    10    |    No    |    FIPS Warning: The length of identifier '%.*ls' exceeds 18.    |
|    1026    |    16    |    No    |    GOTO cannot be used to jump into a TRY or CATCH scope.    |
|    1028    |    15    |    No    |    The CUBE, ROLLUP, and GROUPING SETS constructs are not allowed in a GROUP BY ALL clause.    |
|    1029    |    15    |    No    |    Browse mode is invalid for subqueries and derived tables.    |
|    1030    |    16    |    No    |    Only constants are allowed here. Time literal is not permitted because it refers to the current date.    |
|    1031    |    15    |    No    |    Percent values must be between 0 and 100.    |
|    1032    |    16    |    No    |    Cannot use the column prefix '%.*ls'. This must match the object in the UPDATE clause '%.*ls'.    |
|    1033    |    16    |    No    |    The ORDER BY clause is invalid in views, inline functions, derived tables, subqueries, and common table expressions, unless TOP or FOR XML is also specified.    |
|    1034    |    15    |    No    |    Syntax error: Duplicate specification of the action "%.*s" in the trigger declaration.    |
|    1035    |    15    |    No    |    Incorrect syntax near '%.*ls', expected '%.*ls'.    |
|    1036    |    15    |    No    |    File option %hs is required in this CREATE/ALTER DATABASE statement.    |
|    1037    |    15    |    No    |    The CASCADE, WITH GRANT or AS options cannot be specified with statement permissions.    |
|    1038    |    15    |    No    |    An object or column name is missing or empty. For SELECT INTO statements, verify each column has a name. For other statements, look for empty alias names. Aliases defined as "" or [] are not allowed. Change the alias to a valid name.    |
|    1039    |    16    |    No    |    Option '%.*ls' is specified more than once.    |
|    1041    |    15    |    No    |    Option %.*ls is not allowed for a LOG file.    |
|    1042    |    15    |    No    |    Conflicting %ls optimizer hints specified.    |
|    1043    |    16    |    No    |    '%hs' is not yet implemented.    |
|    1044    |    15    |    No    |    Cannot use an existing function name to specify a stored procedure name.    |
|    1045    |    15    |    No    |    Aggregates are not allowed in this context. Only scalar expressions are allowed.    |
|    1046    |    15    |    No    |    Subqueries are not allowed in this context. Only scalar expressions are allowed.    |
|    1047    |    15    |    No    |    Conflicting locking hints specified.    |
|    1048    |    15    |    No    |    Conflicting cursor options %ls and %ls.    |
|    1049    |    15    |    No    |    Mixing old and new syntax to specify cursor options is not allowed.    |
|    1050    |    15    |    No    |    This syntax is only allowed for parameterized queries.    |
|    1051    |    15    |    No    |    Cursor parameters in a stored procedure must be declared with OUTPUT and VARYING options, and they must be specified in the order CURSOR VARYING OUTPUT.    |
|    1052    |    15    |    No    |    Conflicting %ls options "%ls" and "%ls".    |
|    1053    |    15    |    No    |    For DROP STATISTICS, you must provide both the object (table or view) name and the statistics name, in the form "objectname.statisticsname".    |
|    1054    |    15    |    No    |    Syntax '%ls' is not allowed in schema-bound objects.    |
|    1055    |    15    |    No    |    '%.*ls' is an invalid name because it contains a NULL character or an invalid unicode character.    |
|    1056    |    15    |    No    |    The number of elements in the select list exceeds the maximum allowed number of %d elements.    |
|    1057    |    15    |    No    |    The IDENTITY function cannot be used with a SELECT INTO statement containing a UNION, INTERSECT or EXCEPT operator.    |
|    1058    |    15    |    No    |    Cannot specify both READ_ONLY and FOR READ ONLY on a cursor declaration.    |
|    1059    |    15    |    No    |    Cannot set or reset the 'parseonly' option within a procedure or function.    |
|    1060    |    15    |    No    |    The number of rows in the TOP clause must be an integer.    |
|    1061    |    16    |    No    |    The text/ntext/image constants are not yet implemented.    |
|    1062    |    16    |    No    |    The TOP N WITH TIES clause is not allowed without a corresponding ORDER BY clause.    |
|    1063    |    16    |    No    |    A filegroup cannot be added using ALTER DATABASE ADD FILE. Use ALTER DATABASE ADD FILEGROUP.    |
|    1064    |    16    |    No    |    A filegroup cannot be used with log files.    |
|    1065    |    15    |    No    |    The NOLOCK and READUNCOMMITTED lock hints are not allowed for target tables of INSERT, UPDATE, DELETE or MERGE statements.    |
|    1066    |    10    |    No    |    Warning. Line %d: The option '%ls' is obsolete and has no effect.    |
|    1067    |    15    |    No    |    The SET SHOWPLAN statements must be the only statements in the batch.    |
|    1068    |    16    |    No    |    Only one list of index hints per table is allowed.    |
|    1069    |    16    |    No    |    Index hints are only allowed in a FROM or OPTION clause.    |
|    1070    |    15    |    No    |    CREATE INDEX option '%.*ls' is no longer supported.    |
|    1071    |    16    |    No    |    Cannot specify a JOIN algorithm with a remote JOIN.    |
|    1072    |    16    |    No    |    A REMOTE hint can only be specified with an INNER JOIN clause.    |
|    1073    |    15    |    No    |    '%.*ls' is not a recognized cursor option for cursor %.*ls.    |
|    1074    |    15    |    No    |    Creation of temporary functions is not allowed.    |
|    1075    |    15    |    No    |    RETURN statements in scalar valued functions must include an argument.    |
|    1076    |    15    |    No    |    Function '%s' requires at least %d argument(s).    |
|    1077    |    15    |    No    |    INSERT into an identity column not allowed on table variables.    |
|    1078    |    15    |    No    |    '%.*ls %.*ls' is not a recognized option.    |
|    1079    |    15    |    No    |    A variable cannot be used to specify a search condition in a fulltext predicate when accessed through a cursor.    |
|    1080    |    15    |    No    |    The integer value %.*ls is out of range.    |
|    1081    |    16    |    No    |    %s does not allow specifying the database name as a prefix to the assembly name.    |
|    1082    |    15    |    No    |    %.*ls does not support synchronous trigger registration.    |
|    1083    |    15    |    No    |    OWNER is not a valid option for EXECUTE AS in the context of server and database level triggers.    |
|    1084    |    15    |    No    |    '%.*ls' is an invalid event type.    |
|    1085    |    15    |    No    |    '%.*ls' event type does not support event notifications.    |
|    1086    |    16    |    No    |    The FOR XML clause is invalid in views, inline functions, derived tables, and subqueries when they contain a set operator. To work around, wrap the SELECT containing a set operator using derived table syntax and apply FOR XML on top of it.    |
|    1087    |    15    |    No    |    Must declare the table variable "%.*ls".    |
|    1088    |    15    |    No    |    Cannot find the object "%.*ls" because it does not exist or you do not have permissions.    |
|    1089    |    15    |    No    |    The SET FMTONLY OFF statement must be the last statement in the batch.    |
|    1090    |    15    |    No    |    Invalid default for parameter %d.    |
|    1091    |    15    |    No    |    The option "%ls" is not valid for this function.    |
|    1092    |    16    |    No    |    In this context %d statistics name(s) cannot be specified for option '%ls'.    |
|    1093    |    16    |    No    |    %.*ls is not a valid broker name.    |
|    1094    |    15    |    No    |    Cannot specify a schema name as a prefix to the trigger name for database and server level triggers.    |
|    1095    |    15    |    No    |    %.*ls has already been specified as an event type.    |
|    1096    |    15    |    No    |    Default parameter values for CLR types, nvarchar(max), varbinary(max), and xml are not supported.    |
|    1097    |    15    |    No    |    Cannot use If UPDATE within this CREATE TRIGGER statement.    |
|    1098    |    15    |    No    |    The specified event type(s) is/are not valid on the specified target object.    |
|    1099    |    15    |    No    |    The ON clause is not valid for this statement.    |
|    [1101](../../relational-databases/errors-events/mssqlserver-1101-database-engine-error.md)    |    17    |    Yes    |    Could not allocate a new page for database '%.*ls' because of insufficient disk space in filegroup '%.*ls'. Create the necessary space by dropping objects in the filegroup, adding additional files to the filegroup, or setting autogrowth on for existing files in the filegroup.    |
|    [1105](../../relational-databases/errors-events/mssqlserver-1105-database-engine-error.md)    |    17    |    Yes    |    Could not allocate space for object '%.*ls'%.*ls in database '%.*ls' because the '%.*ls' filegroup is full. Create disk space by deleting unneeded files, dropping objects in the filegroup, adding additional files to the filegroup, or setting autogrowth on for existing files in the filegroup.    |
|    1119    |    16    |    No    |    Removing IAM page %S_PGID failed because someone else is using the object that this IAM page belongs to.    |
|    1121    |    17    |    No    |    Space allocator cannot allocate page in database %d.    |
|    1122    |    14    |    No    |    Table error: Page %S_PGID. Test (%hs) failed. Address 0x%x is not aligned.    |
|    1123    |    14    |    No    |    Table error: Page %S_PGID. Unexpected page type %d.    |
|    1124    |    14    |    No    |    Table error: Page %S_PGID. Test (%hs) failed. Slot %d, offset 0x%x is invalid.    |
|    1125    |    14    |    No    |    Table error: Page %S_PGID. Test (%hs) failed. Slot %d, row extends into free space at 0x%x.    |
|    1126    |    14    |    No    |    Table error: Page %S_PGID. Test (%hs) failed. Slot %d, offset 0x%x overlaps with the prior row.    |
|    1127    |    14    |    No    |    Table error: Page %S_PGID. Test (%hs) failed. Values are %ld and %ld.    |
|    1128    |    14    |    No    |    Table error: Page %S_PGID, row %d. Test (%hs) failed. Values are %ld and %ld.    |
|    1129    |    16    |    No    |    Could not cleanup deferred deallocations from filegroup '%.*ls'.    |
|    1130    |    10    |    Yes    |    Error while allocating extent for a worktable. Extent %S_PGID in TEMPDB may have been lost.    |
|    1131    |    10    |    Yes    |    Failed to truncate AppendOnlyStorageUnit 0x%p. Will retry next time. This is an informational message only. No user action is required.    |
|    1202    |    16    |    No    |    The database-principal '%.*ls' does not exist or user is not a member.    |
|    [1203](../../relational-databases/errors-events/mssqlserver-1203-database-engine-error.md)    |    20    |    Yes    |    Process ID %d attempted to unlock a resource it does not own: %.*ls. Retry the transaction, because this error may be caused by a timing condition. If the problem persists, contact the database administrator.    |
|    [1204](../../relational-databases/errors-events/mssqlserver-1204-database-engine-error.md)    |    19    |    Yes    |    The instance of the SQL Server Database Engine cannot obtain a LOCK resource at this time. Rerun your statement when there are fewer active users. Ask the database administrator to check the lock and memory configuration for this instance, or to check for long-running transactions.    |
|    [1205](../../relational-databases/errors-events/mssqlserver-1205-database-engine-error.md)    |    13    |    No    |    Transaction (Process ID %d) was deadlocked on %.*ls resources with another process and has been chosen as the deadlock victim. Rerun the transaction.    |
|    1206    |    18    |    No    |    The Microsoft Distributed Transaction Coordinator (MS DTC) has cancelled the distributed transaction.    |
|    1207    |    10    |    Yes    |    Can't allocate %u locks on startup, reverting to %u and turning on dynamic lock allocation. Maximum allowed memory usage at startup is %I64u KB.    |
|    1208    |    21    |    Yes    |    Could not allocate initial %u lock blocks during startup. Can not start the server.    |
|    1209    |    21    |    Yes    |    Could not allocate initial %u lock owner blocks during startup. Can not start the server.    |
|    1210    |    21    |    Yes    |    Unable to allocate lock owner block during lock migration. Server halted.    |
|    1212    |    10    |    Yes    |    Lock not logged: %-30ls Mode: %s    |
|    1213    |    21    |    Yes    |    Error spawning Lock Monitor thread: %ls    |
|    1214    |    17    |    Yes    |    Internal Error. There are too many parallel transactions.    |
|    1220    |    17    |    No    |    No more lock classes available from transaction.    |
|    1221    |    20    |    Yes    |    The Database Engine is attempting to release a group of locks that are not currently held by the transaction. Retry the transaction. If the problem persists, contact your support provider.    |
|    [1222](../../relational-databases/errors-events/mssqlserver-1222-database-engine-error.md)    |    16    |    No    |    Lock request time out period exceeded.    |
|    1223    |    16    |    No    |    Cannot release the application lock (Database Principal: '%.*ls', Resource: '%.*ls') because it is not currently held.    |
|    1224    |    16    |    No    |    An invalid application lock resource was passed to %ls.    |
|    1225    |    16    |    No    |    An invalid application lock mode was passed to %ls.    |
|    1226    |    16    |    No    |    An invalid application lock owner was passed to %ls.    |
|    1227    |    16    |    No    |    An invalid application lock time-out was passed to %ls.    |
|    1228    |    16    |    No    |    An invalid parameter "%ls" was passed to the application lock function or procedure.    |
|    1230    |    16    |    No    |    An invalid database principal was passed to %ls.    |
|    [1401](../../relational-databases/errors-events/mssqlserver-1401-database-engine-error.md)    |    21    |    Yes    |    Startup of the database-mirroring master thread routine failed for the following reason: %ls. Correct the cause of this error, and restart the SQL Server service.    |
|    1402    |    20    |    Yes    |    Witness did not find an entry for database mirroring GUID {%.8x-%.4x-%.4x-%.2x%.2x-%.2x%.2x%.2x%.2x%.2x%.2x}. A configuration mismatch exists. Retry the command, or reset the witness from one of the database mirroring partners.    |
|    1403    |    16    |    Yes    |    The witness for the mirroring session received error response %d (state %d) from server instance %.*ls for database %.*ls. For more information about the error, refer to the error log on this server instance and the partner server instance.    |
|    1404    |    16    |    No    |    The command failed because the database mirror is busy. Reissue the command later.    |
|    1405    |    16    |    No    |    The database "%.*ls" is already enabled for database mirroring.    |
|    [1406](../../relational-databases/errors-events/mssqlserver-1406-database-engine-error.md)    |    16    |    No    |    Unable to force service safely. Remove database mirroring and recover database "%.*ls" to gain access.    |
|    1407    |    16    |    No    |    The remote copy of database "%.*ls" is not related to the local copy of the database.    |
|    1408    |    16    |    No    |    The remote copy of database "%.*ls" is not recovered far enough to enable database mirroring.    |
|    1409    |    16    |    No    |    Database mirroring cannot be enabled for the remote copy of database "%.*ls". Restore database logs to the remote copy.    |
|    1410    |    16    |    No    |    The remote copy of database "%.*ls" is already enabled for database mirroring.    |
|    1411    |    16    |    No    |    The remote copy of database "%.*ls" has not had enough log backups applied to roll forward all of its files to a common point in time.    |
|    1412    |    16    |    No    |    The remote copy of database "%.*ls" has not been rolled forward to a point in time that is encompassed in the local copy of the database log.    |
|    1413    |    16    |    Yes    |    Communications to the remote server instance '%.*ls' failed before database mirroring was fully started. The ALTER DATABASE command failed. Retry the command when the remote database is started.    |
|    1414    |    16    |    No    |    The database is being closed before database mirroring is fully initialized. The ALTER DATABASE command failed.    |
|    1415    |    16    |    No    |    The database mirroring operation for database "%.*ls" failed. The requested operation could not be performed.    |
|    1416    |    16    |    No    |    Database "%.*ls" is not configured for database mirroring.    |
|    1417    |    16    |    No    |    Database mirroring has been disabled by the administrator for database "%.*ls".    |
|    [1418](../../relational-databases/errors-events/mssqlserver-1418-database-engine-error.md)    |    16    |    No    |    The server network address "%.*ls" can not be reached or does not exist. Check the network address name and that the ports for the local and remote endpoints are operational.    |
|    1419    |    16    |    No    |    The remote copy of database "%.*ls" cannot be opened. Check the database name and ensure that it is in the restoring state, and then reissue the command.    |
|    1420    |    16    |    No    |    Database mirroring was unable to obtain the network hostname. Operating system error %ls encountered. Verify the network configuration.    |
|    1421    |    16    |    Yes    |    Communications to the remote server instance '%.*ls' failed to complete before its timeout. The ALTER DATABASE command may have not completed. Retry the command.    |
|    1422    |    16    |    No    |    The mirror server instance is not caught up to the recent changes to database "%.*ls". Unable to fail over.    |
|    1423    |    16    |    No    |    The property name is not supported.    |
|    1424    |    16    |    No    |    The remote server instance has a more recent value for the property '%.*ls'. Property value not changed.    |
|    1425    |    16    |    No    |    The property value is invalid for '%.*ls'. Property value not changed.    |
|    1426    |    16    |    No    |    To issue ALTER DATABASE SET WITNESS, all three server instances must be interconnected, and the mirror database must be caught up. When these conditions are met, reissue the command.    |
|    1427    |    16    |    No    |    The server instance '%.*ls' could not act as the witness. The ALTER DATABASE SET WITNESS command failed.    |
|    1428    |    16    |    No    |    The request is refused because the responding server instance is not in a state to service the request.    |
|    1429    |    16    |    No    |    The witness server instance name must be distinct from both of the server instances that manage the database. The ALTER DATABASE SET WITNESS command failed.    |
|    1430    |    16    |    No    |    Database '%.*ls' is in an unstable state for removing database mirroring, so recovery may fail. Verify the data after recovery.    |
|    1431    |    16    |    No    |    Neither the partner nor the witness server instance for database "%.*ls" is available. Reissue the command when at least one of the instances becomes available.    |
|    1432    |    10    |    No    |    Database mirroring is attempting to repair physical page %S_PGID in database "%.*ls" by requesting a copy from the partner.    |
|    1433    |    16    |    No    |    All three server instances did not remain interconnected for the duration of the ALTER DATABASE SET WITNESS command. There may be no witness associated with the database. Verify the status and when necessary repeat the command.    |
|    1434    |    16    |    Yes    |    Invalid or unexpected database mirroring %ls message of type %d was received from server %ls, database %.*ls.    |
|    1435    |    16    |    Yes    |    %ls received unexpected database mirroring error response: status %u, severity %u, state %u, string %.*ls.    |
|    1436    |    16    |    No    |    The Service Broker ID for the remote copy of database "%.*ls" does not match the ID on the principal server.    |
|    1437    |    16    |    Yes    |    Could not post message '%ls' from server instance '%ls' because there is insufficient memory. Reduce non-essential memory load or increase system memory.    |
|    1438    |    16    |    Yes    |    The server instance %ls rejected configure request; read its error log file for more information. The reason %u, and state %u, can be of use for diagnostics by Microsoft. This is a transient error hence retrying the request is likely to succeed. Correct the cause if any and retry.    |
|    1439    |    16    |    No    |    There is currently no witness associated with database "%.*ls".    |
|    1440    |    10    |    Yes    |    Database mirroring is active with database '%.*ls' as the principal copy. This is an informational message only. No user action is required.    |
|    1441    |    10    |    Yes    |    Database mirroring is active with database '%.*ls' as the mirror copy. This is an informational message only. No user action is required.    |
|    1442    |    10    |    Yes    |    Database mirroring is inactive for database '%.*ls'. This is an informational message only. No user action is required.    |
|    1443    |    10    |    Yes    |    Database mirroring has been terminated for database '%.*ls'. This is an informational message only. No user action is required.    |
|    1444    |    10    |    Yes    |    Bypassing recovery for database '%ls' because it is marked as a mirror database, which cannot be recovered. This is an informational message only. No user action is required.    |
|    1445    |    10    |    Yes    |    Bypassing recovery for database '%ls' because it is marked as an inaccessible database mirroring database. A problem exists with the mirroring session. The session either lacks a quorum or the communications links are broken because of problems with links, endpoint configuration, or permissions (for the server account or security certificate). To gain access to the database, figure out what has changed in the session configuration and undo the change.    |
|    1446    |    10    |    No    |    The "%.*ls" server instance is already acting as the witness.    |
|    1447    |    16    |    No    |    ALTER DATABASE "%.*ls" command cannot be executed until both partner server instances are up, running, and connected. Start the partner and reissue the command.    |
|    1448    |    16    |    No    |    The remote copy of database "%.*ls" does not exist. Check the database name and reissue the command.    |
|    1449    |    16    |    No    |    ALTER DATABASE command failed due to an invalid server connection string.    |
|    1450    |    16    |    No    |    The ALTER DATABASE command failed because the worker thread cannot be created.    |
|    1451    |    16    |    No    |    Database mirroring information was not found in the system table.    |
|    1452    |    16    |    No    |    The partner server instance name must be distinct from the server instance that manages the database. The ALTER DATABASE SET PARTNER command failed.    |
|    1453    |    17    |    Yes    |    '%.*ls', the remote mirroring partner for database '%.*ls', encountered error %d, status %d, severity %d. Database mirroring has been suspended. Resolve the error on the remote server and resume mirroring, or remove mirroring and re-establish the mirror server instance.    |
|    1454    |    17    |    Yes    |    Database mirroring will be suspended. Server instance '%.*ls' encountered error %d, state %d, severity %d when it was acting as a mirroring partner for database '%.*ls'. The database mirroring partners might try to recover automatically from the error and resume the mirroring session. For more information, view the error log for additional error messages.    |
|    1455    |    16    |    No    |    The database mirroring service cannot be forced for database "%.*ls" because the database is not in the correct state to become the principal database.    |
|    1456    |    16    |    Yes    |    The ALTER DATABASE command could not be sent to the remote server instance '%.*ls'. The database mirroring configuration was not changed. Verify that the server is connected, and try again.    |
|    [1457](../../relational-databases/errors-events/mssqlserver-1457-database-engine-error.md)    |    23    |    Yes    |    Synchronization of the mirror database, '%.*ls', was interrupted, leaving the database in an inconsistent state. The ALTER DATABASE command failed. Ensure that the principal database, if available, is back up and online, and then reconnect the mirror server instance and allow the mirror database to finish synchronizing.    |
|    1458    |    17    |    Yes    |    The principal copy of the '%.*ls' database encountered error %d, status %d, severity %d while sending page %S_PGID to the mirror. Database mirroring has been suspended. Try to resolve the error condition, and resume mirroring.    |
|    1459    |    24    |    Yes    |    An error occurred while accessing the database mirroring metadata. Drop mirroring (ALTER DATABASE database_name SET PARTNER OFF) and reconfigure it.    |
|    1460    |    16    |    No    |    The database "%.*ls" is already configured for database mirroring on the remote server. Drop database mirroring on the remote server to establish a new partner.    |
|    [1461](../../relational-databases/errors-events/mssqlserver-1461-database-engine-error.md)    |    10    |    No    |    Database mirroring successfully repaired physical page %S_PGID in database "%.*ls" by obtaining a copy from the partner.    |
|    [1462](../../relational-databases/errors-events/mssqlserver-1462-database-engine-error.md)    |    16    |    No    |    Database mirroring is disabled due to a failed redo operation. Unable to resume.    |
|    1463    |    16    |    No    |    Database mirroring is not available in the edition of this SQL Server instance. See books online for more details on feature support in different SQL Server editions.    |
|    1464    |    16    |    No    |    Database mirroring cannot be enabled for the remote copy of database "%.*ls" because the database is not in a recovering state. The remote database must be restored using WITH NORECOVERY.    |
|    1465    |    16    |    No    |    Database mirroring cannot be enabled because the "%.*ls" database is not in full recovery mode on both partners.    |
|    1466    |    16    |    No    |    Database mirroring cannot be enabled because the "%.*ls" database is read-only on one of the partners.    |
|    1467    |    16    |    No    |    Database mirroring cannot be enabled because the "%.*ls" database is in emergency or suspect mode on one of the partners.    |
|    1468    |    16    |    No    |    The operation cannot be performed on database "%.*ls" because it is involved in a database mirroring session.    |
|    1469    |    16    |    No    |    Database mirroring cannot be enabled because the "%.*ls" database is an auto-close database on one of the partners.    |
|    1470    |    16    |    No    |    The alter database for this partner config values may only be initiated on the current principal server for database "%.*ls".    |
|    1471    |    16    |    No    |    The database mirroring connection terminated. Out of memory sending message for database "%.*ls".    |
|    1472    |    16    |    No    |    The database mirroring connection terminated. Communications error sending message for database "%.*ls".    |
|    1473    |    16    |    No    |    This SQL Server edition does not allow changing the safety level. ALTER DATABASE command failed.    |
|    1474    |    16    |    No    |    Database mirroring connection error %d '%.*ls' for '%.*ls'.    |
|    1475    |    16    |    No    |    Database mirroring cannot be enabled because the "%.*ls" database may have bulk logged changes that have not been backed up. The last log backup on the principal must be restored on the mirror.    |
|    1476    |    16    |    No    |    Database mirroring timeout value %d exceeds the maximum value 32767.    |
|    1477    |    16    |    No    |    The database mirroring safety level must be FULL to manually failover database "%.*ls". Set safety level to FULL and retry.    |
|    1478    |    16    |    No    |    The mirror database, "%.*ls", has insufficient transaction log data to preserve the log backup chain of the principal database. This may happen if a log backup from the principal database has not been taken or has not been restored on the mirror database.    |
|    1479    |    16    |    No    |    The mirroring connection to "%.*ls" has timed out for database "%.*ls" after %d seconds without a response. Check the service and network connections.    |
|    1480    |    10    |    No    |    The mirrored database "%.*ls" is changing roles from "%ls" to "%ls" due to %S_MSG.    |
|    1481    |    10    |    No    |    Database mirroring could not repair physical page %S_PGID in database "%.*ls". The mirroring partner could not be contacted or did not provide a copy of the page. Possible reasons include a lack of network connectivity or that the copy of the page kept by the partner is also corrupted. To learn whether the partners are currently connected, view the mirroring_state_desc column of the sys.database_mirroring catalog view. If they are connected, for information about why the partner could not provide a copy of the page, examine its error log entries from around the time when this message was reported. Try to resolve the error and resume mirroring.    |
|    1485    |    10    |    No    |    Database mirroring has been enabled on this instance of SQL Server.    |
|    1486    |    10    |    No    |    Database Mirroring Transport is disabled in the endpoint configuration.    |
|    1487    |    10    |    No    |    Database mirroring is starting %d parallel redo thread(s) with database '%.*ls' as the mirror copy. This is an informational message only. No user action is required.    |
|    1488    |    16    |    No    |    Database mirroring cannot be enabled because the "%.*ls" database is in single user mode.    |
|    1489    |    10    |    No    |    Database Mirroring is disabled on this server due to error %d. Check the errorlog and configuration for more information.    |
|    1499    |    16    |    Yes    |    Database mirroring error: status %u, severity %u, state %u, string %.*ls.    |
|    1501    |    20    |    Yes    |    Sort failure. Contact Technical Support.    |
|    [1505](../../relational-databases/errors-events/mssqlserver-1505-database-engine-error.md)    |    16    |    No    |    The CREATE UNIQUE INDEX statement terminated because a duplicate key was found for the object name '%.*ls' and the index name '%.*ls'. The duplicate key value is %ls.    |
|    1509    |    20    |    Yes    |    Row comparison failed during sort because of an unknown data type on a key column. Metadata might be corrupt. Contact Technical Support.    |
|    1510    |    17    |    No    |    Sort failed. Out of space or locks in database '%.*ls'.    |
|    1511    |    20    |    Yes    |    Sort cannot be reconciled with transaction log.    |
|    1522    |    20    |    Yes    |    Sort operation failed during an index build. The overwriting of the allocation page in database '%.*ls' was prevented by terminating the sort. Run DBCC CHECKDB to check for allocation and consistency errors. It may be necessary restore the database from backup.    |
|    1523    |    20    |    Yes    |    Sort failure. The incorrect extent could not be deallocated. Contact Technical Support.    |
|    1532    |    20    |    Yes    |    New sort run starting on page %S_PGID found an extent not marked as shared. Retry the transaction. If the problem persists, contact Technical Support.    |
|    1533    |    20    |    Yes    |    Cannot share extent %S_PGID. The correct extents could not be identified. Retry the transaction.    |
|    1534    |    20    |    Yes    |    Extent %S_PGID not found in shared extent directory. Retry the transaction. If the problem persists, contact Technical Support.    |
|    1535    |    20    |    Yes    |    Cannot share extent %S_PGID. Shared extent directory is full. Retry the transaction. If the problem persists, contact Technical Support.    |
|    1537    |    20    |    Yes    |    Cannot suspend a sort that is not in row input phase.    |
|    1538    |    20    |    Yes    |    Cannot insert a row into a sort when the sort is not in row input phase.    |
|    1540    |    16    |    No    |    Cannot sort a row of size %d, which is greater than the allowable maximum of %d. Consider resubmitting the query using the ROBUST PLAN hint.    |
|    1541    |    16    |    No    |    Sort failure. A defective CLR type comparison function is suspected.    |
|    1542    |    10    |    Yes    |    BobMgr::GetBuf: Sort Big Output Buffer write not complete after %d seconds.    |
|    1543    |    10    |    Yes    |    Operating system error '%ls' resulted from attempt to read the following: sort run page %S_PGID, in file '%ls', in database with ID %d. Sort is retrying the read.    |
|    1701    |    16    |    No    |    Creating or altering table '%.*ls' failed because the minimum row size would be %d, including %d bytes of internal overhead. This exceeds the maximum allowable table row size of %d bytes.    |
|    1702    |    16    |    No    |    CREATE TABLE failed because column '%.*ls' in table '%.*ls' exceeds the maximum of %d columns.    |
|    1706    |    16    |    No    |    The system table '%.*ls' can only be created or altered during an upgrade.    |
|    1707    |    16    |    No    |    Cannot specify TEXTIMAGE_ON filegroup for a partitioned table.    |
|    1708    |    10    |    No    |    Warning: The table "%.*ls" has been created, but its maximum row size exceeds the allowed maximum of %d bytes. INSERT or UPDATE to this table will fail if the resulting row exceeds the size limit.    |
|    1709    |    16    |    No    |    Cannot use TEXTIMAGE_ON when a table has no text, ntext, image, varchar(max), nvarchar(max), varbinary(max), xml or large CLR type columns.    |
|    1710    |    10    |    No    |    Cannot use alias type with rule or default bound to it as a column type in table variable or return table definition in table valued function. Type '%.*ls' has a %S_MSG bound to it.    |
|    1711    |    16    |    No    |    Cannot define PRIMARY KEY constraint on column '%.*ls' in table '%.*ls'. The computed column has to be persisted and not nullable.    |
|    1712    |    16    |    No    |    Online index operations can only be performed in Enterprise edition of SQL Server.    |
|    1713    |    16    |    No    |    Cannot execute %ls on/using table '%.*ls' since the table is the target table or part of cascading actions of a currently executing trigger.    |
|    1714    |    16    |    No    |    Alter table failed because unique column IDs have been exhausted for table '%.*ls'.    |
|    1715    |    16    |    No    |    Foreign key '%.*ls' creation failed. Only NO ACTION referential update action is allowed for referencing computed column '%.*ls'.    |
|    1716    |    16    |    No    |    FILESTREAM_ON cannot be specified when a table has no FILESTREAM columns. Remove the FILESTREAM_ON clause from the statement, or add a FILESTREAM column to the table.    |
|    1717    |    16    |    No    |    FILESTREAM_ON cannot be specified together with a partition scheme in the ON clause.    |
|    1718    |    16    |    No    |    Change tracking must be enabled on database '%.*ls' before it can be enabled on table '%.*ls'.    |
|    1719    |    16    |    No    |    FILESTREAM data cannot be placed on an empty filegroup.    |
|    1720    |    16    |    No    |    Cannot drop FILESTREAM filegroup or partition scheme since table '%.*ls' has FILESTREAM columns.    |
|    1721    |    16    |    No    |    Altering table '%.*ls' failed because the row size using vardecimal storage format exceeds the maximum allowed table row size of %d bytes.    |
|    1722    |    16    |    No    |    Cannot %S_MSG %S_MSG '%.*ls' since a partition scheme is not specified for FILESTREAM data.    |
|    1723    |    16    |    No    |    Cannot %S_MSG %S_MSG '%.*ls' since a partition scheme was specified for FILESTREAM data but not for the table.    |
|    1724    |    16    |    No    |    Filegroup '%.*ls' is not a FILESTREAM filegroup or partition scheme of FILESTREAM filegroups.    |
|    1725    |    16    |    No    |    Cannot add FILESTREAM column to %S_MSG '%.*ls' because an INSTEAD OF trigger exists on the %S_MSG.    |
|    1726    |    16    |    No    |    Cannot add FILESTREAM filegroup or partition scheme since table '%.*ls' has a FILESTREAM filegroup or partition scheme already.    |
|    1727    |    16    |    No    |    Cannot create nonclustered index '%.*ls' on table '%.*ls' with the FILESTREAM_ON clause.    |
|    1728    |    16    |    No    |    Cannot create index '%.*ls' on table '%.*ls' because the computed column '%.*ls' uses a FILESTREAM column.    |
|    1729    |    16    |    No    |    Cannot create table '%.*ls' because the partitioning column '%.*ls' uses a FILESTREAM column.    |
|    1730    |    16    |    No    |    Creating or altering compressed table '%.*ls' failed because the uncompressed row size would be %d, including %d bytes of internal overhead. This exceeds the maximum allowable table row size of %d bytes.    |
|    1731    |    16    |    No    |    Cannot create the sparse column '%.*ls' in the table '%.*ls' because an option or data type specified is not valid. A sparse column must be nullable and cannot have the ROWGUIDCOL, IDENTITY, or FILESTREAM properties. A sparse column cannot be of the following data types: text, ntext, image, geometry, geography, or user-defined type.    |
|    1732    |    16    |    No    |    Cannot create the sparse column set '%.*ls' in the table '%.*ls' because a table cannot have more than one sparse column set. Modify the statement so that only one column is specified as COLUMN_SET FOR ALL_SPARSE_COLUMNS.    |
|    1733    |    16    |    No    |    Cannot create the sparse column set '%.*ls' in the table '%.*ls' because a sparse column set must be a nullable xml column. Modify the column definition to allow null values.    |
|    1734    |    16    |    No    |    Cannot create the sparse column set '%.*ls' in the table '%.*ls' because the table already contains one or more sparse columns. A sparse column set cannot be added to a table if the table contains a sparse column.    |
|    1736    |    16    |    No    |    The column '%.*ls' in the table '%.*ls' cannot be referenced in a CHECK constraint or computed column definition because the column is a sparse column set. A sparse column set cannot be referenced in a CHECK constraint or computed column definition.    |
|    1738    |    10    |    No    |    Cannot create table '%.*ls' with only a column set column and without any non-computed columns in the table.    |
|    1750    |    10    |    No    |    Could not create constraint. See previous errors.    |
|    1752    |    16    |    No    |    Column '%.*ls' in table '%.*ls' is invalid for creating a default constraint.    |
|    1753    |    16    |    No    |    Column '%.*ls.%.*ls' is not the same length or scale as referencing column '%.*ls.%.*ls' in foreign key '%.*ls'. Columns participating in a foreign key relationship must be defined with the same length and scale.    |
|    1754    |    16    |    No    |    Defaults cannot be created on columns with an IDENTITY attribute. Table '%.*ls', column '%.*ls'.    |
|    1755    |    16    |    No    |    Defaults cannot be created on columns of data type timestamp. Table '%.*ls', column '%.*ls'.    |
|    1756    |    10    |    No    |    Skipping FOREIGN KEY constraint '%.*ls' definition for temporary table. FOREIGN KEY constraints are not enforced on local or global temporary tables.    |
|    1757    |    16    |    No    |    Column '%.*ls.%.*ls' is not of same collation as referencing column '%.*ls.%.*ls' in foreign key '%.*ls'.    |
|    1758    |    16    |    No    |    Only a single constraint can be added or dropped online with no other operations in the same statement.    |
|    1759    |    16    |    No    |    Computed column '%.*ls' in table '%.*ls' is not allowed to be used in another computed-column definition.    |
|    1760    |    16    |    No    |    Constraints of type %ls cannot be created on columns of type %ls.    |
|    1761    |    16    |    No    |    Cannot create the foreign key "%.*ls" with the SET NULL referential action, because one or more referencing columns are not nullable.    |
|    1762    |    16    |    No    |    Cannot create the foreign key "%.*ls" with the SET DEFAULT referential action, because one or more referencing not-nullable columns lack a default constraint.    |
|    1763    |    16    |    No    |    Cross-database foreign key references are not supported. Foreign key '%.*ls'.    |
|    1764    |    16    |    No    |    Computed Column '%.*ls' in table '%.*ls' is invalid for use in '%ls' because it is not persisted.    |
|    1765    |    16    |    No    |    Foreign key '%.*ls' creation failed. Only NO ACTION and CASCADE referential delete actions are allowed for referencing computed column '%.*ls'.    |
|    1766    |    16    |    No    |    Foreign key references to temporary tables are not supported. Foreign key '%.*ls'.    |
|    1767    |    16    |    No    |    Foreign key '%.*ls' references invalid table '%.*ls'.    |
|    1768    |    16    |    No    |    Foreign key '%.*ls' references object '%.*ls' which is not a user table.    |
|    1769    |    16    |    No    |    Foreign key '%.*ls' references invalid column '%.*ls' in referencing table '%.*ls'.    |
|    1770    |    16    |    No    |    Foreign key '%.*ls' references invalid column '%.*ls' in referenced table '%.*ls'.    |
|    1771    |    16    |    No    |    Cannot create foreign key '%.*ls' because it references object '%.*ls' whose clustered index '%.*ls' is disabled.    |
|    1772    |    16    |    No    |    Foreign key '%.*ls' is not valid. A system table cannot be used in a foreign key definition.    |
|    1773    |    16    |    No    |    Foreign key '%.*ls' has implicit reference to object '%.*ls' which does not have a primary key defined on it.    |
|    1774    |    16    |    No    |    The number of columns in the referencing column list for foreign key '%.*ls' does not match those of the primary key in the referenced table '%.*ls'.    |
|    1775    |    16    |    No    |    Cannot create foreign key '%.*ls' because it references object '%.*ls' whose PRIMARY KEY index '%.*ls' is disabled.    |
|    1776    |    16    |    No    |    There are no primary or candidate keys in the referenced table '%.*ls' that match the referencing column list in the foreign key '%.*ls'.    |
|    1778    |    16    |    No    |    Column '%.*ls.%.*ls' is not the same data type as referencing column '%.*ls.%.*ls' in foreign key '%.*ls'.    |
|    1779    |    16    |    No    |    Table '%.*ls' already has a primary key defined on it.    |
|    1781    |    16    |    No    |    Column already has a DEFAULT bound to it.    |
|    1784    |    16    |    No    |    Cannot create the foreign key '%.*ls' because the referenced column '%.*ls.%.*ls' is a non-persisted computed column.    |
|    [1785](../../relational-databases/errors-events/mssqlserver-1785-database-engine-error.md)    |    16    |    No    |    Introducing FOREIGN KEY constraint '%.*ls' on table '%.*ls' may cause cycles or multiple cascade paths. Specify ON DELETE NO ACTION or ON UPDATE NO ACTION, or modify other FOREIGN KEY constraints.    |
|    1786    |    16    |    No    |    Either column '%.*ls.%.*ls' or referencing column '%.*ls.%.*ls' in foreign key '%.*ls' is a timestamp column. This data type cannot be used with cascading referential integrity constraints.    |
|    1787    |    16    |    No    |    Cannot define foreign key constraint '%.*ls' with cascaded DELETE or UPDATE on table '%.*ls' because the table has an INSTEAD OF DELETE or UPDATE TRIGGER defined on it.    |
|    1788    |    16    |    No    |    Cascading foreign key '%.*ls' cannot be created where the referencing column '%.*ls.%.*ls' is an identity column.    |
|    1789    |    16    |    No    |    Cannot use CHECKSUM(*) in a computed column definition.    |
|    1790    |    16    |    No    |    The name of a user-defined table type cannot start with a number (#) sign.    |
|    1791    |    16    |    No    |    A DEFAULT constraint cannot be created on the column '%.*ls' in the table '%.*ls' because the column is a sparse column or sparse column set. Sparse columns or sparse column sets cannot have a DEFAULT constraint.    |
| [1793](../../relational-databases/errors-events/mssqlserver-1793-database-engine-error.md) | 16   | No  | Cannot drop index '%.*ls' since a partition scheme is not specified for FILESTREAM data.
|    1801    |    16    |    No    |    Database '%.*ls' already exists. Choose a different database name.    |
|    1802    |    16    |    No    |    CREATE DATABASE failed. Some file names listed could not be created. Check related errors.    |
|    [1803](../../relational-databases/errors-events/mssqlserver-1803-database-engine-error.md)    |    17    |    No    |    The CREATE DATABASE statement failed. The primary file must be at least %d MB to accommodate a copy of the model database.    |
|    1806    |    16    |    No    |    CREATE DATABASE failed. The default collation of database '%.*ls' cannot be set to '%.*ls'.    |
|    [1807](../../relational-databases/errors-events/mssqlserver-1807-database-engine-error.md)    |    17    |    No    |    Could not obtain exclusive lock on database '%.*ls'. Retry the operation later.    |
|    1810    |    16    |    No    |    The model database must be updatable before a new database can be created.    |
|    1812    |    16    |    No    |    CREATE DATABASE failed. COLLATE clause cannot be used with the FOR ATTACH option.    |
|    1813    |    16    |    No    |    Could not open new database '%.*ls'. CREATE DATABASE is aborted.    |
|    1814    |    10    |    Yes    |    Could not create tempdb. You may not have enough disk space available. Free additional disk space by deleting other files on the tempdb drive and then restart SQL Server. Check for additional errors in the event log that may indicate why the tempdb files could not be initialized.    |
|    1815    |    16    |    No    |    The %ls property cannot be used with database snapshot files.    |
|    1816    |    16    |    No    |    Database snapshot on the system database %.*ls is not allowed.    |
|    1817    |    16    |    No    |    Only the owner of database "%.*s" or the system administrator can create a database snapshot on it.    |
|    1818    |    16    |    No    |    Primary log file '%ls' is missing and the database was not cleanly shut down so it cannot be rebuilt.    |
|    1819    |    10    |    No    |    Could not create default log file because the name was too long.    |
|    1821    |    16    |    No    |    Cannot create a database snapshot on another database snapshot.    |
|    1822    |    16    |    No    |    The database must be online to have a database snapshot.    |
|    1823    |    16    |    No    |    A database snapshot cannot be created because it failed to start.    |
|    1824    |    16    |    No    |    Cannot attach a database that was being restored.    |
|    1825    |    16    |    No    |    Filegroups and collations cannot be specified for database snapshots.    |
|    1826    |    16    |    No    |    User-defined filegroups are not allowed on "%ls".    |
|    1827    |    16    |    No    |    CREATE DATABASE or ALTER DATABASE failed because the resulting cumulative database size would exceed your licensed limit of %I64d MB per %S_MSG.    |
|    1828    |    16    |    No    |    The logical file name "%.*ls" is already in use. Choose a different name.    |
|    1829    |    16    |    No    |    The FOR ATTACH option requires that at least the primary file be specified.    |
|    1830    |    16    |    No    |    The files '%.*ls' and '%.*ls' are both primary files. A database can only have one primary file.    |
|    1831    |    16    |    No    |    File ONLINE/OFFLINE syntax cannot be used with CREATE DATABASE.    |
|    1832    |    20    |    No    |    Cannot attach the file '%.*ls' as database '%.*ls'.%.*ls    |
|    1833    |    16    |    No    |    File '%ls' cannot be reused until after the next BACKUP LOG operation.    |
|    1834    |    16    |    No    |    The file '%ls' cannot be overwritten. It is being used by database '%.*ls'.    |
|    1835    |    16    |    No    |    Unable to create/attach any new database because the number of existing databases has reached the maximum number allowed: %d.    |
|    1836    |    10    |    No    |    Cannot create the default data files because the name that was supplied is too long.    |
|    1837    |    16    |    No    |    The file name "%ls" is too long to create an alternate stream name.    |
|    1838    |    10    |    No    |    Offline database file(s) have been overwritten while being reverted to online state from a database snapshot. The reverted file might contain invalid pages. Please run database consistency checks to assess the data integrity.    |
|    1839    |    16    |    No    |    Could not create default data files because the name '%ls' is a reserved device name.    |
|    1842    |    16    |    No    |    The file size, max size cannot be greater than 2147483647 in units of a page size. The file growth cannot be greater than 2147483647 in units of both page size and percentage.    |
|    1843    |    10    |    Yes    |    Reverting database '%ls' to the point in time of database snapshot '%ls' with split point LSN %.*ls (0x%ls). This is an informational message only. No user action is required.    |
|    1844    |    16    |    No    |    %ls is not supported on %ls.    |
|    1845    |    16    |    No    |    Cannot find SQL Volume Shadow Copy Service (VSS) Writer in writer metadata document provided by VSS while creating auto-recovered VSS snapshot for online DBCC check.    |
|    1846    |    16    |    No    |    Cannot find SQL Volume Shadow Copy (VSS) Writer component for database '%ls' while creating auto-recovered VSS snapshot for online DBCC check.    |
|    1847    |    16    |    No    |    The current version of the operating system doesn't support auto-recovered Volume Shadow Copy (VSS) snapshots.    |
|    1848    |    16    |    No    |    Volume Shadow Copy Service (VSS) failed to create an auto-recovered snapshot of database '%ls' for online DBCC check.    |
|    1849    |    16    |    No    |    CREATE DATABASE failed because FILESTREAM filegroups were declared and ALLOW_SNAPSHOT_ISOLATION or READ_COMMITTED_SNAPSHOT is set to ON in the model database. Either set ALLOW_SNAPSHOT_ISOLATION and READ_COMMITTED_SNAPSHOT to OFF in the model database, or create the database without declaring any FILESTREAM filegroups, set ALLOW_SNAPSHOT_ISOLATION and READ_COMMITTED_SNAPSHOT to OFF in the new database, and then use ALTER DATABASE to add FILESTREAM filegroups and files.    |
|    1901    |    16    |    No    |    Cannot create index or statistics '%.*ls' on view '%.*ls' because key column '%.*ls' is imprecise, computed and not persisted. Consider removing reference to column in view index or statistics key or changing column to be precise. If column is computed in base table consider marking it PERSISTED there.    |
|    1902    |    16    |    No    |    Cannot create more than one clustered index on %S_MSG '%.*ls'. Drop the existing clustered index '%.*ls' before creating another.    |
|    [1904](../../relational-databases/errors-events/mssqlserver-1904-database-engine-error.md)    |    16    |    No    |    The %S_MSG '%.*ls' on table '%.*ls' has %d column names in %S_MSG key list. The maximum limit for index or statistics key column list is %d.    |
|    1907    |    16    |    No    |    Cannot recreate index '%.*ls'. The new index definition does not match the constraint being enforced by the existing index.    |
|    1908    |    16    |    No    |    Column '%.*ls' is partitioning column of the index '%.*ls'. Partition columns for a unique index must be a subset of the index key.    |
|    1909    |    16    |    No    |    Cannot use duplicate column names in %S_MSG. Column name '%.*ls' listed more than once.    |
|    1910    |    16    |    No    |    Could not create %S_MSG '%.*ls' because it exceeds the maximum of %d allowed per table or view.    |
|    1911    |    16    |    No    |    Column name '%.*ls' does not exist in the target table or view.    |
|    1912    |    16    |    No    |    Could not proceed with index DDL operation on %S_MSG '%.*ls' because it conflicts with another concurrent operation that is already in progress on the object. The concurrent operation could be an online index operation on the same object or another concurrent operation that moves index pages like DBCC SHRINKFILE.    |
|    1913    |    16    |    No    |    The operation failed because an index or statistics with name '%.*ls' already exists on %S_MSG '%.*ls'.    |
|    1914    |    16    |    No    |    Index cannot be created on object '%.*ls' because the object is not a user table or view.    |
|    1915    |    16    |    No    |    Cannot alter a non-unique index with ignore_dup_key index option. Index '%.*ls' is non-unique.    |
|    1916    |    16    |    No    |    CREATE INDEX options %ls and %ls are mutually exclusive.    |
|    1917    |    16    |    No    |    Cannot create, rebuild or drop an index on a local temporary table online. Perform the index operation offline.    |
|    1919    |    16    |    No    |    Column '%.*ls' in table '%.*ls' is of a type that is invalid for use as a key column in an index.    |
|    1921    |    16    |    No    |    Invalid %S_MSG '%.*ls' specified.    |
|    1922    |    16    |    No    |    Filegroup '%.*ls' has no files assigned to it. Tables, indexes, and large object columns cannot be created on this filegroup. Use ALTER DATABASE to add one or more files to the filegroup.    |
|    1924    |    16    |    No    |    Filegroup '%.*ls' is read-only.    |
|    1925    |    16    |    No    |    Cannot convert a clustered index to a nonclustered index by using the DROP_EXISTING option. To change the index type from clustered to nonclustered, delete the clustered index, and then create a nonclustered index by using two separate statements.    |
|    1927    |    16    |    No    |    There are already statistics on table '%.*ls' named '%.*ls'.    |
|    1929    |    16    |    No    |    Statistics cannot be created on object '%.*ls' because the object is not a user table or view.    |
|    1930    |    16    |    No    |    Cannot convert a nonclustered index to a clustered index because a foreign key constraint references the index. Remove the foreign key constraint and then retry the operation.    |
|    1931    |    16    |    No    |    The SQL statement cannot be executed because filegroup '%.*ls' is offline. Use the sys.database_files or sys.master_files catalog view to determine the state of the files in this filegroup and then restore the offline file(s) from backup.    |
|    1934    |    16    |    No    |    %ls failed because the following SET options have incorrect settings: '%.*ls'. Verify that SET options are correct for use with %S_MSG.    |
|    1935    |    16    |    No    |    Cannot create index. Object '%.*ls' was created with the following SET options off: '%.*ls'.    |
|    1937    |    16    |    No    |    Cannot create %S_MSG on view '%.*ls' because it references another view '%.*ls'. Consider expanding referenced view's definition by hand in indexed view definition.    |
|    1938    |    16    |    No    |    Index cannot be created on view '%.*ls' because the underlying object '%.*ls' has a different owner.    |
|    1939    |    16    |    No    |    Cannot create %S_MSG on view '%.*ls' because the view is not schema bound.    |
|    1940    |    16    |    No    |    Cannot create %S_MSG on view '%.*ls'. It does not have a unique clustered index.    |
|    1941    |    16    |    No    |    Cannot create nonunique clustered index on view '%.*ls' because only unique clustered indexes are allowed. Consider creating unique clustered index instead.    |
|    1942    |    16    |    No    |    Cannot create %S_MSG on view '%.*ls'. It contains text, ntext, image, FILESTREAM or xml columns.    |
|    1944    |    16    |    No    |    Index '%.*ls' was not created. This index has a key length of at least %d bytes. The maximum permissible key length is %d bytes.    |
|    1945    |    10    |    No    |    Warning! The maximum key length is %d bytes. The index '%.*ls' has maximum length of %d bytes. For some combination of large values, the insert/update operation will fail.    |
|    1946    |    16    |    No    |    Operation failed. The index entry of length %d bytes for the index '%.*ls' exceeds the maximum length of %d bytes.    |
|    1947    |    16    |    No    |    Cannot create %S_MSG on view "%.*ls". The view contains a self join on "%.*ls".    |
|    1949    |    16    |    No    |    Cannot create %S_MSG on view '%.*ls'. The function '%s' yields nondeterministic results. Use a deterministic system function, or modify the user-defined function to return deterministic results.    |
|    1956    |    16    |    No    |    Cannot create %S_MSG on the '%.*ls' view because it uses the nondeterministic user-defined function '%.*ls'. Remove the reference to the function, or make it deterministic.    |
|    1957    |    16    |    No    |    Cannot create %S_MSG on view '%.*ls' because view uses imprecise user-defined function '%.*ls'. Consider removing reference to function or altering it to be precise.    |
|    1959    |    16    |    No    |    Cannot create an index on a view or computed column because the compatibility level of this database is less than 80. Use sp_dbcmptlevel to raise the compatibility level of the database.    |
|    1961    |    16    |    No    |    Cannot create %S_MSG on view '%.*ls'. The collation cast expression with collation name '%.*ls' is non-deterministic because it is dependent on the operating system.    |
|    1962    |    16    |    No    |    Cannot create %S_MSG on view '%.*ls' because column '%.*ls' that is referenced by the view in the WHERE or GROUP BY clause is imprecise. Consider eliminating the column from the view, or altering the column to be precise.    |
|    1963    |    16    |    No    |    Cannot create %S_MSG on view "%.*ls". The view contains a convert that is imprecise or non-deterministic.    |
|    1964    |    16    |    No    |    Cannot create %S_MSG on view "%.*ls". The view contains an imprecise constant.    |
|    1965    |    16    |    No    |    Cannot create %S_MSG on view "%.*ls". The view contains an imprecise arithmetic operator.    |
|    1966    |    16    |    No    |    Cannot create %S_MSG on view '%.*ls'. The view contains an imprecise aggregate operator.    |
|    1967    |    16    |    No    |    Cannot create a new clustered index on a view online.    |
|    1968    |    16    |    No    |    Cannot convert a nonclustered index to a clustered index online using DROP_EXISTING option.    |
|    1969    |    16    |    No    |    Default FILESTREAM filegroup is not available in database '%.*ls'.    |
|    1970    |    10    |    No    |    Warning: Online index operation on table '%.*ls' will proceed but concurrent access to the table may be limited due to residual lock on the table from a previous operation in the same transaction.    |
|    1971    |    16    |    No    |    Cannot disable index '%.*ls' on table '%.*ls'. Permission denied to disable foreign key '%.*ls' on table '%.*ls' that uses this index.    |
|    1972    |    16    |    No    |    Cannot disable clustered index '%.*ls' on table '%.*ls'. Permission denied to alter the referencing view '%.*ls' while disabling its clustered index.    |
|    1973    |    16    |    No    |    Cannot perform the specified operation on disabled index '%.*ls' on %S_MSG '%.*ls'.    |
|    1974    |    16    |    No    |    Cannot perform the specified operation on %S_MSG '%.*ls' because its clustered index '%.*ls' is disabled.    |
|    1975    |    16    |    No    |    Index '%.*ls' row length exceeds the maximum permissible length of '%d' bytes.    |
|    1976    |    16    |    No    |    Cannot create index or statistics '%.*ls' on view '%.*ls' because cannot verify key column '%.*ls' is precise and deterministic. Consider removing column from index or statistics key, marking column persisted in base table if it is computed, or using non-CLR-derived column in key.    |
|    1977    |    16    |    No    |    Could not create %S_MSG '%.*ls' on table '%.*ls'. Only XML Index can be created on XML column '%.*ls'.    |
|    1978    |    16    |    No    |    Column '%.*ls' in table '%.*ls' is of a type that is invalid for use as a key column in an index or statistics.    |
|    1979    |    16    |    No    |    Cannot use index option ignore_dup_key to alter index '%.*ls' as it enforces a primary or unique constraint.    |
|    1980    |    16    |    No    |    Index cannot be created on computed column '%.*ls' of table '%.*ls' because the underlying object '%.*ls' has a different owner.    |
|    1981    |    10    |    No    |    Warning: The maximum length of the row exceeds the permissible limit of %d bytes. For some combination of large values, the insert/update operation will fail.    |
|    1982    |    16    |    No    |    Cannot create %S_MSG on view '%.*ls' because the view references non-deterministic or imprecise member function '%.*ls' on CLR type '%.*ls'. Consider removing reference to the function or altering the function to behave in a deterministic way. Do not declare a CLR function that behaves non-deterministically to have IsDeterministic=true, because that can lead to index corruption. See Books Online for details.    |
|    1983    |    16    |    No    |    Cannot create %S_MSG on view '%.*ls'. The function '%s' yields imprecise results. Use a precise system function, or modify the user-defined function to return precise results.    |
|    1984    |    16    |    No    |    Index '%.*ls' cannot be created or rebuilt. The specified row length for this index using the vardecimal storage format exceeds the maximum allowed length of '%d' bytes.    |
|    1985    |    16    |    No    |    Cannot create %S_MSG on view '%.*ls'. It contains one or more XML data type methods.    |
|    1986    |    10    |    No    |    Cannot replace non-hypothetical index '%.*ls' with a hypothetical index using the DROP_EXISTING option.    |
|    1987    |    16    |    No    |    Cannot %S_MSG %S_MSG '%.*ls' on %S_MSG '%.*ls' because its %S_MSG is disabled.    |
|    1988    |    16    |    No    |    Cannot rebuild clustered index '%.*ls' online because it is disabled.    |
|    1989    |    16    |    No    |    Cannot enable foreign key constraint '%.*ls' as index '%.*ls' on referenced key is disabled.    |
|    1990    |    16    |    No    |    Cannot define an index on a view with ignore_dup_key index option. Remove ignore_dup_key option and verify that view definition does not allow duplicates, or do not index view.    |
|    1991    |    16    |    No    |    Cannot disable clustered index '%.*ls' on table '%.*ls'. Permission denied to disable foreign key '%.*ls' on table '%.*ls' that references this table.    |
|    1992    |    10    |    No    |    Warning: Foreign key '%.*ls' on table '%.*ls' referencing table '%.*ls' was disabled as a result of disabling the index '%.*ls'.    |
|    1993    |    16    |    No    |    Cannot partition an index on a table variable or return table definition in table valued function.    |
|    1994    |    16    |    No    |    Cannot create or update statistics on view "%.*ls" because both FULLSCAN and NORECOMPUTE options are required.    |
|    1995    |    16    |    No    |    Cannot rebuild hypothetical index '%.*ls' online.    |
|    1996    |    16    |    No    |    Could not create index enforcing primary key constraint '%.*ls' using DROP_EXISTING option because table has an XML or spatial index. Drop the XML or spatial index, create the primary key contstraint, and recreate the XML or spatial index.    |
|    1997    |    16    |    No    |    Could not convert the XML or spatial index '%.*ls' to a relational index by using the DROP_EXISTING option. Drop the XML or spatial index and create a relational index with the same name.    |
|    1998    |    10    |    No    |    Warning: Clustered index '%.*ls' on view '%.*ls' referencing table '%.*ls' was disabled as a result of disabling the index '%.*ls'.    |
|    1999    |    16    |    No    |    Column '%.*ls' in table '%.*ls' is of a type that is invalid for use as included column in an index.    |
