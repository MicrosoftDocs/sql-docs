---
title: Design Reporting Services paginated reports with Report Designer (SSRS)
description: See how to use Report Designer in SQL Server Reporting Services to create full-featured paginated reports and reporting solutions.
author: maggiesMSFT
ms.author: maggies
ms.date: 09/06/2024
ms.service: reporting-services
ms.subservice: tools
ms.topic: conceptual
ms.custom: updatefrequency5
helpviewer_keywords:
  - "Report Designer [Reporting Services], report creation"

#customer intent: As a SQL Server user, I want to see how to use Report Designer so that I can create useful reports with my data.
---

# Design Reporting Services paginated reports with Report Designer (SSRS)

You can use Report Designer to create full-featured SQL Server Reporting Services (SSRS) paginated reports and reporting solutions. Report Designer provides a way for you to define data sources. It also provides datasets and queries, report layout positions for data regions and fields, and interactive features such as parameters and sets of reports that work together.  

Report Designer is a feature of [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)], a Microsoft Visual Studio environment for creating business intelligence solutions.

## Benefits of report projects

In Report Designer, you can use projects to organize reports. Report projects act as containers for report definitions and resources. You can use projects for the following tasks:

- Organize reports and related items in one container.  
- Test report solutions that include reports and related items locally.  
- Deploy related items together. You can use project properties and configuration management to deploy items to multiple environments.
- Preserve a set of primary copies for reports and related items. After deployment, published reports can be accidentally modified.  
  
You can use the information in this article to design paginated reports and related items for a single reporting project in an SSDT solution. For more information about solutions and multiple projects in SSDT, see [Reporting Services in SQL Server Data Tools](../../reporting-services/tools/reporting-services-in-sql-server-data-tools-ssdt.md).

## Shared data sources and datasets

SSDT provides a way to define and deploy shared data sources and shared datasets for a reporting solution.

- To deploy a shared data source independently from other items in a project, you can use the **OverwriteDataSources** and **TargetDataSourceFolder** properties.
- To deploy a shared dataset independently from other items in a project, you can use the **OverwriteDatasets** and **TargetDatasetFolder** properties.

For more information, see [Set deployment properties (Reporting Services)](../../reporting-services/tools/set-deployment-properties-reporting-services.md).
  
