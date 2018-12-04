---
title: "Data Source Properties (OLE DB) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database, sql-data-warehouse, pdw"
ms.reviewer: ""
ms.technology: native-client
ms.topic: "reference"
helpviewer_keywords: 
  - "SQL Server Native Client OLE DB provider, data source properties"
  - "properties [OLE DB]"
  - "data source properties [OLE DB]"
  - "OLE DB data source properties [SQL Server Native Client]"
ms.assetid: 6e14fefc-4e0b-4847-a833-4cf0abe65d50
author: MightyPen
ms.author: genemi
manager: craigg
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Data Source Properties (OLE DB)
[!INCLUDE[appliesto-ss-asdb-asdw-pdw-md](../../includes/appliesto-ss-asdb-asdw-pdw-md.md)]
[!INCLUDE[SNAC_Deprecated](../../includes/snac-deprecated.md)]

  The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client OLE DB provider implements data source properties as follows.  
  
|Property ID|Description|  
|-----------------|-----------------|  
|DBPROP_CURRENTCATALOG|R/W: Read/write Default: None<br /><br /> Description: The value of DBPROP_CURRENTCATALOG reports the current database for a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client OLE DB provider session. Setting the property value has the identical effect as setting the current database by using the [!INCLUDE[tsql](../../includes/tsql-md.md)] USE *database* statement.<br /><br /> Beginning with [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)], if you call [sp_defaultdb](../../relational-databases/system-stored-procedures/sp-defaultdb-transact-sql.md) and specify the database name in lower case, even if the database was originally created with a mixed case name, DBPROP_CURRENTCATALOG will return the name in lower case. With previous versions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], DBPROP_CURRENTCATALOG will return the expected mixed case.|  
|DBPROP_MULTIPLECONNECTIONS|R/W: Read/write Default: VARIANT_FALSE<br /><br /> Description: If the connection is running a command that does not produce a rowset or produces a rowset that is not a server cursor and you execute another command, a new connection will be created to execute the new command if DBPROP_MULTIPLECONNECTIONS is VARIANT_TRUE.<br /><br /> The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client OLE DB provider will not create another connection if DBPROP_MULTIPLECONNECTION is VARIANT_FALSE or if a transaction is active on the connection. The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client OLE DB provider returns DB_E_OBJECTOPEN if DBPROP_MULTIPLECONNECTIONS is VARIANT_FALSE and returns E_FAIL if there is an active transaction. Transactions and locking are managed by [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] on a per connection basis. If a second connection is generated, the commands on the separate connections do not share locks. To ensure that one command does not block another, hold locks on rows requested by the other command. This also holds true when creating multiple sessions.<br /><br /> Each session has a separate connection.|  
  
 In the provider-specific property set DBPROPSET_SQLSERVERDATASOURCE, the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client OLE DB provider defines the following additional data source properties.  
  
|Property ID|Description|  
|-----------------|-----------------|  
|SSPROP_ENABLEFASTLOAD|R/W: Read/write Default: VARIANT_FALSE<br /><br /> Description: To enable bulk copy from memory, SSPROP_ENABLEFASTLOAD property should be set to VARIANT_TRUE. With this property set on the data source, the newly created session allows consumer access to the [IRowsetFastLoad](../../relational-databases/native-client-ole-db-interfaces/irowsetfastload-ole-db.md) interface.<br /><br /> If the property is set to VARIANT_TRUE, **IRowsetFastLoad** interface is available through **IOpenRowset::OpenRowset** by requesting the **IID_IRowsetFastLoad** interface or by setting **SSPROP_IRowsetFastLoad** to VARIANT_TRUE.|  
|SSPROP_ENABLEBULKCOPY|R/W: Read/write Default: VARIANT_FALSE<br /><br /> Description: To enable bulk copy from files, SSPROP_ENABLEBULKCOPY property should be set to VARIANT_TRUE. With this property set on the data source, consumer access to the IBCPSession interface is available under the same level as Sessions.<br /><br /> SSPROP_IRowsetFastLoad must also be set to VARIANT_TRUE.|  
  
## See Also  
 [Data Source Objects &#40;OLE DB&#41;](../../relational-databases/native-client-ole-db-data-source-objects/data-source-objects-ole-db.md)  
  
  
