---
author: rwestMSFT
ms.author: randolphwest
ms.date: 10/10/2024
ms.topic: include
---
| Error | Severity | Event logged | Description |
| :--- | :--- | :--- | :--- |
| 41401 | 16 | No | WSFC cluster service is offline. |
| 41402 | 16 | No | The WSFC cluster is offline, and this availability group is not available. This issue can be caused by a cluster service issue or by the loss of quorum in the cluster. |
| 41403 | 16 | No | Availability group is offline. |
| 41404 | 16 | No | The availability group is offline, and is unavailable. This issue can be caused by a failure in the server instance that hosts the primary replica or by the WSFC availability group resource going offline. |
| 41405 | 16 | No | Availability group is not ready for automatic failover. |
| 41406 | 16 | No | The availability group is not ready for automatic failover. The primary replica and a secondary replica are configured for automatic failover, however, the secondary replica is not ready for an automatic failover. Possibly the secondary replica is unavailable, or its data synchronization state is currently not in the SYNCHRONIZED synchronization state. |
| 41407 | 16 | No | Some availability replicas are not synchronizing data. |
| 41408 | 16 | No | In this availability group, at least one secondary replica has a NOT SYNCHRONIZING synchronization state and is not receiving data from the primary replica. |
| 41409 | 16 | No | Some synchronous replicas are not synchronized. |
| 41410 | 16 | No | In this availability group, at least one synchronous replica is not currently synchronized. The replica synchronization state could be either SYNCHRONIZING or NOT SYNCHRONIZING. |
| 41411 | 16 | No | Some availability replicas do not have a healthy role. |
| 41412 | 16 | No | In this availability group, at least one availability replica does not currently have the primary or secondary role. |
| 41413 | 16 | No | Some availability replicas are disconnected. |
| 41414 | 16 | No | In this availability group, at least one secondary replica is not connected to the primary replica. The connected state is DISCONNECTED. |
| 41415 | 16 | No | Availability replica does not have a healthy role. |
| 41416 | 16 | No | The role of this availability replica is unhealthy. The replica does not have either the primary or secondary role. |
| 41417 | 16 | No | Availability replica is disconnected. |
| 41418 | 16 | No | This secondary replica is not connected to the primary replica. The connected state is DISCONNECTED. |
| 41419 | 16 | No | Data synchronization state of some availability database is not healthy. |
| 41420 | 16 | No | At least one availability database on this availability replica has an unhealthy data synchronization state. If this is an asynchronous-commit availability replica, all availability databases should be in the SYNCHRONIZING state. If this is a synchronous-commit availability replica, all availability databases should be in the SYNCHRONIZED state. |
| 41421 | 16 | No | Availability database is suspended. |
| 41422 | 16 | No | Either a database administrator or the system has suspended data synchronization on this availability database. |
| 41423 | 16 | No | Secondary database is not joined. |
| 41424 | 16 | No | This secondary database is not joined to the availability group. The configuration of this secondary database is incomplete. For information about how to join a secondary database to an availability group, see SQL Server Books Online. |
| 41425 | 16 | No | Data synchronization state of availability database is not healthy. |
| 41426 | 16 | No | The data synchronization state of this availability database is unhealthy. On an asynchronous-commit availability replica, every availability database should be in the SYNCHRONIZING state. On a synchronous-commit replica, every availability database should be in the SYNCHRONIZED state. |
| 41427 | 16 | No | Availability replica is not joined. |
| 41428 | 16 | No | This secondary replica is not joined to the availability group. For an availability replica to be successfully joined to the availability group, the join state must be Joined Standalone Instance (1) or Joined Failover Cluster (2). For information about how-to join a secondary replica to an availability group, see SQL Server Books Online. |
| 41500 | 10 | No | An error (0x%08x) occurred when asynchronous operations administrator attempted to notify the client (ID %ls) of the completion of an operation. This is an information message only. No user action is required. |
| 41501 | 16 | No | Failed to register client (ID %ls) with asynchronous operations administrator. A client with this ID has already been registered. Check that the specified client ID is correct, then retry the operation. To re-register a client, the client must first be deregistered. |
| 41502 | 16 | No | Failed to deregister client (ID %ls) from asynchronous operations administrator. The client has not registered with the administrator. Check that the specified client ID is correct, then retry the operation. |
| 41503 | 16 | No | Client (ID %ls) failed to submit work to asynchronous operations administrator. The client has not registered with the administrator. Check that the specified client ID is correct, then retry the operation. |
| 41504 | 16 | No | Asynchronous operations administrator failed to allocate a work item for the work submitted by client (ID %ls). The administrator may have exhausted all available resources. If this condition persists, contact the system administrator. |
| 41505 | 16 | No | Asynchronous operations administrator failed to queue a work item for the work submitted by client (ID %ls) (internal error %d). The administrator may have exhausted all available resources. If this condition persists, contact the system administrator. |
| 41600 | 16 | No | An error has occurred while executing an asynchronous operation for a database replica (Windows Fabric partition ID %ls, operation %d, error 0x%08x). Refer to the error code for more details. If this condition persists, contact the system administrator. |
| 41601 | 16 | No | Valid state transition is not found for local replica with partition ID %ls (current state %ls, trigger %ls, current epoch \[%I64d,%I64d\], triggering epoch \[%I64d,%I64d\]). The replica is not in the correct state to accept the Windows Fabric command. If this condition persists, contact the system administrator. |
| 41602 | 16 | No | An error has occurred while attempting to access replica publisher's subscriber list (partition ID %ls, SQL OS error code 0x%08x). Refer to the error code for more details. If this condition persists, contact the system administrator. |
| 41603 | 16 | No | The transport subscriber failed to process the build secondary replica event (partition ID %ls). If this condition persists, contact the system administrator. |
| 41604 | 16 | No | The transport subscriber failed to process the configuration change replica event (partition ID %ls). If this condition persists, contact the system administrator. |
| 41605 | 16 | No | Cannot associate replica (Windows Fabric replica ID 0x%08X) with the specified Windows Fabric partition (ID %ls). The replica is already associated with a Windows Fabric partition (ID %ls). If this condition persists, contact the system administrator. |
| 41606 | 16 | No | Replica (Windows Fabric replica ID 0x%08X, current state '%ls') cannot process configuration-update command for Windows Fabric partition (ID %ls). Configuration updates can be process by the primary replica only. If this condition persists, contact the system administrator. |
| 41607 | 16 | No | Operation timed out while waiting for %ls access to the cached information in the replica controller (Windows Fabric replica ID 0x%08X, partition ID %ls). If this condition persists, contact the system administrator. |
| 41608 | 16 | No | Failed to obtain %ls access to the cached information in the replica controller (Windows Fabric replica ID 0x%08X, partition ID %ls, SQL OS error %d). The operation may have been aborted. Refer to the SQL OS error number for details. If this condition persists, contact the system administrator. |
| 41609 | 16 | No | Operation timed out while waiting for %ls access to the list of replica controller objects. If this condition persists, contact the system administrator. |
| 41610 | 16 | No | Failed to obtain %ls access to the list of replica controller objects (SQL OS error %d). The operation may have been aborted. Refer to the SQL OS error number for details. If this condition persists, contact the system administrator. |
| 41611 | 16 | No | Replica controller for the local replica (Availability Group ID %ls) cannot be found. Make sure the specified Availability Group ID is correct, then retry the operation. If this condition persists, contact the system administrator. |
| 41612 | 16 | No | An error has occurred while %ls %ls database (SQL Error Code: %d). Refer to the SQL error code for more details. If this condition persists, contact the system administrator. |
| 41613 | 17 | No | Fabric Service '%ls' failed to perform database operation '%ls' on '%ls' database (ID %d). The database might be in an incorrect state for the operation. If this condition persists, contact the system administrator. |
| 41614 | 10 | No | Fabric Service '%ls' encountered a transient error while performing Windows Fabric operation on '%ls' database (ID %d). Refer to the SQL Server error log for information about the errors that were encountered. If this condition persists, contact the system administrator. |
| 41615 | 10 | No | Fabric Service '%ls' encountered a permanent error while performing a Windows Fabric operation on '%ls' database (ID %d). Refer to the SQL Server error log for information about the errors that were encountered. If this condition persists, contact the system administrator. |
| 41616 | 16 | No | SQL Server cannot find the configuration of the replica with ID %ls (Windows Fabric partition ID %ls). Make sure the specified Windows Fabric partition ID and replica ID are correct, then retry the operation. If this condition persists, contact the system administrator. |
| 41617 | 10 | No | Fabric Service '%ls' is unable to find out start of log and end of log LSN for '%ls' database. Refer to the SQL Server error log for information about the errors that were encountered. If this condition persists, contact the system administrator. |
| 41618 | 16 | No | Attempt to access non-existent or uninitialized Windows Fabric partition '%ls'. This is usually an internal condition, such as the Windows Fabric service is getting initialized or it is getting destroyed. |
| 41619 | 16 | No | Windows Fabric '%ls' (partition ID '%ls')encountered transient error %d while waiting for build replica operation on database '%ls' (ID %d). Refer to the SQL Server error log for information about the errors that were encountered. If this condition persists, contact the system administrator. |
| 41620 | 10 | No | Build replica operation on database '%ls' (ID %d) of Windows Fabric partition '%ls' (partition ID '%ls') has been cancelled by Windows Fabric. Windows Fabric cancelled build replica operation. This is an informational message only. No user action is required. |
| 41621 | 10 | No | Windows Fabric partition '%ls' (partition ID '%ls') encountered error '%ls' and is reporting '%ls' failure to Windows Fabric. Refer to the SQL Server error log for information about the errors that were encountered.. If this condition persists, contact the system administrator. |
| 41622 | 16 | No | Windows Fabric service '%ls' (partition ID '%ls') is trying to update primary replica information for local replica %ls which is neither ACTIVE_SECONDARY nor IDLE_SECONDARY (current role %ls). SQL Server cannot update primary replica information in invalid state. This is an informational message only. No user action is required. |
| 41623 | 20 | No | The Database Mirroring endpoint port is unavailable. Verify that the DBM endpoint is created. |
| 41624 | 16 | No | Drop database '%ls' (ID %d) of Windows Fabric partition '%ls' (partition ID '%ls') has failed. SQL Server has failed to drop the database. If this condition persists, contact the system administrator. |
| 41625 | 20 | No | The internal buffer for the replication URL is insufficient. |
| 41626 | 10 | No | Failed to retrieve service desription from Windows Fabric for partition '%ls' (Windows Fabric error 0x%08x). If this condition persists, contact the system administrator. |
| 41627 | 10 | No | An error has occurred while Dropping %ls database (SQL Error Code: %d). Refer to the SQL error code for more details. If this condition persists, contact the system administrator. |
| 41628 | 10 | No | Drop database Timer task encountered an error (SQL Error Code: %d). Refer to the SQL error code for more details. If this condition persists, contact the system administrator. |
| 41629 | 10 | No | Fabric replica publisher encountered an error (SQL Error Code: %d, State: %d) while publishing event '%ls' to subscriber of type '%ls' on Fabric partition '%ls' (partition ID: '%ls'). Refer to the SQL error code for more details. If this condition persists, contact the system administrator. |
| 41630 | 10 | No | Failed to update primary replica information for partition '%ls' (Windows Fabric error 0x%08x). This is an informational message only. No user action is required. |
| 41631 | 16 | No | Fabric service '%ls' failed to retrieve a known hardware sku while performing a build replica operation on '%ls' database (ID %d). Refer to the cluster manifest to ensure a valid SKU is defined for this node type. If this condition persists, contact the system administrator. |
| 41632 | 10 | No | The system encountered SQL Error %d (severity: %d, state: %d), which has no corresponding error text. Refer to the SQL Error number for more information regarding the cause and corrective action. |
| 41633 | 16 | No | Fabric Service '%ls' (partition ID '%ls') is unable to allocate a work item for the database restart of '%ls' database (ID %d). The administrator may have exhausted all available resources. If this condition persists, contact the system administrator. |
| 41634 | 10 | No | Open replica operation on database '%ls' (ID %d) of Windows Fabric partition ID '%ls' has been cancelled. This is an informational message only. No user action is required. |
| 41635 | 10 | No | Open replica operation on database '%ls' (ID %d) of Windows Fabric partition ID '%ls' failed. For more information, see the SQL Server error log. This is an informational message only. No user action is required. |
| 41636 | 16 | No | Fabric Service '%ls' (partition ID '%ls') is unable to enqueue a work item for the database restart of '%ls' database (ID %d). The administrator may have exhausted all available resources. If this condition persists, contact the system administrator. |
| 41637 | 16 | No | The database '%ls' (URI: '%ls', partition ID '%ls') is not currently participating in a GeoDR relationship. |
| 41638 | 16 | No | Could not retrieve remote replica storage configuration for database '%ls' (URI: '%ls'). |
| 41639 | 16 | No | Could not retrieve remote replica configuration for database '%ls' (URI: '%ls'). |
| 41640 | 10 | No | Database '%ls' encountered a transient error (error code: 0x%08X) while performing task '%ls'. Refer to the SQL Server error log for information about the errors that were encountered. If this condition persists, contact the system administrator. |
| 41641 | 16 | No | Could not send global cluster action '%ls' request 0x%x. |
| 41642 | 10 | No | Undo of redo is run in Active Secondary role on database '%ls' (ID %d). Recovery lsn: '%S_LSN', Hardened lsn: '%S_LSN'. If this condition persists, contact the system administrator. |
| 41643 | 16 | No | Could not retrieve the Distributed AG Configuration for database '%ls' (URI: '%ls', partition ID '%ls') . Encountered error (error code: 0x%08X). |
| 41644 | 17 | No | Fabric Service '%ls' failed to perform database operation '%ls' on database '%ls'. The database might be in an incorrect state for the operation. If this condition persists, contact the system administrator. |
| 41645 | 16 | No | Fabric Service '%ls' (partition ID '%ls') encountered error (error code: 0x%08X) while querying for Fabric property '%ls'. |
| 41646 | 16 | No | Invalid Fabric property '%ls' received for partition '%ls'. |
| 41647 | 17 | No | Failed to start the report fault thread during replica manager startup. |
| 41648 | 10 | No | Get current progress was called on '%ls' database (ID %d) which had undo of redo pending. Refer to the SQL Server error log for information about the errors that were encountered. If this condition persists, contact the system administrator. |
| 41649 | 10 | No | Could not submit Change Role completion tasks for %ls' (URI: '%ls', partition ID '%ls') . Encountered error (error code: 0x%08X). |
| 41650 | 10 | No | Waiting for replica catchup for AGID '%ls' and ReplicaID '%ls' Failed. |
| 41651 | 16 | No | Invalid partition id in replica manager |
| 41652 | 17 | No | The replica manager is unavailable or not ready. |
| 41653 | 21 | No | Database '%.\*ls' encountered an error (error type: %d '%.\*ls') causing failure of the availability group '%.\*ls'. Refer to the SQL Server error log for information about the errors that were encountered. If this condition persists, contact the system administrator. |
| 41654 | 17 | No | Failed to start the clean up nonexistant DBs thread during replica manager startup. |
| 41655 | 10 | No | Could not submit logical reseeding task for '%ls' (URI: '%ls', partition id: '%ls') . Encountered error (error code: 0x%08X). |
| 41656 | 17 | No | Failed to start the windows fabric load balancer reporting thread during replica manager startup. |
| 41657 | 16 | No | Database '%ls' (ID %d) of Windows Fabric partition '%ls' (partition ID '%ls') failed the call to UseDB. |
| 41658 | 16 | No | Failed to automatically enable Query Store in Database '%ls' (ID %d) of Windows Fabric partition '%ls' (partition ID '%ls') . |
| 41659 | 16 | No | Checkpoint for Database '%ls' (ID %d) failed. Encountered error (error code: 0x%08X). |
| 41660 | 16 | No | Windows Fabric service '%ls' (partition ID '%ls') received a primary replica information message from remote replica '%ls' with an epoch \[%I64d,%I64d\] which is less than the local epoch \[%I64d,%I64d\]. SQL Server cannot update primary replica information from a replica with a smaller epoch. This is an informational message only. No user action is required. |
| 41661 | 16 | No | There are no waiters on the DataLossEvent for Fabric Service '%ls' (partition ID '%ls'). |
| 41662 | 16 | No | Database '%ls' (ID %d) of Windows Fabric partition '%ls' (partition ID '%ls') hit exception while running async tasks in Generic Subscriber. |
| 41663 | 10 | No | Failed to parse datawarehouse columnar cache settings during replica manager startup. |
| 41664 | 10 | No | Failed to refresh remote replica configuration for fabric service '%ls'. |
| 41665 | 10 | No | Failed to resolve DW logical node id for physical database '%ls', which is hosted by compute service: '%ls'. |
| 41666 | 16 | No | Waiting for replica catchup before GeoDR role change failed for with error %d for database '%ls', DBID %d, AGID '%ls', and ReplicaID '%ls'. |
| 41667 | 16 | No | Fabric Service '%ls' (partition ID '%ls') encountered error (error code: 0x%08X) while setting Fabric property '%ls'. |
| 41668 | 16 | No | Failed to transition to forwarder role for physical database '%ls' (ID %d) of Windows Fabric partition '%ls' (partition ID '%ls'). |
| 41669 | 16 | No | Database '%ls' (ID %d) of Windows Fabric partition '%ls' (partition ID '%ls') hit exception while running async tasks in RbIo Subscriber. |
| 41670 | 16 | No | Cannot retrieve tempdb remote file lease order id. |
| 41700 | 16 | No | System views related to Windows Fabric partitions and replicas are not available at this time, because replica manager has not yet started. Wait for replica manager to start, then retry the system view query. |
| 41701 | 20 | No | The Activation Context is unavailable at this time. The Windows Fabric Runtime is unavailable at this time, retry later. Wait for the activation context to become available, then retry. |
| 41702 | 20 | No | The requested Configuration Package is unavailable at this time. The Configuration Package is not a part of the Activation Context. Verify that the requested Configuration Package name exists and is properly formatted. |
| 41703 | 20 | No | The requested Service Endpoint is unavailable at this time. The Service Endpoint is not a part of the Activation Context. Verify that the requested Service Endpoint name exists and is properly formatted. |
| 41704 | 20 | No | The datasource name is not correctly formatted. The datasource name exceeds the maximum path length or does not adhere to defined format. Verify that the datasource is name is fewer than MAX_PATH characters in length is properly formatted. |
| 41705 | 20 | No | The computer name is unavailable. The computer name was not returned. |
| 41706 | 20 | No | Unable to get Fabric NodeContext. |
| 41801 | 16 | No | Failed to drop the memory optimized container '%.\*ls'. |
| 41802 | 16 | No | Cannot drop the last memory-optimized container '%.\*ls'. |
| 41803 | 16 | No | An In-Memory OLTP physical database was restarted while processing log record ID %S_LSN for database '%.\*ls'. No further action is necessary. |
| 41804 | 16 | No | Internal error for database '%.\*ls' (lookup for HkTruncationLsn failed). The operation will be retried. No user action is required. If the problem persists, contact customer support. |
| 41805 | 16 | No | There is insufficient memory in the resource pool '%ls' to run this operation on memory-optimized tables. See '[http://go.microsoft.com/fwlink/?LinkID=614951](https://go.microsoft.com/fwlink/?LinkID=614951)' for more information. |
| 41806 | 16 | No | Parameter "%ls" specified for procedure or function "%ls" is not valid. |
| 41807 | 16 | No | A MARS batch failed due to a unique constraint violation on a memory optimized table. |
| 41808 | 16 | No | The current MARS batch attempted to update a record that has been updated by another batch within the same transaction. |
| 41809 | 16 | No | Natively compiled triggers do not support statements that output a result set. |
| 41810 | 16 | No | Stored procedures called from natively compiled triggers do not support statements that output a result set. |
| 41811 | 16 | No | XTP physical database was stopped while processing log record ID %S_LSN for database '%.\*ls'. No further action is necessary. |
| 41812 | 16 | No | ALTER TABLE on memory-optimized tables is not supported with concurrent MARS transactions. |
| 41813 | 16 | No | XTP database '%.\*ls' was undeployed. No further action is necessary. |
| 41814 | 16 | No | The procedure '%.\*ls' cannot be called from a user transaction. |
| 41815 | 16 | No | Data migration on table id %d cannot be performed because the table is already in the process of migration. |
| 41816 | 16 | No | The parameter '%.\*ls' for procedure '%.\*ls' cannot be NULL. |
| 41817 | 16 | No | Invalid object id %d provided as input for procedure '%.\*ls'. The object id must refer to a memory-optimized table with a column store index. |
| 41818 | 23 | No | An upgrade operation failed for database '%.\*ls' attempting to upgrade the XTP component from version %u.%u to version %u.%u. Check the error log for further details. |
| 41819 | 16 | No | A MARS batch failed due to a validation failure for a foreign key constraint on memory optimized table '%.\*ls'. Another interleaved MARS batch deleted or updated a row that was referenced by a foreign key row inserted by the failed batch. |
| 41820 | 16 | No | A MARS batch failed due to a validation failure for a foreign key constraint on memory optimized table '%.\*ls'. Another interleaved MARS batch inserted a row that references the row that was deleted by the failed batch. |
| 41822 | 17 | No | There is insufficient disk space to generate checkpoint files and as a result the database is in delete-only mode. In this mode, only deletes and drops are allowed. |
| 41823 | 16 | No | Could not perform the operation because the database has reached its quota for in-memory tables. This error may be transient. Please retry the operation. See '[http://go.microsoft.com/fwlink/?LinkID=623028](https://go.microsoft.com/fwlink/?LinkID=623028)' for more information. |
| 41824 | 16 | No | The transaction was killed by a concurrent ALTER operation or by a write-write conflict. |
| 41825 | 16 | No | Stored procedure '%.\*ls' cannot be used to increase the user memory limit on the database. |
| 41826 | 16 | No | Stored procedure '%.\*ls' tried to set a lower limit to the user memory quota. The operation failed because the user memory consumption is larger than the specified target, delete some user data and try the operation again. |
| 41827 | 16 | No | Upgrade of XTP physical database '%.\*ls' requires restart of XTP engine. |
| 41828 | 16 | No | Creation of memory-optimized tables is temporarily disabled. Please try again later. |
| 41829 | 16 | No | The database cannot proceed with pricing-tier update as it has memory-optimized objects. Please drop such objects and try again. |
| 41830 | 16 | No | Upgrade of XTP physical database '%.\*ls' restarted XTP engine. |
| 41831 | 16 | No | Data migration on table id %d failed either fully or partially. See errorlog for details. |
| 41832 | 16 | No | Index '%.\*ls' cannot be created on table '%.\*ls', because at least one key column is stored off-row. Index key columns memory-optimized tables must fit within the %d byte limit for in-row data. Simplify the index key or reduce the size of the columns to fit within %d bytes. |
| 41833 | 16 | No | Columnstore index '%.\*ls' cannot be created, because table '%.\*ls' has columns stored off-row. Columnstore indexes can only be created on memory-optimized table if the columns fit within the %d byte limit for in-row data. Reduce the size of the columns to fit within %d bytes. |
| 41834 | 16 | No | ALTER TABLE has failed for '%.\*ls' with error code %d. |
| 41835 | 21 | No | An error (error code: 0x%08lx) occurred while adding encryption keys to XTP database '%.\*ls'. |
| 41836 | 16 | No | Rebuilding log is not supported for databases containing files belonging to MEMORY_OPTIMIZED_DATA filegroup. |
| 41837 | 16 | No | Boot-page adjustment of XTP database '%.\*ls' requires restart of XTP engine. |
| 41838 | 16 | No | Failed to retrieve size for this file due to an internal error. Please try again later. |
| 41839 | 16 | No | Transaction exceeded the maximum number of commit dependencies and the last statement was aborted. Retry the statement. |
| 41840 | 16 | No | Could not perform the operation because the elastic pool has reached its quota for in-memory tables. This error may be transient. Please retry the operation. See '[http://go.microsoft.com/fwlink/?LinkID=623028](https://go.microsoft.com/fwlink/?LinkID=623028)' for more information. |
| 41841 | 23 | No | Found inconsistent boot-page for database '%.\*ls'. |
| 41842 | 16 | No | Too many rows inserted or updated in this transaction. You can insert or update at most 4,294,967,294 rows in memory-optimized tables in a single transaction. |
| 41843 | 16 | No | Unable to construct segment for segment table. |
| 41844 | 15 | No | Clustered columnstore indexes are not supported on memory optimized tables with computed columns. |
| 41845 | 16 | No | Checksum verification failed for memory optimized checkpoint file %.\*ls. |
| 41846 | 16 | No | Memory optimized checkpoint table consistency error detected. Checkpoint %I64d does not have unique recoverLsn. PrevLSN = (%I64d:%hu), CurrLSN = (%I64d:%hu). |
| 41847 | 16 | No | Memory optimized checkpoint table consistency error detected. Checkpoint %I64d does not point to a transaction segment definition record. |
| 41849 | 16 | No | Memory optimized segment table consistency error detected. Segments are not contiguous in logical space. Older Segment CkptId = %I64d, LsnInfo = (%I64d:%hu), TxBegin = %I64d, TxEnd = %I64d. Newer Segment CkptId = %I64d, LsnInfo = (%I64d:%hu), TxBegin = %I64d, TxEnd = %I64d. |
| 41850 | 16 | No | Memory optimized segment table consistency error detected. Segments are not well formed for Segment CkptId = %I64d, LsnInfo = (%I64d:%hu), TxBegin = %I64d, TxEnd = %I64d. |
| 41851 | 16 | No | Memory optimized segment table consistency error detected. Segment definition ordering does not match the (strict) logical ordering. Older Segment CkptId = %I64d, LsnInfo = (%I64d:%hu), TxBegin = %I64d, TxEnd = %I64d. Newer Segment CkptId = %I64d, LsnInfo = (%I64d:%hu), TxBegin = %I64d, TxEnd = %I64d. |
| 41852 | 16 | No | Memory optimized segment table consistency error detected. Segment has a NullHkLsn. CkptId = %I64d, Segment LsnInfo = (%I64d:%hu) |
| 41853 | 16 | No | Memory optimized segment table consistency error detected. Current segment goes backward further than the definition record of the N-2 segment. Older Segment CkptId = %I64d, LsnInfo = (%I64d:%hu), TxBegin = %I64d, TxEnd = %I64d. Newer Segment CkptId = %I64d, LsnInfo = (%I64d:%hu), TxBegin = %I64d, TxEnd = %I64d. |
| 41854 | 16 | No | Memory optimized file consistency error detected. An in use file with FileId %.\*ls is referenced by the Checkpoint File Table but is not accounted for in the Storage Interface. |
| 41855 | 16 | No | Memory optimized file consistency error detected. Could not find a file with FileId %.\*ls in the File Watermark Table visible by checkpoint ID %u. |
| 41856 | 16 | No | Memory optimized file consistency error detected. A file with FileId %.\*ls of size %I64d bytes is smaller than expected %I64d bytes. |
| 41861 | 16 | No | Memory optimized large rows table consistency error detected. File Id is %.\*ls. Corresponding LSN range is not ascending. Begin Lsn is (%I64d:%hu), End Lsn is (%I64d:%hu). |
| 41862 | 16 | No | Memory optimized large rows table consistency error detected. Corresponding File %.\*ls not found in File Table. Begin Lsn is (%I64d:%hu), End Lsn is (%I64d:%hu). |
| 41863 | 16 | No | Checkpoint %I64d points to a root file %.\*ls which was in use by a more recent checkpoint. |
| 41864 | 16 | No | Checkpoint %I64d has a file %.\*ls which has a watermark (%I64d) larger than the more recent checkpoints watermark (%I64d). |
| 41865 | 16 | No | File %.\*ls does not have a pair file %.\*ls |
| 41866 | 16 | No | Unprocessed File %.\*ls does not have a pair file which is also unprocessed. Processed pair file is %.\*ls. |
| 41867 | 16 | No | Consistency errors detected in the MEMORY_OPTIMIZED_DATA filegroup. See preceding error messages for details. Consult [https://go.microsoft.com/fwlink/?linkid=845604](https://go.microsoft.com/fwlink/?linkid=845604) for details on how to recover from the errors. |
| 41868 | 16 | No | Memory optimized filegroup checks could not be completed because of system errors. See errorlog for more details. |
| 41869 | 21 | No | In-memory OLTP version %u.%u is not supported on this platform. |
| 41870 | 10 | No | Dropped %d Orphan Internal Table(s). |
| 41871 | 16 | No | Failed to recreate XTP non-durable tables during recovery of the database '%.\*ls'. |
| 41872 | 10 | No | Warning: Articles with varchar(max), nvarchar(max) and varbinary(max) data type columns are not supported with memory optimized tables on subscribers running SQL Server 2014 or earlier. |
| 41901 | 16 | No | One or more of the options (%ls) are not supported for this statement in SQL Database Managed Instance. Review the documentation for supported options. |
| 41902 | 16 | No | Unsupported device type. SQL Database Managed Instance supports database restore from URI backup device only. |
| 41903 | 16 | No | FILENAME option is not allowed in SQL Database Managed Instance. |
| 41904 | 16 | No | BACKUP DATABASE failed. SQL Database Managed Instance supports only COPY_ONLY full database backups which are initiated by user. |
| 41905 | 16 | No | Stored procedure %ls is not supported in SQL Database Managed Instance. |
| 41906 | 16 | No | Statement '%.\*ls' is not supported in SQL Database Managed Instance. |
| 41907 | 16 | No | Unsupported file type during restore on SQL Database Managed Instance. |
| 41908 | 16 | No | Only 'local' routes are supported in SQL Database Managed Instance. |
| 41909 | 16 | No | Modifying logical file name is not supported in SQL Database Managed Instance. |
| 41910 | 16 | No | Add/remove/modify of log files is not supported in SQL Database Managed Instance. |
| 41911 | 16 | No | Adding or removing XTP file or filegroup is not allowed in SQL Database Managed Instance. |
| 41912 | 16 | No | Maximum number of databases for SQL Database Managed Instance reached. |
| 41913 | 16 | No | Multiple filestream files are not supported in SQL Database Managed Instance. |
| 41914 | 16 | No | SQL Server Agent feature %ls is not supported in SQL Database Managed Instance. Review the documentation for supported options. |
| 41915 | 16 | No | Memory-optimized filegroup must be empty in order to be restored on the current tier of SQL Database Managed Instance. |
| 41916 | 16 | No | Maximum number of %u files for SQL Database Managed Instance reached. |
| 41917 | 16 | No | Dropping local instance via sp_dropserver is not allowed in SQL Database Managed Instance. |
| 41918 | 16 | No | Specifying files and filegroups in CREATE DATABASE statement is not supported on SQL Database Managed Instance. |
| 41919 | 16 | No | Multiple backup sets in a single backup file are not supported in SQL Database Managed Instance. |
| 41920 | 16 | No | This feature is not supported through T-SQL on SQL Database Managed Instance. |
| 41921 | 16 | No | Restoring from a backup that contains multiple log files is not supported in SQL Database Managed Instance. |
| 41922 | 16 | No | The backup operation for a database with service-managed transparent data encryption is not supported on SQL Database Managed Instance. |
| 41923 | 16 | No | Cannot find server certificate with thumbprint '%.\*ls'. Please use PowerShell Cmdlet 'Add-AzureRmSqlManagedInstanceTransparentDataEncryptionCertificate' to create the certificate. |
| 41924 | 16 | No | All files need to be smaller or equal to %d MB in this edition of Managed Instance. File id: %ld. Size: %I64d bytes. |
| 41925 | 16 | No | If DB has multiple data files, all files need to have max size larger than %d MB in this edition of Managed Instance. File id: %ld. Size: %I64d bytes. |
| 41926 | 16 | No | Secondary filegroup is not supported in this edition of Managed Instance. File id: %ld. |
| 41927 | 16 | No | The file name 'XTP' is reserved for the files containing In-Memory OLTP data. |
| 41928 | 16 | No | The filegroup name 'XTP' is reserved for the filegroup containing In-Memory OLTP data. |
| 41929 | 16 | No | One or more files are not in online state after the restore. |
| 41930 | 16 | No | Modifying number of files and max size for TempDb is not yet supported in SQL Database Managed Instance. |
| 41931 | 10 | No | Warning: New name of managed database '%.\*ls' is same as the old one. |
| 41932 | 16 | No | Cannot rename managed database '%.\*ls' because it's currently in use. |
| 41933 | 21 | No | Failed to initialize detours and local time for Managed Instance. %ls |
| 41934 | 16 | No | Statement '%.\*ls' is not supported in the current version of the server. |
| 41935 | 16 | No | Managed Instance has reached the total capacity of underlying Azure storage account. Azure Premium Storage account is limited to 35TB of allocated space. |
| 41936 | 16 | No | This feature is not supported in the Hyperscale edition of SQL Database Managed Instance. |
| 41978 | 16 | No | The parameters (%ls) are not supported for this stored procedure in SQL Database Managed Instance. Review the documentation for supported parameters. |
| 42001 | 16 | No | Failed to parse XML configuration. The operating system returned error %ls. |
| 42002 | 16 | No | Failed to parse XML configuration. The parser returned error %.\*ls |
| 42003 | 16 | No | Failed to parse XML configuration. A required attribute '%ls' is missing. |
| 42004 | 16 | No | Failed to parse XML configuration. A required element '%ls' is missing. |
| 42005 | 16 | No | Failed to parse XML configuration. Invalid value for attribute '%ls'. |
| 42006 | 16 | No | The default tempdb directory ('%ls') in XDB is not a valid path. |
| 42007 | 16 | No | The default tempdb directory ('%ls') in XDB is not a local path. |
| 42008 | 16 | No | ODBC error: State: %ls: Error: %d Message:'%ls'. |
| 42009 | 16 | No | Instance certificate '%ls' cannot be found. |
| 42010 | 16 | No | Cannot initiate cross instance connection. |
| 42011 | 16 | No | ODBC initialization error: : %d. |
| 42012 | 16 | No | XodbcWrapper Enforced Retry For Testing. |
| 42013 | 16 | No | HTTP initialization error: : %d. |
| 42014 | 16 | No | Cannot retrieve server admin credential configuration. |
| 42015 | 20 | No | Error occurred while attempting to authenticate user remotely. Error %d, State %d. |
| 42016 | 16 | No | Error occurred in the DosGuard. |
| 42017 | 16 | No | Error occurred in the Redirector's proxy while parsing a packet. Expected: %d, Actual: %d. |
| 42018 | 16 | No | Remote transaction has been doomed and cannot commit. |
| 42019 | 16 | No | %ls operation failed. %ls |
| 42020 | 16 | No | Updating name to '%.\*ls' failed. |
| 42021 | 16 | No | Initialization of http session handle for fetching federation metadata failed during AzureActiveDirectory initialization. |
| 42022 | 16 | No | Initialization of http connect handle for fetching federation metadata failed during AzureActiveDirectory initialization. |
| 42023 | 16 | No | Updating userdb properties on copy termination failed. |
| 42024 | 16 | No | Switching to logical master database failed. |
| 42025 | 16 | No | The extended recovery fork stack in the metadata of FCB is either corrupted or in an unexpected format. |
| 42026 | 16 | No | Loading the Active Directory Library failed. |
| 42027 | 16 | No | Initialization of the Active Directory Function pointers failed. |
| 42028 | 16 | No | The database '%.\*ls' has been detached as it was marked as a shared disk instance and was not started. This is an informational message; no user action is required. |
| 42029 | 16 | No | An internal error happened while generating a new DBTS for database "%.\*ls". Please retry the operation. |
| 42030 | 16 | No | Updating logical master's sys.databases on seeding completion failed. |
| 42031 | 16 | No | This functionality is not supported for A/B test isolated instances. |
| 42032 | 16 | No | XODBC Get Authentication Cache failed, state %d |
| 42033 | 16 | No | Updating Distributed AG Configuration failed. |
| 42034 | 16 | No | Boot of federation host failed with error 0x%08X. |
| 43001 | 16 | No | '%.\*ls' is not a valid login name. |
| 43002 | 16 | No | The storage size of %d MB exceeds the maximum allowed storage of %d MB. |
| 43003 | 16 | No | More than one firewall rules have the same name '%.\*ls'. |
| 43004 | 16 | No | '%.\*ls' is not a valid firewall rule name because it contains invalid characters. |
| 43005 | 16 | No | '%.\*ls' is not a valid database name because it contains invalid characters. |
| 43006 | 16 | No | Database name '%.\*ls' is too long. |
| 43007 | 16 | No | '%.\*ls' is not a valid database charset. |
| 43008 | 16 | No | '%.\*ls' is not a valid database collation. |
| 43009 | 16 | No | The storage size of %d MB does not meet the minimum required storage of %d MB. |
| 43010 | 16 | No | '%.\*ls' cannot be updated. |
| 43011 | 16 | No | '%.\*ls' is not a valid version. |
| 43012 | 16 | No | The configuration name cannot be empty. |
| 43013 | 16 | No | The value for configuration '%.\*ls' cannot be empty. |
| 43014 | 16 | No | The same configuration '%.\*ls' cannot be updated more than once. |
| 43015 | 16 | No | The configuration '%.\*ls' does not exist for %.\*ls server version %.\*ls. |
| 43016 | 16 | No | The value '%.\*ls' for configuration '%.\*ls' is not valid. The allowed values are '%.\*ls'. |
| 43017 | 16 | No | The configuration names you defined are not consistent. |
| 43018 | 16 | No | The value '%.\*ls' for configuration '%.\*ls' is not consistent with default value '%.\*ls'. |
| 43019 | 16 | No | The source '%.\*ls' for configuration '%.\*ls' is not valid. |
| 43020 | 16 | No | The storage size of %d MB is not a valid configuration. Valid storage sizes range from minimum of %d MB and additional increments of %d MB up to maximum of %d MB. |
| 43021 | 16 | No | The point in time %S_DATE is not valid. Valid point in time range from %d days early to now and not before source server creation time. |
| 43022 | 16 | No | The edition %.\*ls is not a valid edition. Edition cannot be changed by restoring. |
| 43023 | 16 | No | The storage size of %d MB is lower than the source storage size. |
| 43024 | 16 | No | The version %.\*ls is not a valid version. Version cannot be changed by restoring. |
| 43025 | 16 | No | Input parameter is incorrect. Please double check the input. |
| 43026 | 16 | No | Cannot drop system database '%.\*ls', skipping. |
| 43027 | 16 | No | Geo Restore is not supported. |
| 43028 | 16 | No | The replication feature is not supported. |
| 43029 | 16 | No | Edition %.\*ls must be the same as the primary server when creating a replica server. |
| 43030 | 16 | No | Version %.\*ls must be the same as the primary server when creating a replica server. |
| 43031 | 16 | No | The storage size of %d MB is lower than the primary server's storage size. |
| 43032 | 16 | No | No available primary server %.\*ls is found when creating a replica server. |
| 43033 | 16 | No | The primary server %.\*ls already has the maximum of replica servers. |
| 43034 | 16 | No | The replica server is not in ready state when promoting. |
| 43035 | 16 | No | '%.\*ls' and '%.\*ls' cannot be updated together. |
| 43036 | 16 | No | Input parameter is incorrect when creating a replica server. Please double check the input. |
| 43037 | 16 | No | An internal error was encountered when processing the restore request. This request has been assigned a tracing ID of '%.\*ls'. Message is '%.\*ls', and details are '%.\*ls'. Provide this tracing ID/Message/Details to customer support when you need assistance. |
| 45001 | 16 | No | %ls operation failed. Specified federation name does not exist. |
| 45002 | 16 | No | %ls operation failed. Specified federation distribution name %.\*ls is not valid. |
| 45003 | 16 | No | %ls operation failed. Specified data type is not supported as a federation distribution. |
| 45004 | 16 | No | %ls operation failed. Specified value is not valid for federation distribution %.\*ls and federation %.\*ls. |
| 45005 | 16 | No | Filter value cannot be set or was already set for this session. |
| 45006 | 16 | No | The federation key value is out of bounds for this member. |
| 45007 | 16 | No | %ls cannot be run while another federation operation is in progress on federation %.\*ls and member with id %d. |
| 45008 | 16 | No | A partition in a table group that has a partition key defined is not allowed to be a federation member. |
| 45014 | 16 | No | %ls is not supported on %S_MSG. |
| 45015 | 16 | No | Specified federation operation id is already in use. |
| 45016 | 16 | No | Specified federation %.\*ls does not exist. |
| 45017 | 16 | No | %ls operation failed. Specified federation name %.\*ls is not valid. |
| 45018 | 16 | No | Specified federation operation id is invalid for %ls operation. |
| 45019 | 16 | No | %ls operation failed. Federation is in invalid state. |
| 45020 | 16 | No | %ls operation failed. %d is not a valid federation id. |
| 45021 | 16 | No | %ls operation failed. %d is not a valid federation member id. |
| 45022 | 16 | No | A column insert or update conflicts with a federation member range. The statement was terminated. The conflict occurred in database '%.\*ls', table '%.\*ls', column '%.\*ls'. |
| 45023 | 16 | No | %ls cannot be called on %S_MSG. |
| 45024 | 16 | No | ALTER FEDERATION SPLIT operation failed. Specified boundary value already exists for federation distribution %.\*ls and federation %.\*ls. |
| 45025 | 16 | No | %ls operation failed. Specified boundary value is not valid for federation distribution %.\*ls and federation %.\*ls. |
| 45026 | 16 | No | %ls operation failed. Specified boundary value does not exist for federation distribution %.\*ls and federation %.\*ls. |
| 45027 | 16 | No | %ls operation failed. Specified type information is not valid for federation distribution. |
| 45028 | 16 | No | %ls operation failed. Specified boundary value is not valid for current federation member. |
| 45029 | 16 | No | %ls operation failed. The federation distribution scheme size cannot exceed 900 bytes. |
| 45030 | 15 | No | The USE FEDERATION statement is missing the required %.\*ls option. Provide the option in the WITH clause of the statement. |
| 45031 | 16 | No | The USE FEDERATION statement is not supported on a connection which has multiple active result sets (MARS) enabled. |
| 45032 | 16 | No | The USE FEDERATION statement is not allowed under non-revertible impersonated security context. |
| 45033 | 16 | No | Federation member %d is not available. Another command is creating or dropping it. |
| 45034 | 16 | No | Federation member database cannot be dropped using DROP DATABASE. |
| 45035 | 16 | No | Federation member database cannot be renamed using ALTER DATABASE. |
| 45036 | 16 | No | ALTER FEDERATION SPLIT operation has been aborted. The %.\*ls federation was dropped while the split was still in progress. |
| 45037 | 16 | No | ALTER FEDERATION SPLIT operation failed due to an internal error. This request has been assigned a tracing ID of '%.\*ls'. Provide this tracing ID to customer support when you need assistance. |
| 45038 | 16 | No | CREATE DATABASE AS COPY OF %S_MSG is not supported. |
| 45039 | 16 | No | Federation member database cannot be restored. |
| 45040 | 16 | No | Service objective for federation member database cannot be changed using ALTER DATABASE. |
| 45041 | 16 | No | Federation member database options cannot be changed. |
| 45042 | 16 | No | Federation root database options cannot be changed. |
| 45043 | 16 | No | ALTER FEDERATION SWITCH %ls is not supported on %ls members. |
| 45044 | 16 | No | ALTER FEDERATION SWITCH IN failed. %.\*ls is not a valid database that can be switched in. |
| 45045 | 16 | No | ALTER FEDERATION SWITCH IN failed. %ls of the federation key in %.\*ls doesn't match with the correponding property of federation %.\*ls. |
| 45046 | 16 | No | %ls operation failed. The specified value for federation %.\*ls does not correspond to a present member. |
| 45101 | 16 | No | Parameter "%ls" should contain all configurable dimension properties. |
| 45102 | 16 | No | Parameter "%ls" should contain settings for all dimensions. |
| 45103 | 16 | No | Cannot reset the last setting for a dimension to non-default. |
| 45104 | 16 | No | The default setting cannot be deleted for a dimension. |
| 45105 | 16 | No | Cannot assign a deprecated setting as successor. |
| 45106 | 16 | No | Cannot create new item. The max provision count has been reached. |
| 45107 | 16 | No | Cannot reset service objective to draft from enabled or disabled state. |
| 45108 | 16 | No | Default service objective cannot be disabled. |
| 45109 | 16 | No | Parameter "%ls" contains conflicting dimension setting selections. |
| 45110 | 16 | No | Parameter %s value cannot be applied to service objective in non-draft mode. |
| 45111 | 16 | No | Cannot reset the last service objective to non-default. |
| 45112 | 16 | No | Cannot delete the default service objective. |
| 45113 | 16 | No | Cannot set the service objective as default in draft mode. |
| 45114 | 16 | No | Cannot edit setting marked as deprecated. |
| 45115 | 16 | No | Cannot assign a disabled service objective to a database. |
| 45116 | 16 | No | Cannot delete a setting without a successor assigned for deprecation. |
| 45117 | 16 | No | Cannot delete a system service objective. |
| 45118 | 16 | No | Cannot assign a system service objective to user database. |
| 45119 | 16 | No | Property selections contain conflicting values. |
| 45120 | 16 | No | The name '%ls' already exists. Choose a different name. |
| 45121 | 16 | No | Server '%ls' does not support memory-optimized data. Make sure both source and target servers are enabled for memory-optimized data. |
| 45122 | 16 | No | '%ls' |
| 45123 | 16 | No | Updating max size is not supported for database '%ls'. Database size will grow automatically as more data is inserted. |
| 45125 | 16 | No | Parameter "%ls" cannot be empty or null. |
| 45126 | 16 | No | Parameter "%ls" is invalid. |
| 45127 | 16 | No | Parameter "%ls" is invalid. |
| 45128 | 16 | No | Parameter "%ls" is invalid. |
| 45129 | 16 | No | Parameter "%ls" is invalid. |
| 45130 | 16 | No | Parameter "%ls" is invalid. |
| 45131 | 16 | No | Parameter "%ls" is invalid. |
| 45132 | 16 | No | Every database must be assigned a service objective. |
| 45133 | 17 | No | A connection failed while the operation was still in progress, and the outcome of the operation is unknown. Query sys.dm_operation_status in the master database for current job status. |
| 45134 | 16 | No | The remote partner server name '%ls' could not be resolved. |
| 45135 | 16 | No | Only continuous database copies can be updated. |
| 45136 | 16 | No | Only continuous database copies can be terminated. |
| 45137 | 16 | No | Insufficient permission to create a database copy on server '%ls'. |
| 45138 | 16 | No | The destination database name '%ls' already exists on the server '%ls'. |
| 45139 | 16 | No | The source server name should be the server of the current connection. |
| 45140 | 16 | No | Maximum lag does not support the specified value. Maximum lag must be between '%ls' and '%ls'. |
| 45141 | 16 | No | Database copies can only be initiated on the source server. |
| 45142 | 16 | No | IsForcedTerminate cannot be set while creating a database copy. This can only be updated on the source server after it is created. |
| 45143 | 16 | No | The source database '%ls' does not exist. |
| 45144 | 16 | No | Continuous copy cannot be initiated on source database '%ls' because it is a federation root member. |
| 45145 | 16 | No | Only continuous database copies can be created. |
| 45146 | 16 | No | Database copy property '%ls' is required. |
| 45147 | 16 | No | Database copy property '%ls' cannot be changed. |
| 45148 | 16 | No | '%ls' is not supported for entity '%ls'. |
| 45149 | 16 | No | Continuous copy is not supported on 'master' database. |
| 45150 | 16 | No | Feature is disabled. |
| 45151 | 16 | No | Changing value(s) '%ls' for entity '%ls' not supported. |
| 45152 | 16 | No | Termination of a database copy cannot be performed because the destination server '%ls' is unavailable or the database copy does not exist on the destination server. Try forced termination instead. |
| 45153 | 16 | No | Management Service is not currently available. Please retry the operation later. If the problem persists, contact customer support, and provide them the session tracing ID of '%ls'. |
| 45154 | 16 | No | A free database already exists for subscription '%ld' for the selected region. Subscription can have only one free database per region. To provision another free database in same subscription, choose a different region. To provision another free database in same region, use different subscription. |
| 45155 | 16 | No | A free database operation is already in progress for subscription '%ld'. Subscription can have only one free database per region. To provision another free database in same subscription, choose a different region. To provision another free database in same region, use different subscription. |
| 45156 | 16 | No | Subscription '%.\*ls' is busy with another operation. Please try your operation later. |
| 45157 | 16 | No | Server '%.\*ls' is busy with another operation. Please try your operation later. |
| 45158 | 16 | No | Cannot move server '%.\*ls' from source subscription '%ls' to target subscription '%ls'. You can have only one free database per subscription. To continue the move operation, drop one of the free databases, or use a different subscription. |
| 45159 | 16 | No | Cannot move servers from source subscription '%ls' to target subscription '%ls' since both subscriptions have free databases. You can have only one free database per subscription. To continue the move operation, drop one of the free databases, or use a different subscription. |
| 45160 | 16 | No | Subscription '%ls' doesn't have any servers to move. |
| 45161 | 16 | No | Managed instance '%.\*ls' is busy with another operation. Please try your operation later. |
| 45162 | 16 | No | Operation failed because subscription '%ls' is disabled. |
| 45163 | 16 | No | Create Server request must either specify SQL login with password or denote admin login is a federated user by setting 'IsFederatedAdminLogin' property to 'True'. |
| 45164 | 16 | No | Invalid number of database copies: '%d'. Only one database copy is currently allowed to be created along with the database creation. |
| 45165 | 16 | No | Continuous copy is not supported on the free database '%.\*ls'. |
| 45166 | 16 | No | Database '%.\*ls' was %.\*ls successfully, but some properties could not be displayed. |
| 45167 | 16 | No | An invalid state transition was attempted. |
| 45168 | 16 | No | The server '%.\*ls' has too many active connections. Try again later. |
| 45169 | 16 | No | The subscription '%.\*ls' has too many active connections. Try again later. |
| 45170 | 16 | No | A trusted certificate was not found for this request. |
| 45171 | 16 | No | Secondary database must be created in the same server as primary database unless 'IsContinuous' is specified. |
| 45172 | 16 | No | The 'MaximumLag' parameter requires that 'IsContinuous' is specified. |
| 45173 | 16 | No | Source server not found for subscription: %ls, resource group: %ls, server name: %ls. |
| 45174 | 16 | No | Unable to queue server %ls for migration. There is a pending migration request for the server. |
| 45175 | 16 | No | Unable to queue server %ls for migration. A migration is already in progress. |
| 45176 | 16 | No | Unable to queue server %ls for migration. The server is using feature %ls that prevents it from being migrated. |
| 45177 | 16 | No | Unable to queue server %ls for migration. Failed to find a target cluster for migration. |
| 45178 | 16 | No | Database create mode '%ls' is not supported. |
| 45179 | 16 | No | The operation timed out and automatically rolled back. Please retry the operation. |
| 45180 | 16 | No | Resource with the name '%ls' already exists. To continue, specify a different resource name. |
| 45181 | 16 | No | Resource with the name '%ls' does not exist. To continue, specify a valid resource name. |
| 45182 | 16 | No | Database '%ls' is busy with another operation. Please try your operation later. |
| 45183 | 16 | No | There is an import or export operation in progress on the database '%ls'. |
| 45184 | 16 | No | Operation Id '%ls' was not found. |
| 45185 | 16 | No | The change role completion notification for logical server '%.\*ls', ag id '%ls', local database id '%ls' was ignored as freshness number did not match. Expected '%d'. |
| 45186 | 16 | No | Database create mode '%ls' is not supported in state '%ls'. |
| 45187 | 16 | No | The replication mode updated notification for logical server '%.\*ls', ag id '%ls', local database id '%ls', partner database id '%ls' was ignored as current replication mode did not match expected value '%ls'. |
| 45188 | 16 | No | The operation has been cancelled by user. |
| 45189 | 16 | No | Insufficient permission to add secondary on server '%ls'. |
| 45190 | 16 | No | '%ls' is an invalid name because it contains one or more unsupported unicode characters. |
| 45191 | 16 | No | The operation could not be completed because it would result in data loss on secondary database '%ls' on server '%ls.' Set the 'replace' parameter to proceed anyway. |
| 45192 | 16 | No | The value specified for 'allow_connections' does not match the value for the existing replication relation for database '%ls' on server '%ls.' |
| 45193 | 16 | No | Database '%ls' on server '%ls' is already the target in another replication relationship. |
| 45194 | 16 | No | The operation could not be completed because it would result in target database '%ls' on server '%ls' having a lower service objective than source database '%ls' on server '%ls.' |
| 45195 | 16 | No | The operation could not be completed because database '%ls' on server '%ls' is the source in another replication relationship. |
| 45196 | 16 | No | The operation on the resource could not be completed because it was interrupted by another operation on the same resource. |
| 45197 | 17 | No | A system maintenance operation is in progress on server '%.\*ls' and database '%.\*ls.' Please wait a few minutes before trying again. |
| 45198 | 16 | No | MODIFY LOG FILE failed. Size is greater than MAXSIZE. |
| 45199 | 16 | No | This command requires a database encryption scan on database '%.\*ls'. However, the database has changes from previous encryption scans that are pending log backup. Please wait several minutes for the log backup to complete and retry the command. |
| 45200 | 16 | No | The server '%ls' are not associated with the tenant '%ls'. |
| 45201 | 16 | No | SQL Server Agent service is not running. |
| 45202 | 16 | No | Timed out after %ld seconds waiting for Smart Admin job to complete. Please run the stored procedure again. |
| 45203 | 16 | No | The parameter @state cannot be NULL, and should be 1 or 0. Specify 1 to start SQL Server Managed Backup to Windows Azure, or 0 to pause. |
| 45204 | 16 | No | The parameter %ls cannot be NULL or empty. Provide a valid %ls. |
| 45205 | 16 | No | No backup setting is supplied. Please specify at least one backup setting to be configured. |
| 45206 | 16 | No | The value specified for parameter @type is invalid. The @type parameter value should either be 'Database' or 'Log'. |
| 45207 | 16 | No | %ls |
| 45208 | 16 | No | SQL Server Managed Backup to Windows Azure master switch is not turned on. |
| 45209 | 16 | No | The parameter value for notification email is not specified or is NULL. Please specify a valid email to enable notifications for Smart Admin. |
| 45210 | 16 | No | Database Mail is not enabled for SQL Agent to use for Notifications. Enable Database Mail as the mail system for alerts. |
| 45211 | 16 | No | Database mail profile is not setup for SQL Agent notifications. |
| 45212 | 16 | No | The value specified for parameter %ls is invalid. Provide a valid %ls. |
| 45213 | 16 | No | @full_backup_freq_type, @backup_begin_time, @backup_duration, and @log_backup_freq must be specified if @scheduling_option is set to 'Custom' |
| 45214 | 16 | No | @days_of_week must be specified if @full_backup_freq_type is set to 'Weekly' |
| 45215 | 16 | No | Local caching is not yet supported. |
| 45216 | 16 | No | @full_backup_freq_type, @backup_begin_time, @backup_duration, and @log_backup_freq must not be specified if @scheduling_option is set to 'System' |
| 45217 | 16 | No | @encryptor_type, and @encryptor_name must not be specified if @encryption_algorithm is set to 'NO_ENCRYPTION' |
| 45218 | 16 | No | @encryptor_type, and @encryptor_name must be specified if @encryption_algorithm is not set to 'NO_ENCRYPTION' |
| 45219 | 16 | No | @days_of_week must not be specified if @full_backup_freq_type is set to 'Daily' |
| 45220 | 10 | No | An error occurred while configuring for the SQL Agent: error %d, severity %d, state %d. |
| 45221 | 10 | No | An error occurred while configuring for the SQL Agent: error %d, severity %d, state %d. |
| 45301 | 17 | No | The resource has been moved to another location |
| 45302 | 17 | No | SLO '%ls' operation cannot succeed as the memory usage of '%ls' exceeds the quota. |
| 45303 | 17 | No | Attempt to cancel activation or rollback activation automatically because of operation timeout, but this is not supported in current state. Please check database status after operation is finished. |
| 45304 | 16 | No | Elastic pool estimate '%.\*ls' was not found for server '%.\*ls' |
| 45305 | 16 | No | Request could not be processed because of conflict in the request: '%.\*ls' |
| 45306 | 16 | No | The external admin cannot be set because the user or group already exists in the 'master' database. |
| 45307 | 16 | No | Advisor '%.\*ls' was not found for requested resource |
| 45308 | 16 | No | Recommended action '%.\*ls' was not found for advisor '%.\*ls' |
| 45309 | 16 | No | The operation could not be completed because database '%ls' on server '%ls' is recovering from a geo-replication role change and is not currently eligible to become a primary or standalone database. Wait until the relationship leaves the 'SUSPENDED' replication state and try again. |
| 45310 | 16 | No | Unable to return metrics. Request would return too much data. |
| 45311 | 16 | No | The server key '%.\*ls' already exists. Please choose a different server key name. |
| 45312 | 16 | No | The server key URI '%.\*ls' already exists as another server key. |
| 45313 | 16 | No | The server key '%.\*ls' cannot be deleted because it is currently in use by '%.\*ls'. |
| 45314 | 16 | No | Server key '%.\*ls' does not exist. Make sure that the server key name is entered correctly. |
| 45315 | 16 | No | The operation could not be completed because a service objective assignment is in progress for database '%.\*ls' on server '%.\*ls.' Wait for the service objective assignment to complete and try again. |
| 45316 | 16 | No | MODIFY FILE failed. Size is greater than MAXSIZE. Please query sys.database_files and use DBCC SHRINKFILE to reduce the file size first. |
| 45317 | 16 | No | Server '%.\*ls' does not exist in resource group '%.\*ls' in subscription '%.\*ls'. |
| 45318 | 16 | No | Service Fabric Application Version is not available. |
| 45319 | 16 | No | The service objective assignment for database '%.\*ls' on server '%.\*ls' could not be completed as the database is too busy. Reduce the workload before initiating another service objective update. |
| 45320 | 16 | No | The operation could not be completed on server '%.\*ls' because the Azure Key Vault key '%.\*ls' is disabled. |
| 45321 | 16 | No | The operation could not be completed on server '%.\*ls' because attempts to connect to Azure Key Vault '%.\*ls' have failed |
| 45322 | 16 | No | The operation could not be completed because the Azure Key Vault key '%.\*ls' expiration date is invalid. The Azure Key Vault key can not have an expiration date. |
| 45323 | 16 | No | Unable to start a copy because the source database '%ls' is being updated. |
| 45324 | 16 | No | The operation could not be completed because the Azure Key Vault Uri is null or empty. |
| 45325 | 16 | No | The operation could not be completed because the Azure Key Vault Key name is null or empty. |
| 45326 | 16 | No | The operation could not be completed because the Azure Key Vault Key name '%ls' does not exist. |
| 45327 | 16 | No | The operation could not be completed because the Azure Key Vault Key name '%ls' is currently set as server encryption protector. |
| 45328 | 16 | No | The server identity is not correctly configured on server '%ls'. (https://aka.ms/sqltdebyoksetup) |
| 45329 | 16 | No | An invalid response from Azure Key Vault. Please use a valid Azure Key Vault URI. |
| 45330 | 16 | No | The server '%ls' requires the following Azure Key Vault permissions: '%ls'. Please grant the missing permissions to the service principal with ID '%ls'. (https://aka.ms/sqltdebyoksetup) |
| 45331 | 16 | No | The operation could not be completed because the read scale value specified is not supported for a '%ls' database. |
| 45332 | 16 | No | The operation could not be completed because the read scale value specified is invalid. |
| 45333 | 16 | No | The service request timed out. %ls. |
| 45334 | 16 | No | Server edition '%ls' is invalid. |
| 45335 | 16 | No | Server type '%ls' is invalid. |
| 45336 | 16 | No | The operation could not be completed because '%ls' is an invalid Server Key name. Please provide a key name in the format of 'vault_key_version'. For example, if the keyId is https://YourVaultName.vault.azure.net/keys/YourKeyName/YourKeyVersion, then the Server Key Name should be formatted as: YourVaultName_YourKeyName_YourKeyVersion. |
| 45337 | 16 | No | The planned failover operation has rolled back because database '%ls' could not be synchronized with its remote partner. This may be due to a service outage, or to a high volume of write traffic. Consider using forced failover. |
| 45338 | 16 | No | The planned failover operation has rolled back because the remote server '%ls' could not be reached. This may be due to a service outage. Consider using forced failover. |
| 45339 | 16 | No | The max size update on the geo-secondary database '%ls' on server '%ls' failed with reason '%ls'. |
| 45340 | 16 | No | The operation could not be completed because an Azure Active Directory error was encountered. The error message from Active Directory Authentication library (ADAL) is '%ls'. |
| 45341 | 16 | No | The operation could not be completed because an error was encountered when attempting to retrieve Key Vault information for '%ls' from server '%ls'. The encountered error message is '%ls'. |
| 45342 | 16 | No | The operation could not be completed because an Azure Active Directory error was encountered. Please ensure the server '%ls' and key vault '%ls' belong to the same tenant. The error message from Active Directory Authentication library is '%ls'. |
| 45343 | 16 | No | The provided Key Vault uri '%ls' is not valid. Please ensure the uri contains the vault, key, and key version information. An example valid uri looks like 'https://YourVaultName.vault.azure.net/keys/YourKeyName/YourKeyVersion'. Please ensure the vault belongs to an endpoint from the list of supported endpoints available at '%ls'. |
| 45344 | 16 | No | Catalog DB creation failed. |
| 45345 | 16 | No | Cannot cancel database management operation '%ls' in the current state. |
| 45346 | 16 | No | Subnet resource ID '%ls' is invalid. Please provide a correct resource Id for the target subnet. |
| 45347 | 16 | No | LongTermRetentionBackup is enabled for server '%ls'. Move server cross subscription is not allowed. |
| 45348 | 16 | No | LongTermRetentionBackup is enabled for server '%ls'. Move server cross resource group is not allowed. |
| 45349 | 16 | No | The operation could not be completed because certificate rotation is in progress for server '%ls'. |
| 45350 | 16 | No | MODIFY MAXSIZE failed. To reduce the database size, the database first needs to reclaim unused space by running DBCC SHRINKDATABASE. Note that this operation can impact performance while it is running and may take several hours to complete. Refer to the following article for details of using T-SQL to run DBCC SHRINKDATABASE: '[https://go.microsoft.com/fwlink/?linkid=852312](https://go.microsoft.com/fwlink/?linkid=852312)' |
| 45351 | 16 | No | MODIFY MAXSIZE failed. The requested database size is smaller than the amount of data storage used. |
| 45352 | 16 | No | Create Managed Instance failed. Provided virtual network subnet is located in %ls, which is a different region than the one you are provisioning Managed Instance in (%ls). To create a VNET-joined Managed Instance, the instance and the virtual network have to be located in the same region. |
| 45353 | 16 | No | This private cluster is associated with TR %ls. You specified TR %ls as a parameter. |
| 45354 | 16 | No | Tenant ring %d already has positive placement weight set(%d) and can't be used for private cluster for subscription %ls. Please ensure that the ring is actually not used, reset weight back to 0 or -1 and re-run the SignalPrivateTenantRingReady CAS. |
| 45355 | 16 | No | The storage account %ls is not valid or does not exist. |
| 45356 | 16 | No | The storage account credentials are not valid. |
| 45357 | 16 | No | Auditing cannot be configured on secondary databases. |
| 45358 | 16 | No | Server auditing settings are being updated for server '%ls'. Please wait for the existing operation to complete. |
| 45359 | 16 | No | Database name validation failed. The database name is not allowed because it contains trailing whitespace characters. |
| 45360 | 16 | No | The operation could not be completed because app config deployment is in progress for the app '%ls'. |
| 45361 | 16 | No | Invalid subnet address range (%ls). Address range has to be in CIDR format (IP/MASK) where IP is a valid IPv4 address and MASK is a number between 0 and 28. |
| 45362 | 16 | No | The operation could not be completed because the Azure Key Vault principal certificate has expired. The error message from Active Directory Authentication library (ADAL) is '%ls'. |
| 45363 | 16 | No | Server automatic tuning settings from previous request have not propagated to all databases yet. Please try again in few minutes. |
| 45364 | 16 | No | The operation could not be completed because database '%ls' on server '%ls' is currently unavailable. Try again later. If the problem persists, contact customer support. |
| 45365 | 16 | No | vCore value (%d) is not valid. Please specify a valid vCore value. |
| 45366 | 16 | No | The retention days of %d is not a valid configuration. Valid backup retention in days must be between %d and %d. |
| 45367 | 16 | No | Invalid virtual network configuration. This is not allowed: %ls. |
| 45368 | 16 | No | The given rule ID: %s is invalid. You can find valid rule ids in scan results file. |
| 45369 | 16 | No | No baseline is set for rule ID: %d. You should first set a baseline for this rule. |
| 45370 | 16 | No | Vulnerability Assessment scan on the resource '%ls' is in progress. Please wait for the existing operation to complete. |
| 45371 | 16 | No | Cannot cancel management operation '%ls' in the current state.%ls |
| 45372 | 16 | No | Automatic tuning option '%.\*ls' was not found for requested resource. |
| 45373 | 16 | No | Automatic tuning is not supported for SQL Data Warehouse. |
| 45374 | 16 | No | HardwareGeneration '%ls' is not valid. Please specify a valid HardwareGeneration value. |
| 45375 | 16 | No | vCore value (%d) and HardwareGeneration '%ls' is not a valid combination. Please specify a valid vCore and HardwareGeneration value. |
| 45376 | 16 | No | Managed Instance cannot be joined to a classic virtual network. Please provide a Resource Manager vnet to join. |
| 45377 | 16 | No | The provided Key Vault uri '%ls' is not valid. Please ensure the key vault has been configured with soft-delete and purge protection. (https://aka.ms/sqltdebyoksoftdelete) |
| 45378 | 16 | No | geo-redundant-backup not supported for the current edition. |
| 45379 | 16 | No | geo-redundant-backup value is not allowed to update |
| 45380 | 16 | No | The edition %.\*ls is not a valid edition. Edition cannot be changed by update. |
| 45381 | 16 | No | SKU Name '%ls' is not valid. Please specify a valid SKU Name. |
| 45382 | 16 | No | Read or write operations are not allowed on the storage account '%ls'. |
| 45383 | 16 | No | The storage account '%ls' is disabled. |
| 45384 | 16 | No | The encryption protectors for all servers linked by GeoDR must be in the same region as their respective servers. Please upload key '%ls' to a Key Vault in the region '%ls' as server '%ls'. (https://aka.ms/sqltdebyokgeodr) |
| 45385 | 16 | No | Unexpected Key Vault region found in the response for Key Vault '%ls' associated with server '%ls'. Expected region: '%ls', Region receieved in response: '%ls'. (https://aka.ms/sqltdebyokgeodr) |
| 45386 | 16 | No | The key vault provided '%ls' on server '%ls' uses unsupported RSA Key Size or Key Type. The supported RSA Key Size is 2048 and Key Type is RSA and RSA-HSM. |
| 45387 | 16 | No | Invalid hardware generation. It isn't allowed to have both Gen4 and Gen5. |
| 45388 | 16 | No | Target subnet has associated Network Security Group (NSG). Remove %ls. ([https://go.microsoft.com/fwlink/?linkid=871071](https://go.microsoft.com/fwlink/?linkid=871071)) |
| 45389 | 16 | No | Target subnet has no associated Route Table. Associate Route Table with single User Defined Route (UDR): Name: default, Address prefix: 0.0.0.0/0, Next hop type: Internet. ([https://go.microsoft.com/fwlink/?linkid=871071](https://go.microsoft.com/fwlink/?linkid=871071)) |
| 45390 | 16 | No | Target subnet has associated Route Table with invalid configuration %ls. Associate Route Table with single User Defined Route (UDR): Name: default, Address prefix: 0.0.0.0/0, Next hop type: Internet. ([https://go.microsoft.com/fwlink/?linkid=871071](https://go.microsoft.com/fwlink/?linkid=871071)) |
| 45391 | 16 | No | Invalid virtual network configuration %ls. Add Azure recursive resolver virtual IP address 168.63.129.16 at the end of your custom DNS list: %ls. ([https://go.microsoft.com/fwlink/?linkid=871071](https://go.microsoft.com/fwlink/?linkid=871071)) |
| 45392 | 16 | No | SQL service endpoint is currently not allowed on Managed Instance. Remove SQL service endpoint from %ls. ([https://go.microsoft.com/fwlink/?linkid=871071](https://go.microsoft.com/fwlink/?linkid=871071)) |
| 45393 | 16 | No | Target subnet is not empty. Remove all resources from %ls or use a different empty subnet. ([https://go.microsoft.com/fwlink/?linkid=871071](https://go.microsoft.com/fwlink/?linkid=871071)) |
| 45394 | 16 | No | Virtual network for the target subnet is locked. Remove the lock from %ls. ([https://go.microsoft.com/fwlink/?linkid=871071](https://go.microsoft.com/fwlink/?linkid=871071)) |
| 45395 | 16 | No | Gateway subnet cannot be used for deploying managed instance. Use a different empty subnet. ([https://go.microsoft.com/fwlink/?linkid=871071](https://go.microsoft.com/fwlink/?linkid=871071)) |
| 45396 | 16 | No | Target subnet %ls does not exist. Use a different empty subnet. ([https://go.microsoft.com/fwlink/?linkid=871071](https://go.microsoft.com/fwlink/?linkid=871071)) |
| 45397 | 16 | No | Managed Instance deployment failed due to conflict with the following network requirements: %ls. Information about the network requirements and how to set them could be found at ([https://go.microsoft.com/fwlink/?linkid=871071](https://go.microsoft.com/fwlink/?linkid=871071)). You can find more details about this error in the virtual network Activity log. |
| 45398 | 16 | No | The specified edition %ls is not consistent with the specified SKU %ls. |
| 45399 | 16 | No | The specified license type %ls is not valid. |
| 45400 | 16 | No | Invalid storage size: %ls GB. Storage size must be specified between %ls and %ls gigabytes, in increments of %ls GB. |
| 45401 | 16 | No | Ring buildout '%ls' has no support for small VM roles, role: '%ls'. |
| 45402 | 16 | No | Ring buildout '%ls' does not support multiple DB roles. |
| 45403 | 16 | No | The operation could not be completed because a service tier change is in progress for managed instance '%.\*ls.' Please wait for the operation in progress to complete and try again. |
| 45404 | 16 | No | The operation failed because update SLO safety checks could not be completed. Details: '%ls'. |
| 45405 | 16 | No | The operation failed because a long running transaction was found on the SQL instance. Please wait for the transaction to finish execution and try again. |
| 45406 | 16 | No | Provisioning of zone redundant database/pool is not supported for your current request. |
| 45407 | 16 | No | The operation timed out and failed: '%ls' |
| 45408 | 16 | No | Resource not found: '%ls'. |
| 45409 | 16 | No | Network resource provider returned following error: '%ls'. |
| 45410 | 16 | No | Network resource provider denied access: '%ls'. |
| 45411 | 16 | No | Virtual network firewall rules are not currently supported on servers with failover groups configured with automatic failover policy. Please configure the failover groups on the server with manual failover policy. |
| 45412 | 16 | No | Failover Groups configured with an automatic failover policy are currently not supported on servers configured with virtual network firewall rules. Please configure the failover group with manual failover policy. |
| 45413 | 16 | No | Only one Interface endpoint profile is allowed. |
| 45414 | 16 | No | The instance collation cannot be changed on Managed Instance. |
| 45415 | 16 | No | Creating secondary of secondary (a process known as chaining) is not supported when enabling Transparent Data Encryption using Azure Key Vault (BYOK). |
| 45416 | 16 | No | Cannot create a Managed Instance with collation '%.\*ls'. Please use collation 'SQL_Latin1_General_CP1_CI_AS' instead. |
| 45417 | 16 | No | Vulnerability Assessment storage container path must be supplied |
| 45418 | 16 | No | The operation failed because the SQL instance had high CPU usage of %.\*f%%. The current threshold is %.\*f%%. Please wait for it to go down and try again. |
| 45419 | 16 | No | The operation failed because the SQL instance had high log write rate of %.\*f%%. The current threshold is %.\*f%%. Please wait for it to go down and try again. |
| 45420 | 16 | No | The operation failed because update elastic pool safety checks could not be completed. Details: '%ls'. |
| 45421 | 16 | No | The operation failed because a long running transaction was found on the elastic pool. Please wait for the transaction to finish execution and try again. |
| 45422 | 16 | No | The operation failed because the elastic pool had high CPU usage of %.\*f%%. The current threshold is %.\*f%%. Please wait for it to go down and try again. |
| 45423 | 16 | No | The operation failed because the elastic pool had high log write rate of %.\*f%%. The current threshold is %.\*f%%. Please wait for it to go down and try again. |
| 45424 | 16 | No | The operation could not be completed because %ls operation is in progress. Please wait for the operation in progress to complete and try again. |
| 45425 | 16 | No | The operation could not be completed because total allocated storage size for General Purpose instance would exceed %ls. Please reduce the number of database files and retry operation. |
| 45426 | 16 | No | The operation could not be completed because instance has configured geo replicated secondary instance. |
| 45427 | 16 | No | Hardware generation of the source is '%.\*ls' while that of the PITR target is '%.\*ls'. |
| 45428 | 16 | No | The operation has failed because you are attempting to deploy managed instance as a geo-replication secondary to the subnet '%.\*ls' in which there already exists a managed instance. Deploying managed instance as a geo-replication secondary is supported only in cases when managed instance is the first instance deployed in a subnet. Consider deploying managed instance as a geo-replication secondary to a different subnet in which there are no existing managed instances, or if deploying to a subnet with existing managed instance disable the geo-replication option. |
| 45429 | 16 | No | The operation failed because it was issued on a custom maintenance window database. This is prohibited by default. Please try again with a switch to disable this check if the instance needs to be updated. |
| 45430 | 16 | No | The operation failed because it was issued on a custom maintenance window database outside of its maintenance window. Please try again within the next maintenance window (%ls - %ls). |
| 45431 | 16 | No | The operation failed because it was issued on a database during the default business hours in the region (%ls - %ls). Please try again once they end. |
| 45432 | 16 | No | Invalid storage size: Storage size limit (%ls GB) is less that current storage used (%ls GB). Please specify higher storage size limit. |
| 45433 | 16 | No | The specified family %ls is not consistent with the specified SKU %ls. |
| 45434 | 16 | No | The point in time '%.\*ls' for restoration can't be later than now. |
| 45435 | 16 | No | The operation could not be completed. The requested sku update would cause the master server to have a larger max_connections value than its replica(s). |
| 45436 | 16 | No | The operation could not be completed. The requested storage update would cause the master server to have a larger storage size than its replica(s). |
| 45437 | 16 | No | The operation could not be completed. Replication is not enabled for the server. |
| 45438 | 16 | No | The timezone cannot be changed on Managed Instance. |
| 45439 | 16 | No | Cannot create a Managed Instance with timezone '%.\*ls'. Please use timezone 'UTC' instead. |
| 45440 | 16 | No | Cannot create a Managed Instance as there are not enough available ip addresses in the selected subnet. |
| 45441 | 16 | No | Elastic server restore verification is not supported. |
| 45442 | 16 | No | The operation failed because the requested update mode '%ls' did not match the chosen one '%ls'. Please try again later or use a different update mode specification. |
| 45443 | 16 | No | Storage Auto Grow is not supported . |
| 45444 | 16 | No | Provisioning of Zone Redundant server is not supported . |
| 45445 | 16 | No | Storage Auto Grow is not supported for Primary and Replica Servers. |
| 45446 | 16 | No | Geo Redundant Backup not supported for Zone Redundant Server. |
| 45447 | 16 | No | Database cannot be paused because it is not supported on a '%ls' database. |
| 45448 | 16 | No | Database with '%ls' feature cannot be paused. Operation failed for Logical Server '%.\*ls', Database '%.\*ls', ServiceLevelObjective '%.\*ls' |
| 45449 | 16 | No | The database is already paused. Server '%.\*ls', Database '%.\*ls', Paused Time '%.\*ls'. |
| 45450 | 16 | No | The database is already resumed. Server '%.\*ls', Database '%.\*ls'. |
| 45451 | 16 | No | Database operation failed because database is paused. Server '%.\*ls', Database '%.\*ls' |
| 45452 | 16 | No | Database operation failed because database is paused. Server '%.\*ls', Database '%.\*ls' |
| 45453 | 10 | No | The operation failed due to throttling. Server '%.\*ls', Database '%.\*ls' |
| 45454 | 16 | No | The operation failed because it is not supported on disabled database. Server '%.\*ls', Database '%.\*ls' |
| 45455 | 16 | No | The operation failed because it is not supported on geo replicated databases. Logical Server '%.\*ls', Database '%.\*ls', ServiceLevelObjective '%.\*ls' |
| 45456 | 16 | No | The request failed because there is an ongoing '%.\*ls' operation. Server '%.\*ls', Database '%.\*ls'. |
| 45457 | 16 | No | The request failed because there is an ongoing migration on server '%.\*ls', database '%.\*ls'. |
| 45458 | 16 | No | Chosen subnet is delegated and cannot be used for deploying managed instance. Remove delegation from subnet. |
| 46501 | 15 | No | External table references '%S_MSG' that does not exist. |
| 46502 | 15 | No | Type with name '%.\*ls' already exists. |
| 46503 | 15 | No | Invalid format for option '%S_MSG'. |
| 46504 | 15 | No | External option '%S_MSG' is not valid. Ensure that the length and range are appropriate. |
| 46505 | 15 | No | Missing required external DDL option '%S_MSG'. |
| 46506 | 15 | No | Invalid set of options specified for '%S_MSG'. |
| 46507 | 15 | No | Cannot perform DML on external tables. |
| 46508 | 15 | No | Incorrect syntax on external DDL option '%S_MSG'. |
| 46509 | 15 | No | FILE_FORMAT must be specified for HADOOP data source. |
| 46510 | 15 | No | FILE_FORMAT cannot be specified for RDBMS data source. |
| 46511 | 15 | No | EXTERNAL %S_MSG with id %d cannot be found. |
| 46512 | 15 | No | %S_MSG cannot be used with %S_MSG data source. |
| 46513 | 15 | No | A sharding column name must be provided when using SHARDED distribution. |
| 46514 | 15 | No | DISTRIBUTION must be specified when using a SHARD_MAP_MANGER data source. |
| 46515 | 15 | No | The specified sharding column name does not match any column in the external table definition. |
| 46516 | 15 | No | The specified credential cannot be found or the user does not have permission to perform this action. |
| 46517 | 17 | No | XML Parse error when de-serializing PDW Metadata. |
| 46518 | 15 | No | The %S_MSG '%ls' is not supported with %S_MSG. |
| 46519 | 15 | No | %ls are not supported with %S_MSG. |
| 46520 | 15 | No | The external DDL statement contained an unrecognized option. |
| 46521 | 15 | No | Queries over external tables are not supported with the current service tier or performance level of this database. Consider upgrading the service tier or performance level of the database. |
| 46522 | 10 | No | Failed to update '%S_MSG'. |
| 46523 | 15 | No | A SCHEMA_NAME must be specified when using OBJECT_NAME. |
| 46524 | 15 | No | An OBJECT_NAME must be specified when using SCHEMA_NAME. |
| 46525 | 15 | No | External tables are not supported with the %S_MSG data source type. |
| 46526 | 15 | No | The %S_MSG operation is not supported with %S_MSG data source type. |
| 46527 | 15 | No | Altering the '%S_MSG' property is not permitted for an external data source of type %ls. |
| 46528 | 15 | No | SHARDED DISTRIBUTION is allowed for SHARD_MAP_MANGER data source only. |
| 46529 | 15 | No | Allowed integer values for FIRST_ROW are between and including 1-101 |
| 46530 | 15 | No | External data sources are not supported with type %S_MSG. |
| 46531 | 15 | No | Support for external data sources of type HADOOP is not enabled. To enable, set 'hadoop connectivity' to desired value. |
| 46532 | 15 | No | Sp_rename is not supported for data pool external table. |
| 46533 | 15 | No | Internal error occurred during distributed operation. |
| 46534 | 15 | No | A maximum of three NULL_VALUES are allowed in the list. |
| 46535 | 15 | No | Unable to retrieve secret \[%s\] from the secret store. |
| 46536 | 15 | No | Unable to process secret \[%s\] from the secret store. |
| 46537 | 16 | No | An unexpected internal error, failed to communicate with controller service, HRESULT 0x%x. |
| 46538 | 16 | No | An unexpected internal error received from controller service error code \[%i\]. |
| 46539 | 15 | No | '%S_MSG' statement failed. At least one partitioning column is required when distribution type is '%S_MSG'. |
| 46540 | 15 | No | '%S_MSG' statement failed. Partitioning columns can be specified only if distribution type is '%S_MSG'. |
| 46541 | 15 | No | '%S_MSG' statement failed. The column name '%.\*ls' specified in the '%S_MSG' option does not match any columns specified in the external table definition. |
| 46542 | 15 | No | %S_MSG statement failed. One or more errors occurred while executing the statement. %s |
| 46543 | 15 | No | %S_MSG statement failed. Invalid URI provided. |
| 46545 | 15 | No | Creating external data source over data pools is not supported in a system database. |
| 46546 | 15 | No | Alter database name is not supported in Big Data Cluster if the database contains external data pool tables. Drop the tables to perform the operation. |
| 46601 | 16 | No | REJECT_TYPE |
| 46602 | 16 | No | FILE_FORMAT |
| 46603 | 16 | No | REJECT_VALUE |
| 46604 | 16 | No | REJECT_SAMPLE_VALUE |
| 46605 | 16 | No | LOCATION |
| 46606 | 16 | No | DATA_SOURCE |
| 46607 | 16 | No | PERCENTAGE |
| 46608 | 16 | No | ROW_TERMINATOR |
| 46609 | 16 | No | DATA_COMPRESSION |
| 46610 | 16 | No | SERDE_METHOD |
| 46611 | 16 | No | ENCODING |
| 46612 | 16 | No | STRING_DELIMITER |
| 46613 | 16 | No | DATE_FORMAT |
| 46614 | 16 | No | FIELD_TERMINATOR |
| 46615 | 16 | No | FORMAT_TYPE |
| 46616 | 16 | No | JOB_TRACKER_LOCATION |
| 46617 | 16 | No | EXTERNAL TABLE |
| 46618 | 16 | No | HADOOP |
| 46619 | 16 | No | RDBMS |
| 46620 | 16 | No | SHARD_MAP_MANGER |
| 46621 | 16 | No | SHARDING_COLUMN_NAME |
| 46622 | 16 | No | DISTRIBUTION |
| 46623 | 16 | No | DATABASE_NAME |
| 46624 | 16 | No | SHARD_MAP_NAME |
| 46625 | 16 | No | CREDENTIAL |
| 46626 | 16 | No | REMOTE_SCHEMA_NAME |
| 46627 | 16 | No | REMOTE_OBJECT_NAME |
| 46628 | 16 | No | SHARED_MEMORY |
| 46629 | 16 | No | EXTRACTOR |
| 46643 | 16 | No | external tables |
| 46644 | 16 | No | external tables for sharded data |
| 46645 | 16 | No | Remote Data Archive |
| 46646 | 16 | No | provided |
| 46647 | 16 | No | seconds |
| 46648 | 16 | No | minutes |
| 46649 | 16 | No | hours |
| 46650 | 16 | No | days |
| 46651 | 16 | No | weeks |
| 46652 | 16 | No | months |
| 46653 | 16 | No | years |
| 46654 | 16 | No | database scoped credential |
| 46655 | 16 | No | rejected rows location |
| 46656 | 16 | No | CONNECTION_OPTIONS |
| 46657 | 16 | No | PUSHDOWN |
| 46658 | 16 | No | GENERIC |
| 46659 | 16 | No | BLOB_STORAGE |
| 46660 | 16 | No | GENERIC |
| 46661 | 16 | No | CREATE EXTERNAL TABLE |
| 46662 | 16 | No | HASH |
| 46663 | 16 | No | PARTITION |
| 46664 | 16 | No | DROP EXTERNAL TABLE |
| 46665 | 16 | No | TRUNCATE EXTERNAL TABLE |
| 46666 | 16 | No | ALTER DATABASE |
| 46667 | 16 | No | DROP DATABASE |
| 46701 | 16 | No | Query notifications is not an implemented feature for SQL Server Parallel DataWarehousing TDS endpoint. |
| 46702 | 16 | No | Remote RPC requests are not an implemented feature for SQL Server Parallel DataWarehousing TDS endpoint. |
| 46703 | 16 | No | Conversion error while attempting conversion between IPC blob parameter. |
| 46704 | 16 | No | Large object column support in SQL Server Parallel DataWarehouse server is limited to only nvarchar(max) data type. |
| 46705 | 16 | No | Unsupported parameter type found while parsing RPC request. The request has been terminated. |
| 46706 | 16 | No | Cursor support is not an implemented feature for SQL Server Parallel DataWarehousing TDS endpoint. |
| 46707 | 16 | No | The given IPC request with code %d is not supported for SQL Server Parallel DataWarehousing TDS endpoint. |
| 46709 | 16 | No | Default parameter support is not an implemented feature for SQL Server Parallel DataWarehousing TDS endpoint. |
| 46710 | 16 | No | Unsupported transaction manager request %d encountered. SQL Server Parallel DataWarehousing TDS endpoint only supports local transaction request for 'begin/commit/rollback'. |
| 46711 | 16 | No | Unsupported TDS request packet of type %d encountered on SQL Server Parallel DataWarehousing TDS endpoint. Only batch, rpc and transaction requests are supported. |
| 46712 | 16 | No | Unexpected error encountered during request processing. Connection will be terminated. |
| 46713 | 16 | No | Integrated authentication is not an implemented feature for SQL Server Parallel DataWarehousing TDS endpoint. |
| 46716 | 16 | No | Attach Database File is not an implemented feature for SQL Server Parallel DataWarehousing TDS endpoint. |
| 46717 | 16 | No | Login as Replication or Remote user is not supported by SQL Server Parallel DataWarehousing TDS endpoint. |
| 46718 | 16 | No | Only 'us_english' or 'English' language is supported by SQL Server Parallel DataWarehousing TDS endpoint. |
| 46719 | 16 | No | Attempt to reset connection with 'Keep Transaction' failed because the incoming request was not a commit/rollback request. This error can occur if more than one SqlConnection is used within the scope of a System.Transactions.Transaction. |
| 46720 | 16 | No | Parallel query execution on the same connection is not supported. |
| 46721 | 16 | No | Login failed. The login is from an untrusted domain and cannot be used with Integrated authentication. |
| 46722 | 16 | No | Client driver version is not supported. |
| 46723 | 16 | No | Large object column in Global Query is not supported for types other than Nvarchar(MAX), Varchar(MAX), Varbinary(MAX) and Image. |
| 46724 | 16 | No | Communication error during bulk copy operation. |
| 46801 | 16 | No | GlobalQuery operations |
| 46802 | 16 | No | Failed to load module for global query. |
| 46803 | 16 | No | Failed to locate entry point for global query module. |
| 46804 | 16 | No | Failed to initialize the global query module. |
| 46805 | 16 | No | Conversion error while constructing the GlobalQuery request. |
| 46806 | 16 | No | An error occurred while executing GlobalQuery operation: %.\*ls |
| 46807 | 16 | No | An access violation occurred while performing GlobalQuery operation. Execution has been aborted. |
| 46808 | 16 | No | An entry corresponding to given Global Query request could not be located. Execution has been aborted. |
| 46809 | 16 | No | An error occurred while attempting to execute the Global Query request. Error Code: %d, Severity: %d, State: %d. |
| 46810 | 16 | No | An unhandled exception occurred while performing GlobalQuery operation. Execution has been aborted. |
| 46811 | 16 | No | An unexpected error with code 0x%x occurred while executing GlobalQuery operation. |
| 46812 | 16 | No | %.\*ls |
| 46813 | 16 | No | %.\*ls |
| 46814 | 16 | No | %.\*ls |
| 46815 | 16 | No | %.\*ls |
| 46816 | 16 | No | %.\*ls |
| 46817 | 16 | No | %.\*ls |
| 46818 | 16 | No | %.\*ls |
| 46819 | 16 | No | %.\*ls |
| 46820 | 16 | No | %.\*ls |
| 46821 | 16 | No | %.\*ls |
| 46822 | 16 | No | %.\*ls |
| 46823 | 16 | No | %.\*ls |
| 46824 | 16 | No | %.\*ls |
| 46825 | 16 | No | %.\*ls |
| 46826 | 16 | No | Output parameters are not supported with %.\*ls. |
| 46827 | 16 | No | The option '%ls' must be turned ON to execute queries referencing external tables. |
| 46828 | 16 | No | The USE PLAN hint is not supported for queries that reference external tables. Consider removing USE PLAN hint. |
| 46829 | 16 | No | The proc %.\*ls is only supported for external data sources of type SHARD_MAP_MANAGER or RDBMS. |
| 46830 | 16 | No | Internal column references are not supported for external tables. |
| 46831 | 16 | No | Provided argument exceeds the current size limit of %u bytes for procedure %.\*ls. |
| 46901 | 10 | No | Stored procedure finished successfully. Polybase engine service is disabled. Please restart Polybase DMS service. |
| 46902 | 10 | No | Stored procedure finished successfully. Polybase engine service is enabled. Please restart Polybase engine and DMS services. |
| 46903 | 10 | No | This stored procedure is not available because Polybase feature is not enabled. |
| 46904 | 16 | No | Failed to get the computer name. This might indicate a problem with the network configuration of the computer. Error: %ls. |
| 46905 | 10 | No | Head node cannot be removed from a Polybase computational group. |
| 46906 | 16 | No | Unable to retrieve registry value '%ls' from Windows registry key '%ls': %ls. |
| 46907 | 16 | No | Unable to delete registry value '%ls' from Windows registry key '%ls': %ls. |
| 46908 | 16 | No | Unable to update registry value '%ls' in Windows registry key '%ls': %ls. |
| 46909 | 16 | No | Unable to open registry key '%ls': %ls. |
| 46910 | 16 | No | Incorrect number of parameters specified for procedure. |
| 46911 | 16 | No | Procedure expects parameter '%ls' of type '%ls'. |
| 46912 | 16 | No | The option '%ls' must be turned ON to execute requests referencing external tables. |
| 46913 | 16 | No | The USE PLAN hint is not supported for queries that reference external tables. Consider removing USE PLAN hint. |
| 46914 | 16 | No | INSERT into external table is disabled. Turn on the configuration option 'allow polybase export' to enable. |
| 46915 | 16 | No | Table hints are not supported on queries that reference external tables. |
| 46916 | 16 | No | Queries that reference external tables are not supported by the legacy cardinality estimation framework. Ensure that trace flag 9481 is not enabled, the database compatibility level is at least 120 and the legacy cardinality estimator is not explicitly enabled through a database scoped configuration setting. |
| 46917 | 16 | No | An internal error occurred while attempting to retrieve the encrypted database-scoped credential secret. |
| 46918 | 16 | No | An internal error occurred while attempting to retrieve the encrypted database-scoped credential secret. |
| 46919 | 16 | No | An internal error occurred while attempting to retrieve the encrypted database-scoped credential secret. |
| 46920 | 10 | No | Polybase feature disabled. |
| 46921 | 16 | No | The EXECUTE AT DATA_SOURCE command is not supported for this external data source type. |
| 46922 | 21 | No | PolyBase provisioning script performing on master database failed. This is a serious error which might prevent the SQL Server instance from starting. Examine the errorlog entries for errors, take the appropriate corrective actions and re-start the database so that the steps run to completion. |
| 46923 | 16 | No | PolyBase feature is not installed. Please consult Books Online for more information on this feature. |
| 46924 | 16 | No | SQL Server failed to start PolyBase processes due to error 0x%lx. |
| 46925 | 16 | No | SQL Server failed to start PolyBase request due to error 0x%lx. |
| 46926 | 16 | No | Failed to get PolyBase service account due to error '%ls'. |
| 46927 | 16 | No | SQL Server failed to get memory for PolyBase startup. |
| 46928 | 16 | No | The specified operation is not supported for this external data source type. |
| 46929 | 16 | No | The value '%ld' is not within range for the parameter '%ls'. |
| 46930 | 16 | No | PolyBase is not enabled because reading of PolyBase registry entries failed. |
| 46931 | 16 | No | PolyBase is not enabled due to error code 0x%lx. |
| 46932 | 16 | No | Unable to read PolyBase registry entries. |
| 46933 | 16 | No | PolyBase configuration is invalid or corrupt. Re-run PolyBase setup. |
| 47000 | 10 | No | Reason: FedAuth RPS Initialization failed when fetching CLSID of RPS ProgID. |
| 47001 | 10 | No | Reason: FedAuth RPS Initialization failed when creating instance of Passport.RPS COM Object. |
| 47002 | 10 | No | Reason: FedAuth RPS Initialization failed when initializing the RPS COM object. |
| 47003 | 10 | No | Reason: FedAuth RPS Authentication failed when getting IRPSAuth object. |
| 47004 | 10 | No | Reason: FedAuth RPS Authentication failed when getting IRPSPropBag object. |
| 47005 | 10 | No | Reason: FedAuth RPS Authentication failed during IRPS::Authenticate. |
| 47006 | 10 | No | Reason: FedAuth RPS Authentication failed when getting IRPSValidatedPropertyBag object. |
| 47007 | 10 | No | Reason: FedAuth RPS Authentication failed during SetAuthPolicy. |
| 47008 | 10 | No | Reason: FedAuth RPS Authentication failed during ValidateTicketWithAuthPolicy. |
| 47009 | 10 | No | Reason: FedAuth RPS Authentication failed when fetching Session Key. |
| 47010 | 10 | No | Reason: FedAuth RPS Authentication failed when initializing HMAC Hash object. |
| 47011 | 10 | No | Reason: FedAuth RPS Authentication failed when computing HMAC Hash Signature. |
| 47012 | 10 | No | Reason: FedAuth RPS Authentication failed when comparing HMAC Hash Signature with the one sent by the client. |
| 47013 | 10 | No | Reason: FedAuth RPS Authentication failed when fetching MemberId Low. |
| 47014 | 10 | No | Reason: FedAuth RPS Authentication failed when fetching MemberId High. |
| 47015 | 10 | No | Reason: FedAuth RPS Authentication failed when fetching MemberName. |
| 47016 | 10 | No | Reason: FedAuth AzureActiveDirectoryService Initialization failed when opening Certificate Store. |
| 47017 | 10 | No | Reason: FedAuth AzureActiveDirectoryService Initialization failed when trying to find ceritificate in store. |
| 47018 | 10 | No | Reason: FedAuth AzureActiveDirectoryService Initialization failed when trying to initialize Error object for service proxy. |
| 47019 | 10 | No | Reason: FedAuth AzureActiveDirectoryService Initialization failed when trying to create service proxy. |
| 47020 | 10 | No | Reason: FedAuth AzureActiveDirectoryService Initialization failed when trying to open service proxy. |
| 47021 | 10 | No | Reason: FedAuth AzureActiveDirectoryService Group Expansion failed when trying to initialize Heap object. |
| 47022 | 10 | No | Reason: FedAuth AzureActiveDirectoryService Group Expansion failed when trying to initialize Error object. |
| 47023 | 10 | No | Reason: FedAuth AzureActiveDirectoryService Group Expansion failed during Group Membership Lookup. |
| 47024 | 10 | No | Reason: FedAuth AzureActiveDirectoryService Group Expansion failed during validation of federated context. |
| 47025 | 10 | No | Reason: FedAuth AzureActiveDirectoryService Group Expansion failed due to unknown reason. |
| 47026 | 10 | No | Reason: Failure occurred when trying to fetch the HMAC signature of prelogin client nonce to set FeatureExtAck |
| 47027 | 10 | No | Reason: This was a non-Microsoft domain login attempt in a Non-SDS session. |
| 47028 | 10 | No | Reason: This FedAuth library is not supported by the security layer for authentication. |
| 47029 | 10 | No | Reason: This FedAuth Ticket type is not supported by the security layer for authentication. |
| 47030 | 10 | No | Reason: The Feature Switch for this FedAuth protocol is OFF. |
| 47031 | 10 | No | Reason: There was a failure in acquiring the max heap memory limit from config during AAD group expansion. |
| 47032 | 10 | No | Reason: There is a user error in FedAuth token parsing. There should be a separate XEvent called 'fedauth_web_token_failure' which indicate the actual error code |
| 47033 | 10 | No | Reason: There is a System error in FedAuth token parsing. There should be a separate XEvent called 'fedauth_web_token_failure' which indicate the actual error code |
| 47034 | 10 | No | Reason: Authentication was successful, but database is in Recovering state. |
| 47035 | 10 | No | Reason: Login failed because it was attempting to use integrated authentication, which we do not support. |
| 47036 | 10 | No | Reason: Login failed because USE db failed while checking firewall rules. |
| 47037 | 10 | No | Reason: Login failed because Deny External Connections flags is on. |
| 47038 | 10 | No | Reason: Login failed because client disconnected when fedauth specific processing was going on during login. |
| 47039 | 10 | No | Reason: Login failed because the client is attempting to use certificate authentication without correct permissions. |
| 47040 | 10 | No | Reason: Login failed because database is not found. |
| 47041 | 10 | No | Reason: Login failed because login token feature extension is not present. |
| 47042 | 10 | No | Reason: Login failed because login token feature extension is malformed. |
| 47043 | 10 | No | Reason: Token-based server access validation failed with an infrastructure error. Login is disabled. |
| 47044 | 10 | No | Reason: Login-based server access validation failed with an infrastructure error. Login is disabled. |
| 47045 | 10 | No | Reason: Token-based server access validation failed with an infrastructure error. Login lacks Connect SQL permission. |
| 47046 | 10 | No | Reason: Login-based server access validation failed with an infrastructure error. Login lacks Connect SQL permission. |
| 47047 | 10 | No | Reason: Token-based server access validation failed with an infrastructure error. Login lacks connect endpoint permission. |
| 47048 | 10 | No | Reason: Login-based server access validation failed with an infrastructure error. Login lacks connect endpoint permission. |
| 47049 | 10 | No | Reason: VNET Firewall Rule has rejected the login. |
| 47050 | 10 | No | Reason: Unexpected error while copying the VNET IPv6 address. |
| 47051 | 10 | No | Reason: Unexpected error while parsing the VNET IPv6. |
| 47052 | 10 | No | Reason: Unexpected error while extracting VNET metadata info from the IPv6 (VNET parsing). |
| 47053 | 10 | No | Reason: Unexpected error on VNET firewall rule table lookup. |
| 47054 | 10 | No | Reason: Unexpected error on VNET firewall rule table COUNT lookup. |
| 47055 | 10 | No | Reason: VNET Firewall rejected the login due to the source of a login being outside a VNET |
| 47056 | 10 | No | Reason: Firewall rejected the login attempt because VNET firewall rules are not database-level, only server-level. |
| 47057 | 10 | No | Reason: Firewall rejected the login because an IPv6 attempt was received when not expected |
| 47058 | 10 | No | Reason: Unexpected error on VNET firewall rule table lookup while looking up the IPv4 Allow All rule. |
| 47059 | 10 | No | Reason: Unexpected error while swapping the session peer address with the VNET CA. |
| 47060 | 10 | No | Reason: Unexpected error on TDS readhandler, payload length \> sni packet buf size |
| 47061 | 10 | No | Reason: Replicated Master is not ready at this point and user connections are disallowed. |
| 47062 | 10 | No | Reason: lock timeout expired while looking up interface endpoints list |
| 47063 | 10 | No | Reason: lock timeout expired while looking up interface endpoints list |
| 47064 | 10 | No | Reason: The incoming login VNET metadata was not found in the list of interface endpoints configured |
| 47065 | 10 | No | Reason: Unexpected error on Interface Endpoints access lockdown check |
| 47066 | 10 | No | Reason: Allow All Azure rule can't be evaluated at DB level Firewall Rule for Interface Endpoints connections |
| 47067 | 10 | No | Reason: Internal error. Unexpected frontend connection to Vldb page server. |
| 47068 | 10 | No | fulltext stoplist |
| 47100 | 16 | No | The cluster type of availability group '%.\*ls' doesn't match its primary configuration. Verify that the specified availability group cluster type is correct, then retry the operation. |
| 47101 | 16 | No | The cluster type of availability group '%.\*ls' only supports MANUAL failover mode. Verify that the specified availability group cluster type is correct, then retry the operation. |
| 47102 | 16 | No | The cluster type of availability group '%.\*ls' only supports EXTERNAL failover mode. Verify that the specified availability group cluster type is correct, then retry the operation. |
| 47103 | 16 | No | The cluster type of availability group '%.\*ls' only supports AUTOMATIC and MANUAL failover modes. Verify that the specified availability group cluster type is correct, then retry the operation. |
| 47104 | 16 | No | This operation cannot be performed on the availability group '%.\*ls' because it has EXTERNAL cluster type. Use the cluster management tools to perform the operation. |
| 47105 | 16 | No | The Always On Availability Groups feature must be enabled for this server instance before you can perform availability group operations. Please enable the feature, then retry the operation. |
| 47106 | 16 | No | Cannot join availability group '%.\*ls'. Download configuration timeout. Please check primary configuration, network connectivity and firewall setup, then retry the operation. |
| 47107 | 16 | No | The %ls operation is not allowed by the current availability group configuration. The availability group '%.\*ls' only supports one replica which has configuration-only availability mode. Verify that the specified availability group availability mode is correct, then retry the operation. |
| 47108 | 16 | No | The %ls operation is not allowed by the current availability group configuration. The availability group '%.\*ls' only supports two synchronous mode replicas and required_synchronized_secondaries_to_commit is zero when configuration-only mode replica is specified. Verify that the specified availability group availability mode is correct, then retry the operation. |
| 47109 | 16 | No | Availability group '%.\*ls' cannot failover to this replica. Configuration-only replica cannot become primary. An attempt to fail over an availability group failed. The replica is a configuration-only and cannot become a primary. |
| 47110 | 15 | No | The '%.\*ls' option is not valid for the '%.\*ls' replica as it is a configuration-only. Remove this option, then retry the operation |
| 47111 | 16 | No | Local availability replica for availability group '%.\*ls' cannot be granted permission to create databases. The replica is a configuration-only and cannot host databases inside the availability group. |
| 47112 | 16 | No | Only the SESSION_TIMEOUT property can be modified for a configuration-only replica. |
| 47113 | 16 | No | The '%.\*ls' option is not valid for the '%.\*ls' replica for modification. Remove this option, then retry the operation. |
| 47114 | 16 | No | Availability replica '%.\*ls' cannot be added to availability group '%.\*ls'. Configuration-only replicas can only be added to availability groups with CLUSTER_TYPE = EXTERNAL. Verify that the AVAILABILITY_MODE setting of the replica spec, then retry the operation. |
| 47115 | 16 | No | The '%ls' option is not valid for WSFC availability group '%.\*ls'. Remove the option or set the 'CLUSTER_TYPE' option to a different value, then retry the operation. |
| 47116 | 16 | No | The external lease cannot be set on availability group '%.\*ls'. External Lease updates are not enabled for this availability group. |
| 47117 | 16 | No | The '%ls' option must be specified with a valid time value when updating the Availability Group's external write lease status on availability group '%.\*ls'. |
| 47118 | 16 | No | The '%ls' option must be specified with a valid value when updating the external lease status on availability group '%.\*ls'. |
| 47119 | 16 | No | The current write lease of the availability group '%.\*ls' is still valid. The lease expiration time cannot be set to an earlier time than its current value. |
| 47120 | 16 | No | The READ_WRITE_ROUTING_URL '%.\*ls' specified for availability replica '%.\*ls' is not valid. It does not follow the required format of 'TCP://system-address:port'. For information about the correct routing URL format, see the CREATE AVAILABILITY GROUP documentation in SQL Server Books Online. |
| 47121 | 16 | No | The replica spec for the local replica '%.\*ls' has an invalid availability mode. Valid values are SYNCHRONOUS_COMMIT and ASYNCHRONOUS_COMMIT. |
| 47122 | 16 | No | Cannot failover an availability replica for availability group '%.\*ls' since it has CLUSTER_TYPE = NONE. Only force failover is supported in this version of SQL Server. |
| 47123 | 16 | No | Creating contained availability group '%.\*ls' failed. No other availability groups can exist before creating contained availability group. Verify specified availability group availability options are correct, then retry the operation. |
| 47124 | 16 | No | Creation of contained availability group master database failed. Only contained availability group can create contained availability group master database. Verify availability group availability options are correct, then retry the operation. |
| 47125 | 16 | No | Joining contained availability group '%.\*ls' has failed. Verify there is no other availability group existed, then retry the operation. |
| 47126 | 16 | No | Joining contained availability group '%.\*ls' failed. Verify that user databases are not present, then retry the operation. |
| 47127 | 16 | No | Joining or creating contained availability group '%.\*ls' failed. The SQL Server instance is not enabled to create or join contained availability group. |
| 47129 | 16 | No | Creating Distributed availability group '%.\*ls' has failed. Cannot create Distributed Availability Group on top of Contained Availability Group. Verify Availability Group's name, then retry the operation. |
| 47130 | 16 | No | Creating contained availability group '%.\*ls' failed. When creating contained availability group, both master database and msdb database must be included in contained availability group. Include master and msdb in CREATE AVAILABILITY GROUP statement and retry the operation. |
| 47131 | 16 | No | Creating or joining availability group '%.\*ls' failed because there is a contained availability group. Remove contained availability group, then retry the operation. |
| 47132 | 16 | No | Joining availability group '%.\*ls' with rebuilding contained system DB has failed because rebuilding contained MSDB has failed. This is caused by contained MSDB is still used. Retry the operation later. |
| 47133 | 16 | No | Joining availability group '%.\*ls' with rebuilding contained system databases has failed because rebuilding contained availability group master database has failed. This is caused by contained availability group master database still being used. Reconnect to master database at SQL Server instance level and retry the operation later. |
| 47134 | 16 | No | Joining availability group '%.\*ls' with 'REBUILD_SYSTEM_DATABASES' has failed. 'REBUILD_SYSTEM_DATABASES' is only valid for joining contained availability group. Remove the option and retry the operation. |
| 47135 | 16 | No | Contained availability group system database '%.\*ls' cannot be removed from contained availability group '%.\*ls'. |
| 47136 | 16 | No | Cannot add database '%.\*ls' to contained availability group '%.\*ls' because it is in the exclusion list. Remove the excluded database from the database list and retry the operation. |
| 47137 | 16 | No | Cannot create contained system databases in contained availability group '%.\*ls' It might be caused by temporary condition. Retry the operation. |
| 47138 | 16 | No | Joining contained availability group '%.\*ls' without rebuilding contained system DB has failed because availability group master '%.\*ls' had incorrect db id '%ld'. Please use rebuild_system_databases option or remove availability group contained master manually, then retry the operation again. |
| 47139 | 16 | No | Join contained availability group '%.\*ls' failed to create group master '%.\*ls' Database ID. Please retry the operation again. |
| 47140 | 16 | No | Starting contained availability group '%.\*ls' failed. Contained availability group system database '%.\*ls' (id = %d) failed to load during startup. |
| 47141 | 16 | No | Starting contained availability group '%.\*ls' failed. One of contained system databases (masterdbid = %d, msdbid = %d) failed to load during startup. |
| 47142 | 16 | No | Creating contained availability group '%.\*ls' failed. One of contained system database '%.\*ls' already exists. Remove it or add 'reuse_system_databases' option, then retry the operation again. |
| 47143 | 16 | No | Option 'reuse_system_databases' is invalid for creating availability group '%.\*ls'. Option 'reuse_system_databases' can only be used with contained availability group. Correct the option and retry the operation. |
| 47145 | 16 | No | Failed to obtain the resource handle for cluster resource with name or ID '%.\*ls'. Cluster service may not be running or may not be accessible in its current state, or the specified cluster resource name or ID is invalid. Otherwise, contact your primary support provider. |
| 47146 | 16 | No | Invalid characters '%ls' found in name '%.\*ls' for contained availability group. Remove invalid characters and retry the operation. |
| 47147 | 16 | No | Creating contained availability group '%.\*ls' failed. When creating contained availability group, neither master database nor msdb database can be included in the CREATE AVAILABILITY GROUP statement. They will be automatically included in the availability group. Remove master database name and msdb database name in CREATE AVAILABILITY GROUP statement and retry the operation. |
| 47148 | 16 | No | Cannot join database '%.\*ls' to contained availability group '%.\*ls'. Before joining other databases to contained availability group, contained availability group master database has to be joined and recovered. Make sure contained availability group master database has been joined and recovered, then retry the operation. |
| 47149 | 16 | No | Cannot start the job "%s" because this is secondary replica of contained availability group. Start the job in the primary replica of this contained availability group. |
| 47152 | 16 | No | Availability group '%.\*ls' that has EXTERNAL or NONE cluster type contains too many databases. Remove some databases from the availability group and retry the operation. |
| 47201 | 16 | No | Procedure expects '%u' parameters. |
| 47202 | 16 | No | Procedure expects '%u' parameters and '%u' for _ex version. |
| 47203 | 16 | No | Procedure expects at least '%u' parameters and '%u' max. |
| 47500 | 16 | No | Manual seeding is not supported option for secondary AG '%.\*ls' configuration when secondary participant in distributed availability group is Azure SQL Managed Instance. |
| 47501 | 16 | No | Synchronous commit is not supported option for the initial secondary AG '%.\*ls' configuration when secondary participant in distributed availability group is Azure SQL Managed Instance. |
| 47502 | 16 | No | Cannot create distributed availability group '%.\*ls' when local AG '%.\*ls' contains more than one database in cases when secondary participant is Azure SQL Managed Instance. |
| 47503 | 16 | No | AG '%.\*ls' already contains one database and adding more is not supported because AG participates in distributed availability group '%.\*ls' with secondary on Azure SQL Managed Instance. |
| 47504 | 16 | No | Error related to distributed availability group '%.\*ls' with secondary participant on Azure SQL Managed Instance. |
| 47505 | 16 | No | Endpoint with type %d does not exist or user does not have permission to view it. |
| 47506 | 16 | No | Certificate for endpoint with type %d does not exist or user does not have permission to view it. |
| 47507 | 16 | No | Adding memory optimized files to the database replicated to Azure SQL Managed Instance is not supported because its service tier does not support In-memory OLTP capabilities. Consider replicating database to managed instance service tier supporting In-memory OLTP capabilities. |
| 47508 | 16 | No | Adding multiple log files to the database replicated to Azure SQL Managed Instance is not supported because managed instance does not support multiple log files. |
| 47509 | 16 | No | Adding FileStream or FileTables to the database replicated to Azure SQL Managed Instance is not supported because managed instance does not support FileStream or FileTables. |
| 47510 | 16 | No | Adding multiple memory optimized files to the database replicated to Azure SQL Managed Instance is not supported because managed instance does not support multiple memory optimized files. |
| 49401 | 16 | Yes | Database backup not supported on this database as it has foreign files attached. |
| 49402 | 16 | Yes | Failed to initialize the covering resilient buffer pool extension for foreign file '%.\*ls' with HRESULT 0x%x. |
| 49403 | 17 | Yes | Database '%.\*ls' does not allow autostart operations. An explicit ONLINE database operation is required. |
| 49404 | 16 | No | Value '%.\*ls' for option '%.\*ls' is not supported in this version of SQL Server. |
| 49405 | 17 | Yes | Skipping the default startup of vldb database '%.\*ls'. The database will be started by the fabric. This is an informational message only. No user action is required. |
| 49406 | 16 | Yes | Recovery modes other than full are not supported in this version of SQL Server. |
| 49407 | 16 | No | Error: %ls. |
| 49408 | 16 | No | Error: %ls. Error code: %d. |
| 49409 | 16 | No | Special procedure is not available on the current SQL instance. |
| 49410 | 16 | Yes | Change tracking is currently not supported in this version of SQL Server. |
| 49411 | 16 | Yes | Couldn't register the database in the page server database map. |
| 49501 | 16 | No | DBCC SHRINKFILE for %.\*ls is aborted. Sbs flat files are not supported |
| 49502 | 10 | No | %.\*ls: Page %d:%d could not be moved because it is an sbs flat file page or the destination is an sbs flat file page. |
| 49503 | 10 | No | %.\*ls: Page %d:%d could not be moved because it is an off-row persistent version store page. Page holdup reason: %ls. Page holdup timestamp: %I64d. |
| 49504 | 10 | No | Error updating failover proc. |
| 49505 | 16 | No | Registering extended stored procedure failed because it is not valid to use UNC path for DLL name. Use local path for DLL name instead. |
| 49506 | 16 | No | The DBCC operation failed because it is not supported in an explicit transaction when Accelerated Database Recovery is enabled on the database. Commit or rollback the current transaction and run the operation again. |
| 49507 | 16 | No | Object ID %d, index ID %d, partition ID %I64d, alloc unit ID %I64d (type %.\*ls), page %S_PGID, row %d: Row is aborted. |
| 49508 | 16 | No | DBCC SHRINKFILE for data files is not supported in this version of SQL Server. |
| 49509 | 16 | No | DBCC SHRINKFILE for PMM files is not supported in this version of SQL Server. |
| 49510 | 16 | No | Managed instance is busy with another operation. Please try your operation later. |
| 49511 | 10 | No | Unable to set one or more trace flags. Unsupported trace flag(s): %ls%ls%ls. |
| 49512 | 10 | No | Session level trace flags are not supported on managed instance. |
| 49513 | 10 | No | %lsDBCC %ls for databese id %d terminated abnormally due to error state %d. Elapsed time: %d hours %d minutes %d seconds. %.\*ls |
| 49514 | 10 | No | %lsDBCC %ls for databese id %d found %d errors and repaired %d errors. Elapsed time: %d hours %d minutes %d seconds. %.\*ls |
| 49600 | 22 | No | SQL tiered storage table schema is corrupt. |
| 49602 | 16 | No | Failure waiting for %ls latch in '%ls'. |
| 49603 | 16 | No | CREATE FILE encountered operating system error %ls while attempting to copy the physical file '%.\*ls'. |
| 49701 | 10 | No | Server override on the category is not supported yet (Server: '%.\*ls', Category: '%.\*ls'). |
| 49702 | 10 | No | The category name is either invalid or not supported yet. Server: '%.\*ls'. CategoryName: '%.\*ls'. |
| 49703 | 10 | No | Failed to parse server override on server '%.\*ls'. The category name is: '%.\*ls' and the override string is: '%.\*ls'. |
| 49704 | 10 | No | Failed to apply server override on category '%.\*ls', because physical db or instance '%.\*ls' in server '%.\*ls' is currently not in 'Ready' or 'Deactivated' state. |
| 49705 | 10 | No | Failed to merge server override into property bag on physical db or instance '%.\*ls' of server '%.\*ls'. The override string is: '%.\*ls'. |
| 49716 | 10 | No | Database override on the category is not supported yet (Server: '%.\*ls', Database: '%.\*ls', Category: '%.\*ls'). |
| 49717 | 10 | No | Failed to apply database override on category '%.\*ls', because physical db or instance '%.\*ls' in server '%.\*ls' is currently not in 'Ready' or 'Deactivated' state. |
| 49718 | 10 | No | The category name is either invalid or not supported yet. Server: '%.\*ls'. Database: '%.\*ls'. CategoryName: '%.\*ls'. |
| 49801 | 10 | No | Dynamic Deactivation Timer task encountered an error (SQL Error Code: %d). Refer to the xel for more details. |
| 49802 | 10 | No | Database is unavailable at the moment, please retry connection at later time. |
| 49803 | 10 | No | Resource Pool Data Space Usage Timer task encountered an error (SQL Error Code: %d). |
| 49804 | 10 | No | Database cannot be deactivated: Server '%.\*ls', Database '%.\*ls', ServiceLevelObjective '%.\*ls' |
| 49805 | 10 | No | DynamicActivation feature switch is off for Server '%.\*ls', Database '%.\*ls', ServiceLevelObjective '%.\*ls' |
| 49806 | 10 | No | DynamicActivation feature switch is not enabled for all remote storage DB: Server '%.\*ls', Database '%.\*ls', ServiceLevelObjective '%.\*ls' |
| 49807 | 10 | No | DynamicActivation is only supported for remote storage DB: Logical Server '%.\*ls', Database '%.\*ls', ServiceLevelObjective '%.\*ls' |
| 49808 | 10 | No | Deactivated database cannot be deactivated again: Server '%.\*ls', Database '%.\*ls' |
| 49809 | 10 | No | Database Operation failed for Server '%.\*ls', Database '%.\*ls' due to unexpected delay. Please try again. |
| 49810 | 10 | No | Workflow failed due to throttling: Server '%.\*ls', Database '%.\*ls' |
| 49811 | 10 | No | Deactivation is not supported on Disabled database: Server '%.\*ls', Database '%.\*ls' |
| 49812 | 10 | No | EnableForceNoBackupDeactivation is not enabled: Server '%.\*ls', Database '%.\*ls' |
| 49813 | 10 | No | Deactivation is not supported on databases part of servers in global transaction partnership: Server '%.\*ls', Database '%.\*ls' |
| 49814 | 10 | No | DynamicActivation is not supported for GeoDR DB: Logical Server '%.\*ls', Database '%.\*ls', ServiceLevelObjective '%.\*ls' |
| 49815 | 10 | No | Database cannot be deactivated: Server '%.\*ls', Database '%.\*ls', ServiceLevelObjective '%.\*ls' |
| 49816 | 10 | No | Server '%.\*ls', Database '%.\*ls' cannot be online as BlockingMode is set |
| 49817 | 10 | No | Failed to query CMS for throttling on database '%.\*ls', '%.\*ls' due to the exception: '%.\*ls' |
| 49818 | 10 | No | Cannot deactivate a database when it is already getting deactivated, Server '%.\*ls', Database '%.\*ls' |
| 49819 | 10 | No | Deflation Monitor Timer task encountered an error (SQL Error Code: %d). Refer to the xel for more details. |
| 49820 | 10 | No | Managed Server Resource Stats Timer task encountered an error (SQL Error Code: %d). |
| 49821 | 10 | No | Rg Metrics Reporting Timer task encountered an error (SQL Error Code: %d). |
| 49822 | 10 | No | Move Cost Calculation and Reporting Timer task encountered an error (SQL Error Code: %d). |
| 49901 | 10 | No | The number of max worker threads that is configured %u is less than the minimum allowed on this computer. The default number of %u will be used instead. To change the number of max worker threads, use sp_configure 'max worker threads'. |
| 49902 | 10 | No | There are not enough worker threads available for the number of CPUs. This is because one or more CPUs were added. To increase the number of worker threads, use sp_configure 'max worker threads'. |
| 49903 | 10 | Yes | Detected %I64d MB of RAM. This is an informational message; no user action is required. |
| 49904 | 10 | Yes | The service account is '%.\*ls'. This is an informational message; no user action is required. |
| 49905 | 10 | Yes | Error %u occurred while opening parameters file '%s'. Verify that the file exists, and if it exists, verify that it is a valid parameters file. |
| 49906 | 10 | Yes | Error %u occurred while processing parameters from either the registry or the command prompt. Verify your parameters. |
| 49907 | 10 | Yes | Ignored deprecated SQL Server startup parameters from the registry: %.\*ls |
| 49908 | 10 | Yes | The following SQL Server startup parameters are either deprecated or incorrectly specified: %.\*ls |
| 49909 | 10 | Yes | Multiple instances of SQL server are installed on this computer. Renter the command, specifying the -s parameter with the name of the instance that you want to start. |
| 49910 | 10 | No | Software Usage Metrics is disabled. |
| 49911 | 10 | No | Software Usage Metrics failed to start. |
| 49912 | 10 | No | Software Usage Metrics is enabled. |
| 49913 | 10 | No | The server could not load DCOM. Software Usage Metrics cannot be started without DCOM. |
| 49914 | 10 | No | %ls Trace: %ls |
| 49915 | 10 | Yes | Invalid, incomplete, or deprecated parameters were found in the command line or in the registry. Normally those would be ignored but the '%s' parameter was specified which cause SQL Server to exit. Remove the offending parameters. Check the error log for further details. |
| 49916 | 10 | Yes | UTC adjustment: %d:%02u |
| 49917 | 10 | Yes | Default collation: %ls (%ls %u) |
| 49918 | 16 | No | Cannot process request. Not enough resources to process request. Please retry your request later. |
| 49919 | 16 | No | Cannot process create or update request. Too many create or update operations in progress for subscription "%ld". Query sys.dm_operation_status for pending operations. Wait till pending create/update requests are complete or delete one of your pending create/update requests and retry your request later. |
| 49920 | 16 | No | Cannot process request. Too many operations in progress for subscription "%ld". Query sys.dm_operation_status for pending operations and wait till the operation is complete or delete one of the pending requests and retry later. |
| 49921 | 10 | Yes | Total Log Writer threads: %ld. This is an informational message; no user action is required. |
| 49922 | 16 | No | Unable to process '%s' notification for subscription '%ld' because it contains '%d' child resources |
| 49924 | 16 | No | Subscription '%ld' does not support creating a database with selected service level objective '%ls'. Try creating a database with different service level objective. |
| 49925 | 16 | No | Databases cannot be updated to free service level objective. |
| 49926 | 10 | No | Server setup is starting |
| 49927 | 10 | No | An error occurred while setting the server administrator (SA) password: error %d, severity %d, state %d. |
| 49928 | 10 | No | An error occurred during server setup. See previous errors for more information. |
| 49929 | 10 | No | Server setup completed successfully. |
| 49930 | 10 | Yes | Parallel redo is %ls for database '%.\*ls' with worker pool size \[%d\]. |
| 49931 | 10 | No | An error occurred while configuring engine telemetry: error %d, severity %d, state %d. |
| 49932 | 10 | No | An error occurred while initializing security. %ls. |
| 49933 | 10 | No | ERROR: The MSSQL_SA_PASSWORD environment variable must be set when using the --reset-sa-password option. |
| 49934 | 10 | Yes | Error %u occurred while reading the RbIo configuration parameters. Verify that the sqlservr.ini or registry entries exist. |
| 49935 | 10 | Yes | Enclave of type %d initialized successfully. |
| 49936 | 10 | No | ERROR: The provided PID \[%s\] is invalid. The PID must be in the form #####-#####-#####-#####-##### where '#' is a number or letter. |
| 49937 | 10 | No | ERROR: A failure occurred in the licensing subsystem. Error \[%d\]. |
| 49938 | 10 | No | The licensing PID was successfully processed. The new edition is \[%s\]. |
| 49939 | 16 | No | Unable to initialize user-specified certificate configuration. The server is being shut down. Verify that the certificate is correctly configured. Error\[%d\]. State\[%d\]. |
| 49940 | 16 | No | Unable to open one or more of the user-specified certificate file(s). Verify that the certificate file(s) exist with read permissions for the user and group running SQL Server. |
| 49941 | 16 | No | Unable to load one or more of the user-specified certificate file(s). Verify that the certificate file(s) are of a supported format. |
| 49942 | 16 | No | Internal error occurred initializing user-specified certificate configuration. Error code \[%08X\]. |
| 49943 | 10 | No | The certificate \[Certificate File:'%hs', Private Key File:'%hs'\] was successfully loaded for encryption. |
| 49944 | 16 | No | The allowed TLS protocol version list \['%hs'\] is invalid. Verify that the supplied TLS version numbers are supported by SQL Server and separated by spaces in the configuration. |
| 49945 | 16 | No | The allowed TLS cipher list \['%hs'\] is invalid. See docs.microsoft.com for more information on creating a cipher list. |
| 49946 | 16 | No | Internal error occurred initializing the TLS configuration. Error code \[%d\]. |
| 49947 | 16 | No | Unable to initialize the TLS configuration. The server is being shut down. Verify that the allowed TLS protocol and cipher lists are configured correctly. Error state \[%d\]. |
| 49948 | 10 | No | Successfully initialized the TLS configuration. Allowed TLS protocol versions are \['%hs'\]. Allowed TLS ciphers are \['%hs'\]. |
| 49949 | 10 | No | ERROR: Unable to set system administrator password: %s. |
| 49950 | 10 | No | The SQL Server End-User License Agreement (EULA) must be accepted before SQL |
| 49951 | 10 | No | Server can start. The license terms for this product can be downloaded from |
| 49952 | 10 | No | [http://go.microsoft.com/fwlink/?LinkId=746388](https://go.microsoft.com/fwlink/?LinkId=746388). |
| 49953 | 10 | No | You can accept the EULA by specifying the --accept-eula command line option, |
| 49954 | 10 | No | setting the ACCEPT_EULA environment variable, or using the mssql-conf tool. |
| 49955 | 10 | No | Environment Variable Startup Parameters:%.\*ls |
| 49956 | 10 | No | The default language (LCID %d) has been set for engine and full-text services. |
| 49957 | 10 | No | The default language (LCID %d) failed to be set for engine and full-text services. |
| 49958 | 21 | No | The server collation cannot be changed with user databases attached. Please detach user databases before changing server collation. |
| 49959 | 10 | No | ERROR: The environment variable MSSQL_COLLATION contains an invalid collation '%.\*ls'. |
| 49960 | 10 | No | Did not find an existing master data file %s, copying the missing default master and other system database files. If you have moved the database location, but not moved the database files, startup may fail. To repair: shutdown SQL Server, move the master database to configured location, and restart. |
| 49961 | 10 | No | Setup step is %scopying system data file '%s' to '%s'. |
| 49962 | 10 | No | ERROR: Setup FAILED copying system data file '%s' to '%s': %s |
| 49963 | 10 | No | ERROR: '%s' is a directory. Cannot continue. |
| 49964 | 10 | No | ERROR: Setup failed to create the system data directory '%s': %s |
| 49965 | 10 | No | Unable to load cluster root CA certificate due to OSError:'%s'. |
| 49972 | 16 | No | Cannot add tempdb remote file to local tempdb filegroup in transition to primary. |
| 49973 | 16 | No | Cannot remove tempdb remote file to local tempdb filegroup in transition to primary. |
| 49975 | 10 | No | Unable to load controller client certificate due to OSError:'%s'. |
