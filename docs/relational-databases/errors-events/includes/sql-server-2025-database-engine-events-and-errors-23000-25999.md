---
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: maghan
ms.date: 08/13/2026
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
| 23118 | 16 | No | Either %ls, or %ls, or both must have a value. These parameters cannot be both set to NULL. |
| 23119 | 16 | No | The index specified by the @index_id is an XML Index. Omit the @data_compression parameter or set it to NULL, XML Indexes do not support data compression, but support XML Compression. |
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
| 23601 | 10 | No | Change Event Streaming |
| 23602 | 16 | No | Creation of the AMQP link on the session failed. |
| 23603 | 16 | No | AMQP link target descriptor creation failed. |
| 23604 | 16 | No | AMQP link source descriptor creation failed. |
| 23605 | 16 | No | AMQP Session flow control configuration failed. |
| 23606 | 16 | No | Creation of the AMQP session on the broker connection failed. |
| 23607 | 16 | No | Creation of the AMQP connection to the broker failed. |
| 23608 | 16 | No | Initialization of the AMQP client library failed. |
| 23609 | 16 | No | Broker acknowledgment timed out. |
| 23610 | 16 | No | The message could not be delivered to the broker. |
| 23611 | 16 | No | Failed to establish an AMQP connection to the broker. |
| 23612 | 16 | No | Failed to acquire a token using a managed service identity. Make sure that managed identity is configured correctly. Return code: '0x%08x'. |
| 23613 | 16 | No | The identity type specified in the database scoped credential is not supported. For more information about supported identity types, see Change Event Streaming documentation. |
| 23614 | 17 | No | Could not allocate memory for Change Event Streaming operation. Verify that sufficient memory is available for all operations. Check the memory settings on the server and examine memory usage to see if another application is excessively consuming memory. |
| 23615 | 16 | No | The argument '%ls' must not be null. |
| 23616 | 16 | No | The value provided for the argument '@partition_key_column_name' is invalid. This argument must be null unless the argument '@partition_key_scheme' is set to 'Column'. |
| 23617 | 16 | No | The argument '@partition_key_column_name' must not be null or empty when using 'Column' partition key scheme. |
| 23618 | 16 | No | The value provided for the argument '%ls' is invalid. Allowed values: %ls. |
| 23619 | 16 | No | The value provided for the argument '%ls' is invalid. Stream group with the provided name already exists. |
| 23620 | 16 | No | The value provided for the argument '%ls' is invalid. %ls with the provided name does not exist. |
| 23621 | 16 | No | The argument '%ls' must not be null or empty. |
| 23622 | 16 | No | Could not enable Change Event Streaming for database '%ls'. Change Event Streaming cannot be enabled on a database that already has Mirroring in Fabric or Synapse Link enabled. |
| 23623 | 16 | No | Cannot enable Change Event Streaming because it is a preview feature in SQL Server. Please enable the PREVIEW_FEATURES database-scoped configuration before configuring Change Event Streaming. |
| 23624 | 16 | No | Insufficient permissions: The user must have CONTROL permission on the database to configure Change Event Streaming. |
| 23625 | 16 | No | The value provided for the argument '%ls' is invalid. |
| 23626 | 16 | No | An error occurred. The error/state returned was %d/%d: '%ls'. |
| 23627 | 16 | No | AMQP link settle mode configuration failed. |
| 23628 | 16 | No | The argument '%s' failed validation. Expects: 'schemaName.objectName' |
| 23629 | 16 | No | AMQP link maximum message size configuration failed. |
| 23630 | 16 | No | Creation of the AMQP message sender for the link failed. |
| 23631 | 16 | No | AMQP message sender Open function failed. |
| 23632 | 16 | No | AMQP message sender failed to send the message. |
| 23633 | 16 | No | Adding application property to the AMQP message failed. |
| 23634 | 16 | No | Change Event Streaming is not supported for this database. |
| 23635 | 16 | No | Adding partition key to the AMQP message failed. |
| 23636 | 16 | No | Change Event Streaming is not enabled on database '%ls'. |
| 23637 | 16 | No | Change Event Streaming is not supported in the free offer databases, single databases using the Basic, S0, S1, or S2 service objectives, and in elastic pools with less than 100 eDTUs or less than 1 vCore. Upgrade to a higher service objective and try again. |
| 23638 | 16 | No | Change Event Streaming encountered an Avro exception. |
| 23639 | 16 | No | LOB data could not be read while extracting column values for change event. HRESULT: '0x%08x'. |
| 23640 | 16 | No | Change Event Streaming serializer not initialized. |
| 23641 | 16 | No | The value in provided argument '@include_old_lob_values' is incompatible with value '0' provided in argument '@include_old_values'. To use '@include_old_lob_values' with value '1', the parameter '@include_old_values' is expected to be '1'. |
| 23642 | 16 | No | Destination location parameter is not in the expected format. Expected format is \[Event Hubs Namespace Host name\]/\[Event Hubs Instance\] or \[Event Hubs Namespace Host name\]:\[Port\]/\[Event Hubs Instance\] |
| 23643 | 16 | No | Change Event Streaming for '%ls' is already enabled for stream group '%ls'. |
| 23644 | 16 | No | Table name supplied in the argument '%ls' is either not configured for Change Event Streaming or it does not exist for the supplied stream group. |
| 23645 | 16 | No | The value provided for the 'Port number' in the destination location parameter is invalid. It must be a numeric value between 0 and 65535. |
| 23646 | 16 | No | Change Event Streaming message exceeds the configured message size limit. |
| 23647 | 16 | No | Tables contained in the changefeed schema cannot be enabled for Change Streams. |
| 23648 | 16 | No | Could not enable Change Event Streaming for database '%ls'. Change Event Streaming is not supported on system databases, or on a distribution database. |
| 23649 | 16 | No | Change Event Streaming is not supported in contained databases. Change Event Streaming cannot be enabled on the contained database '%ls'. |
| 23650 | 16 | No | Could not enable Change Event Streaming for database '%ls'. Change Event Streaming cannot be enabled on a database configured for Change Data Capture. |
| 23651 | 16 | No | Could not enable Change Event Streaming for database '%ls'. Change Event Streaming cannot be enabled on a database with forced or allowed DELAYED_DURABILITY. |
| 23652 | 16 | No | Could not enable Change Event Streaming for database '%ls'. Change Event Streaming cannot be enabled on a database configured for Database Mirroring in SQL Server. |
| 23653 | 16 | No | Could not enable Change Event Streaming for database '%ls' as it is already enabled. |
| 23654 | 16 | No | The application lock request '%ls' needed to modify Change Event Streaming metadata was not granted. The value returned by sp_getapplock was %d. Examine the error cause and resubmit the request. |
| 23655 | 16 | No | Allowed number of Change Event Streaming stream groups is exceeded. The limit is %d stream groups per database. |
| 23656 | 16 | No | Enabling Change Event Streaming for table '%ls' failed because '%ls' table type is not supported. |
| 23657 | 16 | No | The number of tables enabled for a Change Event Streaming stream group cannot exceed %d. Current number of tables enabled: %d. |
| 23658 | 16 | No | Change Event Streaming encountered a SQL exception. |
| 23659 | 16 | No | Schema changes on table '%.\*ls' are not supported because the table is enabled for Change Event Streaming. |
| 23660 | 16 | No | The switch partition operation is not supported for table '%.\*ls' because it is enabled for Change Event Streaming. |
| 23661 | 16 | No | Cannot rename the table because it is enabled for Change Event Streaming. |
| 23662 | 16 | No | Cannot drop the table '%.\*ls' because it is enabled for Change Event Streaming. |
| 23663 | 16 | No | Cannot truncate the table '%.\*ls' because it is enabled for Change Event Streaming. |
| 23664 | 16 | No | Adding primary key constraint failed for table '%.\*ls'. Once tables are enabled for Change Event Streaming, their primary keys cannot be added or modified. |
| 23665 | 16 | No | Dropping primary key constraint failed for table '%.\*ls'. Once tables are enabled for Change Event Streaming, their primary keys cannot be dropped or modified. |
| 23666 | 16 | No | The expected value for argument '%ls' is between '%d' and '%d' KB, inclusive. |
| 23667 | 16 | No | Change Event Streaming event delivery error: '%ls'. |
| 23668 | 16 | No | User table '%ls' has reached the maximum number of Change Event Streaming destinations. |
| 23669 | 10 | No | PREVIEW_FEATURES must be enabled on the database to use Microsoft Fabric Mirroring on Linux. |
| 23670 | 10 | No | No primary system assigned managed identity found. |
| 23671 | 10 | No | Microsoft Fabric Mirroring is not supported on Azure VM. |
| 23672 | 10 | No | Microsoft Fabric Mirroring is not supported with Arc user assigned managed identity. Please use Arc system assigned managed identity. |
| 23675 | 16 | No | Enabling Change Event Streaming for table '%ls' that has a column_set column is not supported. |
| 23676 | 16 | No | Adding a column_set column to table '%.\*ls' that is enabled for Change Event Streaming is not supported. |
| 23677 | 16 | No | Change Event Streaming encountered a Kafka exception. |
| 23678 | 16 | No | Failed to send a message to Kafka endpoint. |
| 23679 | 16 | No | Unable to reach the configured destination. |
| 23680 | 16 | No | The specified destination instance could not be found. Verify the destination_location parameter in the stream group configuration. |
| 23681 | 16 | No | Could not connect to the destination due to an authentication error. |
| 23682 | 16 | No | Could not connect to the Kafka endpoint due to throttling happening on the destination. |
| 23683 | 16 | No | Failed to send a message to AMQP endpoint. |
| 23684 | 16 | No | The specified destination instance could not be found. Verify the destination_location parameter in the stream group configuration. |
| 23685 | 16 | No | The message could not be sent because its size exceeds an AMQP broker limit. |
| 23686 | 16 | No | Could not connect to the AMQP endpoint due to throttling happening on the destination. |
| 23687 | 16 | No | Failed to verify access to the destination. Verify that SAS token is valid. |
| 23688 | 16 | No | Could not connect to the destination due to an authentication error. |
| 23689 | 16 | No | The request to the AMQP endpoint was not authorized. Verify that your credentials or access policy include the required permissions. |
| 23690 | 16 | No | The request to the Kafka endpoint was not authorized. Verify that your credentials or access policy include the required permissions. |
| 23691 | 16 | No | Failed to send a message to the requested topic. The SAS token provided when creating the credential is invalid. |
| 23692 | 16 | No | Delivery of the message failed due to a timeout. Check if the broker is available and accessible over the network. |
| 23693 | 16 | No | Request to the broker failed due to a timeout. Check if the broker is available and accessible over the network. |
| 23694 | 16 | No | Unable to establish a secure connection to the broker. |
| 23695 | 16 | No | The requested AMQP entity does not exist on the broker. |
| 23696 | 16 | No | The AMQP resource was removed or disabled after a connection to the resource was established. |
| 23697 | 16 | No | Failed to create Change Event Streaming stream group object. Destination location is not in the expected format. Expected format is \[Event Hubs Namespace Host name\]/\[Event Hubs Instance\] or \[Event Hubs Namespace Host name\]:\[Port\]/\[Event Hubs Instance\] |
| 23698 | 16 | No | Aborting Change Event Streaming Publish task for partition %ld timed out. Change Event Streaming is not disabled. Retry this operation later. |
| 23699 | 16 | No | Invalid table state encountered while processing Change Event Streaming table. |
| 23701 | 16 | No | Database '%ls' went to suspect state. Backup cannot be performed on a database that is in suspect state. |
| 23702 | 16 | No | Backup on participant is not allowed in SDA failover. |
| 23801 | 16 | No | Change Event Streaming encountered an invalid UTF-8 encoding sequence. |
| 23802 | 16 | No | Change Event Streaming credential not found. |
| 23803 | 16 | No | Cannot drop the database scoped credential '%.\*ls' because it is being used by a Change Event Streaming group. |
| 23804 | 16 | No | Failed to check if credential is in use by a Change Event Streaming group. |
| 23805 | 10 | No | %s is deprecated and will be removed in a future release. Use %s instead. |
| 23806 | 16 | No | Could not find stream group or table with the provided stream_group_id and table_id, or source_schema and source_name. |
| 23807 | 16 | No | Invalid parameter combination. When @table_id is provided, @stream_group_id must also be provided. |
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
| 24523 | 16 | No | The metadata log Block was flushed to disk but the transaction aborted. |
| 24524 | 16 | No | Failed to read or write the Max ASN data (HRESULT = 0x%x). |
| 24525 | 16 | No | Internal error. Unable to interact with an internal metadata table. Please try the operation again and contact Customer Support Services if this persists. |
| 24526 | 16 | No | Internal error. Unable to properly update physical metadata. Please try the operation again and contact Customer Support Services if this persists. |
| 24527 | 16 | No | Internal table %ld operation failed on column %u with result %ld. |
| 24528 | 10 | No | Statement ID: %s |
| 24529 | 10 | No | There is not enough capacity to resize pool '%ls' to %ld backend instances. |
| 24530 | 10 | No | Tenant ring selection failed while creating or activating a pool. |
| 24533 | 16 | No | USE statement does not support switching to '%ls' database. |
| 24534 | 16 | No | USE statement does not support switching to '%ls' database. Use a new connection to connect to '%ls' database. |
| 24535 | 16 | No | Transactional data stored in physical metadata doesn't align with database transactional data. |
| 24536 | 16 | No | Data insertion into Parquet-backed table failed. |
| 24537 | 16 | No | Three-part names are not supported from '%ls' database to '%ls' database. |
| 24539 | 17 | No | Failed to update physical metadata because of a file table transaction error. |
| 24540 | 16 | No | Failed to update physical metadata because we could not read row %lu data from the temp table %ld for column %u. |
| 24541 | 16 | No | Failed to update physical metadata because of invalid file path present in row %lu of temp table %ld. |
| 24542 | 16 | No | Failed to update physical metadata because of an unexpected file action '%.\*hs' present in row %lu of temp table %ld. |
| 24543 | 16 | No | Cannot begin a transaction with the given isolation level. Please specify the snapshot isolation level when explicitly beginning the transaction. |
| 24544 | 16 | No | The current isolation level isn't supported. Please change the transaction isolation level for this session to snapshot then re-run the operation. |
| 24545 | 16 | No | An unexpected error occurred during Transcoder scan. |
| 24546 | 16 | No | An unexpected error occurred during execution. |
| 24547 | 16 | No | There was an error updating runtime physical metadata information needed to execute the statement. |
| 24548 | 16 | No | A conversion error occurred during Transcoder scan. |
| 24549 | 18 | No | Encountered internal error %d (of category '%ls') while attempting to process physical metadata. |
| 24550 | 16 | No | Encountered operating system error %ls while attempting to write physical metadata. |
| 24551 | 16 | No | Encountered operating system error %ls while attempting to read physical metadata. |
| 24552 | 16 | No | Delete operation failed. |
| 24553 | 16 | No | Invalid number of target backend instances for pool resize. |
| 24554 | 16 | No | Pool is not in a valid state for resize. |
| 24555 | 16 | No | Keep list for pool resize legth/content is invalid. |
| 24556 | 16 | No | Snapshot isolation transaction aborted due to update conflict. Using snapshot isolation to access table '%.\*ls' directly or indirectly in database '%.\*ls' can cause update conflicts if rows in that table have been deleted or updated by another concurrent transaction. Retry the transaction. |
| 24557 | 16 | No | Internal error. Encountered an unexpected error while working with an internal table. Result \[%x\]. |
| 24558 | 10 | No | Failed physical metadata notification action '%ls' with result %lu for row %lu data from the temp table %ld. |
| 24559 | 16 | No | Data Manipulation Language (DML) statements are not supported for this table type in this version of SQL Server. |
| 24560 | 16 | No | Internal error. Encountered an issue removing physical metadata. |
| 24561 | 16 | No | Internal error. Invalid physical metadata for CREATE TABLE operation. |
| 24562 | 16 | No | Input not provided. Could not create vdw service principal. Please provide mandatory parameters to create service principal. |
| 24563 | 16 | No | Invalid input. Could not create vdw service principal. The number of provided values does not match the mandatory parameters for the environment. |
| 24564 | 16 | No | sp_get_min_xdes is unable to get and validate min xdes |
| 24565 | 16 | No | sp_get_delta_lake_storage_properties is unable to return |
| 24566 | 16 | No | Failed to update physical metadata with result %lu because of invalid partition values present in row %lu of temp table %ld. |
| 24567 | 18 | No | Failed to parse '%ls' from physical metadata with result %lu for row %lu of temp table %ld. |
| 24568 | 16 | No | Internal DW transaction error. |
| 24570 | 10 | No | CREATE TABLE executed for the database %s without workspace on Trident instance. Normal for Witness databases |
| 24571 | 16 | No | Failed to process extended property '%ls' for DB %ld table %ld. More details: %ls. |
| 24572 | 10 | No | Commits up to '%ld' have already been applied for DB %ld table %ld. Skipped %lu/%lu row(s) of temp table %ld. |
| 24573 | 16 | No | Invalid row group metadata for row group id "%ls". |
| 24574 | 16 | No | The %ls '%ls' is not supported in this edition of SQL Server. |
| 24575 | 10 | No | Manifest for DB %ld table %ld is not available, commit is not possible. |
| 24576 | 16 | No | Table name cannot contain '%.\*ls'. |
| 24577 | 16 | No | Invalid column chunk metadata for column "%ls". Wrong parameter: %ls. Underlying data description: %ls. |
| 24580 | 16 | No | Invalid value %d was provided for the DML config parameter %ls. |
| 24581 | 16 | No | Global temporary tables and transient user tables in TEMPDB are not supported in this edition of SQL Server. |
| 24583 | 16 | No | Enforced constraints are not supported. To create an unenforced constraint you must include the NOT ENFORCED syntax as part of your statement. |
| 24584 | 16 | No | The %ls keyword is not supported in the %ls statement in this edition of SQL Server. |
| 24585 | 16 | No | The specified ALTER TABLE statement is not supported in this edition of SQL Server. |
| 24586 | 16 | No | Distributed transactions are not supported in this edition of SQL Server. |
| 24590 | 16 | No | Encountered incompatible delete format. |
| 24591 | 16 | No | An invalid value is present in the data. |
| 24592 | 16 | No | Invalid metadata present in the file. |
| 24593 | 10 | No | Internal error. Unable to Serialize Usage Data. |
| 24594 | 10 | No | Internal error. Buffer is full. |
| 24595 | 10 | No | SQL internal tasks encountered failure. |
| 24596 | 16 | No | Failed to complete the command because the underlying location does not exist. Underlying data description: %ls. |
| 24597 | 16 | No | An integer precision value between 0 and 6 must be specified. |
| 24599 | 16 | No | Failed to ingest data since a BE connection instead of FE connection was used. |
| 24601 | 16 | No | A Native Shuffle storage provider for the URL '%ls' could not be found. |
| 24602 | 17 | No | Error 0x%X - Could not allocate space while transferring data from one distribution to another. |
| 24603 | 17 | No | Error 0x%X occurred when sending data over the network to another distribution. Please try to run the query again. If the error persists, please contact support. |
| 24604 | 17 | No | Error occurred when distribution %d wrote metadata for distribution %d. Please try to run the query again. If the error persists, please contact support. |
| 24605 | 17 | No | The storage entity does not exist when transferring data from one distribution to another. If the error persists, please contact support. |
| 24606 | 17 | No | The storage entity already exists when transferring data from one distribution to another. If the error persists, please contact support. |
| 24607 | 17 | No | Error occurred when setting the owner for the storage location '%ls'. If the error persists, please contact support. |
| 24608 | 17 | No | Storage location for transfering data to another distribution is not defined. If the error persists, please contact support. |
| 24609 | 17 | No | Error 0x%X occurred while trying to access data exceeding the permitted limit. If the error persists, please contact support. |
| 24610 | 17 | No | Unsupported data movement functionality was used. If the error persists, please contact support. |
| 24611 | 20 | No | Data movement temporary table metadata is invalid. If the error persists, please contact support. |
| 24701 | 16 | No | The query failed because the access is denied on %ls. |
| 24702 | 16 | No | The query failed because the following location is changed during the query execution: %ls. |
| 24703 | 16 | No | The query failed because an unexpected error occurred in distributed query processing phase. File a support ticket and provide the error code and statement id. Underlying data description: %ls. |
| 24704 | 16 | No | The query processor ran out of internal resources. Underlying data description: %ls. |
| 24705 | 16 | No | Error when converting partition column value '%ls' to '%ls' column type. |
| 24706 | 16 | No | Snapshot isolation transaction aborted due to update conflict. You cannot use snapshot isolation to access table '%.\*ls' directly or indirectly in database '%.\*ls' to update, delete, or insert the row that has been modified or deleted by another transaction. Please retry the transaction. |
| 24707 | 16 | No | There was an error reading runtime physical metadata information needed to execute the statement. |
| 24708 | 16 | No | '%ls' is an unsupported partition column type. |
| 24709 | 16 | No | There are null values present in the column which was specified as 'not null'. |
| 24710 | 16 | No | There is insufficient system memory to read the data: %ls. |
| 24711 | 16 | No | The query failed because parquet file is corrupted and cannot be deserialized while attempting to read it from the location: %ls. |
| 24712 | 16 | No | The query failed because parquet file is corrupted and cannot be deserialized while attempting to read it from the location: %ls. |
| 24713 | 16 | No | Oredered index is not supported. |
| 24714 | 16 | No | Unexpected Error in DW FrontEnd Stress Testing. |
| 24715 | 16 | No | The specified ordered column count %d is greater than the maximum ordered column count %d. |
| 24716 | 16 | No | sp_get_managed_delta_table_log_files_metadata is unable to generate the output xml. |
| 24717 | 16 | No | sp_check_file_cleanup_eligibility is unable to generate the output resultset. |
| 24718 | 16 | No | Lakehouse tables cannot be renamed from the SQL Endpoint. Please use the Lakehouse to rename tables. |
| 24719 | 16 | No | sp_trigger_expired_files_cleanup is unable to trigger the system event for expired files cleanup. |
| 24720 | 16 | No | Internal error. Unable to interact with an internal metadata table. Please try the operation again and contact Customer Support Services if this persists. |
| 24721 | 16 | No | The operation could not be completed due to I/O error. This error can occur when the file size defined in the metadata (or delta log) is larger than the actual size of the file. Underlying data description: %ls. |
| 24722 | 17 | No | The remote storage service is not active or available at this time (HTTP 500). This could be a temporary issue. Please try the operation one more time. Underlying data description: %ls. |
| 24723 | 17 | No | The operation has timed out (HTTP 503) due to a lack of response from the remote storage service. This could be a temporary issue. Please try the operation one more time. Underlying data description: %ls. |
| 24724 | 16 | No | Cannot bulk load because of an error writing file. Underlying data description: %ls. |
| 24725 | 16 | No | Data Compaction is only supported for warehouse databases. |
| 24726 | 10 | No | Service is currently experiencing high demand and is unable to process your request at this time. Please try again later, as retrying may help. |
| 24727 | 16 | No | Table name cannot end with '.'. |
| 24728 | 16 | No | Schema name cannot contain '%.\*ls'. |
| 24729 | 16 | No | Schema name cannot end with '.'. |
| 24730 | 17 | No | The operation encountered an HTTP error while trying to read data. This could be a temporary issue. Please try the operation one more time. Underlying data description: %ls. |
| 24731 | 16 | No | The specified clustering column count %d is greater than the maximum clustering column count %d. |
| 24732 | 16 | No | CLUSTER BY is not supported. |
| 24733 | 16 | No | An unexpected error occurred during execution. |
| 24734 | 17 | No | There was an error during stored procedure execution. |
| 24735 | 16 | No | Only nullable columns can be added to an existing table. |
| 24736 | 16 | No | There are multiple empty string indexes present in the dictionary. Column name: '%ls'. |
| 24737 | 16 | No | Data Lake Log publishing is supported only for warehouse databases. |
| 24738 | 20 | No | There was an error reading runtime ingestion information needed to execute the statement. |
| 24739 | 18 | No | No statistics exist for clustering column at ordinal %lu. |
| 24740 | 16 | No | VORDER is supported only for warehouse databases. |
| 24741 | 16 | No | Identity column '%.\*ls' must be of data type BIGINT. |
| 24742 | 16 | No | Identity column '%.\*ls' does not support specifying SEED or INCREMENT. |
| 24743 | 16 | No | An error occurred while attempting to update discovered table properties. |
| 24744 | 16 | No | StringCchCopyNW failed with HRESULT = 0x%x. |
| 24745 | 18 | No | Found column mapping properties, but no column mapping values. |
| 24746 | 18 | No | The `MaxColumnId` value %u is less than the number of column mappings %lu. |
| 24747 | 18 | No | The `MaxColumnId` %u is less than column with field ID value %u. |
| 24748 | 18 | No | The 'MaxColumnId' value %u is out of bounds. |
| 24749 | 18 | No | The column mapping 'Id' value %u is out of bounds. |
| 24750 | 18 | No | Found %lu column mapping values, but did not find the required column mapping properties. |
| 24751 | 18 | No | Found duplicate column mapping id = %lu. |
| 24752 | 18 | No | Found duplicate column mapping logical name = '%ls'. |
| 24753 | 18 | No | Found duplicate column mapping physical name = '%ls'. |
| 24754 | 16 | No | An internal error occurred when truncating table '%.\*ls'. |
| 24755 | 16 | No | An internal error occurred when truncating user table. |
| 24756 | 16 | No | The query failed because handling end of file happened before whole IO buffer was populated: %ls. |
| 24757 | 16 | No | Could not create table '%.\*ls' because the type (collation) of the column '%.\*ls' is not supported in a table with Data Clustering. |
| 24758 | 16 | No | sp_cleanup_dropped_table_metadata is unable to clean up internal metadata for the trident DW table. |
| 24759 | 18 | No | Data insertion into Parquet-backed table failed (%d,%ls). |
| 24760 | 16 | No | The file is temporarily unavailable due to a high number of requests in the lake. This is a transient issue. Please try again shortly. Underlying data description: %ls. |
| 24761 | 16 | No | There are null values present in the partition column '%ls' which was specified as 'not null'. |
| 24762 | 16 | No | Error converting values NaN or Infinity to type '%ls' in the column '%ls'. NaN and Infinity are not supported. Underlying data description: %ls. |
| 24763 | 16 | No | There was an error reading column mapping information for column: '%ls'. |
| 24764 | 18 | No | No histogram steps exist for clustering column at ordinal %lu. Please check the content of the table. |
| 24765 | 18 | No | %ld rows processed for compaction did NOT match %ld rows from the source. |
| 24766 | 16 | No | TIMESTAMP can only be set for snapshot databases. |
| 24767 | 16 | No | CREATE DATABASE AS SNAPSHOT OF supported only for DataWarehouse. |
| 24768 | 16 | No | TIMESTAMP is invalid or missing in the DDL statement. |
| 24769 | 18 | No | Internal error with lock serialization logic during DW pre-commit transactional metadata handling. |
| 24770 | 18 | No | Identity %.\*ls value %I64d is out of range. Value must be between 0 and (2^%d - 1). Retry may not help. Please contact Microsoft support. |
| 24771 | 18 | No | The %.\*ls JSON parameter %.\*ls is missing or incorrectly formatted. Please check the formatting of the JSON. |
| 24772 | 17 | No | Failed to initialize TempDB. |
| 24773 | 16 | No | The query failed because the file is too small. Underlying data description: %ls. |
| 24774 | 18 | No | Internal error occurred during DW table metrics retrieval. |
| 24775 | 18 | No | Internal system error (%ls) when attempting to open or create remotely stored delta log file. This error is usually intermittent. Please try the operation again and contact Customer Support Services if this persists. |
| 24776 | 10 | No | The current version of data being accessed in \[%s\] is as of timestamp '%s'. |
| 24777 | 10 | No | TIMESTAMP must not be before the source database was created. Creation timestamp: '%s', specified timestamp: '%s'. |
| 24778 | 18 | No | Internal error occurred during Trident DW mix-mode query execution. |
| 24779 | 16 | No | The specified ALTER TABLE statement is not supported in this edition of SQL Server. Columns that are part of a data clustered index may not be dropped. |
| 24780 | 16 | No | Columns in this type of table cannot be renamed. |
| 24781 | 16 | No | Desired column data type: '%ls' is not supported, please retry column creation with a supported data type. |
| 24782 | 18 | No | Internal error occurred during Trident DW DDL Operations. |
| 24783 | 18 | No | Invalid column mapping metadata. |
| 24784 | 17 | No | An error occurred while communicating with an external component. Underlying data description: %ls. |
| 24785 | 18 | No | Internal error occurred when attempting to read physical metadata. |
| 24786 | 18 | No | Unable to process insertion into table '%ls' with identity column. Please wait for other active transactions to abort or commit, then retry. |
| 24787 | 18 | No | Internal error occurred during execution of the command. |
| 24788 | 18 | No | Error occurred while attempting to read a deletion vector. |
| 24789 | 16 | No | Insertion of timestamp for column '%ls' failed. The allowed SQL range is from '0001-01-01' to '9999-12-31', but the provided date is '%ls'. Underlying data description: '%ls'. |
| 24790 | 16 | No | Insertion of timestamp for column '%ls' failed. The allowed SQL range for hundreds of nanoseconds from midnight is from '0' to '863999999999', but the provided value is '%ls'. Underlying data description: '%ls' |
| 24791 | 16 | No | An unexpected error occurred during the execution of the distributed temp table operation. |
| 24792 | 18 | No | Table '%ls' with classification '%ls' is not eligible for maintenance operation. |
| 24793 | 16 | No | Refresh is only available for external tables. |
| 24794 | 16 | No | Type mismatch between table schema and physical metadata. |
| 24795 | 16 | No | Unexpected physical metadata file name %ls. |
| 24796 | 16 | No | Refresh cannot be called inside an existing transaction. |
| 24797 | 18 | No | Table in database '%d' with object ID '%d' and classification '%ls' is not a valid DML target. |
| 24798 | 18 | No | Internal error. Unable to interact with an internal metadata table. Please try the operation again and contact Customer Support Services if this persists. |
| 24799 | 16 | No | Cloning an external table is not supported. |
| 24801 | 18 | No | Internal error. Unable to interact with the internal metadata of an external table due to the following reason: %ls |
| 24802 | 16 | No | Table snapshot generation failure during file elimination using file-level statistics. NaN and Infinity are not supported. |
| 24803 | 18 | No | Encountered failure during database upgrade of internal tables. |
| 24804 | 16 | No | This stored procedure supports only one-part or two-part names. Please use either \[table\] or \[schema\].\[table\]. |
| 24805 | 16 | No | Column '%s' of type '%s' could not be validated with the underlying table. Reason: %s. |
| 24806 | 16 | No | Unexpected JSON data during parsing attempt in column type inference. Underlying data description: '%ls' |
| 24807 | 16 | No | Cannot parse unquoted JSON value in the property '%ls'. Valid unquoted values can be true, false, null, and numbers. Underlying data description: '%ls' |
| 24808 | 16 | No | Cannot convert a string value found in the JSON text to binary value because it is not Base64 encoded. |
| 24809 | 16 | No | One or more log or checkpoint files have been deleted or overwritten. No operations can be performed on the external table. Please recreate the external table. |
| 24810 | 16 | No | Updating source database is not supported. |
| 24811 | 16 | No | SOURCE_DATABASE can only be altered only for warehouse snapshots in LIVE mode. |
| 24812 | 18 | No | Setting SOURCE_DATABASE failed. |
| 24813 | 16 | No | Specifying an explicit value for the identity column '%.\*ls' in table '%.\*ls' is not supported. |
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
| 25100 | 17 | No | Storage is unavailable. |
| 25101 | 16 | No | Provided lease is broken. |
| 25102 | 16 | No | Lease was already released. |
| 25103 | 16 | No | Incompatible lease mode. |
| 25104 | 16 | No | Invalid parameter. |
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
| 25654 | 16 | No | Insufficient buffer space to copy error message. |
| 25655 | 16 | No | Internal Extended Event error: invalid message code. |
| 25656 | 16 | No | Error validating action. %ls |
| 25657 | 16 | No | Error validating predicate. %ls |
| 25658 | 16 | No | The %S_MSG name "%.\*ls" is not unique. |
| 25659 | 17 | No | The %S_MSG, "%.\*ls", encountered a configuration error during initialization. The customizable attribute %ls is used internally only. |
| 25664 | 16 | No | Internal Extended Event error: invalid package ID. |
| 25665 | 10 | No | This target does not support the NO_EVENT_LOSS event retention mode. The ALLOW_SINGLE_EVENT_LOSS retention mode is used instead. |
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
| 25757 | 20 | No | Could not stop orphan session (session which is running but has no definition in metadata) with name '%s' |
