---
title: "Lesson 5: Formatting a Report (Reporting Services) | Microsoft Docs"
ms.date: 04/29/2019
ms.prod: reporting-services
ms.prod_service: "reporting-services-native"
ms.technology: reporting-services

ms.topic: conceptual
ms.assetid: ae46efa9-6e04-48ec-afb4-5a2314dcb05a
author: maggiesMSFT
ms.author: maggies
---
# Lesson 5: Formatting a report (Reporting Services)

Now that you've added a data region and some fields to the Sales Orders report, you can format the date and currency fields and the column headers.

## <a name="bkmk_format_date"></a>Format the Date

The Date field displays date and time information by default. You can format it to display only the date.

1. Select **Design** tab.

2. Right-click the cell with the `[Date]` field expression and then select **Text Box Properties**.

3. Select **Number**, and then in the **Category** field, select **Date**.

4. In the **Type** box, select **January 31, 2000**.

5. Select the **OK** button.

6. Preview the report to see the change to the `[Date]` field and then change back to design view.

## <a name="bkmk_format_currency"></a>Format the Currency

The **LineTotal** field displays a general number. Format it to display the number as currency.

1. Right-click the cell with the `[LineTotal]` field expression and then select **Text Box Properties**.

2. Select **Number**, and in the **Category** field, select **Currency**.

3. If your regional setting is English (United States), the defaults should be:

- **Decimal places: 2**
- **Negative numbers: ($12345.00)**
- **Symbol: $ English (United States)**

4. Select **Use 1000 separator (,)**. If the sample text displays **$12,345.00**, then your settings are correct.

5. Select the **OK** button.

6. Preview the report to see the change to the `[LineTotal]` field and then change back to design view.  

## <a name="bkmk_change_textstyle"></a>Change Text Style and Column Widths

You can also change the formatting of the header row to differentiate it from the rows of data in the report. Lastly, you will adjust the widths of the columns.

### To format header rows and table columns

1. Select the table so that column and row handles appear above and next to the table. The gray bars along the top and side of the table are the column and row handles.

2. Point to the line between column handles so that the cursor changes into a double arrow. Drag the columns to the size you want.
    ![rs_BasicTableDetailsDesign](media/rs-basictabledetailsdesign.png)

3. Select the row containing column header labels and from the **Format** menu, point to **Font** and then click **Bold**.

4. To preview your report, select the **Preview** tab. It should look something like this:

    ![Preview of table with bold column headers](media/rs-basictabledetailsformattedpreview.png "Preview of table with bold column headers")  

5. On the **File** menu, select **Save All** to save the report.

## Next steps

You have successfully formatted column headers and date and currency values. Next, you will add grouping and totals to your report. See [Lesson 6: Adding Grouping and Totals &#40;Reporting Services&#41;](lesson-6-adding-grouping-and-totals-reporting-services.md).

## See also

[Formatting Numbers and Dates &#40;Report Builder and SSRS&#41;](report-design/formatting-numbers-and-dates-report-builder-and-ssrs.md)
[Rendering Behaviors &#40;Report Builder  and SSRS&#41;](report-design/rendering-behaviors-report-builder-and-ssrs.md)
