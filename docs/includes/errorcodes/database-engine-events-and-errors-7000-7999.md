---
author: MashaMSFT
ms.author: mathoma
ms.reviewer: randolphwest
ms.date: 08/19/2022
ms.topic: include
---
| Error| Severity | Event Logged | Description|
| :------ | :------| :------| :----------------------------- |
|    7000    |    16    |    No    |    OPENXML document handle parameter must be of data type int.    |
|    7001    |    16    |    No    |    OPENXML flags parameter must be of data type int.    |
|    7002    |    16    |    No    |    OPENXML XPath must be of a string data type, such as nvarchar.    |
|    7003    |    16    |    No    |    Only one OPENXML column can be of type %ls.    |
|    7004    |    16    |    No    |    OPENXML does not support retrieving schema from remote tables, as in '%.*ls'.    |
|    7005    |    16    |    No    |    OPENXML requires a metaproperty namespace to be declared if 'mp' is used for another namespace in sp_xml_preparedocument.    |
|    7006    |    16    |    No    |    OPENXML encountered a problem identifying the metaproperty namespace prefix. Consider removing the namespace parameter from the corresponding sp_xml_preparedocument statement.    |
|    7007    |    16    |    No    |    OPENXML encountered unknown metaproperty '%.*ls'.    |
|    7008    |    16    |    No    |    The OPENXML EDGETABLE is incompatible with the XMLTEXT OVERFLOW flag.    |
|    7009    |    16    |    No    |    OPENXML allows only one metaproperty namespace prefix declaration in sp_xml_preparedocument.    |
|    7101    |    16    |    No    |    You need an active user transaction in order to use text pointers for a table with the option "text in row" set to ON.    |
|    7102    |    20    |    Yes    |    Internal Error: Text manager cannot continue with current statement. Run DBCC CHECKTABLE.    |
|    7104    |    16    |    No    |    Offset or size of data type is not valid. Data type must be of type int or smallint.    |
|    [7105](../../relational-databases/errors-events/mssqlserver-7105-database-engine-error.md)    |    22    |    Yes    |    The Database ID %d, Page %S_PGID, slot %d for LOB data type node does not exist. This is usually caused by transactions that can read uncommitted data on a data page. Run DBCC CHECKTABLE.    |
|    7106    |    16    |    Yes    |    Internal error: An attempt was made to update a LOB data type using a read-only text pointer.    |
|    7107    |    16    |    No    |    You can have only 1,024 in-row text pointers in one transaction    |
|    7108    |    22    |    Yes    |    Database ID %d, page %S_PGID, slot %d, link number %d is invalid. Run DBCC CHECKTABLE.    |
|    7116    |    16    |    No    |    Offset %d is not in the range of available LOB data.    |
|    7117    |    16    |    No    |    Error reading large object (LOB) data from the tabular data stream (TDS).    |
|    7118    |    16    |    No    |    Only complete replacement is supported when assigning a large object (LOB) to itself.    |
|    7119    |    16    |    No    |    Attempting to grow LOB beyond maximum allowed size of %I64d bytes.    |
|    7122    |    16    |    No    |    Invalid text, ntext, or image pointer type. Must be binary(16).    |
|    7123    |    16    |    No    |    Invalid text, ntext, or image pointer value %hs.    |
|    7124    |    16    |    No    |    The offset and length specified in the READTEXT statement is greater than the actual data length of %ld.    |
|    7125    |    16    |    No    |    The text, ntext, or image pointer value conflicts with the column name specified.    |
|    7133    |    16    |    No    |    NULL textptr (text, ntext, or image pointer) passed to %ls function.    |
|    7134    |    16    |    No    |    LOB Locator is not supported as text pointer when using UPDATETEXT/WRITETEXT to update/write a text column.    |
|    7135    |    16    |    No    |    Deletion length %ld is not in the range of available text, ntext, or image data.    |
|    7137    |    16    |    No    |    %s is not allowed because the column is being processed by a concurrent snapshot or is being replicated to a non-SQL Server Subscriber or Published in a publication allowing Data Transformation Services (DTS) or tracked by Change Data Capture.    |
|    7138    |    16    |    No    |    The WRITETEXT statement is not allowed because the column is being replicated with Data Transformation Services (DTS) or tracked by Change Data Capture.    |
|    7139    |    16    |    No    |    Length of LOB data (%I64d) to be replicated exceeds configured maximum %ld.    |
|    7140    |    16    |    No    |    Cannot create additional orphans with the stored procedure sp_createorphan. Free up some of the orphan handles that you have created by inserting or deleting them.    |
|    7141    |    16    |    No    |    Must create orphaned text inside a user transaction.    |
|    7143    |    16    |    No    |    Invalid locator de-referenced.    |
|    7144    |    16    |    No    |    A text/ntext/image column referenced by a persisted or indexed computed column cannot be updated    |
|    7151    |    16    |    No    |    Insufficient buffer space to perform write operation.    |
|    7201    |    17    |    No    |    Could not execute procedure on remote server '%.*ls' because SQL Server is not configured for remote access. Ask your system administrator to reconfigure SQL Server to allow remote access.    |
|    7202    |    11    |    No    |    Could not find server '%.*ls' in sys.servers. Verify that the correct server name was specified. If necessary, execute the stored procedure sp_addlinkedserver to add the server to sys.servers.    |
|    7212    |    16    |    No    |    Could not execute procedure '%.*ls' on remote server '%.*ls'.    |
|    7213    |    20    |    Yes    |    The attempt by the provider to pass remote stored procedure parameters to remote server '%.*ls' failed. Verify that the number of parameters, the order, and the values passed are correct.    |
|    7214    |    16    |    Yes    |    Remote procedure time out of %d seconds exceeded. Remote procedure '%.*ls' is canceled.    |
|    7215    |    16    |    No    |    Could not execute statement on remote server '%.*ls'.    |
|    7221    |    16    |    No    |    Could not relay results of procedure '%.*ls' from remote server '%.*ls'.    |
|    7301    |    16    |    No    |    Cannot obtain the required interface ("%ls") from OLE DB provider "%ls" for linked server "%ls".    |
|    7302    |    16    |    No    |    Cannot create an instance of OLE DB provider "%ls" for linked server "%ls".    |
|    7303    |    16    |    No    |    Cannot initialize the data source object of OLE DB provider "%ls" for linked server "%ls".    |
|    7304    |    16    |    No    |    Cannot connect using OLE DB provider "%ls" to linked server "%ls". Verify the connection parameters or login credentials associated with this linked server.    |
|    7305    |    16    |    No    |    Cannot create a statement object using OLE DB provider "%ls" for linked server "%ls".    |
|    7306    |    16    |    No    |    Cannot open the table "%ls" from OLE DB provider "%ls" for linked server "%ls". %ls    |
|    7307    |    16    |    No    |    Cannot obtain the data source of a session from OLE DB provider "%ls" for linked server "%ls". This action must be supported by the provider.    |
|    [7308](../../relational-databases/errors-events/mssqlserver-7308-database-engine-error.md)    |    16    |    No    |    OLE DB provider '%ls' cannot be used for distributed queries because the provider is configured to run in single-threaded apartment mode.    |
|    7310    |    16    |    No    |    Cannot obtain the set of schema rowsets supported by OLE DB provider "%ls" for linked server "%ls". The provider supports the interface, but returns a failure code when it is used.    |
|    7311    |    16    |    No    |    Cannot obtain the schema rowset "%ls" for OLE DB provider "%ls" for linked server "%ls". The provider supports the interface, but returns a failure code when it is used.    |
|    7312    |    16    |    No    |    Invalid use of schema or catalog for OLE DB provider "%ls" for linked server "%ls". A four-part name was supplied, but the provider does not expose the necessary interfaces to use a catalog or schema.    |
|    7313    |    16    |    No    |    An invalid schema or catalog was specified for the provider "%ls" for linked server "%ls".    |
|    7314    |    16    |    No    |    The OLE DB provider "%ls" for linked server "%ls" does not contain the table "%ls". The table either does not exist or the current user does not have permissions on that table.    |
|    7315    |    16    |    No    |    The OLE DB provider "%ls" for linked server "%ls" contains multiple tables that match the name "%ls".    |
|    7316    |    16    |    No    |    Cannot use qualified table names (schema or catalog) with the OLE DB provider "%ls" for linked server "%ls" because it does not implement required functionality.    |
|    7317    |    16    |    No    |    The OLE DB provider "%ls" for linked server "%ls" returned an invalid schema definition.    |
|    7318    |    16    |    No    |    The OLE DB provider "%ls" for linked server "%ls" returned an invalid column definition for table "%ls".    |
|    7319    |    16    |    No    |    The OLE DB provider "%ls" for linked server "%ls" returned a "%ls" index "%ls" with the incorrect bookmark ordinal %d.    |
|    7320    |    16    |    No    |    Cannot execute the query "%ls" against OLE DB provider "%ls" for linked server "%ls". %ls    |
|    7321    |    16    |    No    |    An error occurred while preparing the query "%ls" for execution against OLE DB provider "%ls" for linked server "%ls". %ls    |
|    7322    |    16    |    No    |    A failure occurred while giving parameter information to OLE DB provider "%ls" for linked server "%ls".    |
|    7323    |    16    |    No    |    An error occurred while submitting the query text to OLE DB provider "%ls" for linked server "%ls".    |
|    7324    |    16    |    No    |    A failure occurred while setting parameter properties with OLE DB provider "%ls" for linked server "%ls".    |
|    7325    |    16    |    No    |    Objects exposing columns with CLR types are not allowed in distributed queries. Please use a pass-through query to access remote object '%ls'.    |
|    7330    |    16    |    No    |    Cannot fetch a row from OLE DB provider "%ls" for linked server "%ls".    |
|    7331    |    16    |    No    |    Rows from OLE DB provider "%ls" for linked server "%ls" cannot be released.    |
|    7332    |    16    |    No    |    Cannot rescan the result set from OLE DB provider "%ls" for linked server "%ls". %ls    |
|    7333    |    16    |    No    |    Cannot fetch a row using a bookmark from OLE DB provider "%ls" for linked server "%ls".    |
|    7339    |    16    |    No    |    OLE DB provider '%ls' for linked server '%ls' returned invalid data for column '%ls.%ls'.    |
|    7340    |    16    |    No    |    Cannot create a column accessor for OLE DB provider "%ls" for linked server "%ls".    |
|    7341    |    16    |    No    |    Cannot get the current row value of column "%ls.%ls" from OLE DB provider "%ls" for linked server "%ls". %ls    |
|    7342    |    16    |    No    |    An unexpected NULL value was returned for column "%ls.%ls" from OLE DB provider "%ls" for linked server "%ls". This column cannot be NULL.    |
|    7343    |    16    |    No    |    The OLE DB provider "%ls" for linked server "%ls" could not %ls table "%ls". %ls    |
|    7344    |    16    |    No    |    The OLE DB provider "%ls" for linked server "%ls" could not %ls table "%ls" because of column "%ls". %ls    |
|    7345    |    16    |    No    |    The OLE DB provider "%ls" for linked server "%ls" could not delete from table "%ls". %ls    |
|    7346    |    16    |    No    |    Cannot get the data of the row from the OLE DB provider "%ls" for linked server "%ls". %ls    |
|    7347    |    16    |    No    |    OLE DB provider '%ls' for linked server '%ls' returned data that does not match expected data length for column '%ls.%ls'. The (maximum) expected data length is %ls, while the returned data length is %ls.    |
|    7348    |    16    |    No    |    The OLE DB provider "%ls" for linked server "%ls" could not set the range for table "%ls". %ls. For possible cause of this issue, see the extended error message.    |
|    7349    |    16    |    No    |    The OLE DB provider "%ls" for linked server "%ls" could not set the range for table "%ls" because of column "%ls". %ls    |
|    7350    |    16    |    No    |    Cannot get the column information from OLE DB provider "%ls" for linked server "%ls".    |
|    7351    |    16    |    No    |    The OLE DB provider "%ls" for linked server "%ls" could not map ordinals for one or more columns of object "%ls".    |
|    7352    |    16    |    No    |    The OLE DB provider "%ls" for linked server "%ls" supplied inconsistent metadata. The object "%ls" was missing the expected column "%ls".    |
|    7353    |    16    |    No    |    The OLE DB provider "%ls" for linked server "%ls" supplied inconsistent metadata. An extra column was supplied during execution that was not found at compile time.    |
|    7354    |    16    |    No    |    The OLE DB provider "%ls" for linked server "%ls" supplied invalid metadata for column "%ls". %ls    |
|    7355    |    16    |    No    |    The OLE DB provider "%ls" for linked server "%ls" supplied inconsistent metadata for a column. The name was changed at execution time.    |
|    7356    |    16    |    No    |    The OLE DB provider "%ls" for linked server "%ls" supplied inconsistent metadata for a column. The column "%ls" (compile-time ordinal %ld) of object "%ls" was reported to have a "%ls" of %ld at compile time and %ld at run time.    |
|    7357    |    16    |    No    |    Cannot process the object "%ls". The OLE DB provider "%ls" for linked server "%ls" indicates that either the object has no columns or the current user does not have permissions on that object.    |
|    7358    |    16    |    No    |    Cannot execute the query. The OLE DB provider "%ls" for linked server "%ls" did not provide an appropriate interface to access the text, ntext, or image column "%ls.%ls".    |
|    7359    |    16    |    No    |    The OLE DB provider "%ls" for linked server "%ls" reported a change in schema version between compile time ("%ls") and run time ("%ls") for table "%ls".    |
|    7360    |    16    |    No    |    Cannot get the length of a storage object from OLE DB provider "%ls" for linked server "%ls" for table "%ls", column "%ls".    |
|    7361    |    16    |    No    |    Cannot read a storage object from OLE DB provider "%ls" for linked server "%ls", for table "%ls", column "%ls".    |
|    7362    |    16    |    No    |    The OLE DB provider "%ls" for linked server "%ls" reported different metadata at run time for table "%ls", column "%ls".    |
|    7365    |    16    |    No    |    Cannot obtain optional metadata columns of columns rowset from OLE DB provider "%ls" for linked server "%ls".    |
|    7366    |    16    |    No    |    Cannot obtain columns rowset from OLE DB provider "%ls" for linked server "%ls".    |
|    7367    |    16    |    No    |    The OLE DB provider "%ls" for linked server "%ls" supports column level collation, but failed to provide the metadata column "%ls" at run time.    |
|    7368    |    16    |    No    |    The OLE DB provider "%ls" for linked server "%ls" supports column level collation, but failed to provide collation data for column "%ls".    |
|    7369    |    16    |    No    |    The OLE DB provider '%ls' for linked server '%ls' provided invalid collation. LCID = %08x, Compflags = %08x, SortOrder = '%.*ls'.    |
|    7370    |    16    |    No    |    One or more properties could not be set on the query for OLE DB provider "%ls" for linked server "%ls". %ls    |
|    7371    |    16    |    No    |    The server option 'collation name' in linked server '%ls' for OLE DB provider '%ls' has collation id %08x which is not supported by SQL Server.    |
|    7372    |    16    |    No    |    Cannot get properties from OLE DB provider "%ls" for linked server "%ls".    |
|    7373    |    16    |    No    |    Cannot set the initialization properties for OLE DB provider "%ls" for linked server "%ls".    |
|    7374    |    16    |    No    |    Cannot set the session properties for OLE DB provider "%ls" for linked server "%ls".    |
|    7375    |    16    |    No    |    Cannot open index "%ls" on table "%ls" from OLE DB provider "%ls" for linked server "%ls". %ls    |
|    7376    |    16    |    No    |    Could not enforce the remote join hint for this query.    |
|    7377    |    16    |    No    |    Cannot specify an index hint for a remote data source.    |
|    7380    |    16    |    No    |    Table-valued parameters are not allowed in remote calls between servers.    |
|    7390    |    16    |    No    |    The requested operation could not be performed because OLE DB provider "%ls" for linked server "%ls" does not support the required transaction interface.    |
|    7391    |    16    |    No    |    The operation could not be performed because OLE DB provider "%ls" for linked server "%ls" was unable to begin a distributed transaction.    |
|    7392    |    16    |    No    |    Cannot start a transaction for OLE DB provider "%ls" for linked server "%ls".    |
|    7393    |    16    |    No    |    The OLE DB provider "%ls" for linked server "%ls" reported an error 0x%08X aborting the current transaction.    |
|    7394    |    16    |    No    |    The OLE DB provider "%ls" for linked server "%ls" reported an error committing the current transaction.    |
|    7395    |    16    |    No    |    Unable to start a nested transaction for OLE DB provider "%ls" for linked server "%ls". A nested transaction was required because the XACT_ABORT option was set to OFF.    |
|    7396    |    16    |    No    |    Varchar(max), nvarchar(max), varbinary(max) and large CLR type data types are not supported as return value or output parameter to remote queries.    |
|    7397    |    16    |    No    |    Remote function returned varchar(max), nvarchar(max), varbinary(max) or large CLR type value which is not supported.    |
|    7399    |    16    |    No    |    The OLE DB provider "%ls" for linked server "%ls" reported an error. %ls    |
|    7401    |    16    |    No    |    The OLE DB provider "%ls" returned invalid literal prefix/suffix string.    |
|    7403    |    16    |    No    |    The OLE DB provider "%ls" has not been registered.    |
|    7404    |    16    |    No    |    The server could not load DCOM.    |
|    7405    |    16    |    No    |    Heterogeneous queries require the ANSI_NULLS and ANSI_WARNINGS options to be set for the connection. This ensures consistent query semantics. Enable these options and then reissue your query.    |
|    7409    |    16    |    No    |    Could not start distributed query using integrated login because the user is logged in using SQL Server Authentication. Provide remote server login user ID and password in the connection string.    |
|    7410    |    16    |    No    |    Remote access is not allowed for impersonated security context.    |
|    7411    |    16    |    No    |    Server '%.*ls' is not configured for %ls.    |
|    7412    |    16    |    No    |    OLE DB provider "%ls" for linked server "%ls" returned message "%ls".    |
|    7413    |    16    |    No    |    Could not connect to linked server '%ls' (OLE DB Provider '%ls'). Enable delegation or use a remote SQL Server login for the current user.    |
|    7414    |    16    |    No    |    Invalid number of parameters. Rowset '%ls' expects %d parameter(s).    |
|    7415    |    16    |    No    |    Ad hoc access to OLE DB provider '%ls' has been denied. You must access this provider through a linked server.    |
|    7416    |    16    |    No    |    Access to the remote server is denied because no login-mapping exists.    |
|    7417    |    16    |    No    |    GROUP BY ALL is not supported in queries that access remote tables if there is also a WHERE clause in the query.    |
|    7418    |    16    |    No    |    Text, image, or ntext column was too large to send to the remote data source due to the storage interface used by the provider.    |
|    7419    |    16    |    No    |    Lazy schema validation error. Linked server schema version has changed. Re-run the query.    |
|    7420    |    16    |    No    |    Remote access is not supported for transaction isolation level "%ls".    |
|    7421    |    10    |    No    |    Cannot fetch the rowset from OLE DB provider "%ls" for linked server "%ls". %ls.    |
|    7422    |    16    |    No    |    OLE DB provider "%ls" for linked server "%ls" returned an invalid index definition for table "%ls".    |
|    7423    |    16    |    No    |    The '%ls' OLE DB provider for the '%ls' linked server returned an invalid CLR type definition for the '%ls' table.    |
|    7424    |    10    |    No    |    OLE DB provider "%ls" for linked server "%ls" returned "%ls" with data type "%ls", which should be type "%ls".    |
|    7425    |    10    |    No    |    OLE DB provider "%ls" for linked server "%ls" returned an incorrect value for "%ls", which should be "%ls".    |
|    7426    |    10    |    No    |    OLE DB provider "%ls" for linked server "%ls" returned "%ls" without "%ls" being supported.    |
|    7427    |    10    |    No    |    OLE DB provider "%ls" for linked server "%ls" returned "%ls" for "%ls" during statistics gathering.    |
|    7428    |    10    |    No    |    OLE DB provider "%ls" for linked server "%ls" supported the schema lock interface, but returned "%ls" for "%ls".    |
|    7429    |    10    |    No    |    %hs SQL Server Remote Metadata Gather Time for Table %s.%s:%hs, CPU time = %lu ms, elapsed time = %lu ms.    |
|    7430    |    16    |    No    |    Out-of-process use of OLE DB provider "%ls" with SQL Server is not supported.    |
|    7431    |    16    |    No    |    Unable to delete OLE DB parameter properties.    |
|    7432    |    16    |    No    |    Heterogeneous queries and use of OLEDB providers are not supported in fiber mode.    |
|    7433    |    10    |    No    |    OLE DB provider '%ls' for linked server '%ls' returned truncated data for column '%ls.%ls'. The actual data length is %ls and truncated data length is %ls.    |
|    7435    |    16    |    No    |    The OLE DB provider "%ls" for linked server "%ls" returned unexpected NULL pointer for string column "%ls.%ls".    |
|    7601    |    16    |    No    |    Cannot use a CONTAINS or FREETEXT predicate on %S_MSG '%.*ls' because it is not full-text indexed.    |
|    7604    |    17    |    No    |    Full-text operation failed due to a time out.    |
|    7606    |    17    |    No    |    Could not find full-text index for database ID %d, table or indexed view ID %d.    |
|    7607    |    17    |    No    |    Search on full-text catalog '%ls' for database ID %d, table or indexed view ID %d with search condition '%ls' failed with unknown result (0x%x).    |
|    7608    |    16    |    No    |    An unknown full-text failure (0x%x) occurred during "%hs".    |
|    7609    |    17    |    No    |    Full-Text Search is not installed, or a full-text component cannot be loaded.    |
|    7610    |    16    |    No    |    Access is denied to "%.*ls", or the path is invalid.    |
|    7613    |    16    |    No    |    Cannot drop index '%.*ls' because it enforces the full-text key for table or indexed view '%.*ls'.    |
|    7614    |    16    |    No    |    Cannot alter or drop column '%.*ls' because it is enabled for Full-Text Search.    |
|    7615    |    16    |    No    |    A CONTAINS or FREETEXT predicate can only operate on one table or indexed view. Qualify the use of * with a table or indexed view name.    |
|    7616    |    16    |    No    |    Full-Text Search is not enabled for the current database. Use sp_fulltext_database to enable full-text search for the database. The functionality to disable and enable full-text search for a database is deprecated. Please change your application.    |
|    7617    |    16    |    No    |    Query does not reference the full-text indexed table or indexed view, or user does not have permission to perform this action.    |
|    7619    |    16    |    No    |    The execution of a full-text query failed. "%ls"    |
|    7620    |    16    |    No    |    The conversion to data type %ls failed for the full-text search key.    |
|    7621    |    16    |    No    |    Invalid use of full-text predicate in the HAVING clause.    |
|    7622    |    17    |    No    |    There is not sufficient disk space to complete this operation for the full-text catalog "%ls".    |
|    7624    |    16    |    No    |    Full-text catalog '%ls' is in an unusable state. Drop and re-create this full-text catalog.    |
|    7625    |    16    |    No    |    Full-text table or indexed view has more than one LCID among its full-text indexed columns.    |
|    7626    |    15    |    No    |    The top_n_by_rank argument ('%d') must be greater than or equal to zero.    |
|    7627    |    16    |    No    |    Cannot create the full-text catalog in the directory "%.*ls" for the clustered server. Only directories on a disk in the cluster group of the server can be used.    |
|    7629    |    17    |    No    |    Cannot open or query the full-text default path registry key. The full-text default catalog path is invalid.    |
|    7630    |    15    |    No    |    Syntax error near '%.*ls' in the full-text search condition '%.*ls'.    |
|    7632    |    15    |    No    |    The value of the Weight argument must be between 0.0 and 1.0.    |
|    7636    |    10    |    No    |    Warning: Request to start a full-text index population on table or indexed view '%.*ls' is ignored because a population is currently active for this table or indexed view.    |
|    7638    |    10    |    No    |    Warning: Request to stop change tracking has deleted all changes tracked on table or indexed view '%ls'.    |
|    7640    |    10    |    No    |    Warning: Request to stop tracking changes on table or indexed view '%.*ls' will not stop population currently in progress on the table or indexed view.    |
|    7641    |    16    |    No    |    Full-Text catalog '%ls' does not exist in database '%.*ls' or user does not have permission to perform this action.    |
|    7642    |    16    |    No    |    A full-text catalog named '%ls' already exists in this database. Use a different name.    |
|    7644    |    16    |    No    |    Full-text crawl manager has not been initialized. Any crawl started before the crawl manager was fully initialized will need to be restarted. Please restart SQL Server and retry the command. You should also check the error log to fix any failures that might have caused the crawl manager to fail.    |
|    7645    |    16    |    No    |    Null or empty full-text predicate.    |
|    7646    |    16    |    No    |    Fulltext predicate references columns from two different tables or indexed views '%.*ls' and '%.*ls' which is not allowed.    |
|    7647    |    10    |    No    |    Warning: Configuration of full-text catalog at '%ls' could not be saved during detach database.    |
|    7648    |    10    |    No    |    Warning: Failed to attach full-text catalog '%ls'.    |
|    7649    |    10    |    No    |    Warning: Failed to dismount full-text catalog at '%ls'.    |
|    7650    |    10    |    No    |    Warning: Failed to drop full-text catalog at '%ls'.    |
|    7651    |    10    |    No    |    Warning: The ongoing population is necessary to ensure an up-to-date index. If needed, stop change tracking first, and then deactivate the full-text index population.    |
|    7652    |    16    |    No    |    A full-text index for table or indexed view '%.*ls' has already been created.    |
|    7653    |    16    |    No    |    '%ls' is not a valid index to enforce a full-text search key. A full-text search key must be a unique, non-nullable, single-column index which is not offline, is not defined on a non-deterministic or imprecise nonpersisted computed column, does not have a filter, and has maximum size of %d bytes. Choose another index for the full-text key.    |
|    7654    |    16    |    No    |    Unable to obtain the population status of the table or indexed view '%.*ls'.    |
|    7655    |    16    |    No    |    TYPE COLUMN option must be specified with column of image or varbinary(max) type.    |
|    7656    |    16    |    No    |    Full-text index for table or indexed view '%.*ls' cannot be populated because the database is in single-user access mode.    |
|    7657    |    10    |    No    |    Warning: Table or indexed view '%.*ls' has full-text indexed columns that are of type image, text, or ntext. Full-text change tracking cannot track WRITETEXT or UPDATETEXT operations performed on these columns.    |
|    7658    |    16    |    No    |    Table or indexed view '%.*ls' does not have a full-text index or user does not have permission to perform this action.    |
|    7659    |    16    |    No    |    Cannot activate full-text search for table or indexed view '%.*ls' because no columns have been enabled for full-text search.    |
|    7660    |    16    |    No    |    Full-text search must be activated on table or indexed view '%.*ls' before this operation can be performed.    |
|    7661    |    10    |    No    |    Warning: Full-text change tracking is currently enabled for table or indexed view '%.*ls'.    |
|    7662    |    10    |    No    |    Warning: Full-text auto propagation is currently enabled for table or indexed view '%.*ls'.    |
|    7663    |    16    |    No    |    Option 'WITH NO POPULATION' should not be used when change tracking is enabled.    |
|    7664    |    16    |    No    |    Full-text change tracking must be started on table or indexed view '%.*ls' before the changes can be flushed.    |
|    7665    |    16    |    No    |    Full Crawl must be executed on table or indexed view '%.*ls'. Columns affecting the index have been added or dropped since the last index full population.    |
|    7666    |    16    |    No    |    User does not have permission to perform this action.    |
|    7668    |    16    |    No    |    Cannot drop full-text catalog '%ls' because it contains a full-text index.    |
|    7669    |    10    |    No    |    Warning: Full-text index for table or indexed view '%.*ls' cannot be populated because the database is in single-user access mode. Change tracking is stopped for this table or indexed view.    |
|    7670    |    16    |    No    |    Column '%.*ls' cannot be used for full-text search because it is not a character-based, XML, image or varbinary(max) type column.    |
|    7671    |    16    |    No    |    Column '%.*ls' cannot be used as full-text type column for image column. It must be a character-based column with a size less or equal than %d characters.    |
|    7672    |    16    |    No    |    A full-text index cannot be created on the table or indexed view because duplicate column '%.*ls' is specified.    |
|    7673    |    10    |    No    |    Warning: Full-text change tracking is currently disabled for table or indexed view '%.*ls'.    |
|    7674    |    10    |    No    |    Warning: The fulltext catalog '%.*ls' is being dropped and is currently set as default.    |
|    7676    |    10    |    No    |    Warning: Full-text auto propagation is on. Stop crawl request is ignored.    |
|    7677    |    16    |    No    |    Column "%.*ls" is not full-text indexed.    |
|    7678    |    16    |    No    |    The following string is not defined as a language alias in syslanguages: %.*ls.    |
|    7679    |    16    |    No    |    Full-text index language of column "%.*ls" is not a language supported by full-text search.    |
|    7680    |    16    |    No    |    Default full-text index language is not a language supported by full-text search.    |
|    7681    |    10    |    No    |    Warning: Directory '%ls' does not have a valid full-text catalog. Full-text catalog header file or attach state file either is missing or corrupted. The full-text catalog cannot be attached.    |
|    7682    |    10    |    No    |    The component '%ls' reported error while indexing. Component path '%ls'.    |
|    7683    |    16    |    No    |    Errors were encountered during full-text index population for table or indexed view '%ls', database '%ls' (table or indexed view ID '%d', database ID '%d'). Please see full-text crawl logs for details.    |
|    7684    |    10    |    No    |    Error '%ls' occurred during full-text index population for table or indexed view '%ls' (table or indexed view ID '%d', database ID '%d'), full-text key value '%ls'. Failed to index the row.    |
|    7685    |    10    |    No    |    Error '%ls' occurred during full-text index population for table or indexed view '%ls' (table or indexed view ID '%d', database ID '%d'), full-text key value '%ls'. Attempt will be made to reindex it.    |
|    7689    |    16    |    No    |    Execution of a full-text operation failed. '%ls'    |
|    7690    |    16    |    No    |    Full-text operation failed because database is read only.    |
|    7691    |    16    |    No    |    Access is denied to full-text log path. Full-text logging is disabled for database '%ls', catalog '%ls' (database ID '%d', catalog ID '%d').    |
|    7692    |    16    |    No    |    Full-text catalog path '%.*ls' exceeded %d character limit.    |
|    7693    |    16    |    No    |    Full-text initialization failed to create a memory clerk.    |
|    7694    |    16    |    No    |    Failed to pause catalog for backup. Backup was aborted.    |
|    7696    |    16    |    No    |    Invalid locale ID was specified. Please verify that the locale ID is correct and corresponding language resource has been installed.    |
|    7697    |    10    |    No    |    Warning: Full-text index on table or indexed view '%.*ls' in database '%.*ls' has been changed after full-text catalog files backup. A full population is required to bring full-text index to a consistent state.    |
|    7698    |    16    |    No    |    GROUP BY ALL cannot be used in full text search queries.    |
|    7699    |    16    |    No    |    TYPE COLUMN option is not allowed for column types other than image or varbinary(max).    |
|    7702    |    16    |    No    |    Empty Partition function type-parameter-list is not allowed when defining a partition function.    |
|    7703    |    16    |    No    |    Can not create RANGE partition function with multiple parameter types.    |
|    7704    |    16    |    No    |    The type '%.*ls' is not valid for this operation.    |
|    7705    |    16    |    No    |    Could not implicitly convert range values type specified at ordinal %d to partition function parameter type.    |
|    7706    |    16    |    No    |    Partition function '%ls' is being used by one or more partition schemes.    |
|    7707    |    16    |    No    |    The associated partition function '%ls' generates more partitions than there are file groups mentioned in the scheme '%ls'.    |
|    7708    |    16    |    No    |    Duplicate range boundary values are not allowed in partition function boundary values list. Partition boundary values at ordinal %d and %d are equal.    |
|    7709    |    10    |    No    |    Warning: Range value list for partition function '%.*ls' is not sorted by value. Mapping of partitions to filegroups during CREATE PARTITION SCHEME will use the sorted boundary values if the function '%.*ls' is referenced in CREATE PARTITION SCHEME.    |
|    7710    |    10    |    No    |    Warning: The partition scheme '%.*ls' does not have any next used filegroup. Partition scheme has not been changed.    |
|    [7711](../../relational-databases/errors-events/mssqlserver-7711-database-engine-error.md)    |    16    |    No    |    The DATA_COMPRESSION option was specified more than once for the table, or for at least one of its partitions if the table is partitioned.    |
|    7712    |    10    |    No    |    Partition scheme '%.*ls' has been created successfully. '%.*ls' is marked as the next used filegroup in partition scheme '%.*ls'.    |
|    7713    |    10    |    No    |    %d filegroups specified after the next used filegroup are ignored.    |
|    7714    |    16    |    No    |    Partition range value is missing.    |
|    7715    |    16    |    No    |    The specified partition range value could not be found.    |
|    7716    |    16    |    No    |    Can not create or alter a partition function to have zero partitions.    |
|    7717    |    16    |    No    |    The partition scheme "%.*ls" is currently being used to partition one or more tables.    |
|    7718    |    16    |    No    |    Partition range value cannot be specified for hash partitioning.    |
|    7719    |    16    |    No    |    CREATE/ALTER partition function failed as only a maximum of %d partitions can be created.    |
|    7720    |    16    |    No    |    Data truncated when converting range values to the partition function parameter type. The range value at ordinal %d requires data truncation.    |
|    7721    |    16    |    No    |    Duplicate range boundary values are not allowed in partition function boundary values list. The boundary value being added is already present at ordinal %d of the boundary value list.    |
|    7722    |    16    |    No    |    Invalid partition number %I64d specified for %S_MSG '%.*ls', partition number can range from 1 to %d.    |
|    7723    |    16    |    No    |    Only a single filegroup can be specified while creating partition scheme using option ALL to specify all the filegroups.    |
|    7724    |    16    |    No    |    Computed column cannot be used as a partition key if it is not persisted. Partition key column '%.*ls' in table '%.*ls' is not persisted.    |
|    7725    |    16    |    No    |    Alter partition function statement failed. Cannot repartition table '%.*ls' by altering partition function '%.*ls' because its clustered index '%.*ls' is disabled.    |
|    7726    |    16    |    No    |    Partition column '%.*ls' has data type %s which is different from the partition function '%.*ls' parameter data type %s.    |
|    7727    |    16    |    No    |    Collation of partition column '%.*ls' does not match collation of corresponding parameter in partition function '%.*ls'.    |
|    7728    |    16    |    No    |    Invalid partition range: %d TO %d. Lower bound must not be greater than upper bound.    |
|    7729    |    16    |    No    |    Cannot specify partition number in the %S_MSG %S_MSG statement as the %S_MSG '%.*ls' is not partitioned.    |
|    7730    |    16    |    No    |    Alter %S_MSG statement failed because partition number %d does not exist in %S_MSG '%.*ls'.    |
|    7731    |    16    |    No    |    Cannot specify partition number in Alter %S_MSG statement to rebuild or reorganize a partition of %S_MSG '%.*ls'.    |
|    7732    |    16    |    No    |    Cannot specify partition number in Alter index statement along with keyword ALL to rebuild partitions of table '%.*ls' when the table does not have any regular indexes.    |
|    7733    |    16    |    No    |    '%ls' statement failed. The %S_MSG '%.*ls' is partitioned while %S_MSG '%.*ls' is not partitioned.    |
|    7734    |    10    |    No    |    The %S_MSG '%.*ls' specified for the clustered index '%.*ls' was used for table '%.*ls' even though %S_MSG '%.*ls' is specified for it.    |
|    7735    |    16    |    No    |    Cannot specify partition number in alter %S_MSG statement to rebuild or reorganize a partition of %S_MSG '%.*ls' as %S_MSG is not partitioned.    |
|    7736    |    16    |    No    |    Partition function can only be created in Enterprise edition of SQL Server. Only Enterprise edition of SQL Server supports partitioning.    |
|    7737    |    16    |    No    |    Filegroup %.*ls is of a different filegroup type than the first filegroup in partition scheme %.*ls    |
|    7738    |    16    |    No    |    Cannot enable compression for object '%.*ls'. Only SQL Server Enterprise Edition supports compression.    |
|    7801    |    15    |    No    |    The required parameter %.*ls was not specified.    |
|    7802    |    16    |    No    |    Functions that have a return type of "%.*ls" are unsupported through SOAP invocation.    |
|    7803    |    15    |    No    |    The clause %.*ls can not be used in the %.*ls statement.    |
|    7804    |    15    |    No    |    %.*ls and %.*ls can not share the same value.    |
|    7805    |    16    |    No    |    The parameter SITE can not be prefixed by a scheme such as 'https://'. Valid values for SITE include {'*' \| '+' \| 'site_name'}.    |
|    7806    |    16    |    No    |    The URL specified by endpoint '%.*ls' is already registered to receive requests or is reserved for use by another service.    |
|    7807    |    16    |    No    |    An error ('0x%x') occurred while attempting to register the endpoint '%.*ls'.    |
|    7808    |    10    |    No    |    The endpoint '%.*ls' could not be unregistered.    |
|    7809    |    10    |    No    |    Cannot find the object '%.*ls', because it does not exist or you do not have permission.    |
|    7810    |    15    |    No    |    The value '%d' is not within range for the '%.*ls' parameter.    |
|    7811    |    16    |    No    |    COMPUTE BY queries are not supported over SOAP.    |
|    7812    |    10    |    Yes    |    The endpoint '%.*ls' has been established in metadata, but HTTP listening has not been enabled because HTTP support did not start successfully. Verify that the operating system and the edition of SQL Server supports native HTTP access. Check the SQL Server error log for any errors that might have occurred while starting HTTP support.    |
|    7813    |    16    |    No    |    The parameter PATH must be supplied in its canonical form. An acceptable PATH is '%.*ls'.    |
|    7814    |    10    |    No    |    The specified value '%.*ls' already exists.    |
|    7815    |    10    |    No    |    The specified value '%.*ls' does not exist.    |
|    7816    |    15    |    No    |    A duplicate parameter was specified, '%.*ls'.    |
|    7817    |    16    |    No    |    The Base64 encoded input data was malformed for the parameter "%.*ls".    |
|    7818    |    16    |    No    |    The request exceeds an internal limit. Simplify or reduce the size of the request.    |
|    7819    |    15    |    No    |    The SOAP method object '%.*ls' must be specified using a fully qualified three-part name.    |
|    7820    |    16    |    No    |    SOAP namespaces beginning with '%.*ls' are disallowed because they are reserved for system use.    |
|    7821    |    10    |    No    |    Cannot find the database '%.*ls', because it does not exist or you do not have permission.    |
|    7822    |    16    |    No    |    An unexpected XML node "%.*ls" (in the namespace "%.*ls") was found in the "%.*ls" element (in the "%.*ls" namespace) of the SOAP request.    |
|    7823    |    16    |    No    |    The "%.*ls" XML element (in the "%.*ls" namespace) was expected in the "%.*ls" element (in the "%.*ls" namespace) of the SOAP request.    |
|    7824    |    16    |    No    |    The "%.*ls" XML element (in the "%.*ls" namespace) was expected as the topmost node of the SOAP request.    |
|    7825    |    16    |    No    |    A SOAP method element was expected in the "%.*ls" element (in the "%.*ls" namespace) of the SOAP request.    |
|    7826    |    16    |    No    |    Unexpected character data was found in the "%.*ls" element (in the "%.*ls" namespace) of the SOAP request.    |
|    7827    |    14    |    No    |    The user does not have permission to reserve and unreserve HTTP namespaces.    |
|    7828    |    11    |    No    |    The statement is not supported on this version of the operating system. Could not find Httpapi.dll in the path.    |
|    7829    |    11    |    No    |    The statement is not supported on this version of the operating system. Could not find function entry point '%.*ls' in Httpapi.dll.    |
|    7830    |    16    |    No    |    Unable to complete the operation because of an unexpected error.    |
|    7831    |    16    |    No    |    A reservation for this HTTP namespace (%.*ls) already exists.    |
|    7832    |    16    |    No    |    A reservation for this HTTP namespace (%.*ls) does not exist.    |
|    7833    |    16    |    No    |    The HTTP namespace (%.*ls) is in an invalid format. Specify the namespace in its canonical form.    |
|    7834    |    10    |    No    |    The reservation for the HTTP namespace (%.*ls) has been deleted. If there are any endpoints associated with this namespace, they will continue to receive and process requests until the server is restarted.    |
|    7835    |    16    |    Yes    |    Endpoint '%.*ls' has been disabled because it is insecurely configured. For a more information, attempt to start the endpoint using the ALTER ENDPOINT statement.    |
|    7836    |    20    |    No    |    A fatal error occurred while reading the input stream from the network. The maximum number of network packets in one request was exceeded. Try using bulk insert, increasing network packet size, or reducing the size of the request. The session will be terminated.    |
|    7847    |    16    |    No    |    XML data was found in the parameter '%.*ls' which is not an XML parameter. Please entitize any invalid XML character data in this parameter, or pass the parameter in typed as XSD:anyType or sqltypes:xml.    |
|    7848    |    15    |    No    |    An invalid or unsupported localeId was specified for parameter "%.*ls".    |
|    7849    |    15    |    No    |    Invalid sqlCompareOptions were specified for parameter "%.*ls".    |
|    7850    |    16    |    No    |    The SQL Server Service account does not have permission to register the supplied URL on the endpoint '%.*ls'. Use sp_reserve_http_namespace to explicitly reserve the URL namespace before you try to register the URL again.    |
|    7851    |    15    |    No    |    The %.*ls attribute must be specified on the %.*ls element of the parameter "%.*ls" because it is of type %.*ls.    |
|    7852    |    15    |    No    |    Parameter "%.*ls": If the %.*ls attribute appears on a parameter value node of type "%.*ls" (in the namespace "%.*ls"), it must refer to a CLR type.    |
|    7853    |    16    |    No    |    The URL specified as the path ("%.*ls") is not in an absolute format, and must begin with "%.*ls".    |
|    7854    |    16    |    No    |    The URL value specified for the "%.*ls" parameter is too long.    |
|    7855    |    15    |    No    |    Reading from HTTP input stream failed.    |
|    7856    |    16    |    No    |    XML parameters do not support non-unicode element or attribute values.    |
|    7857    |    16    |    No    |    Parameter "%.*ls": Function or procedure parameters with improperly formatted or deprecated names are not supported through Native SOAP access. Refer to documentation for rules concerning the proper naming of parameters.    |
|    7858    |    16    |    No    |    The "%.*ls" XML element (in the "%.*ls" namespace) in the "%.*ls" element (in the "%.*ls" namespace) of the SOAP request contained an invalid binary type.    |
|    7859    |    15    |    No    |    Parameter "%.*ls": Parameter collation cannot be specified on the "%.*ls" node (in the namespace "%.*ls").    |
|    7860    |    15    |    No    |    An endpoint's transport or content cannot be changed through the ALTER ENDPOINT statement. Use DROP ENDPOINT and execute the CREATE ENDPOINT statement to make these changes.    |
|    7861    |    15    |    No    |    %.*ls endpoints can only be of the "FOR %.*ls" type.    |
|    7862    |    16    |    No    |    An endpoint of the requested type already exists. Only one endpoint of this type is supported. Use ALTER ENDPOINT or DROP the existing endpoint and execute the CREATE ENDPOINT statement.    |
|    7863    |    16    |    No    |    The endpoint was not changed. The ALTER ENDPOINT statement did not contain any values to modify or update.    |
|    7864    |    16    |    No    |    CREATE/ALTER ENDPOINT cannot be used to update the endpoint with this information. The Dedicated Administrator Connection endpoint is reserved and cannot be updated.    |
|    7865    |    16    |    No    |    Web Services Description Language (WSDL) generation failed because the system was unable to query the metadata for the endpoint.    |
|    7866    |    16    |    No    |    XML attribute and element values larger than 4000 characters are only allowed within the SOAP Body node.    |
|    7867    |    15    |    No    |    An invalid sqlCollationVersion was specified for parameter "%.*ls".    |
|    7868    |    15    |    No    |    An invalid sqlSortId was specified for parameter "%.*ls".    |
|    7869    |    16    |    No    |    The endpoint name '%.*ls' is reserved for used by SQL. Endpoint names cannot begin with '%.*ls'.    |
|    7870    |    16    |    No    |    The AFFINITY clause is not supported for endpoints of this type.    |
|    7871    |    16    |    No    |    The clause "%.*ls" is not valid for this endpoint type.    |
|    7872    |    16    |    No    |    %.*ls is not a parameter for procedure "%.*ls", or it was supplied out of order.    |
|    7873    |    16    |    No    |    The endpoint "%.*ls" is a built-in endpoint and cannot be dropped. Use the protocol configuration utilities to ADD or DROP Transact-SQL endpoints.    |
|    7874    |    16    |    No    |    An endpoint already exists with the bindings specified. Only one endpoint supported for a specific binding. Use ALTER ENDPOINT or DROP the existing endpoint and execute the CREATE ENDPOINT statement.    |
|    7875    |    16    |    No    |    An unexpected XML construct was found in the character data of the "%.*ls" element (in the "%.*ls" namespace) of the SOAP request.    |
|    7878    |    16    |    No    |    This "%.*ls ENDPOINT" statement is not supported on this edition of SQL Server.    |
|    7879    |    10    |    No    |    SQL Server is waiting for %d remaining sessions and connections to close. If these sessions are not closed within a reasonable amount of time, "polite" shutdown will be aborted. This message may appear several times before SQL Server is shutdown.    |
|    7880    |    10    |    No    |    SQL Server has successfully finished closing sessions and connections.    |
|    7881    |    10    |    No    |    SQL Server was unable to close sessions and connections in a reasonable amount of time and is aborting "polite" shutdown.    |
|    7882    |    16    |    No    |    OUTPUT was requested for parameter '%.*ls', which is not supported for a WEBMETHOD with FORMAT=NONE.    |
|    7883    |    16    |    No    |    User-defined functions cannot be used for a WEBMETHOD with FORMAT=NONE.    |
|    7884    |    20    |    Yes    |    Violation of tabular data stream (TDS) protocol. This is most often caused by a previous exception on this task. The last exception on the task was error %d, severity %d, address 0x%p. This connection will be terminated.    |
|    7885    |    20    |    Yes    |    Network error 0x%lx occurred while sending data to the client on process ID %d batch ID %d. A common cause for this error is if the client disconnected without reading the entire response from the server. This connection will be terminated.    |
|    7886    |    20    |    Yes    |    A read operation on a large object failed while sending data to the client. A common cause for this is if the application is running in READ UNCOMMITTED isolation level. This connection will be terminated.    |
|    7887    |    20    |    Yes    |    The IPv6 address specified is not supported. Only addresses that are in their numeric, canonical form are supported for listening.    |
|    7888    |    20    |    Yes    |    The IPv6 address specified is not supported. The server may not be configured to allow for IPv6 connectivity, or the address may not be in a recognized IPv6 format.    |
|    7889    |    16    |    No    |    The SOAP headers on the request have exceeded the size limits established for this endpoint. The endpoint owner may increase these limits via ALTER ENDPOINT.    |
|    7890    |    16    |    No    |    An error occurred while attempting to register the endpoint '%.*ls'. One or more of the ports specified in the CREATE ENDPOINT statement may be bound to another process. Attempt the statement again with a different port or use netstat to find the application currently using the port and resolve the conflict.    |
|    7891    |    10    |    No    |    Creation of a TSQL endpoint will result in the revocation of any 'Public' connect permissions on the '%.*ls' endpoint. If 'Public' access is desired on this endpoint, reapply this permission using 'GRANT CONNECT ON ENDPOINT::[%.*ls] to [public]'.    |
|    7892    |    16    |    No    |    Internal subset DTDs inside SOAP requests are not allowed.    |
|    7893    |    15    |    No    |    Parameter '%.*ls': Incompatible XML attributes were present. The '%.*ls' attribute and the '%.*ls' attribute may not both be present on a parameter value node of type '%.*ls' (in the namespace '%.*ls').    |
|    7894    |    16    |    Yes    |    Listening has not been started on the endpoint '%.*ls' found in metadata. Endpoint operations are disabled on this edition of SQL Server.    |
|    7895    |    14    |    No    |    Only a system administrator can specify a custom WSDL stored procedure on the endpoint.    |
|    7896    |    16    |    No    |    The column or parameter '%.*ls' uses a data type not supported by SOAP. SOAP only supports data types supported in SQL Server 2005 or earlier.    |
|    7897    |    10    |    No    |    Creating and altering SOAP endpoints will be removed in a future version of SQL Server. Avoid using this feature in new development work, and plan to modify applications that currently use it.    |
|    7898    |    10    |    Yes    |    SQL Server native SOAP support is now deprecated and will be removed in a future version of SQL Server. Avoid using this feature in new development work, and plan to modify applications that currently use it.    |
|    7899    |    16    |    No    |    The return value uses a data type not supported by SOAP. SOAP only supports data types supported in SQL Server 2005 or earlier.    |
|    [7901](../../relational-databases/errors-events/mssqlserver-7901-database-engine-error.md)    |    16    |    No    |    The repair statement was not processed. This level of repair is not supported when the database is in emergency mode.    |
|    [7903](../../relational-databases/errors-events/mssqlserver-7903-database-engine-error.md)    |    16    |    No    |    Table error: The orphaned file "%.*ls" was found in the FILESTREAM directory ID %.*ls for object ID %d, index ID %d, partition ID %I64d, column ID %d.    |
|    [7904](../../relational-databases/errors-events/mssqlserver-7904-database-engine-error.md)    |    16    |    No    |    Table error: Cannot find the FILESTREAM file "%.*ls" for column ID %d (column directory ID %.*ls) in object ID %d, index ID %d, partition ID %I64d, page ID %S_PGID, slot ID %d.    |
|    [7905](../../relational-databases/errors-events/mssqlserver-7905-database-engine-error.md)    |    16    |    No    |    Database error: The directory "%.*ls" is not a valid FILESTREAM directory.    |
|    [7906](../../relational-databases/errors-events/mssqlserver-7906-database-engine-error.md)    |    16    |    No    |    Database error: The file "%.*ls" is not a valid FILESTREAM file.    |
|    [7907](../../relational-databases/errors-events/mssqlserver-7907-database-engine-error.md)    |    16    |    No    |    Table error: The directory "%.*ls" under the rowset directory ID %.*ls is not a valid FILESTREAM directory.    |
|    [7908](../../relational-databases/errors-events/mssqlserver-7908-database-engine-error.md)    |    16    |    No    |    Table error: The file "%.*ls" in the rowset directory ID %.*ls is not a valid FILESTREAM file.    |
|    7909    |    20    |    No    |    The emergency-mode repair failed.You must restore from backup.    |
|    [7910](../../relational-databases/errors-events/mssqlserver-7910-database-engine-error.md)    |    10    |    No    |    Repair: The page %S_PGID has been allocated to object ID %d, index ID %d, partition ID %I64d, alloc unit ID %I64d (type %.*ls).    |
|    [7911](../../relational-databases/errors-events/mssqlserver-7911-database-engine-error.md)    |    10    |    No    |    Repair: The page %S_PGID has been deallocated from object ID %d, index ID %d, partition ID %I64d, alloc unit ID %I64d (type %.*ls).    |
|    [7912](../../relational-databases/errors-events/mssqlserver-7912-database-engine-error.md)    |    10    |    No    |    Repair: The extent %S_PGID has been allocated to object ID %d, index ID %d, partition ID %I64d, alloc unit ID %I64d (type %.*ls).    |
|    [7913](../../relational-databases/errors-events/mssqlserver-7913-database-engine-error.md)    |    10    |    No    |    Repair: The extent %S_PGID has been deallocated from object ID %d, index ID %d, partition ID %I64d, alloc unit ID %I64d (type %.*ls).    |
|    [7914](../../relational-databases/errors-events/mssqlserver-7914-database-engine-error.md)    |    10    |    No    |    Repair: %ls page at %S_PGID has been rebuilt.    |
|    [7915](../../relational-databases/errors-events/mssqlserver-7915-database-engine-error.md)    |    10    |    No    |    Repair: IAM chain for object ID %d, index ID %d, partition ID %I64d, alloc unit ID %I64d (type %.*ls), has been truncated before page %S_PGID and will be rebuilt.    |
|    [7916](../../relational-databases/errors-events/mssqlserver-7916-database-engine-error.md)    |    10    |    No    |    Repair: Deleted record for object ID %d, index ID %d, partition ID %I64d, alloc unit ID %I64d (type %.*ls), on page %S_PGID, slot %d. Indexes will be rebuilt.    |
|    7917    |    10    |    No    |    Repair: Converted forwarded record for object ID %d, index ID %d, partition ID %I64d, alloc unit ID %I64d (type %.*ls), at page %S_PGID, slot %d to a data row.    |
|    7918    |    10    |    No    |    Repair: Page %S_PGID next and %S_PGID previous pointers have been set to match each other in object ID %d, index ID %d, partition ID %I64d, alloc unit ID %I64d (type %.*ls).    |
|    7919    |    16    |    No    |    Repair statement not processed. Database needs to be in single user mode.    |
|    [7920](../../relational-databases/errors-events/mssqlserver-7920-database-engine-error.md)    |    10    |    No    |    Processed %ld entries in system catalog for database ID %d.    |
|    7921    |    16    |    No    |    Repair statement not processed. Database cannot be a snapshot.    |
|    7922    |    16    |    No    |    ***************************************************************    |
|    [7923](../../relational-databases/errors-events/mssqlserver-7923-database-engine-error.md)    |    10    |    No    |    Table %.*ls Object ID %ld.    |
|    7924    |    10    |    No    |    Index ID %d, partition ID %I64d, alloc unit ID %I64d (type %.*ls). FirstIAM %S_PGID. Root %S_PGID. Dpages %I64d.    |
|    7925    |    10    |    No    |    Index ID %d, partition ID %I64d, alloc unit ID %I64d (type %.*ls). %I64d pages used in %I64d dedicated extents.    |
|    7926    |    16    |    No    |    Check statement aborted. The database could not be checked as a database snapshot could not be created and the database or table could not be locked. See Books Online for details of when this behavior is expected and what workarounds exist. Also see previous errors for more details.    |
|    7927    |    10    |    No    |    Total number of extents is %I64d.    |
|    7928    |    16    |    No    |    The database snapshot for online checks could not be created. Either the reason is given in a previous error or one of the underlying volumes does not support sparse files or alternate streams. Attempting to get exclusive access to run checks offline.    |
|    7929    |    16    |    No    |    Check statement aborted. Database contains deferred transactions.    |
|    7930    |    16    |    No    |    Mirroring must be removed from the database for this DBCC command.    |
|    [7931](../../relational-databases/errors-events/mssqlserver-7931-database-engine-error.md)    |    16    |    No    |    Database error: The FILESTREAM directory ID %.*ls for a partition was seen two times.    |
|    [7932](../../relational-databases/errors-events/mssqlserver-7932-database-engine-error.md)    |    16    |    No    |    Table error: The FILESTREAM directory ID %.*ls for object ID %d, index ID %d, partition ID %I64d is in filegroup %d, but should be in filegroup %d.    |
|    [7933](../../relational-databases/errors-events/mssqlserver-7933-database-engine-error.md)    |    16    |    No    |    Table error: A FILESTREAM directory ID %.*ls exists for a partition, but the corresponding partition does not exist in the database.    |
|    [7934](../../relational-databases/errors-events/mssqlserver-7934-database-engine-error.md)    |    16    |    No    |    Table error: The FILESTREAM directory ID %.*ls for object ID %d, index ID %d, partition ID %I64d was not found.    |
|    [7935](../../relational-databases/errors-events/mssqlserver-7935-database-engine-error.md)    |    16    |    No    |    Table error: A FILESTREAM directory ID %.*ls exists for a column of object ID %d, index ID %d, partition ID %I64d, but that column does not exist in the partition.    |
|    [7936](../../relational-databases/errors-events/mssqlserver-7936-database-engine-error.md)    |    16    |    No    |    Table error: The FILESTREAM directory ID %.*ls exists for column ID %d of object ID %d, index ID %d, partition ID %I64d, but that column is not a FILESTREAM column.    |
|    [7937](../../relational-databases/errors-events/mssqlserver-7937-database-engine-error.md)    |    16    |    No    |    Table error: The FILESTREAM directory ID %.*ls for column ID %d of object ID %d, index ID %d, partition ID %I64d was not found.    |
|    7938    |    16    |    No    |    Table error: object ID %d, index ID %d, partition ID %I64d processing encountered file name "%.*ls" twice in the column directory %d (for column ID %d).    |
|    7939    |    16    |    No    |    Cannot detach database '%.*ls' because it does not exist.    |
|    7940    |    16    |    No    |    System databases master, model, msdb, and tempdb cannot be detached.    |
|    7941    |    16    |    No    |    Table error: object ID %d, index ID %d, partition ID %I64d processing encountered file name "%.*ls" twice in the column ID %d (for column directory %d).    |
|    7942    |    10    |    No    |    DBCC %ls scanning '%.*ls' table...    |
|    7943    |    10    |    No    |    Table: '%.*ls' (%d); index ID: %d, database ID: %d    |
|    7944    |    10    |    No    |    %ls level scan performed.    |
|    7945    |    10    |    No    |    - Pages Scanned................................: %I64d    |
|    7946    |    10    |    No    |    - Extents Scanned..............................: %I64d    |
|    7947    |    10    |    No    |    - Extent Switches..............................: %I64d    |
|    7948    |    10    |    No    |    - Avg. Pages per Extent........................: %3.1f    |
|    7949    |    10    |    No    |    - Scan Density [Best Count:Actual Count].......: %4.2f%ls [%I64d:%I64d]    |
|    7950    |    10    |    No    |    - Logical Scan Fragmentation ..................: %4.2f%ls    |
|    7951    |    10    |    No    |    Warning: Could not complete filestream consistency checks due to an operating system error. Any consistency errors found in the filestream subsystem will be silenced. Please refer to other errors for more information. This condition is likely transient; try rerunning the command.    |
|    7952    |    10    |    No    |    - Extent Scan Fragmentation ...................: %4.2f%ls    |
|    7953    |    10    |    No    |    - Avg. Bytes Free per Page.....................: %3.1f    |
|    7954    |    10    |    No    |    - Avg. Page Density (full).....................: %4.2f%ls    |
|    7955    |    16    |    No    |    Invalid SPID %d specified.    |
|    7957    |    10    |    No    |    Cannot display the specified SPID's buffer; in transition.    |
|    7958    |    16    |    No    |    The specified SPID does not process input/output data streams.    |
|    7960    |    16    |    No    |    An invalid server process identifier (SPID) %d or batch ID %d was specified.    |
|    7961    |    16    |    No    |    Object ID %d, index ID %d, partition ID %I64d, alloc unit ID %I64d (type %.*ls) , page ID %S_PGID, row ID %d. Column '%.*ls' is a var column with a NULL value and non-zero data length    |
|    7962    |    16    |    No    |    Invalid BATCHID %d specified.    |
|    7963    |    16    |    No    |    Database error: The file "%.*ls" is not a valid FILESTREAM LOG file.    |
|    7964    |    10    |    No    |    Repair: Deleted FILESTREAM file "%.*ls" for column ID %d, for object ID %d, index ID %d, partition ID %I64d, alloc unit ID %I64d (type %.*ls) on page %S_PGID, slot %d.    |
|    7965    |    16    |    No    |    Table error: Could not check object ID %d, index ID %d, partition ID %I64d, alloc unit ID %I64d (type %.*ls) due to invalid allocation (IAM) page(s).    |
|    7966    |    10    |    No    |    Warning: NO_INDEX option of %ls being used. Checks on non-system indexes will be skipped.    |
|    7968    |    10    |    No    |    Transaction information for database '%.*ls'.    |
|    7969    |    16    |    No    |    No active open transactions.    |
|    7970    |    10    |    No    |    %hsOldest active transaction:    |
|    7971    |    10    |    No    |    SPID (server process ID): %d%s    |
|    7972    |    10    |    No    |    UID (user ID) : %d    |
|    7974    |    10    |    No    |    Name : %.*ls    |
|    7975    |    10    |    No    |    LSN : (%d:%d:%d)    |
|    7977    |    10    |    No    |    Start time : %.*ls    |
|    7978    |    10    |    No    |    SID : %.*ls    |
|    7979    |    10    |    No    |    %hsReplicated Transaction Information:    |
|    7980    |    10    |    No    |    Oldest distributed LSN : (%d:%d:%d)    |
|    7982    |    10    |    No    |    Oldest non-distributed LSN : (%d:%d:%d)    |
|    7983    |    14    |    No    |    User '%.*ls' does not have permission to run DBCC %ls for database '%.*ls'.    |
|    [7984](../../relational-databases/errors-events/mssqlserver-7984-database-engine-error.md)    |    16    |    No    |    System table pre-checks: Object ID %d. Page %S_PGID has unexpected page type %d. Check statement terminated due to unrepairable error.    |
|    7985    |    16    |    No    |    System table pre-checks: Object ID %d. Could not read and latch page %S_PGID with latch type %ls. Check statement terminated due to unrepairable error.    |
|    [7986](../../relational-databases/errors-events/mssqlserver-7986-database-engine-error.md)    |    16    |    No    |    System table pre-checks: Object ID %d has cross-object chain linkage. Page %S_PGID points to %S_PGID in alloc unit ID %I64d (should be %I64d). Check statement terminated due to unrepairable error.    |
|    [7987](../../relational-databases/errors-events/mssqlserver-7987-database-engine-error.md)    |    16    |    No    |    System table pre-checks: Object ID %d has chain linkage mismatch. %S_PGID->next = %S_PGID, but %S_PGID->prev = %S_PGID. Check statement terminated due to unrepairable error.    |
|    [7988](../../relational-databases/errors-events/mssqlserver-7988-database-engine-error.md)    |    16    |    No    |    System table pre-checks: Object ID %d. Loop in data chain detected at %S_PGID. Check statement terminated due to unrepairable error.    |
|    7992    |    16    |    No    |    Cannot shrink 'read only' database '%.*ls'.    |
|    7993    |    10    |    No    |    Cannot shrink file '%d' in database '%.*ls' to %u pages as it only contains %u pages.    |
|    [7995](../../relational-databases/errors-events/mssqlserver-7995-database-engine-error.md)    |    16    |    No    |    Database '%.*ls': consistency errors in system catalogs prevent further DBCC %ls processing.    |
|    7996    |    16    |    No    |    Extended stored procedures can only be created in the master database.    |
|    7997    |    16    |    No    |    '%.*ls' does not contain an identity column.    |
|    7998    |    16    |    No    |    Checking identity information: current identity value '%.*hs', current column value '%.*hs'.    |
|    7999    |    16    |    No    |    Could not find any index named '%.*ls' for table '%.*ls'.    |
