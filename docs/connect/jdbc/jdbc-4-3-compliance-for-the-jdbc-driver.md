---
title: "JDBC 4.3 Compliance for the JDBC Driver | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2018"
ms.prod: "sql"
ms.prod_service: "drivers"
ms.service: ""
ms.component: "jdbc"
ms.reviewer: ""
ms.suite: "sql"
ms.technology: 
  - "drivers"
ms.tgt_pltfrm: ""
ms.topic: "article"
ms.assetid: 36025ec0-3c72-4e68-8083-58b38e42d03b
caps.latest.revision: 9
author: "MightyPen"
ms.author: "genemi"
manager: craigg
---
# JDBC 4.3 Compliance for the JDBC Driver
[!INCLUDE[Driver_JDBC_Download](../../includes/driver_jdbc_download.md)]

    
> [!NOTE]  
>  Versions prior to Microsoft JDBC Driver 6.4 for SQL Server are compliant for Java Database Connectivity API 4.2 specifications. This section does not apply for versions prior to the 6.4 release.  
  
 Currently,  Microsoft JDBC Driver 6.4 for SQL Server is Java 9 compatible, but it's not fully compliant for  Java Database Connectivity API 4.3 specifications. For newly introduced APIs in JDBC 4.3, if not supported by the driver, the driver will throw a SQLFeatureNotSupportedException.