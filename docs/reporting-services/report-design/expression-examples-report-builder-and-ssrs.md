---
title: Expression examples in Report Builder paginated reports
description: Learn how to control content and paginated report appearance using built-in functions, custom code, report and group variables, and user-defined variables in Report Builder.
author: maggiesMSFT
ms.author: maggies
ms.date: 04/06/2017
ms.service: reporting-services
ms.subservice: report-design
ms.topic: conceptual
ms.custom: updatefrequency5
helpviewer_keywords:
  - "page breaks [Reporting Services], expressions"
  - "green-bar reports [Reporting Services]"
  - "Visual Basic [Reporting Services]"
  - "functions [Reporting Services], examples"
  - "custom code [Reporting Services]"
  - "appearance of reports"
  - "formatting reports [Reporting Services], expressions"
  - "show/hide [Reporting Services]"
  - "parameters [Reporting Services], expressions"
  - "visibility [Reporting Services], expressions"
  - "page headers [Reporting Services]"
  - "page footers [Reporting Services]"
  - "dates [Reporting Services], expressions"
  - "expressions [Reporting Services], examples"
---
# Expression examples in Report Builder paginated reports

[!INCLUDE[ssrs-appliesto](../../includes/ssrs-appliesto.md)] [!INCLUDE [ssrs-appliesto-ssrs-rb](../../includes/ssrs-appliesto-ssrs-rb.md)] [!INCLUDE [ssrs-appliesto-pbi-rb](../../includes/ssrs-appliesto-pbi-rb.md)] [!INCLUDE [ssrb-applies-to-ssdt-yes](../../includes/ssrb-applies-to-ssdt-yes.md)]

Expressions are used frequently in paginated reports to control content and report appearance. Expressions are written in [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[visual-basic](../../includes/visual-basic-md.md)], and can use built-in functions, custom code, report and group variables, and user-defined variables. Expressions begin with an equal sign (=). For more information about the expression editor and the types of references that you can include, see [Expression uses in paginated reports (Report Builder)](../../reporting-services/report-design/expression-uses-in-reports-report-builder-and-ssrs.md), and [Add an expression to a paginated report (Report Builder)](../../reporting-services/report-design/add-an-expression-report-builder-and-ssrs.md).  
  
> [!IMPORTANT]  
> When RDL Sandboxing is enabled, only certain types and members can be used in expression text at report publish time. For more information, see [Enable and disable RDL sandboxing for Reporting Services in SharePoint integrated mode](../../reporting-services/report-server-sharepoint/enable-and-disable-rdl-sandboxing.md).
  
For expression examples for specific uses, see the following articles:  
  
- [Group expression examples in paginated reports (Report Builder)](../../reporting-services/report-design/group-expression-examples-report-builder-and-ssrs.md)  
  
- [Filter equation examples in a paginated report (Report Builder)](../../reporting-services/report-design/filter-equation-examples-report-builder-and-ssrs.md)  
  
- [Commonly used filters in a paginated report (Report Builder)](../../reporting-services/report-design/commonly-used-filters-report-builder-and-ssrs.md)  
  
- [Report and group variables references in a paginated report (Report Builder)](../../reporting-services/report-design/built-in-collections-report-and-group-variables-references-report-builder.md)  
  
For more information about simple and complex expressions, where you can use expressions, and the types of references that you can include in an expression, see articles under [Expressions in a paginated report (Report Builder)](../../reporting-services/report-design/expressions-report-builder-and-ssrs.md). For more information about the context in which expressions are evaluated for calculating aggregates, see [Expression scope for totals, aggregates, and built-in collections in a paginated report (Report Builder)](../../reporting-services/report-design/expression-scope-for-totals-aggregates-and-built-in-collections.md).  
  
To learn how to write expressions that use many of the functions and operators also used by expression examples in this article, see [Tutorial: Introduce expressions](../../reporting-services/tutorial-introducing-expressions.md).  

## Functions  

