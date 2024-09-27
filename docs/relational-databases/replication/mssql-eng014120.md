---
title: "MSSQL_ENG014120"
description: "MSSQL_ENG014120"
author: "MashaMSFT"
ms.author: "mathoma"
ms.date: 09/25/2024
ms.service: sql
ms.subservice: replication
ms.topic: reference
ms.custom:
  - updatefrequency5
helpviewer_keywords:
  - "MSSQL_ENG014120 error"
monikerRange: "=azuresqldb-mi-current||>=sql-server-2016"
---
# MSSQL_ENG014120
[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]
    
## Message Details  
  
|Attribute|Value|  
|-|-|  
|Product Name|SQL Server|  
|Event ID|14120|  
|Event Source|MSSQLSERVER|  
|Component|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)]|  
|Symbolic Name||  
|Message Text|Could not drop the distribution database '%s'. This distributor database is associated with a Publisher.|  
  
## Explanation  
 The distribution database stores metadata and history data for all types of replication and transactions for transactional replication. This error occurs if you attempt to drop a distribution database that is associated with one or more Publishers.  
  
## User Action  
 To drop a distribution database you must first drop the association between the Distributor and the Publisher. For more information, see [sp_dropdistpublisher &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-dropdistpublisher-transact-sql.md).  
  
## Related content

- [Errors and Events Reference &#40;Replication&#41;](../../relational-databases/replication/errors-and-events-reference-replication.md)
- [Configure Distribution](../../relational-databases/replication/configure-distribution.md)
