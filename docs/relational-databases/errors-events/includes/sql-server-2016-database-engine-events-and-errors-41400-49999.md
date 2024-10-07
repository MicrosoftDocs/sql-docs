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
| 41410 | 16 | No | In this availability group, at least one synchronous replica is not currently synchronized. The replica synchronization state could be either SYNCHONIZING or NOT SYNCHRONIZING. |
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
| 41643 | 16 | No | Could not retrieve the Layered AG Configuration for database '%ls' (URI: '%ls', partition ID '%ls') . Encountered error (error code: 0x%08X). |
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
| 41821 | 16 | No | Stored procedure 'sp_xtp_merge_checkpoint_files' is no longer needed and therefore not allowed. SQL Server will automatically merge the files based on an internal merge policy. |
| 41822 | 17 | No | There is insufficient disk space to generate checkpoint files and as a result the database is in delete-only mode. In this mode, only deletes and drops are allowed. |
| 41823 | 16 | No | Could not perform the operation because the database has reached its quota for in-memory tables. See '[http://go.microsoft.com/fwlink/?LinkID=623028](https://go.microsoft.com/fwlink/?LinkID=623028)' for more information. |
| 41824 | 16 | No | The transaction was killed by a concurrent ALTER operation or by a write-write conflict. |
| 41825 | 16 | No | Stored procedure '%.\*ls' cannot be used to increase the user memory limit on the database. |
| 41826 | 16 | No | Stored procedure '%.\*ls' tried to set a lower limit to the user memory quota. The operation failed because the user memory consumption is larger than the specified target, delete some user data and try the operation again. |
| 41827 | 16 | No | Upgrade of XTP physical database '%.\*ls' requires restart of XTP engine. |
| 41828 | 16 | No | Creation of memory-optimized tables is temporarily disabled. Please try again later. |
| 41829 | 16 | No | The database cannot proceed with SLO update as it has memory-optimized tables. Please drop such tables and try again. |
| 41830 | 16 | No | Upgrade of XTP physical database '%.\*ls' restarted XTP engine. |
| 41831 | 16 | No | Data migration on table id %d failed either fully or partially. See errorlog for details. |
| 41832 | 16 | No | Index '%.\*ls' cannot be created on table '%.\*ls', because at least one key column is stored off-row. Index key columns memory-optimized tables must fit within the %d byte limit for in-row data. Simplify the index key or reduce the size of the columns to fit within %d bytes. |
| 41833 | 16 | No | Columnstore index '%.\*ls' cannot be created, because table '%.\*ls' has columns stored off-row. Columnstore indexes can only be created on memory-optimized table if the columns fit within the %d byte limit for in-row data. Reduce the size of the columns to fit within %d bytes. |
| 41834 | 16 | No | ALTER TABLE has failed for '%.\*ls' with error code %d. |
| 41835 | 21 | No | An error (error code: 0x%08lx) occured while adding encryption keys to XTP database '%.\*ls'. |
| 41836 | 16 | No | Rebuilding log is not supported for databases containing files belonging to MEMORY_OPTIMIZED_DATA filegroup. |
| 41837 | 16 | No | Boot-page adjustment of XTP database '%.\*ls' requires restart of XTP engine. |
| 41839 | 16 | No | Transaction exceeded the maximum number of commit dependencies and the last statement was aborted. Retry the statement. |
| 41872 | 10 | No | Warning: Article with '%ls' data type column is not supported with memory optimized tables on subscribers running SQL Server 2014 or earlier. |
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
| 45301 | 17 | No | The resource has been moved to another location |
| 45302 | 17 | No | SLO '%ls' operation cannot succeed as the memory usage of '%ls' exceeds the quota. |
| 45303 | 17 | No | Attempt to cancel activation or rollback activation automatically because of operation timeout, but this is not supported in current state. Please check database status after operation is finished. |
| 45304 | 16 | No | Elastic pool estimate '%.\*ls' was not found for server '%.\*ls' |
| 45305 | 16 | No | Request could not be processed because of conflict in the request: '%.\*ls' |
| 45306 | 16 | No | The external admin cannot be set because the user or group already exists in the 'master' database. |
| 45307 | 16 | No | Advisor '%.\*ls' was not found for requested resource |
| 45308 | 16 | No | Recommended action '%.\*ls' was not found for advisor '%.\*ls' |
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
| 46516 | 15 | No | The specified credential cannot be found. |
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
| 46721 | 16 | No | Login failed. The login is from an untrusted domain and cannot be used with Windows authentication. |
| 46722 | 16 | No | Client driver version is not supported. |
| 46801 | 16 | No | GlobalQuery operations |
| 46802 | 16 | No | Failed to load module for global query. |
| 46803 | 16 | No | Failed to locate entry point for global query module. |
| 46804 | 16 | No | Failed to initialize the global query module. |
| 46805 | 16 | No | Conversion error while constructing the GlobalQuery request. |
| 46806 | 16 | No | An error occured while executing GlobalQuery operation: %.\*ls |
| 46807 | 16 | No | An access violation occurred while performing GlobalQuery operation. Execution has been aborted. |
| 46808 | 16 | No | An entry corresponding to given Global Query request could not be located. Execution has been aborted. |
| 46809 | 16 | No | An error occurred while writing the response to Global Query request. Error Code: %d, State: %d. |
| 46810 | 16 | No | An unhandled exception occured while performing GlobalQuery operation. Execution has been aborted. |
| 46811 | 16 | No | An unexpected error with code 0x%x occured while executing GlobalQuery operation. |
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
| 46827 | 16 | No | The option '%ls' must be turned ON to execute requests referencing external tables. |
| 46828 | 16 | No | The USE PLAN hint is not supported for queries that reference external tables. Consider removing USE PLAN hint. |
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
| 46920 | 16 | No | An internal error occured while attempting to find the currently executing process. |
| 46921 | 16 | No | An internal error occured due to a Polybase dll path being too long. |
| 46922 | 16 | No | The current edition is suitable for Polybase usage, however, the Polybase dlls are not up to date. Please apply the latest patch again. |
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
| 47026 | 10 | No | Reason: Failure occured when trying to fetch the HMAC signature of prelogin client nonce to set FeatureExtAck |
| 47027 | 10 | No | Reason: This was a non-Microsoft domain login attempt in a Non-SDS session. |
| 47028 | 10 | No | Reason: This FedAuth library is not supported by the security layer for authentication. |
| 47029 | 10 | No | Reason: This FedAuth Ticket type is not supported by the security layer for authentication. |
| 47030 | 10 | No | Reason: The Feature Switch for this FedAuth protocol is OFF. |
| 47031 | 10 | No | Reason: There was a failure in acquiring the max heap memory limit from config during AAD group expansion. |
| 47032 | 10 | No | Reason: There is a user error in FedAuth token parsing. There should be a seperate XEvent called 'fedauth_web_token_failure' which indicate the actual eroor code |
| 47033 | 10 | No | Reason: There is a System error in FedAuth token parsing. There should be a seperate XEvent called 'fedauth_web_token_failure' which indicate the actual eroor code |
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
| 47500 | 16 | No | Manual seeding is not supported option for secondary AG '%.\*ls' configuration when secondary participant in distributed availability group is Azure SQL Managed Instance. |
| 47501 | 16 | No | Synchronous commit is not supported option for the initial secondary AG '%.\*ls' configuration when secondary participant in distributed availability group is Azure SQL Managed Instance. |
| 47502 | 16 | No | Cannot create distributed availability group '%.\*ls' when local AG '%.\*ls' contains more than one database in cases when secondary participant is Azure SQL Managed Instance. |
| 47503 | 16 | No | AG '%.\*ls' already contains one database and adding more is not supported because AG participates in distributed availability group '%.\*ls' with secondary on Azure SQL Managed Instance. |
| 47504 | 16 | No | Error related to distributed availability group '%.\*ls' with secondary participant on Azure SQL Managed Instance. |
| 49701 | 10 | No | Server override on DB RG is not supported yet (Server: '%.\*ls', Category: '%.\*ls'). |
| 49702 | 10 | No | The category name is either invalid or not supported yet. Server: '%.\*ls'. CategoryName: '%.\*ls'. |
| 49703 | 10 | No | Failed to parse server override on server '%.\*ls'. The category name is: '%.\*ls' and the override string is: '%.\*ls'. |
| 49704 | 10 | No | Failed to apply server override on category '%.\*ls', because physical db or instance '%.\*ls' in server '%.\*ls' is currently not in 'Ready' or 'Deactivated' state. |
| 49705 | 10 | No | Failed to merge server override into property bag on physical db or instance '%.\*ls' of server '%.\*ls'. The override string is: '%.\*ls'. |
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
| 49918 | 16 | No | Cannot process request. Not enough resources to process request. Please retry you request later. |
| 49919 | 16 | No | Cannot process create or update request. Too many create or update operations in progress for subscription "%ld". Query sys.dm_operation_stats for pending operations. Wait till pending create/update requests are complete or delete one of your pending create/update requests and retry your request later. |
| 49920 | 16 | No | Cannot process request. Too many operations in progress for subscription "%ld". Query sys.dm_operation_stats for pending operations and wait till the operation is complete or delete one of the pending requests and retry later. |
| 49922 | 16 | No | Unable to process '%s' notification for subscription '%ld' because it contains '%d' child resources |
| 49924 | 16 | No | Subscription '%ld' does not support creating a database with selected service level objective '%ls'. Try creating a database with different service level objective. |
| 49925 | 16 | No | Databases cannot be updated to free service level objective. |
