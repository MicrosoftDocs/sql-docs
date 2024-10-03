---
title: Add a filter to a Report Builder paginated report
description: Learn how to add a filter to a dataset, data region, or group when you want to include or exclude specific values for calculations in a paginated report.
author: maggiesMSFT
ms.author: maggies
ms.date: 09/25/2024
ms.service: reporting-services
ms.subservice: report-design
ms.topic: how-to
ms.custom:
  - updatefrequency5
# customer intent: As a Report Builder user, I want to learn how to add filters to my reports so that I can interactively include and exclude values in my report calculations.
---
# Add a filter to a Report Builder paginated report

[!INCLUDE[ssrs-appliesto](../../includes/ssrs-appliesto.md)] [!INCLUDE [ssrs-appliesto-ssrs-rb](../../includes/ssrs-appliesto-ssrs-rb.md)] [!INCLUDE [ssrs-appliesto-pbi-rb](../../includes/ssrs-appliesto-pbi-rb.md)] [!INCLUDE [ssrb-applies-to-ssdt-yes](../../includes/ssrb-applies-to-ssdt-yes.md)]

Add a filter to a dataset, data region, or group to a paginated report when you want to include or exclude specific values for calculations or display. Filters are applied at run time first on the dataset. Then, the filters are applied on the data region, and then on the group, in top-down order for group hierarchies. In a table, matrix, or list, filters for row groups, column groups, and adjacent groups are applied independently. In a chart, filters for category groups and series groups are applied independently.  
  
To add a filter, specify one or more filter equations. A filter equation consists of an expression that identifies the data that you want to filter, an operator, and the value to compare to. The data types of the filtered data and the value must match. Filtering on aggregate values for a dataset or data region isn't supported.  
  
To filter data points in a chart, you can set a filter on a category group or a series group. By default, the chart uses the built-in function **Sum** to aggregate values that belong to the same group into an individual data point in the series. If you change the aggregate function of a series, you must change the aggregate function in the filter expression.  
  
 For more information about filtering embedded and shared datasets, see [Add a filter to a dataset (Report Builder and SSRS)](../../reporting-services/report-data/add-a-filter-to-a-dataset-report-builder-and-ssrs.md).  
  
> [!NOTE]  
> [!INCLUDE[ssRBRDDup](../../includes/ssrbrddup-md.md)]  
  
## Set a filter on a data region  
  
1. Open a report in **Design** view.  
  
1. Select the data region on the design surface, and then right-click `<data region>`**Properties**. For a gauge, select **Gauge Panel Properties**. The `<data region>`**Properties** dialog opens.

    :::image type="content" source="../../reporting-services/report-design/media/gauge-panel-properties.png" alt-text="Screenshot of the context menu for a Gauge highlighting the Gauge Panel Properties option.":::
  
    > [!NOTE]  
    > On a Tablix data region, right-click the corner cell or a row or column handle, and then select **Tablix Properties**.  
  
1. Select **Filters**. This action displays the current list of filter equations. By default, the list is empty.  
  
1. Select **Add**. A new blank filter equation appears.

    :::image type="content" source="../../reporting-services/report-design/media/add-gauge-filter.png" alt-text="Screenshot of the Filters tab in the Gauge Panel Properties dialog box that highlights the add button.":::
  
1. In **Expression**, enter or select the expression for the field to filter. To edit the expression, select the expression **fx** button.  
  
1. From the list, select the data type that matches the type of data in the expression you created in step 5.  
  
1. In the **Operator** box, select the operator that you want the filter to use to compare the values in the **Expression** box and the **Value** box. The operator you choose determines the number of values that are used from the next step.  
  
1. In the **Value** box, enter the expression or value against which you want the filter to evaluate the value in **Expression**.  
  
     For examples of filter equations, see [Filter equation examples in a paginated report (Report Builder)](../../reporting-services/report-design/filter-equation-examples-report-builder-and-ssrs.md).  
  
1. Select **OK**.
  
## Set a filter on a Tablix row or column group  
  
1. Open a report in **Design** view.  
  
1. Select table, matrix, or list data region on the design surface. The **Grouping** pane displays the groups for the selected item.  
  
