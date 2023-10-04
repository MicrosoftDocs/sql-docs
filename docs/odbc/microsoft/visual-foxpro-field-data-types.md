---
title: "Visual FoxPro Field Data Types"
description: "Visual FoxPro Field Data Types"
author: David-Engel
ms.author: v-davidengel
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
helpviewer_keywords:
  - "field data types [ODBC]"
  - "Visual FoxPro ODBC driver [ODBC], data types"
---
# Visual FoxPro Field Data Types
The following table lists the values for the *FieldType* argument in ALTER TABLE and CREATE TABLE and indicates whether *nFieldWidth* and *nPrecision* arguments are required.  
  
|*FieldType*|*NFieldWidth*|*nPrecision*|Description|  
|-----------------|-------------------|------------------|-----------------|  
|B|-|d|Double|  
|C|N|-|Character field of width *n*|  
|D|-|-|Date|  
|F|N|d|Floating numeric field of width *n* with *d* decimal places|  
|G|-|-|General|  
|I|-|-|Integer|  
|L|-|-|Logical|  
|M|-|-|Memo|  
|N|N|d|Numeric field of width *n* with *d* decimal places|  
|T|-|-|DateTime|  
|Y|-|-|Currency|
