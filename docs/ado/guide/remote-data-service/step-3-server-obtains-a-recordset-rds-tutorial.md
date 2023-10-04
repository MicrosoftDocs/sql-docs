---
title: "Step 3: Server Obtains a Recordset (RDS Tutorial)"
description: "Step 3: Server Obtains a Recordset (RDS Tutorial)"
author: rothja
ms.author: jroth
ms.date: 11/09/2018
ms.service: sql
ms.subservice: ado
ms.topic: conceptual
helpviewer_keywords:
  - "RDS tutorial [ADO], server obtains Recordset"
---
# Step 3: Server Obtains a Recordset (RDS Tutorial)
The server program uses the connect string and command text to query the data source for the desired rows. ADO is typically used to retrieve this **Recordset**, although other Microsoft data access interfaces, such as OLE DB, could be used.  
  
> [!IMPORTANT]
>  Beginning with Windows 8 and Windows Server 2012, RDS server components are no longer included in the Windows operating system (see Windows 8 and [Windows Server 2012 Compatibility Cookbook](https://www.microsoft.com/download/details.aspx?id=27416) for more detail). RDS client components will be removed in a future version of Windows. Avoid using this feature in new development work, and plan to modify applications that currently use this feature. Applications that use RDS should migrate to [WCF Data Service](/dotnet/framework/wcf/).  
  
 A custom server program might look like this:  
  
```vb
Public Function ServerProgram(cn as String, qry as String) as Object  
Dim rs as New ADODB.Recordset  
   rs.CursorLocation = adUseClient  
   rs.Open qry, cn   
   rs.ActiveConnection = Nothing  
   Set ServerProgram = rs  
End Function  
```  
  
## See Also  
 [Step 4: Server Returns the Recordset (RDS Tutorial)](./step-4-server-returns-the-recordset-rds-tutorial.md)   
 [RDS Tutorial (VBScript)](./rds-tutorial-vbscript.md)