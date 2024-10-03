---
title: "Tutorial: Add grouping and totals (Reporting Services)"
description: Learn how to add grouping and totals to your Reporting Services report by using SQL Server Data Tools (SSDT) to organize and summarize your data.
author: maggiesMSFT
ms.author: maggies
ms.date: 09/25/2024
ms.service: reporting-services
ms.subservice: reporting-services
ms.topic: tutorial
ms.custom:
  - updatefrequency5
# customer intent: As a SQL Server user, I want to add grouping and totals to my report by using SQL Server Data Tools (SSDT) so that I can organize and summarize my data effectively.
---

# Tutorial: Add grouping and totals (Reporting Services)

After you format the report fields, you add grouping and totals to your [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] report to organize and summarize your data.  

In this tutorial, you:

> [!div class="checklist"]
> * Group data in a report to organize related information.
> * Add totals to summarize data at different levels.
> * Format grouped data and totals for better readability.

## Prerequisites

* Completion of [Step 1: Create a report server project](tutorial-step-01-create-report-server-project-reporting-services.md).
* Completion of [Step 2: Specify connection information](tutorial-step-02-specify-connection-information-reporting-services.md).
* Completion of [Step 3: Define a dataset for the table report](tutorial-step-03-define-dataset-table-report-reporting-services.md).
* Completion of [Step 4: Add a table to the report](tutorial-step-04-add-table-report-reporting-services.md).
* Completion of [Step 5: Format a report](tutorial-step-05-format-report-reporting-services.md).

## Group data in a report

You can group data in your SQL Server Reporting Services (SSRS) report. Grouping data helps organize related rows of data into sections, making your report easier to read and interpret.

1. Open your Sales Order report definition file (*.rdl*) in Visual Studio.

1. Select the **Design** tab.

1. If you don't see the **Row Groups** pane, right-click the design surface and select **View** > **Grouping**.

1. From the **Report Data** pane, drag the `[Date]` field to the **Row Groups** pane. Place it above the row displayed as **= (Details)**.

    > [!NOTE]
    > The row handle has a bracket in it, to indicate a group. The table also has two `[Date]` expression columns, one on both sides of a vertical dotted line.
    >
    >:::image type="content" source="media/rs-basictablegroups1design.png" alt-text="Screenshot of a table showing the row handle and two Date expression columns.":::

1. From the **Report Data** pane, drag the `[Order]` field to the **Row Groups** pane. Place it after **Date** and before **= (Details)**.

    :::image type="content" source="media/ssrs-ssdt-addorderfield.png" alt-text="Screenshot of the Order field in the Report data field between Date and Details.":::

    > [!NOTE]
    > Now the row handle has two brackets in it, :::image type="icon" source="media/ssrs-ssdt-rowgroupdoublehandles.png"::: to indicate two groups. The table has two `[Order]` expression columns.

1. Delete the original `[Date]` and `[Order]` expression columns to the right of the double line. Select the column handles for the two columns, right-click and choose **Delete Columns**. Report Designer removes the individual row expressions, so that only the group expressions are displayed.

    :::image type="content" source="media/rs-basictablegroupsdeletecols.gif" alt-text="Screenshot of the two columns selected in the table.":::

1. To format the new `[Date]` column, right-click the data region cell that contains the `[Date]` expression, and select **Text Box Properties**.
1. Select **Number** in the left-most box, and **Date** from the **Category** box.
1. In the **Type** box, select **January 31, 2000**.
1. Select **OK**.
1. Preview the report on the **Preview** tab.

    :::image type="content" source="media/rs-basictablegroupspreview.png" alt-text="Screenshot of the preview report for the table.":::

## Add totals to a report

You can add totals to your report to summarize data. Totals help you quickly understand aggregate values and enhance the report's usability.

1. Switch to the **Design** view.
1. Right-click the data region cell that contains the `[LineTotal]` expression, and select **Add Total**. Report Designer adds a row with a sum of the dollar amount for each order.
1. Right-click the cell that contains the field `[Qty]`, and select **Add Total**. Report Designer adds a sum of the quantity for each order to the totals row.
1. In the empty cell to the left of the `Sum[Qty]` cell, enter "Order Total".
1. Select the two sum cells and the label cell in the row where you added the total cells.  
1. Select **Format** > **Background Color** > **Light Gray**.
1. Select **OK**.

    :::image type="content" source="media/rs-basictablesumlinetotaldesign.gif" alt-text="Screenshot of the formatted table with the order total.":::

