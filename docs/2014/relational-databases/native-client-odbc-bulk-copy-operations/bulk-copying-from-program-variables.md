---
title: "Bulk Copying from Program Variables | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: native-client
ms.topic: "reference"
helpviewer_keywords: 
  - "bulk copy [ODBC], program variables"
  - "bcp_bind function"
  - "mapping data types [ODBC]"
  - "SQL Server Native Client ODBC driver, bulk copy"
  - "data types [ODBC], bulk copying"
  - "ODBC, bulk copy operations"
  - "program variables [ODBC]"
ms.assetid: e4284a1b-7534-4b34-8488-b8d05ed67b8c
author: MightyPen
ms.author: genemi
manager: craigg
---
# Bulk Copying from Program Variables
  You can bulk copy directly from program variables. After allocating variables to hold the data for a row and calling [bcp_init](../native-client-odbc-extensions-bulk-copy-functions/bcp-init.md) to start the bulk copy, call [bcp_bind](../native-client-odbc-extensions-bulk-copy-functions/bcp-bind.md) for each column to specify the location and format of the program variable to be associated with the column. Fill each variable with data, then call [bcp_sendrow](../native-client-odbc-extensions-bulk-copy-functions/bcp-sendrow.md) to send one row of data to the server. Repeat the process of filling the variables and calling **bcp_sendrow** until all the rows have been sent to the server, then call [bcp_done](../native-client-odbc-extensions-bulk-copy-functions/bcp-done.md) to specify that the operation is complete.  
  
 The **bcp_bind**_pData_ parameter contains the address of the variable being bound to the column. The data for each column can be stored in one of two ways:  
  
-   Allocate one variable to hold the data.  
  
-   Allocate an indicator variable followed immediately by the data variable.  
  
 The indicator variable indicates the length of the data for variable-length columns, and also indicates NULL values if the column allows NULLs. If only a data variable is used, then the address of this variable is stored in the **bcp_bind**_pData_ parameter. If an indicator variable is used, the address of the indicator variable is stored in the **bcp_bind**_pData_ parameter. The bulk copy functions calculate the location of the data variable by adding the **bcp_bind**_cbIndicator_ and *pData* parameters.  
  
 **bcp_bind** supports three methods for dealing with variable-length data:  
  
-   Use *cbData* with only a data variable. Place the length of the data in *cbData*. Each time the length of the data to be bulk copied changes, call [bcp_collen](../native-client-odbc-extensions-bulk-copy-functions/bcp-collen.md)to reset *cbData*. If one of the other two methods is being used, specify SQL_VARLEN_DATA for *cbData*. If all the data values being supplied for a column are NULL, specify SQL_NULL_DATA for *cbData*.  
  
-   Use indicator variables. As each new data value is moved into the data variable, store the length of the value in the indicator variable. If one of the other two methods is being used, specify 0 for *cbIndicator*.  
  
-   Use terminator pointers. Load the **bcp_bind**_pTerm_ parameter with the address of the bit pattern that terminates the data. If one of the other two methods is being used, specify NULL for *pTerm*.  
  
 All three of these methods can be used on the same **bcp_bind** call, in which case the specification that results in the smallest amount of data being copied is used.  
  
 The **bcp_bind**_type_ parameter uses DB-Library data type identifiers, not ODBC data type identifiers. DB-Library data type identifiers are defined in sqlncli.h for use with the ODBC **bcp_bind** function.  
  
 Bulk copy functions do not support all ODBC C data types. For example, the bulk copy functions do not support the ODBC SQL_C_TYPE_TIMESTAMP structure, so use [SQLBindCol](../native-client-odbc-api/sqlbindcol.md) or [SQLGetData](../native-client-odbc-api/sqlgetdata.md) to convert ODBC SQL_TYPE_TIMESTAMP data to a SQL_C_CHAR variable. If you then use **bcp_bind** with a *type* parameter of SQLCHARACTER to bind the variable to a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] **datetime** column, the bulk copy functions convert the timestamp escape clause in the character variable to the proper datetime format.  
  
 The following table lists the recommended data types to use in mapping from an ODBC SQL data type to a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] data type.  
  
