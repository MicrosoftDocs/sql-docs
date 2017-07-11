---
title: "Programming AMO Security Objects | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.tgt_pltfrm: ""
ms.topic: "reference"
applies_to: 
  - "SQL Server 2016 Preview"
helpviewer_keywords: 
  - "programming [AMO]"
  - "Analysis Management Objects, security"
  - "AMO, security"
ms.assetid: 5d963721-6e6e-46eb-bc9b-18724dd0b751
caps.latest.revision: 20
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# Programming AMO Security Objects
  In [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)], programming security objects or running applications that use AMO security objects requires being a member of the Server Administrator group or the Database Administrator group. Server Administrator and Database Administrator are an access levels supplied by [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)].  
  
 In [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] the user access to any object is obtained through the combination of Roles and Permissions assigned to that object. For more information, see [AMO Security Classes](../../../analysis-services/multidimensional-models/analysis-management-objects/amo-security-classes.md).  
  
## Role and Permission Objects  
 Server roles contain one and only one role in the collection, the Administrators role. New roles cannot be added to the server roles collection. Membership in the Administrators role permits complete access to every object in the server  
  
 <xref:Microsoft.AnalysisServices.Role> objects are created at database level. Role maintenance requires only adding or removing members to or from the role, and adding or dropping roles to the <xref:Microsoft.AnalysisServices.Database> object. A role cannot be dropped if there is any <xref:Microsoft.AnalysisServices.Permission> object associated with the role. To drop a role, all <xref:Microsoft.AnalysisServices.Permission> objects in the <xref:Microsoft.AnalysisServices.Database> objects must be searched, and the <xref:Microsoft.AnalysisServices.Role> removed from permissions, before the <xref:Microsoft.AnalysisServices.Role> can be dropped from the <xref:Microsoft.AnalysisServices.Database>.  
  
 Permissions define the enabled actions on the object where the permission is supplied. Permissions can be supplied to the following objects: <xref:Microsoft.AnalysisServices.Database>, <xref:Microsoft.AnalysisServices.DataSource>, <xref:Microsoft.AnalysisServices.Dimension>, <xref:Microsoft.AnalysisServices.Cube>, <xref:Microsoft.AnalysisServices.MiningStructure>, and <xref:Microsoft.AnalysisServices.MiningModel>. Permission maintenance involves granting or revoking enabled access by the corresponding access property. For each enabled access, there is a property that can be set to the desired level of access. Access can be defined for the following operations: Process, ReadDefinition, Read, Write, and Administer. Administer access is only defined on the <xref:Microsoft.AnalysisServices.Database> object. The database administrator security level is obtained when the role is granted with the Administer database permission.  
  
 The following sample creates four roles: Database Administrators, Processors, Writers, and Readers.  
  
 Database Administrators can administer the supplied database.  
  
 Processors can process all objects in a database and verify results. To verify results, read access to the database object must be explicitly enabled to the supplied cube, because read permission does not apply to children objects.  
  
 Writers can read and write to the supplied cube, and cell access is limited to the 'United States' in the customer dimension.  
  
 Readers can read on the supplied cube, and cell access is limited to the 'United States' in the customer dimension.  
  