Many expressions in a report contain functions. You can format data, apply logic, and access report metadata using these functions. You can write expressions that use functions from the [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[visual-basic](../../includes/visual-basic-md.md)] run-time library, and from the <xref:System.Convert> and <xref:System.Math> namespaces. You can add references to functions from other assemblies or custom code. You can also use classes from the [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[dnprdnshort](../../includes/dnprdnshort-md.md)], including <xref:System.Text.RegularExpressions>.  
  
## <a name="VisualBasicFunctions"></a> Visual Basic Functions  

You can use [!INCLUDE[visual-basic](../../includes/visual-basic-md.md)] functions to manipulate the data that is displayed in text boxes or that is used for parameters, properties, or other areas of the report. This section provides examples demonstrating some of these functions. For more information, see [Visual Basic runtime library members](/dotnet/visual-basic/language-reference/runtime-library-members).  
  
The [!INCLUDE[dnprdnshort](../../includes/dnprdnshort-md.md)] provides many custom format options, for example, for specific date formats.
  
### Math Functions  
  
- The **Round** function is useful to round numbers to the nearest integer. The following expression rounds a 1.3 to 1:  
  
    ``` basic
    = Round(1.3)  
    ```  
  
    You can also write an expression to round a value to a multiple that you specify, similar to the **MRound** function in Excel. Multiply the value by a factor that creates an integer, round the number, and then divide by the same factor. For example, to round 1.3 to the nearest multiple of 0.2 (1.4), use the following expression:  
  
    ``` basic
    = Round(1.3*5)/5  
    ```  
  
### <a name="DateFunctions"></a> Date Functions  
  
- The **Today** function provides the current date. This expression can be used in a text box to display the date on the report, or in a parameter to filter data based on the current date.  
  
    ``` basic
    =Today()  
    ```  
  
- Use the **DateInterval** function to pull out a specific part of a date. Here are some valid **DateInterval** parameters:

  - DateInterval.Second
  - DateInterval.Minute
  - DateInterval.Hour
  - DateInterval.Weekday
  - DateInterval.Day
  - DateInterval.DayOfYear
  - DateInterval.WeekOfYear
  - DateInterval.Month
  - DateInterval.Quarter
  - DateInterval.Year

    For example, this expression shows the number of the week in the current year for today's date:
  
    ``` basic
    =DatePart(DateInterval.WeekOfYear, today()) 
    ```  
  
- The **DateAdd** function is useful for supplying a range of dates based on a single parameter. The following expression provides a date that is six months after the date from a parameter named *StartDate*.  
  
    ``` basic
    =DateAdd(DateInterval.Month, 6, Parameters!StartDate.Value)  
    ```  
  
- The **Year** function displays the year for a particular date. You can use this function to group dates together or to display the year as a label for a set of dates. This expression provides the year for a given group of sales order dates. The **Month** function and other functions can also be used to manipulate dates. For more information, see the [!INCLUDE[visual-basic](../../includes/visual-basic-md.md)] documentation.  
  
    ``` basic
    =Year(Fields!OrderDate.Value)  
    ```  
  
- You can combine functions in an expression to customize the format. The following expression changes the format of a date in the form month-day-year to month-week-week number. For example, `12/23/2009` to `December Week 3`:  
  
    ``` basic
    =Format(Fields!MyDate.Value, "MMMM") & " Week " &   
    (Int(DateDiff("d", DateSerial(Year(Fields!MyDate.Value),   
    Month(Fields!MyDate.Value),1), Fields!FullDateAlternateKey.Value)/7)+1).ToString  
    ```  
  
    When used as a calculated field in a dataset, you can use this expression on a chart to aggregate values by week within each month.  
  
- The following expression formats the SellStartDate value as MMM-YY. SellStartDate field is a datetime data type.  
  
    ``` basic
    =FORMAT(Fields!SellStartDate.Value, "MMM-yy")  
    ```  
  
- The following expression formats the SellStartDate value as dd/MM/yyyy. The SellStartDate field is a datetime data type.  
  
    ``` basic
    =FORMAT(Fields!SellStartDate.Value, "dd/MM/yyyy")  
    ```  
  
