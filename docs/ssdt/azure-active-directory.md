---
title: "Azure Active Directory support in SQL Server Data Tools (SSDT) | Microsoft Docs"
ms.custom: ""
ms.date: "01/25/2018"
ms.prod: "sql-non-specified"
ms.prod_service: "sql-tools"
ms.service: ""
ms.component: "ssdt"
ms.reviewer: ""
ms.suite: "sql"
ms.technology: 
  - "tools-ssdt"
ms.tgt_pltfrm: ""
ms.topic: "article"
author: "stevestein"
ms.author: "sstein"
manager: "craigg"
ms.workload: "Active"
---
# Azure Active Directory support in SQL Server Data Tools (SSDT)

[!INCLUDE[appliesto-xx-asdb-xxxx-xxx-md.md](../includes/appliesto-xx-asdb-xxxx-xxx-md.md)]

SQL Server Data Tools provides a new authentication method for connecting to an Azure SQL database - **Active Directory Interactive Authentication**.

> [!NOTE]
> Active Directory Interactive Authentication is available in SSDT version 15.5.2 and later, and requires .NET 4.7.2 to be installed on the computer running SSDT. If .NET 4.7.2 is not installed, the Active Directory Interactive Authentication option will not be available.

   ![SSDT connection dialog](media/azure-active-directory/interactive.png)

**Active Directory Interactive Authentication** supports an interactive authentication allowing to use Azure Active Directory (AD) Multi-Factor Authentication (MFA) to authenticate with Azure SQL Database. This method supports native and federated Azure AD users and guest users from other accounts (including B2B users, Microsft and non-Microsoft accounts such as @outlook.com, @hotmail.com, @live.com as well as @gmail.com). If this method is specified, the **User Name** must be specified, and the Password field will be disabled. 

When authenticating with *Active Directory Interactive Authentication*, an authentication window opens that requires users to enter a password manually. The MFA enforcement is provided by Azure AD through an additional MFA pop-up window during the authentication process.

> [!NOTE]
> Because *Active Directory Interactive Authentication* requires users to manually (interactively) enter their password, it is not recommended for automated workflows.


## Known issues and limitations

- *Active Directory Interactive Authentication* is only supported when connecting to an Azure SQL database. It is not supported for SQL Server (on-prem or on a VM), or Azure SQL Data Warehouse.
- *Active Directory Interactive Authentication* is not supported in the connection dialog in *Server Explorer*, you must connect using SSDT with *SQL Server Object Explorer*.
- Single sign-on integration with the currently logged in Visual Studio account is not supported for SSDT.
- The SQLPackage.exe installed into the Extensions directory during Visual Studio installation is not meant to be used from that location. To use SQLpackage.exe with AAD go to https://www.microsoft.com/en-us/download/details.aspx?id=55088 
- SSDT Data Compare is not supported for AAD authentication including the new authentication method.  





## See Also  
[Multi-factor authentication](https://docs.microsoft.com/azure/sql-database/sql-database-ssms-mfa-authentication)
[Azure Active Directory authentication with SQL Database](https://docs.microsoft.com/azure/sql-database/sql-database-aad-authentication-configure)
[SQL Server Data Tools in Visual Studio](https://msdn.microsoft.com/library/hh272686(v=vs.103).aspx)  
[SSDT MSDN Forum](https://social.msdn.microsoft.com/Forums/sqlserver/home?forum=ssdt)  
[SSDT Team Blog](http://blogs.msdn.com/b/ssdt/)  
[DACFx API Reference](https://msdn.microsoft.com/library/dn645454.aspx)  
[Download SQL Server Management Studio (SSMS)](../ssms/download-sql-server-management-studio-ssms.md)  
