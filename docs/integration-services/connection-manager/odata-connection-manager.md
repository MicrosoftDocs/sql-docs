---
title: "OData Connection Manager | Microsoft Docs"
ms.date: "03/01/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "integration-services"
ms.tgt_pltfrm: ""
ms.topic: "article"
ms.assetid: 3caa4372-aff3-4c0f-9ecd-97870948b8d0
caps.latest.revision: 9
author: "douglaslMS"
ms.author: "douglasl"
manager: "jhubbard"
---
# OData Connection Manager
  Use an OData connection manager to connect to an OData source. An OData Source component connects to an OData source by using an OData connection manager and consumes data from the service. For more info, see [OData Source](../../integration-services/data-flow/odata-source.md).  
  
## Adding an OData Connection Manager to an SSIS Package  
 You can add a new OData Connection Manager to an SSIS package in three ways:  
  
-   Click the **New…** button in the **OData Source Editor**  
  
-   Right-click the **Connection Managers** folder in **Solution Explorer**, and then click **New Connection Manager**. Select **ODATA** for **Connection manager type**.  
  
-   Right-click in the **Connection Managers** pane at the bottom of the package designer, and then select **New Connection…**. Select **ODATA** for **Connection manager type**.  
  
## Connection Manager Authentication  
 The OData Connection Manager supports five modes of authentication.  
  
-   Windows Authentication  
  
-   Basic Authentication (with username and password)  

-   Microsoft Dynamics AX Online (with username and password)
  
-   Microsoft Dynamics CRM Online (with username and password)
  
-   Microsoft Online Services (with username and password)  
  
 For anonymous access, select the Windows Authentication option.  
  
### Specifying and Securing Credentials  
 If the OData service requires basic authentication, you can specify a username and password in the [OData Connection Manager Editor](../../integration-services/connection-manager/odata-connection-manager-editor.md). The values you enter in the editor are persisted in the package. The password value is encrypted according to the package protection level.  
  
 There are several ways to parameterize the username and password values or to store them outside the package. For example, you can do this by using parameters, or by setting the connection manager properties directly when you run the package using SQL Server Management Studio.  
  
## OData Connection Manager Properties  
 The following list describes the properties of the OData Connection Manager.  
  
|||  
|-|-|  
|Property|Description|  
|Url|URL to the service document.|  
|UserName|Username to use for Basic Authentication.|  
|Password|Password to use for Basic Authentication.|  
|ConnectionString|Reflects other properties of the connection manager.|  
  
## See Also  
 [OData Connection Manager Editor](../../integration-services/connection-manager/odata-connection-manager-editor.md)  
  
  