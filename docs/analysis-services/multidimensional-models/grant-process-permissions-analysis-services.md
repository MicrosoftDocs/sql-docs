---
title: "Grant process permissions (Analysis Services) | Microsoft Docs"
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
# Grant process permissions (Analysis Services)
[!INCLUDE[ssas-appliesto-sqlas](../../includes/ssas-appliesto-sqlas.md)]
  As an administrator, you can create a role dedicated to Analysis Services processing operations, allowing you to delegate that particular task to other users, or to applications used for unattended scheduled processing. Process permissions can be granted at the database, cube, dimension, and mining structure levels. Unless you are working with a very large cube or tabular database, we recommend granting processing rights at the database level, inclusive of all objects, including those having dependencies on each other.  
  
 Permissions are granted through roles that associate objects with permissions and Windows user or group accounts. Remember that permissions are additive. If one role grants permission to process a cube, while a second role gives the same user permission to process a dimension, the permissions from the two different roles combine to give the user permission to both process the cube and process the specified dimension within that database.  
  
> [!IMPORTANT]  
>  A user whose role only has Process permissions will be unable to use [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)] to connect to [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] and process objects. These tools require the **Read Definition** permission to access object metadata. Without the ability to use either tool, XMLA script must be used to execute a processing operation.  
>   
>  We suggest you also grant **Read Definition** permissions for testing purposes. A user having both **Read Definition** and **Process Database** permissions can process objects in [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], interactively. See [Grant read definition permissions on object metadata &#40;Analysis Services&#41;](../../analysis-services/multidimensional-models/grant-read-definition-permissions-on-object-metadata-analysis-services.md) for details.  
  
## Set processing permissions at the database level  
 This section explains how to enable processing by non-administrators, for all cubes, dimensions, mining structures, and mining models in the database.  
  
1.  In [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], connect to the instance of [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)], open the Databases folder, and select a database.  
  
2.  Right-click **Roles** | **New Role**. Enter a name and description.  
  
3.  In the **General** pane, select the **Process Database** check box. Additionally, select **Read Definition** to also enable interactive processing through one of the SQL Server tools, such as [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)].  
  
4.  In the **Membership** pane, add the Windows user and group accounts having permission to process any object in this database.  
  
5.  Click **OK** to complete the role definition.  
  
## Set processing permissions on individual objects  
 You can set processing permissions on individual cubes, dimensions, data mining structures or models.  
  
 Processing can fail if you inadvertently exclude objects that need to be processed together (for example, if you enable processing on a cube, but not on its related dimensions). Because it can be easy to miss object dependencies, thorough testing is essential when setting processing permissions on individual objects.  
  
1.  In [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], connect to the instance of [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)], open the Databases folder, and select a database.  
  
2.  Right-click **Roles** | **New Role**. Enter a name and description.  
  
3.  In the **General** pane, clear the **Process Database** check box. Database permissions override the ability to set permissions on lower-level objects by making role options grayed out or un-selectable.  
  
     Technically, no database permissions are needed for dedicated processing roles. But without **Read Definition** at the database level, you cannot view the database in [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], making testing more difficult.  
  
4.  Select individual objects to process:  
  
    -   In the **Cubes** pane, select the **Process** check box for each cube.  
  
    -   In the **Dimensions** pane, select **All database dimensions**, and then **Process** check box for each dimension. Or, select all rows, then use shift-click to toggle the check box selections.  
  
5.  In the **Membership** pane, add the Windows user and group accounts having permission to process these objects.  
  
6.  Click **OK** to complete the role definition.  
  
## Test processing  
  
1.  Hold down the shift-key and right-click [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], select **Run as a different user** and connect to the instance of [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] using a Windows account assigned to the role you are testing.  
  
2.  Open the Databases folder, and select a database. You will only see the databases that are visible to the roles for which your account has membership.  
  
3.  Right-click a cube or dimension and select **Process**. Choose a processing option. Test all of the options, for all combinations of objects. If errors occur due to missing objects, add the objects to the role.  
  
## Set processing permissions on a data mining structure  
 You can create a role granting permission to process data mining structures. This includes the processing of all mining models.  
  
 **Drill Through** and **Read Definition** permissions used for browsing a mining model and structure are atomic and can be added to the same role, or separated out into a different role.  
  
1.  In [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], connect to the instance of [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)], open the Databases folder, and select a database.  
  
2.  Right-click **Roles** | **New Role**. Enter a name and description. In the **General** pane, make sure that the database permission check boxes are clear. Database permissions will override the ability to set permissions on lower-level objects by making role options grayed out or un-selectable.  
  
3.  In the **Mining Structures** pane, select the **Process** check box for each mining structure.  
  
4.  In the **Membership** pane, add the Windows user and group accounts having permission to process any object in this database.  
  
5.  Click **OK** to complete the role definition.  
  
## See Also  
 [Process Database, Table, or Partition &#40;Analysis Services&#41;](../../analysis-services/tabular-models/process-database-table-or-partition-analysis-services.md)   
 [Processing a multidimensional model &#40;Analysis Services&#41;](../../analysis-services/multidimensional-models/processing-a-multidimensional-model-analysis-services.md)   
 [Grant database permissions &#40;Analysis Services&#41;](../../analysis-services/multidimensional-models/grant-database-permissions-analysis-services.md)   
 [Grant read definition permissions on object metadata &#40;Analysis Services&#41;](../../analysis-services/multidimensional-models/grant-read-definition-permissions-on-object-metadata-analysis-services.md)  
  
  
