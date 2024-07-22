---
title: Add a filter to a Report Builder dataset
description: Learn how to add a filter to a dataset to limit the data in a report after the data is retrieved from an external data source.
author: maggiesMSFT
ms.author: maggies
ms.date: 07/12/2024
ms.service: reporting-services
ms.subservice: report-data
ms.topic: how-to
ms.custom: updatefrequency5

#customer intent: As a Report Builder user, I want to learn how to add filters to my datasets so that I can limit the data in a report that's retrieved from a shared data source.
---
# Add a filter to a Report Builder dataset

Add a filter to a dataset to limit the data in a report after the data is retrieved from an external data source. When you add a filter to a dataset, all report parts or data regions use only data that matches the filter conditions.  

[!INCLUDE [ssrs-report-parts-deprecated](../../includes/ssrs-report-parts-deprecated.md)]  

For a shared dataset, a filter that applies to all dependent items must be part of the shared dataset definition on the report server. A report or report part that contains an instance of a shared dataset can create another filter that applies only to the instance.  
  
To add a filter, you must specify one or more conditions that are filter equations. A filter equation consists of an expression that identifies the data that you want to filter, an operator, and the value to compare to. The data types of the filtered data and the value must match. Filtering on aggregate values for a dataset isn't supported.  
  
> [!NOTE]  
> [!INCLUDE[ssRBRDDup](../../includes/ssrbrddup-md.md)]  
  
## Add a filter to a shared dataset  
  
1. Open a shared dataset in Report Builder.  
  
1. On the **Home** tab, select **Set Options**. The **Dataset Properties** dialog box opens.  
  
1. Select the **Filters** tab. This option displays the current list of filter equations. By default, the list is empty.  
  
1. Select **Add**. A new blank filter equation appears.

    :::image type="content" source="../../reporting-services/report-data/media/add-dataset-filter.png" alt-text="Screenshot of the Dataset Properties dialog box on the Filters tab highlighting the add button.":::
  
1. In **Expression**, enter or select the expression for the field to filter. To edit the expression, select the expression (**fx**) button.  
  
1. From the dropdown list, select the data type that matches the type of data in the expression you created in step 5.  
  
1. In the **Operator** box, select the operator that you want the filter to use to compare the values in the **Expression** box and the **Value** box. The operator you choose determines the number of values that are used from the next step.  
  
1. In the **Value** box, enter the expression or value against which you want the filter to evaluate the value in **Expression**. To edit the value, select the expression (**fx**) button.
  
     For examples of filter equations, see [Filter equation examples in a paginated report (Report Builder)](../../reporting-services/report-design/filter-equation-examples-report-builder-and-ssrs.md).  
  
1. Select **OK**.
  
## Add a filter to an embedded dataset or a shared dataset instance
  
1. In your report, right-click a dataset in the **Report Data** pane and then select **Dataset Properties**. The **Dataset Properties** dialog box opens.  
  
1. Select the **Filters** tab. This option displays the current list of filter equations. By default, the list is empty.  
  
1. Select **Add**. A new blank filter equation appears.
  
1. In **Expression**, enter or select the expression for the field to filter. To edit the expression, select the expression (**fx**) button.
  
1. From the drop-down box, select the data type that matches the type of data in the expression you created in step 5.  
  
1. In the **Operator** box, select the operator that you want the filter to use to compare the values in the **Expression** box and the **Value** box. The operator you choose determines the number of values that are used from the next step.  
  
1. In the **Value** box, type the expression or value against which you want the filter to evaluate the value in **Expression**. To edit the value, select the expression (**fx**) button.
  
     For examples of filter equations, see [Filter equation examples in a paginated report (Report Builder)](../../reporting-services/report-design/filter-equation-examples-report-builder-and-ssrs.md).  
  
1. Select **OK**.
  
## Related content

- [Add dataset filters, data region filters, and group filters to a paginated report (Report Builder)](../../reporting-services/report-design/add-dataset-filters-data-region-filters-and-group-filters.md)
- [Expression examples in paginated reports (Report Builder)](../../reporting-services/report-design/expression-examples-report-builder-and-ssrs.md)
- [Add a filter to a paginated report (Report Builder)](../../reporting-services/report-design/add-a-filter-report-builder-and-ssrs.md)  
  