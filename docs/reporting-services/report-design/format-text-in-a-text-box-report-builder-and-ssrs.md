---
title: "Format text in a text box in paginated reports"
description: Discover how to format text in a text box in paginated reports, and how to mix placeholder text and static text to create mail merges or templates for text in Report Builder.
ms.date: 09/25/2024
ms.service: reporting-services
ms.subservice: report-design
ms.topic: how-to
ms.custom:
  - updatefrequency5
---
# Format text in a text box in paginated reports (Report Builder)

[!INCLUDE[ssrs-appliesto](../../includes/ssrs-appliesto.md)] [!INCLUDE [ssrs-appliesto-ssrs-rb](../../includes/ssrs-appliesto-ssrs-rb.md)] [!INCLUDE [ssrs-appliesto-pbi-rb](../../includes/ssrs-appliesto-pbi-rb.md)] [!INCLUDE [ssrb-applies-to-ssdt-yes](../../includes/ssrb-applies-to-ssdt-yes.md)]

In paginated reports, you can format any part of the text in a text box independently, and mix placeholder text and static text in one text box. This ability to mix formats and add placeholder text enables you to create mail merges or templates for text in your paginated report. Any expression can be defined and formatted separately using a placeholder.  
  
> [!NOTE]  
> [!INCLUDE[ssRBRDDup](../../includes/ssrbrddup-md.md)]  
  
## Combine multiple formats in a text box  
  
1. On the **Insert** tab, select **Text Box**. Select the design surface, and then drag to create a box that is the size you want.  
  
1. Inside the text box, select the text you want to format.  
  
1. Right-click the selected text, and select **Text Properties**.  
  
1. Set formatting options. For example, on the **General** tab:  
  
    -   **Tooltip** Type text or an expression that evaluates to a ToolTip. The ToolTip appears when the user pauses the pointer over the item in a report  
  
    -   **Markup type** Select an option to indicate how the selected text will be rendered:  
  
         **Plain Text** Display the selected text as simple text. HTML will be treated as literal text.  
  
         **HTML**  Display the selected text as HTML. If the expression value of the placeholder contains valid HTML tags, these tags will be rendered as HTML. For more information, see [Importing HTML into a Report &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/importing-html-into-a-report-report-builder-and-ssrs.md).  
1. Select **OK**.  
  
1. Repeat steps 2 through 5 for the remaining text you want to format.  
  
## Format text and placeholders differently in the same text box  
  
1. On the **Insert** tab, select **List**. Select the design surface, and then drag to create a box that is the size you want. The **Dataset Properties** dialog box opens. You can use a shared dataset or a dataset embedded in your report. For more information, select [Dataset Properties Dialog Box, Query &#40;Report Builder&#41;](../../reporting-services/report-data/dataset-properties-dialog-box-query-report-builder.md) or [Dataset Properties Dialog Box, Query](/previous-versions/sql/).  
  
1. On the **Insert** tab, select **Text Box**. Select in the list, and then drag to create a box that is the size you want.  
  
1. Type a label in the text box - for example, **My Field**.  
  
1. Drag a field from your dataset into the text box. A placeholder is created for your field.  
  
1. For basic formatting, select the placeholder text and then select one of the formatting options in the **Font** group on the **Home** tab. For example, select the **Bold** button.  
  
     For more formatting options, right-click the placeholder text, and then select **Placeholder Properties**.  
  
1. Select **OK**. In report design view, the text box should contain "**My Field**: [*FieldName*]", where *FieldName* is the name of your field.  
  
1. Select **Run**.  
  
 The list repeats one time for every value in the field, and the *FieldName* placeholder is replaced each time by the value of that field in the dataset.  
 
## Add Heading elements to a text box 
  
1. On the **Insert** tab, select **Text Box**. Select the design surface, and then drag to create a box that is the size you want.  
  
1. Inside the text box, select the text you want to format.  
  
1. Right-click the selected text, and select **Text Properties**. 
 
1. Set heading elements. For example, on the **Accessibility** tab:

    - **Accessibility options** Overwrite the structural type to give the Textbox a different semantic meaning output format, like HTML and Accessible PDF.
    - **Overwrite structure type** allows you to choose HTML heading elements H1-H6.

1. Make a selection. 

1. Select **OK**.

## Related content

- [Text boxes in paginated reports (Report Builder)](text-boxes-report-builder-and-ssrs.md)
- [Format text and placeholders in paginated reports (Report Builder)](formatting-text-and-placeholders-report-builder-and-ssrs.md)
- [Expression uses in paginated reports (Report Builder)](expression-uses-in-reports-report-builder-and-ssrs.md)
- [Expression examples in Report Builder paginated reports](expression-examples-report-builder-and-ssrs.md)
- [Add HTML into a paginated report (Report Builder)](add-html-into-a-report-report-builder-and-ssrs.md)
- [Tables, matrices, and lists in Report Builder paginated reports](tables-matrices-and-lists-report-builder-and-ssrs.md)
- [Format numbers and dates in Report Builder paginated reports](formatting-numbers-and-dates-report-builder-and-ssrs.md)
