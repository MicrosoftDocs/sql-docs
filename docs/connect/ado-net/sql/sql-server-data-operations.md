---
title: "SQL Server data operations in ADO.NET"
description: "Describes how to work with data in SQL Server. Contains sections about bulk copy operations, MARS, asynchronous operations, and table-valued parameters."
author: David-Engel
ms.author: v-davidengel
ms.reviewer: v-kaywon
ms.date: "08/15/2019"
ms.service: sql
ms.subservice: connectivity
ms.topic: conceptual
---
# SQL Server data operations in ADO.NET

[!INCLUDE[Driver_ADONET_Download](../../../includes/driver_adonet_download.md)]

This section describes SQL Server features and functionality that are specific to the Microsoft SqlClient Data Provider for SQL Server (<xref:Microsoft.Data.SqlClient>).  
  
## In this section  
[Bulk copy operations in SQL Server](bulk-copy-operations-sql-server.md)  
Describes the bulk copy functionality for the .NET Data Provider for SQL Server.  
  
[Multiple Active Result Sets (MARS)](multiple-active-result-sets-mars.md)  
Describes how to have more than one <xref:Microsoft.Data.SqlClient.SqlDataReader> open on a connection when each instance of <xref:Microsoft.Data.SqlClient.SqlDataReader> is started from a separate command.  
  
[Asynchronous operations](asynchronous-operations.md)  
Describes how to perform asynchronous database operations by using an API that is modeled after the asynchronous model used by the .NET Framework.  
  
[Table-valued parameters](table-valued-parameters.md)  
Describes how to work with table-valued parameters, which were introduced in SQL Server 2008.  
  
## Next steps
- [SQL Server and ADO.NET](index.md)
