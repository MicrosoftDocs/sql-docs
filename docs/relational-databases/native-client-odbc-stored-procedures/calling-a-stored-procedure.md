---
title: "Calling a Stored Procedure | Microsoft Docs"
ms.custom: ""
ms.date: "03/16/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database, sql-data-warehouse, pdw"
ms.reviewer: ""
ms.technology: native-client
ms.topic: "reference"
helpviewer_keywords: 
  - "calling stored procedures"
  - "ODBC, stored procedures"
  - "stored procedures [ODBC], calling"
  - "SQL Server Native Client ODBC driver, stored procedures"
  - "ODBC CALL escape sequence"
  - "escape sequences [SQL Server]"
  - "CALL statement"
ms.assetid: d13737f4-f641-45bf-b56c-523e2ffc080f
author: MightyPen
ms.author: genemi
manager: craigg
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Calling a Stored Procedure
[!INCLUDE[appliesto-ss-asdb-asdw-pdw-md](../../includes/appliesto-ss-asdb-asdw-pdw-md.md)]
[!INCLUDE[SNAC_Deprecated](../../includes/snac-deprecated.md)]

  The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client ODBC driver supports both the ODBC CALL escape sequence and the [!INCLUDE[tsql](../../includes/tsql-md.md)][EXECUTE](../../t-sql/language-elements/execute-transact-sql.md) statement for executing stored procedures; the ODBC CALL escape sequence is the preferred method. Using ODBC syntax enables an application to retrieve the return codes of stored procedures and the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client ODBC driver is also optimized to use a protocol originally developed for sending remote procedure (RPC) calls between computers running [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. This RPC protocol increases performance by eliminating much of the parameter processing and statement parsing done on the server.  
  
> [!NOTE]  
>  When calling [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] stored procedures using named parameters with ODBC (for more information, see [Binding Parameters by Name (Named Parameters)](https://go.microsoft.com/fwlink/?LinkID=209721)), the parameter names must start with the '\@' character. This is a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] specific restriction. The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client ODBC driver enforces this restriction more strictly than the Microsoft Data Access Components (MDAC).  
  
 The ODBC CALL escape sequence for calling a procedure is:  
  
 {[**?=**]**call**_procedure_name_[([*parameter*][**,**[*parameter*]]...)]}  
  
 where *procedure_name* specifies the name of a procedure and *parameter* specifies a procedure parameter. Named parameters are only supported in statements using the ODBC CALL escape sequence.  
  
 A procedure can have zero or more parameters. It can also return a value (as indicated by the optional parameter marker ?= at the start of the syntax). If a parameter is an input or an input/output parameter, it can be a literal or a parameter marker. If the parameter is an output parameter, it must be a parameter marker because the output is unknown. Parameter markers must be bound with [SQLBindParameter](../../relational-databases/native-client-odbc-api/sqlbindparameter.md) before the procedure call statement is executed.  
  
 Input and input/output parameters can be omitted from procedure calls. If a procedure is called with parentheses but without any parameters, the driver instructs the data source to use the default value for the first parameter. For example:  
  
 {**call** _procedure_name_**( )**}  
  
 If the procedure does not have any parameters, the procedure can fail. If a procedure is called without parentheses, the driver does not send any parameter values. For example:  
  
 {**call** _procedure_name_}  
  
 Literals can be specified for input and input/output parameters in procedure calls. For example, the procedure InsertOrder has five input parameters. The following call to InsertOrder omits the first parameter, provides a literal for the second parameter, and uses a parameter marker for the third, fourth, and fifth parameters. (Parameters are numbered sequentially, beginning with a value of 1.)  
  
```  
{call InsertOrder(, 10, ?, ?, ?)}  
```  
  
 Note that if a parameter is omitted, the comma delimiting it from other parameters must still appear. If an input or input/output parameter is omitted, the procedure uses the default value of the parameter. Other ways to specify the default value of an input or input/output parameter are to set the value of the length/indicator buffer bound to the parameter to SQL_DEFAULT_PARAM, or to use the DEFAULT keyword.  
  
 If an input/output parameter is omitted, or if a literal is supplied for the parameter, the driver discards the output value. Similarly, if the parameter marker for the return value of a procedure is omitted, the driver discards the return value. Finally, if an application specifies a return value parameter for a procedure that does not return a value, the driver sets the value of the length/indicator buffer bound to the parameter to SQL_NULL_DATA.  
  
## Delimiters in CALL Statements  
 The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client ODBC driver by default also supports a compatibility option specific to the ODBC { CALL } escape sequence. The driver accepts CALL statements with only a single set of double quotation marks delimiting the entire stored procedure name:  
  
```  
{ CALL "master.dbo.sp_who" }  
```  
  
 By default the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client ODBC driver also accepts CALL statements that follow the ISO rules and enclose each identifier in double quotation marks:  
  
```  
{ CALL "master"."dbo"."sp_who" }  
```  
  
 When running with the default settings, however, the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client ODBC driver does not support using either form of quoted identifier with identifiers that contain characters not specified as legal in identifiers by the ISO standard. For example, the driver cannot access a stored procedure named **"My.Proc"** using a CALL statement with quoted identifiers:  
  
```  
{ CALL "MyDB"."MyOwner"."My.Proc" }  
```  
  
 This statement is interpreted by the driver as:  
  
```  
{ CALL MyDB.MyOwner.My.Proc }  
```  
  
 The server raises an error that a linked server named **MyDB** does not exist.  
  
 The issue does not exist when using bracketed identifiers, this statement is interpreted correctly:  
  
```  
{ CALL [MyDB].[MyOwner].[My.Table] }  
```  
  
## See Also  
 [Running Stored Procedures](../../relational-databases/native-client-odbc-stored-procedures/running-stored-procedures.md)  
  
  
