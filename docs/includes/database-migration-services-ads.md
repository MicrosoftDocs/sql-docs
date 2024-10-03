---
author: croblesm
ms.author: roblescarlos
ms.date: 10/03/2022
ms.service: azure-database-migration-service
ms.topic: include
---

The Azure SQL migration extension for Azure Data Studio enables you to assess, get right-sized Azure recommendations and migrate your SQL Server databases to Azure.

The Azure SQL Migration extension for Azure Data Studio offers these key benefits:

- A responsive UI for an end-to-end migration experience. The extension starts with a migration readiness assessment and SKU recommendation (preview) (based on performance data).

- An enhanced assessment mechanism that can evaluate SQL Server instances. The extension identifies databases that are ready to migrate to Azure SQL targets.

  > [!NOTE]
  > You can use the Azure SQL Migration extension to assess SQL Server databases running on Windows or Linux.

- An SKU recommendation engine that collects performance data from the on-premises source SQL Server instance and then generates right-sized SKU recommendations based on your Azure SQL target.

- A reliable Azure service powered by Azure Database Migration Service that orchestrates data movement activities to deliver a seamless migration experience.

- You can run your migration online (for migrations that require minimal downtime) or offline (for migrations where downtime persists throughout the migration) depending on your business requirements.

- You can configure a self-hosted integration runtime to use your own compute resources to access the source SQL Server instance backup files in your on-premises environment.

- Provides a secure and improved user experience for migrating TDE databases and SQL/Windows logins to Azure SQL.

For information about specific migration scenarios and Azure SQL targets, see the list of tutorials in the following table:

| Migration scenario | Migration mode
|---------|---------|
SQL Server to Azure SQL Managed Instance| [Online](/azure/dms/tutorial-sql-server-managed-instance-online-ads) / [Offline](/azure/dms/tutorial-sql-server-managed-instance-offline-ads)
SQL Server to SQL Server on an Azure virtual machine|[Online](/azure/dms/tutorial-sql-server-to-virtual-machine-online-ads) / [Offline](/azure/dms/tutorial-sql-server-to-virtual-machine-offline-ads)
SQL Server to Azure SQL Database | [Offline](/azure/dms/tutorial-sql-server-azure-sql-database-offline-ads)

> [!IMPORTANT]
> If your target is Azure SQL Database, make sure you deploy the database schema before you begin the migration. You can use tools like the [SQL Server dacpac extension](/azure-data-studio/extensions/sql-server-dacpac-extension) or the [SQL Database Projects extension](/azure-data-studio/extensions/sql-database-project-extension) for Azure Data Studio.

The following video explains recent updates and features added to the Azure SQL Migration extension for Azure Data Studio:

<br />

> [!VIDEO https://learn-video.azurefd.net/vod/player?show=data-exposed&ep=how-to-migrate-sql-server-to-azure-sql-database-offline-using-azure-data-studio-data-exposed]