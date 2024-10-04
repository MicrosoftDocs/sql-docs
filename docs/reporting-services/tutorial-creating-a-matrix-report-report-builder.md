---
title: "Tutorial: Create a matrix report (Report Builder)"
description: Learn how to create a Reporting Services paginated report with a matrix of sample sales data in nested row and column groups.
author: maggiesMSFT
ms.author: maggies
ms.date: 09/25/2024
ms.service: reporting-services
ms.subservice: reporting-services
ms.topic: conceptual
ms.custom:
  - updatefrequency5
---
# Tutorial: Create a matrix report (Report Builder)
This tutorial teaches you to create a [!INCLUDE[ssRSnoversion_md](../includes/ssrsnoversion-md.md)] paginated report with a matrix of sample sales data in nested row and column groups. 

You also create an adjacent column group, format columns, and rotate text. The following illustration shows a report similar to the one you create in this tutorial.  

:::image type="content" source="../reporting-services/media/report-builder-matrix-tutorial.png" alt-text="Screenshot of a Report Builder matrix report.":::

Estimated time to complete this tutorial: 20 minutes.  
  
## Requirements  
For information about requirements, see [Prerequisites for Tutorials](../reporting-services/prerequisites-for-tutorials-report-builder.md). 
  
## <a name="CreateMatrix"></a>1. Create a matrix report and dataset from the new table or Matrix Wizard  
In this section, you choose a shared data source, create an embedded dataset, and then display the data in a matrix.  
  
> [!NOTE]  
> In this tutorial, the query already contains the data values, so that it does not need an external data source. This makes the query quite long. In a business environment, a query would not contain the data. This is for learning purposes only.  
  
### Create a matrix  
  
1.  [Start Report Builder](../reporting-services/report-builder/start-report-builder.md) either from your computer, the [!INCLUDE[ssRSnoversion_md](../includes/ssrsnoversion-md.md)] web portal, or SharePoint integrated mode.  
  
    The **New Report or Dataset** dialog box opens.  
  
    If you don't see the **New Report or Dataset** dialog box, on the **File** menu > **New**.  
  
2.  In the left pane, verify that **New Report** is selected.  
  
3.  In the right pane, select **Table or Matrix Wizard**.  
  
4.  On the **Choose a dataset** page, select **Create a dataset**.  
  
5.  Select **Next**.  
  
6.  On the **Choose a connection to a data source** page, select an existing data source, or browse to the report server and choose a data source. If no data source is available or you don't have access to a report server, you can use an embedded data source instead. For information about creating an embedded data source, see [Tutorial: Create a basic table report &#40;Report Builder&#41;](../reporting-services/tutorial-creating-a-basic-table-report-report-builder.md).  
  
7.  Select **Next**.  
  
8.  On the **Design a query** page, select **Edit as Text**.  
  
