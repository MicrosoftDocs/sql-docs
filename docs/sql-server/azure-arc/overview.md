---
title: Overview
description: Feature overview. Explains how you can manage instances of SQL Server enabled by Azure Arc.
author: anosov1960
ms.author: sashan
ms.reviewer: mikeray, randolphwest
ms.date: 02/16/2024
ms.topic: conceptual
---

# SQL Server enabled by Azure Arc

[!INCLUDE [sqlserver](../../includes/applies-to-version/sqlserver.md)]

[!INCLUDE [ssazurearc](../../includes/ssazurearc.md)] extends Azure services to SQL Server instances hosted outside of Azure: in your data center, in edge site locations like retail stores, or any public cloud or hosting provider.

Managing SQL Server through Azure Arc can also be configured for SQL Server VMs in Azure VMware Solution. See [Deploy Arc-enabled Azure VMware Solution](/azure/azure-vmware/deploy-arc-for-azure-vmware-solution).

## Manage your SQL Server instances at scale from a single point of control

Azure Arc enables you to manage all of your SQL Server instances from a single point of control: Azure. As you connect your SQL Server instances to Azure, you get a single place to view the detailed inventory of your SQL Server instances and databases.  

- Look at details for a given SQL Server in the Azure portal such as the name, version, edition, number of cores, and host operating system.
- Query across all of your SQL Server instances using Azure Resource Graph Explorer to answer questions like:
  - "How many SQL Server instances do I have that are SQL Server 2014?"
  - "What are the names of all the SQL Server instances that are running on Linux?"  
- Quickly create charts from these queries and pin them to customizable dashboards.
- View a list of every database on a SQL Server and do cross-SQL Server queries of databases to see:
  - Databases that haven't been backed up recently.
  - Databases that aren't encrypted.

## Example custom dashboard

Review an example of a custom dashboard in [GitHub microsoft/sql-server-samples](https://github.com/microsoft/sql-server-samples/blob/master/samples/features/azure-arc/dashboard/README.md).

:::image type="content" source="media/overview/arc-sql-server-dashboard.png" alt-text="A screenshot of a custom dashboard in the Azure portal." lightbox="media/overview/arc-sql-server-dashboard.png":::

## Best practices assessment

You can optimize the configuration of your SQL Server instances for best performance and security by running a best practices assessment. The assessment report shows you specific ways to improve your configuration. The assessment compares your configuration to best practices established by Microsoft Support through many years of real-world experience. Each suggestion includes the details on how to change the configuration.

## Microsoft Entra authentication

[!INCLUDE [entra-id](../../includes/entra-id.md)]

Starting with [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)], Azure Arc enabled SQL Servers can utilize Microsoft Entra ID for authentication, bringing a modern centralized identity and access management solution to SQL Server. Microsoft Entra authentication provides greatly enhanced security over traditional username and password-based authentication, which is **not recommended**. For more information about the risks and challenges passwords pose, refer to ["What’s the solution to the growing problem of passwords?](https://news.microsoft.com/features/whats-solution-growing-problem-passwords-says-microsoft/) Microsoft Entra authentication removes the need for self-managed secrets entirely when communicating with Azure resources, through managed identity authentication. For user-based authentication, Microsoft Entra ID supports enhanced security measures including multi-factor authentication (MFA), single sign-on (SSO), and modern identity practices.

## Microsoft Defender for Cloud

Microsoft Defender for Cloud helps you discover and mitigate potential database vulnerabilities and alerts you to anomalous activities. These activities might indicate threats to your databases on SQL Server instances enabled for Azure Arc.

- Vulnerability assessment: Scan databases to discover, track, and remediate vulnerabilities.
- Threat protection: Receive detailed security alerts and recommended actions based on SQL Advanced Threat Protection to provide to mitigate threats.

When you enable Microsoft Defender through [!INCLUDE [ssazurearc](../../includes/ssazurearc.md)], you can get substantial cost savings on Defender.

## Microsoft Purview

Microsoft Purview provides a unified data governance solution to help manage and govern your on-premises, multicloud, and software as a service (SaaS) data. Easily create a holistic, up-to-date map of your data landscape with automated data discovery, sensitive data classification, and end-to-end data lineage. Enable data consumers to access valuable, trustworthy data management.

[!INCLUDE [ssazurearc](../../includes/ssazurearc.md)] powers some of the Microsoft Purview features such as access policies and it generally makes it easier for you to get your SQL Server instances connected into Purview.

## Pay-as-you-go for SQL Server

Now, with [!INCLUDE [ssazurearc](../../includes/ssazurearc.md)], you have the option of purchasing SQL Server using a 'pay-as-you-go' model instead of purchasing licenses. This model is a great alternative if you're looking to save costs on SQL Server instances that have variable demand for compute capacity over time. For example, when you can turn off a SQL Server at night or on weekends, or even just scale down the number of cores used during less busy times. It's also a great option if you only plan to use a SQL Server for a short period of time and then won't need it anymore. Pay-as-you-go, billed through Azure, is now available for all versions of SQL Server from 2012 to 2022.

## Extended Security Updates (ESU)

Once [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] has reached the end of its support lifecycle, you can sign up for an Extended Security Update (ESU) subscription for your servers and remain protected for up to three years. When you upgrade to a newer version of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], your ESU subscription is automatically canceled. When you [migrate to Azure SQL](/azure/azure-sql/migration-guides/), the ESU charges automatically stop but you continue to have access to the ESUs.

