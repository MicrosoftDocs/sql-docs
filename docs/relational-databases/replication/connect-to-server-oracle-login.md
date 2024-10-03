---
title: "Connect to Server (Oracle), Login"
description: "Connect to Server (Oracle), Login"
author: "MashaMSFT"
ms.author: "mathoma"
ms.date: 09/25/2024
ms.service: sql
ms.subservice: replication
ms.topic: ui-reference
ms.custom:
  - updatefrequency5
f1_keywords:
  - "sql13.rep.oracleconnection.login.f1"
helpviewer_keywords:
  - "Connect to Server dialog box, replication"
---
# Connect to Server (Oracle), Login
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  Use the **Login** tab of the **Connect to Server** dialog box to specify the account under which connections are made from the [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Distributor to the Oracle Publisher. You must use the same account as the one specified for the replication administrative user schema during configuration of the Publisher. For more information, see [Configure an Oracle Publisher](../../relational-databases/replication/non-sql/configure-an-oracle-publisher.md).  
  
## Options  
 **Server instance**  
 The Transparent Network Substrate (TNS) name of the Oracle Publisher, which is specified during configuration of the Oracle client software installed on the Distributor.  
  
 **Authentication**  
 Select **Oracle Standard Authentication** (recommended) or **Windows Authentication**. If you select **Windows Authentication**:  
  
-   The Oracle server must be configured to allow connections using Windows credentials. For more information, see the Oracle documentation.  
  
-   You must be currently logged in with the same [!INCLUDE[msCoName](../../includes/msconame-md.md)] Windows account you specified for the replication administrative user schema.  
  
 **Login** and **Password**  
 If you selected **Oracle Standard Authentication** for the **Authentication** option, specify the login and password to use, which must be the same as those specified for the replication administrative user schema.  
  
## Related content

- [Glossary of Terms for Oracle Publishing](../../relational-databases/replication/non-sql/glossary-of-terms-for-oracle-publishing.md)
