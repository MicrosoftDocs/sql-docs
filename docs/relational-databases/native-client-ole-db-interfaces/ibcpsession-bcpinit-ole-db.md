---
description: "IBCPSession::BCPInit (Native Client OLE DB provider)"
title: "IBCPSession::BCPInit (Native Client OLE DB provider) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: native-client
ms.topic: "reference"
apiname: 
  - "IBCPSession::BCPInit (OLE DB)"
apitype: "COM"
helpviewer_keywords: 
  - "BCPInit method"
ms.assetid: 583096d7-da34-49be-87fd-31210aac81aa
author: markingmyname
ms.author: maghan
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# IBCPSession::BCPInit (Native Client OLE DB Provider)
[!INCLUDE[SQL Server Azure SQL Database Synapse Analytics PDW ](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

> [!IMPORTANT]
> [!INCLUDE[snac-removed-oledb-only](../../includes/snac-removed-oledb-only.md)]

  Initializes the bulk copy structure, performs some error checking, verifies that the data and format file names are correct, and then opens them.  
  
## Syntax  
  
```  
  
HRESULT BCPInit(   
      const wchar_t *pwszTable,  
      const wchar_t *pwszDataFile,  
      const wchar_t *pwszErrorFile,  
      int eDirection);  
```  
  
## Remarks  
 The **BCPInit** method should be called before any other bulk-copy method. The **BCPInit** method performs the necessary initializations for a bulk copy of data between the workstation and [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
 The **BCPInit** method examines the structure of the database source or target table, not the data file. It specifies data format values for the data file based on each column in the database table, view, or SELECT result set. This specification includes the data type of each column, the presence or absence of a length or null indicator and terminator byte strings in the data, and the width of fixed-length data types. The **BCPInit** method sets these values as follows:  
  
-   The data type specified is the data type of the column in the database table, view, or SELECT result set. The data type is enumerated by [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] native data types specified in the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client header file (sqlncli.h). Their values are in the pattern of BCP_TYPE_XXX. The data is represented in its computer form. That is, data from a column of integer data type is represented by a four-byte sequence that is big-or little-endian based on the computer that created the data file.  
  
-   If a database data type is fixed in length, the data file data is also fixed in length. Bulk-copy methods that process data (for example, [IBCPSession::BCPExec](../../relational-databases/native-client-ole-db-interfaces/ibcpsession-bcpexec-ole-db.md)) parse data rows expecting the length of the data in the data file to be identical to the length of the data specified in the database table, view, or SELECT column list. For example, data for a database column defined as `char(13)` must be represented by 13 characters for each row of data in the file. Fixed-length data can be prefixed with a null indicator if the database column allows null values.  
  
-   When copying data to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], the data file must have data for each column in the database table. When copying data from [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], data from all columns in the database table, view, or SELECT result set are copied to the data file.  
  
-   When copying data to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], the ordinal position of a column in the data file must be identical to the ordinal position of the column in the database table. When copying data from [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], the **BCPExec** method places data based on the ordinal position of the column in the database table.  
  
-   If a database data type is variable in length (for example, `varbinary(22)`) or if a database column can contain null values, data in the data file is prefixed by a length/null indicator. The width of the indicator varies based on the data type and version of bulk copy. The [IBCPSession::BCPControl](../../relational-databases/native-client-ole-db-interfaces/ibcpsession-bcpcontrol-ole-db.md) method option BCP_OPTION_FILEFMT provides compatibility between earlier bulk-copy data files and servers running later versions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] by indicating when the width of indicators in the data is narrower than expected.  
  
> [!NOTE]  
>  To change the data format values specified for a data file, use the [IBCPSession::BCPColumns](../../relational-databases/native-client-ole-db-interfaces/ibcpsession-bcpcolumns-ole-db.md) and [IBCPSession::BCPColFmt](../../relational-databases/native-client-ole-db-interfaces/ibcpsession-bcpcolfmt-ole-db.md) methods.  
  
 Bulk copies to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] can be optimized for tables that do not contain indexes by setting the database option **select into/bulkcopy**.  
  
## Arguments  
 *pwszTable*[in]  
 The name of the database table to be copied into or out of. The name can include the database name or the owner name. For example, "pubs.username.titles", "pubs..titles", "username.titles".  
  
 If the eDirection argument is set to BCP_DIRECTION_OUT, the pwszTable argument can be the name of a database view.  
  
 If the eDirection argument is set to BCP_DIRECTION_OUT and a SELECT statement is specified using the **BCPControl** method before the **BCPExec** method is called, the *pwszTable* argument must be set to NULL.  
  
 *pwszDataFile*[in]  
 The name of the user file to be copied into or out of.  
  
 *pwszErrorFile*[in]  
 The name of the error file to be filled with progress messages, error messages, and copies of any rows that could not be copied from a user file to a table. If the *pwszErrorFile* argument is set to NULL, no error file is used.  
  
 *eDirection*[in]  
 The direction of the copy operation, either BCP_DIRECTION_IN or BCP_DIRECTION _OUT. BCP_DIRECTION _IN indicates a copy from a user file to a database table; BCP_DIRECTION _OUT indicates a copy from a database table to a user file.  
  
## Return Code Values  
 S_OK  
 The method succeeded.  
  
 E_FAIL  
 A provider specific error occurred' for detailed information, use the [ISQLServerErrorInfo](isqlservererrorinfo-geterrorinfo-ole-db.md) interface.  
  
 E_OUTOFMEMORY  
 Out-of-memory error.  
  
 E_INVALIDARG  
 One or more of the arguments was not correctly specified. For example, an invalid file name was given.  
  
## See Also  
 [IBCPSession &#40;OLE DB&#41;](../../relational-databases/native-client-ole-db-interfaces/ibcpsession-ole-db.md)   
 [Performing Bulk Copy Operations](../../relational-databases/native-client/features/performing-bulk-copy-operations.md)  
  
