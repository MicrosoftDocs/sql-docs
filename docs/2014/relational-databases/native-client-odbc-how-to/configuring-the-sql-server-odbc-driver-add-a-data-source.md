---
title: "Add a Data Source (ODBC) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: native-client
ms.topic: "reference"
helpviewer_keywords: 
  - "data sources [ODBC]"
ms.assetid: b4ac6f0e-8e6a-4b1a-9a7e-60e0a69b2180
author: MightyPen
ms.author: genemi
manager: craigg
---
# Add a Data Source (ODBC)
  You can add a data source by using ODBC Administrator, programmatically (by using [SQLConfigDataSource](../native-client-odbc-api/sqlconfigdatasource.md)), or by creating a file.  
  
### To add a data source by using ODBC Administrator  
  
1.  From the **Control Panel**, access **Administrative Tools** and then **Data Sources (ODBC)**. Alternatively, you can invoke odbcad32.exe.  
  
2.  Click the **User DSN**, **System DSN**, or **File DSN** tab, and then click **Add**.  
  
3.  Click **SQL Server**, and then click **Finish**.  
  
4.  Complete the Steps in the Create a New Data Source to SQL Server Wizard.  
  
### To add a data source programmatically  
  
1.  Call [SQLConfigDataSource](../native-client-odbc-api/sqlconfigdatasource.md) with the second parameter set to either ODBC_ADD_DSN or ODBC_ADD_SYS_DSN.  
  
### To add a file data source  
  
1.  Call [SQLDriverConnect](../native-client-odbc-api/sqldriverconnect.md) with a SAVEFILE=file_name parameter in the connect string. If the connect is successful, the ODBC driver creates a file data source with the connection parameters in the location pointed to by the SAVEFILE parameter.  
  
## See Also  
 [Configuring the SQL Server ODBC Driver How-to Topics](../../database-engine/dev-guide/configuring-the-sql-server-odbc-driver-how-to-topics.md)  
  
  
