---
title: "Tutorial: Migrate SQL Server to Azure SQL Database (Offline)"
titleSuffix: Azure Database Migration Service
description: Learn how to migrate on-premises SQL Server to Azure SQL Database offline by using Azure Database Migration Service.
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: abhishekum, mathoma
ms.date: 05/04/2026
ms.service: azure-database-migration-service
ms.topic: tutorial
ms.collection:
  - sql-migration-content
  - migration
  - onprem-to-azure
ms.custom:
  - sfi-image-nochange
---

# Tutorial: Migrate SQL Server to Azure SQL Database (offline)

You can use Azure Database Migration Service via the Azure portal, to migrate databases from an on-premises instance of SQL Server to Azure SQL Database (offline).

In this tutorial, learn how to migrate the sample `AdventureWorks2022` database from an on-premises instance of SQL Server to Azure SQL Database, by using Database Migration Service. This tutorial uses offline migration mode, which considers an acceptable downtime during the migration process.

In this tutorial, you learn how to:
> [!div class="checklist"]
> - Create an instance of Azure Database Migration Service
> - Start your migration and monitor progress to completion

> [!IMPORTANT]  
> Currently, *online* migrations for Azure SQL Database targets aren't available with Azure Database Migration Service. In an *offline* migration, application downtime starts when the migration starts. Testing an offline migration is recommended to determine whether the downtime is acceptable.

## Migration options

The following section describes how to use Azure Database Migration Service with the Azure portal.

### Prerequisites

Before you begin the tutorial:

