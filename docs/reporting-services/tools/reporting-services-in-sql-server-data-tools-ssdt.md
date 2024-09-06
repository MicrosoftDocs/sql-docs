---
title: Reporting Services in SQL Server Data Tools (SSDT)
description: See how to use the SQL Server Data Tools Report Designer authoring environment in Microsoft Visual Studio to create solutions for Reporting Services.
author: maggiesMSFT
ms.author: maggies
ms.date: 09/06/2024
ms.service: reporting-services
ms.subservice: tools
ms.topic: conceptual
ms.custom: updatefrequency5
helpviewer_keywords:
  - "Business Intelligence Development Studio, Reporting Services in"

#customer intent: As a SQL Server user, I want to find out about the various components of Report Designer so that I can build reports efficiently and effectively.
---

# Reporting Services in SQL Server Data Tools (SSDT)

[!INCLUDE[SQL Server Data Tools (SSDT)](../../includes/ssbidevstudiofull-md.md)] is a [!INCLUDE[Microsoft](../../includes/msconame-md.md)] [!INCLUDE[Visual Studio](../../includes/vsprvs-md.md)] environment for creating business intelligence solutions. SSDT features the Report Designer authoring environment. You can use Report Designer to open, modify, preview, save, and deploy SQL Server Reporting Services (SSRS) paginated report definitions, shared data sources, shared datasets, and report parts.

[!INCLUDE [ssrs-report-parts-deprecated](../../includes/ssrs-report-parts-deprecated.md)]

This article describes SSDT solutions, projects, project templates, and configurations used for SSRS. It also describes the views, menus, toolbars, and shortcuts that you can use in Report Designer.  

## SSDT installation

SSDT isn't included with SQL Server. You need to install SSDT separately. For instructions, see [Install SSDT with Visual Studio](../../ssdt/download-sql-server-data-tools-ssdt.md#install-ssdt-with-visual-studio).

