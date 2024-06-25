---
title: Connect .NET with Microsoft Entra multifactor authentication
description: "C# Code example, with explanations, for connecting to Azure SQL Database by using SqlAuthenticationMethod.ActiveDirectoryInteractive mode."
author: nofield
ms.author: nofield
ms.reviewer: wiassaf, vanto, mathoma
ms.date: 09/27/2023
ms.service: sql-database
ms.subservice: security
ms.topic: conceptual
ms.custom:
  - active directory
  - has-adal-ref
  - sqldbrb=1
---
# Connect to Azure SQL Database with Microsoft Entra multifactor authentication
[!INCLUDE[appliesto-sqldb](../includes/appliesto-sqldb.md)]

This article provides a C# program that connects to Azure SQL Database. The program uses interactive mode authentication, which supports [multifactor authentication](/azure/active-directory/authentication/concept-mfa-howitworks) using Microsoft Entra ID ([formerly Azure Active Directory](/entra/fundamentals/new-name)).

For more information about multifactor authentication support for SQL tools, see [Using Microsoft Entra multifactor authentication](./authentication-mfa-ssms-overview.md).

[!INCLUDE [entra-id](../includes/entra-id.md)]

<a name='multi-factor-authentication-for-azure-sql-database'></a>

## Multifactor authentication for Azure SQL Database

`Active Directory Interactive` authentication supports multifactor authentication using [Microsoft.Data.SqlClient](/sql/connect/ado-net/introduction-microsoft-data-sqlclient-namespace) to connect to Azure SQL data sources. In a client C# program, the enum value directs the system to use the Microsoft Entra interactive mode that supports multifactor authentication to connect to Azure SQL Database. The user who runs the program sees the following dialog boxes:

* A dialog box that displays a Microsoft Entra user name and asks for the user's password.

   If the user's domain is federated with Microsoft Entra ID, the dialog box doesn't appear, because no password is needed.

   If the Microsoft Entra policy imposes multifactor authentication on the user, a dialog box to sign in to your account will display.

* The first time a user goes through multifactor authentication, the system displays a dialog box that asks for a mobile phone number to send text messages to. Each message provides the *verification code* that the user must enter in the next dialog box.

* A dialog box that asks for a multifactor authentication verification code, which the system has sent to a mobile phone.

For information about how to configure Microsoft Entra ID to require multifactor authentication, see [Getting started with Microsoft Entra multifactor authentication in the cloud](/azure/active-directory/authentication/howto-mfa-getstarted).

For screenshots of these dialog boxes, see [Using Microsoft Entra multifactor authentication](./authentication-mfa-ssms-overview.md).

> [!TIP]
> You can search .NET Framework APIs with the [.NET API Browser tool page](/dotnet/api/).
>
> You can also search directly with the [optional ?term=&lt;search value&gt; parameter](/dotnet/api/?term=SqlAuthenticationMethod).

## Prerequisite

Before you begin, you should have a [logical SQL server](logical-servers.md) created and available.

<a name='set-an-azure-ad-admin-for-your-server'></a>

### Set a Microsoft Entra admin for your server

For the C# example to run, a [logical server](logical-servers.md) admin needs to assign a Microsoft Entra admin from Microsoft Entra ID for your server.

In the Azure portal, on the **SQL server** page, select **Microsoft Entra ID** from the resource menu, then select **Set admin**.

For more information about Microsoft Entra admins and users for Azure SQL Database, see the screenshots in [Configure and manage Microsoft Entra authentication with SQL Database](authentication-aad-configure.md#provision-azure-ad-admin-sql-database).

## Microsoft.Data.SqlClient

The C# example relies on the [Microsoft.Data.SqlClient](/sql/connect/ado-net/introduction-microsoft-data-sqlclient-namespace) namespace. For more information, see [Using Microsoft Entra authentication with SqlClient](/sql/connect/ado-net/sql/azure-active-directory-authentication).

> [!NOTE]
> [System.Data.SqlClient](/dotnet/api/system.data.sqlclient) uses the Azure Active Directory Authentication Library (ADAL), which is deprecated. If you're using the [System.Data.SqlClient](/dotnet/api/system.data.sqlclient) namespace for Microsoft Entra authentication, migrate applications to [Microsoft.Data.SqlClient](/sql/connect/ado-net/introduction-microsoft-data-sqlclient-namespace) and the [Microsoft Authentication Library (MSAL)](/azure/active-directory/develop/msal-migration). For more information about using Microsoft Entra authentication with SqlClient, see [Using Microsoft Entra authentication with SqlClient](/sql/connect/ado-net/sql/azure-active-directory-authentication).

## Verify with SQL Server Management Studio

Before you run the C# example, it's a good idea to check that your setup and configurations are correct in [SQL Server Management Studio (SSMS)](/sql/ssms/download-sql-server-management-studio-ssms). Any C# program failure can then be narrowed to source code.

### Verify server-level firewall IP addresses

Run SSMS from the same computer, in the same building, where you plan to run the C# example. For this test, any **Authentication** mode is OK. If there's any indication that the server isn't accepting your IP address, see [server-level and database-level firewall rules](firewall-configure.md) for help.

<a name='verify-azure-active-directory-multi-factor-authentication'></a>

### Verify Microsoft Entra multifactor authentication

Run SSMS again, this time with **Authentication** set to **Azure Active Directory - Universal with MFA**. This option requires SSMS version 18.6 or later.

For more information, see [Using Microsoft Entra multifactor authentication](./authentication-mfa-ssms-overview.md).

> [!NOTE]
> For SSMS versions prior to 18.x, guest users must provide the Microsoft Entra domain name or tenant ID for the database: Select **Options** > **AD domain name or tenant ID**. SSMS 18.x and later automatically recognizes the tenant.
>
>To find the domain name in the Azure portal, select **Microsoft Entra ID** > **Custom domain names**. In the C# example program, providing a domain name is not necessary.

## C# code example

> [!NOTE]
> If you are using .NET Core, you will want to use the [Microsoft.Data.SqlClient](/dotnet/api/microsoft.data.sqlclient) namespace. For more information, see the following [blog](https://devblogs.microsoft.com/dotnet/introducing-the-new-microsoftdatasqlclient/).

This is an example of C# source code.

```csharp

using System;
using Microsoft.Data.SqlClient;

public class Program
{
    public static void Main(string[] args)
    {
        // Use your own server, database, and user ID.
        // Connetion string - user ID is not provided and is asked interactively.
        string ConnectionString = @"Server=<your server>.database.windows.net; Authentication=Active Directory Interactive; Database=<your database>";


        using (SqlConnection conn = new SqlConnection(ConnectionString))

        {
            conn.Open();
            Console.WriteLine("ConnectionString2 succeeded.");
            using (var cmd = new SqlCommand("SELECT @@Version", conn))
            {
                Console.WriteLine("select @@version");
                var result = cmd.ExecuteScalar();
                Console.WriteLine(result.ToString());
            }

        }
        Console.ReadKey();

    }
}

```

&nbsp;

This is an example of the C# test output.

```C#
ConnectionString2 succeeded.
select @@version
Microsoft SQL Azure (RTM) - 12.0.2000.8
   ...
```

## Next steps

- [Microsoft Entra server principals](authentication-azure-ad-logins.md)
- [Microsoft Entra-only authentication with Azure SQL](authentication-azure-ad-only-authentication.md)
- [Using Microsoft Entra multifactor authentication](./authentication-mfa-ssms-overview.md)
