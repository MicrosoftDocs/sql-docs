---
title: "Connecting to a Windows Azure SQL Database Using OLE DB Driver for SQL Server | Microsoft Docs"
ms.custom: ""
ms.date: "02/28/2018"
ms.prod: ""
ms.prod_service: "sql-database"
ms.reviewer: ""
ms.service: "sql-database"
ms.component: "oledb|applications"
ms.suite: "sql"
ms.technology: 
  - "docset-sql-devref"
ms.tgt_pltfrm: ""
ms.topic: "reference"
ms.assetid: 0dc20bb6-b142-4259-b87b-427d2ba798af
caps.latest.revision: 6
author: "JennieHubbard"
ms.author: "jhubbard"
manager: "jhubbard"
ms.workload: "Inactive"
---
# Connecting to a Windows Azure SQL Database Using OLE DB Driver for SQL Server
[!INCLUDE[appliesto-ss-asdb-asdw-pdw-md](../../../includes/appliesto-ss-asdb-asdw-pdw-md.md)]

  For a sample that shows how to connect to a [!INCLUDE[ssSDSfull](../../../includes/sssdsfull-md.md)] using OLE DB Driver for SQL Server, see [Development: How-to Topics (Windows Azure SQL Database)](http://msdn.microsoft.com/library/ee621787.aspx).  
  
## Known Issues When Connecting to a SQL Database  
 The following are known issues when connecting to a [!INCLUDE[ssSDS](../../../includes/sssds-md.md)] using OLE DB Driver for SQL Server:  
  
-   A connection made with **SQLBrowseConnect** may be rejected if **SQLBrowseConnect** is used in stages.  For example, if the driver name is sent in the first call, server and credentials (user and password) sent in the second call, establishing the connection, and a database name and a language in the third call.  The third call will cause OLE DB Driver for SQL Server to issue a USE statement to change databases. However, the USE statement is not supported in [!INCLUDE[ssSDS](../../../includes/sssds-md.md)], generating the following error:  
  
    ```  
    [Microsoft][OLE DB Driver for SQL Server][SQL Server]USE statement is not supported to switch between databases. Use a new connection to connect to a different Database.  
    ```  
  
## See Also  
 [Building Applications with OLE DB Driver for SQL Server](../../oledb/applications/building-applications-with-oledb-driver-for-sql-server.md)  
  
  
