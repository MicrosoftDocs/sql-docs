---
title: "MSSQLSERVER_21879 | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: supportability
ms.topic: conceptual
helpviewer_keywords: 
  - "21879 (Database Engine error)"
ms.assetid: fcfab735-05ca-423a-89f1-fdee7e2ed8c0
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# MSSQLSERVER_21879
    
## Details  
  
|||  
|-|-|  
|Product Name|SQL Server|  
|Event ID|21879|  
|Event Source|MSSQLSERVER|  
|Component|SQLEngine|  
|Symbolic Name|SQLErrorNum21879|  
|Message Text|Unable to query the redirected server '%s' for original publisher '%s' and publisher database '%s' to determine the name of the remote server; Error %d, Error message '%s'.|  
  
## Explanation  
 `sp_validate_redirected_publisher` uses a temporary linked server that it creates to connect to the redirected publisher in order to discover the name of the remote server. Error 21879 is returned when the linked server query fails. The call to request the remote server name is typically the first use of the temporary linked server, so if there are connectivity problems they are likely to appear first with this call. This remote call simply executes select `@@servername` at the remote server.  
  
 The linked server used to query the redirected publisher uses the security mode, login, and password supplied when `sp_adddistpublisher` was called for the original publisher.  
  
-   If [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] authentication was used (security mode 0) then the login and password specified are used to connect to the remote server.  
  
-   If Windows authentication was used (security mode 1) a trusted connection is used for the connection.  
  
    -   If `sp_validate_redirected_publisher` is called explicitly by a user, the Windows login that the user is running under is used for the connection.  
  
    -   If `sp_validate_redirected_`publisher is called by a replication agent from `sp_get_redirected_publisher`, the Windows login associated with the agent is used.  
  
 Error 21879 can indicate that `sp_validate_redirected_publisher` was called using a login that is not known at the redirected target publisher.  
  
## User Action  
 Make certain that the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] authentication login or the Windows authentication login is valid at all of the availability group replicas and has sufficient authorization to access the subscription metadata tables (syssubscriptions and sysmergesubscriptions) in the publisher database.  
  
 There are special considerations when error 21879 is returned from a call to `sp_get_redirected_publisher` that is initiated by a replication agent running on a node other that the distributor; such as a merge agent running at a subscriber. If Windows authentication is used for the connection to the redirected publisher, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] must be configured for Kerberos authentication for the connection to be successfully established. When Windows authentication is used and [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is not configured for Kerberos authentication, error 18456 indicating that the 'NT AUTHORITY\ANONYMOUS LOGON' login failed, is received by a merge agent running at a subscriber. There are three possible ways to address this issue:  
  
-   Configure [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] for Kerberos authentication. See **Kerberos Authentication and SQL Server** in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Books Online.  
  
-   Use `sp_changedistpublisher` to change the security mode associated with the original publisher in MSdistpublishers, as well as to specify a login and password to use for the connection.  
  
-   Specify the command line parameter *BypassPublisherValidation* on the merge agent command line to bypass validation when `sp_get_redirected_publisher` is called at the distributor.  
  
  
