---
title: "Load XML data"
description: Learn several methods for transferring XML data into SQL Server databases.
ms.custom: ""
ms.date: 05/05/2022
ms.service: sql
ms.reviewer: randolphwest
ms.subservice: xml
ms.topic: conceptual
helpviewer_keywords:
  - "XML data [SQL Server], loading"
  - "loading XML data"
author: MikeRayMSFT
ms.author: mikeray
---
# Load XML data

[!INCLUDE [SQL Server Azure SQL Database](../../includes/applies-to-version/sql-asdb.md)]

You can transfer XML data into [!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)] in several ways. For example:

- If you have your data in an [n]text or image column in a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database, you can import the table by using [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)]. Change the column type to XML by using the ALTER TABLE statement.

- You can bulk copy your data from another [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database by using bcp out, and then bulk insert the data into the later version database by using bcp in.

- If you have data in relational columns in a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database, create a new table with an [n]text column and, optionally, a primary key column for a row identifier. Use client-side programming to retrieve the XML that is generated at the server with FOR XML and write it into the **[n]text** column. Then, use the previously mentioned techniques to transfer data to a later version database. You can choose to write the XML into an XML column in the later version database directly.

## Bulk load XML data

You can bulk load XML data into the server by using the bulk loading capabilities of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], such as bcp. OPENROWSET allows you to load data into an XML column from files. The following example illustrates this point.

### Example: Load XML from files

This example shows how to insert a row in table T. The value of the XML column is loaded from file `C:\MyFile\xmlfile.xml` as CLOB, and the integer column is supplied the value 10.

```sql
INSERT INTO T
SELECT 10, xCol
FROM    (SELECT *
    FROM OPENROWSET (BULK 'C:\MyFile\xmlfile.xml', SINGLE_CLOB)
AS xCol) AS R(xCol);
```

## Text encoding

[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] stores XML data in Unicode (UTF-16). XML data retrieved from the server comes out in UTF-16 encoding. If you want a different encoding, you have to perform the required conversion on the retrieved data. Sometimes, the XML data may be in a different encoding. If it is, you have to use care during data loading. For example:

- If your text XML is in Unicode (UCS-2, UTF-16), you can assign it to an XML column, variable, or parameter  without any problems.

- If the encoding isn't Unicode and is implicit, because of the source code page, the string code page in the database should be the same as or compatible with the code points that you want to load. If necessary, use COLLATE. If no such server code page exists, you have to add an explicit XML declaration with the correct encoding.

- To use an explicit encoding, use either the **varbinary()** type, which has no interaction with code pages, or use a string type of the appropriate code page. Then, assign the data to an XML column, variable, or parameter.

### Example: Explicitly specify an encoding

Assume that you have an XML document, vcdoc, stored as **varchar(max)** that doesn't have an explicit XML declaration. The following statement adds an XML declaration with the encoding "iso8859-1", concatenates the XML document, casts the result to **varbinary(max)** so that the byte representation is preserved, and then finally casts it to XML. This enables the XML processor to parse the data according to the specified encoding "iso8859-1" and generate the corresponding UTF-16 representation for string values.

```sql
SELECT CAST(
CAST (('<?xml version="1.0" encoding="iso8859-1"?>'+ vcdoc) AS VARBINARY (MAX))
AS XML);
```

### String encoding incompatibilities

If you copy and paste XML as a string literal into the Query Editor window in [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], you might experience **[n]varchar** string encoding incompatibilities. This will depend on the encoding of your XML instance. In many cases, you may want to remove the XML declaration. For example:

```xml
<?xml version="1.0" encoding="UTF-8"?>
  <xsd:schema ...
```

You should then prefix the string with an `N` to make the XML instance an instance of Unicode. For example:

```sql
-- Assign XML instance to a variable.
DECLARE @X XML
SET @X = N'...'
-- Insert XML instance into an xml type column.
INSERT INTO T VALUES (N'...')
-- Create an XML schema collection
CREATE XML SCHEMA COLLECTION XMLCOLL1 AS N'<xsd:schema ... '
```

## See also

- [XML Data &#40;SQL Server&#41;](../../relational-databases/xml/xml-data-sql-server.md)
