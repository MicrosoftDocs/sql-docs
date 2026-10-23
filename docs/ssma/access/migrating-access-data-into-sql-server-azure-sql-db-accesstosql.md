---
title: "Migrating Access Data into SQL Server and Azure SQL (AccessToSQL)"
description: "Migrating Access Data into SQL Server and Azure SQL (AccessToSQL)"
author: nilabjaball
ms.author: niball
ms.reviewer: vanto
ms.date: 12/30/2025
ms.service: sql
ms.subservice: ssma
ms.topic: upgrade-and-migration-article
ms.collection:
  - sql-migration-content
ms.custom:
  - intro-migration
helpviewer_keywords:
  - "bulk loading data"
  - "data, loading into Azure SQL"
  - "data, loading into SQL Server"
  - "migrating databases, loading data"
  - "migrating databases, options"
  - "options, migrating data"
  - "Azure SQL, migrating data into"
  - "SQL Server, migrating data into"
---
# Migrate Access data into SQL Server and Azure SQL (AccessToSQL)

After you use SQL Server Migration Assistant (SSMA) to create the database objects in the target, you can migrate data from Access to [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] or Azure SQL.

## Set migration options

Before you migrate data into [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] or Azure SQL, review the project migration options in the **Project Settings** dialog box.

In this dialog box, you can set:

- migration batch size
- table locking
- constraint checking
- insertion trigger firing
- identity and null value handling
- how to handle dates that are out of the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] range

For more information, see [Project Settings (Migration)](project-settings-migration-accesstosql.md).

## Migrate data

Migrating data is a bulk-load operation that moves rows of data into [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] or Azure SQL in transactions. You can configure the number of rows to load into [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] or Azure SQL in each transaction in the project settings.

To view migration messages, make sure the Output pane is visible. If it's not, on the **View** menu, select **Output**.

1. Make sure you load the Access database objects into [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] or Azure SQL.

1. In Access Metadata Explorer, select the objects that contain the data that you want to migrate:

   - To migrate data for an entire database, select the check box next to the database name.

   - To migrate data from individual tables, expand the database, expand **Tables**, and then select the check box next to the table. To omit data from individual tables, clear the check box.

1. Right-click **Databases** and then select **Migrate Data**.

You can also migrate data outside of SSMA by using the [bcp utility](../../tools/bcp-utility.md) or [SQL Server Integration Services](../../integration-services/sql-server-integration-services.md).

## Related content

- [Migrate Access databases to SQL Server and Azure SQL](migrating-access-databases-to-sql-server-azure-sql-db-accesstosql.md)
- [Set conversion and migration options](setting-conversion-and-migration-options-accesstosql.md)
- [Link Access applications to SQL Server and Azure SQL](linking-access-applications-to-sql-server-azure-sql-db-accesstosql.md)
