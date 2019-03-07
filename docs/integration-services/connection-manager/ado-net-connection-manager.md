---
title: "ADO.NET Connection Manager | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
f1_keywords: 
  - "sql13.dts.designer.adonetconnection.f1"
helpviewer_keywords: 
  - "connection managers [Integration Services], ADO.NET"
  - "ADO.NET connection manager [Integration Services]"
  - "connections [Integration Services], ADO.NET"
ms.assetid: fc5daa2f-0159-4bda-9402-c87f1035a96f
author: "douglaslMS"
ms.author: "douglasl"
manager: craigg
---
# ADO.NET Connection Manager
  An [!INCLUDE[vstecado](../../includes/vstecado-md.md)] connection manager enables a package to access data sources by using a .NET provider. This connection manager is typically used to access data sources such as [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], and also data sources exposed through OLE DB and XML in custom tasks that are written in managed code by using a language such C#.  
  
 When you add an [!INCLUDE[vstecado](../../includes/vstecado-md.md)] connection manager to a package, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] creates a connection manager that is resolved as an [!INCLUDE[vstecado](../../includes/vstecado-md.md)] connection at run time, sets the connection manager properties, and adds the connection manager to the **Connections** collection on the package.  
  
 The **ConnectionManagerType** property of the connection manager is set to **ADO.NET**. The value of **ConnectionManagerType** is qualified to include the name of the .NET provider that the connection manager uses.  
  
## ADO.NET Connection Manager Troubleshooting  
 You can log the calls that the [!INCLUDE[vstecado](../../includes/vstecado-md.md)] connection manager makes to external data providers. You can use this logging capability to troubleshoot the connections that the [!INCLUDE[vstecado](../../includes/vstecado-md.md)] connection manager makes to external data sources. To log the calls that the [!INCLUDE[vstecado](../../includes/vstecado-md.md)] connection manager makes to external data providers, enable package logging and select the **Diagnostic** event at the package level. For more information, see [Troubleshooting Tools for Package Execution](../../integration-services/troubleshooting/troubleshooting-tools-for-package-execution.md).  
  
 When being read by an [!INCLUDE[vstecado](../../includes/vstecado-md.md)] connection manager, data of certain [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] date data types will generate the results shown in the following table.  
  
|SQL Server data type|Result|  
|--------------------------|------------|  
|**time**, **datetimeoffset**|The package fails unless the package uses parameterized SQL commands. To use parameterized SQL commands, use the Execute SQL Task in your package. For more information, see [Execute SQL Task](../../integration-services/control-flow/execute-sql-task.md) and [Parameters and Return Codes in the Execute SQL Task](https://msdn.microsoft.com/library/a3ca65e8-65cf-4272-9a81-765a706b8663).|  
|**datetime2**|The [!INCLUDE[vstecado](../../includes/vstecado-md.md)] connection manager truncates the millisecond value.|  
  
> [!NOTE]  
>  For more information about [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] data types and how they map to [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] data types, see [Data Types &#40;Transact-SQL&#41;](../../t-sql/data-types/data-types-transact-sql.md) and [Integration Services Data Types](../../integration-services/data-flow/integration-services-data-types.md).  
  
## ADO.NET Connection Manager Configuration  
 You can configure an [!INCLUDE[vstecado](../../includes/vstecado-md.md)] connection manager in the following ways:  
  
 You can set properties through [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer or programmatically.  
  
-   Provide a specific connection string configured to meet the requirements of the selected .NET provider.  
  
-   Depending on the provider, include the name of the data source to connect to.  
  
-   Provide security credentials as appropriate for the selected provider.  
  
-   Indicate whether the connection that is created from the connection manager is retained at run time.  
  
 Many of configuration options of the [!INCLUDE[vstecado](../../includes/vstecado-md.md)] connection manager depend on the .NET provider that the connection manager uses.  
  
 For more information about the properties that you can set in [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer, click one of the following topic:  
  
-   [Configure ADO.NET Connection Manager](../../integration-services/connection-manager/configure-ado-net-connection-manager.md)  
  
 For information about configuring a connection manager programmatically, see <xref:Microsoft.SqlServer.Dts.Runtime.ConnectionManager> and [Adding Connections Programmatically](../../integration-services/building-packages-programmatically/adding-connections-programmatically.md).  
  
## Configure ADO.NET Connection Manager
  Use the **Configure ADO.NET Connection Manager** dialog box to add a connection to a data source that can be accessed by using a .NET Framework data provider, such as the SqlClient provider. The connection manager can use an existing connection, or you can create a new one.  
  
 To learn more about the ADO.NET connection manager, see [ADO.NET Connection Manager](../../integration-services/connection-manager/ado-net-connection-manager.md).  
  
### Options  
 **Data connections**  
 Select an existing ADO.NET data connection from the list.  
  
 **Data connection properties**  
 View properties and values for the selected ADO.NET data connection.  
  
 **New**  
 Create an ADO.NET data connection by using the **Connection Manager** dialog box.  
  
 **Delete**  
 Select a connection, and then delete it by using the **Delete** button.  
  
## See Also  
 [Integration Services &#40;SSIS&#41; Connections](../../integration-services/connection-manager/integration-services-ssis-connections.md)  
  
  
