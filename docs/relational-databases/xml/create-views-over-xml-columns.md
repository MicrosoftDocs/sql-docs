---
title: "Create views over XML columns"
description: Learn how to create a view in which the value from an xml type column is retrieved using the value() method of the xml data type.
ms.date: 05/05/2022
ms.service: sql
ms.reviewer: randolphwest
ms.subservice: xml
ms.topic: conceptual
helpviewer_keywords:
  - "views [XML in SQL Server]"
author: MikeRayMSFT
ms.author: mikeray
---
# Create views over XML columns

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

You can use an **xml** type column to create views. The following example creates a view in which the value from an `xml` type column is retrieved using the `value()` method of the **xml** data type.

```sql
-- Create the table.
CREATE TABLE T (
    ProductID INT PRIMARY KEY,
    CatalogDescription XML);
GO
-- Insert sample data.
INSERT INTO T VALUES(1,'<ProductDescription ProductID="1" ProductName="SomeName" />');
GO
-- Create view (note the value() method used to retrieve ProductName
-- attribute value from the XML).
CREATE VIEW MyView AS
  SELECT ProductID,
         CatalogDescription.value('(/ProductDescription/@ProductName)[1]', 'varchar(40)') AS PName
  FROM T;
GO
```

Execute the following query against the view:

```sql
SELECT *
FROM   MyView;
```

This is the result:

```output
ProductID   PName
----------- ------------
1           SomeName
```

Note the following points about using the **xml** data type to create views:

- The **xml** data type can be created in a materialized view. The materialized view can't be based on an **xml** data type method. However, it can be cast to an XML schema collection that is different from the xml type column in the base table.

- The **xml** data type can't be used in Distributed Partitioned Views.

- SQL predicates running against the view won't be pushed into the XQuery of the view definition.

- **xml** data type methods in a view aren't updatable.
