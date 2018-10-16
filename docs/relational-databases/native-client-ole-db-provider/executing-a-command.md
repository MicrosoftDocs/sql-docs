---
title: "Executing a Command | Microsoft Docs"
ms.custom: ""
ms.date: "03/17/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database, sql-data-warehouse, pdw"
ms.reviewer: ""
ms.technology: native-client
ms.topic: "reference"
helpviewer_keywords: 
  - "commands [SQL Server Native Client]"
  - "OLE DB, executing commands"
  - "sessions [SQL Server Native Client]"
  - "OLE DB extensions for XML"
  - "SQL Server Native Client OLE DB provider, command execution"
ms.assetid: bb0b3cbf-fe45-46ba-b2ec-c5a39e3c7081
author: MightyPen
ms.author: genemi
manager: craigg
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Executing a Command
[!INCLUDE[appliesto-ss-asdb-asdw-pdw-md](../../includes/appliesto-ss-asdb-asdw-pdw-md.md)]
[!INCLUDE[SNAC_Deprecated](../../includes/snac-deprecated.md)]

  After the connection to a data source is established, the consumer calls the **IDBCreateSession::CreateSession** method to create a session. The session acts as a command, rowset, or transaction factory.  
  
 To work directly with individual tables or indexes, the consumer requests the **IOpenRowset** interface. The **IOpenRowset::OpenRowset** method opens and returns a rowset that includes all rows from a single base table or index.  
  
 To execute a command (such as SELECT \* FROM Authors), the consumer requests the **IDBCreateCommand** interface. The consumer can execute the **IDBCreateCommand::CreateCommand** method to create a command object and request for the **ICommandText** interface. The **ICommandText::SetCommandText** method is used to specify the command that is to be executed.  
  
 The **Execute** command is used to execute the command. The command can be any SQL statement or procedure name. Not all commands produce a result set (rowset) object. Commands such as SELECT * FROM Authors produce a result set.  
  
## See Also  
 [Creating a SQL Server Native Client OLE DB Provider Application](../../relational-databases/native-client-ole-db-provider/creating-a-sql-server-native-client-ole-db-provider-application.md)  
  
  
