---
title: "Deployment and Version Support in SQL Server Data Tools (SSRS) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "reporting-services-native"
ms.topic: conceptual
ms.assetid: 36f5686d-7e40-4f31-be81-bd197ca33a02
author: maggiesMSFT
ms.author: maggies
manager: kfile
---
# Deployment and Version Support in SQL Server Data Tools (SSRS)
  [!INCLUDE[ssBIDevStudio](../../includes/ssbidevstudio-md.md)] supports the following scenarios:  
  
-   Open report definitions (*.rdl) and report server projects (\*.rptproj).  
  
-   Build report definitions.  
  
-   Preview reports in Report Designer.  
  
-   Deploy reports to report servers.  
  
##  <a name="bkmk_ConfigurationandDeploymentProperties"></a> Configuration and Deployment Properties  
 [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)] supports project configurations. A project configuration consists of a set of properties that specify locations and behaviors when a project is built either as a step in previewing or deploying reports. To learn more about project configurations, see the Visual Studio documentation.  
  
 Use project configurations to control the upgrade of report definitions to schema versions compatible with target report servers. The properties controlled by project configurations include the target report server, the folder where the build process temporarily stores report definitions for preview and deployment, and error levels.  
  
 Reports are built before they are rendered as previews in Report Designer or deployed to the report server.  
  
 You set the configuration properties in the [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)] **Project Property** dialog box.  
  
 The build and deployment properties include:  
  
-   OutputPath is a build property that identifies the path of folders to store the report definition used in build verification, deployment, and preview of reports.  
  
-   ErrorLevel is a build property that identifies the severity of the build issues that are reported as errors. Issues with severity levels less than or equal to the value of ErrorLevel are reported as errors; otherwise, the issues are reported as warnings. For more information, see The "Report Validation and Error Levels" section in [Design Reports with Report Designer &#40;SSRS&#41;](design-reporting-services-paginated-reports-with-report-designer-ssrs.md).  
  
-   TargetServerVersion is a deployment property that identifies the expected version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] that is installed on the target report server specified in the TargetServerURL property.  
  
 When you specify the earlier version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] in the **Project Property** dialog box, the reports are not reverted automatically to the earlier version. As such, a Report Server project can contain reports from the two different versions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. When the Report Server project is deployed, all reports in the project are converted to the version specified in TargetServerVersion.  
  
 You can add more than one project configuration to a project; each one is used for a different scenario, such as deploying to different versions of report servers. For more information, see [Set Deployment Properties &#40;Reporting Services&#41;](set-deployment-properties-reporting-services.md) and [Project Property Pages Dialog Box](project-property-pages-dialog-box.md).  
  
##  <a name="bkmk_SupportedVersions"></a> Supported Versions  
  
> [!NOTE]  
>  [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)], the 32-bit development environment for Report Server projects, is not designed to run on [!INCLUDE[vcpritanium](../../includes/vcpritanium-md.md)]-based computers and is not installed on [!INCLUDE[vcpritanium](../../includes/vcpritanium-md.md)]-based servers. However, support for [!INCLUDE[ssBIDevStudio](../../includes/ssbidevstudio-md.md)] is available for x64-based computers.  
  
 The following table describes the supported versions for authoring and publishing reports in [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)].  
  
> [!NOTE]  
>  The schema has not changed since [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)].  
  
|Project or File type|Version|Author Reports|Publish Reports|Notes|  
|--------------------------|-------------|--------------------|---------------------|-----------|  
|Report Server Project<br /><br /> or<br /><br /> Report Server Wizard Project|[!INCLUDE[ssSQL14](../../includes/sssql14-md.md)]|2014 RDL schema|[!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)]||  
|Report Server Project<br /><br /> or<br /><br /> Report Server Wizard Project|[!INCLUDE[ssSQL11](../../includes/sssql11-md.md)]|2012 RDL schema|[!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)]||  
|Report Server Project<br /><br /> or<br /><br /> Report Server Wizard Project|[!INCLUDE[ssKilimanjaro](../../includes/sskilimanjaro-md.md)]|2008 R2 RDL schema|[!INCLUDE[ssKilimanjaro](../../includes/sskilimanjaro-md.md)] [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)]||  
|Report Server Project<br /><br /> or<br /><br /> Report Server Wizard Project|[!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)]|2008 RDL schema|[!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] report server only|Upgrades 2003 RDL and 2005 RDL to the 2008 RDL schema locally.|  
|Report Server Project<br /><br /> or<br /><br /> Report Server Wizard Project|[!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)]|2005 RDL schema|[!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] or [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] report server||  
|Report Server Project|[!INCLUDE[ssVersion2000](../../includes/ssversion2000-md.md)]|2003 RDL schema|Not supported||  
|[!INCLUDE[vsprvs](../../includes/vsprvs-md.md)] RDLC Report Designer|[!INCLUDE[vsprvs](../../includes/vsprvs-md.md)] 2005<br /><br /> [!INCLUDE[vsprvs](../../includes/vsprvs-md.md)] 2008|2005 RDL schema|Not supported|Does not support 2008 RDL schema.|  
|[!INCLUDE[vsprvs](../../includes/vsprvs-md.md)] Viewer controls|[!INCLUDE[vsprvs](../../includes/vsprvs-md.md)] 2005<br /><br /> [!INCLUDE[vsprvs](../../includes/vsprvs-md.md)] 2008|2008 RDL not supported in local mode|N/A|Can view 2008 RDL reports on [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] report server in server mode.|  
  
 For more information about opening reports in a previous version of the report definition schema, see [Upgrade Reports](../install-windows/upgrade-reports.md). For more information about specific report definition schemas, see [Report Definition Language Specification](https://go.microsoft.com/fwlink/?linkid=116865).  
  
## See Also  
 [Publishing Data Sources and Reports](../reports/publishing-data-sources-and-reports.md)  
  
  
