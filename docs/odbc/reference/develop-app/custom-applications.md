---
title: "Custom Applications | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "interoperability [ODBC], custom applications"
  - "custom applications [ODBC]"
  - "interoperability [ODBC], levels"
ms.assetid: f28178d9-ecd6-4e8c-9644-9bb624999dcb
author: MightyPen
ms.author: genemi
manager: craigg
---
# Custom Applications
Custom applications typically perform a specific task for a few DBMSs. For example, an application might retrieve data from a single DBMS and generate a report, or it might transfer data among several DBMSs. What these applications have in common is that these DBMSs are known before the application is written and are unlikely to change over the life of the application.  
  
 The custom application therefore requires little or no interoperability. The application developer can choose a single driver for each DBMS and code directly to those drivers. The application can safely contain driver-specific code to exploit the capabilities of those drivers and might even make calls to the native database API to use functionality not supported by ODBC.  
  
 The major interoperability concern of most custom applications is whether the target DBMSs will change in the future. If so, this process can be simplified by writing more interoperable code to start with. However, such changing of DBMSs is rare and generally entails a large amount of work. Because of this, developers of custom applications rarely choose to increase interoperability at the expense of functionality; they usually choose to recode that functionality when they change DBMSs.
