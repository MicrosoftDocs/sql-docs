---
title: XML data (SQL Server)
description: Use XML data for developing rich applications for semi-structured data management.
author: MikeRayMSFT
ms.author: mikeray
ms.reviewer: randolphwest
ms.date: 08/04/2023
ms.service: sql
ms.subservice: xml
ms.topic: conceptual
helpviewer_keywords:
  - "XML [SQL Server]"
  - "XML [SQL Server], about XML"
---
# XML data (SQL Server)

[!INCLUDE [SQL Server Azure SQL Database](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] provides a powerful platform for developing rich applications for semi-structured data management. Support for XML is integrated into all the components in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] in the following ways:

- The **xml** data type. XML values can be stored natively in an **xml** data type column that can be typed according to a collection of XML schemas, or left untyped. You can index the XML column.

- The ability to specify an XQuery query against XML data stored in columns and variables of the **xml** type.

- Enhancements to OPENROWSET to allow bulk loading of XML data.

- The FOR XML clause, to retrieve relational data in XML format.

- The OPENXML function, to retrieve XML data in relational format.

- XML compression provides a method to compress off-row XML data for both XML columns and indexes, improving capacity requirements. For more information, see [CREATE TABLE (Transact-SQL)](../../t-sql/statements/create-table-transact-sql.md) and [CREATE INDEX (Transact-SQL)](../../t-sql/statements/create-index-transact-sql.md). XML compression is available in [!INCLUDE[sssql22-md](../../includes/sssql22-md.md)] and later versions, [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)], and [!INCLUDE [ssazuremi](../../includes/ssazuremi-md.md)].

## Next steps

- [XML Data Type and Columns (SQL Server)](../../relational-databases/xml/xml-data-type-and-columns-sql-server.md)
- [XML Indexes (SQL Server)](../../relational-databases/xml/xml-indexes-sql-server.md)
- [XML Schema Collections (SQL Server)](../../relational-databases/xml/xml-schema-collections-sql-server.md)
- [FOR XML (SQL Server)](../../relational-databases/xml/for-xml-sql-server.md)
- [OPENXML (Transact-SQL)](../../t-sql/functions/openxml-transact-sql.md)

## See also

- [Examples of Bulk Import and Export of XML Documents (SQL Server)](../../relational-databases/import-export/examples-of-bulk-import-and-export-of-xml-documents-sql-server.md)
- [XQuery Language Reference (SQL Server)](../../xquery/xquery-language-reference-sql-server.md)
- [xml (Transact-SQL)](../../t-sql/xml/xml-transact-sql.md)