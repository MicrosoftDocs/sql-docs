---
title: "MSSQLSERVER_916 | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: supportability
ms.topic: conceptual
helpviewer_keywords: 
  - "916 (Database Engine error)"
ms.assetid: 73eb6581-99fe-49a5-9b42-e239d7ffe27f
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# MSSQLSERVER_916
    
## Details  
  
|||  
|-|-|  
|Product Name|SQL Server|  
|Event ID|916|  
|Event Source|MSSQLSERVER|  
|Component|SQLEngine|  
|Symbolic Name|NOTUSER|  
|Message Text|The server principal "%.*ls" is not able to access the database "%.\*ls" under the current security context.|  
  
## Explanation  
 The login does not have sufficient permissions to connect to the named database. Logins that can connect to this instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] but that do not have specific permissions in a database receive the permissions of the guest user. This is a security measure to prevent users in one database from connecting to other databases where they do not have privileges. This error message can occur when the guest user does not have CONNECT permission to the named database and the trustworthy property is not set. This error message can occur when the guest user does not have CONNECT permission to the named database.  
  
 When CONNECT permission to the msdb database is denied or revoked, [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] can receive this error when Object Explorer tries to show the Policy Based Management status of each database. Object Explorer uses the permissions of the current login to query the msdb database for this information, which causes the error. The following error message also occurs:  
  
 Failed to retrieve data for this request. (Microsoft.SqlServer.Management.Sdk.Sfc)  
  
## User Action  
  
> [!WARNING]  
>  Before circumventing this security measure be sure to have a clear understanding of users are authenticated in various databases. The following methods may allow users that have permissions in one database to connect to other databases which could expose data to a malicious user. When contained databases are enabled, the following steps can allow database owners in one database to grant access to other database on the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
 You can connect to the database in one of the following ways:  
  
-   Grant the specific login access to the named database. The following example grants the login `Adventure-Works\Larry` access to the `msdb` database.  
  
     USE msdb ;  
  
     GO  
  
     GRANT CONNECT TO [Adventure-Works\Larry] ;  
  
-   Grant the CONNECT permission to the database named in the error message for the guest user. The following example grants the `CONNECT` permission to the `msdb` database for the user `guest`.  
  
     USE msdb ;  
  
     GO  
  
     GRANT CONNECT TO guest ;  
  
-   Enable the TRUSTWORTHY property on the database that has authenticated the user.  
  
     `ALTER DATABASE AdventureWorks SET TRUSTWORTHY ON;`  
  
  