- Ensure that you can access the [Azure portal](https://portal.azure.com).
- Make sure that the [**Microsoft.DataMigration** resource provider is registered in your subscription](/azure/dms/quickstart-create-data-migration-service-portal#register-the-resource-provider).

- Have an Azure account that's assigned to one of the following built-in roles:
  - Contributor for the target Azure SQL Database
  - Reader role for the Azure resource group that contains the target Azure SQL Database
  - Owner or Contributor role for the Azure subscription (required if you create a new instance of Azure Database Migration Service)

  As an alternative to using one of these built-in roles, you can [assign a custom role](custom-roles.md).

- Create a target [Azure SQL Database](/azure/azure-sql/database/single-database-create-quickstart).

- The source SQL Server login must be a member of the **db_datareader** role on the source database and have the **VIEW ANY DEFINITION** server permission. The target SQL Server login must be a member of the db_owner role on the target database.

- To migrate the database Schema from the source to the target Azure SQL Database by using the Database Migration Service, the minimum supported [SHIR version](https://www.microsoft.com/download/details.aspx?id=39717) required is 5.37 or above.

- For schema migration, minimum permissions on the source SQL Server is **db_owner** to access the database and on the target Azure SQL Database, the user should be member of the all the **server level roles** in the following table:

| Roles | Description |
| --- | --- |
| **##MS_DatabaseManager##** | Members of the **##MS_DatabaseManager##** fixed server role can create and delete databases. A member of the **##MS_DatabaseManager##** role that creates a database becomes the owner of that database, which allows that user to connect to that database as the dbo user. The dbo user has all database permissions in the database. Members of the **##MS_DatabaseManager##** role don't necessarily have permission to access databases that they don't own. Use this server role instead of the **dbmanager** fixed database role that exists in `master`. |
| **##MS_DatabaseConnector##** | Members of the **##MS_DatabaseConnector##** fixed server role can connect to any database without requiring a user account in the database to connect with. |
| **##MS_DefinitionReader##** | Members of the **##MS_DefinitionReader##** fixed server role can read all catalog views that are covered by `VIEW ANY DEFINITION` on any database on which the member of this role has a user account. |
| **##MS_LoginManager##** | Members of the **##MS_LoginManager##** fixed server role can create and delete logins. It's recommended to use this server role over the **loginmanager** database level role that exists in the `master` database. |

### Prepare the target Azure SQL Database

To create the login and user on the target Azure SQL Database, run the following script on the `master` database:

```sql
CREATE LOGIN testuser WITH PASSWORD = '<password>';

ALTER SERVER ROLE ##MS_DefinitionReader## ADD MEMBER [testuser];
GO

ALTER SERVER ROLE ##MS_DatabaseConnector## ADD MEMBER [testuser];
GO

ALTER SERVER ROLE ##MS_DatabaseManager## ADD MEMBER [testuser];
GO

ALTER SERVER ROLE ##MS_LoginManager## ADD MEMBER [testuser];
GO

CREATE USER testuser FOR LOGIN testuser;
EXECUTE sp_addRoleMember 'dbmanager', 'testuser';
EXECUTE sp_addRoleMember 'loginmanager', 'testuser';
```

Now, you can migrate both the database schema and data using Database Migration Service. You can also use other tools such as the [SQL Database Projects extension](/sql/tools/sql-database-projects/sql-database-projects#original-projects-vs-sdk-style-projects) in Visual Studio Code to migrate the schema before selecting the list of tables to migrate.

> [!NOTE]  
> If no tables exist on the Azure SQL Database target, or no tables are selected before starting the migration, the **Next** button isn't available to initiate the migration. If no table exists on the target, then you must select the schema migration option to move forward.

### Create a Database Migration Service instance

[!INCLUDE [create-database-migration-service-instance](../../includes/create-database-migration-service-instance.md)]

### Start a new migration

1. To start a new migration, go to [Azure Database Migration Service](https://portal.azure.com) in the Azure portal, and either use **+Create** to create a new instance of Database Migration Service, or select an existing instance, and then go to your Azure Database Migration Service instance.

1. On the **Overview** pane of your Azure Database Migration Service instance, select **New migration**:

   :::image type="content" source="media/database-migration-service/dms-portal-sql-database-dashboard-4-new.png" alt-text="Screenshot of Azure Database Migration Dashboard." lightbox="media/database-migration-service/dms-portal-sql-database-dashboard-4-new.png":::

1. Under **Select new migration** scenario, choose your source, target server type, migration mode and choose **Select**.

   :::image type="content" source="media/database-migration-service/dms-portal-sql-database-scenario-new.png" alt-text="Screenshot of select new migration scenario." lightbox="media/database-migration-service/dms-portal-sql-database-scenario-new.png":::

1. On the **Azure SQL Database Offline Migration Wizard**, follow these steps:

   1. On the **Source details** tab, enter details for the source SQL Server instance, and then select **Next: Connect to source SQL Server**:

      :::image type="content" source="media/database-migration-service/dms-portal-sql-database-source-1-new.png" alt-text="Screenshot of Source Tracking." lightbox="media/database-migration-service/dms-portal-sql-database-source-1-new.png":::

   1. On the **Connect to source SQL Server** tab, provide connection details and then select **Next: Select databases for migration**:

      :::image type="content" source="media/database-migration-service/dms-portal-sql-database-source-2-new.png" alt-text="Screenshot of Connect to source." lightbox="media/database-migration-service/dms-portal-sql-database-source-2-new.png":::

   1. On the **Select databases for migration** tab, check the box next to the databases you want to migrate. Populating the list of databases can take some time. Select **Next: Connect to target Azure SQL Database**.

      :::image type="content" source="media/database-migration-service/dms-portal-sql-database-select-db-1-new.png" alt-text="Screenshot of select db." lightbox="media/database-migration-service/dms-portal-sql-database-select-db-1-new.png":::

   1. On the **Connect to target Azure SQL Database** tab, provide connection details and then select **Next: Map source and target databases**:

      :::image type="content" source="media/database-migration-service/dms-portal-sql-database-connect-target-1-new.png" alt-text="Screenshot of connect target." lightbox="media/database-migration-service/dms-portal-sql-database-connect-target-1-new.png":::

   1. On the **Map source and target databases** tab, map the databases between the source and target.

      :::image type="content" source="media/database-migration-service/dms-portal-sql-database-map-db-1-new.png" alt-text="Screenshot of Map databases." lightbox="media/database-migration-service/dms-portal-sql-database-map-db-1-new.png":::

   1. (Optional) Check the box next to **Migrate Missing schema** to deploy missing schema objects from the source to the Azure SQL Database target to migrate the following schema objects with a *single checkbox*:

      - Schemas
      - Tables (selected)
      - Indexes
      - Views
      - Stored procedures (StoredProcedures)
      - Synonyms
      - DDL triggers (DdlTriggers)
      - Defaults
      - Full text catalogs (FullTextCatalogs)
      - Plan guides (PlanGuides)
      - Roles
      - Rules
      - Application roles (ApplicationRoles)
      - User defined aggregates (UserDefinedAggregates)
      - User defined data types (UserDefinedDataTypes)
      - User defined functions (UserDefinedFunctions)
      - User defined table types (UserDefinedTableTypes)
      - User defined types (UserDefinedTypes)
      - Users* (not every user type)
      - XmlSchemaCollections

      > [!NOTE]  
      > - If you select **Migrate Missing Schema**, the Database Migration service performs the schema migration before data is migrated.
      > - DMS proceeds with the data migration phase even if schema migration encounters errors, unless there are issues with table objects.

      Next, either use **Select all tables** to migrate all tables, or use the text entry box to filter the list of tables and select individual tables to migrate. Then select **Next: Database migration summary**.

      :::image type="content" source="media/database-migration-service/dms-portal-sql-database-select-schema-table-new.png" alt-text="Screenshot of select schema and tables." lightbox="media/database-migration-service/dms-portal-sql-database-select-schema-table-new.png":::

   1. On the **Database migration summary** tab, review the details and then select **Start migration**, which starts database migration and automatically takes you back to the Database Migration Service dashboard.

      :::image type="content" source="media/database-migration-service/dms-portal-sql-database-summary-new.png" alt-text="Screenshot of Summary." lightbox="media/database-migration-service/dms-portal-sql-database-summary-new.png":::

      > [!NOTE]  
      > For an offline migration, application downtime starts when the migration starts.

### Monitor the database migration

1. To monitor your database migration, on the **Overview** pane of your Database Migration Service instance, select **Monitor migrations**.

   :::image type="content" source="media/database-migration-service/dms-portal-sql-database-dashboard-4-new.png" alt-text="Screenshot of Azure Database Migration Service overview in the Azure portal." lightbox="media/database-migration-service/dms-portal-sql-database-dashboard-4-new.png":::

1. Under the **Migrations** tab, you can track migrations that are in progress, completed, and failed (if any), or you can view all database migrations. In the menu bar, select **Refresh** to update the migration status.

   :::image type="content" source="media/database-migration-service/dms-portal-sql-database-dashboard-3-new.png" alt-text="Screenshot of DMS dashboard monitoring." lightbox="media/database-migration-service/dms-portal-sql-database-dashboard-3-new.png":::

   Database Migration Service returns the latest known migration status each time migration status refreshes. The following table describes possible statuses:

   | Status | Description |
   | --- | --- |
   | **Creating** | The service is starting the migration. |
   | **Preparing for copy** | The service is disabling autostats, triggers, and indexes in the target table. |
   | **Copying** | Data is being copied from the source database to the target database. |
   | **Copy finished** | Data copy is finished. The service is waiting on other tables to finish copying to begin the final steps to return tables to their original schema. |
   | **Rebuilding indexes** | The service is rebuilding indexes on target tables. |
   | **Succeeded** | All data is copied and the indexes are rebuilt. |

1. Under **Source name**, select a database name to open the table view.. In this detailed view, you see the current status of the migration, the number of tables that currently are in that status, and a detailed status of each table:

   :::image type="content" source="media/database-migration-service/dms-portal-sql-database-monitoring-1-new.png" alt-text="Screenshot of Detailed migration monitoring." lightbox="media/database-migration-service/dms-portal-sql-database-monitoring-1-new.png":::

1. When all table data is migrated to the Azure SQL Database target, Database Migration Service updates the migration status from **In progress** to **Succeeded**.

   :::image type="content" source="media/database-migration-service/dms-portal-sql-database-monitoring-2-new.png" alt-text="Screenshot of Detailed migration success." lightbox="media/database-migration-service/dms-portal-sql-database-monitoring-2-new.png":::

> [!NOTE]  
> Database Migration Service optimizes migration by skipping tables with no data (0 rows). Tables that don't have data don't appear in the list, even if you selected the tables when you created the migration.

You've completed the migration to Azure SQL Database. Go through a series of post-migration tasks to ensure that everything functions smoothly and efficiently.

---

## Limitations

Azure SQL Database offline migration utilizes Azure Data Factory (ADF) pipelines for data movement and thus abides by ADF limitations. A corresponding ADF is created when a database migration service is also created. Thus factory limits apply per service.

- The machine where the SHIR is installed acts as the compute for migration. Make sure this machine can handle the cpu and memory load of the data copy. To learn more, review [Create and configure a self-hosted integration runtime](/azure/data-factory/create-self-hosted-integration-runtime).
- 100,000 table per database limit.
- 10,000 concurrent database migrations per service.
- Migration speed heavily depends on the target Azure SQL Database SKU and the self-hosted Integration Runtime host.
- Azure SQL Database migration scales poorly with table numbers due to ADF overhead in starting activities. If a database has thousands of tables, the startup process of each table might take a couple of seconds, even if they're composed of one row with 1 bit of data.
- Azure SQL Database table names with double-byte characters currently aren't supported for migration. Mitigation is to rename tables before migration; they can be changed back to their original names after successful migration.
- Tables with large blob columns might fail to migrate due to timeout.
- Database names with SQL Server reserved are currently not supported.
- Database names that include semicolons are currently not supported.
- Computed columns don't get migrated.
- Columns in the source database that have default constraints and contain `NULL` values, are migrated with their defined default values on the target Azure SQL database, rather than retaining the NULLs.
- If your source database has [change data capture](/sql/relational-databases/track-changes/about-change-data-capture-sql-server) (CDC) enabled and you are using Azure Database Migration Service (Azure DMS) for schema migration, you should disable CDC on the source database before starting the migration. If CDC isn't disabled beforehand, Azure DMS will migrate CDC-related objects (such as tables) to the target. This can cause problems when attempting to enable CDC on the target database post-migration.

## Related content

- [Quickstart: Create a single database - Azure SQL Database](/azure/azure-sql/database/single-database-create-quickstart)
- [What is Azure SQL Database?](/azure/azure-sql/database/sql-database-paas-overview)
- [Azure SQL Database and Azure SQL Managed Instance connect and query articles](/azure/azure-sql/database/connect-query-content-reference-guide)
