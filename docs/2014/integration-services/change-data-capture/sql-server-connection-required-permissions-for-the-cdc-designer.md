---
title: "SQL Server Connection Required Permissions for the CDC Designer | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
ms.assetid: 80334de2-17c1-43c9-951e-21a9f864e9cb
author: douglaslMS
ms.author: douglasl
manager: craigg
---
# SQL Server Connection Required Permissions for the CDC Designer
  The CDC Designer Console requires connection information to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] to perform its tasks. This topic describes the information that can be provided in the **Connect to SQL Server** dialog box for setting up the connection to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
 The **Connect to SQL Server** dialog box opens when necessary, such as when the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] connection information is not available or when the information exists but the connection does not have the required permissions.  
  
 The following table describes the various tasks where a connection to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is required and the required permissions from the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login/user.  
  
|Task|Minimum Permissions|  
|----------|-------------------------|  
|View the list of CDC Services and Instances using the associated [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance.|`db_datareader` role on MSXDBCDC|  
|Start/Stop CDC Instance(s)|`db_datareader` and `db_datawriter` roles on MSXDBCDC|  
|View the CDC Instance status.|`db_owner` role on the CDC Instance database|  
|Create a new Oracle CDC Instance database.|`dbcreator` fixed-server role|  
|Create a new Oracle CDC Instance.|`db_datareader` role on MSXDBCDC<br /><br /> `db_owner` role on the CDC database that was created.|  
|Get deployment scripts.|`db_datareader` and `db_datawriter` roles on MSXDBCDC<br /><br /> `db_owner` role on the relatedCDC database|  
|Change configuration and add/remove capture instances.|`db_datareader` and `db_datawriter` roles on MSXDBCDC<br /><br /> `db_owner` role on the related CDC database|  
  
## See Also  
 [Access the CDC Designer Console](access-the-cdc-designer-console.md)   
 [SQL Server Connection for Instance Creation](sql-server-connection-for-instance-creation.md)  
  
  
