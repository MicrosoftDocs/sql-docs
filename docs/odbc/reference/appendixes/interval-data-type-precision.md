---
title: "Interval Data Type Precision | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "data types [ODBC], interval data types"
  - "precision [ODBC], interval data types"
  - "seconds precision [ODBC]"
  - "interval data type [ODBC], precision"
  - "precision [ODBC]"
  - "interval leading precision [ODBC]"
  - "interval precision [ODBC]"
ms.assetid: eb73bd77-2e7e-4498-a266-4d7c990a0d56
author: MightyPen
ms.author: genemi
manager: craigg
---
# Interval Data Type Precision
Precision for an interval data type includes interval leading precision, interval precision, and seconds precision.  
  
 The leading field of an interval is a signed numeric. The maximum number of digits for the leading field is determined by a quantity called *interval leading precision,* which is a part of the data type declaration. For example, the declaration: INTERVAL HOUR(5) TO MINUTE has an interval leading precision of 5; the HOUR field can take values from -99999 through 99999. The interval leading precision is contained in the SQL_DESC_DATETIME_INTERVAL_PRECISION field of the descriptor record.  
  
 The list of fields that an interval data type is made up of is called *interval precision*. It is not a numeric value, as the term "precision" might imply. For example, the interval precision of the type INTERVAL DAY TO SECOND is the list DAY, HOUR, MINUTE, SECOND. There is no descriptor field that holds this value; the interval precision can always be determined by the interval data type.  
  
 Any interval data type that has a SECOND field has a *seconds precision*. This is the number of decimal digits allowed in the fractional part of the seconds value. This is different than for other data types, where precision indicates the number of digits before the decimal point. The seconds precision of an interval data type is the number of digits after the decimal point. For example, if the seconds precision is set to 6, the number 123456 in the fraction field would be interpreted as .123456 and the number 1230 would be interpreted as .001230. For other data types, this is referred to as scale. Interval seconds precision is contained in the SQL_DESC_PRECISION field of the descriptor. If the precision of the fractional seconds component of the SQL interval value is greater than what can be held in the C interval structure, it is driver-defined whether the fractional seconds value in the SQL interval is rounded or truncated when converted to the C interval structure.  
  
 When the SQL_DESC_CONCISE_TYPE field is set to an interval data type, the SQL_DESC_TYPE field is set to SQL_INTERVAL and the SQL_DESC_DATETIME_INTERVAL_CODE is set to the code for the interval data type. The SQL_DESC_DATETIME_INTERVAL_PRECISION field is automatically set to the default interval leading precision of 2, and the SQL_DESC_PRECISION field is automatically set to the default interval seconds precision of 6. If either of these values is not appropriate, the application should explicitly set the descriptor field through a call to **SQLSetDescField**.
