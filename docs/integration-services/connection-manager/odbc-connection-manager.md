---
title: "ODBC Connection Manager | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
f1_keywords: 
  - "sql13.dts.designer.odbcconnection.f1"
helpviewer_keywords: 
  - "connections [Integration Services], ODBC"
  - "ODBC connection manager"
  - "data sources [Integration Services], connections"
  - "connection managers [Integration Services], ODBC"
ms.assetid: e8c77aa7-6772-485e-918e-cef9eeb18c58
author: janinezhang
ms.author: janinez
manager: craigg
---
# ODBC Connection Manager
  An ODBC connection manager enables a package to connect to a variety of database management systems using the Open Database Connectivity specification (ODBC).  
  
 When you add an ODBC connection to a package and set the connection manager properties, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] creates a connection manager and adds the connection manager to the **Connections** collection of the package. At run time the connection manager is resolved as a physical ODBC connection.  
  
 The **ConnectionManagerType** property of the connection manager is set to **ODBC**.  
  
 You can configure the ODBC connection manager in the following ways:  
  
-   Provide a connection string that references a user or system data source name.  
  
-   Specify the server to connect to.  
  
-   Indicate whether the connection is retained at run time.  
  
## Configuration of the ODBC Connection Manager  
 You can set properties through [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer or programmatically.  
  
 For more information about the properties that you can set in [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer, click one of the following topic:  
  
-   [ODBC Connection Manager UI Reference](../../integration-services/connection-manager/odbc-connection-manager-ui-reference.md)  
  
 For information about configuring a connection manager programmatically, see <xref:Microsoft.SqlServer.Dts.Runtime.ConnectionManager> and [Adding Connections Programmatically](../../integration-services/building-packages-programmatically/adding-connections-programmatically.md).  
  
## ODBC Connection Manager UI Reference
  Use the **Configure ODBC Connection Manager** dialog box to add a connection to an ODBC data source.  
  
 To learn more about the ODBC connection manager, see [ODBC Connection Manager](../../integration-services/connection-manager/odbc-connection-manager.md).  
  
### Options  
 **Data connections**  
 Select an existing ODBC connection manager from the list.  
  
 **Data connection properties**  
 View properties and values for the selected ODBC connection manager.  
  
 **New**  
 Create an ODBC connection manager by using the **Connection Manager** dialog box. This dialog box also lets you create a new ODBC data source if it is required.  
  
 **Delete**  
 Select a connection, and then delete it by using the **Delete** button.  
## See Also  
 [Integration Services &#40;SSIS&#41; Connections](../../integration-services/connection-manager/integration-services-ssis-connections.md)  
  
  
