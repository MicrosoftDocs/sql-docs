---
title: "Customize the Report Viewer Web Part | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "reporting-services-native"
ms.topic: conceptual
helpviewer_keywords: 
  - "SharePoint integration [Reporting Services], viewing reports"
  - "Web Parts [Reporting Services]"
  - "Report Viewer Web Part [Reporting Services]"
ms.assetid: 086d6546-7299-41bc-bca9-083a15a53679
author: maggiesMSFT
ms.author: maggies
manager: kfile
---
# Customize the Report Viewer Web Part
  You can use the Report Viewer Web Part to view reports that run on a report server that is configured for SharePoint integration. Reports that you can display include report definition (.rdl) files and Report Builder reports. Reports open in the Report Viewer Web Part in a new page automatically, but you can also add a Report Viewer Web Part to an existing Web page or site if you want a particular report to always be visible on that page.  
  
 You can customize the Report Viewer Web Part in the following ways:  
  
-   Change the appearance of the Web Part by setting properties.  
  
-   Choose which interactive reporting features are available on the report toolbar.  
  
-   Specify which view areas are available. The Report Viewer Web Part has a report view area, a Parameters area, and a Credentials area.  
  
 You cannot extend the Report Viewer Web Part to support different file types, and you cannot replace the report toolbar with a custom toolbar or add new functionality to the existing toolbar. If you require customization of the standard features, you should create a custom Web Part.  
  
## Setting Web Part Properties  
 A Web Part has custom properties that are used to configure specific functionality. A Web Part also has common properties that are standard for every Web Part.  
  
### Change Default Properties  
 The Report Viewer Web Part has default properties that are ideally suited for opening reports on demand from a library or folder. By default, all available controls are displayed on the toolbar, and height and width are set to use all of the available space on the Web page. If you want to modify the default properties, you can customize the Web Part through **Site Settings**.  
  
1.  On the **Site Actions** menu, click **Site Settings**.  
  
2.  Under Galleries, click **Web parts**.  
  
3.  Click **ReportViewer.dwp**.  
  
4.  Open the tool pane and set the properties you want to use.  
  
### Customize an Embedded Report Viewer in a Web Page  
 You can set properties to make the Report Viewer fit within a Web page. The report viewer can use the same style and colors as the page that contains it. You can hide all or part of the toolbar, document map, and parameters area to maximize the report viewing area within the allocated space. The report always uses the styles that are defined for it when it was created; you cannot customize report appearance after it is published to a SharePoint library.  
  
 If you are embedding the Report Viewer Web Part in a Web page, you should set the **Report URL** property to a specific report. If you do not, the Report Viewer will display instructions for linking to a report. You cannot customize or remove the instructions.  
  
### Custom Properties of the Report Viewer Web Part  
 When setting custom properties, be aware that some properties are used only when the Report Viewer Web Part is embedded in a page. Examples include Title, Height, Width, Chrome type, and Zone. Other properties, such as Toolbar settings and Parameters settings, are used regardless of whether the Report Viewer appears within a page or opens a report in full-page mode.  
  
 The custom properties of the Report Viewer Web Part are listed below.  
  
