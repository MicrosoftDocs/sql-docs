---
title: " ADR cleaner retry timeout (min) Configuration Option | Microsoft Docs"
ms.custom: ""
ms.date: "05/19/2020"
ms.prod: sql
ms.reviewer: ""
ms.technology: configuration
ms.topic: conceptual
helpviewer_keywords: 
  - "ADR cleaner retry timeout (min)"
author: MikeRayMSFT
ms.author: mikeray
---
# ADR cleaner retry timeout (min) Configuration Option

Introduced in SQL Server 2019.

This configuration setting is required for [accelerated database recovery](../../relational-databases/accelerated-database-recovery-concepts.md). The cleaner is the asynchronous process that wakes up periodically and cleans page versions that are not needed.

## Remarks  

## Examples  

The following example enables ad hoc distributed queries and then queries a server named `Seattle1` using the `OPENROWSET` function.  

```  
sp_configure 'show advanced options', 1;  
RECONFIGURE;
GO 
sp_configure 'ADR cleaner retry timeout', 1;  
RECONFIGURE;  
GO  
```  

## See Also  

- [Server Configuration Options &#40;SQL Server&#41;](../../database-engine/configure-windows/server-configuration-options-sql-server.md)
- [Accelerated database recovery](../../relational-databases/accelerated-database-recovery-concepts.md)
- [Manage accelerated database recovery](../../relational-databases/accelerated-database-recovery-management.md)