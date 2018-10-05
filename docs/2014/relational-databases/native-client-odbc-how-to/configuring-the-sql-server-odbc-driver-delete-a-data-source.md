---
title: "Delete a Data Source (ODBC) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: native-client
ms.topic: "reference"
helpviewer_keywords: 
  - "data sources [ODBC]"
ms.assetid: 910e3e16-7b91-49d8-80bb-b4243926afaa
author: MightyPen
ms.author: genemi
manager: craigg
---
# Delete a Data Source (ODBC)
  You can delete a data source by using ODBC Administrator, programmatically (by using [SQLConfigDataSource](../native-client-odbc-api/sqlconfigdatasource.md)), or by deleting a file (if a file data source name).  
  
### To delete a data source by using ODBC Administrator  
  
1.  In **Control Panel**, open **Administrative Tools**, and then double-click **Data Sources (ODBC)**. Alternatively, you can run odbcad32.exe from the command prompt.  
  
2.  Click the **User DSN**, **System DSN**, or **File DSN** tab.  
  
3.  Click the data source to delete.  
  
4.  Click **Remove**, and then confirm the deletion.  
  
## Example  
 To programmatically delete a data source, call [SQLConfigDataSource](../native-client-odbc-api/sqlconfigdatasource.md) using either ODBC_REMOVE_DSN or ODBC_REMOVE_SYS_DSN as the second parameter.  
  
 The following sample shows how you can programmatically delete a data source.  
  
```  
// remove_odbc_data_source.cpp  
// compile with: ODBCCP32.lib user32.lib  
#include <iostream>  
#include <windows.h>  
#include <odbcinst.h>  
  
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
 [Configuring the SQL Server ODBC Driver How-to Topics](../../database-engine/dev-guide/configuring-the-sql-server-odbc-driver-how-to-topics.md)  
  
  