9. Copy and paste the following query into the query pane:  
  
    ```  
    SELECT CAST('2015-01-05' AS date) as SalesDate, 'Central' as Territory, 'Accessories' as Subcategory,'Carrying Case' as Product, CAST(16996.60 AS money) AS Sales, 68 as Quantity  
    UNION SELECT CAST('2015-01-05' AS date) as SalesDate, 'North' as Territory, 'Accessories' as Subcategory, 'Carrying Case' as Product, CAST(13747.25 AS money) AS Sales, 55 as Quantity  
    UNION SELECT CAST('2015-01-05' AS date) as SalesDate, 'South' as Territory, 'Accessories' as Subcategory,'Carrying Case' as Product, CAST(9248.15 AS money) As Sales, 37 as Quantity  
    UNION SELECT CAST('2015-01-05' AS date) as SalesDate, 'Central' as Territory, 'Accessories' as Subcategory,'Tripod' as Product, CAST(1350.00 AS money) AS Sales, 18 as Quantity  
    UNION SELECT CAST('2015-01-05' AS date) as SalesDate, 'North' as Territory, 'Accessories' as Subcategory,'Tripod' as Product, CAST(1800.00 AS money) AS Sales, 24 as Quantity  
    UNION SELECT CAST('2015-01-05' AS date) as SalesDate, 'South' as Territory, 'Accessories' as Subcategory,'Tripod' as Product, CAST(1125.00 AS money) AS Sales, 15 as Quantity  
    UNION SELECT CAST('2015-01-05' AS date) as SalesDate, 'Central' as Territory, 'Accessories' as Subcategory,'Lens Adapter' as Product, CAST(1147.50 AS money) AS Sales, 17 as Quantity  
    UNION SELECT CAST('2015-01-05' AS date) as SalesDate, 'North' as Territory, 'Accessories' as Subcategory,  'Lens Adapter' as Product, CAST(742.50 AS money) AS Sales, 11 as Quantity  
    UNION SELECT CAST('2015-01-05' AS date) as SalesDate, 'South' as Territory, 'Accessories' as Subcategory,'Lens Adapter' as Product, CAST(1417.50 AS money) AS Sales, 21 as Quantity  
    UNION SELECT CAST('2015-01-06' AS date) as SalesDate, 'Central' as Territory, 'Accessories' as Subcategory, 'Carrying Case' as Product, CAST(13497.30 AS money) AS Sales, 54 as Quantity  
    UNION SELECT CAST('2015-01-06' AS date) as SalesDate, 'North' as Territory, 'Accessories' as Subcategory, 'Carrying Case' as Product, CAST(11997.60 AS money) AS Sales, 48 as Quantity  
    UNION SELECT CAST('2015-01-06' AS date) as SalesDate, 'South' as Territory, 'Accessories' as Subcategory, 'Carrying Case' as Product, CAST(10247.95 AS money) As Sales, 41 as Quantity  
    UNION SELECT CAST('2015-01-06' AS date) as SalesDate, 'Central' as Territory, 'Accessories' as Subcategory, 'Tripod' as Product, CAST(1200.00 AS money) AS Sales, 16 as Quantity  
    UNION SELECT CAST('2015-01-06' AS date) as SalesDate, 'North' as Territory, 'Accessories' as Subcategory,'Tripod' as Product, CAST(2025.00 AS money) AS Sales, 27 as Quantity  
    UNION SELECT CAST('2015-01-06' AS date) as SalesDate, 'South' as Territory, 'Accessories' as Subcategory,'Tripod' as Product, CAST(1425.00 AS money) AS Sales, 19 as Quantity  
    UNION SELECT CAST('2015-01-06' AS date) as SalesDate, 'Central' as Territory, 'Accessories' as Subcategory,'Lens Adapter' as Product, CAST(887.50 AS money) AS Sales, 13 as Quantity  
    UNION SELECT CAST('2015-01-06' AS date) as SalesDate, 'North' as Territory, 'Accessories' as Subcategory, 'Lens Adapter' as Product, CAST(607.50 AS money) AS Sales, 9 as Quantity  
    UNION SELECT CAST('2015-01-06' AS date) as SalesDate, 'South' as Territory, 'Accessories' as Subcategory,'Lens Adapter' as Product, CAST(1215.00 AS money) AS Sales, 18 as Quantity  
    UNION SELECT CAST('2015-01-05' AS date) as SalesDate,  'Central' as Territory, 'Digital' as Subcategory,'Compact Digital' as Product, CAST(10191.00 AS money) AS Sales, 79 as Quantity  
    UNION SELECT CAST('2015-01-05' AS date) as SalesDate,  'North' as Territory, 'Digital' as Subcategory, 'Compact Digital' as Product, CAST(8772.00 AS money) AS Sales, 68 as Quantity  
    UNION SELECT CAST('2015-01-05' AS date) as SalesDate,  'South' as Territory, 'Digital' as Subcategory, 'Compact Digital' as Product, CAST(10578.00 AS money) AS Sales, 82 as Quantity  
    UNION SELECT CAST('2015-01-05' AS date) as SalesDate, 'Central' as Territory,'Digital' as Subcategory, 'Slim Digital' as Product, CAST(7218.10 AS money) AS Sales, 38 as Quantity  
    UNION SELECT CAST('2015-01-05' AS date) as SalesDate, 'North' as Territory,'Digital' as Subcategory, 'Slim Digital' as Product, CAST(8357.80 AS money) AS Sales, 44 as Quantity  
    UNION SELECT CAST('2015-01-05' AS date) as SalesDate, 'South' as Territory,'Digital' as Subcategory,'Slim Digital' as Product, CAST(9307.55 AS money) AS Sales, 49 as Quantity  
    UNION SELECT CAST('2015-01-06' AS date) as SalesDate,  'Central' as Territory, 'Digital' as Subcategory,'Compact Digital' as Product, CAST(3870.00 AS money) AS Sales, 30 as Quantity  
    UNION SELECT CAST('2015-01-06' AS date) as SalesDate,  'North' as Territory, 'Digital' as Subcategory,'Compact Digital' as Product, CAST(5805.00 AS money) AS Sales, 45 as Quantity  
    UNION SELECT CAST('2015-01-06' AS date) as SalesDate,  'South' as Territory, 'Digital' as Subcategory, 'Compact Digital' as Product, CAST(8643.00 AS money) AS Sales, 67 as Quantity  
    UNION SELECT CAST('2015-01-06' AS date) as SalesDate, 'Central' as Territory, 'Digital' as Subcategory, 'Slim Digital' as Product, CAST(9877.40 AS money) AS Sales, 52 as Quantity  
    UNION SELECT CAST('2015-01-06' AS date) as SalesDate, 'North' as Territory, 'Digital' as Subcategory, 'Slim Digital' as Product, CAST(12536.70 AS money) AS Sales, 66 as Quantity  
    UNION SELECT CAST('2015-01-06' AS date) as SalesDate, 'South' as Territory, 'Digital' as Subcategory, 'Slim Digital' as Product, CAST(6648.25 AS money) AS Sales, 35 as Quantity  
    ```  
  
