---
title: "Update from MDAC"
description: Upgrade from Windows Data Access Components to SQL Server Native Client, which exposes new features of SQL Server 2005 with backward compatibility.
ms.custom: ""
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: native-client
ms.topic: "reference"
helpviewer_keywords: 
  - "MDAC [SQL Server]"
  - "SQLNCLI, vs. MDAC"
  - "SQL Server Native Client, vs. MDAC"
  - "data access [SQL Server Native Client], vs. MDAC"
  - "SQL Server Native Client, updating applications"
ms.assetid: 2860efdd-c59a-4deb-8a0e-5124a8f4e6dd
author: markingmyname
ms.author: maghan
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Updating an Application to SQL Server Native Client from MDAC
[!INCLUDE [SQL Server](../../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

> [!IMPORTANT] 
> [!INCLUDE[snac-removed-oledb-and-odbc](../../../includes/snac-removed-oledb-and-odbc.md)]

  There are a number of differences between [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client and Microsoft Data Access Components (MDAC; starting with Windows Vista, the data access components are now called Windows Data Access Components, or Windows DAC). Although both provide native data access to [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] databases, [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client has been specifically designed to expose the new features of [!INCLUDE[ssVersion2005](../../../includes/ssversion2005-md.md)], while at the same time maintaining backward compatibility with earlier versions.  
  
 The information in this topic helps update your MDAC (or Windows DAC) application to be current with the version of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client that was included in [!INCLUDE[ssVersion2005](../../../includes/ssversion2005-md.md)]. To help you make this application be current with the version of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client that shipped in [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)], see [Updating an Application from SQL Server 2005 Native Client](../../../relational-databases/native-client/applications/updating-an-application-from-sql-server-2005-native-client.md).  

 > [!NOTE]  
 > [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client has been removed from [!INCLUDE [sssql22-md](../../../includes/sssql22-md.md)].
  
 In addition, although MDAC contains components for using OLE DB, ODBC, and ActiveX Data Objects (ADO), [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client only implements OLE DB and ODBC (although ADO can access the functionality of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client).  
  
 [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client and MDAC differ in the other following areas:  
  
-   Users who use ADO to access a [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client provider may find less filtering functionality than when they accessed a SQL OLE DB provider.  
  
-   If an ADO application uses [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client and attempts to update a computed column, an error will be reported. With MDAC the update was accepted but ignored.  
  
-   [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client is a single self-contained dynamic link library (DLL) file. The publicly exposed interfaces have been kept to a minimum, both to ease distribution, as well as to limit security exposure.  
  
-   Only OLE DB and ODBC interfaces are supported.  
  
-   The [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client OLE DB provider and ODBC driver names are different from those used with MDAC.  
  
-   User-accessible functionality supplied by MDAC components is available when using [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client. This includes, but is not limited to, the following: connection pooling, ADO support, and client cursor support. When any of these features are used, [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client supplies only database connectivity. MDAC provides functionality such as tracing, management controls, and performance counters.  
  
-   Applications can use OLE DB core services with [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client, but if using the OLE DB cursor engine, they should use the data type compatibility option to avoid any potential problems that might arise because the cursor engine has no knowledge of the new [!INCLUDE[ssVersion2005](../../../includes/ssversion2005-md.md)] data types.  
  
-   [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client supports access to previous [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] databases.  
  
-   [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client does not contain XML integration. [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client supports SELECT ... FOR XML queries, but does not support any other XML functionality. However, [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client does support the **xml** data type introduced in [!INCLUDE[ssVersion2005](../../../includes/ssversion2005-md.md)].  
  
-   [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client supports configuring client-side network libraries using only connection string attributes. If you need more complete network library configuration, you must use [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Configuration Manager.  
  
-   [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client is not compatible with odbcbcp.dll. Applications that use both ODBC and **bcp** APIs must be rebuilt to link with sqlncli11.lib in order to use [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client.  
  
-   [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client is not supported from the Microsoft OLE DB provider for ODBC (MSDASQL). If you are using the MDAC SQLODBC driver with MSDASQL or MDAC SQLODBC driver with ADO, use OLE DB in [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client..  
  
-   MDAC connection strings allow a Boolean value (**true**) for the **Trusted_Connection** keyword. A [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client connection string must use **yes** or **no**.  
  
-   Minor changes have occurred to warnings and errors. Warnings and errors returned by the server now retain the same severity when passed to [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client. You should ensure you have thoroughly tested your application if you depend on trapping particular warnings and errors.  
  
-   [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client has stricter error checking than MDAC, which means that some applications that do not conform strictly to the ODBC and OLE DB specifications may behave differently. For example, the SQLOLEDB provider did not enforce the rule that parameter names must start with '\@' for result parameters, but the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client OLE DB provider does.  
  
-   [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client behaves differently from MDAC in regards to failed connections. For example, MDAC returns cached property values for a connection that has failed, whereas [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client reports an error to the calling application.  
  
-   [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client does not generate Visual Studio Analyzer events, but instead generates Windows tracing events.  
  
-   [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client cannot be used with perfmon. Perfmon is a Windows tool that can only be used with DSNs that use the MDAC SQLODBC driver included with Windows.  
  
-   When [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client is connected to [!INCLUDE[ssVersion2005](../../../includes/ssversion2005-md.md)] and later versions, server error 16947 is returned as a SQL_ERROR. This error occurs when a positioned update or delete fails to update or delete a row. With MDAC when connecting to any version of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)], server error 16947 is returned as a warning (SQL_SUCCESS_WITH_INFO).  
  
-   [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client implements the **IDBDataSourceAdmin** interface, which is an optional OLE DB interface that was not previously implemented, but only the **CreateDataSource** method of this optional interface is implemented. [!INCLUDE[ssNoteDepFutureAvoid](../../../includes/ssnotedepfutureavoid-md.md)]  
  
-   The [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client OLE DB provider returns synonyms in the TABLES and TABLE_INFO schema rowsets, with TABLE_TYPE set to SYNONYM.  
  
-   Return values of data type **varchar(max)**, **nvarchar(max)**, **varbinary(max)**, **xml**, **udt**, or other large object types can not be returned to client versions earlier than [!INCLUDE[ssVersion2005](../../../includes/ssversion2005-md.md)]. If you wish to use these types as return values, you must use [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client.  
  
-   MDAC allows the following statements to be executed at the start of manual and implicit transactions, but [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client does not. They must be executed in autocommit mode.  
  
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
  
     This type mapping affects the values returned for column metadata. For example, a **text** column has a maximum size of 2,147,483,647, but [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client ODBC reports the maximum size of **varchar(max)** columns as SQL_SS_LENGTH_UNLIMITED, and [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client OLE DB reports the maximum size of **varchar(max)** columns as 2,147,483,647 or -1, depending on platform.  
  
-   [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client allows ambiguity in connection strings (for example, some keywords may be specified more than once, and conflicting keywords may be allowed with resolution based on position or precedence) for reasons of backward compatibility. Future releases of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client might not allow ambiguity in connection strings. It is good practice when modifying applications to use [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client to eliminate any dependency on connection string ambiguity.  
  
-   If you use an ODBC or OLE DB call to start transactions, there is a difference in behavior between [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client and MDAC; transactions will begin immediately with [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client, but transactions will begin after the first database access using MDAC. This can affect the behavior of stored procedures and batches because [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] requires \@\@TRANCOUNT to be the same after a batch or stored procedure finishes execution as it was when the batch or stored procedure started.  
  
-   With [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client, ITransactionLocal::BeginTransaction will cause a transaction to be started immediately. With MDAC the transaction start was delayed until the application executed a statement which required a transaction in implicit transaction mode. For more information, see [SET IMPLICIT_TRANSACTIONS &#40;Transact-SQL&#41;](../../../t-sql/statements/set-implicit-transactions-transact-sql.md).  
  
-   You might encounter errors when using [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client driver with System.Data.Odbc to access a [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] server computer that exposes new, [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)]-specific data types or features. System.Data.Odbc provides a generic ODBC implementation and subsequently does not expose vendor specific functionality or extensions. (The [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client driver is updated to natively support the latest [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] features.) To workaround this issue, you can either revert to MDAC, or migrate to System.Data.SqlClient.  
  
 Both [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client and MDAC support read committed transaction isolation using row versioning, but only [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client supports snapshot transaction isolation. (In programming terms, read committed transaction isolation using row versioning is the same as read-committed transaction.).  
  
## See Also  
 [Building Applications with SQL Server Native Client](../../../relational-databases/native-client/applications/building-applications-with-sql-server-native-client.md)  
  
  
