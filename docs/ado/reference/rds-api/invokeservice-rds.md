---
title: "InvokeService (RDS) | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.topic: conceptual
apitype: "COM"
helpviewer_keywords: 
  - "InvokeService [RDS]"
ms.assetid: ad45c676-ec7e-4a3a-9a6b-a54f75eb3012
author: MightyPen
ms.author: genemi
manager: craigg
---
# InvokeService (RDS)
Returns a pointer to the requested interface on a more capable version of the object.  
  
> [!IMPORTANT]
>  Beginning with Windows 8 and Windows Server 2012, RDS server components are no longer included in the Windows operating system (see Windows 8 and [Windows Server 2012 Compatibility Cookbook](https://www.microsoft.com/download/details.aspx?id=27416) for more detail). RDS client components will be removed in a future version of Windows. Avoid using this feature in new development work, and plan to modify applications that currently use this feature. Applications that use RDS should migrate to  [WCF Data Service](https://go.microsoft.com/fwlink/?LinkId=199565).  
  
## Syntax  
  
```  
  
object.InvokeService(REFID riid, IUknown* punkNotSoFunctionalInterface, IUknown** ppunkMoreFunctionalInterface) As HRESULT  
```  
  
#### Parameters  
 *riid*  
  
 [in] The identifier of the interface being requested.  
  
 *punkNotSoFunctionalInterface*  
  
 [in] The less capable source object.  
  
 *ppunkMoreFunctionalInterface*  
  
 [out] The address of the pointer variable that receives the interface pointer requested in *riid*. Upon successful return, the *ppunkMoreFunctionalInterface* parameter contains the requested interface pointer to the object. If the object does not support the interface specified in *riid*, *ppunkMoreFunctionalInterface* is set to NULL.  
  
## Return Value  
 An HRESULT value that indicates if the call to the **InvokeService** method was successful.  
  
## Remarks  
 The RDS cursor engine implementation of **InvokeService** takes the input rowset (or multiple results object), populates the cursor engine from the input rowset, and then returns a pointer on itself.  
  
## Applies To  
 [IRDSService Interface (RDS)](../../../ado/reference/rds-api/irdsservice-interface-rds.md)  
  
## See Also  
 [RDS Methods](../../../ado/reference/rds-api/rds-methods.md)


