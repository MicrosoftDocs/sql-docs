---
title: "Other Non-SQL Server Subscribers | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: replication
ms.topic: conceptual
helpviewer_keywords: 
  - "non-SQL Server Subscribers, other types"
ms.assetid: 96b8beb9-38e8-4ce4-97ca-c0f8656b73b4
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# Other Non-SQL Server Subscribers
  For a list of non-[!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Subscribers supported by [!INCLUDE[msCoName](../../../includes/msconame-md.md)], see [Non-SQL Server Subscribers](non-sql-server-subscribers.md). This topic includes information about requirements for ODBC drivers and OLE DB providers.  
  
## ODBC Driver Requirements  
 The ODBC driver:  
  
-   Must be ODBC level-1 compliant.  
  
-   Must be thread-safe, and for the processor architecture (Intel or Alpha) and platform (32 bit or 64 bit) on which the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Distributor runs.  
  
-   Must be transaction capable.  
  
-   Must support the Data Definition Language (DDL).  
  
-   Cannot be read-only.  
  
-   Must support long table names such as **MSreplication_subscriptions**.  
  
## Replicating Using OLE DB Interfaces  
 OLE DB providers must support these objects for transactional replication:  
  
-   **DataSource** object  
  
-   **Session** object  
  
-   **Command** object  
  
-   **Rowset** object  
  
-   **Error** object  
  
### DataSource Object Interfaces  
 The following interfaces are required to connect to a data source:  
  
-   `IDBInitialize`  
  
-   `IDBCreateSession`  
  
-   `IDBProperties`  
  
 If the provider supports the **IDBInfo** interface, [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] uses the interface to retrieve information such as the quoted identifier character, maximum SQL statement length, and maximum number of characters in table and column names.  
  
### Session Object Interfaces  
 The following interfaces are required:  
  
-   **IDBCreateCommand**  
  
-   **ITransaction**  
  
-   **ITransactionLocal**  
  
-   **IDBSchemaRowset**  
  
### Command Object Interfaces  
 The following interfaces are required:  
  
-   **ICommand**  
  
-   **ICommandProperties**  
  
-   **ICommandText**  
  
-   **ICommandPrepare**  
  
-   **IColumnsInfo**  
  
-   **IAccessor**  
  
-   **ICommandWithParameters**  
  
 **IAccessor** is necessary to create parameter accessors. If the provider supports **IColumnRowset**, [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] uses that interface to determine whether a column is an identity column.  
  
### Rowset Object Interfaces  
 The following interfaces are required:  
  
-   **IRowset**  
  
-   **IAccessor**  
  
-   **IColumnsInfo**  
  
 An application should open a rowset on a replicated table that is created in the subscription database. **IColumnsInfo** and **IAccessor** are needed to access data in the rowset.  
  
### Error Object Interfaces  
 Use the following interfaces to manage errors:  
  
-   **IErrorRecords**  
  
-   **IErrorInfo**  
  
 Use **ISQLErrorInfo** if it is supported by the OLE DB provider.  
  
 For more information about the OLE DB provider, see the documentation supplied with your OLE DB provider.  
  
## See Also  
 [Non-SQL Server Subscribers](non-sql-server-subscribers.md)  
  
  
