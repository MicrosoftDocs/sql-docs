---
title: "Lesson 4: Add a table to the report"
description: You create a report layout by dragging and dropping report objects, like a table, from the Toolbox pane to the Design surface.
author: maggiesMSFT
ms.author: maggies
ms.date: 12/16/2019
ms.service: reporting-services
ms.subservice: reporting-services
ms.topic: conceptual
ms.custom: updatefrequency5
---
# Lesson 4: Add a table to the report (Reporting Services)

After you define the dataset, you can start designing the paginated report. You create a report layout by dragging and dropping *report objects* from the **Toolbox** pane to the **Design surface**. Some of the types of report objects include:

- Table
- Text Box
- Image
- Line
- Rectangle
- Chart
- Map

Items that contain repeated rows of data from underlying datasets are called *data regions*. After you add a data region, you can add fields to the data region. A basic report has only one data region. You can add extras to display more information such as a chart.

## Add a table data region and fields to a report layout

1. Select the **Toolbox** tab in the left pane of the Report Designer. With your mouse, choose the **Table** object and drag it to the report design surface. Report Designer draws a table data region with three columns in the center of the design surface. If you don't see the **Toolbox** tab, select **View** menu > **Toolbox**.

    :::image type="content" source="media/ssrs-ssdt-addtable.png" alt-text="Screenshot of the Toolbox tab with the Table option selected.":::

    You can also add a table to the report from the design surface. Right-click the design surface and select **Insert** > **Table**.

2. In the **Report Data** pane, expand the AdventureWorksDataset to display the fields.

3. Drag the `[Date]` field from the **Report Data** pane to the first column in the table.

    > [!IMPORTANT]
    > When you drop the field into the first column, two things happen. First, Report Designer displays the field name, known as the *field expression*, in brackets: `[Date]` in the data cell. Second, it adds a column label to the header row, just above the field expression. By default, the column label is the name of the field. You can select the column label and enter a new value if you want to change it.

4. Drag the `[Order]` field from the **Report Data** pane to the second column in the table.

5. Drag the `[Product]` field from the **Report Data** pane to the third column in the table.

6. Drag the `[Qty]` field to the right edge of the third column until you get a vertical cursor and the mouse pointer displays a plus sign [+]. When you release the mouse button, a fourth column is created for the `[Qty]` field expression.

    :::image type="content" source="media/ssrs-tutorial-addcolumn.png" alt-text="Screenshot of the Product field in the table.":::

7. Add the `[LineTotal]` field in the same way, creating a fifth column. The column label is added as "Line Total". Report Designer automatically creates a friendly name for the column by splitting "LineTotal" into two words.

The following diagram shows a table data region populated with these fields: Date, Order, Product, Qty, and Line Total.

:::image type="content" source="./media/rs-basictabledetailsdesign.png" alt-text="Diagram of a table data region populated with the fields: Date, Order, Product, Qty, and Line Total.":::

## Preview your report

Previewing a report enables you to view the rendered report without having to first publish it to a report server. Preview your report frequently while designing it. By doing so, you validate the design and data connections allowing you to correct errors and issues as you go.

### Preview a report

Select the **Preview** tab. Report Designer runs the report and displays it in the **Preview** view.

:::image type="content" source="./media/ssrs-ssdt-preview.png" alt-text="Screenshot of the Preview tab in Report Designer.":::

The following diagram shows part of the report in **Preview** view.

:::image type="content" source="./media/rs-basictabledetailspreview.png" alt-text="Diagram of part of the report in the Preview view.":::

Look at the Date and Line Total values. In the next lesson, you're going to learn how to format them to display more neatly.

> [!NOTE]
> From the **File** menu, select **Save All** to save the report.

## Next step

You successfully added a table data region to your report, added fields to the data region, and previewed your report. In the next lesson, you're going to learn how to format column headers and field expressions. Next, continue with [Lesson 5: Format a report &#40;Reporting Services&#41;](lesson-5-formatting-a-report-reporting-services.md).
  
## Related content

- [Tables &#40;Report Builder and SSRS&#41;](report-design/tables-report-builder-and-ssrs.md)  
- [Dataset fields collection &#40;Report Builder and SSRS&#41;](report-data/dataset-fields-collection-report-builder-and-ssrs.md)  
