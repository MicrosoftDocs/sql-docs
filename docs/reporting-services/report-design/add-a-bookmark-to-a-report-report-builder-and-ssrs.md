---
title: "Add a Bookmark to a Report (Report Builder and SSRS) | Microsoft Docs"
ms.date: 03/01/2017
ms.prod: reporting-services
ms.prod_service: "reporting-services-sharepoint, reporting-services-native"
ms.technology: report-design


ms.topic: conceptual
ms.assetid: f130562e-5673-40e3-8e01-de7227a21d41
author: maggiesMSFT
ms.author: maggies
---
# Add a Bookmark to a Report (Report Builder and SSRS)
  Add bookmarks and bookmark links to a report when you want to provide a customized table of contents or to provide customized internal navigation links in the report. Typically, you add bookmarks to locations in the report to which you want to direct users, such as to each table or chart or to the unique group values displayed in a table or matrix. You can create your own strings to use as bookmarks, or, for groups, you can set the bookmark to the group expression.  
  
 After you create bookmarks, you can add report items that the user can click to go to each bookmark. These items are typically text boxes or images.  
  
 For example, if your report displays a table grouped by color, you would add a bookmark based on the group expression to the group header. Then you would add a table with a single text box at the beginning of the report that displayed the color values, and set the bookmark link on that text box. When you click the color, the report jumps to the page that displays the group header row for that color.  
  
 You can add a bookmark to any report item and add a bookmark link to any item that has an **Action** property, for example, a text box, an image, or a calculated series in a chart. For more information, see the [Action Properties Dialog Box &#40;Report Builder and SSRS&#41;](https://msdn.microsoft.com/library/2c5d915b-4f97-42cf-b8f1-49ca3ff3d0f9).  
  
> [!NOTE]  
>  [!INCLUDE[ssRBRDDup](../../includes/ssrbrddup-md.md)]  
  
### To add a bookmark  
  
1.  In report design view, select the text box, image, chart, or other report item to which you want to add a bookmark. The properties for the selected item appear in the Properties pane.  
  
2.  In the text box next to **Bookmark**, type a string that is the label for this bookmark. For example, you could type **BikePhoto** as the bookmark for an image in your report. Alternatively, click the Expression (**fx**) button to open the **Expression** dialog box to specify an expression that evaluates to a label. For a group, the expression you type should be the group expression.  
  
    > [!NOTE]  
    >  The bookmark can be any string, but it must be unique in the report. If the bookmark is not unique, a link to the bookmark finds the first matching bookmark.  
  
### To add a bookmark link  
  
1.  In Design view, right-click the text box, image, chart, to which you want to add a link and then click **Properties**.  
  
2.  In The **Properties** dialog box for that report item, click **Action**.  
  
3.  Select **Go to bookmark**. An additional section appears in the dialog box for this option.  
  
4.  In the **Select bookmark** box, type or select a bookmark or an expression that evaluates to a bookmark. Using the previous example, type **BikePhoto** to create a link to the image in your report.  
  
5.  [!INCLUDE[clickOK](../../includes/clickok-md.md)]  
  
6.  (Optional) The text is not automatically formatted like a link. For text, it is helpful to change the color and effect of the text to indicate that the text is a link. For example, change the color to blue and the effect to underline in the **Font** section in the Home tab of the Ribbon.  
  
7.  To test the link, click **Run** to preview the report, and then click the report item that you set this link on..  
  
## See Also  
 [Interactive Sort, Document Maps, and Links &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/interactive-sort-document-maps-and-links-report-builder-and-ssrs.md)   
 [Expressions &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/expressions-report-builder-and-ssrs.md)   
 [Filter, Group, and Sort Data &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/filter-group-and-sort-data-report-builder-and-ssrs.md)  
  
  
