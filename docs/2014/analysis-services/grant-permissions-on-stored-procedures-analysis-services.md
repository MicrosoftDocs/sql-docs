---
title: "Grant permissions on stored procedures (Analysis Services) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
ms.assetid: 01793166-a3e5-4856-8302-21b82d494e69
author: minewiskan
ms.author: owend
manager: craigg
---
# Grant permissions on stored procedures (Analysis Services)
  Stored procedures, or assemblies, in [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] are external routines, written in a [!INCLUDE[msCoName](../includes/msconame-md.md)] .NET programming language, that extend the capabilities of [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)]. Assemblies let the developer take advantage of cross-language integration, exception handling, versioning support, deployment support, and debugging support.  
  
 You must be a Server Administrator to register an assembly. See [Grant Server Administrator Permissions &#40;Analysis Services&#41;](instances/grant-server-admin-rights-to-an-analysis-services-instance.md).  
  
## Security context for stored procedure execution  
 Any user can call a stored procedure. Depending on how the stored procedure was configured, the procedure can run either in the context of the user calling the procedure or in the context of an anonymous user. Because an anonymous user has no security context, use this capability together with configuring the instance of [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] to permit anonymous access.  
  
 After the user calls a stored procedure but before [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] runs the stored procedure, [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] evaluates the actions within the stored procedure. [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] evaluates the actions in a stored procedure based on the intersection of the permissions granted to the user and the permission set used to run the procedure. If the stored procedure contains any action that cannot be performed by the database role for the user, that action will not be performed.  
  
 Following are the permission sets that are used to run stored procedures:  
  
-   **Safe** With the Safe permission set, a stored procedure cannot access the protected resources in the [!INCLUDE[msCoName](../includes/msconame-md.md)] .NET Framework. This permission set only allows for computations. This is the safest permission set; information does not leak outside [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)], permissions cannot be elevated, and the risk of data tampering attacks is minimized.  
  
-   **External Access** With the External Access permission set, a stored procedure can access external resources by using managed code. Setting a stored procedure to this permission set will not cause programming errors that could lead to server instability. However, this permission set may result in information leaking outside the server, and the possibility of an elevation in permission and data tampering attacks.  
  
-   **Unrestricted** With the Unrestricted permission set, a stored procedure can access external resources by using any code. With this permission set, there are no security or reliability guarantees for stored procedures.  
  
## See Also  
 [Multidimensional Model Assemblies Management](multidimensional-models/multidimensional-model-assemblies-management.md)  
  
  
