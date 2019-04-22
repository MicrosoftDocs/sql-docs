---
title: "Action Properties Dialog Box (Report Builder and SSRS) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "reporting-services-native"
ms.topic: conceptual
f1_keywords: 
  - "sql12.rtp.rptdesigner.shared.action.f1"
  - "10413"
  - "10504"
  - "sql12.rtp.rptdesigner.calculatedseriesproperties.action.f1"
  - "sql12.rtp.rptdesigner.pictureproperties.action.f1"
  - "sql12.rtp.rptdesigner.actionproperties.f1"
  - "sql12.rtp.rptdesigner.charttitleproperties.action.f1"
  - "10133"
  - "10052"
  - "10147"
  - "sql12.rtp.rptdesigner.chartproperties.action.f1"
  - "10122"
  - "sql12.rtp.rptdesigner.serieslabelproperties.action.f1"
  - "sql12.rtp.rptdesigner.textboxproperties.action.f1"
  - "10162"
  - "10168"
  - "sql12.rtp.rptdesigner.textproperties.action.f1"
  - "sql12.rtp.rptdesigner.placeholderproperties.action.f1"
  - "10250"
  - "10129"
  - "10244"
  - "sql12.rtp.rptdesigner.seriesproperties.action.f1"
ms.assetid: 2c5d915b-4f97-42cf-b8f1-49ca3ff3d0f9
author: maggiesMSFT
ms.author: maggies
manager: kfile
---
# Action Properties Dialog Box (Report Builder and SSRS)
  The **Action** dialog box can be used to enable hyperlink options for a chart, gauge, and map elements that support links. Define an action so that a user can click on the report and link to a URL, to a different report on the same report server or on a SharePoint site that is integrated with a report server, or to a different location in the same report.  
  
## Options  
 **Enable as an action**  
 Select an option to indicate the action to perform when the user clicks the item.  
  
 **None**  
 Choose this option to indicate that the item has no action.  
  
 **Go to report**  
 Choose this option to create a link to a drillthrough report that is located on a report server. The following additional options appear when you select **Go to report**.  
  
 **Specify a report**  
 Type or select the name of the report that you want to use as a drillthrough report. Drillthrough reports must be on the same report server as this report.  
  
 For a report published to a report server configured for native mode, use a full or relative path without the file name extension. If the report is in the same folder as the current report, use the name of the report only. If the report is in a different folder on the same report server, use a relative path or a full path. A relative path starts from the current folder and moves up the folder hierarchy, for example, ../Folder2/Report1. A full path starts from /, the Home folder. For example, /Reports/Report1.  
  
 For a report published to a report server configured in SharePoint integrated mode, use a fully qualified URL including the file name extension (.rdl). For example, http://*\<SharePointservername>/\<site>*/Documents/Report1.rdl. Relative paths are not supported.  
  
 For more information, see [Specifying Paths to External Items &#40;Report Builder and SSRS&#41;](report-design/specifying-paths-to-external-items-report-builder-and-ssrs.md) in the [Report Builder documentation](https://go.microsoft.com/fwlink/?LinkId=154494) on msdn.microsoft.com.  
  
 **Use these parameters to run the report**  
 Add a list of parameters to pass to the drillthrough report. The parameter names must match the parameters defined for the target report. Use the **Add** and **Delete** buttons to add and remove parameters and use the up and down arrows to order the list of parameters.  
  
 **Add**  
 Add a new parameter to pass to the drillthrough report.  
  
 **Delete**  
 Delete a parameter for the drillthrough report.  
  
 **Up arrow**  
 Move the parameter up in the list.  
  
 **Down arrow**  
 Move the parameter down in the list.  
  
 **Name**  
 Type text that is the name of a parameter defined in the drillthrough report.  
  
 **Value**  
 Type or select a value to pass for the named parameter in the drillthrough report. Click the **Expression** (*fx*) button to edit the expression.  
  
 **Omit**  
 Select to prevent the parameter from running. By default, this check box is cleared and not active. To select the check box, click the **Expression** (*fx*) button and either type **True** or create an expression. The check box is selected when you click **OK** in the **Expression** dialog box.  
  
 **Go to bookmark**  
 Choose this option to define a link to a bookmark in the current report. The following additional option appears when you select **Go to bookmark**.  
  
 **Select bookmark**  
 Type or select the bookmark ID for the report to jump to when the user clicks the link. Click the Expression (**fx**) button to change the expression. The bookmark ID can be either a static ID or an expression that evaluates to a bookmark ID. The expression can include a field that contains a bookmark ID.  
  
 To link to a bookmark, you must first set the Bookmark property of a report item. To set the Bookmark property, select a report item, and in the Properties pane, type a value or expression for the bookmark ID; for example, SalesChart or 5TopSales.  
  
 **Go to URL**  
 Choose this option to define a link to a Web page. Type or select the URL of a Web page or an expression that evaluates to the URL of a Web page. Click the **Expression** (*fx*) button to change the expression. This expression can include a field that contains a URL. The following additional option appears when you select **Go to URL**.  
  
 **Select URL**  
 Type or enter the URL of the item. For an item published to a report server configured for native mode, use a full or relative path. For example, http://*\<servername>*/images/image1.jpg. For an item published to a report server configured in SharePoint integrated mode, use a fully qualified URL (for example, http://*\<SharePointservername>/\<site>*/Documents/images/image1.jpg).  
  
## See Also  
 [Charts &#40;Report Builder and SSRS&#41;](report-design/charts-report-builder-and-ssrs.md)   
 [Report Builder Help for Dialog Boxes, Panes, and Wizards](../../2014/reporting-services/report-builder-help-for-dialog-boxes-panes-and-wizards.md)   
 [Report Parameters &#40;Report Builder and Report Designer&#41;](report-design/report-parameters-report-builder-and-report-designer.md)   
 [Add a Subreport and Parameters &#40;Report Builder and SSRS&#41;](report-design/add-a-subreport-and-parameters-report-builder-and-ssrs.md)   
 [Interactive Sort, Document Maps, and Links &#40;Report Builder and SSRS&#41;](report-design/interactive-sort-document-maps-and-links-report-builder-and-ssrs.md)  
  
  
