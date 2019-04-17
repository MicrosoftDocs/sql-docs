---
title: "Information in Error Interfaces | Microsoft Docs"
description: "Information in error interfaces"
ms.custom: ""
ms.date: "06/14/2018"
ms.prod: sql
ms.prod_service: "database-engine, sql-database, sql-data-warehouse, pdw"
ms.reviewer: ""
ms.technology: connectivity
ms.topic: "reference"
helpviewer_keywords: 
  - "OLE DB Driver for SQL Server, errors"
  - "IErrorRecords interface"
  - "IErrorInfo interface"
  - "OLE DB error handling, error interfaces"
  - "ISQLErrorInfo interface"
  - "errors [OLE DB], error interfaces"
author: pmasl
ms.author: pelopes
manager: craigg
---
# Information in Error Interfaces
[!INCLUDE[appliesto-ss-asdb-asdw-pdw-md](../../../includes/appliesto-ss-asdb-asdw-pdw-md.md)]

[!INCLUDE[Driver_OLEDB_Download](../../../includes/driver_oledb_download.md)]

  The OLE DB Driver for SQL Server reports some error and status information in the OLE DB-defined error interfaces **IErrorInfo**, **IErrorRecords**, and **ISQLErrorInfo**.  
  
 The OLE DB Driver for SQL Server supports **IErrorInfo** member functions as follows.  
  
|Member function|Description|  
|---------------------|-----------------|  
|**GetDescription**|Descriptive error message string.|  
|**GetGUID**|GUID of the interface that defined the error.|  
|**GetHelpContext**|Not supported. Always returns zero.|  
|**GetHelpFile**|Not supported. Always returns NULL.|  
|**GetSource**|String "Microsoft OLE DB Driver for SQL Server".|  
  
 The OLE DB Driver for SQL Server supports consumer-available **IErrorRecords** member functions as follows.  
  
|Member function|Description|  
|---------------------|-----------------|  
|**GetBasicErrorInfo**|Fills an ERRORINFO structure with basic information about an error. An ERRORINFO structure contains members that identify the HRESULT return value for the error, and the provider and interface to which the error applies.|  
|**GetCustomErrorObject**|Returns a reference on interfaces **ISQLErrorInfo,** and [ISQLServerErrorInfo](https://msdn.microsoft.com/library/a8323b5c-686a-4235-a8d2-bda43617b3a1).|  
|**GetErrorInfo**|Returns a reference on an **IErrorInfo** interface.|  
|**GetErrorParameters**|The OLE DB Driver for SQL Server does not return parameters to the consumer through **GetErrorParameters**.|  
|**GetRecordCount**|Count of error records available.|  
  
 The OLE DB Driver for SQL Server supports **ISQLErrorInfo::GetSQLInfo** parameters as follows.  
  
|Parameter|Description|  
|---------------|-----------------|  
|*pbstrSQLState*|Returns a SQLSTATE value for the error. SQLSTATE values are defined in the SQL-92, ODBC and ISO SQL, and API specifications. Neither [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] nor the OLE DB Driver for SQL Server defined implementation-specific SQLSTATE values.|  
|*plNativeError*|Returns the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] error number from **master.dbo.sysmessages** when available. Native errors are available after a successful attempt to initialize a OLE DB Driver for SQL Server data source. Prior to the attempt, the OLE DB Driver for SQL Server always returns zero.|  
  
## See Also  
 [Errors](../../oledb/ole-db-errors/errors.md)  
  
  
