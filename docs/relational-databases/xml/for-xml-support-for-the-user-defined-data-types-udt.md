---
title: "FOR XML support for user-defined data types (UDT)"
description: Learn about support for user-defined data types (UDT) when using the FOR XML clause.
author: MikeRayMSFT
ms.author: mikeray
ms.reviewer: randolphwest
ms.date: 05/24/2024
ms.service: sql
ms.subservice: xml
ms.topic: conceptual
helpviewer_keywords:
  - "UDTs [SQL Server], XML"
  - "user-defined types [SQL Server], XML"
---
# FOR XML support for the user-defined data types (UDT)

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

`FOR XML` doesn't support common language runtime (CLR) user-defined data types (UDTs).

To use `FOR XML` with CLR user-defined data types, make sure that the data type has an XML serialization, and use an explicit cast to XML in the `FOR XML` select clause.

## Related content

- [FOR XML support for various SQL Server data types](for-xml-support-for-various-sql-server-data-types.md)
