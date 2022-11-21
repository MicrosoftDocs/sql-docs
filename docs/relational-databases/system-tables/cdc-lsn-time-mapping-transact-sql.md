---
title: "cdc.lsn_time_mapping (Transact-SQL)"
description: cdc.lsn_time_mapping (Transact-SQL)
author: VanMSFT
ms.author: vanto
ms.date: "06/10/2016"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "cdc.lsn_time_mapping"
  - "cdc.lsn_time_mapping_TSQL"
helpviewer_keywords:
  - "cdc.lsn_time_mapping"
dev_langs:
  - "TSQL"
ms.assetid: 1cb7aedc-48a4-486e-9b91-d30c4bd4084e
---
# cdc.lsn_time_mapping (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Returns one row for each transaction having rows in a change table. This table is used to map between log sequence number (LSN) commit values and the time the transaction committed. Entries may also be logged for which there are no change tables entries. This allows the table to record the completion of LSN processing in periods of low or no change activity.  
  
 We recommend that you do not query the system tables directly. Instead, execute the [sys.fn_cdc_map_lsn_to_time &#40;Transact-SQL&#41;](../../relational-databases/system-functions/sys-fn-cdc-map-lsn-to-time-transact-sql.md) and [sys.fn_cdc_map_time_to_lsn &#40;Transact-SQL&#41;](../../relational-databases/system-functions/sys-fn-cdc-map-time-to-lsn-transact-sql.md) system functions.  
    
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**start_lsn**|**binary(10)**|LSN of the committed transaction.|  
|**tran_begin_time**|**datetime**|Time that the transaction associated with the LSN began.|  
|**tran_end_time**|**datetime**|Time that the transaction ended.|  
|**tran_id**|**varbinary(10)**|ID of the transaction.|  
  
## See Also  
 [The Transaction Log &#40;SQL Server&#41;](../../relational-databases/logs/the-transaction-log-sql-server.md)   
 [cdc.&#60;capture_instance&#62;_CT &#40;Transact-SQL&#41;](../../relational-databases/system-tables/cdc-capture-instance-ct-transact-sql.md)  
  
  