- The **CDate** function converts the value to a date. The **Now** function returns a date value containing the current date and time according to your system. **DateDiff** returns a Long value specifying the number of time intervals between two Date values.  
  
    The following example displays the start date of the current year  
  
    ``` basic
    =DateAdd(DateInterval.Year,DateDiff(DateInterval.Year,CDate("01/01/1900"),Now()),CDate("01/01/1900"))  
    ```  
  
- The following example displays the start date for the previous month based on the current month.  
  
    ``` basic
    =DateAdd(DateInterval.Month,DateDiff(DateInterval.Month,CDate("01/01/1900"),Now())-1,CDate("01/01/1900"))  
    ```  
  
- The following expression generates the interval years between SellStartDate and LastReceiptDate. These fields are in two different datasets, DataSet1 and DataSet2. The [Report Builder functions - First function in a paginated report (Report Builder)](../../reporting-services/report-design/report-builder-functions-first-function.md), which is an aggregate function, returns the first value of SellStartDate in DataSet1 and the first value of LastReceiptDate in DataSet2.  
  
    ``` basic
    =DATEDIFF("yyyy", First(Fields!SellStartDate.Value, "DataSet1"), First(Fields!LastReceiptDate.Value, "DataSet2"))  
    ```  
  
- The **DatePart** function returns an Integer value containing the specified component of a given Date value. The following expression returns the year for the first value of the SellStartDate in DataSet1. The dataset scope is specified because there are multiple datasets in the report.  
  
    ``` basic
    =Datepart("yyyy", First(Fields!SellStartDate.Value, "DataSet1"))  
  
    ```  
  
- The **DateSerial** function returns a Date value representing a specified year, month, and day, with the time information set to midnight. The following example displays the ending date for the prior month, based on the current month.  
  
    ``` basic
    =DateSerial(Year(Now()), Month(Now()), "1").AddDays(-1)  
    ```  
  
- The following expressions display various dates based on a date parameter value selected by the user.  
  
|Example Description|Example|  
|-------------------------|-------------|  
|Yesterday|`=DateSerial(Year(Parameters!TodaysDate.Value),Month(Parameters!TodaysDate.Value),Day(Parameters!TodaysDate.Value)-1)`|  
|Two Days Ago|`=DateSerial(Year(Parameters!TodaysDate.Value),Month(Parameters!TodaysDate.Value),Day(Parameters!TodaysDate.Value)-2)`|  
|One Month Ago|`=DateSerial(Year(Parameters!TodaysDate.Value),Month(Parameters!TodaysDate.Value)-1,Day(Parameters!TodaysDate.Value))`|  
|Two Months Ago|`=DateSerial(Year(Parameters!TodaysDate.Value),Month(Parameters!TodaysDate.Value)-2,Day(Parameters!TodaysDate.Value))`|  
|One Year Ago|`=DateSerial(Year(Parameters!TodaysDate.Value)-1,Month(Parameters!TodaysDate.Value),Day(Parameters!TodaysDate.Value))`|  
|Two Years Ago|`=DateSerial(Year(Parameters!TodaysDate.Value)-2,Month(Parameters!TodaysDate.Value),Day(Parameters!TodaysDate.Value))`|  
  
### <a name="StringFunctions"></a> String Functions  
  
- Combine more than one field by using concatenation operators and [!INCLUDE[visual-basic](../../includes/visual-basic-md.md)] constants. The following expression returns two fields, each on a separate line in the same text box:  

    ``` basic
    =Fields!FirstName.Value & vbCrLf & Fields!LastName.Value   
    ```  
  
- Format dates and numbers in a string with the **Format** function. The following expression displays values of the *StartDate* and *EndDate* parameters in long date format:  
  
    ``` basic
    =Format(Parameters!StartDate.Value, "D") & " through " &  Format(Parameters!EndDate.Value, "D")    
    ```  
  
    If the text box contains only a date or number, you should use the **Format** property of the text box to apply formatting instead of the **Format** function within the text box.  
  
