---
title: "Tutorial: Introduce expressions"
description: Learn to use expressions with common functions and operators to create powerful and flexible Reporting Services paginated reports.
author: maggiesMSFT
ms.author: maggies
ms.date: 09/25/2024
ms.service: reporting-services
ms.subservice: reporting-services
ms.topic: conceptual
ms.custom:
  - updatefrequency5
---
# Tutorial: Introduce expressions
In this [!INCLUDE[ssRBnoversion_md](../includes/ssrbnoversion.md)] tutorial, you use expressions with common functions and operators to create powerful and flexible [!INCLUDE[ssRSnoversion_md](../includes/ssrsnoversion-md.md)] paginated reports. 

In this tutorial, you write expressions that concatenate name values, look up values in a separate dataset, display different colors based on field values, and so on.  
  
The report is a banded report with alternating rows in white and a color. The report includes a parameter for selecting the color of the nonwhite rows.  
  
This illustration shows a report similar to the one you create in this tutorial.  

:::image type="content" source="../reporting-services/media/report-builder-expression-tutorial-in-browser.png" alt-text="Screenshot of a Report Builder report that uses expressions.":::

Estimated time to complete this tutorial: 30 minutes.  
  
## Requirements  
For information about requirements, see [Prerequisites for tutorials &#40;Report Builder&#41;](../reporting-services/prerequisites-for-tutorials-report-builder.md).  
  
## <a name="Setup"></a>1. Create a table report and dataset from the table or Matrix Wizard  
In this section, you create a table report, a data source, and a dataset. When you lay out the table, you include only a few fields. After you complete the wizard, you manually add columns. The wizard makes it easy to lay out the table.  
  
> [!NOTE]  
> In this tutorial, the query contains the data values, so it does not need an external data source. This makes the query quite long. In a business environment, a query would not contain the data. This is for learning purposes only.  
  
### Create a table report  
  
1.  [Start Report Builder](../reporting-services/report-builder/start-report-builder.md) either from your computer, the [!INCLUDE[ssRSnoversion_md](../includes/ssrsnoversion-md.md)] web portal, or SharePoint integrated mode.  
  
    The **New Report or Dataset** dialog box opens.  
  
    If you don't see the **New Report or Dataset** dialog box, on the **File** menu > **New**.  
  
2.  In the left pane, verify that **New Report** is selected.  
  
3.  In the right pane, select **Table or Matrix Wizard**.  
  
4.  On the **Choose a dataset** page, select **Create a dataset** > **Next**.  
  
6.  On the **Choose a connection to a data source** page, select a data source that is type **SQL Server**. Choose a data source from the list or browse to the report server to select one.  

    > [!NOTE]  
    > The data source you choose isn't important, as long as you have adequate permissions. You will not be getting data from the data source. For more information, see [Alternative ways to get a data connection &#40;Report Builder&#41;](../reporting-services/alternative-ways-to-get-a-data-connection-report-builder.md).  
  
7.  Select **Next**.  
  
8.  On the **Design a query** page, select **Edit as Text**.  
  
9. Paste the following query into the query pane:  
  
    ```  
    SELECT 'Lauren' AS FirstName,'Johnson' AS LastName, 'American Samoa' AS StateProvince, 1 AS CountryRegionID,'Female' AS Gender, CAST(9996.60 AS money) AS YTDPurchase, CAST('2015-6-10' AS date) AS LastPurchase  
    UNION SELECT'Warren' AS FirstName, 'Pal' AS LastName, 'New South Wales' AS StateProvince, 2 AS CountryRegionID, 'Male' AS Gender, CAST(5747.25 AS money) AS YTDPurchase, CAST('2015-7-3' AS date) AS LastPurchase  
    UNION SELECT 'Fernando' AS FirstName, 'Ross' AS LastName, 'Alberta' AS StateProvince, 3 AS CountryRegionID, 'Male' AS Gender, CAST(9248.15 AS money) AS YTDPurchase, CAST('2015-10-17' AS date) AS LastPurchase  
    UNION SELECT 'Rob' AS FirstName, 'Caron' AS LastName, 'Northwest Territories' AS StateProvince, 3 AS CountryRegionID, 'Male' AS Gender, CAST(742.50 AS money) AS YTDPurchase, CAST('2015-4-29' AS date) AS LastPurchase  
    UNION SELECT 'James' AS FirstName, 'Bailey' AS LastName, 'British Columbia' AS StateProvince, 3 AS CountryRegionID, 'Male' AS Gender, CAST(1147.50 AS money) AS YTDPurchase, CAST('2015-6-15' AS date) AS LastPurchase  
    UNION SELECT  'Bridget' AS FirstName, 'She' AS LastName, 'Hamburg' AS StateProvince, 4 AS CountryRegionID, 'Female' AS Gender, CAST(7497.30 AS money) AS YTDPurchase, CAST('2015-5-10' AS date) AS LastPurchase  
    UNION SELECT 'Alexander' AS FirstName, 'Martin' AS LastName, 'Saxony' AS StateProvince, 4 AS CountryRegionID, 'Male' AS Gender, CAST(2997.60 AS money) AS YTDPurchase, CAST('2015-11-19' AS date) AS LastPurchase  
    UNION SELECT 'Yolanda' AS FirstName, 'Sharma' AS LastName ,'Micronesia' AS StateProvince, 5 AS CountryRegionID, 'Female' AS Gender, CAST(3247.95 AS money) AS YTDPurchase, CAST('2015-8-23' AS date) AS LastPurchase  
    UNION SELECT 'Marc' AS FirstName, 'Zimmerman' AS LastName, 'Moselle' AS StateProvince, 6 AS CountryRegionID, 'Male' AS Gender, CAST(1200.00 AS money) AS YTDPurchase, CAST('2015-11-16' AS date) AS LastPurchase  
    UNION SELECT 'Katherine' AS FirstName, 'Abel' AS LastName, 'Moselle' AS StateProvince, 6 AS CountryRegionID, 'Female' AS Gender, CAST(2025.00 AS money) AS YTDPurchase, CAST('2015-12-1' AS date) AS LastPurchase  
    UNION SELECT 'Nicolas' as FirstName, 'Anand' AS LastName, 'Seine (Paris)' AS StateProvince, 6 AS CountryRegionID, 'Male' AS Gender, CAST(1425.00 AS money) AS YTDPurchase, CAST('2015-12-11' AS date) AS LastPurchase  
    UNION SELECT 'James' AS FirstName, 'Peters' AS LastName, 'England' AS StateProvince, 12 AS CountryRegionID, 'Male' AS Gender, CAST(887.50 AS money) AS YTDPurchase, CAST('2015-8-15' AS date) AS LastPurchase  
    UNION SELECT 'Alison' AS FirstName, 'Nath' AS LastName, 'Alaska' AS StateProvince, 7 AS CountryRegionID, 'Female' AS Gender, CAST(607.50 AS money) AS YTDPurchase, CAST('2015-10-13' AS date) AS LastPurchase  
    UNION SELECT 'Grace' AS FirstName, 'Patterson' AS LastName, 'Kansas' AS StateProvince, 7 AS CountryRegionID, 'Female' AS Gender, CAST(1215.00 AS money) AS YTDPurchase, CAST('2015-10-18' AS date) AS LastPurchase  
    UNION SELECT 'Bobby' AS FirstName, 'Sanchez' AS LastName, 'North Dakota' AS StateProvince, 7 AS CountryRegionID, 'Female' AS Gender, CAST(6191.00 AS money) AS YTDPurchase, CAST('2015-9-17' AS date) AS LastPurchase  
    UNION SELECT 'Charles' AS FirstName, 'Reed' AS LastName, 'Nebraska' AS StateProvince, 7 AS CountryRegionID, 'Male' AS Gender, CAST(8772.00 AS money) AS YTDPurchase, CAST('2015-8-27' AS date) AS LastPurchase  
    UNION SELECT 'Orlando' AS FirstName, 'Romeo' AS LastName, 'Texas' AS StateProvince, 7 AS CountryRegionID, 'Male' AS Gender, CAST(8578.00 AS money) AS YTDPurchase, CAST('2015-7-29' AS date) AS LastPurchase  
    UNION SELECT 'Cynthia' AS FirstName, 'Randall' AS LastName, 'Utah' AS StateProvince, 7 AS CountryRegionID, 'Female' AS Gender, CAST(7218.10 AS money) AS YTDPurchase, CAST('2015-1-11' AS date) AS LastPurchase  
    UNION SELECT 'Rebecca' AS FirstName, 'Roberts' AS LastName, 'Washington' AS StateProvince, 7 AS CountryRegionID, 'Female' AS Gender, CAST(8357.80 AS money) AS YTDPurchase, CAST('2015-10-28' AS date) AS LastPurchase  
    UNION SELECT 'Cristian' AS FirstName, 'Petulescu' AS LastName, 'Wisconsin' AS StateProvince, 7 AS CountryRegionID, 'Male' AS Gender, CAST(3470.00 AS money) AS YTDPurchase, CAST('2015-11-30' AS date) AS LastPurchase  
    UNION SELECT 'Cynthia' AS FirstName, 'Randall' AS LastName, 'Utah' AS StateProvince, 7 AS CountryRegionID, 'Female' AS Gender, CAST(7218.10 AS money) AS YTDPurchase, CAST('2015-1-11' AS date) AS LastPurchase  
    UNION SELECT 'Rebecca' AS FirstName, 'Roberts' AS LastName, 'Washington' AS StateProvince, 7 AS CountryRegionID, 'Female' AS Gender, CAST(8357.80 AS money) AS YTDPurchase, CAST('2015-10-28' AS date) AS LastPurchase  
    UNION SELECT 'Cristian' AS FirstName, 'Petulescu' AS LastName, 'Wisconsin' AS StateProvince, 7 AS CountryRegionID, 'Male' AS Gender, CAST(3470.00 AS money) AS YTDPurchase, CAST('2015-11-30' AS date) AS LastPurchase  
    ```  

  
10. On the query designer toolbar, select **Run** (**!**). The result set displays 23 rows of data in the following columns: FirstName, LastName, StateProvince, CountryRegionID, Gender, YTDPurchase, and LastPurchase.  

    :::image type="content" source="../reporting-services/media/report-builder-expression-tutorial-query-as-text.png" alt-text="Screenshot of the Design a query step of the New Table or Matrix wizard.":::
  
11. Select **Next**.  
  
12. On the **Arrange fields** page, drag the following fields, in the specified order, from the **Available Fields** list to the **Values** list.  
  
    -   StateProvince   
    -   CountryRegionID  
    -   LastPurchase  
    -   YTDPurchase  
  
    Because the CountryRegionID and YTDPurchase contain numeric data, the SUM aggregate is applied to them by default, but you don't want them to be sums.  
   
13. In the **Values** list, right-click **CountryRegionID** and clear the **Sum** check box.  
  
    Sum is no longer applied to CountryRegionID.  
  
14. In the **Values** list, right-click **YTDPurchase** and select the **Sum** option.  
  
    Sum is no longer applied to YTDPurchase.  

    :::image type="content" source="../reporting-services/media/report-builder-expression-not-sum.png" alt-text="Screenshot of the Values list that shows the Sum option ready to be cleared.":::
  
15. select **Next**.  
  
16. On the **Choose the layout** page, keep all the default settings and select **Next**.  

    :::image type="content" source="../reporting-services/media/report-builder-expression-tutorial-choose-layout.png" alt-text="Screenshot of the Choose the layout step of the New Table or Matrix wizard.":::
  
17. Select **Finish**.  
  
## <a name="UpdateNames"></a>2. Update default names of the data source and dataset  
  
### Update the default name of the data source  
  
1.  In the Report Data pane, expand the **Data Sources** folder.  
  
2.  Right-click **DataSource1** and select **Data Source Properties.**  
  
3.  In the **Name** box, enter **ExpressionsDataSource**  
  
4.  Select **OK**.
  
### Update the default name of the dataset  
  
1.  In the Report Data pane, expand the **Datasets** folder.  
  
2.  Right-click **DataSet1** and select **Dataset Properties**.  

    :::image type="content" source="../reporting-services/media/report-builder-expression-tutorial-rename-dataset.png" alt-text="Screenshot that shows how access the Dataset Properties in Report Builder.":::
  
3.  In the **Name** box, enter **Expressions**  
  
4.  Select **OK**.
  
## <a name="Concatenate"></a>3. Display first initial and last name  
In this section, you use the **Left** function and the **Concatenate** (**&**) operator in an expression that evaluates to a name that includes an initial and a last name. You can build the expression step by step or skip ahead in the procedure and copy/paste the expression from the tutorial into the **Expression** dialog box.   
  
1.  Right-click the **StateProvince** column, point to **Insert Column**, and then select **Left**.  
  
    A new column is added to the left of the **StateProvince** column. 

    :::image type="content" source="../reporting-services/media/report-builder-expression-tutorial-insert-column.png" alt-text="Screenshot that shows how to insert a left column into a report.":::
  
2.  Select the header of the new column and enter **Name**.  
  
3.  Right-click the data cell for the **Name** column and select **Expression**.  

    :::image type="content" source="../reporting-services/media/report-builder-expression-tutorial-insert-expression.png" alt-text="Screenshot that shows how to insert an expression into a report.":::
  
4.  In the **Expression** dialog box, expand **Common Functions**, and then select **Text**.  
  
5.  In the **Item** list, double-click **Left**.  
  
    The **Left** function is added to the expression.  

    :::image type="content" source="../reporting-services/media/report-builder-expression-tutorial-left-function.png" alt-text="Screenshot that shows how to add a Left function to an expression.":::
  
6.  In the **Category** list, select **Fields (Expressions)**.  
  
7.  In the **Values** list, double-click **FirstName**.  
  
8.  Enter **, 1)**  
  
    This expression extracts one character from the **FirstName** value, counting from the left.  
  
