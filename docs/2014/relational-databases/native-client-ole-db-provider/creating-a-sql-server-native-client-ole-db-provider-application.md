---
title: "Creating a SQL Server Native Client OLE DB Provider Application | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: native-client
ms.topic: "reference"
helpviewer_keywords: 
  - "SQL Server Native Client OLE DB provider, application creation"
  - "applications [SQL Server Native Client]"
  - "OLE DB, creating applications"
ms.assetid: f3ae6815-f32d-4913-a1a2-2ba2f20cfd88
author: MightyPen
ms.author: genemi
manager: craigg
---
# Creating a SQL Server Native Client OLE DB Provider Application
  Creating a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client OLE DB provider application involves these steps:  
  
1.  Establishing a connection to a data source.  
  
2.  Executing a command.  
  
3.  Processing the results.  
  
> [!NOTE]  
>  When possible, use Windows Authentication. If Windows Authentication is not available, prompt users to enter their credentials at run time. Avoid storing credentials in a file. If you must persist credentials, you should encrypt them with [the Win32 cryptoAPI](https://go.microsoft.com/fwlink/?LinkId=9504).  
  
## In This Section  
  
-   [Establishing a Connection to a Data Source](establishing-a-connection-to-a-data-source.md)  
  
-   [Executing a Command](executing-a-command.md)  
  
-   [Processing Results](processing-results.md)  
  
-   [About OLE DB Properties](about-ole-db-properties.md)  
  
-   [Using the OUTPUT Clause with OLE DB in SQL Server Native Client](using-the-output-clause-with-ole-db-in-sql-server-native-client.md)  
  
## See Also  
 [SQL Server Native Client &#40;OLE DB&#41;](../native-client/ole-db/sql-server-native-client-ole-db.md)  
  
  
