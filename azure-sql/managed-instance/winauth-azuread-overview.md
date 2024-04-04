---
title: What is Windows Authentication for Microsoft Entra principals on Azure SQL Managed Instance?
titleSuffix: Azure SQL Managed Instance
description: Learn about Windows Authentication for Microsoft Entra principals on Azure SQL Managed Instance.
author: sravanisaluru
ms.author: srsaluru
ms.reviewer: mathoma, bonova, urmilano, wiassaf
ms.date: 09/27/2023
ms.service: sql-managed-instance
ms.subservice: deployment-configuration
ms.topic: overview
---

# What is Windows Authentication for Microsoft Entra principals on Azure SQL Managed Instance? 
[!INCLUDE[appliesto-sqlmi](../includes/appliesto-sqlmi.md)]

[Azure SQL Managed Instance](sql-managed-instance-paas-overview.md) is the intelligent, scalable cloud database service that combines the broadest SQL Server database engine compatibility with the benefits of a fully managed and evergreen platform as a service. Kerberos authentication for Microsoft Entra ID ([formerly Azure Active Directory](/entra/fundamentals/new-name)) enables Windows Authentication access to Azure SQL Managed Instance. Windows Authentication for managed instances empowers customers to move existing services to the cloud while maintaining a seamless user experience and provides the basis for infrastructure modernization.

[!INCLUDE [entra-id](../includes/entra-id.md)]

## Key capabilities and scenarios

As customers modernize their infrastructure, application, and data tiers, they also modernize their identity management capabilities by shifting to Microsoft Entra ID. Azure SQL offers multiple [Microsoft Entra authentication](../database/authentication-aad-overview.md) options:

[!INCLUDE [entra-authentication-options](../includes/entra-authentication-options.md)]

However, some legacy apps can't change their authentication to Microsoft Entra ID: legacy application code may longer be available, there may be a dependency on legacy drivers, clients may not be able to be changed, and so on. Windows Authentication for Microsoft Entra principals removes this migration blocker and provides support for a broader range of customer applications.

Windows Authentication for Microsoft Entra principals on managed instances is available for devices or virtual machines (VMs) joined to Active Directory, Microsoft Entra ID, or hybrid Microsoft Entra ID - a hybrid Microsoft Entra user identity exists both in Microsoft Entra ID and Active Directory and can access a managed instance in Azure using Microsoft Entra Kerberos.

Enabling Windows Authentication for a managed instance doesn't require customers to deploy new on-premises infrastructure or manage the overhead of setting up Domain Services.

Windows Authentication for Microsoft Entra principals on Azure SQL Managed Instance enables two key scenarios: migrating on-premises SQL Servers to Azure with minimal changes and modernizing security infrastructure.

### Lift and shift on-premises SQL Servers to Azure with minimal changes

By enabling Windows Authentication for Microsoft Entra principals, customers can migrate to Azure SQL Managed Instance without implementing changes to application authentication stacks or deploying Microsoft Entra Domain Services. Customers can also use Windows Authentication to access a managed instance from their Active Directory or Microsoft Entra-joined devices.

Windows Authentication for Microsoft Entra principals also enables the following patterns on managed instances. These patterns are frequently used in traditional on-premises SQL Servers:


- **"Double hop" authentication**: Web applications use IIS identity impersonation to run queries against an instance in the security context of the end user.
- **Traces using extended events and SQL Server Profiler** can be launched using Windows authentication, providing ease of use for database administrators and developers accustomed to this workflow. Learn how to [run a trace against Azure SQL Managed Instance using Windows Authentication for Microsoft Entra principals](winauth-azuread-run-trace-managed-instance.md).

### Modernize security infrastructure

Enabling Windows Authentication for Microsoft Entra principals on Azure SQL Managed Instance equips customers to modernize their security practices.

For example, a customer can enable a mobile analyst, using proven tools that rely on Windows Authentication, to authenticate to a managed instance using biometric credentials. This can be accomplished even if the mobile analyst works from a laptop that is joined to Microsoft Entra ID.

## Next steps

Learn more about implementing Windows Authentication for Microsoft Entra principals on Azure SQL Managed Instance:

- [How Windows Authentication for Azure SQL Managed Instance is implemented with Microsoft Entra ID and Kerberos](winauth-implementation-aad-kerberos.md)
- [How to set up Windows Authentication for Azure SQL Managed Instance using Microsoft Entra ID and Kerberos](winauth-azuread-setup.md)
