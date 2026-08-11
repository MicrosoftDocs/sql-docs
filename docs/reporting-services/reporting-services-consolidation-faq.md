---
title: "Reporting Services Consolidation FAQ"
description: Frequently asked questions about the Reporting Services consolidation in SQL Server 2025.
ms.reviewer: randolphwest
ms.date: 06/16/2025
ms.service: reporting-services
ms.subservice: reporting-services
ms.topic: faq
ms.custom:
  - ignite-2025
---

# Reporting Services consolidation FAQ

Starting with SQL Server 2025, Microsoft is consolidating all on-premises reporting services under Power BI Report Server (PBIRS). No new versions of SQL Server Reporting Services (SSRS) will be released. PBIRS becomes the default on-premises reporting solution for [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)].

Power BI Report Server is available for customers running SQL Server 2025 Enterprise and Standard editions. For SQL Server Enterprise edition in SQL Server 2022 and earlier versions, Power BI Report Server usage rights apply only to Enterprise edition core licenses with active Software Assurance (SA). This right expires upon expiration of the customer's SA coverage. For more information about PBIRS licensing, see [What is Power BI Report Server?](/power-bi/report-server/)

This article outlines the implications of these changes for you as a customer, and addresses any questions you might have.

## General questions

### What is SQL Server Reporting Services (SSRS)?

SSRS is a long-standing Microsoft reporting platform that enables the creation, deployment, and management of paginated reports using [Report Definition Language (RDL)](reports/report-definition-language-ssrs.md). It was bundled with [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] since 2004, and is widely used for operational reporting.

### What is Power BI Report Server (PBIRS)?

PBIRS is a modern on-premises reporting solution that supports both paginated (RDL) and interactive (PBIX) reports. It offers a bridge to the cloud with compatibility for Power BI, and is governed by the Modern Lifecycle Policy. For more information about PBIRS, see [What is Power BI Report Server?](/power-bi/report-server/)

You can install PBIRS through [Install Power BI Report Server](/power-bi/report-server/install-report-server).

### Why are we making this change?

SSRS represents a subset of PBIRS, offering only RDL capabilities. By transitioning to PBIRS, customers can access advanced functionalities, including rich and interactive PBIX reports, which aren't available in SSRS. Adopting PBIRS enables customers to seamlessly move their reporting capabilities to the cloud when they're ready.

### How is PBIRS different and similar to SSRS?

PBIRS is a superset of SSRS, offering all SSRS capabilities plus support for interactive Power BI reports, and more.

| Feature | SQL Server Reporting Services (SSRS) | Power BI Report Server |
| --- | --- | --- |
| Report authoring in RDL | Yes | Yes |
| Row-level security | Yes | Yes |
| Paginated reports | Yes | Yes |
| PBIX reports | No | Yes |
| Data modeling | No | Yes |
| Visual interactivity | No | Yes |
| Custom visuals | No | Yes |
| Lifecycle | Fixed | [Modern](/lifecycle/policies/modern) |

## Availability and purchase

### What editions of SQL Server 2025 include PBIRS?

Power BI Report Server is available for customers running SQL Server 2025 Enterprise and Standard editions. For SQL Server Enterprise edition in SQL Server 2022 and earlier versions, Power BI Report Server usage rights apply only to Enterprise edition core licenses with active Software Assurance (SA). This right expires upon expiration of the customer's SA coverage. For more information about PBIRS licensing, see [What is Power BI Report Server?](/power-bi/report-server/) Customers can also explore PBIRS by installing one of the [free Developer or Evaluation](/power-bi/report-server/install-report-server) editions.

### Is there an SSRS equivalent for non-SA customers going forward?

Power BI Report Server is available for customers running SQL Server 2025 Enterprise and Standard editions. For SQL Server Enterprise edition in SQL Server 2022 and earlier versions, Power BI Report Server usage rights apply only to Enterprise edition core licenses with active Software Assurance (SA). This right expires upon expiration of the customer's SA coverage. For more information about PBIRS licensing, see [What is Power BI Report Server?](/power-bi/report-server/)

### How long can customers use their existing SSRS installations?

SQL Server Reporting Services (SSRS) 2022 will continue to receive security updates and support through **January 11, 2033**. However, no new versions will be released beyond SSRS 2022. Customers can also continue to use SSRS 2022 with later versions of the SQL Server Database Engine to host the reporting catalog.

### How can customers purchase PBIRS keys with SQL Server starting in 2025?

Customers who purchase a qualifying SQL Server 2025 license can use the same SQL Server 2025 key to install and use PBIRS.

### If customers already have a SQL Server license and key, can they use it to install PBIRS?

PBIRS installation requires keys from SQL Server 2025 and later versions. For [!INCLUDE [sssql22-md](../includes/sssql22-md.md)] and previous versions, access to PBIRS is limited to customers with SQL Server Enterprise edition and Software Assurance (SA), who can use a PBIRS key provided by Microsoft.

## Migration

### What do customers using SSRS need to do?

To take advantage of this change, customers can explore PBIRS by installing one of the [free Developer or Evaluation](/power-bi/report-server/install-report-server) editions, and planning their migration to PBIRS. Existing RDL reports are fully compatible with PBIRS, and customers can use the [SSRS to PBIRS migration guide](/power-bi/report-server/migrate-report-server) to support the transition. SSRS 2022 is supported until **January 11, 2033**, in alignment with the [!INCLUDE [sssql22-md](../includes/sssql22-md.md)] support lifecycle. Customers can also continue to use SSRS 2022 with later versions of the SQL Server Database Engine to host the reporting catalog.

### What is the migration path for customers moving from SSRS to PBIRS?

Microsoft offers a detailed migration guide to help customers move from SSRS to PBIRS. For more information, see [Migrate a report server installation](/power-bi/report-server/migrate-report-server).

### What is the migration path for customers moving from SSRS/PBIRS to the cloud?

Customers can migrate to the Power BI service using available tools and guidance. While some custom implementations might require manual migration or consulting support, most standard RDL workloads can be transitioned smoothly by following the [SSRS to PBIRS migration guide](/power-bi/guidance/migrate-ssrs-reports-to-power-bi).

### How long is SSRS 2022 supported?

SSRS 2022 is supported until **January 11, 2033**, in alignment with the [!INCLUDE [sssql22-md](../includes/sssql22-md.md)] support lifecycle.
