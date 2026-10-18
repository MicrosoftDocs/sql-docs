---
title: SQL Server Audit
description: Learn about SQL Server Audit in Azure SQL Managed Instance.
author: sravanisaluru
ms.author: srsaluru
ms.reviewer: vanto, wiassaf
ms.date: 05/12/2026
ms.service: azure-sql-managed-instance
ms.subservice: security
ms.topic: concept-article
ms.custom:
  - sqldbrb=1
  - sfi-image-nochange
f1_keywords:
  - "mi.azure.sqlaudit.general.f1"
---

# SQL Server Audit in Azure SQL Managed Instance

[!INCLUDE [appliesto-sqlmi](../includes/appliesto-sqlmi.md)]

You can configure [SQL Server Audit](/sql/relational-databases/security/auditing/sql-server-audit-database-engine?view=azuresqldb-mi-current&preserve-view=true) in Azure SQL Managed Instance.

- Auditing helps you maintain regulatory compliance, understand database activity, and gain insight into discrepancies and anomalies that could indicate business concerns or suspected security violations.
- Auditing enables and facilitates adherence to compliance standards, although it doesn't guarantee compliance. For more information, see the [Microsoft Azure Trust Center](https://www.microsoft.com/trust-center/compliance/compliance-overview) where you can find the most current list of SQL Managed Instance compliance certifications.

To get started configuring SQL Server Audit in Azure SQL Managed Instance, see [Get started with Azure SQL Managed Instance auditing](auditing-configure.md).

## Performance optimization

The auditing of Azure SQL Managed Instance is optimized for availability and performance. During high activity, or high network load, Azure SQL Managed Instance allows operations to proceed and might not record some audited events.

## Audit Microsoft Support operations

Auditing of Microsoft Support operations for SQL Managed Instance allows you to audit Microsoft support engineers' operations when they need to access your server during a support request. The use of this capability, along with your auditing, enables more transparency into your workforce and allows for anomaly detection, trend visualization, and data loss prevention.

To enable auditing of Microsoft Support operations, navigate to **Create Audit** under **Security** > **Audit** in your SQL managed instance, and select **Microsoft support operations**.

:::image type="content" source="media/auditing-configure/support-operations.png" alt-text="Screenshot from SQL Server Management Studio showing the Microsoft support operations check box.":::

> [!NOTE]  
> You must create a separate server audit for auditing Microsoft operations. If you enable this check box for an existing audit, then it overwrites the audit and only logs support operations.

## Internal operations in Azure SQL Managed Instance

In Azure SQL Database and Azure SQL Managed Instance, events initiated by `SQLDBControlPlaneFirstPartyApp` are an internal Azure function of the [Azure SQL Database control plane](/azure/azure-resource-manager/management/control-plane-and-data-plane#control-plane). Events initiated by `SQLDBControlPlaneFirstPartyApp` are part of an internal synchronization operation between the SQL engine and Azure Resource Manager. These events are a normal part of resource management and are required for correct resource representation and operation in Azure.

## Audit differences between databases in Azure SQL Managed Instance and databases in SQL Server

The key differences between auditing in databases in Azure SQL Managed Instance and databases in SQL Server are:

- With Azure SQL Managed Instance, auditing works at the server level and stores `.xel` log files in Azure Blob storage.
- In SQL Server, audit works at the server level, but stores events in the file system and Windows event logs.

XEvent auditing in managed instances supports Azure Blob storage targets. File and Windows logs are **not supported**.

The key differences in the `CREATE AUDIT` syntax for auditing to Azure Blob storage are:

- A new syntax `TO URL` is provided and enables you to specify the URL of the Azure Blob storage container where the `.xel` files are placed.
- A new syntax `TO EXTERNAL MONITOR` is provided to enable Event Hubs and Azure Monitor log targets.
- The syntax `TO FILE` is **not supported** because Azure SQL Managed Instance can't access Windows file shares.
- Shutdown option is **not supported**.
- `queue_delay` of 0 is **not supported**.

## Permissions

To set up auditing, you need database permissions within SQL managed instance, and you also need permissions to the Azure resources that are used for storing and accessing the audit logs.

To set up SQL managed instance auditing, you need to following database permissions:

|Database permissions  |Configure audit  |View audit logs using T-SQL  |
|---------|---------|---------|
| `VIEW DATABASE SECURITY AUDIT` |No|Yes|
| `ALTER ANY DATABASE AUDIT` | Yes        | No        |
| `CONTROL DATABASE` | Yes        | Yes        |

To configure auditing to Azure storage, you need the **Storage blob data contributor** role on the storage account or higher permissions. To configure auditing to Event Hubs or Log Analytics, you need the **Monitoring Contributor** role or higher permissions on the resource group where the Event Hubs or Log Analytics workspace is provisioned.

## Automatic internal connectivity tests

Once you enable auditing, you might notice that [!INCLUDE [auto-connectivity-tests](../includes/auto-connectivity-tests.md)]

## Next step

> [!div class="nextstepaction"]
> [Get started: Create a Server Audit](/sql/relational-databases/security/auditing/create-a-server-audit-and-server-audit-specification?view=azuresqldb-mi-current&preserve-view=true)

## Related content

- [Create a server audit and database audit specification](/sql/relational-databases/security/auditing/create-a-server-audit-and-database-audit-specification?view=azuresqldb-mi-current&preserve-view=true)
- [View a SQL Server Audit Log](/sql/relational-databases/security/auditing/view-a-sql-server-audit-log?view=azuresqldb-mi-current&preserve-view=true)
- [Write SQL Server Audit events to the Security log](/sql/relational-databases/security/auditing/write-sql-server-audit-events-to-the-security-log?view=azuresqldb-mi-current&preserve-view=true)
- [Auditing differences between Azure SQL Managed Instance and a database in SQL Server](#audit-differences-between-databases-in-azure-sql-managed-instance-and-databases-in-sql-server)
