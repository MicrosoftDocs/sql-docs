---
title: "Add a bookmark to a paginated report"
description: Find out how to add bookmarks to a paginated report to provide a customized table of contents or to provide customized internal navigation links in Report Builder.
author: maggiesMSFT
ms.author: maggies
ms.date: 03/01/2017
ms.service: reporting-services
ms.subservice: report-design
ms.topic: conceptual
ms.custom: updatefrequency5
---
# Add a bookmark to a paginated report (Report Builder)

[!INCLUDE[ssrs-appliesto](../../includes/ssrs-appliesto.md)] [!INCLUDE [ssrs-appliesto-ssrs-rb](../../includes/ssrs-appliesto-ssrs-rb.md)] [!INCLUDE [ssrs-appliesto-pbi-rb](../../includes/ssrs-appliesto-pbi-rb.md)] [!INCLUDE [ssrb-applies-to-ssdt-yes](../../includes/ssrb-applies-to-ssdt-yes.md)]

  Add bookmarks and bookmark links to a paginated report when you want to provide a customized table of contents or to provide customized internal navigation links in the report. Typically, you add bookmarks to locations in the report to which you want to direct users. For example, you can add bookmarks to each table or chart, or to the unique group values displayed in a table or matrix. You can create your own strings to use as bookmarks, or, for groups, you can set the bookmark to the group expression.  
  
 After you create bookmarks, you can add report items that the user can choose to go to each bookmark. These items are typically text boxes or images.  
  
 For example, if your report displays a table grouped by color, you would add a bookmark based on the group expression to the group header. Then you would add a table with a single text box at the beginning of the report that displayed the color values, and set the bookmark link on that text box. When you select the color, the report jumps to the page that displays the group header row for that color.  
  
 You can add a bookmark to any report item and add a bookmark link to any item that has an **Action** property, for example, a text box, an image, or a calculated series in a chart. For more information, see the [Action Properties dialog&#40;Report Builder&#41;](./add-a-hyperlink-to-a-url-report-builder-and-ssrs.md).  
  
> [!NOTE]  
>  [!INCLUDE[ssRBRDDup](../../includes/ssrbrddup-md.md)]  
  
### Add a bookmark  
  
1.  In report design view, select the text box, image, chart, or other report item to which you want to add a bookmark. The properties for the selected item appear in the **Properties** pane.  
  
1.  In the text box next to **Bookmark**, enter a string that is the label for this bookmark. For example, you could type **BikePhoto** as the bookmark for an image in your report. Alternatively, select the Expression (**fx**) button to open the **Expression** dialog to specify an expression that evaluates to a label. For a group, the expression you type should be the group expression.  
  
    > [!NOTE]  
    >  The bookmark can be any string, but it must be unique in the report. If the bookmark is not unique, a link to the bookmark finds the first matching bookmark.  
  
### Add a bookmark link  
  
1.  In Design view, right-click the text box, image, chart, to which you want to add a link and then select **Properties**.  
  
1.  In The **Properties** dialog for that report item, select **Action**.  
  
1.  Select **Go to bookmark**. Another section appears in the dialog for this option.  
  
1.  In the **Select bookmark** box, enter or select a bookmark or an expression that evaluates to a bookmark. Using the previous example, enter **BikePhoto** to create a link to the image in your report.  
  
1.  Select **OK**.
  
1.  (Optional) The text isn't automatically formatted like a link. For text, it's helpful to change the color and effect of the text to indicate that the text is a link. For example, change the color to blue and the effect to underline in the **Font** section in the **Home** tab of the Ribbon.  
  
1.  To test the link, select **Run** to preview the report, and then select the report item that you set this link on. 
  
## Related content
 [Interactive sort, document maps, and links &#40;Report Builder&#41;](../../reporting-services/report-design/interactive-sort-document-maps-and-links-report-builder-and-ssrs.md)   
 [Expressions &#40;Report Builder&#41;](../../reporting-services/report-design/expressions-report-builder-and-ssrs.md)   
 [Filter, group, and sort data &#40;Report Builder&#41;](../../reporting-services/report-design/filter-group-and-sort-data-report-builder-and-ssrs.md)  
  
