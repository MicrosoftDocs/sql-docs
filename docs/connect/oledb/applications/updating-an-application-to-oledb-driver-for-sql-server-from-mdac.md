---
title: "Updating an Application to OLE DB Driver for SQL Server from MDAC | Microsoft Docs"
description: "Updating an application to OLE DB Driver for SQL Server from MDAC"
ms.custom: ""
ms.date: "06/12/2018"
ms.prod: sql
ms.prod_service: "database-engine, sql-database, sql-data-warehouse, pdw"
ms.reviewer: ""
ms.technology: connectivity
ms.topic: "reference"
helpviewer_keywords:
  - "MDAC [SQL Server]"
  - "MSOLEDBSQL, vs. MDAC"
  - "OLE DB Driver for SQL Server, vs. MDAC"
  - "data access [OLE DB Driver for SQL Server], vs. MDAC"
  - "OLE DB Driver for SQL Server, updating applications"
author: pmasl
ms.author: pelopes
manager: craigg
---
# Updating an Application to OLE DB Driver for SQL Server from MDAC
[!INCLUDE[appliesto-ss-asdb-asdw-pdw-md](../../../includes/appliesto-ss-asdb-asdw-pdw-md.md)]

[!INCLUDE[Driver_OLEDB_Download](../../../includes/driver_oledb_download.md)]

  There are a number of differences between OLE DB Driver for SQL Server and Microsoft Data Access Components (MDAC); starting with Windows Vista, the data access components are now called Windows Data Access Components (or Windows DAC). Although both provide native data access to [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] databases, OLE DB Driver for SQL Server has been designed to expose the new features of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)], while at the same time maintaining backward compatibility with earlier versions.   

 In addition, although MDAC contains components for using OLE DB, ODBC, and ActiveX Data Objects (ADO), OLE DB Driver for SQL Server only implements OLE DB (although ADO can access the functionality of OLE DB Driver for SQL Server).  

 OLE DB Driver for SQL Server and MDAC differ in the other following areas:  

-   Users who use ADO to access the OLE DB Driver for SQL Server may find less filtering functionality than when they accessed the SQL OLE DB provider.  

-   If an ADO application uses OLE DB Driver for SQL Server and attempts to update a computed column, an error will be reported. With MDAC, the update was accepted but ignored.  

-   OLE DB Driver for SQL Server is a single self-contained dynamic link library (DLL) file. The publicly exposed interfaces have been kept to a minimum, both to ease distribution, as well as to limit security exposure.  

-   Only OLE DB interfaces are supported.  

-   The OLE DB Driver for SQL Server names are different from names used with MDAC.  

-   User-accessible functionality supplied by MDAC components is available when using OLE DB Driver for SQL Server. This includes, but is not limited to, the following: connection pooling, ADO support, and client cursor support. When any of these features are used, OLE DB Driver for SQL Server supplies only database connectivity. MDAC provides functionality such as tracing, management controls, and performance counters.  

-   Applications can use OLE DB core services with OLE DB Driver for SQL Server, but if using the OLE DB cursor engine, they should use the data type compatibility option to avoid any potential problems that might arise because the cursor engine has no knowledge of the new [!INCLUDE[ssVersion2005](../../../includes/ssversion2005-md.md)] data types.  

-   OLE DB Driver for SQL Server supports access to previous [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] databases.  

-   OLE DB Driver for SQL Server does not contain XML integration. OLE DB Driver for SQL Server supports SELECT ... FOR XML queries, but does not support any other XML functionality. However, OLE DB Driver for SQL Server does support the **xml** data type introduced in [!INCLUDE[ssVersion2005](../../../includes/ssversion2005-md.md)].  

-   OLE DB Driver for SQL Server supports configuring client-side network libraries using only connection string attributes. If you need more complete network library configuration, you must use [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Configuration Manager.  

-   MDAC connection strings allow a Boolean value (**true**) for the **Trusted_Connection** keyword. An OLE DB Driver for SQL Server connection string must use **yes** or **no**.  

-   Minor changes have occurred to warnings and errors. Warnings and errors returned by the server now retain the same severity when passed to OLE DB Driver for SQL Server. You should ensure you have thoroughly tested your application if you depend on trapping particular warnings and errors.  

-   OLE DB Driver for SQL Server has stricter error checking than MDAC, which means that some applications that do not conform strictly to the OLE DB specifications may behave differently. For example, the SQLOLEDB provider did not enforce the rule that parameter names must start with '\@' for result parameters, but the OLE DB Driver for SQL Server does.  

-   OLE DB Driver for SQL Server behaves differently from MDAC regarding failed connections. For example, MDAC returns cached property values for a connection that has failed, whereas OLE DB Driver for SQL Server reports an error to the calling application.  

-   OLE DB Driver for SQL Server does not generate Visual Studio Analyzer events, but instead generates Windows tracing events.  

-   OLE DB Driver for SQL Server cannot be used with perfmon. Perfmon is a Windows tool that can only be used with DSNs that use the MDAC SQLODBC driver included with Windows.  

-   When OLE DB Driver for SQL Server is connected to [!INCLUDE[ssVersion2005](../../../includes/ssversion2005-md.md)] and later versions, server error 16947 is returned as a SQL_ERROR. This error occurs when a positioned update or delete fails to update or delete a row. With MDAC when connecting to any version of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)], server error 16947 is returned as a warning (SQL_SUCCESS_WITH_INFO).  

