---
title: "Management Data Warehouse stored procedures (Transact-SQL)"
description: "Management Data Warehouse stored procedures (Transact-SQL)"
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 05/24/2023
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
helpviewer_keywords:
  - "management data warehouse, data collector stored procedures"
dev_langs:
  - "TSQL"
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Management Data Warehouse stored procedures (Transact-SQL)

[!INCLUDE [sql-asdb-asdbmi-asa-pdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

The data collector stores data and data collector type information in the management data warehouse. The following stored procedures are used to update data and change the tables in the management data warehouse.

- [core.sp_create_snapshot (Transact-SQL)](core-sp-create-snapshot-transact-sql.md)
- [core.sp_add_collector_type (Transact-SQL)](core-sp-add-collector-type-transact-sql.md)
- [core.sp_purge_data (Transact-SQL)](core-sp-purge-data-transact-sql.md)
- [core.sp_update_data_source (Transact-SQL)](core-sp-update-data-source-transact-sql.md)
- [core.sp_remove_collector_type (Transact-SQL)](core-sp-remove-collector-type-transact-sql.md)

## Related content

- [Data Collector stored procedures (Transact-SQL)](data-collector-stored-procedures-transact-sql.md)
