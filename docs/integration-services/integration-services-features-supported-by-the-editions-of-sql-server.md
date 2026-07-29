---
title: "Integration Services Features Supported by the Editions of SQL Server"
description: "Integration Services Features Supported by the Editions of SQL Server"
ms.reviewer: randolphwest
ms.date: 11/27/2025
ms.service: sql
ms.subservice: integration-services
ms.topic: concept-article
---
# Integration Services features supported by the editions of SQL Server

[!INCLUDE [sqlserver-ssis](../includes/applies-to-version/sqlserver-ssis.md)]

This article provides details about the features of SQL Server Integration Services (SSIS) supported by the different editions of [!INCLUDE [ssNoVersion_md](../includes/ssnoversion-md.md)].

For features supported by Evaluation and Developer editions, see features listed for Enterprise edition in the following tables.

For the latest release notes and what's new information, see the following articles:

- [SQL Server 2016 release notes](../sql-server/sql-server-2016-release-notes.md)
- [What's New in Integration Services in SQL Server 2016](what-s-new-in-integration-services-in-sql-server-2016.md)
- [What's new in Integration Services in SQL Server 2017](what-s-new-in-integration-services-in-sql-server-2017.md)
- [What's New in SQL Server 2025 Integration Services](what-s-new-in-integration-services-in-sql-server-2025.md)

**Try SQL Server 2025**

The SQL Server Evaluation edition is available for a 180-day trial period.

:::image type="icon" source="../includes/media/download.svg" border="false"::: **[Download SQL Server 2025 from the Evaluation Center](https://www.microsoft.com/evalcenter/evaluate-sql-server-2025)**

<a id="ISNew"></a>

## New Integration Services features in SQL Server 2025

ADO.NET connection manager now supports Microsoft SqlClient Data Provider. For more information, see [ADO.NET connection manager](connection-manager/ado-net-connection-manager.md).

## New Integration Services features in SQL Server 2017

| Feature | Enterprise | Standard | Web | Express with Advanced Services | Express |
| --- | --- | --- | --- | --- | --- |
| Scale Out Master | Yes | No | No | No | No |
| Scale Out Worker | Yes | Yes <sup>1</sup> | No | No | No |
| Support for Microsoft Dynamics AX and Microsoft Dynamics CRM in OData components <sup>2</sup> | Yes | Yes | No | No | No |
| Linux support | Yes | Yes | No | No | Yes |

<sup>1</sup> If you run packages that require Enterprise-only features in Scale Out, the Scale Out Workers must also run on instances of SQL Server Enterprise.

<sup>2</sup> This feature is also supported in SQL Server 2016 with Service Pack 1.

<a id="IEWiz"></a>

## SQL Server Import and Export Wizard

| Feature | Enterprise | Standard | Web | Express with Advanced Services | Express |
| --- | --- | --- | --- | --- | --- |
| SQL Server Import and Export Wizard | Yes | Yes | Yes | Yes <sup>1</sup> | Yes <sup>1</sup> |

<sup>1</sup> DTSWizard.exe isn't provided with SQL on Linux. However, dtexec on Linux can be used to execute a package created by DTSWizard on Windows.

<a id="IS"></a>

## Integration Services

| Feature | Enterprise | Standard | Web | Express with Advanced Services | Express |
| --- | --- | --- | --- | --- | --- |
| Built-in data source connectors | Yes | Yes | No | No | No |
| Built in tasks and transformations | Yes | Yes | No | No | No |
| ODBC source and destination | Yes | Yes | No | No | No |
| Azure data source connectors and tasks | Yes | Yes | No | No | No |
| Hadoop/HDFS connectors and tasks | Yes | Yes | No | No | No |
| Basic data profiling tools | Yes | Yes | No | No | No |

<a id="ISAA"></a>

## Integration Services - Advanced sources and destinations

| Feature | Enterprise | Standard | Web | Express with Advanced Services | Express |
| --- | --- | --- | --- | --- | --- |
| High-performance Oracle source and destination by Attunity | Yes | No | No | No | No |
| High-performance Teradata source and destination by Attunity | Yes | No | No | No | No |
| SAP BW source and destination | Yes | No | No | No | No |
| Data mining model training destination | Yes | No | No | No | No |
| Dimension processing destination | Yes | No | No | No | No |
| Partition processing destination | Yes | No | No | No | No |

<a id="ISAT"></a>

## Integration Services - Advanced tasks and transformations

| Feature | Enterprise | Standard | Web | Express with Advanced Services | Express |
| --- | --- | --- | --- | --- | --- |
| Change Data Capture components by Attunity <sup>1</sup> | Yes | No | No | No | No |
| Data mining query transformation | Yes | No | No | No | No |
| Fuzzy grouping and fuzzy lookup transformations | Yes | No | No | No | No |
| Term extraction and term lookup transformations | Yes | No | No | No | No |

<sup>1</sup> The Change Data Capture components by Attunity require Enterprise edition. The Change Data Capture Service and the Change Data Capture Designer, however, don't require Enterprise edition. You can use the Designer and the Service on a computer where SSIS isn't installed.
