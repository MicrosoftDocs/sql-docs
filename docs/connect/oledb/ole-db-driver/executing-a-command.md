---
title: "Executing a command (OLE DB driver)"
description: Learn how a consumer in OLE DB Driver for SQL Server executes a command by first creating a session, getting a rowset, and using Execute.
author: David-Engel
ms.author: v-davidengel
ms.date: "06/14/2018"
ms.service: sql
ms.subservice: connectivity
ms.topic: "reference"
helpviewer_keywords:
  - "commands [OLE DB Driver for SQL Server]"
  - "OLE DB, executing commands"
  - "sessions [OLE DB Driver for SQL Server]"
  - "OLE DB extensions for XML"
  - "OLE DB Driver for SQL Server, command execution"
---
# Executing a Command
[!INCLUDE [SQL Server](../../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

[!INCLUDE[Driver_OLEDB_Download](../../../includes/driver_oledb_download.md)]

  After the connection to a data source is established, the consumer calls the **IDBCreateSession::CreateSession** method to create a session. The session acts as a command, rowset, or transaction factory.  
  
 To work directly with individual tables or indexes, the consumer requests the **IOpenRowset** interface. The **IOpenRowset::OpenRowset** method opens and returns a rowset that includes all rows from a single base table or index.  
  
 To execute a command (such as SELECT \* FROM Authors), the consumer requests the **IDBCreateCommand** interface. The consumer can execute the **IDBCreateCommand::CreateCommand** method to create a command object and request for the **ICommandText** interface. The **ICommandText::SetCommandText** method is used to specify the command that is to be executed.  
  
 The **Execute** command is used to execute the command. The command can be any SQL statement or procedure name. Not all commands produce a result set (rowset) object. Commands such as SELECT * FROM Authors produce a result set.  
  
## See Also  
 [Creating an OLE DB Driver for SQL Server Application](../../oledb/ole-db-driver/creating-a-oledb-driver-for-sql-server-application.md)  
  
  
