---
title: "Use the value() and nodes() Methods with OPENXML"
description: Learn how to extract a rowset of XML values in an SQL query using the value() and nodes() methods or the OpenXML() method.
ms.custom: ""
ms.date: 05/05/2022
ms.service: sql
ms.reviewer: randolphwest
ms.subservice: xml
ms.topic: conceptual
helpviewer_keywords:
  - "OpenXML method [XML in SQL Server]"
  - "value method [XML in SQL Server]"
  - "nodes method [XML in SQL Server]"
author: MikeRayMSFT
ms.author: mikeray
---
# Use the value() and nodes() methods with OPENXML

[!INCLUDE [SQL Server Azure SQL Database](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

You can use multiple `value()` methods on **xml** data type in a **SELECT** clause to generate a rowset of extracted values. The `nodes()` method yields an internal reference for each selected node that can be used for additional query. The combination of the `nodes()` and `value()` methods can be more efficient in generating the rowset when it has several columns and, perhaps, when the path expressions used in its generation are complex.

The `nodes()` method yields instances of a special **xml** data type, each of which has its context set to a different selected node. This kind of XML instance supports `query()`, `value()`, `nodes()`, and `exist()` methods and can be used in `count(*)` aggregations. All other uses cause an error.

## Example: Use nodes()

Assume that you want to extract the first and last names of authors, and the first name isn't "David". Additionally, you want to extract this information as a rowset that contains two columns, FirstName and LastName. By using `nodes()` and `value()` methods, you can accomplish this as shown in the following:

```sql
SELECT nref.value('(first-name/text())[1]', 'nvarchar(50)') FirstName,
       nref.value('(last-name/text())[1]', 'nvarchar(50)') LastName
FROM   T CROSS APPLY xCol.nodes('//author') AS R(nref)
WHERE  nref.exist('first-name[. != "David"]') = 1;
```

In this example, `nodes('//author')` yields a rowset of references to `<author>` elements for each XML instance. The first and last names of authors are obtained by evaluating `value()` methods relative to those references.

SQL Server 2000 provides the capability for generating a rowset from an XML instance by using **OpenXml()**. You can specify the relational schema for the rowset and how values inside the XML instance map to columns in the rowset.

## Example: Use OPENXML() on the xml data type

The query can be rewritten from the previous example by using **OpenXml()** as shown in the following. This is done by creating a cursor that reads each XML instance into an XML variable and then applies OpenXML to it:

```sql
DECLARE name_cursor CURSOR
FOR
   SELECT xCol
   FROM   T
OPEN name_cursor
DECLARE @xmlVal XML
DECLARE @idoc int
FETCH NEXT FROM name_cursor INTO @xmlVal;

WHILE (@@FETCH_STATUS = 0)
BEGIN
   EXEC sp_xml_preparedocument @idoc OUTPUT, @xmlVal
   SELECT   *
   FROM   OPENXML (@idoc, '//author')
          WITH (FirstName  varchar(50) 'first-name',
                LastName   varchar(50) 'last-name') R
   WHERE  R.FirstName != 'David'

   EXEC sp_xml_removedocument @idoc
   FETCH NEXT FROM name_cursor INTO @xmlVal
END;
CLOSE name_cursor;
DEALLOCATE name_cursor;
```

**OpenXml()** creates an in-memory representation and uses work tables instead of the query processor. It relies on the XPath version 1.0 processor of MSXML version 3.0 instead of the XQuery engine. The work tables aren't shared among multiple calls to **OpenXml()**, even on the same XML instance. This limits its scalability. **OpenXml()** allows you to access an edge table format for the XML data when the WITH clause isn't specified. Also, it allows you to use the remaining XML value in a separate, "overflow" column.

The combination of `nodes()` and `value()` functions uses XML indexes effectively. As a result, this combination can exhibit more scalability than **OpenXml**.

## See also

- [OPENXML &#40;SQL Server&#41;](../../relational-databases/xml/openxml-sql-server.md)
