---
title: "Security Roles  (Analysis Services - Multidimensional Data) | Microsoft Docs"
ms.date: 05/02/2018
ms.prod: sql
ms.technology: analysis-services
ms.custom: olap
ms.topic: conceptual
ms.author: owend
ms.reviewer: owend
author: minewiskan
manager: kfile
---
# Security Roles  (Analysis Services - Multidimensional Data)
[!INCLUDE[ssas-appliesto-sqlas](../../../includes/ssas-appliesto-sqlas.md)]
  Roles are used in [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] to manage security for [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] objects and data. In basic terms, a role associates the security identifiers (SIDs) of Microsoft Windows users and groups that have specific access rights and permissions defined for objects managed by an instance of [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)]. Two types of roles are provided in [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)]:  
  
-   The server role, a fixed role that provides administrator access to an instance of [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)].  
  
-   Database roles, roles defined by administrators to control access to objects and data for non-administrator users.  
  
 Security in [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] security is managed by using roles and permissions. Roles are groups of users. Users, also called members, can be added or removed from roles. Permissions for objects are specified by roles, and all members in a role can use the objects for which the role has permissions. All members in a role have equal permissions to the objects. Permissions are particular to objects. Each object has a permissions collection with the permissions granted on that object, different sets of permissions can be granted on an object. Each permission, from the permissions collection of the object, has a single role assigned to it.  
  
## Role and Role Member Objects  
 A role is a containing object for a collection of users (members). A Role definition establishes the membership of the users in [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)]. Because permissions are assigned by role, a user must be a member of a role before the user has access to any object.  
  
 A <xref:Microsoft.AnalysisServices.Role> object is composed of the parameters Name, Id, and Members. Members is a collection of strings. Each member contains the user name in the form of "domain\username". Name is a string that contains the name of the role. ID is a string that contains the unique identifier of the role.  
  
### Server Role  
 The [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] server role defines administrative access of Windows users and groups to an instance of [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)]. Members of this role have access to all [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] databases and objects on an instance of [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)], and can perform the following tasks:  
  
-   Perform server-level administrative functions using SQL Server Management Studio or [!INCLUDE[ssBIDevStudioFull](../../../includes/ssbidevstudiofull-md.md)], including creating databases and setting server-level properties.  
  
-   Perform administrative functions programmatically with Analysis Management Objects (AMO).  
  
-   Maintain [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] database roles.  
  
-   Start traces (other than for processing events, which can be performed by a database role with Process access).  
  
 Every instance of [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] has a server role that defines which users can administer that instance. The name and ID of this role is Administrators, and unlike database roles, the server role cannot be deleted, nor can permissions be added or removed. In other words, a user either is or is not an administrator for an instance of [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)], depending on whether he or she is included in the server role for that instance of [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)].  
  
### Database Roles  
 An [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] database role defines user access to objects and data in an [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] database. A database role is created as a separate object in an [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] database, and applies only to the database in which that role is created. Windows users and groups are included in the role by an administrator, who also defines permissions within the role.  
  
 The permissions of a role may allow members to access and administer the database, in addition to the objects and data within the database. Each permission has one or more access rights associated with it, which in turn give the permission finer control over access to a particular object in the database.  
  
## Permission Objects  
 Permissions are associated with an object (cube, dimension, others) for a particular role. Permissions specify what operations the member of that role can perform on that object.  
  
 The <xref:Microsoft.AnalysisServices.Permission> class is an abstract class. Therefore, you must use the derived classes to define permissions on the corresponding objects. For each object, a permission derived class is defined.  
  
|Object|Class|  
|------------|-----------|  
|<xref:Microsoft.AnalysisServices.Database>|<xref:Microsoft.AnalysisServices.DatabasePermission>|  
|<xref:Microsoft.AnalysisServices.DataSource>|<xref:Microsoft.AnalysisServices.DataSourcePermission>|  
|<xref:Microsoft.AnalysisServices.Dimension>|<xref:Microsoft.AnalysisServices.DimensionPermission>|  
|<xref:Microsoft.AnalysisServices.Cube>|<xref:Microsoft.AnalysisServices.CubePermission>|  
|<xref:Microsoft.AnalysisServices.MiningStructure>|<xref:Microsoft.AnalysisServices.MiningStructurePermission>|  
|<xref:Microsoft.AnalysisServices.MiningModel>|<xref:Microsoft.AnalysisServices.MiningModelPermission>|  
  
 Possible actions enabled by permissions are shown in the list:  
  
