---
title: Reporting Services in SQL Server Data Tools (SSDT)
description: See how to use the SQL Server Data Tools Report Designer authoring environment in Microsoft Visual Studio to create solutions for Reporting Services.
author: maggiesMSFT
ms.author: maggies
ms.date: 09/25/2024
ms.service: reporting-services
ms.subservice: tools
ms.topic: conceptual
ms.custom:
  - updatefrequency5
helpviewer_keywords:
  - "Business Intelligence Development Studio, Reporting Services in"
# customer intent: As a SQL Server user, I want to find out about the various components of Report Designer so that I can build reports efficiently and effectively.
---

# Reporting Services in SQL Server Data Tools (SSDT)

[!INCLUDE[SSRS applies to](../../includes/ssrs-appliesto.md)] [!INCLUDE[SSRS applies to 2016 and later](../../includes/ssrs-appliesto-2016-and-later.md)]

[!INCLUDE[SQL Server Data Tools (SSDT)](../../includes/ssbidevstudiofull-md.md)] is a [!INCLUDE[Microsoft](../../includes/msconame-md.md)] [!INCLUDE[Visual Studio](../../includes/vsprvs-md.md)] environment for creating business intelligence solutions. SSDT features the Report Designer authoring environment. You can use Report Designer to open, modify, preview, save, and deploy SQL Server Reporting Services (SSRS) paginated report definitions, shared data sources, shared datasets, and report parts.

[!INCLUDE [SSRS report parts deprecated](../../includes/ssrs-report-parts-deprecated.md)]

This article describes SSDT solutions, projects, project templates, and configurations that you use for SSRS. It also describes the views, menus, toolbars, and shortcuts that you can use in Report Designer.

## SSDT installation

SSDT isn't included with SQL Server. You need to install SSDT separately. For instructions, see [Install SSDT with Visual Studio](../../ssdt/download-sql-server-data-tools-ssdt.md#install-ssdt-with-visual-studio).

