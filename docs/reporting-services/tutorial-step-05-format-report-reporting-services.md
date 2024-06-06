---
title: "Tutorial: Format a report (Reporting Services)"
description: Learn how to format the date and currency fields and the column headers after you add a data region and some fields to the Sales Orders report.
author: maggiesMSFT
ms.author: maggies
ms.date: 06/06/2024
ms.service: reporting-services
ms.subservice: reporting-services
ms.topic: tutorial
ms.custom: updatefrequency5
#customer intent: As a SQL Server user, I want to format date and currency fields and adjust text styles and column widths in SQL Server Data Tools (SSDT) so that my report is more easily readable.

---
# Tutorial: Format a report (Reporting Services)

Now that you have a data region and some fields to the Sales Orders report, you can format the date and currency fields and the column headers.

In this tutorial, you:

> [!div class="checklist"]
> * Format the date field to display only the date
> * Format the currency field to display as currency
> * Change text style and adjust column widths for better readability

## Prerequisites

* Your report has a data table region and fields defined.

## Format the date

The `[Date]` field expression displays date and time information by default. You can format it to display only the date.

1. On the **Design** tab, right-click the cell with the `[Date]` field expression and then select **Text Box Properties**.

1. Select **Number**, and then choose **Date** from the **Category** box.

1. In the **Type** box, select **January 31, 2000**.

    :::image type="content" source="media/rs-basictabledetailsdesigndate.png" alt-text="Screenshot of options used to format the date in this tutorial.":::

1. Select **OK** to apply the format.

1. Preview the report to see the change to the `[Date]` field formatting, and then change back to design view.

## Format the currency

The `[LineTotal]` field expression displays a general number. You can format it to display the number as currency.

1. Right-click the cell with the `[LineTotal]` expression, and select **Text Box Properties**.

1. Select **Number**, and then choose **Currency** from the **Category** list box.

1. If your regional setting is English (United States), the defaults in the **Type** list box should be:

    - Decimal places: 2
    - Negative numbers: ($12345.00)
    - Symbol: $ English (United States)

1. Select **Use 1000 separator (,)**. If the sample text displays **$12,345.00**, then your settings are correct.

    :::image type="content" source="media/rs-basictabledetailsdesigncurrency.png" alt-text="Screenshot of options used to format the currency in this tutorial.":::

1. Select **OK** to apply the format.

1. Preview the report to see the change to the `[LineTotal]` expression column and then change back to design view.  

## Change text style and column widths

You can add other formatting to your report by highlighting the header row, and adjusting the widths of the data columns.

1. Select the table so that column and row handles appear above and next to the table. The gray bars along the top and side of the table are the column and row handles.

1. Point to the line between column handles so that the cursor changes into a double arrow. Drag the columns to the size you want.

    :::image type="content" source="media/rs-basictabledetailsdesignarrow.png" alt-text="Screenshot of a table showing the double arrow cursor between two columns.":::

1. Highlight the row containing column header labels and then select **Format** > **Font** > **Bold**.

1. Preview your report. It should appear as shown in the following image:

    :::image type="content" source="media/rs-basictabledetailsformattedpreview.png" alt-text="Screenshot of a table preview with bold column headers.":::

1. From the **File** menu, select **Save All** to save the report.

You have successfully formatted column headers and field expressions. Next, you're going to add grouping and totals to your report.

## Next step

> [!div class="nextstepaction"]
> [Step 6: Add Grouping and Totals &#40;Reporting Services&#41;](tutorial-step-06-add-grouping-and-totals-reporting-services.md)

## Related content

- [Format Numbers and Dates &#40;Report Builder and SSRS&#41;](report-design/formatting-numbers-and-dates-report-builder-and-ssrs.md)
- [Rendering Behaviors &#40;Report Builder  and SSRS&#41;](report-design/rendering-behaviors-report-builder-and-ssrs.md)
