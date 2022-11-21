---
title: "Delete a Data Source (ODBC) | Microsoft Docs"
description: Learn how to delete a data source by using ODBC Administrator, programmatically, or using a file, before using ODBC applications with SQL Server 2005 or later.
ms.custom: ""
ms.date: "08/01/2016"
ms.service: sql
ms.reviewer: ""
ms.subservice: native-client
ms.topic: "reference"
helpviewer_keywords: 
  - "data sources [ODBC]"
ms.assetid: 910e3e16-7b91-49d8-80bb-b4243926afaa
author: markingmyname
ms.author: maghan
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Configuring the SQL Server ODBC Driver - Delete a Data Source
[!INCLUDE[SQL Server Azure SQL Database Synapse Analytics PDW ](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

  Before using ODBC applications with [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] or later, you must know how to upgrade the version of the catalog stored procedures on earlier versions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and add, delete, and test data sources.  
  
  You can delete a data source by using ODBC Administrator, programmatically (by using [SQLConfigDataSource](../../relational-databases/native-client-odbc-api/sqlconfigdatasource.md)), or by deleting a file (if a file data source name).  
  
### To delete a data source by using ODBC Administrator  
  
1.  In **Control Panel**, open **Administrative Tools**, and then double-click  either **ODBC Data Sources (64-bit)** or **ODBC Data Sources (32-bit)**. Alternatively, you can run odbcad32.exe from the command prompt.  
  
2.  Click the **User DSN**, **System DSN**, or **File DSN** tab.  
  
3.  Select the data source to delete.  
  
4.  Click **Remove**, and then confirm the deletion.  

## Example  
 To programmatically delete a data source, call [SQLConfigDataSource](../../relational-databases/native-client-odbc-api/sqlconfigdatasource.md) using either ODBC_REMOVE_DSN or ODBC_REMOVE_SYS_DSN as the second parameter.  
  
 The following sample shows how you can programmatically delete a data source.  
  
```  
// remove_odbc_data_source.cpp  
// compile with: ODBCCP32.lib user32.lib  
#include <iostream>  
#include \<windows.h>  
#include \<odbcinst.h>  
  
int main() {   
   LPCSTR provider = "SQL Server";   // Windows SQL Server Driver  
   LPCSTR provider = "SQL Server";   // Windows SQL Server driver  
   LPCSTR provider2 = "SQL Server Native Client 11.0";   // SQL Server 2012 Native Client driver  
   LPCSTR dsnname = "DSN=data2";  
   BOOL retval = SQLConfigDataSource(NULL, ODBC_REMOVE_DSN, provider, dsnname);  
   std::cout << retval;   // 1 if successful  
}  
```  
  
## See Also  
 [Add a Data Source &#40;ODBC&#41;](../../relational-databases/native-client-odbc-how-to/configuring-the-sql-server-odbc-driver-add-a-data-source.md)  
  
  
