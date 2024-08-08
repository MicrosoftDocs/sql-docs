---
title: Microsoft Entra logins and users with nonunique display names (preview)
titleSuffix: Azure SQL Database and Azure SQL Managed Instance
description: Learn how to mitigate naming conflicts for Microsoft Entra logins and users with nonunique display names in Azure SQL Database and Azure SQL Managed Instance by using the T-SQL Object_ID syntax, currently in preview. 
author: tameikal-msft
ms.author: talawren
ms.reviewer: vanto, nofield, mathoma
ms.date: 02/21/2024
ms.service: azure-sql
ms.subservice: security
ms.topic: conceptual
monikerRange: "= azuresql || = azuresql-db || = azuresql-mi"
---

# Microsoft Entra logins and users with nonunique display names (preview)
[!INCLUDE[appliesto-sqldb-sqlmi](../includes/appliesto-sqldb-sqlmi.md)]

This article teaches you how to use the T-SQL Object_ID syntax to create Microsoft Entra logins and users with nonunique display names in Azure SQL Database and Azure SQL Managed Instance. 

> [!NOTE]
> Using `WITH OBJECT_ID` to create users and logins in Azure SQL is currently in preview. 

## Overview

Microsoft Entra ID supports authentication for service principals. However, using a service principal with a display name that is not unique in Microsoft Entra ID leads to errors when creating the login or user in Azure SQL. 


For example, if the application `myapp` isn't unique, you might run into the following error: 

```output
Msg 33131, Level 16, State 1, Line 4 
Principal 'myapp' has a duplicate display name. Make the display name unique in Azure Active Directory and execute this statement again. 
```

When trying to run the following T-SQL statement:

```sql
CREATE LOGIN [myapp] FROM EXTERNAL PROVIDER 
```


## The `WITH OBJECT_ID` extension

The *duplicate display name* error occurs because Microsoft Entra ID allows duplicate display names for [Microsoft Entra application (service principal)](authentication-aad-service-principal.md), while Azure SQL requires unique names to create Microsoft Entra logins and users. To mitigate this problem, the Data Definition Language (DDL) statement to create logins and users has been extended to include the Object ID of the Azure resource with the `WITH OBJECT_ID` clause.

> [!NOTE]
> The `WITH OBJECT_ID` extension is currently in **public preview**.
>
> Most nonunique display names in Microsoft Entra ID are related to service principals, though occasionally group names can also be nonunique. Microsoft Entra user principal names are unique, as two users can't have the same user principal. However, an app registration (service principal) can be created with a display name that is the same as a user principal name.
>
> If the service principal display name is not a duplicate, the default `CREATE LOGIN` or `CREATE USER` statement should be used. The `WITH OBJECT_ID` extension is in **public preview**, and is a troubleshooting repair item implemented for use with nonunique service principals. Using it with a unique service principal is not recommended. Using the `WITH OBJECT_ID` extension for a service principal without adding a suffix will run successfully, but it will not be obvious which service principal the login or user was created for. It's recommended to create an alias using a suffix to uniquely identify the service principal. The `WITH OBJECT_ID` extension is not supported for SQL Server.

## T-SQL create login/user syntax for nonunique display names

```sql
CREATE LOGIN [login_name] FROM EXTERNAL PROVIDER 
  WITH OBJECT_ID = 'objectid'
```

```sql
CREATE USER [user_name] FROM EXTERNAL PROVIDER 
  WITH OBJECT_ID = 'objectid'
```

With the T-SQL DDL supportability extension to create logins or users with the Object ID, you can avoid error *33131* and also specify an alias for the login or user created with the Object ID. For example, the following will create a login `myapp4466e` using the application Object ID `4466e2f8-0fea-4c61-a470-xxxxxxxxxxxx`.

```sql
CREATE LOGIN [myapp4466e] FROM EXTERNAL PROVIDER 
  WITH OBJECT_ID = '4466e2f8-0fea-4c61-a470-xxxxxxxxxxxx' 
```

- To execute the above query, the specified Object ID must exist in the Microsoft Entra tenant where the Azure SQL resource resides. Otherwise, the `CREATE` command will fail with the error message: `Msg 37545, Level 16, State 1, Line 1 '' is not a valid object id for '' or you do not have permission.`
- The login or user name must contain the original service principal name extended by a user-defined suffix when using the `CREATE LOGIN` or `CREATE USER` statement. As a best practice, the suffix can include an initial part of its Object ID. For example, `myapp2ba6c` for the Object ID `2ba6c0a3-cda4-4878-a5ca-xxxxxxxxxxxx`. However, you can also define a custom suffix. Forming the suffix from the Object ID is not required.

