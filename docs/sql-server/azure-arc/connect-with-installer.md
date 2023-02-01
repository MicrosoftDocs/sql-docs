---
title: Connect your SQL Server to Azure Arc (with installer)
description: Connect an instance of SQL Server to Azure Arc with the installer (.msi). Allows you to manage SQL Server centrally, as an Arc-enabled resource.
author: anosov1960
ms.author: sashan
ms.reviewer: mikeray, maghan
ms.date: 01/12/2023
ms.service: sql
ms.topic: conceptual
ms.custom: event-tier1-build-2022
---

# Connect your SQL Server to Azure Arc with installer (.msi)

Beginning with [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] you connect a new SQL Server instance to Azure Arc when you're installing it on Windows Operating System. See [Install SQL Server 2022](../../database-engine/install-windows/install-sql-server-from-the-installation-wizard-setup.md#install-sql-server-2022).

This article explains how to connect your SQL Server instance to Azure Arc with an installer (.msi). Before you proceed, complete the [Prerequisites](prerequisites.md#prerequisites).

## Deploy SQL Server extension from AzureExtensionForSQLServer.msi

Alternatively, you can also onboard your SQL Servers to Azure Arc by directly using  AzureExtensionForSQLServer.msi. This method helps you integrate onboarding SQL Servers to Arc with any existing deployment automation tools and services.

1. Download AzureExtensionForSQLServer.msi from the [link](https://aka.ms/AzureExtensionForSQLServer).
1. Double-select on AzureExtensionForSQLServer.msi.  This installs the necessary packages for onboarding SQL Servers to Azure Arc.
1. Open PowerShell console in admin mode and execute the following commands.

   If you use Azure Active Directory service principal to authenticate, execute the command below on the target SQL Server.

  ```powershell
  '& "$env:ProgramW6432\AzureExtensionForSQLServer\AzureExtensionForSQLServer.exe" --subId <subscriptionid> --resourceGroup <resourceGroupName> --location <AzureRegion> --tenantid <TenantId> --service-principal-app-id <servicePrincipalAppId> --service-principal-secret <servicePrincipalSecret> --proxy <proxy> --licenseType <licenseType> --excluded-SQL-instances <"MSSQLSERVER01 MSSQLSERVER02 MSSQLSERVER15">'
  ```

   Otherwise, execute the command below on the target SQL Server.

  ```powershell
  '& "$env:ProgramW6432\AzureExtensionForSQLServer\AzureExtensionForSQLServer.exe" --subId <subscriptionid> --resourceGroup <resourceGroupName> --location $location --tenantid <TenantId> --proxy <proxy> --licenseType <licenseType> --excluded-SQL-instances <"MSSQLSERVER01 MSSQLSERVER02 MSSQLSERVER15">'
  ```

 > [!IMPORTANT]  
 > Microsoft Azure Arc-enabled SQL Server is licensed to you as part of your or your company's subscription license for Microsoft Azure Services. You may only use the software with Microsoft Azure Services and are subject to the terms and conditions of the agreement under which you obtained Microsoft Azure Services. You may not use the software if you do not have an active subscription license for Microsoft Azure Services.  
 > Microsoft Azure Legal Information: [Microsoft Azure Legal Information](https://azure.microsoft.com/support/legal/) and [Microsoft Privacy Statement](https://azure.microsoft.com/support/legal/)

## Validate your Arc-enabled SQL Server resources

Go to **Azure Arc > SQL Server** and open the newly registered Arc-enabled SQL Server resource to validate.

   :::image type="content" source="media/join/validate-sql-server-azure-arc.png" alt-text="Screenshot of validating a connected SQL server.":::

## Delete your Arc-enabled SQL Server resource

To delete your Arc-enabled SQL Server resource, go to **Azure Arc > SQL Server**, open the Arc-enabled SQL Server resource for that instance, and select the **Delete** button.

> [!IMPORTANT]  
> Because multiple SQL Server instances could be installed on the same machine, the *Delete* button doesn't uninstall the Azure extension for SQL Server on that machine. To uninstall it, follow the [uninstall extension](/azure/azure-arc/servers/manage-vm-extensions-portal#uninstall-extension) steps.

## Restore a deleted Arc-enabled SQL Server resource

If you accidentally deleted your Arc-enabled SQL Server resource, you can restore it with the following steps.

1. If you also uninstalled the SQL Server extension by mistake, reinstall it. Select the correct version for your OS.

    ```azurecli
       az connectedmachine extension create --machine-name "{your machine name}" --location "{azure region}" --name "WindowsAgent.SqlServer" --resource-group "{your resource group name}" --type "{OS}Agent.SqlServer" --publisher "Microsoft.AzureData" --settings '{\"SqlManagement\":{\"IsEnabled\":true},  \"excludedSqlInstances\":[]}'
   ```

   > [!IMPORTANT]  
   > The location property must match the location of the Arc-enabled SQL Server resource for the server specified by the `--machine-name` parameter.

1. Check to make sure your instance is in the exclusion list (see the value of the *excludedSqlInstances* property).

    ```azurecli
        az connectedmachine extension show --machine-name "{your machine name}" --resource-group "{your resource group name}" -n WindowsAgent.SqlServer
    ```

1. Make sure to remove your instance from the exclusion list and update the extension settings.

    ```azurecli
        az connectedmachine extension create --machine-name "{your machine name}" --location "{azure region}" --name "WindowsAgent.SqlServer" --resource-group "{your resource group name}" --type "WindowsAgent.SqlServer" --publisher "Microsoft.AzureData" --settings '{\"SqlManagement\":{\"IsEnabled\":true},  \"excludedSqlInstances\":[\"{named instance 1}\",\"{named instance 3}}\"]}'
    ```

The instance is restored after the next sync with the agent. For information on managing vm extensions using Portal or PowerShell, see [virtual machine extension management](/azure/azure-arc/servers/manage-vm-extensions).

## Next steps

- [Configure advanced data security for your SQL Server instance](configure-advanced-data-security.md)
- [Configure on-demand SQL assessment for your SQL Server instance](assess.md)