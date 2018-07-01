---
title: "Authorizing access to objects and operations (Analysis Services) | Microsoft Docs"
ms.date: 05/02/2018
ms.prod: sql
ms.technology: analysis-services
ms.custom: multidimensional-models
ms.topic: conceptual
ms.author: owend
ms.reviewer: owend
author: minewiskan
manager: kfile
---
# Authorizing access to objects and operations (Analysis Services)
[!INCLUDE[ssas-appliesto-sqlas](../../includes/ssas-appliesto-sqlas.md)]
  Non-administrative user access to cubes, dimensions, and mining models within an [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] database is granted through membership in one or more database roles. [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] administrators create these database roles, granting Read or Read/Write permissions on [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] objects, and then assigning [!INCLUDE[msCoName](../../includes/msconame-md.md)] Windows users and groups to each role.  
  
 [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] determines the effective permissions for a specific Windows user or group by combining the permissions that are associated with each database role to which the user or group belongs. As a result, if one database role does not give a user or group permission to view a dimension, measure, or attribute, but a different database role does give that user or group permission, the user or group will have permission to view the object.  
  
> [!IMPORTANT]  
>  Members of the [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] Server Administrator role and members of a database role having Full Control (Administrator) permissions can access all data and metadata in the database and need no additional permissions to view specific objects. Moreover, members of the [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] server role cannot be denied access to any object in any database, and members of an [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] database role that has Full Control (Administrator) permissions within a database cannot be denied access to any object within that database. Specialized administrative operations, such as processing, can be authorized through separate roles having less permission. See [Grant process permissions &#40;Analysis Services&#41;](../../analysis-services/multidimensional-models/grant-process-permissions-analysis-services.md) for details.  
  
## List roles defined for your database  
 Administrators can run a simple DMV query in SQL Server Management Studio to get a list of all roles defined on the server.  
  
1.  In SSMS, right-click a database and select **New Query** | **MDX**.  
  
2.  Type the following query and press F5 to execute:  
  
    ```  
    Select * from $SYSTEM.DBSCHEMA_CATALOGS  
    ```  
  
     Results include the database name, description, role name, and the date last modified. Using this information as a starting point, you can proceed to individual databases to check membership and permissions of a specific role.  
  
## Top-down overview of Analysis Services authorization  
 This section covers the basic workflow for configuring permissions.  
  
 **Step 1: Server Administration**  
  
 As a first step, decide who will have administrator rights at the server level. During installation, the local administrator who installs SQL Server is required to specify one or more Windows accounts as the Analysis Services server administrator. Server administrators have all possible permissions on a server, including the permission to view, modify, and delete any object on the server, or view associated data. After installation is complete, a server administrator can add or remove accounts to change membership of this role. See [Grant server admin rights to an  Analysis Services instance](../../analysis-services/instances/grant-server-admin-rights-to-an-analysis-services-instance.md) for details about this permission level.  
  
 **Step 2: Database Administration**  
  
 Next, after a tabular or multidimensional solution is created, it is deployed to the server as a database. A server administrator can delegate database administration tasks by defining a role that has Full Control permissions for the database in question. Members of this role can process or query objects in the database, as well as create additional roles for accessing cubes, dimensions, and other objects within the database itself. See [Grant database permissions &#40;Analysis Services&#41;](../../analysis-services/multidimensional-models/grant-database-permissions-analysis-services.md) for more information.  
  
 **Step 3: Enable cube or model access for query and processing workloads**  
  
 By default, only server and database administrators have access to cubes or tabular models. Making these data structures available to other people in your organization requires additional role assignments that map Windows user and group accounts to cubes or models, along with permissions that specify **Read** privileges. See [Grant cube or model permissions &#40;Analysis Services&#41;](../../analysis-services/multidimensional-models/grant-cube-or-model-permissions-analysis-services.md) for details.  
  
 Processing tasks can be isolated from other administrative functions, allowing server and database administrators to delegate this task to other people, or to configure unattended processing by specifying service accounts that run scheduling software. See [Grant process permissions &#40;Analysis Services&#41;](../../analysis-services/multidimensional-models/grant-process-permissions-analysis-services.md) for details.  
  
> [!NOTE]  
>  Users do not require any permissions to the relational tables in the underlying relational database from which [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] loads its data, and do not require any file level permissions on the computer on which the instance of [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] is running.  
  
 **Step 4 (Optional): Allow or deny access to interior cube objects**  
  
 [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] provides security settings for setting permissions on individual objects, including dimension members and cells within a data model. For details, see [Grant custom access to dimension data &#40;Analysis Services&#41;](../../analysis-services/multidimensional-models/grant-custom-access-to-dimension-data-analysis-services.md) and [Grant custom access to cell data &#40;Analysis Services&#41;](../../analysis-services/multidimensional-models/grant-custom-access-to-cell-data-analysis-services.md).  
  
 You can also vary permissions based on user identity. This is often referred to as dynamic security, and is implemented using the [UserName &#40;MDX&#41;](../../mdx/username-mdx.md) function  
  
## Best practices  
 To better manage permissions, we suggest an approach similar to the following:  
  
1.  Create roles by function (for example, dbadmin, cubedeveloper, processadmin) so that whoever maintains the roles can see at glance what the role allows. As noted elsewhere, you can define roles in the model definition, thus preserving those roles over subsequent solution deployments.  
  
2.  Create a corresponding Windows security group in Active Directory, and then maintain the security group in Active Directory to insure it contains the appropriate individual accounts. This places the responsibility of security group membership on security specialists who are already have proficiency with the tools and processes used for account maintenance in your organization.  
  
3.  Generate scripts in [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] so that you can quickly replicate role assignments whenever the model is redeployed from its source files to a server. See [Grant cube or model permissions &#40;Analysis Services&#41;](../../analysis-services/multidimensional-models/grant-cube-or-model-permissions-analysis-services.md) for details on how to quickly generate a script.  
  
4.  Adopt a naming convention that reflects the scope and membership of the role. Role names are only visible in design and administration tools, so use a naming convention that makes sense to your cube security specialists. For example, **processadmin-windowsgroup1** indicates read access, plus processing rights, to people in your organization whose individual Windows user accounts are members of the **windowsgroup1** security group.  
  
     Including account information can help you keep track of which accounts are used in various roles. Because roles are additive, the combined roles associated with **windowsgroup1** make up the effective permission set for people belonging to that security group.  
  
5.  Cube developers will require Full Control permissions for models and databases under development, but only need Read permissions once a database is rolled out to a production server. Remember to develop role definitions and assignments for all scenarios, including development, test, and production deployments.  
  
 Using an approach like this one minimizes churn to role definitions and role membership in the model, and provides visibility into role assignments that makes cube permissions easier to implement and maintain.  
  
## See Also  
 [Grant server admin rights to an  Analysis Services instance](../../analysis-services/instances/grant-server-admin-rights-to-an-analysis-services-instance.md)   
 [Roles and Permissions &#40;Analysis Services&#41;](../../analysis-services/multidimensional-models/roles-and-permissions-analysis-services.md)   
 [Authentication methodologies supported by Analysis Services](../../analysis-services/instances/authentication-methodologies-supported-by-analysis-services.md)  
  
  
