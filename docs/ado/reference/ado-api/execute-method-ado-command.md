---
title: "Execute Method (ADO Command) | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.topic: conceptual
apitype: "COM"
f1_keywords: 
  - "Command15::Execute"
  - "Command15::raw_Execute"
helpviewer_keywords: 
  - "Execute method [ADO]"
ms.assetid: f84a5ff3-0528-4ad7-9bea-9a15103378dd
author: MightyPen
ms.author: genemi
manager: craigg
---
# Execute Method (ADO Command)
Executes the query, SQL statement, or stored procedure specified in the [CommandText](../../../ado/reference/ado-api/commandtext-property-ado.md) or [CommandStream](../../../ado/reference/ado-api/commandstream-property-ado.md) property of the [Command object](../../../ado/reference/ado-api/command-object-ado.md).  
  
## Syntax  
  
```  
  
Set recordset = command.Execute( RecordsAffected, Parameters, Options )  
```  
  
## Return Value  
 Returns a [Recordset](../../../ado/reference/ado-api/recordset-object-ado.md) object reference, a stream, or **Nothing**.  
  
#### Parameters  
 *RecordsAffected*  
 Optional. A **Long** variable to which the provider returns the number of records that the operation affected. The *RecordsAffected* parameter applies only for action queries or stored procedures. *RecordsAffected* does not return the number of records returned by a result-returning query or stored procedure. To obtain this information, use the [RecordCount](../../../ado/reference/ado-api/recordcount-property-ado.md) property. The **Execute** method will not return the correct information when used with **adAsyncExecute**, simply because when a command is executed asynchronously, the number of records affected may not yet be known at the time the method returns.  
  
 *Parameters*  
 Optional. A **Variant** array of parameter values used in conjunction with the input string or stream specified in **CommandText** or **CommandStream**. (Output parameters will not return correct values when passed in this argument.)  
  
 *Options*  
 Optional. A **Long** value that indicates how the provider should evaluate the [CommandText](../../../ado/reference/ado-api/commandtext-property-ado.md) or the [CommandStream](../../../ado/reference/ado-api/commandstream-property-ado.md) property of the [Command](../../../ado/reference/ado-api/command-object-ado.md) object. Can be a bitmask value made using [CommandTypeEnum](../../../ado/reference/ado-api/commandtypeenum.md) and/or [ExecuteOptionEnum](../../../ado/reference/ado-api/executeoptionenum.md) values. For example, you could use **adCmdText** and **adExecuteNoRecords** in combination if you want to have ADO evaluate the value of the **CommandText** property as text, and indicate that the command should discard and not return any records that might be generated when the command text executes.  
  
> [!NOTE]
>  Use the **ExecuteOptionEnum** value **adExecuteNoRecords** to improve performance by minimizing internal processing. If **adExecuteStream** was specified, the options **adAsyncFetch** and **adAsynchFetchNonBlocking** are ignored. Do not use the **CommandTypeEnum** values of **adCmdFile** or **adCmdTableDirect** with **Execute**. These values can only be used as options with the [Open](../../../ado/reference/ado-api/open-method-ado-recordset.md) and [Requery](../../../ado/reference/ado-api/requery-method.md) methods of a **Recordset**.  
  
## Remarks  
 Using the **Execute** method on a **Command** object executes the query specified in the **CommandText** property or **CommandStream** property of the object.  
  
 Results are returned in a **Recordset** (by default) or as a stream of binary information. To obtain a binary stream, specify **adExecuteStream** in *Options*, then supply a stream by setting **Command.Properties("Output Stream")**. An ADO **Stream** object can be specified to receive the results, or another stream object such as the IIS Response object can be specified. If no stream was specified before calling **Execute** with **adExecuteStream**, an error occurs. The position of the stream on return from **Execute** is provider specific.  
  
 If the command is not intended to return results (for example, an SQL UPDATE query) the provider returns **Nothing** as long as the option **adExecuteNoRecords** is specified; otherwise Execute returns a closed **Recordset**. Some application languages allow you to ignore this return value if no **Recordset** is desired.  
  
 **Execute** raises an error if the user specifies a value for **CommandStream** when the **CommandType** is **adCmdStoredProc**, **adCmdTable**, or **adCmdTableDirect**.  
  
 If the query has parameters, the current values for the **Command** object's parameters are used unless you override these with parameter values passed with the **Execute** call. You can override a subset of the parameters by omitting new values for some of the parameters when calling the **Execute** method. The order in which you specify the parameters is the same order in which the method passes them. For example, if there were four (or more) parameters and you wanted to pass new values for only the first and fourth parameters, you would pass `Array(var1,,,var4)` as the *Parameters* argument.  
  
> [!NOTE]
>  Output parameters will not return correct values when passed in the *Parameters* argument.  
  
 An [ExecuteComplete](../../../ado/reference/ado-api/executecomplete-event-ado.md) event will be issued when this operation concludes.  
  
> [!NOTE]
>  When issuing commands containing URLs, those using the http scheme will automatically invoke the [Microsoft OLE DB Provider for Internet Publishing](../../../ado/guide/appendixes/microsoft-ole-db-provider-for-internet-publishing.md). For more information, see [Absolute and Relative URLs](../../../ado/guide/data/absolute-and-relative-urls.md).  
  
## Applies To  
 [Command Object (ADO)](../../../ado/reference/ado-api/command-object-ado.md)  
  
## See Also  
 [Execute, Requery, and Clear Methods Example (VB)](../../../ado/reference/ado-api/execute-requery-and-clear-methods-example-vb.md)   
 [Execute, Requery, and Clear Methods Example (VBScript)](../../../ado/reference/ado-api/execute-requery-and-clear-methods-example-vbscript.md)   
 [Execute, Requery, and Clear Methods Example (VC++)](../../../ado/reference/ado-api/execute-requery-and-clear-methods-example-vc.md)   
 [CommandStream Property (ADO)](../../../ado/reference/ado-api/commandstream-property-ado.md)   
 [CommandText Property (ADO)](../../../ado/reference/ado-api/commandtext-property-ado.md)   
 [CommandTypeEnum](../../../ado/reference/ado-api/commandtypeenum.md)   
 [Execute Method (ADO Connection)](../../../ado/reference/ado-api/execute-method-ado-connection.md)   
 [ExecuteComplete Event (ADO)](../../../ado/reference/ado-api/executecomplete-event-ado.md)
