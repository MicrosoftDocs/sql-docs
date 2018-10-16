---
title: "Align the Data in a Chart in a Table or Matrix (Report Builder and SSRS) | Microsoft Docs"
ms.date: 03/03/2017
ms.prod: reporting-services
ms.prod_service: "reporting-services-sharepoint, reporting-services-native"
ms.technology: report-design


ms.topic: conceptual
ms.assetid: 75137575-d1bf-46d6-8527-5afc85eea5e1
author: maggiesMSFT
ms.author: maggies
---
# Align the Data in a Chart in a Table or Matrix (Report Builder and SSRS)
  Sparklines and data bars are small, simple charts that convey a lot of information with little extraneous detail. In a [!INCLUDE[ssRSnoversion_md](../../includes/ssrsnoversion-md.md)] paginated report, when you check this option the values in your sparklines and data bars will align across the different cells in the table or matrix, even if there are missing values in the data they are based on.  
  
 ![rs_SparklineAlignData](../../reporting-services/report-design/media/rs-sparklinealigndata.gif "rs_SparklineAlignData")  
  
 In this image, the column chart shows daily sales for each employee. Note that for days that an employee has no sales, the chart leaves a blank and aligns subsequent days horizontally. It also aligns the charts vertically by making the sizes of the different charts relative to each other. For more information, see [Sparklines and Data Bars &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/sparklines-and-data-bars-report-builder-and-ssrs.md).  
  
## Align the data in a sparkline or data bar  
  
1.  [Add a sparkline or data bar](../../reporting-services/report-design/add-sparklines-and-data-bars-report-builder-and-ssrs.md) to a table or matrix.  
  
2. Click in the sparkline or data bar, and then click **Horizontal Axis Properties** or **Vertical Axis Properties**.  
  
2.  On the **Axis Options** tab, check the **Align axes in** box, and then in the dropdown box, select the group on which to align the axis.  
  
3.  [!INCLUDE[clickOK](../../includes/clickok-md.md)]  
  
## See Also  
 [Charts &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/charts-report-builder-and-ssrs.md)   
 [Add Sparklines and Data Bars &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/add-sparklines-and-data-bars-report-builder-and-ssrs.md)  
  
  
