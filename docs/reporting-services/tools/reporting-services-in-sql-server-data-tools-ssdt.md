---
title: "Reporting Services in SQL Server Data Tools (SSDT) | Microsoft Docs"
ms.date: 05/30/2017
ms.prod: reporting-services
ms.prod_service: "reporting-services-sharepoint, reporting-services-native"
ms.technology: tools


ms.topic: conceptual
helpviewer_keywords: 
  - "Business Intelligence Development Studio, Reporting Services in"
ms.assetid: 0903c7b2-ac59-45f1-b7d0-922ecd9d76f8
author: markingmyname
ms.author: maghan
---

# Reporting Services in SQL Server Data Tools (SSDT)

  [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)] is a [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[vsprvs](../../includes/vsprvs-md.md)] environment for creating business intelligence solutions. SSDT features the Report Designer authoring environment, where you can open, modify, preview, save, and deploy [!INCLUDE[ssRSnoversion_md](../../includes/ssrsnoversion-md.md)] paginated report definitions, shared data sources, shared datasets, and report parts. [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)] is not included with SQL Server. Download [SQL Server Data Tools](https://go.microsoft.com/fwlink/?LinkID=616714). 
  
 This topic describes [!INCLUDE[ssBIDevStudio](../../includes/ssbidevstudio-md.md)] solutions, projects, project templates, and configurations used for [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)], and the views, menus, toolbars, and shortcuts that you can use in Report Designer.  
  
 To get started designing reports, see [Design Reports with Report Designer &#40;SSRS&#41;](../../reporting-services/tools/design-reporting-services-paginated-reports-with-report-designer-ssrs.md).  
  
##  <a name="bkmk_SolutionsandProjects"></a> Solutions and Projects  
 A report project acts as a container for report definitions and resources. Every file in the report project is published to the report server when the project is deployed. When you create a project for the first time, a solution is also created as a container for the project. You can add multiple projects to a single solution.  
  
  
##  <a name="bkmk_Configurations"></a> Configurations  
 To create multiple sets of project properties for deployment variations such as enterprise test and production report servers, use the Configuration Manager. For more information, see [Deployment and Version Support in SQL Server Data Tools &#40;SSRS&#41;](../../reporting-services/tools/deployment-and-version-support-in-sql-server-data-tools-ssrs.md).  
  
##  <a name="bkmk_ReportServerProjects"></a> Report Server Projects  
 When you install [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)], the following project templates are made available in [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)]:  
  