```  
static public void CreateRolesAndPermissions(Database db, Cube cube)  
{  
    Role role;  
    DatabasePermission dbperm;  
    CubePermission cubeperm;  
  
    #region Create the Database Administrators role  
  
    // Create the Database Administrators role.  
    role = db.Roles.Add("Database Administrators");  
    role.Members.Add(new RoleMember("")); // e.g. domain\user  
    role.Update();  
  
    // Assign administrative permissions to this role.  
    // Members of this role can perform any operation within the database.  
    dbperm = db.DatabasePermissions.Add(role.ID);  
    dbperm.Administer = true;  
    dbperm.Update();  
  
    #endregion  
  
    #region Create the Processors role  
  
    // Create the Processors role.  
    role = db.Roles.Add("Processors");  
    role.Members.Add(new RoleMember("")); // e.g. myDomain\johndoe  
    role.Update();  
  
    // Assign Read and Process permissions to this role.  
    // Members of this role can process objects in the database and query them to verify results.  
    // Process permission applies to all contained objects, i.e. all dimensions and cubes.  
    // Read permission does not apply to contained objects, so we must assign the permission explicitly on the cubes.  
    dbperm = db.DatabasePermissions.Add(role.ID);  
    dbperm.Read = ReadAccess.Allowed;  
    dbperm.Process = true;  
    dbperm.Update();  
  
    cubeperm = cube.CubePermissions.Add(role.ID);  
    cubeperm.Read = ReadAccess.Allowed;  
    cubeperm.Update();  
  
    #endregion  
  
    #region Create the Writers role  
  
    // Create the Writers role.  
    role = db.Roles.Add("Writers");  
    role.Members.Add(new RoleMember("")); // e.g. redmond\johndoe  
    role.Update();  
  
    // Assign Read and Write permissions to this role.  
    // Members of this role can discover, query and writeback to the Adventure Works cube.  
    // However cell access and writeback is restricted to the United States (in the Customer dimension).  
    dbperm = db.DatabasePermissions.Add(role.ID);  
    dbperm.Read = ReadAccess.Allowed;  
    dbperm.Update();  
  
    cubeperm = cube.CubePermissions.Add(role.ID);  
    cubeperm.Read = ReadAccess.Allowed;  
    cubeperm.Write = WriteAccess.Allowed;  
    cubeperm.CellPermissions.Add(new CellPermission(CellPermissionAccess.Read, "[Customer].[Country-Region].CurrentMember is [Customer].[Country-Region].[Country-Region].&[United States]"));  
    cubeperm.CellPermissions.Add(new CellPermission(CellPermissionAccess.ReadWrite, "[Customer].[Country-Region].CurrentMember is [Customer].[Country-Region].[Country-Region].&[United States]"));  
    cubeperm.Update();  
  
    #endregion  
  
    #region Create the Readers role  
  
    // Create the Readers role.  
    role = db.Roles.Add("Readers");  
    role.Members.Add(new RoleMember("")); // e.g. redmond\johndoe  
    role.Update();  
  
    // Assign Read permissions to this role.  
    // Members of this role can discover and query the Adventure Works cube.  
    // However the Customer dimension is restricted to the United States.  
    dbperm = db.DatabasePermissions.Add(role.ID);  
    dbperm.Read = ReadAccess.Allowed;  
    dbperm.Update();  
  
    cubeperm = cube.CubePermissions.Add(role.ID);  
    cubeperm.Read = ReadAccess.Allowed;  
    Dimension dim = db.Dimensions.GetByName("Customer");  
    DimensionAttribute attr = dim.Attributes.GetByName("Country-Region");  
    CubeDimensionPermission cubedimperm = cubeperm.DimensionPermissions.Add(dim.ID);  
    cubedimperm.Read = ReadAccess.Allowed;  
    AttributePermission attrperm = cubedimperm.AttributePermissions.Add(attr.ID);  
    attrperm.AllowedSet = "{[Customer].[Country-Region].[Country-Region].&[United States]}";  
    cubeperm.Update();  
  
    #endregion  
}  
```  
  
## See Also  
 <xref:Microsoft.AnalysisServices>   
 [Introducing AMO Classes](../../../analysis-services/multidimensional-models/analysis-management-objects/amo-classes-introduction.md)   
 [Programming AMO Security objects](../../../analysis-services/multidimensional-models/analysis-management-objects/programming-amo-security-objects.md)   
 [Permissions and Access Rights &#40;Analysis Services - Multidimensional Data&#41;](http://msdn.microsoft.com/library/59fa3573-f985-46cb-8042-7da71bd59a7b)   
 [Logical Architecture &#40;Analysis Services - Multidimensional Data&#41;](../../../analysis-services/multidimensional-models/olap-logical/understanding-microsoft-olap-logical-architecture.md)   
 [Database Objects &#40;Analysis Services - Multidimensional Data&#41;](../../../analysis-services/multidimensional-models/olap-logical/database-objects-analysis-services-multidimensional-data.md)  
  
  