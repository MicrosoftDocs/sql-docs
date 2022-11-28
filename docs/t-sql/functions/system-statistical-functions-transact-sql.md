---
title: "System Statistical Functions (Transact-SQL)"
description: "System Statistical Functions (Transact-SQL)"
author: MikeRayMSFT
ms.author: mikeray
ms.reviewer: ""
ms.date: "03/15/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom: ""
helpviewer_keywords:
  - "statistical functions [SQL Server]"
  - "system statistical functions [SQL Server]"
  - "functions [SQL Server], statistical"
dev_langs:
  - "TSQL"
---
# System Statistical Functions (Transact-SQL)
[!INCLUDE [SQL Server Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdbmi.md)]

  The following scalar functions return statistical information about the system:  

:::row:::
    :::column:::
        [@@CONNECTIONS](../../t-sql/functions/connections-transact-sql.md)
    :::column-end:::
    :::column:::
        [@@PACK_RECEIVED](../../t-sql/functions/pack-received-transact-sql.md)
    :::column-end:::
:::row-end:::  
:::row:::
    :::column:::
        [@@CPU_BUSY](../../t-sql/functions/cpu-busy-transact-sql.md)
    :::column-end:::
    :::column:::
        [@@PACK_SENT](../../t-sql/functions/pack-sent-transact-sql.md)
    :::column-end:::
:::row-end:::  
:::row:::
    :::column:::
        [fn_virtualfilestats](../../relational-databases/system-functions/sys-fn-virtualfilestats-transact-sql.md)
    :::column-end:::
    :::column:::
        [@@TIMETICKS](../../t-sql/functions/timeticks-transact-sql.md)
    :::column-end:::
:::row-end:::  
:::row:::
    :::column:::
        [@@IDLE](../../t-sql/functions/idle-transact-sql.md)
    :::column-end:::
    :::column:::
        [@@TOTAL_ERRORS](../../t-sql/functions/total-errors-transact-sql.md)
    :::column-end:::
:::row-end:::  
:::row:::
    :::column:::
        [@@IO_BUSY](../../t-sql/functions/io-busy-transact-sql.md)
    :::column-end:::
    :::column:::
        [@@TOTAL_READ](../../t-sql/functions/total-read-transact-sql.md)
    :::column-end:::
:::row-end:::  
:::row:::
    :::column:::
        [@@PACKET_ERRORS](../../t-sql/functions/packet-errors-transact-sql.md)
    :::column-end:::
    :::column:::
        [@@TOTAL_WRITE](../../t-sql/functions/total-write-transact-sql.md)
    :::column-end:::
:::row-end:::
  
  
 All system statistical functions are nondeterministic. This means these functions do not always return the same results every time they are called, even with the same set of input values. For more information about function determinism, see [Deterministic and Nondeterministic Functions](../../relational-databases/user-defined-functions/deterministic-and-nondeterministic-functions.md).  
  
## See Also  
 [Built-in Functions &#40;Transact-SQL&#41;](~/t-sql/functions/functions.md)  
  
  
