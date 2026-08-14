---
title: Upgrade Integration Services Packages
description: Upgrade Integration Services Packages
ms.reviewer: randolphwest
ms.date: 08/13/2026
ms.service: sql
ms.subservice: integration-services
ms.topic: upgrade-and-migration-article
ms.custom:
  - sfi-ropc-nochange
helpviewer_keywords:
  - "Integration Services, migrating"
  - "migrating packages [Integration Services]"
---
# Upgrade Integration Services packages

[!INCLUDE [sqlserver-ssis](../../includes/applies-to-version/sqlserver-ssis.md)]

When you upgrade older versions of [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] to the current release, your existing SQL Server Integration Services (SSIS) packages might not be automatically upgraded to the package format that the current release uses. In this scenario, you must select an upgrade method and manually upgrade your packages.

> [!IMPORTANT]  
> When you upgrade [!INCLUDE [sssql11-md](../../includes/sssql11-md.md)] to [!INCLUDE [sssql14-md](../../includes/sssql14-md.md)] or later versions, ensure that you remove the original [!INCLUDE [sssql11-md](../../includes/sssql11-md.md)] instance after the upgrade. If you subscribe to [Extended Security Updates for SQL Server](../../sql-server/end-of-support/sql-server-extended-security-updates.md), you're billed for both instances.

For information on upgrading packages when you convert a project to the project deployment model, see [Deploy Integration Services (SSIS) Projects and Packages](../packages/deploy-integration-services-ssis-projects-and-packages.md).

## Select an upgrade method

You can use various methods to upgrade SSIS packages in older versions of [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)]. For some of these methods, the upgrade is only temporary. For others, the upgrade is permanent. The following table describes each of these methods and whether the upgrade is temporary or permanent.

> [!NOTE]  
> When you run a [!INCLUDE [ssSQL14](../../includes/sssql14-md.md)] or earlier version package by using the **`dtexec`** utility (`dtexec.exe`) that is installed with the current release of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], the temporary package upgrade increases the execution time. The rate of increase in package execution time varies depending on the size of the package. To avoid an increase in the execution time, upgrade the package before running it.

For Script components that reference SSIS-related assemblies which bind with version, the upgrade process keeps these components unchanged. You must manually update the reference to the new version.

| Upgrade method | Type of upgrade |
| --- | --- |
| Use the **`dtexec`** utility (dtexec.exe) that is installed with the current release of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] to run a [!INCLUDE [sql2008-md](../../includes/sql2008-md.md)], [!INCLUDE [sql2008r2](../../includes/sql2008r2-md.md)], [!INCLUDE [ssSQL11](../../includes/sssql11-md.md)], or [!INCLUDE [ssSQL14](../../includes/sssql14-md.md)] package.<br /><br />For more information, see [dtexec Utility](../packages/dtexec-utility.md). | The package upgrade is temporary.<br /><br />The changes can't be saved. |
| Open a [!INCLUDE [sql2008-md](../../includes/sql2008-md.md)], [!INCLUDE [sql2008r2](../../includes/sql2008r2-md.md)], [!INCLUDE [ssSQL11](../../includes/sssql11-md.md)], or [!INCLUDE [ssSQL14](../../includes/sssql14-md.md)] package file in [!INCLUDE [ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)]. | The package upgrade is permanent if you save the package; otherwise, it's temporary if you don't save the package. |
| Add a [!INCLUDE [sql2008-md](../../includes/sql2008-md.md)], [!INCLUDE [sql2008r2](../../includes/sql2008r2-md.md)], [!INCLUDE [ssSQL11](../../includes/sssql11-md.md)], or [!INCLUDE [ssSQL14](../../includes/sssql14-md.md)] package to an existing project in [!INCLUDE [ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)]. | The package upgrade is permanent. |
| Open a [!INCLUDE [ssISversion10](../../includes/ssisversion10-md.md)] or later project file in [!INCLUDE [vsprvs](../../includes/vsprvs-md.md)], and then use the [!INCLUDE [ssIS](../../includes/ssis-md.md)] Package Upgrade Wizard to upgrade multiple packages in the project.<br /><br />For more information, see [Upgrade Integration Services Packages Using the SSIS Package Upgrade Wizard](upgrade-integration-services-packages-using-the-ssis-package-upgrade-wizard.md) and [SSIS Package Upgrade Wizard F1 Help](../ssis-package-upgrade-wizard-f1-help.md). | The package upgrade is permanent. |
| Use the <xref:Microsoft.SqlServer.Dts.Runtime.Application.Upgrade%2A> method to upgrade one or more [!INCLUDE [ssISnoversion](../../includes/ssisnoversion-md.md)] packages. | The package upgrade is permanent. |

