---
title: "Vector Data Type (ODBC)"
description: Guidance for using the new vector SQL data type through the Microsoft ODBC driver for SQL Server.
author: David-Engel
ms.author: davidengel
ms.reviewer: randolphwest
ms.date: 02/10/2026
ms.service: sql
ms.subservice: connectivity
ms.topic: article
ai-usage: ai-assisted
---
# Vector data type (ODBC)

This article documents the **vector** SQL data type as implemented by the Microsoft ODBC Driver for SQL Server starting in version 18.6.1.1. This document outlines the Microsoft driver's behavior for **vector**, and provides usage guidance, API-specific notes, and code snippets. For an overview of vector data types, see [Vector data type](../../t-sql/data-types/vector-data-type.md).

## Overview

The Microsoft ODBC Driver for SQL Server natively supports the **vector** data type. Applications can efficiently store, retrieve, and process fixed-dimension numerical embeddings commonly used in machine learning and AI workloads. The driver exposes vector support through standard ODBC APIs and C data types. Applications can interoperate with SQL Server vector columns without changing existing ODBC workflows.

**Applies to**: Microsoft ODBC Driver for SQL Server 18.6.1.1 and later versions.

For the Microsoft driver (18.6.1.1), vector support is disabled by default and must be explicitly enabled.

## Native C representation

When vector support is enabled, **vector** columns are exchanged using a typed C structure named `SQL_SS_VECTOR_STRUCT`.

```c
    typedef struct tagSQL_SS_VECTOR_STRUCT {
        SQLSMALLINT dimension;   /* Number of elements */
        SQLSMALLINT  type;        /* Element type indicator (0 = float32) */
        union {
            float *f32;           /* Pointer to float32 data */
        } data;
    } SQL_SS_VECTOR_STRUCT;
```

- `dimension`: describes the number of elements in the vector
- `type`: identifies the base element type (currently `float32`)
- `data.f32`: points to the application buffer containing vector values

## Enable vector support

- The Microsoft driver exposes a driver-specific C binding `SQL_C_SS_VECTOR` and supports `SQL_C_BINARY` for vector output when you configure the connection or driver option `vectorTypeSupport` with the `v1` value. In practice:

  - When you enable `vectorTypeSupport=v1`, retrieval APIs (for example, `SQLGetData` and `SQLBindCol`) can return **vector** columns as either `SQL_C_SS_VECTOR` or `SQL_C_BINARY`. `SQL_C_SS_VECTOR` returns the vector in a compact, typed form. `SQL_C_BINARY` returns a **varbinary** payload.

  - For input or parameter binding, the Microsoft driver (18.6.1.1 with `vectorTypeSupport=v1`) supports both `SQL_C_SS_VECTOR` and `SQL_C_BINARY`. `SQL_C_SS_VECTOR` provides a typed, compact input binding. `SQL_C_BINARY` is equivalent and portable. Use `SQL_C_SS_VECTOR` when you want the driver to treat the payload as a native **vector** type.

  - When `vectorTypeSupport=off`, vector columns appear as **varchar(max)** containing JSON arrays.

Applications must also set the ODBC version to ODBC 3.8 before using vector-specific types:

```c
    SQLSetEnvAttr(
    hEnv,
    SQL_ATTR_ODBC_VERSION,
    (SQLPOINTER)SQL_OV_ODBC3_80,
    0);
```

## Supported binding formats

### Native vector binding

C type: `SQL_C_SS_VECTOR`

This format is recommended for performance-critical applications.

### Binary binding

C type: `SQL_C_BINARY`

Vectors are returned using the same layout as `SQL_C_SS_VECTOR`. Applications can use this format for low-level interoperability scenarios. The data pointer buffer can be contiguous or noncontiguous with structure memory address.

### ODBC API guidance

This section describes how ODBC APIs interact with SQL Server vector data, including buffer layout requirements, `NULL` handling, and supported data representations. All behaviors apply when `vectorTypeSupport=v1` is enabled and the environment is configured for ODBC 3.8.

