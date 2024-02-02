---
title: Microsoft Entra ID in SSDT
description: "Learn about the Microsoft Entra authentication methods that SQL Server Data Tools (SSDT) provides for Azure SQL Database and Azure Synapse Analytics."
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 12/21/2023
ms.service: sql
ms.subservice: ssdt
ms.topic: conceptual
monikerRange: "=azuresqldb-current || =azure-sqldw-latest"
---
# Microsoft Entra ID support in SQL Server Data Tools (SSDT)

[!INCLUDE [appliesto-xx-asdb-asdb-xxx-md.md](../includes/appliesto-xx-asdb-asdw-xxx-md.md)]

SQL Server Data Tools (SSDT) provides several authentication methods with Microsoft Entra ID ([formerly Azure Active Directory](/entra/fundamentals/new-name)).

[!INCLUDE [entra-id](../includes/entra-id-hard-coded.md)]

In Visual Studio, open the **SQL Server Object Explorer** (in the **View** menu), and select **Add SQL Server**:

:::image type="content" source="media/azure-active-directory/interactive.png" alt-text="Screenshot of SSDT connection dialog.":::

#### Which Azure SQL products?

This article discusses Microsoft Entra ID for the following list of *SQL* products in the [Azure cloud](https://azure.microsoft.com/):

- [Azure SQL Database](/azure/azure-sql/database/sql-database-paas-overview)
- [Azure SQL Managed Instance](/azure/azure-sql/managed-instance/sql-managed-instance-paas-overview)
- [SQL Server on Azure VMs](/azure/azure-sql/virtual-machines/windows/sql-server-on-azure-vm-iaas-what-is-overview)
- [SQL Server enabled by Azure Arc](../sql-server/azure-arc/overview.md)
- [Azure Synapse Analytics](/azure/synapse-analytics/overview-what-is)

## Microsoft Entra password authentication

*Active Directory Password Authentication* is a mechanism of connecting to the Azure SQL products that were listed previously. The mechanism uses identities in Microsoft Entra ID. Use this method for connecting when:

- You're logged in to Windows with credentials from a domain that isn't federated with Azure, or
- You're using Microsoft Entra authentication with Microsoft Entra ID, based on the initial or client domain.

For more information, see [Connecting to SQL Database By Using Microsoft Entra authentication](/azure/sql-database/sql-database-aad-authentication).

## Microsoft Entra integrated authentication

*Active Directory Integrated Authentication* is a mechanism of connecting to the listed SQL products in Azure by using identities in Microsoft Entra ID. Use this method to connect if you're logged in to Windows using your Microsoft Entra credentials from a [federated](/entra/identity/hybrid/connect/whatis-fed) domain. For more information, see [Connect to Azure SQL Database with Microsoft Entra authentication](/azure/azure-sql/database/authentication-aad-overview).

## Active Directory Interactive Authentication

*Active Directory Interactive Authentication* is available when connecting to the listed Azure SQL products with SSDT, but only with [.NET Framework 4.7.2](/dotnet/api/?view=netframework-4.7.2&preserve-view=true) or a later version.

- [Download and install for .NET Framework, any version](https://dotnet.microsoft.com/download/dotnet-framework).
- [Visual Studio 2017 version 15.6](/visualstudio/releasenotes/vs2017-relnotes), or a later version.

#### Multifactor authentication

Active Directory Interactive Authentication supports an interactive authentication allowing you to use Microsoft Entra multifactor authentication to authenticate with the listed Azure SQL products. This method supports native and [federated](/entra/identity/hybrid/connect/whatis-fed) Microsoft Entra users, and guest users from other accounts. The other types of account include:

- Business-to-Business (Microsoft Entra B2B) users.
- Microsoft accounts, from providers such as Outlook and Windows Live Mail.
- Non-Microsoft accounts, such as Gmail.

If the MFA method is specified, the **User Name** must be specified, and the **Password** field is disabled.

#### Password entry

When you authenticate with *Active Directory Interactive Authentication*, an authentication window opens that requires users to enter a password manually. MFA enforcement is provided by Microsoft Entra ID through this additional MFA pop-up window.

> [!NOTE]  
> Automated workflows would be blocked by the use of *Active Directory Interactive Authentication*. There must be a person available to interact with the authentication process, in the form of manually entering a password.

## Known issues and limitations

- *Active Directory Interactive Authentication* is only supported when connecting to the SQL products that were listed at the start of this article.
- Single sign-on integration with the currently logged in Visual Studio account isn't supported for SSDT.
- The SQLPackage.exe that is installed into the Extensions directory during Visual Studio installation isn't meant to be used from that location. To use SQLPackage.exe with Microsoft Entra ID, go to [Data-Tier Application Framework](https://www.microsoft.com/download/details.aspx?id=55088)

## Related content

- [Multifactor authentication](/azure/sql-database/sql-database-ssms-mfa-authentication)
- [Microsoft Entra authentication with Azure SQL Database](/azure/azure-sql/database/authentication-aad-configure)
- [SSDT MSDN Forum](https://social.msdn.microsoft.com/Forums/home?forum=ssdt)
- [SSDT Team Blog](/archive/blogs/ssdt/)
- [Download SQL Server Management Studio (SSMS)](../ssms/download-sql-server-management-studio-ssms.md)
