---
description: "Backward Compatibility in SMO"
title: "Backward Compatibility in SMO | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: 

ms.topic: "reference"
ms.assetid: 2f986436-aaf2-4eaf-9809-df849d97d4fb
author: "markingmyname"
ms.author: "maghan"
monikerRange: "=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Backward Compatibility in SMO
[!INCLUDE [SQL Server ASDB, ASDBMI, ASDW ](../../includes/applies-to-version/sql-asdb-asdbmi-asa.md)]

  SMO applications that were written using previous versions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] can be recompiled by using SMO in newer versions.  
  
## Migrating SMO Applications  
 References to SMO DLLs in older versions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] must be removed, and references to the new SMO DLLs that are provided with newer versions of [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] must be included.  
  
 Minimally, you would reference the following:  
  
-   Microsoft.SqlServer.ConnectionInfo  
  
-   Microsoft.SqlServer.Smo  
  
-   Microsoft.SqlServer.Management.Sdk.Sfc  
  
 These files are required for connection classes, SMO utility classes, and foundation classes.  
  
> [!NOTE]  
>  SmoEnum.dll has been removed so references to it must be removed from the SMO project.  
  
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
  
 When you migrate code, you might have to modify the code. This is because several [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] and [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] features have been deprecated in newer versions of [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)]. For more information about deprecated features, see [Deprecated Database Engine Features in SQL Server 2016](../../database-engine/deprecated-database-engine-features-in-sql-server-2016.md).
