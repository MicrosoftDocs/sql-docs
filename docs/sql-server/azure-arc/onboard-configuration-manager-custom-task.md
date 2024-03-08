---
title: Connect SQL Server machines at scale with a Configuration Manager custom task sequence | Arc-enabled SQL Server
description: You can use a custom task sequence that can deploy the Connected Machine Agent to onboard a collection of devices to Azure Arc.
author: pochiraju
ms.author: rajpo
ms.reviewer: mikeray, randolphwest
ms.date: 03/08/2024
ms.topic: how-to
---

# Connect SQL Server machines at scale with a Configuration Manager custom task sequence

[!INCLUDE [sqlserver](../../includes/applies-to-version/sqlserver.md)]

[!INCLUDE [automatic](includes/if-manual.md)]

Microsoft Endpoint Configuration Manager facilitates comprehensive management of servers supporting the secure and scalable deployment of applications, software updates, and operating systems. Configuration Manager offers the custom task sequence as a flexible paradigm for application deployment.

You can use a custom task sequence that can deploy the Connected Machine Agent to onboard a collection of devices to Azure Arc-enabled servers.

Before you get started, be sure to review the [prerequisites](prerequisites.md) and verify that your subscription and resources meet the requirements. 

## Generate a service principal

Create a Microsoft Entra ID [service principal](/azure/active-directory/develop/app-objects-and-service-principals). A service principal is a special limited management identity that is granted only the minimum permission necessary to connect machines to Azure and to create the Azure resources for Azure Arc-enabled server and [!INCLUDE [ssazurearc](../../includes/ssazurearc.md)].

[!INCLUDE [entra-id](../../includes/entra-id.md)]

Before you get started, be sure to review the [prerequisites](prerequisites.md) and assign the necessary roles and permissions to the service principal.

## Download the agent and create the application

