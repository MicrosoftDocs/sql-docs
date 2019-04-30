---
title: "Upgrade Integration Services Packages | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
helpviewer_keywords: 
  - "Integration Services, migrating"
  - "migrating packages [Integration Services]"
ms.assetid: 68dbdf81-032c-4a73-99f6-41420e053980
author: "MikeRayMSFT"
ms.author: "mikeray"
manager: "erikre"
---
# Upgrade Integration Services Packages

[!INCLUDE[ssis-appliesto](../../includes/ssis-appliesto-ssvrpluslinux-asdb-asdw-xxx.md)]


  When you upgrade an instance of [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] to the current release of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], your existing [!INCLUDE[ssISversion10](../../includes/ssisversion10-md.md)] packages are not automatically upgraded to the package format that the current release [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)][!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] uses. You will have to select an upgrade method and manually upgrade your packages.  
  
 For information on upgrading packages when you convert a project to the project deployment model, see [Deploy Integration Services (SSIS) Projects and Packages](../../integration-services/packages/deploy-integration-services-ssis-projects-and-packages.md)
  
## Selecting an Upgrade Method  
 You can use various methods to upgrade [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)], [!INCLUDE[ssKilimanjaro](../../includes/sskilimanjaro-md.md)], [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)], or [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] packages. For some of these methods, the upgrade is only temporary. For others, the upgrade is permanent. The following table describes each of these methods and whether the upgrade is temporary or permanent.  
  
> [!NOTE]  
>  When you run a [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)], [!INCLUDE[ssKilimanjaro](../../includes/sskilimanjaro-md.md)], [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)], or [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] package using the **dtexec** utility (dtexec.exe) that is installed with the current release of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], the temporary package upgrade increases the execution time. The rate of increase in package execution time varies depending on the size of the package. To avoid an increase in the execution time, it is recommended that you upgrade the package before running it.  
  
|Upgrade Method|Type of Upgrade|  
|--------------------|---------------------|  
|Use the **dtexec** utility (dtexec.exe) that is installed with the current release of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] to run a [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)], [!INCLUDE[ssKilimanjaro](../../includes/sskilimanjaro-md.md)], [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)], or [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] package.<br /><br /> For more information, see [dtexec Utility](../../integration-services/packages/dtexec-utility.md).|The package upgrade is temporary.<br /><br /> The changes cannot be saved.|  
|Open a [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)], [!INCLUDE[ssKilimanjaro](../../includes/sskilimanjaro-md.md)], [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)], or [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] package file in [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)].|The package upgrade is permanent if you save the package; otherwise, it is temporary if you do not save the package.|  
|Add a [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)], [!INCLUDE[ssKilimanjaro](../../includes/sskilimanjaro-md.md)], [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)], or [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] package to an existing project in [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)].|The package upgrade is permanent.|  
|Open a [!INCLUDE[ssISversion10](../../includes/ssisversion10-md.md)] or later project file in [!INCLUDE[vsprvs](../../includes/vsprvs-md.md)], and then use the [!INCLUDE[ssIS](../../includes/ssis-md.md)] Package Upgrade Wizard to upgrade multiple packages in the project.<br /><br /> For more information, see [Upgrade Integration Services Packages Using the SSIS Package Upgrade Wizard](../../integration-services/install-windows/upgrade-integration-services-packages-using-the-ssis-package-upgrade-wizard.md) and [SSIS Package Upgrade Wizard F1 Help](../../integration-services/ssis-package-upgrade-wizard-f1-help.md).|The package upgrade is permanent.|  
|Use the <xref:Microsoft.SqlServer.Dts.Runtime.Application.Upgrade%2A> method to upgrade one or more [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] packages.|The package upgrade is permanent.|  
  