## SQLBindCol

Use `SQLBindCol` to bind **vector** columns in a result set to application buffers.

- Typical call:

```c
    SQLRETURN SQLBindCol(
      SQLHSTMT       StatementHandle,
      SQLUSMALLINT   ColumnNumber,
      SQLSMALLINT    TargetType,
      SQLPOINTER     TargetValuePtr,
      SQLLEN         BufferLength,
      SQLLEN *       StrLen_or_IndPtr);
```

- `TargetType`: use `SQL_C_SS_VECTOR` or `SQL_C_BINARY`.

- `TargetValuePtr`: pointer to a `SQL_SS_VECTOR_STRUCT` with a **float** array when `TargetType` is `SQL_C_SS_VECTOR`; otherwise, pointer to a buffer with size as the value in `StrLen_or_IndPtr`.

- `BufferLength`: `sizeof(SQL_SS_VECTOR_STRUCT)` + the number of bytes allocated for the column buffer (dimension * 4).

- `StrLen_or_IndPtr`: pointer that receives the byte length of the returned vector (`SQL_DESC_OCTET_LENGTH`). Its value is `sizeof(SQL_SS_VECTOR_STRUCT)` + the **float** array size (dimension * 4).

### Buffer layout expectations

Noncontiguous buffer (recommended)

- The application allocates one `SQL_SS_VECTOR_STRUCT` per row.
- The application allocates memory for `data.f32`.
- The driver populates vector metadata and writes element values into the provided **float** buffer.

```c
    SQLLEN numberOfRow = 1000; // set to SQL_ATTR_ROW_ARRAY_SIZE
    SQLULEN columnSize = x; // use SQLDescribeCol or SQLColAttributeW or sizeof(SQL_SS_VECTOR_STRUCT) + (dimension * 4)
    SQL_SS_VECTOR_STRUCT vecBuffer[numberOfRow]
    std::vector<SQLLEN> indicator(numberOfRow, 0);
    for (int i = 0; i < numberOfRow; i++)
    {
        vecBuffer[i].dimension = static_cast<SQLUSMALLINT>((columnSizes[col - 1] - sizeof(SQL_SS_VECTOR_STRUCT)) / 4);
        vecBuffer[i].type = SQL_VECTOR_TYPE_FLOAT32;
        vecBuffer[i].data.f32 = (float*)malloc(vecBuffer[i].dimension * sizeof(float));
        if (!vecBuffer[i].data.f32) {
            std::cerr << "Memory allocation failed for vector data." << std::endl;
            return SQL_ERROR;
        }
    }
    SQLBindCol(hStmt, col, SQL_C_SS_VECTOR, vecBuffer, columnSize, indicators.data());
```

Contiguous buffer

- The application allocates a single buffer and ODBC fills it accordingly.
- The **float** array must begin immediately after the struct.
- The driver sets `data.f32` to point to this contiguous region.

```c
    SQLLEN numberOfRow = 1000; // set to SQL_ATTR_ROW_ARRAY_SIZE
    SQLULEN columnSize = x; // use SQLDescribeCol or SQLColAttributeW or sizeof(SQL_SS_VECTOR_STRUCT) + (dimension * 4)
    std::vector<BYTE> vecBuffer;
    std::vector<SQLLEN> indicator(numberOfRow, 0);
    vecBuffer.resize(numberOfRow * columnSize);
    SQLBindCol(hStmt, col, SQL_C_BINARY, vecBuffer.data(), columnSize, indicators.data());
```

### NULL handling for SQLBindCol

When the column value is `NULL`, the driver sets `*StrLen_or_IndPtr` to `SQL_NULL_DATA` and doesn't populate the buffer. Always check the indicator before you access vector contents.

> [!NOTE]  
> Reading and row-fetch behavior for **vector** columns is described in detail under `SQLFetch` and `SQLFetchScroll` (see those API references for fetch semantics and array-fetch examples).

## SQLGetData

Use `SQLGetData` to retrieve vector data from unbound columns.

