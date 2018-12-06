---
title: "Supported ODBC SQL Grammar (Visual FoxPro ODBC Driver) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "native Visual FoxPro language syntax [ODBC]"
  - "FoxPro ODBC driver [ODBC], SQL grammar"
  - "SQL grammar [ODBC], Visual FoxPro ODBC driver"
  - "Visual FoxPro ODBC driver [ODBC], SQL grammar"
  - "grammar support in Visual FoxPro ODBC driver [ODBC]"
  - "Visual FoxPro ODBC driver [ODBC], native Visual FoxPro language syntax"
  - "FoxPro ODBC driver [ODBC], native Visual FoxPro language syntax"
ms.assetid: f41a38c2-e22e-4c65-a32e-9a6777435160
author: MightyPen
ms.author: genemi
manager: craigg
---
# Supported ODBC SQL Grammar (Visual FoxPro ODBC Driver)
The Microsoft Visual FoxPro ODBC Driver supports the following:  
  
-   All SQL statements and clauses in the ODBC minimum SQL grammar  
  
-   An additional SQL statement from the ODBC core SQL grammar  
  
 The following table lists the items supported by the driver, by ODBC SQL Grammar level.  
  
|Level|Elements|Item|  
|-----------|--------------|----------|  
|Minimum|Data Definition Language (DDL)|CREATE TABLE and DROP TABLE|  
||Data Manipulation Language (DML)|SELECT, INSERT, UPDATE, and DELETE|  
||Expressions|Simple (such as A>B+C)|  
||Data types|CHAR, VARCHAR, or LONG VARCHAR|  
  
 In addition to the supported ODBC SQL grammar, the Visual FoxPro ODBC Driver supports the complete native Visual FoxPro language syntax for the following Visual FoxPro commands:  
  
 [ALTER TABLE](../../odbc/microsoft/alter-table-sql-command.md)  
  
 [CREATE TABLE](../../odbc/microsoft/create-table-sql-command.md)  
  
 [DELETE](../../odbc/microsoft/delete-sql-command.md)  
  
 [DELETE TAG](../../odbc/microsoft/delete-tag-command.md)  
  
 [DROP TABLE](../../odbc/microsoft/drop-table-command.md)  
  
 [INDEX](../../odbc/microsoft/index-command.md)  
  
 [INSERT](../../odbc/microsoft/insert-sql-command.md)  
  
 [SELECT](../../odbc/microsoft/select-sql-command.md)  
  
 [UPDATE](../../odbc/microsoft/update-sql-command.md)