- The **Right**, **Len**, and **InStr** functions are useful for returning a substring, for example, trimming *DOMAIN*\\*username* to just the user name. The following expression returns the part of the string to the right of a backslash (\\) character from a parameter named *User*:  
  
    ``` basic
    =Right(Parameters!User.Value, Len(Parameters!User.Value) - InStr(Parameters!User.Value, "\"))  
    ```  
  
    The following expression results in the same value as the previous one, using members of the [!INCLUDE[dnprdnshort](../../includes/dnprdnshort-md.md)] <xref:System.String> class instead of [!INCLUDE[visual-basic](../../includes/visual-basic-md.md)] functions:  
  
    ``` basic
    =Parameters!User.Value.Substring(Parameters!User.Value.IndexOf("\")+1, Parameters!User.Value.Length-Parameters!User.Value.IndexOf("\")-1)  
    ```  
  
- Display the selected values from a multivalue parameter. The following example uses the **Join** function to concatenate the selected values of the parameter *MySelection* into a single string that can be set as an expression for the value of a text box in a report item:  
  
    ``` basic
    = Join(Parameters!MySelection.Value)  
    ```  
  
    The following example does the same as the previous example and displays a text string before the list of selected values.  
  
    ``` basic
    ="Report for " & JOIN(Parameters!MySelection.Value, " & ")  
  
    ```  
  
- The **Regex** functions from the [!INCLUDE[dnprdnshort](../../includes/dnprdnshort-md.md)] <xref:System.Text.RegularExpressions> are useful for changing the format of existing strings, for example, formatting a telephone number. The following expression uses the **Replace** function to change the format of a telephone number in a field from "*nnn*-*nnn*-*nnnn*" to "(*nnn*) *nnn*-*nnnn*":  
  
    ``` basic
    =System.Text.RegularExpressions.Regex.Replace(Fields!Phone.Value, "(\d{3})[ -.]*(\d{3})[ -.]*(\d{4})", "($1) $2-$3")  
    ```  
  
    > [!NOTE]  
    > Verify that the value for Fields!Phone.Value has no extra spaces and is of type <xref:System.String>.  
  
### Lookup  
  
- By specifying a key field, you can use the **Lookup** function to retrieve a value from a dataset for a one-to-one relationship, for example, a key-value pair. The following expression displays the product name from a dataset ("Product"), given the product identifier to match on:  
  
    ``` basic
    =Lookup(Fields!PID.Value, Fields!ProductID.Value, Fields!ProductName.Value, "Product")  
    ```  
  
### LookupSet  
  
- By specifying a key field, you can use the **LookupSet** function to retrieve a set of values from a dataset for a one-to-many relationship. For example, a person can have multiple telephone numbers. In the following example, assume the dataset PhoneList contains a person identifier and a telephone number in each row. **LookupSet** returns an array of values. The following expression combines the return values into a single string and displays the list of telephone numbers for the person specified by ContactID:  
  
    ``` basic
    =Join(LookupSet(Fields!ContactID.Value, Fields!PersonID.Value, Fields!PhoneNumber.Value, "PhoneList"),",")  
    ```  
  
### <a name="ConversionFunctions"></a> Conversion Functions  

You can use [!INCLUDE[visual-basic](../../includes/visual-basic-md.md)] functions to convert a field from the one data type to a different data type. Conversion functions can be used to convert the default data type for a field to the data type needed for calculations or to combine text.  
  
- The following expression converts the constant 500 to type Decimal in order to compare it to a [!INCLUDE[tsql](../../includes/tsql-md.md)] money data type in the Value field for a filter expression.  
  
    ``` basic
    =CDec(500)  
    ```  
  
- The following expression displays the number of values selected for the multivalue parameter *MySelection*.  
  
    ``` basic
    =CStr(Parameters!MySelection.Count)  
    ```  
  
### <a name="DecisionFunctions"></a> Decision Functions  
  
- The **IIF** function returns one of two values depending on whether the expression is true or not. The following expression uses the **IIF** function to return a Boolean value of **True** if the value of `LineTotal` exceeds 100. Otherwise it returns **False**:  
  
    ``` basic
    =IIF(Fields!LineTotal.Value > 100, True, False)  
    ```  
  