- Typical call:

```c
    SQLRETURN SQLGetData(
      SQLHSTMT       StatementHandle,
      SQLUSMALLINT   Col_or_Param_Num,
      SQLSMALLINT    TargetType,
      SQLPOINTER     TargetValuePtr,
      SQLLEN         BufferLength,
      SQLLEN *       StrLen_or_IndPtr);
```

- `TargetType`: use `SQL_C_SS_VECTOR` or `SQL_C_BINARY`.

- `TargetValuePtr`: pointer to a `SQL_SS_VECTOR_STRUCT` with a **float** array when `TargetType` is `SQL_C_SS_VECTOR`; otherwise, pointer to a buffer with size as the value in `StrLen_or_IndPtr`.

- `BufferLength`: `sizeof(SQL_SS_VECTOR_STRUCT)` + the number of bytes allocated for the column buffer (dimension * 4).

- `StrLen_or_IndPtr`: pointer that receives the byte length of the returned vector (`SQL_DESC_OCTET_LENGTH`). Its value is `sizeof(SQL_SS_VECTOR_STRUCT)` + the float array size (dimension * 4).

Chunked retrieval isn't supported. You must retrieve the entire vector in one call.

Example (native **vector**):

```c
    SQLLEN dataLen = 0;
    SQL_SS_VECTOR_STRUCT vec = {};
    vec.data.f32 = (float*)malloc(x * sizeof(float)); // x is the dimension
    if (!vec.data.f32) {
        std::cerr << "Memory allocation failed for vector data." << std::endl;
        return SQL_ERROR;
    }
    SQLGetData(hStmt, col, SQL_C_SS_VECTOR, &vec, sizeof(vec) + sizeof(float) * vec.dimension, &dataLen);
```

### NULL handling for SQLGetData

When the column value is `NULL`, the driver sets `*StrLen_or_IndPtr` to `SQL_NULL_DATA` and doesn't populate the buffer. Always check the indicator before you access vector contents.

> [!NOTE]  
> Reading and row-fetch behavior for **vector** columns is described in detail under `SQLFetch` and `SQLFetchScroll` (see those API references for fetch semantics and array-fetch examples).

## SQLFetch

Use `SQLFetch` to fetch the next row and properly materialize any bound `SqlVector` columns.

- Typical call:

```c
    SQLRETURN SQLFetch(
     SQLHSTMT     StatementHandle);
```

Call `SQLFetch` in the standard loop with `SqlVector` columns already bound through `SQLBindCol`. Or, use `SQLGetData` to retrieve data to the application buffer.

Example (native **vector**):

```c
    while ((ret = SQLFetch(hStmt)) == SQL_SUCCESS || ret == SQL_SUCCESS_WITH_INFO) {
        // if SQLBindCol was used directly jump to read buffer
        // else use SQLGetData and than read buffer.
        std::cout << "  data: [";
        if (dataLen != SQL_NULL_DATA)
        {
            for (SQLUSMALLINT d = 0; d < vec.dimension; ++d) {
                std::cout << vec.data.f32[d];
                if (d + 1 < vec.dimension) std::cout << ", ";
            }
        }
        std::cout << "]" << std::endl;
    }
```

## SQLFetchScroll

Use `SQLFetchScroll` to fetch rowsets according to a specified orientation (next, prior, absolute, relative, bookmark). The driver correctly copies vector data to application buffers.

- Typical call:

```c
    SQLRETURN SQLFetchScroll(
      SQLHSTMT      StatementHandle,
      SQLSMALLINT   FetchOrientation,
      SQLLEN        FetchOffset);
```

Example (native **vector**):

