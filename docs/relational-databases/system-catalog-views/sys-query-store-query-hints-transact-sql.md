---
title: "sys.query_store_query_hints (Transact-SQL)"
description: The sys.query_store_query_hints system catalog view returns Query Store hints.
author: rwestMSFT
ms.author: randolphwest
ms.date: 10/14/2022
ms.service: sql
ms.subservice: system-objects
ms.topic: "language-reference"
ms.custom: event-tier1-build-2022
f1_keywords:
  - "SYS.QUERY_STORE_QUERY_HINTS"
  - "QUERY_STORE_QUERY_HINTS"
  - "SYS.QUERY_STORE_QUERY_HINTS_TSQL"
  - "QUERY_STORE_QUERY_HINTS_TSQL"
helpviewer_keywords:
  - "sys.query_store_query_hints catalog view"
  - "query_store_query_hints catalog view"
dev_langs:
  - "TSQL"
monikerRange: "=azuresqldb-current||=azuresqldb-mi-current||>=sql-server-ver16||>=sql-server-linux-ver16"
---
# sys.query_store_query_hints (Transact-SQL)
[!INCLUDE [sqlserver2022-asdb-asmi](../../includes/applies-to-version/sqlserver2022-asdb-asmi.md)]

 Returns query hints from [Query Store hints](../../relational-databases/performance/query-store-hints.md).
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**query_hint_id**|bigint|Unique identifier of a query hint.|  
|**query_id**|bigint|Unique identifier of a query in the Query Store. Foreign key to [sys.query_store_query](sys-query-store-query-transact-sql.md).query_id.)|
|**query_hint_text**|nvarchar(MAX)|Hint definition in form of  N'OPTION (…) |
|**last_query_hint_failure_reason**|int|Error code returned when if applying hints failed. Will include the message_id of the error message.|
|**last_query_hint_failure_reason_desc**|nvarchar(128)|Will include the error description of the error message.|
|**query_hint_failure_count**|bigint|Number of times that the query hint application has failed since the query hint was created or last modified.  |
|**source**|int|Source of Query Store hint: user source is zero and system-generated is non-zero. |
|**source_desc**|nvarchar(128)|Description of source of Query Store hint.|
|**comment**|nvarchar(max)|Internal use only.|
|**query_hint_scope**| tinyint | Determines the scope at which the hint will be applied, as per the `replica_group_id` column in [sys.query_store_replicas](../system-catalog-views/sys-query-store-replicas.md). |

## Remarks
 Query Store hints are created by [sys.sp_query_store_set_hints (Transact-SQL)](../system-stored-procedures/sys-sp-query-store-set-hints-transact-sql.md) and removed by [sys.sp_query_store_clear_hints](../system-stored-procedures/sys-sp-query-store-clear-hints-transact-sql.md).

## Permissions  
 Requires the **VIEW DATABASE STATE** permission.  

## Examples

### View Query Store hints

The following example returns existing Query Store hints for `query_id` 39:

```sql
SELECT query_hint_id, query_id, query_hint_text, last_query_hint_failure_reason, last_query_hint_failure_reason_desc, query_hint_failure_count, source, source_desc 
FROM sys.query_store_query_hints 
WHERE query_id = 39;
```
  
## Next steps
- [Query Store hints](../../relational-databases/performance/query-store-hints.md)  
- [sys.sp_query_store_set_hints](../system-stored-procedures/sys-sp-query-store-set-hints-transact-sql.md)   
- [sys.sp_query_store_clear_hints](../system-stored-procedures/sys-sp-query-store-clear-hints-transact-sql.md) 
- [sys.query_store_query &#40;Transact-SQL&#41;](sys-query-store-query-transact-sql.md)  
