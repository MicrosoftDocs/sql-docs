---
description: "Date, Time, and Timestamp Escape Sequences"
title: "Date, Time, and Timestamp Escape Sequences | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: connectivity
ms.topic: reference
helpviewer_keywords: 
  - "escape sequences [ODBC]"
  - "escape sequences [ODBC], about escape sequences"
  - "ODBC escape sequences [ODBC], about escape sequences"
  - "ODBC escape sequences [ODBC]"
ms.assetid: 67b7dee0-e5b1-4469-a626-0c7767852b80
author: David-Engel
ms.author: v-davidengel
---
# Date, Time, and Timestamp Escape Sequences
ODBC defines escape sequences for date, time, and timestamp literals. The syntax of these escape sequences is as follows:  
  
```  
  
{d 'value'}  
{t 'value'}  
{ts 'value'}  
```  
  
 In BNF notation, the syntax is as follows:  
  
```bnf 
ODBC-date-time-escape ::=  
     ODBC-date-escape  
     | ODBC-time-escape  
     | ODBC-timestamp-escape

ODBC-date-escape ::=  
     ODBC-esc-initiator d 'date-value' ODBC-esc-terminator

ODBC-time-escape ::=  
     ODBC-esc-initiator t 'time-value' ODBC-esc-terminator

ODBC-timestamp-escape ::=  
     ODBC-esc-initiator ts 'timestamp-value' ODBC-esc-terminator

ODBC-esc-initiator ::= {  

ODBC-esc-terminator ::= }  

date-value ::=   
     years-value date-separator months-value date-separator days-value

time-value ::=   
     hours-value time-separator minutes-value time-separator seconds-value

timestamp-value ::= date-value timestamp-separator time-value

date-separator ::= -  

time-separator ::= :  

timestamp-separator ::=  
     (The blank character)

years-value ::= digit digit digit digit

months-value ::= digit digit

days-value ::= digit digit

hours-value ::= digit digit

minutes-value ::= digit digit

seconds-value ::= digit digit[.digit...]  
```  
  
## Remarks  
 The date, time, and timestamp literal escape sequences are supported if the date, time, and timestamp data types are supported by the data source. An application should call **SQLGetTypeInfo** to determine whether these data types are supported.
