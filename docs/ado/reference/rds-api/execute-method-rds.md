---
title: "Execute Method (RDS) | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.topic: conceptual
apitype: "COM"
helpviewer_keywords: 
  - "Execute method [ADO]"
ms.assetid: 2d9c30e9-ab5b-4920-91b8-48454c2fb5d8
author: MightyPen
ms.author: genemi
manager: craigg
---
# Execute Method (RDS)
Executes the request and creates an ADO recordset for use in ADO 2.5 and later.  
  
> [!IMPORTANT]
>  Beginning with Windows 8 and Windows Server 2012, RDS server components are no longer included in the Windows operating system (see Windows 8 and [Windows Server 2012 Compatibility Cookbook](https://www.microsoft.com/download/details.aspx?id=27416) for more detail). RDS client components will be removed in a future version of Windows. Avoid using this feature in new development work, and plan to modify applications that currently use this feature. Applications that use RDS should migrate to [WCF Data Service](https://go.microsoft.com/fwlink/?LinkId=199565).  
  
## Syntax  
  
```  
  
object.Execute(ConnectionString As String, HandlerString As String, QueryString As String, lFetchOptions As Long, Properties, TableId, lExecuteOptions As Long, pParameters, [lcid As Long], [pInformation])  
```  
  
#### Parameters  
 *ConnectionString*  
 A string used to connect to the OLE DB provider where the request will be sent for execution. If a handler is specified by using *HandlerString* it can edit or replace the connection string.  
  
 *HandlerString*  
 A two-part string that identifies the handler to be used with this execution. The string contains two parts. The first part contains the name (ProgID) of the handler to be used. The second part contains arguments to be passed to the handler. The details of how the arguments string is interpreted are specific to each handler. The two parts are separated by the first instance of a comma in the string. The arguments string can contain additional commas. The arguments are optional.  
  
 *QueryString*  
 A command in the command language supported by the OLE DB provider identified in the connection string. For SQL-based providers, *QueryString* might contain a Transact-SQL command statement, but for non-SQL providers (for example, MSDataShape) this may not be a [!INCLUDE[tsql](../../../includes/tsql-md.md)] query statement.  
  
 If a handler is being used, the handler can alter or replace the value specified here. For example, the handler typically replaces *QueryString* with a query string from its .ini file. By default, the Msdfmap.ini file is used.  
  
 *lFetchOptions*  
 Indicates the type of asynchronous fetching.  
  
 For more information, see [FetchOptions Property (RDS)](../../../ado/reference/rds-api/fetchoptions-property-rds.md).  
  
 *TableID*  
 A **Variant** of type either VT_EMPTY or VT_BSTR. If this value is of type VT_EMPTY, it is ignored. If it is of type VT_BSTR, the recordset is created by using **adCmdTableDirect** and the value specified here and the *QueryString* parameter is ignored.  
  
 *lExecuteOptions*  
 A bit mask of execution options:  
  
 1=*ReadOnly* The recordset will be opened by using **adLockReadOnly**.  
  
 2=*NoBatch* The recordset will be opened by using **adLockOptimistic**.  
  
 4=*AllParamInfoSupplied* The caller guarantees that parameter information for all parameters is supplied in *pParameters*.  
  
 8=*GetInfo* Parameter information for the query will be obtained from the OLE DB provider and returned in the *pParameters* parameter. The query is not executed and no recordset is returned.  
  
 16=*GetHiddenColumns* The recordset will be opened by using **adLockBatchOptimistic** and any hidden columns will be included in the recordset.  
  
 *ReadOnly*, *NoBatch* and *GetHiddenColumns* are mutually exclusive options; however, it does not generate an error to set more than one of them. If multiple options are set, *GetHiddenColumns* takes precedence over all others, followed by *ReadOnly*. If no options are specified, by default, the recordset is opened by using **adLockBatchOptimistic** and hidden columns are not included in the recordset.  
  
 *pParameters*  
 A **Variant** that contains a safe array of parameter definitions. If the *GetInfo* option was specified in *lExecuteOptions*, this parameter is used to return the parameter definitions obtained from the OLE DB provider. Otherwise, this parameter can be empty.  
  
 *lcid*  
 The LCID used to build any errors that are returned in *pInformation*.  
  
 *pInformation*  
 A pointer to information error returned by Execute. If NULL, no error information is returned.  
  
## Remarks  
 The *HandlerString* parameter may be null. What happens in this case depends on how the RDS server is configured. A handler string of "MSDFMAP.handler" indicates that the Microsoft supplied handler (Msdfmap.dll) should be used. A handler string of "MASDFMAP.handler,sample.ini" indicates that the Msdfmap.dll handler should be used and that the argument "sample.ini" should be passed to the handler. MSDFMAP.dll will interpret the argument as a direction to use the sample.ini to check the connection and query strings.  
  
## Applies To  
 [DataFactory Object (RDSServer)](../../../ado/reference/rds-api/datafactory-object-rdsserver.md)


