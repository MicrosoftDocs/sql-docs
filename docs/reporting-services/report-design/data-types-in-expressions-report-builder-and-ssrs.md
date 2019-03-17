---
title: "Data Types in Expressions (Report Builder and SSRS) | Microsoft Docs"
ms.date: 08/17/2018
ms.prod: reporting-services
ms.prod_service: "reporting-services-sharepoint, reporting-services-native"
ms.technology: report-design


ms.topic: conceptual
ms.assetid: 94fdf921-270c-4c12-87b3-46b1cc98fae5
author: markingmyname
ms.author: maghan
---
# Data Types in Expressions (Report Builder and SSRS)
  Data types represent different kinds of data so that it can be stored and processed efficiently. Typical data types include text (also known as strings), numbers with and without decimal places, dates and times, and images. Values in a report must be an Report Definition Language (RDL) data type. You can format a value according to your preference when you display it in a report. For example, a field that represents currency is stored in the report definition as a floating point number, but can be displayed in a variety of formats depending on the format property you choose.  
  
 For more information about display formats, see [Formatting Report Items &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/formatting-report-items-report-builder-and-ssrs.md).  
  
> [!NOTE]  
>  [!INCLUDE[ssRBRDDup](../../includes/ssrbrddup-md.md)]  
  
## Report Definition Language (RDL) Data Types and Common Language Runtime (CLR) Data Types  
 Values that are specified in an RDL file must be an RDL data type. When the report is compiled and processed, RDL data types are converted to CLR data types. The following table displays the conversion, which is marked Default:  
  
|RDL Type|CLR Types|  
|--------------|---------------|  
|String|Default: String<br /><br /> Chart, GUID, Timespan|  
|Boolean|Default: Boolean|  
|Integer|Default: Int64<br /><br /> Int16, Int32, Uint16, Uint64, Byte, Sbyte|  
|DateTime|Default: DateTime<br /><br /> DateTimeOffset|  
|Float|Default: Double<br /><br /> Single, Decimal|  
|Binary|Default: Byte[]|  
|Variant|Any of the above except Byte[]|  
|VariantArray|Array of Variant|  
|Serializable|Variant or types marked with Serializable or that implement ISerializable.|  
  
## Understanding Data Types and Writing Expressions  
 It is important to understand data types when you write expressions to compare or combine values, for example, when you define group or filter expressions, or calculate aggregates. Comparisons and calculations are valid only between items of the same data type. If the data types do not match, you must explicitly convert the data type in the report item by using an expression.  
  
 The following list describes cases when you may need to convert data to a different data type:  
  
-   Comparing the value of a report parameter of one data type to a dataset field of a different data type.  
  
-   Writing filter expressions that compare values of different data types.  
  
-   Writing sort expressions that combine fields of different data types.  
  
-   Writing group expressions that combine fields of different data types.  
  
-   Converting a value retrieved from the data source from one data type to a different data type.  
  
## Determining the Data Type of Report Data  
 To determine the data type of a report item, you can write an expression that returns its data type. For example, to show the data type for the field `MyField`, add the following expression to a table cell: `=Fields!MyField.Value.GetType().ToString()`. The result displays the CLR data type used to represent `MyField`, for example, **System.String** or **System.DateTime**.  
  
## Converting Dataset Fields to a Different Data Type  
 You can also convert dataset fields before you use them in a report. The following list describes ways that you can convert an existing dataset field:  
  
-   Modify the dataset query to add a new query field with the converted data. For relational or multidimensional data sources, this uses data source resources to perform the conversion.  
  
