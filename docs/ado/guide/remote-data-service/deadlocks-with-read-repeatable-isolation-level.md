---
title: "Deadlocks with Read Repeatable Isolation Level"
description: "Deadlocks with Read Repeatable Isolation Level"
author: rothja
ms.author: jroth
ms.date: 11/09/2018
ms.service: sql
ms.subservice: ado
ms.topic: conceptual
helpviewer_keywords:
  - "deadlocks in RDS [ADO]"
  - "read repeatable in RDS [ADO]"
---
# Deadlocks with Read Repeatable Isolation Level
If a custom business object uses an isolation level of read repeatable to access a SQL Server, and the business object is called simultaneously by two clients that send a query and update in the same transaction, a deadlock is possible. Remote Data Service is designed to allow one of the processes to time out to release the deadlock, but the update will fail for that client.  
  
 Use the [Cursor Service](../appendixes/microsoft-cursor-service-for-ole-db-ado-service-component.md) **Command Time Out** dynamic property to modify the length of the time-out.  
  
> [!IMPORTANT]
>  Beginning with Windows 8 and Windows Server 2012, RDS server components are no longer included in the Windows operating system (see Windows 8 and [Windows Server 2012 Compatibility Cookbook](https://www.microsoft.com/download/details.aspx?id=27416) for more detail). RDS client components will be removed in a future version of Windows. Avoid using this feature in new development work, and plan to modify applications that currently use this feature. Applications that use RDS should migrate to [WCF Data Service](/dotnet/framework/wcf/).  
  
## See Also  
 [RDS Fundamentals](./rds-fundamentals.md)