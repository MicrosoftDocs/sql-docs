---
title: "Advanced Edit (Condition) Dialog Box | Microsoft Docs"
ms.custom: ""
ms.date: "08/12/2016"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: security
ms.topic: conceptual
f1_keywords: 
  - "sql13.swb.dmf.condition.advancededit.f1"
ms.assetid: a0bbe501-78c5-45ad-9087-965d04855663
author: VanMSFT
ms.author: vanto
manager: craigg
---
# Advanced Edit (Condition) Dialog Box
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  Use the **Advanced Edit** dialog box to create complex expressions for Policy-Based Management conditions.  
  
## Options  
 **Cell value**  
 Displays the function or expression that will be used for the cell value as you create it. When you click **OK**, the cell value will appear in the **Field** or **Value** cell in the condition expression box of the **Create New Condition** or the **Open Condition** dialog box on the **General** page.  
  
 **Functions and properties**  
 Displays the available functions and properties.  
  
 **Details**  
 Displays the information about the functions and properties, in the format: function signature, function description, return value, and example.  
  
## Syntax  
 Valid expressions must be in the following format:  
  
 `{property | function | constant}`  
  
 `{operator}`  
  
 `{property | function | constant}`  
  
## Examples  
 Some examples of valid expressions are as follows:  
  
-   *Property1*> 5  
  
-   *Property1*=*Property2*  
  
-   Add(5, Multiply(.2,*Property1*))<*Property2*  
  
-   *Sometext* IN *Property1*  
  
-   *Property1*< Fn(*Property2*)  
  
-   BitwiseAnd(*Property1*,*Property2*)= 0  
  
## Additional function information  
 The following sections provide additional information about the functions you can use to create complex expressions for Policy-Based Management conditions.  
  
> **IMPORTANT!** The functions that you can use to create Policy-Based Management conditions do not always use [!INCLUDE[tsql](../../includes/tsql-md.md)] syntax. Make sure that you follow the example syntax. For example, when you use the **DateAdd** or **DatePart** functions, you must enclose the *datepart* argument in single quotes.  
  
