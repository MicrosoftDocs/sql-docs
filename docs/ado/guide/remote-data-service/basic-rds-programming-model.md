---
title: "Basic RDS Programming Model"
description: "Basic RDS Programming Model"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: conceptual
helpviewer_keywords:
  - "RDS programming model [ADO]"
---
# Basic RDS Programming Model
> [!IMPORTANT]
>  Beginning with Windows 8 and Windows Server 2012, RDS server components are no longer included in the Windows operating system (see Windows 8 and [Windows Server 2012 Compatibility Cookbook](https://www.microsoft.com/download/details.aspx?id=27416) for more detail). RDS client components will be removed in a future version of Windows. Avoid using this feature in new development work, and plan to modify applications that currently use this feature. Applications that use RDS should migrate to [WCF Data Service](/dotnet/framework/wcf/).  
  
 RDS addresses applications that exist in the following environment: A client application specifies a program that will execute on a server and the parameters required to return the desired information. The program invoked on the server gains access to the specified data source, retrieves the information, optionally processes the data, and then returns the resulting information to your client application in a form that it can easily use. RDS provides the means for you to perform the following sequence of actions:  
  
-   Specify the program to be invoked on the server, and obtain a way to refer to it from the client. (This reference is sometimes called a *proxy*. It represents the remote server program. The client application will "call" the proxy as if it were a local program, but it actually invokes the remote server program.)  
  
-   Invoke the server program. Pass parameters to the server program that identify the data source and the command to issue. (The server program actually uses ADO to gain access to the data source. ADO makes a connection with one of the given parameters, and then issues the command specified in the other parameter.)  
  
-   The server program obtains a [Recordset](../../reference/ado-api/recordset-object-ado.md) object from the data source. Optionally, the **Recordset** object is processed on the server.  
  
-   The server program returns the final **Recordset** object to the client application.  
  
-   On the client, the **Recordset** object is put into a form that can be easily used by visual controls.  
  
-   Any modifications to the **Recordset** object are sent back to the server program, which uses them to update the data source.  
  
 This programming model contains certain convenience features. If you do not need a complex server program to access the data source, and if you provide the required connection and command parameters, RDS will automatically retrieve the specified data with a simple, default server program.  
  
 If you need more complex processing, you can specify your own custom server program. For example, because a custom server program has the full power of ADO at its disposal, it could connect to several different data sources, combine their data in some complex way, and then return a simple, processed result to the client application.  
  
 Finally, if your needs are somewhere in between, ADO now supports customizing the behavior of the default server program.  
  
## See Also  
 [RDS Programming Model in Detail](./rds-programming-model-in-detail.md)   
 [RDS Scenario](./rds-scenario.md)   
 [RDS Tutorial](./rds-tutorial.md)   
 [Recordset Object (ADO)](../../reference/ado-api/recordset-object-ado.md)   
 [RDS Usage and Security](./rds-usage-and-security.md)