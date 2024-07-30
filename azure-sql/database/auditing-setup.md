---
title: Set up Auditing
titleSuffix: Azure SQL Database & Azure Synapse Analytics
description: This article provides an overview of how to set up Auditing and storing those audits to an Azure storage account, Log Analytics workspace, or Event Hubs destination.
author: sravanisaluru
ms.author: srsaluru
ms.reviewer: mathoma
ms.date: 04/26/2023
ms.service: azure-sql-database
ms.subservice: security
ms.topic: conceptual
---
# Set up Auditing for Azure SQL Database and Azure Synapse Analytics

[!INCLUDE[appliesto-sqldb-asa](../includes/appliesto-sqldb-asa.md)]

In this article, we go over setting up Auditing for your logical server or database in [Azure SQL Database](sql-database-paas-overview.md) and [Azure Synapse Analytics](/azure/synapse-analytics/sql-data-warehouse/sql-data-warehouse-overview-what-is).

## Configure Auditing for your server

The default auditing policy includes the following set of action groups, which audits all the queries and stored procedures executed against the database, as well as successful and failed logins:

- BATCH_COMPLETED_GROUP
- SUCCESSFUL_DATABASE_AUTHENTICATION_GROUP
- FAILED_DATABASE_AUTHENTICATION_GROUP

To configure auditing for different types of actions and action groups using PowerShell, see [Manage Azure SQL Database Auditing](auditing-manage-using-api.md).

Azure SQL Database and Azure Synapse Analytics Audit can store 4000 characters of data for character fields in an audit record. When the **statement** or the **data_sensitivity_information** values returned from an auditable action contain more than 4000 characters, any data beyond the first 4000 characters are **truncated and not audited**.

The following section describes the Auditing configuration using the Azure portal.

> [!NOTE]  
> You can't enable auditing on a paused dedicated SQL pool. To enable auditing, [resume the dedicated SQL pool](/azure/synapse-analytics/sql-data-warehouse/pause-and-resume-compute-portal).
>
> When Auditing is configured to a Log Analytics workspace or to an Event Hubs destination in the Azure portal or PowerShell cmdlet, a [Diagnostic Setting](/azure/azure-monitor/essentials/diagnostic-settings) is created with `SQLSecurityAuditEvents` category enabled.

1. Go to the [Azure portal](https://portal.azure.com).
1. Navigate to **Auditing** under the **Security** heading in your **SQL database** or **SQL server** pane.
1. If you prefer to set up a server auditing policy, you can select the **View server settings** link on the database auditing page. You can then view or modify the server auditing settings. Server auditing policies apply to all existing and newly created databases on this server.

   :::image type="content" source="./media/auditing-overview/2_auditing_get_started_server_inherit.png" alt-text="Screenshot that shows the View server settings link highlighted on the database auditing page.":::

1. If you prefer to enable auditing on the database level, switch **Auditing** to **ON**. If server auditing is enabled, the database-configured audit exists side-by-side with the server audit.

1. You have multiple options for configuring where audit logs are stored. You can write logs to an Azure storage account, to a Log Analytics workspace for consumption by Azure Monitor logs, or to event hub for consumption using event hub. You can configure any combination of these options, and audit logs are written to each.

   :::image type="content" source="./media/auditing-overview/auditing-select-destination.png" alt-text="Screenshot that shows the storage options for Auditing.":::

## Audit to storage destination

To configure writing audit logs to a storage account, select **Storage** when you get to the **Auditing** section. Select the Azure storage account where you want to save your logs. You can use the following two storage authentication types: **Managed Identity** and **Storage Access Keys**. For managed identity, system-assigned and user-assigned managed identity is supported. By default, the primary user identity assigned to the server is selected. If there's no user identity, then a system-assigned managed identity is created and used for authentication purposes. After you have chosen an authentication type, select a retention period by opening **Advanced properties** and selecting **Save**. Logs older than the retention period are deleted.

:::image type="content" source="./media/auditing-overview/auditing_select_storage.png" alt-text="Screenshot that shows storage account authentication types for Auditing.":::

> [!NOTE]  
> If you are deploying from the Azure portal, make sure that the storage account is in the same region as your database and server. If you are deploying through other methods, the storage account can be in any region.

- The default value for retention period is 0 (unlimited retention). You can change this value by moving the **Retention (Days)** slider in **Advanced properties** when configuring the storage account for auditing.
  - If you change retention period from 0 (unlimited retention) to any other value, the retention will only apply to logs written after the retention value was changed. Logs written during the period when retention days were set to unlimited retention are preserved, even after retention is enabled.

## Audit to Log Analytics destination

To configure writing audit logs to a Log Analytics workspace, select **Log Analytics** and open **Log Analytics details**. Select the Log Analytics workspace where logs you want logs stored, and then select **OK**. If you haven't created a Log Analytics workspace, see [Create a Log Analytics workspace in the Azure portal](/azure/azure-monitor/logs/quick-create-workspace).

:::image type="content" source="./media/auditing-overview/auditing_select_oms.png" alt-text="Screenshot showing the Log Analytics workspace.":::

## Audit to Event Hubs destination

To configure writing audit logs to an event hub, select **Event Hub**. Select the event hub where you want logs stored, and then select **Save**. Be sure that the event hub is in the same region as your database and server.

:::image type="content" source="./media/auditing-overview/auditing_select_event_hub.png" alt-text="Screenshot showing the Event hub.":::

> [!NOTE]
> If you are using multiple targets like storage account , log analytics or event hub , make sure you have permissions for all the targets else saving audit configuration would fail as it will try to save the settings for all targets. 

## Next steps

> [!div class="nextstepaction"]
> [Use Auditing to analyze audit logs and reports](auditing-analyze-audit-logs.md)

## See also

- [Auditing overview](auditing-overview.md)
- Data Exposed episode [What's New in Azure SQL Auditing](/Shows/Data-Exposed/Whats-New-in-Azure-SQL-Auditing)
- [Auditing for SQL Managed Instance](../managed-instance/auditing-configure.md)
- [Auditing for SQL Server](/sql/relational-databases/security/auditing/sql-server-audit-database-engine)