|Action|Values|Explanation|  
|------------|------------|-----------------|  
|Process|{**true**, **false**}<br /><br /> Default=**false**|If **true**, members can process the object and any object that is contained in the object.<br /><br /> Process permissions do not apply to mining models. <xref:Microsoft.AnalysisServices.MiningModel> permissions are always inherited from <xref:Microsoft.AnalysisServices.MiningStructure>.|  
|ReadDefinition|{**None**, **Basic**, **Allowed**}<br /><br /> Default=**None**|Specifies whether members can read the data definition (ASSL) associated with the object.<br /><br /> If **Allowed**, members can read the ASSL associated with the object.<br /><br /> **Basic** and **Allowed** are inherited by objects that are contained in the object. **Allowed** overrides **Basic** and **None**.<br /><br /> **Allowed** is required for DISCOVER_XML_METADATA on an object. **Basic** is required to create linked objects and local cubes.|  
|Read|{**None**, **Allowed**}<br /><br /> Default=**None** (Except for DimensionPermission, where default=**Allowed**)|Specifies whether members have read access to schema rowsets and data content.<br /><br /> **Allowed** gives read access on a database, which lets you discover a database.<br /><br /> **Allowed** on a cube gives read access in schema rowsets and access to cube content (unless constrained by <xref:Microsoft.AnalysisServices.CellPermission> and <xref:Microsoft.AnalysisServices.CubeDimensionPermission>).<br /><br /> **Allowed** on a dimension grants that read permission on all attributes in the dimension (unless constrained by <xref:Microsoft.AnalysisServices.CubeDimensionPermission>). Read permission is used for static inheritance to the <xref:Microsoft.AnalysisServices.CubeDimensionPermission> only. **None** on a dimension hides the dimension and gives access to the default member only for aggregatable attributes; an error is raised if the dimension contains a non-aggregatable attribute.<br /><br /> **Allowed** on a <xref:Microsoft.AnalysisServices.MiningModelPermission> grants permissions to see objects in schema rowsets and to perform predict joins.<br /><br /> **NoteAllowed** is required to read or write to any object in the database.|  
|Write|{**None**, **Allowed**}<br /><br /> Default=**None**|Specifies whether members have write access to data of the parent object.<br /><br /> Access applies to <xref:Microsoft.AnalysisServices.Dimension>, <xref:Microsoft.AnalysisServices.Cube>, and <xref:Microsoft.AnalysisServices.MiningModel> subclasses. It does not apply to database <xref:Microsoft.AnalysisServices.MiningStructure> subclasses, which generates a validation error.<br /><br /> **Allowed** on a <xref:Microsoft.AnalysisServices.Dimension> grants write permission on all attributes in the dimension.<br /><br /> **Allowed** on a <xref:Microsoft.AnalysisServices.Cube> grants write permission on the cells of the cube for partitions defined as Type=writeback.<br /><br /> **Allowed** on a <xref:Microsoft.AnalysisServices.MiningModel> grants permission to modify model content.<br /><br /> **Allowed** on a <xref:Microsoft.AnalysisServices.MiningStructure> has no specific meaning in [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)].<br /><br /> Note: Write cannot be set to **Allowed** unless read is also set to **Allowed**|  
|Administer<br /><br /> Note: Only in Database permissions|{**true**, **false**}<br /><br /> Default=**false**|Specifies whether members can administer a database.<br /><br /> **true** grants members access to all objects in a database.<br /><br /> A member can have Administer permissions for a specific database, but not for others.|  
  
## See Also  
 [Permissions and Access Rights &#40;Analysis Services - Multidimensional Data&#41;](http://msdn.microsoft.com/library/59fa3573-f985-46cb-8042-7da71bd59a7b)   
 [Authorizing access to objects and operations &#40;Analysis Services&#41;](../../../analysis-services/multidimensional-models/authorizing-access-to-objects-and-operations-analysis-services.md)  
  
  
