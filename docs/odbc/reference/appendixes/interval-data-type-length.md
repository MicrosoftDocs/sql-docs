---
title: "Interval Data Type Length"
description: "Interval Data Type Length"
author: David-Engel
ms.author: davidengel
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
helpviewer_keywords:
  - "data types [ODBC], interval data types"
  - "length of data types [ODBC]"
  - "interval data type [ODBC], length"
---
# Interval Data Type Length
The following rules are used to determine the length of an interval data type in characters. Length is expressed in number of characters. The number of bytes depends upon the character set. The length includes the following values added together:  
  
-   Two characters for every field in the interval that is not the leading field.  
  
-   For the leading field, the number of characters that is the express or implicit leading precision. If the leading precision is not specified, the default value is 2.  
  
-   One character for the separator between the fields.  
  
-   One plus the express or implied seconds precision. If the seconds precision is not specified, the default value is 6.  
  
 Specific column length values for each interval data type are contained in [Column Size](../../../odbc/reference/appendixes/column-size.md).
