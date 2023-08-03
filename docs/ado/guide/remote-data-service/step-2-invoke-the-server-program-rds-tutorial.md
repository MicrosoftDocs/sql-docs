---
title: "Step 2: Invoke the Server Program (RDS Tutorial)"
description: "Step 2: Invoke the Server Program (RDS Tutorial)"
author: rothja
ms.author: jroth
ms.date: 11/09/2018
ms.service: sql
ms.subservice: ado
ms.topic: conceptual
helpviewer_keywords:
  - "RDS tutorial [ADO], invoking server program"
---
# Step 2: Invoke the Server Program (RDS Tutorial)
When you invoke a method on the client *proxy*, the actual program on the server executes the method. In this step, you'll execute a query on the server.  
  
> [!IMPORTANT]
>  Beginning with Windows 8 and Windows Server 2012, RDS server components are no longer included in the Windows operating system (see Windows 8 and [Windows Server 2012 Compatibility Cookbook](https://www.microsoft.com/download/details.aspx?id=27416) for more detail). RDS client components will be removed in a future version of Windows. Avoid using this feature in new development work, and plan to modify applications that currently use this feature. Applications that use RDS should migrate to [WCF Data Service](/dotnet/framework/wcf/).  
  
 **Part A** If you weren't using [RDSServer.DataFactory](../../reference/rds-api/datafactory-object-rdsserver.md) in this tutorial, the most convenient way to perform this step would be to use the [RDS.DataControl](../../reference/rds-api/datacontrol-object-rds.md) object. The **RDS.DataControl** combines the previous step of creating a proxy, with this step, issuing the query.  
  
 Set the **RDS.DataControl** object [Server](../../reference/rds-api/server-property-rds.md) property to identify where the server program should be instantiated; the [Connect](../../reference/rds-api/connect-property-rds.md) property to specify the connect string to access the data source; and the [SQL](../../reference/rds-api/sql-property.md) property to specify the query command text. Then issue the [Refresh](../../reference/rds-api/refresh-method-rds.md) method to cause the server program to connect to the data source, retrieve rows specified by the query, and return a **Recordset** object to the client.  
  
 This tutorial does not use the **RDS.DataControl**, but this is how it would look if it did:  
  
```vb
Sub RDSTutorial2A()  
   Dim DC as New RDS.DataControl  
   DC.Server = "https://yourServer"  
   DC.Connect = "DSN=Pubs"  
   DC.SQL = "SELECT * FROM Authors"  
   DC.Refresh  
...  
```  
  
 Nor does the tutorial invoke RDS with ADO objects, but this is how it would look if it did:  
  
```vb
Dim rs as New ADODB.Recordset  
rs.Open "SELECT * FROM Authors","Provider=MS Remote;Data Source=Pubs;" & _  
        "Remote Server=https://yourServer;Remote Provider=SQLOLEDB;"  
```  
  
 **Part B** The general method of performing this step is to invoke the **RDSServer.DataFactory** object [Query](../../reference/rds-api/query-method-rds.md) method. That method takes a connect string, which is used to connect to a data source, and a command text, which is used to specify the rows to be returned from the data source.  
  
 This tutorial uses the **DataFactory** object **Query** method:  
  
```vb
Sub RDSTutorial2B()  
   Dim DS as New RDS.DataSpace  
   Dim DF  
   Dim RS as ADODB.Recordset  
   Set DF = DS.CreateObject("RDSServer.DataFactory", "https://yourServer")  
   Set RS = DF.Query ("DSN=Pubs", "SELECT * FROM Authors")  
...  
```  
  
## See Also  
 [Step 3: Server Obtains a Recordset (RDS Tutorial)](./step-3-server-obtains-a-recordset-rds-tutorial.md)   
 [RDS Tutorial (VBScript)](./rds-tutorial-vbscript.md)