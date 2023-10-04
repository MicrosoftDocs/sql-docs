---
title: "Address Book Data-Binding Object"
description: "Address Book Data-Binding Object"
author: rothja
ms.author: jroth
ms.date: 11/09/2018
ms.service: sql
ms.subservice: ado
ms.topic: conceptual
helpviewer_keywords:
  - "RDS scenarios [ADO], data-binding object"
  - "address book application scenario [ADO], data-binding object"
---
# Address Book Data-Binding Object
The Address Book application uses the [RDS.DataControl](../../reference/rds-api/datacontrol-object-rds.md) object to bind data from the SQL Server database to a visual object (in this case, a DHTML table) in the application's client HTML page. The event-driven VBScript program logic uses the [RDS.DataControl](../../reference/rds-api/datacontrol-object-rds.md) to:  
  
> [!IMPORTANT]
>  Beginning with Windows 8 and Windows Server 2012, RDS server components are no longer included in the Windows operating system (see Windows 8 and [Windows Server 2012 Compatibility Cookbook](https://www.microsoft.com/download/details.aspx?id=27416) for more detail). RDS client components will be removed in a future version of Windows. Avoid using this feature in new development work, and plan to modify applications that currently use this feature. Applications that use RDS should migrate to [WCF Data Service](/dotnet/framework/wcf/).  
  
-   Query the database, send updates to the database, and refresh the data grid.  
  
-   Allow users to move to the first, next, previous, or last record in the data grid.  
  
 The following code defines the **RDS.DataControl** component:  
  
```vb
<OBJECT classid="clsid:BD96C556-65A3-11D0-983A-00C04FC29E33"  
   ID=DC1 Width=1 Height=1>  
   <PARAM NAME="SERVER" VALUE="https://<%=Request.ServerVariables("SERVER_NAME")%>">  
   <PARAM NAME="CONNECT" VALUE="Provider=sqloledb;  
Initial Catalog=AddrBookDb;Integrated Security=SSPI;">  
</OBJECT>  
```  
  
 The OBJECT tag defines the **RDS.DataControl** component in the program. The tag includes two types of parameters:  
  
-   Those associated with the generic OBJECT tag.  
  
-   Those specific to the **RDS.DataControl** object.  
  
## Generic OBJECT Tag Parameters  
 The following table describes the parameters associated with the OBJECT tag.  
  
|Parameter|Description|  
|---------------|-----------------|  
|***CLASSID***|A unique, 128-bit number that identifies the type of embedded object to the system. This identifier is maintained in the local computer's system registry. (For the class IDs of the **RDS.DataControl** object, see [RDS.DataControl Object](../../reference/rds-api/datacontrol-object-rds.md).)|  
|***ID***|Defines a document-wide identifier for the embedded object that is used to identify it in code.|  
  
## RDS.DataControl Tag Parameters  
 The following table describes the parameters specific to the **RDS.DataControl** object. (For a complete list of the **RDS.DataControl** object parameters, and when to implement them, see [RDS.DataControl object](../../reference/rds-api/datacontrol-object-rds.md).)  
  
|Parameter|Description|  
|---------------|-----------------|  
|[SERVER](../../reference/rds-api/server-property-rds.md)|If you are using HTTP, the value is the name of the server computer preceded by `https://`.|  
|[CONNECT](../../reference/rds-api/connect-property-rds.md)|Provides the necessary connection information for the **RDS.DataControl** to connect to SQL Server.|  
|[SQL](../../reference/rds-api/sql-property.md)|Sets or returns the query string used to retrieve the [Recordset](../../reference/ado-api/recordset-object-ado.md).|  
  
## See Also  
 [Address Book Command Buttons](./address-book-command-buttons.md)