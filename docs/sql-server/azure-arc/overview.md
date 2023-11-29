---
title: Overview
description: Feature overview. Explains how you can manage instances of SQL Server enabled by Azure Arc.
author: anosov1960
ms.author: sashan
ms.reviewer: mikeray, randolphwest
ms.date: 09/26/2023
ms.topic: conceptual
---

# SQL Server enabled by Azure Arc

[!INCLUDE [sqlserver](../../includes/applies-to-version/sqlserver.md)]

[!INCLUDE [ssazurearc](../../includes/ssazurearc.md)] extends Azure services to SQL Server instances hosted outside of Azure: in your data center, in edge site locations like retail stores, or any public cloud or hosting provider.

## Manage your SQL Servers at-scale from a single point of control

Azure Arc enables you to manage all of your SQL Servers from a single point of control: Azure. As you connect your SQL Servers to Azure, you get a single place to view the detailed inventory of your SQL Servers and databases.  

- Look at details for a given SQL Server in the Azure portal such as the name, version, edition, number of cores, and host operating system.
- Query across all of your SQL Servers using Azure Resource Graph Explorer to answer questions like:
  - "How many SQL Servers do I have that are SQL Server 2014?"
  - "What are the names of all the SQL Servers that are running on Linux?"  
- Quickly create charts from these queries and pin them to customizable dashboards.
- View a list of every database on a SQL Server and do cross-SQL Server queries of databases to see:
  - Databases that haven't been backed up recently.
  - Databases that aren't encrypted.

![A screenshot of the Arc-enabled SQL Server dashboard from Azure portal.](media/overview/arc-sql-server-dashboard.png)

## Best practices assessment

You can optimize the configuration of your SQL Servers for best performance and security by running a best practices assessment. The assessment report shows you specific ways to improve your configuration. The assessment compares your configuration to best practices established by Microsoft Support through many years of real-world experience. Each suggestion includes the details on how to change the configuration.

## Microsoft Entra ID authentication

Establish a secure connection to Azure to authenticate with Microsoft Entra ID. Requires:

- [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] or later.
- [!INCLUDE [ssazurearc](../../includes/ssazurearc.md)]

[!INCLUDE [entra-id](../../includes/entra-id.md)]

## Microsoft Defender for Cloud

Microsoft Defender for Cloud helps you discover and mitigate potential database vulnerabilities and alerts you to anomalous activities. These activities might indicate threats to your databases on Arc-enabled SQL Servers.

- Vulnerability assessment: Scan databases to discover, track, and remediate vulnerabilities.
- Threat protection: Receive detailed security alerts and recommended actions based on SQL Advanced Threat Protection to provide to mitigate threats.

When you enable Microsoft Defender through [!INCLUDE [ssazurearc](../../includes/ssazurearc.md)], you can get substantial cost savings on Defender.

## Microsoft Purview

Microsoft Purview provides a unified data governance solution to help manage and govern your on-premises, multicloud, and software as a service (SaaS) data. Easily create a holistic, up-to-date map of your data landscape with automated data discovery, sensitive data classification, and end-to-end data lineage. Enable data consumers to access valuable, trustworthy data management.

[!INCLUDE [ssazurearc](../../includes/ssazurearc.md)] powers some of the Microsoft Purview features such as access policies and it generally makes it easier for you to get your SQL Servers connected into Purview.

## Pay-as-you-go for SQL Server

Now, with [!INCLUDE [ssazurearc](../../includes/ssazurearc.md)], you have the option of purchasing SQL Server using a 'pay-as-you-go' model instead of purchasing licenses. This model is a great alternative if you're looking to save costs on SQL Servers that have variable demand for compute capacity over time. For example, when you can turn off a SQL Server at night or on weekends, or even just scale down the number of cores used during less busy times. It's also a great option if you only plan to use a SQL Server for a short period of time and then won't need it anymore. Pay-as-you-go, billed through Azure, is now available for all versions of SQL Server from 2012 to 2022.

## Extended Security Updates (ESU)

Once [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] has reached the end of its support lifecycle, you can sign up for an Extended Security Update (ESU) subscription for your servers and remain protected for up to three years. When you upgrade to a newer version of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], your ESU subscription is automatically canceled. When you [migrate to Azure SQL](/azure/azure-sql/migration-guides/), the ESU charges automatically stop but you continue to have access to the ESUs.

## Architecture

The SQL Server instance that you want to enable with Azure Arc can be installed in a virtual or physical machine running Windows or Linux. The [Azure Connected Machine agent](/azure/azure-arc/servers/agent-overview) and the Azure Extension for SQL Server securely connect to Azure to establish communication channels with multiple Azure services using only outbound HTTPS traffic on TCP port 443 using Transport Layer Security (TLS). The Azure Connected Machine agent can communicate through a configurable HTTPS proxy server over Azure Express Route, Azure Private Link or over the Internet. Review the [overview](/azure/azure-arc/servers/agent-overview), [network requirements](/azure/azure-arc/servers/network-requirements), and [prerequisites](/azure/azure-arc/servers/prerequisites) for the Azure Connected Machine agent.

Some of the services provided by [!INCLUDE [ssazurearc](../../includes/ssazurearc.md)], such as Microsoft Defender for Cloud and best practices assessment, require the Azure Monitoring agent (AMA) extension to be installed and connected to an Azure Log Analytics workspace for data collection and reporting.

The following diagram illustrates the architecture of [!INCLUDE [ssazurearc](../../includes/ssazurearc.md)].

![Diagram of the architecture for [!INCLUDE [ssazurearc](../../includes/ssazurearc.md)].](media/overview/architecture.png)

## Feature availability depending on license type

[!INCLUDE [license-types](includes/license-types.md)]

## Feature availability by operating system

[!INCLUDE [features-operating-systems](includes/features-operating-system.md)]

## Feature availability by version

[!INCLUDE [features-version](includes/features-version.md)]

## Feature availability by edition

[!INCLUDE [features-edition](includes/features-edition.md)]

## Supported SQL Server versions and operating systems

[!INCLUDE [supported-configurations](includes/supported-configurations.md)]

## Unsupported configurations

[!INCLUDE [unsupported-configurations](includes/unsupported-configurations.md)]

## Supported Azure regions

[!INCLUDE [azure-arc-data-regions](includes/azure-arc-data-regions.md)]

## Related content

- [Learn about the prerequisites to connect your SQL Server to Azure Arc](prerequisites.md)
- [Automatically connect your SQL Server to Azure Arc](automatically-connect.md)
- [Learn more about Microsoft Defender for Cloud](/azure/defender-for-cloud/defender-for-sql-usage)
- [Lean more about Microsoft Purview](/azure/purview/register-scan-azure-arc-enabled-sql-server)
