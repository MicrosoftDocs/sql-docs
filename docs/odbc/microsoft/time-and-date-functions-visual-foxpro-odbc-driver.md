---
title: "Time and Date Functions (Visual FoxPro ODBC Driver) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "ODBC date functions [ODBC]"
  - "Visual FoxPro ODBC driver [ODBC], time and date functions"
  - "FoxPro ODBC driver [ODBC], time and date functions"
  - "time and date functions [ODBC]"
  - "ODBC time and date functions [ODBC]"
  - "date functions [ODBC]"
ms.assetid: c1fb63b7-af50-45d6-8dec-ae6ea7119527
author: MightyPen
ms.author: genemi
manager: craigg
---
# Time and Date Functions (Visual FoxPro ODBC Driver)
The following table lists ODBC time and date functions supported by the Visual FoxPro ODBC Driver; when the Visual FoxPro grammar for the same function differs from the ODBC syntax, the Visual FoxPro equivalent is listed.  
  
|ODBC grammar|Visual FoxPro grammar|  
|------------------|---------------------------|  
|CURDATE*( )*|DATE*( )*|  
|CURTIME*( )*|TIME*( )*|  
|DAYNAME*(date_exp)*|CDOW*(date_exp)*|  
|DAYOFMONTH(*date_exp)*|DAY*( )*|  
|HOUR*(time_exp)*||  
|MINUTE*(time_exp)*||  
|MONTH*(time_exp)*||  
|MONTHNAME*(date_exp)*|CMONTH*(date_exp)*|  
|NOW*( )*|DATETIME*( )*|  
|SECOND*(time_exp)*|SEC*(time_exp)*|  
|WEEK*(date_exp)*||  
|YEAR*(date_exp)*||  
  
 The following time and date functions are not supported:  
  
 DAYOFYEAR *(date_exp)*  
  
 QUARTER *(date_exp)*  
  
 TIMESTAMPADD *(interval, integer_exp, timestamp_exp)*  
  
 TIMESTAMPDIFF *(interval, timestamp_exp1, timestamp_exp2)*  
  
## ODBC Escape Sequences  
 The driver also supports the ODBC escape sequence for date and timestamp data. The escape clause syntax is as follows:  
  
```  
--(*vendor(Microsoft),product(ODBC) d 'value' *)-  
--(*vendor(Microsoft),product(ODBC) ts ''value' *)-  
```  
  
 In this syntax, **d** indicates that *value* is a date in the *yyyy-mm-dd* format and **ts** indicates that *value* is a timestamp in the *yyyy-mm-dd hh:mm:ss*[.*f...*] format. The shorthand syntax for date and timestamp data is as follows:  
  
```  
{d 'value'}  
{ts 'value'}  
```  
  
 For example, each of the following statements updates the ALLTYPES table by using the date and timestamp shorthand syntax in a supported SQL UPDATE command:  
  
```  
UPDATE alltypes  
   SET DAT_COL={d'1968-04-28'}  
   WHERE KEY=111  
  
UPDATE alltypes  
   SET DTI_COL={ts'1968-04-28 12:00:00'}  
   WHERE KEY=111  
```  
  
## Remarks  
 For more information about escape sequences, see [Escape Sequences in ODBC](../../odbc/reference/develop-app/escape-sequences-in-odbc.md) in the *ODBC Programmer's Reference*.
