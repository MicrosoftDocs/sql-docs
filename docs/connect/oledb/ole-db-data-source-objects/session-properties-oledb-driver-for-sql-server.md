---
title: "Session Properties - OLE DB Driver for SQL Server | Microsoft Docs"
description: "Session properties - OLE DB Driver for SQL Server"
ms.custom: ""
ms.date: "06/14/2018"
ms.prod: sql
ms.prod_service: "database-engine, sql-database, sql-data-warehouse, pdw"
ms.reviewer: ""
ms.technology: connectivity
ms.topic: "reference"
helpviewer_keywords: 
  - "sessions [OLE DB]"
  - "OLE DB Driver for SQL Server, sessions"
author: pmasl
ms.author: pelopes
manager: craigg
---
# Session Properties - OLE DB Driver for SQL Server
[!INCLUDE[appliesto-ss-asdb-asdw-pdw-md](../../../includes/appliesto-ss-asdb-asdw-pdw-md.md)]

[!INCLUDE[Driver_OLEDB_Download](../../../includes/driver_oledb_download.md)]

  The OLE DB Driver for SQL Server interprets OLE DB session properties as follows.  
  
|Property ID|Description|  
|-----------------|-----------------|  
|DBPROP_SESS_AUTOCOMMITISOLEVELS|The OLE DB Driver for SQL Server supports all autocommit transaction isolation levels with the exception of the chaos level DBPROPVAL_TI_CHAOS.|  
  
 In the provider-specific property set DBPROPSET_SQLSERVERSESSION, the OLE DB Driver for SQL Server defines the following additional session property.  
  
|Property ID|Description|  
|-----------------|-----------------|  
|SSPROP_QUOTEDCATALOGNAMES|Type: VT_BOOL<br /><br /> R/W: Read/write<br /><br /> Default: VARIANT_FALSE<br /><br /> Description: Quoted identifiers allowed in CATALOG restriction.<br /><br /> VARIANT_TRUE: Quoted identifiers are recognized for a catalog restriction for the schema rowsets that supply distributed query support.<br /><br /> VARIANT_FALSE: Quoted identifiers are not recognized for a catalog restriction for the schema rowsets that supply distributed query support.<br /><br /> For more information about schema rowsets that supply distributed query support, see [Distributed Query Support in Schema Rowsets](../../oledb/ole-db/schema-rowsets-distributed-query-support.md).|  
|SSPROP_ALLOWNATIVEVARIANT|Type: VT_BOOL<br /><br /> R/W: Read/Write<br /><br /> Default: VARIANT_FALSE<br /><br /> Description: Determines if the data fetched in is as DBTYPE_VARIANT or DBTYPE_SQLVARIANT.<br /><br /> VARIANT_TRUE: Column type is returned as DBTYPE_SQLVARIANT in which case the buffer will hold SSVARIANT structure.<br /><br /> VARIANT_FALSE: Column type is returned as DBTYPE_VARIANT and the buffer will have VARIANT structure.|  
|SSPROP_ASYNCH_BULKCOPY|To use asynchronous mode, set the provider specific session property SSPROP_ASYNCH_BULKCOPY to VARIANT_TRUE before calling the BCPExec method. This property is available in the DBPROPSET_SQLSERVERSESSION property set.|  
  
## See Also  
 [Data Source Objects &#40;OLE DB&#41;](../../oledb/ole-db-data-source-objects/data-source-objects-ole-db.md)  
  
  
