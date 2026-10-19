---
title: "Migration Wizard (AccessToSQL)"
description: "Migration Wizard (AccessToSQL)"
author: nilabjaball
ms.author: niball
ms.reviewer: vanto
ms.date: 12/30/2025
ms.service: sql
ms.subservice: ssma
ms.topic: concept-article
ms.collection:
  - sql-migration-content
ms.custom:
  - intro-migration
helpviewer_keywords:
  - "Migration Wizard dialog box"
  - "Migration Wizard, adding Access databases"
  - "Migration Wizard, Connect to Azure SQL"
  - "Migration Wizard, Connect to SQL Server"
  - "Migration Wizard, Link Tables"
  - "Migration Wizard, Migration status"
  - "Migration Wizard, New Project"
  - "Migration Wizard, Selecting objects to migrate"
---
# Migration Wizard (AccessToSQL)

The SQL Server Migration Assistant (SSMA) Migration Wizard guides you through the migration of one or more databases from Access to [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] or Azure SQL. By using the wizard, you create a project, add databases to the project, select objects to migrate, and connect to [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] or Azure SQL. You also convert, load, and migrate Access schemas and data. Optionally, you can link Access tables to [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] or Azure SQL tables.

Most of the Migration Wizard pages contain the same options as existing SSMA dialog boxes. This article describes the wizard pages and provides links so that you can learn more about individual options. If a page contains unique options, this article documents them.

## Start the migration wizard

By default, the Migration Wizard appears when you start SSMA. You can also start the wizard on the **File** menu by selecting **Migration Wizard**.

## Welcome page

The Welcome page introduces the Migration Wizard and provides the following option for starting the wizard.

#### Launch this wizard at startup

By default, SSMA starts the Migration Wizard when you start SSMA. To prevent the automatic start of the wizard, clear this check box.

## Create New Project page

The Create New Project page is where you enter the project file name, location, and migration project type (the version of target SQL Server used for migration). For more information, see [New Project (SSMA)](new-project-ssma-accesstosql.md).

## Add Access Databases page

Use the **Add Access Databases** page to add one or more Access databases to the project. Add individual databases by selecting **Add Databases**, and then selecting the databases from the **Open** window. Or, find databases by using the **Find Databases** button. For more information, see the following articles:

- [Add and remove Access database files](adding-and-removing-access-database-files-accesstosql.md)
- [Find Databases Wizard](find-databases-wizard-accesstosql.md)

## Select Objects to Migrate page

On the **Select Objects to Migrate** page, select objects to convert. You can select all objects, groups of objects, or individual objects.

1. Expand **Access-metabase**, and then expand **Databases**.

1. Perform one or more of the following actions:

   - To convert all databases, select the check box next to **Databases**.

   - To convert or omit individual databases, select or clear the check box next to the database name.

   - To convert or omit queries, expand the database, and then select or clear the **Queries** check box.

   - To convert or omit individual tables, expand the database, expand **Tables**, and then select or clear the check box next to the table.

If you have many objects, use the **Advanced Object Selection** options in the right pane to filter Access database objects. For example, if you select **Tables** in the left pane, you can then filter the list of tables by entering strings in the **Filter** box. You can then select or clear the filtered tables for migration by using the buttons at the top of the pane.

For more information about filtering, see the Options section of [Advanced Object Selection](advanced-object-selection-accesstosql.md).

## Connect to SQL Server page

On the **Connect to SQL Server** page, specify connection properties, and then connect to [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. For more information, see [Connect to SQL Server (GUI)](connect-to-sql-server-accesstosql.md).

> [!IMPORTANT]  
> When the connection succeeds, you see the **Link Tables** page where you can link the tables. Select **Next** to start the migration.

## Connect to Azure SQL page

On the **Connect to Azure SQL** page, specify connection properties, and then connect to Azure SQL. To create a new Azure database, use the **Create Azure Database** option that appears when you select the **Browse** button. For more information, see [Connect to Azure SQL (GUI)](connect-to-azure-sql-db-accesstosql.md).

> [!IMPORTANT]  
> When the connection succeeds, you see the **Link Tables** page where you can link the tables. Select the **Next** button on the **Links** page to start migration.

## Link Tables page

The **Link Tables** page lets you link your original Access tables to the migrated [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] or Azure SQL tables. When you link tables, your Access database is modified so that your queries, forms, reports, and data access pages use the data in the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] or Azure SQL, instead of the data in your Access database.

#### Link tables

Select the **Link tables** check box to link Access tables to the migrated [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] or Azure SQL tables. To start migration, select the **Next** button.

## Migration Status page

The **Migration Status** page shows the progress of converting the Access schemas to [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] or Azure SQL schemas, loading the converted schemas into [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] or Azure SQL, and then migrating data.

For more information about this page, see [Convert, Load, and Migrate](convert-load-and-migrate-accesstosql.md).

## Related content

- [Get started with SQL Server Migration Assistant for Access](getting-started-with-sql-server-migration-assistant-for-access-accesstosql.md)
- [Migrate Access databases to SQL Server and Azure SQL](migrating-access-databases-to-sql-server-azure-sql-db-accesstosql.md)
- [User interface reference](user-interface-reference-accesstosql.md)
