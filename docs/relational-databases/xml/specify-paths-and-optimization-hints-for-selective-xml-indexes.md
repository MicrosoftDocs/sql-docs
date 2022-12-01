---
title: "Paths and optimization hints for selective XML indexes"
description: Learn how to specify node paths and optimization hints when you create or alter selective XML indexes.
ms.date: 05/05/2022
ms.service: sql
ms.reviewer: randolphwest
ms.subservice: xml
ms.topic: conceptual
author: MikeRayMSFT
ms.author: mikeray
ms.custom: "seo-lt-2019"
---
# Specify paths and optimization hints for selective XML indexes

[!INCLUDE [SQL Server Azure SQL Database](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

This article describes how to specify node paths to index and optimization hints for indexing when you create or alter selective XML indexes.

You specify node paths and optimization hints at the same time in one of the following statements:

- In the **FOR** clause of a **CREATE** statement. For more information, see [CREATE SELECTIVE XML INDEX &#40;Transact-SQL&#41;](../../t-sql/statements/create-selective-xml-index-transact-sql.md).

- In the **ADD** clause of an **ALTER** statement. For more information, see [ALTER INDEX &#40;Selective XML Indexes&#41;](../../t-sql/statements/alter-index-selective-xml-indexes.md).

For more information about selective XML indexes, see [Selective XML Indexes &#40;SXI&#41;](../../relational-databases/xml/selective-xml-indexes-sxi.md).

## <a id="untyped"></a> Understand XQuery and SQL Server types in untyped XML

Selective XML indexes support two type systems: XQuery types and [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] types. The indexed path can be used either to match an XQuery expression, or to match the return type of the `value()` method of the **xml** data type.

- When a path to index isn't annotated, or is annotated with the XQUERY keyword, the path matches an XQuery expression. There are two variations for XQUERY-annotated node paths:

  - If you don't specify the XQUERY keyword and the XQuery data type, then default mappings are used. Typically performance and storage aren't optimal.

  - If you specify the XQUERY keyword and the XQuery data type, and optionally other optimization hints, then you can achieve the best possible performance and the most efficient possible storage. However, a cast can fail.

- When a path to index is annotated with the SQL keyword, the path matches the return type of the `value()` method of the **xml** data type. Specify the appropriate [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] data type, which is the return type that you expect from the `value()` method.

There are subtle differences between the XQuery expressions XML type system and the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] type system applied to the `value()` method of the **xml** data type. These differences include the following:

- The XQuery type system is aware of trailing spaces. For example, according to XQuery type semantics, the strings "abc" and "abc " aren't equal, while in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] these strings are equal.

- XQuery floating point data types support special values of +/- zero and +/- infinity. These special values aren't supported in the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] floating point data types.

### XQuery types in untyped XML

- XQuery types match XQuery expressions in all methods of the **xml** data type including the `value()` method.

- XQuery types support these optimization hints: node(), SINGLETON, DATA TYPE, and MAXLENGTH.

For XQuery expressions over untyped XML, you can choose between two modes of operation:

- **Default mapping mode**. In this mode, you specify only the path when creating a selective XML index.

- **User-specified mapping mode**. In this mode, you specify both the path and optional optimization hints.

The default mapping mode uses a conservative storage option, which is always safe and general. It can match any expression type. A limitation of the default mapping mode is less than optimal performance, because an increased number of runtime casts are required, and secondary indexes aren't available.

Here's an example of a selective XML index created with default mappings. For all three paths, the default node type (**xs:untypedAtomic**) and cardinality are used.

```sql
CREATE SELECTIVE XML INDEX example_sxi_UX_default
ON Tbl(xmlcol)
FOR
(
    mypath01 =  '/a/b',
    mypath02 = '/a/b/c',
    mypath03 = '/a/b/d'
);
```

The user-specified mapping mode lets you specify a type and cardinality for the node to obtain better performance. However, this improved performance is achieved by giving up safety - because a cast can fail - and generality - because only the specified type is matched with the selective XML index.

The XQuery types supported for untyped XML case are:

- `xs:boolean`
- `xs:double`
- `xs:string`
- `xs:date`
- `xs:time`
- `xs:dateTime`

If the type isn't specified, the node is assumed to be of the `xs:untypedAtomic` data type.

You can optimize the selective XML index shown in the following manner:

```sql
CREATE SELECTIVE XML INDEX example_sxi_UX_optimized
ON Tbl(xmlcol)
FOR
(
    mypath= '/a/b' as XQUERY 'node()',
    pathX = '/a/b/c' as XQUERY 'xs:double' SINGLETON,
    pathY = '/a/b/d' as XQUERY 'xs:string' MAXLENGTH(200) SINGLETON
);
-- mypath - Only the node value is needed; storage is saved.
-- pathX - Performance is improved; secondary indexes are possible.
-- pathY - Performance is improved; secondary indexes are possible; storage is saved.
```

### SQL Server types in untyped XML

- [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] types match the return value of the `value()` method.

- [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] types support this optimization hint: SINGLETON.

Specifying a type is mandatory for paths that return [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] types. Use the same [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] type that you would use in the `value()` method.

Consider the following query:

```sql
SELECT T.record,
    T.xmldata.value('(/a/b/d)[1]', 'NVARCHAR(200)')
FROM myXMLTable T;
```

The specified query returns a value from the path `/a/b/d` packed into an NVARCHAR(200) data type, so the data type to specify for the node is obvious. However there's no schema to specify the cardinality of the node in untyped XML. To specify that node `d` appears at most once under its parent node `b`, create a selective XML index that uses the SINGLETON optimization hint as follows:

```sql
CREATE SELECTIVE XML INDEX example_sxi_US
ON Tbl(xmlcol)
FOR
(
    node1223 = '/a/b/d' as SQL NVARCHAR(200) SINGLETON
);
```

## <a id="typed"></a> Understand selective XML index support for typed XML

Typed XML in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is a schema associated with a given XML document. The schema defines overall document structure and types of nodes. If a schema exists, Selective XML Index applies the schema structure when the user promotes paths, so there's no need to specify the XQUERY types for paths.

Selective XML Index supports following XSD types:

- `xs:anyUri`
- `xs:boolean`
- `xs:date`
- `xs:dateTime`
- `xs:day`
- `xs:decimal`
- `xs:double`
- `xs:float`
- `xs:int`
- `xs:integer`
- `xs:language`
- `xs:long`
- `xs:name`
- `xs:NCName`
- `xs:negativeInteger`
- `xs:nmtoken`
- `xs:nonNegativeInteger`
- `xs:nonPositiveInteger`
- `xs:positiveInteger`
- `xs:qname`
- `xs:short`
- `xs:string`
- `xs:time`
- `xs:token`
- `xs:unsignedByte`
- `xs:unsignedInt`
- `xs:unsignedLong`
- `xs:unsignedShort`

When a selective XML index is created over a document that has a schema associated with it, specifying an XQuery type at index creation or altering returns an error. The user can use SQL type annotations in the path promotion part. The SQL type must be a valid conversion from the XSD type defined in the schema, or an error is thrown. All SQL types that have adequate representation in XSD are supported, with an exception of date/time types.

> [!NOTE]
> The selective index is used if the type specified in the Selective XML Index path promotion is the same as the `value()` method return value.

The following optimization hints can be used with typed XML documents:

- `node()` optimization hint.

- MAXLENGTH optimization hint can be used with xs:string types to shorten the indexed value.

For more information about optimization hints, see [Specifying Optimization Hints](#hints).

## <a id="paths"></a> Specify paths

A selective XML index lets you index only a subset of nodes from the stored XML data that are relevant for the queries that you expect to run. When the subset of relevant nodes is much smaller than the total number of nodes in the XML document, the selective XML index stores only the relevant nodes. To benefit from a selective XML index, identify the correct subset of nodes to index.

### Choose the nodes to index

You can use the following two principles to identify the correct subset of nodes to add to a selective XML index.

1. **Principle 1**: To evaluate a given XQuery expression, index all nodes that you need to examine.

   - Index all nodes whose existence or value is used in the XQuery expression.

   - Index all nodes in the XQuery expression on which XQuery predicates are applied.

   Consider the following query over the [sample XML document](#sample) in this article:

    ```sql
    SELECT T.record FROM myXMLTable T
    WHERE T.xmldata.exist('/a/b[./c = "43"]') = 1;
    ```

    In order to return the XML instances that satisfy this query, a selective XML index needs to examine two nodes in every XML instance:

   - Node `c`, because its value is used in the XQuery expression.

   - Node `b`, because a predicate is applied over node`b` in the XQuery expression.

2. **Principle 2**: For best performance, index all nodes that are required to evaluate a given XQuery expression. If you index only some of the nodes, then the selective XML index improves the evaluation of subexpressions that include only indexed nodes.

To improve the performance of the SELECT statement shown above, you can create the following selective XML index:

```sql
CREATE SELECTIVE XML INDEX simple_sxi
ON Tbl(xmlcol)
FOR
(
    path123 =  '/a/b',
    path124 =  '/a/b/c'
);
```

### Index identical paths

You can't promote identical paths as the same data type under different path names. For example, the following query raises an error, because `pathOne` and `pathTwo` are identical:

```sql
CREATE SELECTIVE INDEX test_simple_sxi ON T1(xmlCol)
FOR
(
    pathOne = 'book/authors/authorID' AS XQUERY 'xs:string',
    pathTwo = 'book/authors/authorID' AS XQUERY 'xs:string'
);
```

However, you can promote identical paths as different data types with different names. For example, the following query is now acceptable, because the data types are different:

```sql
CREATE SELECTIVE INDEX test_simple_sxi ON T1(xmlCol)
FOR
(
    pathOne = 'book/authors/authorID' AS XQUERY 'xs:double',
    pathTwo = 'book/authors/authorID' AS XQUERY 'xs:string'
);
```

### Examples

Here are some more examples of selecting the correct nodes to index for different XQuery types.

#### Example 1

Here's a simple XQuery that uses the `exist()` method:

```sql
SELECT T.record FROM myXMLTable T
WHERE T.xmldata.exist('/a/b/c/d/e/h') = 1;
```

The following table shows the nodes that should be indexed to let this query use the selective XML index.

|Node to include in the index|Reason for indexing this node|
|----------------------------------|-----------------------------------|
|**/a/b/c/d/e/h**|The existence of node `h` is evaluated in the `exist()` method.|

#### Example 2

Here's a more complex variation of the previous XQuery, with a predicate applied:

```sql
SELECT T.record FROM myXMLTable T
WHERE T.xmldata.exist('/a/b/c/d/e[./f = "SQL"]') = 1;
```

The following table shows the nodes that should be indexed to let this query use the selective XML index.

|Node to include in the index|Reason for indexing this node|
|----------------------------------|-----------------------------------|
|**/a/b/c/d/e**|A predicate is applied over node `e`.|
|**/a/b/c/d/e/f**|The value of node `f` is evaluated inside the predicate.|

#### Example 3

Here's a more complex query with a `value()` clause:

```sql
SELECT T.record,
    T.xmldata.value('(/a/b/c/d/e[./f = "SQL"]/g)[1]', 'nvarchar(100)')
FROM myXMLTable T;
```

The following table shows the nodes that should be indexed to let this query use the selective XML index.

|Node to include in the index|Reason for indexing this node|
|----------------------------------|-----------------------------------|
|**/a/b/c/d/e**|A predicate is applied over node `e`.|
|**/a/b/c/d/e/f**|The value of node `f` is evaluated inside the predicate.|
|**/a/b/c/d/e/g**|The value of node `g` is returned by the `value()` method.|

#### Example 4

Here's a query that uses a FLWOR clause inside an `exist()` clause. (The name FLWOR comes from the five clauses that can make up an XQuery FLWOR expression: for, let, where, order by, and return.)

```sql
SELECT T.record FROM myXMLTable T
WHERE T.xmldata.exist('
  For $x in /a/b/c/d/e
  Where $x/f = "SQL"
  Return $x/g
') = 1;
```

The following table shows the nodes that should be indexed to let this query use the selective XML index.

|Node to include in the index|Reason for indexing this node|
|----------------------------------|-----------------------------------|
|**/a/b/c/d/e**|The existence of node `e` is evaluated in the FLWOR clause.|
|**/a/b/c/d/e/f**|The value of node `f` is evaluated in the FLWOR clause.|
|**/a/b/c/d/e/g**|The existence of node `g` is evaluated by the `exist()` method.|

## <a id="hints"></a> Specify optimization hints

You can use optional optimization hints to specify additional mapping details for a node indexed by a selective XML index. For example, you can specify the data type and cardinality of the node, and certain information about the structure of the data. This additional information supports better mapping. It also results in improvements in performance or savings in storage, or both.

The use of optimization hints is optional. You can always accept the default mappings, which are reliable but may not provide optimal performance and storage.

Some optimization hints, such as the SINGLETON hint, introduce constraints over your data. In some cases, errors may be raised when those constraints aren't met.

### Benefits of optimization hints

The following table identifies the optimization hints that support more efficient storage or improved performance.

|Optimization hint|More efficient storage|Improved performance|
|-----------------------|----------------------------|--------------------------|
|**node()**|Yes|No|
|**SINGLETON**|No|Yes|
|**DATA TYPE**|Yes|Yes|
|**MAXLENGTH**|Yes|Yes|

### Optimization hints and data types

You can index nodes as XQuery data types or as [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] data types. The following table shows which optimization hints are supported with each data type.

|Optimization hint|XQuery data types|SQL data types|
|-----------------------|-----------------------|--------------------|
|**node()**|Yes|No|
|**SINGLETON**|Yes|Yes|
|**DATA TYPE**|Yes|No|
|**MAXLENGTH**|Yes|No|

### node() optimization hint

**Applies to:** XQuery data types

You can use the `node()` optimization to specify a node whose value isn't required to evaluate the typical query. This hint reduces storage requirements when the typical query only has to evaluate the existence of the node. (By default, a selective XML index stores the value for all promoted nodes, except complex node types.)

Consider the following example:

```sql
SELECT T.record FROM myXMLTable T
WHERE T.xmldata.exist('/a/b[./c=5]') = 1;
```

To use a selective XML index to evaluate this query, promote nodes `b` and `c`. However, since the value of node `b` isn't required, you can use the `node()` hint with the following syntax:

```sql
`/a/b/ as node()
```

If a query requires the value of a node that has been indexed with the `node()` hint, then the selective XML index can't be used.

### SINGLETON optimization hint

**Applies to:** XQuery or [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] data types

The SINGLETON optimization hint specifies the cardinality of a node. This hint improves query performance since it's known in advance that a node appears at most one time within its parent or ancestor.

Consider the [sample XML document](#sample) in this article.

To use a selective XML index to query this document, you can specify the SINGLETON hint for node `d` since it appears at most one time within its parent.

If the SINGLETON hint has been specified, but a node appears more than one time within its parent or ancestor, then an error is raised when you create the index (for existing data) or when you run a query (for new data).

### DATA TYPE optimization hint

**Applies to:** XQuery data types

The DATA TYPE optimization hint lets you specify an XQuery or a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] data type for the indexed node. The data type is used for the column in the data table of the selective XML index that corresponds to the indexed node.

When casting an existing value to the specified data type fails, the insert operation (into the index) doesn't fail; however, a null value is inserted into the data table of the index.

### MAXLENGTH optimization hint

**Applies to:** XQuery data types

The MAXLENGTH optimization hint lets you limit the length of xs:string data. MAXLENGTH isn't relevant for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] data types since you specify the length when you specify the VARCHAR or NVARCHAR date types.

When an existing string is longer than the specified MAXLENGTH, then inserting that value into the index fails.

## <a id="sample"></a> Sample XML document for examples

The following sample XML document is referenced in the examples in this article:

```xml
<a>
    <b>
         <c atc="aa">10</c>
         <c atc="bb">15</c>
         <d atd1="dd" atd2="ddd">md </d>
    </b>
     <b>
        <c></c>
        <c atc="">117</c>
     </b>
</a>
```

## See also

- [Selective XML Indexes &#40;SXI&#41;](../../relational-databases/xml/selective-xml-indexes-sxi.md)
- [Create, Alter, and Drop Selective XML Indexes](../../relational-databases/xml/create-alter-and-drop-selective-xml-indexes.md)
