---
title: Security
description: Learn about security in Master Data Services, including types of users, how to set security, security in the add-in for Excel, and related tasks.
author: meetdeepak
ms.author: dkhare
ms.reviewer: randolphwest, mikeray
ms.date: 03/05/2026
ms.service: sql
ms.subservice: master-data-services
ms.topic: concept-article
ms.custom:
  - build-2025
ai-usage: ai-assisted
---
# Security (Master Data Services)

[!INCLUDE [SQL Server - Windows only ASDBMI](../includes/applies-to-version/sql-windows-only-asdbmi.md)]

[!INCLUDE [support-notice](includes/support-notice.md)]

In [!INCLUDE [ssMDSshort](../includes/ssmdsshort-md.md)] (MDS), use security to ensure that users access only the specific master data necessary for their jobs, and prevent users from accessing data that shouldn't be available to them.

You can also use security settings to make someone an administrator of a specific model and functional area. For example, you can grant someone the ability to create versions of the Customer model or give them the ability to set security permissions.

[!INCLUDE [ssMDSshort](../includes/ssmdsshort-md.md)] security is based on local or Active Directory (AD) domain users and groups. MDS security allows you to use a granular level of detail when determining the data a user can access. Because of the granularity, security can easily become complicated. Use caution when assigning permissions to overlapping users and groups. For more information, see [Overlapping User and Group Permissions](overlapping-user-and-group-permissions-master-data-services.md).

You can assign security access in the **User and Group Permissions** functional area of the [!INCLUDE [ssMDSmdm](../includes/ssmdsmdm-md.md)] web application or by using the web service.

## Types of users

There are two types of users in [!INCLUDE [ssMDSshort](../includes/ssmdsshort-md.md)]:

- Users who access data in the **Explorer** functional area.

- Users who have the ability to perform administrative tasks in areas other than **Explorer**. These users are called [Administrators](administrators-master-data-services.md).

## How to set security

To give a user or group permission to access data or functionality in MDS, assign:

- [Functional Area Permissions](functional-area-permissions-master-data-services.md), which determine which of the five functional areas of the user interface a user can access.

- [Model Object Permissions](model-object-permissions-master-data-services.md), which determine the attributes a user can access, and the type of access (Read, Create, and Update) that the user has to these attributes. You can also assign Admin permissions at the Model level.

- Optionally, [hierarchy member permissions](hierarchy-member-permissions-master-data-services.md), which determine the members a user can access, and the type of access (Read, Update, and Delete) the user has to these members.

When you assign permissions to attributes and members, the permissions intersect and rules determine which permission takes precedence. For more information, see [How Permissions Are Determined](how-permissions-are-determined-master-data-services.md).

## Security in the Add-in for Excel

Security set in the [!INCLUDE [ssMDSmdm](../includes/ssmdsmdm-md.md)] web application also applies to the [!INCLUDE [ssMDSXLS](../includes/ssmdsxls-md.md)]. Users can only view and work with data they have permission to access. Administrators can perform administrative tasks.

The only caveat is that all security assigned in [!INCLUDE [ssMDSmdm](../includes/ssmdsmdm-md.md)] doesn't take effect in Excel until a 20-minute interval passes. The `MdsMaximumUserInformationCacheInterval` setting defines this interval in the `web.config` file. To change the interval, change the setting and restart Internet Information Services (IIS).

## Security risks in Master Data Services

This section outlines potential security risks related to Master Data Services (MDS). Certain legacy tools associated with retired features could introduce vulnerabilities, and the product itself can contain inherent security risks. The following information is provided so you can take appropriate measures.

