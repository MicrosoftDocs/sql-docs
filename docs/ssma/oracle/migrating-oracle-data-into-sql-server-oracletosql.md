---
title: "Migrate Oracle Data into SQL Server (OracleToSQL)"
description: Learn how to migrate data from an Oracle database to SQL Server, after you synchronize the converted objects, by using the SSMA for Oracle application.
author: nilabjaball
ms.author: niball
ms.reviewer: randolphwest
ms.date: 06/03/2025
ms.service: sql
ms.subservice: ssma
ms.topic: how-to
ms.collection:
  - sql-migration-content
ms.custom:
  - intro-migration
f1_keywords:
  - "ssma.oracle.migratedata.f1"
helpviewer_keywords:
  - "Oracle Data Migration, Client-Side Migration"
  - "Oracle Data Migration,Server-Side Migration"
---

# Migrate Oracle Data into SQL Server (OracleToSQL)

After you successfully synchronize the converted objects with [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], you can migrate data from Oracle to [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] by using SQL Server Migration Assistant (SSMA) for Oracle.

> [!IMPORTANT]  
> If you're using the server side data migration engine, before you can migrate data, you must install the SSMA for Oracle Extension Pack and the Oracle providers on the computer that is running SSMA. The SQL Server Agent service must also be running. For more information about how to install the extension pack, see [Installing SSMA components on SQL Server](installing-ssma-components-on-sql-server-oracletosql.md).

[!INCLUDE [entra-id](../../includes/entra-id-hard-coded.md)]

## Set migration options

Before you migrate data to [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], review the project migration options in the **Project Settings** dialog.

In this dialog, you can set options such as migration batch size, table locking, constraint checking, null value handling, and identity value handling. For more information about the Project Migration Settings, see [Project Settings (Migration)](project-settings-migration-oracletosql.md).

The **Migration Engine** in the **Project Settings** dialog, allows the user to perform the migration process by using two types of data migration engines:

- Client-side data migration engine
- Server-side data migration engine

### Client-side data migration

To initiate data migration on the client side, select the **Client Side Data Migration Engine** option in the **Project Settings** dialog.

> [!NOTE]  
> The **Client-Side Data Migration Engine** resides inside the SSMA application and is, therefore, not dependent on the availability of the extension pack.

### Server-side data migration

During the server-side data migration, the engine resides on the target database. It's installed through the extension pack. For more information on how to install the extension pack, see [Installing SSMA components on SQL Server](installing-ssma-components-on-sql-server-oracletosql.md).

To initiate migration on the server side, select the **Server Side Data Migration Engine** option in the **Project Settings** dialog.

## Migrate Data to SQL Server

Migrating data is a bulk-load operation that moves rows of data from Oracle tables into [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] tables in transactions. The number of rows loaded into [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] in each transaction is configured in the project settings.

To view migration messages, make sure that the **Output** pane is visible. If it's not, from the **View** menu, select **Output**.

### Migration process

1. Verify the following requirements:

   - The Oracle providers are installed on the computer that is running SSMA.
   - You synchronized the converted objects with the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] database.

1. In Oracle Metadata Explorer, select the objects that contain the data that you want to migrate:

   - To migrate data for all schemas, select the checkbox next to **Schemas**.
   - To migrate data or omit individual tables, first expand the schema. Then expand **Tables**, and select or clear the checkbox next to the table.

1. You can choose either client-side or server-side data migration:

   To perform client-side data migration, select the **Client Side Data Migration Engine** option in the **Project Settings** dialog.

   To perform server-side data migration, first ensure:

   - The SSMA for Oracle Extension Pack is installed on the instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)].

   - The SQL Server Agent service is running on the instance of SQL Server.

   - To perform server-side data migration, select the **Server Side Data Migration Engine** option in the **Project Settings** dialog.

1. Right-click **Schemas** in Oracle Metadata Explorer, and then select **Migrate Data**. You can also migrate data for individual objects or categories of objects. Right-click the object or its parent folder, and then select the **Migrate Data** option.

   If the SSMA for Oracle Extension Pack isn't installed on the instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], and if **Server Side Data Migration Engine** is selected, while migrating the data to the target database, you receive the following error:

   ```output
   SSMA Data Migration components were not found on SQL Server, server-side data migration will not be possible. Please check if Extension Pack is installed correctly.
   ```

   Select **Cancel** to terminate the data migration.

1. In the **Connect to Oracle** dialog, enter the connection credentials, and then select **Connect**. For more information on connecting to Oracle, see [Connect to Oracle](connect-to-oracle-oracletosql.md).

   To connect to the target database [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], enter the connection credentials in the **Connect to SQL Server** dialog, and select **Connect**. For more information on connecting to [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], see [Connecting to SQL Server](connecting-to-sql-server-oracletosql.md).

   Messages appear on the **Output** pane. When the migration is complete, the **Data Migration Report** appears. If any data didn't migrate, select the row that contains the errors, and then select **Details**. When you're finished with the report, select **Close**. For more information, see [Data Migration Report](data-migration-report-oracletosql.md).

> [!NOTE]  
> When SQL Server Express edition is used as the target database, only client-side data migration is allowed and server-side data migration isn't supported.

## Migrate data at scale (preview)

When a large volume of data needs to be migrated within a short duration, at scale offline migration is a suitable option. This feature uses Azure Database Migration Service in the back end, and the scalability of cloud to migrate on-premises or external Oracle data sources to Azure SQL platform. This feature works with virtual machines (VMs) via infrastructure as a service (IaaS) or Azure SQL via platform as a service (PaaS). You can create a new Database Migration Service instance, or use an existing Database Migration Service instance to migrate data to Azure SQL PaaS or SQL Server on Azure VM.

1. For a large offline migration, select **Migrate data with DMS (preview)**.

1. Provide the Microsoft Entra account for authentication. After you enter the username, select **Connect**.

1. Select the tenant and the subscription that already has the SQL Server on Azure VM, Azure SQL database, or SQL managed instance configured, and select **Next**.

1. In the next step of the migration workflow, you can provide the data source, target database, and associated credentials.

1. When you select your Azure resource, you can use existing resources. Select the resource group and the data migration service name, or select **New** to create a resource group and Azure migration service for migration.

1. To migrate an external data source, you need an integration runtime. Either select **New** and follow the steps to create the integration runtime or use an existing integration service. To create an integration service, you need to configure a gateway installation and supply a key to configure integration runtime.

1. The Monitor Migrations Wizard automatically opens within a few seconds of initiating the data migration. To view a live migration status, select **View** under the activity you want to monitor. You can also view the comprehensive report, which takes you to the Azure portal for more granular monitoring details about the migration activity. You can access the wizard with the **Monitoring** button next to **Migrate data with DMS(preview)**.

## Related content

- [Migrate Oracle Databases to SQL Server](migrating-oracle-databases-to-sql-server-oracletosql.md)
