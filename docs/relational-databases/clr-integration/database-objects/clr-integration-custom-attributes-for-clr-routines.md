---
title: "Custom Attributes for CLR Routines | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.reviewer: ""
ms.technology: clr
ms.topic: "reference"
helpviewer_keywords: 
  - "routines [CLR integration]"
  - "SqlFacet attribute"
  - "SqlTrigger attribute"
  - "SqlProcedure attribute"
  - "custom attributes [CLR integration]"
  - "SqlUserDefinedAggregate attribute"
  - "attributes [CLR integration]"
  - "SqlMethod attribute"
  - "SqlFunction attribute"
  - "common language runtime [SQL Server], attributes"
  - "SqlUserDefinedTypeAttribute attribute"
ms.assetid: 95069d22-b05d-4670-b053-15ee2a664e33
author: "rothja"
ms.author: "jroth"
manager: craigg
---
# CLR Integration Custom Attributes for CLR Routines
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  The attributes listed can be applied to common language runtime (CLR) routines, user-defined types, and user-defined aggregates that are registered in [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)]. If the attribute is not applied, [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] assumes the default value. The attributes listed are defined in the **Microsoft.SqlServer.Server** namespace.  
  
## The SqlUserDefinedAggregate Attribute  
 The **SqlUserDefinedAggregate** attribute indicates that the method should be registered as a user-defined aggregate. Every user-defined aggregate must be annotated with this attribute.  
  
 For more information, see [SqlUserDefinedAggregateAttribute](https://go.microsoft.com/fwlink/?LinkId=124626).  
  
## The SqlFunction Attribute  
 The **SqlFunction** attribute indicates the method should be registered as a function, with the appropriate function attributes set.  
  
 For more information, see [SqlFunctionAttribute](https://go.microsoft.com/fwlink/?LinkId=128019).  
  
## The SqlFacet Attribute  
 The **SqlFacet** attribute is used to return information about the return type of a user-defined type (UDT) expression.  
  
 For more information, see [SqlFacetAttribute](https://go.microsoft.com/fwlink/?LinkId=128020).  
  
## The SqlProcedure Attribute  
 The **SqlProcedure** attribute indicates the method should be registered as a stored procedure. This attribute is used only by Visual Studio to register the specified method as a stored procedure automatically; it is not used by [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)].  
  
 For more information, see [SqlProcedureAttribute](https://go.microsoft.com/fwlink/?LinkId=128021).  
  
## The SqlTrigger Attribute  
 The **SqlTrigger** attribute indicates the method should be registered as a trigger.  
  
 For more information, see [SqlTriggerContext](https://go.microsoft.com/fwlink/?LinkId=128022) and [SqlTriggerAttribute](https://go.microsoft.com/fwlink/?LinkId=203898).  
  
## The SqlUserDefinedTypeAttribute  
 You can apply the SqlUserDefinedTypeAttribute to a class definition in the assembly. It causes [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] to create a user-defined type that is bound to the class definition which has this custom attribute.  
  
 For more information, see [SqlUserDefinedTypeAttribute](https://go.microsoft.com/fwlink/?LinkId=128024).  
  
## The SqlMethod Attribute  
 The **SqlMethod** attribute is used to indicate the determinism and data access properties of a method or a property on a UDT.  
  
 For more information, see [SqlMethodAttribute](https://go.microsoft.com/fwlink/?LinkId=128025).  
  
## See Also  
 [CLR User-Defined Aggregates](../../../relational-databases/clr-integration-database-objects-user-defined-functions/clr-user-defined-aggregates.md)   
 [CLR User-Defined Functions](../../../relational-databases/clr-integration-database-objects-user-defined-functions/clr-user-defined-functions.md)   
 [CLR User-Defined Types](../../../relational-databases/clr-integration-database-objects-user-defined-types/clr-user-defined-types.md)   
 [CLR Stored Procedures](https://msdn.microsoft.com/library/bbdd51b2-a9b4-4916-ba6f-7957ac6c3f33)   
 [CLR Triggers](https://msdn.microsoft.com/library/302a4e4a-3172-42b6-9cc0-4a971ab49c1c)  
  
  