```c
    // Fetch rows in batches
    while ((ret = SQLFetchScroll(hStmt, SQL_FETCH_NEXT, 0)) != SQL_NO_DATA) {
        for (SQLULEN i = 0; i < numRowsFetched; i++) {
            // In case of Contiguous Buffer
                SQL_SS_VECTOR_STRUCT* vecptr = reinterpret_cast<SQL_SS_VECTOR_STRUCT*>(
                vecBuffer.data() + (i * columnSizes[col - 1]));
                float* floats = reinterpret_cast<float*>(
                reinterpret_cast<char*>(vecptr) + sizeof(SQL_SS_VECTOR_STRUCT)
            );
            // in Case of non-contiguous Buffer
            SQL_SS_VECTOR_STRUCT* vecptr = &vecBuffer[i];
            float* floats = vecptr->data.f32;

            if (indicators[i] != SQL_NULL_DATA)
            {
                for (SQLUSMALLINT d = 0; d < vecptr->dimension; ++d) {
                    std::cout << floats[d];
                    if (d + 1 < vecptr->dimension) std::cout << ", ";
                }
            }
            std::cout << "]" << std::endl;
        }
    }
```

## SQLBindParameter

Use `SQLBindParameter` to send vector values to SQL Server.

- Typical call:

```c
    SQLRETURN SQLBindParameter(
        SQLHSTMT        StatementHandle,
        SQLUSMALLINT    ParameterNumber,
        SQLSMALLINT     InputOutputType,
        SQLSMALLINT     ValueType,
        SQLSMALLINT     ParameterType,
        SQLULEN         ColumnSize,
        SQLSMALLINT     DecimalDigits,
        SQLPOINTER      ParameterValuePtr,
        SQLLEN          BufferLength,
        SQLLEN *        StrLen_or_IndPtr);
```

- `ParameterValuePtr`: points to a populated `SQL_SS_VECTOR_STRUCT`
- `ColumnSize`: ignored for input parameters. For output parameters and data-at-execution scenarios, specify the total vector size: `sizeof(SQL_SS_VECTOR_STRUCT)` + (**float** array size)
- `DecimalDigits`: ignored for input parameters. For output parameters and data-at-execution scenarios, specify the vector base type
- `BufferLength`: ≥ `sizeof(SQL_SS_VECTOR_STRUCT)` + (dimension * `sizeof(float)`)
- `*StrLen_or_IndPtr`: must contain the same total size

Example (native **vector**):

```c
    float values[3] = {1.0f, 2.0f, 3.0f};
    SQL_SS_VECTOR_STRUCT vec;
    SQLLEN cb;

    vec.dimension = 3;
    vec.type = 0; /* float32 */
    vec.data.f32 = values;

    cb = sizeof(vec) + sizeof(values);

    SQLBindParameter(
        hStmt, 1, SQL_PARAM_INPUT, SQL_C_SS_VECTOR, SQL_SS_VECTOR, 0, 0, &vec, cb, &cb);
```

### NULL handling for SQLBindParameter

Applications can indicate a `NULL` vector using either supported approach:

- Set `*StrLen_or_IndPtr` to `SQL_NULL_DATA` and pass `NULL` as `ParameterValuePtr`
- Provide a `SQL_SS_VECTOR_STRUCT` with:

  dimension set
  type = `float32`
  `data.f32` = `NULL`

## SQLPutData

The driver supports `SQLPutData` for vector parameters with these constraints:

- You must provide the entire vector in the first call.
- The driver doesn't support chunked or incremental vector transmission.
- `NULL` handling follows the same rules as `SQLBindParameter`.

- Typical call:

```c
    SQLRETURN SQLPutData(
        SQLHSTMT     StatementHandle,
        SQLPOINTER   DataPtr,
        SQLLEN       StrLen_or_Ind);
```

- `DataPtr`: points to a populated `SQL_SS_VECTOR_STRUCT`
- `StrLen_or_Ind`: `sizeof(SQL_SS_VECTOR_STRUCT)` + (dimension * `sizeof(float)`)

Example (native **vector**):

