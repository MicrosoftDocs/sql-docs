---
title: "SqlToolsVSNativeHelpers | Microsoft Docs"
ms.custom: ""
ms.date: "03/17/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: 
ms.topic: "reference"
ms.assetid: d33cb556-0380-490a-9220-b74062dbd6d9
author: "CarlRabeler"
ms.author: "carlrab"
monikerRange: "=azuresqldb-current||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# SqlToolsVSNativeHelpers
[!INCLUDE [SQL Server Azure SQL Database](../includes/applies-to-version/sql-asdb.md)]
  Library that supports SQL Server functionality in Visual Studio.  
  
## Syntax  
  
```  
  
BOOL WINAPI DllMain(HINSTANCE hInstance, DWORD dwReason, LPVOID /*lpReserved*/)  
```  
  
## Return Value  
 A Boolean value, **True** if the DLL entry point initialized properly, otherwise **False**.  
  
## See Also  
 [FrameWindowVisible](../relational-databases/sqltoolsvsnativehelpers-framewindowvisible.md)  
  
  
