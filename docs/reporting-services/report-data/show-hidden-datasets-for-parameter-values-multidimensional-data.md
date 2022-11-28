---
title: "Show Hidden Datasets for Parameter Values - Multidimensional Data | Microsoft Docs"
description: Learn how to show hidden datasets for parameter values so you can display all datasets in a report.
ms.date: 03/07/2017
ms.service: reporting-services
ms.subservice: report-data


ms.topic: conceptual
ms.assetid: eb01c4ca-4fd6-4629-b595-f0d2565915df
author: maggiesMSFT
ms.author: maggies
---
# Show Hidden Datasets for Parameter Values - Multidimensional Data
  Your report might include automatically-generated datasets (also known as hidden datasets) that do not appear by default in the Report Data pane. These datasets are created in the following ways:  
  
-   In some query designers for multidimensional databases, you can specify fields to filter on in the filter area of the query pane, and select whether to create a query parameter for the filter. If you select the parameter option, report datasets are automatically created to provide valid values for the report parameter.  
  
-   If you import a query based on multidimensional databases, you might also include hidden datasets in your report.  
  
 Hidden datasets are not available to use from a wizard.  
  
 You can change the view in the Report Data pane to display all datasets in the report.  
  
> [!NOTE]  
>  [!INCLUDE[ssRBRDDup](../../includes/ssrbrddup-md.md)]  
  
### To display hidden datasets  
  
-   In the Report Data pane, right-click the Datasets folder, and then click **Show Hidden Datasets**.  
  
## See Also  
 [Query Design Tools &#40;SSRS&#41;](query-design-tools-ssrs.md)   
 [Reporting Services Query Designers](/previous-versions/sql/)   
 [Report Embedded Datasets and Shared Datasets &#40;Report Builder and SSRS&#41;](../../reporting-services/report-data/report-embedded-datasets-and-shared-datasets-report-builder-and-ssrs.md)   
 [Report Datasets &#40;SSRS&#41;](../../reporting-services/report-data/report-datasets-ssrs.md)  
  
