---
title: "Rowsets | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database, sql-data-warehouse, pdw"
ms.reviewer: ""
ms.technology: native-client
ms.topic: "reference"
helpviewer_keywords: 
  - "rowsets [OLE DB], about rowsets"
  - "SQL Server Native Client OLE DB provider, rowsets"
  - "OLE DB rowsets"
  - "OLE DB rowsets, about rowsets"
  - "rowsets [OLE DB]"
ms.assetid: 5e7b3cbe-3670-4e18-8172-2226e0b6b142
author: MightyPen
ms.author: genemi
manager: craigg
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Rowsets
[!INCLUDE[appliesto-ss-asdb-asdw-pdw-md](../../includes/appliesto-ss-asdb-asdw-pdw-md.md)]
[!INCLUDE[SNAC_Deprecated](../../includes/snac-deprecated.md)]

  A rowset is a set of rows that contain columns of data. Rowsets are central objects that enable all OLE DB data providers to expose result set data in tabular form.  
  
 After a consumer creates a session by using the **IDBCreateSession::CreateSession** method, the consumer can use either the **IOpenRowset** or **IDBCreateCommand** interface on the session to create a rowset. The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client OLE DB provider supports both of these interfaces. Both of these methods are described here.  
  
-   Create a rowset by calling the **IOpenRowset::OpenRowset** method.  
  
     This is equivalent to creating a rowset over a single table. This method opens and returns a rowset that includes all of the rows from a single base table. One of the arguments to **OpenRowset** is a table ID that identifies the table from which to create the rowset.  
  
-   Create a command object by calling the **IDBCreateCommand::CreateCommand** method.  
  
     The command object executes commands that the provider supports. With the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client OLE DB provider, the consumer can specify any [!INCLUDE[tsql](../../includes/tsql-md.md)] statement, such as a SELECT statement or a call to a stored procedure. The steps for creating a rowset by using a command object are:  
  
    1.  The consumer calls the **IDBCreateCommand::CreateCommand** method on the session to get a command object requesting the **ICommandText** interface on the command object. This **ICommandText** interface sets and retrieves the actual command text. The consumer fills in the text command by calling the **ICommandText::SetCommandText** method.  
  
    2.  The user calls the **ICommand::Execute** method on the command. The rowset object built when the command executes contains the result set from the command.  
  
 The consumer can use the **ICommandProperties** interface to get or set the properties for the rowset returned by the command executed by the **ICommand::Execute** interfaces. The most commonly requested properties are the interfaces the rowset must support. In addition to interfaces, the consumer can request properties that modify the behavior of the rowset or interface.  
  
 Consumers release rowsets with the **IRowset::Release** method. Releasing a rowset releases any row handles held by the consumer on that rowset. Releasing a rowset does not release the accessors. If you have an **IAccessor** interface, it still has to be released.  
  
## In This Section  
  
-   [Creating a Rowset with IOpenRowset](../../relational-databases/native-client-ole-db-rowsets/creating-a-rowset-with-iopenrowset.md)  
  
-   [Creating Rowsets with ICommand::Execute](../../relational-databases/native-client-ole-db-rowsets/creating-rowsets-with-icommand-execute.md)  
  
-   [Rowset Properties and Behaviors](../../relational-databases/native-client-ole-db-rowsets/rowset-properties-and-behaviors.md)  
  
-   [Rowsets and SQL Server Cursors](../../relational-databases/native-client-ole-db-rowsets/rowsets-and-sql-server-cursors.md)  
  
-   [Fetching Rows](../../relational-databases/native-client-ole-db-rowsets/fetching-rows.md)  
  
-   [Fetching a Single Row with IRow](../../relational-databases/native-client-ole-db-rowsets/fetching-a-single-row-with-irow.md)  
  
-   [Bookmarks](../../relational-databases/native-client-ole-db-rowsets/bookmarks.md)  
  
-   [Updating Data in Rowsets](../../relational-databases/native-client-ole-db-rowsets/updating-data-in-rowsets.md)  
  
## See Also  
 [SQL Server Native Client &#40;OLE DB&#41;](../../relational-databases/native-client/ole-db/sql-server-native-client-ole-db.md)  
  
  
