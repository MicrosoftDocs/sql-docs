---
title: Auditing
titleSuffix: Azure SQL Database and Azure Synapse Analytics
description: SQL Auditing for Azure SQL Database and Azure Synapse Analytics tracks database events and writes them to an audit log in your Azure storage account, Log Analytics workspace, or Event Hubs.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: peskount, srsaluru, vanto, mathoma
ms.date: 04/15/2026
ms.service: azure-sql-database
ms.subservice: security
ms.topic: concept-article
monikerRange: "=azuresql || =azuresql-db "
ms.custom:
  - azure-synapse
  - sqldbrb=1
---
# Auditing for Azure SQL Database and Azure Synapse Analytics

[!INCLUDE [appliesto-sqldb-asa](../includes/appliesto-sqldb-asa.md)]

Auditing for [Azure SQL Database](sql-database-paas-overview.md) and [Azure Synapse Analytics](/azure/synapse-analytics/sql-data-warehouse/sql-data-warehouse-overview-what-is) tracks database events and writes them to an audit log in your Azure storage account, Log Analytics workspace, or Event Hubs.

Auditing also:

- Helps you maintain regulatory compliance, understand database activity, and gain insight into discrepancies and anomalies that could indicate business concerns or suspected security violations.

- Enables and facilitates adherence to compliance standards, although it doesn't guarantee compliance. For more information, see the [Microsoft Azure Trust Center](https://www.microsoft.com/trust-center/compliance/compliance-overview) where you can find the most current list of SQL Database compliance certifications.

> [!NOTE]  
> For information on Azure SQL Managed Instance auditing, see [Get started with Azure SQL Managed Instance auditing](../managed-instance/auditing-configure.md).

## Overview

You can use SQL Database auditing to:

- **Retain** an audit trail of selected events. You can define categories of database actions to be audited.
- **Report** on database activity. You can use preconfigured reports and a dashboard to get started quickly with activity and event reporting.
- **Analyze** reports. You can find suspicious events, unusual activity, and trends.

> [!IMPORTANT]  
> Auditing for Azure SQL Database, Azure Synapse Analytics SQL pools, and Azure SQL Managed Instance is optimized for availability and performance of the database or instance being audited. During periods of very high activity or high network load, the auditing feature might allow transactions to proceed without recording all of the events marked for auditing.

## Enhancements to performance, availability, and reliability in server auditing for Azure SQL Database (July 2025 GA)

- Re-architected major portions of SQL Auditing resulting in increased availability and reliability of server audits. As an added benefit, there's closer feature alignment with SQL Server and Azure SQL Managed Instance. Database auditing remains unchanged.
- The previous design of auditing triggers a database level audit and executes one audit session for each database in the server. The new architecture of auditing creates one extended event session at the server level that captures audit events for all databases.
- The new auditing design optimizes memory and CPU, and is consistent with how auditing works in SQL Server and Azure SQL Managed Instance.

### Changes from the re-architecture of server auditing

- Folder structure change for storage account:
  - One of the primary changes involves a folder structure change for audit logs stored in storage account containers. Previously, server audit logs were written to separate folders; one for each database, with the database name serving as the folder name. With the new update, all server audit logs are consolidated into a single folder labeled `master`. This behavior is the same as Azure SQL Managed Instance and SQL Server.
- Folder structure change for read-only replicas:
  - Read-only database replicas previously had their logs stored in a read-only folder. Those logs are now written into the `master` folder. You can retrieve these logs by filtering on the new column `is_secondary_replica_true`.
- Permissions required to view Audit logs:
  - `VIEW DATABASE SECURITY AUDIT` permission in user database

### Recommended auditing approach for large OLTP workloads

For environments with many databases running heavy OLTP workloads, using server-level auditing with default settings can lead to very large audit volumes across the logical server. Since all events from all databases are written into the same audit folder, querying audit logs for a single database becomes slow and operationally expensive. To improve performance and reduce noise:

   - **Switch to database-level auditing**. Each database writes to its own audit log folder, reducing the total volume scanned and making retrieval faster.
   - **Review the audit configuration**. Determine whether capturing all batch-completed events is necessary, or if a custom filtered configuration can meet your security and compliance requirements.

## Auditing limitations

- Enabling auditing on a paused **Azure Synapse SQL pool** isn't supported. To enable auditing, resume the **Synapse SQL pool**.
- Enabling auditing by using User Assigned Managed Identity (UAMI) isn't supported on **Azure Synapse**.
- Currently, managed identities aren't supported for Azure Synapse, unless the storage account is behind a virtual network or firewall.

> [!NOTE]
> For Azure Synapse Analytics, auditing to a storage account behind a VNet requires the server's **system-assigned managed identity** with the **Storage Blob Data Contributor** role. User-assigned managed identities (UAMI) aren't supported for Synapse auditing. If you need to audit to a storage account that uses Microsoft Entra-only authentication, configure the system-assigned managed identity on the server and grant it the Storage Blob Data Contributor role on the target storage account. For more information, see [Write audit to a storage account behind VNet and firewall](audit-write-storage-account-behind-vnet-firewall.md).
- Due to performance constraints, we don't audit the `tempdb` and **temporary tables**. While the batch completed action group captures statements against temporary tables, it might not correctly populate the object names. However, the source table is always audited, ensuring that all inserts from the source table to temporary tables are recorded.
- Auditing for **Azure Synapse SQL pools** supports default audit action groups **only**.
- When you configure auditing for a [logical server in Azure](logical-servers.md) or Azure SQL Database with the log destination as a storage account, the authentication mode must match the configuration for that storage account. If using storage access keys as the authentication type, the target storage account must be enabled with access to the storage account keys. If the storage account is configured to only use authentication with Microsoft Entra ID ([formerly Azure Active Directory](/entra/fundamentals/new-name)), auditing can be configured to use managed identities for authentication.

