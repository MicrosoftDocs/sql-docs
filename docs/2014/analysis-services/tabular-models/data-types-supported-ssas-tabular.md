---
title: "Data Types Supported (SSAS Tabular) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
ms.assetid: 92993f7b-7243-4aec-906d-0b0379798242
author: minewiskan
ms.author: owend
manager: craigg
---
# Data Types Supported (SSAS Tabular)
  This article describes the data types that can be used in tabular models, and discusses the implicit conversion of data types when data is calculated or used in a Data Analysis Expressions (DAX) formula.  
  
 This article contains the following sections:  
  
-   [Data Types Used in Tabular Models](#bkmk_data_types)  
  
-   [Implicit and Explicit Data Type Conversion in DAX Formulas](#bkmk_implicit)  
  
-   [Handling of Blanks, Empty Strings, and Zero Values](#bkmk_hand_blanks)  
  
##  <a name="bkmk_data_types"></a> Data Types Used in Tabular Models  
 The following data types are supported. When you import data or use a value in a formula, even if the original data source contains a different data type, the data is converted to one of the following data types. Values that result from formulas also use these data types.  
  
 In general, these data types are implemented to enable accurate calculations in calculated columns, and for consistency the same restrictions apply to the rest of the data in models.  
  
 Formats used for numbers, currency, dates and times should follow the format of the locale that is specified on the client used to work with model data. You can use the formatting options in the model to control the way that the value is displayed.  
  
||||  
|-|-|-|  
|Data type in model|Data type in DAX|Description|  
|Whole Number|A 64 bit (eight-bytes) integer value <sup>1, 2</sup>|Numbers that have no decimal places. Integers can be positive or negative numbers, but must be whole numbers between -9,223,372,036,854,775,808 (-2^63) and 9,223,372,036,854,775,807 (2^63-1).|  
|Decimal Number|A 64 bit (eight-bytes) real number <sup>1, 2</sup>|Real numbers are numbers that can have decimal places. Real numbers cover a wide range of values:<br /><br /> Negative values from -1.79E +308 through -2.23E -308<br /><br /> Zero<br /><br /> Positive values from 2.23E -308 through 1.79E + 308<br /><br /> However, the number of significant digits is limited to 17 decimal digits.|  
|Boolean|Boolean|Either a True or False value.|  
|Text|String|A Unicode character data string. Can be strings, numbers or dates represented in a text format.|  
|Date|Date/time|Dates and times in an accepted date-time representation.<br /><br /> Valid dates are all dates after March 1, 1900.|  
|Currency|Currency|Currency data type allows values between -922,337,203,685,477.5808 to 922,337,203,685,477.5807 with four decimal digits of fixed precision.|  
|N/A|Blank|A blank is a data type in DAX that represents and replaces SQL nulls. You can create a blank by using the BLANK function, and test for blanks by using the logical function, ISBLANK.|  
  
 <sup>1</sup> DAX formulas do not support data types that are smaller than those listed in the table.  
  
 <sup>2</sup> If you attempt to import data that has very large numeric values, import might fail with the following error:  
  
 In-memory database error: The '\<column name>' column of the '\<table name>' table contains a value, '1.7976931348623157e+308', which is not supported. The operation has been cancelled.  
  
 This error occurs because the model designer uses that value to represent nulls. The values in the following list are synonyms to the previous mentioned null value:  
  
||  
|-|  
|Value|  
|9223372036854775807|  
|-9223372036854775808|  
|1.7976931348623158e+308|  
|2.2250738585072014e-308|  
  
 You should remove the value from your data and try importing again.  
  
> [!NOTE]  
>  You cannot import from a **varchar(max)** column that contains a string length of more than 131,072 characters.  
  
### Table Data Type  
 In addition, DAX uses a *table* data type. This data type is used by DAX in many functions, such as aggregations and time intelligence calculations. Some functions require a reference to a table; other functions return a table that can then be used as input to other functions. In some functions that require a table as input, you can specify an expression that evaluates to a table; for some functions, a reference to a base table is required. For information about the requirements of specific functions, see [DAX Function Reference](https://msdn.microsoft.com/library/ee634396.aspx).  
  
##  <a name="bkmk_implicit"></a> Implicit and Explicit Data Type Conversion in DAX Formulas  
 Each DAX function has specific requirements as to the types of data that are used as inputs and outputs. For example, some functions require integers for some arguments and dates for others; other functions require text or tables.  
  
 If the data in the column that you specify as an argument is incompatible with the data type required by the function, DAX in many cases will return an error. However, wherever possible DAX will attempt to implicitly convert the data to the required data type. For example:  
  
-   You can type a number, for example "123", as a string. DAX will parse the string and attempt to specify it as a number data type.  
  
-   You can add TRUE + 1 and get the result 2, because TRUE is implicitly converted to the number 1 and the operation 1+1 is performed.  
  
-   If you add values in two columns, and one value happens to be represented as text ("12") and the other as a number (12), DAX implicitly converts the string to a number and then does the addition for a numeric result. The following expression returns 44: = "22" + 22  
  
-   If you attempt to concatenate two numbers, the [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] add-in will present them as strings and then concatenate. The following expression returns "1234": = 12 & 34  
  
 The following table summarizes the implicit data type conversions that are performed in formulas. In general, semantic model designer behaves like Microsoft Excel, and performs implicit conversions whenever possible when required by the specified operation.  
  
### Table of Implicit Data Conversions  
 The type of conversion that is performed is determined by the operator, which casts the values it requires before performing the requested operation. These tables list the operators, and indicate the conversion that is performed on each data type in the column when it is paired with the data type in the intersecting row.  
  
> [!NOTE]  
>  Text data types are not included in these tables. When a number is represented as in a text format, in some cases, the model designer will attempt to determine the number type and represent it as a number.  
  
#### Addition (+)  
  
||||||  
|-|-|-|-|-|  
|Operator (+)|INTEGER|CURRENCY|REAL|Date/time|  
|INTEGER|INTEGER|CURRENCY|REAL|Date/time|  
|CURRENCY|CURRENCY|CURRENCY|REAL|Date/time|  
|REAL|REAL|REAL|REAL|Date/time|  
|Date/time|Date/time|Date/time|Date/time|Date/time|  
  
 For example, if a real number is used in an addition operation in combination with currency data, both values are converted to REAL, and the result is returned as REAL.  
  
#### Subtraction (-)  
 In the following table the row header is the minuend (left side) and the column header is the subtrahend (right side).  
  
||||||  
|-|-|-|-|-|  
|Operator (-)|INTEGER|CURRENCY|REAL|Date/time|  
|INTEGER|INTEGER|CURRENCY|REAL|REAL|  
|CURRENCY|CURRENCY|CURRENCY|REAL|REAL|  
|REAL|REAL|REAL|REAL|REAL|  
|Date/time|Date/time|Date/time|Date/time|Date/time|  
  
 For example, if a date is used in a subtraction operation with any other data type, both values are converted to dates, and the return value is also a date.  
  
> [!NOTE]  
>  Tabular models also support the unary operator, - (negative), but this operator does not change the data type of the operand.  
  
#### Multiplication (*)  
  
||||||  
|-|-|-|-|-|  
|Operator (*)|INTEGER|CURRENCY|REAL|Date/time|  
|INTEGER|INTEGER|CURRENCY|REAL|INTEGER|  
|CURRENCY|CURRENCY|REAL|CURRENCY|CURRENCY|  
|REAL|REAL|CURRENCY|REAL|REAL|  
  
 For example, if an integer is combined with a real number in a multiplication operation, both numbers are converted to real numbers, and the return value is also REAL.  
  
#### Division (/)  
 In the following table the row header is the numerator and the column header is the denominator.  
  
||||||  
|-|-|-|-|-|  
|Operator (/)<br /><br /> (Row/Column)|INTEGER|CURRENCY|REAL|Date/time|  
|INTEGER|REAL|CURRENCY|REAL|REAL|  
|CURRENCY|CURRENCY|REAL|CURRENCY|REAL|  
|REAL|REAL|REAL|REAL|REAL|  
|Date/time|REAL|REAL|REAL|REAL|  
  
 For example, if an integer is combined with a currency value in a division operation, both values are converted to real numbers, and the result is also a real number.  
  
#### Comparison Operators  
 In comparison expressions Boolean values are considered greater than string values and string values are considered greater than numeric or date/time values; numbers and date/time values are considered to have the same rank. No implicit conversions are performed for Boolean or string values; BLANK or a blank value is converted to 0/""/false depending on the data type of the other compared value.  
  
 The following DAX expressions illustrate this behavior:  
  
 `=IF(FALSE()>"true","Expression is true", "Expression is false")`, returns `"Expression is true"`  
  
 `=IF("12">12,"Expression is true", "Expression is false")`, returns `"Expression is true"`  
  
 `=IF("12"=12,"Expression is true", "Expression is false")`, returns `"Expression is false"`  
  
 Conversions are performed implicitly for numeric or date/time types as described in the following table:  
  
||||||  
|-|-|-|-|-|  
|Comparison Operator|INTEGER|CURRENCY|REAL|Date/time|  
|INTEGER|INTEGER|CURRENCY|REAL|REAL|  
|CURRENCY|CURRENCY|CURRENCY|REAL|REAL|  
|REAL|REAL|REAL|REAL|REAL|  
|Date/time|REAL|REAL|REAL|Date/time|  
  
##  <a name="bkmk_hand_blanks"></a> Handling of Blanks, Empty Strings, and Zero Values  
 The way that DAX handles zero values, nulls, and empty strings is different from both Microsoft Excel and SQL Server. This section describes the differences, and describes how these data types are handled.  
  
 The important thing to remember is that, a blank value, an empty cell, or a missing value are all represented by the same new value type, a BLANK. How blanks are handled in operations, such as addition or concatenation, depends on the individual function. You can also generate blanks by using the BLANK function, or test for blanks by using the ISBLANK function. Database nulls are not supported within a semantic model, and nulls are implicitly converted to blanks when a column that contains a null value is referenced in a DAX formula.  
  
### Defining Blanks, Nulls and Empty Strings  
 The following table summarizes the differences between DAX and in Microsoft Excel, in the way that blanks are handled.  
  
||||  
|-|-|-|  
|Expression|DAX|Excel|  
|BLANK + BLANK|BLANK|0 (zero)|  
|BLANK +5|5|5|  
|BLANK * 5|BLANK|0 (zero)|  
|5/BLANK|Infinity|Error|  
|0/BLANK|NaN|Error|  
|BLANK/BLANK|BLANK|Error|  
|FALSE OR BLANK|FALSE|FALSE|  
|FALSE AND BLANK|FALSE|FALSE|  
|TRUE OR BLANK|TRUE|TRUE|  
|TRUE AND BLANK|FALSE|TRUE|  
|BLANK OR BLANK|BLANK|Error|  
|BLANK AND BLANK|BLANK|Error|  
  
 For details on how a particular function or operator handles blanks, see the individual topics for each DAX function, in the section, [DAX Function Reference](https://msdn.microsoft.com/library/ee634396.aspx).  
  
## See Also  
 [Data Sources &#40;SSAS Tabular&#41;](../data-sources-ssas-tabular.md)   
 [Import Data &#40;SSAS Tabular&#41;](../import-data-ssas-tabular.md)  
  
  
