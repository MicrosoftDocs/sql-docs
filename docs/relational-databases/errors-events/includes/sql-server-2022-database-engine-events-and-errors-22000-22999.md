---
author: rwestMSFT
ms.author: randolphwest
ms.date: 01/11/2024
ms.topic: include
---
| Error | Severity | Event logged | Description |
| :--- | :--- | :--- | :--- |
| 22001 | 16 | No | FreeStaleVersionSpace failed for page %S_PGID for database id '%d'. It will be retired in the next iteration of the version cleaner. |
| 22002 | 17 | No | Internal Error: An expected large object wasn't found. |
| 22003 | 16 | No | Stale/aborted version cleanup was aborted for database id '%d' due to database shutdown. |
| 22004 | 10 | No | \[%d\]. System transaction with xdes id %S_XID was aborted due to failure injection while moving rows from one page to another. |
| 22005 | 10 | No | ADR cleanup failed for database id '%d'. |
| 22006 | 16 | No | Version cleanup was aborted for database id '%d' due to database exclusive waiter. |
| 22007 | 16 | No | Version cleanup was aborted for database id '%d' due to planned failover. |
| 22008 | 16 | No | Aborted versions belonging to this filegroup could not be cleaned up. |
| 22010 | 16 | No | Cannot change the READONLY property of the filegroup containing Persistent Version Store which is required for Accelerated Database Recovery. |
| 22011 | 16 | No | Invalid IAM chain encountered while dropping allocation unit. |
| 22012 | 16 | No | Persistent version store is enabled on the database '%.\*ls' but the version store manager could not be initialized. |
| 22013 | 16 | No | \[DbId: %d\]\[FileId: %d\] Could not obtain file add/remove latch to purge invalid file ranges before shrinking the file. |
| 22014 | 10 | No | Lock ordinal population was aborted due to database exclusive waiter. |
| 22015 | 10 | No | Forwarding of row \[FileId: %d\]\[PageInFile: %d\]\[SlotId: %d\] during logical revert failed due to insufficient space. |
| 22016 | 16 | No | \[DbId:%d\] Sum of row sizes (%d) on page (%d:%d) exceeds maximum size of a page. |
| 22017 | 16 | No | Could not find row version because it has been cleaned, this is due to transaction abort for some other reason on any node in the system where version is not required to be kept for aborted transaction. |
| 22019 | 16 | No | Version cleanup was aborted for database id '%d' due to logical pause of db. |
| 22020 | 17 | No | Internal Error: Tried to access an expired large object. |
| 22021 | 13 | No | The EX Latch failure occurred on RefreshAllHoBts. |
| 22101 | 16 | No | The value supplied for the change_columns argument of CHANGE_TRACKING_IS_COLUMN_IN_MASK function is not valid. The value must be a bitmask returned by the CHANGETABLE(CHANGES ...) function. |
| 22102 | 16 | No | The arguments supplied are not valid for the CHANGES option of the CHANGETABLE function. |
| 22103 | 16 | No | The arguments supplied are not valid for the VERSION option of the CHANGETABLE function. |
| 22104 | 16 | No | A table returned by the CHANGETABLE function must be aliased. |
| 22105 | 16 | No | Change tracking is not enabled on table '%.\*ls'. |
| 22106 | 16 | No | The CHANGETABLE function does not support remote data sources. |
| 22107 | 16 | No | Object '%.\*ls' is of a data type that is not supported by the CHANGETABLE function. The object must be a user-defined table. |
| 22108 | 16 | No | The CHANGE_TRACKING_CONTEXT WITH clause cannot be used with a SELECT statement. |
| 22109 | 16 | No | The "context" argument for the CHANGE_TRACKING_CONTEXT WITH clause must be of type varbinary data type with a maximum length of 128. |
| 22110 | 16 | No | The number of columns specified in the CHANGETABLE(VERSION ...) function does not match the number of primary key columns for table '%.\*ls'. |
| 22111 | 16 | No | The column '%.\*ls' specified in the CHANGETABLE(VERSION ...) function is not part of the primary key for table '%.\*ls'. |
| 22112 | 16 | No | Each primary key column must be specified once in the CHANGETABLE(VERSION ...) function. The column '%.\*ls' is specified more than once. |
| 22113 | 16 | No | %S_MSG is not allowed because the table is being tracked for change tracking. |
| 22114 | 16 | No | Change tracking options for ALTER DATABASE cannot be combined with other ALTER DATABASE options. |
| 22115 | 16 | No | Change tracking is enabled for one or more tables in database '%.\*ls'. Disable change tracking on each table before disabling it for the database. Use the sys.change_tracking_tables catalog view to obtain a list of tables for which change tracking is enabled. |
| 22116 | 16 | No | Change tracking is not supported by this edition of SQL Server. |
| 22117 | 16 | No | For databases that are members of a secondary availability replica, change tracking is not supported. Run change tracking queries on the databases in the primary availability replica. |
| 22118 | 16 | No | Cannot enable change tracking on table '%.\*ls'. Change tracking is not supported when the primary key contains encrypted columns. |
| 22119 | 16 | No | Cannot enable change tracking on table '%.\*ls'. Change tracking requires a primary key constraint on the table to be enabled. Enable the primary key constraint on the table before enabling change tracking. |
| 22120 | 16 | No | Invalid value for cleanup batch size. |
| 22121 | 16 | No | Deleted %ld row(s) per millisecond from %s |
| 22122 | 16 | No | Change Tracking autocleanup failed on side table of "%s". If the failure persists, use sp_flush_CT_internal_table_on_demand to clean up expired records from its side table. |
| 22123 | 16 | No | Change Tracking autocleanup is blocked on side table of "%s". If the failure persists, check if the table "%s" is blocked by any process . |
| 22124 | 16 | No | Change Tracking manual cleanup is blocked on side table of "%s". If the failure persists, check if the table "%s" is blocked by any process . |
| 22125 | 16 | No | Change tracking autocleanup is currently not able to maintain retention for database ID %d. Number of expired records: %d. If this warning persists, check the following resource: [https://learn.microsoft.com/sql/relational-databases/track-changes/cleanup-and-troubleshoot-change-tracking-sql-server](../../track-changes/cleanup-and-troubleshoot-change-tracking-sql-server.md) |
| 22201 | 16 | No | Internal error. Unable to acquire the latch holding buffers for DW Tiered Storage ColumnStore Scan. |
| 22202 | 16 | No | Internal error. Unable to run the remote cs garbage collector. Error Code 22202. |
| 22203 | 16 | No | Internal error. Unable to update the blobs table in the catalogDB. Error Code 22203. |
| 22204 | 16 | No | Internal error. Unable to refresh catalog db information for service uri \[%ls\]. |
| 22205 | 16 | No | Internal error. Unable to get catalog information via the catalog helper. |
| 22206 | 16 | No | Internal error. Unable to get lock for protected shared buffer |
| 22207 | 16 | No | Internal error. Unable to get a valid dbtable. Error Code 22207. |
| 22208 | 16 | No | Access to DW Tiered Storage ColumnStore blob failed. See earlier errors for cause. |
| 22209 | 16 | No | Internal error. Unable to populate instance member list. |
| 22210 | 16 | No | Internal error. Unable to get instance member. |
| 22211 | 16 | No | Internal error. Invalid instance member state. |
| 22212 | 21 | No | An error occurred while reading remote column store segment HoBt 0x%I64X, Object %d, Column %d, Type %d in database %d. The segement could not be decrypted. |
| 22213 | 16 | No | Internal error. Unable to get catalog information via the sp. |
| 22214 | 16 | No | Internal error. Unable to initialise XODBC Connection Manager. |
| 22215 | 16 | No | Internal error. Unable to get catalog information. |
| 22216 | 16 | No | Internal error. Protected buffer failure. |
| 22217 | 16 | No | Internal error. Persist lru cost info failure. |
| 22218 | 16 | No | Internal error. Catalog Communication Failure. |
| 22219 | 16 | No | Internal error. Internal table base failure. |
| 22220 | 10 | No | Beginning database migration scan for database "%s". |
| 22221 | 10 | No | Database migration scan for database "%s" is complete. |
| 22222 | 10 | No | Database migration scan for database '%.\*ls' was aborted. Internal error. Migration scan was aborted. |
| 22223 | 16 | No | Internal error. Unable to refresh migration type from fabric property. |
| 22224 | 16 | No | Internal error. ADW Optimized for Compute storage detected. Unable to retrieve blob. |
| 22225 | 16 | No | An internal error (%d, %d) occurred. Please retry the operation. If the problem persists contact Microsoft Azure Customer Support. |
| 22226 | 16 | No | An internal error (%d, %d) occurred. Please retry the operation. If the problem persists contact Microsoft Azure Customer Support. |
| 22227 | 10 | No | TIERED Storage Scanner encountered an error message "%ls" in "%ls". |
| 22228 | 22 | No | The columnstore remote lob header is invalid. |
| 22229 | 22 | No | Remote storage columnstore data checksum mismatch. Expected checksum from blob is %lu, actual check sum from read buffers is %lu. |
| 22230 | 16 | No | Failure to copy remote consolidated rowgroup blob HoBt id 0x%I64X, rowgroup id %d in database %d to Azure Block Blob Storage. Error code 0x%X. |
| 22231 | 17 | No | : Encountered an unexpected error while trying to access a lob row (HRESULT = 0x%x). |
| 22232 | 21 | No | An error occurred while writing remote column store segment HoBt 0x%I64X, Object %d, Column %d, Type %d in database %d. The segement could not be encrypted. |
| 22233 | 17 | No | : Encountered an unexpected error while trying to access/update remote object (HRESULT = 0x%x). |
| 22234 | 21 | No | An error occurred while writing remote rowgroup metadata HoBt 0x%I64X, Rowgroup %d,in database %d. The metadata could not be encrypted. |
| 22235 | 16 | No | Could not process request due to internal error encountered during control node communication |
| 22236 | 17 | No | : Encountered an unexpected error while trying to access/update fork entries (HRESULT = 0x%x). |
| 22237 | 16 | No | Failure to copy manifest file block blobs during Table Clone operation. Error code 0x%X. |
| 22301 | 16 | No | The feature is not supported in this version of SQL Server. |
| 22302 | 16 | No | Transaction context not found. |
| 22303 | 16 | No | Updates are not allowed from a ReadOnly transaction. |
| 22304 | 16 | No | Reading or Writing to database files is not supported in this Database Version. |
| 22305 | 16 | No | A NULL or unexpected value was returned by an ODBC call. |
| 22306 | 16 | No | Only CCI tables are allowed in this database version. |
| 22307 | 16 | No | Alter DDL statements are not allowed in this transaction scope. |
| 22308 | 16 | No | Thread failed to acquire a lock. |
| 22309 | 16 | No | Cache database not found. |
| 22310 | 16 | No | Failed to initialize database: \[%.\*ls\]. Check errorlog for more info. |
| 22311 | 16 | No | Generic out of bounds error. |
| 22313 | 16 | No | Failed to find columns in rowset with Id '%I64d'. |
| 22314 | 16 | No | Invalid ODBC connection. |
| 22315 | 16 | No | Invalid Transaction type. |
| 22316 | 16 | No | Failed to acquire CSI Cache lock. |
| 22317 | 16 | No | Invalid ODBC column. |
| 22318 | 16 | No | ODBC transaction failed a commit. |
| 22319 | 16 | No | An invalid access to a Rowset (DbId '%lu', RowsetId '%I64d') was performed. |
| 22320 | 16 | No | Open database '%.\*ls' failed. It cannot be used under the current session context. |
| 22321 | 16 | No | Open database (Id:'%lu', Name: '%.\*ls') failed. Only DbId: '%lu' is allowed in current session context. |
| 22322 | 16 | No | Session context information was incorrect. |
| 22323 | 16 | No | Server is not initialized. |
| 22324 | 16 | No | Client is not initialized. |
| 22325 | 16 | No | The Key column Id (%d) for Rowset (%I64d) is out of range. |
| 22326 | 16 | No | Updates are not allowed from a Scan only Rowset. |
| 22327 | 16 | No | Invalid Rowset Id (%I64d) was provided to a Rowset remapping sys RowsetId (%I64d). |
| 22328 | 16 | No | Alters are only allowed in this SQL Server instance. |
| 22329 | 16 | No | Open of Rowset (%I64d) in DB (%d) failed. |
| 22330 | 16 | No | RowsetColumn Id (%d) missing in Rowset (%I64d). |
| 22331 | 16 | No | Provided Accessor Mode is not supported in Rowset. |
| 22332 | 16 | No | Encountered an error or an unexpected data during the operation. |
| 22333 | 16 | No | Failed to retrieve Blob Container info. |
| 22334 | 16 | No | This operation cannot be preformed in this version of Database. |
| 22335 | 16 | No | Cannot obtain a LOCK resource at this time due to internal error. Rerun your statement when there are fewer active users. |
| 22336 | 16 | No | The provided transaction was not found. |
| 22337 | 16 | No | Invalid nesting level for lock manager table. |
| 22338 | 16 | No | Invalid nested transaction value for requesting lock. |
| 22339 | 16 | No | An error occurred while releasing a lock with error code: %d state: %d severity: %d |
| 22340 | 16 | No | An error occurred while requesting a lock with error code: %d state: %d severity: %d |
| 22341 | 16 | No | An error occurred while releasing all locks for transaction id %d with error code: %d state: %d severity: %d |
| 22342 | 16 | No | Cannot release locks for transaction %d as it has active nested transactions |
| 22343 | 16 | No | This DDL statement is not supported in this version of SQL Server. |
| 22344 | 16 | No | This statement cannot be executed in this version of Database. |
| 22345 | 16 | No | Transaction manager already exists. |
| 22346 | 16 | No | Transaction manager not found. |
| 22347 | 16 | No | Transaction manager lock acquisition failure |
| 22348 | 16 | No | Transaction manager address not initialized |
| 22350 | 16 | No | Found a fatal inconsistency in Delta files. |
| 22351 | 16 | No | Encountered error %lu during parsing of RowGroup metadata. Contact Technical Support. |
| 22352 | 16 | No | Encountered unexpected error during physical catalog maintenance. Contact Technical Support. |
| 22353 | 16 | No | The SQL instances has not been correctly setup to allow this operation. Please retry the operation again. If the problem persists contact Microsoft Azure Customer Support. |
| 22354 | 16 | No | Lock manager initialization failed. |
| 22355 | 16 | No | Lock manager does not exist. Please retry the operation again. If the problem persists contact Microsoft Azure Customer Support. |
| 22356 | 16 | No | Lock manager shutdown failed. |
| 22357 | 16 | No | Point in time can only be specified for a Read transaction. |
| 22358 | 16 | No | The Database Controller required for this operation was not found. Please retry the operation again. If the problem persists contact Microsoft Azure Customer Support. |
| 22359 | 16 | No | Exceeded the resource limit on the number concurrent DDLs and active transactions. Please scale up to a higher SLO or reduce the workload on the SQL instance. |
| 22360 | 16 | No | Logical Database Name was not provided |
| 22361 | 16 | No | The handler for the work item does not exists. |
| 22362 | 16 | No | Timed out while waiting for internal task to complete. |
| 22363 | 16 | No | Lock manager client does not exist. |
| 22364 | 16 | No | Lock manager client initialization failed. |
| 22365 | 16 | No | Client initialization failed with error %d. |
| 22366 | 16 | No | Transaction context encode failed with error %d |
| 22367 | 16 | No | Transaction context decode failed with error %d and size %d. |
| 22368 | 16 | No | Database '%ls' configuration '%ls' not found. |
| 22369 | 16 | No | Failed to start an async task to sync the physical catalog. |
| 22370 | 16 | No | The statement failed due to an internal error while retrieving the query text. |
| 22371 | 16 | No | Background task failed. |
| 22372 | 16 | No | Lock release failed due to an internal error. Check the errorlog for more information. |
| 22374 | 16 | No | DDL statements are not allowed in User transactions. |
| 22375 | 16 | No | An operation that requires a feature to be enabled was started, but the feature is disabled. Aborting. Contact support for assistance. |
| 22376 | 16 | No | Invalid Transaction context while requesting lock. |
| 22377 | 16 | No | Transaction abort sequence number entry not found in cache |
| 22378 | 16 | No | DW Temp Table Creation Fail |
| 22379 | 16 | No | Failed to acquire the data update lock for DB Id '%ls'. |
| 22380 | 16 | No | Updating the rowcount of a distributed table is not allowed unless trace flag 12127 is enabled. Rerun the statement by enabling the said trace flag. |
| 22381 | 16 | No | Fido GLM remote execute command failed. |
| 22382 | 16 | No | Transport DBM endpoint not available. |
| 22383 | 16 | No | Transport not initialized. |
| 22384 | 16 | No | DDL statements cannot cross DB boundaries. |
| 22385 | 16 | No | The statement could not be serialized. Please try again. |
| 22386 | 16 | No | Transport UCS connection string resolve failed. |
| 22387 | 16 | No | Transport get response failed. |
| 22388 | 17 | No | Failed to start the client sync thread during manager startup. |
| 22389 | 16 | No | Transaction Action failed with error 0x%x. |
| 22390 | 16 | No | Database name '%.\*ls' is not allowed in this edition of SQL. |
| 22391 | 16 | No | Background task queue failed. |
| 22392 | 16 | No | Background task discovery failed. |
| 22393 | 16 | No | Background task failed with error: %d, state: %d and message: '%ls'. |
| 22394 | 17 | No | Failed to start the Server due to error %d. Check errorlog for more info. |
| 22395 | 17 | No | Failed to start database '%ls' as it already exists. |
| 22396 | 17 | No | Encountered exception when executing DBCC command. |
| 22397 | 16 | No | Attempt to concurrently modify index \[%ls\].\[%ls\] row with Key %ls by multiple transactions '%I64d', '%I64d'. |
| 22398 | 17 | No | Failed to set the drop time of object: '%I64d' as its create was not tracked. TxnId %I64d. |
| 22399 | 17 | No | Failed Update TxnId of object: '%I64d' by TxnId %I64d. Old TxnId %I64d. |
| 22401 | 17 | No | Failed to open internal table. |
| 22402 | 16 | No | A NULL or unexpected value was returned by a remote call. |
| 22403 | 16 | No | Unexpected operation performed on an internal table. |
| 22404 | 16 | No | Failed to Register Database '%ls' in master. |
| 22405 | 16 | No | Bulk insert bucketization failed. |
| 22406 | 16 | No | Failed to Sync Database '%ls' to Server. |
| 22407 | 16 | No | Failed to acquire DW transaction lock. |
| 22408 | 16 | No | A severe error occurred. Error:%d, State:%d. |
| 22409 | 16 | No | Table '%.\*ls' does not exist. |
| 22410 | 16 | No | Validations failed for the internal task handler. |
| 22411 | 16 | No | Transaction got aborted due to unexpect situation. |
| 22412 | 16 | No | Transaction Manager fail to create schema |
| 22413 | 16 | No | Resource manager lock acquisition failure |
| 22414 | 16 | No | Resource manager not found. |
| 22415 | 16 | No | An error occurred while releasing all locks for transaction id 0x%016llx while commit of transaction, HRESULT = 0x%08X |
| 22416 | 16 | No | An error occurred while releasing all locks for transaction id 0x%016llx while rollback of transaction, HRESULT = 0x%08X |
| 22417 | 16 | No | An error occurred while commit transaction id 0x%016llx, HRESULT = 0x%08X |
| 22418 | 16 | No | An error occurred while rollback transaction id 0x%016llx, nested id 0x%016llx, HRESULT = 0x%08X |
| 22419 | 16 | No | An error occurred while begin transaction, transaction options = %d, HRESULT = 0x%08X |
| 22420 | 16 | No | An error occurred while get list, transaction id 0x%016llx, list options = %d, HRESULT = 0x%08X |
| 22421 | 16 | No | Changes in encryption are not allowed on this database type if tables are present. Please drop all tables and try again. |
| 22422 | 16 | No | Failed to initialize a remote connection for an internal component. |
| 22424 | 17 | No | %ls is not a supported statement type. |
| 22425 | 17 | No | %ls is not a supported option in %ls statement. |
| 22426 | 16 | No | Failed to get the client id from the Resource Manager. |
| 22427 | 16 | No | Operation failed due to an error in a background task. Please retry the operation again. If the problem persists contact Microsoft Azure Customer Support. |
| 22428 | 16 | No | Failed to Begin a new transaction as a valid one already exists. Please Commit or Rollback the existing transaction before starting a new one. |
| 22429 | 16 | No | Dropping multiple objects including Temp table is not supported in this version of SQL Server |
| 22430 | 16 | No | Operation failed as the Database '%.\*ls' is shutting down. Please retry the operation again. If the problem persists contact Microsoft Azure Customer Support. |
| 22431 | 16 | No | You must have a compute pool provisioned in order to enable encryption. Create a compute pool and try again. |
| 22432 | 16 | No | Failure in the compression engine. If the problem persists contact Microsoft Azure Customer Support. |
| 22433 | 16 | No | An error occurred while set transaction token, HRESULT = 0x%08X |
| 22434 | 16 | No | An inconsistency was detected while enumerating files for the database. If the problem persists contact Microsoft Azure Customer Support. |
| 22435 | 16 | No | Catalog sync failed while updating system tables for \[%I64d\] database, rowset \[%I64d\], directory \[%I64d\], rowgroup \[%I64d\]. Used by failpoints. |
| 22436 | 16 | No | This operation is not supported in this Database Version. |
| 22437 | 16 | No | Failed to find a valid TxnId of object: '%ld' |
| 22438 | 16 | No | Can't set retention policy on an invalid object id: '%ld' |
| 22439 | 16 | No | Internal error. Unable to load database settings. |
| 22440 | 16 | No | An error occurred during timestamp conversion. Please provide the correct format in the form yyyy-mm-ddThh:mm:ss. |
| 22441 | 16 | No | An error occurred while begin transaction, requested point in time not found '%ls' |
| 22442 | 16 | No | Point in time not supported for this statement type |
| 22443 | 16 | No | ALTER TABLE RANGE statement failed. The specified table '%.\*ls' is not partitioned. |
| 22444 | 16 | No | ALTER TABLE RANGE statement failed. The specified range must not be 'NULL'. |
| 22445 | 16 | No | This DML statement is not supported in this database version. |
| 22446 | 16 | No | DeltaForce encountered an operational exception. |
| 22447 | 16 | No | Internal table IQ_CATATLOG_OBJECTS_TABLE error. |
| 22448 | 10 | No | An error occurred while updating the data encryption key. |
| 22449 | 10 | No | The reencryption task encountered a blob that was already encrypted. |
| 22450 | 10 | No | An error occurred with clone with message '%ls'. |
| 22451 | 16 | No | An error occurred while initializing resource manager |
| 22452 | 16 | No | Index quality DMV got invalid arguments. |
| 22453 | 16 | No | Unable to get remote storage space usage: Error: %d, Severity: %d, State: %d, Line: %d '%s' |
| 22454 | 16 | No | Internal error. Encryption scan was aborted. |
| 22455 | 16 | No | A SERVICE_OBJECTIVE must be specified when creating a Synapse pool. |
| 22456 | 16 | No | Pool '%.\*ls' does not exist. Make sure that the name is entered correctly. |
| 22457 | 16 | No | DBCC command failed with error : '%.\*ls'. |
| 22458 | 16 | No | Cannot specify a temporary object (i.e., View, Table or Stored Procedure) in a rename statement. |
| 22459 | 16 | No | The metadata backup path was incorrectly formatted. |
| 22460 | 16 | No | Index store client does not exist. |
| 22461 | 16 | No | %ls is not supported for %ls. |
| 22462 | 16 | No | Encountered exception during generation of script for clone. |
| 22463 | 16 | No | Increase rowset blob directory generation id failed |
| 22464 | 16 | No | Database collation name '%.\*ls' is not valid. |
| 22465 | 16 | No | Failed to clean up rows for object: '%d'. (HRESULT = 0x%x) |
| 22466 | 16 | No | Connection to reserved databases are not allowed. |
| 22467 | 16 | No | Failed complete data warehouse maintenance operation. See telemetry for more details. |
| 22468 | 16 | No | The metadata backup failed. |
| 22469 | 16 | No | Failed to update metadata of cloned table |
| 22470 | 16 | No | Rowset info is not correct, please check the following info: %ls |
| 22471 | 16 | No | Failed to serialize source table directory info |
| 22472 | 16 | No | The supplied T-SQL statement is too long. The maximum allowed length is 4000 characters. |
| 22473 | 16 | No | %ls is allowed only when connected to the logical master of a Synapse workspace. |
| 22474 | 16 | No | DBCC CHECKIDENTITY with NORESEED is not supported in Azure Synapse Analytics |
| 22475 | 17 | No | Failed to set the undrop time of object: '%I64d' as it was not found or drop was not tracked. TxnId %I64d. |
| 22476 | 16 | No | Toad index tuning policy check failed. See other errors and telemetry for details. |
| 22477 | 16 | No | The period of %ld %S_MSG is too big for data retention. |
| 22478 | 16 | No | Retention Policy is not supported on temporary table |
| 22479 | 16 | No | Failed to Alter Column as a concurrent Alter Column transaction has been detected. |
| 22480 | 16 | No | Creating or altering table '%.\*ls' failed because its maximum row size exceeds the allowed maximum of %d bytes including %d bytes of internal overhead. |
| 22481 | 16 | No | Creating a Synapse SQL pool with name '%ls' is not allowed as it is a reserved system name. Please choose another name for your Synapse SQL pool. |
| 22482 | 16 | No | %ls is an invalid name for a pool. Please make sure that it begins with a lowercase letter, consists only of lowercase letters, numbers, or hyphens ('-'), and does not end with a hyphen. The length cannot exceed 60 characters. |
| 22483 | 16 | No | MAX_SERVICE_OBJECTIVE '%ls' cannot be less than or equal to SERVICE_OBJECTIVE '%ls'. |
| 22484 | 16 | No | MAX_SERVICE_OBJECTIVE '%ls' cannot be more than '%ld' times the value of SERVICE_OBJECTIVE '%ls'. |
| 22485 | 16 | No | '%ls' is an invalid name for a pool as it contains profanity. Please choose a different name for your pool. |
| 22486 | 16 | No | Get directory info failed |
| 22487 | 16 | No | %ls is allowed only when connected to Synapse frontend. |
| 22488 | 16 | No | A WORKLOAD_GROUP must be specified when creating a Synapse workload classifier. |
| 22489 | 16 | No | START_TIME and END_TIME must be specified together when creating a Synapse workload classifier. |
| 22490 | 16 | No | Invalid data requested from database completed requests table. |
| 22491 | 16 | No | The DDL statement failed due to an internal error. Please retry the operation again. If the problem persists contact Microsoft Azure Customer Support. |
| 22492 | 16 | No | Unable to perform operation as the data provided is invalid. |
| 22493 | 16 | No | The database '%.\*ls' failed to sync. Please retry the operation again. If the problem persists contact Microsoft Azure Customer Support. |
| 22494 | 16 | No | There is already an object named '%.\*ls' in the temp db. |
| 22495 | 16 | No | %ls is allowed only when connected to Synapse frontend. |
| 22496 | 16 | No | The client sync failed with a request to retry the operation again. |
| 22497 | 16 | No | Redo of Block %I64d.%I64d on Database \[%ls\] failed with error '%.\*ls'. |
| 22498 | 16 | No | There is not enough resources to perform the operation. Please retry your operation later. |
| 22499 | 10 | No | An error occurred when looking for clone container: '%ls'. |
| 22500 | 16 | No | Unexpected |
| 22501 | 16 | No | All articles in the publication passed data validation (rowcount and checksum). |
| 22502 | 16 | No | Not All articles in the publication passed data validation (rowcount only) |
| 22503 | 16 | No | Initializing. |
| 22504 | 16 | No | Applying the snapshot to the Subscriber. |
| 22505 | 16 | No | The merge completed with no data changes processed. |
| 22506 | 16 | No | No data needed to be merged. |
| 22507 | 16 | No | Uploading data changes to the Publisher. |
| 22508 | 16 | No | Downloading data changes to the Subscriber. |
| 22509 | 16 | No | Retrieving subscription information. |
| 22510 | 16 | No | Retrieving publication information. |
| 22511 | 16 | No | The merge completed successfully. |
| 22512 | 16 | No | Cannot use partition groups with unfiltered publications. Set "use_partition_groups" to "false" using sp_changemergepublication. |
| 22513 | 16 | No | Cannot use partition groups because the join filter between the following articles contains one or more functions: "%s" and "%s". |
| 22514 | 16 | No | Cannot use partition groups because one or more filters reference the following view, which contains functions: "%s". |
| 22515 | 16 | No | The publication cannot use precomputed partitions because there is at least one circular reference in the join filters specified for the articles in the publication. To use precomputed partitions, ensure that no circular join filter relationships exist. |
| 22516 | 16 | No | The publication "%s" was defined as having dynamic filters, but it does not contain any dynamic filters. |
| 22517 | 16 | No | The publication was defined as having no dynamic filters, but contains one or more dynamic filters. |
| 22518 | 16 | No | Cannot use column of type image, ntext, xml, CLR type, varchar(max), nvarchar(max), or varbinary(max) in a subset or join filter for article: '%s'. |
| 22519 | 16 | No | Cannot add a logical record relationship between tables "%s" and "%s" because a text, image, ntext, xml, varchar(max), nvarchar(max), or varbinary(max) column was referenced in the join clause. |
| 22520 | 10 | No | A filtering type changed for the article. Any pending or future changes made in this article by a Subscriber in a given partition will no longer be propagated to Subscribers in other partitions. Check the documentation for details. |
| 22521 | 10 | No | Unable to synchronize the row because the row was updated by a different process outside of replication. |
| 22522 | 16 | No | Article '%s' cannot be published because it is published in another merge publication. An article that has a value of 3 for the @partition_options parameter of sp_addmergearticle (nonoverlapping partitions with a single subscription per partition) cannot be included in multiple publications or subscriptions, and cannot be republished. To include the article in multiple publications, use sp_changemergearticle to specify a different value for the partition_options property of the existing article. |
| 22523 | 16 | No | An article cannot use @partition_options 2 or 3 (nonoverlapping partitions) and be a part of a logical record relationship at the same time. Check article "%s". |
| 22524 | 16 | No | Article '%s' is published in another merge publication, with a different value specified for the @partition_options parameter of sp_addmergearticle. The specified value must be the same in all merge publications. Either specify the same value as the existing article or change the existing article using sp_changemergearticle. |
| 22525 | 16 | No | The publication "%s" cannot allow multiple subscriptions per partition if it contains articles using @partition_options = 3. |
| 22526 | 16 | No | An invalid value was specified for %s. Valid values are 0 (none), 1 (enforced partitions), 2 (nonoverlapping partitions with multiple subscriptions per partition), and 3 (nonoverlapping partitions with single subscription per partition). |
| 22527 | 16 | No | Invalid value specified for %s. Valid values are 'day', 'days', 'dd', 'year', 'years', 'yy', 'yyyy', 'month', 'months', 'mm', 'week', 'weeks', 'wk', 'hour', 'hours', 'hh', 'minute', 'minutes', 'mi'. |
| 22528 | 16 | No | Cannot use a retention period unit other than "days" for publication "%s" because the compatibility level of the publication is lower than 90. Use sp_changemergepublication to set publication_compatibility_level to 90RTM. |
| 22529 | 16 | No | Cannot change the retention period unit for publication "%s" because the compatibility level of the publication is lower than 90. Use sp_changemergepublication to set publication_compatibility_level to 90RTM. |
| 22530 | 16 | No | Cannot update any column in article "%s" that is used in a logical record relationship clause. |
| 22531 | 10 | No | Initialization. |
| 22532 | 10 | No | Upload of Subscriber changes to the Publisher. |
| 22533 | 10 | No | Download of Publisher changes to the Subscriber. |
| 22534 | 16 | No | Character mode publication does not support partitioned tables. |
| 22535 | 16 | No | For heterogeneous publications, the %s parameters should be specified when calling "%s". |
| 22536 | 16 | No | The %s parameter value cannot be updated or changed for heterogeneous publications. |
| 22537 | 16 | No | The job_login provided must match the Publisher login specified when adding the distribution Publisher (sp_adddistpublisher). |
| 22538 | 16 | No | Only replication jobs or job schedules can be added, modified, dropped or viewed through replication stored procedures. |
| 22539 | 16 | No | Use of parameters %s is invalid when parameter %s is set to %s. |
| 22540 | 16 | No | Cannot change publication "%s" to use sync_mode of "character", because it contains one or more logical record relationships. |
| 22541 | 16 | No | Cannot add a logical record relationship in publication "%s" because it uses sync_mode of "character" and could have SQL Server Compact Edition subscribers. |
| 22542 | 16 | No | Invalid value for property @subscriber_upload_options. Valid values are 0 (allow uploads), 1 (disable uploads), 2 (disable uploads prohibit subscriber changes), and 3 (disable_outofpartition_subscriber_changes). |
| 22543 | 16 | No | When the publication property @allow_partition_realignment is set to "false", the article property @subscriber_upload_options for all articles in the publication must be set to disable uploads. |
| 22544 | 10 | No | Warning: The procedure sp_mergecleanupmetadata has been deprecated. In SQL Server 2000 SP1 and later, the merge agent calls sp_mergemetadataretentioncleanup every time it runs, so manual cleanup of metadata is not needed. Ignoring passed-in parameters and calling sp_mergemetadataretentioncleanup. |
| 22545 | 16 | No | Cannot add a logical record relationship in publication "%s" because it allows Web synchronization. |
| 22546 | 16 | No | Cannot change publication "%s" to allow Web synchronization because it contains one or more logical record relationships. |
| 22547 | 16 | No | A concurrent snapshot is not allowed for snapshot publications. |
| 22548 | 16 | No | Vertical partitioning is only allowed for log-based articles. |
| 22549 | 16 | No | A shared distribution agent (%s) already exists for this subscription. |
| 22550 | 16 | No | Cannot drop identity column "%s" from the vertical partition when identityrangemanagementoption is set to auto. |
| 22551 | 16 | No | The type "%s" is invalid. Valid types are "merge", "tran", and "both". |
| 22552 | 16 | No | A valid value for parameter "@resync_date_str" needs to be provided when "@resync_type" is set to 2. |
| 22553 | 16 | No | The parameter "@resync_type" is set to "%d" but this subscription has never been successfully validated. |
| 22554 | 16 | No | Cannot change publication "%s" to use the sync_mode of "character" because it uses a retention period unit other than "day". Use sp_changemergepublication to set the retention period unit to "day". |
| 22555 | 16 | No | Cannot set the retention period unit to a value other than "day" for publication "%s" because it uses the sync_mode of "character" and may have SQL Server Compact Edition subscribers. |
| 22556 | 16 | No | Invalid value for the property "%s". Valid values are 1 and 0. |
| 22557 | 16 | No | The status of the schema change could not be updated because the publication compatibility level is less than 90. Use sp_changemergepublication to set the publication_compatibility_level of publication "%s" to 90RTM. |
| 22558 | 16 | No | The status of the schema change could not be updated. |
| 22559 | 16 | No | The status of the schema change must be "active" or "skipped". |
| 22560 | 16 | No | Merge replication does not allow filters that reference dynamic functions that take one or more parameters. Check the function "%s". |
| 22561 | 16 | No | The requested operation failed because the publication compatibility level is less than 90. Use sp_changemergepublication to set the publication_compatibility_level of publication "%s" to 90RTM. |
| 22562 | 16 | No | Cannot change the compatibility level of a publication to a value lower than the existing value. |
| 22563 | 16 | No | contains one or more articles that do not upload changes |
| 22564 | 16 | No | uses ddl replication |
| 22565 | 16 | No | uses other than day as unit of retention period |
| 22566 | 16 | No | uses logical records |
| 22567 | 16 | No | contains one or more articles that use subscription-based or partition-based filtering |
| 22568 | 16 | No | contains one or more articles which will not compensate for errors |
| 22569 | 16 | No | contains one or more schema only articles |
| 22570 | 16 | No | contains one or more articles that use automatic identity range management |
| 22571 | 16 | No | contains one or more articles that use datatypes new in SQL Server 2000 |
| 22572 | 16 | No | contains one or more articles with a timestamp column |
| 22573 | 16 | No | uses snapshot compression with snapshot_in_defaultfolder set to false |
| 22574 | 16 | No | contains one or more articles that use vertical partitioning |
| 22575 | 16 | No | When article property 'published_in_tran_pub' is set to 'true' then article property 'upload_options' has to be set to disable uploads. |
| 22576 | 10 | No | Invalid failover_mode value of %d was specified for \[%s\].\[%s\].\[%s\], setting to 0 \[immediate\]. |
| 22578 | 16 | No | Cannot change publication "%s" to disallow use_partition_groups because it contains one or more logical record relationships. When using logical record relationships the publication must set the @use_partition_groups property to 'true'. |
| 22579 | 16 | No | The subscription to publication '%s' was not found but a shared agent does exist. To specify a subscription to a publication that is replicated via a shared agent specify '%s' for the publication name. |
| 22580 | 16 | No | Cannot publish database '%s' because it is marked as published on a different server. Before attempting to publish this database, execute sp_replicationdboption, specifying a value of FALSE for 'publish' and 'merge publish'. |
| 22581 | 16 | No | Article '%s' cannot be added or modified in publication '%s'. The replication of FILESTREAM columns is not supported for publications that have a 'sync_mode' of 1 (character mode). Specify a 'sync_mode' of 0 (native mode) for the publication by using sp_addmergepublication or sp_changemergepublication, or partition the article vertically so that the FILESTREAM column is not replicated. |
| 22582 | 16 | No | Article '%s' cannot be added or modified in publication '%s'. The replication of FILESTREAM columns is not supported for publications that have a 'publication_compatibility_level' of less than "90RTM" (SQL Server 2005). Specify a 'publication_compatibility_level' greater than or equal to "90RTM" by using sp_addmergepublication or sp_changemergepublication, or partition the article vertically so that the FILESTREAM column is not replicated. |
| 22583 | 16 | No | Article '%s' cannot be added or modified in publication '%s'. The replication of FILESTREAM columns is not supported for articles that have a 'schema_option' set to 0x20000000. This converts large object data types to data types that are supported on earlier versions of Microsoft SQL Server. Remove this 'schema_option' setting by using sp_addmergepublication or sp_changemergepublication, or partition the article vertically so that the FILESTREAM column is not replicated. |
| 22584 | 10 | No | Warning: Values of some of the flags specified in the 'schema_option' property are not compatible with the publication's compatibility level. The modified schema_option value of '%s' will be used instead. |
| 22585 | 10 | No | The schema option to script out the FILESTREAM attribute on varbinary(max) columns has been enabled for article '%s'. Enabling this option after the article is created can cause replication to fail when the data in a FILESTREAM column exceeds 2GB and there is a conflict during replication. If you want FILESTREAM data to be replicated, drop and re-create the article, and specify the appropriate schema option when you re-create the article. |
| 22586 | 16 | No | Column '%s' cannot be added or modified in article '%s' of publication '%s'. The DDL operatoin on hierarchyid and FILESTREAM columns is not supported for publications that have a 'sync_mode' of 1 (character mode) or with a lower than 90RTM backward compatibility level. |
| 22587 | 16 | No | Non-SQL Server Publishers and Subscribers are supported only on Windows. The platform detected is %s. |
| 22588 | 16 | No | Publications on non-Windows platforms cannot support updateable subscriptions. The platform detected is %s. The values of @allow_sync_tran and @allow_queued_tran must be 'false' or NULL. |
| 22701 | 16 | No | Cannot run this stored procedure as Change Feed feature is not enabled. |
| 22702 | 16 | No | Caller is not authorized to initiate the requested action. only user with control database permission can perform this operation. |
| 22703 | 16 | No | Could not enable Change Feed for database '%s'. Change Feed is not supported on system databases, or on a distribution database. |
| 22704 | 16 | No | Could not enable Change Feed for database '%s'. Change Feed can not be enabled on a DB with Change Data Capture or transactional replication publishing. |
| 22705 | 16 | No | Could not enable Change Feed for database '%s' as it is already enabled. |
| 22706 | 16 | No | Change Feed is not enabled on database '%s'. |
| 22707 | 16 | No | The specified database scoped credential name can't be found. It must have the identity of 'SHARED ACCESS SIGNATURE' |
| 22708 | 16 | No | The specified database scoped credential name does not match the landing zone URL path |
| 22709 | 16 | No | Parameter '%s' cannot be null or empty. Specify a value for the named parameter and retry the operation. |
| 22710 | 16 | No | Could not update the metadata. The failure occurred when executing the command '%s'. The error/state returned was %d/%d: '%s'. Use the action and error to determine the cause of the failure and resubmit the request. |
| 22711 | 16 | No | The specified table group can't be found in the current database. |
| 22712 | 16 | No | The application lock request '%s' needed to modify Change Feed metadata was not granted. The value returned from the request was %d: -1 = timeout; -2 = canceled; -3 = deadlock victim; -999 validation or other call error. Examine the error cause and resubmit the request. |
| 22713 | 16 | No | The value for parameter @maxtrans specified must be greater than 0. |
| 22714 | 16 | No | Tables contained in the changefeed schema cannot be enabled for Change Feed. |
| 22715 | 16 | No | Enabling Change Feed for a temporal history table '%ls' is not allowed. |
| 22716 | 16 | No | Can not enable Change Feed on table '%s' or add ColumnSet column to it because Change Feed does not support ColumnSet. |
| 22717 | 16 | No | A memory-optimized table cannot be enabled for Change Feed. |
| 22718 | 16 | No | Change Feed is already enabled on source table '%s.%s' with link table id '%s'. A table can only be enabled once among all table groups. |
| 22719 | 16 | No | Enabling Change Feed on a table with primary key on computed column is not allowed. |
| 22720 | 16 | No | Enabling Change Feed on a table without primary key is not allowed. |
| 22721 | 16 | No | Can not enable Change Feed on a table where primary key uses columns of the following types: user-defined types, geometry, geography, hierarchyid, sql_variant or timestamp |
| 22722 | 16 | No | Cannot enable Change Feed on a table with encrypted columns. |
| 22723 | 16 | No | Cannot find an object ID for the Change Feed system table '%s'. Verify that the system table exists and is accessible by querying it directly. If it does not exist, drop and reconfigure Change Feed. |
| 22724 | 16 | No | Error occurred while exporting initial snapshot to landing zone. |
| 22725 | 16 | No | Enabling Change Feed for a ledger history table '%ls' is not allowed. |
| 22726 | 16 | No | Could not allocate memory for Change Feed operation. Verify that SQL Server has sufficient memory for all operations. Check the memory settings on the server and examine memory usage to see if another application is excessively consuming memory. |
| 22727 | 16 | No | Failed to load landing zone library (%s) with error %d. Verify that SQL Server is installed properly. |
| 22728 | 16 | No | Could not disable table because it is no longer enabled for table group. |
| 22729 | 16 | No | Could not remove the metadata. The failure occurred when executing the command '%s'. The error/state returned was %d/%d: '%s'. Use the action and error to determine the cause of the failure and resubmit the request. |
| 22730 | 16 | No | Could not disable Change Feed for database '%s' as it is already disabled. |
| 22731 | 16 | No | Error occurred while publish incremental data to landing zone. |
| 22732 | 16 | No | Invalid change feed table state. |
| 22733 | 16 | No | Change Feed snapshot was cancelled. |
| 22734 | 16 | No | Error occurred while process Change Feed on database %d. |
| 22735 | 16 | No | Error occurred while update Change Feed table status for table %ld. |
| 22736 | 16 | No | Error occurred while interacting with landing zone. |
| 22737 | 16 | No | Can not enable Change Feed on a table where the column data type are user-defined types, geometry, geography, hierarchyid, sql_variant or timestamp |
| 22738 | 16 | No | Can not enable Change Feed on a table with computed columns. |
| 22739 | 16 | No | Error occurred while trying to retrieve storage info from Synapse Gateway Service with result code %d. |
| 22740 | 16 | No | Parameter '%s' is invalid. Specify a valid value for the named parameter and retry the operation. |
| 22741 | 16 | No | Landing Zone parameter is not valid on Azure Database. |
| 22742 | 16 | No | Schema changes on table '%s' are not supported because it is enabled for change feed. |
| 22743 | 16 | No | There is an issue with cleaning up metadata of some of the table groups of this database. Retry by dropping the table groups first and then disable Change Feed on the database. |
| 22744 | 16 | No | Cannot disable primary key index "%.\*ls" on table "%.\*ls" because the table has change feed enabled on it. |
| 22745 | 16 | No | Cannot alter the table '%.\*ls' because it has change feed enabled on it. |
| 22746 | 10 | No | Change Feed |
| 22747 | 16 | No | Another connection with session ID %I64d is already running 'sp_replcmds' for Change Feed in the current database. |
| 22748 | 16 | No | Change Feed task is being aborted. |
| 22749 | 16 | No | Cannot rename the table because it is being used for Change Feed |
| 22750 | 16 | No | ALTER TABLE SWITCH statement failed because the partitioned destination table is enabled for Change Feed. |
| 22751 | 16 | No | ALTER TABLE SWITCH statement failed because the partitioned source table is enabled for Change Feed. |
| 22752 | 16 | No | Snapshot DATA_EXPORT query failed. |
| 22753 | 16 | No | The number of tables enabled for a Change Feed table group can not exceed %d. Current number of table enabled: %d. |
| 22754 | 16 | No | Aborting Synapse Link Capture task for this database timed out. Retry this operation later. |
| 22755 | 16 | No | Change Feed is not supported on Free, Basic or Standard tier Single Database (S0,S1,S2) and Database in Elastic pool with max eDTUs \< 100 or max vCore \< 1. Please upgrade to a higher Service Objective. |
| 22756 | 16 | No | The elastic pool cannot lower its service tier with database max eDTUs \< 100 or max vCore \< 1 since one or more of its database(s) use Change Feed. |
| 22757 | 16 | No | The database cannot lower its service tier to Standard(S0,S1,S2), Basic or Free, or move to elastic pool with database max eDTUs \< 100 or max vCore \< 1 since it has Change Feed enabled. |
| 22758 | 16 | No | Aborting Synapse Link Commit task for table group '%s' timed out. Retry this operation later. |
| 22759 | 16 | No | Aborting Synapse Link Snapshot task for table %ld timed out. Retry this operation later. |
| 22760 | 16 | No | Aborting Synapse Link Publish task for partition %ld timed out. Retry this operation later. |
| 22761 | 16 | No | Failed to cleanup previous change feed setup. Please try the operation again. If the problem persists contact Microsoft Azure Customer Support. |
| 22762 | 16 | No | Cannot enable '%S_MSG' on '%s' because it is being used for '%S_MSG'. |
| 22763 | 10 | No | Replication: Distribution |
| 22764 | 10 | No | Replication: transactional or snapshot publication |
| 22765 | 10 | No | Replication: merge publication |
| 22766 | 16 | No | Could not enable Change Feed for database '%s'. Change Feed can not be enabled on a DB which is mirrored. |
| 22767 | 16 | No | The value for parameter @pollinterval specified must be equal to or greater than 5. |
| 22768 | 16 | No | Could not update change_feed_settings, the table has not been created yet. |
| 22769 | 16 | No | Could not find table group or table with the provided table_group_id/table_id or source_schema/source_name |
| 22770 | 16 | No | Could not return results, please provide table_group_id and table_id or source_schema and source_name |
| 22771 | 16 | No | Error occurred while publishing data via CSVOutputter |
| 22772 | 10 | No | environment |
| 22773 | 10 | No | database |
| 22774 | 10 | No | table group |
| 22775 | 17 | No | Failed to get lock to complete in-memory objects cleanup with result: %d |
| 22776 | 16 | No | %S_MSG (%s) can't be found in the current configuration. |
| 22777 | 10 | No | Failure reported in %S_MSG processing |
| 22778 | 10 | No | Failed to set database state to CLEANUP while trying to remove in-memory database objects |
| 22779 | 10 | No | Failed to enqueue partition %d with result %d |
| 22780 | 10 | No | Failed to enqueue work item with result %d |
| 22781 | 10 | No | Change feed failed on processing transaction log |
| 22782 | 10 | No | Failed to output batch data for table %d in partition %d |
| 22783 | 10 | No | Failed to process commit work item |
| 22784 | 10 | No | Change feed schema upgrade failed |
| 22785 | 10 | No | Change batch was reset from unexpected state |
| 22786 | 16 | No | Synapse workspace FQDN is not in the list of Outbound Firewall Rules on the server. Please add this to the list of Outbound Firewall Rules on your server and retry the operation. |
| 22787 | 16 | No | Change feed table group limit of %d groups exceeded |
| 22788 | 16 | No | Could not enable Change Feed for database '%s'. Change Feed can not be enabled on a DB with delayed durability set. |
| 22801 | 16 | No | Starting the Change Data Capture Collection Agent job. To report on the progress of the operation, query the sys.dm_cdc_log_scan_sessions dynamic management view. |
| 22802 | 16 | No | Starting the Change Data Capture Cleanup Agent job using low watermark %s. |
| 22803 | 16 | No | Change Data Capture has scanned the log from LSN{%s} to LSN{%s}, %d transactions with %d commands have been extracted. To report on the progress of the operation, query the sys.dm_cdc_log_scan_sessions dynamic management view. |
| 22804 | 16 | No | Change Data Capture cannot proceed with the job-related action because transactional replication is enabled on database %s but Distributor information cannot be retrieved to determine the state of the logreader agent. Make the Distributor database available, or disable distribution. |
| 22805 | 10 | No | For more information, query the sys.dm_cdc_errors dynamic management view. |
| 22806 | 16 | No | The originator ID '%s' is not valid. You must specify a non-zero ID that has never been used in the topology. |
| 22807 | 16 | No | The publication property '%s' cannot be modified because the peer-to-peer publication '%s' is not enabled for conflict detection. To enable the publication for conflict detection, use sp_configure_peerconflictdetection. |
| 22808 | 16 | No | Cannot execute procedure '%s'. Publication '%s' must be enabled for peer-to-peer replication before you execute this procedure. To enable the publication for peer-to-peer replication, use sp_changepublication. |
| 22809 | 10 | No | The existing conflict table '%s' was dropped. |
| 22810 | 16 | No | The @action parameter value is not valid. Valid values are 'enable' and 'disable'. |
| 22811 | 16 | No | The roundtrip time-out must be greater than 0. |
| 22812 | 10 | No | The roundtrip '%s' finished with timeout: %d seconds. |
| 22813 | 10 | No | The topology contains peer node versions that do not support conflict detection. To use conflict detection, ensure that all nodes in the topology are SQL Server 2008 or later versions. |
| 22814 | 10 | No | The topology contains a duplicate originator ID. To use conflict detection, the originator ID must be unique across the topology. |
| 22815 | 10 | No | A conflict of type '%s' was detected at peer %d between peer %d (incoming), transaction id %s and peer %d (on disk), transaction id %s for Table '%s' with Primary Key(s): %s Current Version '%s', Pre-Version '%s' and Post-Version '%s' |
| 22816 | 16 | No | The qualified table name '%s' is too long to be enabled for peer-to-peer conflict detection. |
| 22817 | 10 | No | %s has %s. |
| 22818 | 10 | No | A delete-delete conflict was detected and resolved. The row could not be deleted from the peer since the row does not exist. The incoming delete was skipped. |
| 22819 | 10 | No | A delete-update conflict between peer %d (incoming) and peer %d (on disk) was detected and could not be resolved automatically. The incoming delete was skipped by peer %d. The conflict has to be resolved manually to guarantee data convergence between the peers. For steps on how to resolve the conflict refer to BOL. |
| 22820 | 10 | No | A delete-update conflict between peer %d (incoming) and peer %d (on disk) was detected and resolved. The incoming delete was applied to peer %d. |
| 22821 | 10 | No | An update-update conflict between peer %d (incoming) and peer %d (on disk) was detected and resolved. The incoming update was skipped by peer %d. |
| 22822 | 10 | No | An update-update conflict between peer %d (incoming) and peer %d (on disk) was detected and resolved. The incoming update was applied to peer %d. |
| 22823 | 10 | No | An update-delete conflict was detected and unresolved. The row could not be updated since the row does not exist. The incoming update was skipped. Check the priority of the destination peer and run data validation to ensure the delete conflict did not result in data non-convergence. |
| 22824 | 10 | No | An insert-insert conflict between peer %d (incoming) and peer %d (on disk) was detected and resolved. The incoming insert was skipped by peer %d. |
| 22825 | 10 | No | An insert-insert conflict between peer %d (incoming) and peer %d (on disk) was detected and resolved. The incoming insert was applied to peer %d. |
| 22827 | 16 | No | Peer-to-peer conflict detection alert |
| 22828 | 16 | No | The publication '%s' was already %s for peer-to-peer conflict detection. |
| 22829 | 16 | No | The command %s failed. The values specified for the @ins_cmd, @del_cmd or @upd_cmd cannot be appended with schema name %s within the size limit %d. |
| 22830 | 16 | No | Could not update the metadata that indicates database %s is enabled for Change Data Capture. The failure occurred when executing the command '%s'. The error returned was %d: '%s'. Use the action and error to determine the cause of the failure and resubmit the request. |
| 22831 | 16 | No | Could not update the metadata that indicates database %s is not enabled for Change Data Capture. The failure occurred when executing the command '%s'. The error returned was %d: '%s'. Use the action and error to determine the cause of the failure and resubmit the request. |
| 22832 | 16 | No | Could not update the metadata that indicates table %s is enabled for Change Data Capture. The failure occurred when executing the command '%s'. The error returned was %d: '%s'. Use the action and error to determine the cause of the failure and resubmit the request. |
| 22833 | 16 | No | Could not update the metadata that indicates table %s is not enabled for Change Data Capture. The failure occurred when executing the command '%s'. The error returned was %d: '%s'. Use the action and error to determine the cause of the failure and resubmit the request. |
| 22834 | 16 | No | Could not modify the the verbose logging status for table %s. The failure occurred when executing the command '%s'. The error returned was %d: '%s'. Use the action and error to determine the cause of the failure and resubmit the request. |
| 22835 | 16 | No | Could not update the metadata for database %s to indicate that a Change Data Capture job has been dropped. The failure occurred when executing the command '%s'. The error returned was %d: '%s'. Use the action and error to determine the cause of the failure and resubmit the request. |
| 22836 | 16 | No | Could not update the metadata for database %s to indicate that a Change Data Capture job has been added. The failure occurred when executing the command '%s'. The error returned was %d: '%s'. Use the action and error to determine the cause of the failure and resubmit the request. |
| 22837 | 16 | No | Could not delete table entries or drop objects associated with capture instance '%s'. The failure occurred when executing the command '%s'. The error returned was %d: '%s'. Use the action and error to determine the cause of the failure and resubmit the request. |
| 22838 | 16 | No | All columns of a CDC unique index must be defined as NOT NULL. Index '%s' selected as the CDC unique index for source table '%s.%s' does not meet this requirement. Define all columns of the selected index as NOT NULL or select another unique index as the CDC index and resubmit the request. |
| 22840 | 16 | No | The application lock request '%s' needed to modify Change Data Capture metadata was not granted. The value returned from the request was %d: -1 = timeout; -2 = canceled; -3 = deadlock victim; -999 validation or other call error. Examine the error cause and resubmit the request. |
| 22841 | 16 | No | Could not upgrade the metadata for database '%s' that is enabled for Change Data Capture. The failure occurred when executing the action '%s'. The error returned was %d: '%s'. Use the action and error to determine the cause of the failure and resubmit the request. |
| 22842 | 16 | No | ALTER TABLE SWITCH statement failed because the partitioned destination table is enabled for Change Data Capture and does not have @allow_partition_switch set to 1. |
| 22843 | 16 | No | ALTER TABLE SWITCH statement failed because the partitioned source table is enabled for Change Data Capture and does not have @allow_partition_switch set to 1. |
| 22844 | 16 | No | The '%s' option must be either 1 or 0. |
| 22845 | 16 | No | Cannot enable change data capture in this edition of SQL Server. |
| 22850 | 16 | No | The threshold value specified for the Change Data Capture cleanup process must be greater than 0. When creating or modifying the cleanup job, specify a positive threshold value. If this error is encountered when executing the sys.sp_cdc_cleanup_change_table stored procedure, reset the threshold value associated with the job to a non-negative value by using the sp_cdc_change_job stored procedure. |
| 22851 | 16 | No | Could not update cdc.change_tables to indicate a change in the low water mark for database %s. |
| 22852 | 10 | No | Could not delete change table entries made obsolete by a change in one or more low water marks for capture instances of database %s. The failure occurred when executing the command '%s'. The error returned was %d: '%s'. Use the action and error to determine the cause of the failure and resubmit the request. |
| 22853 | 10 | No | Could not delete obsolete entries in the cdc.lsn_time_mapping table for database %s. The failure occurred when executing the command '%s'. The error returned was %d: '%s'. Use the action and error to determine the cause of the failure and resubmit the request. |
| 22854 | 16 | No | Can not enable Change Data Capture on table '%s' or add ColumnSet column to it because CDC does not support ColumnSet. |
| 22855 | 10 | No | Warning: The @allow_partition_switch parameter is set to 1. Change data capture will not track changes introduced in the table resulting from a partition switch which will cause data inconsistency when changes are consumed. Refer to books online for more information about partition switching behavior when using Change Data Capture. |
| 22856 | 10 | No | Warning: The @allow_partition_switch parameter is set to 0. ALTER TABLE ... SWITCH PARTITION statement will be disallowed on this partitioned table. Refer to books online for more information about partition switching behavior when using Change Data Capture. |
| 22857 | 10 | No | Warning: The @allow_partition_switch parameter must be 1 for tables that are not partitioned. The explicit setting of the parameter to 0 was ignored. Refer to books online for more information about partition switching behavior when using Change Data Capture. |
| 22858 | 16 | No | Unable to add entries to the Change Data Capture LSN time mapping table to reflect dml changes applied to the tracked tables. Refer to previous errors in the current session to identify the cause and correct any associated problems. |
| 22859 | 16 | No | Log Scan process failed in processing log records. Refer to previous errors in the current session to identify the cause and correct any associated problems. |
| 22860 | 16 | No | Log scan process failed in processing a ddl log record. Refer to previous errors in the current session to identify the cause and correct any associated problems. |
| 22861 | 10 | No | Unable to upgrade database '%s'. Error '%d' was raised: '%s'. Use the reported error to determine the cause of the failure and then execute sys.sp_cdc_vupgrade in the database context to rerun upgrade. |
| 22862 | 16 | No | The database snapshot '%s' does not exist. Correct the parameter value and resubmit the request. |
| 22863 | 16 | No | Failed to insert rows into Change Data Capture change tables. Refer to previous errors in the current session to identify the cause and correct any associated problems. |
| 22864 | 16 | No | The call to sp_MScdc_capture_job by the Capture Job for database '%s' failed. Look at previous errors for the cause of the failure. |
| 22865 | 16 | No | The number of columns in the index '%s', used as the unique row identifier to support net changes for table '%s'.'%s', exceeds the limit of 14 columns. Set the parameter @supports_net_changes to 0 or use the @index_name parameter to identify a unique index with fewer than 15 columns as the unique row identifier and resubmit the request. |
| 22866 | 10 | No | The value returned by %S_MSG is %I64d. |
| 22867 | 10 | No | Total rows deleted: %I64u. |
| 22868 | 10 | No | Cleanup Watermark = %I64u |
| 22869 | 10 | No | Internal Change Tracking table name : %s |
| 22870 | 10 | No | Deleted %I64u rows from %s |
| 22871 | 16 | No | Change Data Capture is not supported on Free, Basic or Standard tier Single Database (S0,S1,S2) and Database in Elastic pool with max eDTUs \< 100 or max vCore \< 1. Please upgrade to a higher Service Objective. |
| 22872 | 16 | No | The elastic pool cannot lower its service tier with database max eDTUs \< 100 or max vCore \< 1 since one or more of its database(s) use Change Data Capture (CDC). |
| 22873 | 16 | No | The database cannot lower its service tier to Standard(S0,S1,S2), Basic or Free, or move to elastic pool with database max eDTUs \< 100 or max vCore \< 1 since it has Change Data Capture (CDC) enabled. |
| 22874 | 16 | No | Cannot set @enable_extended_ddl_handling parameter to 1 as this feature is not enabled. |
| 22875 | 16 | No | Capture instance name '%s' exceeds the length limit of 78 characters when @enable_extended_ddl_handling is enabled. Specify a name that satisfies the length constraint. |
| 22876 | 16 | No | One capture instance already exist with DDL handling enabled for source table '%s.%s'. A table can have only one capture instance with DDL handling enabled. If the current tracking options are not appropriate, disable change tracking for the obsolete instance by using sys.sp_cdc_disable_table and retry the operation. |
| 22877 | 16 | No | All columns are not captured for source table '%s.%s' when using DDL handling feature. DDL handling feature is only supported when all columns are captured in CT table. If user wants to use DDL handling feature then enable CDC on a table without the parameter @captured_column_list or pass NULL for this parameter, this is will by default capture all columns. |
| 22878 | 16 | No | The @p2p_conflictdetection_policy parameter value is not valid. Valid values are 'originatorid' and 'lastwriter'. |
| 22879 | 16 | No | Peer-to-peer publications with last writer conflict detection policy only supports CALL or SCALL command type for @upd_cmd. Change the value for parameter '@upd_cmd'. |
| 22880 | 16 | No | An insert-insert conflict between peer %d (incoming) with commit datetime value of '%s' and peer %d (on disk) with commit datetime value of '%s' was detected and resolved. The incoming insert was applied to peer %d. |
| 22881 | 16 | No | A conflict of type '%s' was detected at peer %d between peer %d (incoming), transaction id %s, commit datetime '%s' and peer %d (on disk), transaction id %s, commit datetime '%s' for Table '%s' with Primary Key(s): %s Current Version '%s', Pre-Version '%s' and Post-Version '%s' |
| 22882 | 10 | No | An update-update conflict between peer %d (incoming) with commit dateime value of '%s' and peer %d (on disk) with commit datetime value of '%s' was detected and resolved. The incoming update was skipped by peer %d. |
| 22883 | 10 | No | An update-update conflict between peer %d (incoming) with commit datetime value of '%s' and peer %d (on disk) with commit datetime value of '%s' was detected and resolved. The incoming update was applied to peer %d. |
| 22884 | 10 | No | An delete-update conflict between peer %d (incoming) with commit datetime value of '%s' and peer %d (on disk) with commit datetime value of '%s'was detected and resolved. The incoming delete was skipped by peer %d. |
| 22885 | 16 | No | An insert-insert conflict between peer %d (incoming) with commit datetime value of '%s' and peer %d (on disk) with commit datetime value of '%s' was detected and resolved. The incoming insert was skipped by peer %d. |
| 22886 | 10 | No | An update-delete conflict was detected. The row could not be updated since the row does not exist. The incoming update was skipped. |
| 22887 | 16 | No | Could not enable Change Data Capture for database '%s'. Change Data Capture cannot be enabled on a DB with Change Feed enabled. |
| 22888 | 16 | No | Could not alter captured column of a CDC tracked table with character/binary/unicode as target data type with ansi warnings turned off. |
| 22889 | 10 | No | Warning: Unable to get database version for the subscription database '%s'. The sp_replmonitorsubscriptionpendingcmds may report incorrect number of pending commands for P2P replication. |
| 22891 | 16 | No | Could not enable '%S_MSG' for database '%s'. '%S_MSG' cannot be enabled on a DB with delayed durability set. |
| 22892 | 16 | No | Could not enable delayed durability on DB. Delayed durability cannot be enabled on a DB while '%S_MSG' is enabled. |
| 22901 | 16 | No | The database '%s' is not enabled for Change Data Capture. Ensure that the correct database context is set and retry the operation. To report on the databases enabled for Change Data Capture, query the is_cdc_enabled column in the sys.databases catalog view. |
| 22902 | 16 | No | Caller is not authorized to initiate the requested action. %s privileges are required. |
| 22903 | 16 | No | Another connection with session ID %I64d is already running 'sp_replcmds' for Change Data Capture in the current database. |
| 22904 | 16 | No | Caller is not authorized to initiate the requested action. DBO privileges are required. |
| 22905 | 10 | No | Database '%s' is already enabled for Change Data Capture. Ensure that the correct database context is set, and retry the operation. To report on the databases enabled for Change Data Capture, query the is_cdc_enabled column in the sys.databases catalog view. |
| 22906 | 16 | No | The database '%s' cannot be enabled for Change Data Capture because a database user named 'cdc' or a schema named 'cdc' already exists in the current database. These objects are required exclusively by Change Data Capture. Drop or rename the user or schema and retry the operation. |
| 22907 | 16 | No | Parameter @role_name cannot be empty. Specify a value for @role_name and retry the operation. Supply null as the value if no role is to be used to gate access to captured change data. |
| 22908 | 16 | No | Could not create the Change Data Capture objects in database '%s'. Refer to previous errors in the current session to identify the cause and correct any associated problems. |
| 22909 | 16 | No | Failed to cleanup the cdc.lsn_time_mapping table in database '%s' when the last database table enabled for Change Data Capture was disabled. Refer to previous errors in the current session to identify the cause and correct any associated problems. |
| 22910 | 16 | No | The cleanup request for database '%s' failed. The database is not enabled for Change Data Capture. |
| 22911 | 16 | No | The capture job cannot be used by Change Data Capture to extract changes from the log when transactional replication is also enabled on the same database. When Change Data Capture and transactional replication are both enabled on a database, use the logreader agent to extract the log changes. |
| 22913 | 16 | No | Could not drop the Change Data Capture objects in database '%s'. Refer to previous errors in the current session to identify the cause and correct any associated problems. |
| 22914 | 16 | No | Only members of the sysadmin or db_owner or db_ddladmin roles can perform this operation when Change Data Capture is enabled for a database. |
| 22916 | 16 | No | Could not grant SELECT permission for the change enumeration functions for capture instance '%s' and source table '%s.%s' for the specified role. Refer to previous errors in the current session to identify the cause and correct any associated problems. |
| 22918 | 16 | No | One or more columns in the list of included columns was not a captured column of the change table %s. |
| 22919 | 16 | No | One or more columns in the list of columns needing update flags was not a captured column of the change table %s. |
| 22920 | 16 | No | The named capture instance %s does not exist for database %s. |
| 22921 | 16 | No | Unable to generate scripts for all capture instances that the caller is authorized to access. To generate all such scripts, the parameters @column_list and @update_flag_list must both be null or empty.' |
| 22923 | 16 | No | Could not compute the new low endpoint for database '%s' from retention %d. Refer to previous errors in the current session to identify the cause and correct any associated problems. |
| 22924 | 16 | No | Could not clean up change tables for database '%s'. A failure occurred when attempting to clean up the database change tables based upon the current retention time. Refer to previous errors in the current session to identify the cause and correct any associated problems. |
| 22925 | 16 | No | The number of columns captured by capture instance '%s' exceeds the maximum allowed number: %d. Use the @captured_columns_list parameter to specify a subset of the columns less than or equal to the maximum allowed and resubmit the request. |
| 22926 | 16 | No | Could not create a capture instance because the capture instance name '%s' already exists in the current database. Specify an explicit unique name for the parameter @capture_instance. |
| 22927 | 16 | No | Capture instance name '%s' exceeds the length limit of 100 characters. Specify a name that satisfies the length constraint. |
| 22928 | 16 | No | Index name '%s' is not an index for table '%s.%s'. Specify a valid index name for the table. |
| 22929 | 16 | No | Index '%s' must be either a primary key or a unique index for table '%s.%s'. Specify an index that meets at least one of these requirements. |
| 22930 | 16 | No | Could not locate '%s' as a column of source table '%s.%s'. Specify a valid column name. |
| 22931 | 16 | No | Source table '%s.%s' does not exist in the current database. Ensure that the correct database context is set. Specify a valid schema and table name for the database. |
| 22932 | 16 | No | Capture instance name '%s' is invalid. Specify a valid name. See the topic 'Identifiers' in SQL Server Books Online for object name rules. |
| 22933 | 16 | No | Could not drop change table objects for capture instance '%s'. The failure occurred when executing the command '%s'. The error returned was %d: '%s'. Use the action and error to determine the cause of the failure and resubmit the request. |
| 22938 | 16 | No | Role name '%s' is invalid. Specify a valid name. See the topic 'Identifiers' in SQL Server Books Online for object name rules. |
| 22939 | 16 | No | The parameter @supports_net_changes is set to 1, but the source table does not have a primary key defined and no alternate unique index has been specified. |
| 22940 | 16 | No | Could not remove DDL history entries in the Change Data Capture metadata for capture instance '%s' and source table '%s.%s'. Refer to previous errors in the current session to identify the cause and correct any associated problems. |
| 22941 | 16 | No | Could not retrieve column information for index '%s' of source table '%s.%s'. Refer to previous errors in the current session to identify the cause and correct any associated problems. |
| 22942 | 16 | No | Columns specified in the captured column list could not be mapped to columns in source table '%s.%s'. Verify that the columns specified in the parameter @captured_column_list are delimited properly and match columns in the source table. |
| 22943 | 16 | No | Columns used to uniquely identify a row for net change tracking must be included in the list of captured columns. Add either the primary key columns of the source table, or the columns defined for the index specified in the parameter @index_name to the list of captured columns and retry the operation. |
| 22944 | 16 | No | Could not create the specified database role '%s' for gating access to change table '%s.%s'. Refer to previous errors in the current session to identify the cause and correct any associated problems. |
| 22945 | 16 | No | Could not add column information to the cdc.index_columns system table for the specified index for source table '%s.%s. Refer to previous errors in the current session to identify the cause and correct any associated problems. |
| 22946 | 16 | No | Could not add column information to the cdc.captured_columns system table for source table '%s.%s'. Refer to previous errors in the current session to identify the cause and correct any associated problems. |
| 22947 | 16 | No | Could not create the change table for capture instance '%s' and source table '%s.%s'. Refer to previous errors in the current session to identify the cause and correct any associated problems. |
| 22948 | 16 | No | Could not create the change enumeration functions for capture instance '%s' and source table '%s.%s'. Refer to previous errors in the current session to identify the cause and correct any associated problems. |
| 22949 | 16 | No | Could not update the Change Data Capture metadata for capture instance '%s' and source table '%s.%s'. Refer to previous errors in the current session to identify the cause and correct any associated problems. |
| 22950 | 16 | No | Could not remove index column entries in the Change Data Capture metadata for capture instance '%s' and source table '%s.%s'. Refer to previous errors in the current session to identify the cause and correct any associated problems. |
| 22951 | 16 | No | Could not remove captured column entries in the Change Data Capture metadata for capture instance '%s' and source table '%s.%s'. Refer to previous errors in the current session to identify the cause and correct any associated problems. |
| 22952 | 16 | No | Could not drop Change Data Capture objects created for capture instance '%s' and source table '%s.%s'. Refer to previous errors in the current session to identify the cause and correct any associated problems. |
| 22953 | 16 | No | Could not remove Change Data Capture metadata for capture instance '%s' and source table '%s.%s'. Refer to previous errors in the current session to identify the cause and correct any associated problems. |
| 22954 | 16 | No | Could not cleanup change tables for capture instance '%s' using low end point %s. Refer to previous errors in the current session to identify the cause and correct any associated problems. |
| 22955 | 16 | No | Could not obtain the maximum LSN for the database from function 'sys.fn_cdc_get_max_lsn'. Refer to previous errors in the current session to identify the cause and correct any associated problems. |
| 22956 | 16 | No | Could not obtain the minimum LSN of the change table associated with capture instance '%s' from function 'sys.fn_cdc_get_min_lsn'. Refer to previous errors in the current session to identify the cause and correct any associated problems. |
| 22957 | 16 | No | LSN %s, specified as the new low end point for the change table associated with capture instance '%s', is not within the Change Data Capture timeline \[%s, %s\]. |
| 22958 | 16 | No | Could not create the function to query for all changes for capture instance '%s'. Refer to previous errors in the current session to identify the cause and correct any associated problems. |
| 22959 | 16 | No | Could not create the function to query for net changes for capture instance '%s'. Refer to previous errors in the current session to identify the cause and correct any associated problems. |
| 22960 | 16 | No | Change data capture instance '%s' has not been enabled for the source table '%s.%s'. Use sys.sp_cdc_help_change_data_capture to verify the capture instance name and retry the operation. |
| 22961 | 16 | No | Could not create a nonclustered index to support net change tracking for capture instance '%s' and source table '%s.%s'. Refer to previous errors in the current session to identify the cause and correct any associated problems. |
| 22962 | 16 | No | Two capture instances already exist for source table '%s.%s'. A table can have at most two capture instances. If the current tracking options are not appropriate, disable change tracking for the obsolete instance by using sys.sp_cdc_disable_table and retry the operation. |
| 22963 | 16 | No | Parameter '%s' cannot be null or empty. Specify a value for the named parameter and retry the operation. |
| 22964 | 16 | No | LSN %s, specified as the new low end point for change table cleanup must represent the start_lsn value of a current entry in the cdc.lsn_time_mapping table. Choose an LSN value that satisfies this requirement. |
| 22965 | 16 | No | A quoted column in the column list is not properly terminated. Verify that columns are correctly delimited and retry the operation. For more information, see 'Delimited Identifiers' in Books Online. |
| 22966 | 16 | No | Could not create table dbo.systranschemas in database '%s'. Refer to previous errors in the current session to identify the cause and correct any associated problems. |
| 22967 | 16 | No | Could not create a clustered index for table dbo.systranschemas in database '%s'. Refer to previous errors in the current session to identify the cause and correct any associated problems. |
| 22968 | 16 | No | Could not create DDL trigger '%s' when enabling Change Data Capture for database '%s'. Additional messages in the SQL Server error log and operating system error log may provide more detail. |
| 22969 | 10 | No | Update mask evaluation will be disabled in net_changes_function because the CLR configuration option is disabled. |
| 22970 | 16 | No | The value for parameter @maxscans specified for the Change Data Capture job must be greater than 0. |
| 22971 | 16 | No | Could not allocate memory for the log reader history cache. Verify that SQL Server has sufficient memory for all operations. Check the physical and virtual settings on the server and examine memory usage to see if another application is excessively consuming memory. |
| 22972 | 16 | No | When calling stored procedure \[sys\].sp_cdc_help_change_data capture, if either @source_schema or @source_name is non-null and non-empty, the other parameter must also be non-null and non-empty. |
| 22973 | 16 | No | The specified filegroup '%s' is not a valid filegroup for database '%s'. Specify a valid existing filegroup or create the named filegroup, and retry the operation. |
| 22974 | 16 | No | Tables contained in the cdc schema cannot be enabled for Change Data Capture. |
| 22975 | 16 | No | Source table '%s' contains one of the following reserved column names: __$start_lsn, __$end_lsn, __$seqval, __$operation, and __$update_mask. To enable Change Data Capture for this table, specify a captured column list and ensure that these columns are excluded from the list. |
| 22976 | 16 | No | Could not alter column '%s' of change table '%s' in response to a data type change in the corresponding column of the source table '%s'. The Change Data Capture meta data for source table '%s' no longer accurately reflects the source table. Refer to previous errors in the current session to identify the cause and correct any associated problems. |
| 22977 | 16 | No | Unable to update DDL history information to reflect columns changes applied to the tracked table associated with change table '%s'. Refer to previous errors in the current session to identify the cause and correct any associated problems. |
| 22978 | 16 | No | Could not update the cdc.captured_columns entry for column '%s' of change table '%s' to reflect a data type change in the corresponding column of the source table '%s'. Change Data Capture column meta data for table '%s' no longer accurately reflects the source table. Refer to previous errors in the current session to identify the cause and correct any associated problems. |
| 22979 | 16 | No | The unique index '%s' on table '%s' is used by Change Data Capture. The constraint using this index cannot be dropped or disabled. |
| 22980 | 16 | No | The unique index '%s' on table '%s.%s' is disabled and cannot be used as a unique index by Change Data Capture. Enable the index. |
| 22981 | 16 | No | Object does not exist or access is denied. |
| 22982 | 16 | No | Could not create internal stored procedures used to populate the change table for capture instance '%s' and source table '%s.%s'. Refer to previous errors in the current session to identify the cause and correct any associated problems. |
| 22983 | 16 | No | The unique index '%s' on source table '%s' is used by Change Data Capture. To alter or drop the index, you must first disable Change Data Capture on the table. |
| 22984 | 16 | No | An error occurred while waiting on the log reader history cache event. This error is reported by the internal task scheduling and might be transient. Retry the operation. |
| 22985 | 16 | No | Change data capture has not been enabled for source table '%s.%s'. Specify the name of a table enabled for Change Data Capture. To report on the tables enabled for Change Data Capture, query the is_tracked_by_cdc column in the sys.tables catalog view. |
| 22986 | 16 | No | Could not allocate memory for Change Data Capture population. Verify that SQL Server has sufficient memory for all operations. Check the physical and virtual memory settings on the server and examine memory usage to see if another application is consuming excessive memory. |
| 22987 | 16 | No | Change Data Capture population failed writing blob data for one or more large object columns. Verify that SQL Server has sufficient memory for all operations. Check the physical and virtual memory settings on the server and examine memory usage to see if another application is consuming excessive memory. |
| 22988 | 16 | No | This instance of SQL Server is the %s. Change data capture is only available in the Enterprise, Developer, Enterprise Evaluation, and Standard editions. |
| 22989 | 16 | No | Could not enable Change Data Capture for database '%s'. Change data capture is not supported on system databases, or on a distribution database. |
| 22990 | 16 | No | The value specified for the parameter @pollinginterval must be greater than or equal to 0 and less than 24 hours(Max: 86399 seconds). Specify a polling interval (in seconds) that is less than 24 hours (86,400 seconds). |
| 22991 | 16 | No | The value specified for the parameter @maxtrans must be greater than 0. |
| 22992 | 16 | No | The specified @job_type, %s, is not supported. The value specified for the parameter @job_type must be N'capture' to indicate a capture job, or N'cleanup' to indicate a cleanup job. |
| 22993 | 16 | No | The Change Data Capture job table containing job information for database '%s' cannot be found in the msdb system database. Run the stored procedure 'sys.sp_cdc_add_job' to create the appropriate CDC capture or cleanup job. The stored procedure will create the required job table. |
| 22994 | 16 | No | The retention value specified for the Change Data Capture cleanup process must be greater than 0 and less than or equal to 52594800. When creating or modifying the cleanup job, specify a retention value (in minutes) that is within that range. If this error is encountered when executing the sys.sp_cdc_cleanup_change_table stored procedure, reset the retention value associated with the job to a non-negative value less than 52594800 by using the sp_cdc_change_job stored procedure. |
| 22995 | 16 | No | A value for the parameter @retention cannot be specified when the job type is 'capture'. Specify NULL for the parameter, or omit the parameter from the statement. |
| 22996 | 16 | No | When adding or modifying the CDC cleanup job, @pollinginterval, @maxtrans, @maxscans, and @continuous may not be assigned non-null values. |
| 22997 | 16 | No | The Change Data Capture '%s' job does not exist in the system table 'msdb.dbo.cdc_jobs'. Use the stored procedure 'sys.sp_cdc_add_job' to add the Change Data Capture job. |
| 22998 | 16 | No | The value specified for the parameter @continuous must be 0 or 1. |
| 22999 | 16 | No | The value specified for the parameter @pollinginterval must be null or 0 when the stored procedure 'sys.sp_cdc_scan' is not being run in continuous mode. |
