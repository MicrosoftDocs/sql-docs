---
author: rwestMSFT
ms.author: randolphwest
ms.date: 01/11/2024
ms.topic: include
---
| Error | Severity | Event logged | Description |
| :--- | :--- | :--- | :--- |
| 23003 | 17 | No | The WinFS share permissions have become corrupted {Error: %ld}. Please try setting the share permissions again. |
| 23100 | 16 | No | Invalid input parameter(s). |
| 23101 | 16 | No | Access is denied. |
| 23102 | 16 | No | Item does not exist {ItemId: %ls}. |
| 23103 | 16 | No | Folder already exists {ItemId: %ls}. |
| 23104 | 16 | No | Folder does not exist {ItemId: %ls}. |
| 23105 | 16 | No | Operation violates hierarchical namespace uniqueness. |
| 23106 | 16 | No | Container is not empty {ItemId: %ls}. |
| 23107 | 16 | No | Item cannot be copied onto itself. |
| 23108 | 16 | No | Scoping path does not exist or is invalid. |
| 23109 | 16 | No | Container does not exist. |
| 23110 | 16 | No | No more items to enumerate. |
| 23111 | 16 | No | Item does not exist in the given scope {ItemId: %ls, Scope: %ls}. |
| 23112 | 16 | No | Transaction not in active state. |
| 23113 | 16 | No | Item either does not exist or it is not a file-backed one. |
| 23114 | 16 | No | Sharing violation. |
| 23115 | 16 | No | Transaction bindtoken must be null when called within the context of a transaction. |
| 23116 | 16 | No | Inconsistent StreamSize and/or AllocationSize data {ItemId: %ls}. |
| 23117 | 16 | No | File-backed item does not exist {ItemId: %ls}. |
| 23200 | 16 | No | ItemId of folder '%ls' not found. |
| 23201 | 16 | No | Share '%ls' does not exist in Catalog. |
| 23202 | 16 | No | Could not delete Share '%ls' in Catalog. |
| 23203 | 16 | No | Store item not found in Catalog. |
| 23204 | 16 | No | Could not delete Store item in Catalog. |
| 23205 | 16 | No | Store database name not found in Catalog. |
| 23206 | 16 | No | Could not create share to the ItemPath '%ls'. |
| 23207 | 16 | No | Could not add Share item for '%ls' in Catalog. |
| 23208 | 16 | No | ItemPath '%ls' does not exist in Store. |
| 23209 | 16 | No | Could not update Store state in Catalog. |
| 23210 | 16 | No | Itempath '%ls' is a file-backed item or within its sub-tree. |
| 23211 | 16 | No | Could not start Store Manager. Please look in WinFS UT Log for details. |
| 23212 | 16 | No | Itempath '%ls' is a compound item. |
| 23500 | 16 | No | Item container does not exist. |
| 23501 | 16 | No | Owning Item does not exist. |
| 23502 | 16 | No | NamespaceName is empty or exceeds the maximum length. |
| 23503 | 16 | No | Invalid Source endpoint type |
| 23504 | 16 | No | Invalid Target endpoint type |
| 23505 | 16 | No | A File-backed item must be a compound item type. |
| 23506 | 16 | No | A File Backed Item may not contain other Items. |
| 23509 | 16 | No | Source Item does not exist. |
| 23510 | 16 | No | Item with name already exists in container. |
| 23511 | 16 | No | New container cannot be a sub-container of item. |
| 23513 | 16 | No | Item does not exist. |
| 23515 | 16 | No | Item can not be deleted if it has children |
| 23519 | 16 | No | Target Item does not exist. |
| 23525 | 16 | No | Invalid Namespace Name. |
| 23530 | 16 | No | Operation can not be called inside a un-committable transaction |
| 23536 | 16 | No | Win32 file handle is open for item |
| 23573 | 16 | No | Cannot change ContainerId when replacing item. |
| 23579 | 16 | No | This procedure is reserved and cannot be called. |
| 23587 | 16 | No | File stream cannot be null. |
| 23588 | 16 | No | Container ids must be the same. |
| 23996 | 16 | No | The request could not be performed because of an device I/O error. |
| 23997 | 16 | No | System error occurred {ErrorCode: %d}. |
| 23998 | 16 | No | Not enough memory available in the system to process the request. |
| 23999 | 16 | No | Unspecified error(s) occurred. |
| 24501 | 16 | No | Internal error when retrieving lock information. Please try the operation again. If the problem persists contact Microsoft Azure Customer Support. |
| 24502 | 16 | No | Failed to get db lock with error %d during redo operation. |
| 24503 | 16 | No | Could not get clone tx ctx. Please try the operation again. If the problem persists contact Microsoft Azure Customer Support. |
| 24504 | 16 | No | Alter of System table Columns are not allowed. |
| 24505 | 16 | No | Table '%.\*ls' already exists. Choose a different table name or rename the existing table. |
| 24506 | 16 | No | Failed to find a TSRBRowGroupObject from the in-memory list |
| 24507 | 16 | No | Index quality generation was aborted due to server shutdown. |
| 24508 | 16 | No | The DB Version upgrade cannot modify user created data and metadata. |
| 24509 | 16 | No | The DB Version upgrade cannot modify user created data and metadata. Index \[%.\*ls\].\[%.\*ls\] row with Key %ls was created by transactions '%I64d'. |
| 24510 | 16 | No | Index maintenance operation complete: %d actions completed successfully in %d iterations with %d non-retriable errors and %d retriable errors encountered. Successful work performed by this command has been committed. If failures have occurred, please retry the command at a later time. |
| 24511 | 16 | No | Cache db version too low |
| 24512 | 16 | No | The requested SLO (%ls) for this pool exceeds the maximum allowed SLO (%ls). |
| 24513 | 16 | No | You have already reached the limit of %ld SQL pools in this workspace. |
| 24514 | 16 | No | You have already reached the limit of %ld SQL pools in this subscription. |
| 24515 | 16 | No | Sum of SLOs for all pools in this workspace (%ls) plus the requested SLO for the new pool (%ls) exceeds the limit (%ls). |
| 24516 | 16 | No | Sum of SLOs for all pools in this subscription (%ls) plus the requested SLO for the new pool (%ls) exceeds the limit (%ls). |
| 24517 | 16 | No | The user provided a disallowed value for %ls for Auto-Pause. The value provided is %ld. The value must be between \[%ld, %ld\] or 0 (zero). |
| 24519 | 16 | No | Internal error when resolving the DW temp table name during statement execution. Please try the operation again. If the problem persists contact Microsoft Azure Customer Support. |
| 24520 | 16 | No | The metadata log for the database '%.\*ls' is corrupted at Block '%I64d' RecordId '%I64d'. |
| 24521 | 16 | No | The metadata log Block '%I64d' for the database '%.\*ls' is not valid. |
| 24522 | 16 | No | Read of the metadata log Block '%I64d' for the database '%.\*ls' failed. Check errorlog for more info. |
| 24523 | 16 | No | The metadata log Block was flushed to disk but the trnasaction aborted. |
| 24524 | 16 | No | Failed to read or write the Max ASN data (HRESULT = 0x%x). |
| 24525 | 16 | No | Internal error. Unable to gain access to the manifest file table. Result \[%x\] |
| 24526 | 16 | No | Internal error. Unable to insert record to the manifest file table. Result \[%x\] |
| 24527 | 16 | No | Internal error. Encounter unexpected error while writing manifest file |
| 24528 | 16 | No | Attempt to commit the block list for a manifest file blob failed. Blob Prefix is "%ls", Blob name is "%ls". Storage Errorcode %ld. |
| 24529 | 10 | No | There is not enough capacity to resize pool '%ls' to %ld backend instances. |
| 24530 | 10 | No | Tenant ring selection failed while creating or activating a pool. |
| 24531 | 16 | No | Internal error. Unable to gain access to the reference table. Result \[%x\] |
| 24532 | 16 | No | Attempt to flush a manifest file blob failed. Blob Path is "%ls". |
| 24533 | 16 | No | USE statement does not support switching to '%ls' database. |
| 24534 | 16 | No | USE statement does not support switching to '%ls' database. Use a new connection to connect to '%ls' database. |
| 24535 | 16 | No | Transactional data stored in physical metadata doesn't align with database transactional data. |
| 24536 | 16 | No | Data insertion into Parquet-backed table failed. |
| 24537 | 16 | No | Three-part names are not supported from '%ls' database to '%ls' database. |
| 24538 | 10 | No | Internal error. Unable to gain access to the table-level write/write conflict table. Result \[%x\] |
| 24539 | 17 | No | Failed to update physical metadata because of a file table transaction error. |
| 24540 | 16 | No | Failed to update physical metadata because we could not read data from the temp table %d. |
| 24541 | 16 | No | Failed to update physical metadata because of invalid file path present in row %d of temp table %d. |
| 24542 | 16 | No | Failed to update physical metadata because of an unexpected file action present in row %d of temp table %d. |
| 25002 | 16 | No | The specified Publisher is not enabled as a remote Publisher at this Distributor. Ensure the value specified for the parameter @publisher is correct, and that the Publisher is enabled as a remote Publisher at the Distributor. |
| 25003 | 16 | No | Upgrade of the distribution database MSmerge_subscriptions table failed. Rerun the upgrade procedure in order to upgrade the distribution database. |
| 25005 | 16 | No | It is invalid to drop the default constraint on the rowguid column that is used by merge replication. |
| 25006 | 16 | No | The new column cannot be added to article '%s' because it has more than %d replicated columns. |
| 25007 | 16 | No | Cannot synchronize the subscription because the schemas of the article at the Publisher and the Subscriber do not match. It is likely that pending schema changes have not yet been propagated to the Subscriber. Run the Merge Agent again to propagate the changes and synchronize the data. |
| 25008 | 16 | No | The merge replication views could not be regenerated after performing the data definition language (DDL) operation. |
| 25009 | 16 | No | Invalid value '%s' specified while executing sp_changemergearticle on article '%s' for the 'identityrangemanagementoption' property. |
| 25010 | 16 | No | The constraint is used by merge replication for identity management and cannot be dropped directly. Execute sp_changemergearticle @publication, @article, "identityrangemanagementoption", "none" to disable merge identity management, which will also drop the constraint. |
| 25012 | 16 | No | Cannot add an identity column since the table is published for merge replication. |
| 25013 | 16 | No | Cannot perform alter table because the table is published in one or more publications with a publication_compatibility_level of lower than '90RTM'. Use sp_repladdcolumn or sp_repldropcolumn. |
| 25014 | 16 | No | sp_repladdcolumn does not allow adding columns of datatypes that are new to this release. |
| 25015 | 10 | No | Schema Changes and Bulk Inserts |
| 25016 | 10 | No | Prepare Dynamic Snapshot |
| 25017 | 16 | No | Failed to execute the command "%s" through xp_cmdshell. Detailed error information is returned in a result set. |
| 25018 | 16 | No | Precomputed partitions cannot be used because articles "%s" and "%s" are part of a join filter and at least one of them has a constraint with a CASCADE action defined. |
| 25019 | 16 | No | The logical record relationship between articles "%s" and "%s" cannot be added because at least one of the articles has a constraint with a CASCADE action defined. |
| 25020 | 16 | No | The article cannot be created on table '%s' because it has more than %d columns and column-level tracking is being used. Either reduce the number of columns in the table or change to row-level tracking. |
| 25021 | 16 | No | Replication stored procedure sp_MSupdategenhistory failed to update the generation '%s'. This generation will be retried in the next merge. |
| 25022 | 16 | No | The snapshot storage option (@snapshot_storage_option) must be 'file system', or 'database'. |
| 25023 | 16 | No | Stored procedures containing table-value parameters cannot be published as '\[serializable\] proc exec' articles. |
| 25024 | 16 | No | A snapshot storage option of 'database' is incompatible with the use of character mode bcp for snapshot generation. |
| 25025 | 16 | No | Cannot add a sparse column or a sparse column set because the table is published for merge replication. Merge replication does not support sparse columns. |
| 25026 | 16 | No | The proc sp_registercustomresolver cannot proceed because it is not run in the context of the distribution database, or the distribution database is not properly upgraded. |
| 25027 | 16 | No | Cannot find a credential for Windows login '%s'. Replication agent job sync needs a credential to be pre-created in all availability group replicas for each required Windows logins. |
| 25028 | 16 | No | Cannot specify 'database_name' when 'all' is set to 1. |
| 25029 | 16 | No | Replication distribution database '%s' is not in the primary replica of its availability group. |
| 25030 | 16 | No | Replication distribution database '%s' is not in an availability group. |
| 25031 | 10 | No | Monitor and sync replication agent jobs |
| 25032 | 10 | No | Sync replication agent jobs and their enabled states. |
| 25033 | 16 | No | The distribution database is part of an availability group without a listener. Add a listener to the availability group before adding publication, subscription or distribution. |
| 25034 | 16 | No | Distribution database in availability group is only supported for publisher_type as MSSQLSERVER. |
| 25035 | 16 | No | When distribution database is part of availability group, then distributor and publisher server cannot be same. |
| 25036 | 16 | No | Distribution database needs to be in readable state on secondary replica, when distribution database is part of availability group. |
| 25037 | 16 | No | The @subscriber parameter must be either @@SERVERNAME or listener name of the availability group that the subscriber database is part of. |
| 25038 | 16 | No | User needs to have VIEW SERVER STATE permission to assign @subscriber as the listener name of the availability group. |
| 25039 | 16 | No | Assigning listener name for @subscriber parameter is not a supported scenario for Managed Instance. |
| 25040 | 16 | No | Assigning the default value to "%ls": %ls. |
| 25507 | 16 | No | Invalid number of arguments passed to unit test '%ls'. |
| 25601 | 17 | No | The Extended Event engine is out of memory. |
| 25602 | 17 | No | The %S_MSG, "%.\*ls", encountered a configuration error during initialization. Object cannot be added to the event session. %ls |
| 25603 | 17 | No | The %S_MSG, "%.\*ls", could not be added. The maximum number of singleton targets has been reached. |
| 25604 | 17 | No | The Extended Event engine is disabled. |
| 25605 | 17 | No | The %S_MSG, "%.\*ls", could not be added. The maximum number of packages has been reached. |
| 25606 | 17 | No | The Extended Event engine could not be initialized. Check the SQL Server error log and the operating system error log for information about possible related problems. |
| 25607 | 17 | No | The Extended Event engine has been disabled by startup options. Features that depend on Extended Events may fail to start. |
| 25608 | 17 | No | The Extended Event engine could not allocate record for local thread/fiber. |
| 25623 | 16 | No | The %S_MSG name, "%.\*ls", is invalid, or the object could not be found |
| 25624 | 16 | No | The constraints of %S_MSG name, "%.\*ls", have been violated. The object does not support binding to actions or predicates. Event not added to event session. |
| 25625 | 16 | No | The %S_MSG, "%.\*ls", already exists in the event session. Object cannot be added to the event session. |
| 25626 | 16 | No | The %S_MSG, "%.\*ls", was specified multiple times. |
| 25629 | 16 | No | For %S_MSG, "%.\*ls", the customizable attribute, "%ls", does not exist. |
| 25630 | 16 | No | The predicate expression bound to %S_MSG ,"%.\*ls", has mismatching types. |
| 25631 | 16 | No | The %S_MSG, "%.\*ls", already exists. Choose a unique name for the event session. |
| 25632 | 16 | No | The specified buffer size is less than the minimum size. The minimum allowed size is %d bytes. |
| 25633 | 16 | No | The buffer size specified exceeds the maximum size. |
| 25634 | 16 | No | The dispatch latency specified is below the minimum size. |
| 25635 | 16 | No | An attempt was made to add an asynchronous target to a session with a maximum memory of 0. For asynchronous targets to be added to a session, the session must have a maximum memory greater than 0. |
| 25636 | 16 | No | Source and comparator types of the predicate do not match. |
| 25639 | 16 | No | The %S_MSG, "%.\*ls", exceeds the number of allowable bound actions. |
| 25640 | 16 | No | Maximum event size is smaller than configured event session memory. Specify a larger value for maximum event size, or specify 0. |
| 25641 | 16 | No | For %S_MSG, "%.\*ls", the parameter "%ls" passed is invalid. %ls |
| 25642 | 16 | No | Mandatory customizable attributes are missing for %S_MSG, "%.\*ls". |
| 25643 | 16 | No | The %S_MSG, "%.\*ls", can not be added to an event session that specifies no event loss. |
| 25644 | 16 | No | The %S_MSG, "%.\*ls", cannot be bound to the event session. |
| 25646 | 16 | No | The %S_MSG name, "%.\*ls", is invalid. |
| 25647 | 16 | No | The %S_MSG, "%.\*ls", could not be found. Ensure that the object exists and the name is spelled correctly. |
| 25648 | 16 | No | The %S_MSG, "%.\*ls", could not be found. Ensure that the package exists and the name is spelled correctly. |
| 25649 | 16 | No | Two of the actions/predicates for %S_MSG, "%.\*ls", can not coexist. Please remove one. |
| 25650 | 16 | No | For %S_MSG, "%.\*ls" the customizable attribute, "%ls", was specified multiple times. |
| 25651 | 16 | No | For %S_MSG, "%.\*ls", the value specified for customizable attribute, "%ls", did not match the expected type, "%ls". |
| 25653 | 16 | No | The %S_MSG, "%.\*ls", does not exist in the event session. Object cannot be dropped from the event session. |
| 25654 | 16 | No | Insuficient buffer space to copy error message. |
| 25655 | 16 | No | Internal Extended Event error: invalid message code. |
| 25656 | 16 | No | Error validating action. %ls |
| 25657 | 16 | No | Error validating predicate. %ls |
| 25658 | 16 | No | The %S_MSG name "%.\*ls" is not unique. |
| 25659 | 17 | No | The %S_MSG, "%.\*ls", encountered a configuration error during initialization. The customizable attribute %ls is used internally only. |
| 25664 | 16 | No | Internal Extended Event error: invalid package ID. |
| 25699 | 17 | No | The Extended Event engine failed unexpectedly while performing an operation. |
| 25701 | 15 | No | Invalid event session name "%.\*ls". Temporary event sessions are not allowed. |
| 25702 | 16 | No | The event session option, "%.\*ls", is set more than once. Remove the duplicate session option and re-issue the statement. |
| 25703 | 16 | No | The event session option, "%.\*ls", has an invalid value. Correct the value and re-issue the statement. |
| 25704 | 16 | No | The event session has already been stopped. |
| 25705 | 16 | No | The event session has already been started. |
| 25706 | 16 | No | The %S_MSG, "%.\*ls", could not be found. |
| 25707 | 16 | No | Event session option "%.\*ls" cannot be changed while the session is running. Stop the event session before changing this session option. |
| 25708 | 16 | No | The "%.\*ls" specified exceeds the maximum allowed value. Specify a smaller configuration value. |
| 25709 | 16 | No | One or more event sessions failed to start. Refer to previous errors in the current session to identify the cause, and correct any associated problems. |
| 25710 | 16 | Yes | Event session "%.\*ls" failed to start. Refer to previous errors in the current session to identify the cause, and correct any associated problems. |
| 25711 | 16 | No | Failed to parse an event predicate. |
| 25712 | 16 | No | An invalid comparison operator was specified for an event predicate. |
| 25713 | 16 | No | The value specified for %S_MSG, "%.\*ls", %S_MSG, "%.\*ls", is invalid. |
| 25715 | 16 | No | The predicate on event "%ls" is invalid. Operator '%ls' is not defined for type "%ls", %S_MSG: "%.\*ls". |
| 25716 | 16 | No | The predicate on event, "%.\*ls", exceeds the maximum length of %d characters. |
| 25717 | 16 | No | The operating system returned error %ls while reading from the file '%s'. |
| 25718 | 16 | No | The log file name "%s" is invalid. Verify that the file exists and that the SQL Server service account has access to it. |
| 25719 | 16 | No | Initial file name and initial offset must be specified as a pair. Please correct the parameters and retry your query. |
| 25720 | 10 | No | 'sys.fn_xe_file_target_read_file' is skipping records from "%.\*ls" at offset %I64d. |
| 25721 | 16 | No | The metadata file name "%s" is invalid. Verify that the file exists and that the SQL Server service account has access to it. |
| 25722 | 16 | No | The offset %I64d is invalid for log file "%s". Specify an offset that exists in the log file and retry your query. |
| 25723 | 16 | No | An error occurred while obtaining metadata information from the file "%s". The file may be damaged. |
| 25724 | 16 | No | Predicate too large for display. |
| 25725 | 16 | No | An error occurred while trying to flush all running Extended Event sessions. Some events may be lost. |
| 25726 | 17 | No | The event data stream was disconnected because there were too many outstanding events. To avoid this error either remove events or actions from your session or add a more restrictive predicate filter to your session. |
| 25727 | 16 | No | The Extended Events session named "%.\*ls" has either been stopped or dropped and can no longer be accessed. |
| 25728 | 16 | No | The Extended Events session named "%.\*ls" could not be found. Make sure the session exists and is started. |
| 25729 | 17 | No | The event data stream was disconnected due to an internal error. |
| 25730 | 10 | No | The Extended Events session named "%.\*ls" was modified during the upgrade and one of the bucketizer targets was excluded from the Extended Events session. Only one bucketizer target is supported for each Extended Events session. |
| 25731 | 16 | No | Execution of event session state change request failed on remote brick. Event session name: "%.\*ls". Refer to previous errors to identify the cause, and correct any associated problems. |
| 25732 | 16 | No | One or more event sessions failed to reconcile their runtime states. Refer to previous errors in the current session to identify the cause, and correct any associated problems. |
| 25733 | 16 | No | Event session "%.\*ls" failed to reconcile its runtime state. Refer to previous errors in the current session to identify the cause, and correct any associated problems. |
| 25734 | 16 | No | The file pattern "%s" represents a full path. Use only relative paths with no drive letters. |
| 25735 | 16 | No | The source option %d is invalid. |
| 25736 | 16 | No | A damaged buffer was found in file "%s" at offset %I64d, no further events will be read from the file. |
| 25737 | 16 | No | Database scoped extended event sessions are not available in server scope or system databases in Azure DB. |
| 25738 | 16 | No | Event session '%.\*ls' could not be started because system is currently busy. Please try again later. |
| 25739 | 16 | No | Failed to start event session '%.\*ls' because required credential for writing session output to Azure blob is missing. |
| 25740 | 16 | No | Unable to start event session '%.\*ls' because system is busy. Please try again later. |
| 25741 | 16 | No | The URL specified for %S_MSG "%.\*ls", %S_MSG "%.\*ls" is invalid. URL must begin with 'https://' . |
| 25742 | 16 | No | Target '%.\*ls' is not available for Azure SQL Database. |
| 25743 | 16 | No | The event '%.\*ls' is not available for Azure SQL Database. |
| 25744 | 16 | No | The action '%.\*ls' is not available for Azure SQL Database. |
| 25745 | 16 | No | Unable to retrieve Azure SQL Database telemetry data. |
| 25746 | 16 | No | Operation failed. Operation will cause database %S_MSG memory to exceed allowed limit. Event session memory may be released by stopping active sessions or altering session memory options. Check sys.dm_xe_database_sessions for active sessions that can be stopped or altered. |
| 25747 | 16 | No | Operation failed. Operation will cause database %S_MSG memory to exceed allowed limit. Event session memory may be released by stopping active sessions or altering session memory options. Check sys.dm_xe_database_sessions for active sessions that can be stopped or altered. If no sessions are active on this database, please check sessions running on other databases under the same logical server. |
| 25748 | 16 | No | The file "%s" contains audit logs. Audit logs can only be accessed by using the fn_get_audit_file function. |
| 25749 | 16 | No | Target '%.\*ls' is not available on SQL Database Managed Instance. |
| 25750 | 16 | No | Event '%.\*ls' is not available on SQL Database Managed Instance. |
| 25751 | 16 | No | Action '%.\*ls' is not available on SQL Database Managed Instance. |
| 25752 | 16 | No | Auto start XE sessions are disabled. |
| 25753 | 10 | No | XE session '%s' started. |
| 25754 | 10 | No | XE session '%s' stopping. |
| 25755 | 16 | No | Could not create live session target because live session targets are disabled. |
| 25756 | 16 | No | Could not create stream XEL file because that functionality is disabled. |
