---
title: "List of bugs fixed | Microsoft Docs"
ms.custom: ""
ms.date: "07/26/2018"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.suite: "sql"
ms.technology: connectivity
ms.tgt_pltfrm: ""
ms.topic: conceptual
helpviewer_keywords: 
  - "driver"
ms.assetid: f78b81ed-5214-43ec-a600-9bfe51c5745a
caps.latest.revision: 1
author: "v-makouz"
ms.author: v-makouz
manager: "kenvh"
---
# Data Classification
[!INCLUDE[Driver_ODBC_Download](../../includes/driver_odbc_download.md)]

## Overview
For the purpose of managing sensitive data SQL Server and Azure SQL Server introduced the ability to provide database columns with sensitivity metadata that allows the client application to handle different types of sensitive data (such as health, financial, etc.) in accordance with data protection policies.

See [SQL Data Discovery and Classification](https://docs.microsoft.com/en-us/sql/relational-databases/security/sql-data-discovery-and-classification?view=sql-server-2017) for more information on how to assign classification to columns.

Microsoft ODBC Driver 17.2 allows the retrieval of this metadata via SQLGetDescField using SQL_CA_SS_DATA_CLASSIFICATION field identifier.

## Format
SQLGetDescField number starting from 1 will return:
-	Version of Data Classification information
-	Number of SENSITIVITYPROPERTY elements for column
-	Array of pairs, where each pair consists of two USHORT offsets (from the beginning of the data) to the 
    - name of the sensitivity label in names array or USHORT_MAX if there is no sensitivity label
    - name of the information type in names array or USHORT_MAX if there is no information type

For the hybrid option requirements SQLBatch processing changes and data organization is the same as for record field option.
In case if the Data Classification feature is not supported by SQL Server, specific error code should be returned.


Returned data should have the following format:

|Field Name|Type (number of elements)|
|-------------------|--------------------| 
|SensitivityLabelsCount|USHORT|
|SensitivityLabels|SENSITIVITYLABEL x SensitivityLabelsCount|
|InformationTypesCount|USHORT|
|InformationTypes|SENSITIVITYINFORMATIONTYPE x InformationTypesCount|
|NumResultColumns|USHORT|
|ColumnData|COLUMNSENSITIVITYMETADATA x NumResultColumns|


<br><br>
SENSITIVITYLABEL is defined as:

|Field Name|Type|
|-------------------|--------------------|  
|Name|B_VARCHAR|  
|Id|B_VARCHAR|

Note: B_VARCHAR is a variable-length character stream which is defined by a length field followed by the data in Unicode characters. If the string is empty it will have only one byte which is 0.

This type is defined in TDS specification, see [Tabular Data Stream Protocol](https://msdn.microsoft.com/en-us/library/dd304523.aspx) for more information.

<br><br>
SENSITIVITYINFORMATIONTYPE is defined as: 

|Field Name|Type|
|-------------------|--------------------|  
|Name|B_VARCHAR|  
|Id|B_VARCHAR|


<br><br>
COLUMNSENSITIVITYMETADATA is defined as: 

|Field Name|Type (number of elements)|
|-------------------|--------------------|
|NumSensitivityProperties|USHORT| 
|SourceData|SENSITIVITYPROPERTY x NumSensitivityProperties|


<br><br>
SENSITIVITYPROPERTY is defined as: 

|Field Name|Type|
|-------------------|--------------------|
|SensitivityLabelIndex|USHORT, index into SensitivityLabels<p>USHORT_MAX if there is no sensitivity label|
|InformationTypeIndex|USHORT, index into InformationTypes<p>USHORT_MAX if there is no info type|


## Code Sample
The following is a test application that demonstrates how to read Data Classification metadata. On Windows it can be compiled using `cl /MD dataclassification.c /I (directory of msodbcsql.h) /link odbc32.lib` and run with a connection string, and a SQL query (that returns classified columns) as parameters.

```
#ifdef _WIN32
#include <windows.h>
#endif
#include <sql.h>
#include <sqlext.h>
#include <msodbcsql.h>
#include <stdio.h>
SQLHANDLE env, dbc, stmt;
void checkRC_exit(SQLRETURN rc, SQLHANDLE hand, SQLSMALLINT htype, int retcode, char *action)
{
    if ((rc == SQL_ERROR || rc == SQL_SUCCESS_WITH_INFO) && hand)
    {
        char msg[1024], state[6];
        int i = 0;
        SQLRETURN rc2;
        SQLINTEGER err;
        SQLSMALLINT lenout;
        while ((rc2 = SQLGetDiagRec(htype, hand, ++i, state, &err, msg, sizeof(msg), &lenout)) == SQL_SUCCESS ||
            rc2 == SQL_SUCCESS_WITH_INFO)
            printf("%d (%d)[%s]%s\n", i, err, state, msg);
    }
    if (rc == SQL_ERROR && retcode)
    {
        printf("Error occurred%s%s\n", action ? " upon " : "", action ? action : "");
        exit(retcode);
    }
}
void printLabelInfo(char *type, char **pptr)
{
    char *ptr = *pptr;
    unsigned short nlabels;
    printf("----- %s(%u) -----\n", type, nlabels = *(unsigned short*)ptr);
    ptr += sizeof(unsigned short);
    while (nlabels--)
    {
        int namelen, idlen;
        char *nameptr, *idptr;
        namelen = *ptr++;
        nameptr = ptr;
        ptr += namelen * 2;
        idlen = *ptr++;
        idptr = ptr;
        ptr += idlen * 2;
        wprintf(L"Name: \"%.*s\" Id: \"%.*s\"\n", namelen, nameptr, idlen, idptr);
    }
    *pptr = ptr;
}
int main(int argc, char **argv)
{
    unsigned char *dcbuf;
    unsigned int dclen = 0;
    SQLRETURN rc;
    SQLHANDLE ird;
    if (argc < 3)
    {
        fprintf(stderr, "usage: dataclassification connstr query\n");
        return 1;
    }
    checkRC_exit(SQLAllocHandle(SQL_HANDLE_ENV, 0, &env), 0, 0,
        2, "allocate environment");
    checkRC_exit(SQLSetEnvAttr(env, SQL_ATTR_ODBC_VERSION, (SQLPOINTER)SQL_OV_ODBC3, 0), env, SQL_HANDLE_ENV,
        3, "set ODBC version");
    checkRC_exit(SQLAllocHandle(SQL_HANDLE_DBC, env, &dbc), env, SQL_HANDLE_ENV,
        4, "allocate connection");
    checkRC_exit(SQLDriverConnect(dbc, 0, argv[1], SQL_NTS, 0, 0, 0, SQL_DRIVER_NOPROMPT), dbc, SQL_HANDLE_DBC,
        5, "connect to server");
    checkRC_exit(SQLAllocHandle(SQL_HANDLE_STMT, dbc, &stmt), dbc, SQL_HANDLE_DBC,
        6, "allocate statement");
    checkRC_exit(SQLExecDirect(stmt, argv[2], SQL_NTS), stmt, SQL_HANDLE_STMT,
        7, "execute query");
    checkRC_exit(SQLGetStmtAttr(stmt, SQL_ATTR_IMP_ROW_DESC, (SQLPOINTER)&ird, SQL_IS_POINTER, 0), stmt, SQL_HANDLE_STMT,
        8, "get IRD handle");
    rc = SQLGetDescFieldW(ird, 0, SQL_CA_SS_DATA_CLASSIFICATION, dcbuf, 0, &dclen);
    if (rc != SQL_SUCCESS && rc != SQL_SUCCESS_WITH_INFO)
    {
        checkRC_exit(rc, ird, SQL_HANDLE_DESC, 0, 0);
        printf("Error reading SQL_CA_SS_DATA_CLASSIFICATION IRD field. Ensure that the driver and\n"
            "server both support the Data Classification feature, and that the query returns columns\n"
            "with classification information.\n");
    }
    else
    {
        SQLINTEGER dclenout;
        unsigned char *dcptr;
        unsigned short ncols;
        printf("Data Classification information (%u bytes):\n", dclen);
        if (!(dcbuf = malloc(dclen)))
        {
            printf("Memory Allocation Error");
            return 9;
        }
        checkRC_exit(SQLGetDescFieldW(ird, 0, SQL_CA_SS_DATA_CLASSIFICATION, dcbuf, dclen, &dclenout),
            ird, SQL_HANDLE_DESC, 10, "reading SQL_CA_SS_DATA_CLASSIFICATION");
        dcptr = dcbuf;
        printLabelInfo("Labels", &dcptr);
        printLabelInfo("Information Types", &dcptr);
        printf("----- Column Sensitivities(%u) -----\n", ncols = *(unsigned short*)dcptr);
        dcptr += sizeof(unsigned short);
        while (ncols--)
        {
            unsigned short nprops = *(unsigned short*)dcptr;
            dcptr += sizeof(unsigned short);
            while (nprops--)
            {
                unsigned short labelidx, typeidx;
                labelidx = *(unsigned short*)dcptr; dcptr += sizeof(unsigned short);
                typeidx = *(unsigned short*)dcptr; dcptr += sizeof(unsigned short);
                printf(labelidx == 0xFFFF ? "(none) " : "%u ", labelidx);
                printf(typeidx == 0xFFFF ? "(none)\n" : "%u\n", typeidx);
            }
            printf("-----\n");
        }
        if (dcptr != dcbuf + dclen)
        {
            printf("Error: unexpected parse of DATACLASSIFICATION data\n");
            return 11;
        }
        free(dcbuf);
    }
    return 0;
}
```

