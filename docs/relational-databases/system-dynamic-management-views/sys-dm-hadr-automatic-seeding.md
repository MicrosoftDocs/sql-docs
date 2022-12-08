---
title: "sys.dm_hadr_automatic_seeding (Transact-SQL)"
description: sys.dm_hadr_automatic_seeding (Transact-SQL)
author: rwestMSFT
ms.author: randolphwest
ms.date: "04/20/2022"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "dm_hadr_automatic_seeding_TSQL"
  - "sys.dm_hadr_automatic_seeding"
  - "sys.dm_hadr_automatic_seeding_TSQL"
  - "dm_hadr_automatic_seeding"
helpviewer_keywords:
  - "Availability Groups [SQL Server], monitoring"
  - "automatic seeding"
  - "sys.dm_hadr_automatic_seeding dynamic management view"
dev_langs:
  - "TSQL"
ms.assetid: d7840adf-4a1b-41ac-bc94-102c07ad1c79
---
# sys.dm_hadr_automatic_seeding (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Query sys.dm_hadr_automatic_seeding on the primary replica to check the status of the automatic seeding process for an [availability group](../../database-engine/availability-groups/windows/always-on-availability-groups-sql-server.md). The view returns one row for each seeding process.
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**start_time**|**datetime**|The time the operation was initiated.|
|**completion_time**|**datetime**|The time the operation completed (NULL if ongoing).|  
|**ag_id**|**uniqueidentifier**|Unique ID for each availability group.|  
|**ag_db_id**|**uniqueidentifier**|Unique ID for each database in the Available Group.|  
|**ag_remote_replica_id**|**uniqueidentifier**|Unique ID for the other replica this seeding operation involves.|
|**operation_id**|**uniqueidentifier**|Unique identifier for this seeding operation.|  
|**is_source**|**bit**| Indicates whether this replica is the source (primary) of the seeding operation.|
|**current_state**|**bit**| The current seeding state the operation is in.|
|**performed_seeding**|**bit**|Database streaming for seeding is initialized.|
|**failure_state**|**int**|The reason the operation failed, expressed as an integer.<BR/><BR/>If **failure_state** is a three digit integer, a value of 1 in the hundred place digit indicates the error occurred on the seeding source. A value of 2 in the hundred place digit indicates an error occurred on the seeding target.<BR/><BR/>Values for **failure_state** include the following. You can also use the **failure_state_desc** column to interpret these values.<BR /><BR />0 = Internal Error<BR/><BR/>1 = User Cancellation<BR/><BR/>2 = SQL Error<BR/><BR/> 3 = Request Denied<BR/><BR/>4 = Thread Abort<BR/><BR/>5 = Primary Failure<BR/><BR/>6 = Transport<BR/><BR/>7 = Transport Replica<BR/><BR/>8 = Check If Seeding Needed<BR/><BR/>9 = Send Database File Info<BR/><BR/>10 = Create Callback<BR/><BR/>11 = Create Operation<BR/><BR/>12 = Create VDI Client<BR/><BR/>13 = Open VDI Client<BR/><BR/>14 = Create USC Session<BR/><BR/>15 = Seeding<BR/><BR/>16 = Restore String Creation<BR/><BR/>17 = Database ID Lookup<BR/><BR/>18 = Create Async Task<BR/><BR/>19 = Create Timeout Task<BR/><BR/>20 = Async Task Failure<BR/><BR/>21 = Seeding Check Message Timeout<BR/><BR/>22 = File Message Timeout<BR/><BR/>23 = Database With Name Already Exists<BR/><BR/>24 = Secondary Catchup Timeout<BR/><BR/>25 = Secondary Restore Stream Ready Timeout |
|**failure_state_desc**|**ncharvar**|Description of why the operation failed. Possible values include: <BR/><BR/>Internal Error<BR/><BR/> User Cancellation<BR/><BR/> SQL Error<BR/><BR/> Request Denied<BR/><BR/> Thread Abort<BR/><BR/> Primary Failure<BR/><BR/> Transport<BR/><BR/> Transport Replica<BR/><BR/> Check If Seeding Needed<BR/><BR/> Send Database File Info<BR/><BR/> Create Callback<BR/><BR/> Create Operation<BR/><BR/> Create VDI Client<BR/><BR/> Open VDI Client<BR/><BR/> Create USC Session<BR/><BR/> Seeding<BR/><BR/> Restore String Creation<BR/><BR/> Database ID Lookup<BR/><BR/> Create Async Task<BR/><BR/> Create Timeout Task<BR/><BR/> Async Task Failure<BR/><BR/> Seeding Check Message Timeout<BR/><BR/> File Message Timeout<BR/><BR/> Database With Name Already Exists<BR/><BR/> Secondary Catchup Timeout<BR/><BR/> Secondary Restore Stream Ready Timeout |
|**error_code**|**int**| Any SQL error code encountered during seeding.|
|**number_of_attempts**|**int**| The number of times this seeding operation has been attempted.|

## Permissions  

Requires VIEW SERVER STATE permission on the server.  
  
## Next steps

Learn more about related concepts in the following articles:

- [sys.dm_hadr_availability_group_states (Transact-SQL)](sys-dm-hadr-availability-group-states-transact-sql.md)
- [Monitor Availability Groups (Transact-SQL)](../../database-engine/availability-groups/windows/monitor-availability-groups-transact-sql.md)
- [sys.dm_hadr_cluster (Transact-SQL)](sys-dm-hadr-cluster-transact-sql.md)
- [View Availability Group Properties (SQL Server)](../../database-engine/availability-groups/windows/view-availability-group-properties-sql-server.md)
