---
title: "Tutorial: Format a report (Reporting Services)"
description: Learn how to format a report in SQL Server Data Tools. You can format text styles, date fields, currency fields, and the column widths after you add a data region.
author: maggiesMSFT
ms.author: maggies
ms.date: 06/13/2024
ms.service: reporting-services
ms.subservice: reporting-services
ms.topic: tutorial
ms.custom: updatefrequency5
#customer intent: As a SQL Server user, I want to format fields, adjust text styles, and modify column widths in SQL Server Data Tools (SSDT) so that my report is easily readable.

---
# Tutorial: Format a report (Reporting Services)

After you add the table to your report, you use SQL Server Data Tools (SSDT) to format the date field, currency field, and the column headers in the Sales Orders report.

In this tutorial, you:

> [!div class="checklist"]
> * Format the date field to display only the date.
> * Format the currency field to display as currency.
> * Change text style and adjust column widths.

## Prerequisites

* Completion of [Step 1: Create a report server project](tutorial-step-01-create-report-server-project-reporting-services.md).
* Completion of [Step 2: Specify connection information](tutorial-step-02-specify-connection-information-reporting-services.md).
* Completion of [Step 3: Define a dataset for the table report](tutorial-step-03-define-dataset-for-table-report-reporting-services.md).
* Completion of [Step 4: Add a table to the report](tutorial-step-04-add-table-to-report-reporting-services.md).

## Format the date

The `[Date]` field expression displays date and time information by default. You can format it to display only the date.

1. Open your Sales Order report definition file (*.rdl*) in Visual Studio.

1. On the **Design** tab, right-click the cell with the `[Date]` field expression and then select **Text Box Properties**.

1. Select **Number**, and then choose **Date** from the **Category** box.

1. In the **Type** box, select **January 31, 2000**.

1. Select **OK**.

1. Preview the report to see the change to the `[Date]` field formatting, and then go back to the **Design** tab.

## Format the currency

The `[LineTotal]` field expression displays a general number. You can format the field to display the number as currency.

1. Right-click the cell with the `[LineTotal]` expression, and select **Text Box Properties**.

1. Select **Number**, and then choose **Currency** from the **Category** list box.

1. If your regional setting is English (United States), the defaults in the **Type** list box are:

    - Decimal places: 2
    - Negative numbers: ($12345.00)
    - Symbol: $ English (United States)

1. Select **Use 1000 separator (,)**. If the sample text displays **$12,345.00**, then your settings are correct.

1. Select **OK**.

1. Preview the report to see the change to the `[LineTotal]` expression column and then go back to the **Design** tab.  

## Change text style and column widths

You can add other formatting to your report by highlighting the header, and then adjusting the widths of the data columns.

1. Select the table so that column and row handles appear on the top and side of the table. The gray bars along the top and side of the table are the column and row handles.

1. Hover over the line between column handles so that the cursor changes into a double arrow. Drag the columns to the size you want.

    :::image type="content" source="media/rs-basictabledetailsdesignarrow.png" alt-text="Screenshot of a table showing the double arrow cursor between two columns.":::

1. Highlight the row containing column header labels and then select **Format** > **Font** > **Bold**.

1. Preview the report on the **Preview** tab.

    :::image type="content" source="media/rs-basictabledetailsformattedpreview.png" alt-text="Screenshot of a table preview with bold column headers as well as updated currency and date values.":::

1. From the **File** menu, select **Save All** to save the report.

## Next step

> [!div class="nextstepaction"]
> [Step 6: Add Grouping and Totals &#40;Reporting Services&#41;](tutorial-step-06-add-grouping-and-totals-reporting-services.md)
