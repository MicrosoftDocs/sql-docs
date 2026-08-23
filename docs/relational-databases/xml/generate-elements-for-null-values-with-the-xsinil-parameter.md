---
title: "Generate elements for NULL values with XSINIL"
description: Learn how to generate XML elements for NULL values by using the XSINIL parameter on the ELEMENTS directive.
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: randolphwest
ms.date: 05/05/2022
ms.service: sql
ms.subservice: xml
ms.topic: concept-article
ms.custom:
  - ignite-2025
helpviewer_keywords:
  - "FOR XML clause, null values"
  - "null values [SQL Server], XML"
  - "ELEMENTS directive"
  - "XSINIL parameter"
---
# Generate elements for NULL values with the XSINIL parameter

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance FabricSQLDB](../../includes/applies-to-version/sql-asdb-asdbmi-fabricsqldb.md)]

The **ELEMENTS** directive constructs XML in which each column value maps to an element in the XML. By default, if the column value is NULL, no element is added. But by specifying the optional **XSINIL** parameter on the ELEMENTS directive, you can request that an element is created for the NULL value. In this case, an element that has the **xsi:nil** attribute set to TRUE is returned for each NULL column value.

## Related content

- [Use RAW mode with FOR XML](use-raw-mode-with-for-xml.md)
- [SELECT - FOR clause (Transact-SQL)](../../t-sql/queries/select-for-clause-transact-sql.md)
