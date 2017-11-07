---
title: "Handling Large Object (LOB) Parameters in the CLR | Microsoft Docs"
ms.custom: ""
ms.date: "03/04/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "docset-sql-devref"
ms.tgt_pltfrm: ""
ms.topic: "reference"
helpviewer_keywords: 
  - "large data, CLR integration"
  - "LOB data [SQL Server], CLR integration"
  - "SqlBytes data type"
  - "SqlChars data type"
ms.assetid: d07956f6-9543-4476-9426-536f95991150
caps.latest.revision: 20
author: "JennieHubbard"
ms.author: "jhubbard"
manager: "jhubbard"
---
# Handling Large Object (LOB) Parameters in the CLR
  Use **SqlBytes** and **SqlChars** to pass large object (LOB) binary type (**varbinary(max)**) and LOB character type (**nvarchar(max)**) parameters, respectively. These types allow streaming the LOB values from the database to the common language runtime (CLR) routine, instead of copying the entire value into managed space. **SqlBinary** and **SqlString** should be used only for small binary and character string values.  
  
## See Also  
 [SQL Server Data Types in the .NET Framework](../../relational-databases/clr-integration-database-objects-types-net-framework/sql-server-data-types-in-the-net-framework.md)  
  
  