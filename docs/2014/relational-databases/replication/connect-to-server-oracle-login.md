---
title: "Connect to Server (Oracle), Login | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: replication
ms.topic: conceptual
f1_keywords: 
  - "sql12.rep.oracleconnection.login.f1"
helpviewer_keywords: 
  - "Connect to Server dialog box, replication"
ms.assetid: 86ed91a1-a07c-46f2-a913-67317ef2255e
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# Connect to Server (Oracle), Login
  Use the **Login** tab of the **Connect to Server** dialog box to specify the account under which connections are made from the [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Distributor to the Oracle Publisher. You must use the same account as the one specified for the replication administrative user schema during configuration of the Publisher. For more information, see [Configure an Oracle Publisher](non-sql/configure-an-oracle-publisher.md).  
  
## Options  
 **Server instance**  
 The Transparent Network Substrate (TNS) name of the Oracle Publisher, which is specified during configuration of the Oracle client software installed on the Distributor.  
  
 **Authentication**  
 Select **Oracle Standard Authentication** (recommended) or **Windows Authentication**. If you select **Windows Authentication**:  
  
-   The Oracle server must be configured to allow connections using Windows credentials. For more information, see the Oracle documentation.  
  
-   You must be currently logged in with the same [!INCLUDE[msCoName](../../includes/msconame-md.md)] Windows account you specified for the replication administrative user schema.  
  
 **Login** and **Password**  
 If you selected **Oracle Standard Authentication** for the **Authentication** option, specify the login and password to use, which must be the same as those specified for the replication administrative user schema.  
  
## See Also  
 [Glossary of Terms for Oracle Publishing](non-sql/glossary-of-terms-for-oracle-publishing.md)  
  
  
