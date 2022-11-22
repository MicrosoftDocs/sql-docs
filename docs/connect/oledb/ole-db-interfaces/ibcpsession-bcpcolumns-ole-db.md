---
title: "IBCPSession::BCPColumns (OLE DB driver)"
description: "Learn how the IBCPSession::BCPColumns method sets the number of fields to be bound to the columns in a SQL Server table in OLE DB Driver for SQL Server."
author: David-Engel
ms.author: v-davidengel
ms.date: "06/14/2018"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
helpviewer_keywords:
  - "BCPColumns method"
apiname: "IBCPSession::BCPColumns (OLE DB)"
apitype: "COM"
---
# IBCPSession::BCPColumns (OLE DB)
[!INCLUDE [SQL Server](../../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

[!INCLUDE[Driver_OLEDB_Download](../../../includes/driver_oledb_download.md)]

  Sets the number of fields that are to be bound to the columns in a [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] table.  
  
## Syntax  
  
```  
  
HRESULT BCPColumns(   
      DBCOUNTITEM nColumns);  
```  
  
## Remarks  
 Internally it calls [IBCPSession::BCPColFmt](../../oledb/ole-db-interfaces/ibcpsession-bcpcolfmt-ole-db.md) to set the default values for field data. These default values are obtained from the SQL Server column information that the provider internally retrieves when the table name is specified through [IBCPSession::BCPInit](../../oledb/ole-db-interfaces/ibcpsession-bcpinit-ole-db.md).  
  
> [!NOTE]  
>  This method can be called only after **BCPInit** has been called with a valid file name.  
  
 You should call this method only if you intend to use a user-file format that differs from the default. For more information about a description of the default user-file format, see the **BCPInit** method.  
  
 After calling the **BCPColumns** method, you must call the **BCPColFmt** method for each column in the user file to completely define a custom file format.  
  
## Arguments  
 *nColumns*[in]  
 The total number of fields in the user file. Even if you are preparing to bulk copy data from the user file to a SQL Server table and do not intend to copy all fields in the user file, you must still set the *nColumns* argument to the total number of user-file fields. The skipped fields can then be specified through **BCPColFmt**.  
  
## Return Code Values  
 S_OK  
 The method succeeded.  
  
 E_FAIL  
 A provider-specific error occurred; for detailed information, use the [ISQLServerErrorInfo](./isqlservererrorinfo-geterrorinfo-ole-db.md) interface.  
  
 E_UNEXPECTED  
 The call to the method was unexpected. For example, the **BCPInit** method was not called before calling this method. Also occurs when this method is called more than once for a bulk copy operation.  
  
 E_OUTOFMEMORY  
 Out-of-memory error.  
  
## See Also  
 [IBCPSession &#40;OLE DB&#41;](../../oledb/ole-db-interfaces/ibcpsession-ole-db.md)   
 [Performing Bulk Copy Operations](../../oledb/features/performing-bulk-copy-operations.md)  
  
