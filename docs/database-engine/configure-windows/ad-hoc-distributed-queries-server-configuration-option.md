---
title: "Server configuration: Ad Hoc Distributed Queries"
description: Find out how to enable Ad Hoc Distributed Queries in SQL Server. You can then use OPENROWSET and OPENDATASOURCE to connect to remote OLE DB data sources.
author: rwestMSFT
ms.author: randolphwest
ms.date: 07/18/2024
ms.service: sql
ms.subservice: configuration
ms.topic: conceptual
helpviewer_keywords:
  - "OPENROWSET function, ad hoc distributed queries option"
  - "Ad Hoc Distributed Queries option"
  - "ad hoc distributed queries"
  - "7415 (Database Engine Error)"
  - "OPENDATASOURCE function, ad hoc distributed queries option"
  - "ad hoc access"
---
# Server configuration: Ad Hoc Distributed Queries

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

By default, [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] doesn't allow ad hoc distributed queries using `OPENROWSET` and `OPENDATASOURCE`. When this option is set to `1`, [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] allows ad hoc access. When this option isn't set or is set to `0`, [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] doesn't allow ad hoc access.

Ad hoc distributed queries use the `OPENROWSET` and `OPENDATASOURCE` functions to connect to remote data sources that use OLE DB. `OPENROWSET` and `OPENDATASOURCE` should be used only to reference OLE DB data sources that are accessed infrequently. For any data sources that are accessed more than several times, define a linked server.

Enabling the use of ad hoc names means that any authenticated [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] account can access the provider. [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] administrators should enable this feature for providers that are safe for any local account to access.

## Remarks

If you attempt to make an ad hoc connection with `Ad Hoc Distributed Queries` disabled, you see the following error:

```output
Msg 7415, Level 16, State 1, Line 1

Ad hoc access to OLE DB provider 'Microsoft.ACE.OLEDB.12.0' has been denied. You must access this provider through a linked server.
```

## Azure SQL Database and Azure SQL Managed Instance

See the [Features comparison: Azure SQL Database and Azure SQL Managed Instance](/azure/azure-sql/database/features-comparison) for reference.

## Examples

The following example enables `Ad Hoc Distributed Queries` and then queries a server named `Seattle1` using the `OPENROWSET` function.

```sql
EXEC sp_configure 'show advanced options', 1;
RECONFIGURE;
GO
EXEC sp_configure 'Ad Hoc Distributed Queries', 1;
RECONFIGURE;
GO

SELECT a.*
FROM OPENROWSET('MSOLEDBSQL', 'Server=Seattle1;Trusted_Connection=yes;',
     'SELECT GroupName, Name, DepartmentID
      FROM AdventureWorks2022.HumanResources.Department
      ORDER BY GroupName, Name') AS a;
GO
```

## Related content

- [Server configuration options](server-configuration-options-sql-server.md)
- [Linked Servers (Database Engine)](../../relational-databases/linked-servers/linked-servers-database-engine.md)
- [OPENROWSET (Transact-SQL)](../../t-sql/functions/openrowset-transact-sql.md)
- [OPENDATASOURCE (Transact-SQL)](../../t-sql/functions/opendatasource-transact-sql.md)
- [sp_addlinkedserver (Transact-SQL)](../../relational-databases/system-stored-procedures/sp-addlinkedserver-transact-sql.md)
