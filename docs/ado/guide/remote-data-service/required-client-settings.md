---
title: "Required Client Settings | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: 11/09/2018
ms.reviewer: ""
ms.topic: conceptual
helpviewer_keywords: 
  - "DataFactory handler in RDS [ADO]"
ms.assetid: e776b4e3-fcc4-4bfb-a7e8-5ffae1d83833
author: MightyPen
ms.author: genemi
manager: craigg
---
# Required Client Settings
> [!IMPORTANT]
>  Beginning with Windows 8 and Windows Server 2012, RDS server components are no longer included in the Windows operating system (see Windows 8 and [Windows Server 2012 Compatibility Cookbook](https://www.microsoft.com/download/details.aspx?id=27416) for more detail). RDS client components will be removed in a future version of Windows. Avoid using this feature in new development work, and plan to modify applications that currently use this feature. Applications that use RDS should migrate to [WCF Data Service](https://go.microsoft.com/fwlink/?LinkId=199565).  
  
 Specify the following settings to use a custom **DataFactory** handler.  
  
-   Specify "Provider=MS Remote" in the [Connection Object (ADO)](../../../ado/reference/ado-api/connection-object-ado.md) object [Provider Property (ADO)](../../../ado/reference/ado-api/provider-property-ado.md) property or the **Connection** object connection string "**Provider**=" keyword.  
  
-   Set the [CursorLocation Property (ADO)](../../../ado/reference/ado-api/cursorlocation-property-ado.md) property to **adUseClient**.  
  
-   Specify the name of the handler to use in the [DataControl Object (RDS)](../../../ado/reference/rds-api/datacontrol-object-rds.md) object's **Handler** property, or the [Recordset Object (ADO)](../../../ado/reference/ado-api/recordset-object-ado.md) object's connection string "**Handler**=" keyword. (You cannot set the handler in the **Connection** object connect string.)  
  
 RDS provides a default handler on the server named **MSDFMAP.Handler**. (The default customization file is named MSDFMAP.INI.)  
  
 **Example**  
  
 Assume that the following sections in **MSDFMAP.INI** and the data source name, AdvWorks, have been previously defined:  
  
```console
[connect CustomerDataBase]  
Access=ReadWrite  
Connect="DSN=AdvWorks"  
  
[sql CustomerById]  
SQL="SELECT * FROM Customers WHERE CustomerID = ?"  
```  
  
 The following code snippets are written in Visual Basic:  
  
## RDS.DataControl Version  
  
```vb
Dim dc as New RDS.DataControl  
Set dc.Handler = "MSDFMAP.Handler"  
Set dc.Server = "https://yourServer"  
Set dc.Connect = "Data Source=CustomerDatabase"  
Set dc.SQL = "CustomerById(4)"  
dc.Refresh  
```  
  
## Recordset Version  
  
```vb
Dim rs as New ADODB.Recordset  
rs.CursorLocation = adUseClient  
```  
  
 Specify either the [Handler Property (RDS)](../../../ado/reference/rds-api/handler-property-rds.md) property or keyword; the [Provider Property (ADO)](../../../ado/reference/ado-api/provider-property-ado.md) property or keyword; and the *CustomerById* and *CustomerDatabase* identifiers. Then open the **Recordset** object  
  
 rs.Open "CustomerById(4)", "Handler=MSDFMAP.Handler;" & _  
  
```vb
"Provider=MS Remote;Data Source=CustomerDatabase;" & _  
"Remote Server=https://yourServer"  
```  
  
## See Also  
 [Customization File Connect Section](../../../ado/guide/remote-data-service/customization-file-connect-section.md)   
 [Customization File SQL Section](../../../ado/guide/remote-data-service/customization-file-sql-section.md)   
 [Customization File UserList Section](../../../ado/guide/remote-data-service/customization-file-userlist-section.md)   
 [DataFactory Customization](../../../ado/guide/remote-data-service/datafactory-customization.md)   
 [Required Client Settings](../../../ado/guide/remote-data-service/required-client-settings.md)   
 [Understanding the Customization File](../../../ado/guide/remote-data-service/understanding-the-customization-file.md)   
 [Writing Your Own Customized Handler](../../../ado/guide/remote-data-service/writing-your-own-customized-handler.md)

