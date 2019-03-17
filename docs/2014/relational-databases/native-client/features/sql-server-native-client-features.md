---
title: "SQL Server Native Client Features | Microsoft Docs"
ms.custom: ""
ms.date: "03/09/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: native-client
ms.topic: "reference"
helpviewer_keywords: 
  - "MDAC [SQL Server]"
  - "SQL Server Native Client ODBC driver, about SQL Server Native Client ODBC driver"
  - "SQLNCLI, about SQL Server Native Client"
  - "data access [SQL Server Native Client], features"
ms.assetid: 7bb32865-5afb-41ab-98b4-3fa545ee8953
author: MightyPen
ms.author: genemi
manager: craigg
---
# SQL Server Native Client Features
  In addition to exposing features of the Windows (formerly Microsoft) Data Access Components (WDAC), [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client also implements many other features to expose [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] functionality.  
  
## In This Section  
 [ODBC Driver Behavior Change When Handling Character Conversions](odbc-driver-behavior-change-when-handling-character-conversions.md)  
 Discusses a change of behavior beginning in [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] 2012 Native Client.  
  
 [Using Database Mirroring](using-database-mirroring.md)  
 Discusses how [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client supports the use of mirrored databases, which is the ability to keep a copy, or mirror, of a [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] database on a standby server.  
  
 [Performing Asynchronous Operations](performing-asynchronous-operations.md)  
 Discusses how [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client supports asynchronous operations, which is the ability to return immediately without blocking on the calling thread.  
  
 [Using Multiple Active Result Sets &#40;MARS&#41;](using-multiple-active-result-sets-mars.md)  
 Discusses how [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client supports multiple active result sets (MARS). MARS enables you to execute and receive multiple result sets using a single database connection  
  
 [Using XML Data Types](using-xml-data-types.md)  
 Discusses how [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client supports the XML data type, which is a XML-based data type that can be used as a column type, variable type, parameter type, or function return type.  
  
 [Using User-Defined Types](using-user-defined-types.md)  
 Discusses how [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client supports User-Defined Types (UDT), which extends the SQL type system by allowing you to store objects and custom data structures in a [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] database.  
  
 [Using Large Value Types](using-large-value-types.md)  
 Discusses how [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client supports large value data types, which are large object data types (LOB).  
  
 [Changing Passwords Programmatically](changing-passwords-programmatically.md)  
 Discusses how [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client supports the handling of expired passwords so that passwords can now be changed on the client without administrator involvement.  
  
 [Working with Snapshot Isolation](working-with-snapshot-isolation.md)  
 Discusses how [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client supports the enhancement to row versioning that improves database performance by avoiding reader-writer blocking scenarios.  
  
 [Working with Query Notifications](working-with-query-notifications.md)  
 Discusses how [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client supports consumer notification on rowset modification.  
  
 [Performing Bulk Copy Operations](performing-bulk-copy-operations.md)  
 Discusses how [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client supports bulk copy operations that allow the transfer of large amounts of data into or out of a [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] table or view.  
  
 [Using Encryption Without Validation](using-encryption-without-validation.md)  
 Discusses how to use [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client to encrypt data sent to the server without validating the certificate.  
  
 [Table-Valued Parameters &#40;SQL Server Native Client&#41;](table-valued-parameters-sql-server-native-client.md)  
 Discusses [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client support for the table-valued parameters.  
  
 [Large CLR User-Defined Types](../../clr-integration-database-objects-user-defined-types/clr-user-defined-types.md)  
 Discusses support for large common language runtime (CLR) user-defined types (UDTs).  
  
 [FILESTREAM Support](filestream-support.md)  
 Discusses [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client support for the enhanced FILESTREAM feature.  
  
 [Service Principal Name &#40;SPN&#41; Support in Client Connections](service-principal-name-spn-support-in-client-connections.md)  
 Discusses how support for service principal names (SPNs) has been extended to enable mutual authentication across all protocols.  
  
 [Sparse Columns Support in SQL Server Native Client](sparse-columns-support-in-sql-server-native-client.md)  
 Discusses [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client support for sparse columns.  
  
 [Date and Time Improvements](date-and-time-improvements.md)  
 Discusses support added to [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client for the date and time data types.  
  
 [Metadata Discovery](metadata-discovery.md)  
 Discusses metadata discovery improvements that were made in [!INCLUDE[ssSQL11](../../../includes/sssql11-md.md)].  
  
 [UTF-16 Support in SQL Server Native Client 11.0](utf-16-support-in-sql-server-native-client-11-0.md)  
 Discusses a behavior change introduced in [!INCLUDE[ssSQL11](../../../includes/sssql11-md.md)]. If you supply a fixed-length buffer when binding a column result or output parameter and if the `wchar` character written into the buffer before the terminating character is a high surrogate code point of a surrogate pair, and if the next `wchar` character is a low surrogate code point, [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client will not add the high surrogate code point to the buffer.  
  
 [SQL Server Native Client Support for High Availability, Disaster Recovery](sql-server-native-client-support-for-high-availability-disaster-recovery.md)  
 Discusses how your application can be configured to take advantage of the high-availability, disaster recovery features added in [!INCLUDE[ssSQL11](../../../includes/sssql11-md.md)].  
  
 [Accessing Diagnostic Information in the Extended Events Log](accessing-diagnostic-information-in-the-extended-events-log.md)  
 Discusses enhancements to [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client and data tracing that gives you access to diagnostic information in the ring buffer and XEvents log.  
  
 [SQL Server Native Client Support for LocalDB](sql-server-native-client-support-for-localdb.md)  
 Discusses [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client support for the LocalDB feature.  
  
## See Also  
 [SQL Server Native Client Programming](../sql-server-native-client-programming.md)   
 [ODBC How-to Topics](../../native-client-odbc-how-to/odbc-how-to-topics.md)   
 [OLE DB How-to Topics](../../native-client-ole-db-how-to/ole-db-how-to-topics.md)   
 [Installing SQL Server Native Client](../applications/installing-sql-server-native-client.md)  
  
  
