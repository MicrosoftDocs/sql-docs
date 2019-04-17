---
title: "Length of the Product Cycle | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "interoperability [ODBC], product cycle"
  - "length of the product cycle [ODBC]"
ms.assetid: 4d08d886-6d8b-40fd-8544-13032f4bf6c7
author: MightyPen
ms.author: genemi
manager: craigg
---
# Length of the Product Cycle
The final question about interoperability is time. Developing an interoperable application usually takes longer than developing a noninteroperable one. The reason is that the application must check DBMS capabilities, perform the same tasks differently for different DBMSs, work around functionality supported by some DBMSs but not others, and so on.  
  
 In addition to development time, product lifetime must be considered. If the application is designed to be used once, such as an application that transfers data when migrating from one DBMS to another, there is no point in making it interoperable. The application will be used once and discarded.  
  
 If the application will exist for a long time, it might be easier to maintain as an interoperable application. This is true even for custom applications that have a single DBMS as a target. The reason is that interoperable code uses a limited subset of database features. The driver is required to keep those features available, even in the face of changes to the underlying DBMS. Thus, interoperable code can shift the burden of coping with changes to the DBMS from the application developer to the driver developer.
