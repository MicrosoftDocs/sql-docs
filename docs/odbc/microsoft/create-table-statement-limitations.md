---
title: "CREATE TABLE Statement Limitations"
description: "CREATE TABLE Statement Limitations"
author: David-Engel
ms.author: v-davidengel
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
helpviewer_keywords:
  - "CREATE TABLE statement limitations [ODBC]"
  - "ODBC SQL grammar, CREATE TABLE statement limitations"
---
# CREATE TABLE Statement Limitations
When the Microsoft Access, Microsoft Excel, or Paradoxdriver is used, and the length of a text or binary column is not specified (or is specified as 0), the column length will be set to 255.  
  
 When the dBASE driver is used, and the length of a text or binary column is not specified (or is specified as 0), the column length will be set to 254.  
  
 A maximum of 255 columns is supported.  
  
 When the Microsoft Excel driver is used on a MicrosoftExcel 5.0, 7.0, or 97 data source, a worksheet cannot be created with the same name as a worksheet that was previously dropped. When the Microsoft Excel driver is used to access a version 5.0, 7.0, or 97 worksheet, a DROP TABLE statement clears the worksheet, but does not delete the worksheet name.  
  
 When the Paradox driver is used, columns cannot be added once an index has been defined on a table. If the first column of the argument list of a CREATE TABLE statement creates an index, a second column cannot be included in the argument list.
