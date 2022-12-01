---
title: "Define the Serialization of XML Data"
description: Learn about the rules used when serializing xml data in SQL Server.
ms.custom: ""
ms.date: 05/05/2022
ms.service: sql
ms.reviewer: randolphwest
ms.subservice: xml
ms.topic: conceptual
helpviewer_keywords:
  - "entitization rules [XML in SQL Server]"
  - "serialization"
  - "reparsing serialized XML structures"
  - "encoding [XML in SQL Server]"
  - "XML [SQL Server], serialization"
  - "xml data type [SQL Server], serialization"
  - "typed XML"
author: MikeRayMSFT
ms.author: mikeray
---
# Define the serialization of XML data

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

When casting the **xml** data type explicitly or implicitly to a SQL string or binary type, the content of the **xml** data type will be serialized according to the rules outlined in this article.

## Serialization encoding

If the SQL target type is VARBINARY, the result is serialized in UTF-16 with a UTF-16-byte order mark in front, but without an XML declaration. If the target type is too small, an error is raised.

For example:

```sql
select CAST(CAST(N'<Δ/>' as XML) as VARBINARY(MAX))
```

This is the result:

```console
0xFFFE3C0094032F003E00
```

If the SQL target type is NVARCHAR or NCHAR, the result is serialized in UTF-16 without the byte order mark in front and without an XML declaration. If the target type is too small, an error is raised.

For example:

```sql
select CAST(CAST(N'<Δ/>' as XML) as NVARCHAR(MAX))
```

This is the result:

```console
<Δ/>
```

If the SQL target type is VARCHAR or CHAR, the result is serialized in the encoding that corresponds to the database's collation code page without a byte order mark or XML declaration. If the target type is too small or the value can't be mapped to the target collation code page, an error is raised.

For example:

```sql
select CAST(CAST(N'<Δ/>' as XML) as VARCHAR(MAX))
```

This may result in an error, if the current collation's code page can't represent the Unicode character Δ, or it will represent it in the specific encoding.

When returning XML results to the client side, the data will be sent in UTF-16 encoding. The client-side provider will then expose the data according to its API rules.

## Serialization of the XML structures

The content of an **xml** data type is serialized in the usual way. Specifically, element nodes are mapped to element markup, and text nodes are mapped to text content. However, the circumstances under which characters are entitized and how typed atomic values are serialized are described in the following sections.

## Entitization of XML characters during serialization

Every serialized XML structure should be capable of being reparsed. Therefore, some characters have to be serialized in an entitized way to preserve the round-trip capability of the characters through the XML parser's normalization phase. However, some characters have to be entitized so that the document is well-formed and, therefore, able to be parsed. Following are the entitization rules that apply during serialization:

- The characters &, \<, and > are always entitized to `&amp;`, `&lt;`, and `&gt;` respectively, if they occur inside an attribute value or element content.

- Because SQL Server uses a quotation mark (U+0022) for enclosing attribute values, the quotation mark in attribute values is entitized as `&quot;`.

- A surrogate pair is entitized as a single numeric character reference, when casting on the server only. For example, the surrogate pair U+D800 U+DF00 is entitized to the numeric character reference `&#x00010300;`.

- To protect a TAB (U+0009) and a linefeed (LF, U+000A) from being normalized during parsing, they're entitized to their numeric character references `&#x9;` and `&#xA;` respectively, inside attribute values.

- To prevent a carriage return (CR, U+000D) from being normalized during parsing, it's entitized to its numeric character reference, `&#xD;` inside both attribute values and element content.

- To protect text nodes that only contain white space, one of the white-space characters, generally the last one, is entitized as its numeric character reference. In this way, reparsing preserves the white-space text node, regardless of the setting of the white-space handling during parsing.

For example:

```sql
DECLARE @u NVARCHAR(50)
set @u = N'<a a="
    '+NCHAR(0xD800)+NCHAR(0xDF00)+N'>">   '+NCHAR(0xA)+N'</a>'
SELECT CAST(CONVERT(XML,@u,1) as NVARCHAR(50));
```

This is the result:

```console
<a a="
    𐌀>">
</a>
```

If you don't want to apply the last white-space protection rule, you can use the explicit CONVERT option 1 when casting from **xml** to a string or binary type. For example, to avoid entitization, you can do the following:

```sql
SELECT CONVERT(NVARCHAR(50), CONVERT(XML, '<a>   </a>', 1), 1);
```

The [query() Method (xml Data Type)](../../t-sql/xml/query-method-xml-data-type.md) results in an **xml** data type instance. Therefore, any result of the `query()` method that is cast to a string or binary type is entitized according to the previously described rules. If you want to obtain the string values that aren't entitized, you should use the [value() Method (xml Data Type)](../../t-sql/xml/value-method-xml-data-type.md) instead. Following is an example of using the `query()` method:

```sql
DECLARE @x xml
SET @x = N'<a>This example contains an entitized char: .</a>'
SELECT @x.query('/a/text()');
```

This is the result:

```console
This example contains an entitized char: .
```

Following is an example of using the `value()` method:

```sql
SELECT @x.value('(/a/text())[1]', 'nvarchar(100)');
```

This is the result:

```console
This example contains an entitized char: .
```

## Serializing a typed xml data type

A typed **xml** data type instance contains values that are typed according to their XML schema types. These values are serialized according to their XML schema type in the same format as the XQuery cast to xs:string produces. For more information, see [Type Casting Rules in XQuery](../../xquery/type-casting-rules-in-xquery.md).

For example, the xs:double value 1.34e1 is serialized to 13.4 as shown in the following example:

```sql
declare @x xml
set @x =''
select CAST(@x.query('1.34e1') as nvarchar(50));
```

This returns the string value 13.4.

## See also

- [Type Casting Rules in XQuery](../../xquery/type-casting-rules-in-xquery.md)
- [CAST and CONVERT &#40;Transact-SQL&#41;](../../t-sql/functions/cast-and-convert-transact-sql.md)
