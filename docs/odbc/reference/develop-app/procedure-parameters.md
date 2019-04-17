---
title: "Procedure Parameters | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "procedure parameters [ODBC]"
ms.assetid: 54fd857e-d2cb-467d-bb72-121e67a8e88d
author: MightyPen
ms.author: genemi
manager: craigg
---
# Procedure Parameters
Parameters in procedure calls can be input, input/output, or output parameters. This is different from parameters in all other SQL statements, which are always input parameters.  
  
 Input parameters are used to send values to the procedure. For example, suppose the Parts table has PartID, Description, and Price columns. The InsertPart procedure might have an input parameter for each column in the table. For example:  
  
```  
{call InsertPart(?, ?, ?)}  
```  
  
 A driver should not modify the contents of an input buffer until **SQLExecDirect** or **SQLExecute** returns SQL_SUCCESS, SQL_SUCCESS_WITH_INFO, SQL_ERROR, SQL_INVALID_HANDLE, or SQL_NO_DATA. The contents of the input buffer should not be modified while **SQLExecDirect** or **SQLExecute** returns SQL_NEED_DATA or SQL_STILL_EXECUTING.  
  
 Input/output parameters are used both to send values to procedures and retrieve values from procedures. Using the same parameter as both an input and an output parameter tends to be confusing and should be avoided. For example, suppose a procedure accepts an order ID and returns the ID of the customer. This can be defined with a single input/output parameter:  
  
```  
{call GetCustID(?)}  
```  
  
 It might be better to use two parameters: an input parameter for the order ID and an output or input/output parameter for the customer ID:  
  
```  
{call GetCustID(?, ?)}  
```  
  
 Output parameters are used to retrieve the procedure return value and to retrieve values from procedure arguments; procedures that return values are sometimes known as *functions*. For example, suppose the **GetCustID** procedure just mentioned returns a value that indicates whether it was able to find the order. In the following call, the first parameter is an output parameter used to retrieve the procedure return value, the second parameter is an input parameter used to specify the order ID, and the third parameter is an output parameter used to retrieve the customer ID:  
  
```  
{? = call GetCustID(?, ?)}  
```  
  
 Drivers handle values for input and input/output parameters in procedures no differently than input parameters in other SQL statements. When the statement is executed, they retrieve the values of the variables bound to these parameters and send them to the data source.  
  
 After the statement has been executed, drivers store the returned values of input/output and output parameters in the variables bound to those parameters. These returned values are not guaranteed to be set until after all results returned by the procedure have been fetched and **SQLMoreResults** has returned SQL_NO_DATA. If executing the statement results in an error, the contents of the input/output parameter buffer or output parameter buffer are undefined.  
  
 An application calls **SQLProcedure** to determine whether a procedure has a return value. It calls **SQLProcedureColumns** to determine the type (return value, input, input/output, or output) of each procedure parameter.