```c
    std::vector<float> floatArray = { 1.0f, 2.0f, 3.0f };

    SQL_SS_VECTOR_STRUCT vectorValue = {0};
    vectorValue.type = SQL_VECTOR_TYPE_FLOAT32;
    vectorValue.dimension = (SQLUSMALLINT)floatArray.size();
    vectorValue.data.f32 = floatArray.data();

    // Tell ODBC this parameter will be supplied at execution time
    SQLLEN cbVectorLen = SQL_LEN_DATA_AT_EXEC(
        (SQLLEN)(sizeof(SQL_SS_VECTOR_STRUCT) + floatArray.size() * sizeof(float))
    );

    // Optional token to identify which parameter needs data
    SQLPOINTER token = (SQLPOINTER)1;

    // Bind as DATA_AT_EXEC: BufferLength is 0, value pointer can be a token
    SQLRETURN rc = SQLBindParameter(hStmt, 1, SQL_PARAM_INPUT, SQL_C_SS_VECTOR, SQL_SS_VECTOR, 0, 0, token, 0, &cbVectorLen);

    if (rc != SQL_SUCCESS && rc != SQL_SUCCESS_WITH_INFO) {
        // handle error
    }

    rc = SQLExecute(hStmt);
    if (rc != SQL_SUCCESS && rc != SQL_SUCCESS_WITH_INFO && rc != SQL_NEED_DATA) {
        // handle error
    }

    SQLPOINTER pParamToken = NULL;
    rc = SQLParamData(hStmt, &pParamToken);

    if (rc == SQL_NEED_DATA) {
        // Provide entire vector in first SQLPutData call (no chunking)
        rc = SQLPutData(
            hStmt,
            (SQLPOINTER)&vectorValue,
            (SQLLEN)(sizeof(SQL_SS_VECTOR_STRUCT) + floatArray.size() * sizeof(float))
        );

        if (rc != SQL_SUCCESS && rc != SQL_SUCCESS_WITH_INFO) {
            // handle error
        }

        // Finalize parameter transfer
        rc = SQLParamData(hStmt, &pParamToken);
    }
```

## Descriptor metadata

When you enable vector support, the Microsoft ODBC Driver for SQL Server exposes vector metadata through standard ODBC descriptor APIs. Applications can use descriptor information to discover vector schema details, compute buffer sizes, and correctly configure bindings for parameters and result sets.

### Descriptor field values for vector

The following table summarizes descriptor field values for SQL Server **vector** columns and parameters.

| Descriptor Field | Value | Description |
| --- | --- | --- |
| `SQL_DESC_TYPE` | `SQL_SS_VECTOR` (`-156`) | Base SQL data type identifier |
| `SQL_DESC_CONCISE_TYPE` | `SQL_SS_VECTOR` | Concise SQL data type |
| `SQL_DESC_TYPE_NAME` | `vector` | SQL type name |
| `SQL_DESC_LOCAL_TYPE_NAME` | `vector` | Driver local type name |
| `SQL_DESC_LENGTH` | `sizeof(SQL_SS_VECTOR_STRUCT) + (dimension * sizeof(float))` | Logical size of the vector value |
| `SQL_DESC_OCTET_LENGTH` | Same as `SQL_DESC_LENGTH` | Physical size in bytes |
| `SQL_DESC_PRECISION` | Same as `SQL_DESC_LENGTH` | Used to report vector size |
| `SQL_DESC_SCALE` | `SQL_VECTOR_TYPE_FLOAT32` | Vector base element type |
| `SQL_DESC_DISPLAY_SIZE` | `dimension * VECTOR_FLOAT32_TO_CHAR_JSON_MAX_SIZE` | Maximum JSON display length |
| `SQL_DESC_FIXED_PREC_SCALE` | `SQL_FALSE` | Vector has no fixed precision/scale |
| `SQL_DESC_NULLABLE` | `SQL_NULLABLE` | Vector columns allow `NULL` values |
| `SQL_DESC_NUM_PREC_RADIX` | `0` | Non-numeric type |
| `SQL_DESC_SEARCHABLE` | `SQL_PRED_NONE` | Not usable in predicates |
| `SQL_DESC_UNSIGNED` | `SQL_TRUE` | Element type is unsigned |
| `SQL_DESC_AUTO_UNIQUE_VALUE` | `SQL_FALSE` | Not autounique |
| `SQL_DESC_CASE_SENSITIVE` | `SQL_FALSE` | Not case-sensitive |
| `SQL_DESC_UPDATABLE` | `SQL_ATTR_READWRITE_UNKNOWN` | Updatability unknown |