9. Enter **&". "&**  

    This expression adds a period and a space after the expression.
  
10. In the **Values** list, double-click **LastName**.  
  
    The completed expression is: `=Left(Fields!FirstName.Value, 1) &". "& Fields!LastName.Value`  

    :::image type="content" source="../reporting-services/media/report-builder-expression-tutorial-complete-name-expression.png" alt-text="Screenshot that shows how to add a LastName value to an expression.":::
  
11. Select **OK**.
  
12. Select **Run** to preview the report.  

## <a name="DateFormat"></a>(Optional) Format the date and currency columns and header row  
In this section, you format the **Last Purchase** column, which contains dates, and the YTDPurchase column, which contains currency. You also format the header row.  
  
### Format the date column  
  
1.  Select **Design** to return to design view.  
  
2.  Select the data cell in the **Last Purchase** column, and on the **Home** tab > **Number** section, choose **Date**.  

    :::image type="content" source="../reporting-services/media/report-builder-expression-tutorial-date-format.png" alt-text="Screenshot that shows how to set the Last Purchase column to Date.":::
  
3.  Also in the **Number** section, select the arrow next to **Placeholder Styles** and choose **Sample Values**. 

    :::image type="content" source="../reporting-services/media/report-builder-expression-tutorial-sample-values.png" alt-text="Screenshot that shows the Sample Values option in Report Builder.":::

    Now you can see an example of the formatting you selected. 
  
