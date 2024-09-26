---
title: "Add a gauge to a paginated report"
description: Learn how to summarize data in a paginated report in a visual format. Summarize the data in Report Builder by creating a gauge data region.
author: maggiesMSFT
ms.author: maggies
ms.date: 09/25/2024
ms.service: reporting-services
ms.subservice: report-design
ms.topic: conceptual
ms.custom:
  - updatefrequency5
---
# Add a gauge to a paginated report (Report Builder)

[!INCLUDE[ssrs-appliesto](../../includes/ssrs-appliesto.md)] [!INCLUDE [ssrs-appliesto-ssrs-rb](../../includes/ssrs-appliesto-ssrs-rb.md)] [!INCLUDE [ssrs-appliesto-pbi-rb](../../includes/ssrs-appliesto-pbi-rb.md)] [!INCLUDE [ssrb-applies-to-ssdt-yes](../../includes/ssrb-applies-to-ssdt-yes.md)]

  In a paginated report, when you want to summarize data in a visual format, you can use a gauge data region. After you add a gauge data region to the design surface, you can drag report dataset fields to a data pane on the gauge.  
  
## Add a gauge to your report  

1.  Create a report or open an existing report.  
  
1.  On the **Insert** tab, double-click **Gauge**. The **Select Gauge Type** dialog opens.  
  
1.  Select the type of gauge you want to add. Select **OK**.
  
    > [!NOTE]  
    >  Unlike charts, gauges have only two types: linear and radial. The available gauges in the **Select a Gauge Type** dialog are templates for these two types of gauges. For this reason, you can't change the gauge type after you add the gauge to your report. You must delete and re-add a gauge to change its type.  
  
     If the report has no data source and dataset, the **Data Source Properties** dialog opens and takes you through the steps to create both. For more information, see [Add and verify a data connection &#40;Report Builder&#41;](../../reporting-services/report-data/add-and-verify-a-data-connection-report-builder-and-ssrs.md).  
  
     If the report has a data source, but no dataset the **Dataset Properties** dialog opens and takes you through the steps to create one. For more information, see [Create a shared dataset or embedded dataset &#40;Report Builder&#41;](../../reporting-services/report-data/create-a-shared-dataset-or-embedded-dataset-report-builder-and-ssrs.md).  
  
1.  Select the gauge to display the data pane. By default, a gauge has one pointer corresponding to one value. You can add more pointers.  
  
1.  Add one field from the dataset to the data field drop-zone. You can add only one field. If you want to display multiple fields, you must add more pointers, one for each field.  
  
     Right-click the gauge scale, and select **Scale Properties**. Enter a value for the **Minimum** and **Maximum** of the scale. For more information, see [Set a minimum or maximum on a gauge &#40;Report Builder&#41;](../../reporting-services/report-design/set-a-minimum-or-maximum-on-a-gauge-report-builder-and-ssrs.md).  
  
## Related content

- [Nested data regions &#40;Report Builder&#41;](../../reporting-services/report-design/nested-data-regions-report-builder-and-ssrs.md)
- [Gauges &#40;Report Builder&#41;](../../reporting-services/report-design/gauges-report-builder-and-ssrs.md)
