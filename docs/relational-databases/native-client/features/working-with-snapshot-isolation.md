---
description: "Working with Snapshot Isolation in SQL Server Native Client"
title: "Working with Snapshot Isolation | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: native-client
ms.topic: "reference"
helpviewer_keywords: 
  - "data access [SQL Server Native Client], snapshot isolation"
  - "SQLNCLI, snapshot isolation"
  - "isolation levels [SQL Server], snapshot"
  - "DBPROPSET_SESSION property set"
  - "DBDROPSET_DATASOURCEINFO property set"
  - "snapshot isolation [SQL Server Native Client]"
  - "SQL Server Native Client OLE DB provider, snapshot isolation"
  - "SQL Server Native Client ODBC driver, snapshot isolation"
  - "SQL Server Native Client, snapshot isolation"
  - "SQLGetInfo function"
  - "concurrency [SQL Server Native Client]"
  - "SQLSetConnectAttr function"
ms.assetid: 39e87eb1-677e-45dd-bc61-83a4025a7756
author: markingmyname
ms.author: maghan
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Working with Snapshot Isolation in SQL Server Native Client
[!INCLUDE [SQL Server](../../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

> [!IMPORTANT] 
> [!INCLUDE[snac-removed-oledb-and-odbc](../../../includes/snac-removed-oledb-and-odbc.md)]

  [!INCLUDE[ssVersion2005](../../../includes/ssversion2005-md.md)] introduced a new "snapshot" isolation level that is intended to enhance concurrency for online transaction processing (OLTP) applications. In earlier versions of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)], concurrency was based solely on locking, which can cause blocking and deadlocking problems for some applications. Snapshot isolation depends on enhancements to row versioning and is intended to improve performance by avoiding reader-writer blocking scenarios.  
  
 Transactions that start under snapshot isolation read a database snapshot as of the time when the transaction starts. One result of this is that keyset, dynamic and static server cursors, when opened within a snapshot transaction context, behave much like static cursors opened within serializable transactions. However, when the cursors are opened under the snapshot isolation level locks are not taken, which can reduce blocking on the server.  
  
## SQL Server Native Client OLE DB Provider  
 The [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client OLE DB provider has enhancements that take advantage of the snapshot isolation introduced in [!INCLUDE[ssVersion2005](../../../includes/ssversion2005-md.md)]. These enhancements include changes to the DBPROPSET_DATASOURCEINFO and DBPROPSET_SESSION property sets.  
  
### DBPROPSET_DATASOURCEINFO  
 The DBPROPSET_DATASOURCEINFO property set has been changed to indicate that the snapshot isolation level is supported by the addition of the DBPROPVAL_TI_SNAPSHOT value that is used in the DBPROP_SUPPORTEDTXNISOLEVELS property. This new value indicates that the snapshot isolation level is supported whether or not versioning has been enabled on the database. The following is a list of the DBPROP_SUPPORTEDTXNISOLEVELS values:  
  
|Property ID|Description|  
|-----------------|-----------------|  
|DBPROP_SUPPORTEDTXNISOLEVELS|Type: VT_I4<br /><br /> R/W: Read only<br /><br /> Description: A bitmask specifying the supported transaction isolation levels. A combination of zero or more of the following:<br /><br /> DBPROPVAL_TI_CHAOS<br /><br /> DBPROPVAL_TI_READUNCOMMITTED<br /><br /> DBPROPVAL_TI_BROWSE<br /><br /> DBPROPVAL_TI_CURSORSTABILITY<br /><br /> DBPROPVAL_TI_READCOMMITTED<br /><br /> DBPROPVAL_TI_REPEATABLEREAD<br /><br /> DBPROPVAL_TI_SERIALIZABLE<br /><br /> DBPROPVAL_TI_ISOLATED<br /><br /> DBPROPVAL_TI_SNAPSHOT|  
  
### DBPROPSET_SESSION  
 The DBPROPSET_SESSION property set has been changed to indicate that the snapshot isolation level is supported by the addition of the DBPROPVAL_TI_SNAPSHOT value that is used in the DBPROP_SESS_AUTOCOMMITISOLEVELS property. This new value indicates that the snapshot isolation level is supported whether or not versioning has been enabled on the database. The following is a list of the DBPROP_SESS_AUTOCOMMITISOLEVELS values:  
  
|Property ID|Description|  
|-----------------|-----------------|  
|DBPROP_SESS_AUTOCOMMITISOLEVELS|Type: VT_I4<br /><br /> R/W: Read only<br /><br /> Description: Specifies a bitmask that indicates the transaction isolation level while in auto-commit mode. The values that can be set in this bitmask are the same as those that can be set for DBPROP_SUPPORTEDTXNISOLEVELS.|  
  
> [!NOTE]  
>  The errors DB_S_ERRORSOCCURRED or DB_E_ERRORSOCCURRED will occur if DBPROPVAL_TI_SNAPSHOT is set when using versions of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] earlier than [!INCLUDE[ssVersion2005](../../../includes/ssversion2005-md.md)].  
  
 For information about how snapshot isolation is supported in transactions, see [Supporting Local Transactions](../../../relational-databases/native-client-ole-db-transactions/supporting-local-transactions.md).  
  
## SQL Server Native Client ODBC Driver  
 The [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client ODBC driver provides support for snapshot isolation though enhancements made to the [SQLSetConnectAttr](../../../relational-databases/native-client-odbc-api/sqlsetconnectattr.md) and [SQLGetInfo](../../../relational-databases/native-client-odbc-api/sqlgetinfo.md) functions.  
  
### SQLSetConnectAttr  
 The **SQLSetConnectAttr** function now supports the use of the SQL_COPT_SS_TXN_ISOLATION attribute. Setting SQL_COPT_SS_TXN_ISOLATION to SQL_TXN_SS_SNAPSHOT indicates that the transaction will take place under the snapshot isolation level.  
  
### SQLGetInfo  
 The [SQLGetInfo](../../../relational-databases/native-client-odbc-api/sqlgetinfo.md) function now supports the SQL_TXN_SS_SNAPSHOT value that has been added to the SQL_TXN_ISOLATION_OPTION info type.  
  
 For information about how snapshot isolation is supported in transactions, see [Cursor Transaction Isolation Level](../../../relational-databases/native-client-odbc-cursors/properties/cursor-transaction-isolation-level.md).  
  
## See Also  
 [SQL Server Native Client Features](../../../relational-databases/native-client/features/sql-server-native-client-features.md)   
 [Rowset Properties and Behaviors](../../../relational-databases/native-client-ole-db-rowsets/rowset-properties-and-behaviors.md)  
  
  
