---
title: "Grant permissions on data mining structures and models (Analysis Services) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
f1_keywords: 
  - "sql12.asvs.roledesignerdialog.miningmodels.f1"
helpviewer_keywords: 
  - "data mining [Analysis Services], security"
  - "permissions [Analysis Services], mining models"
  - "mining models [Analysis Services], security"
  - "mining structures [Analysis Services], security"
  - "permissions [Analysis Services], mining structures"
  - "user access rights [Analysis Services], mining structures"
  - "user access rights [Analysis Services], mining models"
ms.assetid: a0008004-e2b7-47db-acad-5fe7e12b130f
author: minewiskan
ms.author: owend
manager: craigg
---
# Grant permissions on data mining structures and models (Analysis Services)
  By default, only an Analysis Services server administrator has permissions to view data mining structures or mining models in the database. Follow the instructions below to grant permissions to non-administrator users.  
  
## Set permissions to access a mining structure  
  
1.  In SSMS, connect to Analysis Services. See [Connect from client applications &#40;Analysis Services&#41;](../instances/connect-from-client-applications-analysis-services.md) if you need help with the steps.  
  
2.  Open the **Databases** folder, and select a database in Object Explorer.  
  
3.  Right-click **Roles** and choose **New Role**.  
  
4.  In the General page, enter a name, and optionally, a description. The page also contains several database permissions, such as Full Control, Process Database, and Read Definition. None of these permissions are needed for data mining access. See [Grant database permissions &#40;Analysis Services&#41;](grant-database-permissions-analysis-services.md) for more information about database permissions.  
  
5.  In the **Mining Structure** pane, select **Read** or **Read/Write**  for each data mining structure.  
  
6.  In the **Membership** pane, enter the Windows user and group accounts that connect to Analysis Services using this role.  
  
7.  Click **OK** to finish creating the role.  
  
## Set permissions to access a mining model  
 For a data mining model, a role can have either **Read** or **Read/Write** permissions, as well as **Drillthrough** and **Read Definition** permissions that allow viewing and browsing the underlying data.  
  
 **Note** If you enable drillthrough on both the mining structure and the mining model, any user who is a member of a role that has drillthrough permissions on the mining model and the mining structure can also view columns in the mining structure, even if those columns are not included in the mining model. Therefore, to protect sensitive information, you should set up the data source view to mask personal information, and allow drillthrough access on the mining structure only when necessary.  
  
 To grant read or read/write permissions to a database role, a user must be a member of the [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] server role or a member of an [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] database role that has Full Control (Administrator) permissions.  
  
1.  In [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], connect to the instance of [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)], expand **Roles** for the appropriate database in Object Explorer, and then click a database role (or create a new database role).  
  
2.  In the **Mining Structure** pane, locate the mining model in the **Mining Models** list, and then select **Read**, **Read/Write**, **Drill Through**, or **Browse** for that mining model.  
  
3.  In the **Membership** pane, enter the Windows user and group accounts that connect to Analysis Services using this role.  
  
4.  Click **OK** to finish creating the role.  
  
 To use a data source in a drillthrough query that uses the Data Mining Extensions (DMX) OPENQUERY clause, the database role also needs read/write permission on the appropriate data source object. For more information, see [Grant permissions on a data source object &#40;Analysis Services&#41;](grant-permissions-on-a-data-source-object-analysis-services.md) and [OPENQUERY &#40;DMX&#41;](/sql/dmx/source-data-query-openquery).  
  
> [!NOTE]  
>  By default, the submission of DMX queries by using OPENROWSET is disabled.  
  
## See Also  
 [Grant Server Administrator Permissions &#40;Analysis Services&#41;](../instances/grant-server-admin-rights-to-an-analysis-services-instance.md)   
 [Grant cube or model permissions &#40;Analysis Services&#41;](grant-cube-or-model-permissions-analysis-services.md)   
 [Grant custom access to dimension data &#40;Analysis Services&#41;](grant-custom-access-to-dimension-data-analysis-services.md)   
 [Grant custom access to cell data &#40;Analysis Services&#41;](grant-custom-access-to-cell-data-analysis-services.md)  
  
  
