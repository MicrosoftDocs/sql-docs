---
title: "Converting from DB-Library to ODBC Bulk Copy | Microsoft Docs"
ms.custom: ""
ms.date: "03/16/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database, sql-data-warehouse, pdw"
ms.reviewer: ""
ms.technology: native-client
ms.topic: "reference"
helpviewer_keywords: 
  - "converting DB-Library to ODBC bulk copy"
  - "SQL Server Native Client ODBC driver, bulk copy"
  - "bulk copy [ODBC], DB-Library bulk copy"
  - "ODBC, bulk copy operations"
  - "DB-Library bulk copy"
ms.assetid: 0bc15bdb-f19f-4537-ac6c-f249f42cf07f
author: MightyPen
ms.author: genemi
manager: craigg
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Converting from DB-Library to ODBC Bulk Copy
[!INCLUDE[appliesto-ss-asdb-asdw-pdw-md](../../includes/appliesto-ss-asdb-asdw-pdw-md.md)]
[!INCLUDE[SNAC_Deprecated](../../includes/snac-deprecated.md)]

  Converting a DB-Library bulk copy program to ODBC is easy because the bulk copy functions supported by the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client ODBC driver are similar to the DB-Library bulk copy functions, with the following exceptions:  
  
-   DB-Library applications pass a pointer to a DBPROCESS structure as the first parameter of bulk copy functions. In ODBC applications, the DBPROCESS pointer is replaced with an ODBC connection handle.  
  
-   DB-Library applications call **BCP_SETL** before connecting to enable bulk copy operations on a DBPROCESS. ODBC applications instead call [SQLSetConnectAttr](../../relational-databases/native-client-odbc-api/sqlsetconnectattr.md) before connecting to enable bulk operations on a connection handle:  
  
    ```  
    SQLSetConnectAttr(hdbc, SQL_COPT_SS_BCP,  
        (void *)SQL_BCP_ON, SQL_IS_INTEGER);  
    ```  
  
-   The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client ODBC driver does not support DB-Library message and error handlers; you must call **SQLGetDiagRec** to get errors and messages raised by the ODBC bulk copy functions. The ODBC versions of bulk copy functions return the standard bulk copy return codes of SUCCEED or FAILED, not ODBC-style return codes, such as SQL_SUCCESS or SQL_ERROR.  
  
-   The values specified for the DB-Library [bcp_bind](../../relational-databases/native-client-odbc-extensions-bulk-copy-functions/bcp-bind.md)*varlen* parameter are interpreted differently than the ODBC **bcp_bind**_cbData_ parameter.  
  
    |Condition indicated|DB-Library *varlen* value|ODBC *cbData* value|  
    |-------------------------|--------------------------------|-------------------------|  
    |Null values supplied|0|-1 (SQL_NULL_DATA)|  
    |Variable data supplied|-1|-10 (SQL_VARLEN_DATA)|  
    |Zero length character or binary string|NA|0|  
  
     In DB-Library, a *varlen* value of -1 indicates that variable length data is being supplied, which in the ODBC *cbData* is interpreted to mean that only NULL values are being supplied. Change any DB-Library *varlen* specifications of -1 to SQL_VARLEN_DATA and any *varlen* specifications of 0 to SQL_NULL_DATA.  
  
-   The DB-Library **bcp_colfmt**_file_collen_ and the ODBC [bcp_colfmt](../../relational-databases/native-client-odbc-extensions-bulk-copy-functions/bcp-colfmt.md)*cbUserData* have the same issue as the **bcp_bind**_varlen_ and *cbData* parameters noted above. Change any DB-Library *file_collen* specifications of -1 to SQL_VARLEN_DATA and any *file_collen* specifications of 0 to SQL_NULL_DATA.  
  
-   The *iValue* parameter of the ODBC [bcp_control](../../relational-databases/native-client-odbc-extensions-bulk-copy-functions/bcp-control.md) function is a void pointer. In DB-Library, *iValue* was an integer. Cast the values for the ODBC *iValue* to void *.  
  
-   The **bcp_control** option BCPMAXERRS specifies how many individual rows can have errors before a bulk copy operation fails. The default for BCPMAXERRS is 0 (fail on first error) in the DB-Library version of **bcp_control** and 10 in the ODBC version. DB-Library applications that depend on the default of 0 to terminate a bulk copy operation must be changed to call the ODBC **bcp_control** to set BCPMAXERRS to 0.  
  
-   The ODBC **bcp_control** function supports the following options not supported by the DB-Library version of **bcp_control**:  
  
    -   BCPODBC  
  
         When set to TRUE, specifies that **datetime** and **smalldatetime** values saved in character format will have the ODBC timestamp escape sequence prefix and suffix. This only applies to BCP_OUT operations.  
  
         With BCPODBC set to FALSE, a **datetime** value converted to a character string is output as:  
  
        ```  
        1997-01-01 00:00:00.000  
        ```  
  
         With BCPODBC set to TRUE, the same **datetime** value is output as:  
  
        ```  
        {ts '1997-01-01 00:00:00.000' }  
        ```  
  
    -   BCPKEEPIDENTITY  
  
         When set to TRUE, specifies that bulk copy functions insert data values supplied for columns with identity constraints. If this is not set, new identity values are generated for the inserted rows.  
  
    -   BCPHINTS  
  
         Specifies various bulk copy optimizations. This option cannot be used on 6.5 or earlier versions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
    -   BCPFILECP  
  
         Specifies the code page of the bulk copy file.  
  
    -   BCPUNICODEFILE  
  
         Specifies that a character mode bulk copy file is a Unicode file.  
  
-   The ODBC **bcp_colfmt** function does not support the *file_type* indicator of SQLCHAR because it conflicts with the ODBC SQLCHAR typedef. Use SQLCHARACTER instead for **bcp_colfmt**.  
  
-   In the ODBC versions of bulk copy functions, the format for working with **datetime** and **smalldatetime** values in character strings is the ODBC format of yyyy-mm-dd hh:mm:ss.sss; **smalldatetime** values use the ODBC format of yyyy-mm-dd hh:mm:ss.  
  
     The DB-Library versions of the bulk copy functions accept **datetime** and **smalldatetime** values in character strings using several formats:  
  
    -   The default format is *mmm dd yyyy hh:mmxx* where *xx* is either AM or PM.  
  
    -   **datetime** and **smalldatetime** character strings in any format supported by the DB-Library **dbconvert** function.  
  
    -   When the **Use international settings** box is checked on the DB-Library **Options** tab of the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Client Network Utility, the DB-Library bulk copy functions also accept dates in the regional date format defined for the locale setting of the client computer registry.  
  
     The DB-Library bulk copy functions do not accept the ODBC **datetime** and **smalldatetime** formats.  
  
     If the SQL_SOPT_SS_REGIONALIZE statement attribute is set to SQL_RE_ON, the ODBC bulk copy functions accept dates in the regional date format defined for the locale setting of the client computer registry.  
  
-   When outputting **money** values in character format, ODBC bulk copy functions supply four digits of precision and no comma separators; DB-Library versions only supply two digits of precision and include the comma separators.  
  
## See Also  
 [Performing Bulk Copy Operations &#40;ODBC&#41;](../../relational-databases/native-client-odbc-bulk-copy-operations/performing-bulk-copy-operations-odbc.md)   
 [Bulk Copy Functions](../../relational-databases/native-client-odbc-extensions-bulk-copy-functions/sql-server-driver-extensions-bulk-copy-functions.md)  
  
  
