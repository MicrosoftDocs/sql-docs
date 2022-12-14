---
description: "Determining the Target DBMSs and Drivers"
title: "Determining the Target DBMSs and Drivers | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "target DBMSs and drivers in interoperability [ODBC]"
  - "interoperability [ODBC], target dbmss and drivers"
ms.assetid: 23bee0f6-e12a-4598-b34e-df11a8086829
author: David-Engel
ms.author: v-davidengel
---
# Determining the Target DBMSs and Drivers
The next question to consider is, what are the target DBMSs for the application, and what drivers are available that support those DBMSs? Because generic applications tend to be highly interoperable, the question of target DBMSs is most applicable to custom and vertical applications. However, the question of target drivers applies to all applications, because drivers vary widely in speed, quality, feature support, and availability. Also, if drivers are to be redistributed with the application, the cost and availability of licensing plans need to be considered.  
  
 For many custom applications, the target DBMSs are obvious: They are existing DBMSs that the application is designed to access. DBMSs to which future migration is planned should also be considered. However, the major question for these applications is which driver or drivers to use with them. For other custom applications - those which are not designed to access an existing DBMS - the target DBMSs can be chosen based on feature support, concurrent user support, driver availability, and affordability.  
  
 For vertical applications, the target DBMSs are usually chosen based on feature support, driver availability, and market. For example, a vertical application designed for small businesses must target DBMSs that are affordable to those businesses; a vertical application designed as an add-on to existing DBMSs must target widely used DBMSs.  
  
 When choosing target DBMSs, the differences between desktop and server databases should be considered. Desktop databases such as dBASE, Paradox, and Btrieve are less powerful than server databases. Because they are generally accessed through the less powerful SQL engines found in most file-based drivers, they often lack full transaction support, support fewer concurrent users, and have limited SQL. However, they are inexpensive and have a large installed base.  
  
 Server databases such as Oracle, DB2, and SQL Server provide full transaction support, support many concurrent users, and have rich SQL. They are much more expensive and have a smaller installed base. On the other hand, software prices tend to be higher, somewhat offsetting a smaller potential market.  
  
 Thus, target DBMSs sometimes can be chosen based on the features required by the application and the application's target market. For example, an order entry system for large corporations might not target desktop databases because these lack adequate transaction support. A similar system designed for small businesses might exclude most server databases on the basis of cost. And developers of generic applications might target both but avoid using the advanced features found in server databases.