### Format the currency column

- Select the data cell in the **YTDPurchase** column, and in the **Number** section, choose **Currency Symbol**.
 
### Format the column headers

1. Select the row of column headers.

2. On the **Home** tab > **Paragraph** section, select **Left**. 

    :::image type="content" source="../reporting-services/media/report-builder-expression-tutorial-format-headings.png" alt-text="Screenshot that shows how to format headings in Report Builder.":::

3. Select **Run** to preview the report. 

Here's the report so far, with formatted dates, currency, and column headers.

:::image type="content" source="../reporting-services/media/report-builder-expression-tutorial-preview-formatted.png" alt-text="Screenshot that shows the preview of the formatted report.":::

  
## <a name="Gender"></a>4. Use color to display gender  
In this section, you add color to show the gender of a person. You add a new column to display the color, and then determine the color that appears in the column based on the value of the Gender field.  
  
If you want to keep the color you apply in that table cell when you make the report a banded report, you add a rectangle. Then you add the background color to the rectangle.  
    
 
### Add an M/F column  
  
1.  Right-click the **Name** column, point to **Insert Column**, and then select **Left**.  
  
    A new column is added to the left of the **Name** column.  
  
2.  Select the header of the new column and enter **M/F**.  
  
### Add a rectangle  
  
1.   On the **Insert** tab, select **Rectangle** and then choose in the data cell of the **M/F** column.  
  
     A rectangle is added to the cell.  

     :::image type="content" source="../reporting-services/media/report-builder-expression-tutorial-insert-rectangle.png" alt-text="Screenshot that shows how to insert a rectangle.":::
  