1. In the **Grouping** pane, right-click the group, and then select **Group Properties** to add filter to the group.

    :::image type="content" source="../../reporting-services/report-design/media/group-properties.png" alt-text="Screenshot of the context menu for a Tablix data region group that highlights the Group Properties option.":::

1. Select **Filters**. This action displays the current list of filter equations. By default, the list is empty.  
  
1. Select **Add**. A new blank filter equation appears.  
  
1. In **Expression**, enter or select the expression for the field to filter. To edit the expression, select the expression **fx** button.  
  
1. From the list, select the data type that matches the type of data in the expression you created in step 5.  
  
1. In the **Operator** box, select the operator that you want the filter to use to compare the values in the **Expression** box and the **Value** box. The operator you choose determines the number of values that are used from the next step.  
  
1. In the **Value** box, enter the expression or value against which you want the filter to evaluate the value in **Expression**.  
  
     For examples of filter equations, see [Filter equation examples in a paginated report (Report Builder)](../../reporting-services/report-design/filter-equation-examples-report-builder-and-ssrs.md).  
  
1. Select **OK**.
  
## Set a filter on a Chart category group  
  
1. Open a report in **Design** view.  
  
1. On the design surface, select the chart to bring up data, series, and **Category Groups** drop zones.  
  
1. Right-click on a field contained in the **Category Group** drop zone and select **Category Group Properties**.  
  
1. Select **Filters**. This action displays the current list of filter equations. By default, the list is empty.

    :::image type="content" source="../../reporting-services/report-design/media/category-group-properties.png" alt-text="Screenshot of the context menu for the Category Group drop zone that highlights the Category Group Properties option.":::
  
1. Select **Add**. A new blank filter equation appears.  
  
1. In **Expression**, enter or select the expression for the field to filter. To edit the expression, select the expression **fx** button.  
  
1. From the list, select the data type that matches the type of data in the expression you created in step 5.  
  
1. In the **Operator** box, select the operator that you want the filter to use to compare the values in the **Expression** box and the **Value** box. The operator you choose determines the number of values that are used from the next step.  
  
1. In the **Value** box, type the expression or value against which you want the filter to evaluate the value in **Expression**.  
  
     For examples of filter equations, see [Filter equation examples in a paginated report (Report Builder)](../../reporting-services/report-design/filter-equation-examples-report-builder-and-ssrs.md).  
  
1. Select **OK**.
  
## Set a filter on a Chart series group  
  
1. Open a report in **Design** view.  
  
1. On the design surface, double-click the chart to bring up data, series, and category field drop zones.  
  
1. Right-click on a field contained in the series field drop zone and select **Series Group Properties**.

    :::image type="content" source="../../reporting-services/report-design/media/series-group-properties.png" alt-text="Screenshot of the context menu for the Series Group drop zone that highlights the Series Group Properties option.":::
  
1. Select **Filters**. This action displays the current list of filter equations. By default, the list is empty.  
  
1. Select **Add**. A new blank filter equation appears.  
  
1. In **Expression**, enter or select the expression for the field to filter. To edit the expression, select the expression **fx** button.  
  
1. From the list, select the data type that matches the type of data in the expression you created in step 5.  
  
1. In the **Operator** box, select the operator that you want the filter to use to compare the values in the **Expression** box and the **Value** box. The operator you choose determines the number of values that are used from the next step.  
  
1. In the **Value** box, type the expression or value against which you want the filter to evaluate the value in **Expression**.  
  
     For examples of filter equations, see [Filter equation examples in a paginated report (Report Builder)](../../reporting-services/report-design/filter-equation-examples-report-builder-and-ssrs.md).  
  
1. Select **OK**.
  
## Related content

- [Add dataset filters, data region filters, and group filters to a paginated report (Report Builder)](../../reporting-services/report-design/add-dataset-filters-data-region-filters-and-group-filters.md)
- [Expression examples in paginated reports (Report Builder)](../../reporting-services/report-design/expression-examples-report-builder-and-ssrs.md)
- [Gauges in a paginated report (Report Builder)](../../reporting-services/report-design/gauges-report-builder-and-ssrs.md)