To use Report Designer templates, you also need to install an SSRS extension for Visual Studio. For instructions, see [Install extensions for Analysis Services, Integration Services, and Reporting Services](../../ssdt/download-sql-server-data-tools-ssdt.md#install-extensions-for-analysis-services-integration-services-and-reporting-services).

## Solutions and projects  

A report project acts as a container for report definitions and resources. Every file in the report project is published to the report server when the project is deployed. When you create a project for the first time, a solution is also created as a container for the project. You can add multiple projects to a single solution.
  
## Configurations  

To create multiple sets of project properties for deployment variations such as enterprise test and production report servers, you use the Configuration Manager feature in Visual Studio. For more information, see [Deployment and version support in SQL Server Data Tools (SSRS)](../../reporting-services/tools/deployment-and-version-support-in-sql-server-data-tools-ssrs.md).  
  
## Report server projects

When you install the SSRS extension for Visual Studio, as described earlier in [SSDT installation](#ssdt-installation), the following project templates are made available in SSDT:  

- **Report Server Project.** When you use the Report Server Project template, Report Designer opens. You can find this business intelligence project template in the **Create a new project** dialog. For more information, see [Create a report server project](../tutorial-step-01-create-report-server-project-reporting-services.md#create-a-report-server-project). Report Server project properties apply to all reports and all shared data sources in an SSDT project. These properties include the URL for the report server and the folder names for reports and shared data sources. You can use the **Project Property Pages** dialog to view the current property values. To open this dialog box, you go to the **Project** menu, and then select **Properties**.
  
- **Report Server Project Wizard.** When you use the Report Server Wizard Project, a report server project is automatically created, and the report wizard opens. In the wizard, you can create a report by following instructions on each page. The instructions describe how to:
  - Create a connection string to a data source.
  - Set data source credentials.
  - Design a query.
  - Add a table or matrix data region.
  - Specify report data and groups.
  - Pick a font and color style.
  - Publish the report to a report server.
  - Preview the report locally.

  After you create a report with the wizard, you can change the report data and the report designer by using Report Designer in the report server project.  

## Report Designer windows and panes

Report Designer offers multiple windows and panes to help you design reports and view rendered reports.
  
### Report Data pane

The Report Data pane displays:  

- **Built-in fields**. These fields contain predefined report information such as a report name or the time a report was processed.

- **Data sources**. A data source represents the name and connection information for a source of data.

- **Datasets**. Each dataset includes a query that specifies which data to retrieve from the data source. You can expand the dataset to view the collection of fields that the dataset query specifies.  
  
  In some query designers for multidimensional datasets, you can specify filters in the Filters pane and indicate whether to create report parameters. If you specify the report parameter option, a special dataset is automatically created to populate the parameter's valid values list. By default, the dataset doesn't appear in the Report Data pane. For more information, see [Show Hidden Datasets for Parameter Values for Multidimensional Data (Report Builder and SSRS)](../../reporting-services/report-data/show-hidden-datasets-for-parameter-values-multidimensional-data.md).  
  
- **Report parameters**. You can create report parameters manually or automatically when a dataset query includes query parameters.  
  
- **Images**. The images in this list are available to include as image report items in a report.  
  
Data sources and datasets in the Report Data pane represent the elements in the report definition. The Report Data pane is a feature that's supported by multiple report authoring environments. In Report Builder, it's the only pane that's available for managing data sources and datasets. In Report Designer, the Report Data pane works with Solution Explorer, which lists shared data sources and shared datasets as files. Shared data sources and shared datasets in the Report Data pane must point to their corresponding shared data sources and shared datasets in Solution Explorer. The Report Data pane elements then contain a reference to the data files in Solution Explorer. The project properties determine whether the shared data sources and shared datasets are deployed to the report server or SharePoint site. For more information, see [Convert data sources (Report Builder and SSRS)](../../reporting-services/report-data/convert-data-sources-report-builder-and-ssrs.md).  
  
> [!NOTE]  
> If you don't see the Report Data pane, click in the design area. Then on the **View** menu, select **Report Data**. If the Report Data pane is floating, you can anchor it. For more information, see [Dock the Report Data pane in Report Designer (SSRS)](../../reporting-services/tools/dock-the-report-data-pane-in-report-designer-ssrs.md).  

### Grouping pane

You use the Grouping pane to define groups for a tablix data region. You can define row groups and detail groups for tables and row and column groups for matrices. You can't use the Grouping pane to define groups for charts or other data regions. For more information, see [Understanding Groups (Report Builder and SSRS)](../../reporting-services/report-design/understanding-groups-report-builder-and-ssrs.md).  
  
The Grouping pane has two modes:  

- **Default**. You use default mode to display all row and column groups in a hierarchical format that shows the relationship of parent groups, child groups, adjacent groups, and detail groups. A child group appears under and at the next indent level compared to its parent group. An adjacent group appears at the same indent level as its peer or sibling groups.  
  
  You also use default mode to add, edit, or delete groups. For groups based a single dataset field, you drag the field to the Row Groups or Column Groups pane. You can insert the group next to an existing group. To add an adjacent group, you use the shortcut menu of the sibling group. To display which tablix cells belong to a group, you select the group in the Grouping pane.
  
- **Advanced**. You use advanced mode to display static and dynamic row and column group members of a selected tablix data region. In advanced mode, you can also set properties that control the visibility of the rows and columns that are associated with a group or group member. These properties determine the rules that renderers use to try to keep groups together on a page. Group members appear on the design surface as cells in the row group and column group areas.
  
> [!NOTE]  
> To switch between default and advanced modes, you can right-click the down arrow to the right of the **Column Groups** icon.  
  
For more information, see [Grouping pane](../../reporting-services/tools/grouping-pane.md).  
  
### Toolbox pane

The Toolbox pane contains report items that you can drag to the design surface.

- Data regions are report items that you use to organize data on the report. Examples of data regions include a table, matrix, list, chart, gauge, data bar, sparkline, and Indicator.
- Other report items include a map, text box, rectangle, line, image, and subreport.
- Custom report items might also appear in the Toolbox pane if your system administrator installs and registers them.  
  
### Properties pane

The Properties pane is a standard [!INCLUDE[Visual Studio](../../includes/vsprvs-md.md)] window that shows property names and values for the currently selected report item on the design surface.

To display the Properties pane, go to the **View** menu, and then select **Properties Window**. You can undock this pane and move it to another area of the SSDT window, or display it as a tabbed view on the design surface.

In most cases, property names correspond to elements and attributes in the Report Definition Language (RDL) file. You can set the most commonly used properties by using the Properties dialog for a selected item. To open the Properties dialog for an item, you select the item and then select the **Property Pages** button on the Properties pane toolbar. Advanced users can set property values directly in the Properties pane.  
  
You can use the Properties pane for the following tasks:  
  
- Set properties for the currently selected item on the design surface. Some properties provide a dropdown list of values. You can also enter the value directly in the cell. Some properties contain a collection of values, indicated by the value **(Collection)**. Most properties can accept an expression. Complex expressions are indicated by the value **\<Expression>**. To open the **Expression** dialog, you select the **Expression (fx)** button. For more information, see [Expression dialog box](/previous-versions/sql/2014/reporting-services/expression-dialog-box).  
- Use the Properties pane toolbar buttons to change the grid from a category view to an alphabetical view. In the category view, you might need to expand a category to see all the properties under it. To open an item's Properties dialog, you can select the **Property Pages** button on the toolbar. Or you can right-click the item and select **Properties**.  
- Set properties for the currently selected group member in the Grouping pane. Group member properties help control how static group header and footer rows repeat for each group instance. For more information, see [Display headers and footers with a group (Report Builder and SSRS)](../../reporting-services/report-design/display-headers-and-footers-with-a-group-report-builder-and-ssrs.md).

### Solution Explorer

Solution Explorer is a standard [!INCLUDE[vsprvs](../../includes/vsprvs-md.md)] component that displays all the items in your project. For a report server project, this list of items includes folders to organize shared data sources, shared datasets, reports and resources. Folder items are automatically alphabetized when you open the solution file. To view item properties in the Properties pane, select the item.

### Output window

The Output window displays processing errors that occur when you preview a report. This window also displays publishing errors that occur when you deploy a report or a shared data source.

You can use the Output and the Document Outline windows to help debug errors in expressions.

### Document Outline pane

The Document Outline pane displays a hierarchical list of all report items in the report definition. To open the Document Outline pane, you can select **View** > **Other Windows** > **Document Outline**.  
  
The Document Outline pane is useful for identifying text boxes and other report items by name. When you select an item in the Document Outline pane, the item is also selected on the design surface.  
  
### Task List window

The Task List window displays build errors that occur when you import a report from another application. For instance, if you import a report from [!INCLUDE[Microsoft](../../includes/msconame-md.md)] Access and the report contains a feature that SSRS doesn't support, an error is reported in the Task List window.
  
## Report Designer views

Report Designer supports two views:

- **Design**, to define a report's data and layout
- **Preview**, to display a rendered view of a report

### Design view  

When you create a report server project, Report Designer opens in Design view by default and displays the design surface. By default, the design surface displays the report body and the report background.
  
The design surface background has a shortcut menu. That menu provides options for adding a page header and page footer. It also contains a **View** menu that you can use to display a ruler, the Grouping pane, and the Parameters pane.  
  
You can use the zoom control to increase or decrease the magnification of the report.  
  
To design a report, you drag report items from the Toolbox pane to the design surface. Then you configure their properties and alter their arrangement on the report.  

### Preview view

In the Preview view, you run the report and view the rendered report in the report viewer. The preview caches report data locally. You can also set configuration properties to run the report in debug mode by using a browser.
  
When you preview a report, Report Designer:

- Connects to the report data sources.
- Runs dataset queries
- Caches the data on the local computer.
- Processes the report to combine data and layout.
- Renders the report.

You can view the report on the Preview tab, or you can set up project properties to view the report in debug mode and view it directly in a browser.  

- **Previewing parameterized reports**. When you preview a report, the report is processed automatically if all report parameters have valid default values. If one or more report parameters don't have a valid default value, you must choose a value for each unassigned parameter. On the report toolbar, you then have to select **View Report**.  
- **Understanding the local data cache**. When you preview a report, the report processor runs all the queries for datasets in the report by using the current parameter defaults. It saves the results as a local data cache (.rdl.data) file. You can continue to design your report without incurring the overhead of retrieving this data again if you make no changes to either the report dataset queries or the report parameters.  
- **Previewing the report by using Configuration Manager and debug.** In SSDT, project properties define how you want to deploy and debug your reports. These properties apply to all reports and shared data sources in the project. To set the project properties, you go to the **Project** menu and select **Properties**. In the Property Pages dialog, you then select **Configuration Manager**. You can use these settings to test your reports and publish them to the report server.
- **Monitoring the Output pane for error messages.** When you preview a report and the report processor detects a problem, it writes error messages to the Output pane.  

## Report Designer menus

When a Report Designer project is active in SSDT, the following toolbars are added to the main toolbar. The Report Designer menus are visible only in Design view.

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
  
### Report menu

When the report design surface has focus, the **Report** menu contains the following options:  
  
-   **Report Properties** Select to open the **Report Properties** dialog box. In this dialog box, you can assign general report properties, such as author name and grid spacing, and specify properties for the report layout, such as the number of columns and page size. You can also include custom code, references to assemblies and classes, and the names of data output elements, data transforms, and data schemas.  
  
-   **View** Switch between the two Report Designer tabs: Design and Preview.  
  
-   **Page Header** Add or delete a page header to the report. When you delete a page header, all items in the page header are deleted.  
  
-   **Page Footer** Add or delete a page footer to the report. When you delete a page footer, all items in the page footer are deleted.  
  
-   **Grouping Pane** Show or hide the Grouping pane.  
  
###  <a name="ViewMenu"></a> View menu  
 Use the **View** menu to display Report Designer windows and toolbars  
  
-   **Error List** Use this option to display errors detected when publishing or previewing a report.  
  
-   **Output** Use this option to display errors detected when publishing or processing a report, or for more information about expression errors when a report displays the text "#Error".  
  
-   **Properties Window** Use this option to display the property values for the currently selected report item on the design surface. To see properties for nested report items, you must select a report item multiple times to cycle through the hierarchy for a report item and its nested members. Check the name of the item that appears at the top of the Properties pane to see which report item's properties are displayed.  
  
-   **Toolbox** Use this option to display the Toolbox.  
  
-   **Other Windows** Use this option to display the following pane:  
  
    -   **Document Outline** Use this option to display a hierarchical view of report items and their collections of text boxes in a report.  
  
-   **Toolbars** Use this option to display toolbars that support Report Designer features, including **Report Borders** and **Report Formatting**. For more information, see [Report Designer Toolbars](#bkmk_ReportDesignerToolbars).  
  
-   **Report Data** Use this option to display the Report Data pane, where you can add report parameters, data sources, datasets, images.  
  
###  <a name="ProjectMenu"></a> Project menu  

Use the **Project** menu to manage shared data sources and reports in a project. When you add or remove items from the project, the hierarchical display of project items in Solution Explorer is automatically updated.

Mention that the menu includes these options (because it also contains others).
  
-   **Add New Item** Add a new shared data source or new report to the project.  
  
-   **Add Existing Item** Add an existing shared data source or an existing report to the project.  
  
-   **Import Reports** Import reports from another application, for example, Microsoft Access.  
  
-   **Exclude from Project** Exclude items from the project. This option doesn't delete the item from your file system.  
  
-   **Show All Files** Show all files in a project.  
  
-   **Refresh Project Toolbox Items** Refresh the toolbox cache when you install new custom report items in your project.  
  
-   **Properties** Open the **Property Pages** dialog box for this project. For more information, see [Project Property Pages Dialog Box](../../reporting-services/tools/project-property-pages-dialog-box.md).  
  
  
##  <a name="bkmk_ReportDesignerToolbars"></a> Report Designer toolbars  
 Report Designer provides the following specialized toolbars to use when designing reports:  
  
-   **Report** Add a page header or page footer, set report properties, toggle the ruler or Grouping pane, or use zoom to change your view of the report.  
  
-   **Report Borders** Set the color, style, and width for all selected lines and the borders of all selected report items.  
  
-   **Report Formatting** Set the format of selected report items. For text boxes, the following types of formatting can be changed by using the toolbar: font properties and text color, background color, and text justification.  
  
-   **Layout** Set the drawing order of report items and merging cells within a data region.  
  
-   **Standard** Open or save projects, display windows, and select the Debug configuration.  
  
 Use the **View** menu to control whether to display these toolbars. Other [!INCLUDE[vsprvs](../../includes/vsprvs-md.md)] toolbars might be disabled if their functionality doesn't apply to Report Designer features.  
  

##  <a name="bkmk_SourceControl"></a> Source control  
 [!INCLUDE[ssBIDevStudio](../../includes/ssbidevstudio-md.md)] can integrate with source plug-ins. Use the Projects and Solutions pages in the **Options** dialog box to specify the plug-in and configure properties.  
  
##  <a name="bkmk_CustomReportTemplates"></a> Custom report templates  
 To use custom reports as templates for new reports, you copy them to the ReportProject folder on the computer on which [!INCLUDE[ssBIDevStudio](../../includes/ssbidevstudio-md.md)] is installed. By default, this folder is in the following location:
 `<drive>:\Program Files\Microsoft Visual Studio 14.0\Common7\IDE\Private Assemblies\ProjectItems\ReportProject`. 
   When you add a new item to the report project, your custom report appears in the Templates pane.  
  
 You can also add custom styles to the report wizard.  
  
  
##  <a name="bkmk_CommandLineSupportForssdt"></a> Command line support for SQL Server Data Tools  
 [!INCLUDE[ssBIDevStudio](../../includes/ssbidevstudio-md.md)] is based on [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[vsprvs](../../includes/vsprvs-md.md)] and the underlying devenv.exe application. Before you can use these options, you must set valid values for following two items:  
  
-   Project properties for OverwriteDataSources, TargetDataSourceFolder, TargetReportFolder, and TargetServerURL.  
  
-   At least one set of configuration properties, for example, Debug or Release.  
  
 For more information, see [Publishing Data Sources and Reports](../../reporting-services/reports/publishing-data-sources-and-reports.md).  
  
 For a report server project, you can specify the following options from the command line:  
  
-   **/deploy** Deploy reports by using the project properties specified in a configuration file. For example, the following command deploys the reports specified by the solution file Reports.sln by using the Release configuration settings that are specified in the project properties:  
  
    ```  
    devenv.exe "C:\Users\MyUser\Documents\Visual Studio 2015\Projects\Reports\Reports.sln" /deploy "Release"  
    ```  
  
-   **/build** Build the solution file, but don't deploy it. For example, the following command builds the reports specified by the solution file Reports.sln by using the Debug configuration settings that are specified in the project properties:  
  
    ```  
    devenv.exe "C:\Users\MyUser\Documents\Visual Studio 2015\Projects\Reports\Reports.sln" /build "Debug"  
    ```  
  
-   **/out** Redirect the output generated by building a solution to the specified file. For example, the following command redirects the output from the build in the previous example to a file named mybuildlog.txt.  
  
    ```  
    devenv.exe "C:\Users\MyUser\Documents\Visual Studio 2015\Projects\Reports\Reports.sln" /build "Debug" /out mybuildlog.txt  
    ```  
  
##  <a name="bkmk_KeyboardShortcuts"></a> Keyboard shortcuts in Reporting Services  
 Use keyboard shortcuts to:  
  
-   Control windows and modes in [!INCLUDE[ssBIDevStudio](../../includes/ssbidevstudio-md.md)]:  
  
    |Description|Key Combination|  
    |-----------------|---------------------|  
    |Build the selected project|CTRL+SHIFT+B|  
    |Display Properties window|F4|  
    |Display Data window|CTRL+Alt+D|  
    |Start debugging|F5|  
    |Move from one open window to the next|F6|  
  
-   Control items on the report design surface:  
  
    |Description|Key Combination|  
    |-----------------|---------------------|  
    |Move focus from one report item to the next report item|TAB|  
    |Move selected report item|Arrow keys|  
    |Nudge selected report item|CTRL+Arrow keys|  
    |Increase or decrease the size of the selected report item|CTRL+SHIFT+Arrow keys|  
    |In a text box, move cursor to the beginning of the display text that is visible|CTRL+HOME|  
    |In a text box, move cursor to the end of the display text that is visible|CTRL+END|  
    |In a text box, select text from the current cursor position to the beginning of the display text that is visible|SHIFT+HOME|  
    |In a text box, select text from the current cursor position to the end of the display text that is visible|SHIFT+END|  
    |In a text box, select text from the current cursor position to the beginning of the expression|CTRL+SHIFT+HOME|  
    |In a text box, select text from the current cursor position to the end of the expression|CTRL+SHIFT+END|  
    |Open the shortcut menu for the selected report item|SHIFT+F10+Property Key on newer keyboards|
  
## Related content

[Download SQL Server Data Tools](../../ssdt/download-sql-server-data-tools-ssdt.md)
[Solution explorer](../../ssms/solution/solution-explorer.md)   
[Reporting Services reports](../../reporting-services/reports/reporting-services-reports-ssrs.md)   
[Report Definition Language](../../reporting-services/reports/report-definition-language-ssrs.md)   
[Deployment and version support in SQL Server Data Tools](../../reporting-services/tools/deployment-and-version-support-in-sql-server-data-tools-ssrs.md)  

More questions? [Try asking the Reporting Services forum](/answers/search.html?c=&f=&includeChildren=&q=ssrs+OR+reporting+services&redirect=search%2fsearch&sort=relevance&type=question+OR+idea+OR+kbentry+OR+answer+OR+topic+OR+user)
