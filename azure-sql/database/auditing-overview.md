---
title: Auditing
titleSuffix: Azure SQL Database and Azure Synapse Analytics
description: SQL Auditing for Azure SQL Database and Azure Synapse Analytics tracks database events and writes them to an audit log in your Azure storage account, Log Analytics workspace, or Event Hubs.
author: sravanisaluru
ms.author: srsaluru
ms.reviewer: wiassaf, vanto, mathoma
ms.date: 05/31/2023
ms.service: sql-database
ms.subservice: security
ms.topic: conceptual
ms.custom:
  - azure-synapse
  - sqldbrb=1
---
# Auditing for Azure SQL Database and Azure Synapse Analytics

[!INCLUDE[appliesto-sqldb-asa](../includes/appliesto-sqldb-asa.md)]

Auditing for [Azure SQL Database](sql-database-paas-overview.md) and [Azure Synapse Analytics](/azure/synapse-analytics/sql-data-warehouse/sql-data-warehouse-overview-what-is) tracks database events and writes them to an audit log in your Azure storage account, Log Analytics workspace, or Event Hubs.

Auditing also:

- Helps you maintain regulatory compliance, understand database activity, and gain insight into discrepancies and anomalies that could indicate business concerns or suspected security violations.

- Enables and facilitates adherence to compliance standards, although it doesn't guarantee compliance. For more information, see the [Microsoft Azure Trust Center](https://www.microsoft.com/trust-center/compliance/compliance-overview) where you can find the most current list of SQL Database compliance certifications.

> [!NOTE]  
> For information on Azure SQL Managed Instance auditing, see [Get started with SQL Managed Instance auditing](../managed-instance/auditing-configure.md).

## Overview

You can use SQL Database auditing to:

- **Retain** an audit trail of selected events. You can define categories of database actions to be audited.
- **Report** on database activity. You can use preconfigured reports and a dashboard to get started quickly with activity and event reporting.
- **Analyze** reports. You can find suspicious events, unusual activity, and trends.

> [!IMPORTANT]  
> Auditing for Azure SQL Database, Azure Synapse Analytics SQL pools, and Azure SQL Managed Instance is optimized for availability and performance of the database or instance being audited. During periods of very high activity or high network load, the auditing feature may allow transactions to proceed without recording all of the events marked for auditing.

## Auditing limitations

- Enabling auditing on a paused **Azure Synapse SQL pool** isn't supported. To enable auditing, resume the **Synapse SQL pool**.
- Enabling auditing by using User Assigned Managed Identity (UAMI) isn't supported on **Azure Synapse**.
- Currently, managed identities are not supported for Azure Synapse, unless the storage account is behind a virtual network or firewall.
- Auditing for **Azure Synapse SQL pools** supports default audit action groups **only**.
- When you configure auditing for a [logical server in Azure](logical-servers.md) or Azure SQL Database with the log destination as a storage account, the authentication mode must match the configuration for that storage account. If using storage access keys as the authentication type, the target storage account must be enabled with access to the storage account keys. If the storage account is configured to only use authentication with Microsoft Entra ID ([formerly Azure Active Directory](/entra/fundamentals/new-name)), auditing can be configured to use managed identities for authentication.

## Remarks

- **Premium storage** with **BlockBlobStorage** is supported. Standard storage is supported. However, for audit to write to a storage account behind a VNet or firewall, you must have a **general-purpose v2 storage account**. If you have a general-purpose v1 or Blob Storage account, [upgrade to a general-purpose v2 storage account](/azure/storage/common/storage-account-upgrade). For specific instructions see, [Write audit to a storage account behind VNet and firewall](audit-write-storage-account-behind-vnet-firewall.md). For more information, see [Types of storage accounts](/azure/storage/common/storage-account-overview#types-of-storage-accounts).
- **Hierarchical namespace** for all types of **standard storage account** and **premium storage account with BlockBlobStorage** is supported.
- Audit logs are written to **Append Blobs** in an Azure Blob Storage on your Azure subscription
- Audit logs are in .xel format and can be opened with [SQL Server Management Studio (SSMS)](/sql/ssms/download-sql-server-management-studio-ssms).
- To configure an immutable log store for the server or database-level audit events, follow the [instructions provided by Azure Storage](/azure/storage/blobs/immutable-time-based-retention-policy-overview#allow-protected-append-blobs-writes). Make sure you have selected **Allow additional appends** when you configure the immutable blob storage.
- You can write audit logs to an Azure Storage account behind a VNet or firewall.
- For details about the log format, hierarchy of the storage folder, and naming conventions, see the [Blob Audit Log Format Reference](./audit-log-format.md).
- Auditing on [Read-Only Replicas](read-scale-out.md) is automatically enabled. For more information about the hierarchy of the storage folders, naming conventions, and log format, see the [SQL Database Audit Log Format](audit-log-format.md).
- When using Microsoft Entra authentication, failed logins records *don't* appear in the SQL audit log. To view failed login audit records, you need to visit the [Microsoft Entra admin center](https://entra.microsoft.com), which logs details of these events.
- Logins are routed by the gateway to the specific instance where the database is located. With Microsoft Entra logins, the credentials are verified before attempting to use that user to sign into the requested database. In the case of failure, the requested database is never accessed, so no auditing occurs. With SQL logins, the credentials are verified on the requested data, so in this case they can be audited. Successful logins, which obviously reach the database, are audited in both cases.
- After you've configured your auditing settings, you can turn on the new threat detection feature and configure emails to receive security alerts. When you use threat detection, you receive proactive alerts on anomalous database activities that can indicate potential security threats. For more information, see [Getting started with threat detection](threat-detection-overview.md).
- After a database with auditing enabled is copied to another [logical server](logical-servers.md), you may receive an email notifying you that the audit failed. This is a known issue and auditing should work as expected on the newly copied database.

## Next steps

> [!div class="nextstepaction"]
> [Setting up Auditing for Azure SQL Database and Azure Synapse Analytics](auditing-setup.md)

## See also

- Data Exposed episode [What's New in Azure SQL Auditing](/Shows/Data-Exposed/Whats-New-in-Azure-SQL-Auditing)
- [Auditing for SQL Managed Instance](../managed-instance/auditing-configure.md)
- [Auditing for SQL Server](/sql/relational-databases/security/auditing/sql-server-audit-database-engine)
