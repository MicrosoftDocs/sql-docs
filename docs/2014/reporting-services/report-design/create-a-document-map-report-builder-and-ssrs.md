---
title: "Create a Document Map (Report Builder and SSRS) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "reporting-services-native"
ms.topic: conceptual
ms.assetid: c200a97b-67f2-499f-8374-3ed1ebe3f33c
author: maggiesMSFT
ms.author: maggies
manager: kfile
---
# Create a Document Map (Report Builder and SSRS)
  A document map provides a set of navigational links to report items in a rendered report. When you view a report that includes a document map, a separate side pane appears next to the report. A user can click links in the document map to jump to the report page that displays that item. Report sections and groups are arranged in a hierarchy of links. Clicking items in the document map refreshes the report and displays the area of the report that corresponds to the item in the document map.  
  
 To add links to the document map, you set the `DocumentMapLabel` property of the report item to text that you create or to an expression that evaluates to the text that you want display in the document map. You can also add the unique values for a table or matrix group to the document map. For example, for a group based on color, each unique color is a link to the report page that displays the group instance for that color.  
  
 You can also create a URL to a report that overrides the display of the document map, so that you can run the report without displaying the document map, and then click the **Show/Hide Document Map** button on the report viewer toolbar to toggle the display.  
  
> [!NOTE]  
>  [!INCLUDE[ssRBRDDup](../../includes/ssrbrddup-md.md)]  
  
##  <a name="DocMapRenderExtensions"></a> Document Maps and Rendering Extensions  
 The document map is intended for use in the HTML rendering extension-for example, in Preview and the Report Viewer. Other rendering extensions have different ways of articulating a document map:  
  
-   PDF renders a document map as the Bookmarks pane.  
  
-   Excel renders a document map as a named worksheet that includes a hierarchy of links. Report sections are rendered in separate worksheets that are included with the document map in the same workbook.  
  
-   Word includes a document map as the table of contents.  
  
-   Atom, TIFF, XML, and CSV ignore document maps.  
  
 For more information, see [Interactive Functionality for Different Report Rendering Extensions &#40;Report Builder and SSRS&#41;](../report-builder/interactive-functionality-different-report-rendering-extensions.md).  
  
##  <a name="AddRptItemToMap"></a>   
#### To add a report item to a document map  
  
1.  In Design view, select the report item such as a table, matrix, or gauge that you want to add to the document map. The report item properties appear in the Properties pane.  
  
    > [!NOTE]  
    >  To select a tablix data region, click in any cell to display the row and column handles, and then click the corner handle.  
  
2.  In the Properties pane, type the text that you want to appear in the document map in the `DocumentMapLabel` property, or enter an expression that evaluates to a label. For example, type **Sales Chart**.  
  
    > [!NOTE]  
    >  If you do not see the Properties pane, on the **View** tab, in the **Show/Hide** group, select **Properties**.  
  
3.  Repeat steps 1 and 2 for every report item that you want to appear in the document map.  
  
4.  Click **Run**. The report runs and the document map displays the labels you created. Click any link to jump to the report page with that item.  
  
 
  
##  <a name="AddUniqueValuesToMap"></a>   
#### To add unique group values to a document map  
  
1.  In Design view, select the table, matrix, or list that contains the group that you want to display in the document map. The Grouping pane displays the row and column groups.  
  
2.  In the Row Groups pane, right-click the group, and then click **Edit Group**. The **General** page of the **Tablix Group Properties** dialog box opens.  
  
3.  Click **Advanced**.  
  
4.  In the **Document map** list box, type or select an expression that matches the group expression.  
  
5.  [!INCLUDE[clickOK](../../includes/clickok-md.md)]  
  
6.  Repeat steps 1-4 for every group that you want to appear in the document map.  
  
7.  Click **Run**. The report runs and the document map displays the group values. Click any link to jump to the report page with that item.  
  
 
  
##  <a name="HideMapWhenViewRpt"></a>   
#### To hide the document map when you view a report  
  
1.  In Report Manager, browse to the report that has the document map.  
  
     For example, for the [!INCLUDE[ssSampleDBUserInputNonLocal](../../includes/sssampledbuserinputnonlocal-md.md)] sample reports, the following URL specifies the report named Product Catalog.  
  
    ```  
    http://localhost/Reports/Pages/Report.aspx?ItemPath=%2fAdventureWorks2012+Sample+Reports%2fProduct+Catalog  
    ```  
  
2.  Copy the report path on the server. In the example, the report path is `%2fAdventureWorks2012+Sample+Reports%2fProduct+Catalog`.  
  
3.  Create a new URL with the following three components:  
  
    -   The report viewer on the report server: `http://localhost/ReportServer/Pages/ReportViewer.aspx?`  
  
    -   The name of the report you copied in step 1, for example: `%2fAdventureWorks2012+Sample+Reports%2fProduct+Catalog`  
  
    -   The device information parameters that specify hiding the document map: `&rs%3aCommand=Render&rc%3aFormat=HTML4.0&rc%3aDocMap=False`  
  
     The following URL consists of these three components appended in the order they are listed.  
  
    ```  
    http://localhost/ReportServer/Pages/ReportViewer.aspx?  
    %2fAdventureWorks2012+Sample+Reports%2fProduct+Catalog  
    &rs%3aCommand=Render&rc%3aFormat=HTML4.0&rc%3aDocMap=False  
    ```  
  
     To use this URL, copy it and remove all line breaks.  
  
4.  Paste the URL in Report Manager, and then press ENTER. The report runs, and the document map is hidden.  
  
> [!NOTE]  
>  For more information about downloading sample reports, see [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)][Report Builder and Report Designer sample reports](https://go.microsoft.com/fwlink/?LinkId=198283).  
>   
>  For more information, see "URL Access" in the [Reporting Services documentation](https://go.microsoft.com/fwlink/?linkid=121312) in SQL Server Books Online.  
  
 
  
## See Also  
 [Finding and Viewing Reports in Report Manager &#40;Report Builder and SSRS&#41;](../report-builder/finding-and-viewing-reports-in-the-web-portal-report-builder-and-ssrs.md)  
  
  
