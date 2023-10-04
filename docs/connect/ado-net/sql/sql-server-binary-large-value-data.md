---
title: "SQL Server binary and large-value data"
description: "Describes how to work with large value data in SQL Server."
author: David-Engel
ms.author: v-davidengel
ms.reviewer: v-kaywon
ms.date: "08/15/2019"
ms.service: sql
ms.subservice: connectivity
ms.topic: conceptual
---
# SQL Server binary and large-value data

[!INCLUDE[Driver_ADONET_Download](../../../includes/driver_adonet_download.md)]

SQL Server provides the `max` specifier, which expands the storage capacity of the `varchar`, `nvarchar`, and `varbinary` data types. `varchar(max)`, `nvarchar(max)`, and `varbinary(max)` are collectively called *large-value data types*. You can use the large-value data types to store up to 2^31-1 bytes of data.  
  
SQL Server 2008 introduces the FILESTREAM attribute, which is not a data type, but rather an attribute that can be defined on a column, allowing large-value data to be stored on the file system instead of in the database.  
  
## In this section  
[Modifying large-value (max) data in ADO.NET](modify-large-value-max-data.md)  
Describes how to work with the large-value data types.  
  
[FILESTREAM data](filestream-data.md)  
Describes how to work with large-value data stored in SQL Server 2008 with the FILESTREAM attribute.  
  
## Next steps
- [SQL Server data types and ADO.NET](sql-server-data-types.md)
- [SQL Server data operations in ADO.NET](sql-server-data-operations.md)
