---
title: "Create a Server Role | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine, pdw"
ms.reviewer: ""
ms.technology: security
ms.topic: conceptual
f1_keywords: 
  - "SQL13.SWB.SERVERROLE.GENERAL.F1"
  - "sql13.swb.serverrole.memberships.f1"
  - "sql13.swb.serverrole.members.f1"
helpviewer_keywords: 
  - "SERVER ROLE, creating"
ms.assetid: 74f19992-8082-4ed7-92a1-04fe676ee82d
author: VanMSFT
ms.author: vanto
manager: craigg
monikerRange: ">=aps-pdw-2016||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Create a Server Role
[!INCLUDE[appliesto-ss-xxxx-xxxx-pdw-md](../../../includes/appliesto-ss-xxxx-xxxx-pdw-md.md)]
  This topic describes how to create a new server role in [!INCLUDE[ssCurrent](../../../includes/sscurrent-md.md)] by using [!INCLUDE[ssManStudioFull](../../../includes/ssmanstudiofull-md.md)] or [!INCLUDE[tsql](../../../includes/tsql-md.md)].  
  
 **In This Topic**  
  
-   **Before you begin:**  
  
     [Limitations and Restrictions](#Restrictions)  
  
     [Security](#Security)  
  
-   **To create a new server role, using:**  
  
     [SQL Server Management Studio](#SSMSProcedure)  
  
     [Transact-SQL](#TsqlProcedure)  
  
##  <a name="BeforeYouBegin"></a> Before You Begin  
  
###  <a name="Restrictions"></a> Limitations and Restrictions  
 Server roles cannot be granted permission on database-level securables. To create database roles, see [CREATE ROLE &#40;Transact-SQL&#41;](../../../t-sql/statements/create-role-transact-sql.md).  
  
###  <a name="Security"></a> Security  
  
####  <a name="Permissions"></a> Permissions  
  
-   Requires CREATE SERVER ROLE permission or membership in the sysadmin fixed server role.  
  
-   Also requires IMPERSONATE on the *server_principal* for logins, ALTER permission for server roles used as the *server_principal*, or membership in a Windows group that is used as the server_principal.  
  
-   When you use the AUTHORIZATION option to assign server role ownership, the following permissions are also required:  
  
    -   To assign ownership of a server role to another login, requires IMPERSONATE permission on that login.  
  
    -   To assign ownership of a server role to another server role, requires membership in the recipient server role or ALTER permission on that server role.  
  
##  <a name="SSMSProcedure"></a> Using SQL Server Management Studio  
  
#### To create a new server role  
  
1.  In Object Explorer, expand the server where you want to create the new server role.  
  
2.  Expand the **Security** folder.  
  
3.  Right-click the **Server Roles** folder and select **New Server Role...**.  
  
4.  In the **New Server Role -**_server\_role\_name_ dialog box, on the **General** page, enter a name for the new server role in the **Server role name** box.  
  
5.  In the **Owner** box, enter the name of the server principal that will own the new role. Alternately, click the ellipsis **(...)** to open the **Select Server Login or Role** dialog box.  
  
6.  Under **Securables**, select one or more server-level securables. When a securable is selected, this server role can be granted or denied permissions on that securable.  
  
7.  In the **Permissions: Explicit** box, select the check box to grant, grant with grant, or deny permission to this server role for the selected securables. If a permission cannot be granted or denied to all of the selected securables, the permission is represented as a partial select.  
  
8.  On the **Members** page, use the **Add** button to add logins that represent individuals or groups to the new server role.  
  
9. A user-defined server role can be a member of another server role. On the **Memberships** page, select a check box to make the current user-defined server role a member of a selected server role.  
  
10. [!INCLUDE[clickOK](../../../includes/clickok-md.md)]  
  
##  <a name="TsqlProcedure"></a> Using Transact-SQL  
  
#### To create a new server role  
  
1.  In **Object Explorer**, connect to an instance of [!INCLUDE[ssDE](../../../includes/ssde-md.md)].  
  
2.  On the Standard bar, click **New Query**.  
  
3.  Copy and paste the following example into the query window and click **Execute**.  
  
    ```  
    --Creates the server role auditors that is owned the securityadmin fixed server role.  
    USE master;  
    CREATE SERVER ROLE auditors AUTHORIZATION securityadmin;  
    GO  
    ```  
  
 For more information, see [CREATE SERVER ROLE &#40;Transact-SQL&#41;](../../../t-sql/statements/create-server-role-transact-sql.md).  
  
  
