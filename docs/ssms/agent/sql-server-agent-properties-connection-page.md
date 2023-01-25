---
description: "SQL Server Agent Properties (Connection Page)"
title: "SQL Server Agent Properties (Connection Page)"
ms.custom: seo-lt-2019
ms.date: 01/19/2017
ms.service: sql
ms.subservice: ssms
ms.topic: conceptual
f1_keywords: 
  - "sql13.ag.agent.connection.f1"
ms.assetid: d6a677ff-60ad-47ba-a0cb-df4193b165e0
author: markingmyname
ms.author: maghan
ms.reviewer: ""
monikerRange: "= azuresqldb-mi-current || >= sql-server-2016"
---
# SQL Server Agent Properties (Connection Page)
[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]

> [!IMPORTANT]  
> On [Azure SQL Managed Instance](/azure/sql-database/sql-database-managed-instance), most, but not all SQL Server Agent features are currently supported. See [Azure SQL Managed Instance T-SQL differences from SQL Server](/azure/sql-database/sql-database-managed-instance-transact-sql-information#sql-server-agent) for details.

Use this page to view and modify the settings for the connection from the [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent service to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
## Options  
**Alias local host server**  
Specifies the alias to use to connect to the local instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. If you cannot use the default connection options for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent, define an alias for the instance and specify the alias here.  
  
**Use Windows Authentication**  
Sets [!INCLUDE[msCoName](../../includes/msconame-md.md)] Windows Authentication as the authentication method used to connect to the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent connects as the account that the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent service runs as.  
  
**Use SQL Server Authentication**  
Sets [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Authentication as the authentication method used to connect to the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance.  
  
> [!IMPORTANT]  
> [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Authentication is provided for backward compatibility. For improved security, use Windows Authentication if possible.  
  
**Login**  
Specifies the login name to use to connect to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
**Password**  
Specifies the password to use to connect to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
