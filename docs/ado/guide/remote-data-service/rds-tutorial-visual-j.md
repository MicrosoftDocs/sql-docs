---
title: "RDS Tutorial (Visual J++) | Microsoft Docs"
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
  - "RDS tutorial [ADO], Visual J++"
ms.assetid: d0d735e0-669a-41e7-ada2-8dd80924e349
caps.latest.revision: 15
author: "MightyPen"
ms.author: "annemill"
manager: "sonalm"
---
# RDS Tutorial (Visual J++)
ADO/WFC does not completely follow the RDS object model in that it does not implement the [RDS.DataControl](../../../ado/reference/rds-api/datacontrol-object-rds.md) object. ADO/WFC implements only the client-side class, [RDS.DataSpace](../../../ado/reference/rds-api/dataspace-object-rds.md).  
  
> [!IMPORTANT]
>  Beginning with Windows 8 and Windows Server 2012, RDS server components are no longer included in the Windows operating system (see Windows 8 and [Windows Server 2012 Compatibility Cookbook](https://www.microsoft.com/en-us/download/details.aspx?id=27416) for more detail). RDS client components will be removed in a future version of Windows. Avoid using this feature in new development work, and plan to modify applications that currently use this feature. Applications that use RDS should migrate to [WCF Data Service](http://go.microsoft.com/fwlink/?LinkId=199565).  
  
 The **DataSpace** class implements one method, [CreateObject](../../../ado/reference/rds-api/createobject-method-rds.md), which returns an [ObjectProxy](../../../ado/reference/ado-api/objectproxy-ado-wfc-syntax.md) object. The **DataSpace** class also implements the [InternetTimeout](../../../ado/reference/rds-api/internettimeout-property-rds.md) property.  
  
 The **ObjectProxy** class implements one method, call, which can invoke any server-side business object.  
  
 **This is the beginning of the tutorial.**  
  
```  
import com.ms.wfc.data.*;  
public class RDSTutorial   
{  
   public void tutorial()  
   {  
// Step 1: Specify a server program.  
      ObjectProxy obj =   
         DataSpace.createObject(  
            "RDSServer.DataFactory",   
            "http://YourServer");  
  
// Step 2: Server returns a Recordset.   
      Recordset rs = (Recordset) obj.call(  
            "Query",   
            new Object[] {"DSN=Pubs;", "SELECT * FROM Authors"});  
  
// Step 3: Changes are sent to the server.   
      ...                        // Edit Recordset.  
      obj.call(  
            "SubmitChanges",   
            new Object[] {"DSN=Pubs;", rs});     
      return;  
   }  
}  
```  
  
 **This is the end of the tutorial.**  
  
## See Also  
 [RDS Tutorial](../../../ado/guide/remote-data-service/rds-tutorial.md)   
 [RDS Tutorial (VBScript)](../../../ado/guide/remote-data-service/rds-tutorial-vbscript.md)


