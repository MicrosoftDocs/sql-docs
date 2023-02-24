---
title: "Change existing columns to XML columns"
description: Learn how to use the ALTER TABLE statement to change a string type column to an xml data type column.
ms.custom: "fresh2019may"
ms.date: 05/05/2022
ms.service: sql
ms.reviewer: randolphwest
ms.subservice: xml
ms.topic: conceptual
helpviewer_keywords:
  - "tables [XML]"
author: MikeRayMSFT
ms.author: mikeray
---
# Change existing columns to XML columns

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

The ALTER TABLE statement supports the **xml** data type. For example, you can alter any string type column to the **xml** data type. In these cases, the documents contained in the column must be well formed. Also, if you're changing the type of the column from string to typed xml, the documents in the column are validated against the specified XSD schemas.

```sql
CREATE TABLE T (Col1 int primary key, Col2 nvarchar(max));
GO
INSERT INTO T
  VALUES (1, '<Root><Product ProductID="1"/></Root>');
GO
ALTER TABLE T
  ALTER COLUMN Col2 xml;
```

You can change an **xml** type column from untyped XML to typed XML. For example:

```sql
CREATE TABLE T (Col1 int primary key, Col2 xml);
GO
INSERT INTO T
  values (1, '<p1:ProductDescription ProductModelID="1"
xmlns:p1="https://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ProductModelDescription">
            </p1:ProductDescription>');
GO
-- Make it a typed xml column by specifying a schema collection.
ALTER TABLE T
  ALTER COLUMN Col2 xml (Production.ProductDescriptionSchemaCollection);
```

> [!NOTE]
> The script will run against [!INCLUDE[ssSampleDBobject](../../includes/sssampledbobject-md.md)] database, because the XML schema collection, `Production.ProductDescriptionSchemaCollection`, is created as part of the [!INCLUDE[ssSampleDBobject](../../includes/sssampledbobject-md.md)] database.

In the previous example, all the instances stored in the column are validated and typed against the XSD schemas in the specified collection. If the column contains one or more XML instances that are invalid regarding the specified schema, the `ALTER TABLE` statement will fail and you won't be able to change your untyped XML column into typed XML.

> [!NOTE]
> If a table is large, modifying an **xml** type column can be costly. This is because each document must be checked for being well formed and, for typed XML, must also be validated.

## See also

- For more information about typed XML, see [Compare Typed XML to Untyped XML](../../relational-databases/xml/compare-typed-xml-to-untyped-xml.md).