- Use multiple **IIF** functions (also known as "nested **IIF** statements") to return one of three values depending on the value of `PctComplete`. The following expression can be placed in the fill color of a text box to change the background color depending on the value in the text box.  
  
    ``` basic
    =IIF(Fields!PctComplete.Value >= 10, "Green", IIF(Fields!PctComplete.Value >= 1, "Blue", "Red"))  
    ```  
  
    Values greater than or equal to 10 display with a green background. Values between one and nine display with a blue background. values less than one display with a red background.  
  
- A different way to get the same functionality uses the **Switch** function. The **Switch** function is useful when you have three or more conditions to test. The **Switch** function returns the value associated with the first expression in a series that evaluates to true:  
  
    ``` basic
    =Switch(Fields!PctComplete.Value >= 10, "Green", Fields!PctComplete.Value >= 1, "Blue", Fields!PctComplete.Value = 1, "Yellow", Fields!PctComplete.Value <= 0, "Red")  
    ```  
  
    Values greater than or equal to 10 display with a green background. Values between one and nine display with a blue background. Values equal to one display with a yellow background. Values 0 or less display with a red background.  
  
- Test the value of the `ImportantDate` field and return "Red" if it's more than a week old, and "Blue" otherwise. This expression can be used to control the Color property of a text box in a report item:  
  
    ``` basic
    =IIF(DateDiff("d",Fields!ImportantDate.Value, Now())>7,"Red","Blue")  
    ```  
  
- Test the value of the `PhoneNumber` field and return "No Value" if it's **null** (**Nothing** in [!INCLUDE[visual-basic](../../includes/visual-basic-md.md)]); otherwise return the phone number value. This expression can be used to control the value of a text box in a report item.  
  
    ``` basic
    =IIF(Fields!PhoneNumber.Value Is Nothing,"No Value",Fields!PhoneNumber.Value)  
    ```  
  
- Test the value of the `Department` field and return either a subreport name or a **null** (**Nothing** in [!INCLUDE[visual-basic](../../includes/visual-basic-md.md)]). This expression can be used for conditional drillthrough subreports.  
  
    ``` basic
    =IIF(Fields!Department.Value = "Development", "EmployeeReport", Nothing)  
    ```  
  
- Test if a field value is null. This expression can be used to control the **Hidden** property of an image report item. In the following example, the image specified by the field [LargePhoto] is displayed only if the value of the field isn't null.  
  
    ``` basic
    =IIF(IsNothing(Fields!LargePhoto.Value),True,False)  
    ```  
  
- The **MonthName** function returns a string value containing the name of the specified month. The following example displays NA in the Month field when the field contains the value of 0.  
  
    ``` basic
    IIF(Fields!Month.Value=0,"NA",MonthName(IIF(Fields!Month.Value=0,1,Fields!Month.Value)))  
  
    ```  
  
## <a name="ReportFunctions"></a> Report Functions  

In an expression, you can add a reference to more report functions that manipulate data in a report. This section provides examples for two of these functions. For more information about report functions and examples, see [Report Builder functions - aggregate functions reference in paginated reports (Report Builder)](../../reporting-services/report-design/report-builder-functions-aggregate-functions-reference.md).  
  
### <a name="Sum"></a> Sum  
  
- The **Sum** function can total the values in a group or data region. This function can be useful in the header or footer of a group. The following expression displays the sum of data in the Order group or data region:  
  
    ``` basic
    =Sum(Fields!LineTotal.Value, "Order")  
    ```  
  
- You can also use the **Sum** function for conditional aggregate calculations. For example, imagine you have a dataset has a field that is named `State` with possible values Not Started, Started, Finished. The following expression, when placed in a group header, calculates the aggregate sum for only the value Finished:  
  
    ``` basic
    =Sum(IIF(Fields!State.Value = "Finished", 1, 0))  
    ```  
  
### <a name="RowNumber"></a> RowNumber  
  
