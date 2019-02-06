---
title: "OLE DB Driver for SQL Server Features | Microsoft Docs"
description: "OLE DB Driver for SQL Server features"
ms.custom: ""
ms.date: "06/12/2018"
ms.prod: sql
ms.prod_service: "database-engine, sql-database, sql-data-warehouse, pdw"
ms.reviewer: ""
ms.technology: connectivity
ms.topic: "reference"
helpviewer_keywords: 
  - "MDAC [SQL Server]"
  - "MSOLEDBSQL, about OLE DB Driver for SQL Server"
  - "data access [OLE DB Driver for SQL Server], features"
author: pmasl
ms.author: pelopes
manager: craigg
---
# OLE DB Driver for SQL Server Features
[!INCLUDE[appliesto-ss-asdb-asdw-pdw-md](../../../includes/appliesto-ss-asdb-asdw-pdw-md.md)]

[!INCLUDE[Driver_OLEDB_Download](../../../includes/driver_oledb_download.md)]

  In addition to exposing features of the Windows (formerly Microsoft) Data Access Components (WDAC), OLE DB Driver for SQL Server also implements many other features to expose [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] functionality.  
  
## In This Section    
 [Using Database Mirroring](../../oledb/features/using-database-mirroring.md)  
 Discusses how OLE DB Driver for SQL Server supports the use of mirrored databases, which is the ability to keep a copy, or mirror, of a [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] database on a standby server.  
  
 [Performing Asynchronous Operations](../../oledb/features/performing-asynchronous-operations.md)  
 Discusses how OLE DB Driver for SQL Server supports asynchronous operations, which is the ability to return immediately without blocking on the calling thread.  

[Using Azure Active Directory](using-azure-active-directory.md)  
Discusses new authentication methods introduced in OLE DB driver 18.2.1 that have more secure default settings and allow connecting to an instance of Azure SQL database using a federated identity.

 [Using Multiple Active Result Sets &#40;MARS&#41;](../../oledb/features/using-multiple-active-result-sets-mars.md)  
 Discusses how OLE DB Driver for SQL Server supports multiple active result sets (MARS). MARS enables you to execute and receive multiple result sets using a single database connection  
  
 [Using XML Data Types](../../oledb/features/using-xml-data-types.md)  
 Discusses how OLE DB Driver for SQL Server supports the XML data type, which is a XML-based data type that can be used as a column type, variable type, parameter type, or function return type.  
  
 [Using User-Defined Types](../../oledb/features/using-user-defined-types.md)  
 Discusses how OLE DB Driver for SQL Server supports User-Defined Types (UDT), which extends the SQL type system by allowing you to store objects and custom data structures in a [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] database.  
  
 [Using Large Value Types](../../oledb/features/using-large-value-types.md)  
 Discusses how OLE DB Driver for SQL Server supports large value data types, which are large object data types (LOB).  
  
 [Changing Passwords Programmatically](../../oledb/features/changing-passwords-programmatically.md)  
 Discusses how OLE DB Driver for SQL Server supports the handling of expired passwords so that passwords can now be changed on the client without administrator involvement.  
  
 [Working with Snapshot Isolation](../../oledb/features/working-with-snapshot-isolation.md)  
 Discusses how OLE DB Driver for SQL Server supports the enhancement to row versioning that improves database performance by avoiding reader-writer blocking scenarios.  
  
 [Working with Query Notifications](../../oledb/features/working-with-query-notifications.md)  
 Discusses how OLE DB Driver for SQL Server supports consumer notification on rowset modification.  
  
 [Performing Bulk Copy Operations](../../oledb/features/performing-bulk-copy-operations.md)  
 Discusses how OLE DB Driver for SQL Server supports bulk copy operations that allow the transfer of large amounts of data into or out of a [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] table or view.  
  
 [Using Encryption Without Validation](../../oledb/features/using-encryption-without-validation.md)  
 Discusses how to use OLE DB Driver for SQL Server to encrypt data sent to the server without validating the certificate.  
  
 [Table-Valued Parameters &#40;OLE DB Driver for SQL Server&#41;](../../oledb/features/table-valued-parameters-oledb-driver-for-sql-server.md)  
 Discusses OLE DB Driver for SQL Server support for the table-valued parameters.  
  
 [Large CLR User-Defined Types](../../oledb/features/large-clr-user-defined-types.md)  
 Discusses support for large common language runtime (CLR) user-defined types (UDTs).  
  
 [FILESTREAM Support](../../oledb/features/filestream-support.md)  
 Discusses OLE DB Driver for SQL Server support for the enhanced FILESTREAM feature.  
  
 [Service Principal Name &#40;SPN&#41; Support in Client Connections](../../oledb/features/service-principal-name-spn-support-in-client-connections.md)  
 Discusses how support for service principal names (SPNs) has been extended to enable mutual authentication across all protocols.  
  
 [Sparse Columns Support in OLE DB Driver for SQL Server](../../oledb/features/sparse-columns-support-in-oledb-driver-for-sql-server.md)  
 Discusses OLE DB Driver for SQL Server support for sparse columns.  
  
 [Date and Time Improvements](../../oledb/features/date-and-time-improvements.md)  
 Discusses support added to OLE DB Driver for SQL Server for the date and time data types.  
  
 [Metadata Discovery](../../oledb/features/metadata-discovery.md)  
 Discusses metadata discovery improvements that were made in [!INCLUDE[ssSQL11](../../../includes/sssql11-md.md)].  
  
 [UTF-16 Support in OLE DB Driver for SQL Server](../../oledb/features/utf-16-support-in-oledb-driver-for-sql-server.md)  
 Discusses a behavior change introduced in [!INCLUDE[ssSQL11](../../../includes/sssql11-md.md)]. If you supply a fixed-length buffer when binding a column result or output parameter and if the **wchar** character written into the buffer before the terminating character is a high surrogate code point of a surrogate pair, and if the next **wchar** character is a low surrogate code point, OLE DB Driver for SQL Server will not add the high surrogate code point to the buffer.  
  
 [OLE DB Driver for SQL Server Support for High Availability, Disaster Recovery](../../oledb/features/oledb-driver-for-sql-server-support-for-high-availability-disaster-recovery.md)  
 Discusses how your application can be configured to take advantage of the high-availability, disaster recovery features added in [!INCLUDE[ssSQL11](../../../includes/sssql11-md.md)].  
  
 [Accessing Diagnostic Information in the Extended Events Log](../../oledb/features/accessing-diagnostic-information-in-the-extended-events-log.md)  
 Discusses enhancements to OLE DB Driver for SQL Server and data tracing that gives you access to diagnostic information in the ring buffer and XEvents log.  
  
 [OLE DB Driver for SQL Server Support for LocalDB](../../oledb/features/oledb-driver-for-sql-server-support-for-localdb.md)  
 Discusses OLE DB Driver for SQL Server support for the LocalDB feature.  
  
## See Also  
 [OLE DB Driver for SQL Server](../../oledb/oledb-driver-for-sql-server.md)      
 [OLE DB How-to Topics](../../oledb/ole-db-how-to/ole-db-how-to-topics.md)   
 [Installing OLE DB Driver for SQL Server](../../oledb/applications/installing-oledb-driver-for-sql-server.md)  
  
  
