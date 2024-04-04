---
title: "Transact-SQL Syntax Conventions (Transact-SQL)"
description: "This article lists and describes conventions that are used in the syntax diagrams for Transact-SQL."
author: rwestMSFT
ms.author: randolphwest
ms.date: 12/28/2022
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "sql13.TSQLExpandPortal.f1"
helpviewer_keywords:
  - "conventions [SQL Server]"
  - "Applies to section in Transact-SQL topics"
  - "code example conventions [SQL Server]"
  - "objects [SQL Server], names"
  - "code [SQL Server], conventions"
  - "multipart names [SQL Server]"
  - "Transact-SQL syntax conventions"
  - "syntax conventions [SQL Server]"
  - "code [SQL Server]"
  - "Transact-SQL"
  - "naming conventions [SQL Server]"
  - "syntax [SQL Server], Transact-SQL"
dev_langs:
  - "TSQL"
monikerRange: ">= aps-pdw-2016 || = azuresqldb-current || = azure-sqldw-latest || >= sql-server-2016 || >= sql-server-linux-2017 || = azuresqldb-mi-current ||=fabric"
---

# Transact-SQL syntax conventions (Transact-SQL)

[!INCLUDE [sql-asdb-asdbmi-asa-pdw-fabricse-fabricdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw-fabricse-fabricdw.md)]

The following table lists and describes conventions that are used in the syntax diagrams in the [!INCLUDE[tsql](../../includes/tsql-md.md)] reference.

| Convention | Used for |
| --- | --- |
| UPPERCASE | [!INCLUDE[tsql](../../includes/tsql-md.md)] keywords. |
| _italic_ | User-supplied parameters of [!INCLUDE[tsql](../../includes/tsql-md.md)] syntax. |
| **bold** | Type database names, table names, column names, index names, stored procedures, utilities, data type names, and text exactly as shown. |
| &#124; (vertical bar) | Separates syntax items enclosed in brackets or braces. You can use only one of the items. |
| [ ] (brackets) | Optional syntax item. |
| { } (braces) | Required syntax items. Don't type the braces. |
| [ , ...*n* ] | Indicates the preceding item can be repeated _n_ number of times. The occurrences are separated by commas. |
| [ ...*n* ] | Indicates the preceding item can be repeated _n_ number of times. The occurrences are separated by blanks. |
| ; | [!INCLUDE[tsql](../../includes/tsql-md.md)] statement terminator. Although the semicolon isn't required for most statements in this version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], it will be required in a future version. |
| <label> ::= | The name for a block of syntax. Use this convention to group and label sections of lengthy syntax or a unit of syntax that you can use in more than one location within a statement. Each location in which the block of syntax could be used is indicated with the label enclosed in chevrons: \<label>.<br /><br />A set is a collection of expressions, for example \<grouping set>; and a list is a collection of sets, for example \<composite element list>. |

## Multipart names

Unless specified otherwise, all [!INCLUDE[tsql](../../includes/tsql-md.md)] references to the name of a database object can be a four-part name in the following form:

*server_name*.[*database_name*].[*schema_name*].*object_name*

\| *database_name*.[*schema_name*].*object_name*

\| *schema_name*.*object_name*

\| *object_name*

- *server_name*

  Specifies a linked server name or remote server name.

- *database_name*

  Specifies the name of a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database when the object resides in a local instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. When the object is in a linked server, *database_name* specifies an OLE DB catalog.

- *schema_name*

  Specifies the name of the schema that contains the object if the object is in a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database. When the object is in a linked server, *schema_name* specifies an OLE DB schema name.

- *object_name*

  Refers to the name of the object.

When referencing a specific object, you don't always have to specify the server, database, and schema for the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] to identify the object. However, if the object can't be found, an error is returned.

To avoid name resolution errors, we recommend specifying the schema name whenever you specify a schema-scoped object.

To omit intermediate nodes, use periods to indicate these positions. The following table shows the valid formats of object names.

| Object reference format | Description |
| --- | --- |
| *server_name*.*database_name*.*schema_name*.*object_name* | Four-part name. |
| *server_name*.*database_name*..*object_name* | Schema name is omitted. |
| *server_name*..*schema_name*.*object_name* | Database name is omitted. |
| *server_name*...*object_name* | Database and schema name are omitted. |
| *database_name*.*schema_name*.*object_name* | Server name is omitted. |
| *database_name*..*object_name* | Server and schema name are omitted. |
| *schema_name*.*object_name* | Server and database name are omitted. |
| *object_name* | Server, database, and schema name are omitted. |

## Code example conventions

Unless stated otherwise, the examples provided in the [!INCLUDE[tsql](../../includes/tsql-md.md)] reference were tested by using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] and its default settings for the following options:

- ANSI_NULLS
- ANSI_NULL_DFLT_ON
- ANSI_PADDING
- ANSI_WARNINGS
- CONCAT_NULL_YIELDS_NULL
- QUOTED_IDENTIFIER

Most code examples in the [!INCLUDE[tsql](../../includes/tsql-md.md)] reference have been tested on servers that are running a case-sensitive sort order. The test servers were typically running the ANSI/ISO 1252 code page.

Many code examples prefix Unicode character string constants with the letter `N`. Without the `N` prefix, the string is converted to the default code page of the database. This default code page may not recognize certain characters.

## "Applies to" references

The [!INCLUDE[tsql](../../includes/tsql-md.md)] reference articles encompass multiple versions of [!INCLUDE[ssnoversion-md](../../includes/ssnoversion-md.md)], starting with [!INCLUDE[sql2008-md](../../includes/sql2008-md.md)], as well as [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)], [!INCLUDE[ssazuremi-md](../../includes/ssazuremi-md.md)], [!INCLUDE[ssazuresynapse-md](../../includes/ssazuresynapse-md.md)], and [!INCLUDE[ssazurepdw_md](../../includes/ssazurepdw_md.md)].

There's a section near the top of each article indicating which products support the article's subject. If a product is omitted, then the feature described by the article isn't available in that product.

The general subject of the article might be used in a product, but all of the arguments aren't supported in some cases. For example, contained database users were introduced in [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)]. Use the `CREATE USER` statement in any [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] product, however the `WITH PASSWORD` syntax can't be used with older versions. Additional **Applies to** sections are inserted into the appropriate argument descriptions in the body of the article.

## See also

- [Transact-SQL reference (Database Engine)](../language-reference.md)
- [Reserved Keywords (Transact-SQL)](reserved-keywords-transact-sql.md)
