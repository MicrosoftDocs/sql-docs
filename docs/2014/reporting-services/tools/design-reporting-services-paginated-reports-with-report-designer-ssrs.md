---
title: "Design Reports with Report Designer (SSRS) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "reporting-services-native"
ms.topic: conceptual
helpviewer_keywords: 
  - "Report Designer [Reporting Services], report creation"
ms.assetid: 3a26dccc-6ad6-48f5-a882-f96c6c0dd405
author: maggiesMSFT
ms.author: maggies
manager: kfile
---
# Design Reports with Report Designer (SSRS)
  Use Report Designer to create full-featured [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] reports and reporting solutions. Report Designer provides a graphical interface in which you can define data sources, datasets and queries, report layout positions for data regions and fields, and interactive features such as parameters and sets of reports that work together.  
  
## Benefits for Projects  
 Use projects to:  
  
-   Organize reports and related items in one container.  
  
-   Test report solutions that include reports and related items locally.  
  
-   Deploy related items together. Use project properties and configuration management to deploy to multiple environments.  
  
-   Preserve a set of master copies for reports and related items. After deployment, published reports can be accidentally modified.  
  
 Use the information in this topic to design reports and related items for a single reporting project in a [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)] solution. For more information about solutions and multiple projects in [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)], see [Reporting Services in SQL Server Data Tools &#40;SSDT&#41;](reporting-services-in-sql-server-data-tools-ssdt.md).  
  
##  <a name="bkmk_SharedDataSources"></a> Shared Data Sources  
 Use [!INCLUDE[ssBIDevStudio](../../includes/ssbidevstudio-md.md)] to define and deploy shared data sources for a reporting solution. Shared data sources can be deployed independently from other items in a project by using the **OverwriteDataSources** and **TargetDataSourceFolder** properties. For more information, see [Set Deployment Properties &#40;Reporting Services&#41;](set-deployment-properties-reporting-services.md).  
  
 In Report Designer, you work in both the Report Data pane and in Solution Explorer to define the data sources used in a report. For more information, see [Report Data Pane](reporting-services-in-sql-server-data-tools-ssdt.md#bkmk_ReportDataPane). You cannot use [!INCLUDE[ssBIDevStudio](../../includes/ssbidevstudio-md.md)] to open data sources that are published to  a report server or SharePoint site, but not included in the [!INCLUDE[ssBIDevStudio](../../includes/ssbidevstudio-md.md)] solution. For that feature, use [Report Builder &#40;SSRS&#41;](report-builder-authoring-environment-ssrs.md).  
  
 [!INCLUDE[ssBIDevStudio](../../includes/ssbidevstudio-md.md)] is a client tool. You can test your reporting solution locally on your computer, deploy it to a test environment for testing the server solution, and then deploy it to a production environment. After deployment, verify that the data source processing extensions and data source credentials are configured for the report server environment. You can use Configuration Manager to help manage the properties for different deployments. For more information, see [Reporting Services in SQL Server Data Tools &#40;SSDT&#41;](reporting-services-in-sql-server-data-tools-ssdt.md).  
  
 For more information, see [Data Connections, Data Sources, and Connection Strings in Reporting Services](../data-connections-data-sources-and-connection-strings-in-reporting-services.md).  
  
  
##  <a name="bkmk_SharedDatasets"></a> Shared Datasets  
 Use [!INCLUDE[ssBIDevStudio](../../includes/ssbidevstudio-md.md)] to define and deploy shared datasets for a reporting solution. Shared datasets can be deployed independently from other items in a project by using the **OverwriteDatasets** and **TargetDatasetFolder** properties. For more information, see [Set Deployment Properties &#40;Reporting Services&#41;](set-deployment-properties-reporting-services.md).  
  
 In Report Designer, you work in both the Report Data pane and in Solution Explorer to define shared datasets used in a report. For more information, see [Report Data Pane](reporting-services-in-sql-server-data-tools-ssdt.md#bkmk_ReportDataPane). You cannot use [!INCLUDE[ssBIDevStudio](../../includes/ssbidevstudio-md.md)] to open published datasets directly from a report server or SharePoint site. For that feature, use [Report Builder &#40;SSRS&#41;](report-builder-authoring-environment-ssrs.md) in Shared Dataset mode.  
  
 [!INCLUDE[ssBIDevStudio](../../includes/ssbidevstudio-md.md)] is a client tool. You can use query designers to help create and test your query results locally in Preview. After deployment, you can manage shared datasets independently from the shared data sources and reports that they depend on. For more information, see [Report Embedded Datasets and Shared Datasets &#40;Report Builder and SSRS&#41;](../report-data/report-embedded-datasets-and-shared-datasets-report-builder-and-ssrs.md), [Query Design Tools in Report Designer SQL Server Data Tools &#40;SSRS&#41;](../report-data/query-design-tools-ssrs.md), and [Manage Shared Datasets](../report-data/manage-shared-datasets.md).  
  
  
##  <a name="bkmk_Reports"></a> Reports  
 Reports are files that are stored in a report project. Reports can be used as stand-alone reports, subreports, or the targets for drillthrough actions from main reports. Reports can be deployed independently from other items in a project by using **TargetReportFolder** and other properties. For more information, see [Set Deployment Properties &#40;Reporting Services&#41;](set-deployment-properties-reporting-services.md).  
  
> [!NOTE]  
>  If you are publishing to a report server in SharePoint mode, some report solution features cannot be tested in the Report Designer project. References to reports, subreports, and drillthrough reports must use fully-qualified URLs that can be tested only after you deploy the report project. For more information, see [URL Examples for Published Report Items on a Report Server in SharePoint Mode &#40;SSRS&#41;](url-examples-for-items-on-a-report-server-sharepoint-mode.md).  
  
 You can add reports to a project in the following ways:  
  
-   **Add a new report project.** By default, a blank report opens in Report Designer. For more information, see [Add a New or Existing Report to a Report Project &#40;SSRS&#41;](add-a-new-or-existing-report-to-a-report-project-ssrs.md).  
  
-   **Add a Report Wizard project.** You create a report in a guided step-by-step manner. The Report Wizard simplifies data definition and report design into a series of steps that give you a finished report. You can add styles to customize the wizard for your own organization. For more information, see [Add a New or Existing Report to a Report Project &#40;SSRS&#41;](add-a-new-or-existing-report-to-a-report-project-ssrs.md).  
  
-   **Add a new item of type Report.** A blank report opens in Report Designer.  
  
-   **Add an existing item.** An existing report definition (.rdl) opens in Report Designer. Opening a report or project from an earlier version of [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] might automatically upgrade the project to the current version and the report to the current schema. For more information, see [Upgrade Reports](../install-windows/upgrade-reports.md).  
  
-   **Import a [!INCLUDE[msCoName](../../includes/msconame-md.md)] Access report.** Import all reports from an Access database (.mdb, .accdb) or project (.adp) file. Report Designer converts each report in a database or project file to RDL and saves it in the report project. Not all of the functionality of an Access report transfers to a report definition (.rdl) file. For more information, see [Import Reports from Microsoft Access &#40;Reporting Services&#41;](../import-reports-from-microsoft-access-reporting-services.md) and [Supported Access Report Features &#40;SSRS&#41;](../supported-access-report-features-ssrs.md).  
  
    > [!NOTE]  
    >  You must have Access 2002 or a later version installed on the same computer that Report Designer is installed on in order to use the import feature. The data source for the Access reports must be available when the reports are imported.  
  
-   **Work directly in RDL.** When you write a report in Report Designer, the report is saved in XML format as a Report Definition Language (RDL) file. You can edit this file in Report Designer, a text editor, or any tool in which you can edit XML.  
  
     When you edit the report definition source in Report Designer, you are working in the current RDL schema for the version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] from which you installed the development tools. When you build a project, the schema version might change depending on your deployment properties. For more information, see [Deployment and Version Support in SQL Server Data Tools &#40;SSRS&#41;](deployment-and-version-support-in-sql-server-data-tools-ssrs.md).  
  
     Editing RDL directly can result in a report that cannot be published to the report server or cannot run. As with any XML file, ensure that XML-specific characters used within elements are properly encoded. When you publish the report, the report server uses the schema to validate the XML contained within the RDL file.  
  
     To include elements that are not part of the RDL schema, place them in the Custom Element. The Custom element can be read by custom rendering extensions, but is ignored by the rendering extensions provided with [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)]. For example, you can use the Custom element to store comments in the report.  
  
     For more information, see [Report Definition Language &#40;SSRS&#41;](../reports/report-definition-language-ssrs.md).  
  
  
##  <a name="bkmk_ReportParts"></a> Report Parts  
 In Report Designer, after you create tables, charts, and other report items in a project, you can publish them as *report parts* to a report server or SharePoint site integrated with a report server so that you and others can reuse them in other reports. For more information, see [Report Parts in Report Designer &#40;SSRS&#41;](../report-design/report-parts-in-report-designer-ssrs.md).  
  
 Report parts can be deployed independently from other items in a project by using **TargetReportPartFolder** and other properties. For more information, see [Set Deployment Properties &#40;Reporting Services&#41;](set-deployment-properties-reporting-services.md).  
  
  
##  <a name="bkmk_Resources"></a> Resources  
 You can add files to your project that are related to your report but not processed by the report server. For example, you can add images for pictures or ESRI shapefiles for spatial data. For more information, see [Resources](../report-server/report-server-content-management-ssrs-native-mode.md#bkmk_Resources).  
  
  
##  <a name="bkmk_ReportLayout"></a> Report Layout  
 To create the report layout, drag report items and data regions from the Toolbox to the design surface and arrange them. Drag dataset fields to the items on the design surface to add data to the report. To organize data in groups in a tablix data region, drag dataset fields to the Grouping pane. Because report authoring tools are essentially a way to create report definitions, the approach to report design is quite similar between Report Builder and Report Designer.  
  
  
##  <a name="bkmk_Preview"></a> Preview  
 Use **Preview** to verify the report data and layout design. When you preview a report, the report processor validates the report definition schema and expression syntax and lists issues in the [Output](reporting-services-in-sql-server-data-tools-ssdt.md#bkmk_Output) window.  
  
> [!NOTE]  
>  When you preview a report, the data for the report is cached to a file on the local computer. When you preview the same report again using the same query, parameters, and credentials, Report Designer retrieves the cached copy rather than rerunning the query. The data file is saved as *\<reportname>*.rdl.data in the same directory as the report definition file. The file is not deleted when you close Report Designer.  
  
 You can preview a report in the following ways:  
  
-   **Preview view.** Click the **Preview** tab. The report runs locally, using the same report processing and rendering functionality that is provided with the report server. The report that is displayed is an interactive image; you can select parameters, click links, view the document map, and expand and collapse hidden areas of the report. You can also export the report to any of the installed rendering formats.  
  
-   **Standalone Preview.** Run the local report in a browser. By using a debug configuration, you can also use this mode to debug custom assemblies that you write. There are three ways to run a project in Debug mode:  
  
    -   On the **Debug** menu, click **Start**.  
  
    -   On the [!INCLUDE[vsprvs](../../includes/vsprvs-md.md)] standard toolbar, click the **Start** button.  
  
    -   Press F5.  
  
     If you use a project configuration that builds the report but does not deploy it, the report that is specified in the `StartItem` property of the current configuration opens in a separate preview window.  
  
    > [!NOTE]  
    >  To use Debug mode, you must set a start item. In Solution Explorer, right-click the report project, click **Properties**, and in `StartItem`, select the name of the report to display.  
  
     If you wish to preview a particular report that is not the start item for the project, select a configuration that builds the report but does not deploy it (for example, the DebugLocal configuration), right-click the report, and then click **Run**. You must choose a configuration that does not deploy the report; otherwise, the report will be published to the report server instead of displayed locally in a preview window.  
  
-   **Print Preview.**  
  
     When you first view a report on in Preview mode or in the preview window, the view of the report resembles a report produced by the HTML rendering extension. The preview is not HTML, but the layout and pagination of the report is similar to HTML output.  
  
     You can change the view to represent a printed report by switching to print preview mode. Click the **Print Preview** button on the preview toolbar. The report will display as though it were on a physical page. This view resembles the output produced by the Image and PDF rendering extensions. Print preview is not an image or PDF file, but the layout and pagination of the report is similar the output in those formats. You can choose the size of the report image, for example, set the width of the page.  
  
     Print preview helps you identify many of rendering problems you might encounter were you to print the report. Common rendering problems include:  
  
    -   Extra blank pages because the report is too wide to fit on the paper size you specified for the report.  
  
    -   Extra blank pages because the report contains a matrix that dynamically expands to exceed the width of the paper specified.  
  
    -   Page breaks between groups do not work the way you want.  
  
    -   Headers and footers do not display as expected.  
  
    -   Report layout needs modification to read better in a printed format.  
  
  
##  <a name="bkmk_SaveandDeploy"></a> Save and Deploy  
 In Report Designer, you can save reports and other project files locally, or deploy them to a report server or SharePoint site. Shared data sources, shared datasets, reports, report resources, and report parts can be deployed independently or together depending on project deployment properties that you configure. For more information, see [Configuration and Deployment Properties](deployment-and-version-support-in-sql-server-data-tools-ssrs.md#bkmk_ConfigurationandDeploymentProperties).  
  
 In Report Designer, it is important to understand that you design a report using the report definition schema that is supported by the current version of [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] in [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)]. When you set project deployment properties for a specific report server or SharePoint site, and then save the report, Report Designer saves the report definition to the build directory in the schema that matches the version on the target report server. To create reports that can be published on a down-level report server, Report Designer drops report items that do not exist in the target schema. This occurs automatically and without prompting. When this happens, the original report definition is preserved in the project folder. The modified report definition that is deployed is in the build folder.  
  
> [!NOTE]  
>  For debugging expressions and deployment errors, you must view the report definition in the build folder. Do not use **View Source**. **View Source** displays the report definition source from the project folder.  
  
 For more information, see [Deployment and Version Support in SQL Server Data Tools &#40;SSRS&#41;](deployment-and-version-support-in-sql-server-data-tools-ssrs.md).  
  
### Save a Report Locally  
 When you work on report or other project items in Report Designer, the files are saved to your local computer or a share on another computer that you have access to.  
  
 If you are using source control software, you might be checking your reports into the source control server when you save the report. For more information, see [Source Control](reporting-services-in-sql-server-data-tools-ssdt.md#bkmk_SourceControl).  
  
### Deploy or Publish  
 From [!INCLUDE[ssBIDevStudio](../../includes/ssbidevstudio-md.md)], you can deploy reports or other project items to multiple versions of [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] report servers. Use project configurations to control the upgrade of report definitions to schema versions compatible with target report servers. The properties controlled by project configurations include the target report server, the folder where the build process temporarily stores report definitions for preview and deployment, and error levels. For more information, see [Configuration and Deployment Properties](deployment-and-version-support-in-sql-server-data-tools-ssrs.md#bkmk_ConfigurationandDeploymentProperties) and [Set Deployment Properties &#40;Reporting Services&#41;](set-deployment-properties-reporting-services.md).  
  
### Export a Report to a Different File Format  
 Reports can be exported to a variety of formats and these formats affect how some report layout and interactivity features function. For more information about design considerations for various output formats, see [Exporting Reports &#40;Report Builder and SSRS&#41;](../report-builder/export-reports-report-builder-and-ssrs.md).  
  
  
##  <a name="bkmk_ReportValidationandErrorLevels"></a> Report Validation and Error Levels  
 Reports are validated before preview and during deployment. A number of build issues can occur when reports are built. Reports might contain strings such as expressions or queries that are incompatible with the version of [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] that the project configuration specifies, for example.  
  
 Use the ErrorLevel property to manage the build warnings and errors. The ErrorLevel property can contain a value from 0 to 4 inclusive. The value determines which build issues are reported as errors and which are reported as warnings. The default value is 2. The warnings and errors are written to the [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)][Output](reporting-services-in-sql-server-data-tools-ssdt.md#bkmk_Output) window.  
  
 Issues with severity levels less than or equal to the value of ErrorLevel are reported as errors; otherwise, they are reported as warnings.  
  
 The following table lists the error levels.  
  
|Error level|Description|  
|-----------------|-----------------|  
|0|Most severe and unavoidable build issues that prevent preview and deployment of reports.|  
|1|Severe build issues that change the report layout drastically.|  
|2|Less severe build issues that change report layout in significantly.|  
|3|Minor build issues that change the report layout in minor ways that might not be noticeable.|  
|4|Used only for publishing warnings.|  
  
 When you attempt to preview or deploy a report that contains report items new in [!INCLUDE[ssKilimanjaro](../../includes/sskilimanjaro-md.md)], such as maps and data bars, those report items can be removed from the report. By default, the ErrorLevel property of the configuration is set to 2, which would cause the build of the report to fail when the map is removed. However, if you change the value of the ErrorLevel property to 0 or 1, the map is dropped, a warning issued, and the build process continues.  
  
  
## See Also  
 [Reporting Services in SQL Server Data Tools &#40;SSDT&#41;](reporting-services-in-sql-server-data-tools-ssdt.md)   
 [Query Design Tools in Report Designer SQL Server Data Tools &#40;SSRS&#41;](../report-data/query-design-tools-ssrs.md)   
 [Deployment and Version Support in SQL Server Data Tools &#40;SSRS&#41;](deployment-and-version-support-in-sql-server-data-tools-ssrs.md)  
  
  
