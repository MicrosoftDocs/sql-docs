---
title: "Information in Error Interfaces"
description: "Information in OLE DB-defined Error Interfaces"
author: markingmyname
ms.author: maghan
ms.date: "03/14/2017"
ms.service: sql
ms.subservice: native-client
ms.topic: "reference"
helpviewer_keywords:
  - "SQL Server Native Client OLE DB provider, errors"
  - "IErrorRecords interface"
  - "IErrorInfo interface"
  - "OLE DB error handling, error interfaces"
  - "ISQLErrorInfo interface"
  - "errors [OLE DB], error interfaces"
---
# Information in OLE DB-defined Error Interfaces
[!INCLUDE[SQL Server Azure SQL Database Synapse Analytics PDW](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

  The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client OLE DB provider reports some error and status information in the OLE DB-defined error interfaces **IErrorInfo**, **IErrorRecords**, and **ISQLErrorInfo**.  
  
 The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client OLE DB provider supports **IErrorInfo** member functions as follows.  
  
|Member function|Description|  
|---------------------|-----------------|  
|**GetDescription**|Descriptive error message string.|  
|**GetGUID**|GUID of the interface that defined the error.|  
|**GetHelpContext**|Not supported. Always returns zero.|  
|**GetHelpFile**|Not supported. Always returns NULL.|  
|**GetSource**|String "Microsoft SQL Server Native Client".|  
  
 The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client OLE DB provider supports consumer-available **IErrorRecords** member functions as follows.  
  
|Member function|Description|  
|---------------------|-----------------|  
|**GetBasicErrorInfo**|Fills an ERRORINFO structure with basic information about an error. An ERRORINFO structure contains members that identify the HRESULT return value for the error, and the provider and interface to which the error applies.|  
|**GetCustomErrorObject**|Returns a reference on interfaces **ISQLErrorInfo,** and [ISQLServerErrorInfo](../native-client-ole-db-interfaces/isqlservererrorinfo-geterrorinfo-ole-db.md).|  
|**GetErrorInfo**|Returns a reference on an **IErrorInfo** interface.|  
|**GetErrorParameters**|The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client OLE DB provider does not return parameters to the consumer through **GetErrorParameters**.|  
|**GetRecordCount**|Count of error records available.|  
  
 The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client OLE DB provider supports **ISQLErrorInfo::GetSQLInfo** parameters as follows.  
  
|Parameter|Description|  
|---------------|-----------------|  
|*pbstrSQLState*|Returns a SQLSTATE value for the error. SQLSTATE values are defined in the SQL-92, ODBC and ISO SQL, and API specifications. Neither [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] nor the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client OLE DB provider defined implementation-specific SQLSTATE values.|  
|*plNativeError*|Returns the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] error number from **master.dbo.sysmessages** when available. Native errors are available after a successful attempt to initialize a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client OLE DB provider data source. Prior to the attempt, the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client OLE DB provider always returns zero.|  
  
## See Also  
 [Errors](../../relational-databases/native-client-ole-db-errors/errors.md)  
  
