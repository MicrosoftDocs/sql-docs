---
title: "Handling Large Object (LOB) Parameters in the CLR"
description: This article describes how to handle large object (LOB) values for parameters in SQL Server CLR integration. Use SqlBytes and SqlChars for LOB types.
author: uc-msft
ms.author: umajay
ms.date: 12/27/2024
ms.service: sql
ms.subservice: clr
ms.topic: "reference"
helpviewer_keywords:
  - "large data, CLR integration"
  - "LOB data [SQL Server], CLR integration"
  - "SqlBytes data type"
  - "SqlChars data type"
---
# Handle large object (LOB) parameters in the CLR

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Use `SqlBytes` and `SqlChars` to pass large object (LOB) binary type (**varbinary(max)**) and LOB character type (**nvarchar(max)**) parameters, respectively. These types allow streaming the LOB values from the database to the common language runtime (CLR) routine, instead of copying the entire value into managed space. `SqlBinary` and `SqlString` should be used only for small binary and character string values.

## Related content

- [SQL Server data types in the .NET Framework](sql-server-data-types-in-the-net-framework.md)
