---
title: "Using Transactions"
description: "Using Transactions"
author: "markingmyname"
ms.author: "maghan"
ms.date: "03/14/2017"
ms.service: sql
ms.topic: "reference"
ms.custom:
  - ignite-2025
helpviewer_keywords:
  - "SQL Server Management Objects, transactions"
  - "transactions [SMO]"
  - "SMO [SQL Server], transactions"
monikerRange: "=azuresqldb-current || =azure-sqldw-latest || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric-sqldb"
---
# Using Transactions
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance Synapse Analytics FabricSQLDB](../../../includes/applies-to-version/sql-asdb-asdbmi-asa-fabricsqldb.md)]

  In [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Management Objects (SMO), transaction processing is achieved through the connection to the instance of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] by using the <xref:Microsoft.SqlServer.Management.Common.ServerConnection> object. The <xref:Microsoft.SqlServer.Management.Common.ServerConnection> object is referenced by the <xref:Microsoft.SqlServer.Replication.ReplicationObject.ConnectionContext%2A> property of the <xref:Microsoft.SqlServer.Management.Smo.Server> object when the connection is established. Methods such as <xref:Microsoft.SqlServer.Management.Common.DataTransferProgressEventType.StartTransaction>, <xref:Microsoft.SqlServer.Management.Common.ServerConnection.RollBackTransaction%2A>, and <xref:Microsoft.SqlServer.Management.Common.ServerConnection.CommitTransaction%2A> belong to the <xref:Microsoft.SqlServer.Management.Smo.Server.ConnectionContext%2A> object property.  
  
## See Also  
 [Creating SMO Programs](../../../relational-databases/server-management-objects-smo/create-program/creating-smo-programs.md)  
  
  
