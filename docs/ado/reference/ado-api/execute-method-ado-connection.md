---
title: "Execute Method (ADO Connection) | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.topic: conceptual
apitype: "COM"
f1_keywords: 
  - "Connection15::Execute"
  - "Connection15::raw_Execute"
helpviewer_keywords: 
  - "Execute method [ADO]"
ms.assetid: 03c69320-96b2-4d85-8d49-a13b13e31578
author: MightyPen
ms.author: genemi
manager: craigg
---
# Execute Method (ADO Connection)
Executes the specified query, SQL statement, stored procedure, or provider-specific text.  
  
## Syntax  
  
```  
  
Set recordset = connection.Execute (CommandText, RecordsAffected, Options)  
Set recordset = connection.Execute (CommandText, RecordsAffected, Options)  
```  
  
## Return Value  
 Returns a [Recordset Object (ADO)](../../../ado/reference/ado-api/recordset-object-ado.md) object reference.  
  
#### Parameters  
 *CommandText*  
 A **String** value that contains the SQL statement, stored procedure, a URL, or provider-specific text to execute. **Optionally**, table names can be used but only if the provider is SQL aware. For example if a table name of "Customers" is used, ADO will automatically prepend the standard SQL Select syntax to form and pass "SELECT * FROM Customers" as a [!INCLUDE[tsql](../../../includes/tsql-md.md)] statement to the provider.  
  
 *RecordsAffected*  
 Optional. A **Long** variable to which the provider returns the number of records that the operation affected.  
  
 *Options*  
 Optional. A **Long** value that indicates how the provider should evaluate the CommandText argument. Can be a bitmask of one or more [CommandTypeEnum](../../../ado/reference/ado-api/commandtypeenum.md) or [ExecuteOptionEnum](../../../ado/reference/ado-api/executeoptionenum.md) values.  
  
 **Note** Use the **ExecuteOptionEnum** value **adExecuteNoRecords** to improve performance by minimizing internal processing and for applications that you are porting from Visual Basic 6.0.  
  
 Do not use **adExecuteStream** with the **Execute** method of a **Connection** object.  
  
 Do not use the CommandTypeEnum values of adCmdFile or adCmdTableDirect with Execute. These values can only be used as options with the [Open Method (ADO Recordset)](../../../ado/reference/ado-api/open-method-ado-recordset.md) and [Requery Method](../../../ado/reference/ado-api/requery-method.md) methods of a **Recordset**.  
  
## Remarks  
 Using the **Execute** method on a [Connection Object (ADO)](../../../ado/reference/ado-api/connection-object-ado.md) object executes whatever query you pass to the method in the CommandText argument on the specified connection. If the CommandText argument specifies a row-returning query, any results that the execution generates are stored in a new **Recordset** object. If the command is not intended to return results (for example, an SQL UPDATE query) the provider returns **Nothing** as long as the option **adExecuteNoRecords** is specified; otherwise Execute returns a closed **Recordset**.  
  
 The returned **Recordset** object is always a read-only, forward-only cursor. If you need a **Recordset** object with more functionality, first create a **Recordset** object with the desired property settings, then use the **Recordset** object's [Open Method (ADO Recordset)](../../../ado/reference/ado-api/open-method-ado-recordset.md) method to execute the query and return the desired cursor type.  
  
 The contents of the *CommandText* argument are specific to the provider and can be standard SQL syntax or any special command format that the provider supports.  
  
 An ExecuteComplete event will be issued when this operation concludes.  
  
> [!NOTE]
>  URLs using the http scheme will automatically invoke the [Microsoft OLE DB Provider for Internet Publishing](../../../ado/guide/appendixes/microsoft-ole-db-provider-for-internet-publishing.md). For more information, see [Absolute and Relative URLs](../../../ado/guide/data/absolute-and-relative-urls.md).  
  
## Applies To  
 [Connection Object (ADO)](../../../ado/reference/ado-api/connection-object-ado.md)
