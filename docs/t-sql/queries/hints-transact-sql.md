---
title: "Hints (Transact-SQL)"
description: Hints are options or strategies specified for enforcement by the SQL Server query processor on SELECT, INSERT, UPDATE, or DELETE statements.
author: VanMSFT
ms.author: vanto
ms.reviewer: randolphwest
ms.date: 03/07/2025
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom:
  - ignite-2025
helpviewer_keywords:
  - "query optimizer [SQL Server], hints"
  - "hints [SQL Server], about hints"
  - "SELECT statement [SQL Server], hints"
  - "hints [SQL Server]"
  - "INSERT statement [SQL Server], hints"
  - "UPDATE statement [SQL Server], hints"
  - "DELETE statement [SQL Server], hints"
dev_langs:
  - "TSQL"
monikerRange: "=azuresqldb-current || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric || =fabric-sqldb"
---
# Hints (Transact-SQL)

[!INCLUDE [sql-asdb-asdbmi-fabricse-fabricdw-fabricsqldb](../../includes/applies-to-version/sql-asdb-asdbmi-fabricse-fabricdw-fabricsqldb.md)]

Hints are options or strategies specified for enforcement by the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] query processor on `SELECT`, `INSERT`, `UPDATE`, or `DELETE` statements. The hints override any execution plan the query optimizer might select for a query.

> [!CAUTION]  
> Because the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] query optimizer typically selects the best execution plan for a query, we recommend that `<join_hint>`, `<query_hint>`, and `<table_hint>` be used only as a last resort by experienced developers and database administrators.

The following hints are described in this section:

- [Join hints](hints-transact-sql-join.md)
- [Query hints](hints-transact-sql-query.md)
- [Table hints](hints-transact-sql-table.md)
