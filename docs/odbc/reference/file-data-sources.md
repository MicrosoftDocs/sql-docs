---
title: "File Data Sources | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "data sources [ODBC], file"
  - "file data sources [ODBC]"
ms.assetid: db245c80-981a-4638-bd03-69d04bc67af0
author: MightyPen
ms.author: genemi
manager: craigg
---
# File Data Sources
*File data sources* are stored in a file and allow connection information to be used repeatedly by a single user or shared among several users. When a file data source is used, the Driver Manager makes the connection to the data source using the information in a .dsn file. This file can be manipulated like any other file. A file data source does not have a data source name, as does a machine data source, and is not registered to any one user or machine.  
  
 A file data source streamlines the connection process, because the .dsn file contains the connection string that would otherwise have to be built for a call to the **SQLDriverConnect** function. Another advantage of the .dsn file is that it can be copied to any machine, so identical data sources can be used by many machines as long as they have the appropriate driver installed. A file data source can also be shared by applications. A shareable file data source can be placed on a network and used simultaneously by multiple applications.  
  
 A .dsn file can also be unshareable. An unshareable .dsn file resides on a single machine and points to a machine data source. Unshareable file data sources exist mainly to allow the easy conversion of machine data sources to file data sources so that an application can be designed to work solely with file data sources. When the Driver Manager is sent the information in an unshareable file data source, it connects as necessary to the machine data source that the .dsn file points to.  
  
 For more information about file data sources, see [Connecting Using File Data Sources](../../odbc/reference/develop-app/connecting-using-file-data-sources.md), or the [SQLDriverConnect](../../odbc/reference/syntax/sqldriverconnect-function.md) function description.
