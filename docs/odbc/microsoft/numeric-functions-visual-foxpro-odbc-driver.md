---
description: "Numeric Functions (Visual FoxPro ODBC Driver)"
title: "Numeric Functions (Visual FoxPro ODBC Driver) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "ODBC numeric functions [ODBC]"
  - "Visual FoxPro ODBC driver [ODBC], numeric functions"
  - "numeric functions [ODBC]"
  - "FoxPro ODBC driver [ODBC], numeric functions"
ms.assetid: 7caab48e-cbb5-4bbc-a09b-5cf902e5bc45
author: David-Engel
ms.author: v-davidengel
---
# Numeric Functions (Visual FoxPro ODBC Driver)
The following table describes ODBC numeric functions supported by the Visual FoxPro ODBC Driver; when the Visual FoxPro grammar for the same function differs from the ODBC syntax, the Visual FoxPro equivalent is listed.  
  
|ODBC grammar|Visual FoxPro grammar|  
|------------------|---------------------------|  
|ABS *(numeric_exp)*||  
|ACOS *(float_exp)*||  
|ASIN *(float_exp)*||  
|ATAN *(float_exp)*||  
|ATAN2 *(float_exp1, float_exp2)*|ATN2 (*float_exp1, float_exp2*)|  
|CEILING *(numeric_exp)*||  
|COS *(float_exp)*||  
|COT *(float_exp)*||  
|DEGREES *(numeric_exp)*|RTOD *(numeric_exp)*|  
|EXP *(float_exp)*||  
|FLOOR *(numeric_exp)*||  
|LOG *(float_exp)*||  
|LOG10 *(float_exp)*||  
|MOD *(integer_exp1, integer_exp2)*||  
|PI *( )*||  
|RADIANS *(numeric_exp)*|DTOR *(numeric_exp)*|  
|RAND *([integer_exp])*||  
|ROUND *(numeric_exp, integer_exp)*||  
|SIGN *(numeric_exp)*||  
|SIN *(float_exp)*||  
|SQRT *(float_exp)*||  
|TAN *(float_exp)*||  
  
 The following numeric functions are not supported:  
  
 POWER *(numeric_exp, integer_exp)*  
  
 TRUNCATE *(numeric_exp, integer_exp)*