10. (optional) Select the Run icon (!) to run the query and see the data.

11. Select **Next**.  
  
## <a name="Groups"></a>2. Organize data and choose layout from the new table or Matrix Wizard  
Use the wizard to provide a starting design on which to display data. The preview pane in the wizard helps you to visualize the result of grouping data before you complete the matrix design.  
  
1.  On the **Arrange fields** page, drag Territory from **Available fields** to **Row groups**.  
  
2.  Drag SalesDate to **Row groups** and place it after Territory.  
  
    The order in which fields are listed in **Row groups** defines the group hierarchy. Steps 1 and 2 organize the values of the fields first by territory, and then by sales date.  
  
3.  Drag Subcategory to **Column groups**.  
  
4.  Drag Product to **Column groups** and place it after Subcategory.  
  
    Again, the order in which fields are listed in **Column groups** defines the group hierarchy. Steps 3 and 4 organize the values for the fields first by subcategory, and then by product.  
  
5.  Drag Sales to **Values**.  
  
    Sales is summarized with the Sum function, the default function to summarize numeric fields.  
  
6.  Drag Quantity to **Values**.  
  
    Quantity is summarized with the Sum function.  
  
    Steps 5 and 6 specify the data to display in the matrix data cells.

    :::image type="content" source="../reporting-services/media/report-builder-arrange-fields-report-wizard.png" alt-text="Screenshot that shows the Report Builder arrange fields Report Wizard.":::
  
7.  Select **Next**.  
  
8.  On the Choose the Layout page, under **Options**, verify that **Show subtotals and grand totals** is selected.  
  
9. Verify that **Blocked, subtotal below** is selected.  
  
10. Verify the option **Expand/collapse groups** is selected.  
  
11. Select **Next**.  
  
13. Select **Finish**.  
  
    The matrix is added to the design surface. The Row Groups pane shows two row groups: Territory and SalesDate. The Column Groups pane shows two column groups: Subcategory and Product. Detail data is all the data the dataset query retrieves.  

    :::image type="content" source="../reporting-services/media/report-builder-row-and-column-groups.png" alt-text="Screenshot that shows the Report Builder Row Groups and Column Groups.":::
  
14. Select **Run** to preview the report.  
  
    For each product that is sold on a specific date, the matrix shows the subcategory to which the product belongs and the territory of the sales.  

14. Expand a subcategory. You can see the report quickly gets wide.

:::image type="content" source="../reporting-services/media/report-builder-expand-matrix.png" alt-text="Screenshot that shows an expanded Report Builder matrix report.":::
  
## <a name="FormatData"></a>3. Format data  
By default, the summary data for the Sales field displays a general number and the SalesDate field displays both date and time information. In this section, you format the Sales field to display the number as currency and the SalesDate field to display only the date. Toggle **Placeholder Styles** to display formatted text boxes and placeholder text as sample values.  
  
### Format fields  
  
1.  Select **Design** to switch to design view.  
  
2.  Press the Ctrl key, and then select the nine cells that contain `[Sum(Sales)]`.  
  
3.  On the **Home** tab > **Number** > **Currency**. The cells change to show the formatted currency.  
  
    If your regional setting is English (United States), the default sample text is [**$12,345.00**]. If you don't see an example currency value in the **Numbers** group, select **Placeholder Styles** > **Sample Values**.  

    :::image type="content" source="../reporting-services/media/report-builder-placeholder-value.png" alt-text="Screenshot of the Report Builder Sample Values option.":::
  
4.  Select the cell that contains `[SalesDate]`.  
  
5.  In the **Number** group, go to **Date**.  
  
    The cell displays the example date **[1/31/2000]**. If you don't see an example date, select **Placeholder Styles** in the **Numbers** group, and then choose **Sample Values**.  
  
