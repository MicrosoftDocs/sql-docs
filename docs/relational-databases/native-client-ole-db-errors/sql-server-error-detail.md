---
description: "SQL Server Native Client Error Detail"
title: SQL Server error detail (Native Client OLE DB provider)
ms.custom: ""
ms.date: "03/16/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: native-client
ms.topic: "reference"
helpviewer_keywords: 
  - "SQL Server Native Client OLE DB provider, errors"
  - "errors [OLE DB], error details"
  - "IErrorRecords interface"
  - "IErrorInfo interface"
  - "OLE DB error handling, error details"
  - "ISQLServerErrorInfo interface"
ms.assetid: 51500ee3-3d78-47ec-b90f-ebfc55642e06
author: markingmyname
ms.author: maghan
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# SQL Server Native Client Error Detail
[!INCLUDE[SQL Server Azure SQL Database Synapse Analytics PDW ](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

  The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client OLE DB provider defines the provider-specific error interface [ISQLServerErrorInfo](../native-client-ole-db-interfaces/isqlservererrorinfo-geterrorinfo-ole-db.md). The interface returns more detail about a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] error and is valuable when command execution or rowset operations fail.  
  
 There are two ways to obtain access to **ISQLServerErrorInfo** interface.  
  
 The consumer may call **IErrorRecords::GetCustomerErrorObject** to obtain an **ISQLServerErrorInfo** pointer, as shown in the following code sample. (There is no need to obtain **ISQLErrorInfo.**) Both **ISQLErrorInfo** and **ISQLServerErrorInfo** are custom OLE DB error objects, with **ISQLServerErrorInfo** being the interface to use to obtain information of server errors, including such details as procedure name and line numbers.  
  
```  
// Get the SQL Server custom error object.  
if(FAILED(hr=pIErrorRecords->GetCustomErrorObject(  
   nRec, IID_ISQLServerErrorInfo,  
   (IUnknown**)&pISQLServerErrorErrorInfo)))  
```  
  
 Another way to get an **ISQLServerErrorInfo** pointer is to call the **QueryInterface** method on an already-obtained **ISQLErrorInfo** pointer. Note that because **ISQLServerErrorInfo** contains a superset of the information available from **ISQLErrorInfo**, it makes sense to go directly to **ISQLServerErrorInfo** through **GetCustomerErrorObject**.  
  
 The **ISQLServerErrorInfo** interface exposes one member function, [ISQLServerErrorInfo::GetErrorInfo](../../relational-databases/native-client-ole-db-interfaces/isqlservererrorinfo-geterrorinfo-ole-db.md). The function returns a pointer to an SSERRORINFO structure and a pointer to a string buffer. Both pointers reference memory the consumer must deallocate by using the **IMalloc::Free** method.  
  
 SSERRORINFO structure members are interpreted by the consumer as follows.  
  
|Member|Description|  
|------------|-----------------|  
|*pwszMessage*|[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] error message. Identical to the string returned in **IErrorInfo::GetDescription**.|  
|*pwszServer*|Name of the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] for the session.|  
|*pwszProcedure*|If appropriate, the name of the procedure in which the error originated. An empty string otherwise.|  
|*lNative*|[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] native error number. Identical to the value returned in the *plNativeError* parameter of **ISQLErrorInfo::GetSQLInfo**.|  
|*bState*|State of a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] error message.|  
|*bClass*|Severity of a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] error message.|  
|*wLineNumber*|When applicable, the line number of a stored procedure on which the error occurred.|  
  
## See Also  
 [Errors](../../relational-databases/native-client-ole-db-errors/errors.md)   
 [RAISERROR &#40;Transact-SQL&#41;](../../t-sql/language-elements/raiserror-transact-sql.md)  
  
