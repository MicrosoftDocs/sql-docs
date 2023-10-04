---
title: What's new? (Archive)
titleSuffix: Azure SQL Database
description: Learn about the features and documentation improvements for Azure SQL Database made in previous years. (Archive)
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: mathoma
ms.date: 02/27/2023
ms.service: sql-database
ms.subservice: service-overview
ms.topic: whats-new
ms.custom:
  - sqldbrb=2
  - references_regions
---
# What's new in Azure SQL Database? (Archive)

[!INCLUDE[appliesto-sqldb](../includes/appliesto-sqldb.md)]

> [!div class="op_single_selector"]
> * [Azure SQL Database](doc-changes-updates-release-notes-whats-new-archive.md?view=azuresql-db&preserve-view=true)
> * [Azure SQL Managed Instance](../managed-instance/doc-changes-updates-release-notes-whats-new-archive.md?view=azuresql-mi&preserve-view=true)

This article summarizes older documentation changes associated with new features and improvements in the releases of [Azure SQL Database](https://azure.microsoft.com/products/azure-sql/database/). To learn more about Azure SQL Database, see [What is Azure SQL Database?](sql-database-paas-overview.md)

Return to [What's new in Azure SQL Database?](doc-changes-updates-release-notes-whats-new.md)

## 2021

| Changes | Details |
| --- | --- |
| **Azure AD-only authentication** | Restricting authentication to your Azure SQL Database only to Azure Active Directory users is now generally available. To learn more, see [Azure AD-only authentication](../database/authentication-azure-ad-only-authentication.md). |
|**Split what's new** | The previously combined **What's new** article has been split by product - [What's new in SQL Database](doc-changes-updates-release-notes-whats-new.md) and [What's new in SQL Managed Instance](../managed-instance/doc-changes-updates-release-notes-whats-new.md), making it easier to identify what features are currently in preview, generally available, and significant documentation changes. Additionally, the [Known Issues in SQL Managed Instance](../managed-instance/doc-changes-updates-known-issues.md) content has moved to its own page.  |
| **Maintenance Window support for availability zones** | You can now use the [Maintenance Window feature](maintenance-window.md) if your Azure SQL Database is deployed to an availability zone. This feature is currently in preview.  |
| **Azure AD-only authentication** | It's now possible to restrict authentication to your Azure SQL Database to Azure Active Directory users only. This feature is currently in preview. To learn more, see [Azure AD-only authentication](authentication-azure-ad-only-authentication.md). |
| **Query store hints** | It's now possible to use query hints to optimize your query execution via the OPTION clause. To learn more, see [Query store hints](/sql/relational-databases/performance/query-store-hints?view=azuresqldb-current&preserve-view=true). |
| **Change data capture** | Using change data capture (CDC) with Azure SQL Database is now in preview. To learn more, see [Change data capture](/sql/relational-databases/track-changes/about-change-data-capture-sql-server). |
| **SQL Database ledger** | SQL Database ledger is in preview, and introduces the ability to cryptographically attest to other parties, such as auditors or other business parties, that your data hasn't been tampered with. To learn more, see [Ledger](/sql/relational-databases/security/ledger/ledger-overview). |
| **Maintenance window** | The maintenance window feature allows you to configure a maintenance schedule for your Azure SQL Database, currently in preview. To learn more, see [maintenance window](maintenance-window.md).|
| **SQL Insights** | SQL Insights is a comprehensive solution for monitoring any product in the Azure SQL family. The SQL Insights solution uses dynamic management views to expose the data you need to monitor health, diagnose problems, and tune performance. To learn more, see [SQL Insights](/azure/azure-monitor/insights/sql-insights-overview). |


## Contribute to content

To contribute to the Azure SQL documentation, see the [Docs contributor guide](/contribute/).
