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

Report Designer is a feature of [!INCLUDE[SQL Server Data Tools (SSDT)](../../includes/ssbidevstudiofull-md.md)], a Microsoft Visual Studio environment for creating business intelligence solutions.

## SSDT

SSDT isn't included with SQL Server. You need to install SSDT separately. For instructions, see [Install SSDT with Visual Studio](../../ssdt/download-sql-server-data-tools-ssdt.md#install-ssdt-with-visual-studio). To use Report Designer, you also need to install a Reporting Services extension for Visual Studio. For instructions, see [Install extensions for Analysis Services, Integration Services, and Reporting Services](../../ssdt/download-sql-server-data-tools-ssdt.md#install-extensions-for-analysis-services-integration-services-and-reporting-services).

## Benefits of report projects

In Report Designer, you can use projects to organize reports. Report projects act as containers for report definitions and resources. You can use projects for the following tasks:

- Organize reports and related items in one container.  
- Test report solutions that include reports and related items locally.  
- Deploy related items together. You can use project properties and configuration management to deploy items to multiple environments.
- Preserve a set of primary copies for reports and related items. After deployment, published reports can be accidentally modified.  
  
You can use the information in this article to design paginated reports and related items for a single reporting project in an SSDT solution. For more information about solutions and multiple projects in SSDT, see [Reporting Services in SQL Server Data Tools (SSDT)](../../reporting-services/tools/reporting-services-in-sql-server-data-tools-ssdt.md).

## Shared data sources and datasets

SSDT provides a way to define and deploy shared data sources and shared datasets for a reporting solution.

- To deploy a shared data source independently from other items in a project, you can use the **OverwriteDataSources** and **TargetDataSourceFolder** properties.
- To deploy a shared dataset independently from other items in a project, you can use the **OverwriteDatasets** and **TargetDatasetFolder** properties.

For more information, see [Set deployment properties (Reporting Services)](../../reporting-services/tools/set-deployment-properties-reporting-services.md).
  
