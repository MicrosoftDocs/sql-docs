---
title: "Backward Compatibility in SMO | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: 

ms.topic: "reference"
ms.assetid: 2f986436-aaf2-4eaf-9809-df849d97d4fb
author: "stevestein"
ms.author: "sstein"
manager: craigg
monikerRange: "=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Backward Compatibility in SMO
[!INCLUDE[appliesto-ss-asdb-asdw-xxx-md](../../includes/appliesto-ss-asdb-asdw-xxx-md.md)]

  SMO applications that were written using previous versions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] can be recompiled by using SMO in [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].  
  
## Migrating SMO Applications  
 References to SMO dlls in older versions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] must be removed, and references to the new SMO dlls that are provided with [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] must be included.  
  
 Minimally, you would reference the following:  
  
-   Microsoft.SqlServer.ConnectionInfo  
  
-   Microsoft.SqlServer.Smo  
  
-   Microsoft.SqlServer.Management.Sdk.Sfc  
  
 These files are required for connection classes, SMO utility classes, and foundation classes.  
  
> [!NOTE]  
>  SmoEnum.dll has been removed so references to it must be removed from the SMO [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] project.  
  
 The namespaces have also changed, so you can use the following:  
  
##### For Visual C#  
  
```  
using Microsoft.SqlServer.Management.Smo;  
using Microsoft.SqlServer.Management.Common;  
```  
  
##### For Visual Basic  
  
```  
Imports Microsoft.SqlServer.Management.Smo  
Imports Microsoft.SqlServer.Management.Common  
```  
  
 If your code uses Urn functionality, such as **Server.GetSqlSmoObject(Urn)**, you must link to the Microsoft.SqlServer.Management.Sdk.Sfc namespace.  
  
 If your code uses the Transfer object directly, you will have to link to the Microsoft.SqlServer.Management.SmoExtended namespace.  
  
 When you migrate code, you might have to modify the code. This is because several [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] and [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] features have been deprecated in [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)]. For more information about deprecated features, see [Deprecated Database Engine Features in SQL Server 2016](../../database-engine/deprecated-database-engine-features-in-sql-server-2016.md) in [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] Books Online.  
  
  
