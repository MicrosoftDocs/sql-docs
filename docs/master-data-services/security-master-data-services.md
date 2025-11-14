---
title: Security
description: Learn about security in Master Data Services, including types of users, how to set security, security in the add-in for Excel, and related tasks.
author: CordeliaGrey
ms.author: jiwang6
ms.date: "03/01/2017"
ms.service: sql
ms.subservice: master-data-services
ms.topic: conceptual
ms.custom:
  - build-2025
---
# Security (Master Data Services)

[!INCLUDE [SQL Server - Windows only ASDBMI](../includes/applies-to-version/sql-windows-only-asdbmi.md)]

[!INCLUDE [support-notice](includes/support-notice.md)]

  In [!INCLUDE[ssMDSshort](../includes/ssmdsshort-md.md)], use security to ensure that users have access to the specific master data necessary to do their jobs, and to prevent them from accessing data that should not be available to them.  
  
 You can also use security to make someone an administrator of a specific model and functional area (for example, to allow someone to create versions of the Customer model or to give someone the ability to set security permissions).  
  
 [!INCLUDE[ssMDSshort](../includes/ssmdsshort-md.md)] security is based on local or Active Directory domain users and groups. MDS security allows you to use a granular level of detail when determining the data a user can access. Because of the granularity, security can easily become complicated and you should use caution when using overlapping users and groups. For more information, see [Overlapping User and Group Permissions &#40;Master Data Services&#41;](../master-data-services/overlapping-user-and-group-permissions-master-data-services.md).  
  
 You can assign security access in the **User and Group Permissions** functional area of the [!INCLUDE[ssMDSmdm](../includes/ssmdsmdm-md.md)] web application or by using the web service.  
  
## Types of Users  
 There are two types of users in [!INCLUDE[ssMDSshort](../includes/ssmdsshort-md.md)]:  
  
-   Those who access data in the **Explorer** functional area.  
  
-   Those who have the ability to perform administrative tasks in areas other than **Explorer**. These users are called [Administrators &#40;Master Data Services&#41;](../master-data-services/administrators-master-data-services.md).  
  
## How to Set Security  
 To give a user or group permission to access data or functionality in MDS, you must assign:  
  
-   [Functional area access](../master-data-services/functional-area-permissions-master-data-services.md), which determines which of the five functional areas of the user interface a user can access.  
  
-   [Model object permissions](../master-data-services/model-object-permissions-master-data-services.md), which determine the attributes a user can access, and the type of access (Read, Create, and Update) that the user has to those attributes. The user can also assign Admin permissions at the Model level.  
  
-   Optionally, [hierarchy member permissions](../master-data-services/hierarchy-member-permissions-master-data-services.md), which determine the members a user can access, and the type of access (Read, Update, and Delete) the user has to those members.  
  
 When you assign permissions to attributes and members, the permissions intersect and rules determine which permission takes precedence. For more information, see [How Permissions Are Determined &#40;Master Data Services&#41;](../master-data-services/how-permissions-are-determined-master-data-services.md).  
  
## Security in the Add-in for Excel  
 Security set in the [!INCLUDE[ssMDSmdm](../includes/ssmdsmdm-md.md)] web application is also applied to the [!INCLUDE[ssMDSXLS](../includes/ssmdsxls-md.md)]. Users are only able to view and work with data they have permission to. Administrators can perform administrative tasks.  
  
 The only caveat is that all security assigned in [!INCLUDE[ssMDSmdm](../includes/ssmdsmdm-md.md)] does not take effect in Excel until a 20 minute interval passes. The interval is defined by the *MdsMaximumUserInformationCacheInterval* setting in the web.config file. To change the interval, you can change the setting and restart IIS.  

## Security Risk Notice
 This section outlines potential security risks related to our product. Certain legacy tools associated with retired features may introduce vulnerabilities, and the product itself may contain inherent security risks. The following information is provided to ensure customers are aware and can take appropriate measures.
|Title|Description|Recommand|  
|----------------------|-----------|-----------|  
|Authorization Model|MDS uses a custom authorization model where access control is enforced at the application level. If an attacker can manipulate the internal user list or exploit a flaw in the authorization logic, they can gain full access to master data.|Summarize existing architectural risks associated with the custom authorization model and provide concrete hardening recommendations for installations—such as network isolation, strict service-account restrictions and least-privilege enforcement, and comprehensive auditing and monitoring—to mitigate risks from manipulation of internal user lists or flaws in authorization logic.|
|Adoption of Legacy Technology|MDS will be removed from SQL 2025, and earlier versions will only receive security updates, increasing the risk of vulnerabilities and operational issues over time.  A key example is MDS’s reliance on ActiveX, which requires Internet Explorer, which has been officially retired and is no longer supported.|Summarize existing architectural risks associated with the custom authorization model and provide concrete hardening recommendations for installations—such as network isolation, strict service-account restrictions and least-privilege enforcement, and comprehensive auditing and monitoring—to mitigate risks from manipulation of internal user lists or flaws in authorization logic.|
|Data Tampering and Integrity Attacks|An attacker with access to MDS can modify master data, leading to downstream corruption in ERP, CRM, or reporting systems.|To mitigate the risk of data tampering and its potential impact, here are a few feasible suggestions: implementing transaction logging and versioning, integrity checks with reconciliation processes, role-based access controls with change approval workflows, and robust backup and recovery procedures.|
|Data Encryption and Filtering|MDS may store sensitive data (e.g., customer records, employee details).  If access control is bypassed, this data can be exfiltrated.|MDS has limited compatibility with SQL Server encryption options—Always Encrypted may break MDS rules and hierarchies, while TDE only protects data at rest. For better security, enable TDE, apply Dynamic Data Masking where possible, and configure SQL Server Auditing to monitor access. Avoid storing highly sensitive data unless these protections are in place. For advanced governance, consider migrating platforms with built-in protection such as Microsoft Purview with Partner MDM solutions.|
|Data Ingestion|MDS supports data ingestion via various means, including staging tables. There is a documentation about how to use this feature (see [Importing Data from Tables - SQL Server Master Data Services &#124; Microsoft Learn](https://learn.microsoft.com/en-us/sql/master-data-services/overview-importing-data-from-tables-master-data-services?view=sql-server-ver16)), but in this case, some security issues may occur.|When using staging tables for data import in Master Data Services, enforce strict controls to prevent security issues. Prefer pull-based ingestion for centralized governance, require unique service identities with least privilege, and validate schema and business rules before committing data. Ensure full auditability by logging all ingestion events, and isolate contributors by using separate staging schemas or tables to avoid cross-contamination.|
  
## Related Tasks  
  
|Task Description|Topic|  
|----------------------|-----------|  
|Create a user who has full permission to a model.|[Create a Model Administrator &#40;Master Data Services&#41;](../master-data-services/create-a-model-administrator-master-data-services.md)|  
|Add an Active Directory group to [!INCLUDE[ssMDSshort](../includes/ssmdsshort-md.md)]; this is the first step in giving a group permission to access data in the [!INCLUDE[ssMDSshort](../includes/ssmdsshort-md.md)] web application.|[Add a Group &#40;Master Data Services&#41;](../master-data-services/add-a-group-master-data-services.md)|  
|Assign permission to a functional area of the [!INCLUDE[ssMDSshort](../includes/ssmdsshort-md.md)] web application.|[Assign Functional Area Permissions &#40;Master Data Services&#41;](../master-data-services/assign-functional-area-permissions-master-data-services.md)|  
|Assign permission to attribute values by assigning permission to model objects.|[Assign Model Object Permissions &#40;Master Data Services&#41;](../master-data-services/assign-model-object-permissions-master-data-services.md)|  
|Assign permission to member values by assigning permission to hierarchy nodes.|[Assign Hierarchy Member Permissions &#40;Master Data Services&#41;](../master-data-services/assign-hierarchy-member-permissions-master-data-services.md)|  
  
## See Also  
 [Administrators &#40;Master Data Services&#41;](../master-data-services/administrators-master-data-services.md)   
 [Users and Groups &#40;Master Data Services&#41;](../master-data-services/users-and-groups-master-data-services.md)   
 [Functional Area Permissions &#40;Master Data Services&#41;](../master-data-services/functional-area-permissions-master-data-services.md)   
 [Model Object Permissions &#40;Master Data Services&#41;](../master-data-services/model-object-permissions-master-data-services.md)   
 [Hierarchy Member Permissions &#40;Master Data Services&#41;](../master-data-services/hierarchy-member-permissions-master-data-services.md)   
 [How Permissions Are Determined &#40;Master Data Services&#41;](../master-data-services/how-permissions-are-determined-master-data-services.md)  
  
  
