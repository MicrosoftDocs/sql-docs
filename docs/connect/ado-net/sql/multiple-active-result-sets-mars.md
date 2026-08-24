---
title: "Multiple Active Result Sets (MARS)"
description: "Describes how to have more than one SqlDataReader open on a connection when each instance of SqlDataReader is started from a separate command."
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: davidengel, paulmedynski, cmalhotra
ms.date: "08/15/2019"
ms.service: sql
ms.subservice: connectivity
ms.topic: how-to
---
# Multiple Active Result Sets (MARS)

[!INCLUDE[Driver_ADONET_Download](../../../includes/driver_adonet_download.md)]

Multiple Active Result Sets (MARS) is a feature that allows the execution of multiple batches on a single connection. In previous versions, only one batch could be executed at a time against a single connection. Executing multiple batches with MARS does not imply simultaneous execution of operations.  
  
## In this section  
[Enabling Multiple Active Result Sets](enable-multiple-active-result-sets.md)  
Discusses how to use MARS with SQL Server.  
  
[Manipulating data](manipulate-data.md)  
Provides examples of coding MARS applications.  
  
## Related content

- [Asynchronous operations](asynchronous-operations.md)
- [SQL Server and ADO.NET](index.md)
