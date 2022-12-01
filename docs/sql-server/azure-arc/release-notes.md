---
title: Azure Arc-enabled SQL Server - Release notes
description: Latest release notes
author: anosov1960
ms.author: sashan
ms.reviewer: mikeray, randolphwest
ms.date: 11/05/2022
ms.service: sql
ms.topic: conceptual
---

# Release notes - Azure Arc-enabled SQL Server

## November 2022

This release is published November 12, 2022

### Extension version

`1.1.2132.21`

> [!IMPORTANT]
> Billing for this extension will not be enabled until December 1, 2022.

### Azure extension for SQL Server

- Both Linux (`LinuxAgent.SqlServer`) and Windows (`WindowsAgent.SqlServer`) versions of Azure extension for SQL Server now support billing through Azure when pay-as-you-go activation is selected in [SQL 2022[!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] setup wizard](../../database-engine/install-windows/install-sql-server-from-the-installation-wizard-setup.md) or [command prompt](../../database-engine/install-windows/install-sql-server-from-the-command-prompt.md). 
- [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] includes the pay-as-you-go activation option, that forces the installation of Azure extension for SQL Server during set up.
- Billing meters are introduced to support pay-as-you-go billing through Azure

### Other changes

The *LicenseType* property of `SQL Server - Azure Arc` has been extended to provide more granular license information. The values now include:

| **Value** | **Description** |
|:--|:--|
|Paid|SQL Server instance is installed using a product key with Software Assurance or SQL subscription|
|LicenseOnly|SQL Server instance is installed using a product key without Software Assurance or SQL subscription|
|PAYG|SQL Server instance is installed using a pay-as-you-go activation option (new in [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)]|
|Free|Indicates that the instance uses Evaluation or Developer edition of SQL Server|
|HADR|Indicates that the instance is a replica in an availability group. If it's covered by Software Assurance, it may not require a license. For more information, see [SQL Server Commercial Licensing Terms](https://www.microsoft.com/licensing/terms/productoffering/SQLServer/EAEAS).|
|||

### Known issues and limitations

