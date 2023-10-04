---
title: "Synchronize Method (RDS)"
description: "Synchronize Method (RDS)"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: reference
helpviewer_keywords:
  - "Synchronize method [ADO]"
apitype: "COM"
---
# Synchronize Method (RDS)
Synchronize the given Recordset with the database specified by the connection string for use in ADO 2.5 and later.  
  
> [!IMPORTANT]
>  Beginning with Windows 8 and Windows Server 2012, RDS server components are no longer included in the Windows operating system (see Windows 8 and [Windows Server 2012 Compatibility Cookbook](https://www.microsoft.com/download/details.aspx?id=27416) for more detail). RDS client components will be removed in a future version of Windows. Avoid using this feature in new development work, and plan to modify applications that currently use this feature. Applications that use RDS should migrate to [WCF Data Service](/dotnet/framework/wcf/).  
  
## Syntax  
  
```  
  
object.Synchronize(ConnectionString As String, HandlerString As String, lSynchronizeOptions As Long, ppRecordset As Object, pStatusArray, [lcid As Long], [pInformation)  
```  
  
#### Parameters  
 *ConnectionString*  
 A string used to connect to the OLE DB provider where the request will be sent. If a handler is used, the handler may edit or replace the connection string.  
  
 *HandlerString*  
 The string identifies the handler to be used with this execution. The string contains two parts. The first part contains the name (ProgID) of the handler to be used. The second part of the string contains arguments to be passed to the handler. How the arguments string is interpreted is handler specific. The two parts are separated by the first instance of a comma in the string (although the arguments string may contain additional commas). The arguments are optional.  
  
 *lSynchronizeOptions*  
 A bit mask of synchronization options.  
  
 1=*UpdateTransact* Updates to the database are wrapped in a transaction. The transaction is aborted if any of the updates fail.  
  
 2=*RefreshWithUpdate* Causes row statuses to be returned when neither *Refresh* nor *RefreshConflicts* is set.  
  
 4=*Refresh* The recordset is refreshed with current data from the database. Pending updates are not pushed to the database. If this bit is not set, the recordset is not refreshed, and any pending updates are pushed to the database.  
  
 8=*RefreshConflicts* Any rows with pending changes fail to update. The rows which failed to update are refreshed with current data from the database.  
  
 *ppRecordset*  
 A pointer to the recordset to be synchronized.  
  
 *pStatusArray*  
 A variant used to return a safe array of row statuses for the rows affected by synchronize. Not set if none of the following synchronization options are set: *RefreshWithUpdate*, *Refresh* and *RefreshConflicts*.  
  
 *lcid*  
 The LCID used to build any errors that are returned in *pInformation*.  
  
 *pInformation*  
 A pointer to information error returned by **Execute**. If NULL, no error information is returned.  
  
## Remarks  
 The *HandlerString* parameter may be null. What happens in this case depends on how the RDS server is configured. A handler string of "MSDFMAP.handler" indicates that the Microsoft supplied handler (Msdfmap.dll) should be used. A handler string of "MASDFMAP.handler,sample.ini" indicates that the Msdfmap.dll handler should be used and that the argument "sample.ini" should be passed to the handler. Msdfmap.dll will then interpret the argument as a direction to use the sample.ini to check the connection and query strings.  
  
## Applies To  
 [DataFactory Object (RDSServer)](./datafactory-object-rdsserver.md)