This naming convention is recommended to explicitly associate the database user or login back to its object in Microsoft Entra ID.

> [!NOTE]
> The alias adheres to the T-SQL specification for `sysname`, including a max length of 128 characters. We recommend limiting the suffix to the first 5 characters of the Object ID.
>
> The display name of the service principal in Microsoft Entra ID is not synchronized to the database login or user alias. Running `CREATE LOGIN` or `CREATE USER` will not affect the display name in the Azure Portal. Similarly, modifying the Microsoft Entra ID display name will not affect the database login or user alias.

## Identify the user created for the application

For nonunique service principals, it's important to verify the Microsoft Entra alias is tied to the correct application. To check that the user was created for the correct service principal (application):

1. Get the **Application ID** of the application, or **Object ID** of the Microsoft Entra group from the user created in SQL Database. See the following queries:

   - To get the **Application ID** of the service principal from the user created, execute the following query:

     ```sql
     SELECT CAST(sid as uniqueidentifier) ApplicationID, create_date FROM sys.server_principals WHERE NAME = 'myapp2ba6c' 
     ```

     **Example output:**

     :::image type="content" source="media/authentication-microsoft-entra-create-users-with-nonunique-names/application-id-output.png" alt-text="Screenshot of SSMS output for the Application ID.":::

     The Application ID is converted from the security identification number (SID) for the specified login or user name, which we can confirm by executing the below query and comparing the last several digits and create dates:

     ```sql
     SELECT SID, create_date FROM sys.server_principals WHERE NAME = 'myapp2ba6c' 
     ```

     **Example output:**

     :::image type="content" source="media/authentication-microsoft-entra-create-users-with-nonunique-names/security-id-output.png" alt-text="Screenshot of SSMS output for the SID of the application.":::

   - To get the **Object ID** of the Microsoft Entra group from the user created, execute the following query:

     ```sql
     SELECT CAST(sid as uniqueidentifier) ObjectID, createdate FROM sys.sysusers WHERE NAME = 'myappgroupd3451b' 
     ```

     **Example output:**

     :::image type="content" source="media/authentication-microsoft-entra-create-users-with-nonunique-names/object-id-output.png" alt-text="Screenshot of SSMS output for the Object ID of the Microsoft Entra group.":::

     To check the SID of the Microsoft Entra group from the user created, execute the following query:

     ```sql
     SELECT SID, createdate FROM sys.sysusers WHERE NAME = 'myappgroupd3451b' 
     ```

     **Example output:**

     :::image type="content" source="media/authentication-microsoft-entra-create-users-with-nonunique-names/security-id-group-output.png" alt-text="Screenshot of SSMS output for the SID of the group.":::

   - To get the Object ID and Application ID of the application using PowerShell execute the following command: 

     ```powershell
     Get-AzADApplication -DisplayName "myapp2ba6c"
     ```

1. Go to the [Azure portal](https://portal.azure.com), and in your **Enterprise application** or Microsoft Entra group resource, check the **Application ID** or **Object ID** respectively. See if it matches the one obtained from the above query.

> [!NOTE]
> When creating a user from a service principal, the **Object ID** is required when using the `WITH OBJECT_ID` clause with the `CREATE` T-SQL statement. This is different from the **Application ID** that is returned when you are trying to verify the alias in Azure SQL. Using this verification process, you can identify the service principal or group associated with the SQL alias in Microsoft Entra ID, and prevent possible mistakes when creating logins or users with an Object ID.

### Finding the right Object ID

For information on the Object ID of a service principal, see [Service principal object](/azure/active-directory/develop/app-objects-and-service-principals#service-principal-object). You can locate the Object ID of the service principal listed next to the application name in the Azure portal under **Enterprise applications**.

> [!WARNING]
> The Object ID obtained in the **App registration** Overview page is different from the Object ID obtained in the **Enterprise applications** Overview page. If you're in the **App registration** Overview page, select the linked **Managed application in local directory** application name to navigate to the right Object ID on the **Enterprise applications** Overview page.

## Related content

- [Microsoft Entra server principals](authentication-azure-ad-logins.md)
- [CREATE LOGIN (Transact-SQL)](/sql/t-sql/statements/create-login-transact-sql?view=azuresqldb-current&preserve-view=true)
- [CREATE USER (Transact-SQL)](/sql/t-sql/statements/create-user-transact-sql)
