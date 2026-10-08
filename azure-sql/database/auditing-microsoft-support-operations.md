---
title: Auditing Microsoft Support Operations
titleSuffix: Azure SQL Database
description: How to use Auditing to audit Microsoft support operations.
author: sravanisaluru
ms.author: srsaluru
ms.reviewer: vanto
ms.date: 10/05/2026
ms.service: azure-sql-database
ms.subservice: security
ms.topic: concept-article
monikerRange: "=azuresql || =azuresql-db"
---
# Auditing Microsoft support operations

[!INCLUDE[appliesto-sqldb](../includes/appliesto-sqldb.md)]

Auditing of Microsoft support operations for your [logical server](logical-servers.md) in Azure SQL Database allows you to audit Microsoft support engineers' operations when they need to access your server during a support request. The use of this capability, along with your auditing, enables more transparency into your workforce and allows for anomaly detection, trend visualization, and data loss prevention.

Auditing of Microsoft support operations includes the following set of action groups, which audit all queries executed against the database, as well as successful and failed logins by Microsoft support engineers:

- BATCH_COMPLETED_GROUP
- SUCCESSFUL_DATABASE_AUTHENTICATION_GROUP
- FAILED_DATABASE_AUTHENTICATION_GROUP

## Enable auditing

To enable auditing of Microsoft support operations, Go to the [Azure portal](https://portal.azure.com). Navigate to **Auditing** under the **Security** heading in your Azure **SQL server** pane, and switch **Enable Auditing of Microsoft support operations** to **ON**. Configure audit logs to be sent to one or more of the following destinations: Storage Account, Log Analytics, or Event Hubs

:::image type="content" source="media/auditing-microsoft-support-operations/auditing-support-operations.png" alt-text="Screenshot of the Azure portal showing the Auditing page with Enable Auditing of Microsoft support operations toggle highlighted.":::

To review the audit logs of Microsoft support operations in your Log Analytics workspace, use the following query:

```kusto
AzureDiagnostics
| where Category == "DevOpsOperationsAudit"
```

You have the option of choosing a different storage destination for this auditing log, or use the same auditing configuration for your server.

> [!NOTE]
> DevOps audit logs stored in Azure Storage may contain sensitive operational details. If a malicious actor within your environment accesses these logs, they could gain insights into system operations, which may lead to unauthorized access or data breaches.
>
> **Customer responsibility -** Secure these logs by:
> - Restricting access to authorized personnel only
> - Applying strong Azure role-based access control (RBAC) and network controls
> - Monitoring and auditing storage access regularly

## Auditing in Azure Synapse Analytics

For information about auditing Microsoft support operations in Azure Synapse Analytics, see [Auditing Microsoft support operations](/azure/synapse-analytics/sql/auditing-microsoft-support-operations).

## Related content

- [Auditing for Azure SQL Database](auditing-overview.md)
- [What's New in Azure SQL Auditing](/Shows/Data-Exposed/Whats-New-in-Azure-SQL-Auditing)
- [Get started with Azure SQL Managed Instance auditing](../managed-instance/auditing-configure.md)
- [Auditing for SQL Server](/sql/relational-databases/security/auditing/sql-server-audit-database-engine)
