---
title: "Components of SQL Server Native Client | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.reviewer: ""
ms.technology: native-client
ms.topic: "reference"
helpviewer_keywords: 
  - "SQL Server Native Client ODBC driver, about SQL Server Native Client ODBC driver"
  - "data access [SQL Server Native Client], components"
  - "components [SQL Server Native Client]"
  - "SQLNCLI, about SQL Server Native Client"
ms.assetid: 65f932d5-daa1-4eff-b6df-ee633fcf2a7c
author: MightyPen
ms.author: genemi
manager: craigg
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Components of SQL Server Native Client
[!INCLUDE[appliesto-ss-asdb-asdw-pdw-md](../../../includes/appliesto-ss-asdb-asdw-pdw-md.md)]
[!INCLUDE[SNAC_Deprecated](../../../includes/snac-deprecated.md)]

  [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client contains the following components:  
  
|Component|Description|  
|---------------|-----------------|  
|sqlncli11.dll|The dynamic-link library (DLL) file that contains all of the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client functionality. This includes the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client OLE DB provider and the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client ODBC driver.|  
|sqlnclir11.rll|The accompanying resource file for the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client library.|   
|sqlncli.h|The [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client header file that contains all of the new definitions needed in order to use [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client. This header file replaces both the odbcss.h and the sqloledb.h header files.<br /><br /> Note: You cannot reference sqlncli.h and odbcss.h in the same program, but you can reference sqlncli.h and sqloledb.h in same program as long as sqloledb.h is defined first.|  
|sqlncli11.lib|The library file needed to directly call the **bcp** utility functions that are part of the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client ODBC driver.<br /><br /> Note: If you do reference the sqlncli11.lib file in your programming code, you need to make sure that the sqlncli11.dll file is in your system path, and in the system path of the users that make use of your application.|  
  
## See Also  
 [Building Applications with SQL Server Native Client](../../../relational-databases/native-client/applications/building-applications-with-sql-server-native-client.md)  
  
  
