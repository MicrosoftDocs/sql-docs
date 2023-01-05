---
title: "Example: Specifying the CDATA Directive"
description: View an example of how to specify the CDATA directive to wrap the specified data in a CDATA section.
ms.custom: ""
ms.date: 05/05/2022
ms.service: sql
ms.reviewer: randolphwest
ms.subservice: xml
ms.topic: conceptual
helpviewer_keywords:
  - "CDATA directive"
author: MikeRayMSFT
ms.author: mikeray
---
# Example: Specify the CDATA directive

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

If the directive is set to **CDATA**, the contained data isn't entity encoded, but is put in the CDATA section. The **CDATA** attributes must be nameless.

The following query wraps the product model summary description in a CDATA section.

```sql
USE AdventureWorks2012;
GO
SELECT  1 as Tag,
        0 as Parent,
        ProductModelID  as [ProductModel!1!ProdModelID],
        Name            as [ProductModel!1!Name],
        '<Summary>This is summary description</Summary>'
            as [ProductModel!1!!CDATA] -- no attribute name so ELEMENT assumed
FROM    Production.ProductModel
WHERE   ProductModelID = 19
FOR XML EXPLICIT;
```

This is the result:

```xml
<ProductModel ProdModelID="19" Name="Mountain-100">
   <![CDATA[<Summary>This is summary description</Summary>]]>
</ProductModel>
```

## See also

- [Use EXPLICIT Mode with FOR XML](../../relational-databases/xml/use-explicit-mode-with-for-xml.md)
