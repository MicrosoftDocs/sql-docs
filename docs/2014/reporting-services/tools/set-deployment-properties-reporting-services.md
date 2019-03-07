---
title: "Set Deployment Properties (Reporting Services) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "reporting-services-native"
ms.topic: conceptual
helpviewer_keywords: 
  - "reports [Reporting Services], deploying"
  - "publishing reports [Reporting Services]"
  - "properties [Reporting Services], deployment"
  - "deploying reports [Reporting Services]"
ms.assetid: 18201ca0-bf4a-484f-b3a2-95d1046a6a9b
author: markingmyname
ms.author: maghan
manager: kfile
---
# Set Deployment Properties (Reporting Services)
  In[!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)], you must specify the report server and optionally the folders for reports and shared data sources so that you can publish the items in a Report Server project to a report server. The properties and values that [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)] needs to build, preview an deploy reports are stored in project configurations of the Report Server project. You can create multiple named sets for these project properties, so that you can conveniently switch between property sets. Each set of properties is a configuration. For example, you can have a configuration for publishing reports to a test server and a different configuration for publishing reports to a production server.  
  
 Use Configuration Manager to create and manage sets of project properties in project configurations. Configuration Manager is a feature supported by [!INCLUDE[vsprvs](../../includes/vsprvs-md.md)], on which [!INCLUDE[ssBIDevStudio](../../includes/ssbidevstudio-md.md)] is based.  
  
> [!NOTE]  
>  Do not confuse this feature with the Reporting Services Configuration Manager, which is used to configure Reporting Services after installation. For more information, see [Configure and Administer a Report Server &#40;SSRS Native Mode&#41;](../report-server/configure-and-administer-a-report-server-ssrs-native-mode.md).  
  
> [!NOTE]  
>  In [!INCLUDE[ssBIDevStudio](../../includes/ssbidevstudio-md.md)], the action of publishing reports from a Report Server project or solution is known as *deploying reports*.  
  
### To set deployment properties  
  
1.  Right-click the report project, and then click **Properties**.  
  
2.  In the **Property Pages** dialog box for the project, select a configuration to edit from the **Configuration** list. Common configurations are **DebugLocal**, **Debug**, and **Release**.  
  
    > [!NOTE]  
    >  You can use multiple configurations to switch quickly between different report servers or settings.  
  
3.  In the **OutputPath** textbox, type or paste the path in your local file system to store the report definition used in build verification, deployment, and preview of reports. The path must be different than the path that you use for the project and a relative path that is a child folder under the path of the project.  
  
4.  In the **ErrorLevel** text box, type the severity of the build issues that are reported as errors. Issues occurring when building reports, data sources, or other project resources with severity levels less than or equal to the value of **ErrorLevel** are reported as errors; otherwise, the issues are reported as warnings. Any error will cause the build task to fail. The valid severity levels are 0 through 4 inclusive. The default value is 2.  
  
     **ErrorLevel** can be used to increase or decrease the sensitivity of the build. For example, when a report with a map is built during deployment to a [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] report server an error displays by default and building the report fails. If you lower **ErrorLevel** the map is removed from the report, a warning displays, and building the report continues.  
  
5.  In the **StartItem** list, select a report to display in the preview window or in a browser window when the report project is run.  
  
6.  In the **OverwriteDataSources** list, select **True** to overwrite the shared data source on the server each time shared data sources are published, or select **False** to keep the data source on the server.  
  
7.  In the **TargetServerVersion** list, select either the [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] or [!INCLUDE[ssKilimanjaro](../../includes/sskilimanjaro-md.md)] version of [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] or select **Detect Version** to automatically determine the version installed on the server identified by the **TargetServer URL** property. The default value is **SQL Server 2008 R2**.  
  
     Use **TargetServerVersion** to customize the built reports, placed in the path specified in OutputPath, for the version of the report server specified in **TargetServer URL**.  
  
8.  In the **TargetDataSourceFolder** text box, type the folder on the report server in which to place the published shared data sources. The default value for **TargetDataSourceFolder** is Data Sources. If you leave this value blank, the data sources will be published to the location specified in **TargetReportFolder**.  
  
9. In the **TargetReportFolder** text box, type the folder on the report server in which to place the published reports. The default value for **TargetReportFolder** is the name of the report project.  
  
    > [!NOTE]  
    >  For a report server running in native mode, you must have **Publish** permissions on the target folder to publish reports to that folder. Publish permissions are provided through a role assignment that maps your user account to a role that includes publish operations. For more information, see [Create and Manage Role Assignments](../security/create-and-manage-role-assignments.md). For a report server running in SharePoint integrated mode, you must have **Member** or **Owner** permission on the SharePoint site. For more information, see [SharePoint Site and List Permission Reference for Report Server Items](../security/sharepoint-site-and-list-permission-reference-for-report-server-items.md).  
  
10. In the **TargetServerURL** text box, type the URL of the target report server. Before you publish a report, you must set this property to a valid report server URL. When publishing to a report server running in native mode, use the URL of the virtual directory of the report server (for example, http:*//server/reportserver* or https:*//server/reportserver)*. This is the virtual directory of the report server, not Report Manager.  
  
     When publishing to a report server running in SharePoint integrated mode, use a URL to a SharePoint top-level site or subsite. If you do not specify a site, the default top-level site is used (for example, http://*servername*, http://*servername*/*site* or http://*servername*/*site*/*subsite*).  
  
### To set Configuration Manager properties  
  
1.  Right-click the report project, and then click **Properties**.  
  
2.  In the **Property Pages** dialog box for the project, click **Configuration Manager**.  
  
3.  In the **Configuration Manager** dialog box, select the configuration to edit. The currently active configuration is displayed as **Active(***\<configuration>***)**.  
  
4.  In **Project Contexts**, for each project in the solution, select or clear **Build** or **Deploy**.  
  
    > [!NOTE]  
    >  If **Build** is selected, Report Designer builds the report project and checks for errors before previewing or publishing to a report server. If **Deploy** is selected, Report Designer publishes the reports to the report server as defined in deployment properties. If **Deploy** is not selected, Report Designer displays the report specified in the **StartItem** property in a local preview window.  
  
## See Also  
 [Publishing Data Sources and Reports](../reports/publishing-data-sources-and-reports.md)   
 [Previewing Reports](../reports/previewing-reports.md)   
 [Report Designer F1 Help](report-designer-f1-help.md)   
 [URL Examples for Published Report Items on a Report Server in SharePoint Mode &#40;SSRS&#41;](url-examples-for-items-on-a-report-server-sharepoint-mode.md)   
 [Project Property Pages Dialog Box](project-property-pages-dialog-box.md)   
 [Publishing Reports to a Report Server](../reports/publishing-reports-to-a-report-server.md)  
  
  