2. Drag the column divider between the **M/F** and the **Name** to make the **M/F** column narrower.

    :::image type="content" source="../reporting-services/media/report-builder-expression-tutorial-narrow-column.png" alt-text="Screenshot that shows how to make a column narrower.":::
  
### Use color to show gender  
  
1.  Right-click the rectangle in the data cell in the **M/F** column and select **Rectangle Properties**.  
  
2.  In the **Rectangle Properties** dialog box > **Fill** tab, select the expression **fx** button next to **Fill color**.  
  
3.  In the **Expression** dialog box, expand **Common Functions** and select **Program Flow**.  
  
4.  In the **Item** list, double-click **Switch**.  
  
5.  In the **Category** list, select **Fields (Expressions)**.  
  
6.  In the **Values** list, double-click **Gender**.  
  
7.  Enter **="Male",** (including the comma).

8. In the **Category** list, select **Constants**, and in the **Values** box, choose **Cornflower Blue**.

    :::image type="content" source="../reporting-services/media/report-builder-expression-tutorial-color-expression-cornflower-blue.png" alt-text="Screenshot that shows how to use a color to show a gender.":::

9. Enter a comma after it. 
  
5.  In the **Category** list, select **Fields (Expressions)**, and in the **Values** list, double-click **Gender** again.  
  
