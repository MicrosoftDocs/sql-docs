---
title: "Transact-SQL Reference (Database Engine)"
description: This article gives the basics about how to find and use Transact-SQL (T-SQL) reference articles.
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: randolphwest
ms.date: 01/26/2026
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom:
  - ignite-2025
f1_keywords:
  - "sql13.tsqlref.f1"
  - "devlang-tsql"
helpviewer_keywords:
  - "Transact-SQL"
monikerRange: ">=aps-pdw-2016 || =azuresqldb-current || =azure-sqldw-latest || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric || =fabric-sqldb"
---

# Transact-SQL reference (Database Engine)

[!INCLUDE [sql-asdb-asdbmi-asa-pdw-fabricse-fabricdw-fabricsqldb](../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw-fabricse-fabricdw-fabricsqldb.md)]

This article gives the basics about how to find and use the Microsoft [!INCLUDE [tsql](../includes/tsql-md.md)] (T-SQL) reference articles. T-SQL is central to using Microsoft SQL products and services. All tools and applications that communicate with a SQL Server database do so by sending T-SQL commands.

## T-SQL compliance with the SQL standard

For detailed technical documents about how certain standards are implemented in [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)], see the [Microsoft SQL Server Standards Support documentation](/openspecs/sql_standards/ms-sqlstandlp/89fb00b1-4b9e-4296-92ce-a2b3f7ca01d2).

## Tools that use T-SQL

Some of the Microsoft tools that issue T-SQL commands are:

- [SQL Server Management Studio (SSMS)](/ssms/sql-server-management-studio-ssms)
- [MSSQL extension for Visual Studio Code](../tools/visual-studio-code-extensions/mssql/mssql-extension-visual-studio-code.md)
- [SQL Server Data Tools (SSDT)](../ssdt/download-sql-server-data-tools-ssdt.md)
- [sqlcmd](../tools/sqlcmd/sqlcmd-utility.md)

## Locate the Transact-SQL reference articles

To find T-SQL articles, use search at the top right of this page, or use the table of contents on the left side of the page. You can also type a T-SQL key word in the Management Studio Query Editor window, and press F1.

## Common Transact-SQL statements

- [SELECT](queries/select-transact-sql.md)
- [INSERT](statements/insert-transact-sql.md)
- [UPDATE](queries/update-transact-sql.md)
- [DELETE](statements/delete-transact-sql.md)

For more information, see [Transact-SQL statements](statements/statements.md).

## Find system views

To find the system tables, views, functions, and procedures, see these links, which are in the [Databases](../relational-databases/databases/databases.md) section of the SQL documentation.

- [System catalog views](../relational-databases/system-catalog-views/catalog-views-transact-sql.md)
- [System compatibility views](../relational-databases/system-compatibility-views/system-compatibility-views-transact-sql.md)
- [System dynamic management views](../relational-databases/system-dynamic-management-views/system-dynamic-management-views.md)
- [System functions by category](../relational-databases/system-functions/system-functions-category-transact-sql.md)
- [System information schema views](../relational-databases/system-information-schema-views/system-information-schema-views-transact-sql.md)
- [System stored procedures](../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)
- [System tables](../relational-databases/system-tables/system-tables-transact-sql.md)

## "Applies to" references

The T-SQL reference articles encompass multiple versions of SQL Server, starting with [!INCLUDE [sql2008-md](../includes/sql2008-md.md)], and the other Azure SQL services. Near the top of each article, is a section that indicates which products and services support subject of the article.

For example, this article applies to all versions, and has the following label.

[!INCLUDE [sql-asdb-asa-pdw](../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

Another example, the following label indicates an article that applies only to Azure Synapse Analytics and Parallel Data Warehouse.

[!INCLUDE [asa-pdw](../includes/applies-to-version/asa-pdw.md)]

In some cases, the article is used by a product or service, but all of the arguments aren't supported. In this case, other **Applies to** sections are inserted into the appropriate argument descriptions in the body of the article.

For more information, see [SQL Server docs navigation guide](../sql-server/sql-docs-navigation-guide.md#applies-to).

## Get help from Microsoft Q & A

For online help, see the [Microsoft Q & A Transact-SQL Forum](/answers/tags/191/sql-server).

## See other language references

The SQL docs include these other language references:

- [XQuery language reference (SQL Server)](../xquery/xquery-language-reference-sql-server.md)
- [Integration Services language reference](../integration-services/integration-services-language-reference.md)
- [Replication language reference](../relational-databases/replication/replication-language-reference.md)
- [Multidimensional Expressions (MDX) reference](../mdx/multidimensional-expressions-mdx-reference.md)

## Related content

- [Tutorial: Write Transact-SQL statements](tutorial-writing-transact-sql-statements.md)
- [Transact-SQL syntax conventions (Transact-SQL)](language-elements/transact-sql-syntax-conventions-transact-sql.md)
