---
title: Quickstart - Enable SQL Server instance for Azure Arc
description: In this quickstart, you connect and register a hybrid machine with Azure Arc-enabled servers to create an Arc-enabled SQL Server.
ms.topic: quickstart
ms.date: 07/18/2023
author: pochiraju
ms.author: rajpo
ms.reviewer: mikeray
ms.service: sql
ms.custom: 
---

# Quickstart: Connect SQL Server machines to Azure Arc

[!INCLUDE [sqlserver](../../includes/applies-to-version/sqlserver.md)]

Get started with [Azure Arc-enabled SQL Server](overview.md) to manage and govern your Windows and Linux SQL Server instances hosted across on-premises, edge, and multicloud environments.

In this quickstart, you'll deploy and configure the Azure Connected Machine agent and deploy Azure extension for SQL Server on a Windows or Linux machine hosted outside of Azure, so that it can be managed through Azure Arc-enabled servers.

> [!TIP]
> If you prefer to try out things in a sample/practice experience, get started quickly with [Azure Arc Jumpstart](https://azurearcjumpstart.io/azure_arc_jumpstart/azure_arc_servers/).

## Prerequisites

* An Azure account with an active subscription. [Create an account for free](https://azure.microsoft.com/free/?WT.mc_id=A261C142F).
* Deploying the Connected Machine agent on a machine requires that you have administrator permissions to install and configure the agent. On Linux this is done by using the root account, and on Windows, with an account that is a member of the Local Administrators group.
* The Microsoft.HybridCompute, Microsoft.GuestConfiguration, Microsoft.HybridConnectivity, and Microsoft.AzureArcData resource providers must be registered on your subscription. Please [register these resource providers ahead of time](prerequisites.md#register-resource-providers).
* Before you get started, be sure to review the [agent prerequisites](prerequisites.md) and verify the following:
  * Your target machine is running a supported [operating system](prerequisites.md#supported-sql-server-versions-and-operating-systems).
  * Your account has the [required Azure built-in roles](prerequisites.md#permissions).
  * Confirm that the Linux hostname or Windows computer name doesn't use a [reserved word or trademark](/azure/azure-resource-manager/troubleshooting/error-reserved-resource-name).
  * If the machine connects through a firewall or proxy server to communicate over the Internet, make sure the URLs [listed](/azure/azure-arc/servers/network-requirements#urls) are not blocked.
* One or more supported [instances of SQL Server](prerequisites.md#supported-sql-server-versions-and-operating-systems) on the machine.

## Generate installation script

Use the Azure portal to create a script that automates the agent download and installation and establishes the connection with Azure Arc.

1. [Go to the Azure portal page for adding servers with Azure Arc](https://portal.azure.com/#view/Microsoft_Azure_HybridCompute/HybridVmAddBlade). Select the **Add a single server** tile, then select **Generate script**.

    > [!NOTE]
    > In the portal, you can also reach this page by searching for and selecting "Servers - Azure Arc" and then selecting **+Add**.

1. Review the information on the **Prerequisites** page, then select **Next**.

1. On the **Resource details** page, provide the following:

   1. Select the subscription and resource group where you want the machine to be managed within Azure.
   1. For **Region**, choose the Azure region in which the server's metadata will be stored.
   1. For **Operating system**, select the operating system of the server you want to connect.
   1. For **Connectivity method**, choose how the Azure Connected Machine agent should connect to the internet. If you select **Proxy server**, enter the proxy server IP address or the name and port number that the machine will use in the format `http://<proxyURL>:<proxyport>`.
   1. Select **Next**.

1. On the **Tags** page, review the default **Physical location tags** suggested and enter a value, or specify one or more **Custom tags** to support your standards. Then select **Next**.

1. In the **Download or copy the following script** section, review the script. If you want to make any changes, use the **Previous** button to go back and update your selections. Otherwise, select **Download** to save the script file.

## Install the agent using the script

Now that you've generated the script, the next step is to run it on the server that you want to onboard to Azure Arc. The script will download the Connected Machine agent from the Microsoft Download Center, install the agent on the server, create the Azure Arc-enabled server resource, and associate it with the agent.

Follow the steps below for the operating system of your server.

### Windows agent

1. Log in to the server.

1. Open an elevated 64-bit PowerShell command prompt.

1. Change to the folder or share that you copied the script to, then execute it on the server by running the `./OnboardingScript.ps1` script.

### Linux agent

1. To install the Linux agent on the target machine that can directly communicate to Azure, run the following command:

    ```bash
    bash ~/Install_linux_azcmagent.sh
    ```

1. Alternately, if the target machine communicates through a proxy server, run the following command:

    ```bash
    bash ~/Install_linux_azcmagent.sh --proxy "{proxy-url}:{proxy-port}"
    ```

## Verify the connection with Azure Arc

After you install the agent and configure it to connect to Azure Arc-enabled servers, go to the Azure portal to verify that the server has successfully connected. View your machine in the [Azure portal](https://aka.ms/hybridmachineportal).

> [!TIP]
> You can repeat these steps as needed to onboard additional machines. We also provide a variety of other options for deploying the agent, including several methods designed to onboard machines at scale. For more information, see [Azure Connected Machine agent deployment options](/azure/azure-arc/servers/deployment-options).

## Connected machine agent enables SQL Server for Arc

After your server is connected to Azure Arc, if one or more SQL Server instances are on it, the Arc agent automatically enrolls any SQL Server instances in Azure Arc.

## Validate your Arc-enabled SQL Server resources

Go to **Azure Arc > SQL Server** and open the newly registered Arc-enabled SQL Server resource to validate.

:::image type="content" source="media/join/validate-sql-server-azure-arc.png" alt-text="Screenshot of validating a connected SQL Server.":::

## Next steps

Now that you've enabled your Linux or Windows hybrid machine and successfully connected to the service, you are ready to manage, secure and protect your SQL Server from Azure.

- [Automatically connect your SQL Server to Azure Arc](automatically-connect.md)
- [Configure advanced data security for your SQL Server instance](configure-advanced-data-security.md)
- [Configure best practices assessment on an Azure Arc-enabled SQL Server instance](assess.md)