6.  Select **Run** to preview your report.  
  
The date values display only dates and the sales values display as currency.  
  
## <a name="AdjacentGroup"></a>4. Add adjacent column group  
You can nest row and column groups in parent-child relationships, or adjacent in sibling relationships.  
  
In this section, you add a column group next to the Subcategory column group, copy cells to populate the new column group, and then use an expression to create the value of the column group header.  
  
### Add an adjacent column group  
  
1.  Select **Design** to return to design view.  
  
2.  Right-click the cell that contains `[Subcategory]`, point to **Add Group**, and then select **Adjacent Right**.  
  
    The **Tablix Group** dialog box opens.  
  
3.  In the **Group By** list, select SalesDate, and then choose **OK**.  
  
    A new column group is added to the right of the Subcategory column group.  
  
4.  Right-click the cell in the new column group that contains `[SalesDate],` and then select **Expression**.  
  
5.  Copy the following expression to the expression box.  
  
    ```  
    =WeekdayName(DatePart("w",Fields!SalesDate.Value))  
    ```  
  
    This expression extracts the weekday name from the sales date. For more information, see [Expressions in a paginated report (Report Builder)](../reporting-services/report-design/expressions-report-builder-and-ssrs.md).  
  
6.  Right-click the cell in the Subcategory column group that contains Total, and then select **Copy**.  
  
7.  Right-click the cell immediately below the cell that contains the expression you created in step 5 and select **Paste**.  
  
8.  Press the Ctrl key.  
  
9. In the Subcategory group, select the Sales column header and the three cells below it, right-click, and then choose **Copy**.  
  
10. Paste the four cells into the four empty cells in the new column group.  
  
11. Select **Run** to preview the report.  
  
The report includes columns named Monday and Tuesday. The dataset contains only data for these two days.  

:::image type="content" source="../reporting-services/media/report-builder-matrix-weekdays.png" alt-text="Screenshot that shows weekdays displayed in the Report Builder matrix report.":::
  
> [!NOTE]  
> If the data included other days, the report would include columns for them as well. Each column has the column header, **Sales**, and sales totals by territory.  
  
## <a name="Width"></a>5. Change column widths  
A report that includes a matrix typically expands horizontally and vertically when it runs. Controlling horizontal expansion is important if you plan to export the report to formats such as Microsoft Word or Adobe PDF that are used for printed reports. If the report expands horizontally across multiple pages, the printed report is difficult to understand. To minimize horizontal expansion, you can resize columns to be only the width necessary to display the data without wrapping. You can also rename columns so that their titles fit the width needed to display the data.  
  
### Rename and resize the columns  
  
1.  Select **Design** to return to design view.  
  
2.  Select the text in the furthest Quantity column to the left, and then enter **QTY**.  
  
    The column title is now QTY.  
  
3.  Repeat step 2 for the two other columns named Quantity.
  
4.  Select the matrix so that column and row handles appear above the matrix and next to it.  
  
    The gray bars along the top and side of the table are the column and row handles.  

    :::image type="content" source="../reporting-services/media/report-builder-column-handles.png" alt-text="Screenshot for the column and row handles in the Report Builder matrix report.":::
  
5.  To resize the QTY column farthest to the left of the matrix, point to the line between column handles so that the cursor changes into a double arrow. Drag the column towards the left until it's 0.5 inches wide.  
  
    A column width of 0.5 inches is adequate to display the quantity.  
  
6.  Repeat step 5 for the other columns named QTY.  
  
7.  Select **Run** to preview your report.  
  
The columns that contain quantities are now narrower and are named QTY.  
  
## <a name="MergeCells"></a>6. Merge matrix cells  
The corner area is in the upper left corner of the matrix. Depending on the number of row and column groups in the matrix, the number of cells in the corner area varies. The matrix, built in this tutorial, has four cells in its corner area. The cells are arranged in two rows and two columns, reflecting the depth of row and column group hierarchies. The four cells aren't used in this report and you merge them into one.  
  
### Merge matrix cells  
  
1.  Select **Design** to return to design view.  
  
2.  Select the matrix so that column and row handles appear above the matrix and next to it.  
  
3.  Press the Ctrl key and select the four corner cells.  
  
4.  Right-click the cells and select **Merge Cells**.  
  
5.  Right-click the new merged cell and select **Text Box Properties**.  
  
6.  On the **Border** tab > **Presets** > **None**.
  
9. Select **OK**.
  
10. Select **Run** to preview your report.  
  
The cell in the upper corner of the matrix is no longer visible. 
  
