---
title: "Comments in XQuery"
description: Learn the syntax and delimiters for adding comments to a query in XQuery.
author: uc-msft
ms.author: umajay
ms.date: 12/11/2025
ms.service: sql
ms.subservice: xml
ms.topic: reference
helpviewer_keywords:
  - "comments [XQuery]"
  - "XQuery, comments"
dev_langs:
  - "XML"
---
# Comments in XQuery

[!INCLUDE [SQL Server Azure SQL Database](../includes/applies-to-version/sqlserver.md)]

You can add comments to XQuery. Add comment strings by using the `(:` and `:)` delimiters. For example:

```sql
DECLARE @x AS XML;
SET @x = '';

SELECT @x.query('
(: simple query to construct an element :)
<ProductModel ProductModelID = "10" />
');
```

The following example shows a query against an `Instructions` column of the **xml** type:

```sql
SELECT Instructions.query('
(: declare prefix and namespace binding in the prolog. :)
     declare namespace AWMI = "http://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ProductModelManuInstructions";
  (: Following expression retrieves the <Location> element children of the <root> element. :)
  /AWMI:root/AWMI:Location
') AS Result
FROM Production.ProductModel
WHERE ProductModelID = 7;
```
