---
title: "sp_xml_removedocument (Transact-SQL)"
description: "Removes the internal representation of the XML document specified by the document handle and invalidates the document handle."
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 06/26/2023
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_xml_removedocument_TSQL"
  - "sp_xml_removedocument"
helpviewer_keywords:
  - "sp_xml_removedocument"
dev_langs:
  - "TSQL"
---
# sp_xml_removedocument (Transact-SQL)

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

Removes the internal representation of the XML document specified by the document handle and invalidates the document handle.

A parsed document is stored in the internal cache of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. The MSXML parser (`msxmlsql.dll`) uses one-eighth the total memory available for [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. To avoid running out of memory, run `sp_xml_removedocument` to free up the memory.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_xml_removedocument hdoc
[ ; ]
```

## Arguments

#### *hdoc*

The handle to the newly created document. A handle that isn't valid returns an error. *hdoc* is an **integer**.

## Return code values

`0` (success) or `> 0` (failure).

## Permissions

Requires membership in the **public** role.

## Examples

The following example removes the internal representation of an XML document. The handle to the document is provided as input.

```sql
EXEC sp_xml_removedocument @hdoc;
```

## Related content

- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
- [XML stored procedures (Transact-SQL)](xml-stored-procedures-transact-sql.md)
- [sys.dm_exec_xml_handles (Transact-SQL)](../system-dynamic-management-views/sys-dm-exec-xml-handles-transact-sql.md)
- [sp_xml_preparedocument(Transact-SQL)](sp-xml-preparedocument-transact-sql.md)
- [OPENXML (Transact-SQL)](../../t-sql/functions/openxml-transact-sql.md)
