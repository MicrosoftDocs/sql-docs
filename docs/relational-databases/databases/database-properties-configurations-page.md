---
title: "Database Properties (Configurations Page)"
description: "Learn how to use the Configurations tab in the Database Properties dialog box to view or modify a database scoped option."
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: randolphwest, erinstellato 
ms.date: 01/07/2025
ms.service: sql
ms.subservice: configuration
ms.topic: concept-article
f1_keywords:
  - "sql13.swb.databaseproperties.configurations.f1"
---
# Database Properties (Configurations page)

[!INCLUDE [sqlserver2016-asdb-asdbmi-asa-fabricsqldb](../../includes/applies-to-version/sqlserver2016-asdb-asdbmi-asa-fabricsqldb.md)]

Use the **Database Properties** page to view or modify options for the selected database. For more information about the options available on this page, see [ALTER DATABASE SCOPED CONFIGURATION](../../t-sql/statements/alter-database-scoped-configuration-transact-sql.md).

> [!IMPORTANT]
> Different `DATABASE SCOPED CONFIGURATION` options are supported in different versions of SQL Server, and in different Azure or Fabric platforms using the SQL Database Engine.

## Options

#### Database Scoped Option

Displays the name of the database scoped option for the database.

#### Value for Primary

Displays the option value for the primary database.

#### Value for Secondary

Displays the option value for all secondary databases.

## Related content

- [sys.database_scoped_configurations (Transact-SQL)](../system-catalog-views/sys-database-scoped-configurations-transact-sql.md)
