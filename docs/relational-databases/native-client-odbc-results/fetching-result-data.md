---
title: "Fetching Result Data | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database, sql-data-warehouse, pdw"
ms.reviewer: ""
ms.technology: native-client
ms.topic: "reference"
helpviewer_keywords: 
  - "SQLFetchScroll function"
  - "SQL Server Native Client ODBC driver, result sets"
  - "ODBC applications, result sets"
  - "data types [ODBC], fetching"
  - "SQLBindCol function"
  - "result sets [ODBC], fetching"
  - "fetching [ODBC]"
  - "ODBC data types, fetching"
  - "SQLFetch function"
  - "SQL Server Native Client ODBC driver, data types"
  - "SQLGetData function"
ms.assetid: b289c7fb-5017-4d7e-a2d3-19401e9fc4cd
author: MightyPen
ms.author: genemi
manager: craigg
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Fetching Result Data
[!INCLUDE[appliesto-ss-asdb-asdw-pdw-md](../../includes/appliesto-ss-asdb-asdw-pdw-md.md)]
[!INCLUDE[SNAC_Deprecated](../../includes/snac-deprecated.md)]

  An ODBC application has three options for fetching result data.  
  
 The first option is based on [SQLBindCol](../../relational-databases/native-client-odbc-api/sqlbindcol.md). Before fetching the result set, the application uses **SQLBindCol** to bind each column in the result set to a program variable. After the columns have been bound, the driver transfers the data of the current row into the variables bound to the result set columns each time the application calls **SQLFetch** or [SQLFetchScroll](../../relational-databases/native-client-odbc-api/sqlfetchscroll.md). The driver handles data conversions if the result set column and program variable have different data types. If the application has SQL_ATTR_ROW_ARRAY_SIZE set greater than 1, it can bind result columns to arrays of variables, which will all be filled on each call to **SQLFetchScroll**.  
  
 The second option is based on [SQLGetData](../../relational-databases/native-client-odbc-api/sqlgetdata.md). The application does not use **SQLBindCol** to bind result set columns to program variables. After each call to **SQLFetch**, the application calls **SQLGetData** once for each column in the result set. **SQLGetData** instructs the driver to transfer data from a specific result set column to a specific program variable and specifies the data types of the column and variable. This allows the driver to convert data if the result column and program variable have different data types. **Text**, **ntext**, and **image** columns are typically too large to fit into a program variable but can still be retrieved using **SQLGetData**. If the **text**, **ntext**, or **image** data in the result column is larger than the program variable, **SQLGetData** returns SQL_SUCCESS_WITH_INFO and SQLSTATE 01004 (string data, right truncated). Successive calls to **SQLGetData** return successive chunks of the **text** or **image** data. When the end of the data is reached, **SQLGetData** returns SQL_SUCCESS. Each fetch returns a set of rows, or rowset, if SQL_ATTR_ROW_ARRAY_SIZE is greater than 1. Before using **SQLGetData**, you must first use **SQLSetPos** to specify a specific row within the rowset as the current row.  
  
 The third option is to use a mix of **SQLBindCol** and **SQLGetData**. An application could, for example, bind the first ten columns of a result set and then, on each fetch, call **SQLGetData** three times to retrieve the data from three unbound columns. This would typically be used when a result set contains one or more **text** or **image** columns.  
  
 Depending on the cursor options set for the result set, an application can also use the scrolling options of **SQLFetchScroll** to scroll around the result set.  
  
 Excess use of **SQLBindCol** to bind a result set column to a program variable is expensive because **SQLBindCol** causes an ODBC driver to allocate memory. When you bind a result column to a variable, that binding remains in effect until you either call [SQLFreeHandle](../../relational-databases/native-client-odbc-api/sqlfreehandle.md) to free the statement handle or call [SQLFreeStmt](../../relational-databases/native-client-odbc-api/sqlfreestmt.md) with *fOption* set to SQL_UNBIND. The bindings are not automatically undone when the statement completes.  
  
 This logic allows you to effectively deal with executing the same SELECT statement several times with different parameters. Because the result set keeps the same structure, you can bind the result set once, process all the SELECT statements, then call **SQLFreeStmt** with *fOption* set to SQL_UNBIND after the last execution. You should not call **SQLBindCol** to bind the columns in a result set without first calling **SQLFreeStmt** with *fOption* set to SQL_UNBIND to free any previous bindings.  
  
 When using **SQLBindCol**, you can either do row-wise or column-wise binding. Row-wise binding is somewhat faster than column-wise binding.  
  
 You can use **SQLGetData** to retrieve data on a column-by-column basis instead of binding result set columns using **SQLBindCol**. If a result set contains only a few rows, using **SQLGetData** instead of **SQLBindCol** is faster; otherwise, **SQLBindCol** gives the best performance. If you do not always put the data in the same set of variables, you should use **SQLGetData** instead of constantly rebinding. You can only use **SQLGetData** on columns that are in the select list after all columns are bound with **SQLBindCol**. The column must also appear after any columns on which you have already used **SQLGetData**.  
  
 The ODBC functions that deal with moving data into or out of program variables, such as **SQLGetData**, **SQLBindCol**, and [SQLBindParameter](../../relational-databases/native-client-odbc-api/sqlbindparameter.md), support implicit data type conversion. For example, if an application binds an integer column to a character string program variable, the driver automatically converts the data from integer to character before placing it into the program variable.  
  
 Data conversion in applications should be minimized. Unless data conversion is required for the processing done by the application, applications should bind columns and parameters to program variables of the same data type. If the data must be converted from one type to another, however, it is more efficient to have the driver do the conversion than doing it in the application. The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client ODBC driver normally just transfers data directly from the network buffers to the variables of the application. Requesting the driver to do data conversion forces the driver to buffer the data and use CPU cycles to convert the data.  
  
 Program variables should be large enough to hold data transferred in from a column, except for **text**, **ntext**, and **image** data. If an application attempts to retrieve result set data and place it into a variable that is too small to hold it, the driver generates a warning. This forces the driver to allocate memory for the message, and the driver and application both have to spend CPU cycles processing the message and doing error handling. The application should either allocate a variable large enough to hold the data being retrieved or use the SUBSTRING function in the select list to reduce the size of the column in the result set.  
  
 Care must be taken when using SQL_C_DEFAULT to specify the type of the C variable. SQL_C_DEFAULT specifies that the type of the C variable matches the SQL data type of the column or parameter. If SQL_C_DEFAULT is specified for an **ntext**, **nchar**, or **nvarchar** column, Unicode data is returned to the application. This can cause various problems if the application has not been coded to handle Unicode data. The same types of problems can occur with the **uniqueidentifier** (SQL_GUID) data type.  
  
 **text**, **ntext**, and **image** data is typically too large to fit into a single program variable, and is usually processed with **SQLGetData** instead of **SQLBindCol**. When using server cursors, the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client ODBC driver is optimized to not transmit the data for unbound **text**, **ntext**, or **image** columns at the time the row is fetched. The **text**, **ntext**, or **image** data is not actually retrieved from the server until the application issues **SQLGetData** for the column.  
  
 This optimization can be applied to applications so that no **text**, **ntext**, or **image** data is displayed while a user is scrolling up and down a cursor. After the user selects a row, the application can call **SQLGetData** to retrieve the **text**, **ntext**, or **image** data. This saves transmitting the **text**, **ntext**, or **image** data for any of the rows the user does not select and can save the transmission of very large amounts of data.  
  
## See Also  
 [Processing Results &#40;ODBC&#41;](../../relational-databases/native-client-odbc-results/processing-results-odbc.md)  
  
  
