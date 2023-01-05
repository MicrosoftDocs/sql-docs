---
description: "C Interval Structure"
title: "C Interval Structure | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: connectivity
ms.topic: reference
helpviewer_keywords: 
  - "data types [ODBC], interval data types"
  - "interval data type [ODBC], structure"
  - "C data types [ODBC], interval"
ms.assetid: 52b42b56-50aa-4ce6-8d79-0963c7a71437
author: David-Engel
ms.author: v-davidengel
---
# C Interval Structure
Each of the C interval data types listed in the [C Data Types](../../../odbc/reference/appendixes/c-data-types.md) section uses the same structure to contain the interval data. When **SQLFetch**, **SQLFetchScroll**, or **SQLGetData** is called, the driver returns data into the SQL_INTERVAL_STRUCT structure, uses the value that was specified by the application for the C data types (in the call to **SQLBindCol**, **SQLGetData**, or **SQLBindParameter**) to interpret the contents of SQL_INTERVAL_STRUCT, and populates the *interval_type* field of the structure with the *enum* value corresponding to the C type. Note that drivers do not read the *interval_type* field to determine the type of the interval; they retrieve the value of the SQL_DESC_CONCISE_TYPE descriptor field. When the structure is used for parameter data, the driver uses the value specified by the application in the SQL_DESC_CONCISE_TYPE field of the APD to interpret the contents of SQL_INTERVAL_STRUCT, even if the application sets the value of the *interval_type* field to a different value.  
  
 This structure is defined as follows:  
  
```  
typedef struct tagSQL_INTERVAL_STRUCT  
{  
   SQLINTERVAL interval_type;   
   SQLSMALLINT interval_sign;  
   union {  
         SQL_YEAR_MONTH_STRUCT   year_month;  
         SQL_DAY_SECOND_STRUCT   day_second;  
         } intval;  
} SQL_INTERVAL_STRUCT;  
typedef enum   
{  
   SQL_IS_YEAR = 1,  
   SQL_IS_MONTH = 2,  
   SQL_IS_DAY = 3,  
   SQL_IS_HOUR = 4,  
   SQL_IS_MINUTE = 5,  
   SQL_IS_SECOND = 6,  
   SQL_IS_YEAR_TO_MONTH = 7,  
   SQL_IS_DAY_TO_HOUR = 8,  
   SQL_IS_DAY_TO_MINUTE = 9,  
   SQL_IS_DAY_TO_SECOND = 10,  
   SQL_IS_HOUR_TO_MINUTE = 11,  
   SQL_IS_HOUR_TO_SECOND = 12,  
   SQL_IS_MINUTE_TO_SECOND = 13  
} SQLINTERVAL;  
  
typedef struct tagSQL_YEAR_MONTH  
{  
   SQLUINTEGER year;  
   SQLUINTEGER month;   
} SQL_YEAR_MONTH_STRUCT;  
  
typedef struct tagSQL_DAY_SECOND  
{  
   SQLUINTEGER day;  
   SQLUINTEGER hour;  
   SQLUINTEGER minute;  
   SQLUINTEGER second;  
   SQLUINTEGER fraction;  
} SQL_DAY_SECOND_STRUCT;  
```  
  
 The *interval_type* field of the SQL_INTERVAL_STRUCT indicates to the application what structure is held in the union and also what members of the structure are relevant. The *interval_sign* field has the value SQL_FALSE if the interval leading field is unsigned; if it is SQL_TRUE, the leading field is negative. The value in the leading field itself is always unsigned, regardless of the value of *interval_sign*. The *interval_sign* field acts as a sign bit.