## Custom Applications and Custom Components  
 [!INCLUDE[ssISversion2005](../../includes/ssisversion2005-md.md)] custom components will not work with the current release of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)][!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)].  
  
 You can use the current release of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)][!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] tools to run and manage packages that include [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)], [!INCLUDE[ssKilimanjaro](../../includes/sskilimanjaro-md.md)], [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)], or [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)][!INCLUDE[ssIS](../../includes/ssis-md.md)] custom components. We added four binding redirection rules to the following files to help redirect the runtime assemblies from version 10.0.0.0 ( [!INCLUDE[ssKilimanjaro](../../includes/sskilimanjaro-md.md)]), version 11.0.0.0 ( [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)]), or version 12.0.0.0 ( [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)]) to version 13.0.0.0 ( [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)]).  
  
-   DTExec.exe.config  
  
-   dtshost.exe.config  
  
-   DTSWizard.exe.config  
  
-   DTUtil.exe.config  
  
-   DTExecUI.exe.config  
  
 To use [!INCLUDE[ssBIDevStudio](../../includes/ssbidevstudio-md.md)] to design packages that include [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)], [!INCLUDE[ssKilimanjaro](../../includes/sskilimanjaro-md.md)], [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)], or [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] custom components, you need to modify the devenv.exe.config file that is located at *\<drive>*:\Program Files\Microsoft Visual Studio 10.0\Common7\IDE.  
  
 To use these packages with customer applications that are built with the runtime for [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)], include redirection rules in the configuration section of the *.exe.config file for the executable. The rules redirect the runtime assemblies to version 13.0.0.0 ([!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)]). For more information about assembly version redirection, see [\<assemblyBinding> Element for \<runtime>](https://msdn.microsoft.com/library/twy1dw1e.aspx).  
  
### Locating the Assemblies  
 In [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)], the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] assemblies were upgraded to .NET 4.0. There is a separate global assembly cache for .NET 4, located in *\<drive>*:\Windows\Microsoft.NET\assembly. You can find all of the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] assemblies under this path, usually in the GAC_MSIL folder.  
  
 As in previous versions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], the core [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] extensibility .dll files are also located at *\<drive>*:\Program Files\Microsoft SQL Server\130\SDK\Assemblies.  
  
## Understanding SQL Server Package Upgrade Results  
 During the package upgrade process, most components and features in [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)], [!INCLUDE[ssKilimanjaro](../../includes/sskilimanjaro-md.md)], [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)], or [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] packages convert seamlessly to their counterparts in the current release of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. However, there are several components and features that either will not be upgraded or have upgrade results of which you should be aware. The following table identifies these components and features.  
  
> [!NOTE]  
>  To identify which packages have the issues listed in this table, run Upgrade Advisor.  
  
|Component or Feature|Upgrade Results|  
|--------------------------|---------------------|  
|Connection strings|For [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)], [!INCLUDE[ssKilimanjaro](../../includes/sskilimanjaro-md.md)], [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)], or [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] packages, the names of certain providers have changed and require different values in the connection strings. To update the connection strings, use one of the following procedures:<br /><br /> Use the [!INCLUDE[ssIS](../../includes/ssis-md.md)] Package Upgrade Wizard to upgrade the package, and select the **Update connection strings to use new provider names** option.<br /><br /> In [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)], on the General page of the Options dialog box, select the **Update connection strings to use new provider names** option. For more information about this option, see General Page.<br /><br /> In [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)], open the package and manually change the text of the ConnectionString property.<br /><br /> Note: You cannot use the previous procedures to update a connection string when the connection string is stored in either a configuration file or a data source file, or when an expression sets the **ConnectionString** property. To update the connection string in these cases, you must manually update the file or the expression.<br /><br /> For more information about data sources, see [Data Sources](../../integration-services/connection-manager/data-sources.md).|  
  
### Scripts that Depend on ADODB.dll  
 Script Task and Script Component scripts that explicitly reference ADODB.dll may not upgrade or run on machines without [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)] installed. In order to upgrade these Script Task or Script Component scripts, it is recommended that you remove the dependency on ADODB.dll.  Ado.Net is the recommended alternative for managed code such as VB and C# scripts.  
  
  
