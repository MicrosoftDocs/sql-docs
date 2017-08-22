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
f1_keywords: 
  - "sql13.dts.designer.odatasource.connectionmanager.f1"
  - "sql13.dts.designer.odataconnectionmanager.f1"
author: "douglaslMS"
ms.author: "douglasl"
manager: "jhubbard"
---
# OData Connection Manager
 Connect to an OData data source with an OData connection manager. An OData Source component uses an OData connection manager to connect to an OData data source and consume data from the service. For more info, see [OData Source](../../integration-services/data-flow/odata-source.md).  
  
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
|ConnectionString|Includes other properties of the connection manager.|  
  
## OData Connection Manager Editor
  Use the **OData Connection Manager Editor** dialog box to add a connection or edit an existing connection to an OData data source.  
  
### Options  
 **Connection manager name**  
 Name of the connection manager.  
  
 **Service document location**  
 URL for the OData service. For example: http://services.odata.org/V3/Northwind/Northwind.svc/.  
  
 **Authentication**  
Select one of the following options:
-   **Windows Authentication**. For anonymous access, select this option.
-   **Basic Authentication** 
-   **Microsoft Dynamics AX Online** for Dynamics AX Online
-   **Microsoft Dynamics CRM Online** for Dynamics CRM Online
-   **Microsoft Online Services** for Microsoft Online Services
 
If you select an option other than Windows Authentication, enter the **user name** and **password**. 

 **Test Connection**  
 Click this button to test the connection to the OData source.  