## Add the daily total to the report

You can add a daily total to your report. This step provides a daily summary at the end of each date grouping and helps you quickly identify daily aggregates within your report.

1. Right-click the `[Order]`expression cell, and select **Add Total** > **After**. Report Designer adds a new row containing sums of the `[Qty]` and `[Linetotal]` values for each day, and the string "Total" to the bottom of the `[Order]`expression column.
1. Enter the word "Daily" before the word "Total" in the same cell, so it reads "Daily Total".
1. Select that cell and the two adjacent total cells to the right side and the empty cell in between them.
1. Select **Format** > **Background Color** > **Orange**.
1. Select **OK**.

    :::image type="content" source="media/rs-basictablesumdaytotaldesign.gif" alt-text="Screenshot of the formatted table with the daily total.":::

## Add the grand total to the report

You can add a grand total to your report to summarize all the data across the entire report. A grand total provides a comprehensive summary and makes it easier to understand the overall data at a glance.

1. Right-click the `[Date]` expression cell, and select **Add Total** > **After**. Report Designer adds a new row containing sums of the `[Qty]` and `[LineTotal]` values for the entire report, and the string "Total" to the bottom of the `[Date]` expression column.
1. Enter the string "Grand" before the word "Total" in the same cell, so it reads "Grand Total".
1. Select the cell with "Grand Total", the two `Sum()` expression cells and the empty cells between them.
1. Select **Format** > **Background Color** > **Light Blue**.
1. Select **OK**.

    :::image type="content" source="media/rs-basictablesumgrandtotaldesign.gif" alt-text="Screenshot of the formatted table with grand total.":::

## Preview the report

Switch to the **Preview** tab so you can view the report as it appears when published. Look for any errors or issues you can fix before finalizing the report.

1. Select the **Preview** tab. 

1. In the **Preview** toolbar, choose **Last Page**, which looks like :::image type="icon" source="media/ssrs-ssdt-viewertoolbar-lastpage.png":::. The **Grand Total** values display at the end of the report.

   :::image type="content" source="./media/rs-basictablesumgrandtotalpreview.gif" alt-text="Screenshot of the full preview for the formatted table.":::

## Publish the report to the Report Server (optional)

Publish your completed report to the Report Server. This process is optional and involves configuring the report properties, setting the target server URL, and deploying the report. This step is crucial for making your report available to a broader audience through the web portal and ensuring the information is easily accessible.

1. Select **Project** > **Tutorial Properties...**.
2. In the **TargetServerURL**, enter the name of your report server, for example:
    - `http:/<servername>/reportserver` or
    - `http://localhost/reportserver` works if you're designing the report on the report server.

3. The **TargetReportFolder** is named Tutorial from the name of the project. Report Designer deploys the report to this folder.
4. Select **OK**.
5. Select **Build** > **Deploy Tutorial**.

    If you see a message similar the following **Output** window, it indicates a successful deployment.
    
    ```
    ------ Build started: Project: tutorial, Configuration: Debug ------  
    Skipping 'Sales Orders.rdl'. Item is up to date.  
    Build complete -- 0 errors, 0 warnings  
    ------ Deploy started: Project: tutorial, Configuration: Debug ------  
    Deploying to `https://[server name]/reportserver`  
    Deploying report '/tutorial/Sales Orders'.  
    Deploy complete -- 0 errors, 0 warnings  
    ========== Build: 1 succeeded or up-to-date, 0 failed, 0 skipped ==========  
    ========== Deploy: 1 succeeded, 0 failed, 0 skipped ========== 
    ``` 

    If you see an error message, verify you have the appropriate permissions on the report server and you started [!INCLUDE[ssBIDevStudio](../includes/ssbidevstudio-md.md)] with administrator privileges.

6. Open a browser with administrator privileges. For example, right-click the icon for Internet Explorer and select **Run as administrator**.
7. Browse to the web portal URL.
   - `https://<server name>/reports`.
   - `http://localhost/reports` works if you're designing the report on the report server.

8. Select the **Tutorial** folder, and then open the **Sales Orders** report.

    :::image type="content" source="media/ssrs-tutorial-tutorialfolder.png" alt-text="Screenshot of the Tutorial folder in the Home panel.":::

You successfully completed the **Creating a Basic Table Report tutorial**.

## Related content

- [Filter, group, and sort data &#40;Report Builder and SSRS&#41;](report-design/filter-group-and-sort-data-report-builder-and-ssrs.md)
