---
title: "Guidelines and Limitations of SQLXML 4.0 | Microsoft Docs"
ms.custom: ""
ms.date: "03/03/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: xml
ms.topic: "reference"
helpviewer_keywords: 
  - "SQLXML, about SQLXML"
ms.assetid: fe433d30-90a1-421e-85c6-af13294dc18d
author: MightyPen
ms.author: genemi
manager: craigg
monikerRange: "=azuresqldb-current||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Guidelines and Limitations of SQLXML 4.0
[!INCLUDE[appliesto-ss-asdb-xxxx-xxx-md](../../includes/appliesto-ss-asdb-xxxx-xxx-md.md)]
  Remember the following when working with SQLXML 4.0:  
  
-   XML returned as a query result is not validated against the mapping schema that generated the XML.  
  
-   SQLXML 4.0 includes version-independent and version-dependent PROGIDs. It is recommended that all production applications use version-dependent PROGIDs. This is especially important because SQLXML 4.0 is not fully backward compatible. Using version dependent PROGIDs protects from possible production failures when you install newer releases. From release to release, program behavior may change for a variety of reasons, such as bug fixes, possible design changes, and so on. Using version-dependent PROGIDs protects from unexpected failure when you install newer releases. With version-dependent PROGIDs, when you install a newer release, your application will continue to work without failure. If you decide to change the previous version-dependent PROGIDs and use the recent version-dependent PROGIDs in a newer release, you must test your application before putting it into production. For example, applications using version-independent PROGIDs may fail in the following scenario:  
  
     You are running an application that uses SQLXML 4.0 and version-independent PROGIDs, and you decide to install some other software program. This program might install an earlier version of SQLXML. Your application may fail because the version-independent PROGIDS in your application now point to the earlier version of SQLXML, which may or may not have the SQLXML feature that your application is using.  
  
-   If for any reason you don't want to use the SQLXMLOLEDB provider, and instead want to use the SQLOLEDB provider for SQLXML features, set the **SQLXML Version** property to "SQLXML.4.0".  
  
  
