---
title: Auditing Microsoft support operations
titleSuffix: Azure SQL Database & Azure Synapse Analytics
description: How to use Auditing to audit Microsoft support operations.
author: sravanisaluru
ms.author: srsaluru
ms.reviewer: mathoma
ms.date: 04/26/2023
ms.service: azure-sql-database
ms.subservice: security
ms.topic: conceptual
---
# Auditing Microsoft support operations

[!INCLUDE[appliesto-sqldb-asa](../includes/appliesto-sqldb-asa.md)]

Auditing of Microsoft support operations for your [logical server](logical-servers.md) in Azure SQL Database allows you to audit Microsoft support engineers' operations when they need to access your server during a support request. The use of this capability, along with your auditing, enables more transparency into your workforce and allows for anomaly detection, trend visualization, and data loss prevention.

Auditing of Microsoft support operations includes the following set of action groups, which audit all queries executed against the database, as well as successful and failed logins by Microsoft support engineers:

- BATCH_COMPLETED_GROUP
- SUCCESSFUL_DATABASE_AUTHENTICATION_GROUP
- FAILED_DATABASE_AUTHENTICATION_GROUP

## Enable auditing

To enable auditing of Microsoft support operations, Go to the [Azure portal](https://portal.azure.com).Navigate to **Auditing** under the **Security** heading in your Azure **SQL server** pane, and switch **Enable Auditing of Microsoft support operations** to **ON**.

:::image type="content" source="./media/auditing-overview/support-operations.png" alt-text="Screenshot of Microsoft support operations in the Azure portal.":::

To review the audit logs of Microsoft support operations in your Log Analytics workspace, use the following query:

```kusto
AzureDiagnostics
| where Category == "DevOpsOperationsAudit"
```

You have the option of choosing a different storage destination for this auditing log, or use the same auditing configuration for your server.

:::image type="content" source="media/auditing-overview/auditing-support-operation-log-destination.png" alt-text="Screenshot of Auditing configuration for auditing Microsoft support operations.":::

## See also

- [Auditing overview](auditing-overview.md)
- Data Exposed episode [What's New in Azure SQL Auditing](/Shows/Data-Exposed/Whats-New-in-Azure-SQL-Auditing)
- [Auditing for SQL Managed Instance](../managed-instance/auditing-configure.md)
- [Auditing for SQL Server](/sql/relational-databases/security/auditing/sql-server-audit-database-engine)
