---
author: rwestMSFT
ms.author: randolphwest
ms.date: 05/16/2024
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
| 41429 | 16 | No | Adding another database to an existing Azure SQL Managed Instance link is not supported, as a single link can contain only one user database. Create a new link for each additional database. |
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
| 41650 | 10 | No | Waiting for replica catchup for AGID '%ls' and ReplicaID '%ls' Failed. \[MakeLogUnavailable: %d\] |
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
| 41671 | 10 | No | Failed to transition to secondary role for physical database '%ls' (ID %d) of Windows Fabric partition '%ls' (partition ID '%ls'). |
| 41672 | 17 | No | Failed to obtain if the application is the VLDB reverse migration target. Application name: '%ls'. HRESULT \[%d\]. |
| 41673 | 17 | No | Failed to start up the log replica for physical database '%ls'. HRESULT \[%d\]. |
| 41674 | 17 | No | HyperScale Storage V1 to V2 migration source database '%ls' is already Storage V2. |
| 41675 | 17 | No | HyperScale Storage V1 to V2 migration for source database '%ls' is already started. |
| 41676 | 17 | No | Source database '%ls' is in geo relationship and is not supported for migration. |
| 41677 | 17 | No | Source database '%ls' does not match the logical database id provided. |
| 41678 | 16 | No | Failed to transition to PRIMARY role for physical database '%ls' (ID %d) of Windows Fabric partition '%ls' (partition ID '%ls'). |
| 41679 | 16 | No | Transition to PRIMARY role for physical database '%ls' (ID %d) of Windows Fabric partition '%ls' (partition ID '%ls') has been aborted. |
| 41681 | 16 | No | SQL Server cannot generate grip tds redirection url of the replica with ID %ls (Windows Fabric partition ID %ls). |
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
| 41805 | 16 | No | There is insufficient memory in the resource pool '%ls' to run this operation on memory-optimized tables. See 'http://go.microsoft.com/fwlink/?LinkID=614951' for more information. |
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
| 41823 | 16 | No | Could not perform the operation because the database has reached its quota for in-memory tables. This error may be transient. Please retry the operation. See 'http://go.microsoft.com/fwlink/?LinkID=623028' for more information. |
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
| 41840 | 16 | No | Could not perform the operation because the elastic pool or managed instance has reached its quota for in-memory tables. This error may be transient. Please retry the operation. See 'http://go.microsoft.com/fwlink/?LinkID=623028' for more information. |
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
| 41867 | 16 | No | Consistency errors detected in the MEMORY_OPTIMIZED_DATA filegroup. See preceding error messages for details. Consult https://go.microsoft.com/fwlink/?linkid=845604 for details on how to recover from the errors. |
| 41868 | 16 | No | Memory optimized filegroup checks could not be completed because of system errors. See errorlog for more details. |
| 41869 | 21 | No | In-memory OLTP version %u.%u is not supported on this platform. |
| 41870 | 10 | No | Dropped %d orphaned internal table(s). |
| 41871 | 16 | No | Failed to recreate XTP non-durable tables during recovery of the database '%.\*ls'. |
| 41872 | 10 | No | Warning: Articles with varchar(max), nvarchar(max) and varbinary(max) data type columns are not supported with memory optimized tables on subscribers running SQL Server 2014 or earlier. |
| 41873 | 16 | No | File in memory-optimized filegroup must have MAXSIZE set to be UNLIMITED. |
| 41874 | 10 | No | There are %d orphaned internal table(s) eligible for removal. Investigate the error log for the table identifiers. CHECKPOINT the database, create a full database backup, and re-run DBCC CHECKCATALOG with trace flag 9947 enabled to remove these tables. |
| 41875 | 16 | No | The operation on database '%ls' cannot complete because XTP engine is running in checkpoint-only mode due to insufficient memory. Memory-optimized tables are not accessible in this mode. Consider changing database service level objective to increase available memory. |
| 41876 | 16 | Yes | CREATE TABLE has failed for '%.\*ls' with error code 0x%x. This may be a transient condition, please retry. |
| 41899 | 20 | Yes | The operation caused an undefined internal error (HRESULT: %X). |
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
| 41937 | 16 | No | BACKUP WITH COPY_ONLY cannot be performed until after the next automatic BACKUP LOG operation |
| 41938 | 16 | No | BACKUP WITH COPY_ONLY cannot be performed since database encryption key for the database '%.\*ls' still exists. Retry command after you drop database encryption key. |
| 41939 | 16 | No | The Persistent Version Store filegroup must be set to the PRIMARY filegroup in order to be restored on SQL Database Managed Instance. |
| 41940 | 16 | No | Cross-db xact outcome cleanup failed due to unavailable/inaccessible databases. |
| 41941 | 10 | No | Changing recovery model for database with ID %lu is not supported and corresponding error has been suppressed as per server configuration. See https://go.microsoft.com/fwlink/?linkid=2112920 for more information. |
| 41942 | 16 | No | Renaming local instance is not allowed in SQL Database Managed Instance. |
| 41943 | 16 | No | SQL Database Managed Instance does not support creating route with TRANSPORT or MIRROR address. |
| 41944 | 16 | No | Cannot change the recovery model in Managed Instance Azure Arc environment |
| 41945 | 16 | No | Cannot rename managed database '%.\*ls' to an empty name. |
| 41946 | 16 | No | SQL Database Managed Instance does not support modifying default size of TempDb files. |
| 41947 | 16 | No | Memory-optimized filegroup must be empty on the SQL Server source database when replicated to Azure SQL Managed Instance with service tier not supporting In-memory OLTP capabilities. Consider using Managed Instance service tier that supports In-memory OLTP capabilities. |
| 41948 | 16 | No | Azure SQL Managed Instance link data replication requires source database on SQL Server to have a single log file instead of multiple log files. Consider using a single log file on the source database and try again. |
| 41949 | 16 | No | Managed Instance link cannot replicate data because maximum number of %u files were reached on Azure SQL Managed Instance. Consider reducing the number of files on the source and try again. |
| 41950 | 16 | No | Source database on SQL Server must not use FileStream or FileTables for data replication to Azure SQL Managed Instance. Consider removing FileStream and FileTables on the source database on SQL Server and try again. |
| 41951 | 16 | No | Azure SQL Managed Instance link cannot be created with source database '%.\*ls' on SQL Server, because the database contains files in DEFUNCT state. Remove the faulty files and retry again. |
| 41952 | 16 | No | Azure SQL Managed Instance link cannot be created with source database '%.\*ls' on SQL Server, bacause the database contains unsupported feature '%.\*ls'. |
| 41953 | 16 | No | Functionality cannot be used as it is not available on Azure SQL Managed Instance. |
| 41954 | 21 | No | Transient error occurred on database '%.\*ls' while trying to change managed instance role to primary. The system will automatically retry the operation. If the issue persists, contact Microsoft Support for Azure SQL Managed Instance. |
| 41955 | 21 | No | Transient error occurred while trying to retrieve backup metadata for database '%.\*ls'. The system will automatically retry the operation. If the issue persists, contact Microsoft Support for Azure SQL Managed Instance. |
| 41956 | 21 | No | Transient error occurred while trying to retrieve backup URIs property for database '%.\*ls'. The system will automatically retry the operation. If the issue persists, contact Microsoft Support for Azure SQL Managed Instance. |
| 41957 | 21 | No | Backup URIs property for database '%.\*ls' contains invalid data. Consider engaging Microsoft support for Azure SQL Managed Instance. |
| 41958 | 21 | No | Transient error occurred as backup metadata is unavailable for database '%.\*ls' due to error %u. The system will automatically retry the operation. If the issue persists, contact Microsoft Support for Azure SQL Managed Instance. |
| 41959 | 21 | No | Transient error occurred while reading from backup metadata for database '%.\*ls' due to error %u. The system will automatically retry the operation. If the issue persists, contact Microsoft Support for Azure SQL Managed Instance. |
| 41960 | 21 | No | Transient error occurred while writing to backup metadata for database '%.\*ls' due to error %u. The system will automatically retry the operation. If the issue persists, contact Microsoft Support for Azure SQL Managed Instance. |
| 41961 | 21 | No | Transient error occurred during the instance role change to primary. The system will automatically retry the operation. If the issue persists, contact Microsoft Support for Azure SQL Managed Instance. |
| 41962 | 16 | No | Operation was aborted as replication to Azure SQL Managed Instance did not start within %u minutes since it was initiated. Please verify the network connectivity and firewall rules are configured according to the guidelines described at https://aka.ms/mi-link-troubleshooting, and retry the operation. |
| 41963 | 16 | No | Certificate cannot be created as it is already used with '%.\*ls' feature. Consider using a different certificate. |
| 41964 | 16 | No | The '%.\*ls' operation cannot be completed as there exists a database in the process of creation through Azure SQL Managed Instance link. Please wait for the link creation to complete, or alternatively delete the link and retry the operation again. |
| 41965 | 16 | No | Setting the role cannot be applied as Azure SQL Managed Instance is already in the requested role. |
| 41966 | 16 | No | The '%.\*ls' operation cannot be completed as '%.\*ls' server configuration option is not supported. |
| 41967 | 16 | No | The '%.\*ls' operation cannot be completed as incorrect value was specified for '%.\*ls' server configuration option. |
| 41968 | 16 | No | Changing the server role cannot be executed at this time as there exist a previous role change in progress on Azure SQL Managed Instance. Only when the previous role change completes, a new request could be submitted. Please wait for the previous role change to complete and retry again. |
| 41969 | 16 | No | Setting the role has failed due to an unknown reason. Please check the error log file. If the issue persists, contact Azure support. |
| 41970 | 16 | No | Switching to Primary role in planned fashion is not supported. |
| 41973 | 16 | No | The link cannot be established because the endpoint certificate from SQL Server has not been imported to Azure SQL Managed Instance. Please import the endpoint certificate from SQL Server to Managed Instance, and retry the link creation again. Please see online documentation for Managed Instance link for more information. |
| 41974 | 16 | No | The link cannot be established because the endpoint certificate from SQL Azure SQL Managed Instance has not been imported to SQL Server. Please download the endpoint certificate from Managed Instance and import it to SQL Server, and retry the link creation again. Please see online documentation for Managed Instance link for more information. |
| 41975 | 16 | No | Unsupported storage type. Azure SQL Managed Instance does not support database backup to the provided storage type. |
| 41976 | 16 | No | Connection with availability group '%.\*ls' on the server with endpoint '%.\*ls' cannot be established as it is not responding. Possible causes could be nonexistence of availability group or distributed availability group on the partner server, incorrectly specified names or configuration parameters. Please check the log file on the partner server for the exact error cause. |
| 41977 | 16 | No | Connection with availability group '%.\*ls' on the server with endpoint '%.\*ls' is established, but there is no response from the target database. Possible causes could be errors with creating a database on the partner server, incorrectly specified names or configuration parameters. Please check the log file on the partner server for the exact error cause. |
| 41978 | 16 | No | The parameters (%ls) are not supported for this stored procedure in SQL Database Managed Instance. Review the documentation for supported parameters. |
| 41986 | 16 | No | Azure SQL Managed Instance link creation failed because either connection to SQL Server failed, or the server did not respond after a prolonged period of time. Verify the network connectivity and firewall rules between SQL Server and Managed Instance are properly configured and retry again. |
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
| 42035 | 16 | No | %ls operation timed out. Request was sent to the service resulting in response timeout. The operation might start anyway. |
| 42036 | 16 | No | %ls operation has been cancelled. |
| 42101 | 10 | No | Reason: Unable to create or update firewall rules since Deny Public Network Access is set to Yes. Please set Deny Public Network Access to No and retry the operation ([https://learn.microsoft.com/azure/azure-sql/database/connectivity-settings#deny-public-network-access](/azure/azure-sql/database/connectivity-settings#deny-public-network-access)). |
| 42102 | 10 | No | Reason: Unable to set Deny Public Network Access to Yes since there is no private endpoint enabled to access the server. Please set up private endpoints and retry the operation ([https://learn.microsoft.com/azure/sql-database/sql-database-private-endpoint-overview#how-to-set-up-private-link-for-azure-sql-database](/azure/sql-database/sql-database-private-endpoint-overview#how-to-set-up-private-link-for-azure-sql-database)). |
| 42103 | 16 | No | The SQL Pool '%.\*ls' already exists. Please choose a new name. |
| 42104 | 16 | No | Could not create SQL Pool '%.\*ls'. Please try again later. |
| 42105 | 16 | No | The server role '%.\*ls' already exists. Please choose a new name. |
| 42106 | 16 | No | Could not create Server Role '%.\*ls'. Please try again later. |
| 42107 | 16 | No | Could not create SQL Pool '%.\*ls', it must be less than %d characters. |
| [42108](../mssqlserver-42108-database-engine-error.md) | 16 | No | Can not connect to the SQL pool since it is paused. Please resume the SQL pool and try again. |
| [42109](../mssqlserver-42109-database-engine-error.md) | 16 | No | The SQL pool is warming up. Please try again. |
| 42110 | 16 | No | The IPv6 firewall rule name cannot be empty. |
| 42111 | 16 | No | '%.\*ls' is not a valid IPv6 address. |
| 42112 | 16 | No | The IPv6 address that starts with '%.\*ls' is too long. Maximum length is %d. |
| 42113 | 16 | No | Start IPv6 address of firewall rule cannot exceed End IPv6 address. |
| 42114 | 16 | No | Windows Azure SQL Database supports a maximum of 128 IPv6 firewall rules. |
| 42115 | 16 | No | IPv6 Feature Support is not Enabled |
| 42116 | 11 | No | Cannot open database "%.\*ls" requested by the login because this database is currently being dropped. The login failed. |
| 42117 | 16 | No | The elastic pool '%.\*ls' has reached its active database count limit. The active database count for the elastic pool cannot exceed (%d) for the current service tier. |
| 42118 | 16 | No | Login failed because the network security perimeter denied inbound access. |
