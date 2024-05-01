---
title: "Lesson 6: Add grouping and totals (Reporting Services)"
description: Learn how to add grouping and totals to your Reporting Services report to organize and summarize your data.
author: maggiesMSFT
ms.author: maggies
ms.date: 04/18/2019
ms.service: reporting-services
ms.subservice: reporting-services
ms.topic: conceptual
ms.custom: updatefrequency5
---

# Lesson 6: Add grouping and totals (Reporting Services)

In the final tutorial lesson, you're going to add grouping and totals to your [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] report to organize and summarize your data.  

## Group data in a report

1. Select the **Design** tab.
2. If you don't see the **Row Groups** pane, right-click the design surface and select **View** >**Grouping**.
3. From the **Report Data** pane, drag the `[Date]` field to the **Row Groups** pane. Place it above the row displayed as **= (Details)**.

    > [!NOTE]
    > The row handle now has a bracket in it, to indicate a group. The table now also has two `[Date]` expression columns, one on both sides of a vertical dotted line.
    >
    >:::image type="content" source="media/rs-basictablegroups1design.png" alt-text="Screenshot of a table showing the row handle and two Date expression columns.":::

4. From the **Report Data** pane, drag the `[Order]` field to the **Row Groups** pane. Place it after **Date** and before **= (Details)**.

    :::image type="content" source="media/ssrs-ssdt-addorderfield.png" alt-text="Screenshot of the Order field in the Report data field between Date and Details.":::

    > [!NOTE]
    > Now the row handle has two brackets in it, :::image type="icon" source="media/ssrs-ssdt-rowgroupdoublehandles.png"::: to indicate two groups. The table now also has two `[Order]` expression columns.

5. Delete the original `[Date]` and `[Order]` expression columns to the right of the double line. Select the column handles for the two columns, right-click and choose **Delete Columns**. Report Designer removes the individual row expressions, so that only the group expressions are displayed.

    :::image type="content" source="media/rs-basictablegroupsdeletecols.gif" alt-text="Screenshot of the two columns selected in the table.":::

6. To format the new `[Date]` column, right-click the data region cell that contains the `[Date]` expression, and select **Text Box Properties**.
7. Select **Number** in the left-most column list box, and **Date** from the **Category** list box.
8. In the **Type** list box, select **January 31, 2000**.
9. Select **OK** to apply the format.
10. Again, preview the report. It should look like the following image:

    :::image type="content" source="media/rs-basictablegroupspreview.png" alt-text="Screenshot of the preview report for the table.":::

## Add totals to a report

1. Switch to the **Design** view.
2. Right-click the data region cell that contains the `[LineTotal]` expression, and select **Add Total**. Report Designer adds a row with a sum of the dollar amount for each order.
3. Right-click the cell that contains the field `[Qty]`, and select **Add Total**. Report Designer adds a sum of the quantity for each order to the totals row.
4. In the empty cell to the left of the `Sum[Qty]` cell, enter the string "Order Total".
5. You can add a background color to the totals row. Select the two sum cells and the label cell.  
6. From the **Format** menu, select **Background Color** > **Light Gray** square.
7. Select **OK** to apply the format.

    :::image type="content" source="media/rs-basictablesumlinetotaldesign.gif" alt-text="Screenshot of the formatted table with the order total.":::

## Add the daily total to the report

1. Right-click the `[Order]`expression cell, and select **Add Total** > **After**. Report Designer adds a new row containing sums of the `[Qty]` and `[Linetotal]` values for each day, and the string "Total" to the bottom of the `[Order]`expression column.
2. Enter the word "Daily" before the word "Total" in the same cell, so it reads "Daily Total".
3. Select that cell and the two adjacent total cells to the right side and the empty cell in between them.
4. From the **Format** menu, select **Background Color** > **Orange** square.
5. Select **OK** to apply the format.

    :::image type="content" source="media/rs-basictablesumdaytotaldesign.gif" alt-text="Screenshot of the formatted table with the daily total.":::

## Add the grand total to the report

1. Right-click the `[Date]` expression cell, and select **Add Total** > **After**. Report Designer adds a new row containing sums of the `[Qty]` and `[LineTotal]` values for the entire report, and the string "Total" to the bottom of the `[Date]` expression column.
2. Enter the string "Grand" before the word "Total" in the same cell, so it reads "Grand Total".
3. Select the cell with "Grand Total", the two `Sum()` expression cells and the empty cells between them.
4. From the **Format** menu, select **Background Color** > **Light Blue** square.
5. Select **OK** to apply the format.

    :::image type="content" source="media/rs-basictablesumgrandtotaldesign.gif" alt-text="Screenshot of the formatted table with grand total.":::

## Preview the report

To preview the format changes, select the **Preview** tab. In the **Preview** toolbar, choose the **Last Page** button, which looks like :::image type="icon" source="media/ssrs-ssdt-viewertoolbar-lastpage.png"::: The results should display as shown in the following image:

:::image type="content" source="./media/rs-basictablesumgrandtotalpreview.gif" alt-text="Screenshot of the full preview for the formatted table.":::

## Publish the report to the *Report Server* (Optional)

An optional step is to publish the completed report to the Report Server so you can view the report in the web portal.

1. Select **Project** menu > **Tutorial Properties...**
2. In the **TargetServerURL**, enter the name of your report server, for example:
    - `http:/<servername>/reportserver` or
    - `https://localhost/reportserver` works if you're designing the report on the report server.

3. The **TargetReportFolder** is named Tutorial from the name of the project. Report Designer deploys the report to this folder.
4. Select **OK**.
5. Select **Build** menu > **Deploy Tutorial**.

    If you see something like the message in the following **Output** window, it indicates a successful deployment.

    > ------ Build started: Project: tutorial, Configuration: Debug ------  
    > Skipping 'Sales Orders.rdl'. Item is up to date.  
    > Build complete -- 0 errors, 0 warnings  
    > ------ Deploy started: Project: tutorial, Configuration: Debug ------  
    > Deploying to `https://[server name]/reportserver`  
    > Deploying report '/tutorial/Sales Orders'.  
    > Deploy complete -- 0 errors, 0 warnings  
    > ========== Build: 1 succeeded or up-to-date, 0 failed, 0 skipped ==========  
    > ========== Deploy: 1 succeeded, 0 failed, 0 skipped ==========  

    If you see something similar to the following error message, verify you have the appropriate permissions on the report server and you started [!INCLUDE[ssBIDevStudio](../includes/ssbidevstudio-md.md)] with administrator privileges.
    >
    > "The permissions granted to user 'XXXXXXXX\\[your user name]' are insufficient for performing this operation"

6. Open a browser with administrator privileges. For example, right-click the icon for Internet Explorer and select **Run as administrator**.
7. Browse to the web portal URL.
   - `https://<server name>/reports`.
   - `https://localhost/reports` works if you're designing the report on the report server.

8. Select the Tutorial folder, and then choose the "Sales Orders" report to view the report.

    :::image type="content" source="media/ssrs-tutorial-tutorialfolder.png" alt-text="Screenshot of the Tutorial folder in the Home panel.":::

You successfully completed the **Creating a Basic Table Report tutorial**.

## Related content

- [Filter, group, and sort data &#40;Report Builder and SSRS&#41;](report-design/filter-group-and-sort-data-report-builder-and-ssrs.md)