---

### SQLDescribeCol

When you call `SQLDescribeCol` for a **vector** column:

- `DataType` is `SQL_SS_VECTOR`
- `ColumnSize` matches `SQL_DESC_PRECISION`
- `DecimalDigits` is `0` (base type indicator, not numeric scale)
- `Nullable` is `SQL_NULLABLE`

The reported column size represents the native vector payload size: `sizeof(SQL_SS_VECTOR_STRUCT)` + (dimension * `sizeof(float)`)

### SQLDescribeParam

When you use `SQLDescribeParam` for a **vector** parameter:

- `DataType` is `SQL_SS_VECTOR`
- `ColumnSize` equals the native vector payload size
- `DecimalDigits` is `0` (base type indicator, not numeric scale)
- `Nullable` is `SQL_NULLABLE`

This information allows applications to allocate parameter buffers correctly before binding.

### SQLColAttribute

- Use `SQLColAttribute(hstmt, ColumnNumber, SQL_DESC_OCTET_LENGTH, ...)` to get the exact byte length of the vector payload. `SQL_DESC_LENGTH` and `SQL_DESC_PRECISION` might carry driver-specific values. For byte count, prefer `SQL_DESC_OCTET_LENGTH`.

  - `NULL` handling: When a column is `NULL`, `SQLColAttribute` (or the `StrLen_or_IndPtr` used with `SQLBindCol`) returns `SQL_NULL_DATA`. Check for `SQL_NULL_DATA` before using returned lengths or buffers.

## Bulk copy (BCP)

You can bulk import and export vector columns through BCP files and the `bcp_bind` API, just like other data types. Currently, vector import and export supports only native format (`SQLVECTOR`) or **varbinary** (`SQLBINARY`), but not character format. Conversion between the **vector** type and character type isn't supported.

For more information about the type token, default prefix length, and default field length for **vector**, see [File Storage Type](../../relational-databases/import-export/specify-file-storage-type-by-using-bcp-sql-server.md), [Prefix Length](../../relational-databases/import-export/specify-prefix-length-in-data-files-by-using-bcp-sql-server.md), and [Field Length](../../relational-databases/import-export/specify-field-length-by-using-bcp-sql-server.md).

### bcp_gettypename

When you use `bcp_gettypename` to get the SQL type name of **vector**, it returns the BCP type token (`SQLVECTOR`) and `"vector"`.

### bcp_bind

Use `bcp_bind` to bulk insert program variables into a **vector** column.

Typical call:

```c
RETCODE bcp_bind (
        HDBC hdbc,
        LPCBYTE pData,
        INT cbIndicator,
        DBINT cbData,
        LPCBYTE pTerm,
        INT cbTerm,
        INT eDataType,
        INT idxServerCol);
```

- `pData`: if `cbIndicator` is zero, contains a pointer to `SQL_SS_VECTOR_STRUCT` data, with **float** array data inside it (`vectorStruct.data.f32` field). If `cbIndicator` is nonzero, the indicator appears in memory directly before the data. So `pData` points to a buffer that first has `cbIndicator` bytes of length indicator, followed by the vector struct.
- `cbData`: if provided, must have value exactly equal to - `sizeof(SQL_SS_VECTOR_STRUCT)` + (`sizeof(float32)` * dimension). If not, an error occurs.
- `eDataType`: `SQLVECTOR` or `SQLBINARY`

When you import data to a vector column through `bcp_bind`, set `eDataType` to `SQLVECTOR` or `SQLBINARY`. In both cases, you must provide data in the form of `SQL_SS_VECTOR_STRUCT`.

## Troubleshooting and tips

- If `SQLGetTypeInfo` doesn't list `VECTOR`, fall back to storing vectors as **varchar**.
