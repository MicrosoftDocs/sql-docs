---
title: "Add a Gauge to a Report (Report Builder and SSRS) | Microsoft Docs"
ms.date: 03/01/2017
ms.prod: reporting-services
ms.prod_service: "reporting-services-sharepoint, reporting-services-native"
ms.technology: report-design


ms.topic: conceptual
ms.assetid: 45da4fef-2b02-40e1-977c-f8f80d87155e
author: markingmyname
ms.author: maghan
---
# Add a Gauge to a Report (Report Builder and SSRS)
  In a [!INCLUDE[ssRSnoversion_md](../../includes/ssrsnoversion-md.md)] paginated report, when you want to summarize data in a visual format, you can use a gauge data region. After you add a gauge data region to the design surface, you can drag report dataset fields to a data pane on the gauge.  
  
## To add a gauge to your report  
  
1.  Create a report or open an existing report.  
  
2.  On the Insert tab, double-click Gauge. The **Select Gauge Type** dialog box opens.  
  
3.  Select the type of gauge you want to add. [!INCLUDE[clickOK](../../includes/clickok-md.md)]  
  
    > [!NOTE]  
    >  Unlike charts, gauges have only two types: linear and radial. The available gauges in the **Select a Gauge Type** dialog box are templates for these two types of gauges. For this reason, you cannot change the gauge type after you add the gauge to your report. You must delete and re-add a gauge to change its type.  
  
     If the report has no data source and dataset the **Data Source Properties** dialog box opens and takes you through the steps to create both. For more information see, [Add and Verify a Data Connection &#40;Report Builder and SSRS&#41;](../../reporting-services/report-data/add-and-verify-a-data-connection-report-builder-and-ssrs.md).  
  
     If the report has a data source, but no dataset the **Dataset Properties** dialog box opens and takes you through the steps to create one. For more information, see [Create a Shared Dataset or Embedded Dataset &#40;Report Builder and SSRS&#41;](../../reporting-services/report-data/create-a-shared-dataset-or-embedded-dataset-report-builder-and-ssrs.md).  
  
4.  Click the gauge to display the data pane. By default, a gauge has one pointer corresponding to one value. You can add additional pointers.  
  
5.  Add one field from the dataset to the data field drop-zone. You can add only one field. If you want to display multiple fields, you must add additional pointers, one for each field.  
  
     Right-click the gauge scale, and select **Scale Properties**. Type a value for the **Minimum** and **Maximum** of the scale. For more information, see [Set a Minimum or Maximum on a Gauge &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/set-a-minimum-or-maximum-on-a-gauge-report-builder-and-ssrs.md).  
  
## See Also  
 [Nested Data Regions &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/nested-data-regions-report-builder-and-ssrs.md)   
 [Gauges &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/gauges-report-builder-and-ssrs.md)  
  
  
