---
title: "FORMAT_STRING Contents (MDX) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
helpviewer_keywords: 
  - "formats [Analysis Services], string values"
  - "VALUE property"
  - "formats [Analysis Services], numeric values"
  - "FORMATTED_VALUE property"
  - "FORMAT_STRING contents"
ms.assetid: c354c938-0328-4b8e-adc5-3b52fd2a7152
author: minewiskan
ms.author: owend
manager: craigg
---
# FORMAT_STRING Contents (MDX)
  The `FORMAT_STRING` cell property formats the `VALUE` cell property, creating the value for the `FORMATTED_VALUE` cell property. The `FORMAT_STRING` cell property handles both string and numeric raw values, applying a format expression against the value to return a formatted value for the `FORMATTED_VALUE` cell property. The following tables detail the syntax and formatting characters used to handle string and numeric values.  
  
## String Values  
 A format expression for strings can have one section or two sections separated by a semicolon (;).  
  
|Usage|Result|  
|-----------|------------|  
|One section|The format applies to all string values.|  
|Two sections|The first section applies to string data, whereas the second section applies to null values and zero-length strings ("").|  
  
 The characters described in the following table can appear in the format string for character strings.  
  
|Character|Description|  
|---------------|-----------------|  
|@|Represents a character placeholder that displays a character or a space. If the string has a character in the position where the at sign (@) appears in the format string, the formatted string displays the character. Otherwise, the formatted string displays a space in that position. Placeholders are filled from right to left unless there is an exclamation point (!) in the format string.|  
|&|Represents a character placeholder that displays a character or nothing. If the string has a character in the position where the ampersand (&) appears, the formatted string displays the character. Otherwise, the formatted string displays nothing. Placeholders are filled from right to left unless there is an exclamation point (!) in the format string.|  
|\<|Forces lowercase. The formatted string displays all characters in lowercase format.|  
|>|Forces uppercase. The formatted string displays all characters in uppercase format.|  
|!|Forces left-to-right fill of placeholders. (The default is to fill placeholders from right to left.)|  
  
## Numeric Values  
 A user-defined format expression for numbers can have anywhere from one to four sections separated by semicolons. If the format argument contains one of the named numeric formats, only one section is allowed.  
  
|Usage|Result|  
|-----------|------------|  
|One section|The format expression applies to all values.|  
|Two sections|The first section applies to positive values and zeros, the second to negative values.|  
|Three sections|The first section applies to positive values, the second to negative values, and the third to zeros.|  
|Four sections|The first section applies to positive values, the second to negative values, the third to zeros, and the fourth to null values.|  
  
 The following example has two sections. The first section defines the format for positive values and zeros, and the second section defines the format for negative values.  
  
```  
"$#,##0;($#,##0)"  
```  
  
 If you include semicolons with nothing between them, the missing section prints using the format of the positive value. For example, the following format displays positive and negative values using the format in the first section and displays "Zero" if the value is zero:  
  
```  
"$#,##0;;\Z\e\r\o"  
```  
  
 The following table identifies the characters that can appear in the format string for numeric formats.  
  
