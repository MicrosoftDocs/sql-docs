---
description: "Logged vs. Unlogged Modifications"
title: "Logged vs. Unlogged Modifications | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: native-client
ms.topic: "reference"
helpviewer_keywords: 
  - "text columns [ODBC]"
  - "SQL Server Native Client ODBC driver, image columns"
  - "SQL Server Native Client ODBC driver, text columns"
  - "data types [ODBC], image"
  - "data types [ODBC], text"
  - "logged vs. nonlogged modifications [SQL Server Native Client]"
  - "columns [ODBC]"
  - "ODBC data types, image columns"
  - "nonlogged vs. logged modifications"
  - "ODBC data types, text columns"
  - "image columns [ODBC]"
ms.assetid: 20aa5b27-4a2c-46e7-8356-beb0eebf4b7e
author: markingmyname
ms.author: maghan
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Logged vs. Unlogged Modifications
[!INCLUDE[SQL Server Azure SQL Database Synapse Analytics PDW ](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

  An application can request that the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client ODBC driver not log **text**, **ntext**, and **image** modifications. Care should be used with this option, however. It should be used only for those situations where the **text**, **ntext**, or **image** data is not critical and data owners are willing to trade off the ability to recover data for higher performance.  
  
 The logging of **text**, **ntext**, and **image** modifications is controlled by calling [SQLSetStmtAttr](../../relational-databases/native-client-odbc-api/sqlsetstmtattr.md) with the *Attribute* parameter set to SQL_SOPT_SS_ TEXTPTR_LOGGING and *ValuePtr* set to either SQL_TL_ON or SQL_TL_OFF.  
  
## See Also  
 [Managing Text and Image Columns](../../relational-databases/native-client-odbc-text-image-columns/managing-text-and-image-columns.md)  
  
  