-   OLE DB Driver for SQL Server implements the **IDBDataSourceAdmin** interface, which is an optional OLE DB interface that was not previously implemented, but only the **CreateDataSource** method of this optional interface is implemented. [!INCLUDE[ssNoteDepFutureAvoid](../../../includes/ssnotedepfutureavoid-md.md)]  

-   The OLE DB Driver for SQL Server returns synonyms in the TABLES and TABLE_INFO schema rowsets, with TABLE_TYPE set to SYNONYM.  

-   Return values of data type **varchar(max)**, **nvarchar(max)**, **varbinary(max)**, **xml**, **udt**, or other large object types cannot be returned to client versions earlier than [!INCLUDE[ssVersion2005](../../../includes/ssversion2005-md.md)]. If you wish to use these types as return values, you must use OLE DB Driver for SQL Server.  

-   MDAC allows the following statements to be executed at the start of manual and implicit transactions, but OLE DB Driver for SQL Server does not. They must be executed in autocommit mode.  

    -   All full-text operations (index and catalog DDL)  

    -   All database operations (create database, alter database, drop database)  

    -   Reconfigure  

    -   Shutdown  

    -   Kill  

    -   Backup  

-   When MDAC applications connect to [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)], the data types introduced in [!INCLUDE[ssVersion2005](../../../includes/ssversion2005-md.md)] will appear as [!INCLUDE[ssVersion2000](../../../includes/ssversion2000-md.md)]-compatible data types as shown in the following table.  

    |SQL Server 2005 type|SQL Server 2000 type|  
    |--------------------------|--------------------------|  
    |**varchar(max)**|**text**|  
    |**nvarchar(max)**|**ntext**|  
    |**varbinary(max)**|**image**|  
    |**udt**|**varbinary**|  
    |**xml**|**ntext**|  

     This type mapping affects the values returned for column metadata. For example, a **text** column has a maximum size of 2,147,483,647, but OLE DB Driver for SQL Server reports the maximum size of **varchar(max)** columns as 2,147,483,647 or -1, depending on platform.  

-   OLE DB Driver for SQL Server allows ambiguity in connection strings (for example, some keywords may be specified more than once, and conflicting keywords may be allowed with resolution based on position or precedence) for reasons of backward compatibility. Future releases of OLE DB Driver for SQL Server might not allow ambiguity in connection strings. It is good practice when modifying applications to use OLE DB Driver for SQL Server to eliminate any dependency on connection string ambiguity.  

-   If you use an OLE DB call to start transactions, there is a difference in behavior between OLE DB Driver for SQL Server and MDAC; transactions will begin immediately with OLE DB Driver for SQL Server, but transactions will begin after the first database access using MDAC. This can affect the behavior of stored procedures and batches because [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] requires @@TRANCOUNT to be the same after a batch or stored procedure finishes execution as it was when the batch or stored procedure started.  

-   With OLE DB Driver for SQL Server, ITransactionLocal::BeginTransaction will cause a transaction to be started immediately. With MDAC the transaction start was delayed until the application executed a statement that required a transaction in implicit transaction mode. For more information, see [SET IMPLICIT_TRANSACTIONS &#40;Transact-SQL&#41;](../../../t-sql/statements/set-implicit-transactions-transact-sql.md).  


 Both OLE DB Driver for SQL Server and MDAC support read committed transaction isolation using row versioning, but only OLE DB Driver for SQL Server supports snapshot transaction isolation. (In programming terms, read committed transaction isolation using row versioning is the same as read-committed transaction.).  

## See Also  
 [Building Applications with OLE DB Driver for SQL Server](../../oledb/applications/building-applications-with-oledb-driver-for-sql-server.md)  
