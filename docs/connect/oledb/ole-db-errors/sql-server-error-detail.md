---
title: SQL Server error detail (OLE DB driver)
description: Learn about the provider-specific error interface ISQLServerErrorInfo in OLE DB Driver for SQL Server, which returns details about a SQL Server error.
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: davidengel, sunilbs, mcimfl
ms.date: "06/14/2018"
ms.service: sql
ms.subservice: connectivity
ms.topic: "reference"
ms.custom:
  - ignite-2025
helpviewer_keywords:
  - "OLE DB Driver for SQL Server, errors"
  - "errors [OLE DB], error details"
  - "IErrorRecords interface"
  - "IErrorInfo interface"
  - "OLE DB error handling, error details"
  - "ISQLServerErrorInfo interface"
---
# SQL Server Error Detail
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance Azure Synapse Analytics PDW FabricSQLDB](../../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw-fabricsqldb.md)]

[!INCLUDE[Driver_OLEDB_Download](../../../includes/driver_oledb_download.md)]

  The OLE DB Driver for SQL Server defines the provider-specific error interface [ISQLServerErrorInfo](../ole-db-interfaces/isqlservererrorinfo-geterrorinfo-ole-db.md). The interface returns more detail about a [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] error and is valuable when command execution or rowset operations fail.  
  
 There are two ways to obtain access to **ISQLServerErrorInfo** interface.  
  
 The consumer may call **IErrorRecords::GetCustomerErrorObject** to obtain an **ISQLServerErrorInfo** pointer, as shown in the following code sample. (There is no need to obtain **ISQLErrorInfo.**) Both **ISQLErrorInfo** and **ISQLServerErrorInfo** are custom OLE DB error objects, with **ISQLServerErrorInfo** being the interface to use to obtain information of server errors, including such details as procedure name and line numbers.  
  
```  
// Get the SQL Server custom error object.  
if(FAILED(hr=pIErrorRecords->GetCustomErrorObject(  
   nRec, IID_ISQLServerErrorInfo,  
   (IUnknown**)&pISQLServerErrorErrorInfo)))  
```  
  
 Another way to get an **ISQLServerErrorInfo** pointer is to call the **QueryInterface** method on an already-obtained **ISQLErrorInfo** pointer. Note that because **ISQLServerErrorInfo** contains a superset of the information available from **ISQLErrorInfo**, it makes sense to go directly to **ISQLServerErrorInfo** through **GetCustomerErrorObject**.  
  
 The **ISQLServerErrorInfo** interface exposes one member function, [ISQLServerErrorInfo::GetErrorInfo](../../oledb/ole-db-interfaces/isqlservererrorinfo-geterrorinfo-ole-db.md). The function returns a pointer to an SSERRORINFO structure and a pointer to a string buffer. Both pointers reference memory the consumer must deallocate by using the **IMalloc::Free** method.  
  
 SSERRORINFO structure members are interpreted by the consumer as follows.  
  
|Member|Description|  
|------------|-----------------|  
|*pwszMessage*|[!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] error message. Identical to the string returned in **IErrorInfo::GetDescription**.|  
|*pwszServer*|Name of the instance of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] for the session.|  
|*pwszProcedure*|If appropriate, the name of the procedure in which the error originated. An empty string otherwise.|  
|*lNative*|[!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] native error number. Identical to the value returned in the *plNativeError* parameter of **ISQLErrorInfo::GetSQLInfo**.|  
|*bState*|State of a [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] error message.|  
|*bClass*|Severity of a [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] error message.|  
|*wLineNumber*|When applicable, the line number of a stored procedure on which the error occurred.|  
  
## Related content

- [Errors](errors.md)
- [RAISERROR (Transact-SQL)](../../../t-sql/language-elements/raiserror-transact-sql.md)
