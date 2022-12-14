---
title: "sys.dm_tran_locks (Transact-SQL)"
description: sys.dm_tran_locks (Transact-SQL)
author: rwestMSFT
ms.author: randolphwest
ms.date: "03/30/2017"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "dm_tran_locks"
  - "sys.dm_tran_locks"
  - "sys.dm_tran_locks_TSQL"
  - "dm_tran_locks_TSQL"
helpviewer_keywords:
  - "sys.dm_tran_locks dynamic management view"
dev_langs:
  - "TSQL"
ms.assetid: f0d3b95a-8a00-471b-9da4-14cb8f5b045f
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# sys.dm_tran_locks (Transact-SQL)
[!INCLUDE [sql-asdb-asdbmi-asa-pdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

  Returns information about currently active lock manager resources in [!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)]. Each row represents a currently active request to the lock manager for a lock that has been granted or is waiting to be granted.  
  
 The columns in the result set are divided into two main groups: resource and request. The resource group describes the resource on which the lock request is being made, and the request group describes the lock request.  
  
> [!NOTE]  
> To call this from [!INCLUDE[ssSDWfull](../../includes/sssdwfull-md.md)] or [!INCLUDE[ssPDW](../../includes/sspdw-md.md)], use the name **sys.dm_pdw_nodes_tran_locks**. [!INCLUDE[synapse-analytics-od-unsupported-syntax](../../includes/synapse-analytics-od-unsupported-syntax.md)] 
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**resource_type**|**nvarchar(60)**|Represents the resource type. The value can be one of the following: DATABASE, FILE, OBJECT, PAGE, KEY, EXTENT, RID, APPLICATION, METADATA, HOBT, or ALLOCATION_UNIT.|  
|**resource_subtype**|**nvarchar(60)**|Represents a subtype of **resource_type**. Acquiring a subtype lock without holding a nonsubtyped lock of the parent type is technically valid. Different subtypes do not conflict with each other or with the nonsubtyped parent type. Not all resource types have subtypes.|  
|**resource_database_id**|**int**|ID of the database under which this resource is scoped. All resources handled by the lock manager are scoped by the database ID.|  
|**resource_description**|**nvarchar(256)**|Description of the resource that contains only information that is not available from other resource columns.|  
|**resource_associated_entity_id**|**bigint**|ID of the entity in a database with which a resource is associated. This can be an object ID, Hobt ID, or an Allocation Unit ID, depending on the resource type.|  
|**resource_lock_partition**|**Int**|ID of the lock partition for a partitioned lock resource. The value for nonpartitioned lock resources is 0.|  
|**request_mode**|**nvarchar(60)**|Mode of the request. For granted requests, this is the granted mode; for waiting requests, this is the mode being requested. <br /><br /> NULL = No access is granted to the resource. Serves as a placeholder.<br /><br /> Sch-S (Schema stability) = Ensures that a schema element, such as a table or index, is not dropped while any session holds a schema stability lock on the schema element.<br /><br /> Sch-M (Schema modification) = Must be held by any session that wants to change the schema of the specified resource. Ensures that no other sessions are referencing the indicated object.<br /><br /> S (Shared) = The holding session is granted shared access to the resource.<br /><br /> U (Update) = Indicates an update lock acquired on resources that may eventually be updated. It is used to prevent a common form of deadlock that occurs when multiple sessions lock resources for potential update in the future.<br /><br /> X (Exclusive) = The holding session is granted exclusive access to the resource.<br /><br /> IS (Intent Shared) = Indicates the intention to place S locks on some subordinate resource in the lock hierarchy.<br /><br /> IU (Intent Update) = Indicates the intention to place U locks on some subordinate resource in the lock hierarchy.<br /><br /> IX (Intent Exclusive) = Indicates the intention to place X locks on some subordinate resource in the lock hierarchy.<br /><br /> SIU (Shared Intent Update) = Indicates shared access to a resource with the intent of acquiring update locks on subordinate resources in the lock hierarchy.<br /><br /> SIX (Shared Intent Exclusive) = Indicates shared access to a resource with the intent of acquiring exclusive locks on subordinate resources in the lock hierarchy.<br /><br /> UIX (Update Intent Exclusive) = Indicates an update lock hold on a resource with the intent of acquiring exclusive locks on subordinate resources in the lock hierarchy.<br /><br /> BU = Used by bulk operations.<br /><br /> RangeS_S (Shared Key-Range and Shared Resource lock) = Indicates serializable range scan.<br /><br /> RangeS_U (Shared Key-Range and Update Resource lock) = Indicates serializable update scan.<br /><br /> RangeI_N (Insert Key-Range and Null Resource lock) = Used to test ranges before inserting a new key into an index.<br /><br /> RangeI_S = Key-Range Conversion lock, created by an overlap of RangeI_N and S locks.<br /><br /> RangeI_U = Key-Range Conversion lock, created by an overlap of RangeI_N and U locks.<br /><br /> RangeI_X = Key-Range Conversion lock, created by an overlap of RangeI_N and X locks.<br /><br /> RangeX_S = Key-Range Conversion lock, created by an overlap of RangeI_N and RangeS_S. locks.<br /><br /> RangeX_U = Key-Range Conversion lock, created by an overlap of RangeI_N and RangeS_U locks.<br /><br /> RangeX_X (Exclusive Key-Range and Exclusive Resource lock) = This is a conversion lock used when updating a key in a range.|  
|**request_type**|**nvarchar(60)**|Request type. The value is LOCK.|  
|**request_status**|**nvarchar(60)**|Current status of this request. Possible values are GRANTED, CONVERT, WAIT, LOW_PRIORITY_CONVERT, LOW_PRIORITY_WAIT, or ABORT_BLOCKERS. For more information about low priority waits and abort blockers, see the *low_priority_lock_wait* section of [ALTER INDEX &#40;Transact-SQL&#41;](../../t-sql/statements/alter-index-transact-sql.md).|  
|**request_reference_count**|**smallint**|Returns an approximate number of times the same requestor has requested this resource.|  
|**request_lifetime**|**int**|[!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]|  
|**request_session_id**|**int**|Session ID that currently owns this request. The owning session ID can change for distributed and bound transactions. A value of -2 indicates that the request belongs to an orphaned distributed transaction. A value of -3 indicates that the request belongs to a deferred recovery transaction, such as, a transaction for which a rollback has been deferred at recovery because the rollback could not be completed successfully.|  
|**request_exec_context_id**|**int**|Execution context ID of the process that currently owns this request.|  
|**request_request_id**|**int**|Request ID (batch ID) of the process that currently owns this request. This value will change every time that the active Multiple Active Result Set (MARS) connection for a transaction changes.|  
|**request_owner_type**|**nvarchar(60)**|Entity type that owns the request. Lock manager requests can be owned by a variety of entities. Possible values are:<br /><br /> TRANSACTION = The request is owned by a transaction.<br /><br /> CURSOR = The request is owned by a cursor.<br /><br /> SESSION = The request is owned by a user session.<br /><br /> SHARED_TRANSACTION_WORKSPACE = The request is owned by the shared part of the transaction workspace.<br /><br /> EXCLUSIVE_TRANSACTION_WORKSPACE = The request is owned by the exclusive part of the transaction workspace.<br /><br /> NOTIFICATION_OBJECT = The request is owned by an internal [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] component. This component has requested the lock manager to notify it when another component is waiting to take the lock. The FileTable feature is a component that uses this value.<br /><br /> **Note:** Work spaces are used internally to hold locks for enlisted sessions.|  
|**request_owner_id**|**bigint**|ID of the specific owner of this request.<br /><br /> When a transaction is the owner of the request, this value contains the transaction ID.<br /><br /> When a FileTable is the owner of the request, **request_owner_id** has one of the following values:<br /> <ul><li>**-4** : A FileTable has taken a database lock.<li> **-3** : A FileTable has taken a table lock.<li> **Other value** : The value represents a file handle. This value also appears as **fcb_id** in the dynamic management view [sys.dm_filestream_non_transacted_handles &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-filestream-non-transacted-handles-transact-sql.md).</li></ul>|  
|**request_owner_guid**|**uniqueidentifier**|GUID of the specific owner of this request. This value is only used by a distributed transaction where the value corresponds to the MS DTC GUID for that transaction.|  
|**request_owner_lockspace_id**|**nvarchar(32)**|[!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)] This value represents the lockspace ID of the requestor. The lockspace ID determines whether two requestors are compatible with each other and can be granted locks in modes that would otherwise conflict with one another.|  
|**lock_owner_address**|**varbinary(8)**|Memory address of the internal data structure that is used to track this request. This column can be joined the with **resource_address** column in **sys.dm_os_waiting_tasks**.|  
|**pdw_node_id**|**int**|**Applies to**: [!INCLUDE[ssSDWfull](../../includes/sssdwfull-md.md)], [!INCLUDE[ssPDW](../../includes/sspdw-md.md)]<br /><br /> The identifier for the node that this distribution is on.|  
  
## Permissions
On [!INCLUDE[ssNoVersion_md](../../includes/ssnoversion-md.md)] and SQL Managed Instance, requires `VIEW SERVER STATE` permission.

On SQL Database **Basic**, **S0**, and **S1** service objectives, and for databases in **elastic pools**, the [server admin](/azure/azure-sql/database/logins-create-manage#existing-logins-and-user-accounts-after-creating-a-new-database) account, the [Azure Active Directory admin](/azure/azure-sql/database/authentication-aad-overview#administrator-structure) account, or membership in the `##MS_ServerStateReader##` [server role](/azure/azure-sql/database/security-server-roles) is required. On all other SQL Database service objectives, either the `VIEW DATABASE STATE` permission on the database, or membership in the `##MS_ServerStateReader##` server role is required.   
 
## Remarks  
 A granted request status indicates that a lock has been granted on a resource to the requestor. A waiting request indicates that the request has not yet been granted. The following waiting-request types are returned by the **request_status** column:  
  
-   A convert request status indicates that the requestor has already been granted a request for the resource and is currently waiting for an upgrade to the initial request to be granted.  
  
-   A wait request status indicates that the requestor does not currently hold a granted request on the resource.  
  
 Because **sys.dm_tran_locks** is populated from internal lock manager data structures, maintaining this information does not add extra overhead to regular processing. Materializing the view does require access to the lock manager internal data structures. This can have minor effects on the regular processing in the server. These effects should be unnoticeable and should only affect heavily used resources. Because the data in this view corresponds to live lock manager state, the data can change at any time, and rows are added and removed as locks are acquired and released. Applications querying this view might experience unpredictable performance due to the nature of protecting the integrity of lock manager structures. This view has no historical information.  
  
 Two requests operate on the same resource only if all the resource-group columns are equal.  
  
 You can control the locking of read operations by using the following tools:  
  
-   SET TRANSACTION ISOLATION LEVEL to specify the level of locking for a session. For more information, see [SET TRANSACTION ISOLATION LEVEL &#40;Transact-SQL&#41;](../../t-sql/statements/set-transaction-isolation-level-transact-sql.md).  
  
-   Locking table hints to specify the level of locking for an individual reference of a table in a FROM clause. For syntax and restrictions, see [Table Hints &#40;Transact-SQL&#41;](../../t-sql/queries/hints-transact-sql-table.md).  
  
 A resource that is running under one session ID can have more than one granted lock. Different entities that are running under one session can each own a lock on the same resource, and the information is displayed in the **request_owner_type** and **request_owner_id** columns that are returned by **sys.dm_tran_locks**. If multiple instances of the same **request_owner_type** exist, the **request_owner_id** column is used to distinguish each instance. For distributed transactions, the **request_owner_type** and the **request_owner_guid** columns will show the different entity information.  
  
 For example, Session S1 owns a shared lock on **Table1**; and transaction T1, which is running under session S1, also owns a shared lock on **Table1**. In this case, the **resource_description** column that is returned by **sys.dm_tran_locks** will show two instances of the same resource. The **request_owner_type** column will show one instance as a session and the other as a transaction. Also, the **resource_owner_id** column will have different values.  
  
 Multiple cursors that run under one session are indistinguishable and are treated as one entity.  
  
 Distributed transactions that are not associated with a session ID value are orphaned transactions and are assigned the session ID value of -2. For more information, see [KILL &#40;Transact-SQL&#41;](../../t-sql/language-elements/kill-transact-sql.md).  

## <a name="locks"></a> Locks
Locks are held on [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] resources, such as rows read or modified during a transaction, to prevent concurrent use of resources by different transactions. For example, if an exclusive (X) lock is held on a row within a table by a transaction, no other transaction can modify that row until the lock is released. Minimizing locks increases concurrency, which can improve performance. 

## Resource Details  
 The following table lists the resources that are represented in the **resource_associated_entity_id** column.  
  
|Resource type|Resource description|Resource_associated_entity_id|  
|-------------------|--------------------------|--------------------------------------|  
|DATABASE|Represents a database.|Not applicable|  
|FILE|Represents a database file. This file can be either a data or a log file.|Not applicable|  
|OBJECT|Represents a database object. This object can be a data table, view, stored procedure, extended stored procedure, or any object that has an object ID.|Object ID|  
|PAGE|Represents a single page in a data file.|HoBt ID. This value corresponds to **sys.partitions.hobt_id**. The HoBt ID is not always available for PAGE resources because the HoBt ID is extra information that can be provided by the caller, and not all callers can provide this information.|  
|KEY|Represents a row in an index.|HoBt ID. This value corresponds to **sys.partitions.hobt_id**.|  
|EXTENT|Represents a data file extent. An extent is a group of eight contiguous pages.|Not applicable|  
|RID|Represents a physical row in a heap.|HoBt ID. This value corresponds to **sys.partitions.hobt_id**. The HoBt ID is not always available for RID resources because the HoBt ID is extra information that can be provided by the caller, and not all callers can provide this information.|  
|APPLICATION|Represents an application specified resource.|Not applicable|  
|METADATA|Represents metadata information.|Not applicable|  
|HOBT|Represents a heap or a B-tree. These are the basic access path structures.|HoBt ID. This value corresponds to **sys.partitions.hobt_id**.|  
|ALLOCATION_UNIT|Represents a set of related pages, such as an index partition. Each allocation unit covers a single Index Allocation Map (IAM) chain.|Allocation Unit ID. This value corresponds to **sys.allocation_units.allocation_unit_id**.|  

[!INCLUDE [sql-b-tree](../../includes/sql-b-tree.md)]
  
 The following table lists the subtypes that are associated with each resource type.  
  
|ResourceSubType|Synchronizes|  
|---------------------|------------------|  
|ALLOCATION_UNIT.BULK_OPERATION_PAGE|Pre-allocated pages used for bulk operations.|  
|ALLOCATION_UNIT.PAGE_COUNT|Allocation unit page count statistics during deferred drop operations.|  
|DATABASE.BULKOP_BACKUP_DB|Database backups with bulk operations.|  
|DATABASE.BULKOP_BACKUP_LOG|Database log backups with bulk operations.|  
|DATABASE.CHANGE_TRACKING_CLEANUP|Change tracking cleanup tasks.|  
|DATABASE.CT_DDL|Database and table-level change tracking DDL operations.|  
|DATABASE.CONVERSATION_PRIORITY|Service Broker conversation priority operations such as CREATE BROKER PRIORITY.|  
|DATABASE.DDL|Data definition language (DDL) operations with filegroup operations, such as drop.|  
|DATABASE.ENCRYPTION_SCAN|TDE encryption synchronization.|  
|DATABASE.PLANGUIDE|Plan guide synchronization.|  
|DATABASE.RESOURCE_GOVERNOR_DDL|DDL operations for resource governor operations such as ALTER RESOURCE POOL.|  
|DATABASE.SHRINK|Database shrink operations.|  
|DATABASE.STARTUP|Used for database startup synchronization.|  
|FILE.SHRINK|File shrink operations.|  
|HOBT.BULK_OPERATION|Heap-optimized bulk load operations with concurrent scan, under these isolation levels: snapshot, read uncommitted, and read committed using row versioning.|  
|HOBT.INDEX_REORGANIZE|Heap or index reorganization operations.|  
|OBJECT.COMPILE|Stored procedure compile.|  
|OBJECT.INDEX_OPERATION|Index operations.|  
|OBJECT.UPDSTATS|Statistics updates on a table.|  
|METADATA.ASSEMBLY|[!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]|  
|METADATA.ASSEMBLY_CLR_NAME|[!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]|  
|METADATA.ASSEMBLY_TOKEN|[!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]|  
|METADATA.ASYMMETRIC_KEY|[!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]|  
|METADATA.AUDIT|[!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]|  
|METADATA.AUDIT_ACTIONS|[!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]|  
|METADATA.AUDIT_SPECIFICATION|[!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]|  
|METADATA.AVAILABILITY_GROUP|[!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]|  
|METADATA.CERTIFICATE|[!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]|  
|METADATA.CHILD_INSTANCE|[!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]|  
|METADATA.COMPRESSED_FRAGMENT|[!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]|  
|METADATA.COMPRESSED_ROWSET|[!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]|  
|METADATA.CONVERSTATION_ENDPOINT_RECV|[!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]|  
|METADATA.CONVERSTATION_ENDPOINT_SEND|[!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]|  
|METADATA.CONVERSATION_GROUP|[!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]|  
|METADATA.CONVERSATION_PRIORITY|[!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]|  
|METADATA.CREDENTIAL|[!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]|  
|METADATA.CRYPTOGRAPHIC_PROVIDER|[!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]|  
|METADATA.DATA_SPACE|[!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]|  
|METADATA.DATABASE|[!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]|  
|METADATA.DATABASE_PRINCIPAL|[!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]|  
|METADATA.DB_MIRRORING_SESSION|[!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]|  
|METADATA.DB_MIRRORING_WITNESS|[!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]|  
|METADATA.DB_PRINCIPAL_SID|[!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]|  
|METADATA.ENDPOINT|[!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]|  
|METADATA.ENDPOINT_WEBMETHOD|[!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]|  
|METADATA.EXPR_COLUMN|[!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]|  
|METADATA.EXPR_HASH|[!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]|  
|METADATA.FULLTEXT_CATALOG|[!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]|  
|METADATA.FULLTEXT_INDEX|[!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]|  
|METADATA.FULLTEXT_STOPLIST|[!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]|  
|METADATA.INDEX_EXTENSION_SCHEME|[!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]|  
|METADATA.INDEXSTATS|[!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]|  
|METADATA.INSTANTIATED_TYPE_HASH|[!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]|  
|METADATA.MESSAGE|[!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]|  
|METADATA.METADATA_CACHE|[!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]|  
|METADATA.PARTITION_FUNCTION|[!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]|  
|METADATA.PASSWORD_POLICY|[!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]|  
|METADATA.PERMISSIONS|[!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]|  
|METADATA.PLAN_GUIDE|[!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]|  
|METADATA.PLAN_GUIDE_HASH|[!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]|  
|METADATA.PLAN_GUIDE_SCOPE|[!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]|  
|METADATA.QNAME|[!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]|  
|METADATA.QNAME_HASH|[!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]|  
|METADATA.REMOTE_SERVICE_BINDING|[!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]|  
|METADATA.ROUTE|[!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]|  
|METADATA.SCHEMA|[!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]|  
|METADATA.SECURITY_CACHE|[!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]|  
|METADATA.SECURITY_DESCRIPTOR|[!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]|  
|METADATA.SEQUENCE|[!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]|  
|METADATA.SERVER_EVENT_SESSIONS|[!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]|  
|METADATA.SERVER_PRINCIPAL|[!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]|  
|METADATA.SERVICE|[!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]|  
|METADATA.SERVICE_BROKER_GUID|[!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]|  
|METADATA.SERVICE_CONTRACT|[!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]|  
|METADATA.SERVICE_MESSAGE_TYPE|[!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]|  
|METADATA.STATS|[!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]|  
|METADATA.SYMMETRIC_KEY|[!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]|  
|METADATA.USER_TYPE|[!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]|  
|METADATA.XML_COLLECTION|[!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]|  
|METADATA.XML_COMPONENT|[!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]|  
|METADATA.XML_INDEX_QNAME|[!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]|  
  
 The following table provides the format of the **resource_description** column for each resource type.  
  
|Resource|Format|Description|  
|--------------|------------|-----------------|  
|DATABASE|Not applicable|Database ID is already available in the **resource_database_id** column.|  
|FILE|<file_id>|ID of the file that is represented by this resource.|  
|OBJECT|<object_id>|ID of the object that is represented by this resource. This object can be any object listed in **sys.objects**, not just a table.|  
|PAGE|<file_id>:<page_in_file>|Represents the file and page ID of the page that is represented by this resource.|  
|KEY|<hash_value>|Represents a hash of the key columns from the row that is represented by this resource.|  
|EXTENT|<file_id>:<page_in_files>|Represents the file and page ID of the extent that is represented by this resource. The extent ID is the same as the page ID of the first page in the extent.|  
|RID|<file_id>:<page_in_file>:<row_on_page>|Represents the page ID and row ID of the row that is represented by this resource. Note that if the associated object ID is 99, this resource represents one of the eight mixed page slots on the first IAM page of an IAM chain.|  
|APPLICATION|\<DbPrincipalId>:\<upto 32 characters>:(<hash_value>)|Represents the ID of the database principal that is used for scoping this application lock resource. Also included are up to 32 characters from the resource string that corresponds to this application lock resource. In certain cases, only 2 characters can be displayed due to the full string no longer being available. This behavior occurs only at database recovery time for application locks that are reacquired as part of the recovery process. The hash value represents a hash of the full resource string that corresponds to this application lock resource.|  
|HOBT|Not applicable|HoBt ID is included as the **resource_associated_entity_id**.|  
|ALLOCATION_UNIT|Not applicable|Allocation Unit ID is included as the **resource_associated_entity_id**.|  
|METADATA.ASSEMBLY|assembly_id = A|[!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]|  
|METADATA.ASSEMBLY_CLR_NAME|$qname_id = Q|[!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]|  
|METADATA.ASSEMBLY_TOKEN|assembly_id = A, $token_id|[!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]|  
|METADATA.ASSYMMETRIC_KEY|asymmetric_key_id = A|[!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]|  
|METADATA.AUDIT|audit_id = A|[!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]|  
|METADATA.AUDIT_ACTIONS|device_id = D, major_id = M|[!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]|  
|METADATA.AUDIT_SPECIFICATION|audit_specification_id = A|[!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]|  
|METADATA.AVAILABILITY_GROUP|availability_group_id = A|[!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]|  
|METADATA.CERTIFICATE|certificate_id = C|[!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]|  
|METADATA.CHILD_INSTANCE|$hash = H1:H2:H3|[!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]|  
|METADATA.COMPRESSED_FRAGMENT|object_id = O , compressed_fragment_id = C|[!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]|  
|METADATA.COMPRESSED_ROW|object_id = O|[!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]|  
|METADATA.CONVERSTATION_ENDPOINT_RECV|$hash = H1:H2:H3|[!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]|  
|METADATA.CONVERSTATION_ENDPOINT_SEND|$hash = H1:H2:H3|[!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]|  
|METADATA.CONVERSATION_GROUP|$hash = H1:H2:H3|[!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]|  
|METADATA.CONVERSATION_PRIORITY|conversation_priority_id = C|[!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]|  
|METADATA.CREDENTIAL|credential_id = C|[!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]|  
|METADATA.CRYPTOGRAPHIC_PROVIDER|provider_id = P|[!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]|  
|METADATA.DATA_SPACE|data_space_id = D|[!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]|  
|METADATA.DATABASE|database_id = D|[!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]|  
|METADATA.DATABASE_PRINCIPAL|principal_id = P|[!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]|  
|METADATA.DB_MIRRORING_SESSION|database_id = D|[!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]|  
|METADATA.DB_MIRRORING_WITNESS|$hash = H1:H2:H3|[!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]|  
|METADATA.DB_PRINCIPAL_SID|$hash = H1:H2:H3|[!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]|  
|METADATA.ENDPOINT|endpoint_id = E|[!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]|  
|METADATA.ENDPOINT_WEBMETHOD|$hash = H1:H2:H3|[!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]|  
|METADATA.FULLTEXT_CATALOG|fulltext_catalog_id = F|[!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]|  
|METADATA.FULLTEXT_INDEX|object_id = O|[!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]|  
|METADATA.EXPR_COLUMN|object_id = O, column_id = C|[!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]|  
|METADATA.EXPR_HASH|object_id = O, $hash = H|[!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]|  
|METADATA.FULLTEXT_CATALOG|fulltext_catalog_id = F|[!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]|  
|METADATA.FULLTEXT_INDEX|object_id = O|[!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]|  
|METADATA.FULLTEXT_STOPLIST|fulltext_stoplist_id = F|[!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]|  
|METADATA.INDEX_EXTENSION_SCHEME|index_extension_id = I|[!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]|  
|METADATA.INDEXSTATS|object_id = O, index_id or stats_id = I|[!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]|  
|METADATA.INSTANTIATED_TYPE_HASH|user_type_id = U, hash = H|[!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]|  
|METADATA.MESSAGE|message_id = M|[!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]|  
|METADATA.METADATA_CACHE|$hash = H1:H2:H3|[!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]|  
|METADATA.PARTITION_FUNCTION|function_id = F|[!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]|  
|METADATA.PASSWORD_POLICY|principal_id = P|[!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]|  
|METADATA.PERMISSIONS|class = C|[!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]|  
|METADATA.PLAN_GUIDE|plan_guide_id = P|[!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]|  
|METADATA. PLAN_GUIDE_HASH|$hash = H1:H2:H3|[!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]|  
|METADATA. PLAN_GUIDE_SCOPE|scope_id = S|[!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]|  
|METADATA.QNAME|$qname_id = Q|[!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]|  
|METADATA.QNAME_HASH|$qname_scope_id = Q, $qname_hash = H|[!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]|  
|METADATA.REMOTE_SERVICE_BINDING|remote_service_binding_id = R|[!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]|  
|METADATA.ROUTE|route_id = R|[!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]|  
|METADATA.SCHEMA|schema_id = S|[!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]|  
|METADATA.SECURITY_CACHE|$hash = H1:H2:H3|[!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]|  
|METADATA.SECURITY_DESCRIPTOR|sd_id = S|[!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]|  
|METADATA.SEQUENCE|$seq_type = S, object_id = O|[!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]|  
|METADATA.SERVER|server_id = S|[!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]|  
|METADATA.SERVER_EVENT_SESSIONS|event_session_id = E|[!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]|  
|METADATA.SERVER_PRINCIPAL|principal_id = P|[!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]|  
|METADATA.SERVICE|service_id = S|[!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]|  
|METADATA.SERVICE_BROKER_GUID|$hash = H1:H2:H3|[!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]|  
|METADATA.SERVICE_CONTRACT|service_contract_id = S|[!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]|  
|METADATA.SERVICE_MESSAGE_TYPE|message_type_id = M|[!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]|  
|METADATA.STATS|object_id = O, stats_id = S|[!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]|  
|METADATA.SYMMETRIC_KEY|symmetric_key_id = S|[!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]|  
|METADATA.USER_TYPE|user_type_id = U|[!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]|  
|METADATA.XML_COLLECTION|xml_collection_id = X|[!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]|  
|METADATA.XML_COMPONENT|xml_component_id = X|[!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]|  
|METADATA.XML_INDEX_QNAME|object_id = O, $qname_id = Q|[!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]|  
  
 The following XEvents are related to partition **SWITCH** and online index rebuild. For information about syntax, see [ALTER TABLE &#40;Transact-SQL&#41;](../../t-sql/statements/alter-table-transact-sql.md) and [ALTER INDEX &#40;Transact-SQL&#41;](../../t-sql/statements/alter-index-transact-sql.md).  
  
-   lock_request_priority_state  
  
-   process_killed_by_abort_blockers  
  
-   ddl_with_wait_at_low_priority  
  
 The existing XEvent **progress_report_online_index_operation** for online index operations was extended by adding **partition_number** and **partition_id**.  
  
## Examples  
  
### A. Using sys.dm_tran_locks with other tools  
 The following example works with a scenario in which an update operation is blocked by another transaction. By using **sys.dm_tran_locks** and other tools, information about locking resources is provided.  
  
```sql  
USE tempdb;  
GO  
  
-- Create test table and index.  
CREATE TABLE t_lock  
    (  
    c1 int, c2 int  
    );  
GO  
  
CREATE INDEX t_lock_ci on t_lock(c1);  
GO  
  
-- Insert values into test table  
INSERT INTO t_lock VALUES (1, 1);  
INSERT INTO t_lock VALUES (2,2);  
INSERT INTO t_lock VALUES (3,3);  
INSERT INTO t_lock VALUES (4,4);  
INSERT INTO t_lock VALUES (5,5);  
INSERT INTO t_lock VALUES (6,6);  
GO  
  
-- Session 1  
SET TRANSACTION ISOLATION LEVEL READ COMMITTED;  
  
BEGIN TRAN  
    SELECT c1  
        FROM t_lock  
        WITH(holdlock, rowlock);  
  
-- Session 2  
BEGIN TRAN  
    UPDATE t_lock SET c1 = 10  
```  
  
 The following query will display lock information. The value for `<dbid>` should be replaced with the **database_id** from **sys.databases**.  
  
```sql  
SELECT resource_type, resource_associated_entity_id,  
    request_status, request_mode,request_session_id,  
    resource_description   
    FROM sys.dm_tran_locks  
    WHERE resource_database_id = <dbid>  
```  
  
 The following query returns object information by using `resource_associated_entity_id` from the previous query. This query must be executed while you are connected to the database that contains the object.  
  
```  
SELECT object_name(object_id), *  
    FROM sys.partitions  
    WHERE hobt_id=<resource_associated_entity_id>  
```  
  
 The following query will show blocking information.  
  
```sql  
SELECT   
        t1.resource_type,  
        t1.resource_database_id,  
        t1.resource_associated_entity_id,  
        t1.request_mode,  
        t1.request_session_id,  
        t2.blocking_session_id  
    FROM sys.dm_tran_locks as t1  
    INNER JOIN sys.dm_os_waiting_tasks as t2  
        ON t1.lock_owner_address = t2.resource_address;  
```  
  
 Release the resources by rolling back the transactions.  
  
```sql  
-- Session 1  
ROLLBACK;  
GO  
  
-- Session 2  
ROLLBACK;  
GO  
```  
  
### B. Linking session information to operating system threads  
 The following example returns information that associates a session ID with a Windows thread ID. The performance of the thread can be monitored in the Windows Performance Monitor. This query does not return session IDs that are currently sleeping.  
  
```sql  
SELECT STasks.session_id, SThreads.os_thread_id  
    FROM sys.dm_os_tasks AS STasks  
    INNER JOIN sys.dm_os_threads AS SThreads  
        ON STasks.worker_address = SThreads.worker_address  
    WHERE STasks.session_id IS NOT NULL  
    ORDER BY STasks.session_id;  
GO  
```  
  
## See Also  
[sys.dm_tran_database_transactions &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-tran-database-transactions-transact-sql.md)      
[Dynamic Management Views and Functions &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/system-dynamic-management-views.md)     
[Transaction Related Dynamic Management Views and Functions &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/transaction-related-dynamic-management-views-and-functions-transact-sql.md)      
[SQL Server, Locks Object](../../relational-databases/performance-monitor/sql-server-locks-object.md)
