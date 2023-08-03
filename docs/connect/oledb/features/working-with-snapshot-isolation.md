---
title: "Working with Snapshot Isolation"
description: Learn how OLE DB Driver for SQL Server enhancements use snapshot isolation, which enhances concurrency for online transaction processing applications.
author: David-Engel
ms.author: v-davidengel
ms.date: "06/12/2018"
ms.service: sql
ms.subservice: connectivity
ms.topic: "reference"
helpviewer_keywords:
  - "data access [OLE DB Driver for SQL Server], snapshot isolation"
  - "MSOLEDBSQL, snapshot isolation"
  - "isolation levels [SQL Server], snapshot"
  - "DBPROPSET_SESSION property set"
  - "DBDROPSET_DATASOURCEINFO property set"
  - "snapshot isolation [OLE DB Driver for SQL Server]"
  - "OLE DB Driver for SQL Server, snapshot isolation"
  - "SQLGetInfo function"
  - "concurrency [OLE DB Driver for SQL Server]"
  - "SQLSetConnectAttr function"
---
# Working with Snapshot Isolation
[!INCLUDE [SQL Server](../../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

[!INCLUDE[Driver_OLEDB_Download](../../../includes/driver_oledb_download.md)]

  [!INCLUDE[ssVersion2005](../../../includes/ssversion2005-md.md)] introduced a new "snapshot" isolation level that is intended to enhance concurrency for online transaction processing (OLTP) applications. In earlier versions of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)], concurrency was based solely on locking, which can cause blocking and deadlocking problems for some applications. Snapshot isolation depends on enhancements to row versioning and is intended to improve performance by avoiding reader-writer blocking scenarios.  
  
 Transactions that start under snapshot isolation read a database snapshot as of the time when the transaction starts. Keyset, dynamic and static server cursors, opened within a snapshot transaction context behave like much like static cursors that were opened within serializable transactions. However, when the cursors are opened under the snapshot isolation level locks are not taken. This fact can reduce blocking on the server.  
  
## OLE DB Driver for SQL Server  
 The OLE DB Driver for SQL Server has enhancements that take advantage of the snapshot isolation introduced in [!INCLUDE[ssVersion2005](../../../includes/ssversion2005-md.md)]. These enhancements include changes to the DBPROPSET_DATASOURCEINFO and DBPROPSET_SESSION property sets.  
  
### DBPROPSET_DATASOURCEINFO  
 The DBPROPSET_DATASOURCEINFO property set has been changed to indicate that the snapshot isolation level is supported by the addition of the DBPROPVAL_TI_SNAPSHOT value that is used in the DBPROP_SUPPORTEDTXNISOLEVELS property. This new value indicates that the snapshot isolation level is supported whether or not versioning has been enabled on the database. The following table lists the DBPROP_SUPPORTEDTXNISOLEVELS values:  
  
|Property ID|Description|  
|-----------------|-----------------|  
|DBPROP_SUPPORTEDTXNISOLEVELS|Type: VT_I4<br /><br /> R/W: Read only<br /><br /> Description: A bitmask specifying the supported transaction isolation levels. A combination of zero or more of the following:<br /><br /> DBPROPVAL_TI_CHAOS<br /><br /> DBPROPVAL_TI_READUNCOMMITTED<br /><br /> DBPROPVAL_TI_BROWSE<br /><br /> DBPROPVAL_TI_CURSORSTABILITY<br /><br /> DBPROPVAL_TI_READCOMMITTED<br /><br /> DBPROPVAL_TI_REPEATABLEREAD<br /><br /> DBPROPVAL_TI_SERIALIZABLE<br /><br /> DBPROPVAL_TI_ISOLATED<br /><br /> DBPROPVAL_TI_SNAPSHOT|  
  
### DBPROPSET_SESSION  
 The DBPROPSET_SESSION property set has been changed to indicate that the snapshot isolation level is supported by the addition of the DBPROPVAL_TI_SNAPSHOT value that is used in the DBPROP_SESS_AUTOCOMMITISOLEVELS property. This new value indicates that the snapshot isolation level is supported whether or not versioning has been enabled on the database. The following table lists the DBPROP_SESS_AUTOCOMMITISOLEVELS values:
  
|Property ID|Description|  
|-----------------|-----------------|  
|DBPROP_SESS_AUTOCOMMITISOLEVELS|Type: VT_I4<br /><br /> R/W: Read only<br /><br /> Description: Specifies a bitmask that indicates the transaction isolation level while in auto-commit mode. The values that can be set in this bitmask are the same as the values that can be set for DBPROP_SUPPORTEDTXNISOLEVELS.|  
  
> [!NOTE]  
>  The errors DB_S_ERRORSOCCURRED or DB_E_ERRORSOCCURRED will occur if DBPROPVAL_TI_SNAPSHOT is set when using versions of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] earlier than [!INCLUDE[ssVersion2005](../../../includes/ssversion2005-md.md)].  
  
 For information about how snapshot isolation is supported in transactions, see [Supporting Local Transactions](../../oledb/ole-db-transactions/supporting-local-transactions.md).  

  
## See Also  
 [OLE DB Driver for SQL Server Features](../../oledb/features/oledb-driver-for-sql-server-features.md)    
 [Rowset Properties and Behaviors](../../oledb/ole-db-rowsets/rowset-properties-and-behaviors.md)  
  
  
