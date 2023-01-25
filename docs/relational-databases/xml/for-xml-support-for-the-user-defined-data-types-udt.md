---
title: "FOR XML support for user-defined data types (UDT)"
description: Learn about support for user-defined data types (UDT) when using the FOR XML clause.
ms.date: 05/05/2022
ms.service: sql
ms.reviewer: randolphwest
ms.subservice: xml
ms.topic: conceptual
helpviewer_keywords:
  - "UDTs [SQL Server], XML"
  - "user-defined types [SQL Server], XML"
author: MikeRayMSFT
ms.author: mikeray
ms.custom: "seo-lt-2019"
---
# FOR XML support for the user-defined data types (UDT)

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

FOR XML doesn't support common language runtime (CLR) user-defined data types (UDTs).

To use FOR XML with CLR user-defined data types, make sure that the data type has an XML serialization, and use an explicit cast to XML in the FOR XML select clause.

## See also

- [FOR XML Support for Various SQL Server Data Types](../../relational-databases/xml/for-xml-support-for-various-sql-server-data-types.md)
