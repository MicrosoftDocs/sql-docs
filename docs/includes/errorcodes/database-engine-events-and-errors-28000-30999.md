---
author: MashaMSFT
ms.author: mathoma
ms.reviewer: randolphwest
ms.date: 08/19/2022
ms.topic: include
---
| Error| Severity | Event Logged | Description|
| :------ | :------| :------| :----------------------------- |
|    28000    |    16    |    No    |    The decrypted session key has an unexpected size.    |
|    28001    |    16    |    No    |    A corrupted message has been received. It contains invalid flags. This occurred in the message with Conversation ID '%.*ls', Initiator: %d, and Message sequence number: %I64d.    |
|    28002    |    16    |    No    |    Cannot start service broker manager. Operating system error: %ls.    |
|    28003    |    16    |    No    |    An internal service broker error occurred. Operating system error: %ls.    |
|    28004    |    16    |    No    |    This message could not be delivered because the '%S_MSG' action cannot be performed in the '%.*ls' state.    |
|    28005    |    16    |    No    |    An exception occurred while enqueueing a message in the target queue. Error: %d, State: %d. %.*ls    |
|    28006    |    14    |    No    |    User does not have permission to %S_MSG the conversation '%.*ls' in state '%.*ls'. Only members of the sysadmin fixed server role and the db_owner fixed database role have this permission.    |
|    28007    |    16    |    No    |    A corrupted message has been received. The highest seen message number must be greater than the acknowledged message number. This occurred in the message with Conversation ID '%.*ls', Initiator: %d, and Message sequence number: %I64d.    |
|    28008    |    16    |    No    |    The conversation handle '{%.8x-%.4x-%.4x-%.2x%.2x-%.2x%.2x%.2x%.2x%.2x%.2x}' is invalid.    |
|    28009    |    16    |    No    |    The crypto API has detected bad data while trying to perform a decryption operation.    |
|    28010    |    16    |    No    |    This message could not be delivered because it contains an invalid acknowledged message number. Highest expected message number: %I64d. Acknowledged message number: %I64d, fragment number: %d.    |
|    28011    |    16    |    No    |    This message could not be delivered because its %S_MSG has expired or is invalid.    |
|    28012    |    16    |    No    |    The Service Broker in the target database is unavailable: '%S_MSG'.    |
|    28013    |    16    |    No    |    The service broker is administratively disabled.    |
|    28014    |    16    |    No    |    The database is in read-only mode.    |
|    28015    |    16    |    No    |    The database is in single-user mode.    |
|    28016    |    16    |    No    |    The message has been dropped because the service broker in the target database is unavailable: '%S_MSG'.    |
|    28017    |    16    |    No    |    The message has been dropped because the target service broker is unreachable.    |
|    28018    |    16    |    No    |    The database is a replica of a mirrored database.    |
|    28019    |    16    |    No    |    System error %d occurred while creating a new message element GUID for this forwarded message.    |
|    28020    |    16    |    No    |    Could not create user token for user %d in database %d.    |
|    28021    |    16    |    No    |    One or more messages could not be delivered to the local service targeted by this dialog.    |
|    28022    |    10    |    No    |    An error occurred while looking up the public key certificate associated with this SQL Server instance: The certificate is not yet valid.    |
|    28023    |    10    |    No    |    An error occurred while looking up the public key certificate associated with this SQL Server instance: The certificate has expired.    |
|    28024    |    16    |    Yes    |    The security certificate bound to database principal (Id: %i) is not yet valid. Either wait for the certificate to become valid or install a certificate that is currently valid.    |
|    28025    |    16    |    Yes    |    The security certificate bound to database principal (Id: %i) has expired. Create or install a new certificate for the database principal.    |
|    28026    |    10    |    No    |    Connection handshake failed. Not enough memory available. State %d.    |
|    28027    |    10    |    No    |    Connection handshake failed. There is no compatible %S_MSG. State %d.    |
|    28028    |    10    |    No    |    Connection handshake failed. Could not send a handshake message because the connection was closed by peer. State %d.    |
|    28029    |    10    |    No    |    Connection handshake failed. Unexpected event (%d) for current context (%d). State %d.    |
|    28030    |    10    |    No    |    Connection handshake failed. A call to the SQL Server Network Interface failed: (%x) %ls. State %d.    |
|    28031    |    10    |    No    |    Connection handshake failed. An OS call failed: (%x) %ls. State %d.    |
|    28032    |    10    |    No    |    A previously existing connection with the same peer was detected during connection handshake. This connection lost the arbitration and it will be closed. All traffic will be redirected to the previously existing connection. This is an informational message only. No user action is required. State %d.    |
|    28033    |    10    |    No    |    A new connection was established with the same peer. This connection lost the arbitration and it will be closed. All traffic will be redirected to the newly opened connection. This is an informational message only. No user action is required. State %d.    |
|    28034    |    10    |    No    |    Connection handshake failed. The login '%.*ls' does not have CONNECT permission on the endpoint. State %d.    |
|    28035    |    10    |    No    |    Connection handshake failed. The certificate used by the peer is invalid due to the following reason: %S_MSG. State %d.    |
|    28036    |    10    |    No    |    Connection handshake failed. The certificate used by this endpoint was not found: %S_MSG. Use DBCC CHECKDB in master database to verify the metadata integrity of the endpoints. State %d.    |
|    28037    |    10    |    No    |    Connection handshake failed. Error %d occurred while initializing the private key corresponding to the certificate. The SQL Server errorlog and the Windows event log may contain entries related to this error. State %d.    |
|    28038    |    10    |    No    |    Connection handshake failed. The handshake verification failed. State %d.    |
|    28039    |    10    |    No    |    Connection handshake failed. The receive SSPI packet is not of type of the negotiated package. State %d.    |
|    28040    |    10    |    No    |    A corrupted message has been received. The adjacent error message header is invalid.    |
|    28041    |    16    |    No    |    A corrupted message has been received. The encrypted payload offset is invalid (%d).    |
|    28042    |    16    |    No    |    A corrupted message has been received. The arbitration request header is invalid.    |
|    28043    |    16    |    No    |    A corrupted message has been received. The arbitration response header is invalid.    |
|    28044    |    16    |    No    |    A corrupted message has been received. It is not encrypted and signed using the currently configured endpoint algorithm. This occurred in the message with Conversation ID '%.*ls', Initiator: %d, and Message sequence number: %I64d.    |
|    28045    |    10    |    No    |    Connection handshake failed. The certificate used by the peer does not match the one in MASTER database with same issuer name and serial number. State %d.    |
|    28046    |    10    |    Yes    |    %S_MSG Login succeeded for user '%.*ls'. Authentication mode: %.*ls. %.*ls    |
|    28047    |    10    |    Yes    |    %S_MSG login attempt failed with error: '%.*ls'. %.*ls    |
|    28048    |    10    |    Yes    |    %S_MSG login attempt by user '%.*ls' failed with error: '%.*ls'. %.*ls    |
|    28050    |    10    |    No    |    The session keys for this conversation could not be created or accessed. The database master key is required for this operation.    |
|    28051    |    10    |    No    |    Could not save a dialog session key. A master key is required in the database to save the session key.    |
|    28052    |    16    |    No    |    Cannot decrypt session key while regenerating master key with FORCE option.    |
|    28053    |    16    |    No    |    Service Broker could not upgrade conversation session keys in database '%.*ls' to encrypted format (Error: %d). The Service Broker in this database was disabled. A master key is required to the database in order to enable the broker.    |
|    28054    |    16    |    No    |    Service Broker needs to access the master key in the database '%.*ls'. Error code:%d. The master key has to exist and the service master key encryption is required.    |
|    28055    |    16    |    No    |    The certificate '%.*ls' is not valid for endpoint authentication. The certificate must have a private key encrypted with the database master key and current UTC date has to be between the certificate start date and the certificate expiration date.    |
|    28056    |    16    |    No    |    This message could not be delivered because the user with ID %i in database ID %i does not have control permission on the service. Service name: '%.*ls'.    |
|    28057    |    10    |    No    |    Service Broker in database '%.*ls' has a pending conversation upgrade operation. A database master key in the database is required for this operation to complete.    |
|    28058    |    16    |    No    |    Service Broker could not upgrade this conversation during a database upgrade operation.    |
|    28059    |    16    |    No    |    Connection handshake failed. The received premaster secret of size %d does not have the expected size of %d. State %d.    |
|    28060    |    16    |    No    |    The AES encryption algorithm is only supported on Windows XP, Windows Server 2003 or later versions.    |
|    28061    |    16    |    No    |    A corrupted message has been received. The adjacent message integrity check signature could not be validated.    |
|    28062    |    16    |    No    |    A corrupted message has been received. The signed dialog message header is invalid.    |
|    28063    |    16    |    No    |    A corrupted message has been received. A required variable data field is not present: %S_MSG. This occurred in the message with Conversation ID '%.*ls', Initiator: %d, and Message sequence number: %I64d.    |
|    28064    |    16    |    No    |    A corrupted message has been received. A string variable data field is not a valid UNICODE string: %S_MSG. This occurred in the message with Conversation ID '%.*ls', Initiator: %d, and Message sequence number: %I64d.    |
|    28065    |    16    |    No    |    A corrupted message has been received. The unsigned dialog message header is invalid. This occurred in the message with Conversation ID '%.*ls', Initiator: %d, and Message sequence number: %I64d.    |
|    28066    |    16    |    No    |    A corrupted message has been received. The security dialog message header is invalid. This occurred in the message with Conversation ID '%.*ls', Initiator: %d, and Message sequence number: %I64d.    |
|    28067    |    16    |    No    |    A corrupted message has been received. The encrypted offset of the envelope does not match the payload encrypted offset. This occurred in the message with Conversation ID '%.*ls', Initiator: %d, and Message sequence number: %I64d.    |
|    28068    |    16    |    No    |    A corrupted message has been received. The envelope payload is bigger than the message. This occurred in the message with Conversation ID '%.*ls', Initiator: %d, and Message sequence number: %I64d.    |
|    28069    |    16    |    Yes    |    Unexpected session key when encrypting a dialog message.    |
|    28070    |    10    |    No    |    Connection handshake failed. The received SSPI message Confirm status is unexpected. State %d.    |
|    28072    |    16    |    No    |    A serious error occurred in the Service Broker message transmitter (operation %i): Error: %i, State: %i. Message transmission will resume in %i seconds.    |
|    28073    |    16    |    No    |    An out of memory condition has occurred in the Service Broker message transmitter (operation %i). Message transmission will resume in %i seconds.    |
|    28074    |    16    |    No    |    Service Broker could not upgrade conversation with conversation_handle '%ls'. Use END CONVERSATION ... WITH CLEANUP to delete this conversation, then try again to enable the broker. Use ALTER DATABASE ... SET ERROR_BROKER to error all conversations in this database. Use ALTER DATABASE ... SET NEW_BROKER to delete all conversations in this database.    |
|    28075    |    10    |    No    |    The broker in the sender's database is in single user mode. Messages cannot be delivered while in single user mode.    |
|    28076    |    10    |    No    |    Could not query the FIPS compliance mode flag from registry. Error %ls.    |
|    28077    |    10    |    No    |    %S_MSG transport is running in FIPS compliance mode. This is an informational message only. No user action is required.    |
|    28078    |    10    |    No    |    The RC4 encryption algorithm is not supported when running in FIPS compliance mode.    |
|    28079    |    10    |    No    |    Connection handshake failed. The received SSPI packet is not of the expected direction. State %d.    |
|    28080    |    10    |    No    |    Connection handshake failed. The %S_MSG endpoint is not configured. State %d.    |
|    28081    |    10    |    No    |    Connection handshake failed. An unexpected status %d was returned when trying to send a handshake message. State %d.    |
|    28082    |    10    |    No    |    Connection handshake failed. An unexpected internal failure occurred when trying to marshal a message. State %d.    |
|    28083    |    16    |    No    |    The database principal '%.*ls' cannot be used in a remote service binding because it cannot own certificates. Remote service bindings cannot be associated with 1) roles, 2) groups or 3) principals mapped to certificates or asymmetric keys.    |
|    28084    |    10    |    No    |    An error occurred in Service Broker internal activation while trying to scan the user queue '%ls' for its status. Error: %i, State: %i. %.*ls This is an informational message only. No user action is required.    |
|    28085    |    16    |    No    |    The activated task was ended because the associated queue was dropped.    |
|    28086    |    16    |    No    |    The activated task was ended because either the queue or activation was disabled.    |
|    28087    |    16    |    No    |    The activated task was aborted because the invoked stored procedure '%ls' did not execute RECEIVE.    |
|    28088    |    16    |    No    |    The activated task was aborted due to an error (Error: %i, State %i). Check ERRORLOG or the previous "Broker:Activation" trace event for possible output from the activation stored procedure.    |
|    28089    |    16    |    No    |    The database principal '%.*ls' cannot be used in a remote service binding because it cannot own certificates. This is a special user for backward compatibility with implicitly connected user schemas.    |
|    28090    |    16    |    No    |    An error occurred while deleting sent messages from the transmission queue, Error: %i, State: %i. Verify that no other operation is locking the transmission queue, and that the database is available.    |
|    28098    |    10    |    No    |    A previously existing connection with the same peer was found after DNS lookup. This connection will be closed. All traffic will be redirected to the previously existing connection. This is an informational message only. No user action is required. State %d.    |
|    28099    |    10    |    No    |    During the database upgrade process on database '%.*ls', a user object '%S_MSG' named '%.*ls' was found to already exist. That object is now reserved by the system in this version of SQL Server. Because it already exists in the database, the upgrade process is unable to install the object. Remove or rename the user object from the original (pre-upgraded) database on an older version of SQL Server and then retry the database upgrade process by using CREATE DATABASE FOR ATTACH. Functionality that relies on the reserved object may not function correctly if you continue to use the database in the current state.    |
|    28101    |    16    |    No    |    User '%.*ls\%.*ls' does not have permission to debug the requested client connection.    |
|    28102    |    16    |    No    |    Batch execution is terminated because of debugger request.    |
|    28201    |    10    |    No    |    SQLSQM.EXE cannot be launched. This can either be caused if the required information in the registry is missing or corrupted or SQLSQM.EXE cannot be found.    |
|    28102    |    16    |    No    |    Batch execution is terminated because of debugger request.    |
|    28201    |    10    |    No    |    SQLSQM.EXE cannot be launched. This can either be caused if the required information in the registry is missing or corrupted or SQLSQM.EXE cannot be found.    |
|    29001    |    16    |    No    |    To connect to this server you must use SQL Server Management Studio or SQL Server Management Objects (SMO).    |
|    29003    |    16    |    No    |    Invalid parameter combinations.    |
|    29004    |    16    |    No    |    Unknown property specified: %s.    |
|    30003    |    16    |    No    |    A fulltext system view or stvf cannot open database id %d.    |
|    30004    |    16    |    No    |    A fulltext system view or stvf cannot open user table object id %d.    |
|    30005    |    16    |    No    |    The name specified for full-text index fragment %.*ls is not valid.    |
|    30006    |    16    |    No    |    A fulltext system view or stvf cannot open fulltext index for user table object id %d.    |
|    30007    |    16    |    No    |    Parameters of dm_fts_index_keywords and dm_fts_index_keywords_by_document cannot be null.    |
|    30008    |    16    |    No    |    The level number specified for function fn_fulltext_compindex is not valid. Valid level numbers start from 0 and must be less than the number of levels of the compressed index. Use the correct level number.    |
|    30009    |    16    |    No    |    The argument data type '%ls' specified for the full-text query is not valid. Allowed data types are char, varchar, nchar, nvarchar.    |
|    30020    |    16    |    No    |    The full-text query parameter for %S_MSG is not valid.    |
|    30022    |    10    |    No    |    Warning: The configuration of a full-text stoplist was modified using the WITH NO POPULATION clause. This put the full-text index into an inconsistent state. To bring the full-text index into a consistent state, start a full population. The basic Transact-SQL syntax for this is: ALTER FULLTEXT INDEX ON table_name START FULL POPULATION.    |
|    30023    |    16    |    No    |    The fulltext stoplist '%.*ls' does not exist or the current user does not have permission to perform this action. Verify that the correct stoplist name is specified and that the user had the permission required by the Transact-SQL statement.    |
|    30024    |    16    |    No    |    The fulltext stoplist '%.*ls' already exists in the current database. Duplicate stoplist names are not allowed. Rerun the statement and specify a unique stoplist name.    |
|    30028    |    17    |    No    |    Failed to get pipeline interface for '%ls', resulting in error: 0x%X. There is a problem communicating with the host controller or filter daemon host.    |
|    30029    |    17    |    No    |    The full-text host controller failed to start. Error: 0x%X.    |
|    30031    |    17    |    No    |    A full-text master merge failed on full-text catalog '%ls' in database '%.*ls' with error 0x%08X.    |
|    30032    |    16    |    No    |    The stoplist '%.*ls' does not contain fulltext stopword '%.*ls' with locale ID %d. Specify a valid stopword and locale identifier (LCID) in the Transact-SQL statement.    |
|    30033    |    16    |    No    |    The stoplist '%.*ls' already contains full-text stopword '%.*ls' with locale ID %d. Specify a unique stopword and locale identifier (LCID) in the Transact-SQL statement.    |
|    30034    |    16    |    No    |    Full-text stoplist '%.*ls' cannot be dropped because it is being used by at least one full-text index. To identify which full-text index is using a stoplist: obtain the stoplist ID from the stoplist_id column of the sys.fulltext_indexes catalog view, and then look up that stoplist ID in the stoplist_id column of the sys.fulltext_stoplists catalog view. Either drop the full-text index by using DROP FULLTEXT INDEX or change its stoplist setting by using ALTER FULLTEXT INDEX. Then retry dropping the stoplist.    |
|    30037    |    16    |    No    |    An argument passed to a fulltext function is not valid.    |
|    30038    |    17    |    No    |    Fulltext index error during compression or decompression. Full-text index may be corrupted on disk. Run dbcc checkdatabase and re-populate the index.    |
|    30039    |    17    |    No    |    Data coming back to the SQL Server process from the filter daemon host is corrupted. This may be caused by a bad filter. The batch for the indexing operation will automatically be retried using a smaller batch size.    |
|    30043    |    16    |    No    |    Stopwords of zero length cannot be added to a full-text stoplist. Specify a unique stopword that contains at least one character.    |
|    30044    |    16    |    No    |    The user does not have permission to alter the current default stoplist '%.*ls'. To change the default stoplist of the database, ALTER permission is required on both new and old default stoplists.    |
|    30045    |    17    |    No    |    Fulltext index error during compression or decompression. Full-text index may be corrupted on disk. Run dbcc checkdatabase and re-populate the index.    |
|    30046    |    16    |    No    |    SQL Server encountered error 0x%x while communicating with full-text filter daemon host (FDHost) process. Make sure that the FDHost process is running. To re-start the FDHost process, run the sp_fulltext_service 'restart_all_fdhosts' command or restart the SQL Server instance.    |
|    30047    |    16    |    No    |    The user does not have permission to %.*ls stoplist '%.*ls'.    |
|    30048    |    10    |    No    |    Informational: Ignoring duplicate thesaurus rule '%ls' while loading thesaurus file for LCID %d. A duplicate thesaurus phrase was encountered in either the \<sub\> section of an expansion rule or the \<pat\> section of a replacement rule. This causes an ambiguity and hence this phrase will be ignored.    |
|    30049    |    17    |    No    |    Fulltext thesaurus internal error (HRESULT = '0x%08x')    |
|    30050    |    16    |    No    |    Both the thesaurus file for lcid '%d' and the global thesaurus could not be loaded.    |
|    30051    |    16    |    No    |    Phrases longer than 512 unicode characters are not allowed in a thesaurus file. Phrase: '%ls'.    |
|    30052    |    16    |    No    |    The full-text query has a very complex NEAR clause in the CONTAINS predicate or CONTAINSTABLE function. To ensure that a NEAR clause runs successfully, use only six or fewer terms. Modify the query to simplify the condition by removing prefixes or repeated terms.    |
|    [30053](../../relational-databases/errors-events/mssqlserver-30053-database-engine-error.md)    |    16    |    No    |    Word breaking timed out for the full-text query string. This can happen if the wordbreaker took a long time to process the full-text query string, or if a large number of queries are running on the server. Try running the query again under a lighter load.    |
|    30055    |    10    |    No    |    Full-text catalog import has started for full-text catalog '%ls' in database '%ls'.    |
|    30056    |    10    |    No    |    Full-text catalog import has finished for full-text catalog '%ls' in database '%ls'. %d fragments and %d keywords were processed.    |
|    30057    |    10    |    No    |    Upgrade option '%ls' is being used for full-text catalog '%ls' in database '%ls'.    |
|    30059    |    16    |    No    |    A fatal error occurred during a full-text population and caused the population to be cancelled. Population type is: %s; database name is %s (id: %d); catalog name is %s (id: %d); table name %s (id: %d). Fix the errors that are logged in the full-text crawl log. Then, resume the population. The basic Transact-SQL syntax for this is: ALTER FULLTEXT INDEX ON table_name RESUME POPULATION.    |
|    30060    |    16    |    No    |    The import population for database %ls (id: %d), catalog %ls (id: %d) is being cancelled because of a fatal error ('%ls'). Fix the errors that are logged in the full-text crawl log. Then resume the import either by detaching the database and re-attaching it, or by taking the database offline and bringing it back online. If the error is not recoverable, rebuild the full-text catalog.    |
|    30061    |    17    |    No    |    The SQL Server failed to create full-text filterdata directory. This might be because FulltextDefaultPath is invalid or SQL Server service account does not have permission. Full-text blob indexing will fail until this issue is resolved. Restart SQL Server after the issue is fixed.    |
|    30062    |    17    |    No    |    The SQL Server failed to load FDHost service group sid. This might be because installation is corrupted.    |
|    30063    |    10    |    No    |    Warning: SQL Server could not set fdhost.exe processor affinity to %d because the value is not valid.    |
|    30064    |    17    |    No    |    SQL Server failed to set security information on the full-text FilterData directory in the FTData folder. Full-text indexing of some types of documents may fail until this issue is resolved. You will need to repair the SQL Server installation.    |
|    30067    |    10    |    No    |    Warning: The detach operation cannot delete a full-text index created on table '%ls' in database '%ls' because the index is on a read-only filegroup. To drop the full-text index, re-attach the database, change the read-only filegroup to read/write access and then detach it. This warning will not fail the database detach operation.    |
|    30068    |    10    |    No    |    During the database upgrade, the full-text filter component '%ls' that is used by catalog '%ls' was successfully verified. Component version is '%ls'; Full path is '%.*ls'.    |
|    30069    |    11    |    No    |    The full-text filter component '%ls' used to populate catalog '%ls' in a previous SQL Server release is not the current version (component version is '%ls', full path is '%.*ls'). This may cause search results to differ slightly from previous releases. To avoid this, rebuild the full-text catalog using the current version of the filter component.    |
|    30070    |    10    |    No    |    During the database upgrade, the full-text word-breaker component '%ls' that is used by catalog '%ls' was successfully verified. Component version is '%ls'. Full path is '%.*ls'. Language requested is %d. Language used is %d.    |
|    30071    |    11    |    No    |    The full-text word-breaker component '%ls' used to populate catalog '%ls' in a previous SQL Server release is not the current version (component version is '%ls', full path is '%.*ls', language requested is %d, language used is %d). This may cause search results to differ slightly from previous releases. To avoid this, rebuild the full-text catalog using the current version of the word-breaker component.    |
|    30072    |    10    |    No    |    During the database upgrade, the full-text protocol handler component '%ls' that is used by catalog '%ls' was successfully verified. Component version is '%ls'. Full path is '%.*ls'. Program ID is '%.*ls'.    |
|    30073    |    11    |    No    |    The full-text protocol handler component '%ls' used to populate catalog '%ls' in a previous SQL Server release is not the current version (component version is '%ls', full path is '%.*ls', program ID is '%.*ls'). This may cause search results to differ slightly from previous releases. To avoid this, rebuild the full-text catalog using the current version of the protocol handler component.    |
|    30074    |    17    |    No    |    The master merge of full-text catalog '%ls' in database '%.*ls' was cancelled.    |
|    30075    |    10    |    No    |    Full-text crawls for database ID: %d, table ID: %d, catalog ID: %d will be stopped since the clustered index on the table has been altered or dropped. Crawl will need to re-start from the beginning.    |
|    30076    |    10    |    No    |    Full-text crawl forward progress information for database ID: %d, table ID: %d, catalog ID: %d has been reset due to the modification of the clustered index. Crawl will re-start from the beginning when it is unpaused.    |
|    30077    |    16    |    No    |    The full-text query did not use the value specified for the OPTIMIZE FOR query hint. Only single terms are allowed as values for full-text queries that contain an OPTIMIZE FOR query hint. Modify the OPTIMIZE FOR query hint value to be a single, non-empty term.    |
|    30078    |    10    |    No    |    The fulltext query did not use the value specified for the OPTIMIZE FOR hint because the query contained more than one type of full-text logical operator.    |
|    30079    |    10    |    No    |    The full text query ignored UNKNOWN in the OPTIMIZE FOR hint.    |
|    30080    |    16    |    No    |    The full-text population on table '%ls' cannot be started because the full-text catalog is importing data from existing catalogs. After the import operation finishes, rerun the command.    |
|    30081    |    10    |    No    |    A cached plan was compiled using trace flags that are incompatible with the current values. Consider recompiling the query with the new trace flag settings.    |
|    30082    |    16    |    No    |    Full-text predicates cannot appear in an aggregate expression. Place the aggregate expression in a subquery.    |
|    30083    |    16    |    No    |    Full-text predicates cannot appear in the GROUP BY clause. Place a GROUP BY clause expression in a subquery.    |
|    30084    |    16    |    No    |    The full-text index cannot be created because filegroup '%.*ls' does not exist or the filegroup name is incorrectly specified. Specify a valid filegroup name.    |
|    30085    |    16    |    No    |    A stoplist cache cannot be generated while processing a full-text query or performing full-text indexing. There is not enough memory to load the stoplist cache. Rerun the query or indexing command when more resources are available.    |
|    30086    |    16    |    No    |    The system ran out of memory while building a full-text index. The batch for the full-text indexing operation will automatically be retried using a smaller batch size.    |
|    30087    |    16    |    No    |    Data coming back to the SQL Server process from the filter daemon host is corrupted. This may be caused by a bad filter. The batch for the indexing operation will automatically be retried using a smaller batch size.    |
|    30088    |    10    |    No    |    The full-text filter daemon host process has stopped normally. The process will be automatically restarted if necessary.    |
|    [30089](../../relational-databases/errors-events/mssqlserver-30089-database-engine-error.md)    |    17    |    No    |    The fulltext filter daemon host (FDHost) process has stopped abnormally. This can occur if an incorrectly configured or malfunctioning linguistic component, such as a wordbreaker, stemmer or filter has caused an irrecoverable error during full-text indexing or query processing. The process will be restarted automatically.    |
|    30090    |    10    |    No    |    A new instance of the full-text filter daemon host process has been successfully started.    |
|    30091    |    10    |    No    |    A request to start a full-text index population on table or indexed view '%.*ls' is ignored because a population is currently paused. Either resume or stop the paused population. To resume it, use the following Transact-SQL statement: ALTER FULLTEXT INDEX ON %.*ls RESUME POPULATION. To stop it, use the following statement: ALTER FULLTEXT INDEX ON %.*ls STOP POPULATION.    |
|    30092    |    16    |    No    |    Full-text stoplist ID '%d' does not exist.    |
|    30093    |    17    |    No    |    The SQL Server word-breaking client failed to initialize. This might be because a filter daemon host process is not in a valid state. This can prevent SQL Server from initializing critical system objects. Full-text queries will fail until this issue is resolved. Try stopping SQL Server and any filter daemon host processes and then restarting the instance of SQL Server.    |
|    30094    |    17    |    No    |    The full-text indexing pipeline could not be initialized. This might be because the resources on the system are too low to allocate memory or create tasks. Try restarting the instance of SQL Server.    |
|    30095    |    10    |    No    |    The version of the language components used by full-text catalog '%ls' in database '%ls' is different from the version of the language components included this version of SQL Server. The full-text catalog will still be imported as part of database upgrade. To avoid any possible inconsistencies of query results, consider rebuilding the full-text catalog.    |
|    30096    |    10    |    No    |    A full-text retry pass of %ls population started for table or indexed view '%ls'. Table or indexed view ID is '%d'. Database ID is '%d'.    |
|    30097    |    10    |    No    |    The fulltext catalog upgrade failed because of an inconsistency in metadata between sys.master_files and sys.fulltext_catalogs for the catalog ID %d in database ID %d. Try to reattach this database. If this fails, then the catalog will need to be dropped or recreated before attach.    |
|    30098    |    10    |    No    |    An internal query to load data for a crawl on database '%.*ls' and table '%.*ls' failed with error code %d. Check the sql error code for more information about the condition causing this failure. The crawl needs to be restarted after this condition is removed.    |
|    30099    |    17    |    No    |    Fulltext internal error    |
