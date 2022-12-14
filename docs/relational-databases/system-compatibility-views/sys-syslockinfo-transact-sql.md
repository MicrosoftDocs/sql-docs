---
description: "sys.syslockinfo (Transact-SQL)"
title: "sys.syslockinfo (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/15/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords: 
  - "syslockinfo_TSQL"
  - "sys.syslockinfo_TSQL"
  - "sys.syslockinfo"
  - "syslockinfo"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "syslockinfo system table"
  - "sys.syslockinfo compatibility view"
ms.assetid: d8cae434-807a-473e-b94f-f7a0e1b2daf0
author: rwestMSFT
ms.author: randolphwest
---
# sys.syslockinfo (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Contains information about all granted, converting, and waiting lock requests.  
  
> [!IMPORTANT]  
>  [!INCLUDE[ssnoteCompView](../../includes/ssnotecompview-md.md)]  
  
> [!IMPORTANT]  
>  This feature has changed from earlier versions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. For more information, see [Breaking Changes to Database Engine Features in SQL Server 2016](../../database-engine/breaking-changes-to-database-engine-features-in-sql-server-2016.md).  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**rsc_text**|**nchar(32)**|Textual description of a lock resource. Contains a part of the resource name.|  
|**rsc_bin**|**binary(16)**|Binary lock resource. Contains the actual lock resource that is contained in the lock manager. This column is included for tools that know about the lock resource format for generating their own formatted lock resource, and for performing self joins on **syslockinfo**.|  
|**rsc_valblk**|**binary(16)**|Lock value block. Some resource types may include additional data in the lock resource that is not hashed by the lock manager to determine ownership of a particular lock resource. For example, page locks are not owned by a particular object ID. For lock escalation and other purposes. However, the object ID of a page lock may be included in the lock value block.|  
|**rsc_dbid**|**smallint**|Database ID associated with the resource.|  
|**rsc_indid**|**smallint**|Index ID associated with the resource, if appropriate.|  
|**rsc_objid**|**int**|Object ID associated with the resource, if appropriate.|  
|**rsc_type**|**tinyint**|Resource type:<br /><br /> 1 = NULL Resource (not used)<br /><br /> 2 = Database<br /><br /> 3 = File<br /><br /> 4 = Index<br /><br /> 5 = Table<br /><br /> 6 = Page<br /><br /> 7 = Key<br /><br /> 8 = Extent<br /><br /> 9 = RID (Row ID)<br /><br /> 10 = Application|  
|**rsc_flag**|**tinyint**|Internal resource flags.|  
|**req_mode**|**tinyint**|Lock request mode. This column is the lock mode of the requester and represents either the granted mode, or the convert or waiting mode.<br /><br /> 0 = NULL. No access is granted to the resource. Serves as a placeholder.<br /><br /> 1 = Sch-S (Schema stability). Ensures that a schema element, such as a table or index, is not dropped while any session holds a schema stability lock on the schema element.<br /><br /> 2 = Sch-M (Schema modification). Must be held by any session that wants to change the schema of the specified resource. Ensures that no other sessions are referencing the indicated object.<br /><br /> 3 = S (Shared). The holding session is granted shared access to the resource.<br /><br /> 4 = U (Update). Indicates an update lock acquired on resources that may eventually be updated. It is used to prevent a common form of deadlock that occurs when multiple sessions lock resources for potential update in the future.<br /><br /> 5 = X (Exclusive). The holding session is granted exclusive access to the resource.<br /><br /> 6 = IS (Intent Shared). Indicates the intention to place S locks on some subordinate resource in the lock hierarchy.<br /><br /> 7 = IU (Intent Update). Indicates the intention to place U locks on some subordinate resource in the lock hierarchy.<br /><br /> 8 = IX (Intent Exclusive). Indicates the intention to place X locks on some subordinate resource in the lock hierarchy.<br /><br /> 9 = SIU (Shared Intent Update). Indicates shared access to a resource with the intent of acquiring update locks on subordinate resources in the lock hierarchy.<br /><br /> 10 = SIX (Shared Intent Exclusive). Indicates shared access to a resource with the intent of acquiring exclusive locks on subordinate resources in the lock hierarchy.<br /><br /> 11 = UIX (Update Intent Exclusive). Indicates an update lock hold on a resource with the intent of acquiring exclusive locks on subordinate resources in the lock hierarchy.<br /><br /> 12 = BU. Used by bulk operations.<br /><br /> 13 = RangeS_S (Shared Key-Range and Shared Resource lock). Indicates serializable range scan.<br /><br /> 14 = RangeS_U (Shared Key-Range and Update Resource lock). Indicates serializable update scan.<br /><br /> 15 = RangeI_N (Insert Key-Range and Null Resource lock). Used to test ranges before inserting a new key into an index.<br /><br /> 16 = RangeI_S. Key-Range Conversion lock, created by an overlap of RangeI_N and S locks.<br /><br /> 17 = RangeI_U. Key-Range Conversion lock, created by an overlap of RangeI_N and U locks.<br /><br /> 18 = RangeI_X. Key-Range Conversion lock, created by an overlap of RangeI_N and X locks.<br /><br /> 19 = RangeX_S. Key-Range Conversion lock, created by an overlap of RangeI_N and RangeS_S. locks.<br /><br /> 20 = RangeX_U. Key-Range Conversion lock, created by an overlap of RangeI_N and RangeS_U locks.<br /><br /> 21 = RangeX_X (Exclusive Key-Range and Exclusive Resource lock). This is a conversion lock used when updating a key in a range.|  
|**req_status**|**tinyint**|Status of the lock request:<br /><br /> 1 = Granted<br /><br /> 2 = Converting<br /><br /> 3 = Waiting|  
|**req_refcnt**|**smallint**|Lock reference count. Every time a transaction asks for a lock on a particular resource, a reference count is incremented. The lock cannot be released until the reference count equals 0.|  
|**req_cryrefcnt**|**smallint**|Reserved for future used. Always set to 0.|  
|**req_lifetime**|**int**|Lock lifetime bitmap. During certain query processing strategies, locks must be maintained on resources until the query processor has completed a particular phase of the query. The lock lifetime bitmap is used by the query processor and transaction manager to indicate groups of locks that can be released when a certain phase of a query has finished running. Certain bits in the bitmap are used to indicate locks that are held until the end of a transaction, even if their reference count equals 0.|  
|**req_spid**|**int**|Internal [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] process ID of the session requesting the lock.|  
|**req_ecid**|**int**|Execution context ID (ECID). Used to indicate which thread in a parallel operation owns a particular lock.|  
|**req_ownertype**|**smallint**|Type of object associated with the lock:<br /><br /> 1 = Transaction<br /><br /> 2 = Cursor<br /><br /> 3 = Session<br /><br /> 4 = ExSession<br /><br /> Note that 3 and 4 represent a special version of session locks, tracking database and file group locks, respectively.|  
|**req_transactionID**|**bigint**|Unique transaction ID used in **syslockinfo** and in profiler event|  
|**req_transactionUOW**|**uniqueidentifier**|Identifies the Unit of Work ID (UOW) of the DTC transaction. For non-MS DTC transactions, UOW is set to 0.|  
  
## Permissions  
 Requires VIEW SERVER STATE permission on the server.  
  
## See Also  
 [Mapping System Tables to System Views &#40;Transact-SQL&#41;](../../relational-databases/system-tables/mapping-system-tables-to-system-views-transact-sql.md)   
 [Compatibility Views &#40;Transact-SQL&#41;](~/relational-databases/system-compatibility-views/system-compatibility-views-transact-sql.md)  
  
  
