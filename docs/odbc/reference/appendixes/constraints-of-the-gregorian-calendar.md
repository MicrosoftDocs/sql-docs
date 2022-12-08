---
description: "Constraints of the Gregorian Calendar"
title: "Constraints of the Gregorian Calendar | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: connectivity
ms.topic: reference
helpviewer_keywords: 
  - "data types [ODBC], Gregorian calendar"
  - "Gregorian calendar [ODBC]"
ms.assetid: 70667410-c582-4369-8e06-9d98e21cd2bf
author: David-Engel
ms.author: v-davidengel
---
# Constraints of the Gregorian Calendar
Date and datetime data types, and the trailing fields of interval data types, must conform to the constraints of the Gregorian calendar. These constraints are as follows:  
  
-   The value of the month field must be between 1 and 12, inclusive.  
  
-   The value of the day field must be in the range from 1 through the number of days in the month. The number of days in the month is determined from the values of the year and months fields and can be 28, 29, 30, or 31. (The number of days in the month can also depend on whether it is a leap year.)  
  
-   The value of the hour field must be between 0 and 23, inclusive.  
  
-   The value of the minute field must be between 0 and 59, inclusive.  
  
-   For the trailing seconds field of interval data types, the value of the seconds field must be between 0 and 59.9(*n*), inclusive, where *n* is the number of digits in the fractional seconds precision.  
  
-   For the trailing seconds field of datetime data types, the value of the seconds field must be between 0 and 61.9(*n*), inclusive, where *n* specifies the number of "9" digits and the value of *n* is the fractional seconds precision. (The range of seconds allows as many as two leap seconds to maintain synchronization of sidereal time.)
