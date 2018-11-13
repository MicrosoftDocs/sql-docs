---
title: "InternetTimeout Property (RDS) | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.topic: conceptual
apitype: "COM"
helpviewer_keywords: 
  - "InternetTimeout property [ADO]"
ms.assetid: 4d1c8892-4bbc-4e71-bf4b-ba52c0ea9549
author: MightyPen
ms.author: genemi
manager: craigg
---
# InternetTimeout Property (RDS)
Indicates the number of milliseconds to wait before a request times out.  
  
> [!IMPORTANT]
>  Beginning with Windows 8 and Windows Server 2012, RDS server components are no longer included in the Windows operating system (see Windows 8 and [Windows Server 2012 Compatibility Cookbook](https://www.microsoft.com/download/details.aspx?id=27416) for more detail). RDS client components will be removed in a future version of Windows. Avoid using this feature in new development work, and plan to modify applications that currently use this feature. Applications that use RDS should migrate to [WCF Data Service](https://go.microsoft.com/fwlink/?LinkId=199565).  
  
## Settings and Return Values  
 Sets or returns a **Long** value that represents the number of milliseconds before a request times out.  
  
## Remarks  
 This property applies only to requests sent with the HTTP or HTTPS protocols.  
  
 Requests in a three-tier environment can take several minutes to execute. Use this property to specify additional time for long-running requests.  
  
## Applies To  
  
|||  
|-|-|  
|[DataControl Object (RDS)](../../../ado/reference/rds-api/datacontrol-object-rds.md)|[DataSpace Object (RDS)](../../../ado/reference/rds-api/dataspace-object-rds.md)|  
  
## See Also  
 [InternetTimeout Property Example (VB)](../../../ado/reference/rds-api/internettimeout-property-example-vb.md)   
 [InternetTimeout Property Example (VC++)](../../../ado/reference/rds-api/internettimeout-property-example-vc.md)   
 

