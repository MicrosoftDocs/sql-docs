---
title: "Add a Filter to a Dataset (Report Builder) | Microsoft Docs"
description: Learn how to add a filter to a dataset to limit the data in a report after the data is retrieved from an external data source.
ms.date: 03/07/2017
ms.service: reporting-services
ms.subservice: report-data


ms.topic: conceptual
ms.assetid: eed37e74-6a43-4d7c-9959-2d5fa6a6aba9
author: maggiesMSFT
ms.author: maggies
---
# Add a Filter to a Dataset (Report Builder and SSRS)
  Add a filter to a dataset to limit the data in a report after the data is retrieved from an external data source. When you add a filter to a dataset, all report parts or data regions use only data that matches the filter conditions.  

[!INCLUDE [ssrs-report-parts-deprecated](../../includes/ssrs-report-parts-deprecated.md)]  

 For a shared dataset, a filter that applies to all dependent items must be part of the shared dataset definition on the report server. A report or report part that contains an instance of a shared dataset can create an additional filter that applies only to the instance.  
  
 To add a filter, you must specify one or more conditions that are filter equations. A filter equation consists of an expression that identifies the data that you want to filter, an operator, and the value to compare to. The data types of the filtered data and the value must match. Filtering on aggregate values for a dataset is not supported.  
  
> [!NOTE]  
>  [!INCLUDE[ssRBRDDup](../../includes/ssrbrddup-md.md)]  
  
### To add a filter to a shared dataset  
  
1.  Open a shared dataset in shared dataset mode.  
  
2.  On the **Home** tab, in the **Shared Datasets** group, click Datasets. The **Dataset Properties** dialog box opens.  
  
3.  Click **Filters**. This displays the current list of filter equations. By default, the list is empty.  
  
4.  Click **Add**. A new blank filter equation appears.  
  
5.  In **Expression**, type or select the expression for the field to filter. To edit the expression, click the expression (*fx*) button.  
  
6.  From the list box, select the data type that matches the type of data in the expression you created in step 5.  
  
7.  In the **Operator** box, select the operator that you want the filter to use to compare the values in the **Expression** box and the **Value** box. The operator you choose determines the number of values that are used from the next step.  
  
8.  In the **Value** box, type the expression or value against which you want the filter to evaluate the value in **Expression**.  
  
     For examples of filter equations, see [Filter Equation Examples &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/filter-equation-examples-report-builder-and-ssrs.md).  
  
9.  Select **OK**.
  
### To add a filter to an embedded dataset or a shared dataset instance  
  
1.  Open a report in report design mode.  
  
2.  Right-click a dataset in the **Report Data** pane and then click **Dataset Properties**. The **Dataset Properties** dialog box opens.  
  
3.  Click **Filters**. This displays the current list of filter equations. By default, the list is empty.  
  
4.  Click **Add**. A new blank filter equation appears.  
  
5.  In **Expression**, type or select the expression for the field to filter. To edit the expression, click the expression (*fx*) button.  
  
6.  From the drop-down box, select the data type that matches the type of data in the expression you created in step 5.  
  
7.  In the **Operator** box, select the operator that you want the filter to use to compare the values in the **Expression** box and the **Value** box. The operator you choose determines the number of values that are used from the next step.  
  
8.  In the **Value** box, type the expression or value against which you want the filter to evaluate the value in **Expression**.  
  
     For examples of filter equations, see [Filter Equation Examples &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/filter-equation-examples-report-builder-and-ssrs.md).  
  
9.  Select **OK**.
  
## See Also  
 [Add Dataset Filters, Data Region Filters, and Group Filters &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/add-dataset-filters-data-region-filters-and-group-filters.md)   
 [Expression Examples &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/expression-examples-report-builder-and-ssrs.md)   
 [Add a Filter &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/add-a-filter-report-builder-and-ssrs.md)  
  
  
