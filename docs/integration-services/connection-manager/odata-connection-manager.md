---
description: "OData Connection Manager"
title: "OData Connection Manager | Microsoft Docs"
ms.date: "03/01/2017"
ms.service: sql
ms.reviewer: ""
ms.custom: ""
ms.subservice: integration-services
ms.topic: conceptual
ms.assetid: 3caa4372-aff3-4c0f-9ecd-97870948b8d0
f1_keywords: 
  - "sql13.dts.designer.odatasource.connectionmanager.f1"
  - "sql13.dts.designer.odataconnectionmanager.f1"
author: chugugrace
ms.author: chugu
---
# OData Connection Manager

[!INCLUDE[sqlserver-ssis](../../includes/applies-to-version/sqlserver-ssis.md)]


 Connect to an OData data source with an OData connection manager. An OData Source component uses an OData connection manager to connect to an OData data source and consume data from the service. For more info, see [OData Source](../../integration-services/data-flow/odata-source.md).  
  
## Adding an OData Connection Manager to an SSIS Package  
 You can add a new OData connection manager to an SSIS package in three ways:  
  
-   Click the **New...** button in the **OData Source Editor**  
  
-   Right-click the **Connection Managers** folder in **Solution Explorer**, and then click **New Connection Manager**. Select **ODATA** for **Connection manager type**.  
  
-   Right-click in the **Connection Managers** pane at the bottom of the package designer, and then select **New Connection**. Select **ODATA** for **Connection manager type**.  
> [!NOTE]
> Make sure that the international settings language in your Visual Studio / SQL Server Data Tools (SSDT) is aligned to the default language of the target SQL Server instance where the SSIS packages are deployed.
>   
## Connection Manager Authentication  
 The OData connection manager supports five modes of authentication.  
  
-   Windows Authentication  
  
-   Basic Authentication (with username and password)  

-   Microsoft Dynamics AX Online (with username and password)
  
-   Microsoft Dynamics CRM Online (with username and password)
  
-   Microsoft Online Services (with username and password)  
  
For anonymous access, select the Windows Authentication option.  

To connect to Microsoft Dynamics AX Online or Microsoft Dynamics CRM online, you can't use the **Microsoft Online Services** authentication option. You also can't use any option that's configured for multi-factor authentication. Currently Modern authentication is not supported. 
  
### Specifying and Securing Credentials  
 If the OData service requires basic authentication, you can specify a username and password in the [OData Connection Manager Editor](). The values you enter in the editor are persisted in the package. The password value is encrypted according to the package protection level.  
  
 There are several ways to parameterize the username and password values or to store them outside the package. For example, you can use parameters, or set the connection manager properties directly when you run the package from SQL Server Management Studio.  
  
## OData Connection Manager Properties  
 The following list describes the properties of the OData connection manager.  
  
|Property|Default Value|Description|  
|-|-|-|
|Keep Alive|False|Value of the "Keep-Alive" header when sending web request.|
|Max Received Message Size|4TB|The max received message size in byte when sending web request.|
|Retry Count|5|Retry count when sending web request.|
|Retry Sleep|100|Sleep time in millisecond for retry when sending web request.|
|Time Out|600|Timeout in second when sending web request.|
|Url||URL to the service document.|  
|UserName||User name to use for authentication, if required.|  
|Password||Password to use for authentication, if required.|  
|ConnectionString||Includes other properties of the connection manager.|  
  
## OData Connection Manager Editor
  Use the **OData Connection Manager Editor** dialog box to add a connection or edit an existing connection to an OData data source.  
  
### Options  
 **Connection manager name**  
 Name of the connection manager.  
  
 **Service document location**  
 URL for the OData service. For example: https://services.odata.org/V3/Northwind/Northwind.svc/.  
  
 **Authentication**  
Select one of the following options:
-   **Windows Authentication**. For anonymous access, select this option.
-   **Basic Authentication** 
-   **Microsoft Dynamics AX Online** for Dynamics AX Online
-   **Microsoft Dynamics CRM Online** for Dynamics CRM Online
-   **Microsoft Online Services** for Microsoft Online Services

If you select an option other than Windows Authentication, enter the **user name** and **password**. 

To connect to Microsoft Dynamics AX Online or Microsoft Dynamics CRM online, you can't use the **Microsoft Online Services** authentication option. You also can't use any option that's configured for multi-factor authentication.

 **Test Connection**  
 Click this button to test the connection to the OData source.