In Report Designer, you work in both the Report Data pane and in Solution Explorer to define the data sources and shared datasets that you use in a report. For more information, see [Report Data pane](../../reporting-services/tools/reporting-services-in-sql-server-data-tools-ssdt.md#bkmk_ReportDataPane).

If a data source is published to a report server or SharePoint site but not included in the SSDT solution, you can's use SSDT to open that data source. Similarly, you can't use SSDT to open published datasets directly from a report server or SharePoint site. To open a data source or dataset in these scenarios, use the [Report Builder authoring environment (SSRS)](../../reporting-services/tools/report-builder-authoring-environment-ssrs.md). For datasets, use the environment in Shared Dataset mode.

SSDT is a client tool. You can use it to:

- Test your reporting solution locally on your computer.
- Deploy your reporting solution to a test environment to test the server solution.
- Deploy your reporting solution to a production environment.
- Help create and test your query results locally by using preview capabilities of SSDT query designers.

After deployment:

- You should verify that the data source processing extensions and data source credentials are configured for the report server environment. You can use Report Server Configuration Manager to help manage the properties for multiple deployments. For more information, see [Reporting Services in SQL Server Data Tools (SSDT)](../../reporting-services/tools/reporting-services-in-sql-server-data-tools-ssdt.md).
- You can manage shared datasets independently from the shared data sources and reports that they depend on. For more information, see the following resources:
  - [Report embedded datasets and shared datasets (Report Builder and SSRS)](../../reporting-services/report-data/report-embedded-datasets-and-shared-datasets-report-builder-and-ssrs.md)
  - [Query design tools (SSRS)](../../reporting-services/report-data/query-design-tools-ssrs.md)
  - [Manage shared datasets](../../reporting-services/report-data/manage-shared-datasets.md)

For more information about including data in paginated reports, see [Create data connection strings in Report Builder](../../reporting-services/report-data/data-connections-data-sources-and-connection-strings-report-builder-and-ssrs.md).

##  <a name="bkmk_Reports"></a> Paginated reports

Paginated reports are files that are stored in a report project. You can use a paginated report file as a stand-alone report, a subreport, or as a target for drill-through actions from main reports. To deploy a report independently from other items in a project, you can use the **TargetReportFolder** property and other properties. For more information, see [Set deployment properties (Reporting Services)](../../reporting-services/tools/set-deployment-properties-reporting-services.md).
  
> [!NOTE]
> If you publish to a report server in SharePoint mode, you can't test some report solution features in the Report Designer project. References to reports, subreports, and drill-through reports must use fully-qualified URLs that can be tested only after you deploy the report project. For more information, see [URL examples for published report items on a report server in SharePoint mode (SSRS)](../../reporting-services/tools/url-examples-for-items-on-a-report-server-sharepoint-mode.md).

You can add a report to a project in the following ways:

- **Use the report wizard.** You create a report in a guided step-by-step manner. The report wizard simplifies data definition and report design into a series of steps that give you a finished report. You can add styles to customize the wizard for your own organization. For more information, see [Add a new report by using the report wizard](../../reporting-services/tools/add-a-new-or-existing-report-to-a-report-project-ssrs.md#add-a-new-report-by-using-the-report-wizard).
- **Add a new item of type Report.** A blank report opens in Report Designer. For more information, see [Add a new report by using the report wizard](../../reporting-services/tools/add-a-new-or-existing-report-to-a-report-project-ssrs.md#add-a-new-blank-report).
- **Add an existing item.** An existing report definition (.rdl) file opens in Report Designer. Opening a report or project from an earlier version of SSRS might automatically upgrade the project to the current version and the report to the current schema. For more information, see [Upgrade reports](../../reporting-services/install-windows/upgrade-reports.md).
- **Import a [!INCLUDE[msCoName](../../includes/msconame-md.md)] Access report.** Import all reports from an Access database (.mdb, .accdb) or project (.adp) file. Report Designer converts each report in a database or project file to RDL and saves it in the report project. Not all of the functionality of an Access report transfers to a report definition (.rdl) file. For more information, see [Import Reports from Microsoft Access (Reporting Services)](../reports/reporting-services-reports-ssrs.md) and [Supported Access Report Features (SSRS)](../reports/reporting-services-reports-ssrs.md).

  > [!NOTE]
  > You must have Access 2002 or a later version installed on the same computer that Report Designer is installed on in order to use the import feature. The data source for the Access reports must be available when the reports are imported.

- **Work directly in RDL.** When you write a report in Report Designer, the report is saved in XML format as a Report Definition Language (RDL) file. You can edit this file in Report Designer, a text editor, or any tool in which you can edit XML.

  When you edit the report definition source in Report Designer, you're working in the current RDL schema for the version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] from which you installed the development tools. When you build a project, the schema version might change depending on your deployment properties. For more information, see [Deployment and Version Support in SQL Server Data Tools (SSRS)](../../reporting-services/tools/deployment-and-version-support-in-sql-server-data-tools-ssrs.md).

  Editing RDL directly can result in a report that can't be published to the report server or can't run. As with any XML file, ensure that XML-specific characters used within elements are properly encoded. When you publish the report, the report server uses the schema to validate the XML contained within the RDL file.

  To include elements that aren't part of the RDL schema, place them in the Custom Element. Custom rendering extensions can read the Custom element. But, the rendering extensions that SSRS provides ignore the element. For example, you can use the Custom element to store comments in the report.

  For more information, see [Report Definition Language (SSRS)](../../reporting-services/reports/report-definition-language-ssrs.md).
  
##  <a name="bkmk_ReportParts"></a> Report parts

[!INCLUDE [ssrs-report-parts-deprecated](../../includes/ssrs-report-parts-deprecated.md)]

In Report Designer, you can create tables, charts, and other paginated report items in a project. After you create them, you can publish them as *report parts* to a report server or SharePoint site integrated with a report server. This action allows you and others to reuse them in other reports. For more information, see [Report Parts in Report Designer (SSRS)](../../reporting-services/report-design/report-parts-in-report-designer-ssrs.md).

Report parts can be deployed independently from other items in a project by using **TargetReportPartFolder** and other properties. For more information, see [Set Deployment Properties (Reporting Services)](../../reporting-services/tools/set-deployment-properties-reporting-services.md).

##  <a name="bkmk_Resources"></a> Resources

You can add files to your project that are related to your report but not processed by the report server. For example, you can add images for pictures or ESRI shapefiles for spatial data. For more information, see [Resources](../../reporting-services/report-server/report-server-content-management-ssrs-native-mode.md#bkmk_Resources).

##  <a name="bkmk_ReportLayout"></a> Paginated report layout

To create the report layout, drag report items and data regions from the Toolbox to the design surface and arrange them. Drag dataset fields to the items on the design surface to add data to the report. To organize data in groups in a tablix data region, drag dataset fields to the Grouping pane. Because report authoring tools are essentially a way to create report definitions, the approach to report design is similar between Report Builder and Report Designer.

##  <a name="bkmk_Preview"></a> Preview a paginated report

Use **Preview** to verify the report data and layout design. When you preview a report, the report processor validates the report definition schema and expression syntax and lists issues in the [Output](../../reporting-services/tools/reporting-services-in-sql-server-data-tools-ssdt.md#bkmk_Output) window.

> [!NOTE]
> When you preview a report, the data for the report is cached to a file on the local computer. When you preview the same report again by using the same query, parameters, and credentials, Report Designer retrieves the cached copy rather than rerunning the query. The data file is saved as `<reportname>.rdl.data` in the same directory as the report definition file. The file is not deleted when you close Report Designer.

You can preview a report in the following ways:

- **Preview view.** Select the **Preview** tab. The report runs locally, and uses the same report processing and rendering functionality that is provided with the report server. The report that is displayed is an interactive image; you can select parameters, select links, view the document map, and expand and collapse hidden areas of the report. You can also export the report to any of the installed rendering formats.

- **Standalone Preview.** Run the local report in a browser. By using a debug configuration, you can also use this mode to debug custom assemblies that you write. There are three ways to run a project in Debug mode:
  - On the **Debug** menu, select **Start Debugging**.
  - On the [!INCLUDE[vsprvs](../../includes/vsprvs-md.md)] standard toolbar, select the **Start** button.
    - Press F5.
      If you use a project configuration that builds the report but doesn't deploy it, the report that is specified in the **StartItem** property of the current configuration opens in a separate preview window.
      > [!NOTE]
      > To use Debug mode, you must set a start item. In Solution Explorer, right-click the report project, click **Properties**, and in **StartItem**, select the name of the report to display.

     If you wish to preview a particular report that isn't the start item for the project, select a configuration that builds the report but doesn't deploy it (for example, the DebugLocal configuration), right-click the report, and then select **Run**. You must choose a configuration that doesn't deploy the report. Otherwise, the report isn't published to the report server instead of displayed locally in a preview window.

- **Print Preview.**

  When you first view a report on in Preview mode or in the preview window, the view of the report resembles a report produced by the HTML rendering extension. The preview isn't HTML, but the layout and pagination of the report is similar to HTML output.
  
  You can change the view to represent a printed report by switching to print preview mode. Select the **Print Preview** button on the preview toolbar. The report displays as though it were on a physical page. This view resembles the output produced by the Image and PDF rendering extensions. Print preview isn't an image or PDF file, but the layout and pagination of the report is similar the output in those formats. You can choose the size of the report image, for example, set the width of the page.

  Print preview helps you identify many of rendering problems you might encounter when you print the report. Common rendering problems include:
  
  - Extra blank pages because the report is too wide to fit on the paper size you specified for the report.
  - Extra blank pages because the report contains a matrix that dynamically expands to exceed the width of the paper specified.
  - Page breaks between groups don't work the way you want.
  - Headers and footers don't display as expected.
  - Report layout needs modification to read better in a printed format.

##  <a name="bkmk_SaveandDeploy"></a> Save and deploy paginated reports

In Report Designer, you can save reports and other project files locally, or deploy them to a report server or SharePoint site. Shared data sources, shared datasets, reports, report resources, and report parts can be deployed independently or together depending on project deployment properties that you configure. For more information, see [Configuration and deployment properties](../../reporting-services/tools/deployment-and-version-support-in-sql-server-data-tools-ssrs.md#bkmk_ConfigurationandDeploymentProperties).

In Report Designer, it's important to understand that you design a report by using the report definition schema that the current version of SSRS in SSDT supports. You can set project deployment properties for a specific report server or SharePoint site. When you save the report, Report Designer saves the report definition to the build directory in the schema that matches the version on the target report server. To create reports that can be published on a down-level report server, Report Designer drops report items that don't exist in the target schema. This action occurs automatically and without prompting. When this action happens, the original report definition is preserved in the project folder. The modified report definition that is deployed is in the build folder.

> [!NOTE]
> For debugging expressions and deployment errors, you must view the report definition in the build folder. Do not use **View Source**. **View Source** displays the report definition source from the project folder.

For more information, see [Deployment and version support in SQL Server Data Tools (SSRS)](../../reporting-services/tools/deployment-and-version-support-in-sql-server-data-tools-ssrs.md).

### Save a report locally

When you work on report or other project items in Report Designer, the files are saved to your local computer or a share on another computer that you have access to.

If you use source control software, you might be checking your reports into the source control server when you save the report. For more information, see [Source control](../../reporting-services/tools/reporting-services-in-sql-server-data-tools-ssdt.md#bkmk_SourceControl).

### Deploy or publish paginated reports

From SSDT, you can deploy reports or other project items to multiple versions of SSRS report servers. Use project configurations to control the upgrade of report definitions to schema versions compatible with target report servers. The properties controlled by project configurations include the target report server, the folder where the build process temporarily stores report definitions for preview and deployment, and error levels. For more information, see [Configuration and deployment properties](../../reporting-services/tools/deployment-and-version-support-in-sql-server-data-tools-ssrs.md#bkmk_ConfigurationandDeploymentProperties) and [Set deployment properties (Reporting Services)](../../reporting-services/tools/set-deployment-properties-reporting-services.md).
  
### Export a paginated report to a different file format

Reports can be exported to various formats and these formats affect how some report layout and interactivity features function. For more information about design considerations for various output formats, see [Export Reports (Report Builder and SSRS)](../../reporting-services/report-builder/export-reports-report-builder-and-ssrs.md).

##  <a name="bkmk_ReportValidationandErrorLevels"></a> Report validation and error levels

Reports are validated before preview and during deployment. Many build issues can occur when reports are built. Reports might contain strings such as expressions or queries that are incompatible with the version of SSRS that the project configuration specifies, for example.

Use the ErrorLevel property to manage the build warnings and errors. The ErrorLevel property can contain a value from 0 to 4 inclusive. The value determines which build issues are reported as errors and which are reported as warnings. The default value is 2. The warnings and errors are written to the SSDT [Output](../../reporting-services/tools/reporting-services-in-sql-server-data-tools-ssdt.md#bkmk_Output) window.

Issues with severity levels less than or equal to the value of ErrorLevel are reported as errors; otherwise, they're reported as warnings.

The following table lists the error levels.

|Error level|Description|
|-----------------|-----------------|
|0|Most severe and unavoidable build issues that prevent preview and deployment of reports.|
|1|Severe build issues that change the report layout drastically.|
|2|Less severe build issues that change report layout in significantly.|
|3|Minor build issues that change the report layout in minor ways that might not be noticeable.|
|4|Used only for publishing warnings.|

When you attempt to preview or deploy a report that contains report items new in [!INCLUDE[ssRSCurrent](../../includes/ssrscurrent-md.md)],  those report items can be removed from the report. By default, the ErrorLevel property of the configuration is set to 2, which would cause the build of the report to fail when the map is removed. However, if you change the value of the ErrorLevel property to 0 or 1, the map is dropped, a warning issued, and the build process continues.

## Related content

[Download SQL Server Data Tools](../../ssdt/download-sql-server-data-tools-ssdt.md)
[Reporting Services in SQL Server Data Tools](../../reporting-services/tools/reporting-services-in-sql-server-data-tools-ssdt.md)
[Query design tools](../../reporting-services/report-data/query-design-tools-ssrs.md)
[Deployment and version support in SQL Server Data Tools](../../reporting-services/tools/deployment-and-version-support-in-sql-server-data-tools-ssrs.md)

More questions? [Try asking the Reporting Services forum](/answers/search.html?c=&f=&includeChildren=&q=ssrs+OR+reporting+services&redirect=search%2fsearch&sort=relevance&type=question+OR+idea+OR+kbentry+OR+answer+OR+topic+OR+user)
