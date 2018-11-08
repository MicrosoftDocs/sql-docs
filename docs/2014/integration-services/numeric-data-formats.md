---
title: "Numeric Data Formats | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "integration-services"
ms.topic: conceptual
helpviewer_keywords: 
  - "integer data types [Integration Services]"
  - "numeric data formats for fast parse"
  - "locale-sensitive parsing [Integration Services]"
  - "fast parse [Integration Services]"
ms.assetid: fa3975ce-9d21-408a-857d-f85e30af27b0
author: douglaslms
ms.author: douglasl
manager: craigg
---
# Numeric Data Formats
  Fast parse provides a fast, simple, locale-insensitive set of routines for parsing data. Fast parse supports only a limited set of formats for integer data types.  
  
## Integer Data Types  
 The integer data types that [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] provides are DT_I1, DT_UI1, DT_I2, DT_UI2, DT_I4, DT_UI4, DT_I8, and DT_UI8. For more information, see [Integration Services Data Types](data-flow/integration-services-data-types.md).  
  
 Fast parse supports the following formats for integer data types:  
  
-   Zero or more leading and trailing spaces or tab stops. For example, the value "  123  " is valid. A value that is all spaces evaluates to zero.  
  
-   A leading plus sign, minus sign, or neither. For example, the values +123, -123, and 123 are valid.  
  
-   One or more Hindu-Arabic numerals (0-9). For example, the value 345 is valid. Other language numerals are not supported.  
  
 Non-supported data formats include the following:  
  
-   Special characters. For example, the currency character $ is not supported, and the value $20 cannot be parsed.  
  
-   White-space characters such as line feed, carriage returns, and non-breaking spaces. For example, the value " 123" cannot be parsed.  
  
-   Hexadecimal representations of integers. For example, the value 2EE cannot be parsed.  
  
-   Scientific notation representation of integers. For example, the value 1E+10 cannot be parsed.  
  
 The following formats are output data formats for integers:  
  
-   A minus sign for negative numbers and nothing for positive ones.  
  
-   No white spaces.  
  
-   One or more Hindu-Arabic numerals (0-9).  
  
  
