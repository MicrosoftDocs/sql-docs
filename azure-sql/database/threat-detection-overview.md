---
title: Advanced Threat Protection
titleSuffix: Azure SQL Database & SQL Managed Instance
description: Advanced Threat Protection detects anomalous database activities that find security threats in Azure SQL Database and Azure SQL Managed Instance.
author: VanMSFT
ms.author: vanto
ms.reviewer: wiassaf, mathoma
ms.date: 10/06/2026
ms.service: azure-sql
ms.subservice: security
ms.topic: concept-article
ai-usage: ai-assisted
ms.custom:
  - sfi-image-nochange
monikerRange: "=azuresql || =azuresql-db || =azuresql-mi"
---

# SQL Advanced Threat Protection

[!INCLUDE [appliesto-sqldb-sqlmi-sqlvm-arc](../includes/appliesto-sqldb-sqlmi-sqlvm-arc.md)]

Advanced Threat Protection for [Azure SQL Database](sql-database-paas-overview.md), [Azure SQL Managed Instance](../managed-instance/sql-managed-instance-paas-overview.md), [SQL Server on Azure VMs](../virtual-machines/windows/sql-server-on-azure-vm-iaas-what-is-overview.md), and [SQL Server enabled by Azure Arc](/sql/sql-server/azure-arc/overview) detects anomalous activities that indicate unusual and potentially harmful attempts to access or exploit databases.

Advanced Threat Protection is part of the [Microsoft Defender for SQL](/azure/security-center/defender-for-sql-introduction) offering, which is a unified package for advanced SQL security capabilities. You can access and manage Advanced Threat Protection through the central Microsoft Defender for SQL portal.

## Overview

Advanced Threat Protection provides a new layer of security. It enables you to detect and respond to potential threats as they occur by providing security alerts on anomalous activities. You receive an alert upon suspicious database activities, potential vulnerabilities, and SQL injection attacks, as well as anomalous database access and query patterns. Advanced Threat Protection integrates alerts with [Microsoft Defender for Cloud](https://azure.microsoft.com/services/security-center/), which include details of suspicious activity and recommend action on how to investigate and mitigate the threat. Advanced Threat Protection makes it simple to address potential threats to the database without the need to be a security expert or manage advanced security monitoring systems.

For a full investigation experience, enable auditing, which writes database events to an audit log in your Azure storage account. To enable auditing, see [Auditing for Azure SQL Database](auditing-overview.md) or [Auditing for Azure SQL Managed Instance](../managed-instance/auditing-configure.md).

## Alerts

Advanced Threat Protection detects anomalous activities that indicate unusual and potentially harmful attempts to access or exploit databases. For a list of alerts, see [alerts in Microsoft Defender for Cloud](/azure/security-center/alerts-reference#alerts-sql-db-and-warehouse).

## Explore detection of a suspicious event

You receive an email notification when the system detects anomalous database activities. The email provides information on the suspicious security event, including the nature of the anomalous activities, database name, server name, application name, and the event time. In addition, the email provides information on possible causes and recommended actions to investigate and mitigate the potential threat to the database.

:::image type="content" source="media/threat-detection-overview/anomalous-activity-report.png" alt-text="Screenshot of anomalous activity report." lightbox="media/threat-detection-overview/anomalous-activity-report.png" :::

1. Select the **View recent SQL alerts** link in the email to launch the Azure portal and show the Microsoft Defender for Cloud alerts page. This page provides an overview of active threats detected on the database.

   :::image type="content" source="media/threat-detection-overview/active_threats.png" alt-text="Screenshot of Activity threats." lightbox="media/threat-detection-overview/active_threats.png":::

1. Select a specific alert to get additional details and actions for investigating this threat and remediating future threats.

   For example, SQL injection is one of the most common Web application security issues on the Internet that bad actors use to attack data-driven applications. They take advantage of application vulnerabilities to inject malicious SQL statements into application entry fields, breaching or modifying data in the database. For SQL Injection alerts, the alert's details include the vulnerable SQL statement that was exploited.

   :::image type="content" source="media/threat-detection-overview/specific-alert.png" alt-text="Screenshot of specific alert." lightbox="media/threat-detection-overview/specific-alert.png" :::

## Explore alerts in the Azure portal

Advanced Threat Protection integrates its alerts with [Microsoft Defender for Cloud](https://azure.microsoft.com/services/security-center/). Live SQL Advanced Threat Protection tiles within the database and SQL Microsoft Defender for Cloud blades in the Azure portal track the status of active threats.

Select **Advanced Threat Protection alert** to launch the Microsoft Defender for Cloud alerts page and get an overview of active SQL threats detected on the database.

:::image type="content" source="media/azure-defender-for-sql/advanced-threat-protection-alerts.png" alt-text="Screenshot of advanced threat protection alerts in database overview." lightbox="media/azure-defender-for-sql/advanced-threat-protection-alerts.png":::

:::image type="content" source="media/azure-defender-for-sql/advanced-threat-protection.png" alt-text="Screenshot of advanced threat protection in Defender for SQL." lightbox="media/azure-defender-for-sql/advanced-threat-protection.png":::

## Advanced Threat Protection for Azure Synapse Analytics

For more information, see [Advanced Threat Protection for Azure Synapse Analytics](/azure/synapse-analytics/sql/threat-detection-overview).

## Related content

- [Advanced Threat Protection in Azure SQL Database](threat-detection-configure.md)
- [Advanced Threat Protection in Azure SQL Managed Instance](../managed-instance/threat-detection-configure.md)
- [Microsoft Defender for SQL](azure-defender-for-sql.md)
- [Azure SQL Database auditing](auditing-overview.md)
- [Microsoft Defender for Cloud](/azure/security-center/security-center-introduction)
- [Azure SQL Database pricing page](https://azure.microsoft.com/pricing/details/sql-database/)