- Feature use requires Azure extension for SQL Server version `v1.1.2132.21` or higher.
- The pay-as-you-go billing is limited to SQL Server 2022[!INCLUDE [sssql22-md](../../includes/sssql22-md.md)]
- Azure extension for SQL Server is not supported in the following environments:
  - SQL Server in Linux containers
  - SQL Server on VMware clusters or on Azure VMware Solution
  - SQL Server in Azure VMs. If a custom VM image is migrated to Azure VM, Azure extension for SQL Server will stop working. The customers need to [remove the Azure extension for SQL Server](connect.md#delete-your-arc-enabled-sql-server-resource) and [enable automatic registration with SQL IaaS Agent extension](/azure/azure-sql/virtual-machines/windows/sql-agent-extension-automatic-registration-all-vms.md).

## September 2022

This release is published September 13, 2022

### Extension version

`1.1.2082.9`

### Azure extension for SQL Server

- Both Linux (`LinuxAgent.SqlServer`) and Windows (`WindowsAgent.SqlServer`) versions of Azure extension for SQL Server have been released with the same image tag.
- SQL Server 2022 RC0 includes the option of installing Azure extension for SQL Server during setup. 
- The extension now supports HTTP proxy servers on the Linux platforms.

### Azure Active Directory authentication 

- Azure extension for SQL Server has been enhanced to support configuring Active Directory authentication using mssql-conf. For more details, see [Azure Active Directory authentication for SQL Server 2022](https://cloudblogs.microsoft.com/sqlserver/2022/07/28/azure-active-directory-authentication-for-sql-server-2022/).

## July 2022

This release is published July 26, 2022

### Extension version

`v1.0.2019.2`

### Azure extension for SQL Server

- Preview support for Azure extension for SQL Server on Linux operating system

### Azure Active Directory authentication 

- Azure Active Directory authentication configuration enabled for SQL Server 2022 via Azure Extension for SQL Server [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] only, running on Linux operating system

## July 30, 2021

Azure Arc-enabled SQL Server releases for general availability support

### Breaking changes

This release introduces a SQL Server extension that continuously monitors for changes of the SQL Server configuration and automatically updates the corresponding Arc-enabled SQL Server resources. The extension is installed as part of the SQL Server instance registration process. To upgrade your existing Arc-enabled SQL Server resources to an agent-based configuration, use any of the methods described in [Connect your SQL Server to Azure Arc](connect.md).

> [!IMPORTANT]
> If you installed a version of SQL Server extension during the preview, make sure it is updated to version 1.1.1668.43 or later. An extension can be updated using the *Extention* pane of the specific connected server resource.

The introduction of SQL Server extension requires that the user accounts have more privileges in order to connect a  SQL Server instance to Azure Arc. For details, see [Required permissions](overview.md#required-permissions).

> [!NOTE]
> In this release, the SQL Server extension is only available for  Windows. A Linux version of the extension will be announced separately.

### Other changes

This release includes a new Azure policy *Configure Arc-enabled machines running SQL Server to have SQL Server extension installed*. It enables automatic registration of all SQL Server instances after their hosting server is connected to Azure Arc. For details of using this policy, see [At scale registration from Azure](connect-at-scale.md#connect-at-scale-using-azure-policy).

## April 2021

### Breaking change

No breaking changes

### Other changes

A new property *LicenseType* has been added to the **SQL Server - Azure Arc** resource type. It indicates if your SQL Server instance requires a license. The property can have one of the following values:

| **Value** | **Description** |
|:--|:--|
|Paid|Indicates that the instance uses Enterprise, Standard or Web edition of SQL Server|
|Free|Indicates that the instance uses Express or Developer edition of SQL Server|
|HADR|Indicates that the instance is a replica in an availability group. If it's covered by Software Assurance, it may not require a license. For more information, see [SQL Server Commercial Licensing Terms](https://www.microsoft.com/licensing/terms/productoffering/SQLServer/EAEAS).

> [!NOTE]
> For the existing **SQL Server - Azure Arc** resources, this property will show a *Null* value. It will be automatically updated with the correct value after Azure Arc-enabled SQL Server becomes generally available.

## December 2020

### Breaking change

This release introduces an updated [resource provider](/azure/azure-resource-manager/management/azure-services-resource-providers) called `Microsoft.AzureArcData`. Before you can continue using Azure Arc-enabled SQL Server, you need to register this resource provider. See the resource provider registration instructions in the [Prerequisites](connect.md#prerequisites) section.

If you have existing SQL Server - Azure Arc resources, use these steps to migrate them to Microsoft.AzureArcData namespace.

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
>
> - To paste the commands into the shell, use `Ctrl-Shift-V` on Windows or `Cmd-v` on MacOS.
> - The `curl` command will copy the script directly to the home folder associated with your Cloud Shell session.
> - The script will prompt for the resource group name and print a message when migration is completed.

### Other changes

- The *TCPPorts* property in the **SQL Server - Azure Arc** resource type has been renamed to *TCPStaticPorts*
- The permissions required aren't as broad as they used to be. See the [Required permission](overview.md#required-permissions) section for details.

### Known issues

- The *CreateTime* property won't be added to any newly created resources in the AzureArcData namespace, including the **SQL Server - Azure Arc** resources.

## October 2020

The October update includes the following improvements:

- The register Azure Arc-enabled SQL Server pane now includes the **Tags** tab. The tags are included in the registration script and are reflected in the **SQL Server - Azure Arc** resource(s). For details, see [Connect your SQL Server to Azure Arc](connect.md).

- The **Environment Health** entry now supports activation of **SQL Assessment** from the Portal by deploying a *CustomScriptExtension*. For details, see [Configure SQL Assessment](assess.md#run-on-demand-sql-assessment).

### Known issues

The following issues apply to the October release:

- Connecting SQL Server instances to Azure Arc requires an account with a broad set of permissions. For details, see [Required permissions](overview.md#required-permissions).

## September 2020

Azure Arc-enabled SQL Server is released for public preview. Azure Arc-enabled SQL Server extends Azure services to SQL Server instances hosted outside of Azure in the customer's datacenter, on the edge or in a multicloud environment.

For details, see [Azure Arc-enabled SQL Server Overview](overview.md)

### Known issues

The following issues apply to the September release:

- The **Register Azure Arc-enabled SQL Server** pane doesn't support configuring custom tags. To add custom tags, open the **SQL Server - Azure Arc** resource after registration and change Tags in the **Overview** page.

- Connecting SQL Server instances to Azure Arc requires an account with a broad set of permissions. For details, see [Required permissions](overview.md#required-permissions).

## Next steps

**Just want to try things out?**  Get started quickly with [Azure Arc-enabled SQL Server Jumpstart](https://aka.ms/AzureArcSqlServerJumpstart).
