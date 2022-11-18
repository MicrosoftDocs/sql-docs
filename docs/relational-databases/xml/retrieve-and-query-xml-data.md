---
title: "Retrieve and query XML data"
description: Learn about the query options that must be specified when querying XML data, and about the parts of XML instances that aren't preserved when stored in databases.
ms.custom: ""
ms.date: 05/05/2022
ms.service: sql
ms.reviewer: randolphwest
ms.subservice: xml
ms.topic: conceptual
helpviewer_keywords:
  - "XML data [SQL Server], retrieving"
  - "XML instance retrieval"
author: MikeRayMSFT
ms.author: mikeray
---
# Retrieve and query XML data

[!INCLUDE [SQL Server Azure SQL Database](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

This article describes the query options that you have to specify to query XML data. It also describes the parts of XML instances that aren't preserved when they're stored in databases.

## <a id="features"></a> Features of an XML instance that aren't preserved

[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] preserves the content of the XML instance, but doesn't preserve aspects of the XML instance that aren't considered to be significant in the XML data model. This means that a retrieved XML instance might not be identical to the instance that was stored in the server, but will contain the same information.

### XML declaration

The XML declaration in an instance isn't preserved when the instance is stored in the database. For example:

```sql
CREATE TABLE T1 (Col1 int primary key, Col2 xml);
GO
INSERT INTO T1 values (1, '<?xml version="1.0" encoding="windows-1252" ?><doc></doc>');
GO
SELECT Col2
FROM T1;
```

The result is `<doc/>`.

The XML declaration, such as `<?xml version='1.0'?>`, isn't preserved when storing XML data in an **xml** data type instance. This is by design. The XML declaration () and its attributes (version/encoding/stand-alone) are lost after data is converted to type **xml**. The XML declaration is treated as a directive to the XML parser. The XML data is stored internally as ucs-2. All other PIs in the XML instance are preserved.

### Order of attributes

The order of attributes in an XML instance isn't preserved. When you query the XML instance stored in the **xml** type column, the order of attributes in the resulting XML may be different from the original XML instance.

### Quotation marks around attribute values

Single quotation marks and double quotations marks around attribute values aren't preserved. The attribute values are stored in the database as a name and value pair. The quotation marks aren't stored. When an XQuery is executed against an XML instance, the resulting XML is serialized with double quotation marks around the attribute values.

```sql
DECLARE @x xml;
-- Use double quotation marks.
SET @x = '<root a="1" />';
SELECT @x;
GO
DECLARE @x xml;
-- Use single quotation marks.
SET @x = '<root a=''1'' />';
SELECT @x;
GO
```

Both queries return = `<root a="1" />`.

### Namespace prefixes

Namespace prefixes aren't preserved. When you specify XQuery against an **xml** type column, the serialized XML result may return different namespace prefixes.

```sql
DECLARE @x xml;
SET @x = '<ns1:root xmlns:ns1="abc" xmlns:ns2="abc">
            <ns2:SomeElement/>
          </ns1:root>';
SELECT @x;
SELECT @x.query('/*');
GO
```

The namespace prefix in the result may be different. For example:

```xml
<p1:root xmlns:p1="abc"><p1:SomeElement/></p1:root>
```

## <a id="query"></a> Sett required query options

When querying **xml** type columns or variables using **xml** data type methods, the following options must be set as shown.

|SET Options|Required Values|
|-----------------|---------------------|
|ANSI_NULLS|ON|
|ANSI_PADDING|ON|
|ANSI_WARNINGS|ON|
|ARITHABORT|ON|
|CONCAT_NULL_YIELDS_NULL|ON|
|NUMERIC_ROUNDABORT|OFF|
|QUOTED_IDENTIFIER|ON|

If the options aren't set as shown, queries and modifications on **xml** data type methods will fail.

## See also

- [Create Instances of XML Data](../../relational-databases/xml/create-instances-of-xml-data.md)