7.  Enter **="Female",** (including the comma). 

8. In the **Category** list, select **Constants**, and in the **Values** box, choose **Tomato**.

13. Enter a closing parenthesis **)** after it. 
  
    The completed expression is: 
    `=Switch(Fields!Gender.Value ="Male", "CornflowerBlue",Fields!Gender.Value ="Female","Tomato")`  

    :::image type="content" source="../reporting-services/media/report-builder-expression-tutorial-color-expression-complete.png" alt-text="Screenshot that shows the complete expression in the Expression dialog box.":::
  
12. Select **OK**, then choose **OK** again to close the **Rectangle Properties** dialog box.  
  
14. Select **Run** to preview the report.  

    :::image type="content" source="../reporting-services/media/report-builder-expression-tutorial-preview-m-f-column.png" alt-text="Screenshot that shows the preview with the M/F column.":::

### Format the color rectangles

1. Select **Design** to return to design view.  

16. Select the rectangle in the **M/F** column. In the Properties pane, in the Border section, set these properties:

    - BorderColor = White
    - BorderStyle = Solid
    - BorderWidth = 5pt

    :::image type="content" source="../reporting-services/media/report-builder-expression-tutorial-format-m-f-column.png" alt-text="Screenshot that shows how to format the color rectangles in the M/F column.":::

18. Select **Run** to preview the report again. This time the color blocks have white space around them.

    :::image type="content" source="../reporting-services/media/report-builder-expression-tutorial-preview-formatted-m-f-column.png" alt-text="Screenshot that shows the preview with the rectangles formatted in the M/F column.":::
  
## <a name="Lookup"></a>5. Look up the CountryRegion name  
In this section, you create the CountryRegion dataset and use the **Lookup** function to display the name of a country/region instead of the identifier of the country/region.  
  
### Create the CountryRegion dataset  
  
1.  Select **Design** to return to design view.  
  
2.  In the Report Data pane, select **New** and then choose **Dataset**.  
  
3.  In **Dataset Properties**, select **Use a dataset embedded in my report**.  
  
4.  In the **Data source** list, select ExpressionsDataSource.  
  
5.  In the **Name** box, enter **CountryRegion**  
  
6.  Verify that the **Text** query type is selected and select **Query Designer**.  
  
7.  Select **Edit as Text**.  
  
8.  Copy and paste the following query into the query pane:  
  
    ```  
    SELECT 1 AS ID, 'American Samoa' AS CountryRegion  
    UNION SELECT 2 AS CountryRegionID, 'Australia' AS CountryRegion  
    UNION SELECT 3 AS ID, 'Canada' AS CountryRegion  
    UNION SELECT 4 AS ID, 'Germany' AS CountryRegion  
    UNION SELECT 5 AS ID, 'Micronesia' AS CountryRegion  
    UNION SELECT 6 AS ID, 'France' AS CountryRegion  
    UNION SELECT 7 AS ID, 'United States' AS CountryRegion  
    UNION SELECT 8 AS ID, 'Brazil' AS CountryRegion  
    UNION SELECT 9 AS ID, 'Mexico' AS CountryRegion  
    UNION SELECT 10 AS ID, 'Japan' AS CountryRegion  
    UNION SELECT 10 AS ID, 'Australia' AS CountryRegion  
    UNION SELECT 12 AS ID, 'United Kingdom' AS CountryRegion  
    ```  
  
9. Select **Run** (**!**) to run the query.  
  
    The query results are the country/region identifiers and names.  
  
10. Select **OK**.
  
11. Select **OK** again to close the **Dataset Properties** dialog box.  

     Now you have a second dataset in the **Report Data** column.
  
### Look up values in the CountryRegion dataset  
  
1.  Select the **Country Region ID** column header and delete the text: **ID**, so it reads **Country Region**.  
  
2.  Right-click the data cell for the **Country Region** column and select **Expression**.  
  
3.  Delete the expression except the initial equal (=) sign.  
  
    The remaining expression is: `=`  
  
4.  In the **Expression** dialog box, expand **Common Functions** and select **Miscellaneous**, and in the **Item** list, double-click **Lookup**.  
  
6.  In the **Category** list, select **Fields (Expressions)**, and in the **Values** list, double-click **CountryRegionID**.  
  
