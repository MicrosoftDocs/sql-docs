---
title: "OData Connection Manager | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
ms.assetid: 3caa4372-aff3-4c0f-9ecd-97870948b8d0
author: douglaslMS
ms.author: douglasl
manager: craigg
---
# OData Connection Manager
  An OData connection manager enables a package to connect to an OData source. An OData Source component connects to an OData source using an OData connection manager and consumes data from the service. See [OData Source](../data-flow/odata-source.md)section for detailed information including the installation instructions for these components.  
  
## Adding Connection Manager to an SSIS Package  
 You can add a new OData Connection Manager to an SSIS package in three ways:  
  
-   Click the **New...** button in the **OData Source Editor**  
  
-   Right-click **Connection Managers** folder in the **Solution Explorer** and click **New Connection Manager**. Select **ODATA** for **Connection manager type**.  
  
-   Right-click in the **Connection Managers** pane at the bottom of the package designer, and select **New Connection...**. Select **ODATA** for **Connection manager type**.  
  
## Connection Manager Authentication  
 The OData Connection Manager supports two modes of authentication.  
  
-   Windows Authentication  
  
-   Basic Authentication (username/password)  
  
 For anonymous access, select the Windows Authentication option  
  
### Specifying and Securing Credentials  
 If your OData service requires basic authentication, you can specify a username and password in the [OData Connection Manager Editor](../odata-connection-manager-editor.md). The values you enter in the editor are persisted in the package. The password value is encrypted according to the package protection level.  
  
 There are multiple ways to externalize/parameterize the username and password values. The two primary ways of doing this in [!INCLUDE[ssISversion11](../../includes/ssisversion11-md.md)] are by using parameters, or by setting the connection manager properties directly when you run the package using SQL Server Management Studio.  
  
## OData Connection Manager Properties  
 The following list describes some of the OData Connection Manager properties that you may want to change.  
  
|||  
|-|-|  
|Property|Description|  
|Url|URL to the service document.|  
|UserName|Username to use for Basic Authentication.|  
|Password|Password to use for Basic Authentication.|  
|ConnectionString|Reflects other properties of the connection manager.|  
  
## See Also  
 [OData Connection Manager Editor](../odata-connection-manager-editor.md)  
  
  