|Function|Signature|Description|Arguments|Return value|Example|  
|--------------|---------------|-----------------|---------------|------------------|-------------|  
|**Add()**|Numeric Add (Numeric *expression1*, Numeric *expression2*)|Adds two numbers.|*expression1* and *expression2* - Is any valid expression of any one of the data types in the numeric category, except the **bit** data type. Can be a constant, property, or function that returns a numeric type.|Returns the data type of the argument that has the greater precedence.|`Add(Property1, 5)`|  
|**Array()**|Array Array (VarArgs *expression*)|Creates an array from a list of values. Can be used with aggregate functions such as Sum() and Count().|*expression* - Is an expression that will be converted to an array.|The array|`Array(2,3,4,5,6)`|  
|**Avg()**|Numeric Avg (*VarArgs*)|Returns the average of the values in the argument list.|*VarArgs* - Is list of Variant expression of the exact numeric or approximate numeric data type category, except for the **bit** data type.|The return type is determined by the type of the evaluated result of expression.<br /><br /> If the expression result is **integer**, **decimal**, **money** and **smallmoney**, **float** and **real** category, the return types are **int**, **decimal**, **money**, and **float**; respectively.|`Avg(1.0, 2.0, 3.0, 4.0, 5.0)` returns `3.0` in this example.|  
|**BitwiseAnd()**|Numeric BitwiseAnd (Numeric *expression 1*, Numeric *expression2*)|Performs a bitwise logical AND operation between two integer values.|*expression1* and *expression2* - Is any valid expression of any one of the data types of the integer data type category.|Returns a value of integer data type category.|`BitwiseAnd(Property1, Property2)`|  
|**BitwiseOr()**|Numeric BitwiseOr (Numeric *expression1*, Numeric *expression2*)|Performs a bitwise logical OR operation between two specified integer values.|*expression1* and *expression2* - Is any valid expression of any one of the data types of the integer data type category.|Returns a value of integer data type category.|`BitwiseOr(Property1, Property2)`|  
|**Concatenate()**|String Concatenate (String *string1*, String *string2*)|Concatenates two strings.|*string1* and *string2* - Are the two strings that you want to concatenate. Can be any valid non-null string.|The concatenated string, with *string1* followed by *string2*.|`Concatenate("Hello", " World` `")` returns "`Hello World`".|  
|**Count()**|Numeric Count (*VarArgs*)|Returns the number of items in the argument list.|*VarArgs* - Is an expression of any type except **text**, **image**, and **ntext**.|Returns a value of integer data type category.|`Count(1.0, 2.0, 3.0, 4.0, 5.0)` returns `5` in this example.|  
|**DateAdd()**|DateTime DateAdd (String *datepart*, Numeric *number*, DateTime *date*)|Returns a new **datetime** value that is based on adding an interval to the specified date.|*datepart* - Is the parameter that specifies on which part of the date to return a new value. Some of the supported types are year(yy, yyyy), month(mm, m)and dayofyear(dy, y). For more information, see [DATEADD &#40;Transact-SQL&#41;](../../t-sql/functions/dateadd-transact-sql.md).<br /><br /> *number* - Is the value that is used to increment *datepart*.<br /><br /> *date* - Is an expression that returns a **datetime** value, or a character string in a date format.|Is the new **datetime** value that is based on adding an interval to the specified date.|**Example:** `DateAdd('day', 21, DateTime('2007-08-06 14:21:50'))` returns `'2007-08-27 14:21:50'` in this example.<br /><br /> The following are *dateparts* and abbreviations that are supported by this function:<br /><br /> **year**: yy, yyyy<br /><br /> **month**: mm, m<br /><br /> **dayofyear**: dy, y<br /><br /> **day**: dd, d<br /><br /> **week**: wk, ww<br /><br /> **weekday**: dw, w<br /><br /> **hour**: hh<br /><br /> **minute**: mi, n<br /><br /> **second**: ss, s<br /><br /> **millisecond**: ms|  
|**DatePart()**|Numeric DatePart (String *datepart*, DateTime *date*)|Returns an integer that represents the specified *datepart* of the specified date.|*datepart* - Is the parameter that specifies the part of the date to return. Some of the supported types are year(yy, yyyy), month (mm, m)and dayofyear(dy, y). For more information, see [DATEPART &#40;Transact-SQL&#41;](../../t-sql/functions/datepart-transact-sql.md).<br /><br /> *date* - Is an expression that returns a **datetime** value, or a character string in a date format.|Returns value of integer data type category that represents the specified *datepart* of the specified date.|`DatePart('month', DateTime('2007-08-06 14:21:50.620'))` returns `8` in this example.|  
|**DateTime()**|DateTime DateTime (String *dateString*)|Creates a datetime value from a string.|*dateString* - Is the datetime value as a string.|Returns a datatime value created from the input string.|`DateTime('3/12/2006')`|  
|**Divide()**|Numeric Divide (Numeric *expression_dividend*, Numeric *expression_divisor*)|Divides one number by another.|*expression_dividend* - Is the numeric expression to divide. The dividend can be any valid expression of any one of the data types of the numeric data type category, except the **datetime** data type.<br /><br /> *expression_divisor* - Is the numeric expression by which to divide the dividend. The divisor can be any valid expression of any one of the data types of the numeric data type category, except the **datetime** data type.|Returns the data type of the argument that has the greater precedence.|**Example:** `Divide(Property1, 2)`<br /><br /> Note: This will be a double operation. To do an integer compare, you must combine the results with `Round()`. For example: `Round(Divide(10, 3), 0) = 3`.|  
|**Enum()**|Numeric Enum (String *enumTypeName*, String *enumValueName*)|Creates an enum value from a string.|*enumTypeName* - Is the name of the enum type.<br /><br /> *enumValueName* - Is the value of the enum.|Returns the enum value as a numeric value.|`Enum('CompatibilityLevel','Version100')`|  
|**Escape()**|String Escape (String *replaceString*, String *stringToEscape*, String *escapeString*)|Escapes a substring of the input string with a given escape string.|*replaceString* - Is the input string.<br /><br /> *stringToEscape* - Is a substring of *replaceString*. This is the string that you want to add an escape string in front of.<br /><br /> *escapeString* - Is the escape string that you want to add in front of each instance of *stringToEscape*.|Returns a modified *replaceString* in which each instance of *stringToEscape* is preceded by *escapeString*.|`Escape("Hello", "l", "[")` returns "`He[l[lo`".|  
|**ExecuteSQL()**|Variant ExecuteSQL (String *returnType*, String *sqlQuery*)|Executes the [!INCLUDE[tsql](../../includes/tsql-md.md)] query against the target server.<br /><br /> For more information about ExecuteSql(), see [ExecuteSql() function](https://blogs.msdn.com/b/sqlpbm/archive/2008/07/03/executesql.aspx).|*returnType* - Specifies the return type of data returned by the [!INCLUDE[tsql](../../includes/tsql-md.md)] statement. The valid literals for *returnType* are as follows: **Numeric**, **String**, **Bool**, **DateTime**, **Array**, and **Guid**.<br /><br /> *sqlQuery* - Is the string that contains query to be executed.||`ExecuteSQL ('Numeric', 'SELECT COUNT(*) FROM msdb.dbo.sysjobs') <> 0`<br /><br /> Runs a scalar-valued Transact-SQL query against a target instance of SQL Server. Only one column can be specified in a `SELECT` statement; additional columns beyond the first are ignored. The resulting query should return only one row; additional rows beyond the first are ignored. If the query returns an empty set, then the condition expression built around `ExecuteSQL` will evaluate to false. `ExecuteSql` supports the **On demand** and **On schedule** evaluation modes.<br /><br /> -`@@ObjectName`:<br />                      Corresponds to the name field in [sys.objects](../../relational-databases/system-catalog-views/sys-objects-transact-sql.md). The variable will be replaced with the name of the current object.<br /><br /> -`@@SchemaName`:  Corresponds to the name field in [sys.schemas](../../relational-databases/system-catalog-views/schemas-catalog-views-sys-schemas.md). The variable will be replaced with the name of the schema for the current object, if applicable.<br /><br /> Note: To include a single quotation mark in an ExecuteSQL statement, escape the single quotation mark with a second single quotation mark. For example, to include a reference to a user named O'Brian, type O''Brian.|  
|**ExecuteWQL()**|Variant ExecuteWQL (string *returnType* , string *namespace*, string *wql*)|Executes the WQL script against the namespace that is provided. Select statement can contain only a single return column. If more than one column is provided, error will be thrown.|*returnType* - Specifies the return type of data that is returned by the WQL. The valid literals are **Numeric**, **String**, **Bool**, **DateTime**, **Array**, and **Guid**.<br /><br /> *namespace* - Is the WMI Namespace to execute against.<br /><br /> *wql* - Is the string that contains the WQL to be executed.||`ExecuteWQL('Numeric', 'root\CIMV2', 'select NumberOfProcessors from win32_ComputerSystem') <> 0`|  
|**False()**|Bool False()|Returns the Boolean value FALSE.|None|Returns the Boolean value FALSE.|`IsDatabaseMailEnabled = False()`|  
|**GetDate()**|DateTime GetDate()|Returns the system date.|None|Returns the system date as DateTime.|`@DateLastModified = GetDate()`|  
|**Guid()**|Guid Guid(String *guidString*)|Returns a GUID from a string.|*guidString* - Is the string representation of the GUID to be created.|Returns the GUID created from the string.|`Guid('12340000-0000-3455-0000-000000000454')`|  
|**IsNull()**|Variant IsNull (Variant *check_expression*, Variant *replacement_value*)|The value of *check_expression* is returned if it is not NULL; otherwise, *replacement_value* is returned. If the types are different, *replacement_value* is implicitly converted to the type of *check_expression*.|*check_expression* - Is the expression to be checked for NULL. *check_expression* can be of any Policy-Based Management supported types: Numeric, String, Bool, DateTime, Array, and Guid.<br /><br /> *replacement_value* - Is the expression to be returned if *check_expression* is NULL. *replacement_value* must be of a type that is implicitly converted to the type of *check_expression*.|The return type is type of *check_expression* if *check_expression* is not NULL; otherwise, the type of *replacement_value* is returned.||  
|**Len()**|Numeric Len (*string_expression*)|Returns the number of characters, of the given string expression, excluding trailing blanks.|*string_expression* - Is the string expression to be evaluated.|Returns a value of integer data type category.|`Len('Hello')` returns `5` in this example.|  
|**Lower()**|String Lower (String*_expression*)|Returns the string after converting all uppercase characters to lower case.|*expression* - Is the source string expression.|Returns a string that represents the source string expression after all uppercase characters are converted to lowercase.|`Len('HeLlO')` returns `'hello'` in this example.|  
|**Mod()**|Numeric Mod (Numeric *expression_dividend*, Numeric *expression_divisor*)|Provides the integer remainder after dividing the first numeric expression by the second numeric expression.|*expression_dividend* - Is the numeric expression to divide. *expression_dividend* must be a valid expression of any one of the data types in the integer or the numeric data type categories.<br /><br /> *expression_divisor* - Is the numeric expression to divide the dividend by. *expression_divisor* must be any valid expression of any one of the data types in the integer or the numeric data type categories.|Returns a value of integer data type category.|`Mod(Property1, 3)`|  
|**Multiply()**|Numeric Multiply (Numeric *expression1*, Numeric *expression2*)|Multiplies two expressions.|*expression1* and *expression2* - Is any valid expression of any one of the data types in the numeric category, except the **datetime** data type.|Returns the data type of the argument that has the greater precedence.|`Multiply(Property1, .20)`|  
|**Power()**|Numeric Power (Numeric *numeric_expression*, Numeric *expression_power*)|Returns the value of the specified expression to the specified power.|*numeric_expression* - Is an expression of the exact numeric or approximate numeric data type category, except for the bit data type.<br /><br /> *expression_power* - Is the power to which to raise *numeric_expression*. *expression_power* can be an expression of the exact numeric or approximate numeric data type category, except for the **bit** data type.|Return type is same as *numeric_expression*.|`Power(Property1, 3)`|  
|**Round()**|Numeric Round (Numeric *expression*, Numeric *expression_precision*)|Returns a numeric expression that is rounded to the specified length or precision.|*expression* - Is an expression of the exact numeric or approximate numeric data type category, except for the **bit** data type.<br /><br /> *expression_precision* - Is the precision to which expression is to be rounded. When *expression_precision* is a positive number, *numeric_expression* is rounded to the number of decimal positions that is specified by length. When *expression_precision* is a negative number, *numeric_expression* is rounded on the left side of the decimal point, as specified by *expression_precision*.|Returns the same type as *numeric_expression*.|`Round(5.333, 0)`|  
|**String()**|String String (Variant*_expression*)|Converts a variant to a string.|*expression* - Is the variant expression to be converted to a string.|Returns the string value of the variant expression.|`String(4)`|  
|**Sum()**|Numeric Sum (*VarArgs*)|Returns the sum of all the values in the argument list. Sum can be used with numeric values.|*VarArgs*- Is a list of Variant expression of the exact numeric or approximate numeric data type category, except for the **bit** data type.|Returns the summation of all expression values in the most precise expression data type.<br /><br /> If the expression result is **integer**, **numeric**, **money** and **small money**, **float** and **real** category, the return types are **int**, **numeric**, **money**, and **float**; respectively.|`Sum(1.0, 2.0, 3.0, 4.0, 5.0)` returns `15` in this example.|  
|**True()**|Bool TRUE()|Returns the Boolean value TRUE.||Returns the Boolean value TRUE.|`IsDatabaseMailEnabled = True()`|  
|**Upper()**|String Upper (String*_expression*)|Returns the string after converting all lowercase characters to uppercase.|*expression* - Is the source string expression.|Returns a string that represents the source string expression after all lowercase characters are converted to uppercase.|`Upper('HeLlO')` returns `'HELLO'` in this example.|  
  
## See also  
 [Create New Condition or Open Condition Dialog Box, General Page](../../relational-databases/policy-based-management/create-new-condition-or-open-condition-dialog-box-general-page.md)   
 [Administer Servers by Using Policy-Based Management](../../relational-databases/policy-based-management/administer-servers-by-using-policy-based-management.md)  
  
  
