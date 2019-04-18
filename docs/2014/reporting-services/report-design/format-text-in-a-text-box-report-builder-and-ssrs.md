---
title: "Format Text in a Text Box (Report Builder and SSRS) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "reporting-services-native"
ms.topic: conceptual
ms.assetid: df0794b5-96b0-4034-bd17-1be7f31e29db
author: maggiesMSFT
ms.author: maggies
manager: kfile
---
# Format Text in a Text Box (Report Builder and SSRS)
  You can format any part of the text within a text box independently, and mix placeholder text and static text in one text box. This ability to mix formats and add placeholder text enables you to create mail merges or templates for text in your report. Any expression can be defined and formatted separately using a placeholder.  
  
> [!NOTE]  
>  [!INCLUDE[ssRBRDDup](../../includes/ssrbrddup-md.md)]  
  
### To combine multiple formats in a text box  
  
1.  On the **Insert** tab, click **Text Box**. Click the design surface, and then drag to create a box that is the size you want.  
  
2.  Inside the text box, select the text you want to format.  
  
3.  Right-click the selected text, and click **Text Properties**.  
  
4.  Set formatting options. For example, on the **General** tab:  
  
    -   **Tooltip** Type text or an expression that evaluates to a ToolTip. The ToolTip appears when the user pauses the pointer over the item in a report  
  
    -   **Markup type** Select an option to indicate how the selected text will be rendered:  
  
         **Plain Text** Display the selected text as simple text. HTML will be treated as literal text.  
  
         **HTML**  Display the selected text as HTML. If the expression value of the placeholder contains valid HTML tags, these tags will be rendered as HTML. For more information, see [Importing HTML into a Report &#40;Report Builder and SSRS&#41;](importing-html-into-a-report-report-builder-and-ssrs.md).  
  
5.  Click **OK**.  
  
6.  Repeat steps 2 through 5 for the remaining text you want to format.  
  
### To format text and placeholders differently in the same text box  
  
1.  On the **Insert** tab, click **List**. Click the design surface, and then drag to create a box that is the size you want. The **Dataset Properties** dialog box opens. You can use a shared dataset or a dataset embedded in your report. For more information, click [Dataset Properties Dialog Box, Query &#40;Report Builder&#41;](../report-data/dataset-properties-dialog-box-query-report-builder.md) or [Dataset Properties Dialog Box, Query](../dataset-properties-dialog-box-query.md).  
  
2.  On the **Insert** tab, click **Text Box**. Click in the list, and then drag to create a box that is the size you want.  
  
3.  Type a label in the text box - for example, **My Field**:.  
  
4.  Drag a field from your dataset into the text box. A placeholder is created for your field.  
  
5.  For basic formatting, select the placeholder text and then click one of the formatting options in the **Font** group on the **Home** tab. For example, click the **Bold** button.  
  
     For more formatting options, right-click the placeholder text, and then click **Placeholder Properties**.  
  
6.  Click **OK**. In report design view, the text box should contain "**My Field**: [*FieldName*]", where *FieldName* is the name of your field.  
  
7.  Click **Run**.  
  
 The list repeats one time for every value in the field, and the *FieldName* placeholder is replaced each time by the value of that field in the dataset.  
  
## See Also  
 [Text Boxes &#40;Report Builder and SSRS&#41;](text-boxes-report-builder-and-ssrs.md)   
 [Formatting Text and Placeholders &#40;Report Builder and SSRS&#41;](formatting-text-and-placeholders-report-builder-and-ssrs.md)   
 [Expression Uses in Reports &#40;Report Builder and SSRS&#41;](expression-uses-in-reports-report-builder-and-ssrs.md)   
 [Expression Examples &#40;Report Builder and SSRS&#41;](expression-examples-report-builder-and-ssrs.md)   
 [Add HTML into a Report &#40;Report Builder and SSRS&#41;](add-html-into-a-report-report-builder-and-ssrs.md)   
 [Lists &#40;Report Builder and SSRS&#41;](tables-matrices-and-lists-report-builder-and-ssrs.md)   
 [Formatting Numbers and Dates &#40;Report Builder and SSRS&#41;](formatting-numbers-and-dates-report-builder-and-ssrs.md)   
 [Placeholder Properties Dialog Box, General &#40;Report Builder and SSRS&#41;](../placeholder-properties-dialog-box-general-report-builder-and-ssrs.md)  
  
  
