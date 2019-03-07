---
title: "Interval Escape Sequences | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "interval literals [ODBC]"
  - "escape sequences [ODBC], interval"
  - "ODBC escape sequences [ODBC], interval"
ms.assetid: 303e8dab-8f13-4fa5-857f-15cc1f75bdd6
author: MightyPen
ms.author: genemi
manager: craigg
---
# Interval Escape Sequences
ODBC uses escape sequences for interval literals. The syntax of this escape sequence is as follows:  
  
 {*interval-literal*}  
  
 For the BNF syntax of *interval-literal*, see the [Interval Literal Syntax](../../../odbc/reference/appendixes/interval-literal-syntax.md) section later in this appendix.  
  
 The interval literal escape sequence is supported if the interval data types are supported by the data source. An application should call **SQLGetTypeInfo** to determine whether these data types are supported.
