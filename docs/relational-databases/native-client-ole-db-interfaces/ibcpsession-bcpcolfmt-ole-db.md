---
description: "IBCPSession::BCPColFmt (Native Client OLE DB provider)"
title: "IBCPSession::BCPColFmt (Native Client OLE DB provider) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: native-client
ms.topic: "reference"
apiname: 
  - "IBCPSession::BCPColFmt (OLE DB)"
apitype: "COM"
helpviewer_keywords: 
  - "BCPColFmt method"
ms.assetid: 2852f4ba-f1c6-4c4c-86b2-b77e4abe70de
author: markingmyname
ms.author: maghan
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# IBCPSession::BCPColFmt (Native Client OLE DB Provider)
[!INCLUDE[SQL Server Azure SQL Database Synapse Analytics PDW ](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

> [!IMPORTANT]
> [!INCLUDE[snac-removed-oledb-only](../../includes/snac-removed-oledb-only.md)]

  Creates a binding between program variables and [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] columns.  
  
## Syntax  
  
```  
  
HRESULT BCPColFmt(   
      DBORDINAL idxUserDataCol,  
      int eUserDataType,  
      int cbIndicator,  
      int cbUserData,  
      BYTE *pbUserDataTerm,  
      int cbUserDataTerm,  
      DBORDINAL idxServerCol);  
```  
  
## Remarks  
 The **BCPColFmt** method is used to create a binding between BCP data file fields and [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] columns. It takes in the length, type, terminator, and prefix length of a column as parameters and sets each of these properties for individual fields.  
  
 If the user chooses interactive mode, this method is called twice; once to set the column format according to the default values (which are according to the type of the server column), and once to set the format according to the column type of the choice of the client chosen during interactive mode, for each column.  
  
 In non-interactive modes, it is called only once per column to set the type of each column to character or native type, and to set the column and row terminators.  
  
 The **BCPColFmt** method allows you to specify the user-file format for bulk copies. For bulk copy, a format contains the following parts:  
  
-   A mapping from user-file fields to database columns.  
  
-   The data type of each user-file field.  
  
-   The length of the optional indicator for each field.  
  
-   The maximum length of data per user-file field.  
  
-   The optional terminating byte sequence for each field.  
  
-   The length of the optional terminating byte sequence.  
  
 Each call to **BCPColFmt** specifies the format for one user-file field. For example, to change the default settings for three fields in a five-field user data file, first call `BCPColumns(5)`, and then call **BCPColFmt** five times, with three of those calls setting your custom format. For the remaining two calls, set *eUserDataType* to BCP_TYPE_DEFAULT, and set *cbIndicator*, *cbUserData*, and *cbUserDataTerm* to 0, BCP_VARIABLE_LENGTH, and 0 respectively. This procedure copies all five columns, three with your customized format and two with the default format.  
  
> [!NOTE]  
>  The [IBCPSession::BCPColumns](../../relational-databases/native-client-ole-db-interfaces/ibcpsession-bcpcolumns-ole-db.md) method must be called before any calls to **BCPColFmt**. You must call **BCPColFmt** once for each column in the user file. Calling **BCPColFmt** more than once for any user-file column causes an error.  
  
 You do not have to copy all of the data in a user file to a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] table. To skip a column, specify the format of the data for the column setting the idxServerCol parameter to 0. In order to skip a field, you still need all the information for the method to work correctly.  
  
 **Note** The [IBCPSession::BCPWriteFmt](../../relational-databases/native-client-ole-db-interfaces/ibcpsession-bcpwritefmt-ole-db.md) function can be used to persist the format specification provided through **BCPColFmt**.  
  
## Arguments  
 *idxUserDataCol*[in]  
 Index of field in the user's data file.  
  
 *eUserDataType*[in]  
 The data type of field in the user's data file. The data types available are listed in the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client header file (sqlncli.h) with BCP_TYPE_XXX format, for example, BCP_TYPE_SQLINT4. If the BCP_TYPE_DEFAULT value is specified, the provider tries to use the same type as the table or view column type. For bulk copy operations out of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and into a file when the **eUserDataType** argument is BCP_TYPE_SQLDECIMAL or BCP_TYPE_SQLNUMERIC:  
  
-   If the source column is not decimal or numeric, the default precision and scale are used.  
  
-   If the source column is decimal or numeric, the precision and scale of the source column are used.  
  
 *cbIndicator*[in]  
 Prefix length for the field. The default is BCP_PREFIX_DEFAULT. The valid lengths for the prefix are 0, 1, 2, 4 and 8. A prefix size of 8 is most commonly used to indicate that the field is chunked. This is used for efficiently bulk copying large value type columns.  
  
 *cbUserData*[in]  
 The maximum length, in bytes, of this field's data in the user file, not including the length of any length indicator or terminator.  
  
 Setting **cbUserData** to BCP_LENGTH_NULL indicates that all values in the data file fields are, or should be set to NULL. Setting **cbUserData** to BCP_LENGTH_VARIABLE indicates that the system should determine the length of data for each field. For some fields, this could mean that a length/null indicator is generated to precede data on a copy from [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], or that the indicator is expected in data copied to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
 For [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] character and binary data types, **cbUserData** can be BCP_LENGTH_VARIABLE, BCP_LENGTH_NULL, 0, or some positive value. If **cbUserData** is BCP_LENGTH_VARIABLE, the system uses either the length indicator, if present, or a terminator sequence to determine the length of the data. If both a length indicator and a terminator sequence are supplied, bulk copy uses the one that results in the least amount of data being copied. If **cbUserData** is BCP_LENGTH_VARIABLE, the data type is a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] character or binary type, and if neither a length indicator nor a terminator sequence is specified, the system returns an error message.  
  
 If **cbUserData** is 0 or a positive value, the system uses **cbUserData** as the maximum data length. However, if, in addition to a positive **cbUserData**, a length indicator or terminator sequence is provided, the system determines the data length by using the method that results in the least amount of data being copied.  
  
 The **cbUserData** value represents the count of bytes of data. If character data is represented by Unicode wide characters, then a positive **cbUserData** parameter value represents the number of characters multiplied by the size, in bytes, of each character.  
  
 *pbUserDataTerm*[size_is][in]  
 The terminator sequence to be used for the field. This parameter is useful mainly for character data types because all other types are of fixed length or, in the case of binary data, require an indicator of length to accurately record the number of bytes present.  
  
 To avoid terminating extracted data, or to indicate that data in a user file is not terminated, set this parameter to NULL.  
  
 If more than one means of specifying a user-file column length is used (such as a terminator and a length indicator, or a terminator and a maximum column length), bulk copy chooses the one that results in the least amount of data being copied.  
  
 The bulk copy API performs Unicode-to-MBCS character conversion as required. Care must be taken to ensure that both the terminator byte string and the length of the byte string are set correctly.  
  
 *cbUserDataTerm*[in]  
 The length, in bytes, of the terminator sequence to be used for the column. If no terminator is present or desired in the data, set this value to 0.  
  
 *idxServerCol*[in]  
 The ordinal position of the column in the database table. The first column number is 1. The ordinal position of a column is reported by **IColumnsInfo::GetColumnInfo** or similar methods. If this value is 0, bulk copy ignores the field in the data file.  
  
## Return Code Values  
 S_OK  
 The method succeeded.  
  
 E_FAIL  
 A provider specific error occurred, for detailed information use the [ISQLServerErrorInfo](isqlservererrorinfo-geterrorinfo-ole-db.md) interface.  
  
 E_UNEXPECTED  
 The call to the method was unexpected. For example, the [IBCPSession::BCPInit](../../relational-databases/native-client-ole-db-interfaces/ibcpsession-bcpinit-ole-db.md) method was not called before calling this method.  
  
 E_INVALIDARG  
 The argument was invalid.  
  
 E_OUTOFMEMORY  
 Out of memory error.  
  
## See Also  
 [IBCPSession &#40;OLE DB&#41;](../../relational-databases/native-client-ole-db-interfaces/ibcpsession-ole-db.md)   
 [Performing Bulk Copy Operations](../../relational-databases/native-client/features/performing-bulk-copy-operations.md)  
  
