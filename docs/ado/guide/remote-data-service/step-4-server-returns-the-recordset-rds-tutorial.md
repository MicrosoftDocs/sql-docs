---
title: "Step 4: Server Returns the Recordset (RDS Tutorial) | Microsoft Docs"
ms.prod: "sql-non-specified"
ms.technology:
  - "drivers"
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.suite: ""
ms.tgt_pltfrm: ""
ms.topic: "article"
helpviewer_keywords: 
  - "RDS tutorial [ADO], server returns Recordset"
ms.assetid: 3d1855c4-419c-4810-b5ea-6c874b5e2905
caps.latest.revision: 14
author: "MightyPen"
ms.author: "genemi"
manager: "jhubbard"
---
# Step 4: Server Returns the Recordset (RDS Tutorial)
> [!IMPORTANT]
>  Beginning with Windows 8 and Windows Server 2012, RDS server components are no longer included in the Windows operating system (see Windows 8 and [Windows Server 2012 Compatibility Cookbook](https://www.microsoft.com/en-us/download/details.aspx?id=27416) for more detail). RDS client components will be removed in a future version of Windows. Avoid using this feature in new development work, and plan to modify applications that currently use this feature. Applications that use RDS should migrate to [WCF Data Service](http://go.microsoft.com/fwlink/?LinkId=199565).  
  
 RDS converts the retrieved **Recordset** object to a form that can be sent back to the client (that is, it *marshals* the **Recordset**). The exact form of the conversion and how it is sent depends on whether the server is on the Internet or an intranet, a local area network, or is a dynamic-link library. However, this detail is not critical; all that matters is that RDS sends the **Recordset** back to the client.  
  
 On the client side, a **Recordset** object is returned and assigned to a local variable.  
  
```  
Sub RDSTutorial4()  
   Dim DS as New RDS.DataSpace  
   Dim RS as ADODB.Recordset  
   Dim DF as Object  
   Set DF = DS.CreateObject("RDSServer.DataFactory", "http://yourServer")  
   Set RS = DF.Query("DSN=Pubs", "SELECT * FROM Authors")  
...  
```  
  
## See Also  
 [Step 5: DataControl is Made Usable (RDS Tutorial)](../../../ado/guide/remote-data-service/step-5-datacontrol-is-made-usable-rds-tutorial.md)   
 [RDS Tutorial (VBScript)](../../../ado/guide/remote-data-service/rds-tutorial-vbscript.md)   
