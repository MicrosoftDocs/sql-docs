---
title: "Case Study: Building an Enterprise Ecosystem with Microsoft Dynamics ERP and SQL Server 2014 Replication for Scalability and Performance | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.topic: conceptual
ms.assetid: 2b0b5ab7-4e08-431a-bd59-360177c4565c
author: mashamsft
ms.author: mathoma
manager: craigg
---
# Case Study: Building an Enterprise Ecosystem with Microsoft Dynamics ERP and SQL Server 2014 Replication for Scalability and Performance

  **Summary:** This paper covers the following scenarios:  
How to use transactional replication in SQL Server 2014 to distribute the transactions from Dynamics AX clients across multiple nodes. Because data is maintained across the nodes in real-time, transactional replication provides data redundancy, which increases the availability of data, includes data available for more efficient performance analysis.  
How to understand the specifics involved while leveraging transactional replication to build highly scalable Enterprise ecosystems in Microsoft Dynamics ERP. Deliver high performance and scalability without customizing the AX out-of-box features.  
  
 Transactional replication is typically used in server-to-server workflows that require high throughput. These include scenarios, such as improving scalability and availability, data warehousing and reporting, integrating data from multiple sites, integrating heterogeneous data, and offloading batch processing. This white paper describes a distinct scenario and associated patterns where transactional replication is leveraged in Microsoft Dynamics ERP. It also covers the challenges and best practices when considering transactional replication to build enterprise solutions specific to Enterprise Resource Planning (ERP), as well as the performance analysis at different stages.  
  
 This content is suitable for developers, architects, and database administrators. It is assumed that readers of this white paper have basic knowledge of SQL Server 2008, 2012, or 2014 as well as SQL Server administration experience.  
  
 **Writer:**Prabhakaran Sethuraman (PRAB), Microsoft  
  
 **Technical Reviewers:**Prabhakaran Sethuraman (PRAB), Microsoft; Santosh Padhy, Microsoft; Pavel Majstrov, Microsoft; Karthik Sankaranarayanan, Microsoft; Jon Acone, Microsoft; David Stahlkopf, Microsoft;Kent Oldenburger, Microsoft; Mandi Ohlinger, Microsoft; Jason Roth, Microsoft  
  
 **Published:**October 2015  
  
 **Applies to:**SQL Server 2008, SQL Server 2012, and SQL Server 2014  
  
 To review the document, please download the  
        [Case Study: Building an Enterprise Ecosystem with Microsoft Dynamics ERP and SQL Server 2014 Replication for Scalability and Performance](https://download.microsoft.com/download/D/2/0/D20E1C5F-72EA-4505-9F26-FEF9550EFD44/A%20Case%20Study%20Using%20Replication%20to%20Build%20an%20Enterprise%20Ecosystem%20in%20Microsoft%20Dynamics%20ERP%20for%20Scalability%20and%20Performance.docx) Word document.  
  
  
