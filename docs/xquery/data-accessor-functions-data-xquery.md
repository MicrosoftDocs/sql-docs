---
title: "data Function (XQuery)"
description: Learn how to use the XQuery function data() to return the typed value for each item in a specified sequence of items.
author: uc-msft
ms.author: umajay
ms.reviewer: vanto
ms.date: 09/26/2025
ms.service: sql
ms.subservice: xml
ms.topic: reference
helpviewer_keywords:
  - "fn:data function"
  - "data function [XQuery]"
  - "data function"
dev_langs:
  - "XML"
---
# Data accessor functions - data (XQuery)

[!INCLUDE [SQL Server Azure SQL Database](../includes/applies-to-version/sqlserver.md)]

Returns the typed value for each item specified by *$arg*.

## Syntax

```syntaxsql
fn:data ($arg as item()*) as xdt:untypedAtomic*
```

## Arguments

#### *$arg*

Sequence of items whose typed values are returned.

## Remarks

The following considerations apply to typed values:

- The typed value of an atomic value is the atomic value.

- The typed value of a text node is the string value of the text node.

- The typed value of a comment is the string value of the comment.

- The typed value of a processing instruction is the content of the processing-instruction, without the processing instruction target name.

- The typed value of a document node is its string value.

The following considerations apply to attribute and element nodes:

- If an attribute node is typed with an XML schema type, its typed value is the typed value, accordingly.

- If the attribute node is untyped, its typed value is equal to its string value that is returned as an instance of `xdt:untypedAtomic`.

- If the element node hasn't been typed, its typed value is equal to its string value that is returned as an instance of `xdt:untypedAtomic`.

The following considerations apply to typed element nodes:

- If the element has a simple content type, `data()` returns the typed value of the element.

- If the node is of complex type, including xs:anyType, `data()` returns a static error.

Although using the `data()` function is frequently optional, as shown in the following examples, specifying the `data()` function explicitly increases query readability. For more information, see [XQuery Basics](xquery-basics.md).

You can't specify `data()` on constructed XML, as shown in the following example:

```sql
DECLARE @x AS XML;

SET @x = '';

SELECT @x.query('data(<SomeNode>value</SomeNode>)');
```

## Examples

This article provides XQuery examples against XML instances stored in various **xml** type columns in the AdventureWorks database.

### A. Use the data() XQuery function to extract typed value of a node

The following query illustrates how the `data()` function is used to retrieve values of an attribute, an element, and a text node:

```sql
WITH XMLNAMESPACES ('https://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ProductModelDescription' AS p1, 'https://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ProductModelWarrAndMain' AS wm)
SELECT CatalogDescription.query(N'
 for $pd in //p1:ProductDescription
 return
    <Root
      ProductID = "{ data( ($pd//@ProductModelID)[1] ) }"
      Feature =   "{ data( ($pd/p1:Features/wm:Warranty/wm:Description)[1] ) }" >
    </Root>
 ') AS Result
FROM Production.ProductModel
WHERE ProductModelID = 19;
```

Here's the result:

```xml
<Root ProductID="19" Feature="parts and labor"/>
```

As mentioned, the `data()` function is optional when you're constructing attributes. If you don't specify the `data()` function, it's implicitly assumed. The following query produces the same results as the previous query:

```sql
WITH XMLNAMESPACES ('https://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ProductModelDescription' AS p1, 'https://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ProductModelWarrAndMain' AS wm)
SELECT CatalogDescription.query('
      for $pd in //p1:ProductDescription
         return
          <Root
                ProductID = "{ ($pd/@ProductModelID)[1] }"
                Feature =   "{ ($pd/p1:Features/wm:Warranty/wm:Description)[1] }" >
           </Root>
 ') AS Result
FROM Production.ProductModel
WHERE ProductModelID = 19;
```

The following examples illustrate instances in which the `data()` function is required.

In the following query, `$pd/p1:Specifications/Material` returns the <`Material`> element. Also, `data($pd/p1:Specifications/ Material)` returns character data typed as `xdt:untypedAtomic`, because <`Material`> is untyped. When the input is untyped, the result of `data()` is typed as `xdt:untypedAtomic`.

```sql
SELECT CatalogDescription.query('
declare namespace p1="https://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ProductModelDescription";
      for $pd in //p1:ProductDescription
         return
          <Root>
             { $pd/p1:Specifications/Material }
             { data($pd/p1:Specifications/Material) }
           </Root>
 ') AS Result
FROM Production.ProductModel
WHERE ProductModelID = 19;
```

Here's the result:

```xml
<Root>
  <Material>Aluminum Alloy</Material>Aluminum Alloy
</Root>
```

In the following query, `data($pd/p1:Features/wm:Warranty)` returns a static error, because <`Warranty`> is a complex type element.

```sql
WITH XMLNAMESPACES ('https://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ProductModelDescription' AS p1, 'https://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ProductModelWarrAndMain' AS wm)
SELECT CatalogDescription.query('
 <Root>
   {     /p1:ProductDescription/p1:Features/wm:Warranty }
   { data(/p1:ProductDescription/p1:Features/wm:Warranty) }
 </Root>
 ') AS Result
FROM Production.ProductModel
WHERE ProductModelID = 23;
```

## Related content

- [XQuery Functions against the xml Data Type](xquery-functions-against-the-xml-data-type.md)
