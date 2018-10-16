---
title: "Grant database permissions (Analysis Services) | Microsoft Docs"
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
# Grant database permissions (Analysis Services)
[!INCLUDE[ssas-appliesto-sqlas](../../includes/ssas-appliesto-sqlas.md)]
  If you are approaching Analysis Services database administration with a background in relational databases, the first thing you need to understand is that, in terms of data access, the database is not the primary securable object in Analysis Services.  
  
 The primary query structure in Analysis Services is a cube (or a tabular model), with user permissions set on those particular objects. Contrasted with the relational database engine ─ where database logins and user permissions (often **db_datareader**) are set on the database itself ─ an Analysis Services database is mostly a container for the main query objects in a data model. If your immediate objective is to enable data access for a cube or tabular model, you can bypass database permissions for now and go straight to this topic: [Grant cube or model permissions &#40;Analysis Services&#41;](../../analysis-services/multidimensional-models/grant-cube-or-model-permissions-analysis-services.md).  
  
 Database permissions in Analysis Services enable administrative functions; broadly, as is the case with the Full Control database permission, or of a more granular nature if you are delegating processing operations. Permission levels for an Analysis Services database are specified on the **General** pane of the **Create Role** dialog box, shown in the following illustration and described below.  
  
 There are no logins in Analysis Services. You simply create roles and assign Windows accounts in the **Membership** pane. All users, including administrators, connect to Analysis Services using a Windows account.  
  
 ![Create role dialog showing database permissions](../../analysis-services/multidimensional-models/media/ssas-permsdbrole.png "Create role dialog showing database permissions")  
  
 There are three types of permissions specified at the database level.  
  
 **Full Control (Administrator)** ─ Full Control is an all-encompassing permission that conveys broad powers over an Analysis Services database, such as the ability to query or process any object within the database, and manage role security. Full Control is synonymous with database administrator status. When you select **Full Control**, the **Process Database** and **Read Definition** permissions are also selected and cannot be removed.  
  
> [!NOTE]  
>  Server administrators (members of the Server Administrator role) also have implicit Full Control over every database on the server.  
  
 **Process Database** ─ This permission is used to delegate processing at the database level. As an administrator, you can offload this task by creating a role that allows another person or service to invoke processing operations for any object in the database. Alternatively, you can also create roles that enable processing on specific objects. See [Grant process permissions &#40;Analysis Services&#41;](../../analysis-services/multidimensional-models/grant-process-permissions-analysis-services.md) for more information.  
  
 **Read Definition** ─ This permission grants the ability to read object metadata, minus the ability to view associated data. Typically this permission is used in roles created for dedicated processing, adding the ability to use tools such as [!INCLUDE[ssBIDevStudio](../../includes/ssbidevstudio-md.md)] or [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] to process a database interactively. Without **Read Definition**, the **Process Database** permission is effective only in scripted scenarios. If you plan to automate processing, perhaps through SSIS or another scheduler, you probably want to create a role that has **Process Database** without **Read Definition**. Otherwise, consider combining the two properties together in the same role to support both unattended and interactive processing via SQL Server tools that visualize the data model in a user interface.  
  
## Full Control (Administrator) permissions  
 In [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)], a database administrator is any Windows user identity assigned to a role that includes Full Control (Administrator) permissions. A database administrator can perform any task within the database, including:  
  
-   Process objects  
  
-   Read data and metadata for all objects in the database, including cubes, dimensions, measure groups, perspectives and data mining models  
  
-   Create or modify database roles by adding users or permissions, including adding users to roles also having Full Control permissions  
  
-   Delete database roles or role membership  
  
-   Register assemblies (or stored procedures) for the database.  
  
 Notice that a database administrator cannot add or delete databases on the server, or grant administrator rights to other databases on the same server. That privilege belongs to server administrators alone. See [Grant server admin rights to an  Analysis Services instance](../../analysis-services/instances/grant-server-admin-rights-to-an-analysis-services-instance.md) for more information about this permission level.  
  
 Because all roles are user-defined, we recommend that you create a role dedicated for this purpose (for example, a role named "dbadmin"), and then assign Windows user and group accounts accordingly.  
  
#### Create roles in SSMS  
  
1.  In [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], connect to the instance of [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)], open the **Databases** folder, select a database, and right-click **Roles** | **New Role**.  
  
2.  In the **General** pane, enter a name, such as DBAdmin.  
  
3.  Select the **Full Control (Administrator)** check box for the cube. Notice that **Process Database** and **Read Definition** are selected automatically. Both of these permissions are always included in roles that include **Full Control**.  
  
4.  In the **Membership** pane, enter the Windows user and group accounts that connect to Analysis Services using this role.  
  
5.  Click **OK** to finish creating the role.  
  
## Process database  
 When defining a role that grants database permissions, you can skip **Full Control** and choose just **Process Database**. This permission, set at the database level, allows processing on all objects within the database. See [Grant process permissions &#40;Analysis Services&#41;](../../analysis-services/multidimensional-models/grant-process-permissions-analysis-services.md)  
  
## Read definition  
 Like **Process Database**, setting **Read Definition** permissions at the database level has a cascading effect on other objects within the database. If you want to set Read Definition permissions at a more granular level, you must clear Read Definition as a database property in the General pane. See [Grant read definition permissions on object metadata &#40;Analysis Services&#41;](../../analysis-services/multidimensional-models/grant-read-definition-permissions-on-object-metadata-analysis-services.md) for more information.  
  
## See Also  
 [Grant server admin rights to an  Analysis Services instance](../../analysis-services/instances/grant-server-admin-rights-to-an-analysis-services-instance.md)   
 [Grant process permissions &#40;Analysis Services&#41;](../../analysis-services/multidimensional-models/grant-process-permissions-analysis-services.md)  
  
  
