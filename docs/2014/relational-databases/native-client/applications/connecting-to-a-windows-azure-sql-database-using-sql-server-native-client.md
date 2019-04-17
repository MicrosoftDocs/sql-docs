---
title: "Connecting to a Azure SQL Database Using SQL Server Native Client | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: native-client
ms.topic: "reference"
ms.assetid: 0dc20bb6-b142-4259-b87b-427d2ba798af
author: MightyPen
ms.author: genemi
manager: craigg
---
# Connecting to a Azure SQL Database Using SQL Server Native Client
  For a sample that shows how to connect to a [!INCLUDE[ssSDSfull](../../../includes/sssdsfull-md.md)] using [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client, see [Development: How-to Topics (Windows Azure SQL Database)](https://msdn.microsoft.com/library/ee621787.aspx).  
  
## Known Issues When Connecting to a SQL Database  
 The following are known issues when connecting to a [!INCLUDE[ssSDS](../../../includes/sssds-md.md)] using [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client:  
  
-   A connection made with `SQLBrowseConnect` may be rejected if `SQLBrowseConnect` is used in stages.  For example, if the driver name is sent in the first call, server and credentials (user and password) sent in the second call, establishing the connection, and a database name and a language in the third call.  The third call will cause [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client to issue a USE statement to change databases. However, the USE statement is not supported in [!INCLUDE[ssSDS](../../../includes/sssds-md.md)], generating the following error:  
  
    ```  
    [Microsoft][SQL Server Native Client 11.0][SQL Server]USE statement is not supported to switch between databases. Use a new connection to connect to a different Database.  
    ```  
  
## See Also  
 [Building Applications with SQL Server Native Client](building-applications-with-sql-server-native-client.md)  
  
  