| Title | Description | Recommendation |
| --- | --- | --- |
| **Authorization model** | MDS uses a custom authorization model where access control is enforced at the application level. If an attacker can manipulate the internal user list or exploit a flaw in the authorization logic, they can gain full access to master data. | To reduce the impact of user-list manipulation or authorization flaws, summarize the risks in the custom authorization model and outline hardening measures such as network isolation, strict least-privilege service accounts, and comprehensive auditing. |
| **Adoption of legacy technology** | MDS is removed from [!INCLUDE [sssql25-md](../includes/sssql25-md.md)], and earlier versions only receive security updates, increasing the risk of vulnerabilities and operational issues over time. A key example is MDS's reliance on ActiveX, which requires Internet Explorer, which is officially retired and no longer supported. | Plan for migration to supported platforms before end-of-life, and avoid new deployments of MDS. For existing installations, minimize exposure by restricting access to legacy components (such as ActiveX and Internet Explorer), use modern browsers where possible, and implement compensating controls such as network isolation and enhanced monitoring. Evaluate alternative solutions for master data management (MDM), such as Microsoft Purview or partner MDM platforms, and develop a phased migration strategy to ensure business continuity and security. |
| **Data tampering and integrity attacks** | A bad actor with access to Master Data Services can modify master data, leading to downstream corruption in enterprise resource planning (ERP), customer relationship management (CRM), or reporting systems. | To mitigate the risk of data tampering and its potential effect, here are a few feasible suggestions: implementing transaction logging and versioning, integrity checks with reconciliation processes, role-based access controls with change approval workflows, and robust backup and recovery procedures. |
| **Data encryption and filtering** | MDS might store sensitive data (for example, customer records, employee details). If access control is bypassed, this data can be exfiltrated. | MDS compatibility with [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] encryption options is limited. For example, Always Encrypted might break MDS rules and hierarchies, while transparent data encryption (TDE) only protects data at rest. For better security, enable TDE, apply dynamic data masking where possible, and configure [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] Auditing to monitor access. Avoid storing highly sensitive data unless these protections are in place. For advanced governance, consider migrating platforms with built-in protection such as Microsoft Purview with Partner MDM solutions. |
| **Data ingestion** | MDS supports data ingestion via various means, including staging tables. For more information, see [Overview: Importing Data from Tables](overview-importing-data-from-tables-master-data-services.md). In this case, some security issues can occur. | When using staging tables for data import in Master Data Services, enforce strict controls to prevent security issues. Prefer pull-based ingestion for centralized governance, require unique service identities with least privilege, and validate schema and business rules before committing data. Ensure full auditability by logging all ingestion events, and isolate contributors by using separate staging schemas or tables to avoid cross-contamination. |

## Related tasks

| Task Description | Article |
| --- | --- |
| Create a user who has full permission to a model. | [Create a Model Administrator](create-a-model-administrator-master-data-services.md) |
| Add an Active Directory group to [!INCLUDE [ssMDSshort](../includes/ssmdsshort-md.md)]. This step is the first in giving a group permission to access data in the [!INCLUDE [ssMDSshort](../includes/ssmdsshort-md.md)] web application. | [Add a Group](add-a-group-master-data-services.md) |
| Assign permission to a functional area of the [!INCLUDE [ssMDSshort](../includes/ssmdsshort-md.md)] web application. | [Assign Functional Area Permissions](assign-functional-area-permissions-master-data-services.md) |
| Assign permission to attribute values by assigning permission to model objects. | [Assign Model Object Permissions](assign-model-object-permissions-master-data-services.md) |
| Assign permission to member values by assigning permission to hierarchy nodes. | [Assign Hierarchy Member Permissions](assign-hierarchy-member-permissions-master-data-services.md) |

## Related content

- [Administrators (Master Data Services)](administrators-master-data-services.md)
- [Users and Groups (Master Data Services)](users-and-groups-master-data-services.md)
- [Functional Area Permissions (Master Data Services)](functional-area-permissions-master-data-services.md)
- [Model Object Permissions (Master Data Services)](model-object-permissions-master-data-services.md)
- [Hierarchy Member Permissions (Master Data Services)](hierarchy-member-permissions-master-data-services.md)
- [How Permissions Are Determined (Master Data Services)](how-permissions-are-determined-master-data-services.md)
