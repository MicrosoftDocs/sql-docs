---
description: "MSSQLSERVER_41301"
title: "MSSQLSERVER_41301 | Microsoft Docs"
ms.custom: ""
ms.date: "04/04/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: supportability
ms.topic: "reference"
helpviewer_keywords: 
  - "41301 (Database Engine error)"
ms.assetid: c6127e1e-2846-4ee9-bc42-2d896ea9730e
author: MashaMSFT
ms.author: mathoma
---
# MSSQLSERVER_41301
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  
## Details  
  
| Attribute | Value |  
| :-------- | :---- |  
|Product Name|[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]|  
|Event ID|41301|  
|Event Source|MSSQLSERVER|  
|Component|SQLEngine|  
|Symbolic Name|COMMIT_DEPENDENCY_FAILURE|  
|Message Text|A previous transaction that the current transaction took a dependency on has aborted, and the current transaction can no longer commit.|  
  
## Explanation  
The transaction encountered a dependency failure, and is now doomed.  
  
This error can also be caused by too many dependent transactions. Any write transaction can have a limited number of dependent transactions. For example, this error can occur if too many read transactions try to take a dependency on the update transaction.  
  
## User Action  
Don't do any work on the transaction. Call ROLLBACK TRAN to roll back the transaction. For more information, see [In-Memory OLTP &#40;In-Memory Optimization&#41;](~/relational-databases/in-memory-oltp/in-memory-oltp-in-memory-optimization.md).  
  
## See Also  
[Enable and Disable Always On Availability Groups &#40;SQL Server&#41;](~/database-engine/availability-groups/windows/enable-and-disable-always-on-availability-groups-sql-server.md)  
  