|Character|Description|  
|---------------|-----------------|  
|None|Displays the number without any formatting.|  
|**0**|Represents a digit placeholder that displays a digit or a zero (0).<br /><br /> If the number has a digit in the position where the zero appears in the format string, the formatted value displays the digit. Otherwise, the formatted value displays a zero in that position.<br /><br /> If the number has fewer digits than there are zeros (on either side of the decimal) in the format string, the formatted value displays leading or trailing zeros.<br /><br /> If the number has more digits to the right of the decimal separator than there are zeros to the right of the decimal separator in the format expression, the formatted value rounds the number to as many decimal places as there are zeros.<br /><br /> If the number has more digits to the left of the decimal separator than there are zeros to the left of the decimal separator in the format expression, the formatted value displays the additional digits without modification.|  
|**#**|Represents a digit placeholder that displays a digit or nothing.<br /><br /> If the expression has a digit in the position where the number sign (**#**) appears in the format string, the formatted value displays the digit. Otherwise, the formatted value displays nothing in that position.<br /><br /> The number sign (**#**) placeholder works like the zero (**0**) digit placeholder except that leading and trailing zeros are not displayed if the number has the same or fewer digits than there are **#** characters on either side of the decimal separator in the format expression.|  
|**.**|Represents a decimal placeholder that determines how many digits are displayed to the left and right of the decimal separator.<br /><br /> If the format expression contains only number sign (**#**) characters to the left of the period (**.**), numbers smaller than 1 start with a decimal separator. To display a leading zero displayed with fractional numbers, use zero (0) as the first digit placeholder to the left of the decimal separator.<br /><br /> The actual character used as a decimal placeholder in the formatted output depends on the number format recognized by the computer system.<br /><br /> Note: In some locales, a comma is used as the decimal separator.|  
|**%**|Represents a percentage placeholder. The expression is multiplied by 100. The percent character (**%**) is inserted in the position where the percentage appears in the format string.|  
|**,**|Represents a thousand separator that separates thousands from hundreds within a number that has four or more places to the left of the decimal separator.<br /><br /> Standard use of the thousand separator is specified if the format contains a thousand separator enclosed in digit placeholders (**0** or **#**).<br /><br /> Two adjacent thousand separators, or a thousand separator immediately to the left of the decimal separator (whether or not a decimal is specified), means "scale the number by dividing the number by 1000, rounding as required." For example, you can use the format string "**##0**,," to represent 100 million as 100. Numbers smaller than 1 million are displayed as 0. Two adjacent thousand separators in any position other than immediately to the left of the decimal separator are treated as specifying the use of a thousand separator.<br /><br /> The actual character used as the thousand separator in the formatted output depends on the number format recognized by the computer system.<br /><br /> Note: In some locales, a period is used as the thousand separator.|  
|**:**|Represents a time separator that separates hours, minutes, and seconds when time values are formatted.<br /><br /> Note: In some locales, other characters may be used as the time separator.<br /><br /> The actual character used as the time separator in formatted output is determined by the system settings on the computer.|  
|**/**|Represents a date separator that separates the day, month, and year when date values are formatted.<br /><br /> The actual character used as the date separator in formatted output is determined by the system settings on the computer.<br /><br /> Note: In some locales, other characters may used as the date separator.|  
|**E- E+ e- e+**|Represents scientific format.<br /><br /> If the format expression contains at least one digit placeholder (**0** or **#**) to the right of **E-**, **E+**, **e-**, or **e+**, the formatted value displays in scientific format and E or e is inserted between the number and the number's exponent. The number of digit placeholders to the right determines the number of digits in the exponent. Use **E-** or **e-** to include a minus sign next to negative exponents. Use **E+** or **e+** to include a minus sign next to negative exponents and a plus sign next to positive exponents.|  
|**- + $ ( )**|Displays a literal character.<br /><br /> To display a character other than one of those listed, put a backslash (**\\**)  before the character or enclose the character in double quotation marks (**" "**).|  
|**\\**|Displays the next character in the format string.<br /><br /> To display a character that has special meaning as a literal character, put a backslash (**\\**) before the character. The backslash itself is not displayed. Using a backslash is the same as enclosing the next character in double quotation marks. To display a backslash, use two backslashes (**\\\\**). Examples of characters that cannot be displayed as literal characters include the following characters:<br /><br /> The date-formatting and time-formatting characters-**a**, **c**, **d**, **h**, **m**, **n**, **p**, **q**, **s**, **t**, **w**, **y**, **/**, and **:**<br /><br /> The numeric-formatting characters-**#**, **0**, **%**, **E**, **e**, **comma**, and **period**<br /><br /> The string-formatting characters-**@**, **&**, **\<**, **>**, and **!**|  
|**"ABC"**|Displays the string inside the double quotation marks (**" "**).<br /><br /> To include a string in format from within code, use Chr(**34**) to enclose the text. (The character code for a double quotation mark is **34**.)|  
  
### Named Numeric Formats  
 The following table identifies the predefined numeric format names:  
  
|Format name|Description|  
|-----------------|-----------------|  
|`General Number`|Displays the number with no thousand separator.|  
|`Currency`|Displays the number with a thousand separator, if appropriate. Displays two digits to the right of the decimal separator. Output is based on system locale settings.|  
|`Fixed`|Displays at least one digit to the left and two digits to the right of the decimal separator.|  
|`Standard`|Displays the number with thousand separator, at least one digit to the left and two digits to the right of the decimal separator.|  
|`Percent`|Displays the number multiplied by 100 with a percent sign (%) appended to the right. Always displays two digits to the right of the decimal separator.|  
|`Scientific`|Uses standard scientific notation.|  
|`Yes/No`|Displays No if the number is 0; otherwise, displays Yes.|  
|`True/False`|Displays False if the number is 0; otherwise, displays True.|  
|`On/Off`|Displays Off if the number is 0; otherwise, displays On.|  
  
## Date Values  
 The following table identifies characters that can appear in the format string for date/time formats.  
  
|Character|Description|  
|---------------|-----------------|  
|**:**|Represents a time separator that separates hours, minutes, and seconds when time values are formatted.<br /><br /> The actual character used as the time separator in formatted output is determined by the system settings of the computer.<br /><br /> Note: In some locales, other characters may used as the time separator.|  
|**/**|Represents a date separator that separates the day, month, and year when date values are formatted.<br /><br /> The actual character used as the date separator in formatted output is determined by the system settings of the computer.<br /><br /> Note: In some locales, other characters may be used to represent the date separator|  
|**C**|Displays the date as **ddddd** and displays the time as **ttttt**, in that order.<br /><br /> Displays only date information if there is no fractional part to the date serial number. Displays only time information if there is no integer portion.|  
|**d**|Displays the day as a number without a leading zero (1-31).|  
|**dd**|Displays the day as a number with a leading zero (01-31).|  
|**ddd**|Displays the day as an abbreviation (Sun-Sat).|  
|**dddd**|Displays the day as a full name (Sunday-Saturday).|  
|**ddddd**|Displays the date as a complete date (including day, month, and year), formatted according to your system's short date format setting.<br /><br /> For Microsoft Windows, the default short date format is **m/d/yy**.|  
|**dddddd**|Displays a date serial number as a complete date (including day, month, and year), formatted according to the long date setting recognized by the computer system.<br /><br /> For Windows, the default long date format is **mmmm dd, yyyy**.|  
|**w**|Displays the day of the week as a number (1 for Sunday through 7 for Saturday).|  
|**ww**|Displays the week of the year as a number (1-54).|  
|**m**|Displays the month as a number without a leading zero (1-12).<br /><br /> If **m** immediately follows **h** or **hh**, the minute instead of the month is displayed.|  
|**mm**|Displays the month as a number with a leading zero (01-12).<br /><br /> If **m** immediately follows **h** or **hh**, the minute instead of the month is displayed.|  
|**mmm**|Displays the month as an abbreviation (Jan-Dec).|  
|**mmmm**|Displays the month as a full month name (January-December).|  
|**q**|Displays the quarter of the year as a number (1-4).|  
|**y**|Displays the day of the year as a number (1-366).|  
|**yy**|Displays the year as a two-digit number (00-99).|  
|**yyyy**|Displays the year as a four-digit number (100-9999).|  
|**h**|Displays the hour as a number without leading zeros (0-23).|  
|**hh**|Displays the hour as a number with leading zeros (00-23).|  
|**n**|Displays the minute as a number without leading zeros (0-59).|  
|**nn**|Displays the minute as a number with leading zeros (00-59).|  
|**s**|Displays the second as a number without leading zeros (0-59).|  
|**ss**|Displays the second as a number with leading zeros (00-59).|  
|**t t t t t**|Displays a time as a complete time (including hour, minute, and second), formatted using the time separator defined by the time format recognized by the computer system.<br /><br /> A leading zero is displayed if the leading zero option is selected, and the time is earlier than 10:00 in either the A.M. or the P.M. cycle. For example, 09:59,<br /><br /> For Windows, the default time format is **h:mm:ss**.|  
|**AM/PM**|Displays an uppercase **AM** with any hour from midnight until noon; displays an uppercase **PM** with any hour from noon until midnight.<br /><br /> Note: Uses the 12-hour clock.|  
|**am/pm**|Displays a lowercase **am** with any hour from midnight until noon; displays a lowercase **pm** with any hour from noon until midnight.<br /><br /> Note: Uses the 12-hour clock.|  
|**A/P**|Displays an uppercase **A** with any hour from midnight until noon; displays an uppercase **P** with any hour from noon until midnight.<br /><br /> Note: Uses the 12-hour clock.|  
|**a/p**|Displays a lowercase **a** with any hour from midnight until noon; displays a lowercase **p** with any hour from noon until midnight.<br /><br /> Note: Uses the 12-hour clock.|  
|**AMPM**|Displays the AM string literal as defined by the computer system with any hour from midnight until noon; displays the PM string literal as defined by the computer system with any hour from noon until midnight.<br /><br /> Note: Uses the 12-hour clock.<br /><br /> **AMPM** can be either uppercase or lowercase, but the case of the string displayed matches the string as defined by the system settings of the computer.<br /><br /> For Windows, the default format is **AM/PM**.|  
  
### Named Date Formats  
 The following table identifies the predefined date and time format names:  
  
|Format Name|Description|  
|-----------------|-----------------|  
|`General Date`|Displays a date and/or time. For real numbers, displays a date and time, for example, 4/3/93 05:34 PM. If there is no fractional part, displays only a date, for example, 4/3/93. If there is no integer part, displays a time only, for example, 05:34 PM. The format of the date display is determined by your system settings.|  
|`Long Date`|Displays a date according to your system's long date format.|  
|`Medium Date`|Displays a date using the medium date format appropriate for the language version of the host application.|  
|`Short Date`|Displays a date using your system's short date format.|  
|`Long Time`|Displays a time using your system's long time format; includes hours, minutes, and seconds.|  
|`Medium Time`|Displays a time in the 12-hour format using hours and minutes and the AM/PM designator.|  
|`Short Time`|Displays a time using the 24-hour format, for example, 17:45.|  
  
## See Also  
 [LANGUAGE and FORMAT_STRING on FORMATED_VALUE](mdx-cell-properties-formatted-value-property.md)   
 [Using Cell Properties &#40;MDX&#41;](mdx-cell-properties-using-cell-properties.md)   
 [Creating and Using Property Values &#40;MDX&#41;](../../creating-and-using-property-values-mdx.md)   
 [MDX Query Fundamentals &#40;Analysis Services&#41;](mdx-query-fundamentals-analysis-services.md)  
  
  