8.  Place the cursor immediately after `CountryRegionID.Value`, and enter **,Fields!ID.value, Fields!CountryRegion.value, "CountryRegion")**.
  
    The completed expression: `=Lookup(Fields!CountryRegionID.Value,Fields!ID.value, Fields!CountryRegion.value, "CountryRegion")`  
  
    The syntax of the **Lookup** function specifies a lookup between CountryRegionID in the Expressions dataset and ID in the CountryRegion dataset that returns the CountryRegion value from the CountryRegion dataset.  
  
10. Select **OK**.
  
11. Select **Run** to preview the report.  
  
## <a name="Count"></a>6. Count days since last purchase  
In this section, you add a column and then use the **Now** function or the `ExecutionTime` built-in global variable to calculate the number of days from today since a customer's last purchases.  
  
### Add the Days Ago column  
  
1.  Select **Design** to return to design view.  
  
2.  Right-click the **Last Purchase** column, point to **Insert Column**, and then select **Right**.  
  
    A new column is added to the right of the **Last Purchase** column.  
  
3.  In the column header, enter **Days Ago**.
  
4.  Right-click the data cell for the **Days Ago** column and select **Expression**.  
  
5.  In the **Expression** dialog box, expand **Common Functions**, and then select **Date & Time**.  
  
6.  In the **Item** list, double-click **DateDiff**.  
  
7.  Immediately after `DateDiff(`, enter **"d",** (including the quotation marks "" and comma). 
  
9. In the **Category** list, select **Fields (Expressions)**, and in the **Values** list, double-click **LastPurchase**.  
  
11. Immediately after `Fields!LastPurchase.Value`, enter **,** (a comma).
  
13. In the **Category** list, select **Date & Time** again, and in the **Item** list, double-click **Now**.  
  
    > [!WARNING]  
    > In production reports you should not use the **Now** function in expressions that are evaluated multiple times as the report renders (for example, in the detail rows of a report). The value of **Now** changes from row to row and the different values affect the evaluation results of expressions, which leads to results that are subtly inconsistent. Instead, use the `ExecutionTime` global variable that [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] provides.  
  
15. Delete the left parenthesis after `Now(`, and then enter a right parenthesis **)**.
  
    The completed expression is: `=DateDiff("d", Fields!LastPurchase.Value, Now)`  

    :::image type="content" source="../reporting-services/media/report-builder-expression-tutorial-date-since-last-purchase.png" alt-text="Screenshot that shows the complete expression for the date since last purchase.":::
  
17. Select **OK**.

11. Select **Run** to preview the report.  
  
## <a name="Indicator"></a>7. Use an indicator to show sales comparison  
In this section, you add a new column and use an indicator to show whether a person's year-to-date (YTD) purchases are greater or less than the average YTD purchases. The **Round** function removes decimals from values.  
  
Configuring the indicator and its states takes many steps. If you want, you can skip ahead in the "To configure the indicator" procedure, and copy/paste the completed expressions from this tutorial into the **Expression** dialog box.  
  
### Add the + or - AVG Sales column  
  
1.  Right-click the **YTD Purchase** column, point to **Insert Column**, and then select **Right**.  
  
    A new column is added to the right of the **YTD Purchase** column.  
  
2.  Select the column header and enter **+ or - AVG Sales**. 
  
### Add an indicator  
  
1.  On the **Insert** tab, select **Indicator**, and then choose the data cell for the **+ or - AVG Sales** column.  
  
    The **Select Indicator Type** dialog box opens.  
  
2.  In the **Directional** group of icon sets, select the set of three gray arrows.  

    :::image type="content" source="../reporting-services/media/report-builder-expression-tutorial-select-indicator.png" alt-text="Screenshot that shows how to add an indicator.":::
  
3.  Select **OK**.
  
### Configure the indicator  
  
1.  Right-click the indicator, select **Indicator Properties**, and then choose **Value and States**.  
  
2.  Select the expression **fx** button next to the **Value** text box.  
  
3.  In the **Expression** dialog box, expand **Common Functions**, and then select **Math**.  
  
4.  In the **Item** list, double-click **Round**.  
  
5.  In the **Category** list, select **Fields (Expressions)**, and in the **Values** list, double-click **YTDPurchase**.  
  
7.  Immediately after `Fields!YTDPurchase.Value`, enter  **-** (a minus sign). 
  
9. Expand **Common Functions** again, select **Aggregate**, and in the **Item** list, double-click **Avg**.  
  
