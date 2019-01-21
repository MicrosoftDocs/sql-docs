---
title: "CreateObject Method (RDS) | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.topic: conceptual
apitype: "COM"
helpviewer_keywords: 
  - "CreateObject method [ADO]"
ms.assetid: dec96be6-0b31-4953-9c9a-e962b5afcd18
author: MightyPen
ms.author: genemi
manager: craigg
---
# CreateObject Method (RDS)
Creates the proxy for the target business object and returns a pointer to it. The proxy packages and marshals data to the server-side stub for communications with the business object to send requests and data over the Internet. For in-process component objects, no proxies are used, just a pointer to the object is provided.  
  
> [!IMPORTANT]
>  Beginning with Windows 8 and Windows Server 2012, RDS server components are no longer included in the Windows operating system (see Windows 8 and [Windows Server 2012 Compatibility Cookbook](https://www.microsoft.com/download/details.aspx?id=27416) for more detail). RDS client components will be removed in a future version of Windows. Avoid using this feature in new development work, and plan to modify applications that currently use this feature. Applications that use RDS should migrate to [WCF Data Service](https://go.microsoft.com/fwlink/?LinkId=199565).  
  
## Syntax  
 Remote Data Service supports the following protocols: HTTP, HTTPS (HTTP over Secure Socket Layer), DCOM, and in-process.  
  
|Protocol|Syntax|  
|--------------|------------|  
|HTTP|Set object = DataSpace.CreateObject("ProgId", "https\://awebsrvr")|  
|HTTPS|Set object = DataSpace.CreateObject("ProgId", "https\://awebsrvr")|  
|DCOM|Set object = DataSpace.CreateObject("ProgId", "computername")|  
|In-process|Set object = DataSpace.CreateObject("ProgId", "")|  
  
## Parameters  
 *Object*  
 An object variable that evaluates to an object that is the type specified in *ProgID*.  
  
 *DataSpace*  
 An object variable that represents an [RDS.DataSpace](../../../ado/reference/rds-api/dataspace-object-rds.md) object used to create an instance of the new object.  
  
 *ProgID*  
 A **String** value that contains the programmatic identifier specifying a server-side business object that implements your application's business rules.  
  
 *awebsrvr* or *computername*  
 A **String** value that represents a URL identifying the Internet Information Services (IIS) Web server where an instance of the server business object is created.  
  
## Remarks  
 The *HTTP protocol* is the standard Web protocol; *HTTPS* is a secure Web protocol. Use the *DCOM protocol* when running a local-area network without HTTP. The *in-process* protocol is a local dynamic-link library (DLL); it does not use a network.  
  
## Applies To  
 [DataSpace Object (RDS)](../../../ado/reference/rds-api/dataspace-object-rds.md)  
  
## See Also  
 [DataFactory Object, Query Method, and CreateObject Method Example (VBScript)](../../../ado/reference/rds-api/datafactory-object-query-method-and-createobject-method-example-vbscript.md)   
 [DataSpace Object and CreateObject Method Example (VBScript)](../../../ado/reference/rds-api/dataspace-object-and-createobject-method-example-vbscript.md)   
 [CreateRecordset Method (RDS)](../../../ado/reference/rds-api/createrecordset-method-rds.md)