|Property|Description|  
|--------------|-----------------|  
|Report|A fully-qualified path to a report that is on the current SharePoint site, or on a site within the same Web application or farm. For best results when setting additional properties, click Apply after you specify the report URL.|  
|Hyperlink Target|Standard HTML that specifies the target frame for displaying linked content within the current document. For reports that include hyperlinks to external Web sites, you can specify whether a target document replaces the existing report within the current window, or opens in a new browser window. Valid values include `_Top`, `_Blank`, and `_Self`. `_Top` uses the current window, `_Blank` loads the document in a new browser window, and `_Self` opens the document within the current frame. Although `_Parent` is a valid value for the Target attribute in HTML, do not use it for a Report Viewer Web Part that is embedded in a page.|  
|Auto-Generate Web Part Title|A generated title that includes the name of the Report Viewer Web Part plus the name of the report, separated by a dash. If the report does not have a title, the report file name is used. The title is visible when you add a Web Part to a page. If this check box is selected, the title will be generated each time the page is refreshed.|  
|Auto-Generate Web Part Title Detail Link|A generated hyperlink that appears above the Web Part. You can click the link to open the report in a new page, in full-page mode.|  
|Show report builder menu item|Shows or hides the **Actions** menu option to open Report Builder.|  
|Show subscription menu item|Shows or hides the **Actions** menu option to create a subscription for the report.|  
|Show print menu item|Shows or hides the **Actions** menu option to print the report.|  
|Show export menu item|Shows or hides the **Actions** menu option to export the report.|  
|Show refresh button|Shows or hides the refresh button on the toolbar.|  
|Show page navigation controls|Shows or hides the report navigation buttons on the toolbar. This option changes the visibility of all the navigation controls.|  
|Show back button|Shows or hides the back button on the toolbar.|  
|Show find controls|Shows or hides the find controls on the toolbar. The find controls allow a user to search for text in the rendered report. This option changes the visibility of all the find controls.|  
|Show zoom control|Shows or hides the zoom control on the toolbar.|  
|Show ATOM feed button|Shows or hides the ATOM feed button on the toolbar.<br /><br /> ![htmlviewer_datafeed](media/htmlviewer-datafeed.gif "htmlviewer_datafeed")|  
|ToolBar location|Determines the location of the toolbar within the report viewer. Valid values include `Top` and `Bottom`.|  
|Prompt area|Valid values include `Displayed`, `Collapsed`, and `Hidden`. `Displayed` displays the Parameters area for reports that include parameterized values and that require user input before the report will run. Use `Hidden` if all of the report parameters are specified and you do not want the parameters area to be visible to users.|  
|Parameters Area Width|You can choose the measurement and value. The default is 200 pixels. The only requirement for this property is that it is greater than zero.|  
|Document Map|A report navigation control that is defined in the report and used to provide one-click access to specific sections of a report. It is available in HTML reports. The document map is displayed in a collapsible area next to the report viewing area. Valid values include `Displayed`, `Collapsed`, and `Hidden`. If a document map is defined for a report, the area is expanded by default unless marked as hidden or collapsed in the Web Part properties. If the document map is collapsed, you can click the arrow to expand it.|  
|Document Map Area Width|You can choose the measurement and value. The default is 200 pixels. The only requirement for this property is that it is greater than zero.|  
|Load Parameters|Retrieve parameter properties for the report. Not all reports have parameters. If the report does not have parameters, no values will be returned. If you are setting properties for a report that you just uploaded, you might get an error indicating that the data source connection has been deleted. If this occurs, reset the connection and then finish setting parameter properties after the connection is specified. For more information about how to set the connection, see [Create and Manage Shared Data Sources &#40;Reporting Services in SharePoint Integrated Mode&#41;](../../2014/reporting-services/create-manage-shared-data-sources-reporting-services-sharepoint-integrated-mode.md).<br /><br /> For best results, click **Apply** before clicking Load Parameters.<br /><br /> After you load parameter properties, you can set them the same way you would in the parameter property pages of the report. For more information about how to set parameters, see [Set Parameters on a Published Report &#40;Reporting Services in SharePoint Integrated Mode&#41;](report-design/set-parameters-on-a-published-report-sharepoint-integrated-mode.md).|  
  
## Customizing the Toolbar  
 The toolbar appears beneath the title and extends across the top of the report. The toolbar provides an **Actions** menu, page navigation for paginated reports, refresh, and zoom. It includes a document map control for reports that have a document map. The **Actions** menu includes commands for exporting the report, searching for text or numbers within a report, printing the report, and, opening the report in Report Builder.  
  
 You cannot add new commands to the  **Actions** menu but you can customize it by changing the options that are visible to users. To change the visibility of toolbar buttons and controls, you change options in the **ToolBar Items Visibility** section of the Web part. You can also remove the **Print** command or specific export formats by making these features unavailable on the report server. Page navigation controls are available for reports that have page breaks; otherwise, the report is a single page of variable length. **Refresh** re-processes the report using the parameters that are current for the report. To display all the controls on one line, set the overall width of the Web Part to at least 400 pixels.  
  
## Customizing the Viewing Area  
 The view area is used to display reports. The report view area is shared with the Parameters and Credentials area, if they are used. If credentials are required, the Credentials area is displayed next to an empty report view area. The Credentials area closes after the user provides credentials and runs the report. To customize the text that prompts users to set credentials, modify the data source connection properties. For more information, see [Create and Manage Shared Data Sources &#40;Reporting Services in SharePoint Integrated Mode&#41;](../../2014/reporting-services/create-manage-shared-data-sources-reporting-services-sharepoint-integrated-mode.md).  
  
 The Parameters area provides fields for entering values before running the report. It is only used when a report definition includes parameters. When either the Parameters or Credentials areas are displayed, the report view is adjusted to use the remaining width of the Web Part. You can set properties on the Web Part to customize the width of Parameters. You can also define the labels that appear next to individual parameters on the page. For more information about how to modify parameter labels, see [Set Parameters on a Published Report &#40;Reporting Services in SharePoint Integrated Mode&#41;](report-design/set-parameters-on-a-published-report-sharepoint-integrated-mode.md).  
  
## See Also  
 [Report Viewer Web Part on a SharePoint Site](../../2014/reporting-services/report-viewer-web-part-on-a-sharepoint-site.md)   
 [Add the Report Viewer Web Part to a Web Page &#40;Reporting Services in SharePoint Integrated Mode&#41;](report-server-sharepoint/add-reporting-services-content-types-to-a-sharepoint-library.md)  
  
  
