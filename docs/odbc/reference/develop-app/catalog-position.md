---
description: "Catalog Position"
title: "Catalog Position | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "SQL statements [ODBC], interoperability"
  - "interoperability of SQL statements [ODBC], catalog position"
  - "catalog position [ODBC]"
ms.assetid: 5bc5f64b-c75a-43d2-8745-102ec7a49000
author: David-Engel
ms.author: v-davidengel
---
# Catalog Position
The position of a catalog name in an identifier and how it is separated from the rest of the identifier varies from data source to data source. For example, in an Xbase data source, the catalog name is a directory and, in Microsoft® Windows®, is separated from the table name (which is a file name) by a backslash (\\). The following illustration demonstrates this condition.  
  
 ![Catalog postion: Xbase](../../../odbc/reference/develop-app/media/ch0801.gif "ch0801")  
  
 In a SQL Server data source, the catalog is a database and is separated from the schema and table names by a period (.).  
  
 ![Catalog postion: SQL Server](../../../odbc/reference/develop-app/media/ch0802.gif "ch0802")  
  
 In an Oracle data source, the catalog is also the database but follows the table name and is separated from the schema and table names by an at sign (@).  
  
 ![Catalog postion: Oracle](../../../odbc/reference/develop-app/media/ch0803.gif "ch0803")  
  
 To determine the catalog separator and the location of the catalog name, an application calls **SQLGetInfo** with the SQL_CATALOG_NAME_SEPARATOR and SQL_CATALOG_LOCATION options. Interoperable applications should construct identifiers according to these values.  
  
 When quoting identifiers that contain more than one part, applications must be careful to quote each part separately and not quote the character that separates the identifiers. For example, the following statement to select all of the rows and columns of an Xbase table quotes the catalog (\XBASE\SALES\CORP) and table (Parts.dbf) names, but not the catalog separator (\\):  
  
```  
SELECT * FROM "\XBASE\SALES\CORP"\"PARTS.DBF"  
```  
  
 The following statement to select all of the rows and columns of an Oracle table quotes the catalog (Sales), schema (Corporate), and table (Parts) names, but not the catalog (@) or schema (.) separators:  
  
```  
SELECT * FROM "Corporate"."Parts"@"Sales"  
```  
  
 For information about quoting identifiers, see the next section, [Quoted Identifiers](../../../odbc/reference/develop-app/quoted-identifiers.md).
