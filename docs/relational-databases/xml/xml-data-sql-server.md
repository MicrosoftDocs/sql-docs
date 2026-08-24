---
title: XML data (SQL Server)
description: Use XML data for developing rich applications for semi-structured data management.
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: randolphwest
ms.date: 08/04/2023
ms.service: sql
ms.subservice: xml
ms.topic: concept-article
ms.custom:
  - ignite-2025
helpviewer_keywords:
  - "XML [SQL Server]"
  - "XML [SQL Server], about XML"
---
# XML data (SQL Server)

[!INCLUDE [SQL Server Azure SQL Database FabricSQLDB](../../includes/applies-to-version/sql-asdb-asdbmi-fabricsqldb.md)]

[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] provides a powerful platform for developing rich applications for semi-structured data management. Support for XML is integrated into all the components in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] in the following ways:

- The **xml** data type. XML values can be stored natively in an **xml** data type column that can be typed according to a collection of XML schemas, or left untyped. You can index the XML column.

- The ability to specify an XQuery query against XML data stored in columns and variables of the **xml** type.

- Enhancements to OPENROWSET to allow bulk loading of XML data.

- The FOR XML clause, to retrieve relational data in XML format.

- The OPENXML function, to retrieve XML data in relational format.

- XML compression provides a method to compress off-row XML data for both XML columns and indexes, improving capacity requirements. For more information, see [CREATE TABLE (Transact-SQL)](../../t-sql/statements/create-table-transact-sql.md) and [CREATE INDEX (Transact-SQL)](../../t-sql/statements/create-index-transact-sql.md). XML compression is available in [!INCLUDE[sssql22-md](../../includes/sssql22-md.md)] and later versions, [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)], and [!INCLUDE [ssazuremi](../../includes/ssazuremi-md.md)].

## Related content

- [Examples of bulk import and export of XML documents (SQL Server)](../import-export/examples-of-bulk-import-and-export-of-xml-documents-sql-server.md)
- [XQuery Language Reference (SQL Server)](../../xquery/xquery-language-reference-sql-server.md)
- [xml (Transact-SQL)](../../t-sql/xml/xml-transact-sql.md)
- [xml data type and columns (SQL Server)](xml-data-type-and-columns-sql-server.md)
- [XML indexes (SQL Server)](xml-indexes-sql-server.md)
- [XML schema collections (SQL Server)](xml-schema-collections-sql-server.md)
- [FOR XML (SQL Server)](for-xml-sql-server.md)
- [OPENXML (Transact-SQL)](../../t-sql/functions/openxml-transact-sql.md)