- Auditing isn't supported on databases with names that contain the `?` character. This applies to both **server-level** and **database-level** auditing, as databases with `?` in their names are *no longer supported on Azure*.

- **Azure SQL Database and Azure Synapse** audit logs capture up to **4,000 characters** in the `statement` and `data_sensitivity_information` fields. If the output from an auditable action exceeds this limit, any content beyond the first 4,000 characters is **truncated** and **excluded from the audit record**.

## Remarks

- Events initiated by `SQLDBControlPlaneFirstPartyApp` in the Activity log are an internal Azure function of the [Azure SQL Database control plane](/azure/azure-resource-manager/management/control-plane-and-data-plane#control-plane). Events initiated by `SQLDBControlPlaneFirstPartyApp` are part of an internal synchronization operation between the SQL engine and Azure Resource Manager. These events are a normal part of Azure SQL Database management and are required for correct resource representation and operation in Azure.
- **Premium storage** with **BlockBlobStorage** is supported. Standard storage is supported. However, for audit to write to a storage account behind a virtual network or firewall, you must have a **general-purpose v2 storage account**. If you have a general-purpose v1 or Blob Storage account, [upgrade to a general-purpose v2 storage account](/azure/storage/common/storage-account-upgrade). For specific instructions see, [Write audit to a storage account behind VNet and firewall](audit-write-storage-account-behind-vnet-firewall.md). For more information, see [Types of storage accounts](/azure/storage/common/storage-account-overview#types-of-storage-accounts).
- When customers enable SQL auditing and also configure **outbound networking** restrictions, they must allow list the fully qualified domain names of their auditing storage account to ensure audit events can successfully reach the destination. If the storage endpoint isn't allowlisted, audit traffic is blocked, resulting in audit event loss. After adding the required storage account FQDNs to the allow list, customers must **re-save** their auditing configuration to resume normal audit event flow.
- **Hierarchical namespace** for all types of **standard storage account** and **premium storage account with BlockBlobStorage** is supported.
- Audit logs are written to **Append Blobs** in an Azure Blob Storage on your Azure subscription
- Audit logs are in .xel format and can be opened with [SQL Server Management Studio (SSMS)](/ssms/sql-server-management-studio-ssms).
- To configure an immutable log store for the server or database-level audit events, follow the [instructions provided by Azure Storage](/azure/storage/blobs/immutable-time-based-retention-policy-overview#allow-protected-append-blobs-writes). When configuring immutable blob storage for auditing, ensure that **Allow protected append writes** is set to either **Append blobs** or **Block and append blobs**. The **None** option isn't supported. For time-based retention policies, the storage account's retention interval must be shorter than the SQL Auditing retention setting. Configurations where the storage policy is set, but SQL Auditing retention is `0`, aren't supported.
- You can write audit logs to an Azure Storage account behind a virtual network or firewall.
- For details about the log format, hierarchy of the storage folder, and naming conventions, see the article, [SQL Database audit log format](audit-log-format.md).
- Auditing on [Use read-only replicas to offload read-only query workloads](read-scale-out.md) is automatically enabled. For more information about the hierarchy of the storage folders, naming conventions, and log format, see the article, [SQL Database audit log format](audit-log-format.md).
- When using Microsoft Entra authentication, failed logins records *don't* appear in the SQL audit log. To view failed login audit records, you need to visit the [Microsoft Entra admin center](https://entra.microsoft.com), which logs details of these events.
- Logins are routed by the gateway to the specific instance where the database is located. With Microsoft Entra logins, the credentials are verified before attempting to use that user to sign into the requested database. In the case of failure, the requested database is never accessed, so no auditing occurs. With SQL logins, the credentials are verified on the requested data, so in this case they can be audited. Successful logins, which obviously reach the database, are audited in both cases.
- After you've configured your auditing settings, you can turn on the new threat detection feature and configure emails to receive security alerts. When you use threat detection, you receive proactive alerts on anomalous database activities that can indicate potential security threats. For more information, see [SQL Advanced Threat Protection](threat-detection-overview.md).
- After a database with auditing enabled is copied to another [logical server](logical-servers.md), you might receive an email notifying you that the audit failed. This is a known issue and auditing should work as expected on the newly copied database.

## Related content

- [What's New in Azure SQL Auditing](/shows/data-exposed/server-audit-redesign-for-azure-sql-database-data-exposed)
- [Get started with Azure SQL Managed Instance auditing](../managed-instance/auditing-configure.md)
- [Auditing for SQL Server](/sql/relational-databases/security/auditing/sql-server-audit-database-engine)
- [Set up Auditing for Azure SQL Database and Azure Synapse Analytics](auditing-setup.md)
- [Modifiable configuration reference for Azure SQL Database](modifiable-configuration-reference.md)