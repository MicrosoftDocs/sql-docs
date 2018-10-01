---
title: "Procedure Calls | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "escape sequences [ODBC], procedure calls"
  - "procedure calls [ODBC]"
ms.assetid: 145130cc-40e7-4722-8417-dff131084752
author: MightyPen
ms.author: genemi
manager: craigg
---
# Procedure Calls
A *procedure* is an executable object stored on the data source. Generally, it is one or more SQL statements that have been precompiled. The escape sequence for calling a procedure is  
  
 **{**[**?=**]**call** *procedure-name*[**(**[*parameter*][**,**[*parameter*]]...**)**]**}**  
  
 where *procedure-name* specifies the name of a procedure and *parameter* specifies a procedure parameter.  
  
 For more information about the procedure call escape sequence, see [Procedure Call Escape Sequence](../../../odbc/reference/appendixes/procedure-call-escape-sequence.md) in Appendix C: SQL Grammar.  
  
 A procedure can have zero or more parameters. It can also return a value, as indicated by the optional parameter marker **?=** at the start of the syntax. If *parameter* is an input or an input/output parameter, it can be a literal or a parameter marker. However, interoperable applications should always use parameter markers because some data sources do not accept literal parameter values. If *parameter* is an output parameter, it must be a parameter marker. Parameter markers must be bound with **SQLBindParameter** before the procedure call statement is executed.  
  
 Input and input/output parameters can be omitted from procedure calls. If a procedure is called with parentheses but without any parameters, such as {call *procedure-name*()}, the driver instructs the data source to use the default value for the first parameter. If the procedure does not have any parameters, this might cause the procedure to fail. If a procedure is called without parentheses, such as {call *procedure-name*}, the driver does not send any parameter values.  
  
 Literals can be specified for input and input/output parameters in procedure calls. For example, suppose the procedure **InsertOrder** has five input parameters. The following call to **InsertOrder** omits the first parameter, provides a literal for the second parameter, and uses a parameter marker for the third, fourth, and fifth parameters:  
  
```  
{call InsertOrder(, 10, ?, ?, ?)}   // Not interoperable!  
```  
  
 Notice that if a parameter is omitted, the comma delimiting it from other parameters must still appear. If an input or input/output parameter is omitted, the procedure uses the default value of the parameter. Another way to specify the default value of an input or input/output parameter is to set the value of the length/indicator buffer bound to the parameter to SQL_DEFAULT_PARAM.  
  
 If an input/output parameter is omitted or if a literal is supplied for the parameter, the driver discards the output value. Similarly, if the parameter marker for the return value of a procedure is omitted, the driver discards the return value. Finally, if an application specifies a return value parameter for a procedure that does not return a value, the driver sets the value of the length/indicator buffer bound to the parameter to SQL_NULL_DATA.  
  
 Suppose the procedure PARTS_IN_ORDERS creates a result set that contains a list of orders that contain a particular part number. The following code calls this procedure for part number 544:  
  
```  
SQLUINTEGER   PartID;  
SQLINTEGER    PartIDInd = 0;  
  
// Bind the parameter.  
SQLBindParameter(hstmt, 1, SQL_PARAM_INPUT, SQL_C_SLONG, SQL_INTEGER, 0, 0,  
                  &PartID, 0, PartIDInd);  
  
// Place the department number in PartID.  
PartID = 544;  
  
// Execute the statement.  
SQLExecDirect(hstmt, "{call PARTS_IN_ORDERS(?)}", SQL_NTS);  
```  
  
 To determine whether a data source supports procedures, an application calls **SQLGetInfo** with the SQL_PROCEDURES option.  
  
 For more information about procedures, see [Procedures](../../../odbc/reference/develop-app/procedures-odbc.md).
