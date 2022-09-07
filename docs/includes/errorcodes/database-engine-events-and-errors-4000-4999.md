---
author: MashaMSFT
ms.author: mathoma
ms.reviewer: randolphwest
ms.date: 08/19/2022
ms.topic: include
---
| Error| Severity | Event Logged | Description|
| :------ | :------| :------| :----------------------------- |
|    4001    |    10    |    No    |    Client sends a sp_reset_connection while there is still pending requests, server is disconnecting.    |
|    4002    |    16    |    No    |    The incoming tabular data stream (TDS) protocol stream is incorrect. The stream ended unexpectedly.    |
|    4004    |    16    |    No    |    Unicode data in a Unicode-only collation or ntext data cannot be sent to clients using DB-Library (such as ISQL) or ODBC version 3.7 or earlier.    |
|    4005    |    16    |    No    |    Cannot update columns from more than one underlying table in a single update call.    |
|    4006    |    16    |    No    |    You cannot delete rows from more than one underlying table in a single delete call.    |
|    4007    |    16    |    No    |    Cannot update or insert column "%.*ls". It may be an expression.    |
|    4008    |    16    |    No    |    The data types varchar(max), nvarchar(max), varbinary(max), and XML cannot be used in the compute clause by client driver versions earlier than SQL Server 2005. Please resubmit the query using a more recent client driver.    |
|    4009    |    16    |    No    |    The incoming tabular data stream (TDS) protocol stream is incorrect. The TDS headers contained errors.    |
|    4010    |    16    |    No    |    The incoming tabular data stream (TDS) protocol stream is incorrect. The Query Notification TDS header contained errors.    |
|    4011    |    16    |    No    |    The incoming tabular data stream (TDS) protocol stream is incorrect. The MARS TDS header contained errors.    |
|    4012    |    16    |    No    |    An invalid tabular data stream (TDS) collation was encountered.    |
|    4013    |    16    |    No    |    The incoming tabular data stream (TDS) protocol stream is incorrect. The multiple active result sets (MARS) TDS header is missing.    |
|    4014    |    20    |    No    |    A fatal error occurred while reading the input stream from the network. The session will be terminated (input error: %d, output error: %d).    |
|    4015    |    16    |    No    |    Language requested in login '%.*ls' is not an official name on this SQL Server. Using server-wide default %.*ls instead.    |
|    4016    |    16    |    No    |    Language requested in 'login %.*ls' is not an official name on this SQL Server. Using user default %.*ls instead.    |
|    4017    |    16    |    No    |    Neither the language requested in 'login %.*ls' nor user default language %.*ls is an official language name on this SQL Server. Using server-wide default %.*ls instead.    |
|    4018    |    16    |    No    |    User default language %.*ls is not an official language name on this SQL Server. Using server-wide default %.*ls instead.    |
|    4019    |    16    |    No    |    Language requested in login '%.*ls' is not an official language name on this SQL Server. Login fails.    |
|    4020    |    16    |    No    |    Default date order '%.*ls' for language %.*ls is invalid. Using mdy instead.    |
|    4021    |    16    |    No    |    Resetting the connection results in a different state than the initial login. The login fails.    |
|    4022    |    16    |    No    |    Bulk load data was expected but not sent. The batch will be terminated.    |
|    4027    |    16    |    No    |    Mount tape for %hs of database '%ls' on tape drive '%ls'.    |
|    4028    |    16    |    No    |    End of tape has been reached. Remove tape '%ls' and mount next tape for %hs of database '%ls'.    |
|    4030    |    10    |    No    |    The medium on device '%ls' expires on %hs and cannot be overwritten.    |
|    4035    |    10    |    No    |    Processed %I64d pages for database '%ls', file '%ls' on file %d.    |
|    4037    |    16    |    No    |    The user-specified MEDIANAME "%.*ls" does not match the MEDIANAME "%ls" of the device "%ls".    |
|    4038    |    16    |    No    |    Cannot find file ID %d on device '%ls'.    |
|    4060    |    11    |    No    |    Cannot open database "%.*ls" requested by the login. The login failed.    |
|    4061    |    11    |    No    |    Neither the database "%.*ls" requested by the login nor the user default database could be opened. The master database is being used instead.    |
|    4062    |    11    |    No    |    Cannot open user default database. Using master database instead.    |
|    4063    |    11    |    No    |    Cannot open database "%.*ls" that was requested by the login. Using the user default database "%.*ls" instead.    |
|    [4064](../../relational-databases/errors-events/mssqlserver-4064-database-engine-error.md)    |    11    |    No    |    Cannot open user default database. Login failed.    |
|    4065    |    16    |    Yes    |    User is trying to use '%.*ls' through ODS, which is not supported any more.    |
|    4066    |    16    |    No    |    Type IDs larger than 65535 cannot be sent to clients shipped in SQL Server 2000 or earlier.    |
|    4067    |    16    |    No    |    CLR type serialization failed because an invalid cookie was specified.    |
|    4068    |    20    |    No    |    sp_resetconnection was sent as part of a remote procedure call (RPC) batch, but it was not the last RPC in the batch. This connection will be terminated.    |
|    4069    |    16    |    No    |    The final value of the output parameter was null, and could not be sent to a 6.5 client expecting the parameter to be non-nullable.    |
|    4070    |    16    |    No    |    More than 255 columns were specified in the COMPUTE clause, and this metadata cannot be sent to a SQL Server version 6.5 client.    |
|    4071    |    10    |    No    |    The XP callback function '%.*ls' failed in extended procedure '%.*ls' because it was executed within an INSERT-EXEC statement which does not allow the extended procedure to send information other than result set.    |
|    4072    |    10    |    No    |    The XP callback function '%.*ls' failed in extended procedure '%.*ls' because the extended procedure is called inside an UDF which doesn't allow sending data.    |
|    4073    |    16    |    No    |    A return value of data type varchar(max), nvarchar(max), varbinary(max), XML or other large object type can not be returned to client driver versions earlier than SQL Server 2005. Please resubmit the query using a more recent client driver.    |
|    4074    |    16    |    No    |    Client drivers do not accept result sets that have more than 65,535 columns.    |
|    4075    |    16    |    No    |    The USE database statement failed because the database collation %.*ls is not recognized by older client drivers. Try upgrading the client operating system or applying a service update to the database client software, or use a different collation. See SQL Server Books Online for more information on changing collations.    |
|    4076    |    16    |    No    |    The ALTER DATABASE statement failed because the database collation %.*ls is not recognized by older client drivers. Try upgrading the client operating system or applying a service update to the database client software, or use a different collation. See SQL Server Books Online for more information on changing collations.    |
|    4077    |    20    |    No    |    The statement failed because the sql_variant value uses collation %.*ls, which is not recognized by older client drivers. Try upgrading the client operating system or applying a service update to the database client software, or use a different collation. See SQL Server Books Online for more information on changing collations.    |
|    4078    |    16    |    No    |    The statement failed because column '%.*ls' (ID=%d) uses collation %.*ls, which is not recognized by older client drivers. Try upgrading the client operating system or applying a service update to the database client software, or use a different collation. See SQL Server Books Online for more information on changing collations.    |
|    4079    |    16    |    No    |    The statement failed due to arithmetic overflow when sending data stream.    |
|    4101    |    16    |    No    |    Aggregates on the right side of an APPLY cannot reference columns from the left side.    |
|    4102    |    15    |    No    |    The READPAST lock hint is only allowed on target tables of UPDATE and DELETE and on tables specified in an explicit FROM clause.    |
|    4103    |    15    |    No    |    %.*ls: Temporary views are not allowed.    |
|    [4104](../../relational-databases/errors-events/mssqlserver-4104-database-engine-error.md)    |    16    |    No    |    The multi-part identifier "%.*ls" could not be bound.    |
|    4105    |    16    |    No    |    User-defined functions, partition functions, and column references are not allowed in expressions in this context.    |
|    4106    |    16    |    No    |    Non-ANSI outer joins (*= and =*) are not allowed when a table that contains a column set is used in a query. Change the query to use ANSI outer joins.    |
|    4107    |    16    |    No    |    Inserting into remote tables or views is not allowed by using the BCP utility or by using BULK INSERT.    |
|    4108    |    16    |    No    |    Windowed functions can only appear in the SELECT or ORDER BY clauses.    |
|    4109    |    16    |    No    |    Windowed functions cannot be used in the context of another windowed function or aggregate.    |
|    4110    |    16    |    No    |    The argument type "%s" is invalid for argument %d of "%s".    |
|    4111    |    16    |    No    |    The CREATE SCHEMA statement should be followed by a name or authorization keyword.    |
|    4112    |    16    |    No    |    The ranking function "%.*ls" must have an ORDER BY clause.    |
|    4113    |    16    |    No    |    %.*ls is not a valid windowing function, and cannot be used with the OVER clause.    |
|    4114    |    16    |    No    |    The function '%.*ls' takes exactly %d argument(s).    |
|    4115    |    16    |    No    |    The reference to column "%.*ls" is not allowed in the argument of the TOP clause. Only references to columns at an outer scope or standalone expressions and subqueries are allowed here.    |
|    4116    |    16    |    No    |    The function 'ntile' takes only a positive int or bigint expression as its input.    |
|    4117    |    16    |    No    |    Cannot retrieve table data for the query operation because the table "%.*ls" schema is being altered too frequently. Because the table "%.*ls" contains a computed column, changes to the table schema require a refresh of all table data. Retry the query operation, and if the problem persists, use SQL Server Profiler to identify what schema-altering operations are occurring.    |
|    4118    |    16    |    No    |    An invalid expression was specified in the FOR UPDATE clause.    |
|    4119    |    16    |    No    |    Default values cannot be assigned to property setters of columns with a CLR type.    |
|    4120    |    16    |    No    |    A user-defined function name cannot be prefixed with a database name in this context.    |
|    4121    |    16    |    No    |    Cannot find either column "%.*ls" or the user-defined function or aggregate "%.*ls", or the name is ambiguous.    |
|    4122    |    16    |    No    |    Remote table-valued function calls are not allowed.    |
|    4124    |    16    |    No    |    The parameters supplied for the batch are not valid.    |
|    4126    |    16    |    No    |    No full-text indexed columns were found.    |
|    4127    |    16    |    No    |    At least one of the arguments to COALESCE must be a typed NULL.    |
|    4128    |    16    |    No    |    An internal error occurred during remote query execution. Contact your SQL Server support professional and provide details about the query you were trying to run.    |
|    4129    |    16    |    No    |    The inline function "%.*ls" cannot take correlated parameters or subqueries because it uses a full-text operator.    |
|    4130    |    16    |    No    |    A duplicate hint was specified for the BULK rowset.    |
|    4131    |    16    |    No    |    A compile-time literal value is specified more than once for the variable "%.*ls" in one or more OPTIMIZE FOR clauses.    |
|    4132    |    16    |    No    |    The value specified for the variable "%.*ls" in the OPTIMIZE FOR clause could not be implicitly converted to that variable's type.    |
|    4133    |    16    |    No    |    Only a scalar expression may be specified as the argument to the RETURN statement.    |
|    4134    |    16    |    Yes    |    Metadata stored on disk for computed column '%.*ls' in table '%.*ls' did not match the column definition. In order to avoid possible index corruption, please drop and recreate this computed column.    |
|    4135    |    16    |    No    |    Synonym '%.*ls' is defined over queue '%.*ls'. Synonyms on queues are not allowed.    |
|    4136    |    16    |    No    |    The hint '%.*ls' cannot be used with the hint '%.*ls'.    |
|    4137    |    16    |    No    |    A format file cannot be specified together with SINGLE_BLOB, SINGLE_CLOB or SINGLE_NCLOB option.    |
|    4138    |    16    |    No    |    Conflicting locking hints are specified for table "%.*ls". This may be caused by a conflicting hint specified for a view.    |
|    4139    |    16    |    No    |    Cannot process the query because it references the common language runtime (CLR) table-valued function "%.*ls" with a hint through view "%.*ls".    |
|    4140    |    15    |    No    |    The READCOMMITTEDLOCK lock hint is not allowed on the target table of an INSERT statement.    |
|    4141    |    16    |    No    |    Nested CLR type updates are not allowed.    |
|    4142    |    16    |    No    |    Aggregates are not allowed in the RECEIVE list.    |
|    4143    |    16    |    No    |    The bulk openrowset provider is not a valid target for %.*ls.    |
|    4144    |    16    |    No    |    The hint '%.*ls' is not allowed when inserting into remote tables.    |
|    4145    |    15    |    No    |    An expression of non-boolean type specified in a context where a condition is expected, near '%.*ls'.    |
|    4146    |    16    |    No    |    Statistics can only be created on columns.    |
|    4147    |    15    |    No    |    The query uses non-ANSI outer join operators ("*=" or "=*"). To run this query without modification, please set the compatibility level for current database to 80, using the SET COMPATIBILITY_LEVEL option of ALTER DATABASE. It is strongly recommended to rewrite the query using ANSI outer join operators (LEFT OUTER JOIN, RIGHT OUTER JOIN). In the future versions of SQL Server, non-ANSI join operators will not be supported even in backward-compatibility modes.    |
|    4148    |    16    |    No    |    XML methods are not allowed in a GROUP BY clause.    |
|    4150    |    16    |    No    |    Hints are not allowed on recursive common table expression (CTE) references. Consider removing hint from recursive CTE reference '%.*ls'.    |
|    4151    |    16    |    No    |    The type of the first argument to NULLIF cannot be the NULL constant because the type of the first argument has to be known.    |
|    4152    |    16    |    No    |    Type "%.*ls" is not a CLR type.    |
|    4153    |    16    |    No    |    Cannot treat data type "%ls" as data type "%ls".    |
|    4154    |    16    |    No    |    UNNEST can only take an expression of type multiset.    |
|    4155    |    15    |    No    |    The SELECT list for the nested INSERT statement can only contain one item.    |
|    4156    |    16    |    No    |    The target of nested insert, nested update, or nested delete must be of type multiset.    |
|    4157    |    16    |    No    |    %.*ls is not a valid property, field, or method.    |
|    4158    |    16    |    No    |    The field "%.*ls" is referenced more than once in the set list, either directly or through a property.    |
|    4159    |    16    |    No    |    Delayed CLR type instances require local base table column as an argument.    |
|    4160    |    16    |    No    |    Could not find suitable key in table '%.*ls' for use in delayed CLR type fetching.    |
|    4161    |    16    |    No    |    Only CLR types are allowed in delayed CLR type fetching.    |
|    4162    |    16    |    No    |    A PROB_MATCH table can only be used inside of a PROB_MATCH query.    |
|    4163    |    16    |    No    |    A GROUP BY clause is required in a PROB_MATCH query.    |
|    4164    |    16    |    No    |    A GROUP BY clause in a PROB_MATCH query can only have key columns, and needs to include all the key columns.    |
|    4165    |    16    |    No    |    The score override argument, if present in one of the subqueries, must be present in all subqueries and must be the same constant and variable.    |
|    4166    |    16    |    No    |    Invalid PROB_MATCH subquery.    |
|    4167    |    16    |    No    |    Multiple PROB_MATCH subqueries can only refer to the same base table.    |
|    4168    |    16    |    No    |    Invalid PROB_MATCH project item in the PROB_MATCH SELECT list.    |
|    4169    |    16    |    No    |    Applying TREAT more than once to the same expression is not allowed in a full-text property reference.    |
|    4170    |    16    |    No    |    The (ANY) specification can only be applied to expressions of type multiset.    |
|    4171    |    16    |    No    |    Alias was not specified for an aggregate in the PROB_MATCH SELECT list.    |
|    4172    |    16    |    No    |    Incorrect use of full-text %s.    |
|    4173    |    16    |    No    |    %.*s is not a valid scoring function name.    |
|    4174    |    16    |    No    |    Delayed CLR type instantiation fetch value query may only reference column of a large object or large value type.    |
|    4175    |    16    |    No    |    Nested updates cannot be performed on CLR types that are not Format.Structured.    |
|    4176    |    16    |    No    |    Too many parameters were specified for FULLTEXTTABLE of type "Simple". The maximum number of parameters is %d.    |
|    4177    |    16    |    No    |    The FROM clause of a PROB_MATCH query must consist of a single derived table.    |
|    4184    |    16    |    No    |    Cannot retrieve table data for the query operation because the table "%.*ls" schema is being altered too frequently. Because the table "%.*ls" contains a filtered index or filtered statistics, changes to the table schema require a refresh of all table data. Retry the query operation, and if the problem persists, use SQL Server Profiler to identify what schema-altering operations are occurring.    |
|    4185    |    16    |    No    |    This action cannot be performed on a system type.    |
|    [4186](../../relational-databases/errors-events/mssqlserver-4186-database-engine-error.md)    |    16    |    No    |    Column '%ls.%.*ls' cannot be referenced in the OUTPUT clause because the column definition contains a subquery or references a function that performs user or system data access. A function is assumed by default to perform data access if it is not schemabound. Consider removing the subquery or function from the column definition or removing the column from the OUTPUT clause.    |
|    4202    |    16    |    No    |    BACKUP LOG is not possible because bulk logged changes exist in the database and one or more filegroups are unavailable.    |
|    4208    |    16    |    No    |    The statement %hs is not allowed while the recovery model is SIMPLE. Use BACKUP DATABASE or change the recovery model using ALTER DATABASE.    |
|    4212    |    16    |    No    |    Cannot back up the log of the master database. Use BACKUP DATABASE instead.    |
|    [4214](../../relational-databases/errors-events/mssqlserver-4214-database-engine-error.md)    |    16    |    No    |    BACKUP LOG cannot be performed because there is no current database backup.    |
|    4215    |    10    |    No    |    The log was not truncated because records at the beginning of the log are pending replication or Change Data Capture. Ensure the Log Reader Agent or capture job is running or use sp_repldone to mark transactions as distributed or captured.    |
|    4217    |    10    |    No    |    BACKUP LOG cannot modify the database because the database is read-only. The backup will continue, although subsequent backups will duplicate the work of this backup.    |
|    4218    |    16    |    No    |    Bulk-logged operations exist in the database. Perform a BACKUP LOG.    |
|    4302    |    16    |    No    |    The option "%ls" conflicts with online restore. Remove the conflicting option and reissue the command.    |
|    4303    |    16    |    No    |    The roll forward start point is now at log sequence number (LSN) %.*s. Additional roll forward past LSN %.*s is required to complete the restore sequence.    |
|    4305    |    16    |    No    |    The log in this backup set begins at LSN %.*ls, which is too recent to apply to the database. An earlier log backup that includes LSN %.*ls can be restored.    |
|    4307    |    16    |    No    |    The online restore to database '%ls' failed. It may be appropriate to perform an offline restore instead. To force an offline restore, first take the database offline using the ALTER DATABASE statement.    |
|    4308    |    10    |    No    |    The online restore is complete, but WITH NORECOVERY was specified. Use RESTORE WITH RECOVERY to bring affected data online.    |
|    4309    |    16    |    No    |    The state of file "%ls" prevents restoring individual pages. Only a file restore is currently possible.    |
|    4310    |    16    |    No    |    RESTORE PAGE is not allowed on file "%ls" because the file is not online.    |
|    4311    |    16    |    No    |    RESTORE PAGE is not allowed from backups taken with earlier versions of SQL Server.    |
|    4312    |    16    |    No    |    This log cannot be restored because a gap in the log chain was created. Use more recent data backups to bridge the gap.    |
|    4315    |    10    |    No    |    Some files still require more restore steps before the online restore sequence can be completed.    |
|    4318    |    16    |    No    |    File '%ls' has been rolled forward to LSN %.*ls. This log terminates at LSN %.*ls, which is too early to apply the WITH RECOVERY option. Reissue the RESTORE LOG statement WITH NORECOVERY.    |
|    4319    |    16    |    No    |    A previous restore operation was interrupted and did not complete processing on file '%ls'. Either restore the backup set that was interrupted or restart the restore sequence.    |
|    4320    |    16    |    No    |    The file "%ls" was not fully restored by a database or file restore. The entire file must be successfully restored before applying this backup set.    |
|    4322    |    10    |    No    |    This backup set contains records that were logged before the designated point in time. The database is being left in the restoring state so that more roll forward can be performed.    |
|    4323    |    16    |    No    |    A previous RESTORE WITH CONTINUE_AFTER_ERROR operation left the database in a potentially damaged state. To continue this RESTORE sequence, all further steps must include the CONTINUE_AFTER_ERROR option.    |
|    4326    |    16    |    No    |    The log in this backup set terminates at LSN %.*ls, which is too early to apply to the database. A more recent log backup that includes LSN %.*ls can be restored.    |
|    4327    |    16    |    No    |    The log in this backup set contains bulk-logged changes. Point-in-time recovery was inhibited. The database has been rolled forward to the end of the log.    |
|    4328    |    16    |    No    |    The file "%ls" is missing. Roll forward stops at log sequence number %.*ls. The file is created at log sequence number (LSN) %.*ls, dropped at LSN %.*ls. Restore the transaction log beyond the point in time when the file was dropped, or restore data to be consistent with rest of the database.    |
|    4329    |    10    |    No    |    This log file contains records logged before the designated mark. The database is being left in the Restoring state so you can apply another log file.    |
|    4330    |    16    |    No    |    This backup set cannot be applied because it is on a recovery path that is inconsistent with the database. The recovery path is the sequence of data and log backups that have brought the database to a particular recovery point. Find a compatible backup to restore, or restore the rest of the database to match a recovery point within this backup set, which will restore the database to a different point in time. For more information about recovery paths, see SQL Server Books Online.    |
|    4331    |    16    |    No    |    The database cannot be recovered because the files have been restored to inconsistent points in time.    |
|    4332    |    16    |    No    |    RESTORE LOG has been halted. To use the database in its current state, run RESTORE DATABASE %ls WITH RECOVERY.    |
|    4333    |    16    |    No    |    The database cannot be recovered because the log was not restored.    |
|    4334    |    16    |    No    |    The named mark does not identify a valid LSN.    |
|    4335    |    16    |    No    |    The specified STOPAT time is too early. All or part of the database is already rolled forward beyond that point.    |
|    4336    |    16    |    No    |    The filegroup "%ls" has been dropped and cannot be restored into the online database.    |
|    4337    |    16    |    No    |    The file "%ls" has been dropped and cannot be restored into the online database.    |
|    4338    |    16    |    No    |    The STOPAT clause specifies a point too early to allow this backup set to be restored. Choose a different stop point or use RESTORE DATABASE WITH RECOVERY to recover at the current point.    |
|    4339    |    10    |    No    |    This RESTORE statement successfully performed some actions, but the database could not be brought online because one or more RESTORE steps are needed. Previous messages indicate reasons why recovery cannot occur at this point.    |
|    4340    |    16    |    No    |    The point-in-time clause of this RESTORE statement is restricted for use by RESTORE LOG statements only. Omit the clause or use a clause that includes a timestamp.    |
|    4341    |    16    |    No    |    This log backup contains bulk-logged changes. It cannot be used to stop at an arbitrary point in time.    |
|    4342    |    16    |    No    |    Point-in-time recovery is not possible unless the primary filegroup is part of the restore sequence. Omit the point-in-time clause or restore the primary filegroup.    |
|    4343    |    16    |    No    |    The database has been rolled forward to the end of this backup set and beyond the specified point in time. RESTORE WITH RECOVERY can be used to accept the current recovery point.    |
|    4344    |    16    |    No    |    RESTORE PAGE is not allowed on read-only databases or filegroups.    |
|    4345    |    10    |    No    |    Problems recording information in the msdb..suspect_pages table were encountered. This error does not interfere with any activity except maintenance of the suspect_pages table. Check the error log for more information.    |
|    4346    |    16    |    No    |    RESTORE PAGE is not allowed with databases that use the simple recovery model.    |
|    4347    |    16    |    No    |    The current restore sequence was previously interrupted during the transition to the online state. RESTORE DATABASE WITH RECOVERY can be used to complete the transition to online.    |
|    4348    |    16    |    No    |    The online restore to database '%ls' failed. It may be appropriate to perform an offline restore instead. An offline restore is initiated by using BACKUP LOG WITH NORECOVERY.    |
|    4349    |    16    |    No    |    The log in this backup set begins at LSN %.*ls, which is too recent to apply to the database. This restore sequence needs to initialize the log to start at LSN %.*ls. Reissue the RESTORE LOG statement using an earlier log backup.    |
|    4350    |    16    |    No    |    The list of pages provided with the RESTORE PAGE statement is incorrectly formatted. Prior to the problem %d pages were correctly identified. The problem was hit at character offset %d. Check that all pages are identified by numeric \<file\>:\<page\> pairs with commas separating each pair. For example: PAGE='1:57,2:31'.    |
|    4351    |    16    |    No    |    Backups taken on earlier versions of SQL Server are not supported by fn_dump_dblog.    |
|    4352    |    16    |    No    |    RESTORE LOG is not supported from this data backup because file '%ls' is too old. Use a regular log backup to continue the restore sequence.    |
|    4353    |    16    |    No    |    Conflicting file relocations have been specified for file '%.*ls'. Only a single WITH MOVE clause should be specified for any logical file name.    |
|    4354    |    10    |    Yes    |    The file '%.*ls' of restored database '%ls' is being left in the defunct state because the database is being upgraded from a prior version. Piecemeal restore is not supported when an upgrade is involved.    |
|    4355    |    16    |    No    |    The revert command is incorrectly specified. The RESTORE statement must be of the form: RESTORE DATABASE \<x\> FROM DATABASE_SNAPSHOT = \<y\>.    |
|    4356    |    10    |    No    |    Restore is complete on database '%ls'. The database is now available.    |
|    4357    |    16    |    No    |    Restore cannot take '%ls' offline because changes exist that require a log backup. Take a log backup and then retry the RESTORE.    |
|    4358    |    16    |    No    |    The database can not be brought online because file '%ls' is currently restored to LSN %.*ls but must be restored to LSN %.*ls.    |
|    4359    |    16    |    No    |    The STOPAT option cannot be used with this partial restore sequence because one or more FILESTREAM filegroups are not included. The CONTINUE_AFTER_ERROR option can be used to force the recovery, but this should only be used if you do not intend to subsequently restore the FILESTREAM filegroups.    |
|    4360    |    16    |    No    |    RESTORE LOG WITH CONTINUE_AFTER_ERROR was unsuccessful. Execution of the RESTORE command was aborted.    |
|    4403    |    16    |    No    |    Cannot update the view or function '%.*ls' because it contains aggregates, or a DISTINCT or GROUP BY clause, or PIVOT or UNPIVOT operator.    |
|    4405    |    16    |    No    |    View or function '%.*ls' is not updatable because the modification affects multiple base tables.    |
|    4406    |    16    |    No    |    Update or insert of view or function '%.*ls' failed because it contains a derived or constant field.    |
|    4408    |    19    |    Yes    |    Too many tables. The query and the views or functions in it exceed the limit of %d tables. Revise the query to reduce the number of tables.    |
|    4413    |    16    |    No    |    Could not use view or function '%.*ls' because of binding errors.    |
|    4414    |    16    |    No    |    Could not allocate ancillary table for view or function resolution. The maximum number of tables in a query (%d) was exceeded.    |
|    4415    |    16    |    No    |    View '%.*ls' is not updatable because either it was created WITH CHECK OPTION or it spans a view created WITH CHECK OPTION and the target table is referenced multiple times in the resulting query.    |
|    4416    |    16    |    No    |    UNION ALL view '%.*ls' is not updatable because the definition contains a disallowed construct.    |
|    4417    |    16    |    No    |    Derived table '%.*ls' is not updatable because the definition contains a UNION operator.    |
|    4418    |    16    |    No    |    Derived table '%.*ls' is not updatable because it contains aggregates, or a DISTINCT or GROUP BY clause, or PIVOT or UNPIVOT operator.    |
|    4420    |    16    |    No    |    Derived table '%.*ls' is not updatable because the modification affects multiple base tables.    |
|    4421    |    16    |    No    |    Derived table '%.*ls' is not updatable because a column of the derived table is derived or constant.    |
|    4422    |    16    |    No    |    View '%.*ls' has an INSTEAD OF UPDATE trigger and cannot be a target of an UPDATE FROM statement.    |
|    4423    |    16    |    No    |    View '%.*ls' has an INSTEAD OF DELETE trigger and cannot be a target of a DELETE FROM statement.    |
|    4424    |    16    |    No    |    Joined tables cannot be specified in a query containing outer join operators. View or function '%.*ls' contains joined tables.    |
|    4425    |    16    |    No    |    Cannot specify outer join operators in a query containing joined tables. View or function '%.*ls' contains outer join operators.    |
|    4426    |    16    |    No    |    View '%.*ls' is not updatable because the definition contains a UNION operator.    |
|    4427    |    16    |    No    |    Cannot update the view "%.*ls" because it or a view it references was created with WITH CHECK OPTION and its definition contains the TOP clause.    |
|    4429    |    16    |    No    |    View or function '%.*ls' contains a self-reference. Views or functions cannot reference themselves directly or indirectly.    |
|    4430    |    10    |    No    |    Warning: Index hints supplied for view '%.*ls' will be ignored.    |
|    4431    |    16    |    No    |    Partitioned view '%.*ls' is not updatable because table '%.*ls' has a timestamp column.    |
|    4432    |    16    |    No    |    Partitioned view '%.*ls' is not updatable because table '%.*ls' has a DEFAULT constraint.    |
|    4433    |    16    |    No    |    Cannot INSERT into partitioned view '%.*ls' because table '%.*ls' has an IDENTITY constraint.    |
|    4434    |    16    |    No    |    Partitioned view '%.*ls' is not updatable because table '%.*ls' has an INSTEAD OF trigger.    |
|    4435    |    16    |    No    |    Partitioned view '%.*ls' is not updatable because a value was not specified for partitioning column '%.*ls'.    |
|    4436    |    16    |    No    |    UNION ALL view '%.*ls' is not updatable because a partitioning column was not found.    |
|    4437    |    16    |    No    |    Partitioned view '%.*ls' is not updatable as the target of a bulk operation.    |
|    4438    |    16    |    No    |    Partitioned view '%.*ls' is not updatable because it does not deliver all columns from its member tables.    |
|    4439    |    16    |    No    |    Partitioned view '%.*ls' is not updatable because the source query contains references to partition table '%.*ls'.    |
|    4440    |    16    |    No    |    UNION ALL view '%.*ls' is not updatable because a primary key was not found on table '%.*ls'.    |
|    4441    |    16    |    No    |    Partitioned view '%.*ls' is not updatable because the table '%.*ls' has an index on a computed column.    |
|    4442    |    16    |    No    |    UNION ALL view '%.*ls' is not updatable because base table '%.*ls' is used multiple times.    |
|    4443    |    16    |    No    |    UNION ALL view '%.*ls' is not updatable because column '%.*ls' of base table '%.*ls' is used multiple times.    |
|    4444    |    16    |    No    |    UNION ALL view '%.*ls' is not updatable because the primary key of table '%.*ls' is not included in the union result.    |
|    4445    |    16    |    No    |    UNION ALL view '%.*ls' is not updatable because the primary key of table '%.*ls' is not unioned with primary keys of preceding tables.    |
|    4446    |    16    |    No    |    Cannot update the UNION ALL view "%.*ls" because the definition of column "%.*ls" of view "%.*ls" is used by another view column.    |
|    4447    |    16    |    No    |    View '%.*ls' is not updatable because the definition contains a set operator.    |
|    4448    |    16    |    No    |    Cannot INSERT into partitioned view '%.*ls' because values were not supplied for all columns.    |
|    4449    |    16    |    No    |    Using defaults is not allowed in views that contain a set operator.    |
|    4450    |    16    |    No    |    Cannot update partitioned view '%.*ls' because the definition of the view column '%.*ls' in table '%.*ls' has an IDENTITY constraint.    |
|    4451    |    16    |    No    |    Views referencing tables on multiple servers are not updatable in the edition of this SQL Server instance '%.*ls'. See books online for more details on feature support in different SQL Server editions.    |
|    4452    |    16    |    No    |    Cannot UPDATE partitioning column '%.*ls' of view '%.*ls' because the table '%.*ls' has a CASCADE DELETE or CASCADE UPDATE constraint.    |
|    4453    |    16    |    No    |    Cannot UPDATE partitioning column '%.*ls' of view '%.*ls' because the table '%.*ls' has a INSERT, UPDATE or DELETE trigger.    |
|    4454    |    16    |    No    |    Cannot update the partitioned view "%.*ls" because the partitioning columns of its member tables have mismatched types.    |
|    4456    |    16    |    No    |    The partitioned view "%.*ls" is not updatable because one or more of the non-partitioning columns of its member tables have mismatched types.    |
|    4457    |    16    |    No    |    The attempted insert or update of the partitioned view failed because the value of the partitioning column does not belong to any of the partitions.    |
|    4502    |    16    |    No    |    View or function '%.*ls' has more column names specified than columns defined.    |
|    4503    |    16    |    No    |    Could not create schemabound %S_MSG '%.*ls' because it references an object in another database.    |
|    4504    |    16    |    No    |    Could not create %S_MSG '%.*ls' because CLR type '%.*ls' does not exist in the target database '%.*ls'.    |
|    4505    |    16    |    No    |    CREATE VIEW failed because column '%.*ls' in view '%.*ls' exceeds the maximum of %d columns.    |
|    4506    |    16    |    No    |    Column names in each view or function must be unique. Column name '%.*ls' in view or function '%.*ls' is specified more than once.    |
|    4508    |    16    |    No    |    Views or functions are not allowed on temporary tables. Table names that begin with '#' denote temporary tables.    |
|    4510    |    16    |    No    |    Could not perform CREATE VIEW because WITH %ls was specified and the view is not updatable.    |
|    4511    |    16    |    No    |    Create View or Function failed because no column name was specified for column %d.    |
|    4512    |    16    |    No    |    Cannot schema bind %S_MSG '%.*ls' because name '%.*ls' is invalid for schema binding. Names must be in two-part format and an object cannot reference itself.    |
|    4513    |    16    |    No    |    Cannot schema bind %S_MSG '%.*ls'. '%.*ls' is not schema bound.    |
|    4514    |    16    |    No    |    CREATE FUNCTION failed because a column name is not specified for column %d.    |
|    4515    |    16    |    No    |    CREATE FUNCTION failed because column '%.*ls' in function '%.*ls' exceeds the maximum of %d columns.    |
|    4516    |    16    |    No    |    Cannot schema bind function '%.*ls' because it contains an EXECUTE statement.    |
|    4517    |    16    |    No    |    Service queue object cannot be used in schemabinding expressions. '%.*ls' is a service queue.    |
|    4519    |    16    |    No    |    Cannot %S_MSG %S_MSG '%.*ls' on view '%.*ls' because it is a system generated view that was created for optimization purposes.    |
|    4520    |    16    |    No    |    Cannot disable index on view '%.*ls' because it is a system generated view that was created for optimization purposes.    |
|    4521    |    16    |    No    |    Cannot use object '%.*ls' with autodrop object attribute in schemabinding expressions because it is a system generated view that was created for optimization purposes.    |
|    4522    |    16    |    No    |    Cannot alter view '%.*ls' because it is a system generated view that was created for optimization purposes.    |
|    4523    |    16    |    No    |    Cannot create trigger on view '%.*ls' because it is a system generated view that was created for optimization purposes.    |
|    4602    |    14    |    No    |    Only members of the sysadmin role can grant or revoke the CREATE DATABASE permission.    |
|    4604    |    16    |    No    |    There is no such user or group '%.*ls' or you do not have permission.    |
|    4606    |    16    |    No    |    Granted or revoked privilege %ls is not compatible with object.    |
|    4610    |    16    |    No    |    You can only grant or revoke permissions on objects in the current database.    |
|    4611    |    16    |    No    |    To revoke or deny grantable privileges, specify the CASCADE option.    |
|    4613    |    16    |    No    |    Grantor does not have GRANT permission.    |
|    4615    |    16    |    No    |    Invalid column name '%.*ls'.    |
|    4616    |    16    |    No    |    You cannot perform this operation for the resource database.    |
|    4617    |    16    |    No    |    Cannot grant, deny or revoke permissions to or from special roles.    |
|    4618    |    16    |    No    |    You do not have permission to use %.*ls in the AS clause.    |
|    4619    |    16    |    No    |    CREATE DATABASE permission can only be granted in the master database.    |
|    4620    |    16    |    No    |    All permissions in a grant/deny/revoke statement should be at the same scope (e.g., server or database)    |
|    4621    |    16    |    No    |    Permissions at the server scope can only be granted when the current database is master    |
|    4622    |    16    |    No    |    Permissions at the server scope can only be granted to logins    |
|    4623    |    16    |    No    |    The all permission has been deprecated and is not available for this class of entity    |
|    4624    |    16    |    No    |    Cannot grant, deny, or revoke permissions to sa, dbo, entity owner, information_schema, sys, or yourself.    |
|    4625    |    16    |    No    |    There is no such server principal %.*s or you do not have permission.    |
|    4627    |    16    |    No    |    Cannot grant, deny, or revoke the connect database permission to roles and application roles.    |
|    4628    |    16    |    No    |    The ALL permission is deprecated and maintained only for compatibility. It DOES NOT imply ALL permissions defined on the entity.    |
|    4629    |    16    |    No    |    Permissions on server scoped catalog views or system stored procedures or extended stored procedures can be granted only when the current database is master.    |
|    4701    |    16    |    No    |    Cannot find the object "%.*ls" because it does not exist or you do not have permissions.    |
|    4707    |    16    |    No    |    Could not truncate object '%.*ls' because it or one of its indexes resides on a READONLY filegroup '%.*ls'.    |
|    4708    |    16    |    No    |    Could not truncate object '%.*ls' because it is not a table.    |
|    4709    |    16    |    No    |    You are not allowed to truncate the system table '%.*ls'.    |
|    4710    |    16    |    No    |    Could not truncate object '%.*ls' because it or one of its indexes resides on an offline filegroup '%.*ls'.    |
|    4711    |    16    |    No    |    Cannot truncate table '%.*ls' because it is published for replication or enabled for Change Data Capture.    |
|    4712    |    16    |    No    |    Cannot truncate table '%.*ls' because it is being referenced by a FOREIGN KEY constraint.    |
|    4801    |    16    |    No    |    Insert bulk is not supported over this access protocol.    |
|    4802    |    16    |    No    |    The SINGLE_LOB, SINGLE_CLOB, and SINGLE_NCLOB options are mutually exclusive with all other options.    |
|    4803    |    21    |    Yes    |    The bulk copy (bcp) client has sent a row length of %d. This is not a valid size. The maximum row size is %d. Use a supported client application programming interface (API).    |
|    4804    |    21    |    Yes    |    While reading current row from host, a premature end-of-message was encountered--an incoming data stream was interrupted when the server expected to see more data. The host program may have terminated. Ensure that you are using a supported client application programming interface (API).    |
|    4805    |    17    |    No    |    The front-end tool you are using does not support bulk load from host. Use the supported tools for this command.    |
|    4806    |    16    |    No    |    SINGLE_CLOB requires a double-byte character set (DBCS) (char) input file. The file specified is Unicode.    |
|    4807    |    21    |    Yes    |    The bulk copy (bcp) client sent a row length of %d. This size is not valid. The minimum row size is %d. Use a supported client application programming interface (API).    |
|    4808    |    16    |    No    |    Bulk copy operations cannot trigger bulk load statements.    |
|    4809    |    16    |    No    |    SINGLE_NCLOB requires a UNICODE (widechar) input file. The file specified is not Unicode.    |
|    4810    |    16    |    No    |    Expected the TEXT token in data stream for bulk copy of text or image data.    |
|    4811    |    16    |    No    |    Expected the column offset in data stream for bulk copy of text or image data.    |
|    4812    |    16    |    No    |    Expected the row offset in data stream for bulk copy of text or image data.    |
|    4813    |    16    |    No    |    Expected the text length in data stream for bulk copy of text, ntext, or image data.    |
|    4814    |    16    |    No    |    Bulk copy into a partitioned table is not supported for down-level clients.    |
|    4815    |    16    |    No    |    Received an invalid column length from the bcp client for colid %d.    |
|    4816    |    16    |    No    |    Invalid column type from bcp client for colid %d.    |
|    4817    |    16    |    No    |    Could not bulk load. The sorted column '%.*ls' is not valid. The ORDER hint is ignored.    |
|    4818    |    16    |    No    |    Could not bulk load. The sorted column '%.*ls' was specified more than once. The ORDER hint is ignored.    |
|    4819    |    16    |    No    |    Cannot bulk load. The bulk data stream was incorrectly specified as sorted or the data violates a uniqueness constraint imposed by the target table. Sort order incorrect for the following two rows: primary key of first row: %s, primary key of second row: %s.    |
|    4820    |    16    |    No    |    Cannot bulk load. Unknown version of format file "%s".    |
|    4821    |    16    |    No    |    Cannot bulk load. Error reading the number of columns from the format file "%s".    |
|    4822    |    16    |    No    |    Cannot bulk load. Invalid number of columns in the format file "%s".    |
|    4823    |    16    |    No    |    Cannot bulk load. Invalid column number in the format file "%s".    |
|    4824    |    16    |    No    |    Cannot bulk load. Invalid data type for column number %d in the format file "%s".    |
|    4825    |    16    |    No    |    Cannot bulk load. Invalid prefix for column number %d in the format file "%s".    |
|    4826    |    16    |    No    |    Cannot bulk load. Invalid column length for column number %d in the format file "%s".    |
|    4827    |    16    |    No    |    Cannot bulk load. Invalid column terminator for column number %d in the format file "%s".    |
|    4828    |    16    |    No    |    Cannot bulk load. Invalid destination table column number for source column %d in the format file "%s".    |
|    4829    |    16    |    No    |    Cannot bulk load. Error reading the destination table column name for source column %d in the format file "%s".    |
|    4830    |    10    |    No    |    Bulk load: DataFileType was incorrectly specified as char. DataFileType will be assumed to be widechar because the data file has a Unicode signature.    |
|    4831    |    10    |    No    |    Bulk load: DataFileType was incorrectly specified as widechar. DataFileType will be assumed to be char because the data file does not have a Unicode signature.    |
|    4832    |    16    |    No    |    Bulk load: An unexpected end of file was encountered in the data file.    |
|    4833    |    16    |    No    |    Bulk load: Version mismatch between the provider dynamic link library and the server executable.    |
|    4834    |    16    |    No    |    You do not have permission to use the bulk load statement.    |
|    4835    |    16    |    No    |    Bulk copying into a table with computed columns is not supported for downlevel clients.    |
|    4836    |    10    |    No    |    Warning: Table "%.*s" is published for merge replication. Reinitialize affected subscribers or execute sp_addtabletocontents to ensure that data added is included in the next synchronization.    |
|    4837    |    16    |    No    |    Cannot bulk copy into a table "%.*s" that is enabled for immediate-updating subscriptions.    |
|    4838    |    16    |    No    |    The bulk data source does not support the SQLNUMERIC or SQLDECIMAL data types.    |
|    4839    |    16    |    No    |    Cannot perform the bulk load. Invalid collation name for source column %d in the format file "%s".    |
|    4840    |    16    |    No    |    The bulk data source provider string has an invalid %ls property value %ls.    |
|    4841    |    16    |    No    |    The data source name is not a simple object name.    |
|    4842    |    16    |    No    |    The required FormatFile property is missing from the provider string of the server.    |
|    4843    |    16    |    No    |    The bulk data source provider string has a syntax error ('%lc') near character position %d.    |
|    4844    |    16    |    No    |    The bulk data source provider string has an unsupported property name (%ls).    |
|    4845    |    16    |    No    |    The bulk data source provider string has a syntax error near character position %d. Expected '%lc', but found '%lc'.    |
|    [4846](../../relational-databases/errors-events/mssqlserver-4846-database-engine-error.md)    |    16    |    No    |    The bulk data provider failed to allocate memory.    |
|    4847    |    16    |    No    |    Bulk copying into a table with bigint columns is not supported for versions earlier than SQL Server 2000.    |
|    4848    |    16    |    No    |    Bulk copying into a table with sql_variant columns is not supported for versions earlier than SQL Server 2000.    |
|    4855    |    16    |    No    |    Line %d in format file "%ls": unexpected element "%ls".    |
|    4856    |    16    |    No    |    Line %d in format file "%ls": unexpected info item.    |
|    4857    |    16    |    No    |    Line %d in format file "%ls": Attribute "%ls" could not be specified for this type.    |
|    4858    |    16    |    No    |    Line %d in format file "%ls": bad value %ls for attribute "%ls".    |
|    4859    |    16    |    No    |    Line %d in format file "%ls": required attribute "%ls" is missing.    |
|    4860    |    16    |    No    |    Cannot bulk load. The file "%ls" does not exist.    |
|    4861    |    16    |    No    |    Cannot bulk load because the file "%ls" could not be opened. Operating system error code %ls.    |
|    4862    |    16    |    No    |    Cannot bulk load because the file "%ls" could not be read. Operating system error code %ls.    |
|    4863    |    16    |    No    |    Bulk load data conversion error (truncation) for row %d, column %d (%ls).    |
|    4864    |    16    |    No    |    Bulk load data conversion error (type mismatch or invalid character for the specified codepage) for row %d, column %d (%ls).    |
|    4865    |    16    |    No    |    Cannot bulk load because the maximum number of errors (%d) was exceeded.    |
|    4866    |    16    |    No    |    The bulk load failed. The column is too long in the data file for row %d, column %d. Verify that the field terminator and row terminator are specified correctly. Bulk load failed due to invalid column value in CSV data file %ls in row %d, column %d | 
|    4867    |    16    |    No    |    Bulk load data conversion error (overflow) for row %d, column %d (%ls).    |
|    4868    |    16    |    No    |    The bulk load failed. The codepage "%d" is not installed. Install the codepage and run the command again.    |
|    4869    |    16    |    No    |    The bulk load failed. Unexpected NULL value in data file row %d, column %d. The destination column (%ls) is defined as NOT NULL.    |
|    4870    |    16    |    No    |    Cannot bulk load because of an error writing file "%ls". Operating system error code %ls.    |
|   4879    |   16  |   No  | 
|    4871    |    16    |    No    |    Bulk load error while attempting to log errors.    |
|    4872    |    16    |    No    |    Line %d in format file "%ls": duplicate element id "%ls".    |
|    4873    |    16    |    No    |    Line %d in format file "%ls": referencing non-existing element id "%ls".    |
|    4874    |    16    |    No    |    Line %d in format file "%ls": duplicate element id reference "%ls".    |
|    4875    |    16    |    No    |    Invalid column attribute from bcp client for colid %d.    |
|    4876    |    16    |    No    |    The Bulk Insert operation of SQL Server Destination has timed out. Please consider increasing the value of Timeout property on the SQL Server Destination in the dataflow.    |
|    4877    |    16    |    No    |    Error parsing DTS stream when reading row %d, column %d.    |
|    4880    |    16    |    No    |    Cannot bulk load. When you use the FIRSTROW and LASTROW parameters, the value for FIRSTROW cannot be greater than the value for LASTROW.    |
|    4881    |    10    |    No    |    Note: Bulk Insert through a view may result in base table default values being ignored for NULL columns in the data file.    |
|    4882    |    16    |    No    |    Cannot bulk load. A prefix length, field length, or terminator is required for the source column %d in the format file "%s".    |
|    4883    |    16    |    No    |    The XML reader returned 0x%08X for the info item starting near line %d column %d in format file "%ls".    |
|    4884    |    16    |    No    |    Unknown error near info item starting near line %d column %d in format file "%ls".    |
|    4885    |    16    |    No    |    Cannot open file "%ls". A Windows NT Integrated Security login is required.    |
|    4886    |    16    |    No    |    Cannot open the file "%ls". Operating system error: %ls    |
|    4887    |    16    |    No    |    Cannot open the file "%ls". Only disk files are supported.    |
|    4888    |    16    |    No    |    Cannot open the file "%ls". The bulkadmin role membership is required.    |
|    4889    |    16    |    No    |    Cannot open the file "%ls". A unicode byte-order mark is missing.    |
|    4890    |    16    |    No    |    Insert bulk is not supported in showplan mode.    |
|    4891    |    16    |    No    |    Insert bulk failed due to a schema change of the target table.    |
|    4892    |    16    |    No    |    Bulk insert failed due to a schema change of the target table.    |
|    4893    |    16    |    No    |    Could not bulk load because SSIS file mapping object '%ls' could not be opened. Operating system error code %ls. Make sure you are accessing a local server via Windows security.    |
|    4894    |    21    |    Yes    |    COLMETADATA must be present when using bcp.    |
|    4895    |    21    |    Yes    |    Unicode data is odd byte size for column %d. Should be even byte size.    |
|    4896    |    16    |    No    |    Invalid column value from bcp client for colid %d.    |
|    4897    |    16    |    No    |    Received an invalid length for chunked LOB data for colid %d.    |
|    4900    |    16    |    No    |    The ALTER TABLE SWITCH statement failed for table '%.*ls'. It is not possible to switch the partition of a table that has change tracking enabled. Disable change tracking before using ALTER TABLE SWITCH.    |
|    4901    |    16    |    No    |    ALTER TABLE only allows columns to be added that can contain nulls, or have a DEFAULT definition specified, or the column being added is an identity or timestamp column, or alternatively if none of the previous conditions are satisfied the table must be empty to allow addition of this column. Column '%.*ls' cannot be added to non-empty table '%.*ls' because it does not satisfy these conditions.    |
|    4902    |    16    |    No    |    Cannot find the object "%.*ls" because it does not exist or you do not have permissions.    |
|    4903    |    10    |    No    |    Warning: The specified partition %d for the table '%.*ls' was ignored in ALTER TABLE SWITCH statement because the table is not partitioned.    |
|    4904    |    16    |    No    |    ALTER TABLE SWITCH statement failed. The specified partition %d of target table '%.*ls' must be empty.    |
|    4905    |    16    |    No    |    ALTER TABLE SWITCH statement failed. The target table '%.*ls' must be empty.    |
|    4906    |    16    |    No    |    '%ls' statement failed. The %S_MSG '%.*ls' is %S_MSG partitioned while index '%.*ls' is %S_MSG partitioned.    |
|    4907    |    16    |    No    |    '%ls' statement failed. The %S_MSG '%.*ls' has %d partitions while index '%.*ls' has %d partitions.    |
|    4908    |    16    |    No    |    '%ls' statement failed. The range boundary values used to partition the %S_MSG '%.*ls' are different from the range boundary values used for index '%.*ls'.    |
|    4909    |    16    |    No    |    Cannot alter '%.*ls' because it is not a table.    |
|    4911    |    16    |    No    |    Cannot specify a partitioned table without partition number in ALTER TABLE SWITCH statement. The table '%.*ls' is partitioned.    |
|    4912    |    16    |    No    |    '%ls' statement failed. The columns set used to partition the %S_MSG '%.*ls' is different from the column set used to partition index '%.*ls'.    |
|    4913    |    16    |    No    |    ALTER TABLE SWITCH statement failed. The table '%.*ls' has clustered index '%.*ls' while the table '%.*ls' does not have clustered index.    |
|    4914    |    16    |    No    |    The ALTER TABLE SWITCH statement failed. The table "%.*ls" has a disabled clustered index.    |
|    4915    |    16    |    No    |    '%ls' statement failed. The parameter type of the partition function used to partition the %S_MSG '%.*ls' is different from the parameter type of the partition function used to partition index '%.*ls'.    |
|    4916    |    16    |    No    |    Could not enable or disable the constraint. See previous errors.    |
|    4917    |    16    |    No    |    Constraint '%.*ls' does not exist.    |
|    4918    |    16    |    No    |    ALTER TABLE SWITCH statement failed because the table '%.*ls' has fulltext index on it.    |
|    4919    |    16    |    No    |    PERSISTED attribute cannot be altered on column '%.*ls' because this column is not computed.    |
|    4920    |    16    |    No    |    ALTER TABLE failed because trigger '%.*ls' on table '%.*ls' does not exist.    |
|    4921    |    16    |    No    |    ALTER TABLE failed because trigger '%.*ls' does not belong to table '%.*ls'.    |
|    4922    |    16    |    No    |    %ls %.*ls failed because one or more objects access this column.    |
|    4923    |    16    |    No    |    ALTER TABLE DROP COLUMN failed because '%.*ls' is the only data column in table '%.*ls'. A table must have at least one data column.    |
|    4924    |    16    |    No    |    %ls failed because column '%.*ls' does not exist in table '%.*ls'.    |
|    4925    |    16    |    No    |    ALTER TABLE ALTER COLUMN ADD ROWGUIDCOL failed because a column already exists in table '%.*ls' with ROWGUIDCOL property.    |
|    4926    |    16    |    No    |    ALTER TABLE ALTER COLUMN DROP ROWGUIDCOL failed because a column does not exist in table '%.*ls' with ROWGUIDCOL property.    |
|    4927    |    16    |    No    |    Cannot alter column '%.*ls' to be data type %.*ls.    |
|    4928    |    16    |    No    |    Cannot alter column '%.*ls' because it is '%ls'.    |
|    4929    |    16    |    No    |    Cannot alter the %S_MSG '%.*ls' because it is being published for replication.    |
|    4933    |    16    |    No    |    Computed column '%.*ls' in table '%.*ls' cannot be persisted because the column depends on a non-schemabound object.    |
|    4934    |    16    |    No    |    Computed column '%.*ls' in table '%.*ls' cannot be persisted because the column does user or system data access.    |
|    4935    |    16    |    No    |    ALTER TABLE ADD COLUMN cannot specify a FILESTREAM filegroup that differs from the existing one.    |
|    4936    |    16    |    No    |    Computed column '%.*ls' in table '%.*ls' cannot be persisted because the column is non-deterministic.    |
|    4938    |    16    |    No    |    ALTER TABLE SWITCH statement failed. Partition %d of %S_MSG '%.*ls' is in filegroup '%.*ls' and partition %d of %S_MSG '%.*ls' is in filegroup '%.*ls'.    |
|    4939    |    16    |    No    |    ALTER TABLE SWITCH statement failed. %S_MSG '%.*ls' is in filegroup '%.*ls' and partition %d of %S_MSG '%.*ls' is in filegroup '%.*ls'.    |
|    4940    |    16    |    No    |    ALTER TABLE SWITCH statement failed. %S_MSG '%.*ls' is in filegroup '%.*ls' and %S_MSG '%.*ls' is in filegroup '%.*ls'.    |
|    4941    |    16    |    No    |    ALTER TABLE SWITCH statement failed because the table '%.*ls' is marked for merge replication.    |
|    4942    |    16    |    No    |    ALTER TABLE SWITCH statement failed because column '%.*ls' at ordinal %d in table '%.*ls' has a different name than the column '%.*ls' at the same ordinal in table '%.*ls'.    |
|    4943    |    16    |    No    |    ALTER TABLE SWITCH statement failed because table '%.*ls' has %d columns and table '%.*ls' has %d columns.    |
|    4944    |    16    |    No    |    ALTER TABLE SWITCH statement failed because column '%.*ls' has data type %s in source table '%.*ls' which is different from its type %s in target table '%.*ls'.    |
|    4945    |    16    |    No    |    ALTER TABLE SWITCH statement failed because column '%.*ls' does not have the same collation in tables '%.*ls' and '%.*ls'.    |
|    4946    |    16    |    No    |    ALTER TABLE SWITCH statement failed because column '%.*ls' does not have the same persistent attribute in tables '%.*ls' and '%.*ls'.    |
|    4947    |    16    |    No    |    ALTER TABLE SWITCH statement failed. There is no identical index in source table '%.*ls' for the index '%.*ls' in target table '%.*ls' .    |
|    4948    |    16    |    No    |    ALTER TABLE SWITCH statement failed. The source table '%.*ls' is in database '%.*ls' while the target table '%.*ls' is in database '%.*ls'.    |
|    4949    |    16    |    No    |    ALTER TABLE SWITCH statement failed because the object '%.*ls' is not a user defined table.    |
|    4950    |    16    |    No    |    ALTER TABLE SWITCH statement failed because partition number %d does not exist in table '%.*ls'.    |
|    4951    |    16    |    No    |    ALTER TABLE SWITCH statement failed because column '%.*ls' does not have the same FILESTREAM storage attribute in tables '%.*ls' and '%.*ls'.    |
|    4952    |    16    |    No    |    ALTER TABLE SWITCH statement failed because column '%.*ls' does not have the same ANSI trimming semantics in tables '%.*ls' and '%.*ls'.    |
|    4953    |    16    |    No    |    ALTER TABLE SWITCH statement failed. The columns set used to partition the table '%.*ls' is different from the column set used to partition the table '%.*ls'.    |
|    4954    |    16    |    No    |    ALTER TABLE SWITCH statement failed. The table '%.*ls' has inline limit of %d for text in row data which is different from value %d used by table '%.*ls'.    |
|    4955    |    16    |    No    |    ALTER TABLE SWITCH statement failed. The source table '%.*ls' and target table '%.*ls' are same.    |
|    4956    |    16    |    No    |    ALTER TABLE SWITCH statement failed because the table '%.*ls' is not RANGE partitioned.    |
|    4957    |    16    |    No    |    '%ls' statement failed because the expression identifying partition number for the %S_MSG '%.*ls' is not of integer type.    |
|    4958    |    16    |    No    |    ALTER TABLE SWITCH statement failed because column '%.*ls' does not have the same ROWGUIDCOL property in tables '%.*ls' and '%.*ls'.    |
|    4959    |    16    |    No    |    ALTER TABLE SWITCH statement failed. Partition %d of %S_MSG '%.*ls' has TEXT filegroup '%.*ls' and partition %d of %S_MSG '%.*ls' has TEXT filegroup '%.*ls'.    |
|    4960    |    16    |    No    |    ALTER TABLE SWITCH statement failed. Check constraint '%.*ls' in source table '%.*ls' is NOCHECK constraint but the matching check constraint '%.*ls' in target table '%.*ls' is CHECK.    |
|    4961    |    16    |    No    |    ALTER TABLE SWITCH statement failed. Column '%.*ls' in table '%.*ls' is nullable and it is not nullable in '%.*ls'.    |
|    4962    |    16    |    No    |    ALTER TABLE SWITCH statement failed. Partition %d in table '%.*ls' is not a range partition.    |
|    4963    |    16    |    No    |    ALTER TABLE SWITCH statement failed. Partition %d is not valid for table '%.*ls'.    |
|    4964    |    16    |    No    |    ALTER TABLE SWITCH statement failed. Table '%.*ls' has RULE constraint '%.*ls'. SWITCH is not allowed on tables with RULE constraints.    |
|    4965    |    16    |    No    |    ALTER TABLE SWITCH statement failed. Column '%.*ls' in table '%.*ls' is computed column but the same column in '%.*ls' is not computed.    |
|    4966    |    16    |    No    |    ALTER TABLE SWITCH statement failed. Computed column '%.*ls' defined as '%.*ls' in table '%.*ls' is different from the same column in table '%.*ls' defined as '%.*ls'.    |
|    4967    |    16    |    No    |    ALTER TABLE SWITCH statement failed. SWITCH is not allowed because source table '%.*ls' contains primary key for constraint '%.*ls'.    |
|    4968    |    16    |    No    |    ALTER TABLE SWITCH statement failed. Target table '%.*ls' has foreign key for constraint '%.*ls' but source table '%.*ls' does not have corresponding key.    |
|    4969    |    16    |    No    |    ALTER TABLE SWITCH statement failed. Foreign key constraint '%.*ls' is disabled in source table '%.*ls' and the corresponding constraint '%.*ls' is enabled in target table '%.*ls'. The source table constraint must be enabled.    |
|    4970    |    16    |    No    |    ALTER TABLE SWITCH statement failed. Target table '%.*ls' has a table level check constraint '%.*ls' but the source table '%.*ls' does not have a corresponding constraint.    |
|    4971    |    16    |    No    |    ALTER TABLE SWITCH statement failed. Target table '%.*ls' has a column level check constraint '%.*ls' but the source table '%.*ls' does not have a corresponding constraint.    |
|    4972    |    16    |    No    |    ALTER TABLE SWITCH statement failed. Check constraints or partition function of source table '%.*ls' allows values that are not allowed by check constraints or partition function on target table '%.*ls'.    |
|    4973    |    16    |    No    |    ALTER TABLE SWITCH statement failed. Range defined by partition %d in table '%.*ls' is not a subset of range defined by partition %d in table '%.*ls'.    |
|    4974    |    16    |    No    |    ALTER TABLE SWITCH statement failed. Foreign key constraint '%.*ls' is NOCHECK in source table '%.*ls' and the corresponding constraint '%.*ls' is CHECK in target table '%.*ls'. The source table constraint must be in CHECK.    |
|    4975    |    16    |    No    |    ALTER TABLE SWITCH statement failed. Check constraint '%.*ls' in source table '%.*ls' and check constraint '%.*ls' in target table '%.*ls' have different 'Not For Replication' settings.    |
|    4976    |    16    |    No    |    ALTER TABLE SWITCH statement failed. Target table '%.*ls' has a check constraint '%.*ls' on an XML column, but the source table '%.*ls' does not have an identical check constraint.    |
|    4977    |    16    |    No    |    ALTER TABLE SWITCH statement failed. Target table '%.*ls' has a check constraint '%.*ls' on a CLR type column, but the source table '%.*ls' does not have an identical check constraint.    |
|    4978    |    16    |    No    |    ALTER TABLE SWITCH statement failed. The partition %d in table '%.*ls' resides in a read-only filegroup '%.*ls'.    |
|    4979    |    16    |    No    |    ALTER TABLE SWITCH statement failed. The table '%.*ls' resides in a readonly filegroup '%.*ls'.    |
|    4980    |    16    |    No    |    ALTER TABLE SWITCH statement failed. The lobdata of partition %d in table '%.*ls' resides in a readonly filegroup '%.*ls'.    |
|    4981    |    16    |    No    |    ALTER TABLE SWITCH statement failed. The lobdata of table '%.*ls' resides in a readonly filegroup '%.*ls'.    |
|    4982    |    16    |    No    |    ALTER TABLE SWITCH statement failed. Check constraints of source table '%.*ls' allow values that are not allowed by range defined by partition %d on target table '%.*ls'.    |
|    4983    |    16    |    No    |    ALTER TABLE SWITCH statement failed. Target table '%.*ls' has an XML or spatial index '%.*ls' on it. Only source table can have XML or spatial indexes in the ALTER TABLE SWITCH statement.    |
|    4984    |    16    |    No    |    ALTER TABLE SWITCH statement failed. Target table '%.*ls' and source table '%.*ls' have different vardecimal storage format values. Use stored procedure sp_tableoption to alter the 'vardecimal storage format' option for the tables to make sure that the values are the same.    |
|    4985    |    16    |    No    |    ALTER TABLE SWITCH statement failed because column '%.*ls' does not have the same nullability attribute in tables '%.*ls' and '%.*ls'.    |
|    4986    |    16    |    No    |    ALTER TABLE SWITCH statement failed because column '%.*ls' does not have the same CLR type in tables '%.*ls' and '%.*ls'.    |
|    4987    |    16    |    No    |    ALTER TABLE SWITCH statement failed because column '%.*ls' does not have the same XML Schema Collection in tables '%.*ls' and '%.*ls'.    |
|    4988    |    16    |    No    |    Cannot persist computed column '%.*ls'. Underlying object '%.*ls' has a different owner than table '%.*ls'.    |
|    4989    |    16    |    No    |    Cannot drop the ROWGUIDCOL property for column '%.*ls' in table '%.*ls' because the column is not the designated ROWGUIDCOL for the table.    |
|    4990    |    16    |    No    |    Cannot alter column '%.*ls' in table '%.*ls' to add or remove the FILESTREAM column attribute.    |
|    4991    |    16    |    No    |    Cannot alter NOT FOR REPLICATION attribute on column '%.*ls' in table '%.*ls' because this column is not an identity column.    |
|    4992    |    16    |    No    |    Cannot use table option LARGE VALUE TYPES OUT OF ROW on a user table that does not have any of large value types varchar(max), nvarchar(max), varbinary(max), xml or large CLR type columns in it. This option can be applied to tables having large values computed column that are persisted.    |
|    4993    |    16    |    No    |    ALTER TABLE SWITCH statement failed. The table '%.*ls' has different setting for Large Value Types Out Of Row table option as compared to table '%.*ls'.    |
|    4994    |    16    |    No    |    Computed column '%.*ls' in table '%.*ls' cannot be persisted because the column type, '%.*ls', is a non-byte-ordered CLR type.    |
|    4995    |    16    |    No    |    Vardecimal storage format cannot be enabled on table '%.*ls' because database '%.*ls' is a system database. Vardecimal storage format is not available in system databases.    |
|    4996    |    16    |    No    |    Change tracking is already enabled for table '%.*ls'.    |
|    4997    |    16    |    No    |    Cannot enable change tracking on table '%.*ls'. Change tracking requires a primary key on the table. Create a primary key on the table before enabling change tracking.    |
|    4998    |    16    |    No    |    Change tracking is not enabled on table '%.*ls'.    |
|    4999    |    16    |    No    |    Cannot enable change tracking on table '%.*ls'. Change tracking does not support a primary key of type timestamp on a table.    |