## <a name="HeaderTitle"></a>7. Add a report header and report title  
A report title appears at the top of the report. You can place the report title in a report header or if the report doesn't use one, in a text box at the top of the report body. In this tutorial, you remove the text box at the top of the report and add a title to the header.  
  
### Add a report header and report title  
  
1.  Select **Design** to return to design view.  
  
2.  Select the text box at the top of the report body that contains **Click to add title**, and then press the Delete key.  
  
3.  On the **Insert** tab, go to **Header** > **Add Header**.  
  
    A header is added to the top of the report body.  
  
4.  On the **Insert** tab, select **Text Box**, and then drag a text box inside the report header. Make the text box about 6 inches long and 3/4 inch tall and place it on the left side of the report header.  
  
5.  In the text box, enter **Sales by Territory, Subcategory, and Day**.  
  
6.  Select the text you entered, on the **Home** tab > **Font**:
    * **Size 24 pt**
    * **Color Maroon**
 
10. Select **Run** to preview the report.  
  
The report includes a report title in the report header.  
  
## <a name="Save"></a>8. Save the report  
You can save reports to a report server, SharePoint library, or your computer.  
  
In this tutorial, save the report to a report server. If you don't have access to a report server, save the report to your computer.  
  
### Save the report on a report server  
  
1.  From the **Report Builder** button, select **Save As**.  
  
2.  Select **Recent Sites and Servers**.  
  
3.  Select or enter the name of the report server where you have permission to save reports.  
  
    The message "Connecting to report server" appears. When the connection is complete, you see the contents of the report folder that the report server administrator specified as the default report location.  
  
4.  In **Name**, replace the default name with **SalesByTerritorySubcategory**.  
  
5.  Select **Save**.  
  
The report is saved to the report server. The name of the report server that you're connected to appears in the status bar at the bottom of the window.  
  
#### Save the report on your computer  
  
1.  From the **Report Builder** button, select **Save As**.  
  
2.  Select **Desktop**, **My Documents**, or **My computer**, and then browse to the folder where you want to save the report.  
  
3.  In **Name**, replace the default name with **SalesByTerritorySubcategory**.  
  
4.  Select **Save**.  
  
## <a name="RotateTextBox"></a>9. (Optional) Rotate text box 270 degrees  
A report with matrices can expand horizontally and vertically when it runs. By rotating text boxes vertically, or 270 degrees, you can save horizontal space. The rendered report is then narrower and if exported to a format such as Microsoft Word, is more likely to fit on a printed page.  
  
A text box can also display text as horizontal, vertical (top to bottom). For more information, see [Text boxes in paginated reports (Report Builder)](../reporting-services/report-design/text-boxes-report-builder-and-ssrs.md).  
  
### Rotate text box 270 degrees  
  
1.  Select **Design** to return to design view.  
  
2.  Select the cell that contains `[Territory].` 

    > [!NOTE]  
    > Select the cell, not the text. The WritingMode property is only available for the cell.

    :::image type="content" source="../reporting-services/media/report-builder-select-territory-cell.png" alt-text="Screenshot that shows the Territory cell in the Report Builder matrix report.":::
  
3.  In the Properties pane, locate the WritingMode property and change it from **Default** to **Rotate270**.  
  
    If the Properties pane isn't open, select the **View** tab of the ribbon, and then choose **Properties**.  
  
4.  Verify that the CanGrow property is set to **True**.  
  
5.  On the **Home** tab > **Paragraph** section, select **Middle** and **Center** to locate the text in the center of the cell both vertically and horizontally.  
 
6. Resize the Territory column to be 0.5 inches wide and delete the column title.  
6. Select **Run** to preview your report.  
  
The territory name is written vertically, bottom to top. The height of the Territory row group varies by the length of the territory name.  
  
## Related content

- [Report Builder tutorials](../reporting-services/report-builder-tutorials.md)
- [Report Builder in SQL Server](../reporting-services/report-builder/report-builder-in-sql-server-2016.md)
- [Tables, matrices, and lists in paginated reports (Report Builder)](../reporting-services/report-design/tables-matrices-and-lists-report-builder-and-ssrs.md)
- [Create a matrix in a paginated report (Report Builder)](../reporting-services/report-design/create-a-matrix-report-builder-and-ssrs.md)
- [Tablix data region areas in a paginated report (Report Builder)](../reporting-services/report-design/tablix-data-region-areas-report-builder-and-ssrs.md)
- [Cells, rows, & columns in a tablix in a paginated report (Report Builder)](../reporting-services/report-design/tablix-data-region-cells-rows-and-columns-report-builder-and-ssrs.md)
