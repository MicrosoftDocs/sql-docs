---
title: "Disallowed Types and Members in System.Data.dll | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: clr
ms.topic: "reference"
helpviewer_keywords: 
  - "host protection attributes [CLR integration]"
  - "common language runtime [SQL Server], host protection attributes"
ms.assetid: ee5f55e9-fbef-401a-be18-a2e5873c8720
author: rothja
ms.author: jroth
manager: craigg
---
# Disallowed Types and Members in System.Data.dll
  [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] common language integration (CLR) programming disallows the use of a type or member that has a `HostProtectionAttribute` that specifies a `System.Security.Permissions.HostProtectionResource` enumeration with a value of `ExternalProcessMgmt`, `ExternalThreading`, `MayLeakOnAbort`, `SecurityInfrastructure`, `SelfAffectingProcessMgmnt`, `SelfAffectingThreading`, **SharedState**, `Synchronization`, or `UI`. The following table lists the members and types of the System.Data.dll assembly whose Host Protection Attribute (HPA) values are disallowed.  
  
> [!NOTE]  
>  This list was generated from the supported assemblies. For more information, see [Supported .NET Framework Libraries](../clr-integration/database-objects/supported-net-framework-libraries.md).  
  
|Type or Member|HPA Value(s)|  
|--------------------|--------------------|  
|System.Data.SqlClient.SqlCommand.BeginExecuteNonQuery()|ExternalThreading|  
|System.Data.SqlClient.SqlCommand.BeginExecuteReader()|ExternalThreading|  
|System.Data.SqlClient.SqlCommand.BeginExecuteXmlReader()|ExternalThreading|  
|System.Data.SqlClient.SqlDependency..ctor()|ExternalThreading|  
|System.Data.SqlClient.SqlDependency.Start()|ExternalThreading|  
|System.Data.SqlClient.SqlDependency.Stop()|ExternalThreading|  
|System.Data.TypedDataSetGenerator|SharedState, Synchronization|  
|System.Xml.XmlDataDocument|Synchronization|  
  
## See Also  
 [Host Protection Attributes and CLR Integration Programming](host-protection-attributes-and-clr-integration-programming.md)   
 [Disallowed Types and Members in Microsoft.VisualBasic.dll](disallowed-types-and-members-in-microsoft-visualbasic-dll.md)   
 [Disallowed Types and Members in mscorlib.dll](disallowed-types-and-members-in-mscorlib-dll.md)   
 [Disallowed Types and Members in System.dll](disallowed-types-and-members-in-system-dll.md)   
 [Disallowed Types and Members in System.Core.dll](disallowed-types-and-members-in-system-core-dll.md)  
  
  
