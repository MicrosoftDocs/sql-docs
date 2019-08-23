---
title: "IBCPSession::BCPControl (OLE DB) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: native-client
ms.topic: "reference"
api_name: 
  - "IBCPSession::BCPControl (OLE DB)"
topic_type: 
  - "apiref"
helpviewer_keywords: 
  - "BCPControl method"
ms.assetid: d58f3fe1-45e3-4e46-8e9c-000971829d99
author: MightyPen
ms.author: genemi
manager: craigg
---
# IBCPSession::BCPControl (OLE DB)
  Sets the options for a bulk-copy operation.  
  
## Syntax  
  
```  
  
HRESULT BCPControl(   
inteOption,  
void *iValue);  
```  
  
## Remarks  
 The **BCPControl** method sets various control parameters for bulk-copy operations including the number of errors allowed before canceling a bulk copy, the numbers of the first and last rows to copy from a data file, and the batch size.  
  
 This method is also used to specify the SELECT statement to use when bulk copying data out from [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. You can set the `eOption` argument to BCP_OPTION_HINTS and `iValue` argument to have a pointer to a wide character string containing the SELECT statement.  
  
 Possible values for *eOption* are:  
  
|Option|Description|  
|------------|-----------------|  
|BCP_OPTION_ABORT|Stops a bulk-copy operation that is already in progress. You can call the **BCPControl** method with an *eOption* argument of BCP_OPTION_ABORT from another thread to stop a running bulk-copy operation. The *iValue* argument is ignored.|  
|BCP_OPTION_BATCH|The number of rows per batch. The default is 0, which indicates all rows in a table when data is being extracted, or all rows in the user data file when data is being copied to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. A value less than 1 resets BCP_OPTION_BATCH to the default.|  
|BCP_OPTION_DELAYREADFMT|A Boolean, if set to true, will cause [IBCPSession::BCPReadFmt](ibcpsession-bcpreadfmt-ole-db.md) to read at execution. If false (the default), IBCPSession::BCPReadFmt will immediately read the format file. A sequence error will occur if `BCP_OPTION_DELAYREADFMT` is true and you call IBCPSession::BCPColumns or IBCPSession::BCPColFmt.<br /><br /> A sequence error will also occur if you call `IBCPSession::BCPControl(BCPDELAYREADFMT, (void *)FALSE))` after calling `IBCPSession::BCPControl(BCPDELAYREADFMT, (void *)TRUE)` and IBCPSession::BCPWriteFmt.<br /><br /> For more information, see [Metadata Discovery](../native-client/features/metadata-discovery.md).|  
|BCP_OPTION_FILECP|The *iValue* argument contains the number of the code page for the data file. You can specify the number of the code page, such as 1252 or 850, or one of the following values:<br /><br /> -   BCP_FILECP_ACP: data in the file is in the Microsoft Windows?? code page of the client.<br />-   BCP_FILECP_OEMCP: data in the file is in the OEM code page of the client (default).<br />-   BCP_FILECP_RAW: data in the file is in the code page of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].|  
|BCP_OPTION_FILEFMT|The version number of the data file format. This can be 80 ([!INCLUDE[ssVersion2000](../../includes/ssversion2000-md.md)]), 90 ([!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)]), 100 ([!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] or [!INCLUDE[ssKilimanjaro](../../includes/sskilimanjaro-md.md)]), 110 ([!INCLUDE[ssSQL11](../../includes/sssql11-md.md)]), or 120 ([!INCLUDE[ssSQL14](../../includes/sssql14-md.md)]. 120 is the default. This is useful for exporting and importing data in formats that were supported by earlier version of the server.  For example, to import data obtained from a text column in a [!INCLUDE[ssVersion2000](../../includes/ssversion2000-md.md)] server into a **varchar(max)** column in a [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] or later server, you should specify 80. Similarly, if you specify 80 while exporting data from a **varchar(max)** column, it will be saved just like text columns are saved in the [!INCLUDE[ssVersion2000](../../includes/ssversion2000-md.md)] format, and can be imported into a text column of a [!INCLUDE[ssVersion2000](../../includes/ssversion2000-md.md)] server.|  
|BCP_OPTION_FIRST|The first row of data of the file or table to copy. The default is 1; a value less than 1 resets this option to its default.|  
|BCP_OPTION_FIRSTEX|For BCP out operations, specifies the first row of the database table to copy into the data file.<br /><br /> For BCP in operations, specifies the first row of the data file to copy into the database table.<br /><br /> The *iValue* parameter is expected to be the address of a signed 64-bit integer that contains the value. The maximum value that can be passed to BCPFIRSTEX 2^63-1.|  
|BCP_OPTION_FMTXML|Used to specify that the format file generated should be in an XML format. It is off by default and by default the format files are saved as text files. The XML format file provides greater flexibility but with some added constraints. For example, you can not specify the prefix and terminator for a field simultaneously which is possible in older format files. **Note:**  XML format files are only supported when [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] tools are installed along with [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client.|  
|BCP_OPTION_HINTS|The *iValue* argument contains a wide character string pointer. The string addressed specifies either [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] bulk-copy processing hints or a [!INCLUDE[tsql](../../includes/tsql-md.md)] statement that returns a result set. If a [!INCLUDE[tsql](../../includes/tsql-md.md)] statement is specified that returns more than one result set, all result sets after the first are ignored.|  
|BCP_OPTION_KEEPIDENTITY|When the *iValue* argument is set to TRUE, this option specifies that the bulk copy methods insert data values supplied for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] columns defined with an identity constraint. The input file must supply values for the identity columns. If this is not set, new identity values are generated for the inserted rows. Any data present in the file for the identity columns is ignored.|  
|BCP_OPTION_KEEPNULLS|Specifies whether empty data values in the file will be converted to NULL values in the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] table. When the *iValue* argument is set to TRUE, empty values will be converted to NULL in the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] table. The default is for empty values to be converted to a default value for the column in the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] table if a default exists.|  
|BCP_OPTION_LAST|The last row to copy. The default is to copy all rows. A value less than 1 resets this option to its default.|  
|BCP_OPTION_LASTEX|For BCP out operations, specifies the last row of the database table to copy into the data file.<br /><br /> For BCP in operations, specifies the last row of the data file to copy into the database table.<br /><br /> The *iValue* parameter is expected to be the address of a signed 64-bit integer that contains the value. The maximum value that can be passed to BCPLASTEX is 2^63-1.|  
|BCP_OPTION_MAXERRS|The number of errors allowed before the bulk copy operation fails. The default is 10. A value less than 1 resets this option to its default. Bulk copy imposes a maximum of 65,535 errors. An attempt to set this option to a value larger than 65,535 results in the option being set to 65,535.|  
|BCP_OPTION_ROWCOUNT|Returns the number of rows affected by the current (or last) BCP operation.|  
|BCP_OPTION_TEXTFILE|The data file is not a binary file, but is a text file. BCP does the detection whether the text file is Unicode or not by checking the Unicode byte marker in the first 2 bytes of the data file.|  
|BCP_OPTION_UNICODEFILE|When set to TRUE, this option specifies that the input file is a Unicode file format.|  
  
## Arguments  
 *eOption*[in]  
 Set to one of the options listed in the remarks section above.  
  
 *iValue*[in]  
 The value for the specified *eOption*. The *iValue* argument is an integer value cast to a void pointer to allow for future expansion to 64 bit values.  
  
## Return Code Values  
 S_OK  
 The method succeeded.  
  
 E_FAIL  
 A provider-specific error occurred; for detailed information, use the [ISQLServerErrorInfo](../../database-engine/dev-guide/isqlservererrorinfo-ole-db.md) interface.  
  
 E_UNEXPECTED  
 The call to the method was unexpected. For example, the [IBCPSession::BCPInit](ibcpsession-bcpinit-ole-db.md) method was not called before calling this function.  
  
 E_OUTOFMEMORY  
 Out-of-memory error.  
  
## See Also  
 [IBCPSession &#40;OLE DB&#41;](ibcpsession-ole-db.md)   
 [Performing Bulk Copy Operations](../native-client/features/performing-bulk-copy-operations.md)  
  
  