In Report Designer, you work in both the Report Data pane and in Solution Explorer to define the data sources and shared datasets that you use in a report. For more information, see [Report Data pane](../../reporting-services/tools/reporting-services-in-sql-server-data-tools-ssdt.md#bkmk_ReportDataPane).

If a data source is published to a report server or SharePoint site but not included in the SSDT solution, you can't use SSDT to open that data source. Similarly, you can't use SSDT to open published datasets directly from a report server or SharePoint site. To open a data source or dataset in these scenarios, use the [Report Builder authoring environment (SSRS)](../../reporting-services/tools/report-builder-authoring-environment-ssrs.md). For datasets, use the environment in Shared Dataset mode.

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

## Paginated reports

Paginated reports are files that are stored in a report project. You can use a paginated report file as a stand-alone report, a subreport, or as a target for drill-through actions from main reports. To deploy a report independently from other items in a project, you can use the **TargetReportFolder** property and other properties. For more information, see [Set deployment properties (Reporting Services)](../../reporting-services/tools/set-deployment-properties-reporting-services.md).
  
> [!NOTE]
> If you publish to a report server in SharePoint mode, you can't test some report solution features in the Report Designer project. References to reports, subreports, and drill-through reports must use fully-qualified URLs that can be tested only after you deploy the report project. For more information, see [URL examples for items on a report server - SharePoint mode](../../reporting-services/tools/url-examples-for-items-on-a-report-server-sharepoint-mode.md).

You can add a report to a project in the following ways:

- **Use the report wizard.** You create a report in a guided step-by-step manner. The report wizard simplifies data definition and report design into a series of steps that give you a finished report. You can add styles to customize the wizard for your own organization. For more information, see [Add a new report by using the report wizard](../../reporting-services/tools/add-a-new-or-existing-report-to-a-report-project-ssrs.md#add-a-new-report-by-using-the-report-wizard).
- **Add a new item of type report.** A blank report opens in Report Designer. For more information, see [Add a new blank report](../../reporting-services/tools/add-a-new-or-existing-report-to-a-report-project-ssrs.md#add-a-new-blank-report).
- **Add an existing item.** An existing report definition (.rdl) file opens in Report Designer. Opening a report or project from an earlier version of SSRS might automatically upgrade the project to the current version and the report to the current schema. For more information, see [Upgrade reports (SSRS)](../../reporting-services/install-windows/upgrade-reports.md).
- **Import a [!INCLUDE[Microsoft](../../includes/msconame-md.md)] Access report.** The import process imports all reports from an Access database (.mdb, .accdb) or project (.adp) file. Report Designer converts each report in a database or project file to RDL and saves it in the report project. Not all the functionality of an Access report transfers to a report definition (.rdl) file. For more information, see [Import reports from Microsoft Access (Reporting Services)](/previous-versions/sql/sql-server-2012/ms156508(v=sql.110)).

  > [!NOTE]
  > You must have Access 2002 or a later version installed on the same computer that Report Designer is installed on in order to use the import feature. The data source for the Access reports must be available when the reports are imported.

- **Work directly in an Report Definition Language (RDL) file.** The report is saved in XML format as an RDL file. You can edit this file in Report Designer, a text editor, or any XML editor.

  When you edit the report definition source in Report Designer, you work in the current RDL schema for the version of [!INCLUDE[SQL Server no version](../../includes/ssnoversion-md.md)] that you installed the development tools from. When you build a project, the schema version might change depending on your deployment properties. For more information, see [Deployment and version support in SQL Server Data Tools (SSDT)](../../reporting-services/tools/deployment-and-version-support-in-sql-server-data-tools-ssrs.md).

  Editing RDL directly can result in a report that can't be published to the report server or can't run. As with any XML file, ensure that you properly encode XML-specific characters that  you use within elements. When you publish the report, the report server uses the schema to validate the XML in the RDL file.

  To include elements that aren't part of the RDL schema, place them in a custom element. Custom rendering extensions can read custom elements. But the rendering extensions that SSRS provides ignore the element. For example, you can use a custom element to store comments in your report.

  For more information, see [Report Definition Language (SSRS)](../../reporting-services/reports/report-definition-language-ssrs.md).
  
## Report parts

[!INCLUDE [SSRS report parts deprecated](../../includes/ssrs-report-parts-deprecated.md)]

In Report Designer, you can create tables, charts, and other paginated report items in a project. After you create them, you can publish them as *report parts* to a report server or SharePoint site integrated with a report server. This action allows you and others to reuse them in other reports. For more information, see [Report parts in Report Designer (SSRS)](../../reporting-services/report-design/report-parts-in-report-designer-ssrs.md).

You can deploy report parts independently from other items in a project by using the **TargetReportPartFolder** property and other properties. For more information, see [Set deployment properties (Reporting Services)](../../reporting-services/tools/set-deployment-properties-reporting-services.md).

## Resources

You can add files to your project that are related to your report but not processed by the report server. For example, you can add images, or you can add Environmental Systems Research Institute, Inc. (ESRI) shapefiles for spatial data. For more information, see [Resources](../../reporting-services/report-server/report-server-content-management-ssrs-native-mode.md#bkmk_Resources).

## Paginated report layout

To create the report layout, you drag report items and data regions from the Toolbox to the design surface and arrange them. By dragging dataset fields to the items on the design surface, you can add data to the report. To organize data in groups in a tablix data region, you drag dataset fields to the Grouping pane. Because report authoring tools are essentially a way to create report definitions, the approach to report design is similar in Report Builder and Report Designer.

## Preview a paginated report

You can use the **Preview** view to verify the report data and layout design. When you preview a report, the report processor validates the report definition schema and expression syntax. It also lists issues in the [Output](../../reporting-services/tools/reporting-services-in-sql-server-data-tools-ssdt.md#bkmk_Output) window.

> [!NOTE]
> When you preview a report, the data for the report is cached to a file on the local computer. When you preview the same report again by using the same query, parameters, and credentials, Report Designer retrieves the cached copy rather than rerunning the query. The data file is saved as `<report-name>.rdl.data` in the same directory as the report definition file. The file isn't deleted when you close Report Designer.

You can preview a report in the following ways:

- **Preview view.** If you select the **Preview** tab, the report runs locally. It uses the same report processing and rendering functionality that's provided with the report server. The report that's displayed is an interactive image. You can select parameters, select links, view the document map, and expand and collapse hidden areas of the report. You can also export the report to any of the installed rendering formats.

- **Standalone preview.** You can run the local report in a browser. By using a debug configuration, you can also use this mode to debug custom assemblies that you write. There are three ways to run a project in debug mode:
  - On the **Debug** menu, you can select **Start Debugging**.
  - On the [!INCLUDE[Visual Studio](../../includes/vsprvs-md.md)] standard toolbar, you can select the **Start** button.
  - You can select **F5**.

  If you use a project configuration that builds the report but doesn't deploy it, the report that's specified in the **StartItem** property of the current configuration opens in a separate preview window.

  > [!NOTE]
  > To use debug mode, you must set a start item. In Solution Explorer, you right-click the report project and select **Properties**. Then in **StartItem**, you select the name of the report to display.

  If you want to preview a particular report that isn't the start item for the project, you can select a configuration that builds the report but doesn't deploy it, for example, the DebugLocal configuration. Then you right-click the report and select **Run**. You must choose a configuration that doesn't deploy the report. Otherwise, the report is published to the report server instead of being displayed locally in a preview window.

- **Print preview.** When you first view a report in preview mode or in the preview window, the view of the report resembles a report that's produced by the HTML rendering extension. The preview isn't HTML, but the layout and pagination of the report is similar to HTML output.
  
  You can change the view to represent a printed report by switching to print preview mode. When you select **Print Preview** on the preview toolbar, the report is displayed as though it were on a physical page. This view resembles the output that's produced by the image and PDF rendering extensions. The print preview isn't an image or PDF file, but the layout and pagination of the report is similar to output in those formats. You can choose the size of the report image, for example, and set the width of the page.

  The print preview helps you identify rendering problems you might encounter when you print the report. Common rendering problems include:
  
  - Extra blank pages because the report is too wide to fit on the paper size you specify for the report.
  - Extra blank pages because the report contains a matrix that dynamically expands to exceed the specified width of the paper.
  - Page breaks between groups that don't work the way you want.
  - Headers and footers that don't appear as expected.
  - Report layouts that need modification to appear better in a printed format.

##  <a name="bkmk_SaveandDeploy"></a> Save and deploy paginated reports

In Report Designer, you can save reports and other project files locally, or deploy them to a report server or SharePoint site. Shared data sources, shared datasets, reports, report resources, and report parts can be deployed independently or together depending on project deployment properties that you configure. For more information, see [Configuration and deployment properties](../../reporting-services/tools/deployment-and-version-support-in-sql-server-data-tools-ssrs.md#bkmk_ConfigurationandDeploymentProperties).

In Report Designer, you design a report by using the report definition schema that the current version of SSRS in SSDT supports. You can set project deployment properties for a specific report server or SharePoint site. When you save the report, Report Designer saves the report definition to the build directory in the schema that matches the version on the target report server. To create reports that can be published on a report server that uses an earlier version of SSRS, Report Designer drops report items that don't exist in the target schema. This action occurs automatically and without prompting. When this action happens, the original report definition is preserved in the project folder. The modified report definition that's deployed is in the build folder.

> [!NOTE]
> For debugging expressions and deployment errors, you must view the report definition in the build folder. Don't use **View Code** on the report file's shortcut menu to view the report definition in this case. When you select **View Code**, you open the report definition source from the project folder.

For more information, see [Deployment and version support in SQL Server Data Tools (SSDT)](../../reporting-services/tools/deployment-and-version-support-in-sql-server-data-tools-ssrs.md).

### Save a report locally

When you work on reports or other project items in Report Designer, the files are saved to your local computer or a share on another computer that you have access to.

If you use source control software, the act of saving a report might check in into the source control server. For more information, see [Source control](../../reporting-services/tools/reporting-services-in-sql-server-data-tools-ssdt.md#bkmk_SourceControl).

### Deploy or publish paginated reports

From SSDT, you can deploy reports or other project items to multiple versions of SSRS report servers. Use project configurations to control the upgrade of report definitions to schema versions that are compatible with target report servers. The properties that are controlled by project configurations include the target report server, the folder where the build process temporarily stores report definitions for preview and deployment, and error levels. For more information, see [Configuration and deployment properties](../../reporting-services/tools/deployment-and-version-support-in-sql-server-data-tools-ssrs.md#bkmk_ConfigurationandDeploymentProperties) and [Set deployment properties (Reporting Services)](../../reporting-services/tools/set-deployment-properties-reporting-services.md).
  
### Export a paginated report to a different file format

Reports can be exported to various formats. These formats affect how some report layout and interactivity features function. For more information about design considerations for various output formats, see [Export paginated reports (Report Builder)](../../reporting-services/report-builder/export-reports-report-builder-and-ssrs.md).

##  <a name="bkmk_ReportValidationandErrorLevels"></a> Report validation and error levels

Reports are validated before preview and during deployment. Many build issues can occur when reports are built. For example, reports can contain strings such as expressions or queries that are incompatible with the version of SSRS that the project configuration specifies.

Use the **ErrorLevel** property to manage the build warnings and errors. The **ErrorLevel** property can contain a value from 0 to 4 inclusive. The value determines which build issues are reported as errors and which are reported as warnings. The default value is 2. The warnings and errors are written to the SSDT [Output](../../reporting-services/tools/reporting-services-in-sql-server-data-tools-ssdt.md#bkmk_Output) window.

Issues with severity levels that are less than or equal to the **ErrorLevel** value are reported as errors. Other issues are reported as warnings.

The following table lists the error levels.

|Error level|Description|
|-----------------|-----------------|
|0|Most severe build issues that prevent the preview and deployment of reports.|
|1|Severe build issues that change the report layout drastically.|
|2|Less severe build issues that change the report layout significantly.|
|3|Minor build issues that change the report layout in minor ways that might not be noticeable.|
|4|Warnings about publishing issues.|

You can attempt to preview or deploy a report that contains report items that aren't supported in your version of SSRS. But those report items are sometimes removed from the report and a level-2 error is issued. If your **ErrorLevel** property is set to the default value of 2, the build fails in this case.

You can get around this problem by changing the **ErrorLevel** value to 0 or 1. Then when the item is dropped, a warning is issued, and the build process continues.

## Related content

- [Reporting Services reports (SSRS)](../reports/reporting-services-reports-ssrs.md)
- [Install SQL Server Data Tools (SSDT) for Visual Studio](../../ssdt/download-sql-server-data-tools-ssdt.md)
- [Query design tools (SSRS)](../../reporting-services/report-data/query-design-tools-ssrs.md)
