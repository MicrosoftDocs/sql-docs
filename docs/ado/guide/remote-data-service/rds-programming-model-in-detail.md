---
title: "RDS Programming Model in Detail"
description: "RDS Programming Model in Detail"
author: rothja
ms.author: jroth
ms.date: 11/09/2018
ms.service: sql
ms.subservice: ado
ms.topic: conceptual
helpviewer_keywords:
  - "RDS programming model [ADO], details"
---
# RDS Programming Model in Detail
The following are key elements of the RDS programming model:  
  
-   RDS.DataSpace  
  
-   RDSServer.DataFactory  
  
-   RDS.DataControl  
  
-   Event  
  
> [!IMPORTANT]
>  Beginning with Windows 8 and Windows Server 2012, RDS server components are no longer included in the Windows operating system (see Windows 8 and [Windows Server 2012 Compatibility Cookbook](https://www.microsoft.com/download/details.aspx?id=27416) for more detail). RDS client components will be removed in a future version of Windows. Avoid using this feature in new development work, and plan to modify applications that currently use this feature. Applications that use RDS should migrate to [WCF Data Service](/dotnet/framework/wcf/).  
  
## RDS.DataSpace  
 Your client application must specify the server and the server program to invoke. In return, your application receives a reference to the server program and can treat the reference as if it were the server program itself.  
  
 The RDS object model embodies this functionality with the [RDS.DataSpace](../../reference/rds-api/dataspace-object-rds.md) object.  
  
 The server program is specified with a program identifier, or *ProgID*. The server uses the *ProgID* and the server machine's registry to locate information about the actual program to initiate.  
  
 RDS makes a distinction internally depending on whether the server program is on a remote server across the Internet or an intranet; a server on a local area network; or not on a server at all, but instead on a local dynamic-link library (DLL). This distinction determines how information is exchanged between the client and the server, and makes a tangible difference in the type of reference returned to your client application. However, from your point of view, this distinction has no special meaning. All that matters is that you receive a usable program reference.  
  
## RDSServer.DataFactory  
 RDS provides a default server program that can either perform a SQL query against the data source and return a [Recordset](../../reference/ado-api/recordset-object-ado.md) object or take a **Recordset** object and update the data source.  
  
 The RDS object model embodies this functionality with the [RDSServer.DataFactory](../../reference/rds-api/datafactory-object-rdsserver.md) object.  
  
 In addition, this object has a method for creating an empty **Recordset** object that you can fill programmatically ([CreateRecordset](../../reference/rds-api/createrecordset-method-rds.md)), and another method for converting a **Recordset** object into a text string to build a Web page ([ConvertToString](../../reference/rds-api/converttostring-method-rds.md)).  
  
 With ADO, you can override some of the standard connection and command behavior of the **RDSServer.DataFactory** with a **DataFactory** handler and a customization file that contains connection, command, and security parameters.  
  
 The server program is sometimes called a *business object*. You can write your own custom business object that can perform complicated data access, validity checks, and so on. Even when writing a custom business object, you can create an instance of an **RDSServer.DataFactory** object and use some of its methods to accomplish your own tasks.  
  
## RDS.DataControl  
 RDS provides a means to combine the functionality of the **RDS.DataSpace** and **RDSServer.DataFactory**, and also enable visual controls to easily use the **Recordset** object returned by a query from a data source. RDS attempts, for the most common case, to do as much as possible to automatically gain access to information on a server and display it in a visual control.  
  
 The RDS object model embodies this functionality with the [RDS.DataControl](../../reference/rds-api/datacontrol-object-rds.md) object.  
  
 The **RDS.DataControl** has two aspects. One aspect pertains to the data source. If you set the command and connection information using the **Connect** and **SQL** properties of the **RDS.DataControl**, it will automatically use the **RDS.DataSpace** to create a reference to the default **RDSServer.DataFactory** object. Then the **RDSServer.DataFactory** will use the **Connect** property value to connect to the data source, use the **SQL** property value to obtain a **Recordset** from the data source, and return the **Recordset** object to the **RDS.DataControl**.  
  
 The second aspect pertains to the display of returned **Recordset** information in a visual control. You can associate a visual control with the **RDS.DataControl** (in a process called binding) and gain access to the information in the associated **Recordset** object, displaying query results on a Web page in Microsoft® Internet Explorer. Each **RDS.DataControl** object binds one **Recordset** object, representing the results of a single query, to one or more visual controls (for example, a text box, combo box, grid control, and so forth). There may be more than one **RDS.DataControl** object on each page. Each **RDS.DataControl** object can be connected to a different data source and contain the results of a separate query.  
  
 The **RDS.DataControl** object also has its own methods for navigating, sorting, and filtering the rows of the associated **Recordset** object. These methods are similar, but not the same as the methods on the ADO **Recordset** object.  
  
## Events  
 RDS supports two of its own events, which are independent of the ADO event model. The [onReadyStateChange](../../reference/rds-api/onreadystatechange-event-rds.md) event is called whenever the **RDS.DataControl** [ReadyState](../../reference/rds-api/readystate-property-rds.md) property changes, thus notifying you when an asynchronous operation has successfully completed, terminated, or experienced an error. The [onError](../../reference/rds-api/onerror-event-rds.md) event is called whenever an error occurs, even if the error occurs during an asynchronous operation.  
  
> [!NOTE]
>  Microsoft Internet Explorer provides two additional events to RDS: **onDataSetChanged**, which indicates that the **Recordset** is functional but still retrieving rows, and **onDataSetComplete**, which indicates that the **Recordset** has finished retrieving rows.  
  
## See Also  
 [RDS Programming Model with Objects](./rds-programming-model-with-objects.md)   
 [DataControl Object (RDS)](../../reference/rds-api/datacontrol-object-rds.md)   
 [DataFactory Object (RDSServer)](../../reference/rds-api/datafactory-object-rdsserver.md)   
 [DataSpace Object (RDS)](../../reference/rds-api/dataspace-object-rds.md)   
 [RDS Scenario](./rds-scenario.md)   
 [RDS Tutorial](./rds-tutorial.md)   
 [RDS Usage and Security](./rds-usage-and-security.md)