11. In the **Category** list, select **Fields (Expressions)**, and in the **Values** list, double-click **YTDPurchase**.  
  
13. Immediately after `Fields!YTDPurchase.Value`, enter **, "Expressions"))**.
  
    The completed expression is: `=Round(Fields!YTDPurchase.Value - Avg(Fields!YTDPurchase.Value, "Expressions"))`  
  
15. Select **OK**.
  
16. In the **States Measurement Unit** box, select **Numeric**.  
  
17. In the row with the down-pointing arrow, select the **fx** button to the right of the text box for the **Start** value.  

    :::image type="content" source="../reporting-services/media/report-builder-expression-tutorial-indicator-start.png" alt-text="Screenshot that shows how to select the fx button next to the Start text box.":::
  
18. In the **Expression** dialog box, expand **Common Functions**, and then select **Math**.  
  
19. In the **Item** list, double-click **Round**.  
  
20. In the **Category** list, select **Fields (Expressions)**, and in the **Values** list, double-click **YTDPurchase**.  
  
22. Immediately after `Fields!YTDPurchase.Value`, enter  **-** (a minus sign). 
  
24. Expand **Common Functions** again and select **Aggregate**, and in the **Item** list, double-click **Avg**.  
  
26. In the **Category** list, select **Fields (Expressions)**, and in the **Values** list, double-click **YTDPurchase**.  
  
28. Immediately after `Fields!YTDPurchase.Value`, enter **, "Expressions")) < 0**  
  
    The completed expression: `=Round(Fields!YTDPurchase.Value - Avg(Fields!YTDPurchase.Value, "Expressions")) < 0`  
  
30. Select **OK**.
  
31. In the text box for the **End** value, enter **0**.
  
32. Select the row with the horizontal-pointing arrow and choose **Delete**.  

    :::image type="content" source="../reporting-services/media/report-builder-expression-tutorial-delete-indicator-state.png" alt-text="Screenshot that shows how to delete an indicator.":::
    
    Now there are only two arrows, either up or down.
  
33. In the row with the up-pointing arrow, in the **Start** box, enter **0**.
  
34. Select the **fx** button to the right of the text box for the **End** value.  
  
35. In the **Expression** dialog box, delete **100** and create the expression: `=Round(Fields!YTDPurchase.Value - Avg(Fields!YTDPurchase.Value, "Expressions")) >0`  
  
36. Select **OK**.
  
37. Select **OK** again to close the **Indicator properties** dialog box.  
  
38. Select **Run** to preview the report.  

    :::image type="content" source="../reporting-services/media/report-builder-expression-tutorial-preview-indicator.png" alt-text="Screenshot that shows the preview with the + or - AVG Sales column including all of the new indicators.":::
  
## <a name="GreenBar"></a>8. Make a banded report  
Create a parameter so report readers can specify the color to apply to alternating rows in the report, making it a banded report.  
  
### Add a parameter  
  
1.  Select **Design** to return to design view.  
  
2.  In the **Report Data** pane, right-click **Parameters** and select **Add Parameter**.  

    :::image type="content" source="../reporting-services/media/report-builder-expression-tutorial-add-parameter.png" alt-text="Screenshot that shows how to add a parameter.":::
  
    The **Report Parameter Properties** dialog box opens.  
  
3.  In **Prompt**, enter **Choose color**.
  
4.  In **Name**, enter **RowColor**. 
  
5.  On the **Available Values** tab, select **Specify values**.  
  
7.  Select **Add**.  
  
8.  In the **Label** box, enter **Yellow**.
  
9. In the **Value** box, enter **Yellow**.
  
10. Select **Add**.  
  
11. In the **Label** box, enter **Green**.
  
12. In the **Value** box, enter **PaleGreen**.
  
13. Select **Add**.  
  
14. In the **Label** box, enter **Blue**.
  
15. In the **Value** box, enter **LightBlue**.  
  
16. Select **Add**.  
  
17. In the **Label** box, enter **Pink**.
  
18. In the **Value** box, enter **Pink**. 

    :::image type="content" source="../reporting-services/media/report-builder-expression-tutorial-parameter-available.png" alt-text="Screenshot of the Report Parameter Properties dialog box that shows the Choose the available values for this parameter step.":::
  
19. Select **OK**.
  
### Apply alternating colors to detail rows
  
1.   Select all the cell in the data row except the cell in the **M/F** column, which has its own background color.  

:::image type="content" source="../reporting-services/media/report-builder-expression-tutorial-select-banded.png" alt-text="Screenshot that shows cells selected in a data row.":::