## Custom applications and custom components

[!INCLUDE [ssISversion2005](../../includes/ssisversion2005-md.md)] custom components don't work with the current release of SSIS.

However, you can use the current release of SSIS tools to run and manage packages for custom components from [!INCLUDE [sql2008-md](../../includes/sql2008-md.md)] through [!INCLUDE [sssql14-md](../../includes/sssql14-md.md)]. To help redirect the runtime assemblies from version 10.0.0.0 ([!INCLUDE [sql2008r2](../../includes/sql2008r2-md.md)]), version 11.0.0.0 ([!INCLUDE [ssSQL11](../../includes/sssql11-md.md)]), or version 12.0.0.0 ([!INCLUDE [ssSQL14](../../includes/sssql14-md.md)]) to version 15.0.0.0 ([!INCLUDE [ssSQL19](../../includes/sssql19-md.md)]), four binding redirection rules are added to the following files:

- DTExec.exe.config
- dtshost.exe.config
- DTSWizard.exe.config
- DTUtil.exe.config
- DTExecUI.exe.config

To use [!INCLUDE [ssBIDevStudio](../../includes/ssbidevstudio-md.md)] to design packages that include custom components for [!INCLUDE [ssSQL14](../../includes/sssql14-md.md)] and earlier versions, modify the `devenv.exe.config` file located at `<drive>:\Program Files\Microsoft Visual Studio 10.0\Common7\IDE`.

To use these packages with custom applications built with the runtime for [!INCLUDE [ssSQL19](../../includes/sssql19-md.md)], include redirection rules in the configuration section of the `*.exe.config` file for the executable. The rules redirect the runtime assemblies to version 15.0.0.0 ([!INCLUDE [ssSQL19](../../includes/sssql19-md.md)]). For more information about assembly version redirection, see [\<assemblyBinding> Element for \<runtime>](/dotnet/framework/configure-apps/file-schema/runtime/assemblybinding-element-for-runtime).

### Locate the assemblies

In [!INCLUDE [ssSQL19](../../includes/sssql19-md.md)], the [!INCLUDE [ssISnoversion](../../includes/ssisnoversion-md.md)] assemblies were upgraded to .NET 4.0. There's a separate global assembly cache for .NET 4, located in `<drive>:\Windows\Microsoft.NET\assembly`. You can find all of the [!INCLUDE [ssISnoversion](../../includes/ssisnoversion-md.md)] assemblies under this path, usually in the `GAC_MSIL` folder.

As in previous versions of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], the core [!INCLUDE [ssISnoversion](../../includes/ssisnoversion-md.md)] extensibility `.dll` files are also located at `<drive>:\Program Files\Microsoft SQL Server\130\SDK\Assemblies`.

## Understand SQL Server package upgrade results

During the package upgrade process, most components and features in packages from [!INCLUDE [ssSQL14](../../includes/sssql14-md.md)] and earlier versions, convert seamlessly to their counterparts in the current release of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. However, there are several components and features that either aren't upgraded, or have upgrade results of which you should be aware.

To identify which packages have the issues listed in this section, run Upgrade Advisor.

### Connection strings

For packages in [!INCLUDE [ssSQL14](../../includes/sssql14-md.md)] and earlier versions, the names of certain providers have changed and require different values in the connection strings. To update the connection strings, use one of the following procedures:

- Use the [!INCLUDE [ssIS](../../includes/ssis-md.md)] Package Upgrade Wizard to upgrade the package, and select the **Update connection strings to use new provider names** option.
- In [!INCLUDE [ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)], on the General page of the Options dialog box, select the **Update connection strings to use new provider names** option. For more information about this option, see General Page.
- In [!INCLUDE [ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)], open the package and manually change the text of the ConnectionString property.

You can't use these procedures to update a connection string when the connection string is stored in either a configuration file or a data source file, or when an expression sets the **ConnectionString** property. To update the connection string in these cases, you must manually update the file or the expression. For more information about data sources, see [Data Sources for [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] packages](../connection-manager/data-sources.md).

### Scripts that depend on `ADODB.DLL`

Script Task and Script Component scripts that explicitly reference `ADODB.DLL` might not upgrade or run on machines without [!INCLUDE [ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or [!INCLUDE [ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)] installed. To upgrade these Script Task or Script Component scripts, remove the dependency on `ADODB.DLL`. Use ADO.NET as the alternative for managed code such as VB and C# scripts.
