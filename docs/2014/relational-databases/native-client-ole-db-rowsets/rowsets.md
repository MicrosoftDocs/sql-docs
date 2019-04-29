---
title: "Rowsets | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
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
---
# Rowsets
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
  
-   [Creating a Rowset with IOpenRowset](creating-a-rowset-with-iopenrowset.md)  
  
-   [Creating Rowsets with ICommand::Execute](creating-rowsets-with-icommand-execute.md)  
  
-   [Rowset Properties and Behaviors](rowset-properties-and-behaviors.md)  
  
-   [Rowsets and SQL Server Cursors](rowsets-and-sql-server-cursors.md)  
  
-   [Fetching Rows](fetching-rows.md)  
  
-   [Fetching a Single Row with IRow](fetching-a-single-row-with-irow.md)  
  
-   [Bookmarks](bookmarks.md)  
  
-   [Updating Data in Rowsets](updating-data-in-rowsets.md)  
  
## See Also  
 [SQL Server Native Client &#40;OLE DB&#41;](../native-client/ole-db/sql-server-native-client-ole-db.md)  
  
  
