---
description: "Integration Services Data Types"
title: "Integration Services Data Types | Microsoft Docs"
ms.custom: ""
ms.date: "03/16/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: integration-services
ms.topic: conceptual
helpviewer_keywords: 
  - "modifying data types"
  - "data types [Integration Services], listed"
  - "data types [Integration Services]"
  - "column data types [Integration Services]"
  - "SSIS, data types"
  - "Integration Services, data types"
  - "SQL Server Integration Services, data types"
ms.assetid: 896fc3e8-3aa6-4396-ba82-5d7741cffa56
author: chugugrace
ms.author: chugu
---
# Integration Services Data Types

[!INCLUDE[sqlserver-ssis](../../includes/applies-to-version/sqlserver-ssis.md)]


  When data enters a data flow in a package, the source that extracts the data converts the data to an [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] data type. Numeric data is assigned a numeric data type, string data is assigned a character data type, and dates are assigned a date data type. Other data, such as GUIDs and Binary Large Object Blocks (BLOBs), are also assigned appropriate [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] data types. If data has a data type that is not convertible to an [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] data type, an error occurs.  
  
 Some data flow components convert data types between the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] data types and the managed data types of the [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[dnprdnshort](../../includes/dnprdnshort-md.md)]. For more information about the mapping between [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] and managed data types, see [Working with Data Types in the Data Flow](../../integration-services/extending-packages-custom-objects/data-flow/working-with-data-types-in-the-data-flow.md).  
  
 The following table lists the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] data types. Some of the data types in the table have precision and scale information that applies to them. For more information about precision and scale, see [Precision, Scale, and Length &#40;Transact-SQL&#41;](../../t-sql/data-types/precision-scale-and-length-transact-sql.md).  
  
|Data type|Description|  
|---------------|-----------------|  
|DT_BOOL|A Boolean value.|  
|DT_BYTES|A binary data value. The length is variable and the maximum length is 8000 bytes.|  
|DT_CY|A currency value. This data type is an eight-byte signed integer with a scale of 4 and a maximum precision of 19 digits.|  
|DT_DATE|A date structure that consists of year, month, day, hour, minute, seconds, and fractional seconds.  The fractional seconds have a fixed scale of 7 digits.<br /><br /> The DT_DATE data type is implemented using an 8-byte floating-point number. Days are represented by whole number increments, starting with 30 December 1899, and midnight as time zero. Hour values are expressed as the absolute value of the fractional part of the number. However, a floating point value cannot represent all real values; therefore, there are limits on the range of dates that can be presented in DT_DATE.<br /><br /> On the other hand, DT_DBTIMESTAMP is represented by a structure that internally has individual fields for year, month, day, hours, minutes, seconds, and milliseconds. This data type has larger limits on ranges of the dates it can present.|  
|DT_DBDATE|A date structure that consists of year, month, and day.|  
|DT_DBTIME|A time structure that consists of hour, minute, and second.|  
|DT_DBTIME2|A time structure that consists of hour, minute, second, and fractional seconds. The fractional seconds have a maximum scale of 7 digits.|  
|DT_DBTIMESTAMP|A timestamp structure that consists of year, month, day, hour, minute, second, and fractional seconds. The fractional seconds have a maximum scale of 3 digits.|  
|DT_DBTIMESTAMP2|A timestamp structure that consists of year, month, day, hour, minute, second, and fractional seconds. The fractional seconds have a maximum scale of 7 digits.|  
|DT_DBTIMESTAMPOFFSET|A timestamp structure that consists of year, month, day, hour, minute, second, and fractional seconds. The fractional seconds have a maximum scale of 7 digits.<br /><br /> Unlike the DT_DBTIMESTAMP and DT_DBTIMESTAMP2 data types, the DT_DBTIMESTAMPOFFSET data type has a time zone offset. This offset specifies the number of hours and minutes that the time is offset from the Coordinated Universal Time (UTC). The time zone offset is used by the system to obtain the local time.<br /><br /> The time zone offset must include a sign, plus or minus, to indicate whether the offset is added or subtracted from the UTC. The valid number of hours offset is between -14 and +14. The sign for the minute offset depends on the sign for the hour offset:<br /><br /> If the sign of the hour offset is negative, the minute offset must be negative or zero.<br /><br /> If the sign for the hour offset is positive, the minute offset must be positive or zero.<br /><br /> If the sign for the hour offset is zero, the minute offset can be any value from negative 0.59 to positive 0.59.|  
|DT_DECIMAL|An exact numeric value with a fixed precision and a fixed scale. This data type is a 12-byte unsigned integer with a separate sign, a scale of 0 to 28, and a maximum precision of 29.|  
|DT_FILETIME|A 64-bit value that represents the number of 100-nanosecond intervals since January 1, 1601. The fractional seconds have a maximum scale of 3 digits.|  
|DT_GUID|A globally unique identifier (GUID).|  
|DT_I1|A one-byte, signed integer.|  
|DT_I2|A two-byte, signed integer.|  
|DT_I4|A four-byte, signed integer.|  
|DT_I8|An eight-byte, signed integer.|  
|DT_NUMERIC|An exact numeric value with a fixed precision and scale. This data type is a 16-byte unsigned integer with a separate sign, a scale of 0 - 38, and a maximum precision of 38.|  
|DT_R4|A single-precision floating-point value.|  
|DT_R8|A double-precision floating-point value.|  
|DT_STR|A null-terminated [!INCLUDE[vcpransi](../../includes/vcpransi-md.md)]/MBCS character string with a maximum length of 8000 characters. (If a column value contains additional null terminators, the string will be truncated at the occurrence of the first null.)|  
|DT_UI1|A one-byte, unsigned integer.|  
|DT_UI2|A two-byte, unsigned integer.|  
|DT_UI4|A four-byte, unsigned integer.|  
|DT_UI8|An eight-byte, unsigned integer.|  
|DT_WSTR|A null-terminated Unicode character string with a maximum length of 4000 characters. (If a column value contains additional null terminators, the string will be truncated at the occurrence of the first null.)|  
|DT_IMAGE|A binary value with a maximum size of 2^31-1 (2,147,483,647) bytes. .|  
|DT_NTEXT|A Unicode character string with a maximum length of 2^30 - 1 (1,073,741,823) characters.|  
|DT_TEXT|An [!INCLUDE[vcpransi](../../includes/vcpransi-md.md)]/MBCS character string with a maximum length of 2^31-1 (2,147,483,647) characters.|  
  
## Conversion of Data Types  
 If the data in a column does not require the full width allocated by the source data type, you might want to change the data type of the column. Making each data row as narrow as possible helps optimize performance when transferring data because the narrower each row is, the faster the data is moved from source to destination.  
  
 [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] includes a complete set of numeric data types, so that you can match the data type closely to the size of the data. For example, if the values in a column with a DT_UI8 data type are always integers between 0 and 3000, you can change the data type to DT_UI2. Similarly, if a column with the DT_CY data type can meet the package data requirements by using an integer data type instead, you can change the data type to DT_I4.  
  
 You can change the data type of a column in the following ways:  
  
-   Use an expression to implicitly convert data types. For more information, see [Integration Services Data Types in Expressions](../../integration-services/expressions/integration-services-data-types-in-expressions.md), [Integration Services Data Types in Expressions](../../integration-services/expressions/integration-services-data-types-in-expressions.md), and [Integration Services &#40;SSIS&#41; Expressions](../../integration-services/expressions/integration-services-ssis-expressions.md).  
  
-   Use the cast operator to convert data types. For more information, see [Cast &#40;SSIS Expression&#41;](../../integration-services/expressions/cast-ssis-expression.md).  
  
-   Use the Data Conversion transformation to cast the data type of a column from one data type to a different data type. For more information, see [Data Conversion Transformation](../../integration-services/data-flow/transformations/data-conversion-transformation.md).  
  
-   Use the Derived Column transformation to create a copy of a column that has a different data type than the original column. For more information, see [Derived Column Transformation](../../integration-services/data-flow/transformations/derived-column-transformation.md).  
  
### Converting Between Strings and Date/Time Data Types  
 The following table lists the results of casting or converting between date/time data types and strings:  
  
-   When you use the cast operator or the Data Conversion transformation, the date or time type data type will be converted to the corresponding string format. For example, the DT_DBTIME data type will be converted to a string that has the format, "hh:mm:ss".  
  
-   When you want to convert from a string to a date or time data type, the string must use the string format that corresponds to the appropriate date or time data type. For example, to successfully convert some date strings to the DT_DBDATE data type, these date strings must be in the format, "yyyy-mm-dd".  
  
    |Data type|String format|  
    |---------------|-------------------|  
    |DT_DBDATE|yyyy-mm-dd|  
    |DT_FILETIME|yyyy-mm-dd hh:mm:ss:fff|  
    |DT_DBTIME|hh:mm:ss|  
    |DT_DBTIME2|hh:mm:ss[.fffffff]|  
    |DT_DBTIMESTAMP|yyyy-mm-dd hh:mm:ss[.fff]|  
    |DT_DBTIMESTAMP2|yyyy-mm-dd hh:mm:ss[.fffffff]|  
    |DT_DBTIMESTAMPOFFSET|yyyy-mm-dd hh:mm:ss[.fffffff] [{+&#124;-} hh:mm]|  
  
 In the format for DT_FILETIME and DT_DBTIMESTAMP fff is a value between 0 and 999 that represents fractional seconds.  
  
 In the date format for DT_DBTIMESTAMP2, DT_DBTIME2, and DT_DBTIMESTAMPOFFSET, fffffff is a value between 0 and 9999999 that represents fractional seconds.  
  
 The date format for DT_DBTIMESTAMPOFFSET also includes a time zone element. There is a space between the time element and the time zone element.  
  
### Converting Date/Time Data Types  
 You can change the data type on a column with date/time data to extract the date or the time part of the data. The following tables list the results of changing from one date/time data type to another date/time data type.  
  
#### Converting from DT_FILETIME  
  
|Convert DT_FILETIME to|Result|  
|-----------------------------|------------|  
|DT_FILETIME|No change.|  
|DT_DATE|Converts the data type.|  
|DT_DBDATE|Removes the time value.|  
|DT_DBTIME|Removes the date value.<br /><br /> Removes the fractional second value when its scale is greater than the number of fractional digits that the DT_DBTIME data type can contain. After removing the fractional second value, generates a report about this data truncation. For more information, see [Error Handling in Data](../../integration-services/data-flow/error-handling-in-data.md).|  
|DT_DBTIME2|Removes the date value represented by the DT_FILETIME data type.<br /><br /> Removes the fractional second value when its scale is greater than the number of fractional second digits that the DT_DBTIME2 data type can contain. After removing the fractional second value, generates a report about this data truncation. For more information, see [Error Handling in Data](../../integration-services/data-flow/error-handling-in-data.md).|  
|DT_DBTIMESTAMP|Converts the data type.|  
|DT_DBTIMESTAMP2|Removes the fractional second value when its scale is greater than the number of fractional second digits that the DT_DBTIMESTAMP2 data type can contain. After removing the fractional second value, generates a report about this data truncation. For more information, see [Error Handling in Data](../../integration-services/data-flow/error-handling-in-data.md).|  
|DT_DBTIMESTAMPOFFSET|Sets the time zone field in the DT_DBTIMESTAMPOFFSET data type to zero.<br /><br /> Removes the fractional second value when its scale is greater than the number of fractional second digits that the DT_DBTIMESTAMPOFFSET data type can contain. After removing the fractional second value, generates a report about this data truncation. For more information, see [Error Handling in Data](../../integration-services/data-flow/error-handling-in-data.md).|  
  
#### Converting from DT_DATE  
  
|Convert DT_DATE to|Result|  
|-------------------------|------------|  
|DT_FILETIME|Converts the data type.|  
|DT_DATE|No change.|  
|DT_DBDATE|Removes the time value represented by the DT_DATA data type.|  
|DT_DBTIME|Removes the date value represented by the DT_DATE data type.|  
|DT_DBTIME2|Removes the date value represented by the DT_DATE data type.|  
|DT_DBTIMESTAMP|Converts the data type.|  
|DT_DBTIMESTAMP2|Converts the data type.|  
|DT_DBTIMESTAMPOFFSET|Sets the time zone field in the DT_DBTIMESTAMPOFFSET data type to zero.|  
  
#### Converting from DT_DBDATE  
  
|Convert DT_DBDATE to|Result|  
|---------------------------|------------|  
|DT_FILETIME|Sets the time fields in the DT_FILETIME data type to zero.|  
|DT_DATE|Sets the time fields in the DT_DATE data type to zero.|  
|DT_DBDATE|No change.|  
|DT_DBTIME|Sets the time fields in the DT_DBTIME data type to zero.|  
|DT_DBTIME2|Sets the time fields in the DT_DBTIME2 data type to zero.|  
|DT_DBTIMESTAMP|Sets the time fields in the DT_DBTIMESTAMP data type to zero.|  
|DT_DBTIMESTAMP2|Sets the time fields in the DT_DBTIMESTAMP data type to zero.|  
|DT_DBTIMESTAMPOFFSET|Sets the time fields and the time zone field in the DT_DBTIMESTAMPOFFSET data type to zero.|  
  
#### Converting from DT_DBTIME  
  
|Convert DT_DBTIME to|Result|  
|---------------------------|------------|  
|DT_FILETIME|Sets the date field in the DT_FILETIME data type to the current date.|  
|DT_DATE|Sets the date field in the DT_DATE data type to the current date.|  
|DT_DBDATE|Sets the date field in the DT_DBDATE data type to the current date.|  
|DT_DBTIME|No change.|  
|DT_DBTIME2|Converts the data type.|  
|DT_DBTIMESTAMP|Sets the date field in the DT_DBTIMESTAMP data type to the current date.|  
|DT_DBTIMESTAMP2|Sets the date field in the DT_DBTIMESTAMP2 data type to the current date.|  
|DT_DBTIMESTAMPOFFSET|Sets the date field and the time zone field in the DT_DBTIMESTAMPOFFSET data type to the current date and to zero, respectively.|  
  
#### Converting from DT_DBTIME2  
  
|Convert DT_DBTIME2 to|Result|  
|----------------------------|------------|  
|DT_FILETIME|Sets the date field in the DT_FILETIME data type to the current date.<br /><br /> Removes the fractional second value when its scale is greater than the number of fractional second digits that the DT_FILETIME data type can contain. After removing the fractional second value, generates a report about this data truncation. For more information, see [Error Handling in Data](../../integration-services/data-flow/error-handling-in-data.md).|  
|DT_DATE|Sets the date field of the DT_DATE data type to the current date.<br /><br /> Removes the fractional second value when its scale is greater than the number of fractional second digits that the DT_DATE data type can contain. After removing the fractional second value, generates a report about this data truncation. For more information, see [Error Handling in Data](../../integration-services/data-flow/error-handling-in-data.md).|  
|DT_DBDATE|Sets the date field of the DT_DBDATE data type to the current date.|  
|DT_DBTIME|Removes the fractional second value when its scale is greater than the number of fractional second digits that the DT_DBTIME data type can contain. After removing the fractional second value, generates a report about this data truncation. For more information, see [Error Handling in Data](../../integration-services/data-flow/error-handling-in-data.md).|  
|DT_DBTIME2|Removes the fractional second value when its scale is greater than the number of fractional second digits that the destination DT_DBTIME2 data type can contain. After removing the fractional second value, generates a report about this data truncation. For more information, see [Error Handling in Data](../../integration-services/data-flow/error-handling-in-data.md).|  
|DT_DBTIMESTAMP|Set the date field in the DT_DBTIMESTAMP data type to the current date.<br /><br /> Removes the fractional second value when its scale is greater than the number of fractional second digits that the DT_DBTIMESTAMP data type can contain. After removing the fractional second value, generates a report about this data truncation. For more information, see [Error Handling in Data](../../integration-services/data-flow/error-handling-in-data.md).|  
|DT_DBTIMESTAMP2|Sets the date field in the DT_DBTIMESTAMP2 data type to the current date.<br /><br /> Removes the fractional second value when its scale is greater than the number of fractional second digits that the DT_DBTIMESTAMP2 data type can contain. After removing the fractional second value, generates a report about this data truncation. For more information, see [Error Handling in Data](../../integration-services/data-flow/error-handling-in-data.md).|  
|DT_DBTIMESTAMPOFFSET|Sets the date field and the time zone field in the DT_DBTIMESTAMPOFFSET data type to the current date and to zero, respectively.<br /><br /> Removes the fractional second value when its scale is greater than the number of fractional second digits that the DT_DBTIMESTAMPOFFSET data type can contain. After removing the fractional second value, generates a report about this data truncation. For more information, see [Error Handling in Data](../../integration-services/data-flow/error-handling-in-data.md).|  
  
#### Converting from DT_DBTIMESTAMP  
  
|Convert DT_DBTIMESTAMP to|Result|  
|--------------------------------|------------|  
|DT_FILETIME|Converts the data type.|  
|DT_DATE|If a value represented by the DT_DBTIMESTAMP data type overflows the range of the DT_DATE data type, returns the DB_E_DATAOVERFLOW error. For more information, see [Error Handling in Data](../../integration-services/data-flow/error-handling-in-data.md).|  
|DT_DBDATE|Removes the time value represented by the DT_DBTIMESTAMP data type.|  
|DT_DBTIME|Removes the date value represented by the DT_DBTIMESTAMP data type.<br /><br /> Removes the fractional second value when its scale is greater than the number of fractional second digits that the DT_DBTIME data type can contain. After removing the fractional second value, generates a report about this data truncation. For more information, see [Error Handling in Data](../../integration-services/data-flow/error-handling-in-data.md).|  
|DT_DBTIME2|Removes the date value represented by the DT_DBTIMESTAMP data type.<br /><br /> Removes the fractional second value when its scale is greater than the number of fractional second digits that the DT_DBTIME2 data type can contain. After removing the fractional second value, generates a report about this data truncation. For more information, see [Error Handling in Data](../../integration-services/data-flow/error-handling-in-data.md).|  
|DT_DBTIMESTAMP|No change.|  
|DT_DBTIMESTAMP2|Removes the fractional second value when its scale is greater than the number of fractional second digits that the DT_DBTIMESTAMP2 data type can contain. After removing the fractional second value, generates a report about this data truncation. For more information, see [Error Handling in Data](../../integration-services/data-flow/error-handling-in-data.md).|  
|DT_DBTIMESTAMPOFFSET|Sets the time zone field in the DT_DBTIMESTAMPOFFSET data type to zero.<br /><br /> Removes the fractional second value when its scale is greater than the number of fractional second digits that the DT_DBTIMESTAMPOFFSET data type can contain. After removing the fractional second value, generates a report about this data truncation. For more information, see [Error Handling in Data](../../integration-services/data-flow/error-handling-in-data.md).|  
  
#### Converting from DT_DBTIMESTAMP2  
  
|Convert DT_DBTIMESTAMP2 to|Result|  
|---------------------------------|------------|  
|DT_FILETIME|Removes the fractional second value when its scale is greater than the number of fractional second digits that the DT_FILETIME data type can contain. After removing the fractional second value, generates a report about this data truncation. For more information, see [Error Handling in Data](../../integration-services/data-flow/error-handling-in-data.md).|  
|DT_DATE|If a value represented by the DT_DBTIMESTAMP2 data type overflows the range of the DT_DATE data type, the DB_E_DATAOVERFLOW error is returned. For more information, see [Error Handling in Data](../../integration-services/data-flow/error-handling-in-data.md).<br /><br /> Removes the fractional second value when its scale is greater than the number of fractional second digits that the DT_DATE data type can contain. After removing the fractional second value, generates a report about this data truncation. For more information, see [Error Handling in Data](../../integration-services/data-flow/error-handling-in-data.md).|  
|DT_DBDATE|Removes the time value represented by the DT_DBTIMESTAMP2 data type.|  
|DT_DBTIME|Removes the date value represented by the DT_DBTIMESTAMP2 data type.<br /><br /> Removes the fractional second value when its scale is greater than the number of fractional second digits that the DT_DBTIME data type can contain. After removing the fractional second value, generates a report about this data truncation. For more information, see [Error Handling in Data](../../integration-services/data-flow/error-handling-in-data.md).|  
|DT_DBTIME2|Removes the date value represented by the DT_DBTIMESTAMP2 data type.<br /><br /> Removes the fractional second value when its scale is greater than the number of fractional second digits that the DT_DBTIME2 data type can contain. After removing the fractional second value, generates a report about this data truncation. For more information, see [Error Handling in Data](../../integration-services/data-flow/error-handling-in-data.md).|  
|DT_DBTIMESTAMP|If a value represented by the DT_DBTIMESTAMP2 data type overflows the range of the DT_DBTIMESTAMP data type, returns the DB_E_DATAOVERFLOW error.<br /><br /> DT_DBTIMESTAMP2 maps to a SQL Server data type, datetime2, with a range of January 1, 1A.D. through December 31, 9999. DT_DBTIMESTAMP maps to a SQL Server data type, datetime, with smaller a range of January 1, 1753 through December 31, 9999.<br /><br /> Removes the fractional second value when its scale is greater than the number of fractional second digits that the DT_DBTIMESTAMP data type can contain. After removing the fractional second value, generates a report about this data truncation.<br /><br /> For more information about errors, see [Error Handling in Data](../../integration-services/data-flow/error-handling-in-data.md).|  
|DT_DBTIMESTAMP2|Removes the fractional second value when its scale is greater than the number of fractional second digits that the destination DT_DBTIMESTAMP2 data type can contain. After removing the fractional second value, generates a report about this data truncation. For more information, see [Error Handling in Data](../../integration-services/data-flow/error-handling-in-data.md).|  
|DT_DBTIMESTAMPOFFSET|Sets the time zone field in the DT_DBTIMESTAMPOFFSET data type to zero.<br /><br /> Removes the fractional second value when its scale is greater than the number of fractional second digits that the DT_DBTIMESTAMPOFFSET data type can contain. After removing the fractional second value, generates a report about this data truncation. For more information, see [Error Handling in Data](../../integration-services/data-flow/error-handling-in-data.md).|  
  
#### Converting from DT_DBTIMESTAMPOFFSET  
  
|Convert DT_DBTIMESTAMPOFFSET to|Result|  
|--------------------------------------|------------|  
|DT_FILETIME|Changes the time value represented by the DT_DBTIMESTAMPOFFSET data type to Coordinated Universal Time (UTC).<br /><br /> Removes the fractional second value when its scale is greater than the number of fractional second digits that the DT_FILETIME data type can contain. After removing the fractional second value, generates a report about this data truncation. For more information, see [Error Handling in Data](../../integration-services/data-flow/error-handling-in-data.md).|  
|DT_DATE|Changes the time value represented by the DT_DBTIMESTAMPOFFSET data type to UTC.<br /><br /> If a value represented by the DT_DBTIMESTAMPOFFSET data type overflows the range of the DT_DATE data type, returns the DB_E_DATAOVERFLOW error.<br /><br /> Removes the fractional second value when its scale is greater than the number of fractional second digits that the DT_DATE data type can contain. After removing the fractional second value, generates a report about this data truncation.<br /><br /> For more information, see [Error Handling in Data](../../integration-services/data-flow/error-handling-in-data.md).|  
|DT_DBDATE|Changes the time value represented by the DT_DBTIMESTAMPOFFSET data type to UTC, which can affect the date value. The time value is then removed.|  
|DT_DBTIME|Changes the time value represented by the DT_DBTIMESTAMPOFFSET data type to UTC.<br /><br /> Removes the data value represented by the DT_DBTIMESTAMPEOFFSET data type.<br /><br /> Removes the fractional second value when its scale is greater than the number of fractional second digits that the DT_DBTIME data type can contain. After removing the fractional second value, generates a report about this data truncation. For more information, see [Error Handling in Data](../../integration-services/data-flow/error-handling-in-data.md).|  
|DT_DBTIME2|Changes the time value represented by the DT_DBTIMESTAMPOFFSET data type to UTC.<br /><br /> Removes the date value represented by the DT_DBTIMESTAMPOFFSET data type.<br /><br /> Removes the fractional second value when its scale is greater than the number of fractional second digits that the DT_DBTIME2 data type can contain. After removing the fractional second value, generates a report about this data truncation. For more information, see [Error Handling in Data](../../integration-services/data-flow/error-handling-in-data.md).|  
|DT_DBTIMESTAMP|Changes the time value represented by the DT_DBTIMESTAMPOFFSET data type to UTC.<br /><br /> If a value represented by the DT_DBTIMESTAMPOFFSET data type overflows the range of the DT_DBTIMESTAMP data type, the DB_E_DATAOVERFLOW error is returned.<br /><br /> Removes the fractional second value when its scale is greater than the number of fractional second digits that the DT_DBTIMESTAMP data type can contain. After removing the fractional second value, generates a report about this data truncation.<br /><br /> For more information, see [Error Handling in Data](../../integration-services/data-flow/error-handling-in-data.md).|  
|DT_DBTIMESTAMP2|Changes the time value represented by the DT_DBTIMESTAMPOFFSET data type to UTC.<br /><br /> Removes the fractional second value when its scale is greater than the number of fractional second digits that the DT_DBTIMESTAMP2 data type can contain. After removing the fractional second value, generates a report about this data truncation. For more information, see [Error Handling in Data](../../integration-services/data-flow/error-handling-in-data.md).|  
|DT_DBTIMESTAMPOFFSET|Removes the fractional second value when its scale is greater than the number of fractional second digits that the destination DT_DBTIMESTAMPOFFSET data type can contain. After removing the fractional second value, generates a report about this data truncation. For more information, see [Error Handling in Data](../../integration-services/data-flow/error-handling-in-data.md).|  
  
## Mapping of Integration Services Data Types to Database Data Types  
 The following table provides guidance on mapping the data types used by certain databases to [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] data types. These mappings are summarized from the mapping files used by the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Import and Export Wizard when it imports data from these sources. For more information about these mapping files, see [SQL Server Import and Export Wizard](~/integration-services/import-export-data/welcome-to-sql-server-import-and-export-wizard.md).  
  
> [!IMPORTANT]  
>  These mappings are not intended to represent strict equivalency, but only to provide guidance. In certain situations, you may need to use a different data type than the one shown in this table.  
  
> [!NOTE]  
>  You can use the SQL Server data types to estimate the size of corresponding Integration Services date and time data types.  
  
|Data Type|SQL Server<br /><br /> (SQLOLEDB; SQLNCLI10)|SQL Server (SqlClient)|Jet|Oracle<br /><br /> (OracleClient)|DB2<br /><br /> (DB2OLEDB)|DB2<br /><br /> (IBMDADB2)|  
|---------------|--------------------------------------------|------------------------------|---------|---------------------------------|--------------------------|--------------------------|  
|DT_BOOL|bit|bit|Bit||||  
|DT_BYTES|binary, varbinary, timestamp|binary, varbinary, timestamp|BigBinary, VarBinary|RAW|||  
|DT_CY|smallmoney, money|smallmoney, money|Currency||||  
|DT_DATE|||||||  
|DT_DBDATE|[date &#40;Transact-SQL&#41;](../../t-sql/data-types/date-transact-sql.md)|[date &#40;Transact-SQL&#41;](../../t-sql/data-types/date-transact-sql.md)||date|date|date|  
|DT_DBTIME||||timestamp|time|time|  
|DT_DBTIME2|[time &#40;Transact-SQL&#41;](../../t-sql/data-types/time-transact-sql.md)(p)|[time &#40;Transact-SQL&#41;](../../t-sql/data-types/time-transact-sql.md) (p)|||||  
|DT_DBTIMESTAMP|[datetime &#40;Transact-SQL&#41;](../../t-sql/data-types/datetime-transact-sql.md), [smalldatetime &#40;Transact-SQL&#41;](../../t-sql/data-types/smalldatetime-transact-sql.md)|[datetime &#40;Transact-SQL&#41;](../../t-sql/data-types/datetime-transact-sql.md), [smalldatetime &#40;Transact-SQL&#41;](../../t-sql/data-types/smalldatetime-transact-sql.md)|DateTime|TIMESTAMP, DATE, INTERVAL|TIME, TIMESTAMP, DATE|TIME, TIMESTAMP, DATE|  
|DT_DBTIMESTAMP2|[datetime2 &#40;Transact-SQL&#41;](../../t-sql/data-types/datetime2-transact-sql.md)|[datetime2 &#40;Transact-SQL&#41;](../../t-sql/data-types/datetime2-transact-sql.md)||timestamp|timestamp|timestamp|  
|DT_DBTIMESTAMPOFFSET|[datetimeoffset &#40;Transact-SQL&#41;](../../t-sql/data-types/datetimeoffset-transact-sql.md)(p)|[datetimeoffset &#40;Transact-SQL&#41;](../../t-sql/data-types/datetimeoffset-transact-sql.md) (p)||timestampoffset|timestamp,<br /><br /> varchar|timestamp,<br /><br /> varchar|  
|DT_DECIMAL|||||||  
|DT_FILETIME|||||||  
|DT_GUID|uniqueidentifier|uniqueidentifier|GUID||||  
|DT_I1|||||||  
|DT_I2|smallint|smallint|Short||SMALLINT|SMALLINT|  
|DT_I4|int|int|Long||INTEGER|INTEGER|  
|DT_I8|bigint|bigint|||BIGINT|BIGINT|  
|DT_NUMERIC|decimal, numeric|decimal, numeric|Decimal|NUMBER, INT|DECIMAL, NUMERIC|DECIMAL, NUMERIC|  
|DT_R4|real|real|Single||REAL|REAL|  
|DT_R8|float|float|Double|FLOAT, REAL|FLOAT, DOUBLE|FLOAT, DOUBLE|  
|DT_STR|char, varchar||VarChar||CHAR, VARCHAR|CHAR, VARCHAR|  
|DT_UI1|tinyint|tinyint|Byte||||  
|DT_UI2|||||||  
|DT_UI4|||||||  
|DT_UI8|||||||  
|DT_WSTR|nchar, nvarchar, sql_variant, xml|char, varchar, nchar, nvarchar, sql_variant, xml|LongText|CHAR, ROWID, VARCHAR2, NVARCHAR2, NCHAR|GRAPHIC, VARGRAPHIC|GRAPHIC, VARGRAPHIC|  
|DT_IMAGE|image|image|LongBinary|LONG RAW, BLOB, LOBLOCATOR, BFILE, VARGRAPHIC, LONG VARGRAPHIC, user-defined|CHAR () FOR BIT DATA, VARCHAR () FOR BIT DATA|CHAR () FOR BIT DATA, VARCHAR () FOR BIT DATA, BLOB|  
|DT_NTEXT|ntext|text, ntext||LONG, CLOB, NCLOB, NVARCHAR, TEXT|LONG VARCHAR, NCHAR, NVARCHAR, TEXT|LONG VARCHAR, DBCLOB, NCHAR, NVARCHAR, TEXT|  
|DT_TEXT|text||||LONG VARCHAR FOR BIT DATA|LONG VARCHAR FOR BIT DATA, CLOB|  
  
 For information on mapping data types in the data flow, see [Working with Data Types in the Data Flow](../../integration-services/extending-packages-custom-objects/data-flow/working-with-data-types-in-the-data-flow.md).  
  
## Related Content  
 Blog entry, [Performance Comparison between Data Type Conversion Techniques in SSIS 2008](https://techcommunity.microsoft.com/t5/datacat/performance-comparison-between-data-type-conversion-techniques/ba-p/305035), on blogs.msdn.com.  
  
## See Also  
 [Data in Data Flows](../../integration-services/data-flow/data-in-data-flows.md)  
  
  