- The **RowNumber** function, when used in a text box within a data region, displays the row number for each instance of the text box in which the expression appears. This function can be useful to number rows in a table. It can also be useful for more complex tasks, such as providing page breaks based on number of rows. For more information, see [Page Breaks](#PageBreaks) in this article.  
  
    The scope you specify for **RowNumber** controls when renumbering begins. The **Nothing** keyword indicates that the function starts counting at the first row in the outermost data region. To start counting within nested data regions, use the name of the data region. To start counting within a group, use the name of the group.  
  
    ``` basic
    =RowNumber(Nothing)  
    ```  
  
## <a name="AppearanceofReportData"></a> Appearance of Report Data  

You can use expressions to manipulate how data appears on a report. For example, you can display the values of two fields in a single text box, display information about the report, or affect how page breaks are inserted in the report.  
  
### <a name="PageHeadersandFooters"></a> Page Headers and Footers  

When designing a report, you might want to display the name of the report and page number in the report footer. You can use the following expressions:  
  
- The following expression provides the name of the report and the time it was run. It can be placed in a text box in the report footer or in the body of the report. The time is formatted with the [!INCLUDE[dnprdnshort](../../includes/dnprdnshort-md.md)] formatting string for short date:  

    ``` basic
    =Globals.ReportName & ", dated " & Format(Globals.ExecutionTime, "d")  
    ```  
  
- The following expression, placed in a text box in the footer of a report, provides page number and total pages in the report:  
  
    ``` basic
    =Globals.PageNumber & " of " & Globals.TotalPages  
    ```  
  
The following examples describe how to display the first and last values from a page in the page header, similar to what you might find in a directory listing. The example assumes a data region that contains a text box named `LastName`.  
  
- The following expression, placed in a text box on the left side of the page header, provides the first value of the `LastName` text box on the page:  
  
    ``` basic
    =First(ReportItems("LastName").Value)  
    ```  
  
- The following expression, placed in a text box on the right side of the page header, provides the last value of the `LastName` text box on the page:  
  
    ``` basic
    =Last(ReportItems("LastName").Value)  
    ```  
  
The following example describes how to display a page total. The example assumes a data region that contains a text box named `Cost`.  
  
- The following expression, placed in the page header or footer, provides the sum of the values in the `Cost` text box for the page:  
  
    ``` basic
    =Sum(ReportItems("Cost").Value)  
    ```  
  
> [!NOTE]  
> You can refer to only one report item per expression in a page header or footer. Also, you can refer to the text box name, but not the actual data expression within the text box, in page header and footer expressions.  
  
### <a name="PageBreaks"></a> Page Breaks  

In some reports, you might want to place a page break at the end of a specified number of rows instead of, or in addition to, on groups or report items. Create a group that contains the groups or detail records you want. Add a page break to the group, and then add a group expression to group by a specified number of rows.  
  
- The following expression, when placed in the group expression, assigns a number to each set of 25 rows. When a page break is defined for the group, this expression results in a page break every 25 rows.  
  
    ``` basic
    =Ceiling(RowNumber(Nothing)/25)  
    ```  
  
    To allow the user to set a value for the number of rows per page, create a parameter named `RowsPerPage` and base the group expression on the parameter, as shown in the following expression:  
  
    ``` basic
    =Ceiling(RowNumber(Nothing)/Parameters!RowsPerPage.Value)  
    ```  
  
    For more information about setting page breaks for a group, see [Add a page break to a paginated report (Report Builder)](../../reporting-services/report-design/add-a-page-break-report-builder-and-ssrs.md).  
  
## <a name="Properties"></a> Properties  

Expressions aren't only used to display data in text boxes. They can also be used to change how properties are applied to report items. You can change style information for a report item, or change its visibility.  
  
### <a name="Formatting"></a> Formatting  
  
- The following expression, when used in the Color property of a text box, changes the color of the text depending on the value of the `Profit` field:  
  
    ``` basic
    =Iif(Fields!Profit.Value < 0, "Red", "Black")  
    ```  
  
    You can also use the [!INCLUDE[visual-basic](../../includes/visual-basic-md.md)] object variable `Me`. This variable is another way of referring to the value of a text box.  
  
    `=Iif(Me.Value < 0, "Red", "Black")`  
  
- The following expression, when used in the BackgroundColor property of a report item in a data region, alternates the background color of each row between pale green and white:  

    ``` basic
    =Iif(RowNumber(Nothing) Mod 2, "PaleGreen", "White")  
    ```  
  
    If you're using an expression for a specified scope, you might have to indicate the dataset for the aggregate function:  
  
    ``` basic
    =Iif(RowNumber("Employees") Mod 2, "PaleGreen", "White")  
    ```  
  
> [!NOTE]  
> Available colors come from the [!INCLUDE[dnprdnshort](../../includes/dnprdnshort-md.md)] KnownColor enumeration.  
  
### Chart colors  

To specify colors for a Shape chart, you can use custom code to control the order that colors are mapped to data point values. This custom code helps you use consistent colors for multiple charts that have the same category groups. For more information, see [Specify consistent colors in multiple shape charts in a paginated report (Report Builder)](../../reporting-services/report-design/specify-consistent-colors-across-multiple-shape-charts-report-builder-and-ssrs.md).  
  
### <a name="Visibility"></a> Visibility  

You can show and hide items in a report using the visibility properties for the report item. In a data region such as a table, you can initially hide detail rows based on the value in an expression.  
  
- The following expression, when used for initial visibility of detail rows in a group, shows the detail rows for all sales exceeding 90 percent in the `PctQuota` field:  
  
    ``` basic
    =Iif(Fields!PctQuota.Value>.9, False, True)  
    ```  
  
- The following expression, when set in the Hidden property of a table, shows the table only if it has more than 12 rows:  
  
    ``` basic
    =IIF(CountRows()>12,false,true)  
    ```  
  
- The following expression, when set in the **Hidden** property of a column, shows the column only if the field exists in the report dataset after the data is retrieved from the data source:  
  
    ``` basic
    =IIF(Fields!Column_1.IsMissing, true, false)  
    ```  
  
### <a name="Hyperlinks"></a> URLs  

You can customize URLs by using report data and also conditionally control whether URLs are added as an action for a text box.  
  
- The following expression, when used as an action on a text box, generates a customized URL that specifies the dataset field `EmployeeID` as a URL parameter.  
  
    ``` basic
    ="https://adventure-works/MyInfo?ID=" & Fields!EmployeeID.Value  
    ```  
  
     For more information, see [Add a hyperlink to a URL in a paginated report (Report Builder)](../../reporting-services/report-design/add-a-hyperlink-to-a-url-report-builder-and-ssrs.md).  
  
- The following expression conditionally controls whether to add a URL in a text box. This expression depends on a parameter named `IncludeURLs` that allows a user to decide whether to include active URLs in a report. This expression is set as an action on a text box. By setting the parameter to False and then viewing the report, you can export the report Microsoft Excel without hyperlinks.  
  
    ``` basic
    =IIF(Parameters!IncludeURLs.Value,"https://adventure-works.com/productcatalog",Nothing)  
    ```  
  
## <a name="ReportData"></a> Report data  

Expressions can be used to manipulate the data that is used in the report. You can refer to parameters and other report information. You can even change the query that is used to retrieve data for the report.  
  
### <a name="Parameters"></a> Parameters  

You can use expressions in a parameter to vary the default value for the parameter. For example, you can use a parameter to filter data to a particular user based on the user ID that is used to run the report.  
  
- The following expression, when used as the default value for a parameter, collects the user ID of the person running the report:  
  
    ``` basic
    =User!UserID  
    ```  
  
- To refer to a parameter in a query parameter, filter expression, text box, or other area of the report, use the **Parameters** global collection. This example assumes that the parameter is named *Department*:  
  
    ``` basic
    =Parameters!Department.Value  
    ```  
  
- Parameters can be created in a report but set to hidden. When the report runs on the report server, the parameter doesn't appear in the toolbar and the report reader can't change the default value. You can use a hidden parameter set to a default value as custom constant. You can use this value in any expression, including a field expression. The following expression identifies the field specified by the default parameter value for the parameter named *ParameterField*:  
  
    ``` basic
    =Fields(Parameters!ParameterField.Value).Value  
    ```  
  
## <a name="CustomCode"></a> Custom code  

 You can use custom code in a report. Custom code is either embedded in a report or stored in a custom assembly used in the report. For more information about custom code, see [Custom code and assembly references in expressions in a paginated report in Report Designer (SSRS)](../../reporting-services/report-design/custom-code-and-assembly-references-in-expressions-in-report-designer-ssrs.md).  
  
### Use group variables for custom aggregation  

You can initialize the value for a group variable that is local to a particular group scope and then include a reference to that variable in expressions. One of the ways that you can use a group variable with custom code is to implement a custom aggregate. For more information, see [Use group variables in Reporting Services 2008 for custom aggregation](/archive/blogs/robertbruckner/using-group-variables-in-reporting-services-2008-for-custom-aggregation).  
  
For more information about variables, see [Report and group variables references in a paginated report (Report Builder)](../../reporting-services/report-design/built-in-collections-report-and-group-variables-references-report-builder.md).  
  
## Suppress null or zero values at run time  

Some values in an expression can evaluate to null or undefined at report processing time. These values can create run-time errors that result in **#Error** displaying in the text box instead of the evaluated expression. The **IIF** function is sensitive to this behavior because, unlike an If-Then-Else statement, each part of the **IIF** statement is evaluated (including function calls) before being passed to the routine that tests for **true** or **false**. The statement `=IIF(Fields!Sales.Value is NOTHING, 0, Fields!Sales.Value)` generates **#Error** in the rendered report if `Fields!Sales.Value` is NOTHING.  
  
To avoid this condition, use one of the following strategies:  
  
- Set the numerator to 0 and the denominator to 1 if the value for field B is 0 or undefined. Otherwise, set the numerator to the value for field A and the denominator to the value for field B.  
  
    ``` basic
    =IIF(Field!B.Value=0, 0, Field!A.Value / IIF(Field!B.Value =0, 1, Field!B.Value))  
    ```  
  
- Use a custom code function to return the value for the expression. The following example returns the percentage difference between a current value and a previous value. This value can be used to calculate the difference between any two successive values and it handles the edge case of the first comparison (when there's no previous value) and cases whether either the previous value or the current value is **null** (**Nothing** in [!INCLUDE[visual-basic](../../includes/visual-basic-md.md)]).  
  
    ``` basic
    Public Function GetDeltaPercentage(ByVal PreviousValue, ByVal CurrentValue) As Object  
        If IsNothing(PreviousValue) OR IsNothing(CurrentValue) Then  
            Return Nothing  
        Else if PreviousValue = 0 OR CurrentValue = 0 Then  
            Return Nothing  
        Else   
            Return (CurrentValue - PreviousValue) / CurrentValue  
        End If  
    End Function  
    ```  
  
    The following expression shows how to call this custom code from a text box, for the "ColumnGroupByYear" container (group or data region).  
  
    ``` basic
    =Code.GetDeltaPercentage(Previous(Sum(Fields!Sales.Value),"ColumnGroupByYear"), Sum(Fields!Sales.Value))  
    ```  
  
    This code helps to avoid run-time exceptions. You can now use an expression like `=IIF(Me.Value < 0, "red", "black")` in the **Color** property of the text box to conditionally the display text based on whether the values are greater than or less than 0.  
  
## Related content  

- [Filter equation examples in a paginated report (Report Builder)](../../reporting-services/report-design/filter-equation-examples-report-builder-and-ssrs.md)
- [Group expression examples in paginated reports (Report Builder)](../../reporting-services/report-design/group-expression-examples-report-builder-and-ssrs.md)
- [Expression uses in paginated reports (Report Builder)](../../reporting-services/report-design/expression-uses-in-reports-report-builder-and-ssrs.md)
