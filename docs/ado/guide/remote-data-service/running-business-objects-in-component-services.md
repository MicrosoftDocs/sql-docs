---
title: "Running Business Objects in Component Services | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: 11/09/2018
ms.reviewer: ""
ms.topic: conceptual
helpviewer_keywords: 
  - "component services in RDS [ADO]"
ms.assetid: 3077d0b6-42d6-4f10-8e5d-42e6204f1109
author: MightyPen
ms.author: genemi
manager: craigg
---
# Running Business Objects in Component Services
> [!IMPORTANT]
>  Beginning with Windows 8 and Windows Server 2012, RDS server components are no longer included in the Windows operating system (see Windows 8 and [Windows Server 2012 Compatibility Cookbook](https://www.microsoft.com/download/details.aspx?id=27416) for more detail). RDS client components will be removed in a future version of Windows. Avoid using this feature in new development work, and plan to modify applications that currently use this feature. Applications that use RDS should migrate to [WCF Data Service](https://go.microsoft.com/fwlink/?LinkId=199565).  
  
 Business objects can be executable files (.exe) or dynamic-link libraries (.dll). The configuration you use to run the business object depends on whether the object is a .dll or .exe file:  
  
-   Business objects created as .exe files can be called through DCOM. If these business objects are used through Internet Information Services (IIS), they are subject to additional marshaling of data, which will slow client performance.  
  
-   Business objects created as .dll files can be used through IIS, and therefore also by HTTP. They can also be used over DCOM only through Component Services, or through Microsoft Transaction Server, if you are using Windows NT. Business object DLLs will need to be registered on the IIS server computer to access them through  IIS. For information about how to configure a DLL to run on DCOM, see the section, [Enabling a DLL to Run on DCOM](../../../ado/guide/remote-data-service/enabling-a-dll-to-run-on-dcom.md).  
  
> [!NOTE]
>  When business objects on the middle tier are implemented as Component Services components by using **GetObjectContext**, **SetComplete**, and **SetAbort**, the business objects can use Component Services (or MTS, if you are using Windows NT) context objects to maintain their state across multiple client calls. This scenario is possible with DCOM, which is typically implemented between trusted clients and servers in an intranet. In this case, the [RDS.DataSpace](../../../ado/reference/rds-api/dataspace-object-rds.md) object and [CreateObject](../../../ado/reference/rds-api/createobject-method-rds.md) method on the client side are replaced by the transaction context object and **CreateInstance** method, which are provided by the **ITransactionContext** interface and implemented by Component Services.  
  
## See Also  
 [RDS Fundamentals](../../../ado/guide/remote-data-service/rds-fundamentals.md)