4.  In the Properties pane, select **BackgroundColor**. 

     If you don't see the Properties pane, on the **View** tab select the **Properties** box.  
  
    If the properties are listed by category in the Properties pane, you find **BackgroundColor** in the **Misc** category.  
  
5.  Select the down arrow and then choose **Expression**.  

    :::image type="content" source="../reporting-services/media/report-builder-expression-tutorial-banded-color-property.png" alt-text="Screenshot of the Properties box that shows how to associate an expression with a BackgroundColor.":::
  
6.  In the **Expression** dialog box, expand **Common Functions**, and then select **Program Flow**.  
  
7.  In the **Item** list, double-click **IIf**.  
  
8.  Under **Common Functions**, select **Miscellaneous**, and in the **Item** list, double-click **RowNumber**.  

9. Immediately after **RowNumber(** enter **Nothing) MOD 2,**.
  
8. Select **Parameters** and in the **Values** list, double-click **RowColor**.  
  
22. Immediately after `Parameters!RowColor.Value`, enter **, "White")**.  
  
    The completed expression is: `=IIF(RowNumber(Nothing) MOD 2, Parameters!RowColor.Value, "White")`  

    :::image type="content" source="../reporting-services/media/report-builder-expression-tutorial-banded-color-expressn.png" alt-text="Screenshot that shows the complete banded color expression.":::
  
24. Select **OK**.
  
### Run the report  
  
1.  On the **Home** tab, select **Run**.  

    Now when you run the report, you don't see the report until you choose a color for the nonwhite bands.
  
3.  In the **Choose color** list, select a color for the nonwhite bands in the report.  

    :::image type="content" source="../reporting-services/media/report-builder-expression-tutorial-select-color.png" alt-text="Screenshot that shows how to choose a color for nonwhite bands.":::
  
4.  Select **View Report**.  
  
    The report renders and alternating rows have the background that you chose. 

    :::image type="content" source="../reporting-services/media/report-builder-expression-tutorial-preview-banded.png" alt-text="Screenshot that shows the preview with rows with the alternating colors.":::
  
## <a name="Title"></a>(Optional) Add a report title  
Add a title to the report.  
  
### Add a report title  
  
1.  On the design surface, select **Click to add title**.  
  
2.  Enter **Sales Comparison Summary**, then select the text.  
  
3.  On the **Home** tab, in the **Font** box, set:

    -  Size = 18
    -  Color = Gray
    -  Bold
  
4.  On the **Home** tab, select **Run**.  
  
3.  Select a color for the nonwhite bands in the report, and choose **View Report**.  
  
## <a name="Save"></a>(Optional) Save the report  
You can save reports to a report server, SharePoint library, or your computer. For more information, see [Save reports &#40;Report Builder&#41;](../reporting-services/report-builder/saving-reports-report-builder.md).  
  
In this tutorial, you save the report to a report server. If you don't have access to a report server, save the report to your computer.  
  
### Save the report to a report server  
  
1.  On the **File** menu, select **Save As**.  
  
2.  Select **Recent Sites and Servers**.  
  
3.  Select or enter the name of the report server where you have permission to save reports.  
  
    The message "Connecting to report server" appears. When the connection is complete, you see the contents of the report folder that the report server administrator specified as the default report location.  
  
4.  Give the report a name and select **Save**.  
  
The report is saved to the report server. The name of report server that you're connected to appears in the status bar at the bottom of the window.

Now your report readers can view your report in the [!INCLUDE[ssRSnoversion_md](../includes/ssrsnoversion-md.md)] web portal.

:::image type="content" source="../reporting-services/media/report-builder-expression-tutorial-final-in-browser.png" alt-text="Screenshot of the new report complete with each expression visible.":::

   
## Related content

- [Expressions in a paginated report (Report Builder)](../reporting-services/report-design/expressions-report-builder-and-ssrs.md)
- [Expression examples in paginated reports (Report Builder)](../reporting-services/report-design/expression-examples-report-builder-and-ssrs.md)
- [Indicators in a paginated report (Report Builder)](../reporting-services/report-design/indicators-report-builder-and-ssrs.md)
- [Images, text boxes, rectangles, and lines in a paginated report (Report Builder)](../reporting-services/report-design/images-text-boxes-rectangles-and-lines-report-builder-and-ssrs.md)
- [Tables in paginated reports (Report Builder)](../reporting-services/report-design/tables-report-builder-and-ssrs.md)
- [Report datasets &#40;SSRS&#41;](../reporting-services/report-data/report-datasets-ssrs.md)
