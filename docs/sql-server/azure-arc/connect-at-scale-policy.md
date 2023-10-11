---
title: Connect SQL Servers on Azure Arc-enabled servers at scale using Azure policy
description: In this article, you learn different ways of connecting SQL Server instances to Azure Arc at scale.
author: anosov1960
ms.author: sashan
ms.reviewer: mikeray, randolphwest
ms.date: 07/18/2023
ms.topic: conceptual
---

# Connect SQL Server instances to Azure at scale

[!INCLUDE [sqlserver](../../includes/applies-to-version/sqlserver.md)]

> [!IMPORTANT]  
> Azure Arc automatically installs the Azure extension for SQL Server when a server connected to Azure Arc has SQL Server installed. All the SQL Server instance resources are automatically created in Azure, providing a centralized management platform for all your SQL Servers.
To automatically connect your SQL Server instances, see [Automatically Connect your SQL Server to Azure Arc](automatically-connect.md).
Use the method below, if your server is already connected to Azure, but Azure extension for SQL Server is not deployed automatically using above methods.
>
This article describes two methods of how to connect multiple instances of SQL Server to Azure Arc as a single task. Before you start complete the [Prerequisites](prerequisites.md#prerequisites).


## Connect at-scale using Azure Policy

You can automatically connect SQL Server instances on multiple Arc-enabled machines using an Azure policy definition called *Configure Arc-enabled machines running SQL Server to have SQL Server extension installed*. This policy definition is not assigned to a scope by default. If you assign this policy definition to a scope of your choice, it installs the *Azure extension for SQL Server* on all Azure Arc-enabled servers where SQL Server is installed. Once installed, the extension connects the SQL Server instances on the machine with Azure. After that, the extension runs continuously to detect changes of the SQL Server configuration and synchronize them with Azure. For example, if a new SQL Server instance is installed on the machine, the extension automatically registers it with Azure.


### Connect at-scale using Azure Policy assignment

 

1. Navigate to **Azure Policy** in the Azure portal and choose **Definitions**. 
1. Search for *Configure Arc-enabled machines running SQL Server to have SQL Server extension installed.* and click on the policy.
1. Select **Assign**. 
1. Choose a Scope.
1. Select **Next**, **Next**.
1. On the **Remediation** tab, click **Create a remediation task**.
1. Choose **System assigned managed identity** (recommended) or **User assigned managed identity** and choose a managed identity which has *User Access Administration* and *Log Analytics Contributor* role assignments. 
1. Click **Review + Create**.
1. Click **Create**.

See [Azure Policy documentation](/azure/governance/policy) for general instructions about how to assign an Azure policy using Azure portal or an API of your choice.

> [!IMPORTANT]
> - The Arc-enabled SQL Server resources for the `SQL Server - Azure Arc` resources are created in the same region and the resource group as the `Server - Azure Arc` resources on which they are hosted.
> - Because Azure extension for SQL Server synchronizes with Azure once an hour, it may take up to one hour before these resources are created after you create the policy assignment.
> 

## Connect at-scale using the automatic Arc-enabled SQL Server registration method (Recommended)

Alternatively, you can quickly enable at-scale registration using the method below:

1. Navigate to the **SQL Server - Azure Arc** view in the Azure portal.
1. Select the **Automatic Arc-enabled SQL Server registration** button at the top of the list.
1. Select a subscription and optionally a resource group.  
1. Select License type.  Please note that some Arc-enabled SQL Server features are only available for SQL Servers with Software Assurance (Paid) or with Azure pay-as-you-go. For more information, review [Manage SQL Server license type](manage-configuration.md).
1. Select **Enable**.

These steps create a new Azure Policy assignment of the *Configure Arc-enabled machines running SQL Server to have SQL Server extension installed* policy definition to the selected subscription and, optionally, a specific resource group scope. A new system assigned managed identity is created and granted the required permissions to onboard Arc-enabled SQL Servers. This new managed identity is used by the policy remediation to install the Azure extension for SQL Server.


## Validate successful onboarding

After you connected the SQL Server instances to Azure, go to the [Azure portal](https://aka.ms/azureportal) and view the newly created Azure Arc resources. You'll see a new `Server - Azure Arc` resource for each connected machine and a new `SQL Server - Azure Arc` resource for each connected SQL Server instance within approximately 1 minute. If these resources aren't created, it means something went wrong during the extension installation and activation process. See [Troubleshoot Azure extension for SQL Server](troubleshoot-deployment.md).

:::image type="content" source="./media/join-at-scale/successful-onboard.png" alt-text="Screenshot showing a successful onboard.":::



## Next steps

- Learn how to [Configure your SQL Server instance for periodic environment health check using on-demand SQL assessment](assess.md)

- Learn how to [Protect Azure Arc-enabled SQL Server with Microsoft Defender for Cloud](configure-advanced-data-security.md)