## Performance dashboards

Monitor SQL Server instances from Azure portal with performance dashboards. Performance dashboards simplify performance monitoring in Azure portal.

:::image type="content" source="media/overview/performance-dashboard.png" alt-text="Screenshot of performance dashboard for SQL Server enabled by Azure Arc." lightbox="media/overview/performance-dashboard.png":::

For details, see [Monitor SQL Server enabled by Azure Arc (preview)](sql-monitoring.md).

## Migration assessment

[!INCLUDE [ssazurearc](../../includes/ssazurearc.md)] migration assessment is a crucial tool for your cloud migration and modernization journey. It simplifies the discovery and readiness assessment for migration by providing:

- Cloud readiness analysis
- Identification of risks and mitigation strategies
- Recommendations for the specific service tier and Azure SQL configuration (SKU size) that best fits the workload needs
- Automatic generation of the assessment
- Continuous running on a default schedule of once per week
- Availability for all SQL Server editions

Migration assessment is for SQL Servers located in various environments, including your data center, edge sites, or any public cloud or hosting provider. It is available for any instance of SQL Server that is enabled by Azure Arc.

For details, review [Configure SQL best practices assessment - SQL Server enabled by Azure Arc](assess.md).

## Architecture

The SQL Server instance that you want to enable with Azure Arc can be installed in a virtual or physical machine running Windows or Linux. The [Azure Connected Machine agent](/azure/azure-arc/servers/agent-overview) and the Azure Extension for SQL Server securely connect to Azure to establish communication channels with multiple Azure services using only outbound HTTPS traffic on TCP port 443 using Transport Layer Security (TLS). The Azure Connected Machine agent can communicate through a configurable HTTPS proxy server over Azure Express Route, Azure Private Link or over the Internet. Review the [overview](/azure/azure-arc/servers/agent-overview), [network requirements](/azure/azure-arc/servers/network-requirements), and [prerequisites](/azure/azure-arc/servers/prerequisites) for the Azure Connected Machine agent.

Some of the services provided by [!INCLUDE [ssazurearc](../../includes/ssazurearc.md)], such as Microsoft Defender for Cloud and best practices assessment, require the Azure Monitoring agent (AMA) extension to be installed and connected to an Azure Log Analytics workspace for data collection and reporting.

The following diagram illustrates the architecture of [!INCLUDE [ssazurearc](../../includes/ssazurearc.md)].

:::image type="content" source="media/overview/architecture.png" alt-text="Diagram of the architecture for [!INCLUDE [ssazurearc](../../includes/ssazurearc.md)]" lightbox="media/overview/architecture.png":::

## <a id="feature-differentiation"></a> Feature availability depending on license type

[!INCLUDE [license-types](includes/license-types.md)]

## Feature availability by operating system

[!INCLUDE [features-operating-systems](includes/features-operating-system.md)]

## Feature availability by version

[!INCLUDE [features-version](includes/features-version.md)]

## Feature availability by edition

[!INCLUDE [features-edition](includes/features-edition.md)]

[!INCLUDE [supported-configurations](includes/supported-configurations.md)]

## Unsupported configurations

[!INCLUDE [unsupported-configurations](includes/unsupported-configurations.md)]

## Installation

The [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] Setup Installation Wizard doesn't support installation of the Azure extension for SQL Server. You can install this component from the command line, or by connecting the server to Azure Arc.

- [Install Azure extension for SQL Server from the command line](../../database-engine/install-windows/install-sql-server-from-the-command-prompt.md#install-and-connect-to-azure)
- [Automatically connect your SQL Server to Azure Arc](automatically-connect.md)

For VMware clusters, review [Support on VMware](#support-on-vmware).

## Supported Azure regions

[!INCLUDE [azure-arc-data-regions](includes/azure-arc-data-regions.md)]

## Related content

- [Learn about the prerequisites to connect your SQL Server to Azure Arc](prerequisites.md)
- [Automatically connect your SQL Server to Azure Arc](automatically-connect.md)
- [Learn more about Microsoft Defender for Cloud](/azure/defender-for-cloud/defender-for-sql-usage)
- [Lean more about Microsoft Purview](/azure/purview/register-scan-azure-arc-enabled-sql-server)
