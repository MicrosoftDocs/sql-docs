---
title: "Add HTML into a Report (Report Builder and SSRS) | Microsoft Docs"
ms.date: 03/01/2017
ms.prod: reporting-services
ms.prod_service: "reporting-services-sharepoint, reporting-services-native"
ms.technology: report-design


ms.topic: conceptual
ms.assetid: 30bd631a-f774-48e7-a13a-b6c2eb54d9bb
author: markingmyname
ms.author: maghan
---
# Add HTML into a Report (Report Builder and SSRS)
  Using a placeholder, you can import HTML from a field in your dataset for use in the report. By default, a placeholder represents plain text, so you will need to change the placeholder mark-up type to HTML. For more information, see [Importing HTML into a Report &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/importing-html-into-a-report-report-builder-and-ssrs.md).  
  
> [!NOTE]  
>  [!INCLUDE[ssRBRDDup](../../includes/ssrbrddup-md.md)]  
  
### To add HTML from a field in your dataset into a text box  
  
1.  On the **Insert** tab, click **List**. Click the design surface, and then drag to create a box that is the size you want.  
  
     The **Dataset Properties** dialog box opens. You can use a shared dataset or a dataset embedded in your report. For more information, click [Dataset Properties Dialog Box, Query &#40;Report Builder&#41;](../../reporting-services/report-data/dataset-properties-dialog-box-query-report-builder.md) or [Dataset Properties Dialog Box, Query](https://msdn.microsoft.com/library/1fa34a4b-7de0-4e92-99fa-bc28a206773f).  
  
2.  On the **Insert** tab, click **Text Box**. Click in the list, and then drag to create a box that is the size you want.  
  
3.  Drag an HTML field from your dataset into the text box. A placeholder is created for your field.  
  
4.  Right-click the placeholder, and then click **Placeholder Properties**.  
  
5.  On the **General** tab, verify that the **Value** box contains an expression that evaluates to the field you dropped in step 3.  
  
6.  Click **HTML - Interpret HTML tags as styles**. This causes the field to be evaluated as HTML.  
  
7.  [!INCLUDE[clickOK](../../includes/clickok-md.md)]  
  
## See Also  
 [Formatting Numbers and Dates &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/formatting-numbers-and-dates-report-builder-and-ssrs.md)   
 [Formatting Lines, Colors, and Images &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/formatting-lines-colors-and-images-report-builder-and-ssrs.md)   
 [Placeholder Properties Dialog Box, General &#40;Report Builder and SSRS&#41;](https://msdn.microsoft.com/library/7a867736-a3b0-4b5a-b3e5-fe7c8d7618a8)  
  
  
