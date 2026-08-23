---
title: "local-name Function (XQuery)"
description: Learn how to use the XQuery function local-name() to return the local-name part of a node.
author: rothja
ms.author: jroth
ms.reviewer: randolphwest
ms.date: 09/26/2025
ms.service: sql
ms.subservice: xml
ms.topic: reference
helpviewer_keywords:
  - "fn:local-name function"
  - "local-name function"
dev_langs:
  - "XML"
---
# Functions on nodes - local-name

[!INCLUDE [SQL Server Azure SQL Database](../includes/applies-to-version/sqlserver.md)]

Returns the local part of the name of *$arg* as an `xs:string` that is either the zero-length string, or has the lexical form of an `xs:NCName`. If the argument isn't provided, the default is the context node.

## Syntax

```syntaxsql
fn:local-name() as xs:string
fn:local-name($arg as node()?) as xs:string
```

## Arguments

#### *$arg*

Node name whose local-name part is retrieved.

## Remarks

- In SQL Server, `fn:local-name()` without an argument can only be used in the context of a context-dependent predicate. Specifically, it can only be used inside brackets (`[ ]`).

- If the argument is supplied and is the empty sequence, the function returns the zero-length string.

- If the target node has no name, because it's a document node, a comment, or a text node, the function returns the zero-length string.

## Examples

This article provides XQuery examples against XML instances that are stored in various **xml** type columns in the AdventureWorks database.

### A. Retrieve local name of a specific node

The following query is specified against an untyped XML instance. The query expression, `local-name(/ROOT[1])`, retrieves the local name part of the specified node.

```sql
DECLARE @x AS XML;

SET @x = '<ROOT><a>111</a></ROOT>';

SELECT @x.query('local-name(/ROOT[1])');
-- result = ROOT
```

The following query is specified against the Instructions column, a typed xml column, of the ProductModel table. The expression, `local-name(/AWMI:root[1]/AWMI:Location[1])`, returns the local name, `Location`, of the specified node.

```sql
SELECT Instructions.query('
declare namespace AWMI="https://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ProductModelManuInstructions" ;
     local-name(/AWMI:root[1]/AWMI:Location[1])') AS Result
FROM Production.ProductModel
WHERE ProductModelID = 7;
-- result = Location
```

### B. Use local-name without argument in a predicate

The following query is specified against the Instructions column, typed **xml** column, of the ProductModel table. The expression returns all the element children of the <`root`> element whose local name part of the QName is `"Location"`. The `local-name()` function is specified in the predicate and it has no arguments The context node is used by the function.

```sql
SELECT Instructions.query('
declare namespace AWMI="https://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ProductModelManuInstructions" ;
  /AWMI:root//*[local-name() = "Location"]') AS Result
FROM Production.ProductModel
WHERE ProductModelID = 7;
```

The query returns all the `<Location>` element children of the `<root>` element.

## Related content

- [XQuery Functions against the xml Data Type](xquery-functions-against-the-xml-data-type.md)
- [Functions on Nodes - namespace-uri](functions-on-nodes-namespace-uri.md)
