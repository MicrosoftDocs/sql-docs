---
title: "Create an Application Role | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: security
ms.topic: conceptual
f1_keywords: 
  - "sql13.swb.approle.general.f1"
helpviewer_keywords: 
  - "application roles [SQL Server], creating"
ms.assetid: 6b8da1f5-3d8e-4f88-b111-b915788b06f1
author: VanMSFT
ms.author: vanto
manager: craigg
monikerRange: "=azuresqldb-current||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Create an Application Role
[!INCLUDE[appliesto-ss-asdb-xxxx-xxx-md](../../../includes/appliesto-ss-asdb-xxxx-xxx-md.md)]
  This topic describes how to create an application role in [!INCLUDE[ssCurrent](../../../includes/sscurrent-md.md)] by using [!INCLUDE[ssManStudioFull](../../../includes/ssmanstudiofull-md.md)] or [!INCLUDE[tsql](../../../includes/tsql-md.md)]. Application roles restrict user access to a database except through specific applications. Application roles have no users, so the **Role Members** list is not displayed when **Application role** is selected.  
  
> [!IMPORTANT]  
>  Password complexity is checked when application role passwords are set. Applications that invoke application roles must store their passwords. Application role passwords should always be stored encrypted.  
  
 **In This Topic**  
  
-   **Before you begin:**  
  
     [Security](#Security)  
  
-   **To create an application role, using:**  
  
     [SQL Server Management Studio](#SSMSProcedure)  
  
     [Transact-SQL](#TsqlProcedure)  
  
##  <a name="BeforeYouBegin"></a> Before You Begin  
  
###  <a name="Security"></a> Security  
  
####  <a name="Permissions"></a> Permissions  
 Requires ALTER ANY APPLICATION ROLE permission on the database.  
  
##  <a name="SSMSProcedure"></a> Using SQL Server Management Studio  
  
##### To create an application role  
  
1.  In Object Explorer, expand the database where you want to create an application role.  
  
2.  Expand the **Security** folder.  
  
3.  Expand the **Roles** folder.  
  
4.  Right-click the **Application Roles** folder and select **New Application Role...**.  
  
5.  In the **Application Role - New** dialog box, on the **General Page**, enter the new name of the new application role in the **Role name** box.  
  
6.  In the **Default Schema** box, specify the schema that will own objects created by this role by entering the object names. Alternately, click the ellipsis **(...)** to open the **Locate Schema** dialog box.  
  
7.  In the **Password** box, enter a password for the new role. Enter that password again into the **Confirm Password** box.  
  
8.  Under **Schemas owned by this role**, select or view schemas that will be owned by this role. A schema can be owned by only one schema or role.  
  
9. [!INCLUDE[clickOK](../../../includes/clickok-md.md)]  
  
### Additional Options  
 The **Application Role - New** dialog box also offers options on two additional pages: **Securables** and **Extended Properties**.  
  
-   The **Securables** page lists all possible securables and the permissions on those securables that can be granted to the login.  
  
-   The **Extended properties** page allows you to add custom properties to database users.  
  
##  <a name="TsqlProcedure"></a> Using Transact-SQL  
  
#### To create an application role  
  
1.  In **Object Explorer**, connect to an instance of [!INCLUDE[ssDE](../../../includes/ssde-md.md)].  
  
2.  On the Standard bar, click **New Query**.  
  
3.  Copy and paste the following example into the query window and click **Execute**.  
  
    ```  
    -- Creates an application role called "weekly_receipts" that has the password "987Gbv876sPYY5m23" and "Sales" as its default schema.  
  
    CREATE APPLICATION ROLE weekly_receipts   
        WITH PASSWORD = '987G^bv876sPY)Y5m23'   
        , DEFAULT_SCHEMA = Sales;  
    GO  
    ```  
  
 For more information, see [CREATE APPLICATION ROLE &#40;Transact-SQL&#41;](../../../t-sql/statements/create-application-role-transact-sql.md).  
  
  