Download AzureExtensionForSQLServer.msi from the [link](https://aka.ms/AzureExtensionForSQLServer) for Windows.  The .msi must be saved in a server share for the custom task sequence.

Next, [create an application in Configuration Manager](/mem/configmgr/apps/get-started/create-and-deploy-an-application) using the installed Azure Connected Machine agent package:

1. In the **Configuration Manager** console, select **Software Library > Application Management > Applications**.
1. On the **Home** tab, in the **Create** group, select **Create Application**.
1. On the **General** page of the Create Application Wizard, select **Automatically detect information about this application from installation files**. This action pre-populates some of the information in the wizard with information that is extracted from the installation .msi file. Then, specify the following information:
   1. **Type**: Select **Windows Installer (*.msi file)**
   1. **Location**: Select **Browse** to choose the location where you saved the installation file **AzureExtensionForSQLServer.msi**.
      :::image type="content" source="media/onboard-configuration-manager-custom-task/configuration-manager-create-application.png" alt-text="Screenshot of the Create Application Wizard in Configuration Manager.":::
1. Select **Next**, and on the **Import Information** page, select **Next** again.
1. On the **General Information** page, you can supply further information about the application to help you sort and locate it in the Configuration Manager console. Once complete, select Next.
1. On the **Installation program** page, select **Next**.
1. On the **Summary** page, confirm your application settings and then complete the wizard.

You have finished creating the application. To find it, in the **Software Library** workspace, expand **Application Management**, and then choose **Applications**.

## Create a task sequence

The next step is to define a custom task sequence that installs the Azure Connected Machine Agent on a machine and deploy Azure Extension for SQL Server, then connects it to Azure Arc.

1. In the Configuration Manager console, go to the **Software Library** workspace, expand **Operating Systems**, and then select the **Task Sequences** node.
1. On the **Home** tab of the ribbon, in the **Create** group, select **Create Task Sequence**. This will launch the Create Task Sequence Wizard.
1. On the **Create a New Task Sequence** page, select **Create a new custom task sequence**.
1. On the **Task Sequence Information** page, specify a name for the task sequence and optionally a description of the task sequence.

   :::image type="content" source="media/onboard-configuration-manager-custom-task/configuration-manager-create-task-sequence.png" alt-text="Screenshot of the Create Task Sequence Wizard in Configuration Manager.":::

After you complete the Create Task Sequence Wizard, Configuration Manager adds the custom task sequence to the **Task Sequences** node. You can now edit this task sequence to add steps to it.

1. In the Configuration Manager console, go to the **Software Library** workspace, expand **Operating Systems**, and then select the **Task Sequences** node.
1. In the **Task Sequence** list, select the task sequence that you want to edit.
1. Define **Install Application** as the first task in the task sequence.
   1. On the **Home** tab of the ribbon, in the**Task Sequence** group, select **Edit**. Then, select **Add**, select **Software**, and select **Install Application**.
   1. Set the name to `Install Connected Machine Agent and Azure extension for SQL Server`.
   1. Select the Azure Extension for SQL Server.
      :::image type="content" source="media/onboard-configuration-manager-custom-task/configuration-manager-edit-task-sequence.png" alt-text="Screenshot showing a task sequence being edited in Configuration Manager.":::
1. Define **Run PowerShell Script** as the second task in the task sequence.
   1. Select **Add**, select **General**, and select **Run PowerShell Script**.
   1. Set the name to `Connect to Azure Arc`.
   1. Select **Enter a PowerShell script**.
   1. Select **Add Script**, and then edit the script to connect to Arc as shown below. Note that this template script has placeholder values for the service principal, tenant, subscription, resource group, and location, which you should update to the appropriate values.

   ```azurepowershell
   '& "$env:ProgramW6432\AzureExtensionForSQLServer\AzureExtensionForSQLServer.exe" --subId <subscriptionid> --resourceGroup <resourceGroupName> --location <AzureRegion> --tenantid <TenantId> --service-principal-app-id <servicePrincipalAppId> --service-principal-secret <servicePrincipalSecret> --proxy <proxy> --licenseType <licenseType> --excluded-SQL-instances <"MSSQLSERVER01 MSSQLSERVER02 MSSQLSERVER15"> --machineName <"ArcServerName">'
   ```

   :::image type="content" source="media/onboard-configuration-manager-custom-task/configuration-manager-connect-to-azure-arc.png" alt-text="Screenshot showing a task sequence being edited to run a PowerShell script.":::

1. Select **OK** to save the changes to your custom task sequence.

## Deploy the custom task sequence and verify connection to Azure Arc

Follow the steps outlined in Deploy a task sequence to deploy the task sequence to the target collection of devices. Choose the following parameter settings.

- Under **Deployment Settings**, set **Purpose** as **Required** so that Configuration Manager automatically runs the task sequence according to the configured schedule. If **Purpose** is set to **Available** instead, the task sequence will need to be installed on demand from Software Center.
- Under **Scheduling**, set **Rerun Behavior** to **Rerun if failed previous attempt**.

## Validate successful onboarding

After you connected the SQL Server instances to Azure, go to the [Azure portal](https://aka.ms/azureportal) and view the newly created Azure Arc resources. You'll see a new `Server - Azure Arc` resource for each connected machine and a new `SQL Server - Azure Arc` resource for each connected SQL Server instance within approximately 1 minute. If these resources aren't created, it means something went wrong during the extension installation and activation process. See [Troubleshoot Azure extension for SQL Server](troubleshoot-deployment.md) for the troubleshooting options.

:::image type="content" source="./media/join-at-scale/successful-onboard.png" alt-text="Screenshot showing a successful onboard.":::



## Next steps

- Learn how to [Configure your SQL Server instance for periodic environment health check using on-demand SQL assessment](assess.md)

- Learn how to [Protect [!INCLUDE [ssazurearc](../../includes/ssazurearc.md)] with Microsoft Defender for Cloud](configure-advanced-data-security.md)
