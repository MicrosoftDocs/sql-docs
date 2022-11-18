---
title: "Generate elements for NULL values with XSINIL"
description: Learn how to generate XML elements for NULL values by using the XSINIL parameter on the ELEMENTS directive.
ms.date: 05/05/2022
ms.service: sql
ms.reviewer: randolphwest
ms.subservice: xml
ms.topic: conceptual
helpviewer_keywords:
  - "FOR XML clause, null values"
  - "null values [SQL Server], XML"
  - "ELEMENTS directive"
  - "XSINIL parameter"
author: MikeRayMSFT
ms.author: mikeray
ms.custom: "seo-lt-2019"
---
# Generate elements for NULL values with the XSINIL parameter

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

The **ELEMENTS** directive constructs XML in which each column value maps to an element in the XML. By default, if the column value is NULL, no element is added. But by specifying the optional **XSINIL** parameter on the ELEMENTS directive, you can request that an element is created for the NULL value. In this case, an element that has the **xsi:nil** attribute set to TRUE is returned for each NULL column value.

## See also

- [Use RAW Mode with FOR XML](../../relational-databases/xml/use-raw-mode-with-for-xml.md)
- [SELECT - FOR clause](../../t-sql/queries/select-for-clause-transact-sql.md)
