---
title: "Add, Move, or Delete a Text Box (Report Builder and SSRS) | Microsoft Docs"
ms.date: 03/01/2017
ms.prod: reporting-services
ms.prod_service: "reporting-services-sharepoint, reporting-services-native"
ms.technology: report-design


ms.topic: conceptual
ms.assetid: f042cf81-d933-4ac7-9287-c074a46bde98
author: markingmyname
ms.author: maghan
---
# Add, Move, or Delete a Text Box (Report Builder and SSRS)
  Text boxes are the most commonly used report item in [!INCLUDE[ssRSnoversion_md](../../includes/ssrsnoversion-md.md)] paginated reports. You can add a text box to the report body to display information such as titles, parameter choices, built-in fields, and dates.  
  
 Every cell in a table or matrix is really a text box. Almost all report data displayed in a report with tables and matrices is the result of the report processor evaluating the contents of each text box in the report. As such, you can format cells in the same way you would format other text boxes outside the data region.  
  
 To add a text box to a list data region, you must first add the text box and then drag it into the list.  
  
> [!NOTE]  
>  When you click a text box, you're immediately editing the text in the text box. To select the text box itself, not the text in it, press ESC.  
  
## To add a text box  
  
1.  On the **Insert** tab in Design view, click **Text Box**.  
  
2.  On the design surface, click and then drag a box to the desired size of the text box.  
  
## To add a text box in a list  
  
1.  On the **Insert** tab in report design view, click **List**.  
  
2.  On the design surface, click and then drag a box to the desired size of the list.  
  
3.  On the **Insert** tab, click **Text Box**.  
  
4.  On the design surface, click and then drag a box to the desired size of the text box inside the list you added in step 1.   
  
5.  To confirm the text box is correctly nested inside the list, select the text box.  
  
    > [!NOTE]  
    >  If you click in the text box and are in edit mode, press ESC to select the text box.  
  
6.  In the Properties pane, verify that the **Parent** property is the rectangle that was automatically added to the list data region.  
  
    > [!NOTE]  
    >  If the Properties pane is not visible, check **Properties** on the **View** tab.  
  
## To move a text box  
  
1.  In report design view, click any empty space within the text box to select the text box.  
  
    > [!NOTE]  
    >  If you click in the text box and are in edit mode, press ESC to select the text box.  
  
2.  Click the text box handle and drag the text box to the new location.   
    Alternatively, use the arrow keys to move a selected text box horizontally or vertically. To move the text box in smaller increments on the design surface, hold down CTRL plus the arrow keys.  
  
## To delete a text box  
  
1.  In report design view, right-click any empty space within the text box to select it, and then click **Delete**. Alternatively, click any empty space within the text box, and then press DELETE.  
  
    > [!NOTE]  
    >  If you click in the text box and are in edit mode, press ESC to select the text box.  
  
## See Also  
 [Text Boxes &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/text-boxes-report-builder-and-ssrs.md)   
 [Expressions &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/expressions-report-builder-and-ssrs.md)   
 [Keyboard Shortcuts &#40;Report Builder&#41;](../../reporting-services/report-builder/keyboard-shortcuts-report-builder.md)  
  
  
