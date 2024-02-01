---
title: "Catalog stored procedures (Transact-SQL)"
description: "An index of catalog stored procedures in SQL Server Transact-SQL."
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 05/24/2023
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
helpviewer_keywords:
  - "ODBC applications, catalog stored procedures"
  - "isolating ODBC applications from changes"
  - "ODBC data dictionary functions"
  - "system stored procedures [SQL Server], catalog"
  - "catalog system stored procedures [SQL Server]"
dev_langs:
  - "TSQL"
---
# Catalog stored procedures (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

[!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] supports the following system stored procedures that implement ODBC data dictionary functions and isolate ODBC applications from changes to underlying system tables.

:::row:::
    :::column:::
        [sp_column_privileges](sp-column-privileges-transact-sql.md)

        [sp_columns](sp-columns-transact-sql.md)

        [sp_databases](sp-databases-transact-sql.md)

        [sp_fkeys](sp-fkeys-transact-sql.md)

        [sp_pkeys](sp-pkeys-transact-sql.md)

        [sp_server_info](sp-server-info-transact-sql.md)
    :::column-end:::
    :::column:::
        [sp_special_columns](sp-special-columns-transact-sql.md)

        [sp_sproc_columns](sp-sproc-columns-transact-sql.md)

        [sp_statistics](sp-statistics-transact-sql.md)

        [sp_stored_procedures](sp-stored-procedures-transact-sql.md)

        [sp_table_privileges](sp-table-privileges-transact-sql.md)

        [sp_tables](sp-tables-transact-sql.md)
    :::column-end:::
:::row-end:::

Stored procedures for certain platforms only as specified, including Azure Synapse Analytics:

:::row:::
    :::column:::
        [sp_create_openrowset_statistics](sp-create-openrowset-statistics.md)

        [sp_drop_openrowset_statistics](sp-drop-openrowset-statistics.md)

    :::column-end:::
:::row-end:::

## Related content

- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