-   Create a calculated field based on an existing report dataset field by writing an expression that converts all the data in one result set column to a new column with a different data type. For example, the following expression converts the field Year from an integer value to a string value: `=CStr(Fields!Year.Value)`. For more information, see [Add, Edit, Refresh Fields in the Report Data Pane &#40;Report Builder and SSRS&#41;](../../reporting-services/report-data/add-edit-refresh-fields-in-the-report-data-pane-report-builder-and-ssrs.md).  
  
-   Check whether the data processing extension you are using includes metadata for retrieving preformatted data. For example, a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] MDX query includes a FORMATTED_VALUE extended property for cube values that have already been formatted when processing the cube. For more information, see [Extended Field Properties for an Analysis Services Database &#40;SSRS&#41;](../../reporting-services/report-data/extended-field-properties-for-an-analysis-services-database-ssrs.md).  
  
## Understanding Parameter Data Types  
 Report parameters must be one of five data types: Boolean, DateTime, Integer, Float, or Text (also known as String). When a dataset query includes query parameters, report parameters are automatically created and linked to the query parameters. The default data type for a report parameter is String. To change the default data type of a report parameter, select the correct value from the **Data type** drop-down list on the **General** page of the **Report Parameter Properties** dialog box.  
  
> [!NOTE]  
>  Report parameters that are DateTime data types do not support milliseconds. Although you can create a parameter based on values that include milliseconds, you cannot select a value from an available values drop-down list that includes Date or Time values that include milliseconds.  
  
## Writing Expressions that Convert Data Types or Extract Parts of Data  
 When you combine text and dataset fields using the concatenation operator (&), the common language runtime (CLR) generally provides default formats. When you need to explicitly convert a dataset field or parameter to a specific data type, you must use a CLR method or a Visual Basic runtime library function to convert the data.  
  
 The following table shows examples of converting data types.  
  
|Type of conversion|Example|  
|------------------------|-------------|  
|DateTime to String|`=CStr(Fields!Date.Value)`|  
|String to DateTime|`=DateTime.Parse(Fields!DateTimeinStringFormat.Value)`|  
|String to DateTimeOffset|`=DateTimeOffset.Parse(Fields!DateTimeOffsetinStringFormat.Value)`|  
|Extracting the Year|`=Year(Fields!TimeinStringFormat.Value)`<br /><br /> `-- or --`<br /><br /> `=Year(Fields!TimeinDateTimeFormat.Value)`|  
|Boolean to Integer|`=CInt(Parameters!BooleanField.Value)`<br /><br /> -1 is True and 0 is False.|  
|Boolean to Integer|`=System.Convert.ToInt32(Fields!BooleanFormat.Value)`<br /><br /> 1 is True and 0 is False.|  
|Just the DateTime part of a DateTimeOffset value|`=Fields!MyDatetimeOffset.Value.DateTime`|  
|Just the Offset part of a DateTimeOffset value|`=Fields!MyDatetimeOffset.Value.Offset`|  
  
 You can also use the Format function to control the display format for value. For more information, see [Functions (Visual Basic)](https://go.microsoft.com/fwlink/?linkid=111483).  
  
## Advanced Examples  
 When you connect to a data source with a data provider that does not provide conversion support for all the data types on the data source, the default data type for unsupported data source types is String. The following examples provide solutions to specific data types that are returned as a string.  
  
### Concatenating a String and a CLR DateTimeOffset Data Type  
 For most data types, the CLR provides default conversions so that you can concatenate values that are different data types into one string by using the & operator. For example, the following expression concatenates the text "The date and time are: " with a dataset field StartDate, which is a <xref:System.DateTime> value: `="The date and time are: " & Fields!StartDate.Value`.  
  
 For some data types, you may need to include the ToString function. For example, the following expression shows the same example using the CLR data type <xref:System.DateTimeOffset>, which include the date, the time, and a time-zone offset relative to the UTC time zone: `="The time is: " & Fields!StartDate.Value.ToString()`.  
  
### Converting a String Data Type to a CLR DateTime Data Type  
 If a data processing extension does not support all data types defined on a data source, the data may be retrieved as text. For example, a **datetimeoffset(7)** data type value may be retrieved as a String data type. In Perth, Australia, the string value for July 1, 2008, at 6:05:07.9999999 A.M. would resemble:  
  
 `2008-07-01 06:05:07.9999999 +08:00`  
  
 This example shows the date (July 1, 2008), followed by the time to a 7-digit precision (6:05:07.9999999 A.M.), followed by a UTC time zone offset in hours and minutes (plus 8 hours, 0 minutes). For the following examples, this value has been placed in a **String** field called `MyDateTime.Value`.  
  
 You can use one of the following strategies to convert this data to one or more CLR values:  
  
-   In a text box, use an expression to extract parts of the string. For example:  
  
    -   The following expression extracts just the hour part of the UTC time zone offset and converts it to minutes: `=CInt(Fields!MyDateTime.Value.Substring(Fields!MyDateTime.Value.Length-5,2)) * 60`  
  
         The result is `480`.  
  
    -   The following expression converts the string to a date and time value: `=DateTime.Parse(Fields!MyDateTime.Value)`  
  
         If the `MyDateTime.Value` string has a UTC offset, the `DateTime.Parse` function first adjusts for the UTC offset (7 A.M. - [`+08:00`] to the UTC time of 11 P.M. the night before). The `DateTime.Parse` function then applies the local report server UTC offset and, if necessary, adjusts the time again for Daylight Saving Time. For example, in Redmond, Washington, the local time offset adjusted for Daylight Saving Time is `[-07:00]`, or 7 hours earlier than 11 PM. The result is the following **DateTime** value: `2007-07-06 04:07:07 PM` (July 6, 2007 at 4:07 P.M).  
  
 For more information about converting strings to **DateTime** data types, see [Parsing Date and Time Strings](https://go.microsoft.com/fwlink/?LinkId=89703), [Formatting Date and Time for a Specific Culture](https://go.microsoft.com/fwlink/?LinkId=89704), and [Choosing Between DateTime, DateTimeOffset, and TimeZoneInfo](https://go.microsoft.com/fwlink/?linkid=110652) on MSDN.  
  
-   Add a new calculated field to the report dataset that uses an expression to extract parts of the string. For more information, see [Add, Edit, Refresh Fields in the Report Data Pane &#40;Report Builder and SSRS&#41;](../../reporting-services/report-data/add-edit-refresh-fields-in-the-report-data-pane-report-builder-and-ssrs.md).  
  
-   Change the report dataset query to use [!INCLUDE[tsql](../../includes/tsql-md.md)] functions to extract the date and time values independently to create separate columns. The following example shows how to use the function **DatePart** to add a column for the year and a column for the UTC time zone converted to minutes:  
  
     `SELECT`  
  
     `MyDateTime,`  
  
     `DATEPART(year, MyDateTime) AS Year,`  
  
     `DATEPART(tz, MyDateTime) AS OffsetinMinutes`  
  
     `FROM MyDates`  
  
     The result set has three columns. The first column is the date and time, the second column is the year, and the third column is the UTC offset in minutes. The following row shows example data:  
  
     `2008-07-01 06:05:07             2008                   480`  
  
 For more information about [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database data types, see [Data Types &#40;Transact-SQL&#41;](../../t-sql/data-types/data-types-transact-sql.md), and [Date and Time Data Types and Functions &#40;Transact-SQL&#41;](../../t-sql/functions/date-and-time-data-types-and-functions-transact-sql.md).  
  
 For more information about [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] data types, see [Data Types in Analysis Services](../../analysis-services/multidimensional-models/olap-physical/data-types-in-analysis-services.md).  
  
## See Also  
 [Formatting Report Items &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/formatting-report-items-report-builder-and-ssrs.md)  
  
  
