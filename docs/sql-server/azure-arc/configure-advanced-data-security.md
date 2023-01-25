---
title: Protect Azure Arc-enabled SQL Server with Configure Microsoft Defender for Cloud 
titleSuffix: Azure Arc-enabled SQL Server
description: Protect Azure Arc-enabled SQL Server with Microsoft Defender for Cloud
author: anosov1960
ms.author: sashan
ms.reviewer: mikeray, randolphwest
ms.date: 10/12/2022
ms.service: sql
ms.topic: conceptual
---
# Protect Azure Arc-enabled SQL Server with Microsoft Defender for Cloud 

You can configure your instance connected to Azure with Microsoft Defender for Cloud by following these steps.

## Prerequisites

- Your Windows-based SQL Server instance is connected to Azure. Follow the instructions to [onboard your SQL Server instance to Azure Arc-enabled SQL Server](connect.md).

   > [!NOTE]
   > Microsoft Defender for Cloud is only supported for SQL Server instances on Windows machines. This will not work for SQL Server on Linux machines.

- Your user account is assigned one of the [Security Center Roles (RBAC)](/azure/security-center/security-center-permissions)

## Create a Log Analytics workspace

1. Search for **Log Analytics workspaces** resource type and add a new one through the creation pane.

   > [!NOTE]
   > You can use a Log Analytics workspace in any region so if you already have one, you can use it. But we recommend creating it in the same region where your Azure Arc-enabled SQL Server resource is created.

1. Go to **Agents management > Log Analytics agent instructions**  and copy Workspace ID and Primary key for later use.

## Install Log Analytics Agent

The next step is needed only if you haven't yet configured MMA on the remote machine.

1. Go to **Azure Arc > Servers** and open  the Azure Arc-enabled server resource for the machine where the SQL Server instance is installed. 

1. Open the **Extensions** blade and click **+ Add**. 

1. Select **Log Analytics Agent - Azure Arc** and click **Next**. 

1. Set the Workspace ID and Workspace key using the values you saved in the previous step.

1. After validation succeeds, select **Create** to install the agent. When the deployment completes, the status updates to *Succeeded*.

For more information, see [Extension management with Azure Arc](/azure/azure-arc/servers/manage-vm-extensions).

## Enable Microsoft Defender for Cloud

1. Go to **Azure Arc > SQL Servers** and open  the Azure Arc-enabled SQL server resource for the instance that you want to protect. 

1. Click on the **Microsoft Defender for Cloud** tile. If Enablement Status shows **Disabled at the subscription-level**, follow the steps documented in [Enable Microsoft Defender for SQL servers on machines](/azure/defender-for-cloud/defender-for-sql-usage#step-3-enable-the-optional-plan-in-defender-for-clouds-environment-settings-page).

> [!NOTE]
> The first scan to generate the vulnerability assessment happens within 24 hours after enabling Microsoft Defender for Cloud. After that, auto scans are be performed every week on Sunday.

## Explore

Explore security anomalies and threats in Azure Security Center.

1. Open your SQL Server – Azure Arc resource and select **Microsoft Defender for Cloud** in the **Settings** section of the left menu. to see the recommendations and alerts for that SQL Server instance.

   :::image type="content" source="media/configure-advanced-data-security/security-heading-sql-server-arc.png" alt-text="Screenshot showing how to select security heading.":::

1. Select any of the recommendations to see the vulnerability details.

   :::image type="content" source="media/configure-advanced-data-security/vulnerabilities-report.png" alt-text="Screenshot showing the Vulnerability report.":::

1. Select any security alert for full details and further explore the attack. The following diagram is an example of the Potential SQL Injection alert.

   :::image type="content" source="media/configure-advanced-data-security/brute-force-alert.png" alt-text="Screenshot showing a brute force alert.":::

1. Select **Take action** to mitigate the alert.

   :::image type="content" source="media/configure-advanced-data-security/brute-force-alert-mitigation.png" alt-text="Screenshot showing alert mitigation.":::

## Next steps

- You can further investigate the security alerts and attacks using [Azure Sentinel](/azure/sentinel/overview). For details, see [on-board Azure Sentinel](/azure/sentinel/connect-data-sources).