-   **Report Server Project.** When you select a Report Server Project, Report Designer opens. A Report Server Project is a Business Intelligence Projects template installed by [!INCLUDE[vsprvs](../../includes/vsprvs-md.md)] that is available from the **New Project** dialog box. For more information, see [Add a New or Existing Report to a Report Project &#40;SSRS&#41;](../../reporting-services/tools/add-a-new-or-existing-report-to-a-report-project-ssrs.md).Report Server project properties apply to all reports and all shared data sources in a [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)] project. These properties include the URL for the report server and the folder names for reports and shared data sources. Use the **Project Property Pages** dialog box to view the current property values. To open this dialog box, on the **Project** menu, click **Properties**.  
  
-   **Report Server Project Wizard.** When you select a Report Server Wizard Project, a report server project is automatically created, and the Report Wizard opens. In the wizard, you can create a report by following instructions on each page to create a connection string to a data source, set data source credentials, design a query, add a table or matrix data region, specify report data and groups, pick a font and color style, publish the report to a report server, and preview the report locally. After you create a report with the wizard, you can change the report data and the report designer by using Report Designer in the Report Server project.  
  
 ![New Project templates in SSDT](../../analysis-services/media/ssdt-biprojects.png "New Project templates in SSDT")  
  
  
##  <a name="bkmk_ReportDesignerWindowsandPanes"></a> Report Designer Windows and Panes  
 Report Designer supports two views: **Design** to define the report data and report layout, and **Preview** to display a rendered view of the report. In each view, you can display multiple windows to help you design or view a rendered report.  
  
###  <a name="bkmk_ReportDataPane"></a> Report Data Pane  
 The Report Data pane displays built-in fields, data sources, datasets, field collections, report parameters, and images.  
  
 Use the Report Data pane to view:  
  
-   **Built-in Fields** Predefined report information such as the report name or the time the report was processed.  
  
-   **Data sources** A data source represents a name and connection to a source of data.  
  
-   **Datasets** Each dataset includes a query that specifies which data to retrieve from the data source. Expand the dataset to view the collection of fields specified by the dataset query.  
  
     In some query designers for multidimensional datasets, you can specify filters in the Filters pane and indicate whether to create report parameters. If you specify the report parameter option, a special dataset is automatically created to populate the parameter's valid values list.  By default, the dataset does not appear in the Report Data Pane. For more information, see [Show Hidden Datasets for Parameter Values for Multidimensional Data &#40;Report Builder and SSRS&#41;](../../reporting-services/report-data/show-hidden-datasets-for-parameter-values-multidimensional-data.md).  
  
-   **Report parameters** The list of report parameters. Parameters can be created manually or automatically when a dataset query includes query parameters.  
  
-   **Images** The list of images that are available to include as an Image report item in a report.  
  
 Data sources and datasets in the Report Data pane represent the elements in the report definition. The Report Data pane is a feature supported by multiple report authoring environments. In Report Builder, it is the only pane available for managing data sources and datasets. In Report Designer, the Report Data pane works with Solution Explorer, which lists shared data sources and shared datasets as files. Shared data sources and shared datasets in the Report Data pane must point to their corresponding Shared Data Sources and Shared Datasets in Solution Explorer. The Report Data pane elements then contain a reference to the data files in Solution Explorer. The project properties determine whether the shared data sources and shared datasets are deployed to the report server or SharePoint site. For more information, see [Convert Data Sources &#40;Report Builder and SSRS&#41;](../../reporting-services/report-data/convert-data-sources-report-builder-and-ssrs.md).  
  
> [!NOTE]  
>  If you do not see the Report Data pane, click in the Design area and then on the **View** menu, click **Report Data**. If the Report Data pane is floating, you can anchor it. For more information, see [Dock the Report Data Pane in Report Designer &#40;SSRS&#41;](../../reporting-services/tools/dock-the-report-data-pane-in-report-designer-ssrs.md).  
  
  
###  <a name="bkmk_GroupingPane"></a> Grouping Pane  
 Use the Grouping pane to define groups for a tablix data region. You can define row groups and detail groups for tables and row and column groups for matrices. You cannot use the Grouping pane to define groups for Charts or other data regions. For more information, see [Understanding Groups &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/understanding-groups-report-builder-and-ssrs.md).  
  
 The Grouping pane has two modes:  
  
-   **Default.** Use the **Default** mode to display all row and column groups in a hierarchical format that shows the relationship of parent groups, child groups, adjacent groups, and detail groups. A child group appears under and at the next indent level compared to its parent group. An adjacent group appears at the same indent level as its peer or sibling groups.  
  
     Use default mode to add, edit, or delete groups. For groups based a single dataset field, drag the field to the Row Groups or Column Groups pane. You can insert the group above or below an existing group. To add an adjacent group, right-click the sibling group, and use the shortcut menu. To display which tablix cells belong to a group, select the group in the Grouping pane.  
  
-   **Advanced.** Use the **Advanced** mode to display static and dynamic row and column group members of the selected tablix data region.  You must use group members to set properties that control the visibility of the rows and columns associated with a group or group member, or the rules that renderers use to try to keep groups together on a page. Group members appear on the design surface as cells in the row group and column group areas.  
  
> [!NOTE]  
>  To toggle between **Default** and **Advanced** modes, right-click the down arrow to the right of the **Column Groups** icon.  
  
 For more information, see [Grouping Pane](../../reporting-services/tools/grouping-pane.md).  
  
  
###  <a name="bkmk_Toolbox"></a> Toolbox  
 The Toolbox contains report items that you can drag to the design surface. Data regions are report items that you use to organize data on the report. Table, Matrix, List, Chart, Gauge, Data Bar, Sparkline, and Indicator are data regions. Other report items include Map, Text Box, Rectangle, Line, Image, and Subreport. Custom report items might also appear on this list if they have been installed and registered by your system administrator.  
  
###  <a name="bkmk_PropertiesPane"></a> Properties Pane  
 The Properties pane is a standard [!INCLUDE[vsprvs](../../includes/vsprvs-md.md)] window that shows property names and values for the currently selected report item on the design surface. In most cases, property names correspond to elements and attributes in the Report Definition Language (RDL) file. The most commonly used properties can be set by using the Properties dialog box for the selected item. To open the corresponding dialog box, click the **Property Pages** button on the Properties pane toolbar. Advanced users can set property values directly in the Properties pane.  
  
 Use the Properties pane to:  
  
-   Set properties for the currently selected item on the design surface. Some properties provide a drop-down list of values. You can also type the value directly in the cell. Some properties contain a collection of values, indicated by the value **(Collection)**. Most properties can accept an expression; complex expressions are indicated by the value **\<Expression>**. Click **\<Expression>** to open the **Expression** dialog box. For more information, see [Expression Dialog Box](https://msdn.microsoft.com/library/e6c74ccb-4594-4d4f-b958-618d710e34eb).  
  
-   Use the Properties pane toolbar buttons to change the grid from category view to alphabetical view. In category view, you may need to expand a category to see all the properties under it. To open an item's Properties dialog box, click the **Property Pages** button on the toolbar or by right-click the item and click **Properties**.  
  
-   Set properties for the currently selected group member in the Grouping pane. Group member properties help control how static group header and footer rows repeat for each group instance. For more information, see [Display Headers and Footers with a Group &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/display-headers-and-footers-with-a-group-report-builder-and-ssrs.md).  
  
 To display the Properties pane, from the **View** menu, click **Properties Window**. You can undock this pane and move it to another area of the [!INCLUDE[ssBIDevStudio](../../includes/ssbidevstudio-md.md)]window, or display it as a tabbed view on the design surface.  
  
  
###  <a name="bkmk_SolutionExplorer"></a> Solution Explorer  
 Solution Explorer is a standard [!INCLUDE[vsprvs](../../includes/vsprvs-md.md)] component that displays all the items in your project. For a Report Server project, this includes folders to organize shared data sources, shared datasets, reports and resources. Folder items are automatically alphabetized when you open the solution file. To view item properties in the Properties pane, select the item.  
  
###  <a name="bkmk_Output"></a> Output  
 The Output window displays processing errors when you preview a report, and publishing errors when you deploy a report or a shared data source.  
  
 Use the Output and the Document Outline windows to help debug errors in expressions.  
  
  
###  <a name="bkmk_DocumentOutline"></a> Document Outline  
 The Document Outline window displays a hierarchical list of all report items in the report definition. To open the Document Outline pane, from the **View** menu, point to **Other Windows** and click **Document Window**.  
  
 Use the Document Outline pane to help identify text boxes and other report items by name. When you select an item in the Document Outline, the item is also selected on the Design Surface.  
  
###  <a name="bkmk_TaskList"></a> Task List  
 The Task List window displays build errors for unsupported features when you import a report from another application, such as [!INCLUDE[msCoName](../../includes/msconame-md.md)] Access.  
  
  
##  <a name="bkmk_ReportDesignerDesignView"></a> Report Designer Design View  
 By default, when you create a Report Server project, Report Designer opens in Design view and displays the design surface. By default, the design surface displays the report body and the report background.  
  
 The shortcut menu on the background provides options to add a page header and page footer, and from the View menu, display a ruler and the Grouping pane.  
  
 Use the zoom control to increase or decrease the magnification of the report.  
  
 To design a report, drag report items from the Toolbox to the design surface, and then configure their properties and alter their arrangement on the report.  
  
  
##  <a name="bkmk_ReportDesignerPreview"></a> Report Designer Preview  
 Use Preview to run the report and view the rendered report in the report viewer. Preview caches report data locally. You can also set configuration properties to run the report in debug view, using a browser.  
  
 When you preview a report, Report Designer connects to the report data sources, runs dataset queries, caches the data on the local computer, processes the report to combine data and layout, and renders the report. You can view the report in on the Preview tab or you can set up project properties to view the report in debug mode and view it directly in a browser.  
  
-   **Previewing Parameterized Reports.** When you preview a report, the report is processed automatically if all report parameters have valid default values. If one or more report parameters do not have a valid default value, you must choose a value for each unassigned parameter, and then, on the report toolbar, click **View Report**.  
  
-   **Understanding the Local Data Cache** When you preview a report, the report processor runs all the queries for datasets in the report using the current parameter defaults, and saves the results as a local data cache (.rdl.data) file. You can continue to design your report without incurring the overhead of retrieving this data again if you make no changes to either the report dataset queries or the report parameters.  
  
-   **Previewing the Report using Configuration Manager and Debug.** In [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)], project properties define how you want to deploy and debug your reports. These properties apply to all reports and shared data sources in the project. To set the project properties, from the **Project** menu, click **Properties**. Use these settings to test your reports and publish them to the report server.  
  
-   **Monitoring the Output Pane for Error Messages.** When you preview a report and the report processor detects a problem, it writes error messages to the Output pane.  
  
  
##  <a name="bkmk_ReportDesignerMenus"></a> Report Designer Menus  
 When a Report Designer project is active in [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)], the following toolbars are added to the main toolbar. The Report Designer menus are visible only when in Design view.  
  
###  <a name="FormatMenu"></a> Format Menu  
 When you select an item on the design surface, the **Format** menu contains the following options:  
  
-   **Foreground Color** Select a text color. Black is the default text color.  
  
-   **Background Color** Select a background color for your text boxes and data regions.  
  
-   **Font** Specify whether the text is bold, italic, or underlined.  
  
-   **Justify** Specify whether the text is right-aligned, centered, or left-aligned.  
  
-   **Align** Specify how the selected objects are aligned in relation to one another within the report.  
  
-   **Make Same Size** Adjust the size of the selected objects within the report.  
  
-   **Horizontal Spacing** Adjust the horizontal spacing between the selected objects within the report.  
  
-   **Vertical Spacing** Adjust the vertical spacing between the selected objects within the report.  
  
-   **Center in Form** Center the selected object vertically and horizontally in relation to the Report Designer window.  
  
-   **Order** Move selected objects into the background or foreground.  
  
###  <a name="ReportMenu"></a> Report Menu  
 When the report design surface has focus, the **Report** menu contains the following options:  
  
-   **Report Properties** Select to open the **Report Properties** dialog box. In this dialog box, you can assign general report properties, such as author name and grid spacing, and specify properties for the report layout, such as the number of columns and page size. You can also include custom code, references to assemblies and classes, and the names of data output elements, data transforms, and data schemas.  
  
-   **View** Switch between the two Report Designer tabs: Design and Preview.  
  
-   **Page Header** Add or delete a page header to the report. When you delete a page header, all items in the page header are deleted.  
  
-   **Page Footer** Add or delete a page footer to the report. When you delete a page footer, all items in the page footer are deleted.  
  
-   **Grouping Pane** Show or hide the Grouping pane.  
  
###  <a name="ViewMenu"></a> View Menu  
 Use the **View** menu to display Report Designer windows and toolbars  
  
-   **Error List** Use this option to display errors detected when publishing or previewing a report.  
  
-   **Output** Use this option to display errors detected when publishing or processing a report, or for more information about expression errors when a report displays the text "#Error".  
  
-   **Properties Window** Use this option to display the property values for the currently selected report item on the design surface. To see properties for nested report items, you must click a report item multiple times to cycle through the hierarchy for a report item and its nested members. Check the name of the item that appears at the top of the Properties pane to see which report item's properties are displayed.  
  
-   **Toolbox** Use this option to display the Toolbox.  
  
-   **Other Windows** Use this option to display the following pane:  
  
    -   **Document Outline** Use this option to display a hierarchical view of report items and their collections of text boxes in a report.  
  
-   **Toolbars** Use this option to display toolbars that support Report Designer features, including **Report Borders** and **Report Formatting**. For more information, see [Report Designer Toolbars](#bkmk_ReportDesignerToolbars).  
  
-   **Report Data** Use this option to display the Report Data pane, where you can add report parameters, data sources, datasets, images.  
  
###  <a name="ProjectMenu"></a> Project Menu  
 Use the **Project** menu to manage shared data sources and reports in a project. When you add or remove items from the project, the hierarchical display of project items in Solution Explorer is automatically updated.  
  
-   **Add New Item** Add a new shared data source or new report to the project.  
  
-   **Add Existing Item** Add an existing shared data source or an existing report to the project.  
  
-   **Import Reports** Import reports from another application, for example, Microsoft Access.  
  
-   **Exclude from Project** Exclude items from the project. This option does not delete the item from your file system.  
  
-   **Show All Files** Show all files in a project.  
  
-   **Refresh Project Toolbox Items** Refresh the toolbox cache when you install new custom report items in your project.  
  
-   **Properties** Open the **Property Pages** dialog box for this project. For more information, see [Project Property Pages Dialog Box](../../reporting-services/tools/project-property-pages-dialog-box.md).  
  
  
##  <a name="bkmk_ReportDesignerToolbars"></a> Report Designer Toolbars  
 Report Designer provides the following specialized toolbars to use when designing reports:  
  
-   **Report** Add a page header or page footer, set report properties, toggle the ruler or Grouping pane, or use zoom to change your view of the report.  
  
-   **Report Borders** Set the color, style, and width for all selected lines and the borders of all selected report items.  
  
-   **Report Formatting** Set the format of selected report items. For text boxes, the following types of formatting can be changed using the toolbar: font properties and text color, background color, and text justification.  
  
-   **Layout** Set the drawing order of report items and merging cells within a data region.  
  
-   **Standard** Open or save projects, display windows, and select the Debug configuration.  
  
 Use the **View** menu to control whether to display these toolbars. Other [!INCLUDE[vsprvs](../../includes/vsprvs-md.md)] toolbars may be disabled if their functionality does not apply to Report Designer features.  
  

##  <a name="bkmk_SourceControl"></a> Source Control  
 [!INCLUDE[ssBIDevStudio](../../includes/ssbidevstudio-md.md)] can integrate with source plug-ins. Use the Projects and Solutions pages in the **Options** dialog box to specify the plug-in and configure properties.  
  
##  <a name="bkmk_CustomReportTemplates"></a> Custom Report Templates  
 To use custom reports as templates for new reports, you simply copy them to the ReportProject folder on the computer on which [!INCLUDE[ssBIDevStudio](../../includes/ssbidevstudio-md.md)] is installed. By default, this folder is in the following location:
 `<drive>:\Program Files\Microsoft Visual Studio 14.0\Common7\IDE\Private Assemblies\ProjectItems\ReportProject`. 
   When you add a new item to the report project, your custom report appears in the Templates pane.  
  
 You can also add custom styles to the report wizard.  
  
  
##  <a name="bkmk_CommandLineSupportForssdt"></a> Command Line Support for SQL Server Data Tools  
 [!INCLUDE[ssBIDevStudio](../../includes/ssbidevstudio-md.md)] is based on [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[vsprvs](../../includes/vsprvs-md.md)] and the underlying devenv.exe application. Before you can use these options, you must set valid values for following two items:  
  
-   Project properties for OverwriteDataSources, TargetDataSourceFolder, TargetReportFolder, and TargetServerURL.  
  
-   At least one set of configuration properties, for example, Debug or Release.  
  
 For more information, see [Publishing Data Sources and Reports](../../reporting-services/reports/publishing-data-sources-and-reports.md).  
  
 For a report server project, you can specify the following options from the command line:  
  
-   **/deploy** Deploy reports by using the project properties specified in a configuration file. For example, the following command deploys the reports specified by the solution file Reports.sln by using the Release configuration settings that are specified in the project properties:  
  
    ```  
    devenv.exe "C:\Users\MyUser\Documents\Visual Studio 2015\Projects\Reports\Reports.sln" /deploy "Release"  
    ```  
  
-   **/build** Build the solution file, but do not deploy it. For example, the following command builds the reports specified by the solution file Reports.sln by using the Debug configuration settings that are specified in the project properties:  
  
    ```  
    devenv.exe "C:\Users\MyUser\Documents\Visual Studio 2015\Projects\Reports\Reports.sln" /build "Debug"  
    ```  
  
-   **/out** Redirect the output generated by building a solution to the specified file. For example, the following command redirects the output from the build in the previous example to a file named mybuildlog.txt.  
  
    ```  
    devenv.exe "C:\Users\MyUser\Documents\Visual Studio 2015\Projects\Reports\Reports.sln" /build "Debug" /out mybuildlog.txt  
    ```  
  
##  <a name="bkmk_KeyboardShortcuts"></a> Keyboard Shortcuts in Reporting Services  
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
  
## Next steps

[Download SQL Server Data Tools](https://go.microsoft.com/fwlink/?LinkID=616714)
[Solution Explorer](../../ssms/solution/solution-explorer.md)   
[Reporting Services Reports](../../reporting-services/reports/reporting-services-reports-ssrs.md)   
[Report Definition Language](../../reporting-services/reports/report-definition-language-ssrs.md)   
[Deployment and Version Support in SQL Server Data Tools](../../reporting-services/tools/deployment-and-version-support-in-sql-server-data-tools-ssrs.md)  

More questions? [Try asking the Reporting Services forum](https://go.microsoft.com/fwlink/?LinkId=620231)