|ODBC SQLdata type|ODBC C data type|bcp_bind *type* parameter|SQL Server data type|  
|-----------------------|----------------------|--------------------------------|--------------------------|  
|SQL_CHAR|SQL_C_CHAR|SQLCHARACTER|**character**<br /><br /> **char**|  
|SQL_VARCHAR|SQL_C_CHAR|SQLCHARACTER|**varchar**<br /><br /> **character varying**<br /><br /> **char varying**<br /><br /> **sysname**|  
|SQL_LONGVARCHAR|SQL_C_CHAR|SQLCHARACTER|**text**|  
|SQL_WCHAR|SQL_C_WCHAR|SQLNCHAR|**nchar**|  
|SQL_WVARCHAR|SQL_C_WCHAR|SQLNVARCHAR|**nvarchar**|  
|SQL_WLONGVARCHAR|SQL_C_WCHAR|SQLNTEXT|**ntext**|  
|SQL_DECIMAL|SQL_C_CHAR|SQLCHARACTER|**decimal**<br /><br /> **dec**<br /><br /> **money**<br /><br /> **smallmoney**|  
|SQL_NUMERIC|SQL_C_NUMERIC|SQLNUMERICN|**numeric**|  
|SQL_BIT|SQL_C_BIT|SQLBIT|**bit**|  
|SQL_TINYINT (signed)|SQL_C_SSHORT|SQLINT2|**smallint**|  
|SQL_TINYINT (unsigned)|SQL_C_UTINYINT|SQLINT1|**tinyint**|  
|SQL_SMALL_INT (signed)|SQL_C_SSHORT|SQLINT2|**smallint**|  
|SQL_SMALL_INT (unsigned)|SQL_C_SLONG|SQLINT4|**int**<br /><br /> **integer**|  
|SQL_INTEGER (signed)|SQL_C_SLONG|SQLINT4|**int**<br /><br /> **integer**|  
|SQL_INTEGER (unsigned)|SQL_C_CHAR|SQLCHARACTER|**decimal**<br /><br /> **dec**|  
|SQL_BIGINT (signed and unsigned)|SQL_C_CHAR|SQLCHARACTER|**bigint**|  
|SQL_REAL|SQL_C_FLOAT|SQLFLT4|**real**|  
|SQL_FLOAT|SQL_C_DOUBLE|SQLFLT8|**float**|  
|SQL_DOUBLE|SQL_C_DOUBLE|SQLFLT8|**float**|  
|SQL_BINARY|SQL_C_BINARY|SQLBINARY|**binary**<br /><br /> **timestamp**|  
|SQL_VARBINARY|SQL_C_BINARY|SQLBINARY|**varbinary**<br /><br /> **binary varying**|  
|SQL_LONGVARBINARY|SQL_C_BINARY|SQLBINARY|**image**|  
|SQL_TYPE_DATE|SQL_C_CHAR|SQLCHARACTER|**datetime**<br /><br /> **smalldatetime**|  
|SQL_TYPE_TIME|SQL_C_CHAR|SQLCHARACTER|**datetime**<br /><br /> **smalldatetime**|  
|SQL_TYPE_TIMESTAMP|SQL_C_CHAR|SQLCHARACTER|**datetime**<br /><br /> **smalldatetime**|  
|SQL_GUID|SQL_C_GUID|SQLUNIQUEID|**uniqueidentifier**|  
|SQL_INTERVAL_|SQL_C_CHAR|SQLCHARACTER|**char**|  
  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] does not have signed **tinyint**, unsigned **smallint**, or unsigned **int** data types. To prevent the loss of data values when migrating these data types, create the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] table with the next largest integer data type. To prevent users from later adding values outside the range allowed by the original data type, apply a rule to the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] column to restrict the allowable values to the range supported by the data type in the original source:  
  
```  
CREATE TABLE Sample_Ints(STinyIntCol   SMALLINT,  
USmallIntCol INT)  
GO  
CREATE RULE STinyInt_Rule  
AS   
@range >= -128 AND @range <= 127  
GO  
CREATE RULE USmallInt_Rule  
AS   
@range >= 0 AND @range <= 65535  
GO  
sp_bindrule STinyInt_Rule, 'Sample_Ints.STinyIntCol'  
GO  
sp_bindrule USmallInt_Rule, 'Sample_Ints.USmallIntCol'  
GO  
```  
  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] does not support interval data types directly. An application can, however, store interval escape sequences as character strings in a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] character column. The application can read them for later use, but they cannot be used in [!INCLUDE[tsql](../../includes/tsql-md.md)] statements.  
  
 The bulk copy functions can be used to quickly load data into [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] that has been read from an ODBC data source. Use [SQLBindCol](../native-client-odbc-api/sqlbindcol.md) to bind the columns of a result set to program variables, then use **bcp_bind** to bind the same program variables to a bulk copy operation. Calling [SQLFetchScroll](../native-client-odbc-api/sqlfetchscroll.md) or **SQLFetch** then fetches a row of data from the ODBC data source into the program variables, and calling [bcp_sendrow](../native-client-odbc-extensions-bulk-copy-functions/bcp-sendrow.md) bulk copies the data from the program variables to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] .  
  
 An application can use the [bcp_colptr](../native-client-odbc-extensions-bulk-copy-functions/bcp-colptr.md) function anytime it needs to change the address of the data variable originally specified in the **bcp_bind** _pData_ parameter. An application can use the [bcp_collen](../native-client-odbc-extensions-bulk-copy-functions/bcp-collen.md) function anytime it needs to change the data length originally specified in the **bcp_bind**_cbData_ parameter.  
  
 You cannot read data from [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] into program variables using bulk copy; there is nothing like a "bcp_readrow" function. You can only send data from the application to the server.  
  
## See Also  
 [Performing Bulk Copy Operations &#40;ODBC&#41;](performing-bulk-copy-operations-odbc.md)  
  
  
