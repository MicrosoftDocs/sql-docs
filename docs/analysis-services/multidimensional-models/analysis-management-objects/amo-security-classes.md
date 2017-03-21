---
title: "AMO Security Classes | Microsoft Docs"
ms.custom: ""
ms.date: "03/03/2017"
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
  - "Analysis Management Objects, security"
  - "security [AMO]"
  - "AMO, security"
ms.assetid: e3d5012a-8121-40de-9244-1fc824228693
caps.latest.revision: 23
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# AMO Security Classes
  This topic contains the following sections:  
  
-   [Role and RoleMember Objects](#RolesMembers)  
  
-   [Permission Objects](#Permissions)  
  
 The following illustration shows the relationship of the classes that are explained in this topic.  
  
 ![Security classes in AMO covered in this topic](../../../analysis-services/multidimensional-models/analysis-management-objects/media/amo-securityclasses.gif "Security classes in AMO covered in this topic")  
  
##  <a name="RolesMembers"></a> Role and RoleMember Objects  
 A <xref:Microsoft.AnalysisServices.Role> object is created by adding it to the roles collection of the database, and updating the <xref:Microsoft.AnalysisServices.Role> object to the server by using the Update method. A <xref:Microsoft.AnalysisServices.Role> object has to be updated before it can be used.  
  
 To remove a <xref:Microsoft.AnalysisServices.Role> object, it has to be dropped by using the Drop method of the <xref:Microsoft.AnalysisServices.Role> object. The Remove method, from the roles collection, only prevents you from seeing the role in your application, but it does not remove the role from the server. A <xref:Microsoft.AnalysisServices.Role> object cannot be dropped if there are any permissions associated with it.  
  
 A <xref:Microsoft.AnalysisServices.RoleMember> object is created by adding a user to the members collection of the role and updating the <xref:Microsoft.AnalysisServices.Role> object to the server by using the Update method. Only Server Administrators or Database Administrators are permitted to create roles. A <xref:Microsoft.AnalysisServices.Role> object has to be updated to the server before any of its members is allowed to use any the objects to which the user has been granted permission.  
  
 To remove a <xref:Microsoft.AnalysisServices.RoleMember> object, it has to be removed from the collection by using the Remove method of the collection, and then updating the role by using the Update method.  
  
 For more information about methods and properties available for these objects, see <xref:Microsoft.AnalysisServices.Role> and <xref:Microsoft.AnalysisServices.RoleMember> in the <xref:Microsoft.AnalysisServices>.  
  
##  <a name="Permissions"></a> Permission Objects  
 A <xref:Microsoft.AnalysisServices.Permission> object is created by adding it to the permissions collection of the object and updating the <xref:Microsoft.AnalysisServices.Permission> object to the server by using the Update method.  
  
 To remove a <xref:Microsoft.AnalysisServices.Permission> object, it has to be dropped by using the Drop method of the object. The remove method, from the permissions collection, only prevents you from seeing the permission in your application, but it does not remove the <xref:Microsoft.AnalysisServices.Permission> object from the server. A role cannot be deleted if there is any permission associated with it.  
  
 For more information about methods and properties available, see <xref:Microsoft.AnalysisServices.Permission> in <xref:Microsoft.AnalysisServices>.  
  
## See Also  
 <xref:Microsoft.AnalysisServices>   
 [Programming AMO Security Objects](../../../analysis-services/multidimensional-models/analysis-management-objects/programming-amo-security-objects.md)   
 [Permissions and Access Rights &#40;Analysis Services - Multidimensional Data&#41;](http://msdn.microsoft.com/library/59fa3573-f985-46cb-8042-7da71bd59a7b)   
 [Introducing AMO Classes](../../../analysis-services/multidimensional-models/analysis-management-objects/amo-classes-introduction.md)   
 [Logical Architecture &#40;Analysis Services - Multidimensional Data&#41;](../../../analysis-services/multidimensional-models/olap-logical/understanding-microsoft-olap-logical-architecture.md)   
 [Database Objects &#40;Analysis Services - Multidimensional Data&#41;](../../../analysis-services/multidimensional-models/olap-logical/database-objects-analysis-services-multidimensional-data.md)  
  
  