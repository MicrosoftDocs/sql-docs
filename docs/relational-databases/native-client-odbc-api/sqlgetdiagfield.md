---
description: "SQLGetDiagField"
title: "SQLGetDiagField | Microsoft Docs"
ms.custom: ""
ms.date: "03/16/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: native-client
ms.topic: "reference"
apitype: "DLLExport"
helpviewer_keywords: 
  - "SQLGetDiagField function"
ms.assetid: 395245ba-0372-43ec-b9a4-a29410d85a6d
author: markingmyname
ms.author: maghan
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# SQLGetDiagField
[!INCLUDE[SQL Server Azure SQL Database Synapse Analytics PDW ](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

  The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client ODBC driver specifies the following additional diagnostics fields for **SQLGetDiagField**. These fields support rich error reporting for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] applications and are available in all diagnostics records generated on connected ODBC connection handles and ODBC statement handles. The fields are defined in sqlncli.h.  
  
|Diagnostics record field|Description|  
|------------------------------|-----------------|  
|SQL_DIAG_SS_LINE|Reports the line number of a stored procedure generating an error. The value of SQL_DIAG_SS_LINE is meaningful only if SQL_DIAG_SS_PROCNAME returns a value. The value is returned as an unsigned, 16-bit integer.|  
|SQL_DIAG_SS_MSGSTATE|The state of an error message. For information about the error message state, see [RAISERROR](../../t-sql/language-elements/raiserror-transact-sql.md). The value is returned as a signed, 32-bit integer.|  
|SQL_DIAG_SS_PROCNAME|Name of the stored procedure generating an error, if appropriate. The value is returned as a character string. The length of the string (in characters) depends on the version of the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. It can be determined by calling [SQLGetInfo](../../relational-databases/native-client-odbc-api/sqlgetinfo.md) requesting the value for SQL_MAX_PROCEDURE_NAME_LEN.|  
|SQL_DIAG_SS_SEVERITY|The severity level of the associated error message. The value is returned as a signed, 32-bit integer.|  
|SQL_DIAG_SS_SRVNAME|The name of the server on which the error occurred. The value is returned as a character string. The length of the string (in characters) is defined by the SQL_MAX_SQLSERVERNAME macro in sqlncli.h.|  
  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]-specific diagnostic fields that contain character data, SQL_DIAG_SS_PROCNAME and SQL_DIAG_SS_SRVNAME, return that data to the client as null terminated, ANSI, or Unicode strings. If necessary, the count of characters should be adjusted by the character width. Alternately, a portable C data type such as TCHAR or SQLTCHAR can be used to ensure correct program variable length.  
  
 The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client ODBC driver reports the following additional dynamic function codes that identify the last attempted [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] statement. The dynamic function code is returned in the header (record 0) of the diagnostics record set and is therefore available on every execution (successful or not).  
  
