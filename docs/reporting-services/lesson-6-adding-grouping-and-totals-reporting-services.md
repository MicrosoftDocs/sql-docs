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

In this tutorial lesson, you will add grouping and totals to your [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] report to organize and summarize your data.  

## <a name="bkmk_groupdata"></a>To group data in a report

1. Select the **Design** tab.

2. If you do not see the **Row Groups** pane, right-click the design surface and select **View** >**Grouping**.

3. From the **Report Data** pane, drag the **Date** field to the **Row Groups** pane. Place it above the row called **(Details)**.

    > [!NOTE]
    > Note that the row handle now has a bracket in it, to show a group. The table now also has two date columns, one on both sides of a vertical dotted line.
    >
    >![date group added](media/rs-basictablegroups1design.png "date group added")

4. From the **Report Data** pane, drag the **Order** field to the **Row Groups** pane. Place it below **Date** and above **(Details)**.

    ![ssrs_ssdt_addorderfield](media/ssrs-ssdt-addorderfield.png)

    > [!NOTE]
    > Note that the row handle now has two brackets in it, ![ssrs_ssdt_rowgroupdoublehandles](media/ssrs-ssdt-rowgroupdoublehandles.png) to show two groups. The table now has two **Order** columns also.

5. Delete the original **Date** and **Order** columns to the **right** of the double line. This removes the individual record values so that only the group value is displayed. Select the column handles for the two columns, right-click and select **Delete Columns**.

    ![Select columns to delete](media/rs-basictablegroupsdeletecols.gif "Select columns to delete")

6. To format the new date column, right-click the cell with the `[Date]` field expression and then select **Text Box Properties**.

7. Select **Number**, and then in the **Category** field, select **Date**.

8. In the **Type** box, select **January 31, 2000**.

9. Select the **OK** button.

10. Switch to the **Preview** tab to preview the report. It should look similar to the following illustration:

    ![rs_BasicTableGroupsPreview](media/rs-basictablegroupspreview.png)

## <a name="bkmk_addtotals"></a>Adding totals to a report

1. Switch to *Design* view.

2. Right-click the data region cell that contains the field `[LineTotal]`, and select **Add Total**. This adds a row with a sum of the dollar amount for each order.

3. Right-click the cell that contains the field `[Qty]`, and select **Add Total**. This adds a sum of the quantity for each order to the totals row.

4. In the empty cell to the left of `Sum[Qty]`, type the label **"Order Total"**.

5. You can add a background color to the totals row. Select the two sum cells and the label cell.  

6. On the **Format** menu, select **Background Color** > **Light Gray**.

7. Select the **OK** button.

   ![Design view: Basic table with order total](media/rs-basictablesumlinetotaldesign.gif "Design view: Basic table with order total")

## <a name="bkmk_adddailytotal"></a>Add the daily total to the report

1. Right-click the **Order** cell, select **Add Total** > **After**. This adds a new row containing sums of the quantity and dollar amount for each day, and the label "**Total**" to the bottom of the Order column.  

2. Type the word **Daily** before the word **Total** in the same cell, so it reads **Daily Total**.  

3. Select the **Daily Total** cell, the two **Sum** cells and the empty cell between them.  

4. On the **Format** menu, click **Background Color**, click **Orange**, and click **OK**.  

   ![Set background color to Orange](media/rs-basictablesumdaytotaldesign.gif "rs_BasicTableSumDayTotalDesign")

## <a name="bkmk_addgrandtotal"></a>Add the grand total to the report

1. Right-click the Date cell, point to **Add Total**, and click **After**. This adds a new row containing sums of the quantity and dollar amount for the entire report, and the **Total** label in the **Date** column.

2. Type the word **Grand** before the word **Total** in the same cell, so it reads **Grand Total**.

3. Select the **Grand Total** cell, the two **Sum** cells and the empty cells between them.

4. On the **Format** menu, click **Background Color**, select **Light Blue**, and select the **OK** button.

    ![Design view: Grand total in basic table](media/rs-basictablesumgrandtotaldesign.gif "Design view: Grand total in basic table")

5. Select the **Preview** tab. The last page should look similar to the following image. In the toolbar, select the **Last Page**
   ![ssrs_ssdt_viewertoolbar_lastpage](media/ssrs-ssdt-viewertoolbar-lastpage.png) button.

   ![Preview: Basic table with grand total](media/rs-basictablesumgrandtotalpreview.gif "Preview: Basic table with grand total")

## <a name="bkmk_publishreport"></a>Publishing the report to the *Report Server* (Optional)

1. An optional step is to publish the completed report to the *Report Server* so you can view the report in the web portal.

2. Select the **Project** menu > **Tutorial Properties...**

3. In the **TargetServerURL**, type the name of your report server, for example:
    - `http:/<servername>/reportserver`  

    - `https://localhost/reportserver` works if your designing the report on the report server.  

4. Note the **TargetReportFolder** is *Tutorial*, the name of the project. This is the name of the folder that the report will deploy to in the next step.

5. Click the **OK** button.

6. Select the **Build** menu > **Deploy tutorial**.

    If you see a message similar to the following in the output window, it indicates a successful deployment.

    > ------ Build started: Project: tutorial, Configuration: Debug ------
    > Skipping 'Sales Orders.rdl'. Item is up to date.
    > Build complete -- 0 errors, 0 warnings
    > ------ Deploy started: Project: tutorial, Configuration: Debug ------
    > Deploying to https://[server name]/reportserver
    > Deploying report '/tutorial/Sales Orders'.
    > Deploy complete -- 0 errors, 0 warnings
    > ========== Build: 1 succeeded or up-to-date, 0 failed, 0 skipped ==========
    > ========== Deploy: 1 succeeded, 0 failed, 0 skipped ==========

    If you see an error message similar to the following, verify you have permissions on the report server and you have started [!INCLUDE[ssBIDevStudio](../includes/ssbidevstudio-md.md)] with administrator privileges.

    > "The permissions granted to user 'XXXXXXXX\\[your user name]' are insufficient for performing this operation"

7. Browse to the web portal with administrator privileges, for example, right-click the icon for Internet Explorer and select **Run as administrator**.

    Browse to [!INCLUDE[ssRSnoversion_md](../includes/ssrsnoversion-md.md)] web portal URL.
    **Note:** The *portal* URL is "Reports", not the Report *Server* URL of "Reportserver".  For example:
    `https://<server name>/reports`.
    `https://localhost/reports` works if you're designing the report on the report server.

8. Browse to the folder that contains the report. The default name is *Tutorial*, the name of the project or the name you typed into the TargetReportFolder field in the project properties. Select the name of the report **Sales Orders** to view the rendered report in the browser.  

    ![ssrs_tutorial_tutorialfolder](media/ssrs-tutorial-tutorialfolder.png)  

You have successfully completed the **Creating a Basic Table Report tutorial**.

## See also

[Filter, Group, and Sort Data &#40;Report Builder and SSRS&#41;](report-design/filter-group-and-sort-data-report-builder-and-ssrs.md)
