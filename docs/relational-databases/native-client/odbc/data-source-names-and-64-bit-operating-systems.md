---
title: "Data Source Names and 64-Bit Operating Systems"
description: Learn how to build and run an application as a 32-bit application on a 64-bit operating system by creating the ODBC data source with the ODBC Administrator.
author: markingmyname
ms.author: maghan
ms.date: "03/06/2017"
ms.service: sql
ms.subservice: native-client
ms.topic: "reference"
---
# Data Source Names and 64-Bit Operating Systems
[!INCLUDE [SQL Server](../../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

> [!IMPORTANT] 
> [!INCLUDE[snac-removed-oledb-and-odbc](../../../includes/snac-removed-oledb-and-odbc.md)]

  To build and run an application as a 32-bit application on a 64-bit operating system, you must create the ODBC data source with the ODBC Administrator in %windir%\SysWOW64\odbcad32.exe.  
  
## Remarks  
 A 64-bit Windows operating system has two odbcad32.exe files:  
  
-   %SystemRoot%\system32\odbcad32.exe is used to create and maintain data source names for 64-bit applications.  
  
-   %SystemRoot%\SysWOW64\odbcad32.exe is used to create and maintain data source names for 32-bit applications, including 32-bit applications that run on 64-bit operating systems.  
  
## See Also  
 [SQL Server Native Client &#40;ODBC&#41;](../../../relational-databases/native-client/odbc/sql-server-native-client-odbc.md)  
  
  
