---
title: "Add HTML into a paginated report"
description: Find out how to import HTML using a placeholder from a field in your dataset to use in your paginated report in Report Builder.
author: maggiesMSFT
ms.author: maggies
ms.date: 03/01/2017
ms.service: reporting-services
ms.subservice: report-design
ms.topic: conceptual
ms.custom: updatefrequency5
---
# Add HTML into a paginated report (Report Builder)

[!INCLUDE[ssrs-appliesto](../../includes/ssrs-appliesto.md)] [!INCLUDE [ssrs-appliesto-ssrs-rb](../../includes/ssrs-appliesto-ssrs-rb.md)] [!INCLUDE [ssrs-appliesto-pbi-rb](../../includes/ssrs-appliesto-pbi-rb.md)] [!INCLUDE [ssrb-applies-to-ssdt-yes](../../includes/ssrb-applies-to-ssdt-yes.md)]

  Using a placeholder, you can import HTML from a field in your dataset for use in the paginated report. By default, a placeholder represents plain text, so you need to change the placeholder mark-up type to HTML. For more information, see [Importing HTML into a report &#40;Report Builder&#41;](../../reporting-services/report-design/importing-html-into-a-report-report-builder-and-ssrs.md).  
  
> [!NOTE]  
>  [!INCLUDE[ssRBRDDup](../../includes/ssrbrddup-md.md)]  
  
### Add HTML from a field in your dataset into a text box  
  
1.  On the **Insert** tab, select **List**. Choose the design surface, and then drag to create a box that is the size you want.  
  
     The **Dataset Properties** dialog opens. You can use a shared dataset or a dataset embedded in your report. For more information, select [Dataset Properties dialog, query &#40;Report Builder&#41;](../../reporting-services/report-data/dataset-properties-dialog-box-query-report-builder.md) or [Dataset Properties dialog, query](/previous-versions/sql/).  
  
1.  On the **Insert** tab, select **Text Box**. Drag the box from the list to create a box that is the size you want.  
  
1.  Drag an HTML field from your dataset into the text box. A placeholder is created for your field.  
  
1.  Right-click the placeholder, and then select **Placeholder Properties**.  
  
1.  On the **General** tab, verify that the **Value** box contains an expression that evaluates to the field you dropped in step 3.  
  
1.  Select **HTML - Interpret HTML tags as styles**. This causes the field to be evaluated as HTML.  
  
1.  Select **OK**.
  
## Related content
 [Format numbers and dates &#40;Report Builder&#41;](../../reporting-services/report-design/formatting-numbers-and-dates-report-builder-and-ssrs.md)   
 [Format lines, colors, and images &#40;Report Builder&#41;](../../reporting-services/report-design/formatting-lines-colors-and-images-report-builder-and-ssrs.md)   
 [Placeholder Properties dialog, general &#40;Report Builder&#41;](./text-boxes-report-builder-and-ssrs.md)  
  
