---
author: MashaMSFT
ms.author: mathoma
ms.reviewer: randolphwest
ms.date: 08/19/2022
ms.topic: include
---
| Error| Severity | Event Logged | Description|
| :------ | :------| :------| :----------------------------- |
|    10000    |    16    |    No    |    Unknown provider error.    |
|    [10001](../../relational-databases/errors-events/mssqlserver-10001-database-engine-error.md)    |    16    |    No    |    The provider reported an unexpected catastrophic failure.    |
|    10002    |    16    |    No    |    The provider did not implement the functionality.    |
|    [10003](../../relational-databases/errors-events/mssqlserver-10003-database-engine-error.md)    |    16    |    No    |    The provider ran out of memory.    |
|    10004    |    16    |    No    |    One or more arguments were reported invalid by the provider.    |
|    10005    |    16    |    No    |    The provider did not support an interface.    |
|    10006    |    16    |    No    |    The provider indicated an invalid pointer was used.    |
|    10007    |    16    |    No    |    The provider indicated an invalid handle was used.    |
|    10008    |    16    |    No    |    The provider terminated the operation.    |
|    10009    |    16    |    No    |    The provider did not give any information about the error.    |
|    10010    |    16    |    No    |    The data necessary to complete this operation was not yet available to the provider.    |
|    10011    |    16    |    No    |    Access denied.    |
|    10021    |    16    |    No    |    Execution terminated by the provider because a resource limit was reached.    |
|    10022    |    16    |    No    |    The provider called a method from IRowsetNotify in the consumer, and the method has not yet returned.    |
|    10023    |    16    |    No    |    The provider does not support the necessary method.    |
|    10024    |    16    |    No    |    The provider indicates that the user did not have the permission to perform the operation.    |
|    10025    |    16    |    No    |    Provider caused a server fault in an external process.    |
|    10026    |    16    |    No    |    No command text was set.    |
|    10027    |    16    |    No    |    Command was not prepared.    |
|    10028    |    16    |    No    |    Authentication failed.    |
|    10032    |    16    |    No    |    Cannot return multiple result sets (not supported by the provider).    |
|    10033    |    16    |    No    |    The specified index does not exist or the provider does not support an index scan on this data source.    |
|    10034    |    16    |    No    |    The specified table or view does not exist or contains errors.    |
|    10035    |    16    |    No    |    No value was given for one or more of the required parameters.    |
|    10042    |    16    |    No    |    Cannot set any properties while there is an open rowset.    |
|    10052    |    16    |    No    |    The insertion was canceled by the provider during notification.    |
|    10053    |    16    |    No    |    Could not convert the data value due to reasons other than sign mismatch or overflow.    |
|    10054    |    16    |    No    |    The data value for one or more columns overflowed the type used by the provider.    |
|    10055    |    16    |    No    |    The data violated the integrity constraints for one or more columns.    |
|    10056    |    16    |    No    |    The number of rows that have pending changes has exceeded the limit specified by the DBPROP_MAXPENDINGROWS property.    |
|    10057    |    16    |    No    |    Cannot create the row. Would exceed the total number of active rows supported by the rowset.    |
|    10058    |    16    |    No    |    The consumer cannot insert a new row before releasing previously-retrieved row handles.    |
|    [10060](../../relational-databases/errors-events/mssqlserver-10060-database-engine-error.md)    |        |        |    An error has occurred while establishing a connection to the server. When connecting to SQL Server, this failure may be caused by the fact that under the default settings SQL Server does not allow remote connections. (provider: TCP Provider, error: 0 - A connection attempt failed because the connected party did not properly respond after a period of time, or established connection failed because connected host has failed to respond.) (Microsoft SQL Server, Error: 10060)    |
|    [10061](../../relational-databases/errors-events/mssqlserver-10061-database-engine-error.md)    |        |        |    An error has occurred while establishing a connection to the server. When connecting to SQL Server, this failure may be caused by the fact that under the default settings SQL Server does not allow remote connections. (provider: TCP Provider, error: 0 - No connection could be made because the target machine actively refused it.) (Microsoft SQL Server, Error: 10061)    |
|    10062    |    16    |    No    |    The change was canceled by the provider during notification.    |
|    10063    |    16    |    No    |    The change was canceled by the provider during notification.    |
|    10064    |    16    |    No    |    Could not convert the data value due to reasons other than sign mismatch or overflow.    |
|    10065    |    16    |    No    |    The data value for one or more columns overflowed the type used by the provider.    |
|    10066    |    16    |    No    |    The data violated the integrity constraints for one or more columns.    |
|    10067    |    16    |    No    |    The number of rows that have pending changes has exceeded the limit specified by the DBPROP_MAXPENDINGROWS property.    |
|    10068    |    16    |    No    |    The rowset was using optimistic concurrency and the value of a column has been changed after the containing row was last fetched or resynchronized.    |
|    10069    |    16    |    No    |    The consumer could not delete the row. A deletion is pending or has already been transmitted to the data source.    |
|    10081    |    16    |    No    |    The consumer could not delete the row. The insertion has been transmitted to the data source.    |
|    10085    |    16    |    No    |    The rowset uses integrated indexes and there is no current index.    |
|    10086    |    16    |    No    |    RestartPosition on the table was canceled during notification.    |
|    10087    |    16    |    No    |    The table was built over a live data stream and the position cannot be restarted.    |
|    10088    |    16    |    No    |    The provider did not release some of the existing rows.    |
|    10100    |    16    |    No    |    The order of the columns was not specified in the object that created the rowset. The provider had to reexecute the command to reposition the next fetch position to its initial position, and the order of the columns changed.    |
|    10101    |    16    |    No    |    Cannot create %S_MSG on view "%.*ls" because it contains the DISTINCT keyword. Consider removing DISTINCT from the view or not indexing the view. Alternatively, consider replacing DISTINCT with GROUP BY or COUNT_BIG(*) to simulate DISTINCT on grouping columns.    |
|    10102    |    16    |    No    |    Cannot create %S_MSG on view "%.*ls" because it contains the TOP keyword. Consider removing TOP or not indexing the view.    |
|    10103    |    16    |    No    |    Cannot create %S_MSG on view "%.*ls" because it contains the TABLESAMPLE clause. Consider removing TABLESAMPLE or not indexing the view.    |
|    10104    |    16    |    No    |    Cannot create %S_MSG on view "%.*ls" because it uses OPENROWSET, OPENQUERY, or OPENDATASOURCE. Consider not indexing the view, or eliminating OPENQUERY, OPENROWSET, and OPENDATASOURCE.    |
|    10105    |    16    |    No    |    Cannot create %S_MSG on view "%.*ls" because it references a table using a CONTAINSTABLE or FREETEXTTABLE full-text function. Consider removing use of these functions or not indexing the view.    |
|    10106    |    16    |    No    |    Cannot create %S_MSG on view "%.*ls" because it uses the OPENXML rowset provider. Consider removing OPENXML or not indexing the view.    |
|    10107    |    16    |    No    |    Cannot create %S_MSG on view "%.*ls" because it references an internal system rowset provider. Consider not indexing this view.    |
|    10108    |    16    |    No    |    Cannot create %S_MSG on view "%.*ls" because it uses table variable "%.*ls". Consider not indexing this view or removing the reference to the table variable.    |
|    10109    |    16    |    No    |    Cannot create %S_MSG on view "%.*ls" because it references a SQL Server internal table.    |
|    10110    |    16    |    No    |    Cannot create %S_MSG on view "%.*ls" because it references derived table "%.*ls" (defined by SELECT statement in FROM clause). Consider removing the reference to the derived table or not indexing the view.    |
|    10111    |    16    |    No    |    Cannot create %S_MSG on view "%.*ls" because it contains an OUTER APPLY. Consider not indexing the view, or removing OUTER APPLY.    |
|    10112    |    16    |    No    |    Cannot create %S_MSG on view "%.*ls" because it contains a join using an ODBC standard escape syntax. Consider using an ANSI join syntax instead.    |
|    10113    |    16    |    No    |    Cannot create %S_MSG on view '%.*ls' because it contains an INNER join that specifies a join hint. Consider removing the join hint.    |
|    10114    |    16    |    No    |    Cannot create %S_MSG on view "%.*ls" because it uses a LEFT, RIGHT, or FULL OUTER join, and no OUTER joins are allowed in indexed views. Consider using an INNER join instead.    |
|    10115    |    16    |    No    |    Cannot create %S_MSG on view "%.*ls" because it uses the PIVOT operator. Consider not indexing this view.    |
|    10116    |    16    |    No    |    Cannot create %S_MSG on view "%.*ls" because it uses the UNPIVOT operator. Consider not indexing this view.    |
|    10117    |    16    |    No    |    Cannot create %S_MSG on view '%.*ls' because it contains one or more UNION, INTERSECT, or EXCEPT operators. Consider creating a separate indexed view for each query that is an input to the UNION, INTERSECT, or EXCEPT operators of the original view.    |
|    10118    |    16    |    No    |    Cannot create %S_MSG on view "%.*ls" because the view uses the "*" operator to select columns. Consider referencing columns by name instead.    |
|    10119    |    16    |    No    |    Cannot create %S_MSG on view "%.*ls" because it contains a GROUP BY ALL. Consider using a GROUP BY instead.    |
|    10121    |    16    |    No    |    Cannot create %S_MSG on view "%.*ls" because it contains a CUBE, ROLLUP, or GROUPING SETS operator. Consider not indexing this view.    |
|    10122    |    16    |    No    |    Cannot create %S_MSG on view "%.*ls" because it contains a HAVING clause. Consider removing the HAVING clause.    |
|    10123    |    16    |    No    |    Cannot create %S_MSG on view "%.*ls" because it contains a COMPUTE clause. Consider not indexing this view, or using a GROUP BY or aggregate view instead to replace the COMPUTE calculation of aggregate results.    |
|    10124    |    16    |    No    |    Cannot create %S_MSG on view "%.*ls" because it contains a join that uses deprecated Transact-SQL join syntax ( *= and =* ). Consider using = operator (non-outer-join) instead.    |
|    10125    |    16    |    No    |    Cannot create %S_MSG on view "%.*ls" because it references an internal SQL Server column.    |
|    10126    |    16    |    No    |    Cannot create %S_MSG on view "%.*ls" because it uses aggregate "%.*ls". Consider eliminating the aggregate, not indexing the view, or using alternate aggregates. For example, for AVG substitute SUM and COUNT_BIG, or for COUNT, substitute COUNT_BIG.    |
|    10127    |    16    |    No    |    Cannot create %S_MSG on view "%.*ls" because it uses aggregate "%.*ls" with the DISTINCT keyword. Consider not indexing this view or eliminating DISTINCT. Consider use of a GROUP BY or COUNT_BIG(*) view to simulate DISTINCT on grouping columns.    |
|    10128    |    16    |    No    |    Cannot create %S_MSG on view "%.*ls" because it contains one or more subqueries. Consider changing the view to use only joins instead of subqueries. Alternatively, consider not indexing this view.    |
|    10129    |    16    |    No    |    Cannot create %S_MSG on view "%.*ls" because it uses a CONTAINS or FREETEXT full-text predicate. Consider eliminating CONTAINS or FREETEXT, or not indexing the view.    |
|    10130    |    16    |    No    |    Cannot create %S_MSG on view "%.*ls" because it references the inline or multistatement table-valued function "%.*ls". Consider expanding the function definition by hand in the view definition, or not indexing the view.    |
|    10131    |    16    |    No    |    Cannot create %S_MSG on view "%.*ls" because it uses non-deterministic common language runtime (CLR) table-valued function "%.*ls". Consider not indexing the view or changing it to not use this function.    |
|    10132    |    16    |    No    |    Cannot create %S_MSG on view "%.*ls" because it references imprecise common language runtime (CLR) table-valued function "%.*ls". Consider not indexing the view.    |
|    10133    |    16    |    No    |    Cannot create %S_MSG on view "%.*ls" because it references table valued common language runtime (CLR) function "%.*ls". Consider removing reference to the function or not indexing the view.    |
|    10134    |    16    |    No    |    Cannot create %S_MSG on view "%.*ls" because function "%.*ls" referenced by the view performs user or system data access.    |
|    10136    |    16    |    No    |    Cannot create %S_MSG on view "%.*ls" because it contains more than one APPLY. Consider not indexing the view, or using only one APPLY.    |
|    10137    |    16    |    No    |    Cannot create %S_MSG on view "%.*ls" because it uses the aggregate COUNT. Use COUNT_BIG instead.    |
|    10138    |    16    |    No    |    Cannot create %S_MSG on view "%.*ls" because it references common table expression "%.*ls". Views referencing common table expressions cannot be indexed. Consider not indexing the view, or removing the common table expression from the view definition.    |
|    10139    |    16    |    No    |    Cannot create %S_MSG on view '%.*ls' because its select list does not include a proper use of COUNT_BIG. Consider adding COUNT_BIG(*) to select list.    |
|    10140    |    16    |    No    |    Cannot create %S_MSG on view '%.*ls' because the view uses an implicit conversion from string to datetime or smalldatetime. Use an explicit CONVERT with a deterministic style value.    |
|    10141    |    16    |    No    |    Cannot create %S_MSG on view '%.*ls' because the view contains a table hint. Consider removing the hint.    |
|    10142    |    16    |    No    |    Cannot create %S_MSG on view '%.*ls' because it references CLR routine (function or method) '%.*ls' outside non-key columns of SELECT list. Recreate or alter view so it does not reference CLR routines except in non-key columns of SELECT list, and then create index.    |
|    10143    |    16    |    No    |    Cannot create %S_MSG on view "%.*ls" because it contains an APPLY. Consider not indexing the view, or removing APPLY.    |
|    10144    |    16    |    No    |    Cannot create %S_MSG on view "%.*ls" because it contains a ranking or aggregate window function. Remove the function from the view definition or, alternatively, do not index the view.    |
|    10145    |    16    |    No    |    Cannot create %S_MSG on view '%.*ls' because it uses the CHANGETABLE function.    |
|    10211    |    16    |    No    |    Cannot create %S_MSG on the view '%.*ls' because it references a sparse column set. Views that contain a sparse column set cannot be indexed. Consider removing the sparse column set from the view or not indexing the view.    |
|    10227    |    16    |    No    |    Cannot invoke mutator on a null CLR type value.    |
|    10240    |    16    |    No    |    Field "%.*ls" of type "%.*ls.%.*ls" cannot be updated because the field is "%.*ls".    |
|    10300    |    16    |    No    |    Could not find UdtExtensions.dll. Please check your installation.    |
|    10301    |    16    |    No    |    Assembly '%.*ls' references assembly '%.*ls', which is not present in the current database. SQL Server attempted to locate and automatically load the referenced assembly from the same location where referring assembly came from, but that operation has failed (reason: %S_MSG). Please load the referenced assembly into the current database and retry your request.    |
|    10302    |    16    |    No    |    Assembly '%.*ls' references assembly '%.*ls', which is not present in the current database. SQL Server attempted to locate and automatically load the referenced assembly from the same location where referring assembly came from, but that operation has failed (reason: %s). Please load the referenced assembly into the current database and retry your request.    |
|    10303    |    16    |    Yes    |    Could not get path for SQL Server: '%ls'.    |
|    10304    |    16    |    No    |    Could not create AppDomain manager: '%.*ls'.    |
|    10305    |    16    |    No    |    Failed to enter Common Language Runtime (CLR) with HRESULT 0x%x. This may due to low resource conditions.    |
|    10306    |    16    |    No    |    The Init method for a CLR table-valued function must be annotated with SqlFunctionAttribute.    |
|    10307    |    16    |    No    |    The SqlFunctionAttribute of the Init method for a CLR table-valued function must set the FillRowMethodName property.    |
|    10308    |    10    |    No    |    The FillRowMethodName property of SqlFunctionAttribute does not contain a valid method name.    |
|    10309    |    10    |    No    |    Warning: The Microsoft .NET Framework assembly '%.*ls' you are registering is not fully tested in the SQL Server hosted environment and is not supported. In the future, if you upgrade or service this assembly or the .NET Framework, your CLR integration routine may stop working. Please refer SQL Server Books Online for more details.    |
|    10310    |    10    |    Yes    |    Warning: The SQL Server client assembly '%.*ls' you are registering is not fully tested in SQL Server hosted environment.    |
|    10311    |    10    |    Yes    |    AppDomain %i (%.*ls) is marked for unload due to common language runtime (CLR) or security data definition language (DDL) operations.    |
|    10312    |    16    |    No    |    AppDomain %i (%.*ls) is marked for unload due to memory pressure.    |
|    10313    |    16    |    No    |    .NET Framework execution was aborted. The UDP/UDF/CLR type did not revert thread token.    |
|    10314    |    16    |    No    |    An error occurred while using the .NET Framework during %S_MSG. The server may be running out of resources. Try running the query again. If the problem persist, contact a support professional. %.*ls    |
|    10316    |    16    |    No    |    An error occurred in the Microsoft .NET Framework while trying to load assembly id %d. The server may be running out of resources, or the assembly may not be trusted with PERMISSION_SET = EXTERNAL_ACCESS or UNSAFE. Run the query again, or check documentation to see how to solve the assembly trust issues. For more information about this error: %.*ls    |
|    10317    |    16    |    No    |    The app domain with specified version id (%d) was unloaded due to memory pressure and could not be found.    |
|    10318    |    16    |    No    |    An error occurred trying to get file version info for the file '%s'.    |
|    10319    |    16    |    No    |    '%.*ls' failed because parameter %d of method '%.*ls' of type '%.*ls' is annotated with unsupported attribute System.ParamArrayAttribute.    |
|    10320    |    16    |    No    |    UserDefinedType method call failed because parameter %d of method '%.*ls' of type '%.*ls' is annotated with unsupported attribute System.ParamArrayAttribute.    |
|    10321    |    16    |    No    |    Method name '%.*ls' is invalid for '%.*ls'.    |
|    10322    |    16    |    No    |    Method name '%.*ls' is invalid for UserDefinedType method call.    |
|    10323    |    16    |    Yes    |    Type %.*ls not found in database %.*ls    |
|    10324    |    16    |    No    |    Invalid user code has been identified by .Net Framework Managed Debug Assistant %.*ls    |
|    10325    |    16    |    Yes    |    WITH ENCRYPTION option of CREATE TRIGGER is only applicable to T-SQL triggers and not to CLR triggers.    |
|    10326    |    16    |    Yes    |    The server is shutting down due to stack overflow in user's unmanaged code.    |
|    10327    |    14    |    No    |    Two versions of assembly '%.*ls' cannot coexist in database '%.*ls'. Keep one version and drop the other.    |
|    10328    |    16    |    No    |    %ls ASSEMBLY for assembly '%.*ls' failed because assembly '%.*ls' is not authorized for PERMISSION_SET = %ls. The assembly is authorized when either of the following is true: the database owner (DBO) has %ls permission and the database has the TRUSTWORTHY database property on; or the assembly is signed with a certificate or an asymmetric key that has a corresponding login with %ls permission.    |
|    10329    |    16    |    No    |    There is not enough stack to create appdomain '%.*ls'.    |
|    10330    |    16    |    No    |    .Net Framework execution was aborted. %.*ls    |
|    10331    |    16    |    No    |    ALTER ASSEMBLY failed because serialization layout of type '%s' would change as a result of a change in type '%s' in the updated assembly. Persisted types are not allowed to change serialization layout.    |
|    10501    |    16    |    No    |    Type '%ls' in assembly '%.*ls' derives from a generic type which is not supported for a CLR Type.    |
|    [10502](../../relational-databases/errors-events/mssqlserver-10502-database-engine-error.md)    |    16    |    No    |    Cannot create plan guide '%.*ls' because type '%.*ls' provided is not allowed.    |
|    10503    |    16    |    No    |    Cannot create plan guide '%.*ls' because the statement specified by \@stmt and \@module_or_batch, or by \@plan_handle and \@statement_start_offset, matches the existing plan guide '%.*ls' in the database. Drop the existing plan guide before creating the new plan guide.    |
|    10504    |    16    |    No    |    Operation '%.*ls' is not allowed.    |
|    10505    |    16    |    No    |    Cannot create plan guide '%.*ls' because parameter \@hints is incorrect. Use N'OPTION ( <query_hint> [ ,...n ] )'.    |
|    10506    |    16    |    No    |    Cannot create plan guide '%.*ls' because value '%.*ls' provided for \@module_or_batch is not legal two-part name. Use 'schema_name.object_name'.    |
|    [10507](../../relational-databases/errors-events/mssqlserver-10507-database-engine-error.md)    |    16    |    No    |    Cannot create plan guide '%.*ls' because parameter \@stmt has more than one statement.    |
|    10508    |    16    |    No    |    Cannot create plan guide '%.*ls' because the statement specified by \@stmt and \@module_or_batch, or by \@plan_handle and \@statement_start_offset, does not match any statement in the specified module or batch. Modify the values to match a statement in the module or batch.    |
|    [10509](../../relational-databases/errors-events/mssqlserver-10509-database-engine-error.md)    |    16    |    No    |    Cannot '%ls' plan guide '%.*ls' because it does not exist or you do not have permission. Verify plan guide name and database of current session, and that you have needed permission.    |
|    10510    |    16    |    No    |    Cannot create plan guide '%.*ls' because the statement specified by \@stmt or \@statement_start_offset either contains a syntax error or is ineligible for use in a plan guide. Provide a single valid Transact-SQL statement or a valid starting position of the statement within the batch. To obtain a valid starting position, query the 'statement_start_offset' column in the sys.dm_exec_query_stats dynamic management function.    |
|    10512    |    16    |    No    |    Cannot create plan guide '%.*ls' because there is already a plan guide with that name in the database. Use a unique name.    |
|    10513    |    16    |    No    |    Cannot create plan guide '%.*ls' because object '\@module_or_batch' is encrypted. Consider tuning query using other techniques such as indexes and statistics.    |
|    10515    |    16    |    No    |    Cannot %S_MSG %S_MSG '%.*ls' because it is referenced by plan guide '%.*ls'. Use sp_control_plan_guide to drop the plan guide first. Record the plan guide definition for future use if needed.    |
|    10516    |    16    |    No    |    Cannot create plan guide '%.*ls' because the module '%.*ls' does not exist or you do not have needed permission.    |
|    10517    |    16    |    No    |    Cannot create plan guide '%.*ls' because \@module_or_batch can not be compiled.    |
|    10518    |    16    |    No    |    Cannot create plan guide '%.*ls' because you do not have needed permission. Alter database permission required.    |
|    [10519](../../relational-databases/errors-events/mssqlserver-10519-database-engine-error.md)    |    16    |    No    |    Cannot execute sp_control_plan_guide because of insufficient permissions to control plan guide '%.*ls'. Alter permission on object referenced by plan guide, or alter database permission required.    |
|    [10520](../../relational-databases/errors-events/mssqlserver-10520-database-engine-error.md)    |    16    |    No    |    Cannot create plan guide '%.*ls' because the hints specified in \@hints cannot be applied to the statement specified by either \@stmt or \@statement_start_offset. Verify that the hints can be applied to the statement.    |
|    [10521](../../relational-databases/errors-events/mssqlserver-10521-database-engine-error.md)    |    16    |    No    |    Cannot create plan guide '%.*ls' because \@type was specified as '%ls' and a non-NULL value is specified for the parameter '%ls'. This type requires a NULL value for the parameter. Specify NULL for the parameter, or change the type to one that allows a non-NULL value for the parameter.    |
|    10522    |    16    |    No    |    Cannot create plan guide '%.*ls' because \@type was specified as '%ls' and the parameter '%ls' is NULL. This type requires a non-NULL value for the parameter. Specify a non-NULL value for the parameter, or change the type to one that allows a NULL value for the parameter.    |
|    10523    |    16    |    No    |    Cannot create plan guide '%.*ls' because \@hints has illegal value. \@hints must be OPTION(PARAMETERIZATION FORCED) or OPTION(PARAMETERIZATION SIMPLE) if \@type is 'template'.    |
|    10524    |    16    |    No    |    Cannot generate query template because \@querytext does not contain a valid single query.    |
|    10525    |    10    |    No    |    Cannot parameterize \@querytext.    |
|    10526    |    16    |    No    |    Plan guide '%.*ls' matched statement after it was parameterized automatically by FORCED or SIMPLE parameterization, but the RECOMPILE hint it contains was ignored. RECOMPILE is not supported on automatically parameterized statements. Consider dropping this plan guide or removing RECOMPILE from it.    |
|    10527    |    16    |    No    |    Cannot drop %S_MSG '%.*ls' because its trigger '%.*ls' is referenced by plan guide '%.*ls'. Use sp_control_plan_guide to drop the plan guide first. Record the plan guide definition for future use if needed.    |
|    10528    |    16    |    No    |    Cannot create plan guide '%.*ls' because the object '%.*ls' is a temporary object.    |
|    10529    |    16    |    No    |    Cannot create plan guide '%.*ls' because its name is invalid. Plan guide name cannot begin with a '#' character.    |
|    10530    |    16    |    No    |    Cannot create plan guide '%.*ls' because there is already a planguide '%.*ls' of \@type 'template' on \@stmt.    |
|    [10531](../../relational-databases/errors-events/mssqlserver-10531-database-engine-error.md)    |    16    |    No    |    Cannot create plan guide '%.*ls' because the statement specified by \@statement_start_offset does not match any statement in specified module or batch. Consider modifying \@statement_start_offset to match a statement in module or batch.    |
|    [10532](../../relational-databases/errors-events/mssqlserver-10532-database-engine-error.md)    |    16    |    No    |    Cannot create plan guide '%.*ls' from cache because the user does not have adequate permissions. Grant the VIEW SERVER STATE permission to the user creating the plan guide.    |
|    [10533](../../relational-databases/errors-events/mssqlserver-10533-database-engine-error.md)    |    16    |    No    |    Cannot create plan guide '%.*ls' because the batch or module specified by \@plan_handle does not contain a statement that is eligible for a plan guide. Specify a different value for \@plan_handle.    |
|    [10534](../../relational-databases/errors-events/mssqlserver-10534-database-engine-error.md)    |    16    |    No    |    Cannot create plan guide '%.*ls' because the plan guide name exceeds 124, the maximum number of characters allowed. Specify a name that contains fewer than 125 characters.    |
|    [10535](../../relational-databases/errors-events/mssqlserver-10535-database-engine-error.md)    |    16    |    No    |    Cannot create plan guide '%.*ls' because the value specified for \@params is invalid. Specify the value in the form <parameter_name> <parameter_type>, or specify NULL.    |
|    [10536](../../relational-databases/errors-events/mssqlserver-10536-database-engine-error.md)    |    16    |    No    |    Cannot create plan guide '%.*ls' because a plan was not found in the plan cache that corresponds to the specified plan handle. Specify a cached plan handle. For a list of cached plan handles, query the sys.dm_exec_query_stats dynamic management view.    |
|    [10537](../../relational-databases/errors-events/mssqlserver-10537-database-engine-error.md)    |    16    |    No    |    Cannot create plan guide '%.*ls' because the batch or module corresponding to the specified \@plan_handle contains more than 1000 eligible statements. Create a plan guide for each statement in the batch or module by specifying a statement_start_offset value for each statement.    |
|    [10538](../../relational-databases/errors-events/mssqlserver-10538-database-engine-error.md)    |    16    |    No    |    Cannot enable plan guide '%.*ls' because the enabled plan guide '%.*ls' contains the same scope and starting offset value of the statement. Disable the existing plan guide before enabling the specified plan guide.    |
|    [10539](../../relational-databases/errors-events/mssqlserver-10539-database-engine-error.md)    |    16    |    No    |    Cannot find the plan guide either because the specified plan guide ID is NULL or invalid, or you do not have permission on the object referenced by the plan guide. Verify that the plan guide ID is valid, the current session is set to the correct database context, and you have ALTER permission on the object referenced by the plan guide or ALTER DATABASE permission.    |
|    10601    |    16    |    No    |    Cannot create plan guide '%.*ls' from cache because a query plan is not available for the statement with start offset %d.This problem can occur if the statement depends on database objects that have not yet been created. Make sure that all necessary database objects exist, and execute the statement before creating the plan guide.    |
|    10602    |    16    |    No    |    Cannot specify included columns for a clustered index.    |
|    10603    |    16    |    No    |    Mixing old and new syntax in CREATE/ALTER/DROP INDEX statement is not allowed.    |
|    10604    |    16    |    No    |    Cannot rebuild clustered index '%.*ls' on view '%.*ls' because the view is dependent on base table '%.*ls' whose clustered index '%.*ls' is disabled.    |
|    10605    |    16    |    No    |    Cannot convert a statistic to an index using DROP_EXISTING index option when ONLINE index option is also specified.    |
|    10606    |    16    |    No    |    Cannot disable primary key index "%.*ls" on table "%.*ls" because the table is published for replication.    |
|    10607    |    16    |    No    |    Cannot disable the clustered index "%.*ls" on view "%.*ls" because the indexed view is published for replication.    |
|    10608    |    16    |    No    |    The clustered index '%.*ls' on table '%.*ls' cannot be disabled because the table has change tracking enabled. Disable change tracking on the table before disabling the clustered index.    |
|    10609    |    16    |    No    |    The index '%.*ls' on table '%.*ls' cannot be disabled because the table has change tracking enabled. Change tracking requires a primary key constraint on the table, and disabling the index will drop the constraint. Disable change tracking on the table before disabling the index.    |
|    10610    |    16    |    No    |    Filtered %S_MSG '%.*ls' cannot be created on table '%.*ls' because the column '%.*ls' in the filter expression is a computed column. Rewrite the filter expression so that it does not include this column.    |
|    10611    |    16    |    No    |    Filtered index '%.*ls' cannot be created on object '%.*ls' because it is not a user table. Filtered indexes are only supported on tables. If you are trying to create a filtered index on a view, consider creating an indexed view with the filter expression incorporated in the view definition.    |
|    10612    |    16    |    No    |    Filtered %S_MSG '%.*ls' cannot be created on table '%.*ls' because the column '%.*ls' in the filter expression is compared with a constant of higher data type precedence or of a different collation. Converting a column to the data type of a constant is not supported for filtered %S_MSG. To resolve this error, explicitly convert the constant to the same data type and collation as the column '%.*ls'.    |
|    10617    |    16    |    No    |    Filtered %S_MSG '%.*ls' cannot be created on table '%.*ls' because the column '%.*ls' in the filter expression is compared with a constant that cannot be converted to the data type of the column. Rewrite the filter expression so that it does not include this comparison.    |
|    10618    |    16    |    No    |    Index '%.*ls' could not be created or rebuilt. The key length for this index (%d bytes) exceeds the maximum allowed length of '%d' bytes when using the vardecimal storage format.    |
|    10619    |    16    |    No    |    Cannot %S_MSG filtered index '%.*ls' on table '%.*ls' because the statement sets the IGNORE_DUP_KEY option to ON. Rewrite the statement so that it does not use the IGNORE_DUP_KEY option.    |
|    10620    |    16    |    No    |    Filtered %S_MSG '%.*ls' cannot be created on table '%.*ls' because the column '%.*ls' in the filter expression is of a CLR data type. Rewrite the filter expression so that it does not include this column.    |
|    10621    |    16    |    No    |    Filtered %S_MSG '%.*ls' cannot be created on table '%.*ls' because the filter expression contains a comparison with a literal NULL value. Rewrite the comparison to use the IS [NOT] NULL comparison operator to test for NULL values.    |
|    10622    |    16    |    No    |    The index '%.*ls' on table '%.*ls' could not be created because the column '%.*ls' in the filter expression of the index is a column set.    |
|    10623    |    16    |    No    |    Index '%.*ls' could not be created or rebuilt. A compressed index is not supported on table that contains sparse columns or a column set column.    |
|    10700    |    16    |    No    |    Filtered statistics '%.*ls' cannot be created on object '%.*ls' because it is not a user table. Filtered statistics are only supported on user tables.    |
|    10701    |    15    |    No    |    The table-valued parameter "%.*ls" is READONLY and cannot be modified.    |
|    10702    |    15    |    No    |    The READONLY option cannot be used in an EXECUTE or CREATE AGGREGATE statement.    |
|    10703    |    15    |    No    |    The WITH CUBE and WITH ROLLUP options are not permitted with a ROLLUP, CUBE, or GROUPING SETS specification.    |
|    10705    |    15    |    No    |    Too many grouping sets. The maximum number is %d.    |
|    10706    |    15    |    No    |    Subqueries are not allowed in the OUTPUT clause.    |
|    10707    |    15    |    No    |    Too many expressions are specified in the GROUP BY clause. The maximum number is %d when grouping sets are supplied.    |
|    10708    |    15    |    No    |    The CUBE() and ROLLUP() grouping constructs are not allowed in the current compatibility mode. They are only allowed in 100 mode or higher.    |
|    10709    |    15    |    No    |    DEFAULT is not allowed on the right hand side of "%.*ls"    |
|    10710    |    15    |    No    |    The number of columns for each row in a table value constructor must be the same.    |
|    10711    |    15    |    No    |    An action of type '%S_MSG' is not allowed in the 'WHEN NOT MATCHED' clause of a MERGE statement.    |
|    10712    |    15    |    No    |    An action of type 'INSERT' is not allowed in the '%S_MSG' clause of a MERGE statement.    |
|    10713    |    15    |    No    |    Non-ANSI outer join operators ("*=" or "=*") are not allowed in a MERGE statement. Use the OUTER JOIN keywords instead.    |
|    10714    |    15    |    No    |    A MERGE statement must be terminated by a semi-colon (;).    |
|    10716    |    15    |    No    |    An action of type '%S_MSG' cannot appear more than once in a '%S_MSG' clause of a MERGE statement.    |
|    10717    |    15    |    No    |    A nested INSERT, UPDATE, DELETE, or MERGE statement must have an OUTPUT clause.    |
|    10718    |    15    |    No    |    The %S_MSG clause is not allowed when the FROM clause contains a nested INSERT, UPDATE, DELETE, or MERGE statement.    |
|    10719    |    15    |    No    |    Query hints are not allowed in nested INSERT, UPDATE, DELETE, or MERGE statements.    |
|    10720    |    15    |    No    |    Non-ANSI outer join operators ("*=" or "=*") are not allowed in a nested INSERT, UPDATE, DELETE, or MERGE statement. Use the OUTER JOIN keywords instead.    |
|    10721    |    15    |    No    |    An OUTPUT INTO clause is not allowed in a nested INSERT, UPDATE, DELETE, or MERGE statement.    |
|    10722    |    15    |    No    |    The WHERE CURRENT OF clause is not allowed in a nested INSERT, UPDATE, DELETE, or MERGE statement.    |
|    10723    |    15    |    No    |    The DISTINCT keyword is not allowed when the FROM clause contains a nested INSERT, UPDATE, DELETE, or MERGE statement.    |
|    10724    |    15    |    No    |    In a MERGE statement, a variable cannot be set to a column and expression in the same assignment in the SET clause of an UPDATE action. Assignments of the form 'SET \@variable = column = expression' are not valid in the SET clause of an UPDATE action in a MERGE statement. Modify the SET clause to only specify assignments of the form 'SET \@variable = column' or 'SET \@variable = expression'.    |
|    10725    |    15    |    No    |    The FORCESEEK hint is not allowed for target tables of INSERT, UPDATE, or DELETE statements.    |
|    10726    |    15    |    No    |    Cannot use the VARYING option in a DECLARE, CREATE AGGREGATE or CREATE FUNCTION statement.    |
|    10727    |    15    |    No    |    User defined aggregates do not support default parameters.    |
|    10728    |    15    |    No    |    A nested INSERT, UPDATE, DELETE, or MERGE statement is not allowed on either side of a JOIN or APPLY operator.    |
|    10729    |    15    |    No    |    A nested INSERT, UPDATE, DELETE, or MERGE statement is not allowed as the table source of a PIVOT or UNPIVOT operator.    |
|    10730    |    15    |    No    |    A nested INSERT, UPDATE, DELETE, or MERGE statement is not allowed in a SELECT statement that is not the immediate source of rows for an INSERT statement.    |
|    10731    |    15    |    No    |    A nested INSERT, UPDATE, DELETE, or MERGE statement is not allowed in the FROM clause of an UPDATE or DELETE statement.    |
|    10732    |    15    |    No    |    A nested INSERT, UPDATE, DELETE, or MERGE statement is not allowed inside another nested INSERT, UPDATE, DELETE, or MERGE statement.    |
|    10733    |    15    |    No    |    A nested INSERT, UPDATE, DELETE, or MERGE statement is not allowed on either side of a UNION, INTERSECT, or EXCEPT operator.    |
|    10734    |    16    |    No    |    A nested INSERT, UPDATE, DELETE, or MERGE statement is not allowed in the USING clause of a MERGE statement.    |
|    10735    |    15    |    No    |    Variable assignment is not allowed in a statement containing a top level UNION, INTERSECT or EXCEPT operator.    |
|    10736    |    15    |    No    |    Incorrect WHERE clause for filtered %S_MSG '%.*ls' on table '%.*ls'.    |
|    [10737](../../relational-databases/errors-events/mssqlserver-10737-database-engine-error.md)    |    15    |    No    |    A full-text stoplist statement must be terminated by a semi-colon (;).    |
|    10738    |    15    |    No    |    In an ALTER TABLE REBUILD or ALTER INDEX REBUILD statement, when a partition is specified in a DATA_COMPRESSION clause, PARTITION=ALL must be specified. The PARTITION=ALL clause is used to reinforce that all partitions of the table or index will be rebuilt, even if only a subset is specified in the DATA_COMPRESSION clause.    |
|    [10770](../../relational-databases/errors-events/mssqlserver-10770-database-engine-error.md)    |    15    |    No    |    The number of row value expressions in the INSERT statement exceeds the maximum allowed number of %d row values.    |
|    [10771](../../relational-databases/errors-events/mssqlserver-10771-database-engine-error.md)    |        |        |    The *construct* '*feature*' is not yet implemented with memory optimized tables.    |
|    [10772](../../relational-databases/errors-events/mssqlserver-10772-database-engine-error.md)    |        |        |    The *construct* '*feature*' is not supported with natively compiled stored procedures.    |
|    [10773](../../relational-databases/errors-events/mssqlserver-10773-database-engine-error.md)    |        |        |    The *construct* '*feature*' is not yet implemented with natively compiled stored procedures.    |
|    [10785](../../relational-databases/errors-events/mssqlserver-10785-database-engine-error.md)    |        |        |    The *construct* '*feature*' is not supported with an active transaction that accesses memory optimized tables or natively compiled stored procedures.    |
|    [10787](../../relational-databases/errors-events/mssqlserver-10787-database-engine-error.md)    |        |        |    The *construct* '*feature*' is not supported with a hash index.    |
|    [10794](../../relational-databases/errors-events/mssqlserver-10794-database-engine-error.md)    |        |        |    The *construct* '*feature*' is not supported with *construct*.    |
|    10900    |    16    |    No    |    The insert column list used in the MERGE statement cannot contain multi-part identifiers. Use single part identifiers instead.    |
|    10901    |    16    |    No    |    Failed to configure resource governor during startup. Check SQL Server error log for specific error messages or check the consistency of master database by running DBCC CHECKCATALOG('master').    |
|    10902    |    16    |    No    |    User does not have permission to alter the resource governor configuration.    |
|    10903    |    16    |    No    |    User-defined function '%s' does not exist in master database, or the user does not have permission to access it.    |
|    10904    |    16    |    No    |    The specified schema name '%.*ls' for classifier user-defined function either does not exist, or the user does not have permission to use it.    |
|    10905    |    16    |    No    |    Resource governor configuration failed. There are active sessions in workload groups being dropped or moved to different resource pools. Disconnect all active sessions in the affected workload groups and try again.    |
|    10906    |    16    |    No    |    Could not complete resource governor configuration because there is not enough memory. Reduce the server load or try the operation on a dedicated administrator connection.    |
|    10907    |    16    |    No    |    The object '%.*ls'.'%.*ls' is not a valid resource governor classifier user-defined function. A valid classifier user-defined function must be schema-bound, return sysname, and have no parameters.    |
|    10908    |    16    |    No    |    Attribute '%.*ls' with value of %u is greater than attribute '%.*ls' with value of %u.    |
|    10909    |    16    |    No    |    Attribute '%.*ls' with value of %u is less than attribute '%.*ls' with value of %u.    |
|    10910    |    16    |    No    |    The resource pool cannot be created. The maximum number of resource pools cannot exceed current limit of %u including predefined resource pools.    |
|    10911    |    16    |    No    |    The operation could not be completed. The specified '%.*ls' value, %u, causes the sum of minimums on all resource pools to exceed 100 percent. Reduce the value or modify other resource pools so that the sum is less than 100.    |
|    10912    |    16    |    No    |    Requested operation cannot be performed because the resource pool '%.*ls' does not exist.    |
|    10913    |    16    |    No    |    The operation could not be completed. Dropping predefined %S_MSG is not allowed.    |
|    10914    |    16    |    No    |    Users are not allowed to %S_MSG the workload group '%.*ls' in the '%.*ls' resource pool.    |
|    10915    |    16    |    No    |    The name of the %S_MSG '%.*ls' cannot begin with # of ##.    |
|    10916    |    16    |    No    |    The operation could not be completed. Altering '%.*ls' %S_MSG is not allowed.    |
|    10917    |    16    |    No    |    Cannot drop resource pool '%.*ls' because it contains workload group '%.*ls'. Drop or remove all workload groups using this resource pool before dropping it.    |
|    10918    |    16    |    No    |    ALTER WORKLOAD GROUP failed. Either a 'WITH' or 'USING' clause must be specified.    |
|    10919    |    16    |    No    |    Cannot create %S_MSG '%.*ls' because it already exists.    |
|    10920    |    16    |    No    |    An error occurred while reading the resource governor configuration from master database. Check the integrity of master database or contact the system administrator.    |
|    10921    |    16    |    No    |    Cannot %S_MSG user-defined function '%.*ls'. It is being used as a resource governor classifier.    |
|    10922    |    16    |    No    |    The '%.*ls' %S_MSG cannot be moved out of '%.*ls' %S_MSG.    |
|    10923    |    16    |    No    |    %ls failed. Rerun the statement.    |
|    10981    |    10    |    No    |    %ls failed. The resource governor is not available in this edition of SQL Server. You can manipulate resource governor metadata but you will not be able to apply resource governor configuration. Only Enterprise edition of SQL Server supports resource governor.    |
|    10982    |    16    |    Yes    |    Resource governor reconfiguration succeeded.    |
|    10983    |    16    |    No    |    Failed to run resource governor classifier user-defined function. See previous errors in SQL Server error log from session ID %ld for details. Classifier elapsed time: %I64u ms.    |
|    10984    |    16    |    No    |    Resource governor '%ls' operation was canceled by user.    |