To use Report Designer templates, you also need to install an SSRS extension for [!INCLUDE[Visual Studio](../../includes/vsprvs-md.md)]. For instructions, see [Install extensions for Analysis Services, Integration Services, and Reporting Services](../../ssdt/download-sql-server-data-tools-ssdt.md#install-extensions-for-analysis-services-integration-services-and-reporting-services).

To get started designing reports, see [Design Reporting Services paginated reports with Report Designer (SSRS)](../../reporting-services/tools/design-reporting-services-paginated-reports-with-report-designer-ssrs.md).

## Solutions and projects

A report project acts as a container for report definitions and resources. Every file in the report project is published to the report server when the project is deployed. When you create a project for the first time, a solution is also created as a container for the project. You can add multiple projects to a single solution.

## Configurations

To create multiple sets of project properties for deployment variations such as enterprise, test, and production report servers, you use the Configuration Manager feature in [!INCLUDE[Visual Studio](../../includes/vsprvs-md.md)]. For more information, see [Deployment and version support in SQL Server Data Tools (SSRS)](../../reporting-services/tools/deployment-and-version-support-in-sql-server-data-tools-ssrs.md).

## Report server projects

When you install the SSRS extension for [!INCLUDE[Visual Studio](../../includes/vsprvs-md.md)], as described earlier in [SSDT installation](#ssdt-installation), the following project templates are made available in SSDT:

- **Report Server Project.** When you use the Report Server Project template, Report Designer opens. You can find this business intelligence project template in the **Create a new project** dialog. For more information, see [Create a report server project](../tutorial-step-01-create-report-server-project-reporting-services.md#create-a-report-server-project).

  Report Server project properties apply to all reports and all shared data sources in an SSDT project. These properties include the URL for the report server and the folder names for reports and shared data sources. You can use the **Project Property Pages** dialog to view the current property values. To open this dialog, you go to the **Project** menu, and then select **Properties**.

- **Report Server Project Wizard.** When you use the Report Server Wizard Project template, a report server project is automatically created, and the report wizard opens. In the wizard, you can create a report by following instructions on each page. The instructions describe how to:
  - Create a connection string to a data source.
  - Set data source credentials.
  - Design a query.
  - Add a table or matrix data region.
  - Specify report data and groups.
  - Pick a font and color style.
  - Publish the report to a report server.
  - Preview the report locally.

  After you create a report with the wizard, you can change the report data and the report design by using Report Designer in the report server project.

## Report Designer windows and panes

Report Designer offers multiple windows and panes to help you design reports and view rendered reports.

### Report Data pane

The Report Data pane displays data objects that you can use in a report.

To open the Report Data pane, you shift the focus to the design area. Then on the **View** menu, you select **Report Data**.

The following types of objects are available on the Report Data pane:

- **Built-in fields**. These fields contain predefined report information such as a report name or the time a report was processed.
- **Data sources**. A data source represents the name and connection information for a source of data.
- **Datasets**. Each dataset includes a query that specifies which data to retrieve from the data source. You can expand the dataset to view the collection of fields that the dataset query specifies. In some query designers for multidimensional datasets, you can specify filters in the Filters pane and indicate whether to create report parameters. If you specify the report parameter option, a special dataset is automatically created to populate the parameter's valid values list. By default, the dataset doesn't appear in the Report Data pane. For more information, see [Show hidden datasets for parameter values - multidimensional data](../../reporting-services/report-data/show-hidden-datasets-for-parameter-values-multidimensional-data.md).
- **Report parameters**. You can create report parameters manually or automatically when a dataset query includes query parameters.
- **Images**. The images in this list are available to include as image report items in a report.

Data sources and datasets in the Report Data pane represent the elements in the report definition. The Report Data pane is a feature that multiple report authoring environments support.

- In Report Builder, it's the only pane that's available for managing data sources and datasets.
- In Report Designer, the Report Data pane works with Solution Explorer, which lists shared data sources and shared datasets as files. Shared data sources and shared datasets in the Report Data pane must point to their corresponding shared data sources and shared datasets in Solution Explorer. The Report Data pane elements then contain a reference to the data files in Solution Explorer. The project properties determine whether the shared data sources and shared datasets are deployed to the report server or SharePoint site. For more information, see [Convert data sources (Report Builder and SSRS)](../../reporting-services/report-data/convert-data-sources-report-builder-and-ssrs.md).

If the Report Data pane is floating, you can anchor it. For more information, see [Dock the Report Data pane in Report Designer (SSRS)](../../reporting-services/tools/dock-the-report-data-pane-in-report-designer-ssrs.md).

### Grouping pane

You use the Grouping pane to define groups for a tablix data region. You can define row groups and detail groups for tables, and you can define row and column groups for matrices. You can't use the Grouping pane to define groups for charts or other data regions. For more information, see [Groups in a Report Builder paginated report](../../reporting-services/report-design/understanding-groups-report-builder-and-ssrs.md).

The Grouping pane has two modes:

- **Default**. You use default mode to display all row and column groups in a hierarchical format that shows the relationships between parent groups, child groups, adjacent groups, and detail groups. A child group appears under and at the next indent level compared to its parent group. An adjacent group appears at the same indent level as its peer or sibling groups.

  You also use default mode to add, edit, or delete groups. For groups based a single dataset field, you drag the field to the Row Groups or Column Groups pane. You can insert the group next to an existing group. To add an adjacent group, you use the shortcut menu of the sibling group. To display which tablix cells belong to a group, you select the group in the Grouping pane.

- **Advanced**. You use advanced mode to display static and dynamic row and column group members of a selected tablix data region. In advanced mode, you can also set properties that control the visibility of the rows and columns that are associated with a group or group member. These properties determine the rules that renderers use to try to keep groups together on a page. Group members appear on the design surface as cells in the row group and column group areas.

> [!NOTE]
> To switch between default and advanced modes, you can right-click the down arrow to the right of the **Column Groups** icon.

For more information, see [Grouping pane](../../reporting-services/tools/grouping-pane.md).

### Toolbox pane

The Toolbox pane contains report items that you can drag to the design surface.

- Data regions are report items that you use to organize data on the report. Examples of data regions include a table, matrix, list, chart, gauge, data bar, sparkline, and indicator.
- Other report items include a map, text box, rectangle, line, image, and subreport.
- Custom report items might also appear in the Toolbox pane if your system administrator installs and registers them.

### Properties pane

The Properties pane is a standard [!INCLUDE[Visual Studio](../../includes/vsprvs-md.md)] window that shows property names and values for the currently selected report item on the design surface.

To display the Properties pane, you go to the **View** menu, and then select **Properties Window**. You can undock this pane and move it to another area of the SSDT window, or display it as a tabbed view on the design surface.

In most cases, property names correspond to elements and attributes in the Report Definition Language (RDL) file. You can set the most commonly used properties by using the Properties dialog for a selected item. To open the Properties dialog for an item, you select the item and then select the **Property Pages** button on the Properties pane toolbar. Advanced users can set property values directly in the Properties pane.

You can use the Properties pane for the following tasks:

- Set properties for the currently selected item on the design surface. Some properties provide a dropdown list of values. You can also enter the value directly in the cell. Some properties contain a collection of values, indicated by the value **(Collection)**. Most properties can accept an expression. Complex expressions are indicated by the value **\<expression>**. To open the **Expression** dialog, you select the **Expression (fx)** button. For more information, see [Expression dialog box](/previous-versions/sql/2014/reporting-services/expression-dialog-box).
- Use the Properties pane toolbar buttons to change the grid from a category view to an alphabetical view. In the category view, you might need to expand a category to see all the properties under it. To open an item's Properties dialog, you can select the **Property Pages** button on the toolbar. Or you can right-click the item and select **Properties**.
- Set properties for the currently selected group member in the Grouping pane. Group member properties help control how static group header and footer rows are repeated for each group instance. For more information, see [Display headers and footers with a group in a paginated report (Report Builder)](../../reporting-services/report-design/display-headers-and-footers-with-a-group-report-builder-and-ssrs.md).

### Solution Explorer

Solution Explorer is a standard [!INCLUDE[Visual Studio](../../includes/vsprvs-md.md)] component that displays all the items in your project. For a report server project, this list of items includes folders to organize shared data sources, shared datasets, reports, and resources. Folder items are automatically alphabetized when you open the solution file. To view item properties in the Properties pane, select the item.

### Output window

The Output window displays processing errors that occur when you preview a report. This window also displays publishing errors that occur when you deploy a report or a shared data source.

You can use the Output window to help debug errors in expressions.

### Document Outline pane

The Document Outline pane displays a hierarchical list of all report items in the report definition. To open the Document Outline pane, you can select **View** > **Other Windows** > **Document Outline**.

The Document Outline pane is useful for identifying text boxes and other report items by name. When you select an item in the Document Outline pane, the item is also selected on the design surface.

You can also use the Document Outline pane to help debug errors in expressions.

### Task List window

The Task List window displays build errors that occur when you import a report from another application. For instance, if you import a report from [!INCLUDE[Microsoft](../../includes/msconame-md.md)] Access and the report contains a feature that SSRS doesn't support, an error is reported in the Task List window.

## Report Designer views

Report Designer supports two views:

- **Design**, to define a report's data and layout
- **Preview**, to display a rendered view of a report

### Design view

When you create a report server project, Report Designer opens in Design view by default and displays the design surface. By default, the design surface displays the report body and the report background.

The design surface background has a shortcut menu. That menu provides options for adding a page header and page footer. It also contains a **View** menu that you can use to display the ruler, the Grouping pane, and the Parameters pane.

You can use the zoom control to increase or decrease the magnification of the report.

To design a report, you drag report items from the Toolbox pane to the design surface. Then you configure their properties and alter their arrangement on the report.

### Preview view

In the Preview view, you run the report and view the rendered report in the report viewer. You can also set configuration properties to run the report in debug mode by using a browser.

When you preview a report, Report Designer:

- Connects to the report data sources.
- Runs dataset queries.
- Caches the data on the local computer.
- Processes the report to combine data and layout.
- Renders the report.

There are a few points to keep in mind when you use the Preview view:

- **Parameterized reports**. When you preview a report, the report is processed automatically if all report parameters have valid default values. If one or more report parameters don't have a valid default value, you must choose a value for each unassigned parameter. On the report toolbar, you then have to select **View Report**.
- **The local data cache**. When you preview a report, the report processor runs all the queries for datasets in the report by using the current parameter defaults. It saves the results as a local data cache (.rdl.data) file. You can continue to design your report without incurring the overhead of retrieving this data again if you make no changes to either the report dataset queries or the report parameters.
- **Configuration Manager and debug**. In SSDT, project properties define how you want to deploy and debug your reports. These properties apply to all reports and shared data sources in the project. To set the project properties, you go to the **Project** menu and select **Properties**. In the Property Pages dialog, you then select **Configuration Manager**. You can use these settings to test your reports and publish them to the report server.
- **The Output pane**. When you preview a report and the report processor detects a problem, it writes error messages to the Output pane.

## Report Designer menus

When a Report Designer project is active in SSDT, the following menus are added to the main menu.

### Format menu

When you select an item on the design surface, the **Format** menu contains the following options:

| Option | Purpose |
| --- | --- |
| **Foreground Color** | Select a text color. Black is the default text color. |
| **Background Color** | Select a background color for your text boxes and data regions. |
| **Font** | Specify whether the text is bold, italic, or underlined. |
| **Justify** | Specify whether the text is right-aligned, centered, or left-aligned. |
| **Align** | Specify how the selected objects are aligned in relation to one another within the report. |
| **Make Same Size** | Adjust the size of the selected objects within the report. |
| **Horizontal Spacing** | Adjust the horizontal spacing between the selected objects within the report. |
| **Vertical Spacing** | Adjust the vertical spacing between the selected objects within the report. |
| **Center in Form** | Center the selected object vertically and horizontally in relation to the Report Designer window. |
| **Order** | Move selected objects into the background or foreground. |

### Report menu and design surface shortcut menu

You can use the following options to configure settings that apply to an entire report. The availability and location of these options depend on the version of [!INCLUDE[Visual Studio](../../includes/vsprvs-md.md)] that you use:

- In earlier versions, you can find these options in the **Report** menu.
- In later versions, most options are in the shortcut menu of the design surface background.

| Option | Purpose |
| --- | --- |
| **Report Properties** | Open the **Report Properties** dialog to assign general report properties, such as the author name, grid spacing, the number of columns, and the page size. You can also configure settings for custom code, references to assemblies and classes, and the names of data output elements, data transforms, and data schemas.|
| **View** | Switch between the two Report Designer tabs: Design and Preview. Show or hide the ruler, the Grouping pane, or the Parameters pane. |
| **Add Page Header** | Add a page header to the report or delete one. When you delete a page header, all items in the page header are deleted. |
| **Add Page Footer** | Add a page footer to the report or delete one. When you delete a page footer, all items in the page footer are deleted. |
| **Publish Report Parts** | Select report parts to publish. |

### View menu

You can use the following **View** menu options to show or hide various Report Designer windows and toolbars:

| Option | Component to show or hide |
| --- | --- |
| **Error List** | Errors that are detected when your publish or preview a report. |
| **Output** | Errors that are detected when you publish or process a report. Also, detailed information about expression errors when a report displays the text "#Error." |
| **Properties Window** | Property values for the currently selected report item on the design surface. You can use this option to see properties for nested report items. But you must select a report item multiple times to cycle through its hierarchy and nested members. To see which report item's properties are displayed, you can check the item name at the top of the Properties pane.|
| **Toolbox** | The toolbox. |
| **Other Windows** > **Document Outline** | A hierarchical view of report items and their collections of text boxes in a report. |
| **Toolbars** | Various toolbars, including **Report Borders** and **Report Formatting**. For more information, see [Report Designer toolbars](#report-designer-toolbars). |
| **Report Data** | The Report Data pane, where you can add report parameters, data sources, datasets, and images. |

### Project menu

You can use the following **Project** menu options to manage shared data sources and reports in a project. When you add or remove items from the project, the hierarchical display of project items in Solution Explorer is automatically updated.

| Option | Purpose |
| --- | --- |
| **Add New Item** | Add a new shared data source or new report to the project. |
| **Add Existing Item** | Add an existing shared data source or an existing report to the project. |
| **Import Reports** | Import reports from another application, for example, Microsoft Access. |
| **Exclude from Project** | Exclude items from the project. This option doesn't delete excluded items from your file system. |
| **Show All Files** | Show all files in a project. |
| **Refresh Project Toolbox Items** | Refresh the toolbox cache when you install new custom report items in your project. |
| **Properties** | Open the **Property Pages** dialog for this project. For more information, see [Project Property Pages dialog box](../../reporting-services/tools/project-property-pages-dialog-box.md). |

## Report Designer toolbars

Report Designer provides the following specialized toolbars to use when you design reports:

| Toolbar | Purpose |
| --- | --- |
| **Report** | Add a page header or page footer. Set report properties. Show or hide the ruler or Grouping pane. Use the zoom control to change your view of a report. |
| **Report Borders** | Set the color, style, and width for all selected lines and the borders of all selected report items. |
| **Report Formatting** | Set the format of selected report items. For text boxes, you can use the toolbar to change the following types of formatting: font properties, text color, background color, and text justification. |
| **Layout** | Set the drawing order of report items and merging cells within a data region. |
| **Standard** | Open or save projects, display windows, and select the Debug configuration. |

You can use the **View** menu to show or hide these toolbars. Other [!INCLUDE[Visual Studio](../../includes/vsprvs-md.md)] toolbars might be unavailable if their functionality doesn't apply to Report Designer features.

## Source control

SSDT can integrate with source plug-ins. You can use the Source Controls pages in the Options dialog to specify a plug-in and configure properties.

## Custom report templates

To use custom reports as templates for new reports, you copy them to the ReportProject folder on the computer on which SSDT is installed.

The default location of this folder depends on the version and edition of [!INCLUDE[Visual Studio](../../includes/vsprvs-md.md)] that you use. For [!INCLUDE[Visual Studio](../../includes/vsprvs-md.md)] 2022, 2019, and 2017, this folder is in the following location:

`%ProgramFiles%\Microsoft Visual Studio\<release-year>\<edition>\Common7\IDE\CommonExtensions\Microsoft\SSRS\ProjectItems\ReportProject`

For [!INCLUDE[Visual Studio](../../includes/vsprvs-md.md)] 2015, the default location is the following folder:

`%ProgramFiles%\Microsoft Visual Studio 14.0\Common7\IDE\Private Assemblies\ProjectItems\ReportProject`

When you add a new item to the report project, your custom report appears in the Templates pane. You can also add custom styles to the report wizard.

## Command-line support for SSDT

SSDT is based on [!INCLUDE[Visual Studio](../../includes/vsprvs-md.md)] and the underlying devenv.exe application. This application offers several command-line options that are useful for working with reports.

Before you can use the command-line options that this section describes, you must set valid values for the following two items:

- Project properties for `OverwriteDataSources`, `TargetDataSourceFolder`, `TargetReportFolder`, and `TargetServerURL`.
- At least one set of configuration properties, for example, Debug or Release.

For more information, see [Publishing data sources and reports](../../reporting-services/reports/publishing-data-sources-and-reports.md).

For a report server project, you can specify the following options from the command line:

- `/deploy`: Deploys reports by using the project properties that the configuration file specifies. For example, the following command deploys the reports that the solution file Reports.sln specifies. It uses the Release configuration settings that are specified in the project properties:

  ```
  devenv.exe "C:\Users\<user-name>\source\repos\Reports\Reports.sln" /deploy "Release"
  ```

- `/build`: Builds the solution file, but doesn't deploy it. For example, the following command builds the reports that the solution file Reports.sln specifies. It uses the Debug configuration settings that are specified in the project properties:

  ```
  devenv.exe "C:\Users\<user-name>\source\repos\Reports\Reports.sln" /build "Debug"
  ```

- `/out`: Redirects the output that's generated by building a solution to the specified file. For example, the following command redirects the output from the build in the previous example to a file named mybuildlog.txt.

  ```
  devenv.exe "C:\Users\<user-name>\source\repos\Reports\Reports.sln" /build "Debug" /out mybuildlog.txt
  ```

## Keyboard shortcuts in SSDT

You can use keyboard shortcuts to:

- Control windows and modes in SSDT:

    |Description|Key combination|
    |-----------------|---------------------|
    |Build the selected project|**Ctrl**+**Shift**+**B**|
    |Display the Properties window|**F4**|
    |Display the Report Data window|**Ctrl**+**Alt**+**D**|
    |Start debugging|**F5**|
    |Move from one open window to the next|**F6**|

- Control items on the report design surface:

    |Description|Key Combination|
    |-----------------|---------------------|
    |Move the focus from one report item to the next report item|**Tab**|
    |Move the selected report item|Arrow keys|
    |Nudge the selected report item|**Ctrl**+Arrow keys|
    |Increase or decrease the size of the selected report item|**Ctrl**+**Shift**+Arrow keys|
    |In a text box, move the cursor to the beginning of the display text that's visible|**Ctrl**+**Home**|
    |In a text box, move the cursor to the end of the display text that's visible|**Ctrl**+**End**|
    |In a text box, select text from the current cursor position to the beginning of the display text that's visible|**Shift**+**Home**|
    |In a text box, select text from the current cursor position to the end of the display text that's visible|**Shift**+**End**|
    |In a text box, select text from the current cursor position to the beginning of the expression|**Ctrl**+**Shift**+**Home**|
    |In a text box, select text from the current cursor position to the end of the expression|**Ctrl**+**Shift**+**End**|
    |Open the shortcut menu for the selected report item|**Shift**+**F10**|

## Related content

- [Solution Explorer](../../ssms/solution/solution-explorer.md)
- [Reporting Services reports (SSRS)](../../reporting-services/reports/reporting-services-reports-ssrs.md)
- [Report Definition Language (SSRS)](../../reporting-services/reports/report-definition-language-ssrs.md)
