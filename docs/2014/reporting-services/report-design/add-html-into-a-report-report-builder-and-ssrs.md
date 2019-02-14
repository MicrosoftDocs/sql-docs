---
title: "Add HTML into a Report (Report Builder and SSRS) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "reporting-services-native"
ms.topic: conceptual
ms.assetid: 30bd631a-f774-48e7-a13a-b6c2eb54d9bb
author: maggiesMSFT
ms.author: maggies
manager: kfile
---
# Add HTML into a Report (Report Builder and SSRS)
  Using a placeholder, you can import HTML from a field in your dataset for use in the report. By default, a placeholder represents plain text, so you will need to change the placeholder mark-up type to HTML. For more information, see [Importing HTML into a Report &#40;Report Builder and SSRS&#41;](importing-html-into-a-report-report-builder-and-ssrs.md).  
  
> [!NOTE]  
>  [!INCLUDE[ssRBRDDup](../../includes/ssrbrddup-md.md)]  
  
### To add HTML from a field in your dataset into a text box  
  
1.  On the **Insert** tab, click **List**. Click the design surface, and then drag to create a box that is the size you want.  
  
     The **Dataset Properties** dialog box opens. You can use a shared dataset or a dataset embedded in your report. For more information, click [Dataset Properties Dialog Box, Query &#40;Report Builder&#41;](../report-data/dataset-properties-dialog-box-query-report-builder.md) or [Dataset Properties Dialog Box, Query](../dataset-properties-dialog-box-query.md).  
  
2.  On the **Insert** tab, click **Text Box**. Click in the list, and then drag to create a box that is the size you want.  
  
3.  Drag an HTML field from your dataset into the text box. A placeholder is created for your field.  
  
4.  Right-click the placeholder, and then click **Placeholder Properties**.  
  
5.  On the **General** tab, verify that the **Value** box contains an expression that evaluates to the field you dropped in step 3.  
  
6.  Click **HTML - Interpret HTML tags as styles**. This causes the field to be evaluated as HTML.  
  
7.  [!INCLUDE[clickOK](../../includes/clickok-md.md)]  
  
## See Also  
 [Formatting Numbers and Dates &#40;Report Builder and SSRS&#41;](formatting-numbers-and-dates-report-builder-and-ssrs.md)   
 [Formatting Lines, Colors, and Images &#40;Report Builder and SSRS&#41;](images-report-builder-and-ssrs.md)   
 [Placeholder Properties Dialog Box, General &#40;Report Builder and SSRS&#41;](../placeholder-properties-dialog-box-general-report-builder-and-ssrs.md)  
  
  
