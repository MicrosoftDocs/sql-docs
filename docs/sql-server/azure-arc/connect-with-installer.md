---
title: Connect your SQL Server to Azure Arc (with installer)
description: Connect an instance of SQL Server to Azure Arc with the installer (.msi). Allows you to manage SQL Server centrally, as an Arc-enabled resource.
author: anosov1960
ms.author: sashan
ms.reviewer: mikeray, maghan
ms.date: 03/08/2024
ms.topic: conceptual
---

# Connect your SQL Server to Azure Arc with installer (.msi)

[!INCLUDE [sqlserver](../../includes/applies-to-version/sqlserver.md)]

This article explains how to connect your SQL Server instance to Azure Arc with an installer (.msi). Before you proceed, complete the [Prerequisites](prerequisites.md).

## Deploy SQL Server extension from AzureExtensionForSQLServer.msi

You can also onboard your SQL Server instances to Azure Arc by directly using  AzureExtensionForSQLServer.msi. This method helps you integrate onboarding SQL Server instances to Arc with any existing deployment automation tools and services.

1. Download [AzureExtensionForSQLServer.msi](https://aka.ms/AzureExtensionForSQLServer).
1. Open AzureExtensionForSQLServer.msi. This installs the necessary packages for onboarding SQL Server instances to Azure Arc.
1. Open PowerShell console in admin mode and execute the following commands.

   If you use a Microsoft Entra ID service principal to authenticate, execute the following command on the target SQL Server.

   [!INCLUDE [entra-id](../../includes/entra-id.md)]

   ```powershell
   '& "$env:ProgramW6432\AzureExtensionForSQLServer\AzureExtensionForSQLServer.exe" --subId <subscriptionid> --resourceGroup <resourceGroupName> --location <AzureRegion> --tenantid <TenantId> --service-principal-app-id <servicePrincipalAppId> --service-principal-secret <servicePrincipalSecret> --proxy <proxy> --licenseType <licenseType> --excluded-SQL-instances <"MSSQLSERVER01 MSSQLSERVER02 MSSQLSERVER15"> --machineName <"ArcServerName">'
   ```

   Otherwise, execute the following command on the target SQL Server.

   ```powershell
   '& "$env:ProgramW6432\AzureExtensionForSQLServer\AzureExtensionForSQLServer.exe" --subId <subscriptionid> --resourceGroup <resourceGroupName> --location $location --tenantid <TenantId> --proxy <proxy> --licenseType <licenseType> --excluded-SQL-instances <"MSSQLSERVER01 MSSQLSERVER02 MSSQLSERVER15"> --machineName <"ArcServerName">'
   ```

   > [!NOTE]  
   > Command line parameter "--machineName" is an optional parameter, if it is not provided then name of the Arc enabled server resource will be the host name of the machine.

   > [!IMPORTANT]  
   > Microsoft [!INCLUDE [ssazurearc](../../includes/ssazurearc.md)] is licensed to you as part of your or your company's subscription license for Microsoft Azure Services. You may only use the software with Microsoft Azure Services and are subject to the terms and conditions of the agreement under which you obtained Microsoft Azure Services. You may not use the software if you do not have an active subscription license for Microsoft Azure Services.  
   > Microsoft Azure Legal Information: [Microsoft Azure Legal Information](https://azure.microsoft.com/support/legal/) and [Microsoft Privacy Statement](https://azure.microsoft.com/support/legal/)

## Validate your Arc-enabled SQL Server resources

Go to **Azure Arc > SQL Server** and open the newly registered Arc-enabled SQL Server resource to validate.

:::image type="content" source="media/join/validate-sql-server-azure-arc.png" alt-text="Screenshot of validating a connected SQL Server.":::

## Next steps

- [Configure advanced data security for your SQL Server instance](configure-advanced-data-security.md)
- [Configure best practices assessment on a [!INCLUDE [ssazurearc](../../includes/ssazurearc.md)] instance](assess.md)
- [Known issues: SQL Server enabled by Azure Arc](known-issues.md)
