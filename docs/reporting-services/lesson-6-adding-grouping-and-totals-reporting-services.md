---
title: "Lesson 6: Adding Grouping and Totals (Reporting Services) | Microsoft Docs"
ms.date: 04/18/2019
ms.prod: reporting-services
ms.prod_service: "reporting-services-native"
ms.technology: reporting-services

ms.topic: conceptual
ms.assetid: e3d61228-2aa4-42cc-955e-602dbf3406a7
author: maggiesMSFT
ms.author: maggies
---

# Lesson 6: Adding Grouping and Totals (Reporting Services)

In the final tutorial lesson, you're going to add grouping and totals to your [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] report to organize and summarize your data.  

## <a name="bkmk_groupdata"></a>To group data in a report

1. Select the **Design tab**.
2. If you don't see the **Row Groups pane**, right-click the design surface and select **View** >**Grouping**.
3. From the **Report Data pane**, drag the `[Date]` field to the **Row Groups pane**. Place it above the row displayed as **= (Details)**.

    > [!NOTE]
    > Note that the row handle now has a bracket in it, to indicate a group. The table now also has two **Date columns**, one on both sides of a vertical dotted line.
    >
    >![date group added](media/rs-basictablegroups1design.png "date group added")
4. From the **Report Data pane**, drag the `[Order]` field to the **Row Groups pane**. Place it below **Date** and above **= (Details)**.

    ![ssrs_ssdt_addorderfield](media/ssrs-ssdt-addorderfield.png)

    > [!NOTE]
    > Note that the row handle now has two brackets in it, ![ssrs_ssdt_rowgroupdoublehandles](media/ssrs-ssdt-rowgroupdoublehandles.png) to show two groups. The table now also has two **Order columns**.

5. Delete the original **Date column** and **Order column** to the right of the double line. Select the column handles for the two columns, right-click and select **Delete Columns**. Report Designer removes the individual record values so that only the group values are displayed.

    ![Select columns to delete](media/rs-basictablegroupsdeletecols.gif "Select columns to delete")

6. To format the new **Date column**, right-click the cell with the `[Date]` field expression and then select **Text Box Properties**.
7. Select **Number** in the first column list box, and **Date** from the **Category List box**.
8. In the **Type list box**, select **January 31, 2000**.
9. Select the **OK button** to apply the format.
10. Again, preview the report. It should look as below:

    ![rs_BasicTableGroupsPreview](media/rs-basictablegroupspreview.png)

## <a name="bkmk_addtotals"></a>Adding totals to a report

1. Switch to **Design view**.
2. Right-click the data region cell that contains the field `[LineTotal]`, and select **Add Total**. Report Designer adds a row with a sum of the dollar amount for each order.
3. Right-click the cell that contains the field `[Qty]`, and select **Add Total**. Report Designer adds a sum of the quantity for each order to the totals row.
4. In the empty cell to the left of `Sum[Qty]`, type the label **"Order Total"**.
5. You can add a background color to the totals row. Select the two sum cells and the label cell.  
6. From the **Format menu**, select **Background Color** > **Light Gray square**.
7. Select the **OK button** to apply the format.

   ![Design view: Basic table with order total](media/rs-basictablesumlinetotaldesign.gif "Design view: Basic table with order total")

## <a name="bkmk_adddailytotal"></a>Add the daily total to the report

1. Right-click the **Order expression cell**, select **Add Total** > **After**. Report Designer adds a new row containing sums of the `[Qty]` and `[Linetotal]` amounts for each day, and the label "Total" to the bottom of the **Order column**.
2. Type the word "Daily" before the word "Total" in the same cell, so it reads "Daily Total".
3. Select that cell and the two adjacent total cells to the right and the empty cell in between them.
4. From the **Format menu**, select **Background Color** > **Orange square**.
5. Select the **OK button** to apply the format.

   ![Set background color to Orange](media/rs-basictablesumdaytotaldesign.gif "rs_BasicTableSumDayTotalDesign")

## <a name="bkmk_addgrandtotal"></a>Add the grand total to the report

1. Right-click the **Date expression cell** > **Add Total** > **After**. Report Designer adds a new row containing sums of the quantity and dollar amount for the entire report, and the **Total label** in the **Date column**.
2. Type the word **Grand** before the word **Total** in the same cell, so it reads **Grand Total**.
3. Select the **Grand Total** cell, the two **Sum** cells and the empty cells between them.
4. On the **Format** menu, click **Background Color**, select **Light Blue square**.
5. Select the **OK button** to apply the format.

    ![Design view: Grand total in basic table](media/rs-basictablesumgrandtotaldesign.gif "Design view: Grand total in basic table")

To preview the format changes, select the **Preview tab**. In the **Preview toolbar**, select the **Last Page button**.
   ![ssrs_ssdt_viewertoolbar_lastpage](media/ssrs-ssdt-viewertoolbar-lastpage.png)
The results should display as below:

   ![Preview: Basic table with grand total](media/rs-basictablesumgrandtotalpreview.gif "Preview: Basic table with grand total")

## <a name="bkmk_publishreport"></a>Publishing the report to the *Report Server* (Optional)

An optional step is to publish the completed report to the Report Server so you can view the report in the web portal.

1. Select the **Project menu** > **Tutorial Properties...**
2. In the **TargetServerURL**, type the name of your report server, for example:
    - `http:/<servername>/reportserver` or
    - `https://localhost/reportserver` works if you're designing the report on the report server.

3. The **TargetReportFolder** is named Tutorial from the name of the project. The report is deployed to this folder in the next step.
4. Select the **OK button**.
5. Select **Build menu** > **Deploy Tutorial**.

    If you see a message like below in the **Output window**, it indicates a successful deployment.

    > ------ Build started: Project: tutorial, Configuration: Debug ------
    > Skipping 'Sales Orders.rdl'. Item is up to date.
    > Build complete -- 0 errors, 0 warnings
    > ------ Deploy started: Project: tutorial, Configuration: Debug ------
    > Deploying to `https://[server name]/reportserver`
    > Deploying report '/tutorial/Sales Orders'.
    > Deploy complete -- 0 errors, 0 warnings
    > ========== Build: 1 succeeded or up-to-date, 0 failed, 0 skipped ==========
    > ========== Deploy: 1 succeeded, 0 failed, 0 skipped ==========
    >
    If you see an error message similar to below, verify you've the appropriate permissions on the report server and you've started [!INCLUDE[ssBIDevStudio](../includes/ssbidevstudio-md.md)] with administrator privileges.
    >
    > "The permissions granted to user 'XXXXXXXX\\[your user name]' are insufficient for performing this operation"

6. Open a browser with administrator privileges. For example, right-click the icon for Internet Explorer and select **Run as administrator**.
7. Browse to the web portal URL.
   - `https://<server name>/reports`.
   - `https://localhost/reports` works if you're designing the report on the report server.

8. Browse to the folder named Tutorial, and select the **Sales Orders report** to view the report rendered in the browser.  

    ![ssrs_tutorial_tutorialfolder](media/ssrs-tutorial-tutorialfolder.png)  

You've successfully completed the **Creating a Basic Table Report tutorial**.

## See also

[Filter, Group, and Sort Data &#40;Report Builder and SSRS&#41;](report-design/filter-group-and-sort-data-report-builder-and-ssrs.md)
