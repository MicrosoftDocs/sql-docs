---
title: "Interval Escape Sequences"
description: "Interval Escape Sequences"
author: David-Engel
ms.author: davidengel
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
helpviewer_keywords:
  - "interval literals [ODBC]"
  - "escape sequences [ODBC], interval"
  - "ODBC escape sequences [ODBC], interval"
---
# Interval Escape Sequences
ODBC uses escape sequences for interval literals. The syntax of this escape sequence is as follows:  
  
 {*interval-literal*}  
  
 For the BNF syntax of *interval-literal*, see the [Interval Literal Syntax](../../../odbc/reference/appendixes/interval-literal-syntax.md) section later in this appendix.  
  
 The interval literal escape sequence is supported if the interval data types are supported by the data source. An application should call **SQLGetTypeInfo** to determine whether these data types are supported.
