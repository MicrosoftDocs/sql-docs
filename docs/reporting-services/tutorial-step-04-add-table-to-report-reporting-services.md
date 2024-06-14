---
title: "Tutorial: Add a table to the report"
description: Learn how to create a report layout in SQL Server Data Tools (SSDT) by dragging and dropping report objects from the tool pane to the design surface.
author: maggiesMSFT
ms.author: maggies
ms.date: 06/13/2024
ms.service: reporting-services
ms.subservice: reporting-services
ms.topic: tutorial
ms.custom: updatefrequency5
#customer intent: As a SQL Server user, I want to use the drag-and-drop feature in SQL Server Data Tools (SSDT) to quickly create a report layout.
---
# Tutorial: Add a table to the report (Reporting Services)

In this tutorial, you can design a paginated report. You create a report layout by dragging and dropping report objects from the **Toolbox** pane to the **Design surface**. 

Items that contain repeated rows of data from underlying datasets are called *data regions*. After you add a data region, you can add fields. A basic report has only one data region. You can add extras to display more information such as a chart.

In this tutorial, you:

> [!div class="checklist"]
> * Add a table data region to the report layout
> * Populate the table with fields from the dataset
> * Preview your report to validate design and data connections

## Prerequisites

* Completion of [Step 1: Create a report server project](tutorial-step-01-create-report-server-project-reporting-services.md).
* Completion of [Step 2: Specify connection information](tutorial-step-02-specify-connection-information-reporting-services.md).
* Completion of [Step 3: Define a dataset for the table report](tutorial-step-03-define-dataset-for-table-report-reporting-services.md).

## Add a table data region and fields to a report layout

Here, you add a table data region to your report layout and populate it with fields from your dataset to display organized data.

1. Make sure your Sales Order report is open in Visual Studio.

1. Select the **Toolbox** tab in the left pane of the Report Designer. If you don't see the **Toolbox** tab, select **View** menu > **Toolbox**.

1. Choose the **Table** object and drag it to the report design surface. You can also add a table to the report from the design surface. Right-click the design surface and select **Insert** > **Table**.

    :::image type="content" source="media/ssrs-ssdt-addtable.png" alt-text="Screenshot of the Toolbox tab with the Table option selected.":::

    Report Designer draws a table data region with three columns in the center of the design surface.

1. In the **Report Data** pane, expand **AdventureWorksDataset** to display the fields.

1. Drag the `Date` field from the **Report Data** pane to the first column in the table.

    > [!IMPORTANT]
    > When you drop the field into the first column, two things happen. 
    > * Report Designer displays the field name, known as the *field expression*, in brackets. For example, in this tutorial, `Date` displays in the data cell. 
    > * Report Designer adds a column label to the header row, just above the field expression. By default, the column label is the name of the field. You can select the column label and enter a new value if you want to change it.

1. Drag the `Order` field from the **Report Data** pane to the second column in the table.

1. Drag the `Product` field from the **Report Data** pane to the third column in the table.

1. Drag the `Qty` field to the right edge of the third column until you get a vertical cursor and the pointer shows a plus sign [+]. When you drop the field, you create a fourth column for the `Qty` field expression.

    :::image type="content" source="media/ssrs-tutorial-addcolumn.png" alt-text="Screenshot of the Product field in the table.":::

1. Add the `LineTotal` field in the same way, creating a fifth column. The column label shows as "Line Total". Report Designer automatically creates a friendly name for the column by splitting "LineTotal" into two words.

    The following diagram shows a table data region populated with these fields: `Date`, `Order`, `Product`, `Qty`, and `Line Total`.

    :::image type="content" source="./media/rs-basictabledetailsdesign.png" alt-text="Diagram of a table data region populated with the fields: Date, Order, Product, Qty, and Line Total.":::

## Preview your report

While designing, preview your report frequently. By doing so, you validate the design and data connections and you can correct errors and issues as you go.

1. Select the **Preview** tab. 

    :::image type="content" source="./media/ssrs-ssdt-preview.png" alt-text="Screenshot of the Preview tab in Report Designer.":::

    Report Designer runs the report and displays it in the **Preview** view. The following diagram shows part of the report in **Preview** view.

    :::image type="content" source="./media/rs-basictabledetailspreview.png" alt-text="Diagram of part of the report in the Preview view.":::

1. Select **File** > **Save All** to save the report.

## Next step

> [!div class="nextstepaction"]
> [Step 5: Format a report &#40;Reporting Services&#41;](tutorial-step-05-format-report-reporting-services.md) 
