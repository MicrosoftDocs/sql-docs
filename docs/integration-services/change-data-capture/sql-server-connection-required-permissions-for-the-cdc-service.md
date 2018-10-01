---
title: "SQL Server Connection Required Permissions for the CDC Service | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
ms.assetid: d9e968f9-180c-4fa0-a849-98f2b1942330
author: "douglaslMS"
ms.author: "douglasl"
manager: craigg
---
# SQL Server Connection Required Permissions for the CDC Service
  The CDC Service Configuration Console requires connection information to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] to perform their tasks. This topic describes the information that can be provided in the Connect to SQL Server dialog box for setting up the connection to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
 The Connect to SQL Server dialog box opens when necessary, such as when the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] connection information is not available or when the information exists but the connection does not have the required permissions.  
  
 The following table describes the various tasks where a connection to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is required and the required permissions from the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login/user.  
  
|Task|Minimum Permissions|  
|----------|-------------------------|  
|Prepare [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Instance.|`dbcreator` fixed-server role|  
|Create an Oracle CDC Service-SQL Server login for use by the Oracle CDC service.|`public` fixed-server role|  
|Create an Oracle CDC Service-login to use for registering the new service in MSXDBCDC.|`db_datareader` and `db_datawriter` roles on MSXDBCDC|  
|Edit an Oracle CDC Service-login to use for updating the registration of the service in MSXDBCDC.|`db_datareader` and `db_datawriter` roles on MSXDBCDC|  
|Delete an Oracle CDC Service-login to use for updating the registration of the service in MSXDBCDC.|`db_datareader` and `db_datawriter` roles on MSXDBCDC|  
  
## See Also  
 [Connection to SQL Server](../../integration-services/change-data-capture/connection-to-sql-server.md)   
 [Connection to SQL Server for Delete](../../integration-services/change-data-capture/connection-to-sql-server-for-delete.md)  
  
  
