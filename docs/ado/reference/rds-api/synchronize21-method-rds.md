---
title: "Synchronize21 Method (RDS) | Microsoft Docs"
ms.technology: connectivity
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: 
ms.prod: sql  
ms.prod_service: connectivity
ms.topic: conceptual
apitype: "COM"
helpviewer_keywords: 
  - "Synchronize21 method [ADO]"
ms.assetid: 6b35f136-9d9a-4bdd-8144-67decfd3c4e9
author: MightyPen
ms.author: genemi
manager: craigg
---
# Synchronize21 Method (RDS)
Synchronize the given recordset with the database specified by the connection string for use with ADO 2.1.  
  
> [!IMPORTANT]
>  Beginning with Windows 8 and Windows Server 2012, RDS server components are no longer included in the Windows operating system (see Windows 8 and [Windows Server 2012 Compatibility Cookbook](https://www.microsoft.com/download/details.aspx?id=27416) for more detail). RDS client components will be removed in a future version of Windows. Avoid using this feature in new development work, and plan to modify applications that currently use this feature. Applications that use RDS should migrate to [WCF Data Service](https://go.microsoft.com/fwlink/?LinkId=199565).  
  
## Syntax  
  
```  
  
object.Synchronize21(ConnectionString As String, HandlerString As String, lSynchronizeOptions As Long, ppRecordset As Object, pStatusArray)  
```  
  
#### Parameters  
 *ConnectionString*  
 A string used to connect to the OLE DB provider where the request will be sent. If a handler is used, the handler can edit or replace the connection string.  
  
 *HandlerString*  
 The string identifies the handler to be used with this execution. The string contains two parts. The first part contains the name (ProgID) of the handler to be used. The second part of the string contains arguments to be passed to the handler. How the arguments string is interpreted is handler specific. The two parts are separated by the first instance of a comma in the string. The arguments string can contain additional commas. The arguments are optional.  
  
 *lSynchronizeOptions*  
 A bit mask of synchronization options.  
  
 1=*UpdateTransact* Updates to the database are wrapped in a transaction. The transaction is aborted if any of the updates fail.  
  
 2=*RefreshWithUpdate* Causes row statuses to be returned when neither *Refresh* nor *RefreshConflicts* is set.  
  
 4=*Refresh* The recordset is refreshed with current data from the database. Pending updates are not pushed to the database. If this bit is not set, the recordset is not refreshed and any pending updates are pushed to the database.  
  
 8=*RefreshConflicts* Any rows with pending changes fail to update. The rows which failed to update are refreshed with current data from the database.  
  
 *ppRecordset*  
 A pointer to a pointer to the recordset to be synchronized.  
  
 *pStatusArray*  
 A variant used to return a safe array of row statuses for the rows affected by synchronize. Not set if none of the following synchronization options are set: *RefreshWithUpdate*, *Refresh* and *RefreshConflicts*.  
  
## Remarks  
 The *HandlerString* parameter can be null. What happens in this case depends on how the RDS server is configured. A handler string of "MSDFMAP.handler" indicates that the Microsoft supplied handler (Msdfmap.dll) should be used. A handler string of "MASDFMAP.handler,sample.ini" indicates that the Msdfmap.dll handler should be used and that the argument "sample.ini" should be passed to the handler. Msdfmap.dll will then interpret the argument as a direction to use the sample.ini to check the connection and query strings.  
  
> [!NOTE]
>  The **Synchronize21** method is simply a version of the [Synchronize Method (RDS)](../../../ado/reference/rds-api/synchronize-method-rds.md). Where you need to use the **Synchronize** method to communicate with ADO 2.1, the **Synchronize21** method can be called instead. The capabilities of the **Synchronize** method in ADO 2.5 and later are a superset of the capabilities provided for the same method in ADO 2.1.  
  
## Applies To  
 [DataFactory Object (RDSServer)](../../../ado/reference/rds-api/datafactory-object-rdsserver.md)