|Dynamic function code|Source|  
|---------------------------|------------|  
|SQL_DIAG_DFC_SS_ALTER_DATABASE|ALTER DATABASE statement|  
|SQL_DIAG_DFC_SS_CHECKPOINT|CHECKPOINT statement|  
|SQL_DIAG_DFC_SS_CONDITION|Error arose in the WHERE or HAVING clauses of a statement.|  
|SQL_DIAG_DFC_SS_CREATE_DATABASE|CREATE DATABASE statement|  
|SQL_DIAG_DFC_SS_CREATE_DEFAULT|CREATE DEFAULT statement|  
|SQL_DIAG_DFC_SS_CREATE_PROCEDURE|CREATE PROCEDURE statement|  
|SQL_DIAG_DFC_SS_CREATE_RULE|CREATE RULE statement|  
|SQL_DIAG_DFC_SS_CREATE_TRIGGER|CREATE TRIGGER statement|  
|SQL_DIAG_DFC_SS_CURSOR_DECLARE|DECLARE CURSOR statement|  
|SQL_DIAG_DFC_SS_CURSOR_OPEN|OPEN statement|  
|SQL_DIAG_DFC_SS_CURSOR_FETCH|FETCH statement|  
|SQL_DIAG_DFC_SS_CURSOR_CLOSE|CLOSE statement|  
|SQL_DIAG_DFC_SS_DEALLOCATE_CURSOR|DEALLOCATE statement|  
|SQL_DIAG_DFC_SS_DBCC|DBCC statement|  
|SQL_DIAG_DFC_SS_DENY|DENY statement|  
|SQL_DIAG_DFC_SS_DROP_DATABASE|DROP DATABASE statement|  
|SQL_DIAG_DFC_SS_DROP_DEFAULT|DROP DEFAULT statement|  
|SQL_DIAG_DFC_SS_DROP_PROCEDURE|DROP PROCEDURE statement|  
|SQL_DIAG_DFC_SS_DROP_RULE|DROP RULE statement|  
|SQL_DIAG_DFC_SS_DROP_TRIGGER|DROP TRIGGER statement|  
|SQL_DIAG_DFC_SS_DUMP_DATABASE|BACKUP or DUMP DATABASE statement|  
|SQL_DIAG_DFC_SS_DUMP_TABLE|DUMP TABLE statement|  
|SQL_DIAG_DFC_SS_DUMP_TRANSACTION|BACKUP or DUMP TRANSACTION statement. Also returned for a CHECKPOINT statement if the **trunc. log on chkpt.** database option is on.|  
|SQL_DIAG_DFC_SS_GOTO|GOTO control-of-flow statement|  
|SQL_DIAG_DFC_SS_INSERT_BULK|INSERT BULK statement|  
|SQL_DIAG_DFC_SS_KILL|KILL statement|  
|SQL_DIAG_DFC_SS_LOAD_DATABASE|LOAD or RESTORE DATABASE statement|  
|SQL_DIAG_DFC_SS_LOAD_HEADERONLY|LOAD or RESTORE HEADERONLY statement|  
|SQL_DIAG_DFC_SS_LOAD_TABLE|LOAD TABLE statement|  
|SQL_DIAG_DFC_SS_LOAD_TRANSACTION|LOAD or RESTORE TRANSACTION statement|  
|SQL_DIAG_DFC_SS_PRINT|PRINT statement|  
|SQL_DIAG_DFC_SS_RAISERROR|RAISERROR statement|  
|SQL_DIAG_DFC_SS_READTEXT|READTEXT statement|  
|SQL_DIAG_DFC_SS_RECONFIGURE|RECONFIGURE statement|  
|SQL_DIAG_DFC_SS_RETURN|RETURN control-of-flow statement|  
|SQL_DIAG_DFC_SS_SELECT_INTO|SELECT INTO statement|  
|SQL_DIAG_DFC_SS_SET|SET statement (generic, all options)|  
|SQL_DIAG_DFC_SS_SET_IDENTITY_INSERT|SET IDENTITY_INSERT statement|  
|SQL_DIAG_DFC_SS_SET_ROW_COUNT|SET ROWCOUNT statement|  
|SQL_DIAG_DFC_SS_SET_STATISTICS|SET STATISTICS IO or SET STATISTICS TIME statements|  
|SQL_DIAG_DFC_SS_SET_TEXTSIZE|SET TEXTSIZE statement|  
|SQL_DIAG_DFC_SS_SETUSER|SETUSER statement|  
|SQL_DIAG_DFC_SS_SET_XCTLVL|SET TRANSACTION ISOLATION LEVEL statement|  
|SQL_DIAG_DFC_SS_SHUTDOWN|SHUTDOWN statement|  
|SQL_DIAG_DFC_SS_TRANS_BEGIN|BEGIN TRAN statement|  
|SQL_DIAG_DFC_SS_TRANS_COMMIT|COMMIT TRAN statement|  
|SQL_DIAG_DFC_SS_TRANS_PREPARE|Prepare to commit a distributed transaction|  
|SQL_DIAG_DFC_SS_TRANS_ROLLBACK|ROLLBACK TRAN statement|  
|SQL_DIAG_DFC_SS_TRANS_SAVE|SAVE TRAN statement|  
|SQL_DIAG_DFC_SS_TRUNCATE_TABLE|TRUNCATE TABLE statement|  
|SQL_DIAG_DFC_SS_UPDATE_STATISTICS|UPDATE STATISTICS statement|  
|SQL_DIAG_DFC_SS_UPDATETEXT|UPDATETEXT statement|  
|SQL_DIAG_DFC_SS_USE|USE statement|  
|SQL_DIAG_DFC_SS_WAITFOR|WAITFOR control-of-flow statement|  
|SQL_DIAG_DFC_SS_WRITETEXT|WRITETEXT statement|  
  
## SQLGetDiagField and Table-Valued Parameters  
 SQLGetDiagField can be used to retrieve two diagnostic fields: SQL_DIAG_SS_TABLE_COLUMN_NUMBER and SQL_DIAG_SS_TABLE_ROW_NUMBER. These fields help you determine which value caused the error or warning associated with the diagnostic record.  
  
 For more information about table-valued parameters, see [Table-Valued Parameters &#40;ODBC&#41;](../../relational-databases/native-client-odbc-table-valued-parameters/table-valued-parameters-odbc.md).  
  
## See Also  
 [SQLGetDiagField Function](../../odbc/reference/syntax/sqlgetdiagfield-function.md)   
 [ODBC API Implementation Details](../../relational-databases/native-client-odbc-api/odbc-api-implementation-details.md)  
  
