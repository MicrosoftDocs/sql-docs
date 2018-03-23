---
title: "Creating an OLE DB Driver for SQL Server Application | Microsoft Docs"
ms.custom: ""
ms.date: "02/28/2018"
ms.prod: "sql-non-specified"
ms.prod_service: "database-engine, sql-database, sql-data-warehouse, pdw"
ms.service: ""
ms.component: "oledb-driver-for-sql-server"
ms.reviewer: ""
ms.suite: "sql"
ms.technology: 
  - "docset-sql-devref"
ms.tgt_pltfrm: ""
ms.topic: "reference"
helpviewer_keywords: 
  - "OLE DB Driver for SQL Server, application creation"
  - "applications [OLE DB Driver for SQL Server]"
  - "OLE DB, creating applications"
author: "pmasl"
ms.author: "Pedro.Lopes"
manager: "jhubbard"
ms.workload: "Inactive"
---
# Creating an OLE DB Driver for SQL Server Application
[!INCLUDE[appliesto-ss-asdb-asdw-pdw-md](../../../includes/appliesto-ss-asdb-asdw-pdw-md.md)]

  Creating an OLE DB Driver for SQL Server application involves these steps:  
  
1.  Establishing a connection to a data source.  
  
2.  Executing a command.  
  
3.  Processing the results.  
  
> [!NOTE]  
>  When possible, use Windows Authentication. If Windows Authentication is not available, prompt users to enter their credentials at run time. Avoid storing credentials in a file. If you must persist credentials, you should encrypt them with [the Win32 cryptoAPI](http://go.microsoft.com/fwlink/?LinkId=9504).  
  
## In This Section  
  
-   [Establishing a Connection to a Data Source](../../oledb/ole-db-driver/establishing-a-connection-to-a-data-source.md)  
  
-   [Executing a Command](../../oledb/ole-db-driver/executing-a-command.md)  
  
-   [Processing Results](../../oledb/ole-db-driver/processing-results.md)  
  
-   [About OLE DB Properties](../../oledb/ole-db-driver/about-ole-db-properties.md)  
  
-   [Using the OUTPUT Clause with OLE DB in OLE DB Driver for SQL Server](../../oledb/ole-db-driver/using-the-output-clause-with-ole-db-in-oledb-driver-for-sql-server.md)  
  
## See Also  
 [OLE DB Driver for SQL Server &#40;OLE DB&#41;](../../oledb/ole-db/oledb-driver-for-sql-server-ole-db.md)  
  
  
