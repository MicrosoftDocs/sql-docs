---
title: "Execute21 Method (RDS) | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.topic: conceptual
apitype: "COM"
helpviewer_keywords: 
  - "Execute21 method [RDS]"
ms.assetid: 9f131c8d-1497-416d-8209-abb481c38f7b
author: MightyPen
ms.author: genemi
manager: craigg
---
# Execute21 Method (RDS)
Executes the request and creates an ADO recordset for use in ADO 2.1.  
  
> [!IMPORTANT]
>  Beginning with Windows 8 and Windows Server 2012, RDS server components are no longer included in the Windows operating system (see Windows 8 and [Windows Server 2012 Compatibility Cookbook](https://www.microsoft.com/download/details.aspx?id=27416) for more detail). RDS client components will be removed in a future version of Windows. Avoid using this feature in new development work, and plan to modify applications that currently use this feature. Applications that use RDS should migrate to [WCF Data Service](https://go.microsoft.com/fwlink/?LinkId=199565).  
  
## Syntax  
  
```  
  
object.Execute21(ConnectionString As String, HandlerString As String, QueryString As String, lMarshalOptions As Long, Properties, TableId, lExecuteOptions As Long, pParameters)  
```  
  
#### Parameters  
 *ConnectionString*  
 A string used to connect to the OLE DB provider where the request will be sent for execution. If a handler is specified by using *HandlerString*, it can edit or replace the connection string.  
  
 *HandlerString*  
 The string identifies the handler to be used with this execution. The string contains two parts. The first part contains the name (ProgID) of the handler to be used. The second part of the string contains arguments to be passed to the handler. How the arguments string is interpreted is handler specific. The two parts are separated by the first instance of a comma in the string (although the arguments string can contain additional commas). The arguments are optional.  
  
 *QueryString*  
 A command in the command language supported by the OLE DB provider identified in the connection string. For SQL-based providers, it might contain a [!INCLUDE[tsql](../../../includes/tsql-md.md)] command statement, but for non-SQL providers (for example, MSDataShape) this may not be a [!INCLUDE[tsql](../../../includes/tsql-md.md)] query statement.  
  
 Also, if a handler is being used (and it is highly recommended that a handler be used), the handler can alter or replace the value specified here. For example, the handler typically replaces *QueryString* with a query string from its .ini file. By default, the Msdfmap.ini file is used.  
  
 *lMarshalOptions*  
 Used to set the marshaling options on the rowset/recordset being returned.  
  
 *TableID*  
 A variant of type either VT_EMPTY or VT_BSTR. If this value is of type VT_EMPTY, it is ignored. If it is of type VT_BSTR, the recordset is created by using **adCmdTableDirect** using the value specified here and the *QueryString* parameter is ignored.  
  
 *lExecuteOptions*  
 A bitmask of execution options:  
  
 1=*ReadOnly* The recordset will be opened by using **adLockReadOnly**.  
  
 2=*NoBatch* The recordset will be opened by using **adLockOptimistic**.  
  
 4=*AllParamInfoSupplied* The caller guarantees that parameter information for all parameters is supplied in *pParameters*.  
  
 8=*GetInfo* Parameter information for the query will be obtained from the OLE DB provider and returned in the *pParameters* parameter. The query is not executed and no recordset is returned.  
  
 16=GetHiddenColumns     The recordset will be opened by using **adLockBatchOptimistic** and any hidden columns will be included in the recordset.  
  
 Although *ReadOnly*, *NoBatch* and *GetHiddenColumns* are mutually exclusive options, it is not an error to set more than one of them. If multiple options are set, *GetHiddenColumns* takes precedence over all other options, followed by *ReadOnly*. If no options are specified, by default, the recordset is opened by using **adLockBatchOptimistic** but hidden columns are not included in the recordset.  
  
 *pParameters*  
 A variant that contains a safe array of parameter definitions. If the *GetInfo* option was specified in *lExecuteOptions*, this parameter is used to return the parameter definitions obtained from the OLE DB provider. Otherwise, this parameter may be empty.  
  
## Remarks  
 The *HandlerString* parameter may be null. What occurs in this case depends on how the RDS server is configured. A handler string of "MSDFMAP.handler" indicates that the Microsoft supplied handler (Msdfmap.dll) should be used. A handler string of "MASDFMAP.handler,sample.ini" indicates that the Msdfmap.dll handler should be used and that the argument "sample.ini" should be passed to the handler. MSDFMAP.dll will interpret the argument as a direction to use the sample.ini to check the connection and query strings.  
  
> [!NOTE]
>  The **Execute21** method is a version of the [Execute method (RDS)](../../../ado/reference/rds-api/execute-method-rds.md). Where you need to use the **Execute** method to communicate with ADO 2.1, the **Execute21** method can be called instead. The capabilities of the **Execute** method in ADO 2.5 and later are a superset of the capabilities provided for the same method in ADO 2.1.  
  
## Applies To  
 [DataFactory Object (RDSServer)](../../../ado/reference/rds-api/datafactory-object-rdsserver.md)


