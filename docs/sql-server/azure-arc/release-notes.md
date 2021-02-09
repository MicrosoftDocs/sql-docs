---
title: Azure Arc enabled SQL Server - Release notes
description: Latest release notes 
author: anosov1960
ms.author: sashan
ms.reviewer: mikeray
ms.date: 12/08/2020
ms.topic: conceptual
ms.prod: sql
---

# Release notes - Azure Arc enabled SQL Server (Preview)

> [!NOTE]
> As a preview feature, the technology presented in this article is subject to [Supplemental Terms of Use for Microsoft Azure Previews](https://azure.microsoft.com/support/legal/preview-supplemental-terms/).

## December 2020

### Breaking change

This release introduces an updated [resource provider](/azure/azure-resource-manager/management/azure-services-resource-providers) called `Microsoft.AzureArcData`. Before you can continue using Azure Arc enabled SQL Server, you need to register this resource provider. See the resource provider registration instructions in the [Prerequisites](connect.md#prerequisites) section.

If you have existing existing SQL Server - Azure Arc resources, use these steps to migrate them to Microsoft.AzureArcData namespace.

1. Launch the [Cloud Shell](https://shell.azure.com/). For details, [read more about PowerShell in Cloud Shell](/azure/cloud-shell/quickstart-powershell).

2. Upload the script to the shell using the following command:

    ```console
    curl https://raw.githubusercontent.com/microsoft/sql-server-samples/master/samples/manage/azure-arc-enabled-sql-server/migrate-to-azure-arc-data.ps1 -o migrate-to-azure-arc-data.ps1
    ```
3. Run the script.  

    ```console
   ./migrate-to-azure-arc-data.ps1
    ```

> [!NOTE]
> - To paste the commands into the shell, use `Ctrl-Shift-V` on Windows or `Cmd-v` on MacOS.
> - The `curl` command will copy the script directly to the home folder associated with your Cloud Shell session.
> - The script will prompt for the resource group name and print a message when migration is completed.

### Other changes

* The *TCPPorts* property in the **SQL Server - Azure Arc** resource type has been renamed to *TCPStaticPorts*
* The permissions required aren’t as broad as they used to be. See the [Required permission](overview.md#required-permissions) section for details.

### Known issues

* The *CreateTime* property won’t be added to any newly created resources in the AzureArcData namespace, including the **SQL Server - Azure Arc** resources.

## October 2020

The October update includes the following improvements:

* The register Azure Arc enabled SQL Server blade now includes the **Tags** tab. The tags are included in the registration script and are reflected in the **SQL Server - Azure Arc** resource(s). For details, see [Connect your SQL Server to Azure Arc](connect.md).

* The **Environment Health** entry now supports activation of **SQL Assessment** from the Portal by deploying a *CustomScriptExtension*. For details, see [Configure SQL Assessment](assess.md#run-on-demand-sql-assessment).

### Known issues

The following issues apply to the October release:

* Connecting SQL Server instances to Azure Arc requires an account with a broad set of permissions. For details, see [Required permissions](overview.md#required-permissions).

## September 2020

Azure Arc enabled SQL Server is released for public preview. Azure Arc enabled SQL Server extends Azure services to SQL Server instances hosted outside of Azure in the customer’s datacenter, on the edge or in a multi-cloud environment.

For details, see [Azure Arc enabled SQL Server Overview](overview.md)

### Known issues

The following issues apply to the September release:

* The **Register Azure Arc enabled SQL Server** blade does not support configuring custom tags. To add custom tags, open the **SQL Server - Azure Arc** resource after registration and change Tags in the **Overview** page.

* Connecting SQL Server instances to Azure Arc requires an account with a broad set of permissions. For details, see [Required permissions](overview.md#required-permissions).

## Next steps

**Just want to try things out?**  Get started quickly with [Azure Arc enabled SQL Server Jumpstart](https://aka.ms/AzureArcSqlServerJumpstart).