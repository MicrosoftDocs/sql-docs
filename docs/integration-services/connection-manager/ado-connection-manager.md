---
title: "ADO Connection Manager | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
helpviewer_keywords: 
  - "connections [Integration Services], ADO"
  - "connection managers [Integration Services], ADO"
  - "ADO connection manager [Integration Services]"
ms.assetid: 490418bc-5ef1-41b8-a9c8-de38aa96e0f6
author: janinezhang
ms.author: janinez
manager: craigg
---
# ADO Connection Manager
  An ADO connection manager enables a package to connect to ActiveX Data Objects (ADO) objects, such as a recordset. This connection manager is typically used in custom tasks written in an earlier version of a language, such as Microsoft Visual Basic 6.0, or in custom tasks that are part of an existing application that uses ADO to connect to a data source.  
  
 When you add an ADO connection manager to a package, [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] creates a connection manager that will resolve to an ADO connection at run time, sets the connection manager properties, and adds the connection manager to the **Connections** collection on the package. The **ConnectionManagerType** property of the connection manager is set to **ADO**.  
  
## Troubleshooting the ADO Connection Manager  
 When being read by an ADO connection manager, certain [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] date data types will generate the results shown in the following table.  
  
|SQL Server Data type|Result|  
|--------------------------|------------|  
|**time**, **datetimeoffset**|The package fails unless the package uses parameterized SQL commands. To use parameterized SQL commands, use the Execute SQL Task in your package. For more information, see [Execute SQL Task](../../integration-services/control-flow/execute-sql-task.md) and [Parameters and Return Codes in the Execute SQL Task](https://msdn.microsoft.com/library/a3ca65e8-65cf-4272-9a81-765a706b8663).|  
|**datetime2**|The ADO connection manager truncates the millisecond value.|  
  
> [!NOTE]  
>  For more information about [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] data types and how they map to [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] data types, see [Data Types &#40;Transact-SQL&#41;](../../t-sql/data-types/data-types-transact-sql.md) and [Integration Services Data Types](../../integration-services/data-flow/integration-services-data-types.md).  
  
## Configuring the ADO Connection Manager  
 You can configure an ADO connection manager in the following ways:  
  
-   Provide a specific connection string configured to meet the requirements of the selected provider.  
  
-   Depending on the provider, include the name of the data source to connect to.  
  
-   Provide security credentials as appropriate for the selected provider.  
  
-   Indicate whether the connection that is created from the connection manager is retained at run time.  
  
 You can set properties through [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer or programmatically.  
  
 For more information about the properties that you can set in [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer, click the following topic:  
  
-   [Configure OLE DB Connection Manager](../../integration-services/connection-manager/configure-ole-db-connection-manager.md)  
  
 For information about configuring a connection manager programmatically, see <xref:Microsoft.SqlServer.Dts.Runtime.ConnectionManager> and [Adding Connections Programmatically](../../integration-services/building-packages-programmatically/adding-connections-programmatically.md).  
  
## See Also  
 [Integration Services &#40;SSIS&#41; Connections](../../integration-services/connection-manager/integration-services-ssis-connections.md)  
  
  
