---
title: "Add, move, or delete a text box in a paginated report"
description: Add a text box to the paginated report body to display information such as titles, parameter choices, built-in fields, and dates in Report Builder.
author: maggiesMSFT
ms.author: maggies
ms.date: 03/01/2017
ms.service: reporting-services
ms.subservice: report-design
ms.topic: conceptual
ms.custom: updatefrequency5
---
# Add, move, or delete a text box in a paginated report (Report Builder)

[!INCLUDE[ssrs-appliesto](../../includes/ssrs-appliesto.md)] [!INCLUDE [ssrs-appliesto-ssrs-rb](../../includes/ssrs-appliesto-ssrs-rb.md)] [!INCLUDE [ssrs-appliesto-pbi-rb](../../includes/ssrs-appliesto-pbi-rb.md)] [!INCLUDE [ssrb-applies-to-ssdt-yes](../../includes/ssrb-applies-to-ssdt-yes.md)]

  Text boxes are the most commonly used report item in paginated reports. You can add a text box to the report body to display information such as titles, parameter choices, built-in fields, and dates.  
  
 Every cell in a table or matrix is really a text box. Almost all report data displayed in a report with tables and matrices is the result of the report processor evaluating the contents of each text box in the report. As such, you can format cells in the same way you would format other text boxes outside the data region.  
  
 To add a text box to a list data region, you must first add the text box, and then drag it into the list.  
  
> [!NOTE]  
>  When you select a text box, you're immediately editing the text in the text box. To select the text box itself, not the text in it, select Esc.  
  
## Add a text box  
  
1.  On the **Insert** tab in Design view, select **Text Box**.  
  
1.  On the design surface, select and then drag a box to the desired size of the text box.  
  
## Add a text box in a list  
  
1.  On the **Insert** tab in report design view, select **List**.  
  
1.  On the design surface, select and then drag a box to the desired size of the list.  
  
1.  On the **Insert** tab, select **Text Box**.  
  
1.  On the design surface, select and then drag a box to the desired size of the text box inside the list you added in step 1.   
  
1.  To confirm the text box is correctly nested inside the list, select the text box.  
  
    > [!NOTE]  
    >  If you select the text box and are in edit mode, select Esc to choose the text box.  
  
1.  In the **Properties** pane, verify that the **Parent** property is the rectangle that was automatically added to the list data region.  
  
    > [!NOTE]  
    >  If the **Properties** pane is not visible, check **Properties** on the **View** tab.  
  
## Move a text box  
  
1.  In report design view, select any empty space within the text box to choose the text box.  
  
    > [!NOTE]  
    >  If you select the text box and are in edit mode, select Esc to select the text box.  
  
1.  Select the text box handle and drag the text box to the new location.   
    Alternatively, use the arrow keys to move a selected text box horizontally or vertically. To move the text box in smaller increments on the design surface, select Ctrl plus the arrow keys.  
  
## Delete a text box  
  
1.  In report design view, right-click any empty space within the text box to select it, and then choose **Delete**. Alternatively, select any empty space within the text box, and then select Delete.  
  
    > [!NOTE]  
    >  If you select the text box and are in edit mode, select Esc to choose the text box.  
  
## Related content  
 [Text boxes &#40;Report Builder&#41;](../../reporting-services/report-design/text-boxes-report-builder-and-ssrs.md)   
 [Expressions &#40;Report Builder&#41;](../../reporting-services/report-design/expressions-report-builder-and-ssrs.md)   
 [Keyboard shortcuts &#40;Report Builder&#41;](../../reporting-services/report-builder/keyboard-shortcuts-report-builder.md)  
  
  
