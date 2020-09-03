---
title:
titleSuffix:  
description: 
author: anosov1960
ms.author: sashan 
ms.reviewer: mikeray
ms.date: 08/04/2020
ms.topic: conceptual
ms.prod: sql
---
# Configure advanced data security for Arc enabled SQL Server instance

You can enable advanced data security for your SQL Server instances on premises by following these steps.

## Pre-requisites

* Your SQL Server instance is onboarded to Arc-enabled SQL Server. Follow these the instructions to [onboard your SQL Server instance to  Arc-enabled SQL Server](connect.md).

* Your user account is assigned one of the [Security Center Roles (RBAC)](https://docs.microsoft.com/azure/security-center/security-center-permissions)

## Create a new Log Analytics workspace

* Search for _Log Analytics workspaces_ resource type and add a new one through the creation blade.

:::image type="content" source="media/configure-advanced-data-security/create-new-log-analytics-workspace.png" alt-text="Create new log analytics workspace":::

> [!NOTE]
> You can use a Log Analytics workspace in any region so if you already have one, you can use it. But we recommend creating it in the same region where your _Machine - Azure Arc_ resource is created.

* Go to the overview page of the Log Analytics workspace resource and select “Windows, Linux and other sources”. Copy the workspace ID and primary key for later use.

:::image type="content" source="media/configure-advanced-data-security/log-analytics-workspace-blade.png" alt-text="Log analytics workspace blade":::

## Install Microsoft Monitoring Agent (MMA) on the remote machine

* Select the __Machine - Azure Arc__ resource for the virtual or physical server where the SQL Server instance is installed and add the extension __Microsoft Monitoring Agent - Azure Arc__ using the  **Extensions** feature. When asked to configure the Log Analytics workspace, use the workspace ID and primary you saved in the previous step.  \\

:::image type="content" source="media/configure-advanced-data-security/install-mma-extension.png" alt-text="Install MMA extension":::

* After validation succeeds, click **Create** to start the MMA Arc Extension deployment workflow. When deployment completes the status will be updated to **Succeeded**.

* For more details, see [Extension management with Azure Arc](/azure/azure-arc/servers/manage-vm-extensions)

## Enable Advanced Data Security for SQL Server instance

* Go to Security Center and open the **Pricing & settings** page from the sidebar.

* Select the workspace that you have configured for the MMA extension in the previous step

* Select **Standard**. Make sure the option for **SQL servers on Machine (Preview)** is Enabled.

:::image type="content" source="media/configure-advanced-data-security/upgrade-log-analytics-workspace.png" alt-text="Upgrade log analytics workspace":::

## Explore security anomalies and threats in Azure Security Center

* Open your SQL Server – Azure Arc resource and select **Security** in the left menu. to see the recommendations and alerts for that instance.

   :::image type="content" source="media/configure-advanced-data-security/security-heading-sql-server-arc.png" alt-text="Security heading":::

* Click on any of the recommendations to see the vulnerability details in __Security Center__ .

   :::image type="content" source="media/configure-advanced-data-security/vulnerabilities-report.png" alt-text="Vulnerabilities report":::

* Click on any security alert for full details and further explore the attack in [Azure Sentinel](https://docs.microsoft.com/azure/sentinel/overview). The following diagram is an example of the brute force alert.

   :::image type="content" source="media/configure-advanced-data-security/brute-force-alert.png" alt-text="Brute force alert":::

* Click on **Take action** to mitigate the alert.

   :::image type="content" source="media/configure-advanced-data-security/brute-force-alert-mitigation.png" alt-text="Brute force alert mitigation":::

> [!NOTE]
> The general __Security Center__ link at the top of the page does not use the Preview portal URL so your __SQL Server - Azure Arc__ resources will not be visible there. We recommend following the links for the individual recommendations or alerts.

## Next steps

You can further investigate the security alerts and attacks using [Azure Sentinel](/azure/sentinel/overview). Follow these instructions to [on-board Azure Sentinel](/azure/sentinel/connect-data-sources).
