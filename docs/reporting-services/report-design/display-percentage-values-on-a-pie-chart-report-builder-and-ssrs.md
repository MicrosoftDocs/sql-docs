---
title: Display percentage values on a pie chart in a Report Builder paginated report
description: Learn how to display percentage values in a paginated report on a pie chart, in the legend, or in the pie slices in Report Builder.
author: maggiesMSFT
ms.author: maggies
ms.date: 07/12/2024
ms.service: reporting-services
ms.subservice: report-design
ms.topic: how-to
ms.custom: updatefrequency5

#customer intent: As a Report Builder user, I want to learn how to display percentage values in my reports so that I can customize the data visualizations in my reports to fit my needs.
---
# Display percentage values on a pie chart in a Report Builder paginated report 

[!INCLUDE[ssrs-appliesto](../../includes/ssrs-appliesto.md)] [!INCLUDE [ssrs-appliesto-ssrs-rb](../../includes/ssrs-appliesto-ssrs-rb.md)] [!INCLUDE [ssrs-appliesto-pbi-rb](../../includes/ssrs-appliesto-pbi-rb.md)] [!INCLUDE [ssrb-applies-to-ssdt-yes](../../includes/ssrb-applies-to-ssdt-yes.md)]

Report Builder lets you create pie charts and other visualizations to represent your data in a report. Within these charts, you can further customize how they look and behave to make them more useful for your report, including percentages and legends display on your charts. By default the legend for pie charts shows categories. You might also want percentages in the legend or the pie slices themselves.

:::image type="content" source="../../reporting-services/media/report-builder-pie-chart-preview-percents.png" alt-text="Screenshot of a pie chart that shows the legend of categories and percentages in the pie chart slices.":::

The [Tutorial: Add a pie chart to your report (Report Builder)](../tutorial-add-a-pie-chart-to-your-report-report-builder.md) walks you through adding percentages to pie slices, if you'd like to practice with sample data.

## Display percentage values as labels on a pie chart

1. From the **Insert** on the Report Builder menu, select **Chart** and then either **Chart Wizard...** or **Insert Chart**. For more information, see [Add a chart to a paginated report (Report Builder)](../../reporting-services/report-design/add-a-chart-to-a-report-report-builder-and-ssrs.md).

1. On the **Select Chart Type** dialog box, select one of the pie chart options under **Shape** in the **Column** tab. Or select **Pie** in the **Chart Wizard** on the **Choose a chart type** page.

1. On the **Design** surface, right-click on the pie chart and select **Show Data Labels**. The data labels appear within each slice on the pie chart.

1. On the design surface, right-click on the labels and select **Series Label Properties**. The **Series Label Properties** dialog box appears.

1. Enter `#PERCENT` for the **Label data** field.

1. (Optional) To specify how many decimal places the label shows, enter `#PERCENT{P*n*}` where `*n*` is the number of decimal places to display. For example, to display no decimal places, enter `#PERCENT{P0}`.

1. Select **OK**.

## Display percentage values in the legend of a pie chart

1. On the design surface, right-click on the pie chart and select **Series Properties**. The **Series Properties** dialog box displays.

1. On the **Legend** tab, enter `#PERCENT` in the **Custom legend text** field.

## Related content

- [Pie charts in a paginated report (Report Builder)](../../reporting-services/report-design/pie-charts-report-builder-and-ssrs.md)
- [Chart legend - format the legend on a paginated report chart (Report Builder)](../../reporting-services/report-design/chart-legend-formatting-report-builder.md)
- [Display data point labels outside a pie chart in a paginated report (Report Builder)](../../reporting-services/report-design/display-data-point-labels-outside-a-pie-chart-report-builder-and-ssrs.md)
