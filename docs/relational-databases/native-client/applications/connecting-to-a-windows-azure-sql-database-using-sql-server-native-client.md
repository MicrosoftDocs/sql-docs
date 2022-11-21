---
description: "Connecting to an Azure SQL Database Using SQL Server Native Client"
title: "Connect to Azure SQL Database"
ms.custom: ""
ms.date: "03/14/2017"
ms.reviewer: ""
ms.service: sql
ms.subservice: native-client
ms.topic: "reference"
ms.assetid: 0dc20bb6-b142-4259-b87b-427d2ba798af
author: markingmyname
ms.author: maghan
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Connecting to an Azure SQL Database Using SQL Server Native Client
[!INCLUDE [SQL Server](../../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

> [!IMPORTANT] 
> [!INCLUDE[snac-removed-oledb-and-odbc](../../../includes/snac-removed-oledb-and-odbc.md)]

  For a sample that shows how to connect to a [!INCLUDE[ssSDSfull](../../../includes/sssdsfull-md.md)] using [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client, see [Development: How-to Topics (Azure SQL Database)](/previous-versions/azure/ee621787(v=azure.100)).  
  
## Known Issues When Connecting to a SQL Database  
 The following are known issues when connecting to a [!INCLUDE[ssSDS](../../../includes/sssds-md.md)] using [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client:  
  
-   A connection made with **SQLBrowseConnect** may be rejected if **SQLBrowseConnect** is used in stages.  For example, if the driver name is sent in the first call, server and credentials (user and password) sent in the second call, establishing the connection, and a database name and a language in the third call.  The third call will cause [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client to issue a USE statement to change databases. However, the USE statement is not supported in [!INCLUDE[ssSDS](../../../includes/sssds-md.md)], generating the following error:  
  
    ```  
    [Microsoft][SQL Server Native Client 11.0][SQL Server]USE statement is not supported to switch between databases. Use a new connection to connect to a different Database.  
    ```  
  
## See Also  
 [Building Applications with SQL Server Native Client](../../../relational-databases/native-client/applications/building-applications-with-sql-server-native-client.md)  
  
