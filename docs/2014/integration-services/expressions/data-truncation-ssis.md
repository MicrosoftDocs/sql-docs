---
title: "Data Truncation (SSIS) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
helpviewer_keywords: 
  - "truncating data"
  - "data truncation [Integration Services]"
  - "expressions [Integration Services], data truncation"
  - "truncate options [Integration Services]"
ms.assetid: 02461e15-49ca-445b-abb3-5c369c283ec2
author: janinezhang
ms.author: janinez
manager: craigg
---
# Data Truncation (SSIS)
  An expression may inadvertently cause data to be truncated. Truncation can occur under the following circumstances:  
  
-   Strings. For example, translating string data with a DT_WSTR data type to the same length string, measured in characters, with a DT_STR data type causes data loss if the original string contains double-byte characters.  
  
-   Significant digits. For example, casting an integer from a DT_I4 data type to a DT_I2 data type or an unsigned integer to a signed integer.  
  
-   Insignificant digits. For example, casting a real number from a DT_R8 to a DT_R4 or an integer from a DT_I4 data type to a DT_R4 data type.  
  
 The expression evaluator identifies explicit casts that may cause truncation and issues a warning when the expression is parsed. For example, the expression evaluator warns you if a 30-character string is cast into a 20-character string.  
  
> [!NOTE]  
>  Truncation is not checked at run time; data is truncated without warning. However, most data adapters and transformations support error outputs that can handle the disposition of error rows. For more information about handling truncation of data, see [Error Handling in Data](../data-flow/error-handling-in-data.md).  
  
  
